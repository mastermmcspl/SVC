Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsDispatchFormMaster
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

            sSql = "Select * from Dispatch_Master,Sales_Proforma_Order where DM_OrderID=SPO_ID And SPO_OrderType='S' And DM_YearID=" & iYearID & " And DM_CompID =" & iCompID & " "
            sSql = sSql & " Order By DM_ID DESC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("DM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("DM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DM_Code").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("DM_Code").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DM_DispatchDate").ToString()) = False Then
                        dr("InvoiceDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("DM_DispatchDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DM_SupplierID").ToString()) = False Then
                        dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & ds.Tables(0).Rows(i)("DM_SupplierID").ToString() & " And BM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DM_DispatchStatus").ToString()) = False Then
                        dr("SaleType") = ds.Tables(0).Rows(i)("DM_DispatchStatus").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DM_Status").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("DM_Status").ToString() = "W" Then
                            dr("Status") = "Waiting For Approve"
                        ElseIf ds.Tables(0).Rows(i)("DM_Status").ToString() = "A" Then
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
            sSql = "Update Dispatch_Master Set DM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " DM_Status='A',DM_ApprovedBy= " & iUserID & ",DM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " DM_Status='D',DM_DeletedBy= " & iUserID & ",DM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " DM_Status='A' "
            End If
            sSql = sSql & " Where DM_CompID=" & iCompID & " And DM_YearID=" & iYearID & " And DM_ID = " & iMasId & ""
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
            sSql = "" : sSql = "Select * From Dispatch_Master Where DM_Status='" & sSelectedStatus & "' And DM_CompID=" & iCompID & " And DM_YearID=" & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Dispatch_Master Set DM_IPAddress='" & sIPAddress & "',"
                    If sStatus = "W" Then
                        sSql = sSql & " DM_Status='A',DM_ApprovedBy= " & iUserID & ",DM_ApprovedOn=GetDate()"
                    ElseIf sStatus = "D" Then
                        sSql = sSql & " DM_Status='D',DM_DeletedBy= " & iUserID & ",DM_DeletedOn=GetDate()"
                    ElseIf sStatus = "A" Then
                        sSql = sSql & " DM_Status='A' "
                    End If
                    sSql = sSql & " Where DM_CompID=" & iCompID & " And DM_YearID=" & iYearID & " And DM_ID = " & dtPurchase.Rows(j)("DM_ID") & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
