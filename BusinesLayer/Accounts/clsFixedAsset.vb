Imports DatabaseLayer
Imports System
Imports System.Data
Public Class clsFixedAsset
    Dim objDB As New DBHelper
    'Public Function BindFixedAsset(ByVal sNameSpace As String, iCompID As Integer, iYearID As Integer) As DataTable
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim dtTab As New DataTable
    '    Dim sSql As String = ""
    '    Dim dtTN As New DataTable

    '    Dim iOpnBalance, iOpenBalTotal, iGrandTotal As Double
    '    Dim iClosingBal, iClosingTotal, iClsoingBalTotal As Double
    '    Dim iDepreciationAmt, iDepreciationTotal, iDepreciationGrandTotal As Double
    '    Dim iUptoBal, iUptoTotal, iUptoBalTotal As Double
    '    Dim iAdditionAmt, iAdditionBal, iAdditionBalTotal As Double
    '    Dim iNetBlock1, iNetBlockBal, iNetBlockTotal As Double
    '    Dim iDeductionAmt, iDeductionBal, iDeductionBalTotal As Double
    '    Dim iDepDeductionAmt, iDepDeductionTotal, iDepDeductionGrandTotal As Double

    '    Try
    '        dt.Columns.Add("GL")
    '        dt.Columns.Add("glHead")
    '        dt.Columns.Add("Description")
    '        dt.Columns.Add("NoteNo")
    '        dt.Columns.Add("OpeningBalance")
    '        dt.Columns.Add("Additions")
    '        dt.Columns.Add("Deductions")
    '        dt.Columns.Add("ClosingBlock")
    '        dt.Columns.Add("Dep")
    '        dt.Columns.Add("Depreciation")
    '        dt.Columns.Add("DepDeduction")
    '        dt.Columns.Add("Upto")
    '        dt.Columns.Add("MarchF")
    '        dt.Columns.Add("March2")

    '        Dim iLastYear As Integer = GetYearID(sNameSpace, iCompID, iYearID)

    '        sSql = "Select * From Chart_Of_Accounts Where gl_acchead=1 And gl_Parent In (Select gl_ID From Chart_Of_Accounts Where gl_CompID =" & iCompID & " and gl_Desc='Fixed Assets' and gl_Delflag='C' and gl_Status='A')"
    '        dtTN = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dtTN.Rows.Count > 0 Then
    '            For j = 0 To dtTN.Rows.Count - 1
    '                dtTab = objDB.SQLExecuteDataSet(sNameSpace, "Select * From Chart_Of_Accounts Where gl_CompID =" & iCompID & " and gl_Status='A' and gl_Delflag='C' And gl_acchead=1 And gl_Parent=" & dtTN.Rows(j)("gl_ID") & " And gl_Desc Not Like 'Depreciation - %' Order By gl_Desc ").Tables(0)
    '                If dtTab.Rows.Count > 0 Then
    '                    dRow = dt.NewRow()
    '                    dRow("GL") = dtTN.Rows(j)("gl_ID")
    '                    dRow("glHead") = dtTN.Rows(j)("gl_Head")
    '                    dRow("Description") = dtTN.Rows(j)("gl_Desc")
    '                    dt.Rows.Add(dRow)
    '                    For i = 0 To dtTab.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        dRow("GL") = dtTab.Rows(i)("gl_ID")
    '                        dRow("glHead") = dtTab.Rows(i)("gl_Head")
    '                        dRow("Description") = dtTab.Rows(i)("gl_Desc")
    '                        dRow("NoteNo") = ""
    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=1 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
    '                            iOpnBalance = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=1 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
    '                        End If
    '                        dRow("OpeningBalance") = Convert.ToDecimal(iOpnBalance).ToString("#,##0.00")
    '                        iOpenBalTotal = iOpenBalTotal + iOpnBalance

    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=2 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
    '                            iAdditionAmt = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=2 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
    '                        End If
    '                        dRow("Additions") = Convert.ToDecimal(iAdditionAmt).ToString("#,##0.00")
    '                        iAdditionBal = iAdditionBal + iAdditionAmt

    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=3 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
    '                            iDeductionAmt = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=3 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
    '                        End If
    '                        dRow("Deductions") = Convert.ToDecimal(iDeductionAmt).ToString("#,##0.00")
    '                        iDeductionBal = iDeductionBal + iDeductionAmt

    '                        iClosingBal = (iOpnBalance + iAdditionAmt) - iDeductionAmt
    '                        dRow("ClosingBlock") = Convert.ToDecimal(iClosingBal).ToString("#,##0.00")
    '                        iClosingTotal = iClosingTotal + iClosingBal
    '                        iOpnBalance = 0

    '                        dRow("Dep") = ""

    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=4 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
    '                            iDepreciationAmt = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=4 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
    '                        End If
    '                        dRow("Depreciation") = Convert.ToDecimal(iDepreciationAmt).ToString("#,##0.00")
    '                        iDepreciationTotal = iDepreciationTotal + iDepreciationAmt

    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=5 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
    '                            iDepDeductionAmt = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=5 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
    '                        End If
    '                        dRow("DepDeduction") = iDepDeductionAmt
    '                        iDepDeductionTotal = iDepDeductionTotal + iDepDeductionAmt

    '                        iUptoBal = iDepreciationAmt + iDepDeductionAmt
    '                        dRow("Upto") = Convert.ToDecimal(iUptoBal).ToString("#,##0.00")
    '                        iUptoTotal = iUptoTotal + iUptoBal
    '                        iDepreciationAmt = 0

    '                        iNetBlock1 = iClosingBal - iUptoBal
    '                        dRow("MarchF") = Convert.ToDecimal(iNetBlock1).ToString("#,##0.00")
    '                        iNetBlockBal = iNetBlockBal + iNetBlock1
    '                        dRow("March2") = ""

    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                dRow = dt.NewRow()
    '                dRow("glHead") = 0
    '                dRow("Description") = "Sub Total"
    '                dRow("OpeningBalance") = Convert.ToDecimal(iOpenBalTotal).ToString("#,##0.00")
    '                iGrandTotal = iGrandTotal + iOpenBalTotal
    '                iOpenBalTotal = 0

    '                dRow("Additions") = Convert.ToDecimal(iAdditionBal).ToString("#,##0.00")
    '                iAdditionBalTotal = iAdditionBalTotal + iAdditionBal
    '                iAdditionBal = 0

    '                dRow("Deductions") = Convert.ToDecimal(iDeductionBal).ToString("#,##0.00")
    '                iDeductionBalTotal = iDeductionBalTotal + iDeductionBal
    '                iDeductionBal = 0

    '                dRow("ClosingBlock") = Convert.ToDecimal(iClosingTotal).ToString("#,##0.00")
    '                iClsoingBalTotal = iClsoingBalTotal + iClosingTotal
    '                iClosingTotal = 0

    '                dRow("Depreciation") = Convert.ToDecimal(iDepreciationTotal).ToString("#,##0.00")
    '                iDepreciationGrandTotal = iDepreciationGrandTotal + iDepreciationTotal
    '                iDepreciationTotal = 0

    '                dRow("DepDeduction") = iDepDeductionTotal
    '                iDepDeductionGrandTotal = iDepDeductionGrandTotal + iDepDeductionTotal
    '                iDepDeductionTotal = 0

    '                dRow("Upto") = Convert.ToDecimal(iUptoTotal).ToString("#,##0.00")
    '                iUptoBalTotal = iUptoBalTotal + iUptoTotal
    '                iUptoTotal = 0

    '                dRow("MarchF") = Convert.ToDecimal(iNetBlockBal).ToString("#,##0.00")
    '                iNetBlockTotal = iNetBlockTotal + iNetBlockBal
    '                iNetBlockBal = 0
    '                dt.Rows.Add(dRow)

    '                dRow = dt.NewRow()
    '                dRow("glHead") = 0
    '                dRow("Description") = ""
    '                dt.Rows.Add(dRow)
    '            Next
    '        End If

    '        dRow = dt.NewRow()
    '        dRow("glHead") = 0
    '        dRow("Description") = "TOTAL"
    '        dRow("OpeningBalance") = Convert.ToDecimal(iGrandTotal).ToString("#,##0.00")
    '        iGrandTotal = 0

    '        dRow("Additions") = Convert.ToDecimal(iAdditionBalTotal).ToString("#,##0.00")
    '        iAdditionBalTotal = 0

    '        dRow("Deductions") = Convert.ToDecimal(iDeductionBalTotal).ToString("#,##0.00")
    '        iDeductionBalTotal = 0

    '        dRow("ClosingBlock") = Convert.ToDecimal(iClsoingBalTotal).ToString("#,##0.00")
    '        iClsoingBalTotal = 0

    '        dRow("Depreciation") = Convert.ToDecimal(iDepreciationGrandTotal).ToString("#,##0.00")
    '        iDepreciationGrandTotal = 0

    '        dRow("DepDeduction") = iDepDeductionGrandTotal
    '        iDepDeductionGrandTotal = 0

    '        dRow("Upto") = Convert.ToDecimal(iUptoBalTotal).ToString("#,##0.00")
    '        iUptoBalTotal = 0

    '        dRow("MarchF") = Convert.ToDecimal(iNetBlockTotal).ToString("#,##0.00")
    '        iNetBlockTotal = 0
    '        dt.Rows.Add(dRow)

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetYearID(ByVal sNameSpace As String, iCompID As Integer, iYearID As Integer) As Integer
        Dim sSql As String = ""
        Dim iYear As Integer
        Try
            sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from acc_Year_Master where yms_To_year in(Select yms_From_Year from acc_Year_Master where yms_id = " & iYearID & " and yms_CompID =" & iCompID & ")"
            iYear = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iYear
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CUSTB_ID,CUSTB_NAME From MST_CUSTOMER_MASTER_Branch where CUSTB_CompID =" & iCompID & " Order by CUSTB_NAME"
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function BindFixedAsset(ByVal sNameSpace As String, iCompID As Integer, iYearID As Integer) As DataTable
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim dtTab As New DataTable
    '    Dim sSql As String = ""
    '    Dim dtTN As New DataTable

    '    Dim iOpnBalance, iOpenBalTotal, iGrandTotal As Double
    '    Dim iClosingBal, iClosingTotal, iClsoingBalTotal As Double
    '    Dim iDepreciationAmt, iDepreciationTotal, iDepreciationGrandTotal As Double
    '    Dim iUptoBal, iUptoTotal, iUptoBalTotal As Double
    '    Dim iAdditionAmt, iAdditionBal, iAdditionBalTotal As Double
    '    Dim iNetBlock1, iNetBlockBal, iNetBlockTotal As Double
    '    Dim iDeductionAmt, iDeductionBal, iDeductionBalTotal As Double
    '    Dim iDepDeductionAmt, iDepDeductionTotal, iDepDeductionGrandTotal As Double

    '    Dim dFAJEDebit, dFAJECredit As Double

    '    Try
    '        dt.Columns.Add("GL")
    '        dt.Columns.Add("glHead")
    '        dt.Columns.Add("Description")
    '        dt.Columns.Add("NoteNo")
    '        dt.Columns.Add("OpeningBalance")
    '        dt.Columns.Add("Additions")
    '        dt.Columns.Add("Deductions")
    '        dt.Columns.Add("ClosingBlock")
    '        dt.Columns.Add("Dep")
    '        dt.Columns.Add("Depreciation")
    '        dt.Columns.Add("DepDeduction")
    '        dt.Columns.Add("Upto")
    '        dt.Columns.Add("MarchF")
    '        dt.Columns.Add("March2")

    '        Dim iLastYear As Integer = GetYearID(sNameSpace, iCompID, iYearID)

    '        sSql = "Select * From Chart_Of_Accounts Where gl_acchead=1 And gl_Parent In (Select gl_ID From Chart_Of_Accounts Where gl_CompID =" & iCompID & " and gl_Desc='Fixed Assets' and gl_Delflag='C' and gl_Status='A')"
    '        dtTN = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dtTN.Rows.Count > 0 Then
    '            For j = 0 To dtTN.Rows.Count - 1
    '                dtTab = objDB.SQLExecuteDataSet(sNameSpace, "Select * From Chart_Of_Accounts Where gl_CompID =" & iCompID & " and gl_Status='A' and gl_Delflag='C' And gl_acchead=1 And gl_Parent=" & dtTN.Rows(j)("gl_ID") & " And gl_Desc Not Like 'Depreciation - %' Order By gl_Desc ").Tables(0)
    '                If dtTab.Rows.Count > 0 Then
    '                    dRow = dt.NewRow()
    '                    dRow("GL") = dtTN.Rows(j)("gl_ID")
    '                    dRow("glHead") = dtTN.Rows(j)("gl_Head")
    '                    dRow("Description") = dtTN.Rows(j)("gl_Desc")
    '                    dt.Rows.Add(dRow)
    '                    For i = 0 To dtTab.Rows.Count - 1
    '                        dRow = dt.NewRow()
    '                        dRow("GL") = dtTab.Rows(i)("gl_ID")
    '                        dRow("glHead") = dtTab.Rows(i)("gl_Head")
    '                        dRow("Description") = dtTab.Rows(i)("gl_Desc")
    '                        dRow("NoteNo") = ""
    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "SElect Opn_DebitAmt From Acc_Opening_Balance Where Opn_GLID=" & dtTab.Rows(i)("gl_ID") & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iLastYear & " ")) = False Then
    '                            iOpnBalance = objDB.SQLExecuteScalar(sNameSpace, "SElect Opn_DebitAmt From Acc_Opening_Balance Where Opn_GLID=" & dtTab.Rows(i)("gl_ID") & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iLastYear & "")
    '                        End If
    '                        dRow("OpeningBalance") = Convert.ToDecimal(iOpnBalance).ToString("#,##0.00")
    '                        iOpenBalTotal = iOpenBalTotal + iOpnBalance

    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "select ATR_DbAmount from account_Transactions where atr_trType = 1 And ATR_YearID=" & iLastYear & " And ATR_CompID=" & iCompID & " And atr_glcode in (Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtTab.Rows(i)("gl_ID") & " And GL_Head=" & dtTab.Rows(i)("gl_Head") & " And GL_CompID=" & iCompID & ")")) = False Then
    '                            iAdditionAmt = objDB.SQLExecuteScalar(sNameSpace, "select ATR_DbAmount from account_Transactions where atr_trType = 1 And ATR_YearID=" & iLastYear & " And ATR_CompID=" & iCompID & " And atr_glcode in (Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtTab.Rows(i)("gl_ID") & " And GL_Head=" & dtTab.Rows(i)("gl_Head") & " And GL_CompID=" & iCompID & ")")
    '                            If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select Sum(AFJD_Debit) From Acc_FixedAssets_JE_Details Where AFJD_Head=" & dtTab.Rows(i)("gl_AccHead") & " And AFJD_GL=" & dtTab.Rows(i)("gl_Parent") & " And AFJD_SubGL=" & dtTab.Rows(i)("gl_ID") & " And AFJD_MasterID in(Select AFJ_ID From Acc_FixedAssets_JE Where AFJ_Block=1) And AFJD_CompID=" & iCompID & "")) = False Then
    '                                dFAJEDebit = objDB.SQLExecuteScalar(sNameSpace, "Select Sum(AFJD_Debit) From Acc_FixedAssets_JE_Details Where AFJD_Head=" & dtTab.Rows(i)("gl_AccHead") & " And AFJD_GL=" & dtTab.Rows(i)("gl_Parent") & " And AFJD_SubGL=" & dtTab.Rows(i)("gl_ID") & " And AFJD_MasterID in(Select AFJ_ID From Acc_FixedAssets_JE Where AFJ_Block=1) And AFJD_CompID=" & iCompID & "")
    '                            Else
    '                                dFAJEDebit = 0
    '                            End If
    '                        End If
    '                        dRow("Additions") = Convert.ToDecimal(iAdditionAmt + dFAJEDebit).ToString("#,##0.00")
    '                        iAdditionBal = iAdditionBal + iAdditionAmt + dFAJEDebit

    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "select ATR_CrAmount from account_Transactions where atr_trType = 3 And ATR_YearID=" & iLastYear & " And ATR_CompID=" & iCompID & " And atr_glcode in (Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtTab.Rows(i)("gl_ID") & " And GL_Head=" & dtTab.Rows(i)("gl_Head") & " And GL_CompID=" & iCompID & ")")) = False Then
    '                            iDeductionAmt = objDB.SQLExecuteScalar(sNameSpace, "select ATR_CrAmount from account_Transactions where atr_trType = 3 And ATR_YearID=" & iLastYear & " And ATR_CompID=" & iCompID & " And atr_glcode in (Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtTab.Rows(i)("gl_ID") & " And GL_Head=" & dtTab.Rows(i)("gl_Head") & " And GL_CompID=" & iCompID & ")")

    '                            If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select Sum(AFJD_Credit) From Acc_FixedAssets_JE_Details Where AFJD_Head=" & dtTab.Rows(i)("gl_AccHead") & " And AFJD_GL=" & dtTab.Rows(i)("gl_Parent") & " And AFJD_SubGL=" & dtTab.Rows(i)("gl_ID") & " And AFJD_MasterID in(Select AFJ_ID From Acc_FixedAssets_JE Where AFJ_Block=2) And AFJD_CompID=" & iCompID & "")) = False Then
    '                                dFAJECredit = objDB.SQLExecuteScalar(sNameSpace, "Select Sum(AFJD_Credit) From Acc_FixedAssets_JE_Details Where AFJD_Head=" & dtTab.Rows(i)("gl_AccHead") & " And AFJD_GL=" & dtTab.Rows(i)("gl_Parent") & " And AFJD_SubGL=" & dtTab.Rows(i)("gl_ID") & " And AFJD_MasterID in(Select AFJ_ID From Acc_FixedAssets_JE Where AFJ_Block=2) And AFJD_CompID=" & iCompID & "")
    '                            Else
    '                                dFAJECredit = 0
    '                            End If

    '                        End If
    '                        dRow("Deductions") = Convert.ToDecimal(iDeductionAmt + dFAJECredit).ToString("#,##0.00")
    '                        iDeductionBal = iDeductionBal + iDeductionAmt + dFAJECredit

    '                        iClosingBal = (iOpnBalance + iAdditionAmt) - iDeductionAmt
    '                        dRow("ClosingBlock") = Convert.ToDecimal(iClosingBal).ToString("#,##0.00")
    '                        iClosingTotal = iClosingTotal + iClosingBal
    '                        iOpnBalance = 0

    '                        dRow("Dep") = ""

    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=4 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
    '                            iDepreciationAmt = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=4 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
    '                        End If
    '                        dRow("Depreciation") = Convert.ToDecimal(iDepreciationAmt).ToString("#,##0.00")
    '                        iDepreciationTotal = iDepreciationTotal + iDepreciationAmt

    '                        If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=5 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
    '                            iDepDeductionAmt = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=5 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
    '                        End If
    '                        dRow("DepDeduction") = iDepDeductionAmt
    '                        iDepDeductionTotal = iDepDeductionTotal + iDepDeductionAmt

    '                        iUptoBal = iDepreciationAmt + iDepDeductionAmt
    '                        dRow("Upto") = Convert.ToDecimal(iUptoBal).ToString("#,##0.00")
    '                        iUptoTotal = iUptoTotal + iUptoBal
    '                        iDepreciationAmt = 0

    '                        iNetBlock1 = iClosingBal - iUptoBal
    '                        dRow("MarchF") = Convert.ToDecimal(iNetBlock1).ToString("#,##0.00")
    '                        iNetBlockBal = iNetBlockBal + iNetBlock1
    '                        dRow("March2") = ""

    '                        dt.Rows.Add(dRow)
    '                    Next
    '                End If
    '                dRow = dt.NewRow()
    '                dRow("glHead") = 0
    '                dRow("Description") = "Sub Total"
    '                dRow("OpeningBalance") = Convert.ToDecimal(iOpenBalTotal).ToString("#,##0.00")
    '                iGrandTotal = iGrandTotal + iOpenBalTotal
    '                iOpenBalTotal = 0

    '                dRow("Additions") = Convert.ToDecimal(iAdditionBal).ToString("#,##0.00")
    '                iAdditionBalTotal = iAdditionBalTotal + iAdditionBal
    '                iAdditionBal = 0

    '                dRow("Deductions") = Convert.ToDecimal(iDeductionBal).ToString("#,##0.00")
    '                iDeductionBalTotal = iDeductionBalTotal + iDeductionBal
    '                iDeductionBal = 0

    '                dRow("ClosingBlock") = Convert.ToDecimal(iClosingTotal).ToString("#,##0.00")
    '                iClsoingBalTotal = iClsoingBalTotal + iClosingTotal
    '                iClosingTotal = 0

    '                dRow("Depreciation") = Convert.ToDecimal(iDepreciationTotal).ToString("#,##0.00")
    '                iDepreciationGrandTotal = iDepreciationGrandTotal + iDepreciationTotal
    '                iDepreciationTotal = 0

    '                dRow("DepDeduction") = iDepDeductionTotal
    '                iDepDeductionGrandTotal = iDepDeductionGrandTotal + iDepDeductionTotal
    '                iDepDeductionTotal = 0

    '                dRow("Upto") = Convert.ToDecimal(iUptoTotal).ToString("#,##0.00")
    '                iUptoBalTotal = iUptoBalTotal + iUptoTotal
    '                iUptoTotal = 0

    '                dRow("MarchF") = Convert.ToDecimal(iNetBlockBal).ToString("#,##0.00")
    '                iNetBlockTotal = iNetBlockTotal + iNetBlockBal
    '                iNetBlockBal = 0
    '                dt.Rows.Add(dRow)

    '                dRow = dt.NewRow()
    '                dRow("glHead") = 0
    '                dRow("Description") = ""
    '                dt.Rows.Add(dRow)
    '            Next
    '        End If

    '        dRow = dt.NewRow()
    '        dRow("glHead") = 0
    '        dRow("Description") = "TOTAL"
    '        dRow("OpeningBalance") = Convert.ToDecimal(iGrandTotal).ToString("#,##0.00")
    '        iGrandTotal = 0

    '        dRow("Additions") = Convert.ToDecimal(iAdditionBalTotal).ToString("#,##0.00")
    '        iAdditionBalTotal = 0

    '        dRow("Deductions") = Convert.ToDecimal(iDeductionBalTotal).ToString("#,##0.00")
    '        iDeductionBalTotal = 0

    '        dRow("ClosingBlock") = Convert.ToDecimal(iClsoingBalTotal).ToString("#,##0.00")
    '        iClsoingBalTotal = 0

    '        dRow("Depreciation") = Convert.ToDecimal(iDepreciationGrandTotal).ToString("#,##0.00")
    '        iDepreciationGrandTotal = 0

    '        dRow("DepDeduction") = iDepDeductionGrandTotal
    '        iDepDeductionGrandTotal = 0

    '        dRow("Upto") = Convert.ToDecimal(iUptoBalTotal).ToString("#,##0.00")
    '        iUptoBalTotal = 0

    '        dRow("MarchF") = Convert.ToDecimal(iNetBlockTotal).ToString("#,##0.00")
    '        iNetBlockTotal = 0
    '        dt.Rows.Add(dRow)

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function BindFixedAsset(ByVal sNameSpace As String, iCompID As Integer, iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtTab As New DataTable
        Dim sSql As String = ""
        Dim dtTN As New DataTable

        Dim iOpnBalance, iOpenBalTotal, iGrandTotal As Double
        Dim iClosingBal, iClosingTotal, iClsoingBalTotal As Double
        Dim iDepreciationAmt, iDepreciationTotal, iDepreciationGrandTotal As Double
        Dim iUptoBal, iUptoTotal, iUptoBalTotal As Double
        Dim iAdditionAmt, iAdditionBal, iAdditionBalTotal As Double
        Dim iNetBlock1, iNetBlockBal, iNetBlockTotal As Double
        Dim iDeductionAmt, iDeductionBal, iDeductionBalTotal As Double
        Dim iDepDeductionAmt, iDepDeductionTotal, iDepDeductionGrandTotal As Double

        Dim dFAJEDebit, dFAJECredit As Double

        Dim dDepreciationRate As Double
        Try
            dt.Columns.Add("GL")
            dt.Columns.Add("glHead")
            dt.Columns.Add("Description")
            dt.Columns.Add("NoteNo")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Additions")
            dt.Columns.Add("Deductions")
            dt.Columns.Add("ClosingBlock")
            dt.Columns.Add("Dep")
            dt.Columns.Add("Depreciation")
            dt.Columns.Add("DepDeduction")
            dt.Columns.Add("Upto")
            dt.Columns.Add("MarchF")
            dt.Columns.Add("March2")

            Dim iLastYear As Integer = GetYearID(sNameSpace, iCompID, iYearID)

            sSql = "Select * From Chart_Of_Accounts Where gl_acchead=1 And gl_Parent In (Select gl_ID From Chart_Of_Accounts Where gl_CompID =" & iCompID & " and gl_Desc='Fixed Assets' and gl_Delflag='C' and gl_Status='A')"
            dtTN = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtTN.Rows.Count > 0 Then
                For j = 0 To dtTN.Rows.Count - 1
                    dtTab = objDB.SQLExecuteDataSet(sNameSpace, "Select * From Chart_Of_Accounts Where gl_CompID =" & iCompID & " and gl_Status='A' and gl_Delflag='C' And gl_acchead=1 And gl_Parent=" & dtTN.Rows(j)("gl_ID") & " And gl_Desc Not Like 'Depreciation - %' Order By gl_Desc ").Tables(0)
                    If dtTab.Rows.Count > 0 Then
                        dRow = dt.NewRow()
                        dRow("GL") = dtTN.Rows(j)("gl_ID")
                        dRow("glHead") = dtTN.Rows(j)("gl_Head")
                        dRow("Description") = dtTN.Rows(j)("gl_Desc")
                        dt.Rows.Add(dRow)
                        For i = 0 To dtTab.Rows.Count - 1
                            dRow = dt.NewRow()
                            dRow("GL") = dtTab.Rows(i)("gl_ID")
                            dRow("glHead") = dtTab.Rows(i)("gl_Head")
                            dRow("Description") = dtTab.Rows(i)("gl_Desc")
                            dRow("NoteNo") = ""
                            If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "SElect Opn_DebitAmt From Acc_Opening_Balance Where Opn_GLID=" & dtTab.Rows(i)("gl_ID") & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iLastYear & " ")) = False Then
                                iOpnBalance = objDB.SQLExecuteScalar(sNameSpace, "SElect Opn_DebitAmt From Acc_Opening_Balance Where Opn_GLID=" & dtTab.Rows(i)("gl_ID") & " And Opn_CompID=" & iCompID & " And Opn_YearID=" & iLastYear & "")
                            End If
                            dRow("OpeningBalance") = Convert.ToDecimal(iOpnBalance).ToString("#,##0.00")
                            iOpenBalTotal = iOpenBalTotal + iOpnBalance

                            If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "select ATR_DbAmount from account_Transactions where atr_trType = 1 And ATR_YearID=" & iLastYear & " And ATR_CompID=" & iCompID & " And atr_glcode in (Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtTab.Rows(i)("gl_ID") & " And GL_Head=" & dtTab.Rows(i)("gl_Head") & " And GL_CompID=" & iCompID & ")")) = False Then
                                iAdditionAmt = objDB.SQLExecuteScalar(sNameSpace, "select ATR_DbAmount from account_Transactions where atr_trType = 1 And ATR_YearID=" & iLastYear & " And ATR_CompID=" & iCompID & " And atr_glcode in (Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtTab.Rows(i)("gl_ID") & " And GL_Head=" & dtTab.Rows(i)("gl_Head") & " And GL_CompID=" & iCompID & ")")
                                If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select Sum(AFJD_Debit) From Acc_FixedAssets_JE_Details Where AFJD_Head=" & dtTab.Rows(i)("gl_AccHead") & " And AFJD_GL=" & dtTab.Rows(i)("gl_Parent") & " And AFJD_SubGL=" & dtTab.Rows(i)("gl_ID") & " And AFJD_MasterID in(Select AFJ_ID From Acc_FixedAssets_JE Where AFJ_Block=1) And AFJD_CompID=" & iCompID & "")) = False Then
                                    dFAJEDebit = objDB.SQLExecuteScalar(sNameSpace, "Select Sum(AFJD_Debit) From Acc_FixedAssets_JE_Details Where AFJD_Head=" & dtTab.Rows(i)("gl_AccHead") & " And AFJD_GL=" & dtTab.Rows(i)("gl_Parent") & " And AFJD_SubGL=" & dtTab.Rows(i)("gl_ID") & " And AFJD_MasterID in(Select AFJ_ID From Acc_FixedAssets_JE Where AFJ_Block=1) And AFJD_CompID=" & iCompID & "")
                                Else
                                    dFAJEDebit = 0
                                End If
                            End If
                            dRow("Additions") = Convert.ToDecimal(iAdditionAmt + dFAJEDebit).ToString("#,##0.00")
                            iAdditionBal = iAdditionBal + iAdditionAmt + dFAJEDebit

                            If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "select ATR_CrAmount from account_Transactions where atr_trType = 3 And ATR_YearID=" & iLastYear & " And ATR_CompID=" & iCompID & " And atr_glcode in (Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtTab.Rows(i)("gl_ID") & " And GL_Head=" & dtTab.Rows(i)("gl_Head") & " And GL_CompID=" & iCompID & ")")) = False Then
                                iDeductionAmt = objDB.SQLExecuteScalar(sNameSpace, "select ATR_CrAmount from account_Transactions where atr_trType = 3 And ATR_YearID=" & iLastYear & " And ATR_CompID=" & iCompID & " And atr_glcode in (Select GL_GLCode From Chart_Of_Accounts Where GL_ID=" & dtTab.Rows(i)("gl_ID") & " And GL_Head=" & dtTab.Rows(i)("gl_Head") & " And GL_CompID=" & iCompID & ")")

                                If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select Sum(AFJD_Credit) From Acc_FixedAssets_JE_Details Where AFJD_Head=" & dtTab.Rows(i)("gl_AccHead") & " And AFJD_GL=" & dtTab.Rows(i)("gl_Parent") & " And AFJD_SubGL=" & dtTab.Rows(i)("gl_ID") & " And AFJD_MasterID in(Select AFJ_ID From Acc_FixedAssets_JE Where AFJ_Block=2) And AFJD_CompID=" & iCompID & "")) = False Then
                                    dFAJECredit = objDB.SQLExecuteScalar(sNameSpace, "Select Sum(AFJD_Credit) From Acc_FixedAssets_JE_Details Where AFJD_Head=" & dtTab.Rows(i)("gl_AccHead") & " And AFJD_GL=" & dtTab.Rows(i)("gl_Parent") & " And AFJD_SubGL=" & dtTab.Rows(i)("gl_ID") & " And AFJD_MasterID in(Select AFJ_ID From Acc_FixedAssets_JE Where AFJ_Block=2) And AFJD_CompID=" & iCompID & "")
                                Else
                                    dFAJECredit = 0
                                End If

                            End If
                            dRow("Deductions") = Convert.ToDecimal(iDeductionAmt + dFAJECredit).ToString("#,##0.00")
                            iDeductionBal = iDeductionBal + iDeductionAmt + dFAJECredit

                            iClosingBal = (iOpnBalance + iAdditionAmt) - iDeductionAmt
                            dRow("ClosingBlock") = Convert.ToDecimal(iClosingBal).ToString("#,##0.00")
                            iClosingTotal = iClosingTotal + iClosingBal
                            'iOpnBalance = 0

                            dRow("Dep") = ""

                            'If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=4 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
                            '    iDepreciationAmt = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=4 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
                            'End If
                            dDepreciationRate = objDB.SQLGetDescription(sNameSpace, "Select FAM_DepRate From Fixed_Asset_Master Where FAM_DespreaciationType=1 And FAM_glDescID=" & dtTab.Rows(i)("gl_ID") & " And FAM_CompID=" & iCompID & "")

                            iDepreciationAmt = (((iOpnBalance - (iDeductionAmt + dFAJECredit))) * dDepreciationRate) / 100
                            dRow("Depreciation") = Convert.ToDecimal(iDepreciationAmt).ToString("#,##0.00")
                            iDepreciationTotal = iDepreciationTotal + iDepreciationAmt
                            iOpnBalance = 0

                            If IsDBNull(objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=5 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")) = False Then
                                iDepDeductionAmt = objDB.SQLExecuteScalar(sNameSpace, "Select AFB_Amount From Acc_FixedAssets_Breakup Where AFB_Head=5 And AFB_GLID=" & dtTab.Rows(i)("gl_ID") & " And AFB_YearId=" & iLastYear & " And AFB_CompID=" & iCompID & "")
                            End If
                            dRow("DepDeduction") = iDepDeductionAmt
                            iDepDeductionTotal = iDepDeductionTotal + iDepDeductionAmt

                            iUptoBal = iDepreciationAmt + iDepDeductionAmt
                            dRow("Upto") = Convert.ToDecimal(iUptoBal).ToString("#,##0.00")
                            iUptoTotal = iUptoTotal + iUptoBal
                            iDepreciationAmt = 0

                            iNetBlock1 = iClosingBal - iUptoBal
                            dRow("MarchF") = Convert.ToDecimal(iNetBlock1).ToString("#,##0.00")
                            iNetBlockBal = iNetBlockBal + iNetBlock1
                            dRow("March2") = ""

                            dt.Rows.Add(dRow)
                        Next
                    End If
                    dRow = dt.NewRow()
                    dRow("glHead") = 0
                    dRow("Description") = "Sub Total"
                    dRow("OpeningBalance") = Convert.ToDecimal(iOpenBalTotal).ToString("#,##0.00")
                    iGrandTotal = iGrandTotal + iOpenBalTotal
                    iOpenBalTotal = 0

                    dRow("Additions") = Convert.ToDecimal(iAdditionBal).ToString("#,##0.00")
                    iAdditionBalTotal = iAdditionBalTotal + iAdditionBal
                    iAdditionBal = 0

                    dRow("Deductions") = Convert.ToDecimal(iDeductionBal).ToString("#,##0.00")
                    iDeductionBalTotal = iDeductionBalTotal + iDeductionBal
                    iDeductionBal = 0

                    dRow("ClosingBlock") = Convert.ToDecimal(iClosingTotal).ToString("#,##0.00")
                    iClsoingBalTotal = iClsoingBalTotal + iClosingTotal
                    iClosingTotal = 0

                    dRow("Depreciation") = Convert.ToDecimal(iDepreciationTotal).ToString("#,##0.00")
                    iDepreciationGrandTotal = iDepreciationGrandTotal + iDepreciationTotal
                    iDepreciationTotal = 0

                    dRow("DepDeduction") = iDepDeductionTotal
                    iDepDeductionGrandTotal = iDepDeductionGrandTotal + iDepDeductionTotal
                    iDepDeductionTotal = 0

                    dRow("Upto") = Convert.ToDecimal(iUptoTotal).ToString("#,##0.00")
                    iUptoBalTotal = iUptoBalTotal + iUptoTotal
                    iUptoTotal = 0

                    dRow("MarchF") = Convert.ToDecimal(iNetBlockBal).ToString("#,##0.00")
                    iNetBlockTotal = iNetBlockTotal + iNetBlockBal
                    iNetBlockBal = 0
                    dt.Rows.Add(dRow)

                    dRow = dt.NewRow()
                    dRow("glHead") = 0
                    dRow("Description") = ""
                    dt.Rows.Add(dRow)
                Next
            End If

            dRow = dt.NewRow()
            dRow("glHead") = 0
            dRow("Description") = "TOTAL"
            dRow("OpeningBalance") = Convert.ToDecimal(iGrandTotal).ToString("#,##0.00")
            iGrandTotal = 0

            dRow("Additions") = Convert.ToDecimal(iAdditionBalTotal).ToString("#,##0.00")
            iAdditionBalTotal = 0

            dRow("Deductions") = Convert.ToDecimal(iDeductionBalTotal).ToString("#,##0.00")
            iDeductionBalTotal = 0

            dRow("ClosingBlock") = Convert.ToDecimal(iClsoingBalTotal).ToString("#,##0.00")
            iClsoingBalTotal = 0

            dRow("Depreciation") = Convert.ToDecimal(iDepreciationGrandTotal).ToString("#,##0.00")
            iDepreciationGrandTotal = 0

            dRow("DepDeduction") = iDepDeductionGrandTotal
            iDepDeductionGrandTotal = 0

            dRow("Upto") = Convert.ToDecimal(iUptoBalTotal).ToString("#,##0.00")
            iUptoBalTotal = 0

            dRow("MarchF") = Convert.ToDecimal(iNetBlockTotal).ToString("#,##0.00")
            iNetBlockTotal = 0
            dt.Rows.Add(dRow)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
