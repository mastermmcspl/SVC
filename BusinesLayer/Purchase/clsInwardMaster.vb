'Public Class clsInwardMaster

'End Class
Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsInwardMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral


    Public Function LoadInwardOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim sl As Integer = 0
        Dim i As Integer = 0
        Try
            dc = New DataColumn("SLNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GinNumber", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from purchase_gin_master where PGM_OrderID in(select POM_ID from purchase_order_master where POM_OralStatus='P') and PGM_YearID=" & iYearID & " And PGM_CompID =" & iCompID & " "
            sSql = sSql & " Order By PGM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    sl = sl + 1
                    dr("SLNo") = sl
                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("PGM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_DocumentRefNo").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("PGM_DocumentRefNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_InvoiceDate").ToString()) = False Then
                        dr("InvoiceDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("PGM_InvoiceDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_Supplier").ToString()) = False Then
                        dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("PGM_Supplier").ToString() & " And CSM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_GIN_Number").ToString()) = False Then
                        dr("GinNumber") = ds.Tables(0).Rows(i)("PGM_GIN_Number").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_Status").ToString()) = False Then
                        If (ds.Tables(0).Rows(i)("PGM_Status").ToString() = "W") Then
                            dr("Status") = "Waiting For Approval"
                        Else
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
End Class
