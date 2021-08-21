Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports DatabaseLayer
Imports System
Imports Microsoft.VisualBasic
Imports System.Configuration
Imports BusinesLayer
Public Class clsAuxilaryReport
    Private objDBL As New DatabaseLayer.DBHelper
    Private objFASGen As New clsFASGeneral
    Public Function LoadReports(ByVal sNameSpace As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_ID,Mas_Desc from ACC_General_Master where Mas_Master =14 and Mas_Delflag ='X' order by mas_id"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetReportDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iRptId As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dRow As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("ID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("MasterGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Particulars", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpeningBalance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Additions", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Transfer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Reduction", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Sold", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("RTransfer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("RReduction", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("RRateOff", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ROpnBal", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DFortheYear", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DDeduction", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DClsBal", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("MOpnBal", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("MClsBal", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Trace_Report_Master where TRM_RptID = " & iRptId & " order by TRM_Id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dRow = dt.NewRow()
                dRow("ID") = ds.Tables(0).Rows(i)("TRM_ID")
                dRow("Particulars") = ds.Tables(0).Rows(i)("TRM_HeaderName")

                'Opening Balance
                dRow("OpeningBalance") = ""

                'Additon
                dRow("Additions") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_Additon from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Trnasfer
                dRow("Transfer") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_Transfer from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Reduction
                dRow("Reduction") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_Reduction from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Sold
                dRow("Sold") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_Sold from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Transfer
                dRow("RTransfer") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_RTransfer from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Reduction
                dRow("RReduction") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_RReduction from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Rate off
                dRow("RRateOff") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_RRateoff from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Opening Balance
                dRow("ROpnBal") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_ROpnBal from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'For the Year
                dRow("DFortheYear") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_DFortheYear from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Deduction
                dRow("DDeduction") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_DDeduction from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Close Balance
                dRow("DClsBal") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_DClsBal from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Opening Main Balance
                dRow("MOpnBal") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_MOpnBal from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                'Closing Main Balance
                dRow("MClsBal") = objDBL.SQLExecuteScalar(sNameSpace, "Select Acc_FAT_MClsBal from acc_FixedAssets_Transaction where Acc_FAT_FixedAssetsID = " & ds.Tables(0).Rows(i)("TRM_ID") & " ")

                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
