Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSalesPartyDashboard
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadSalesParty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PartyName", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PartyCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ContactPerson", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("MobileNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Email", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Sales_Buyers_Masters where BM_Year=" & iYearID & " And BM_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And BM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And BM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And BM_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By BM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("BM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("BM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("BM_Name").ToString()) = False Then
                        dr("PartyName") = ds.Tables(0).Rows(i)("BM_Name").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("BM_Code").ToString()) = False Then
                        dr("PartyCode") = ds.Tables(0).Rows(i)("BM_Code").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("BM_ContactPerson").ToString()) = False Then
                        dr("ContactPerson") = ds.Tables(0).Rows(i)("BM_ContactPerson").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("BM_MobileNo").ToString()) = False Then
                        dr("MobileNo") = ds.Tables(0).Rows(i)("BM_MobileNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("BM_EmailID").ToString()) = False Then
                        dr("Email") = ds.Tables(0).Rows(i)("BM_EmailID").ToString()
                    End If

                    If (ds.Tables(0).Rows(i)("BM_DelFlag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("BM_DelFlag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("BM_DelFlag") = "D") Then
                        dr("Status") = "De-Activated"
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdatePartyMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Buyers_Masters Set BM_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " BM_Status='A',BM_DelFlag='A',BM_ApprovedBy= " & iUserID & ",BM_ApprovedOn=GetDate()"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " BM_Status='D',BM_DelFlag='D',BM_DeletedBy= " & iUserID & ",BM_DeletedOn=GetDate()"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " BM_Status='A',BM_DelFlag='A' "
            End If
            sSql = sSql & " Where BM_CompID=" & iCompID & " And BM_Year=" & iYearID & " And BM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdatePartyMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Sales_Buyers_Masters Where BM_DelFlag='" & sSelectedStatus & "' And BM_CompID=" & iCompID & " And BM_Year=" & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Sales_Buyers_Masters Set SPO_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " BM_Status='A',BM_DelFlag='A',BM_ApprovedBy= " & iUserID & ",BM_ApprovedOn=GetDate()"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " BM_Status='D',BM_DelFlag='D',BM_DeletedBy= " & iUserID & ",BM_DeletedOn=GetDate()"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " BM_Status='A',BM_DelFlag='A' "
                    End If
                    sSql = sSql & " Where BM_CompID=" & iCompID & " And BM_Year=" & iYearID & " And BM_ID = " & dtPurchase.Rows(j)("BM_ID") & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
