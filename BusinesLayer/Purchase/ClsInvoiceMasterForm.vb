Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsInvoiceMasterForm
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
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
            dc = New DataColumn("Supplier", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SaleType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceStatus", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Purchase_Invoice_Master where PIM_YearID=" & iYearID & " And PIM_CompID =" & iCompID & " "
            sSql = sSql & " Order By PIM_ID DESC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("PIM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_No").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("PIM_No").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_InvoiceDate").ToString()) = False Then
                        dr("InvoiceDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("PIM_InvoiceDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_SupplierID").ToString()) = False Then
                        dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("PIM_SupplierID").ToString() & " And CSM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_InvoiceStatus").ToString()) = False Then
                        dr("SaleType") = ds.Tables(0).Rows(i)("PIM_InvoiceStatus").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_BillDifferenceStatus").ToString()) = False Then
                        dr("InvoiceStatus") = ds.Tables(0).Rows(i)("PIM_BillDifferenceStatus").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_Status").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("PIM_Status").ToString() = "W" Then
                            dr("Status") = "Waiting For Approve"
                        ElseIf ds.Tables(0).Rows(i)("PIM_Status").ToString() = "A" Then
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
            sSql = "Update Purchase_Invoice_Master Set PIM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " PIM_Status='A',PIM_ApprovedBy= " & iUserID & ",PIM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " PIM_Status='D',PIM_DeletedBy= " & iUserID & ",PIM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " PIM_Status='A' "
            End If
            sSql = sSql & " Where PIM_CompID=" & iCompID & " And PIM_YearID=" & iYearID & " And PIM_ID = " & iMasId & ""
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
            sSql = "" : sSql = "Select * From Purchase_Invoice_Master Where PIM_Status='" & sSelectedStatus & "' And PIM_CompID=" & iCompID & " And PIM_YearID=" & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Purchase_Invoice_Master Set PIM_IPAddress='" & sIPAddress & "',"
                    If sStatus = "W" Then
                        sSql = sSql & " PIM_Status='A',PIM_ApprovedBy= " & iUserID & ",PIM_ApprovedOn=GetDate()"
                    ElseIf sStatus = "D" Then
                        sSql = sSql & " PIM_Status='D',PIM_DeletedBy= " & iUserID & ",PIM_DeletedOn=GetDate()"
                    ElseIf sStatus = "A" Then
                        sSql = sSql & " PIM_Status='A' "
                    End If
                    sSql = sSql & " Where PIM_CompID=" & iCompID & " And PIM_YearID=" & iYearID & " And PIM_ID = " & dtPurchase.Rows(j)("PIM_ID") & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
