Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsNonTradingDashBoard
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadPurchaseVoucher(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYear As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransactionNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Party", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillAmount", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillStatus", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Acc_NonTrading_Purchase_Master where Acc_Purchase_Year=" & iYear & " And Acc_Purchase_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Acc_Purchase_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_Purchase_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_Purchase_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By Acc_Purchase_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_Purchase_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_Purchase_TransactionNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_BillNo").ToString()) = False Then
                        dr("BillNo") = ds.Tables(0).Rows(i)("Acc_Purchase_BillNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_BillDate").ToString()) = False Then
                        dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_Purchase_BillDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_BillAmount").ToString()) = False Then
                        dr("BillAmount") = ds.Tables(0).Rows(i)("Acc_Purchase_BillAmount").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_Party").ToString()) = False Then
                        dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_Purchase_Party").ToString())
                    End If

                    'If (ds.Tables(0).Rows(i)("Acc_Purchase_MisMatchFlag") = "0") Then
                    '    dr("BillStatus") = "Matched"
                    'ElseIf (ds.Tables(0).Rows(i)("Acc_Purchase_MisMatchFlag") = "1") Then
                    '    dr("BillStatus") = "Mis Matched"
                    'End If

                    If (ds.Tables(0).Rows(i)("Acc_Purchase_Status") = "S") Then
                        dr("Status") = "Submitted"
                    Else
                        If (ds.Tables(0).Rows(i)("Acc_Purchase_DelFlag") = "W") Then
                            dr("Status") = "Waiting for Approval"
                        ElseIf (ds.Tables(0).Rows(i)("Acc_Purchase_DelFlag") = "A") Then
                            dr("Status") = "Activated"
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
    Public Function GetPartyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As String
        Dim sSQL As String = ""
        Dim sParty As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "" : sSQL = "Select *  from CustomerSupplierMaster where CSM_Delflag='A' and CSM_ID = " & iParty & " and CSM_CompID= " & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("CSM_Name").ToString()) = False Then
                    sParty = dt.Rows(0)("CSM_Name").ToString() & " - " & dt.Rows(0)("CSM_Code").ToString()
                Else
                    sParty = ""
                End If
            Else
                sParty = ""
            End If
            Return sParty
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
