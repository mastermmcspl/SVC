Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsDispatchDetailsMaster
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
            dc = New DataColumn("InvoiceNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SaleType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Sales_Dispatch_Master,Sales_Proforma_Order where SDM_OrderID=SPO_ID And SPO_OrderType='S' And SDM_YearID=" & iYearID & " And SDM_CompID =" & iCompID & " "
            sSql = sSql & " Order By SDM_ID DESC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("SDM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_Code").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("SDM_Code").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_DispatchDate").ToString()) = False Then
                        dr("InvoiceDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("SDM_DispatchDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_SupplierID").ToString()) = False Then
                        dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & ds.Tables(0).Rows(i)("SDM_SupplierID").ToString() & " And BM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_DispatchStatus").ToString()) = False Then
                        dr("SaleType") = ds.Tables(0).Rows(i)("SDM_DispatchStatus").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_Status").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("SDM_Status").ToString() = "W" Then
                            dr("Status") = "Waiting For Approve"
                        ElseIf ds.Tables(0).Rows(i)("SDM_Status").ToString() = "A" Then
                            dr("Status") = "Approved"
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
    Public Sub UpdateJEMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Dispatch_Master Set SDM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " SDM_Status='A',SDM_ApprovedBy= " & iUserID & ",SDM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " SDM_Status='D',SDM_DeletedBy= " & iUserID & ",SDM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " SDM_Status='A' "
            End If
            sSql = sSql & " Where SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " And SDM_ID = " & iMasId & ""
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
            sSql = "" : sSql = "Select * From Sales_Dispatch_Master Where SDM_Status='" & sSelectedStatus & "' And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Sales_Dispatch_Master Set SDM_IPAddress='" & sIPAddress & "',"
                    If sStatus = "W" Then
                        sSql = sSql & " SDM_Status='A',SDM_ApprovedBy= " & iUserID & ",SDM_ApprovedOn=GetDate()"
                    ElseIf sStatus = "D" Then
                        sSql = sSql & " SDM_Status='D',SDM_DeletedBy= " & iUserID & ",SDM_DeletedOn=GetDate()"
                    ElseIf sStatus = "A" Then
                        sSql = sSql & " SDM_Status='A' "
                    End If
                    sSql = sSql & " Where SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " And SDM_ID = " & dtPurchase.Rows(j)("SDM_ID") & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadCashSalesOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SaleType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Sales_Dispatch_Master,Sales_Proforma_Order where SDM_OrderID=SPO_ID And SPO_OrderType='O' And SDM_YearID=" & iYearID & " And SDM_CompID =" & iCompID & " "
            sSql = sSql & " Order By SDM_ID DESC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("SDM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_Code").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("SDM_Code").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_DispatchDate").ToString()) = False Then
                        dr("InvoiceDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("SDM_DispatchDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_SupplierID").ToString()) = False Then
                        dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & ds.Tables(0).Rows(i)("SDM_SupplierID").ToString() & " And BM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_DispatchStatus").ToString()) = False Then
                        dr("SaleType") = ds.Tables(0).Rows(i)("SDM_DispatchStatus").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_Status").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("SDM_Status").ToString() = "W" Then
                            dr("Status") = "Waiting For Approve"
                        ElseIf ds.Tables(0).Rows(i)("SDM_Status").ToString() = "A" Then
                            dr("Status") = "Approved"
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
