Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsReports3
    Dim objDB As New DBHelper
    Dim objGen As New clsFASGeneral


    Dim dGrandOpnDebit As Double = 0
    Dim dGrandOpnCredit As Double = 0
    Dim dGrandTransDebit As Double = 0
    Dim dGrandTransCredit As Double = 0
    Dim dGrandCloseDebit As Double = 0
    Dim dGrandCloseCredit As Double = 0

    'Public Function LoadTrialBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
    '    Dim sSql As String = "", aSql As String = "", iSql As String = "", mSql As String = ""
    '    Dim dt As New DataTable, dtGL As New DataTable, dtTrans As New DataTable, dtOB As New DataTable
    '    Dim dtSubGL As New DataTable
    '    Dim i As Integer = 0, j As Integer = 0
    '    Dim dRow As DataRow

    '    Dim dOpnDebit As Double = 0, dOpnCredit As Double = 0
    '    Dim dTransDebit As Double = 0, dTransCredit As Double = 0

    '    Dim dTotalOpnDebit As Double = 0, dTotalOpnCredit As Double = 0
    '    Dim dTotalTransDebit As Double = 0, dTotalTransCredit As Double = 0
    '    Dim dTotalCloseDebit As Double = 0, dTotalCloseCredit As Double = 0

    '    Dim dGrandTotalOpnDebit As Double = 0 : Dim dGrandTotalOpnCredit As Double = 0
    '    Dim dGrandTotalTransDebit As Double = 0, dGrandTotalTransCredit As Double = 0
    '    Dim dGrandTotalCloseDebit As Double = 0, dGrandTotalCloseCredit As Double = 0
    '    Try
    '        dt.Columns.Add("gl_id")
    '        dt.Columns.Add("gl_Code")
    '        dt.Columns.Add("gl_Desc")
    '        dt.Columns.Add("gl_Head")
    '        dt.Columns.Add("gl_AccHead")
    '        dt.Columns.Add("gl_Parent")
    '        dt.Columns.Add("OpDebit")
    '        dt.Columns.Add("OpCredit")
    '        dt.Columns.Add("TransDebit")
    '        dt.Columns.Add("TransCredit")
    '        dt.Columns.Add("ClosingDebit")
    '        dt.Columns.Add("ClosingCredit")

    '        sSql = "" : sSql = "Select * from chart_of_Accounts A join acc_Opening_Balance B on A.gl_id = B.opn_glID and  "
    '        sSql = sSql & "A.gl_head = 2 and A.gl_Delflag ='C'  and A.gl_Status = 'A' and B.opn_yearID = " & iYearId & " order by A.gl_glcode"
    '        dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dtGL.Rows.Count > 0 Then
    '            For i = 0 To dtGL.Rows.Count - 1
    '                dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0
    '                dTotalOpnDebit = 0 : dTotalOpnCredit = 0 : dTotalTransDebit = 0 : dTotalTransCredit = 0
    '                dTotalCloseDebit = 0 : dTotalCloseCredit = 0
    '                dRow = dt.NewRow()

    '                If IsDBNull(dtGL.Rows(i)("gl_id").ToString()) = False Then
    '                    dRow("gl_id") = dtGL.Rows(i)("gl_id").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
    '                    dRow("gl_Code") = dtGL.Rows(i)("gl_glCode").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
    '                    dRow("gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("gl_head").ToString()) = False Then
    '                    dRow("gl_Head") = dtGL.Rows(i)("gl_head").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("gl_Parent").ToString()) = False Then
    '                    dRow("gl_Parent") = dtGL.Rows(i)("gl_Parent").ToString()
    '                End If


    '                If IsDBNull(dtGL.Rows(i)("Opn_DebitAmt").ToString()) = False Then
    '                    dRow("OpDebit") = dtGL.Rows(i)("Opn_DebitAmt").ToString()
    '                    dOpnDebit = dtGL.Rows(i)("Opn_DebitAmt").ToString()
    '                    dTotalOpnDebit = dTotalOpnDebit + dtGL.Rows(i)("Opn_DebitAmt").ToString()
    '                    dGrandTotalOpnDebit = dGrandTotalOpnDebit + dtGL.Rows(i)("Opn_DebitAmt").ToString()
    '                Else
    '                    dRow("OpDebit") = "0.00"
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("Opn_CreditAmount").ToString()) = False Then
    '                    dRow("OpCredit") = dtGL.Rows(i)("Opn_CreditAmount").ToString()
    '                    dOpnCredit = dtGL.Rows(i)("Opn_CreditAmount").ToString()
    '                    dTotalOpnCredit = dTotalOpnCredit + dtGL.Rows()("Opn_CreditAmount").ToString()
    '                    dGrandTotalOpnCredit = dGrandTotalOpnCredit + dtGL.Rows(i)("Opn_CreditAmount").ToString()
    '                Else
    '                    dRow("OpCredit") = "0.00"
    '                End If

    '                aSql = "" : aSql = "Select Sum(ATD_Debit) as Debit ,sum(ATD_Credit) as Credit from Acc_Transactions_Details where ATD_GL = '" & dtGL.Rows(i)("gl_id").ToString() & "' and ATD_YearID =" & iYearId & " and ATD_CompID=" & iCompID & ""
    '                dtTrans = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
    '                If dtTrans.Rows.Count > 0 Then
    '                    If IsDBNull(dtTrans.Rows(0)("Debit").ToString()) = False Then
    '                        If dtTrans.Rows(0)("Debit").ToString() <> "" Then
    '                            dRow("TransDebit") = dtTrans.Rows(0)("Debit").ToString()
    '                            dTransDebit = dtTrans.Rows(0)("Debit").ToString()
    '                            dTotalTransDebit = dTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
    '                            dGrandTotalTransDebit = dGrandTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
    '                        Else
    '                            dRow("TransDebit") = "0.00"
    '                        End If
    '                    Else
    '                        dRow("TransDebit") = "0.00"
    '                    End If

    '                    If IsDBNull(dtTrans.Rows(0)("Credit").ToString()) = False Then

    '                        If dtTrans.Rows(0)("Credit").ToString() <> "" Then
    '                            dRow("TransCredit") = dtTrans.Rows(0)("Credit").ToString()
    '                            dTransCredit = dtTrans.Rows(0)("Credit").ToString()
    '                            dTotalTransCredit = dTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
    '                            dGrandTotalTransCredit = dGrandTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
    '                        Else
    '                            dRow("TransCredit") = "0.00"
    '                        End If
    '                    Else
    '                        dRow("TransCredit") = "0.00"
    '                    End If
    '                Else
    '                    dRow("TransCredit") = "0.00"
    '                    dRow("TransCredit") = "0.00"
    '                End If

    '                Dim dClosingDborCr As Double = 0
    '                dClosingDborCr = (dOpnDebit + dTransDebit) - (dOpnCredit + dTransCredit)
    '                If dClosingDborCr > 0 Then
    '                    dRow("ClosingDebit") = dClosingDborCr
    '                    dRow("ClosingCredit") = "0.00"
    '                    dTotalCloseDebit = dTotalCloseDebit + dRow("ClosingDebit")
    '                    dGrandTotalCloseDebit = dGrandTotalCloseDebit + dClosingDborCr
    '                Else
    '                    dRow("ClosingCredit") = dClosingDborCr
    '                    dRow("ClosingDebit") = "0.00"
    '                    dTotalCloseCredit = dTotalCloseCredit + dRow("ClosingCredit")
    '                    dGrandTotalCloseCredit = dGrandTotalCloseCredit + dRow("ClosingCredit")
    '                End If
    '                dt.Rows.Add(dRow)

    '                '' SUb GL
    '                mSql = "" : mSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGL.Rows(i)("gl_id").ToString() & " and gl_CompID =" & iCompID & " and "
    '                mSql = mSql & "gl_Delflag='C' and gl_Status ='A' Order by gl_glcode"
    '                dtSubGL = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
    '                If dtSubGL.Rows.Count > 0 Then
    '                    For j = 0 To dtSubGL.Rows.Count - 1
    '                        dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSubGL.Rows(j)("gl_id").ToString()) = False Then
    '                            dRow("gl_id") = dtSubGL.Rows(j)("gl_id").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_glCode").ToString()) = False Then
    '                            dRow("gl_Code") = dtSubGL.Rows(j)("gl_glCode").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("Gl_Desc").ToString()) = False Then
    '                            dRow("gl_Desc") = dtSubGL.Rows(j)("Gl_Desc").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_head").ToString()) = False Then
    '                            dRow("gl_Head") = dtSubGL.Rows(j)("gl_head").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_Parent").ToString()) = False Then
    '                            dRow("gl_Parent") = dtSubGL.Rows(j)("gl_Parent").ToString()
    '                        End If


    '                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLCode ='" & dtSubGL.Rows(j)("gl_glCode").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
    '                        dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
    '                        If dtOB.Rows.Count > 0 Then

    '                            If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
    '                                dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                                dOpnDebit = dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                                dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                                dGrandTotalOpnDebit = dGrandTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                            Else
    '                                dRow("OpDebit") = "0.00"
    '                            End If

    '                            If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
    '                                dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                                dOpnCredit = dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                                dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                                dGrandTotalOpnCredit = dGrandTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                            Else
    '                                dRow("OpCredit") = "0.00"
    '                            End If
    '                        End If

    '                        aSql = "" : aSql = "Select Sum(ATD_Debit) as Debit ,sum(ATD_Credit) as Credit from Acc_Transactions_Details where ATD_SubGL = '" & dtSubGL.Rows(j)("gl_id").ToString() & "' and ATD_YearID =" & iYearId & " and ATD_CompID=" & iCompID & ""
    '                        dtTrans = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
    '                        If dtTrans.Rows.Count > 0 Then
    '                            If IsDBNull(dtTrans.Rows(0)("Debit").ToString()) = False Then
    '                                If dtTrans.Rows(0)("Debit").ToString() <> "" Then
    '                                    dRow("TransDebit") = dtTrans.Rows(0)("Debit").ToString()
    '                                    dTransDebit = dtTrans.Rows(0)("Debit").ToString()
    '                                    dTotalTransDebit = dTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
    '                                    dGrandTotalTransDebit = dGrandTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
    '                                Else
    '                                    dRow("TransDebit") = "0.00"
    '                                End If
    '                            Else
    '                                dRow("TransDebit") = "0.00"
    '                            End If

    '                            If IsDBNull(dtTrans.Rows(0)("Credit").ToString()) = False Then
    '                                If dtTrans.Rows(0)("Credit").ToString() <> "" Then
    '                                    dRow("TransCredit") = dtTrans.Rows(0)("Credit").ToString()
    '                                    dTransCredit = dtTrans.Rows(0)("Credit").ToString()
    '                                    dTotalTransCredit = dTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
    '                                    dGrandTotalTransCredit = dGrandTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
    '                                Else
    '                                    dRow("TransCredit") = "0.00"
    '                                End If
    '                            Else
    '                                dRow("TransCredit") = "0.00"
    '                            End If
    '                        Else
    '                            dRow("TransCredit") = "0.00"
    '                            dRow("TransCredit") = "0.00"
    '                        End If

    '                        dClosingDborCr = (dOpnDebit + dTransDebit) - (dOpnCredit + dTransCredit)
    '                        If dClosingDborCr > 0 Then
    '                            dRow("ClosingDebit") = dClosingDborCr
    '                            dRow("ClosingCredit") = "0.00"
    '                            dTotalCloseDebit = dTotalCloseDebit + dRow("ClosingDebit")
    '                            dGrandTotalCloseDebit = dGrandTotalCloseDebit + dRow("ClosingDebit")
    '                        Else
    '                            dRow("ClosingCredit") = dClosingDborCr
    '                            dRow("ClosingDebit") = "0.00"
    '                            dTotalCloseCredit = dTotalCloseCredit + dRow("ClosingCredit")
    '                            dGrandTotalCloseCredit = dGrandTotalCloseCredit + dRow("ClosingCredit")
    '                        End If

    '                        dt.Rows.Add(dRow)
    '                    Next

    '                    If j = dtSubGL.Rows.Count Then
    '                        dRow = dt.NewRow()
    '                        dRow("gl_Desc") = "<B>" & "TOTAL" & "</B>"
    '                        dRow("OpDebit") = dTotalOpnDebit
    '                        dRow("OpCredit") = dTotalOpnCredit
    '                        dRow("TransDebit") = dTotalTransDebit
    '                        dRow("TransCredit") = dTotalTransCredit
    '                        dRow("ClosingDebit") = dTotalCloseDebit
    '                        dRow("ClosingCredit") = dTotalCloseCredit
    '                        dt.Rows.Add(dRow)
    '                    End If
    '                End If
    '            Next
    '        End If

    '        If dtGL.Rows.Count > 0 Then
    '            dRow = dt.NewRow()
    '            dRow("gl_Desc") = "<B>" & "GRAND TOTAL" & "</B>"
    '            dRow("OpDebit") = dGrandTotalOpnDebit
    '            dRow("OpCredit") = dGrandTotalOpnCredit
    '            dRow("TransDebit") = dGrandTotalTransDebit
    '            dRow("TransCredit") = dGrandTotalTransCredit
    '            dRow("ClosingDebit") = dGrandTotalCloseDebit
    '            dRow("ClosingCredit") = dGrandTotalCloseCredit
    '            dt.Rows.Add(dRow)
    '        End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    'Public Function LoadTrialBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
    '    Dim sSql As String = "", aSql As String = "", iSql As String = ""
    '    Dim mSql As String = "", kSql As String = "", lSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dtGL As New DataTable, dtSubGL As New DataTable
    '    Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
    '    Dim dRow As DataRow
    '    Dim dtOB As New DataTable
    '    Dim dtTrans As New DataTable
    '    Dim sStr As String = ""
    '    Dim dOpnDebit As Double = 0, dOpnCredit As Double = 0
    '    Dim dTransDebit As Double = 0, dTransCredit As Double = 0
    '    Dim iNext As Integer = 0
    '    Dim dTotalOpnDebit As Double = 0, dTotalOpnCredit As Double = 0
    '    Dim dTotalTransDebit As Double = 0, dTotalTransCredit As Double = 0
    '    Dim dTotalCloseDebit As Double = 0, dTotalCloseCredit As Double = 0

    '    Dim dGrandTotalOpnDebit As Double = 0 : Dim dGrandTotalOpnCredit As Double = 0
    '    Dim dGrandTotalTransDebit As Double = 0, dGrandTotalTransCredit As Double = 0
    '    Dim dGrandTotalCloseDebit As Double = 0, dGrandTotalCloseCredit As Double = 0
    '    Try
    '        dt.Columns.Add("gl_id")
    '        dt.Columns.Add("gl_Code")
    '        dt.Columns.Add("gl_Desc")
    '        dt.Columns.Add("gl_Head")
    '        dt.Columns.Add("gl_AccHead")
    '        dt.Columns.Add("gl_Parent")
    '        dt.Columns.Add("OpDebit")
    '        dt.Columns.Add("OpCredit")
    '        dt.Columns.Add("TransDebit")
    '        dt.Columns.Add("TransCredit")
    '        dt.Columns.Add("ClosingDebit")
    '        dt.Columns.Add("ClosingCredit")


    '        sSql = "" : sSql = "Select A.gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
    '        sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
    '        sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
    '        sSql = sSql & "and A.gl_head in (2) and A.gl_Delflag='C' and A.gl_Status ='A' order by A.gl_glcode"
    '        dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dtGL.Rows.Count > 0 Then
    '            For i = 0 To dtGL.Rows.Count - 1
    '                dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0
    '                dTotalOpnDebit = 0 : dTotalOpnCredit = 0  'dTotalTransDebit = 0 : dTotalTransCredit = 0
    '                dTotalCloseDebit = 0 : dTotalCloseCredit = 0
    '                dRow = dt.NewRow()

    '                If IsDBNull(dtGL.Rows(i)("gl_id").ToString()) = False Then
    '                    dRow("gl_id") = dtGL.Rows(i)("gl_id").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
    '                    dRow("gl_Code") = dtGL.Rows(i)("gl_glCode").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
    '                    dRow("gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("gl_head").ToString()) = False Then
    '                    dRow("gl_Head") = dtGL.Rows(i)("gl_head").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("gl_Parent").ToString()) = False Then
    '                    dRow("gl_Parent") = dtGL.Rows(i)("gl_Parent").ToString()
    '                End If

    '                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtGL.Rows(i)("gl_id").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
    '                dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
    '                If dtOB.Rows.Count > 0 Then

    '                    If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
    '                        dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                        dOpnDebit = dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                        dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                        dGrandTotalOpnDebit = dGrandTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                    Else
    '                        dRow("OpDebit") = "0.00"
    '                    End If

    '                    If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
    '                        dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                        dOpnCredit = dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                        dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                        dGrandTotalOpnCredit = dGrandTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                    Else
    '                        dRow("OpCredit") = "0.00"
    '                    End If
    '                End If


    '                aSql = "" : aSql = "Select Sum(ATD_Debit) as Debit ,sum(ATD_Credit) as Credit from Acc_Transactions_Details where "
    '                aSql = aSql & "ATD_GL = '" & dtGL.Rows(i)("gl_id").ToString() & "' and ATD_YearID =" & iYearId & " and ATD_CompID=" & iCompID & ""
    '                dtTrans = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
    '                If dtTrans.Rows.Count > 0 Then
    '                    If IsDBNull(dtTrans.Rows(0)("Debit").ToString()) = False Then
    '                        If dtTrans.Rows(0)("Debit").ToString() <> "" Then
    '                            dRow("TransDebit") = dtTrans.Rows(0)("Debit").ToString()
    '                            dTransDebit = dtTrans.Rows(0)("Debit").ToString()
    '                            dTotalTransDebit = dTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
    '                            dGrandTotalTransDebit = dGrandTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
    '                        Else
    '                            dRow("TransDebit") = "0.00"
    '                        End If
    '                    Else
    '                        dRow("TransDebit") = "0.00"
    '                    End If

    '                    If IsDBNull(dtTrans.Rows(0)("Credit").ToString()) = False Then

    '                        If dtTrans.Rows(0)("Credit").ToString() <> "" Then
    '                            dRow("TransCredit") = dtTrans.Rows(0)("Credit").ToString()
    '                            dTransCredit = dtTrans.Rows(0)("Credit").ToString()
    '                            dTotalTransCredit = dTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
    '                            dGrandTotalTransCredit = dGrandTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
    '                        Else
    '                            dRow("TransCredit") = "0.00"
    '                        End If
    '                    Else
    '                        dRow("TransCredit") = "0.00"
    '                    End If
    '                Else
    '                    dRow("TransCredit") = "0.00"
    '                    dRow("TransCredit") = "0.00"
    '                End If

    '                Dim dClosingDborCr As Double = 0
    '                dClosingDborCr = (dOpnDebit + dTransDebit) - (dOpnCredit + dTransCredit)
    '                If dClosingDborCr > 0 Then
    '                    dRow("ClosingDebit") = dClosingDborCr
    '                    dRow("ClosingCredit") = "0.00"
    '                    dTotalCloseDebit = dTotalCloseDebit + dRow("ClosingDebit")
    '                    dGrandTotalCloseDebit = dGrandTotalCloseDebit + dClosingDborCr
    '                Else
    '                    dRow("ClosingCredit") = dClosingDborCr
    '                    dRow("ClosingDebit") = "0.00"
    '                    dTotalCloseCredit = dTotalCloseCredit + dRow("ClosingCredit")
    '                    dGrandTotalCloseCredit = dGrandTotalCloseCredit + dRow("ClosingCredit")
    '                End If
    '                dt.Rows.Add(dRow)


    '                '' SUb GL
    '                mSql = "" : mSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGL.Rows(i)("gl_id").ToString() & " and gl_CompID =" & iCompID & " and "
    '                mSql = mSql & "gl_Delflag='C' and gl_Status ='A' Order by gl_glcode"
    '                dtSubGL = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
    '                If dtSubGL.Rows.Count > 0 Then
    '                    For j = 0 To dtSubGL.Rows.Count - 1
    '                        dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0
    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSubGL.Rows(j)("gl_id").ToString()) = False Then
    '                            dRow("gl_id") = dtSubGL.Rows(j)("gl_id").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_glCode").ToString()) = False Then
    '                            dRow("gl_Code") = dtSubGL.Rows(j)("gl_glCode").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("Gl_Desc").ToString()) = False Then
    '                            dRow("gl_Desc") = dtSubGL.Rows(j)("Gl_Desc").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_head").ToString()) = False Then
    '                            dRow("gl_Head") = dtSubGL.Rows(j)("gl_head").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_Parent").ToString()) = False Then
    '                            dRow("gl_Parent") = dtSubGL.Rows(j)("gl_Parent").ToString()
    '                        End If


    '                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtSubGL.Rows(j)("gl_id").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
    '                        dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
    '                        If dtOB.Rows.Count > 0 Then

    '                            If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
    '                                dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                                dOpnDebit = dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                                dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                                dGrandTotalOpnDebit = dGrandTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                            Else
    '                                dRow("OpDebit") = "0.00"
    '                            End If

    '                            If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
    '                                dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                                dOpnCredit = dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                                dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                                dGrandTotalOpnCredit = dGrandTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                            Else
    '                                dRow("OpCredit") = "0.00"
    '                            End If
    '                        End If

    '                        aSql = "" : aSql = "Select Sum(ATD_Debit) as Debit ,sum(ATD_Credit) as Credit from Acc_Transactions_Details where ATD_SubGL = '" & dtSubGL.Rows(j)("gl_id").ToString() & "' and ATD_YearID =" & iYearId & " and ATD_CompID=" & iCompID & ""
    '                        dtTrans = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
    '                        If dtTrans.Rows.Count > 0 Then
    '                            If IsDBNull(dtTrans.Rows(0)("Debit").ToString()) = False Then
    '                                If dtTrans.Rows(0)("Debit").ToString() <> "" Then
    '                                    dRow("TransDebit") = dtTrans.Rows(0)("Debit").ToString()
    '                                    '  dTransDebit = dtTrans.Rows(0)("Debit").ToString()
    '                                    ' dTotalTransDebit = dTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
    '                                    dGrandTotalTransDebit = dGrandTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
    '                                Else
    '                                    dRow("TransDebit") = "0.00"
    '                                End If
    '                            Else
    '                                dRow("TransDebit") = "0.00"
    '                            End If

    '                            If IsDBNull(dtTrans.Rows(0)("Credit").ToString()) = False Then
    '                                If dtTrans.Rows(0)("Credit").ToString() <> "" Then
    '                                    dRow("TransCredit") = dtTrans.Rows(0)("Credit").ToString()
    '                                    ' dTransCredit = dtTrans.Rows(0)("Credit").ToString()
    '                                    ' dTotalTransCredit = dTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
    '                                    dGrandTotalTransCredit = dGrandTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
    '                                Else
    '                                    dRow("TransCredit") = "0.00"
    '                                End If
    '                            Else
    '                                dRow("TransCredit") = "0.00"
    '                            End If
    '                        Else
    '                            dRow("TransCredit") = "0.00"
    '                            dRow("TransCredit") = "0.00"
    '                        End If

    '                        dClosingDborCr = (dOpnDebit + dTransDebit) - (dOpnCredit + dTransCredit)
    '                        If dClosingDborCr > 0 Then
    '                            dRow("ClosingDebit") = dClosingDborCr
    '                            dRow("ClosingCredit") = "0.00"
    '                            dTotalCloseDebit = dTotalCloseDebit + dRow("ClosingDebit")
    '                            dGrandTotalCloseDebit = dGrandTotalCloseDebit + dRow("ClosingDebit")
    '                        Else
    '                            dRow("ClosingCredit") = dClosingDborCr
    '                            dRow("ClosingDebit") = "0.00"
    '                            dTotalCloseCredit = dTotalCloseCredit + dRow("ClosingCredit")
    '                            dGrandTotalCloseCredit = dGrandTotalCloseCredit + dRow("ClosingCredit")
    '                        End If

    '                        dt.Rows.Add(dRow)
    '                    Next

    '                    If j = dtSubGL.Rows.Count Then
    '                        dRow = dt.NewRow()
    '                        dRow("gl_Desc") = "<B>" & "TOTAL" & "</B>"
    '                        dRow("OpDebit") = dTotalOpnDebit
    '                        dRow("OpCredit") = dTotalOpnCredit
    '                        dRow("TransDebit") = dTotalTransDebit
    '                        dRow("TransCredit") = dTotalTransCredit
    '                        dRow("ClosingDebit") = dTotalCloseDebit
    '                        dRow("ClosingCredit") = dTotalCloseCredit
    '                        dt.Rows.Add(dRow)
    '                    End If
    '                End If
    '            Next
    '        End If

    '        If dtGL.Rows.Count > 0 Then
    '            dRow = dt.NewRow()
    '            dRow("gl_Desc") = "<B>" & "GRAND TOTAL" & "</B>"
    '            dRow("OpDebit") = dGrandTotalOpnDebit
    '            dRow("OpCredit") = dGrandTotalOpnCredit
    '            dRow("TransDebit") = dGrandTotalTransDebit
    '            dRow("TransCredit") = dGrandTotalTransCredit
    '            dRow("ClosingDebit") = dGrandTotalCloseDebit
    '            dRow("ClosingCredit") = dGrandTotalCloseCredit
    '            dt.Rows.Add(dRow)
    '        End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function LoadTrialBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = "", aSql As String = "", iSql As String = ""
        Dim mSql As String = "", kSql As String = "", lSql As String = ""
        Dim dt As New DataTable
        Dim dtGL As New DataTable, dtSubGL As New DataTable
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
        Dim dRow As DataRow
        Dim dtOB As New DataTable
        Dim dtTrans As New DataTable
        Dim sStr As String = ""
        Dim dOpnDebit As Double = 0, dOpnCredit As Double = 0
        Dim dTransDebit As Double = 0, dTransCredit As Double = 0
        Dim dCloseDebit As Double = 0, dCloseCredit As Double = 0
        Dim iNext As Integer = 0
        Dim dTotalOpnDebit As Double = 0, dTotalOpnCredit As Double = 0
        Dim dTotalTransDebit As Double = 0, dTotalTransCredit As Double = 0
        Dim dTotalCloseDebit As Double = 0, dTotalCloseCredit As Double = 0

        Dim dGrandTotalOpnDebit As Double = 0 : Dim dGrandTotalOpnCredit As Double = 0
        Dim dGrandTotalTransDebit As Double = 0, dGrandTotalTransCredit As Double = 0
        Dim dGrandTotalCloseDebit As Double = 0, dGrandTotalCloseCredit As Double = 0
        Try
            dt.Columns.Add("gl_id")
            dt.Columns.Add("gl_Code")
            dt.Columns.Add("gl_Desc")
            dt.Columns.Add("gl_Head")
            dt.Columns.Add("gl_AccHead")
            dt.Columns.Add("gl_Parent")
            dt.Columns.Add("OpDebit")
            dt.Columns.Add("OpCredit")
            dt.Columns.Add("TransDebit")
            dt.Columns.Add("TransCredit")
            dt.Columns.Add("ClosingDebit")
            dt.Columns.Add("ClosingCredit")


            sSql = "" : sSql = "Select A.gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
            sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
            sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
            sSql = sSql & "and A.gl_head in (2) and A.gl_Delflag='C' and A.gl_Status ='A' order by A.gl_glcode"
            dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGL.Rows.Count > 0 Then
                For i = 0 To dtGL.Rows.Count - 1
                    dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0
                    dTotalOpnDebit = 0 : dTotalOpnCredit = 0 : dTotalTransDebit = 0 : dTotalTransCredit = 0
                    dTotalCloseDebit = 0 : dTotalCloseCredit = 0
                    dRow = dt.NewRow()

                    If IsDBNull(dtGL.Rows(i)("gl_id").ToString()) = False Then
                        dRow("gl_id") = dtGL.Rows(i)("gl_id").ToString()
                    End If

                    If dtGL.Rows(i)("gl_glCode").ToString() = "A020301" Then
                        dRow("gl_id") = ""
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
                        dRow("gl_Code") = dtGL.Rows(i)("gl_glCode").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
                        dRow("gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_head").ToString()) = False Then
                        dRow("gl_Head") = dtGL.Rows(i)("gl_head").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_Parent").ToString()) = False Then
                        dRow("gl_Parent") = dtGL.Rows(i)("gl_Parent").ToString()
                    End If

                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtGL.Rows(i)("gl_id").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
                    dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                    If dtOB.Rows.Count > 0 Then

                        If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
                            dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                            dOpnDebit = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                            dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
                            dGrandTotalOpnDebit = dGrandTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
                        Else
                            dRow("OpDebit") = "0.00"
                        End If

                        If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
                            dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                            dOpnCredit = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                            dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
                            dGrandTotalOpnCredit = dGrandTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
                        Else
                            dRow("OpCredit") = "0.00"
                        End If
                    End If


                    aSql = "" : aSql = "Select Sum(ATD_Debit) as Debit ,sum(ATD_Credit) as Credit from Acc_Transactions_Details where "
                    aSql = aSql & "ATD_GL = '" & dtGL.Rows(i)("gl_id").ToString() & "' and ATD_YearID =" & iYearId & " and ATD_CompID=" & iCompID & ""
                    dtTrans = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtTrans.Rows.Count > 0 Then
                        If IsDBNull(dtTrans.Rows(0)("Debit").ToString()) = False Then
                            If dtTrans.Rows(0)("Debit").ToString() <> "" Then
                                dRow("TransDebit") = dtTrans.Rows(0)("Debit").ToString()
                                dTransDebit = dtTrans.Rows(0)("Debit").ToString()
                                'dTotalTransDebit = dTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
                                dGrandTotalTransDebit = dGrandTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
                            Else
                                dRow("TransDebit") = "0.00"
                            End If
                        Else
                            dRow("TransDebit") = "0.00"
                        End If

                        If IsDBNull(dtTrans.Rows(0)("Credit").ToString()) = False Then

                            If dtTrans.Rows(0)("Credit").ToString() <> "" Then
                                dRow("TransCredit") = dtTrans.Rows(0)("Credit").ToString()
                                dTransCredit = dtTrans.Rows(0)("Credit").ToString()
                                'dTotalTransCredit = dTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
                                dGrandTotalTransCredit = dGrandTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
                            Else
                                dRow("TransCredit") = "0.00"
                            End If
                        Else
                            dRow("TransCredit") = "0.00"
                        End If
                    Else
                        dRow("TransCredit") = "0.00"
                        dRow("TransCredit") = "0.00"
                    End If

                    Dim dClosingDborCr As Double = 0
                    dClosingDborCr = (dOpnDebit + dTransDebit) - (dOpnCredit + dTransCredit)
                    If dClosingDborCr > 0 Then
                        dRow("ClosingDebit") = dClosingDborCr
                        dRow("ClosingCredit") = "0.00"
                        dTotalCloseDebit = dTotalCloseDebit + dRow("ClosingDebit")
                        dGrandTotalCloseDebit = dGrandTotalCloseDebit + dClosingDborCr
                    Else
                        dRow("ClosingCredit") = dClosingDborCr
                        dRow("ClosingDebit") = "0.00"
                        dTotalCloseCredit = dTotalCloseCredit + dRow("ClosingCredit")
                        dGrandTotalCloseCredit = dGrandTotalCloseCredit + dRow("ClosingCredit")
                    End If
                    dt.Rows.Add(dRow)


                    '' SUb GL
                    mSql = "" : mSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGL.Rows(i)("gl_id").ToString() & " and gl_CompID =" & iCompID & " and "
                    mSql = mSql & "gl_Delflag='C' and gl_Status ='A' Order by gl_glcode"
                    dtSubGL = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                    If dtSubGL.Rows.Count > 0 Then
                        For j = 0 To dtSubGL.Rows.Count - 1
                            dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0
                            dRow = dt.NewRow()
                            If IsDBNull(dtSubGL.Rows(j)("gl_id").ToString()) = False Then
                                dRow("gl_id") = dtSubGL.Rows(j)("gl_id").ToString()
                            End If

                            If IsDBNull(dtSubGL.Rows(j)("gl_glCode").ToString()) = False Then
                                dRow("gl_Code") = dtSubGL.Rows(j)("gl_glCode").ToString()
                            End If

                            If IsDBNull(dtSubGL.Rows(j)("Gl_Desc").ToString()) = False Then
                                dRow("gl_Desc") = dtSubGL.Rows(j)("Gl_Desc").ToString()
                            End If

                            If IsDBNull(dtSubGL.Rows(j)("gl_head").ToString()) = False Then
                                dRow("gl_Head") = dtSubGL.Rows(j)("gl_head").ToString()
                            End If

                            If IsDBNull(dtSubGL.Rows(j)("gl_Parent").ToString()) = False Then
                                dRow("gl_Parent") = dtSubGL.Rows(j)("gl_Parent").ToString()
                            End If


                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtSubGL.Rows(j)("gl_id").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
                            dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                            If dtOB.Rows.Count > 0 Then

                                If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
                                    dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                                    dOpnDebit = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                                    dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
                                    dGrandTotalOpnDebit = dGrandTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
                                Else
                                    dRow("OpDebit") = "0.00"
                                End If

                                If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
                                    dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                                    dOpnCredit = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                                    dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
                                    dGrandTotalOpnCredit = dGrandTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
                                Else
                                    dRow("OpCredit") = "0.00"
                                End If
                            End If

                            aSql = "" : aSql = "Select Sum(ATD_Debit) as Debit ,sum(ATD_Credit) as Credit from Acc_Transactions_Details where ATD_SubGL = '" & dtSubGL.Rows(j)("gl_id").ToString() & "' and ATD_YearID =" & iYearId & " and ATD_CompID=" & iCompID & ""
                            dtTrans = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                            If dtTrans.Rows.Count > 0 Then
                                If IsDBNull(dtTrans.Rows(0)("Debit").ToString()) = False Then
                                    If dtTrans.Rows(0)("Debit").ToString() <> "" Then
                                        dRow("TransDebit") = dtTrans.Rows(0)("Debit").ToString()
                                        dTransDebit = dtTrans.Rows(0)("Debit").ToString()
                                        dTotalTransDebit = dTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
                                        dGrandTotalTransDebit = dGrandTotalTransDebit + dtTrans.Rows(0)("Debit").ToString()
                                    Else
                                        dRow("TransDebit") = "0.00"
                                    End If
                                Else
                                    dRow("TransDebit") = "0.00"
                                End If

                                If IsDBNull(dtTrans.Rows(0)("Credit").ToString()) = False Then
                                    If dtTrans.Rows(0)("Credit").ToString() <> "" Then
                                        dRow("TransCredit") = dtTrans.Rows(0)("Credit").ToString()
                                        dTransCredit = dtTrans.Rows(0)("Credit").ToString()
                                        dTotalTransCredit = dTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
                                        dGrandTotalTransCredit = dGrandTotalTransCredit + dtTrans.Rows(0)("Credit").ToString()
                                    Else
                                        dRow("TransCredit") = "0.00"
                                    End If
                                Else
                                    dRow("TransCredit") = "0.00"
                                End If
                            Else
                                dRow("TransCredit") = "0.00"
                                dRow("TransCredit") = "0.00"
                            End If

                            dClosingDborCr = (dOpnDebit + dTransDebit) - (dOpnCredit + dTransCredit)
                            If dClosingDborCr > 0 Then
                                dRow("ClosingDebit") = dClosingDborCr
                                dRow("ClosingCredit") = "0.00"
                                ' dTotalCloseDebit = dTotalCloseDebit + dRow("ClosingDebit")
                                dTotalCloseDebit = dRow("ClosingDebit")
                                dGrandTotalCloseDebit = dGrandTotalCloseDebit + dRow("ClosingDebit")
                            Else
                                dRow("ClosingCredit") = dClosingDborCr
                                dRow("ClosingDebit") = "0.00"
                                ' dTotalCloseCredit = dTotalCloseCredit + dRow("ClosingCredit")
                                dTotalCloseCredit = dRow("ClosingCredit")
                                dGrandTotalCloseCredit = dGrandTotalCloseCredit + dRow("ClosingCredit")
                            End If

                            dt.Rows.Add(dRow)
                        Next

                        If j = dtSubGL.Rows.Count Then
                            dRow = dt.NewRow()
                            dRow("gl_Desc") = "<B>" & "TOTAL" & "</B>"
                            dRow("OpDebit") = dTotalOpnDebit
                            dRow("OpCredit") = dTotalOpnCredit
                            dRow("TransDebit") = dTotalTransDebit
                            dRow("TransCredit") = dTotalTransCredit
                            dRow("ClosingDebit") = dTotalCloseDebit
                            dRow("ClosingCredit") = dTotalCloseCredit
                            dt.Rows.Add(dRow)
                        End If
                    End If
                Next
            End If

            If dt.Rows.Count > 0 Then
                dOpnDebit = 0 : dOpnCredit = 0
                dTransDebit = 0 : dTransCredit = 0
                dCloseDebit = 0 : dCloseCredit = 0
                For l = 0 To dt.Rows.Count - 1
                    If dt.Rows(l)("gl_Head").ToString() = "2" Then
                        If dt.Rows(l)("OpDebit").ToString() <> "" Then
                            dOpnDebit = dOpnDebit + dt.Rows(l)("OpDebit").ToString()
                        Else
                            dOpnDebit = dOpnDebit + 0
                        End If

                        If dt.Rows(l)("OpCredit").ToString() <> "" Then
                            dOpnCredit = dOpnCredit + dt.Rows(l)("OpCredit").ToString()
                        Else
                            dOpnCredit = dOpnCredit + 0
                        End If

                        If dt.Rows(l)("TransDebit").ToString() <> "" Then
                            dTransDebit = dTransDebit + dt.Rows(l)("TransDebit").ToString()
                        Else
                            dTransDebit = dTransDebit + 0
                        End If

                        If dt.Rows(l)("TransCredit").ToString() <> "" Then
                            dTransCredit = dTransCredit + dt.Rows(l)("TransCredit").ToString()
                        Else
                            dTransCredit = dTransCredit + 0
                        End If

                        If dt.Rows(l)("ClosingDebit").ToString() <> "" Then
                            dCloseDebit = dCloseDebit + dt.Rows(l)("ClosingDebit").ToString()
                        Else
                            dCloseDebit = dCloseDebit + 0
                        End If

                        If dt.Rows(l)("ClosingCredit").ToString() <> "" Then
                            dCloseCredit = dCloseCredit + dt.Rows(l)("ClosingCredit").ToString()
                        Else
                            dCloseCredit = dCloseCredit + 0
                        End If

                    End If
                Next

                dRow = dt.NewRow
                dRow("gl_Desc") = "<B>" & "GRAND TOTAL" & "</B>"
                dRow("OpDebit") = dOpnDebit
                dRow("OpCredit") = dOpnCredit
                dRow("TransDebit") = dTransDebit
                dRow("TransCredit") = dTransCredit
                dRow("ClosingDebit") = dCloseDebit
                dRow("ClosingCredit") = dCloseCredit
                dt.Rows.Add(dRow)
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetYear(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYear As Integer) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sYear As String = ""
        Try
            sSql = "Select YMS_To_Year from acc_year_Master where YMS_ID = " & iYear & " and YMS_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sYear = dt.Rows(0)("YMS_To_Year").ToString()
            End If
            Return sYear
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCustomers(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sValue As String = ""
        Try
            sSql = "Select Cust_Name from MST_Customer_Master Where Cust_CompID =" & iCompID & ""
            sValue = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return sValue
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
        Dim dt As New DataTable, dtGL As New DataTable, dtOB As New DataTable
        Dim sSql As String = "", iSql As String = ""
        Dim i As Integer = 0, j As Integer = 0
        Dim dRow As DataRow
        Dim dDebit As Double = 0, dCredit As Double = 0, dBalance As Double = 0
        Dim dGrandDebitTotal As Double = 0, dGrandCreditTotal As Double = 0, dGrandBalanceTotal As Double = 0
        Try
            dt.Columns.Add("gl_id")
            dt.Columns.Add("gl_glCode")
            dt.Columns.Add("gl_Parent")
            dt.Columns.Add("gl_Desc")
            dt.Columns.Add("gl_Head")
            dt.Columns.Add("Opn_DebitAmt")
            dt.Columns.Add("Opn_CreditAmount")
            dt.Columns.Add("Balance")

            sSql = "" : sSql = "Select A.gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
            sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
            sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
            sSql = sSql & "and A.gl_head in (2,3) and A.gl_Delflag='C' and A.gl_Status ='A' order by A.gl_glcode"
            dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGL.Rows.Count > 0 Then
                For i = 0 To dtGL.Rows.Count - 1
                    dRow = dt.NewRow()

                    If IsDBNull(dtGL.Rows(i)("gl_id").ToString()) = False Then
                        dRow("gl_id") = dtGL.Rows(i)("gl_id").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
                        dRow("gl_glCode") = dtGL.Rows(i)("gl_glCode").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
                        dRow("gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_head").ToString()) = False Then
                        dRow("gl_Head") = dtGL.Rows(i)("gl_head").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_Parent").ToString()) = False Then
                        dRow("gl_Parent") = dtGL.Rows(i)("gl_Parent").ToString()
                    End If

                    If (dtGL.Rows(i)("gl_AccHead").ToString() = "1") Or (dtGL.Rows(i)("gl_AccHead").ToString() = "4") Then
                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtGL.Rows(i)("gl_id").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
                        dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                        If dtOB.Rows.Count > 0 Then
                            If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
                                dRow("Opn_DebitAmt") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                                dDebit = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                                dGrandDebitTotal = dGrandDebitTotal + dtOB.Rows(0)("Opn_DebitAmt").ToString()
                            Else
                                dRow("Opn_DebitAmt") = "0.00"
                            End If

                            If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
                                dRow("Opn_CreditAmount") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                                dCredit = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                                dGrandCreditTotal = dGrandCreditTotal + dtOB.Rows(0)("Opn_CreditAmount").ToString()
                            Else
                                dRow("Opn_CreditAmount") = "0.00"
                            End If

                            dBalance = dDebit - dCredit
                            If IsDBNull(dBalance) = False Then
                                dRow("Balance") = dBalance
                                dGrandBalanceTotal = dGrandBalanceTotal + dBalance
                            Else
                                dRow("Balance") = "0.00"
                            End If
                        End If
                    Else
                        dRow("Opn_DebitAmt") = "0.00"
                        dRow("Opn_CreditAmount") = "0.00"
                        dRow("Balance") = "0.00"
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If

            dRow = dt.NewRow()
            dRow("gl_Desc") = "<B>" & "GRAND TOTAL" & "</B>"
            dRow("Opn_DebitAmt") = dGrandDebitTotal
            dRow("Opn_CreditAmount") = dGrandCreditTotal
            dRow("Balance") = dGrandBalanceTotal
            dt.Rows.Add(dRow)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function GeneralLedgerWithDetail(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer)
    '    Dim sSql As String = "", aSql As String = "", iSql As String = ""
    '    Dim pSql As String = ""
    '    Dim dtGL As New DataTable
    '    Dim dtSubGL As New DataTable
    '    Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
    '    Dim dRow As DataRow
    '    Dim dtOB As New DataTable
    '    Dim sStr As String = ""
    '    Dim dTotalOpnDebit As Double = 0, dTotalOpnCredit As Double = 0
    '    Dim dTotalTransDebit As Double = 0, dTotalTransCredit As Double = 0
    '    Dim dTotalClsDebit As Double = 0, dTotalClsCredit As Double = 0
    '    Dim dtCheck As New DataTable
    '    Dim iTotalCheck As Integer = 0
    '    Dim dtPayment As New DataTable, dtReceipt As New DataTable, dtPettyCash As New DataTable, dtJE As New DataTable
    '    Dim dtFinal As New DataTable
    '    Dim dtDateSort As New DataTable

    '    Dim dTransDebit As Double = 0, dTransCredit As Double = 0
    '    Try
    '        sSql = "" : sSql = "Select A.gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
    '        sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
    '        sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
    '        sSql = sSql & "and A.gl_head in (2,3) and A.gl_CompID =" & iCompID & " and A.gl_Delflag='C' and A.gl_Status ='A' order by A.gl_glcode"
    '        dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dtGL.Rows.Count > 0 Then
    '            For i = 0 To dtGL.Rows.Count - 1
    '                Dim dt As New DataTable

    '                dt.Columns.Add("BillDate")
    '                dt.Columns.Add("gl_Code")
    '                dt.Columns.Add("TransactionType")
    '                dt.Columns.Add("gl_Desc")
    '                dt.Columns.Add("Narration")
    '                dt.Columns.Add("CorresGLCode")
    '                dt.Columns.Add("CorresDescription")
    '                dt.Columns.Add("OpDebit")
    '                dt.Columns.Add("OpCredit")
    '                dt.Columns.Add("TransDebit")
    '                dt.Columns.Add("TransCredit")
    '                dt.Columns.Add("ClosingDebit")
    '                dt.Columns.Add("ClosingCredit")
    '                dt.Columns.Add("Status")

    '                iTotalCheck = 0
    '                dTotalOpnDebit = 0 : dTotalOpnCredit = 0
    '                dTotalTransDebit = 0 : dTotalTransCredit = 0
    '                dTotalClsDebit = 0 : dTotalClsCredit = 0
    '                dRow = dt.NewRow()


    '                If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
    '                    dRow("gl_Code") = dtGL.Rows(i)("gl_glCode").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
    '                    dRow("Gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
    '                End If
    '                dRow("BillDate") = "01/01/1900"
    '                dRow("Status") = "0"

    '                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtGL.Rows(i)("gl_ID").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
    '                dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
    '                If dtOB.Rows.Count > 0 Then

    '                    If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
    '                        dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                        dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
    '                    Else
    '                        dRow("OpDebit") = "0.00"
    '                    End If

    '                    If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
    '                        dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                        dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
    '                    Else
    '                        dRow("OpCredit") = "0.00"
    '                    End If

    '                    If IsDBNull(dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString()) = False Then
    '                        If dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString() <> "" Then
    '                            dRow("ClosingDebit") = dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString()
    '                        Else
    '                            dRow("ClosingDebit") = "0.00"
    '                        End If
    '                    Else
    '                        dRow("ClosingDebit") = "0.00"
    '                    End If

    '                    If IsDBNull(dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString()) = False Then
    '                        If dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString() <> "" Then
    '                            dRow("ClosingCredit") = dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString()
    '                        Else
    '                            dRow("ClosingCredit") = "0.00"
    '                        End If
    '                    Else
    '                        dRow("ClosingCredit") = "0.00"
    '                    End If
    '                End If
    '                dt.Rows.Add(dRow)

    '                'Payment                    
    '                aSql = "" : aSql = "Select Convert(Char(10), B.Acc_PBD_BilLDate, 111) As BillDate,"
    '                aSql = aSql & "A.ATR_GLcode as GLCode,B.Acc_PBD_TransactionType as TransactionType,E.Gl_Desc as Descriptions,"
    '                aSql = aSql & "A.Atr_VoucherNo as VoucherNo,C.Acc_PT_ChequeNo as ChequeNo,C.Acc_PT_ChequeDate as ChequeDate,A.ATR_Party as Party,"
    '                aSql = aSql & "A.ATR_DBAmount as Debit,A.ATR_CrAmount as Credit,B.Acc_PBD_BillAmount as BillAmount,"
    '                aSql = aSql & "C.Acc_PT_ChequeAmount as ChequeAmount,C.Acc_PT_BankName as BankName,C.Acc_PT_BranchNane as BranchName,"
    '                aSql = aSql & "D.ATR_GlCode as CorresGLCode, F.Gl_Desc as CorresDescription "
    '                aSql = aSql & "from account_transactions A join Acc_Payment_BillDetails B ON A.ATR_BillID = B.Acc_pbd_ID and A.ATR_TrType = 1 "
    '                aSql = aSql & "and A.ATR_GlCode = '" & dtGL.Rows(i)("gl_glCode").ToString() & "' and A.ATR_YearID = " & iYearId & " and A.ATR_COmpID = " & iCompID & " "
    '                aSql = aSql & "join Acc_Payment_Transaction C On C.Acc_PT_MasterBillID = B.Acc_pbd_ID "
    '                aSql = aSql & "join account_transactions D on D.ATR_BillID = A.ATR_BillID and D.ATR_VoucherNo = A.ATR_VoucherNo and "
    '                aSql = aSql & "D.ATR_GLCode != A.ATR_GLCode "
    '                aSql = aSql & "Join Chart_of_Accounts E on E.gl_Glcode = A.ATR_GLCode "
    '                aSql = aSql & "Join Chart_of_Accounts F on F.gl_Glcode = D.ATR_GLCode order by BillDate Asc"
    '                dtPayment = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
    '                For j = 0 To dtPayment.Rows.Count - 1
    '                    dRow = dt.NewRow()
    '                    dRow("BillDate") = dtPayment.Rows(j)("BillDate").ToString() 'clsTRACeGeneral.FormatDtForRDBMS(dtPayment.Rows(j)("BillDate").ToString(), "DT")
    '                    dRow("TransactionType") = "Payment"
    '                    dRow("Narration") = dtPayment.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtPayment.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtPayment.Rows(j)("ChequeNo").ToString()
    '                    dRow("CorresGLCode") = dtPayment.Rows(j)("CorresGLcode").ToString()
    '                    dRow("CorresDescription") = dtPayment.Rows(j)("CorresDescription").ToString()

    '                    If dtPayment.Rows(j)("Debit").ToString() = "" Then
    '                        dRow("TransDebit") = "0.00" : dTransDebit = 0
    '                    Else
    '                        dRow("TransDebit") = dtPayment.Rows(j)("Debit").ToString()
    '                    End If

    '                    If dtPayment.Rows(j)("Credit").ToString() = "" Then
    '                        dRow("TransCredit") = "0.00" : dTransDebit = 0
    '                    Else
    '                        dRow("TransCredit") = dtPayment.Rows(j)("Credit").ToString()
    '                    End If
    '                    dRow("Status") = "1"
    '                    dt.Rows.Add(dRow)
    '                Next

    '                'PettyCash                   
    '                aSql = "" : aSql = "Select Convert(Char(10), B.Acc_PCB_BilLDate, 111) As BillDate,"
    '                aSql = aSql & "A.ATR_GLcode as GLCode,B.Acc_PCB_TransactionType as TransactionType,E.Gl_Desc as Descriptions,"
    '                aSql = aSql & "A.Atr_VoucherNo as VoucherNo,C.Acc_PCT_ChequeNo as ChequeNo,C.Acc_PCT_ChequeDate as ChequeDate,A.ATR_Party as Party,"
    '                aSql = aSql & "A.ATR_DBAmount as Debit,A.ATR_CrAmount as Credit,B.Acc_PCB_BillAmount as BillAmount,"
    '                aSql = aSql & "C.Acc_PCT_ChequeAmount as ChequeAmount,C.Acc_PCT_BankName as BankName,C.Acc_PCT_BranchName as BranchName,"
    '                aSql = aSql & "D.ATR_GlCode as CorresGLcode, F.Gl_Desc as CorresDescription "
    '                aSql = aSql & "from account_transactions A join Acc_PettyCash_BillDetails B ON A.ATR_BillID = B.Acc_pcb_ID and A.ATR_TrType = 2 "
    '                aSql = aSql & "and A.ATR_GlCode = '" & dtGL.Rows(i)("gl_glCode").ToString() & "' and A.ATR_YearID = " & iYearId & " and A.ATR_COmpID = " & iCompID & " "
    '                aSql = aSql & "join Acc_PettyCash_Transaction C On C.Acc_PCT_MasterBillID = B.Acc_pcb_ID "
    '                aSql = aSql & "join account_transactions D on D.ATR_BillID = A.ATR_BillID and D.ATR_VoucherNo = A.ATR_VoucherNo and "
    '                aSql = aSql & "D.ATR_GLCode != A.ATR_GLCode "
    '                aSql = aSql & "Join Chart_of_Accounts E on E.gl_Glcode = A.ATR_GLCode "
    '                aSql = aSql & "Join Chart_of_Accounts F on F.gl_Glcode = D.ATR_GLCode order by B.Acc_PCB_BilLDate Asc"
    '                dtPettyCash = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
    '                For j = 0 To dtPettyCash.Rows.Count - 1
    '                    dRow = dt.NewRow()
    '                    dRow("BillDate") = dtPettyCash.Rows(j)("BillDate").ToString() 'clsTRACeGeneral.FormatDtForRDBMS(dtPettyCash.Rows(j)("BillDate").ToString(), "DT")
    '                    dRow("TransactionType") = "Petty Cash"
    '                    ' dRow("gl_Desc") = dtPettyCash.Rows(j)("Descriptions").ToString()
    '                    dRow("Narration") = dtPettyCash.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtPettyCash.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtPettyCash.Rows(j)("ChequeNo").ToString()
    '                    dRow("CorresGLCode") = dtPettyCash.Rows(j)("CorresGLcode").ToString()
    '                    dRow("CorresDescription") = dtPettyCash.Rows(j)("CorresDescription").ToString()
    '                    ' dRow("OpDebit") = dtPettyCash.Rows(j)("CorresDescription").ToString()
    '                    ' dRow("OpCredit") = dtPettyCash.Rows(j)("CorresDescription").ToString()                       

    '                    If dtPettyCash.Rows(j)("Debit").ToString() = "" Then
    '                        dRow("TransDebit") = "0.00" : dTransDebit = 0
    '                    Else
    '                        dRow("TransDebit") = dtPettyCash.Rows(j)("Debit").ToString()
    '                    End If

    '                    If dtPettyCash.Rows(j)("Credit").ToString() = "" Then
    '                        dRow("TransCredit") = "0.00" : dTransDebit = 0
    '                    Else
    '                        dRow("TransCredit") = dtPettyCash.Rows(j)("Credit").ToString()
    '                    End If

    '                    dRow("Status") = "1"
    '                    dt.Rows.Add(dRow)
    '                Next


    '                'Receipt                   
    '                aSql = "" : aSql = "Select Convert(Char(10), B.Acc_RB_BilLDate, 111) As BillDate,"
    '                aSql = aSql & "A.ATR_GLcode as GLCode,B.Acc_RB_TransactionType as TransactionType,E.Gl_Desc as Descriptions,"
    '                aSql = aSql & "A.Atr_VoucherNo as VoucherNo,C.Acc_RT_ChequeNo as ChequeNo,C.Acc_RT_ChequeDate as ChequeDate,A.ATR_Party as Party,"
    '                aSql = aSql & "A.ATR_DBAmount as Debit,A.ATR_CrAmount as Credit,B.Acc_RB_BillAmount as BillAmount,"
    '                aSql = aSql & "C.Acc_RT_ChequeAmount as ChequeAmount,C.Acc_RT_BankName as BankName,C.Acc_RT_BranchNane as BranchName,"
    '                aSql = aSql & "D.ATR_GlCode as CorresGLcode, F.Gl_Desc as CorresDescription "
    '                aSql = aSql & "from account_transactions A join Acc_Receipt_BillDetails B ON A.ATR_BillID = B.Acc_RB_ID And A.ATR_TrType = 3 "
    '                aSql = aSql & "And A.ATR_GlCode = '" & dtGL.Rows(i)("gl_glCode").ToString() & "' and A.ATR_YearID = " & iYearId & " and A.ATR_COmpID = " & iCompID & " "
    '                aSql = aSql & "join Acc_Receipt_Transaction C On C.Acc_RT_MasterBillID = B.Acc_Rb_ID "
    '                aSql = aSql & "join account_transactions D on D.ATR_ID = A.ATR_ID and D.ATR_VoucherNo = A.ATR_VoucherNo and "
    '                aSql = aSql & "D.ATR_GLCode = A.ATR_GLCode "
    '                aSql = aSql & "Join Chart_of_Accounts E on E.gl_Glcode = A.ATR_GLCode "
    '                aSql = aSql & "Join Chart_of_Accounts F on F.gl_Glcode = D.ATR_GLCode order by B.Acc_RB_BilLDate Asc"
    '                dtReceipt = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
    '                For j = 0 To dtReceipt.Rows.Count - 1
    '                    dRow = dt.NewRow()
    '                    dRow("BillDate") = dtReceipt.Rows(j)("BillDate").ToString() 'clsTRACeGeneral.FormatDtForRDBMS(dtReceipt.Rows(j)("BillDate").ToString(), "DT")
    '                    dRow("TransactionType") = "Receipt"
    '                    'dRow("gl_Desc") = dtReceipt.Rows(j)("Descriptions").ToString()
    '                    dRow("Narration") = dtReceipt.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtReceipt.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtReceipt.Rows(j)("ChequeNo").ToString()
    '                    dRow("CorresGLCode") = dtReceipt.Rows(j)("CorresGLcode").ToString()
    '                    dRow("CorresDescription") = dtReceipt.Rows(j)("CorresDescription").ToString()
    '                    'dRow("OpDebit") = dtReceipt.Rows(j)("CorresDescription").ToString()
    '                    'dRow("OpCredit") = dtReceipt.Rows(j)("CorresDescription").ToString()

    '                    If dtReceipt.Rows(j)("Debit").ToString() = "" Then
    '                        dRow("TransDebit") = "0.00" : dTransDebit = 0
    '                    Else
    '                        dRow("TransDebit") = dtReceipt.Rows(j)("Debit").ToString()
    '                    End If

    '                    If dtReceipt.Rows(j)("Credit").ToString() = "" Then
    '                        dRow("TransCredit") = "0.00" : dTransDebit = 0
    '                    Else
    '                        dRow("TransCredit") = dtReceipt.Rows(j)("Credit").ToString()
    '                    End If

    '                    dRow("Status") = "1"
    '                    dt.Rows.Add(dRow)
    '                Next


    '                'Journal Entry                    
    '                aSql = "" : aSql = "Select Convert(Char(10), B.Acc_JE_BilLDate, 111) As BillDate,"
    '                aSql = aSql & "A.ATR_GLcode as GLCode,B.Acc_JE_TransactionType as TransactionType,E.Gl_Desc as Descriptions,"
    '                aSql = aSql & "A.Atr_VoucherNo as VoucherNo,C.Acc_JET_ChequeNo as ChequeNo,C.Acc_JET_ChequeDate as ChequeDate,A.ATR_Party as Party,"
    '                aSql = aSql & "A.ATR_DBAmount as Debit,A.ATR_CrAmount as Credit,B.Acc_JE_BillAmount as BillAmount,"
    '                aSql = aSql & "C.Acc_JET_ChequeAmount as ChequeAmount,C.Acc_JET_BankName as BankName,C.Acc_JET_BranchName as BranchName,"
    '                aSql = aSql & "D.ATR_GlCode as CorresGLcode, F.Gl_Desc as CorresDescription "
    '                aSql = aSql & "from account_transactions A join Acc_JE_BillDetails B ON A.ATR_BillID = B.Acc_JE_ID and A.ATR_TrType = 4 "
    '                aSql = aSql & "And A.ATR_GlCode = '" & dtGL.Rows(i)("gl_glCode").ToString() & "' and A.ATR_YearID = " & iYearId & " and A.ATR_COmpID = " & iCompID & " "
    '                aSql = aSql & "join Acc_JE_Transaction C On C.Acc_JET_MasterBillID = B.Acc_JE_ID "
    '                aSql = aSql & "join account_transactions D on D.ATR_ID = A.ATR_ID and D.ATR_VoucherNo = A.ATR_VoucherNo and "
    '                aSql = aSql & "D.ATR_GLCode = A.ATR_GLCode "
    '                aSql = aSql & "Join Chart_of_Accounts E on E.gl_Glcode = A.ATR_GLCode "
    '                aSql = aSql & "Join Chart_of_Accounts F on F.gl_Glcode = D.ATR_GLCode order by B.Acc_JE_BilLDate Asc"
    '                dtJE = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
    '                For j = 0 To dtJE.Rows.Count - 1
    '                    dRow = dt.NewRow()
    '                    dRow("BillDate") = dtJE.Rows(j)("BillDate").ToString() 'clsTRACeGeneral.FormatDtForRDBMS(dtJE.Rows(j)("BillDate").ToString(), "DT")
    '                    dRow("TransactionType") = "JE"
    '                    'dRow("gl_Desc") = dtJE.Rows(j)("Descriptions").ToString()
    '                    dRow("Narration") = dtJE.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtJE.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtJE.Rows(j)("ChequeNo").ToString()
    '                    dRow("CorresGLCode") = dtJE.Rows(j)("CorresGLcode").ToString()
    '                    dRow("CorresDescription") = dtJE.Rows(j)("CorresDescription").ToString()
    '                    'dRow("OpDebit") = dtJE.Rows(j)("CorresDescription").ToString()
    '                    'dRow("OpCredit") = dtJE.Rows(j)("CorresDescription").ToString()                      

    '                    If dtJE.Rows(j)("Debit").ToString() = "" Then
    '                        dRow("TransDebit") = "0.00" : dTransDebit = 0
    '                    Else
    '                        dRow("TransDebit") = dtJE.Rows(j)("Debit").ToString()
    '                    End If



    '                    If dtJE.Rows(j)("Credit").ToString() = "" Then
    '                        dRow("TransCredit") = "0.00" : dTransDebit = 0
    '                    Else
    '                        dRow("TransCredit") = dtJE.Rows(j)("Credit").ToString()
    '                    End If
    '                    dRow("Status") = "1"
    '                    dt.Rows.Add(dRow)
    '                Next

    '                Dim dataView As New DataView(dt)
    '                dataView.Sort = "BillDate Asc"
    '                dtDateSort = dataView.ToTable
    '                dtDateSort = GetMonthlyReports(dtDateSort)

    '                dtFinal.Merge(dtDateSort)
    '                dtFinal.AcceptChanges()
    '            Next
    '        End If


    '        dtDateSort = GetGrandTotal(dtFinal)
    '        dtFinal.Merge(dtDateSort)
    '        dtFinal.AcceptChanges()
    '        Return dtFinal
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetGrandTotal(ByVal dt As DataTable) As DataTable
        Dim dtGrand As New DataTable
        Dim dRow As DataRow
        Try
            dtGrand.Columns.Add("BillDate")
            dtGrand.Columns.Add("gl_Code")
            dtGrand.Columns.Add("TransactionType")
            dtGrand.Columns.Add("gl_Desc")
            dtGrand.Columns.Add("Narration")
            dtGrand.Columns.Add("CorresGLCode")
            dtGrand.Columns.Add("CorresDescription")
            dtGrand.Columns.Add("OpDebit")
            dtGrand.Columns.Add("OpCredit")
            dtGrand.Columns.Add("TransDebit")
            dtGrand.Columns.Add("TransCredit")
            dtGrand.Columns.Add("ClosingDebit")
            dtGrand.Columns.Add("ClosingCredit")
            dtGrand.Columns.Add("Status")

            dRow = dtGrand.NewRow
            dRow("Gl_Desc") = ""
            dtGrand.Rows.Add(dRow)


            For i = 0 To dt.Rows.Count - 1
                If (dt.Rows(i)("Status").ToString() = "0") Or (dt.Rows(i)("Status").ToString() = "1") Then
                    dGrandOpnDebit = dGrandOpnDebit + dt.Rows(i)("OpDebit").ToString()
                    dGrandOpnCredit = dGrandOpnCredit + dt.Rows(i)("OpCredit").ToString()

                    dGrandTransDebit = dGrandTransDebit + dt.Rows(i)("TransDebit").ToString()
                    dGrandTransCredit = dGrandTransCredit + dt.Rows(i)("TransCredit").ToString()

                    dGrandCloseDebit = dGrandCloseDebit + dt.Rows(i)("ClosingDebit").ToString()
                    dGrandCloseCredit = dGrandCloseCredit + dt.Rows(i)("ClosingCredit").ToString()
                End If
            Next

            dRow = dtGrand.NewRow
            dRow("BillDate") = ""
            dRow("gl_Code") = ""
            dRow("Gl_Desc") = "<B>" & "GRAND TOTAL" & "</B>"
            dRow("TransactionType") = ""
            dRow("Narration") = ""
            dRow("CorresGLCode") = ""
            dRow("CorresDescription") = ""
            dRow("OpDebit") = Convert.ToDecimal(dGrandOpnDebit).ToString("#,##0.00")
            dRow("OpCredit") = Convert.ToDecimal(dGrandOpnCredit).ToString("#,##0.00")

            dRow("TransDebit") = Convert.ToDecimal(dGrandTransDebit).ToString("#,##0.00")
            dRow("TransCredit") = Convert.ToDecimal(dGrandTransCredit).ToString("#,##0.00")

            dRow("ClosingDebit") = Convert.ToDecimal(dGrandCloseDebit).ToString("#,##0.00")
            dRow("ClosingCredit") = Convert.ToDecimal(dGrandCloseCredit).ToString("#,##0.00")

            dtGrand.Rows.Add(dRow)
            Return dtGrand
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMonthlyReports(ByVal dt As DataTable) As DataTable
        Dim dtMonth As New DataTable
        Dim dRow As DataRow
        Dim dOpnDebit As Double = 0, dOpnCredit As Double = 0
        Dim dTransDebit As Double = 0, dTransCredit As Double = 0
        Dim dCloseDebit As Double = 0, dCloseCredit As Double = 0
        Dim iStatus As Integer = 0

        Dim dTotalOpnDebit As Double = 0, dTotalOpnCredit As Double = 0
        Dim dTotalTransDebit As Double = 0, dTotalTransCredit As Double = 0
        Dim dTotalCloseDebit As Double = 0, dTotalCloseCredit As Double = 0

        Dim sMonth As String = "", sYear As String = ""
        Dim sCurrentMonth As String = "", sCurrentYear As String = ""

        Dim dClosingTotal As Double = 0
        Try
            dtMonth.Columns.Add("BillDate")
            dtMonth.Columns.Add("gl_Code")
            dtMonth.Columns.Add("TransactionType")
            dtMonth.Columns.Add("gl_Desc")
            dtMonth.Columns.Add("Narration")
            dtMonth.Columns.Add("CorresGLCode")
            dtMonth.Columns.Add("CorresDescription")
            dtMonth.Columns.Add("OpDebit")
            dtMonth.Columns.Add("OpCredit")
            dtMonth.Columns.Add("TransDebit")
            dtMonth.Columns.Add("TransCredit")
            dtMonth.Columns.Add("ClosingDebit")
            dtMonth.Columns.Add("ClosingCredit")
            dtMonth.Columns.Add("Status")

            For i = 0 To dt.Rows.Count - 1
                dRow = dtMonth.NewRow

                Dim dDate As Date = dt.Rows(i)("BillDate").ToString()

                If dDate.Month.ToString.Length = 1 Then
                    sCurrentMonth = "0" & dDate.Month
                    sCurrentYear = dDate.Year
                Else
                    sCurrentMonth = dDate.Month
                    sCurrentYear = dDate.Year
                End If

                If sMonth = "" Then
                    sMonth = sCurrentMonth
                    sYear = sCurrentYear
                End If


                '01/01/1900
                If (sCurrentMonth = "01") And (sCurrentYear = "1900") Then
                    dRow = dtMonth.NewRow
                    dRow("BillDate") = ""
                    dRow("gl_Code") = dt.Rows(i)("gl_Code").ToString()
                    dRow("Gl_Desc") = dt.Rows(i)("Gl_Desc").ToString()
                    dRow("TransactionType") = dt.Rows(i)("TransactionType").ToString()
                    dRow("Narration") = dt.Rows(i)("Narration").ToString()
                    dRow("CorresGLCode") = dt.Rows(i)("CorresGLCode").ToString()
                    dRow("CorresDescription") = dt.Rows(i)("CorresDescription").ToString()

                    If dt.Rows(i)("OpDebit").ToString() = "" Then
                        dRow("OpDebit") = "0.00" : dOpnDebit = 0
                        dGrandOpnDebit = dGrandOpnDebit + dOpnDebit
                    Else
                        dRow("OpDebit") = Convert.ToDecimal(dt.Rows(i)("OpDebit").ToString()).ToString("#,##0.00")
                        dOpnDebit = Convert.ToDecimal(dt.Rows(i)("OpDebit").ToString()).ToString("#,##0.00")
                    End If
                    dTotalOpnDebit = dOpnDebit

                    If dt.Rows(i)("OpCredit").ToString() = "" Then
                        dRow("OpCredit") = "0.00" : dOpnCredit = 0
                        dGrandOpnCredit = dGrandOpnCredit + dOpnCredit
                    Else
                        dRow("OpCredit") = Convert.ToDecimal(dt.Rows(i)("OpCredit").ToString()).ToString("#,##0.00")
                        dOpnCredit = Convert.ToDecimal(dt.Rows(i)("OpCredit").ToString()).ToString("#,##0.00")
                    End If
                    dTotalOpnCredit = dOpnCredit

                    If dt.Rows(i)("TransDebit").ToString() = "" Then
                        dRow("TransDebit") = "0.00" : dTransDebit = 0
                    Else
                        dRow("TransDebit") = Convert.ToDecimal(dt.Rows(i)("TransDebit").ToString()).ToString("#,##0.00")
                        dTransDebit = Convert.ToDecimal(dt.Rows(i)("TransDebit").ToString()).ToString("#,##0.00")
                    End If

                    If dt.Rows(i)("TransCredit").ToString() = "" Then
                        dRow("TransCredit") = "0.00" : dTransCredit = 0
                        dGrandTransCredit = dGrandTransCredit + dTransCredit
                    Else
                        dRow("TransCredit") = Convert.ToDecimal(dt.Rows(i)("TransCredit").ToString()).ToString("#,##0.00")
                        dTransCredit = Convert.ToDecimal(dt.Rows(i)("TransCredit").ToString()).ToString("#,##0.00")
                    End If
                    dRow("Status") = "0"

                    dCloseDebit = dOpnDebit + dTransDebit
                    dCloseCredit = dOpnCredit + dTransCredit
                    dClosingTotal = dCloseDebit - dCloseCredit

                    If dClosingTotal >= 0 Then
                        dRow("ClosingDebit") = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                        dRow("ClosingCredit") = "0.00"
                    Else
                        dRow("ClosingCredit") = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                        dRow("ClosingDebit") = "0.00"
                    End If
                    dCloseDebit = 0 : dCloseCredit = 0 : dTotalCloseDebit = 0 : dTotalCloseCredit = 0
                    dtMonth.Rows.Add(dRow) : sMonth = ""
                Else

                    If (sMonth.Contains(sCurrentMonth) = True) And (sYear.Contains(sCurrentYear) = True) Then
                        dRow = dtMonth.NewRow
                        If dt.Rows(i)("BillDate").ToString() = "01/01/1900" Then
                            dRow("BillDate") = ""
                        Else
                            dRow("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("BillDate").ToString(), "D")
                        End If

                        dRow("gl_Code") = dt.Rows(i)("gl_Code").ToString()
                        dRow("Gl_Desc") = dt.Rows(i)("Gl_Desc").ToString()
                        dRow("TransactionType") = dt.Rows(i)("TransactionType").ToString()
                        dRow("Narration") = dt.Rows(i)("Narration").ToString()
                        dRow("CorresGLCode") = dt.Rows(i)("CorresGLCode").ToString()
                        dRow("CorresDescription") = dt.Rows(i)("CorresDescription").ToString()

                        If dt.Rows(i)("Status").ToString() = "1" Then
                            iStatus = 1
                            dRow("OpDebit") = "0.00" : dRow("OpCredit") = "0.00"
                            dGrandOpnDebit = dGrandOpnDebit + 0
                            dGrandOpnCredit = dGrandOpnCredit + 0

                            If dt.Rows(i)("TransDebit").ToString() = "" Then
                                dRow("TransDebit") = "0.00" : dTransDebit = 0
                                dTotalTransDebit = dTotalTransDebit + dTransDebit
                            Else
                                dRow("TransDebit") = Convert.ToDecimal(dt.Rows(i)("TransDebit").ToString()).ToString("#,##0.00")
                                dTransDebit = dOpnDebit + Convert.ToDecimal(dt.Rows(i)("TransDebit").ToString()).ToString("#,##0.00")
                                dTotalTransDebit = dTotalTransDebit + Convert.ToDecimal(dt.Rows(i)("TransDebit").ToString()).ToString("#,##0.00")
                            End If

                            If dt.Rows(i)("TransCredit").ToString() = "" Then
                                dRow("TransCredit") = "0.00" : dTransCredit = 0
                                dTotalTransCredit = dTotalTransCredit + dTransCredit
                            Else
                                dRow("TransCredit") = Convert.ToDecimal(dt.Rows(i)("TransCredit").ToString()).ToString("#,##0.00")
                                dTransCredit = dOpnCredit + Convert.ToDecimal(dt.Rows(i)("TransCredit").ToString()).ToString("#,##0.00")
                                dTotalTransCredit = dTotalTransCredit + Convert.ToDecimal(dt.Rows(i)("TransCredit").ToString()).ToString("#,##0.00")
                            End If

                            dClosingTotal = dTransDebit - dTransCredit

                            If dClosingTotal >= 0 Then
                                dRow("ClosingDebit") = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00") : dRow("ClosingCredit") = "0.00"
                                dOpnDebit = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                                dTotalCloseDebit = dTotalCloseDebit + Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                            Else
                                dRow("ClosingCredit") = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00") : dRow("ClosingDebit") = "0.00"
                                dOpnDebit = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                                dTotalCloseCredit = dTotalCloseCredit + Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                            End If
                        End If
                        dRow("Status") = "1"
                        dtMonth.Rows.Add(dRow)
                        sMonth = sCurrentMonth : sYear = sCurrentYear

                        If i = dt.Rows.Count - 1 Then
                            dRow = dtMonth.NewRow
                            dRow("Gl_Desc") = "<B>" & "TOTAL" & "</B>"
                            dRow("OpDebit") = Convert.ToDecimal(dTotalOpnDebit).ToString("#,##0.00")
                            dRow("OpCredit") = Convert.ToDecimal(dTotalOpnCredit).ToString("#,##0.00")

                            dRow("TransDebit") = Convert.ToDecimal(dTotalTransDebit).ToString("#,##0.00")
                            dRow("TransCredit") = Convert.ToDecimal(dTotalTransCredit).ToString("#,##0.00")

                            dRow("ClosingDebit") = "0.00"
                            dRow("ClosingCredit") = "0.00"

                            'dRow("ClosingDebit") = Convert.ToDecimal(dTotalCloseDebit).ToString("#,##0.00")
                            ' dRow("ClosingCredit") = Convert.ToDecimal(dTotalCloseCredit).ToString("#,##0.00")
                            dRow("Status") = "2"
                            dtMonth.Rows.Add(dRow)

                            dRow = dtMonth.NewRow
                            dRow("Gl_Desc") = ""
                            dtMonth.Rows.Add(dRow)
                            dTotalCloseDebit = 0 : dTotalCloseCredit = 0 : dTotalTransDebit = 0 : dTotalTransCredit = 0
                            'dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0 : dCloseDebit = 0 : dCloseCredit = 0
                        End If
                    Else
                        If iStatus = 1 Then
                            dRow = dtMonth.NewRow
                            dRow("Gl_Desc") = "<B>" & "TOTAL" & "</B>"
                            'dRow("OpDebit") = Convert.ToDecimal(dTotalOpnDebit).ToString("#,##0.00")
                            'dRow("OpCredit") = Convert.ToDecimal(dTotalOpnCredit).ToString("#,##0.00")

                            dRow("OpDebit") = "0.00"
                            dRow("OpCredit") = "0.00"

                            dRow("TransDebit") = Convert.ToDecimal(dTotalTransDebit).ToString("#,##0.00")
                            dRow("TransCredit") = Convert.ToDecimal(dTotalTransCredit).ToString("#,##0.00")

                            dRow("ClosingDebit") = "0.00"
                            dRow("ClosingCredit") = "0.00"
                            ' dRow("ClosingDebit") = Convert.ToDecimal(dTotalCloseDebit).ToString("#,##0.00")
                            'dRow("ClosingCredit") = Convert.ToDecimal(dTotalCloseCredit).ToString("#,##0.00")
                            dRow("Status") = "2"
                            dtMonth.Rows.Add(dRow)

                            dRow = dtMonth.NewRow
                            dRow("Gl_Desc") = ""
                            dtMonth.Rows.Add(dRow)
                            dTotalCloseDebit = 0 : dTotalCloseCredit = 0 : dTotalTransDebit = 0 : dTotalTransCredit = 0
                            'dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0 : dCloseDebit = 0 : dCloseCredit = 0

                            dRow = dtMonth.NewRow
                            dRow("Status") = "1"
                            If dt.Rows(i)("BillDate").ToString() = "01/01/1900" Then
                                dRow("BillDate") = ""
                            Else
                                dRow("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("BillDate").ToString(), "D")
                            End If

                            dRow("gl_Code") = dt.Rows(i)("gl_Code").ToString()
                            dRow("Gl_Desc") = dt.Rows(i)("Gl_Desc").ToString()
                            dRow("TransactionType") = dt.Rows(i)("TransactionType").ToString()
                            dRow("Narration") = dt.Rows(i)("Narration").ToString()
                            dRow("CorresGLCode") = dt.Rows(i)("CorresGLCode").ToString()
                            dRow("CorresDescription") = dt.Rows(i)("CorresDescription").ToString()
                            If dt.Rows(i)("Status").ToString() = "1" Then
                                iStatus = 1
                                If dt.Rows(i)("OpDebit").ToString() = "" Then
                                    dRow("OpDebit") = "0.00" : dOpnDebit = 0
                                Else
                                    dRow("OpDebit") = Convert.ToDecimal(dt.Rows(i)("OpDebit").ToString()).ToString("#,##0.00")
                                    dOpnDebit = dOpnDebit + Convert.ToDecimal(dt.Rows(i)("OpDebit").ToString()).ToString("#,##0.00")
                                End If

                                If dt.Rows(i)("OpCredit").ToString() = "" Then
                                    dRow("OpCredit") = "0.00" : dOpnCredit = 0
                                Else
                                    dRow("OpCredit") = Convert.ToDecimal(dt.Rows(i)("OpCredit").ToString()).ToString("#,##0.00")
                                    dOpnDebit = dOpnDebit + Convert.ToDecimal(dt.Rows(i)("OpCredit").ToString()).ToString("#,##0.00")
                                End If

                                If dt.Rows(i)("TransDebit").ToString() = "" Then
                                    dRow("TransDebit") = "0.00" : dTransDebit = 0
                                    dTotalTransDebit = dTotalTransDebit + dTransDebit
                                Else
                                    dRow("TransDebit") = Convert.ToDecimal(dt.Rows(i)("TransDebit").ToString()).ToString("#,##0.00")
                                    dTransDebit = dTransDebit + Convert.ToDecimal(dt.Rows(i)("TransDebit").ToString()).ToString("#,##0.00")
                                    dTotalTransDebit = dTotalTransDebit + Convert.ToDecimal(dt.Rows(i)("TransDebit").ToString()).ToString("#,##0.00")
                                End If

                                If dt.Rows(i)("TransCredit").ToString() = "" Then
                                    dRow("TransCredit") = "0.00" : dTransCredit = 0
                                    dTotalTransCredit = dTotalTransCredit + dTransCredit
                                Else
                                    dRow("TransCredit") = Convert.ToDecimal(dt.Rows(i)("TransCredit").ToString()).ToString("#,##0.00")
                                    dTransCredit = dTransCredit + Convert.ToDecimal(dt.Rows(i)("TransCredit").ToString()).ToString("#,##0.00")
                                    dGrandTransCredit = dGrandTransCredit + Convert.ToDecimal(dt.Rows(i)("TransCredit").ToString()).ToString("#,##0.00")
                                End If

                                dClosingTotal = dTransDebit - dTransCredit

                                If dClosingTotal >= 0 Then
                                    dRow("ClosingDebit") = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00") : dRow("ClosingCredit") = "0.00"
                                    dOpnDebit = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                                    dTotalCloseDebit = dTotalCloseDebit + Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                                Else
                                    dRow("ClosingCredit") = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00") : dRow("ClosingDebit") = "0.00"
                                    dOpnDebit = Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                                    dTotalCloseCredit = dTotalCloseCredit + Convert.ToDecimal(dClosingTotal).ToString("#,##0.00")
                                End If
                            End If
                            dtMonth.Rows.Add(dRow)
                            sMonth = sCurrentMonth : sYear = sCurrentYear

                            If i = dt.Rows.Count - 1 Then
                                dRow = dtMonth.NewRow
                                dRow("Gl_Desc") = "<B>" & "TOTAL" & "</B>"
                                dRow("OpDebit") = Convert.ToDecimal(dTotalOpnDebit).ToString("#,##0.00")
                                dRow("OpCredit") = Convert.ToDecimal(dTotalOpnCredit).ToString("#,##0.00")

                                dRow("TransDebit") = Convert.ToDecimal(dTotalTransDebit).ToString("#,##0.00")
                                dRow("TransCredit") = Convert.ToDecimal(dTotalTransCredit).ToString("#,##0.00")

                                dRow("ClosingDebit") = "0.00"
                                dRow("ClosingCredit") = "0.00"

                                'dRow("ClosingDebit") = Convert.ToDecimal(dTotalCloseDebit).ToString("#,##0.00")
                                'dRow("ClosingCredit") = Convert.ToDecimal(dTotalCloseCredit).ToString("#,##0.00")
                                dRow("Status") = "2"
                                dtMonth.Rows.Add(dRow)
                                dTotalCloseDebit = 0 : dTotalCloseCredit = 0 : dTotalTransDebit = 0 : dTotalTransCredit = 0
                                ' dOpnDebit = 0 : dOpnCredit = 0 : dTransDebit = 0 : dTransCredit = 0 : dCloseDebit = 0 : dCloseCredit = 0
                            End If
                        End If
                    End If
                End If
            Next
            Return dtMonth
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadBalanceSheet(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim dtGroup As New DataTable
        Dim dtSub As New DataTable
        Dim dtLink As New DataTable
        Dim dtArray As New DataTable
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0
        Dim a As Integer = 0
        Dim sArray As Array
        Dim dDebit As Double = 0.00
        Dim dCredit As Double = 0.00

        Dim dLDebit As Double = 0.00
        Dim dLCredit As Double = 0.00

        Dim dTotalDebit As Double = 0.00
        Dim dTotalCredit As Double = 0.00

        Dim dTotalLDebit As Double = 0.00
        Dim dTotalLCredit As Double = 0.00

        Dim iHead As Integer = 0
        Dim iSLNo As Integer = 0
        Dim iLastYear As Integer = 0

        Dim iFixedAssets As Integer = 0

        Dim iStatusCheck As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")

            'Liabilites

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iCOmpID & ")"
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                iLastYear = dr("YMS_ID")
            Else
                iLastYear = 0
            End If


            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
            dr = objDB.SQLDataReader(sNameSpace, iSql)
            If dr.HasRows = True Then
                dr.Read()
                If dr("Opn_Status") = "F" Then
                    iStatusCheck = 0
                Else
                    iStatusCheck = 1
                End If
            Else
                iStatusCheck = 1
            End If


            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 4 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EQUITY AND LIABILITIES" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iCOmpID & " and gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 4 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & ""
                            dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then

                                            If iStatusCheck = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                        End If
                                                    Next

                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next

                                                End If
                                            End If
                                        End If
                                    Next
                                    dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    dTotalDebit = dTotalDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If


                                'Last Year

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                            dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                    End If
                                                Next

                                            End If
                                        End If
                                    Next

                                    dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))

                                    Dim dLDbCr As Double = 0.00
                                    dLDbCr = dLDebit - dLCredit
                                    dTotalLDebit = dTotalLDebit + dLDbCr

                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If


            'Assets
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 1 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "ASSETS" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
                    dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            If (dtSub.Rows(j)("gl_Desc").ToString() = "Tangible Assets") Or (dtSub.Rows(j)("gl_Desc").ToString() = "Intangible Assets") Then
                                iFixedAssets = 1
                            Else
                                iFixedAssets = 0
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & ""
                            dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iFixedAssets = 0 Then

                                                If iStatusCheck = 0 Then
                                                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                    iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                    dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                            End If

                                                            If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                            End If
                                                        Next

                                                    End If
                                                Else
                                                    iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                    iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                    dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(0)("ATD_Debit").ToString())
                                                            End If

                                                            If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                            End If
                                                        Next

                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next

                                    dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    dTotalDebit = dTotalDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iFixedAssets = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                        End If
                                                    Next

                                                End If
                                            End If
                                        End If
                                    Next
                                    dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))

                                    Dim dLDbCr As Double = 0.00
                                    dLDbCr = dLDebit - dLCredit
                                    dTotalLDebit = dTotalLDebit + dLDbCr

                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadPLReports(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim dtGroup As New DataTable
        Dim dtSub As New DataTable
        Dim dtLink As New DataTable
        Dim dtArray As New DataTable
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0
        Dim sArray As Array
        Dim dDebit As Double = 0.00
        Dim dCredit As Double = 0.00

        Dim dLDebit As Double = 0.00
        Dim dLCredit As Double = 0.00

        Dim dTotalDebit As Double = 0.00
        Dim dTotalCredit As Double = 0.00

        Dim dTotalLDebit As Double = 0.00
        Dim dTotalLCredit As Double = 0.00

        Dim iHead As Integer = 0
        Dim iSLNo As Integer = 0
        Dim iLastYear As Integer = 0
        Dim iStatusCheck As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")


            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iCOmpID & ")"
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                iLastYear = dr("YMS_ID")
            Else
                iLastYear = 0
            End If

            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
            dr = objDB.SQLDataReader(sNameSpace, iSql)
            If dr.HasRows = True Then
                dr.Read()
                If dr("Opn_Status") = "F" Then
                    iStatusCheck = 0
                Else
                    iStatusCheck = 1
                End If
            Else
                iStatusCheck = 1
            End If

            'Income
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 2 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "REVENUE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iCOmpID & " and gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =2 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & ""
                            dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iStatusCheck = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    Next

                                    dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    dTotalDebit = dTotalDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                            dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                    End If
                                                Next

                                            End If
                                        End If
                                    Next

                                    dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))

                                    Dim dLdbCr As Double = 0.00
                                    dLdbCr = dLDebit - dLCredit
                                    dTotalLDebit = dTotalLDebit + dLdbCr

                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
                dt.Rows.Add(dRow)
            End If


            'Expenditure
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 3 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EXPENDITURE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iCOmpID & " and gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =3 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & ""
                            dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iStatusCheck = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                        End If
                                                    Next

                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from Acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next

                                                End If
                                            End If
                                        End If
                                    Next


                                    dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dDebit - dCredit))

                                    Dim dDbCr As Double = 0.00
                                    dDbCr = dDebit - dCredit
                                    dTotalDebit = dTotalDebit + dDbCr

                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                            dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                    End If
                                                Next

                                            End If
                                        End If
                                    Next

                                    dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dLDebit - dLCredit))

                                    Dim dLdbCr As Double = 0.00
                                    dLdbCr = dLDebit - dLCredit
                                    dTotalLDebit = dTotalLDebit + dLdbCr

                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalDebit))
                dRow("LastYear") = String.Format("{0:0.00}", Convert.ToDecimal(dTotalLDebit))
                dt.Rows.Add(dRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPaymentReportsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select A.Acc_PM_Party,A.Acc_PM_TransactionNo,convert(char(10),A.Acc_PM_BillDate,103) Acc_PM_BillDate,"
            sSql = sSql & "A.Acc_Bill_Narration,C.ACM_ID,C.ACM_Name,B.ATD_GL, B.ATD_SubGL, B.ATD_Debit, B.ATD_Credit,d.gl_glcode as glcode, d.gl_desc,e.gl_glcode as subglcode, "
            sSql = sSql & "e.gl_desc AS SubGLDesc,A.Acc_PM_BillAmount,A.Acc_PM_ChequeNo,convert(char(10),A.Acc_PM_ChequeDate,103) Acc_PM_ChequeDate "
            sSql = sSql & "from acc_Payment_Master A join Acc_Transactions_Details as B on A.Acc_PM_ID = B.ATD_BillID "
            sSql = sSql & "INNER JOIN Acc_Customer_Master as C on A.Acc_PM_Party = C.ACM_ID "
            sSql = sSql & "INNER JOIN chart_of_Accounts as d on B.atd_Gl = d.gl_id "
            sSql = sSql & "INNER JOIN chart_of_Accounts as e on b.atd_subGl = e.gl_id  where b.atd_trtype = 1 and A.Acc_PM_CompID = " & iCompID & ""
            If iParty <> 0 Then
                sSql = sSql & " And C.ACM_ID =" & iParty & ""
            End If
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadReceiptReportsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select A.Acc_RM_Party,A.Acc_RM_TransactionNo,A.acc_RM_TransactionType,convert(char(10),A.Acc_RM_BillDate,103) Acc_RM_BillDate,"
            sSql = sSql & "A.Acc_RM_BillNarration,C.ACM_ID,C.ACM_Name,B.ATD_GL, B.ATD_SubGL, B.ATD_Debit, B.ATD_Credit,d.gl_glcode as glcode, d.gl_desc,e.gl_glcode as subglcode, "
            sSql = sSql & "e.gl_desc AS SubGLDesc,A.Acc_RM_BillAmount,A.Acc_RM_ChequeNo,convert(char(10),A.Acc_RM_ChequeDate,103) Acc_RM_ChequeDate "
            sSql = sSql & "from acc_Receipt_Master A join Acc_Transactions_Details as B on A.Acc_RM_ID = B.ATD_BillID INNER JOIN Acc_Customer_Master as C on "
            sSql = sSql & "A.Acc_RM_Party = C.ACM_ID INNER JOIN chart_of_Accounts as d on B.atd_Gl = d.gl_id INNER JOIN chart_of_Accounts as e on "
            sSql = sSql & "B.atd_subGl = E.gl_id  where B.atd_trtype = 3 and A.Acc_RM_CompID = " & iCompID & ""
            If iParty <> 0 Then
                sSql = sSql & " And C.ACM_ID =" & iParty & ""
            End If
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPettyCashReportsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Try
            Dim sSql As String = ""
            Dim dt As New DataTable

            sSql = "" : sSql = "SELECT A.Acc_PCM_Party, A.Acc_PCM_TransactionNo, CONVERT(char(10), A.Acc_PCM_BillDate, 103) AS Acc_PCM_BillDate,"
            sSql = sSql & "C.ACM_Name, B.ATD_GL, B.ATD_SubGL,B.ATD_Debit, B.ATD_Credit, d.gl_glcode AS glcode, d.gl_desc, e.gl_glcode AS subglcode, e.gl_desc AS SubGLDesc, "
            sSql = sSql & "A.Acc_PCM_BillAmount FROM Acc_PettyCash_Master AS A INNER JOIN "
            sSql = sSql & "Acc_Transactions_Details AS B ON A.Acc_PCM_ID = B.ATD_BillId INNER JOIN "
            sSql = sSql & "Acc_Customer_Master AS C ON A.Acc_PCM_Party = C.ACM_ID INNER JOIN "
            sSql = sSql & "Chart_of_Accounts AS d ON B.ATD_GL = d.gl_id INNER JOIN "
            sSql = sSql & "Chart_of_Accounts AS e ON B.ATD_SubGL = e.gl_id "
            sSql = sSql & "WHERE (B.ATD_TrType = 2) AND (A.Acc_PCM_CompID = " & iCompID & ")"

            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadScheduleReport(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = "", aSql As String = "", mSql As String = "", iSql As String = ""
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim dtGroup As New DataTable
        Dim dtSub As New DataTable
        Dim dtFA As New DataTable
        Dim dtLink As New DataTable
        Dim dtArray As New DataTable
        Dim dr As OleDb.OleDbDataReader
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0, m As Integer = 0
        Dim sArray As Array
        Dim dDebit As Double = 0.00
        Dim dCredit As Double = 0.00

        Dim dLDebit As Double = 0.00
        Dim dLCredit As Double = 0.00

        Dim dTotalDebit As Double = 0.00
        Dim dTotalCredit As Double = 0.00

        Dim dTotalLDebit As Double = 0.00
        Dim dTotalLCredit As Double = 0.00

        Dim iHead As Integer = 0
        Dim iSLNo As Integer = 0
        Dim iLastYear As Integer = 0
        Dim iStatusCheck As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("Particulars")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")

            'Liabilites

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and Yms_CompID =" & iCOmpID & ")"
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                iLastYear = dr("YMS_ID")
            Else
                iLastYear = 0
            End If

            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
            dr = objDB.SQLDataReader(sNameSpace, iSql)
            If dr.HasRows = True Then
                dr.Read()
                If dr("Opn_Status") = "F" Then
                    iStatusCheck = 0
                Else
                    iStatusCheck = 1
                End If
            Else
                iStatusCheck = 1
            End If

            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 4 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EQUITY AND LIABILITIES" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iCOmpID & " and gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            dDebit = 0 : dCredit = 0

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head = 4 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & ""
                            dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iStatusCheck = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearID =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    Next

                                    If (dDebit <> 0) And (dCredit <> 0) Then
                                        dRow("PresentYear") = dCredit - dDebit
                                    ElseIf dDebit <> 0 Then
                                        dRow("PresentYear") = dDebit
                                    ElseIf dCredit <> 0 Then
                                        dRow("PresentYear") = dCredit
                                    ElseIf (dDebit = 0) And (dCredit = 0) Then
                                        dRow("PresentYear") = "0.00"
                                    End If

                                    dTotalDebit = dTotalDebit + dRow("PresentYear")
                                    dDebit = 0 : dCredit = 0
                                End If


                                'Last Year

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                            dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                    End If
                                                Next
                                            End If
                                        End If
                                    Next

                                    If (dLDebit <> 0) And (dLCredit <> 0) Then
                                        dRow("LastYear") = dLCredit - dLDebit
                                    ElseIf dLDebit <> 0 Then
                                        dRow("LastYear") = dLDebit
                                    ElseIf dLCredit <> 0 Then
                                        dRow("LastYear") = dLCredit
                                    ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                        dRow("LastYear") = "0.00"
                                    End If

                                    dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit 'dTotalCredit - dTotalDebit
                dRow("LastYear") = dTotalLDebit 'dTotalLCredit - dTotalLDebit
                dt.Rows.Add(dRow)
            End If


            'Assets
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 1 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "ASSETS" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
                    dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            dDebit = 0 : dCredit = 0

                            If dtSub.Rows(j)("gl_Desc").ToString() = "Fixed Assets" Then
                                aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtSub.Rows(j)("gl_ID") & " and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
                                dtFA = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                                If dtFA.Rows.Count > 0 Then
                                    For m = 0 To dtFA.Rows.Count - 1
                                        dt.Rows.Add(dRow)
                                        dRow = dt.NewRow()

                                        If IsDBNull(dtFA.Rows(m)("gl_Desc").ToString()) = False Then
                                            dRow("Particulars") = dtFA.Rows(m)("gl_Desc")
                                        End If

                                        mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                                        mSql = mSql & "SLM_SUbGroupID =" & dtFA.Rows(m)("gl_Parent") & " and SLM_CompID =" & iCOmpID & ""
                                        dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                                        If dtLink.Rows.Count > 0 Then
                                            If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                                dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                            End If

                                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                            If sArray.Length - 1 Then
                                                For k = 0 To sArray.Length - 1
                                                    If sArray(k) <> "" Then
                                                        If iStatusCheck = 0 Then
                                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                            iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                            dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                            If dtArray.Rows.Count > 0 Then
                                                                For a = 0 To dtArray.Rows.Count - 1
                                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                                    End If

                                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                                    End If
                                                                Next
                                                            End If
                                                        Else
                                                            iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                            iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                            dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                            If dtArray.Rows.Count > 0 Then
                                                                For a = 0 To dtArray.Rows.Count - 1
                                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                        dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                                    End If

                                                                    If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                        dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                                    End If
                                                                Next

                                                            End If
                                                        End If

                                                    End If
                                                Next


                                                If (dDebit <> 0) And (dCredit <> 0) Then
                                                    dRow("PresentYear") = dCredit - dDebit
                                                ElseIf dDebit <> 0 Then
                                                    dRow("PresentYear") = dDebit
                                                ElseIf dCredit <> 0 Then
                                                    dRow("PresentYear") = dCredit
                                                ElseIf (dDebit = 0) And (dCredit = 0) Then
                                                    dRow("PresentYear") = "0.00"
                                                End If

                                                dTotalDebit = dTotalDebit + dRow("PresentYear")
                                                dDebit = 0 : dCredit = 0
                                            End If

                                            'Last Year
                                            sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                            If sArray.Length - 1 Then
                                                For k = 0 To sArray.Length - 1
                                                    If sArray(k) <> "" Then
                                                        iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                                        iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                                        dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                        If dtArray.Rows.Count > 0 Then
                                                            For a = 0 To dtArray.Rows.Count - 1
                                                                If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                                    dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                                End If

                                                                If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                                    dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                                End If
                                                            Next

                                                        End If
                                                    End If
                                                Next

                                                If (dLDebit <> 0) And (dLCredit <> 0) Then
                                                    dRow("LastYear") = dLCredit - dLDebit
                                                ElseIf dLDebit <> 0 Then
                                                    dRow("LastYear") = dLDebit
                                                ElseIf dLCredit <> 0 Then
                                                    dRow("LastYear") = dLCredit
                                                ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                                    dRow("LastYear") = "0.00"
                                                End If

                                                dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                                dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                            End If
                                        End If
                                    Next
                                End If

                            Else

                                '------------------------------------------------------------------------------------------
                                mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =1 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                                mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & ""
                                dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                                If dtLink.Rows.Count > 0 Then
                                    If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                        dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                    End If

                                    sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                    If sArray.Length - 1 Then
                                        For k = 0 To sArray.Length - 1
                                            If sArray(k) <> "" Then
                                                If iStatusCheck = 0 Then
                                                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                    iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                    dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                            End If

                                                            If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                            End If
                                                        Next
                                                    End If
                                                Else
                                                    iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                    iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                    dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                    If dtArray.Rows.Count > 0 Then
                                                        For a = 0 To dtArray.Rows.Count - 1
                                                            If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                            End If

                                                            If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                            End If
                                                        Next

                                                    End If
                                                End If

                                            End If
                                        Next

                                        If (dDebit <> 0) And (dCredit <> 0) Then
                                            dRow("PresentYear") = dCredit - dDebit
                                        ElseIf dDebit <> 0 Then
                                            dRow("PresentYear") = dDebit
                                        ElseIf dCredit <> 0 Then
                                            dRow("PresentYear") = dCredit
                                        ElseIf (dDebit = 0) And (dCredit = 0) Then
                                            dRow("PresentYear") = "0.00"
                                        End If

                                        dTotalDebit = dTotalDebit + dRow("PresentYear")
                                        dDebit = 0 : dCredit = 0
                                    End If

                                    'Last Year
                                    sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                    If sArray.Length - 1 Then
                                        For k = 0 To sArray.Length - 1
                                            If sArray(k) <> "" Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                            dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                            dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                        End If
                                                    Next

                                                End If
                                            End If
                                        Next


                                        If (dLDebit <> 0) And (dLCredit <> 0) Then
                                            dRow("LastYear") = dLCredit - dLDebit
                                        ElseIf dLDebit <> 0 Then
                                            dRow("LastYear") = dLDebit
                                        ElseIf dLCredit <> 0 Then
                                            dRow("LastYear") = dLCredit
                                        ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                            dRow("LastYear") = "0.00"
                                        End If

                                        dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                        dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                    End If
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If




            'Income
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 2 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "REVENUE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iCOmpID & " and gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            dDebit = 0 : dCredit = 0

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =2 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & ""
                            dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iStatusCheck = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            End If

                                        End If
                                    Next

                                    If (dDebit <> 0) And (dCredit <> 0) Then
                                        dRow("PresentYear") = dCredit - dDebit
                                    ElseIf dDebit <> 0 Then
                                        dRow("PresentYear") = dDebit
                                    ElseIf dCredit <> 0 Then
                                        dRow("PresentYear") = dCredit
                                    ElseIf (dDebit = 0) And (dCredit = 0) Then
                                        dRow("PresentYear") = "0.00"
                                    End If

                                    dTotalDebit = dTotalDebit + dRow("PresentYear")
                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                            dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                    End If
                                                Next

                                            End If
                                        End If
                                    Next

                                    If (dLDebit <> 0) And (dLCredit <> 0) Then
                                        dRow("LastYear") = dLCredit - dLDebit
                                    ElseIf dLDebit <> 0 Then
                                        dRow("LastYear") = dLDebit
                                    ElseIf dLCredit <> 0 Then
                                        dRow("LastYear") = dLCredit
                                    ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                        dRow("LastYear") = "0.00"
                                    End If

                                    dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If


            'Expenditure
            dDebit = 0 : dCredit = 0 : dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_head in(0) and gl_AccHead = 3 and gl_Delflag ='C' and gl_Status ='A' and gl_CompID =" & iCOmpID & " order by gl_id"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "EXPENDITURE" & "</B>"
                dt.Rows.Add(dRow)

                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtGroup.Rows(i)("gl_ID").ToString()) = False Then
                        dRow("ID") = dtGroup.Rows(i)("gl_ID")
                    End If

                    dRow("SLNo") = iSLNo + 1

                    If IsDBNull(dtGroup.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("Particulars") = "<B>" & dtGroup.Rows(i)("gl_Desc") & "</B>"
                    End If
                    dt.Rows.Add(dRow)

                    aSql = "" : aSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGroup.Rows(i)("gl_ID") & " and gl_CompID =" & iCOmpID & " and gl_Delflag ='C' and gl_Status ='A' order by gl_id"
                    dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    If dtSub.Rows.Count > 0 Then
                        For j = 0 To dtSub.Rows.Count - 1
                            dRow = dt.NewRow()
                            If IsDBNull(dtSub.Rows(j)("gl_ID").ToString()) = False Then
                                dRow("ID") = dtSub.Rows(j)("gl_ID")
                            End If

                            If IsDBNull(dtSub.Rows(j)("gl_Desc").ToString()) = False Then
                                dRow("Particulars") = dtSub.Rows(j)("gl_Desc")
                            End If

                            dDebit = 0 : dCredit = 0

                            mSql = "" : mSql = "Select * from Schedule_Linkage_Master where SLM_Head =3 and SLM_GroupID =" & dtGroup.Rows(i)("gl_ID") & " and "
                            mSql = mSql & "SLM_SUbGroupID =" & dtSub.Rows(j)("gl_ID") & " and SLM_CompID =" & iCOmpID & ""
                            dtLink = objDB.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtLink.Rows.Count > 0 Then
                                If IsDBNull(dtLink.Rows(0)("SLM_NoteNo").ToString()) = False Then
                                    dRow("NoteNo") = dtLink.Rows(0)("SLM_NoteNo")
                                End If

                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            If iStatusCheck = 0 Then
                                                iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " and Opn_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            Else
                                                iSql = "" : iSql = "Select * from acc_Transactions_Details where ATD_YearId =" & iYearID & " and ATD_CompID =" & iCOmpID & " and "
                                                iSql = iSql & "ATD_GL =" & sArray(k) & ""
                                                dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                                If dtArray.Rows.Count > 0 Then
                                                    For a = 0 To dtArray.Rows.Count - 1
                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Debit").ToString()) = False) And (dtArray.Rows(a)("ATD_Debit").ToString() <> "") Then
                                                            dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("ATD_Debit").ToString())
                                                        End If

                                                        If (IsDBNull(dtArray.Rows(a)("ATD_Credit").ToString()) = False) And (dtArray.Rows(a)("ATD_Credit").ToString() <> "") Then
                                                            dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("ATD_Credit").ToString())
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    Next

                                    If (dDebit <> 0) And (dCredit <> 0) Then
                                        dRow("PresentYear") = dCredit - dDebit
                                    ElseIf dDebit <> 0 Then
                                        dRow("PresentYear") = dDebit
                                    ElseIf dCredit <> 0 Then
                                        dRow("PresentYear") = dCredit
                                    ElseIf (dDebit = 0) And (dCredit = 0) Then
                                        dRow("PresentYear") = "0.00"
                                    End If

                                    dTotalDebit = dTotalDebit + dRow("PresentYear")
                                    dDebit = 0 : dCredit = 0
                                End If

                                'Last Year
                                sArray = dtLink.Rows(0)("SLM_GLLedger").ToString().Split(",")
                                If sArray.Length - 1 Then
                                    For k = 0 To sArray.Length - 1
                                        If sArray(k) <> "" Then
                                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " and Opn_CompID =" & iCOmpID & " and "
                                            iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                            dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                            If dtArray.Rows.Count > 0 Then
                                                For a = 0 To dtArray.Rows.Count - 1
                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                        dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                    End If

                                                    If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                        dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                    End If
                                                Next
                                            End If
                                        End If
                                    Next

                                    If (dLDebit <> 0) And (dLCredit <> 0) Then
                                        dRow("LastYear") = dLCredit - dLDebit
                                    ElseIf dLDebit <> 0 Then
                                        dRow("LastYear") = dLDebit
                                    ElseIf dLCredit <> 0 Then
                                        dRow("LastYear") = dLCredit
                                    ElseIf (dLDebit = 0) And (dLCredit = 0) Then
                                        dRow("LastYear") = "0.00"
                                    End If

                                    dTotalLDebit = dTotalLDebit + dRow("LastYear")
                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    iSLNo = iSLNo + 1
                Next

                dRow = dt.NewRow()
                dRow("Particulars") = "<B>" & "TOTAL" & "</B>"
                dRow("PresentYear") = dTotalDebit
                dRow("LastYear") = dTotalLDebit
                dt.Rows.Add(dRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLCode(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iGLID As Integer)
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim sGLCode As String = ""
        Try
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_id = " & iGLID & " and gl_CompID =" & iCOmpID & ""
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sGLCode = dr("gl_Glcode")
            End If
            Return sGLCode
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadScheduleReportsNotes(ByVal sNameSpace As String, ByVal iCOmpID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = "", iSql As String = "", aSql As String = ""
        Dim dt As New DataTable, dtGroup As New DataTable, dtArray As New DataTable
        Dim dtSub As New DataTable
        Dim dr As OleDb.OleDbDataReader
        Dim iLastYear As Integer = 0
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0, l As Integer = 0, a As Integer = 0
        Dim iStatusCheck As Integer = 0
        Dim dRow As DataRow
        Dim sArray As Array

        Dim dDebit As Double = 0.00
        Dim dCredit As Double = 0.00

        Dim dLDebit As Double = 0.00
        Dim dLCredit As Double = 0.00

        Dim dTotalDebit As Double = 0.00
        Dim dTotalCredit As Double = 0.00

        Dim dTotalLDebit As Double = 0.00
        Dim dTotalLCredit As Double = 0.00
        Try
            dt.Columns.Add("Particulars")
            dt.Columns.Add("PresentYear")
            dt.Columns.Add("LastYear")

            sSql = "" : sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from "
            sSql = sSql & "acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and YMS_CompID =" & iCOmpID & ")"
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                iLastYear = dr("YMS_ID")
            Else
                iLastYear = 0
            End If

            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_yearID =" & iYearID & ""
            dr = objDB.SQLDataReader(sNameSpace, iSql)
            If dr.HasRows = True Then
                dr.Read()
                If dr("Opn_Status") = "F" Then
                    iStatusCheck = 0
                Else
                    iStatusCheck = 1
                End If
            Else
                iStatusCheck = 1
            End If

            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_CompID =" & iCOmpID & " and SLM_NoteNo <> 0 order by SLM_NoteNo"
            dtGroup = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGroup.Rows.Count > 0 Then
                For i = 0 To dtGroup.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("Particulars") = "<B>" & "NOTE NO: " & dtGroup.Rows(i)("SLM_NoteNo") & " - " & objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from Chart_of_Accounts where gl_id = " & dtGroup.Rows(i)("SLM_SubGroupID") & "") & "</B>"
                    dt.Rows.Add(dRow)

                    sArray = dtGroup.Rows(i)("SLM_GLLedger").ToString().Split(",")
                    If sArray.Length > 0 Then
                        For k = 0 To sArray.Length - 1
                            If sArray(k) <> "" Then

                                aSql = "" : aSql = "Select * from Chart_of_Accounts where gl_id = " & sArray(k) & " and gl_compID = " & iCOmpID & " and gl_Status ='A' and gl_Delflag='C'"
                                dtSub = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                                If dtGroup.Rows.Count > 0 Then
                                    dRow = dt.NewRow()
                                    dRow("Particulars") = dtSub.Rows(0)("gl_glCode") & " - " & dtSub.Rows(0)("gl_Desc")

                                    'Current Year

                                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iYearID & " And Opn_CompID =" & iCOmpID & " And "
                                    iSql = iSql & "Opn_GLID =" & sArray(k) & ""
                                    dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                    If dtArray.Rows.Count > 0 Then
                                        For a = 0 To dtArray.Rows.Count - 1
                                            If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                dDebit = dDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                dTotalDebit = dTotalDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                            End If

                                            If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                dCredit = dCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                dTotalCredit = dTotalCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                            End If
                                        Next
                                    End If


                                    'Last year
                                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_YearID =" & iLastYear & " And Opn_CompID =" & iCOmpID & " And "
                                    iSql = iSql & "Opn_Status ='F' and Opn_GLID =" & sArray(k) & ""
                                    dtArray = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                                    If dtArray.Rows.Count > 0 Then
                                        For a = 0 To dtArray.Rows.Count - 1
                                            If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString() <> "") Then
                                                dLDebit = dLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                                dTotalLDebit = dTotalLDebit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceDebit").ToString())
                                            End If

                                            If (IsDBNull(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString()) = False) And (dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString() <> "") Then
                                                dLCredit = dLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                                dTotalLCredit = dTotalLCredit + Convert.ToDouble(dtArray.Rows(a)("Opn_ClosingBalanceCredit").ToString())
                                            End If
                                        Next
                                    End If

                                    If dDebit > 0 Then
                                        dRow("PresentYear") = dDebit
                                    Else
                                        dRow("PresentYear") = dCredit
                                    End If

                                    If dLDebit > 0 Then
                                        dRow("LastYear") = dLDebit
                                    Else
                                        dRow("LastYear") = dLCredit
                                    End If

                                    dt.Rows.Add(dRow)
                                    dDebit = 0 : dCredit = 0 : dLDebit = 0 : dLCredit = 0
                                End If
                            End If
                        Next

                        dRow = dt.NewRow()
                        dRow("Particulars") = "<B>" & "TOTAL" & "</B>"

                        If (dTotalDebit <> 0) And (dTotalCredit <> 0) Then
                            dRow("PresentYear") = dTotalCredit - dTotalDebit
                        ElseIf (dTotalDebit <> 0) Then
                            dRow("PresentYear") = dTotalDebit
                        ElseIf (dTotalCredit <> 0) Then
                            dRow("PresentYear") = dTotalCredit
                        ElseIf (dTotalDebit = 0) And (dTotalCredit = 0) Then
                            dRow("PresentYear") = "0.00"
                        End If

                        If (dTotalLDebit <> 0) And (dTotalLCredit <> 0) Then
                            dRow("LastYear") = dTotalLCredit - dTotalLDebit
                        ElseIf (dTotalLDebit <> 0) Then
                            dRow("LastYear") = dTotalLDebit
                        ElseIf (dTotalLCredit <> 0) Then
                            dRow("LastYear") = dTotalLCredit
                        ElseIf (dTotalLDebit = 0) And (dTotalLCredit = 0) Then
                            dRow("LastYear") = "0.00"
                        End If

                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow()
                        dRow("Particulars") = ""
                        dt.Rows.Add(dRow)
                        dTotalDebit = 0 : dTotalCredit = 0 : dTotalLDebit = 0 : dTotalLCredit = 0
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDebtorsOrCreditors(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer, ByVal iGL As Integer)
        Dim dt As New DataTable, dtTrans As New DataTable, dtType As New DataTable
        Dim sSql As String = "", aSql As String = ""
        Dim dRow As DataRow
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
        Try
            dt.Columns.Add("Date")
            dt.Columns.Add("VoucherNo")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("DocumentRefNo")
            dt.Columns.Add("OpDebit")
            dt.Columns.Add("OpCredit")
            dt.Columns.Add("TrDebit")
            dt.Columns.Add("TrCredit")
            dt.Columns.Add("ClDebit")
            dt.Columns.Add("ClCredit")
            dt.Columns.Add("Customer")

            sSql = "" : sSql = "Select * from Acc_Transactions_Details where ATD_CompID =" & iCompID & " "
            If iParty > 0 Then
                sSql = sSql & " and ATD_SubGL =" & iParty & ""
            Else
                sSql = sSql & " and ATD_SubGL in(Select gl_id from  chart_of_Accounts where gl_Parent = " & iGL & " and gl_CompID =" & iCompID & ")"
            End If
            sSql = sSql & " Order by ATD_SubGL"

            dtTrans = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dtTrans.Rows.Count > 0 Then

                For j = 0 To dtTrans.Rows.Count - 1


                    'Payment
                    If dtTrans.Rows(j)("ATD_TrType").ToString() = 1 Then
                        aSql = "" : aSql = "Select * from acc_Payment_Master where Acc_PM_ID =" & dtTrans.Rows(j)("ATD_BillID").ToString() & " And Acc_PM_CompID =" & iCompID & ""
                        dtType = objDB.SQLExecuteDataTable(sNameSpace, aSql)
                        If dtType.Rows.Count > 0 Then
                            dRow = dt.NewRow()
                            dRow("Date") = objGen.FormatDtForRDBMS(dtType.Rows(0)("Acc_PM_BillDate").ToString(), "D")
                            dRow("VoucherNo") = dtType.Rows(0)("Acc_PM_TransactionNo").ToString()
                            dRow("TransactionType") = "Purchase"
                            dRow("DocumentRefNo") = dtType.Rows(0)("Acc_PM_BillNo").ToString()
                            dRow("TrDebit") = Convert.ToDecimal(dtTrans.Rows(j)("ATD_Debit").ToString()).ToString("#,##0.00")
                            dRow("TrCredit") = Convert.ToDecimal(dtTrans.Rows(j)("ATD_Credit").ToString()).ToString("#,##0.00")
                            dRow("Customer") = objDB.SQLExecuteScalar(sNameSpace, "SElect gl_Desc from chart_of_Accounts where gl_id = " & dtTrans.Rows(j)("ATD_SubGL").ToString() & " and gl_CompID =" & iCompID & "")
                            dt.Rows.Add(dRow)
                        End If
                    End If


                    'Receipt
                    If dtTrans.Rows(j)("ATD_TrType").ToString() = 3 Then
                        aSql = "" : aSql = "Select * from acc_Receipt_Master where Acc_RM_ID =" & dtTrans.Rows(j)("ATD_BillID").ToString() & " And Acc_RM_CompID =" & iCompID & ""
                        dtType = objDB.SQLExecuteDataTable(sNameSpace, aSql)
                        If dtType.Rows.Count > 0 Then
                            dRow = dt.NewRow()
                            dRow("Date") = objGen.FormatDtForRDBMS(dtType.Rows(0)("Acc_RM_BillDate").ToString(), "D")
                            dRow("VoucherNo") = dtType.Rows(0)("Acc_RM_TransactionNo").ToString()
                            dRow("TransactionType") = "Receipt"
                            dRow("DocumentRefNo") = dtType.Rows(0)("Acc_RM_BillNo").ToString()
                            dRow("TrDebit") = Convert.ToDecimal(dtTrans.Rows(j)("ATD_Debit").ToString()).ToString("#,##0.00")
                            dRow("TrCredit") = Convert.ToDecimal(dtTrans.Rows(j)("ATD_Credit").ToString()).ToString("#,##0.00")
                            dRow("Customer") = objDB.SQLExecuteScalar(sNameSpace, "SElect gl_Desc from chart_of_Accounts where gl_id = " & dtTrans.Rows(j)("ATD_SubGL").ToString() & " and gl_CompID =" & iCompID & "")
                            dt.Rows.Add(dRow)
                        End If
                    End If
                Next

            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDebtorsOrCreditorsGL(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable, dtCr As New DataTable
        Dim dRow As DataRow
        Try
            dtCr.Columns.Add("ID")
            dtCr.Columns.Add("Description")

            sSql = "" : sSql = "Select Acc_GL from acc_Application_Settings where Acc_Types = 'Customer' and Acc_LedgerType = 'Customer' and Acc_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dRow = dtCr.NewRow()
                dRow("ID") = dt.Rows(0)("Acc_GL").ToString()
                dRow("Description") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_glCode + ' - ' + gl_Desc from chart_of_Accounts where gl_id =" & dt.Rows(0)("Acc_GL").ToString() & " and gl_CompID =" & iCompID & "")
                dtCr.Rows.Add(dRow)
            End If

            sSql = "" : sSql = "Select Acc_GL from acc_Application_Settings where Acc_Types = 'Supplier' and Acc_LedgerType = 'Supplier' and Acc_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dRow = dtCr.NewRow()
                dRow("ID") = dt.Rows(0)("Acc_GL").ToString()
                dRow("Description") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_glCode + ' - ' + gl_Desc from chart_of_Accounts where gl_id =" & dt.Rows(0)("Acc_GL").ToString() & " and gl_CompID =" & iCompID & "")
                dtCr.Rows.Add(dRow)
            End If
            Return dtCr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SubLedgerWithDetail(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal PartyID As Integer, ByVal glID As Integer)
        Dim sSql As String = "", aSql As String = "", iSql As String = ""
        Dim pSql As String = ""
        Dim dtGL As New DataTable
        Dim dtSubGL As New DataTable
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
        Dim dRow As DataRow
        Dim dtOB As New DataTable
        Dim sStr As String = ""
        Dim dTotalOpnDebit As Double = 0, dTotalOpnCredit As Double = 0
        Dim dTotalTransDebit As Double = 0, dTotalTransCredit As Double = 0
        Dim dTotalClsDebit As Double = 0, dTotalClsCredit As Double = 0
        Dim dtCheck As New DataTable
        Dim iTotalCheck As Integer = 0
        Dim dtPayment As New DataTable, dtReceipt As New DataTable, dtPettyCash As New DataTable, dtJE As New DataTable
        Dim dtFinal As New DataTable
        Dim dtDateSort As New DataTable, dtPV As New DataTable, dtSV As New DataTable, dtgledger As New DataTable
        Dim dt As New DataTable
        Dim dTransDebit As Double = 0, dTransCredit As Double = 0
        Try
            dt.Columns.Add("BillDate")
            dt.Columns.Add("gl_Code")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("gl_Desc")
            dt.Columns.Add("Narration")
            dt.Columns.Add("CorresGLCode")
            dt.Columns.Add("CorresDescription")
            dt.Columns.Add("OpDebit")
            dt.Columns.Add("OpCredit")
            dt.Columns.Add("TransDebit")
            dt.Columns.Add("TransCredit")
            dt.Columns.Add("ClosingDebit")
            dt.Columns.Add("ClosingCredit")
            dt.Columns.Add("Status")
            sSql = "" : sSql = "Select A.gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
            sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
            sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
            sSql = sSql & "and A.gl_head=2 and A.gl_CompID =" & iCompID & " and A.gl_Delflag='C' and A.gl_Status ='A'"
            If (glID > 0) Then
                sSql = sSql & " and A.gl_ID=" & glID & ""
            End If
            sSql = sSql & " order by A.gl_glcode"
            dtgledger = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtgledger.Rows.Count > 0 Then
                For s = 0 To dtgledger.Rows.Count - 1
                    iTotalCheck = 0
                    dTotalOpnDebit = 0 : dTotalOpnCredit = 0
                    dTotalTransDebit = 0 : dTotalTransCredit = 0
                    dTotalClsDebit = 0 : dTotalClsCredit = 0
                    dRow = dt.NewRow()
                    If IsDBNull(dtgledger.Rows(s)("gl_glCode").ToString()) = False Then
                        dRow("gl_Code") = dtgledger.Rows(s)("gl_glCode").ToString()
                    End If
                    If IsDBNull(dtgledger.Rows(s)("Gl_Desc").ToString()) = False Then
                        dRow("Gl_Desc") = dtgledger.Rows(s)("Gl_Desc").ToString()
                    End If
                    dRow("BillDate") = "01/01/1900"
                    dRow("Status") = "GL"
                    dRow("OpDebit") = GetTotalDabitAmount(sNameSpace, iCompID, dtgledger.Rows(s)("gl_id"))
                    dRow("OpCredit") = GetTotalCreditAmount(sNameSpace, iCompID, dtgledger.Rows(s)("gl_id"))
                    dRow("ClosingDebit") = "0"
                    dRow("TransDebit") = GetTotalTrnctnDabitAmount(sNameSpace, iCompID, dtgledger.Rows(s)("gl_id"))
                    dRow("TransCredit") = GetTotalTrnCreditAmount(sNameSpace, iCompID, dtgledger.Rows(s)("gl_id"))
                    dt.Rows.Add(dRow)

                    sSql = "" : sSql = "Select A.gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
                    sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
                    sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
                    sSql = sSql & "and A.gl_head in(2,3) and A.gl_CompID =" & iCompID & " and A.gl_Delflag='C' and A.gl_Status ='A'"

                    If (glID > 0) Then
                        sSql = sSql & " and A.gl_Parent=" & glID & ""
                    Else
                        sSql = sSql & " and A.gl_Parent=" & dtgledger.Rows(s)("gl_id") & ""
                    End If

                    sSql = sSql & " order by A.gl_glcode"

                    dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    If dtGL.Rows.Count > 0 Then
                        For i = 0 To dtGL.Rows.Count - 1
                            iTotalCheck = 0
                            dTotalOpnDebit = 0 : dTotalOpnCredit = 0
                            dTotalTransDebit = 0 : dTotalTransCredit = 0
                            dTotalClsDebit = 0 : dTotalClsCredit = 0
                            dRow = dt.NewRow()
                            If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
                                dRow("gl_Code") = dtGL.Rows(i)("gl_glCode").ToString()
                            End If
                            If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
                                dRow("Gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
                            End If
                            dRow("BillDate") = "01/01/1900"
                            dRow("Status") = "0"
                            iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtGL.Rows(i)("gl_ID").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
                            dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                            If dtOB.Rows.Count > 0 Then

                                If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
                                    dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                                    dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
                                Else
                                    dRow("OpDebit") = "0.00"
                                End If
                                If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
                                    dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                                    dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
                                Else
                                    dRow("OpCredit") = "0.00"
                                End If

                                If IsDBNull(dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString()) = False Then
                                    If dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString() <> "" Then
                                        dRow("ClosingDebit") = dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString()
                                    Else
                                        dRow("ClosingDebit") = "0.00"
                                    End If
                                Else
                                    dRow("ClosingDebit") = "0.00"
                                End If

                                If IsDBNull(dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString()) = False Then
                                    If dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString() <> "" Then
                                        dRow("ClosingCredit") = dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString()
                                    Else
                                        dRow("ClosingCredit") = "0.00"
                                    End If
                                Else
                                    dRow("ClosingCredit") = "0.00"
                                End If

                                dRow("TransDebit") = GetTotalSubGlnDabitAmount(sNameSpace, iCompID, dtGL.Rows(i)("gl_ID").ToString())
                                dRow("TransCredit") = GetTotalSubglCreditAmount(sNameSpace, iCompID, dtGL.Rows(i)("gl_ID").ToString())

                            End If
                            dt.Rows.Add(dRow)
                            'SubLeger Opening Balance
                            If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_SLJE_BillDate as BillDate, C.Acc_SLJE_RefNo as BillNo,C.Acc_SLJE_TransactionNo as VoucherNo "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 7 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_Ledger_JE_Master C On B.ATD_BillID = C.Acc_SLJE_ID and C.Acc_SLJE_Status = 'A' "

                            ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_SLJE_BillDate as BillDate, C.Acc_SLJE_RefNo as BillNo,C.Acc_SLJE_TransactionNo as VoucherNo "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 7 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_Ledger_JE_Master C On B.ATD_BillID = C.Acc_SLJE_ID and C.Acc_SLJE_Status = 'A' "
                            End If
                            dtJE = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                            For j = 0 To dtJE.Rows.Count - 1
                                dRow = dt.NewRow()
                                dRow("BillDate") = dtJE.Rows(j)("BillDate").ToString()
                                dRow("TransactionType") = "SubLedger BillWise Entry"
                                dRow("Narration") = "Bill Date:-" & dtJE.Rows(j)("BillDate").ToString() & "/Bill Reference No:-" & dtJE.Rows(j)("BillNo").ToString()
                                'dRow("CorresGLCode") = dtJE.Rows(j)("CorresGLcode").ToString()
                                'dRow("CorresDescription") = dtJE.Rows(j)("CorresDescription").ToString()

                                If dtJE.Rows(j)("Debit").ToString() = "" Then
                                    dRow("TransDebit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransDebit") = dtJE.Rows(j)("Debit").ToString()
                                End If

                                If dtJE.Rows(j)("Credit").ToString() = "" Then
                                    dRow("TransCredit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransCredit") = dtJE.Rows(j)("Credit").ToString()
                                End If
                                dRow("Status") = "1"
                                dt.Rows.Add(dRow)
                            Next

                            'Payment     
                            If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_PM_BillDate as BillDate, C.Acc_PM_BillNo as BillNo,C.Acc_PM_TransactionNo as VoucherNo, C.Acc_Bill_Narration,"
                                aSql = aSql & "C.Acc_PM_Party as party,C.Acc_PM_ChequeNo as chequeNo,C.Acc_PM_ChequeDate "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 1 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_Payment_Master C On B.ATD_BillID = C.Acc_PM_ID And C.Acc_PM_Status = 'A'"

                            ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_PM_BillDate as BillDate, C.Acc_PM_BillNo as BillNo,C.Acc_PM_TransactionNo as VoucherNo, C.Acc_Bill_Narration,"
                                aSql = aSql & "C.Acc_PM_Party as party,C.Acc_PM_ChequeNo as chequeNo,C.Acc_PM_ChequeDate "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 1 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_Payment_Master C On B.ATD_BillID = C.Acc_PM_ID And C.Acc_PM_Status = 'A'"
                            End If

                            dtPayment = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                            For j = 0 To dtPayment.Rows.Count - 1
                                dRow = dt.NewRow()
                                dRow("BillDate") = dtPayment.Rows(j)("BillDate").ToString()
                                dRow("TransactionType") = "Payment"
                                dRow("Narration") = dtPayment.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtPayment.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtPayment.Rows(j)("ChequeNo").ToString()

                                If dtPayment.Rows(j)("Debit").ToString() = "" Then
                                    dRow("TransDebit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransDebit") = dtPayment.Rows(j)("Debit").ToString()
                                End If

                                If dtPayment.Rows(j)("Credit").ToString() = "" Then
                                    dRow("TransCredit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransCredit") = dtPayment.Rows(j)("Credit").ToString()
                                End If
                                dRow("Status") = "1"
                                dt.Rows.Add(dRow)
                            Next
                            'PettyCash                   
                            If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_PCM_BillDate as BillDate, C.Acc_PCM_BillNo as BillNo,C.Acc_PCM_TransactionNo as VoucherNo, C.Acc_PCM_PaymentNarration,"
                                aSql = aSql & "C.Acc_PCM_Party as party "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 2 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_PettyCash_Master C On B.ATD_BillID = C.Acc_PCM_ID and C.Acc_PCM_Status = 'A' "

                            ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_PCM_BillDate as BillDate, C.Acc_PCM_BillNo as BillNo,C.Acc_PCM_TransactionNo as VoucherNo, C.Acc_PCM_PaymentNarration,"
                                aSql = aSql & "C.Acc_PCM_Party as party "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 2 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_PettyCash_Master C On B.ATD_BillID = C.Acc_PCM_ID and C.Acc_PCM_Status = 'A' "
                            End If
                            dtPettyCash = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                            For j = 0 To dtPettyCash.Rows.Count - 1
                                dRow = dt.NewRow()
                                dRow("BillDate") = dtPettyCash.Rows(j)("BillDate").ToString()
                                dRow("TransactionType") = "Petty Cash"
                                dRow("Narration") = dtPettyCash.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtPettyCash.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "")

                                If dtPettyCash.Rows(j)("Debit").ToString() = "" Then
                                    dRow("TransDebit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransDebit") = dtPettyCash.Rows(j)("Debit").ToString()
                                End If

                                If dtPettyCash.Rows(j)("Credit").ToString() = "" Then
                                    dRow("TransCredit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransCredit") = dtPettyCash.Rows(j)("Credit").ToString()
                                End If

                                dRow("Status") = "1"
                                dt.Rows.Add(dRow)
                            Next
                            'Receipt                   
                            If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_RM_BillDate as BillDate, C.Acc_RM_BillNo as BillNo,C.Acc_RM_TransactionNo as VoucherNo, C.Acc_RM_BillNarration,"
                                aSql = aSql & "C.Acc_RM_Party as party,C.Acc_RM_ChequeNo as chequeNo,C.Acc_RM_ChequeDate "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 3 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_Receipt_Master C On B.ATD_BillID = C.Acc_RM_ID and C.Acc_RM_Status = 'A'"

                            ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_RM_BillDate as BillDate, C.Acc_RM_BillNo as BillNo,C.Acc_RM_TransactionNo as VoucherNo, C.Acc_RM_BillNarration,"
                                aSql = aSql & "C.Acc_RM_Party as party,C.Acc_RM_ChequeNo as chequeNo,C.Acc_RM_ChequeDate "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 3 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_Receipt_Master C On B.ATD_BillID = C.Acc_RM_ID and C.Acc_RM_Status = 'A'"
                            End If
                            dtReceipt = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                            For j = 0 To dtReceipt.Rows.Count - 1
                                dRow = dt.NewRow()
                                dRow("BillDate") = dtReceipt.Rows(j)("BillDate").ToString()
                                dRow("TransactionType") = "Receipt"
                                dRow("Narration") = dtReceipt.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtReceipt.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtReceipt.Rows(j)("ChequeNo").ToString()

                                If dtReceipt.Rows(j)("Debit").ToString() = "" Then
                                    dRow("TransDebit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransDebit") = dtReceipt.Rows(j)("Debit").ToString()
                                End If

                                If dtReceipt.Rows(j)("Credit").ToString() = "" Then
                                    dRow("TransCredit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransCredit") = dtReceipt.Rows(j)("Credit").ToString()
                                End If

                                dRow("Status") = "1"
                                dt.Rows.Add(dRow)
                            Next
                            'Purchase voucher                 
                            If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_Purchase_BillDate as BillDate, C.Acc_Purchase_BillNo as BillNo,C.Acc_Purchase_TransactionNo as VoucherNo,"
                                aSql = aSql & "C.Acc_Purchase_Party as party "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 8 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join acc_purchase_Master C On B.ATD_BillID = C.Acc_Purchase_ID and C.Acc_Purchase_DelFlag = 'A' "

                            ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_Purchase_BillDate as BillDate, C.Acc_Purchase_BillNo as BillNo,C.Acc_Purchase_TransactionNo as VoucherNo,"
                                aSql = aSql & "C.Acc_Purchase_Party as party "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 8 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join acc_purchase_Master C On B.ATD_BillID = C.Acc_Purchase_ID and C.Acc_Purchase_DelFlag = 'A' "
                            End If
                            dtPV = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                            For j = 0 To dtPV.Rows.Count - 1
                                dRow = dt.NewRow()
                                dRow("BillDate") = dtPV.Rows(j)("BillDate").ToString()
                                dRow("TransactionType") = "Purchase Voucher"
                                dRow("Narration") = dtPV.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select CSM_Name from CustomerSupplierMaster where CSM_ID =" & dtPV.Rows(j)("Party").ToString() & " and CSM_CompID =" & iCompID & "")

                                If dtPV.Rows(j)("Debit").ToString() = "" Then
                                    dRow("TransDebit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransDebit") = dtPV.Rows(j)("Debit").ToString()
                                End If

                                If dtPV.Rows(j)("Credit").ToString() = "" Then
                                    dRow("TransCredit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransCredit") = dtPV.Rows(j)("Credit").ToString()
                                End If

                                dRow("Status") = "1"
                                dt.Rows.Add(dRow)
                            Next
                            'Sales voucher                 
                            If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                                aSql = "" : aSql = "Select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_Sales_BillDate as BillDate, C.Acc_Sales_BillNo as BillNo,C.Acc_Sales_TransactionNo as VoucherNo,"
                                aSql = aSql & "C.Acc_Sales_Party as party "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 9 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join acc_Sales_masters C On B.ATD_BillID = C.Acc_Sales_ID and C.Acc_Sales_DelFlag = 'A' "

                            ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_Sales_BillDate as BillDate, C.Acc_Sales_BillNo as BillNo,C.Acc_Sales_TransactionNo as VoucherNo,"
                                aSql = aSql & "C.Acc_Sales_Party as party "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 9 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join acc_Sales_masters C On B.ATD_BillID = C.Acc_Sales_ID and C.Acc_Sales_DelFlag = 'A' "
                            End If
                            dtSV = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                            For j = 0 To dtSV.Rows.Count - 1
                                dRow = dt.NewRow()
                                dRow("BillDate") = dtSV.Rows(j)("BillDate").ToString()
                                dRow("TransactionType") = "Sales Voucher"
                                dRow("Narration") = dtSV.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select BM_Name from sales_Buyers_Masters where BM_ID =" & dtSV.Rows(j)("Party").ToString() & " and BM_CompID =" & iCompID & "")

                                If dtSV.Rows(j)("Debit").ToString() = "" Then
                                    dRow("TransDebit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransDebit") = dtSV.Rows(j)("Debit").ToString()
                                End If
                                If dtSV.Rows(j)("Credit").ToString() = "" Then
                                    dRow("TransCredit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransCredit") = dtSV.Rows(j)("Credit").ToString()
                                End If
                                dRow("Status") = "1"
                                dt.Rows.Add(dRow)
                            Next
                            'Journal Entry                    
                            If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_JE_BillDate as BillDate, C.Acc_JE_BillNo as BillNo,C.Acc_JE_TransactionNo as VoucherNo, C.Acc_JE_PaymentNarration,"
                                aSql = aSql & "C.Acc_JE_Party as party,C.Acc_JE_ChequeNo as chequeNo,C.Acc_JE_ChequeDate "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 4 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_JE_Master C On B.ATD_BillID = C.Acc_JE_ID and C.Acc_JE_Status = 'A' "

                            ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                                aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                                aSql = aSql & "C.Acc_JE_BillDate as BillDate, C.Acc_JE_BillNo as BillNo,C.Acc_JE_TransactionNo as VoucherNo, C.Acc_JE_PaymentNarration,"
                                aSql = aSql & "C.Acc_JE_Party as party,C.Acc_JE_ChequeNo as chequeNo,C.Acc_JE_ChequeDate "
                                aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                                aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 4 and B.ATD_YearID = " & iYearId & " and "
                                aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                                aSql = aSql & "join Acc_JE_Master C On B.ATD_BillID = C.Acc_JE_ID and C.Acc_JE_Status = 'A' "
                            End If
                            dtJE = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                            For j = 0 To dtJE.Rows.Count - 1
                                dRow = dt.NewRow()
                                dRow("BillDate") = dtJE.Rows(j)("BillDate").ToString()
                                dRow("TransactionType") = "Journal Entry"
                                dRow("Narration") = dtJE.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtJE.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtJE.Rows(j)("ChequeNo").ToString()
                                dRow("CorresGLCode") = dtJE.Rows(j)("CorresGLcode").ToString()
                                dRow("CorresDescription") = dtJE.Rows(j)("CorresDescription").ToString()

                                If dtJE.Rows(j)("Debit").ToString() = "" Then
                                    dRow("TransDebit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransDebit") = dtJE.Rows(j)("Debit").ToString()
                                End If

                                If dtJE.Rows(j)("Credit").ToString() = "" Then
                                    dRow("TransCredit") = "0.00" : dTransDebit = 0
                                Else
                                    dRow("TransCredit") = dtJE.Rows(j)("Credit").ToString()
                                End If
                                dRow("Status") = "1"
                                dt.Rows.Add(dRow)
                            Next
                        Next
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function PartySubledgerLedgerWithDetail(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal PartyID As Integer, ByVal glID As Integer)
        Dim sSql As String = "", aSql As String = "", iSql As String = ""
        Dim pSql As String = ""
        Dim dtGL As New DataTable
        Dim dtSubGL As New DataTable
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
        Dim dRow As DataRow
        Dim dtOB As New DataTable
        Dim sStr As String = ""
        Dim dTotalOpnDebit As Double = 0, dTotalOpnCredit As Double = 0
        Dim dTotalTransDebit As Double = 0, dTotalTransCredit As Double = 0
        Dim dTotalClsDebit As Double = 0, dTotalClsCredit As Double = 0
        Dim dtCheck As New DataTable
        Dim iTotalCheck As Integer = 0
        Dim dtPayment As New DataTable, dtReceipt As New DataTable, dtPettyCash As New DataTable, dtJE As New DataTable
        Dim dtFinal As New DataTable
        Dim dtDateSort As New DataTable, dtPV As New DataTable, dtSV As New DataTable, dtgledger As New DataTable
        Dim dt As New DataTable
        Dim dTransDebit As Double = 0, dTransCredit As Double = 0
        Try
            dt.Columns.Add("BillDate")
            dt.Columns.Add("gl_Code")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("gl_Desc")
            dt.Columns.Add("Narration")
            dt.Columns.Add("CorresGLCode")
            dt.Columns.Add("CorresDescription")
            dt.Columns.Add("OpDebit")
            dt.Columns.Add("OpCredit")
            dt.Columns.Add("TransDebit")
            dt.Columns.Add("TransCredit")
            dt.Columns.Add("ClosingDebit")
            dt.Columns.Add("ClosingCredit")
            dt.Columns.Add("Status")

            sSql = "" : sSql = "Select A.gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
            sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
            sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
            sSql = sSql & "and A.gl_head in(2,3) and A.gl_CompID =" & iCompID & " and A.gl_Delflag='C' and A.gl_Status ='A'"
            If (PartyID > 0) Then
                sSql = sSql & " and A.gl_id=" & PartyID & ""
            End If
            sSql = sSql & " order by A.gl_glcode"
            dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGL.Rows.Count > 0 Then
                For i = 0 To dtGL.Rows.Count - 1
                    iTotalCheck = 0
                    dTotalOpnDebit = 0 : dTotalOpnCredit = 0
                    dTotalTransDebit = 0 : dTotalTransCredit = 0
                    dTotalClsDebit = 0 : dTotalClsCredit = 0
                    dRow = dt.NewRow()
                    If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
                        dRow("gl_Code") = dtGL.Rows(i)("gl_glCode").ToString()
                    End If
                    If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
                        dRow("Gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
                    End If
                    dRow("BillDate") = "01/01/1900"
                    dRow("Status") = "0"
                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtGL.Rows(i)("gl_ID").ToString() & "' and  Opn_YearID =" & iYearId & " and Opn_CompID =" & iCompID & ""
                    dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                    If dtOB.Rows.Count > 0 Then

                        If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
                            dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                            dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
                        Else
                            dRow("OpDebit") = "0.00"
                        End If
                        If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
                            dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                            dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
                        Else
                            dRow("OpCredit") = "0.00"
                        End If

                        If IsDBNull(dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString()) = False Then
                            If dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString() <> "" Then
                                dRow("ClosingDebit") = dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString()
                            Else
                                dRow("ClosingDebit") = "0.00"
                            End If
                        Else
                            dRow("ClosingDebit") = "0.00"
                        End If

                        If IsDBNull(dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString()) = False Then
                            If dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString() <> "" Then
                                dRow("ClosingCredit") = dtOB.Rows(0)("Opn_ClosingBalanceCredit").ToString()
                            Else
                                dRow("ClosingCredit") = "0.00"
                            End If
                        Else
                            dRow("ClosingCredit") = "0.00"
                        End If
                    End If
                    dt.Rows.Add(dRow)
                    'SubLeger Opening Balance
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_SLJE_BillDate as BillDate, C.Acc_SLJE_RefNo as BillNo,C.Acc_SLJE_TransactionNo as VoucherNo "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 7 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Ledger_JE_Master C On B.ATD_BillID = C.Acc_SLJE_ID and C.Acc_SLJE_Status = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_SLJE_BillDate as BillDate, C.Acc_SLJE_RefNo as BillNo,C.Acc_SLJE_TransactionNo as VoucherNo "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 7 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Ledger_JE_Master C On B.ATD_BillID = C.Acc_SLJE_ID and C.Acc_SLJE_Status = 'A' "
                    End If
                    dtJE = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtJE.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtJE.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "SubLedger BillWise Entry"
                        dRow("Narration") = "Bill Date:-" & dtJE.Rows(j)("BillDate").ToString() & "/Bill Reference No:-" & dtJE.Rows(j)("BillNo").ToString()

                        If dtJE.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtJE.Rows(j)("Debit").ToString()
                        End If

                        If dtJE.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtJE.Rows(j)("Credit").ToString()
                        End If
                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next

                    'Payment     
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_PM_BillDate as BillDate, C.Acc_PM_BillNo as BillNo,C.Acc_PM_TransactionNo as VoucherNo, C.Acc_Bill_Narration,"
                        aSql = aSql & "C.Acc_PM_Party as party,C.Acc_PM_ChequeNo as chequeNo,C.Acc_PM_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 1 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Payment_Master C On B.ATD_BillID = C.Acc_PM_ID And C.Acc_PM_Status = 'A'"

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_PM_BillDate as BillDate, C.Acc_PM_BillNo as BillNo,C.Acc_PM_TransactionNo as VoucherNo, C.Acc_Bill_Narration,"
                        aSql = aSql & "C.Acc_PM_Party as party,C.Acc_PM_ChequeNo as chequeNo,C.Acc_PM_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 1 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Payment_Master C On B.ATD_BillID = C.Acc_PM_ID And C.Acc_PM_Status = 'A'"
                    End If

                    dtPayment = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtPayment.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtPayment.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Payment"
                        dRow("Narration") = dtPayment.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtPayment.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtPayment.Rows(j)("ChequeNo").ToString()

                        If dtPayment.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtPayment.Rows(j)("Debit").ToString()
                        End If

                        If dtPayment.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtPayment.Rows(j)("Credit").ToString()
                        End If
                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next

                    'PettyCash                   
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_PCM_BillDate as BillDate, C.Acc_PCM_BillNo as BillNo,C.Acc_PCM_TransactionNo as VoucherNo, C.Acc_PCM_PaymentNarration,"
                        aSql = aSql & "C.Acc_PCM_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 2 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_PettyCash_Master C On B.ATD_BillID = C.Acc_PCM_ID and C.Acc_PCM_Status = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_PCM_BillDate as BillDate, C.Acc_PCM_BillNo as BillNo,C.Acc_PCM_TransactionNo as VoucherNo, C.Acc_PCM_PaymentNarration,"
                        aSql = aSql & "C.Acc_PCM_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 2 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_PettyCash_Master C On B.ATD_BillID = C.Acc_PCM_ID and C.Acc_PCM_Status = 'A' "
                    End If

                    dtPettyCash = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtPettyCash.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtPettyCash.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Petty Cash"
                        dRow("Narration") = dtPettyCash.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtPettyCash.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "")

                        If dtPettyCash.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtPettyCash.Rows(j)("Debit").ToString()
                        End If

                        If dtPettyCash.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtPettyCash.Rows(j)("Credit").ToString()
                        End If

                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next
                    'Receipt                   
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_RM_BillDate as BillDate, C.Acc_RM_BillNo as BillNo,C.Acc_RM_TransactionNo as VoucherNo, C.Acc_RM_BillNarration,"
                        aSql = aSql & "C.Acc_RM_Party as party,C.Acc_RM_ChequeNo as chequeNo,C.Acc_RM_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 3 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Receipt_Master C On B.ATD_BillID = C.Acc_RM_ID and C.Acc_RM_Status = 'A'"

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_RM_BillDate as BillDate, C.Acc_RM_BillNo as BillNo,C.Acc_RM_TransactionNo as VoucherNo, C.Acc_RM_BillNarration,"
                        aSql = aSql & "C.Acc_RM_Party as party,C.Acc_RM_ChequeNo as chequeNo,C.Acc_RM_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 3 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Receipt_Master C On B.ATD_BillID = C.Acc_RM_ID and C.Acc_RM_Status = 'A'"
                    End If
                    dtReceipt = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtReceipt.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtReceipt.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Receipt"
                        dRow("Narration") = dtReceipt.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtReceipt.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtReceipt.Rows(j)("ChequeNo").ToString()

                        If dtReceipt.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtReceipt.Rows(j)("Debit").ToString()
                        End If

                        If dtReceipt.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtReceipt.Rows(j)("Credit").ToString()
                        End If

                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next
                    'Purchase voucher                 
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_Purchase_BillDate as BillDate, C.Acc_Purchase_BillNo as BillNo,C.Acc_Purchase_TransactionNo as VoucherNo,"
                        aSql = aSql & "C.Acc_Purchase_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 8 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join acc_purchase_Master C On B.ATD_BillID = C.Acc_Purchase_ID and C.Acc_Purchase_DelFlag = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_Purchase_BillDate as BillDate, C.Acc_Purchase_BillNo as BillNo,C.Acc_Purchase_TransactionNo as VoucherNo,"
                        aSql = aSql & "C.Acc_Purchase_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 8 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join acc_purchase_Master C On B.ATD_BillID = C.Acc_Purchase_ID and C.Acc_Purchase_DelFlag = 'A' "
                    End If
                    dtPV = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtPV.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtPV.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Purchase Voucher"
                        dRow("Narration") = dtPV.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select CSM_Name from CustomerSupplierMaster where CSM_ID =" & dtPV.Rows(j)("Party").ToString() & " and CSM_CompID =" & iCompID & "")

                        If dtPV.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtPV.Rows(j)("Debit").ToString()
                        End If

                        If dtPV.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtPV.Rows(j)("Credit").ToString()
                        End If

                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next
                    'Sales voucher                 
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "Select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_Sales_BillDate as BillDate, C.Acc_Sales_BillNo as BillNo,C.Acc_Sales_TransactionNo as VoucherNo,"
                        aSql = aSql & "C.Acc_Sales_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 9 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join acc_Sales_masters C On B.ATD_BillID = C.Acc_Sales_ID and C.Acc_Sales_DelFlag = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_Sales_BillDate as BillDate, C.Acc_Sales_BillNo as BillNo,C.Acc_Sales_TransactionNo as VoucherNo,"
                        aSql = aSql & "C.Acc_Sales_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 9 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join acc_Sales_masters C On B.ATD_BillID = C.Acc_Sales_ID and C.Acc_Sales_DelFlag = 'A' "
                    End If
                    dtSV = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtSV.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtSV.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Sales Voucher"
                        dRow("Narration") = dtSV.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select BM_Name from sales_Buyers_Masters where BM_ID =" & dtSV.Rows(j)("Party").ToString() & " and BM_CompID =" & iCompID & "")

                        If dtSV.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtSV.Rows(j)("Debit").ToString()
                        End If

                        If dtSV.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtSV.Rows(j)("Credit").ToString()
                        End If

                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next

                    'Journal Entry                    
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_JE_BillDate as BillDate, C.Acc_JE_BillNo as BillNo,C.Acc_JE_TransactionNo as VoucherNo, C.Acc_JE_PaymentNarration,"
                        aSql = aSql & "C.Acc_JE_Party as party,C.Acc_JE_ChequeNo as chequeNo,C.Acc_JE_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 4 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_JE_Master C On B.ATD_BillID = C.Acc_JE_ID and C.Acc_JE_Status = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_JE_BillDate as BillDate, C.Acc_JE_BillNo as BillNo,C.Acc_JE_TransactionNo as VoucherNo, C.Acc_JE_PaymentNarration,"
                        aSql = aSql & "C.Acc_JE_Party as party,C.Acc_JE_ChequeNo as chequeNo,C.Acc_JE_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 4 and B.ATD_YearID = " & iYearId & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_JE_Master C On B.ATD_BillID = C.Acc_JE_ID and C.Acc_JE_Status = 'A' "
                    End If
                    dtJE = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtJE.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtJE.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Journal Entry"
                        dRow("Narration") = dtJE.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtJE.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtJE.Rows(j)("ChequeNo").ToString()
                        dRow("CorresGLCode") = dtJE.Rows(j)("CorresGLcode").ToString()
                        dRow("CorresDescription") = dtJE.Rows(j)("CorresDescription").ToString()

                        If dtJE.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtJE.Rows(j)("Debit").ToString()
                        End If

                        If dtJE.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtJE.Rows(j)("Credit").ToString()
                        End If
                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next
                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDebtorsOrCreditorss(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer, ByVal iGL As Integer, ByVal iYearID As Integer)

        Dim sSql As String = "", aSql As String = "", iSql As String = ""
        Dim pSql As String = ""
        Dim dtGL As New DataTable
        Dim dtSubGL As New DataTable
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
        Dim dRow As DataRow
        Dim dtOB As New DataTable
        Dim sStr As String = ""
        Dim dTotalOpnDebit As Double = 0, dTotalOpnCredit As Double = 0
        Dim dTotalTransDebit As Double = 0, dTotalTransCredit As Double = 0
        Dim dTotalClsDebit As Double = 0, dTotalClsCredit As Double = 0
        Dim dtCheck As New DataTable
        Dim iTotalCheck As Integer = 0
        Dim dtPayment As New DataTable, dtReceipt As New DataTable, dtPettyCash As New DataTable, dtJE As New DataTable
        Dim dtFinal As New DataTable
        Dim dtDateSort As New DataTable, dtPV As New DataTable, dtSV As New DataTable

        Dim dt As New DataTable
        Dim dTransDebit As Double = 0, dTransCredit As Double = 0
        Try
            dt.Columns.Add("Date")
            dt.Columns.Add("VoucherNo")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("DocumentRefNo")
            dt.Columns.Add("OpDebit")
            dt.Columns.Add("OpCredit")
            dt.Columns.Add("TrDebit")
            dt.Columns.Add("TrCredit")
            dt.Columns.Add("ClDebit")
            dt.Columns.Add("ClCredit")
            dt.Columns.Add("Customer")
            sSql = "" : sSql = "Select A.gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
            sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
            sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
            sSql = sSql & "and A.gl_head in (2,3) and A.gl_CompID =" & iCompID & " and A.gl_Delflag='C' and A.gl_Status ='A' and A.gl_Parent=" & iGL & " order by A.gl_glcode "
            dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGL.Rows.Count > 0 Then
                For i = 0 To dtGL.Rows.Count - 1
                    iTotalCheck = 0
                    dTotalOpnDebit = 0 : dTotalOpnCredit = 0
                    dTotalTransDebit = 0 : dTotalTransCredit = 0
                    dTotalClsDebit = 0 : dTotalClsCredit = 0
                    dRow = dt.NewRow()


                    'If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
                    '    dRow("gl_Code") = dtGL.Rows(i)("gl_glCode").ToString()
                    'End If

                    'If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
                    '    dRow("Gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
                    'End If
                    dRow("Date") = "01/01/1900"
                    dRow("VoucherNo") = "01/01/1900"
                    'dRow("Status") = "0"

                    iSql = "" : iSql = "Select * from acc_Opening_Balance where Opn_GLID ='" & dtGL.Rows(i)("gl_ID").ToString() & "' and  Opn_YearID =" & iYearID & " and Opn_CompID =" & iCompID & ""
                    dtOB = objDB.SQLExecuteDataSet(sNameSpace, iSql).Tables(0)
                    If dtOB.Rows.Count > 0 Then

                        If IsDBNull(dtOB.Rows(0)("Opn_DebitAmt").ToString()) = False Then
                            dRow("OpDebit") = dtOB.Rows(0)("Opn_DebitAmt").ToString()
                            dTotalOpnDebit = dTotalOpnDebit + dtOB.Rows(0)("Opn_DebitAmt").ToString()
                        Else
                            dRow("OpDebit") = "0.00"
                        End If

                        If IsDBNull(dtOB.Rows(0)("Opn_CreditAmount").ToString()) = False Then
                            dRow("OpCredit") = dtOB.Rows(0)("Opn_CreditAmount").ToString()
                            dTotalOpnCredit = dTotalOpnCredit + dtOB.Rows(0)("Opn_CreditAmount").ToString()
                        Else
                            dRow("OpCredit") = "0.00"
                        End If

                        If IsDBNull(dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString()) = False Then
                            If dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString() <> "" Then
                                dRow("ClosingDebit") = dtOB.Rows(0)("Opn_ClosingBalanceDebit").ToString()
                            Else
                                dRow("ClosingDebit") = "0.00"
                            End If
                        Else
                            dRow("ClosingDebit") = "0.00"
                        End If

                    End If
                    dt.Rows.Add(dRow)


                    'Payment     
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_PM_BillDate as BillDate, C.Acc_PM_BillNo as BillNo,C.Acc_PM_TransactionNo as VoucherNo, C.Acc_Bill_Narration,"
                        aSql = aSql & "C.Acc_PM_Party as party,C.Acc_PM_ChequeNo as chequeNo,C.Acc_PM_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 1 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Payment_Master C On B.ATD_BillID = C.Acc_PM_ID And C.Acc_PM_Status = 'A'"

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_PM_BillDate as BillDate, C.Acc_PM_BillNo as BillNo,C.Acc_PM_TransactionNo as VoucherNo, C.Acc_Bill_Narration,"
                        aSql = aSql & "C.Acc_PM_Party as party,C.Acc_PM_ChequeNo as chequeNo,C.Acc_PM_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 1 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Payment_Master C On B.ATD_BillID = C.Acc_PM_ID And C.Acc_PM_Status = 'A'"
                    End If

                    dtPayment = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtPayment.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtPayment.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Payment"
                        dRow("Narration") = dtPayment.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtPayment.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtPayment.Rows(j)("ChequeNo").ToString()

                        If dtPayment.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtPayment.Rows(j)("Debit").ToString()
                        End If

                        If dtPayment.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtPayment.Rows(j)("Credit").ToString()
                        End If
                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next

                    'PettyCash                   
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_PCM_BillDate as BillDate, C.Acc_PCM_BillNo as BillNo,C.Acc_PCM_TransactionNo as VoucherNo, C.Acc_PCM_PaymentNarration,"
                        aSql = aSql & "C.Acc_PCM_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 2 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_PettyCash_Master C On B.ATD_BillID = C.Acc_PCM_ID and C.Acc_PCM_Status = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_PCM_BillDate as BillDate, C.Acc_PCM_BillNo as BillNo,C.Acc_PCM_TransactionNo as VoucherNo, C.Acc_PCM_PaymentNarration,"
                        aSql = aSql & "C.Acc_PCM_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 2 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_PettyCash_Master C On B.ATD_BillID = C.Acc_PCM_ID and C.Acc_PCM_Status = 'A' "
                    End If

                    dtPettyCash = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtPettyCash.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtPettyCash.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Petty Cash"
                        dRow("Narration") = dtPettyCash.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtPettyCash.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "")

                        If dtPettyCash.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtPettyCash.Rows(j)("Debit").ToString()
                        End If

                        If dtPettyCash.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtPettyCash.Rows(j)("Credit").ToString()
                        End If

                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next


                    'Receipt                   
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_RM_BillDate as BillDate, C.Acc_RM_BillNo as BillNo,C.Acc_RM_TransactionNo as VoucherNo, C.Acc_RM_BillNarration,"
                        aSql = aSql & "C.Acc_RM_Party as party,C.Acc_RM_ChequeNo as chequeNo,C.Acc_RM_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 3 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Receipt_Master C On B.ATD_BillID = C.Acc_RM_ID and C.Acc_RM_Status = 'A'"

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_RM_BillDate as BillDate, C.Acc_RM_BillNo as BillNo,C.Acc_RM_TransactionNo as VoucherNo, C.Acc_RM_BillNarration,"
                        aSql = aSql & "C.Acc_RM_Party as party,C.Acc_RM_ChequeNo as chequeNo,C.Acc_RM_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 3 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Receipt_Master C On B.ATD_BillID = C.Acc_RM_ID and C.Acc_RM_Status = 'A'"
                    End If
                    dtReceipt = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtReceipt.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtReceipt.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Receipt"
                        dRow("Narration") = dtReceipt.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtReceipt.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtReceipt.Rows(j)("ChequeNo").ToString()

                        If dtReceipt.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtReceipt.Rows(j)("Debit").ToString()
                        End If

                        If dtReceipt.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtReceipt.Rows(j)("Credit").ToString()
                        End If

                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next


                    'Journal Entry                    
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_JE_BillDate as BillDate, C.Acc_JE_BillNo as BillNo,C.Acc_JE_TransactionNo as VoucherNo, C.Acc_JE_PaymentNarration,"
                        aSql = aSql & "C.Acc_JE_Party as party,C.Acc_JE_ChequeNo as chequeNo,C.Acc_JE_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 4 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_JE_Master C On B.ATD_BillID = C.Acc_JE_ID and C.Acc_JE_Status = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_JE_BillDate as BillDate, C.Acc_JE_BillNo as BillNo,C.Acc_JE_TransactionNo as VoucherNo, C.Acc_JE_PaymentNarration,"
                        aSql = aSql & "C.Acc_JE_Party as party,C.Acc_JE_ChequeNo as chequeNo,C.Acc_JE_ChequeDate "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 4 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_JE_Master C On B.ATD_BillID = C.Acc_JE_ID and C.Acc_JE_Status = 'A' "
                    End If
                    dtJE = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtJE.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtJE.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Journal Entry"
                        dRow("Narration") = dtJE.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select APM_Name from accounts_Party_Master where APM_ID =" & dtJE.Rows(j)("Party").ToString() & " and APM_CompID =" & iCompID & "") & "/" & dtJE.Rows(j)("ChequeNo").ToString()
                        dRow("CorresGLCode") = dtJE.Rows(j)("CorresGLcode").ToString()
                        dRow("CorresDescription") = dtJE.Rows(j)("CorresDescription").ToString()

                        If dtJE.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtJE.Rows(j)("Debit").ToString()
                        End If

                        If dtJE.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtJE.Rows(j)("Credit").ToString()
                        End If
                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next


                    'SubLeger Opening Balance
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_SLJE_BillDate as BillDate, C.Acc_SLJE_RefNo as BillNo,C.Acc_SLJE_TransactionNo as VoucherNo "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 7 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Ledger_JE_Master C On B.ATD_BillID = C.Acc_SLJE_ID and C.Acc_SLJE_Status = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_SLJE_BillDate as BillDate, C.Acc_SLJE_RefNo as BillNo,C.Acc_SLJE_TransactionNo as VoucherNo "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 7 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join Acc_Ledger_JE_Master C On B.ATD_BillID = C.Acc_SLJE_ID and C.Acc_SLJE_Status = 'A' "
                    End If
                    dtJE = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtJE.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtJE.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "SubLedger BillWise Entry"
                        dRow("Narration") = "Bill Date:-" & dtJE.Rows(j)("BillDate").ToString() & "/Bill Reference No:-" & dtJE.Rows(j)("BillNo").ToString()
                        'dRow("CorresGLCode") = dtJE.Rows(j)("CorresGLcode").ToString()
                        'dRow("CorresDescription") = dtJE.Rows(j)("CorresDescription").ToString()

                        If dtJE.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtJE.Rows(j)("Debit").ToString()
                        End If

                        If dtJE.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtJE.Rows(j)("Credit").ToString()
                        End If
                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next


                    'Purchase voucher                 
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_Purchase_BillDate as BillDate, C.Acc_Purchase_BillNo as BillNo,C.Acc_Purchase_TransactionNo as VoucherNo,"
                        aSql = aSql & "C.Acc_Purchase_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 8 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join acc_purchase_Master C On B.ATD_BillID = C.Acc_Purchase_ID and C.Acc_Purchase_DelFlag = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_Purchase_BillDate as BillDate, C.Acc_Purchase_BillNo as BillNo,C.Acc_Purchase_TransactionNo as VoucherNo,"
                        aSql = aSql & "C.Acc_Purchase_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 8 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join acc_purchase_Master C On B.ATD_BillID = C.Acc_Purchase_ID and C.Acc_Purchase_DelFlag = 'A' "
                    End If
                    dtPV = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtPV.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtPV.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Purchase Voucher"
                        dRow("Narration") = dtPV.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select CSM_Name from CustomerSupplierMaster where CSM_ID =" & dtPV.Rows(j)("Party").ToString() & " and CSM_CompID =" & iCompID & "")

                        If dtPV.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtPV.Rows(j)("Debit").ToString()
                        End If

                        If dtPV.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtPV.Rows(j)("Credit").ToString()
                        End If

                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next


                    'Sales voucher                 
                    If dtGL.Rows(i)("gl_head").ToString() = "2" Then
                        aSql = "" : aSql = "Select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_Sales_BillDate as BillDate, C.Acc_Sales_BillNo as BillNo,C.Acc_Sales_TransactionNo as VoucherNo,"
                        aSql = aSql & "C.Acc_Sales_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_GL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_SubGL = 0  and B.ATD_TRType = 9 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join acc_Sales_masters C On B.ATD_BillID = C.Acc_Sales_ID and C.Acc_Sales_DelFlag = 'A' "

                    ElseIf dtGL.Rows(i)("gl_head").ToString() = "3" Then
                        aSql = "" : aSql = "select A.gl_Glcode as GLCode, A.gl_Desc as GlDescription,B.ATD_Debit as Debit, B.ATD_Credit as Credit,"
                        aSql = aSql & "C.Acc_Sales_BillDate as BillDate, C.Acc_Sales_BillNo as BillNo,C.Acc_Sales_TransactionNo as VoucherNo,"
                        aSql = aSql & "C.Acc_Sales_Party as party "
                        aSql = aSql & "from chart_of_Accounts A  join acc_Transactions_Details B On "
                        aSql = aSql & "B.ATD_SubGL = " & dtGL.Rows(i)("gl_Id").ToString() & " and B.ATD_TRType = 9 and B.ATD_YearID = " & iYearID & " and "
                        aSql = aSql & "B.ATD_CompID = " & iCompID & " And B.ATD_GL = A.gl_ID "
                        aSql = aSql & "join acc_Sales_masters C On B.ATD_BillID = C.Acc_Sales_ID and C.Acc_Sales_DelFlag = 'A' "
                    End If
                    dtSV = objDB.SQLExecuteDataSet(sNameSpace, aSql).Tables(0)
                    For j = 0 To dtSV.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("BillDate") = dtSV.Rows(j)("BillDate").ToString()
                        dRow("TransactionType") = "Sales Voucher"
                        dRow("Narration") = dtSV.Rows(j)("VoucherNo").ToString() & "/" & objDB.SQLExecuteScalar(sNameSpace, "Select BM_Name from sales_Buyers_Masters where BM_ID =" & dtSV.Rows(j)("Party").ToString() & " and BM_CompID =" & iCompID & "")

                        If dtSV.Rows(j)("Debit").ToString() = "" Then
                            dRow("TransDebit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransDebit") = dtSV.Rows(j)("Debit").ToString()
                        End If

                        If dtSV.Rows(j)("Credit").ToString() = "" Then
                            dRow("TransCredit") = "0.00" : dTransDebit = 0
                        Else
                            dRow("TransCredit") = dtSV.Rows(j)("Credit").ToString()
                        End If

                        dRow("Status") = "1"
                        dt.Rows.Add(dRow)
                    Next


                Next



                'Dim dataView As New DataView(dt)
                'dataView.Sort = "BillDate Asc"
                'dtDateSort = dataView.ToTable
                'dtDateSort = GetMonthlyReports(dtDateSort)

                'dtFinal.Merge(dtDateSort)
                'dtFinal.AcceptChanges()
            End If

            'dtDateSort = GetGrandTotal(dtFinal)
            'dtFinal.Merge(dtDateSort)
            'dtFinal.AcceptChanges()
            'Return dtFinal
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDayBookReport(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iFromDate As Date, ByVal iToDate As Date) As DataTable
        Dim dt As New DataTable, dtGL As New DataTable, dtSubGL As New DataTable, dtPay As New DataTable, dtReceipt As New DataTable, dtPetty As New DataTable, dtJE As New DataTable
        Dim sSql As String = "", iSql As String = ""
        Dim i As Integer = 0, j As Integer = 0
        Dim dRow As DataRow

        Try
            dt.Columns.Add("ATD_TransactionDate")
            dt.Columns.Add("VoucherNo")
            dt.Columns.Add("ATD_TrType")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDesc")
            dt.Columns.Add("SubGLCode")
            dt.Columns.Add("subDesc")
            dt.Columns.Add("ATD_Debit")
            dt.Columns.Add("ATD_Credit")

            dtSubGL = GetchartofAccounts(sNameSpace)
            dtPay = GetPaymentMaster(sNameSpace)
            dtReceipt = GetReceiptMaster(sNameSpace)
            dtPetty = GetPettyCashMaster(sNameSpace)
            dtJE = GetJournalEntryMaster(sNameSpace)

            sSql = "" : sSql = "Select ATD_TransactionDate, ATD_TrType, ATD_BillId, ATD_Debit, ATD_Credit, ATD_GL, ATD_SubGL from Acc_Transactions_Details "
            sSql = sSql & " where ATD_TransactionDate between convert(datetime, '" & iFromDate & "', 103) and convert(datetime, ' " & iToDate & "', 103) and ATD_CompID= " & iCompID & " and (ATD_Trtype=1 or ATD_Trtype=2 or ATD_Trtype=3 or ATD_Trtype=4) "
            dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGL.Rows.Count > 0 Then
                For i = 0 To dtGL.Rows.Count - 1
                    dRow = dt.NewRow()

                    If IsDBNull(dtGL.Rows(i)("ATD_TransactionDate").ToString()) = False Then
                        dRow("ATD_TransactionDate") = objGen.FormatDtForRDBMS(dtGL.Rows(i)("ATD_TransactionDate").ToString(), "D")
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_BillId").ToString()) = False Then
                        If dtGL.Rows(i)("ATD_TrType").ToString() = "1" Then
                            Dim dttrans As New DataTable
                            Dim DVGLCODE As New DataView(dtPay)
                            DVGLCODE.RowFilter = "Acc_PM_Id=" & dtGL.Rows(i)("ATD_BillId")
                            dttrans = DVGLCODE.ToTable
                            If dtGL.Rows.Count > 0 Then
                                dRow("VoucherNo") = dttrans.Rows(0)("Acc_PM_TransactionNo")
                            End If
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "2" Then
                            Dim dttrans As New DataTable
                            Dim DVGLCODE As New DataView(dtPetty)
                            DVGLCODE.RowFilter = "Acc_PCM_Id=" & dtGL.Rows(i)("ATD_BillId")
                            dttrans = DVGLCODE.ToTable
                            If dtGL.Rows.Count > 0 Then
                                dRow("VoucherNo") = dttrans.Rows(0)("Acc_PCM_TransactionNo")
                            End If
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "3" Then
                            Dim dttrans As New DataTable
                            Dim DVGLCODE As New DataView(dtReceipt)
                            DVGLCODE.RowFilter = "Acc_RM_Id=" & dtGL.Rows(i)("ATD_BillId")
                            dttrans = DVGLCODE.ToTable
                            If dtGL.Rows.Count > 0 Then
                                dRow("VoucherNo") = dttrans.Rows(0)("Acc_RM_TransactionNo")
                            End If
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "4" Then
                            Dim dttrans As New DataTable
                            Dim DVGLCODE As New DataView(dtJE)
                            DVGLCODE.RowFilter = "Acc_JE_Id=" & dtGL.Rows(i)("ATD_BillId")
                            dttrans = DVGLCODE.ToTable
                            If dtGL.Rows.Count > 0 Then
                                dRow("VoucherNo") = dttrans.Rows(0)("Acc_JE_TransactionNo")
                            End If
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_TrType").ToString()) = False Then
                        If dtGL.Rows(i)("ATD_TrType").ToString() = "1" Then
                            dRow("ATD_TrType") = "Payment"
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "2" Then
                            dRow("ATD_TrType") = "Petty Cash"
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "3" Then
                            dRow("ATD_TrType") = "Receipt"
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "4" Then
                            dRow("ATD_TrType") = "Journal Entry"
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_GL").ToString()) = False Then
                        Dim dttab As New DataTable
                        Dim DVGLCODE As New DataView(dtSubGL)
                        DVGLCODE.RowFilter = "Gl_id=" & dtGL.Rows(i)("ATD_GL")
                        dttab = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dRow("GLCode") = dttab.Rows(0)("gl_glCode")
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_GL").ToString()) = False Then
                        Dim dttab As New DataTable
                        Dim DVGLCODE As New DataView(dtSubGL)
                        DVGLCODE.RowFilter = "Gl_id=" & dtGL.Rows(i)("ATD_GL")
                        dttab = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dRow("GLDesc") = dttab.Rows(0)("gl_Desc")
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_SubGL").ToString()) = False Then
                        Dim dttab As New DataTable
                        Dim DVGLCODE As New DataView(dtSubGL)
                        DVGLCODE.RowFilter = "Gl_id=" & dtGL.Rows(i)("ATD_SubGL")
                        dttab = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dRow("SubGLCode") = dttab.Rows(0)("gl_glCode")
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_SubGL").ToString()) = False Then
                        Dim dttab As New DataTable
                        Dim DVGLCODE As New DataView(dtSubGL)
                        DVGLCODE.RowFilter = "Gl_id=" & dtGL.Rows(i)("ATD_SubGL")
                        dttab = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dRow("subDesc") = dttab.Rows(0)("gl_Desc")
                        End If
                    End If


                    If IsDBNull(dtGL.Rows(i)("ATD_Debit").ToString()) = False Then
                        dRow("ATD_Debit") = dtGL.Rows(i)("ATD_Debit").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_Credit").ToString()) = False Then
                        dRow("ATD_Credit") = dtGL.Rows(i)("ATD_Credit").ToString()
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetchartofAccounts(ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select gl_id, gl_glCode, gl_Parent, gl_desc from chart_of_Accounts "
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPaymentMaster(ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from acc_payment_master "
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetReceiptMaster(ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from acc_receipt_master "
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPettyCashMaster(ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from acc_pettycash_master "
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetJournalEntryMaster(ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from acc_je_master "
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWeekBackDate(ByVal sAC As String) As String
        Dim sSql As String
        Try
            sSql = "Select Convert(Varchar(10),Getdate()-7,103)"
            Return objDB.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrentDate(ByVal sAC As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Convert(Varchar(10),Getdate(),103)"
            Return objDB.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadBankBookReport(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal dFromDate As String, ByVal dToDate As String) As DataTable
        Dim dt As New DataTable, dtGL As New DataTable, dtBank As New DataTable, dtSubGL As New DataTable, dtPay As New DataTable
        Dim sSql As String = "", iSql As String = "", aSql As String = "", strUser As String = ""
        Dim i As Integer = 0, j As Integer = 0, GLid As Integer = 0
        Dim dRow As DataRow
        Dim drUsr As OleDb.OleDbDataReader

        dtPay = GetPaymentMaster(sNameSpace)
        dtSubGL = GetchartofAccounts(sNameSpace)

        Try
            dt.Columns.Add("ATD_TransactionDate")
            dt.Columns.Add("ATD_BillId")
            dt.Columns.Add("ATD_TrType")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDesc")
            dt.Columns.Add("SubGLCode")
            dt.Columns.Add("subDesc")
            dt.Columns.Add("ATD_Debit")
            dt.Columns.Add("ATD_Credit")

            sSql = "Select Acc_GL from acc_application_settings where Acc_Types='Bank'"
            GLid = objDB.SQLExecuteScalar(sNameSpace, sSql)
            If GLid > 0 Then
                aSql = " Select GL_ID From chart_of_accounts Where gl_parent = " & GLid & ""
            End If
            drUsr = objDB.SQLDataReader(sNameSpace, aSql)
            If drUsr.HasRows Then
                While drUsr.Read
                    strUser = strUser & "," & drUsr("GL_ID")
                End While
                strUser = Right(strUser, Len(strUser) - 1)
            Else
                strUser = "Null"
            End If


            sSql = "" : sSql = "Select ATD_TransactionDate, ATD_TrType, ATD_BillId, ATD_Debit, ATD_Credit, ATD_GL, ATD_SubGL from Acc_Transactions_Details "
            sSql = sSql & " where ATD_TransactionDate between  '" & dFromDate & "' And DateAdd(s, -1, DateAdd(d, 1,'" & dToDate & "')) And ATD_CompID= " & iCompID & " And ATD_SubGL In (" & strUser & ")"
            dtGL = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGL.Rows.Count > 0 Then
                For i = 0 To dtGL.Rows.Count - 1
                    dRow = dt.NewRow()

                    If IsDBNull(dtGL.Rows(i)("ATD_TransactionDate").ToString()) = False Then
                        dRow("ATD_TransactionDate") = objGen.FormatDtForRDBMS(dtGL.Rows(i)("ATD_TransactionDate").ToString(), "D")
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_BillId").ToString()) = False Then
                        If dtGL.Rows(i)("ATD_TrType").ToString() = "1" Then
                            Dim dttrans As New DataTable
                            Dim DVGLCODE As New DataView(dtPay)
                            DVGLCODE.RowFilter = "Acc_PM_Id=" & dtGL.Rows(i)("ATD_BillId")
                            dttrans = DVGLCODE.ToTable
                            If dtGL.Rows.Count > 0 Then
                                dRow("ATD_BillId") = dttrans.Rows(0)("Acc_PM_TransactionNo")
                            End If
                            'ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "2" Then
                            '    Dim dttrans As New DataTable
                            '    Dim DVGLCODE As New DataView(dtPetty)
                            '    DVGLCODE.RowFilter = "Acc_PCM_Id=" & dtGL.Rows(i)("ATD_BillId")
                            '    dttrans = DVGLCODE.ToTable
                            '    If dtGL.Rows.Count > 0 Then
                            '        dRow("VoucherNo") = dttrans.Rows(0)("Acc_PCM_TransactionNo")
                            '    End If
                            'ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "3" Then
                            '    Dim dttrans As New DataTable
                            '    Dim DVGLCODE As New DataView(dtReceipt)
                            '    DVGLCODE.RowFilter = "Acc_RM_Id=" & dtGL.Rows(i)("ATD_BillId")
                            '    dttrans = DVGLCODE.ToTable
                            '    If dtGL.Rows.Count > 0 Then
                            '        dRow("VoucherNo") = dttrans.Rows(0)("Acc_RM_TransactionNo")
                            '    End If
                            'ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "4" Then
                            '    Dim dttrans As New DataTable
                            '    Dim DVGLCODE As New DataView(dtJE)
                            '    DVGLCODE.RowFilter = "Acc_JE_Id=" & dtGL.Rows(i)("ATD_BillId")
                            '    dttrans = DVGLCODE.ToTable
                            'If dtGL.Rows.Count > 0 Then
                            '    dRow("VoucherNo") = dttrans.Rows(0)("Acc_JE_TransactionNo")
                            'End If
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_TrType").ToString()) = False Then
                        If dtGL.Rows(i)("ATD_TrType").ToString() = "1" Then
                            dRow("ATD_TrType") = "Payment"
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "2" Then
                            dRow("ATD_TrType") = "Petty Cash"
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "3" Then
                            dRow("ATD_TrType") = "Receipt"
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "4" Then
                            dRow("ATD_TrType") = "Journal Entry"
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "7" Then
                            dRow("ATD_TrType") = "SubLeger Opening Balance"
                        ElseIf dtGL.Rows(i)("ATD_TrType").ToString() = "8" Then
                            dRow("ATD_TrType") = "Purchase"
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_GL").ToString()) = False Then
                        Dim dttab As New DataTable
                        Dim DVGLCODE As New DataView(dtSubGL)
                        DVGLCODE.RowFilter = "Gl_id=" & dtGL.Rows(i)("ATD_GL")
                        dttab = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dRow("GLCode") = dttab.Rows(0)("gl_glCode")
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_GL").ToString()) = False Then
                        Dim dttab As New DataTable
                        Dim DVGLCODE As New DataView(dtSubGL)
                        DVGLCODE.RowFilter = "Gl_id=" & dtGL.Rows(i)("ATD_GL")
                        dttab = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dRow("GLDesc") = dttab.Rows(0)("gl_Desc")
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_SubGL").ToString()) = False Then
                        Dim dttab As New DataTable
                        Dim DVGLCODE As New DataView(dtSubGL)
                        DVGLCODE.RowFilter = "Gl_id=" & dtGL.Rows(i)("ATD_SubGL")
                        dttab = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dRow("SubGLCode") = dttab.Rows(0)("gl_glCode")
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_SubGL").ToString()) = False Then
                        Dim dttab As New DataTable
                        Dim DVGLCODE As New DataView(dtSubGL)
                        DVGLCODE.RowFilter = "Gl_id=" & dtGL.Rows(i)("ATD_SubGL")
                        dttab = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dRow("subDesc") = dttab.Rows(0)("gl_Desc")
                        End If
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_Debit").ToString()) = False Then
                        dRow("ATD_Debit") = dtGL.Rows(i)("ATD_Debit").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("ATD_Credit").ToString()) = False Then
                        dRow("ATD_Credit") = dtGL.Rows(i)("ATD_Credit").ToString()
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalCreditAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As Integer) As Decimal
        Dim sSql As String = ""
        Dim iCOA As Decimal = 0
        Try
            sSql = "" : sSql = "Select ISNULL(sum(Opn_CreditAmount),0) from ACC_Opening_Balance where Opn_GLCode In(Select gl_glcode  from chart_of_Accounts where gl_parent=" & GlCode & ")"
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
            'sSql = "" : sSql = "Select ISNULL(sum(ATD_Credit),0) from acc_Transactions_Details where  ATD_GL=" & GlCode & " and ATD_BillId<>0)"
            'iCOA = iCOA + objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalTrnCreditAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As Integer) As Decimal
        Dim sSql As String = ""
        Dim iCOA As Decimal = 0
        Try
            'sSql = "" : sSql = "Select ISNULL(sum(Opn_CreditAmount),0) from ACC_Opening_Balance where Opn_GLCode In(Select gl_glcode  from chart_of_Accounts where gl_parent=" & GlCode & ")"
            'iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "" : sSql = "Select ISNULL(sum(ATD_Credit),0) from acc_Transactions_Details where  ATD_GL=" & GlCode & " and ATD_BillId<>0"
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTotalDabitAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As Integer) As Decimal
        Dim sSql As String = ""
        Dim iCOA As Decimal = 0
        Try
            sSql = "" : sSql = "Select ISNULL(sum(Opn_DebitAmt),0) from ACC_Opening_Balance where Opn_GLCode In(Select gl_glcode  from chart_of_Accounts where gl_parent=" & GlCode & ")"
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTotalTrnctnDabitAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As Integer) As Decimal
        Dim sSql As String = ""
        Dim iCOA As Decimal = 0
        Try

            sSql = "" : sSql = "Select ISNULL(sum(ATD_Debit),0) from acc_Transactions_Details where  ATD_SubGL=" & GlCode & " and ATD_BillId<>0"
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function GetTotalTrnctnDabitAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As Integer) As Decimal
    '    Dim sSql As String = ""
    '    Dim iCOA As Decimal = 0
    '    Try

    '        sSql = "" : sSql = "Select ISNULL(sum(ATD_Debit),0) from acc_Transactions_Details where  ATD_SubGL=" & GlCode & " and ATD_BillId<>0"
    '        iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
    '        Return iCOA
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function GetTotalSubGlnDabitAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As Integer) As Decimal
        Dim sSql As String = ""
        Dim iCOA As Decimal = 0
        Try

            sSql = "" : sSql = "Select ISNULL(sum(ATD_Debit),0) from acc_Transactions_Details where ATD_SubGL=" & GlCode & " and ATD_BillId<>0"
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalSubglCreditAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GlCode As Integer) As Decimal
        Dim sSql As String = ""
        Dim iCOA As Decimal = 0
        Try
            'sSql = "" : sSql = "Select ISNULL(sum(Opn_CreditAmount),0) from ACC_Opening_Balance where Opn_GLCode In(Select gl_glcode  from chart_of_Accounts where gl_parent=" & GlCode & ")"
            'iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "" : sSql = "Select ISNULL(sum(ATD_Credit),0) from acc_Transactions_Details where  ATD_SubGL=" & GlCode & " and ATD_BillId<>0"
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
