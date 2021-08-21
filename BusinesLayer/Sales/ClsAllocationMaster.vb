Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsAllocationMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0

        Dim sString As String = ""
        Dim bCheck As Boolean
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("AllocationOrder", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Sales_Allocate_Master where SAM_YearID=" & iYearID & " And SAM_CompID =" & iCompID & " "
            sSql = sSql & " Order By SAM_ID Desc"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("SAM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("SAM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SAM_Code").ToString()) = False Then
                        dr("AllocationOrder") = ds.Tables(0).Rows(i)("SAM_Code").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SAM_Party").ToString()) = False Then
                        dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & ds.Tables(0).Rows(i)("SAM_Party").ToString() & " And BM_CompID=" & iCompID & " ")
                    End If

                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Dispatch_Master Where SDM_AllocateID=" & ds.Tables(0).Rows(i)("SAM_ID").ToString() & " ")
                    If bCheck = True Then
                        'sString = objDBL.SQLGetDescription(sNameSpace, "Select SDM_Status From Sales_Dispatch_Master Where SDM_AllocateID=" & ds.Tables(0).Rows(i)("SAM_ID").ToString() & " ")
                        'If (sString = "W") Then
                        '    dr("Status") = "Dispatched"
                        'End If
                        dr("Status") = "Dispatched"
                    Else
                        dr("Status") = "Waiting For Dispatch"
                    End If

                    'If (ds.Tables(0).Rows(i)("SAM_Status") = "W") Then
                    '    dr("Status") = "Dispatched"
                    'ElseIf (ds.Tables(0).Rows(i)("SAM_Status") = "A") Then
                    '    dr("Status") = "Approved"
                    'ElseIf (ds.Tables(0).Rows(i)("SAM_Status") = "D") Then
                    '    dr("Status") = "De-Activated"
                    'End If

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
            sSql = "Update Sales_Allocate_Master Set SAM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " SAM_Status='A',SAM_ApprovedBy= " & iUserID & ",SAM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " SAM_Status='D',SAM_DeletedBy= " & iUserID & ",SAM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " SAM_Status='A' "
            End If
            sSql = sSql & " Where SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " And SAM_ID = " & iMasId & ""
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
            sSql = "" : sSql = "Select * From Sales_Allocate_Master Where SAM_Status='" & sSelectedStatus & "' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Sales_Allocate_Master Set SAM_IPAddress='" & sIPAddress & "',"
                    If sStatus = "W" Then
                        sSql = sSql & " SAM_Status='A',SAM_ApprovedBy= " & iUserID & ",SAM_ApprovedOn=GetDate()"
                    ElseIf sStatus = "D" Then
                        sSql = sSql & " SAM_Status='D',SAM_DeletedBy= " & iUserID & ",SAM_DeletedOn=GetDate()"
                    ElseIf sStatus = "A" Then
                        sSql = sSql & " SAM_Status='A' "
                    End If
                    sSql = sSql & " Where SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " And SAM_ID = " & dtPurchase.Rows(j)("SAM_ID") & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
