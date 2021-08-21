Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsGoodsReturnMaster
    Private objFAS As New clsFASGeneral
    Private objDb As New DBHelper
    Public Function LoadAllReturnDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtZoneRegionBranchAreaDetails As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String, sModuleRole As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("PoID")
            dt.Columns.Add("POnO")
            dt.Columns.Add("PoDate")
            dt.Columns.Add("Supplier")
            dt.Columns.Add("Status")

            sSql = "SELECT GRM_ID,GRM_ReturnDate,GRM_ReturnNo,GRM_Supplier,GRM_Status,GRM_DelFlag FROM Goods_Return_Master"
            dtDetails = objDb.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("GRM_ID")) = False Then
                    dRow("PoID") = dtDetails.Rows(i)("GRM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("GRM_ReturnNo")) = False Then
                    dRow("POnO") = dtDetails.Rows(i)("GRM_ReturnNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("GRM_ReturnDate")) = False Then
                    dRow("PoDate") = objFAS.FormatDtForRDBMS(dtDetails.Rows(i)("GRM_ReturnDate"), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("GRM_Supplier")) = False Then
                    dRow("Supplier") = objDb.SQLExecuteScalar(sAC, "select CSM_Name from CustomerSupplierMaster where CSM_ID= " & dtDetails.Rows(i)("GRM_Supplier") & "")
                End If
                If (dtDetails.Rows(i)("GRM_DelFlag") = "W") Then
                    dRow("Status") = "Waiting for Approval"
                ElseIf (dtDetails.Rows(i)("GRM_DelFlag") = "A") Then
                    dRow("Status") = "Activated"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGoodsReturn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("SrNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PoID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("POnO", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PoDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Supplier", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "SELECT * FROM Goods_Return_Master where GRM_YearID=" & iYearID & " And GRM_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " and GRM_DelFlag = 'A'"
            ElseIf iStatus = 1 Then
                sSql = sSql & " and GRM_DelFlag = 'W'"
            End If

            ds = objDb.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    'If IsDBNull(ds.Tables(0).Rows(i)("SPO_ID").ToString()) = False Then
                    dr("SrNo") = i + 1
                    'End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GRM_ID").ToString()) = False Then
                        dr("PoID") = ds.Tables(0).Rows(i)("GRM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GRM_ReturnNo").ToString()) = False Then
                        dr("POnO") = ds.Tables(0).Rows(i)("GRM_ReturnNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GRM_ReturnDate").ToString()) = False Then
                        dr("PoDate") = objFAS.FormatDtForRDBMS(ds.Tables(0).Rows(i)("GRM_ReturnDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GRM_Supplier").ToString()) = False Then
                        dr("Supplier") = objDb.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("GRM_Supplier").ToString() & " And CSM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GRM_DelFlag").ToString()) = False Then
                        Dim status As String
                        status = ds.Tables(0).Rows(i)("GRM_DelFlag").ToString()
                        If status = "A" Then
                            dr("Status") = "Activated"
                        ElseIf status = "W" Then
                            dr("Status") = "Waiting for Approval"
                        End If
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
