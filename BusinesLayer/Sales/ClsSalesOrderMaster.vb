Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSalesOrderMaster
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
            dc = New DataColumn("SalesOrder", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OrderDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Category", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BuyerRefNo", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Sales_ProForma_Order where SPO_OrderType='S' AND SPO_YearID=" & iYearID & " And SPO_CompID =" & iCompID & " "
            sSql = sSql & " Order By SPO_ID Desc"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("SPO_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_OrderCode").ToString()) = False Then
                        dr("SalesOrder") = ds.Tables(0).Rows(i)("SPO_OrderCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_OrderDate").ToString()) = False Then
                        dr("OrderDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("SPO_OrderDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_PartyName").ToString()) = False Then
                        dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & ds.Tables(0).Rows(i)("SPO_PartyName").ToString() & " And BM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_Category").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("SPO_Category").ToString() <> "" Then
                            dr("Category") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where MAS_Master=23 And MAS_ID=" & ds.Tables(0).Rows(i)("SPO_Category").ToString() & " And MAS_DelFlag='A' And Mas_CompID=" & iCompID & " ")
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_BuyerOrderNo").ToString()) = False Then
                        dr("BuyerRefNo") = ds.Tables(0).Rows(i)("SPO_BuyerOrderNo").ToString()
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateJEMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_ProForma_Order Set SPO_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " SPO_Status='A',SPO_ApprovedBy= " & iUserID & ",SPO_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " SPO_Status='D',SPO_DeletedBy= " & iUserID & ",SPO_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " SPO_Status='A' "
            End If
            sSql = sSql & " Where SPO_OrderType='S' AND SPo_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " And SPO_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateJEMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Sales_ProForma_Order Where SPO_OrderType='S' AND SPO_Status='" & sSelectedStatus & "' And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Sales_ProForma_Order Set SPO_IPAddress='" & sIPAddress & "',"
                    If sStatus = "W" Then
                        sSql = sSql & " SPO_Status='A',SPO_ApprovedBy= " & iUserID & ",SPO_ApprovedOn=GetDate()"
                    ElseIf sStatus = "D" Then
                        sSql = sSql & " SPO_Status='D',SPO_DeletedBy= " & iUserID & ",SPO_DeletedOn=GetDate()"
                    ElseIf sStatus = "A" Then
                        sSql = sSql & " SPO_Status='A' "
                    End If
                    sSql = sSql & " Where SPO_OrderType='S' AND SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " And SPO_ID = " & dtPurchase.Rows(j)("SPO_ID") & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
