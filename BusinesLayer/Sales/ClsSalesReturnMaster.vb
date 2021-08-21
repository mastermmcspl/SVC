Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSalesReturnMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadSalesOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SalesReturn", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ReturnDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ReferenceNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Category", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Sales_Return_Master where SRM_YearID=" & iYearID & " And SRM_CompID =" & iCompID & " "
            sSql = sSql & " Order By SRM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("SRM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("SRM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SRM_ReturnOrderCode").ToString()) = False Then
                        dr("SalesReturn") = ds.Tables(0).Rows(i)("SRM_ReturnOrderCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SRM_ReturnDate").ToString()) = False Then
                        dr("ReturnDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("SRM_ReturnDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SRM_ReferenceNo").ToString()) = False Then
                        dr("ReferenceNo") = ds.Tables(0).Rows(i)("SRM_ReferenceNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SRM_PartyID").ToString()) = False Then
                        dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & ds.Tables(0).Rows(i)("SRM_PartyID").ToString() & " And BM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SRM_Category").ToString()) = False Then
                        dr("Category") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where MAS_Master=" & ds.Tables(0).Rows(i)("SRM_Category").ToString() & " And MAS_DelFlag='A' And Mas_CompID=" & iCompID & " ")
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
