Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsPurchaseSalesJE
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadPurchaseJournalEntry(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer, ByVal sType As String) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0

        Dim dtATD As New DataTable
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
            dc = New DataColumn("BillType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Acc_Purchase_JE_Master where Acc_PJE_YearID=" & iYearID & " And Acc_PJE_CompID =" & iCompID & " And Acc_PJE_Type='" & sType & "' "
            If iStatus = 0 Then
                sSql = sSql & " And Acc_PJE_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_PJE_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_PJE_Status='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By Acc_PJE_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PJE_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_PJE_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PJE_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_PJE_TransactionNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PJE_BillNo").ToString()) = False Then
                        dr("BillNo") = ds.Tables(0).Rows(i)("Acc_PJE_BillNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PJE_BillDate").ToString()) = False Then
                        dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_PJE_BillDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PJE_BillType").ToString()) = False Then
                        dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_PJE_BillType").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PJE_Party").ToString()) = False Then
                        dr("Party") = GetSupplierName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_PJE_Party").ToString())
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_PJE_Status") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_PJE_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_PJE_Status") = "D") Then
                        dr("Status") = "De-Activated"
                    End If

                    dtATD = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & dr("Id") & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
                    If dtATD.Rows.Count > 0 Then
                        'For k = 0 To dtATD.Rows.Count - 1
                        If dtATD.Rows(0)("ATD_GL") = 0 Then
                            dr("Status") = "InComplete Transaction"
                        End If
                        'Next
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesJournalEntry(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer, ByVal sType As String) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0

        Dim dtATD As New DataTable
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
            dc = New DataColumn("BillType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Acc_Sales_JE_Master where Acc_SJE_YearID=" & iYearID & " And Acc_SJE_CompID =" & iCompID & " And Acc_SJE_Type='" & sType & "' "

            If iStatus = 0 Then
                sSql = sSql & " And Acc_SJE_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_SJE_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_SJE_Status='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By Acc_SJE_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_SJE_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_SJE_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_SJE_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_SJE_TransactionNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_SJE_BillNo").ToString()) = False Then
                        dr("BillNo") = ds.Tables(0).Rows(i)("Acc_SJE_BillNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_SJE_BillDate").ToString()) = False Then
                        dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_SJE_BillDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_SJE_BillType").ToString()) = False Then
                        dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_SJE_BillType").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_SJE_Party").ToString()) = False Then
                        dr("Party") = GetCustomerName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_SJE_Party").ToString())
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_SJE_Status") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_SJE_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_SJE_Status") = "D") Then
                        dr("Status") = "De-Activated"
                    End If

                    dtATD = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & dr("Id") & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
                    If dtATD.Rows.Count > 0 Then
                        'For k = 0 To dtATD.Rows.Count - 1
                        If dtATD.Rows(0)("ATD_GL") = 0 Then
                            dr("Status") = "InComplete Transaction"
                        End If
                        'Next
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Sub UpdateJEMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal iPS As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
    '    Dim sSql As String = ""
    '    Try
    '        If iPS = 0 Then 'Purchase
    '            sSql = "Update Acc_Purchase_JE_Master Set Acc_PJE_IPAddress='" & sIPAddress & "',"
    '            If sStatus = "W" Then
    '                sSql = sSql & " Acc_PJE_Status='A',Acc_PJE_ApprovedBy= " & iUserID & ",Acc_PJE_ApprovedOn=GetDate()"
    '            ElseIf sStatus = "D" Then
    '                sSql = sSql & " Acc_PJE_Status='D',Acc_PJE_DeletedBy= " & iUserID & ",Acc_PJE_DeletedOn=GetDate()"
    '            ElseIf sStatus = "A" Then
    '                sSql = sSql & " Acc_PJE_Status='A' "
    '            End If
    '            sSql = sSql & " Where Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " And Acc_PJE_ID = " & iMasId & ""
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '        ElseIf iPS = 1 Then 'Sales
    '            sSql = "Update Acc_Sales_JE_Master Set Acc_SJE_IPAddress='" & sIPAddress & "',"
    '            If sStatus = "W" Then
    '                sSql = sSql & " Acc_SJE_Status='A',Acc_SJE_ApprovedBy= " & iUserID & ",Acc_SJE_ApprovedOn=GetDate()"
    '            ElseIf sStatus = "D" Then
    '                sSql = sSql & " Acc_SJE_Status='D',Acc_SJE_DeletedBy= " & iUserID & ",Acc_SJE_DeletedOn=GetDate()"
    '            ElseIf sStatus = "A" Then
    '                sSql = sSql & " Acc_SJE_Status='A' "
    '            End If
    '            sSql = sSql & " Where Acc_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " And Acc_SJE_ID = " & iMasId & ""
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    'Preethi Changes
    Public Sub UpdateJEMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal iPS As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer, ByVal sType As String)
        Dim sSql As String = ""
        Try
            If iPS = 0 Then 'Purchase
                sSql = "Update Acc_Purchase_JE_Master Set Acc_PJE_IPAddress='" & sIPAddress & "',"
                If sStatus = "W" Then
                    sSql = sSql & " Acc_PJE_Status='A',Acc_PJE_ApprovedBy= " & iUserID & ",Acc_PJE_ApprovedOn=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " Acc_PJE_Status='D',Acc_PJE_DeletedBy= " & iUserID & ",Acc_PJE_DeletedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " Acc_PJE_Status='A' "
                End If
                sSql = sSql & " Where Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " And Acc_PJE_ID = " & iMasId & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            ElseIf iPS = 1 Then 'Sales
                sSql = "Update Acc_Sales_JE_Master Set Acc_SJE_IPAddress='" & sIPAddress & "',"
                If sStatus = "W" Then
                    sSql = sSql & " Acc_SJE_Status='A',Acc_SJE_ApprovedBy= " & iUserID & ",Acc_SJE_ApprovedOn=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " Acc_SJE_Status='D',Acc_SJE_DeletedBy= " & iUserID & ",Acc_SJE_DeletedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " Acc_SJE_Status='A' "
                End If
                sSql = sSql & " Where Acc_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " And Acc_SJE_ID = " & iMasId & ""
                If sType = "CS" Then
                    sSql = sSql & " And Acc_SJE_Type='CS'"
                ElseIf sType = "SR" Then
                    sSql = sSql & " And Acc_SJE_Type='SR'"
                Else
                    sSql = sSql & " And Acc_SJE_Type='SI'"
                End If
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetSupplierName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As String
        Dim sSQL As String = ""
        Dim sParty As String = ""
        Dim dt As New DataTable
        Try
            'sSQL = "" : sSQL = "Select *  from Accounts_Party_Master where APM_Delflag='A' and APM_ID = " & iParty & " and APM_CompID= " & iCompID & ""
            'dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            'If dt.Rows.Count > 0 Then
            '    If IsDBNull(dt.Rows(0)("APM_Name").ToString()) = False Then
            '        sParty = dt.Rows(0)("APM_Name").ToString() & " - " & dt.Rows(0)("APM_Code").ToString()
            '    Else
            '        sParty = ""
            '    End If
            'Else
            '    sParty = ""
            'End If
            'Return sParty
            'sSQL = "" : sSQL = "Select *  from Acc_Customer_Master where ACM_Status ='A' and ACM_ID = " & iParty & " and ACM_CompID= " & iCompID & ""
            'dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            'If dt.Rows.Count > 0 Then
            '    If IsDBNull(dt.Rows(0)("ACM_Name").ToString()) = False Then
            '        sParty = dt.Rows(0)("ACM_Name").ToString() & " - " & dt.Rows(0)("ACM_Code").ToString()
            '    Else
            '        sParty = ""
            '    End If
            'Else
            '    sParty = ""
            'End If

            sSQL = "" : sSQL = "Select * from CustomerSupplierMaster where CSM_Delflag ='A' and CSM_ID = " & iParty & " and CSM_CompID= " & iCompID & ""
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
    Public Function GetCustomerName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As String
        Dim sSQL As String = ""
        Dim sParty As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "" : sSQL = "Select * from Sales_Buyers_Masters where BM_DelFlag ='A' and BM_ID = " & iParty & " and BM_CompID= " & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("BM_Name").ToString()) = False Then
                    sParty = dt.Rows(0)("BM_Name").ToString() & " - " & dt.Rows(0)("BM_Code").ToString()
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

    Public Function GetBillType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBillType As Integer) As String
        Dim sSQL As String = ""
        Dim sBillType As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "" : sSQL = "Select * from ACC_General_Master where mas_master = 9 and mas_Delflag ='A' and Mas_ID = " & iBillType & " and mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Mas_Desc").ToString()) = False Then
                    sBillType = dt.Rows(0)("Mas_Desc").ToString()
                Else
                    sBillType = ""
                End If
            Else
                sBillType = ""
            End If
            Return sBillType
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Sstr As String) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            If Sstr = "P" Then
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_PJE_ID)+1,1) from Acc_Purchase_JE_Master")
                'sSql = "" : sSql = "Select * from ACC_Voucher_Settings where AVS_TransType = 4  and AVS_CompID = " & iCompID & ""
                'ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                'If ds.Tables(0).Rows.Count > 0 Then
                'sPrefix = ds.Tables(0).Rows(0)("AVS_Prefix").ToString() & "00" & iMax
                sPrefix = "PJE00" & iMax
                'Else
                'sPrefix = ""
                'End If
                Return sPrefix
            ElseIf Sstr = "S" Then
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_SJE_ID)+1,1) from Acc_Sales_JE_Master")
                'sSql = "" : sSql = "Select * from ACC_Voucher_Settings where AVS_TransType = 4  and AVS_CompID = " & iCompID & ""
                'ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                'If ds.Tables(0).Rows.Count > 0 Then
                'sPrefix = ds.Tables(0).Rows(0)("AVS_Prefix").ToString() & "00" & iMax
                sPrefix = "SJE00" & iMax
                'Else
                'sPrefix = ""
                'End If
                Return sPrefix
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadFrequentlyUsed(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT ATR_GLCode,COUNT(ATR_GLCode) AS occurrence FROM Account_Transactions where Atr_CompID = " & iCompID & " GROUP BY ATR_GLCode ORDER BY occurrence DESC"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExistingVoucherNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iParty = 0 Then
                If sStr = "P" Then
                    sSql = "Select Acc_PJE_TransactionNo,Acc_PJE_ID from  Acc_Purchase_JE_Master where Acc_PJE_CompID=" & iCompID & " and Acc_PJE_YearID=" & iYearID & " order by Acc_PJE_ID Desc"
                ElseIf sStr = "S" Then
                    sSql = "Select Acc_SJE_TransactionNo,Acc_SJE_ID from  Acc_Sales_JE_Master where Acc_SJE_CompID=" & iCompID & " and Acc_SJE_YearID=" & iYearID & " order by Acc_SJE_ID Desc"
                End If
            Else
                If sStr = "P" Then
                    sSql = "Select Acc_PJE_TransactionNo,Acc_PJE_ID from  Acc_Purchase_JE_Master where Acc_PJE_CompID=" & iCompID & " and Acc_PJE_YearID=" & iYearID & " and Acc_PJE_Party = " & iParty & " order by Acc_PJE_ID Desc"
                ElseIf sStr = "S" Then
                    sSql = "Select Acc_SJE_TransactionNo,Acc_SJE_ID from  Acc_Sales_JE_Master where Acc_SJE_CompID=" & iCompID & " and Acc_SJE_YearID=" & iYearID & " and Acc_SJE_Party = " & iParty & " order by Acc_SJE_ID Desc"
                End If
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadBIllType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc from ACC_General_Master where mas_master = 9 and mas_Delflag ='A' and mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSuppliers(sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CSM_ID,CSM_Code + ' - ' + CSM_Name as Name  from CustomerSupplierMaster where CSM_DelFlag='A' and CSM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomers(sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Code + ' - ' + BM_Name as Name  from sales_Buyers_Masters where BM_DelFlag='A' and BM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadParty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sStr = "P" Then
                sSql = "Select ACM_ID,ACM_Name + ' - ' + ACM_Code as Name from Acc_Customer_Master where ACM_Type='S' And ACM_Status='A' and ACM_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            ElseIf sStr = "S" Then
                sSql = "Select ACM_ID,ACM_Name + ' - ' + ACM_Code as Name  from Acc_Customer_Master where ACM_Type='C' And ACM_Status='A' and ACM_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLocations(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select Mas_ID,Mas_Description + ' - ' + Mas_Code as Name  from sad_location_general_master where Mas_CustID = " & iParty & ""
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent =0 and Org_CompID=" & iCompID & "))))"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sStr <> "" Then
                sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
                sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and (gl_Reason_Creation Like '%" & sStr & "' Or gl_Reason_Creation Like '" & sStr & "%' Or gl_Reason_Creation Like '%" & sStr & "%') and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
                sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sStr <> "" Then
                sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
                sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and (gl_Reason_Creation Like '%" & sStr & "' Or gl_Reason_Creation Like '" & sStr & "%' Or gl_Reason_Creation Like '%" & sStr & "%') and gl_AccHead = " & iAccHead & " order by gl_glcode"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
                sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iAccHead & " order by gl_glcode"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function SaveSalesJournalMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objJE As ClsPurchaseSalesJE) As Integer
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from Acc_Sales_JE_Master where Acc_SJE_ID =" & objJE.iAcc_JE_ID & " and Acc_SJE_CompID =" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        If dt.Rows.Count > 0 Then
    '            sSql = "" : sSql = "Update Acc_Sales_JE_Master set Acc_SJE_Party = " & objJE.iAcc_JE_Party & ",Acc_SJE_Location=" & objJE.iAcc_JE_Location & ","
    '            sSql = sSql & "Acc_SJE_BillType = " & objJE.iAcc_JE_BillType & ",Acc_SJE_BillNo = '" & objGen.SafeSQL(objJE.sAcc_JE_BillNo) & "',"
    '            sSql = sSql & "Acc_SJE_BillDate = " & objGen.FormatDtForRDBMS(objJE.dAcc_JE_BillDate, "I") & ",Acc_SJE_BillAmount = " & objJE.dAcc_JE_BillAmount & " "

    '            If iPaymentType = 1 Then
    '                sSql = sSql & ",Acc_SJE_AdvanceAmount = " & objJE.dAcc_JE_AdvanceAmount & ",Acc_SJE_AdvanceNaration = '" & objGen.SafeSQL(objJE.sAcc_JE_AdvanceNaration) & "',Acc_JE_BalanceAmount = " & objJE.dAcc_JE_BalanceAmount & " "
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & ",Acc_SJE_NetAmount = " & objJE.dAcc_JE_NetAmount & ",Acc_SJE_PaymentNarration = '" & objJE.sAcc_JE_PaymentNarration & "' "
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & ",Acc_SJE_ChequeNo = " & objJE.sAcc_JE_ChequeNo & ","
    '                sSql = sSql & "Acc_SJE_ChequeDate = " & objGen.FormatDtForRDBMS(Acc_JE_ChequeDate, "I") & ",Acc_SJE_IFSCCode = '" & objJE.sAcc_JE_IFSCCode & "',"
    '                sSql = sSql & "Acc_SJE_BankName = '" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "',Acc_SJE_BranchName = '" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "' "
    '            End If
    '            sSql = sSql & "Where Acc_SJE_ID = " & objJE.iAcc_JE_ID & " and Acc_SJE_CompID =" & iCompID & ""
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return objJE.iAcc_JE_ID
    '        Else
    '            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_SJE_ID)+1,1) from Acc_Sales_JE_Master")
    '            sSql = "" : sSql = "Insert into Acc_Sales_JE_Master(Acc_SJE_ID,Acc_SJE_TransactionNo,Acc_SJE_Party,Acc_SJE_Location,"
    '            sSql = sSql & "Acc_SJE_BillType,Acc_SJE_BillNo,Acc_SJE_BillDate,Acc_SJE_BillAmount,"

    '            If iPaymentType = 1 Then
    '                sSql = sSql & "Acc_SJE_AdvanceAmount,Acc_SJE_AdvanceNaration,Acc_SJE_BalanceAmount,"
    '            ElseIf iPaymentType = 2 Then
    '                sSql = sSql & "Acc_SJE_TDSType,ACC_SJE_TDSDeduct,Acc_SJE_TDSAmount,Acc_SJE_TDSNarration,"
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & "Acc_SJE_NetAmount,Acc_SJE_PaymentNarration,"
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & "Acc_SJE_ChequeNo,Acc_SJE_ChequeDate,Acc_SJE_IFSCCode,Acc_SJE_BankName,Acc_SJE_BranchName,"
    '            End If

    '            sSql = sSql & "Acc_SJE_CreatedBy,Acc_SJE_CreatedOn,Acc_SJE_YearID,Acc_SJE_CompID,"
    '            sSql = sSql & "Acc_SJE_Status,Acc_SJE_Operation,Acc_SJE_IPAddress,Acc_SJE_BillCreatedDate)"
    '            sSql = sSql & "Values(" & iMax & ",'" & objJE.sAcc_JE_TransactionNo & "'," & objJE.iAcc_JE_Party & "," & objJE.iAcc_JE_Location & ","
    '            sSql = sSql & "" & objJE.iAcc_JE_BillType & ",'" & objGen.SafeSQL(objJE.sAcc_JE_BillNo) & "'," & objGen.FormatDtForRDBMS(objJE.dAcc_JE_BillDate, "I") & "," & objJE.dAcc_JE_BillAmount & ","

    '            If iPaymentType = 1 Then
    '                sSql = sSql & "" & objJE.dAcc_JE_AdvanceAmount & ",'" & objGen.SafeSQL(objJE.sAcc_JE_AdvanceNaration) & "'," & objJE.dAcc_JE_BalanceAmount & ","
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & "" & objJE.dAcc_JE_NetAmount & ",'" & objGen.SafeSQL(objJE.sAcc_JE_PaymentNarration) & "',"
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & "'" & objJE.sAcc_JE_ChequeNo & "'," & objGen.FormatDtForRDBMS(objJE.dAcc_JE_ChequeDate, "I") & ","
    '                sSql = sSql & "'" & objGen.SafeSQL(objJE.sAcc_JE_IFSCCode) & "','" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "','" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "',"
    '            End If

    '            sSql = sSql & "" & objJE.iAcc_JE_CreatedBy & ",GetDate()," & objJE.iAcc_JE_YearID & "," & iCompID & ","
    '            sSql = sSql & "'" & objJE.sAcc_JE_Status & "','" & objJE.sAcc_JE_Operation & "','" & objJE.sAcc_JE_IPAddress & "',GetDate())"
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return iMax
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadSubGLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_head=3 order by gl_AccHead"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeletePaymentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete from Acc_Transactions_Details where ATD_ID = " & iTransactionID & " and Atd_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetTransactionsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransType As Integer, ByVal iTransactionID As Integer)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from Acc_Transactions_Details where ATD_ID =" & iTransactionID & " and ATD_TrTYpe =" & iTransType & " and ATD_CompID =" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetChartOfAccountHead(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccHead As Integer = 0
        Try
            sSql = "Select gl_AccHead from Chart_of_Accounts where gl_id =" & iGL & " and gl_CompID =" & iCompID & ""
            iAccHead = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iAccHead
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetParent(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSubGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iParent As Integer = 0
        Try
            sSql = "Select gl_Parent from Chart_of_Accounts where gl_id =" & iSubGL & " and gl_CompID =" & iCompID & ""
            iParent = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Sub UpdateJEMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sStatus As String, ByVal iUserID As Integer, ByVal iPS As Integer, ByVal sIPAddress As String)
    '    Dim sSql As String = ""
    '    Dim dtPurchase As New DataTable
    '    Dim dtSales As New DataTable
    '    Try
    '        If iPS = 0 Then 'Purchase
    '            sSql = "" : sSql = "Select * From Acc_Purchase_JE_Master Where ACC_PJE_Status='" & sSelectedStatus & "' And ACC_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " "
    '            dtPurchase = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '            If dtPurchase.Rows.Count > 0 Then
    '                For i = 0 To dtPurchase.Rows.Count - 1
    '                    sSql = "" : sSql = "Update Acc_Purchase_JE_Master Set Acc_PJE_IPAddress='" & sIPAddress & "',"
    '                    If sStatus = "W" Then
    '                        sSql = sSql & " Acc_PJE_Status='A',Acc_PJE_ApprovedBy= " & iUserID & ",Acc_PJE_ApprovedOn=GetDate()"
    '                    ElseIf sStatus = "D" Then
    '                        sSql = sSql & " Acc_PJE_Status='D',Acc_PJE_DeletedBy= " & iUserID & ",Acc_PJE_DeletedOn=GetDate()"
    '                    ElseIf sStatus = "A" Then
    '                        sSql = sSql & " Acc_PJE_Status='A' "
    '                    End If
    '                    sSql = sSql & " Where ACC_PJE_ID=" & iPS & " And ACC_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " And Acc_PJE_ID = " & dtPurchase.Rows(i)("Acc_PJE_ID") & ""
    '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '                Next
    '            End If
    '        ElseIf iPS = 1 Then 'Sales
    '            sSql = "" : sSql = "Select * From Acc_Sales_JE_Master Where ACC_SJE_Status='" & sSelectedStatus & "' And ACC_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " "
    '            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '            If dtSales.Rows.Count > 0 Then
    '                For j = 0 To dtSales.Rows.Count - 1
    '                    sSql = "" : sSql = "Update Acc_Sales_JE_Master Set Acc_SJE_IPAddress='" & sIPAddress & "',"
    '                    If sStatus = "W" Then
    '                        sSql = sSql & " Acc_SJE_Status='A',Acc_SJE_ApprovedBy= " & iUserID & ",Acc_SJE_ApprovedOn=GetDate()"
    '                    ElseIf sStatus = "D" Then
    '                        sSql = sSql & " Acc_SJE_Status='D',Acc_SJE_DeletedBy= " & iUserID & ",Acc_SJE_DeletedOn=GetDate()"
    '                    ElseIf sStatus = "A" Then
    '                        sSql = sSql & " Acc_SJE_Status='A' "
    '                    End If
    '                    sSql = sSql & " Where ACC_SJE_ID=" & iPS & " And ACC_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " And Acc_SJE_ID = " & dtPurchase.Rows(j)("Acc_SJE_ID") & ""
    '                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    'Preethi Changes
    Public Sub UpdateJEMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sStatus As String, ByVal iUserID As Integer, ByVal iPS As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            If iPS = 0 Then 'Purchase
                sSql = "" : sSql = "Select * From Acc_Purchase_JE_Master Where ACC_PJE_Status='" & sSelectedStatus & "' And ACC_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " "
                dtPurchase = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                If dtPurchase.Rows.Count > 0 Then
                    For i = 0 To dtPurchase.Rows.Count - 1
                        sSql = "" : sSql = "Update Acc_Purchase_JE_Master Set Acc_PJE_IPAddress='" & sIPAddress & "',"
                        If sStatus = "W" Then
                            sSql = sSql & " Acc_PJE_Status='A',Acc_PJE_ApprovedBy= " & iUserID & ",Acc_PJE_ApprovedOn=GetDate()"
                        ElseIf sStatus = "D" Then
                            sSql = sSql & " Acc_PJE_Status='D',Acc_PJE_DeletedBy= " & iUserID & ",Acc_PJE_DeletedOn=GetDate()"
                        ElseIf sStatus = "A" Then
                            sSql = sSql & " Acc_PJE_Status='A' "
                        End If
                        sSql = sSql & " Where ACC_PJE_ID=" & iPS & " And ACC_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " And Acc_PJE_ID = " & dtPurchase.Rows(i)("Acc_PJE_ID") & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Next
                End If
            ElseIf iPS = 1 Then 'Sales
                sSql = "" : sSql = "Select * From Acc_Sales_JE_Master Where ACC_SJE_Status='" & sSelectedStatus & "' And ACC_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " "
                dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                If dtSales.Rows.Count > 0 Then
                    For j = 0 To dtSales.Rows.Count - 1
                        sSql = "" : sSql = "Update Acc_Sales_JE_Master Set Acc_SJE_IPAddress='" & sIPAddress & "',"
                        If sStatus = "W" Then
                            sSql = sSql & " Acc_SJE_Status='A',Acc_SJE_ApprovedBy= " & iUserID & ",Acc_SJE_ApprovedOn=GetDate()"
                        ElseIf sStatus = "D" Then
                            sSql = sSql & " Acc_SJE_Status='D',Acc_SJE_DeletedBy= " & iUserID & ",Acc_SJE_DeletedOn=GetDate()"
                        ElseIf sStatus = "A" Then
                            sSql = sSql & " Acc_SJE_Status='A' "
                        End If
                        sSql = sSql & " Where ACC_SJE_ID=" & iPS & " And ACC_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " And Acc_SJE_ID = " & dtPurchase.Rows(j)("Acc_SJE_ID") & ""
                        If sType = "CS" Then
                            sSql = sSql & " And Acc_SJE_Type='CS'"
                        ElseIf sType = "SR" Then
                            sSql = sSql & " And Acc_SJE_Type='SR'"
                        Else
                            sSql = sSql & " And Acc_SJE_Type='SI'"
                        End If
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckLedgerTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iPS As Integer)
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim iMaxID As Integer
        Dim iAccHead, iHeadID, iGLID As Integer
        Dim dt As New DataTable
        Dim iID As Integer

        Dim dOpenDebit As Double : Dim dOpenCredit As Double
        Dim dTransDebit As Double : Dim dTransCredit As Double
        Dim dCloseDebit As Double : Dim dCloseCredit As Double
        Dim dtDetails As New DataTable
        Dim iTrAccHead, iTrHead, iTrGLID As Integer
        Dim dPreviousTransDebit, dTotalTransDebit As Double : Dim dPreviousTransCredit, dTotalTransCredit As Double

        Dim dtValues As New DataTable
        Try
            If iPS = "0" Then   'Purchase
                sSql = "" : sSql = "Select * From Chart_OF_Accounts A 
                                Left Join Acc_Ledger_Masters B On B.ALM_GL = A.GL_ID
                                Where A.gl_Head in (2,3) And A.gl_CompID=" & iCompID & " And A.GL_ID  Not In (Select ALM_GL From Acc_Ledger_Masters Where ALM_CompID=" & iCompID & ")"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        iAccHead = dt.Rows(i)("GL_AccHead")
                        iHeadID = dt.Rows(i)("GL_Head")
                        iGLID = dt.Rows(i)("GL_ID")

                        iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ALM_ID)+1,1) from Acc_Ledger_Masters")
                        sSql = "" : sSql = "Insert Into Acc_Ledger_Masters(ALM_ID,ALM_AccountType,ALM_Head,ALM_GL,ALM_OBDebit,ALM_OBCredit,ALM_TrDebit,ALM_TrCredit,ALM_CloseDebit,ALM_CloseCredit,ALM_Year,ALM_CompID,ALM_Createdby,ALM_CreatedOn) "
                        sSql = sSql & " Values(" & iMaxID & "," & iAccHead & "," & iHeadID & "," & iGLID & ",0,0,0,0,0,0," & iYearID & "," & iCompID & "," & iUserID & ",GetDate() ) "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Next
                End If

                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_TrType=5 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For j = 0 To dtDetails.Rows.Count - 1

                        iTrAccHead = dtDetails.Rows(j)("ATD_Head")
                        If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
                            iTrGLID = dtDetails.Rows(j)("ATD_SubGL")
                        Else
                            iTrGLID = dtDetails.Rows(j)("ATD_GL")
                        End If
                        iTrHead = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Gl_Head From Chart_Of_Accounts Where GL_ID=" & iTrGLID & " And Gl_AccHead=" & iTrAccHead & " And GL_CompID=" & iCompID & " ")

                        If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
                            sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                            sSql = sSql & " From Acc_Transactions_Details A"
                            sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_SUBGL"
                            sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_SUBGL"
                            sSql = sSql & " Where A.ATD_TrType=5 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
                            dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                        Else
                            sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                            sSql = sSql & " From Acc_Transactions_Details A"
                            sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
                            sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
                            sSql = sSql & " Where A.ATD_TrType=5 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
                            dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                        End If

                        sSql = "" : sSql = "Select * From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
                        bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)

                        If bCheck = True Then
                            iID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select ALM_ID From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " ")

                            If dtValues.Rows.Count > 0 Then

                                If IsDBNull(dtValues.Rows(0)("Opn_DebitAmt")) = False Then
                                    dOpenDebit = dtValues.Rows(0)("Opn_DebitAmt")
                                Else
                                    dOpenDebit = 0
                                End If
                                If IsDBNull(dtValues.Rows(0)("Opn_CreditAmount")) = False Then
                                    dOpenCredit = dtValues.Rows(0)("Opn_CreditAmount")
                                Else
                                    dOpenCredit = 0
                                End If

                                dTransDebit = dtValues.Rows(0)("ATD_Debit")
                                dTransCredit = dtValues.Rows(0)("ATD_Credit")

                                dPreviousTransDebit = dtValues.Rows(0)("ALM_TrDebit")
                                dTotalTransDebit = dPreviousTransDebit + dTransDebit

                                dPreviousTransCredit = dtValues.Rows(0)("ALM_TrCredit")
                                dTotalTransCredit = dPreviousTransCredit + dTransCredit

                                dCloseDebit = dOpenDebit + dTotalTransDebit
                                dCloseCredit = dOpenCredit + dTotalTransCredit

                                sSql = "" : sSql = "Update Acc_Ledger_Masters Set ALM_OBDebit=" & dOpenDebit & ",ALM_OBCredit=" & dOpenCredit & ",ALM_TrDebit=" & dTotalTransDebit & ",ALM_TrCredit=" & dTotalTransCredit & ",ALM_CloseDebit=" & dCloseDebit & ",ALM_CloseCredit=" & dCloseCredit & " "
                                sSql = sSql & " Where ALM_ID =" & iID & " And ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
                                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                dOpenDebit = 0 : dOpenCredit = 0 : dTransDebit = 0 : dTransCredit = 0 : dPreviousTransDebit = 0 : dTotalTransDebit = 0
                                dPreviousTransCredit = 0 : dTotalTransCredit = 0 : dCloseDebit = 0 : dCloseCredit = 0
                            End If
                        End If

                    Next
                End If


            ElseIf iPS = "1" Then   'Sales
                sSql = "" : sSql = "Select * From Chart_OF_Accounts A 
                                Left Join Acc_Ledger_Masters B On B.ALM_GL = A.GL_ID
                                Where A.gl_Head in (2,3) And A.gl_CompID=" & iCompID & " And A.GL_ID  Not In (Select ALM_GL From Acc_Ledger_Masters Where ALM_CompID=" & iCompID & ")"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        iAccHead = dt.Rows(i)("GL_AccHead")
                        iHeadID = dt.Rows(i)("GL_Head")
                        iGLID = dt.Rows(i)("GL_ID")

                        iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ALM_ID)+1,1) from Acc_Ledger_Masters")
                        sSql = "" : sSql = "Insert Into Acc_Ledger_Masters(ALM_ID,ALM_AccountType,ALM_Head,ALM_GL,ALM_OBDebit,ALM_OBCredit,ALM_TrDebit,ALM_TrCredit,ALM_CloseDebit,ALM_CloseCredit,ALM_Year,ALM_CompID,ALM_Createdby,ALM_CreatedOn) "
                        sSql = sSql & " Values(" & iMaxID & "," & iAccHead & "," & iHeadID & "," & iGLID & ",0,0,0,0,0,0," & iYearID & "," & iCompID & "," & iUserID & ",GetDate() ) "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Next
                End If

                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_TrType=6 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For j = 0 To dtDetails.Rows.Count - 1

                        iTrAccHead = dtDetails.Rows(j)("ATD_Head")
                        If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
                            iTrGLID = dtDetails.Rows(j)("ATD_SubGL")
                        Else
                            iTrGLID = dtDetails.Rows(j)("ATD_GL")
                        End If
                        iTrHead = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Gl_Head From Chart_Of_Accounts Where GL_ID=" & iTrGLID & " And Gl_AccHead=" & iTrAccHead & " And GL_CompID=" & iCompID & " ")

                        If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
                            sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                            sSql = sSql & " From Acc_Transactions_Details A"
                            sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_SUBGL"
                            sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_SUBGL"
                            sSql = sSql & " Where A.ATD_TrType=6 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
                            dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                        Else
                            sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                            sSql = sSql & " From Acc_Transactions_Details A"
                            sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
                            sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
                            sSql = sSql & " Where A.ATD_TrType=6 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
                            dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                        End If

                        sSql = "" : sSql = "Select * From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
                        bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)

                        If bCheck = True Then
                            iID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select ALM_ID From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " ")

                            If dtValues.Rows.Count > 0 Then

                                If IsDBNull(dtValues.Rows(0)("Opn_DebitAmt")) = False Then
                                    dOpenDebit = dtValues.Rows(0)("Opn_DebitAmt")
                                Else
                                    dOpenDebit = 0
                                End If
                                If IsDBNull(dtValues.Rows(0)("Opn_CreditAmount")) = False Then
                                    dOpenCredit = dtValues.Rows(0)("Opn_CreditAmount")
                                Else
                                    dOpenCredit = 0
                                End If

                                dTransDebit = dtValues.Rows(0)("ATD_Debit")
                                dTransCredit = dtValues.Rows(0)("ATD_Credit")

                                dPreviousTransDebit = dtValues.Rows(0)("ALM_TrDebit")
                                dTotalTransDebit = dPreviousTransDebit + dTransDebit

                                dPreviousTransCredit = dtValues.Rows(0)("ALM_TrCredit")
                                dTotalTransCredit = dPreviousTransCredit + dTransCredit

                                dCloseDebit = dOpenDebit + dTotalTransDebit
                                dCloseCredit = dOpenCredit + dTotalTransCredit

                                sSql = "" : sSql = "Update Acc_Ledger_Masters Set ALM_OBDebit=" & dOpenDebit & ",ALM_OBCredit=" & dOpenCredit & ",ALM_TrDebit=" & dTotalTransDebit & ",ALM_TrCredit=" & dTotalTransCredit & ",ALM_CloseDebit=" & dCloseDebit & ",ALM_CloseCredit=" & dCloseCredit & " "
                                sSql = sSql & " Where ALM_ID =" & iID & " And ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
                                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                dOpenDebit = 0 : dOpenCredit = 0 : dTransDebit = 0 : dTransCredit = 0 : dPreviousTransDebit = 0 : dTotalTransDebit = 0
                                dPreviousTransCredit = 0 : dTotalTransCredit = 0 : dCloseDebit = 0 : dCloseCredit = 0
                            End If
                        End If

                    Next
                End If

            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function CheckLedgerTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)
    '    Dim sSql As String = ""
    '    Dim bCheck As Boolean
    '    Dim iMaxID As Integer
    '    Dim iAccHead, iHeadID, iGLID As Integer
    '    Dim dt As New DataTable
    '    Dim iID As Integer

    '    Dim dOpenDebit As Double : Dim dOpenCredit As Double
    '    Dim dTransDebit As Double : Dim dTransCredit As Double
    '    Dim dCloseDebit As Double : Dim dCloseCredit As Double
    '    Dim dtDetails As New DataTable
    '    Dim iTrAccHead, iTrHead, iTrGLID As Integer
    '    Dim dPreviousTransDebit, dTotalTransDebit As Double : Dim dPreviousTransCredit, dTotalTransCredit As Double

    '    Dim dtValues As New DataTable
    '    Dim dATDDebit, dATDCredit As Double
    '    Dim dOpenDebitTotal, dOpenCreditTotal As Double
    '    Try

    '        sSql = "" : sSql = "Select * From Chart_OF_Accounts A 
    '                            Left Join Acc_Ledger_Masters B On B.ALM_GL = A.GL_ID
    '                            Where A.gl_Head in (2,3) And A.gl_CompID=" & iCompID & " And A.GL_ID  Not In (Select ALM_GL From Acc_Ledger_Masters Where ALM_CompID=" & iCompID & ")"
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                iAccHead = dt.Rows(i)("GL_AccHead")
    '                iHeadID = dt.Rows(i)("GL_Head")
    '                iGLID = dt.Rows(i)("GL_ID")

    '                iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ALM_ID)+1,1) from Acc_Ledger_Masters")
    '                sSql = "" : sSql = "Insert Into Acc_Ledger_Masters(ALM_ID,ALM_AccountType,ALM_Head,ALM_GL,ALM_OBDebit,ALM_OBCredit,ALM_TrDebit,ALM_TrCredit,ALM_CloseDebit,ALM_CloseCredit,ALM_Year,ALM_CompID,ALM_Createdby,ALM_CreatedOn) "
    '                sSql = sSql & " Values(" & iMaxID & "," & iAccHead & "," & iHeadID & "," & iGLID & ",0,0,0,0,0,0," & iYearID & "," & iCompID & "," & iUserID & ",GetDate() ) "
    '                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Next
    '        End If

    '        dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_TrType=5 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
    '        If dtDetails.Rows.Count > 0 Then
    '            For j = 0 To dtDetails.Rows.Count - 1

    '                iTrAccHead = dtDetails.Rows(j)("ATD_Head")
    '                If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
    '                    iTrGLID = dtDetails.Rows(j)("ATD_SubGL")
    '                Else
    '                    iTrGLID = dtDetails.Rows(j)("ATD_GL")
    '                End If
    '                iTrHead = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Gl_Head From Chart_Of_Accounts Where GL_ID=" & iTrGLID & " And Gl_AccHead=" & iTrAccHead & " And GL_CompID=" & iCompID & " ")

    '                If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
    '                    sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
    '                    sSql = sSql & " From Acc_Transactions_Details A"
    '                    sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_SUBGL"
    '                    sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_SUBGL"
    '                    sSql = sSql & " Where A.ATD_TrType=5 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
    '                    dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '                Else
    '                    sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
    '                    sSql = sSql & " From Acc_Transactions_Details A"
    '                    sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
    '                    sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
    '                    sSql = sSql & " Where A.ATD_TrType=5 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
    '                    dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '                End If

    '                sSql = "" : sSql = "Select * From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
    '                bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)

    '                If bCheck = True Then
    '                    iID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select ALM_ID From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " ")

    '                    If dtValues.Rows.Count > 0 Then

    '                        For i = 0 To dtValues.Rows.Count - 1
    '                            If IsDBNull(dtValues.Rows(i)("Opn_DebitAmt")) = False Then
    '                                dOpenDebit = dtValues.Rows(i)("Opn_DebitAmt")
    '                                dOpenDebitTotal = dOpenDebitTotal + dOpenDebit
    '                            Else
    '                                dOpenDebit = 0
    '                                dOpenDebitTotal = dOpenDebitTotal + dOpenDebit
    '                            End If
    '                            If IsDBNull(dtValues.Rows(i)("Opn_CreditAmount")) = False Then
    '                                dOpenCredit = dtValues.Rows(i)("Opn_CreditAmount")
    '                                dOpenCreditTotal = dOpenCreditTotal + dOpenCredit
    '                            Else
    '                                dOpenCredit = 0
    '                                dOpenCreditTotal = dOpenCreditTotal + dOpenCredit
    '                            End If

    '                            dATDDebit = dtValues.Rows(i)("ATD_Debit")
    '                            dTransDebit = dTransDebit + dATDDebit

    '                            dATDCredit = dtValues.Rows(i)("ATD_Credit")
    '                            dTransCredit = dTransCredit + dATDCredit

    '                            dPreviousTransDebit = dtValues.Rows(i)("ALM_TrDebit")
    '                            dTotalTransDebit = dTotalTransDebit + dPreviousTransDebit

    '                            dPreviousTransCredit = dtValues.Rows(i)("ALM_TrCredit")
    '                            dTotalTransCredit = dTotalTransCredit + dPreviousTransCredit

    '                            dCloseDebit = dOpenDebitTotal + dTotalTransDebit
    '                            dCloseCredit = dOpenCreditTotal + dTotalTransCredit

    '                            sSql = "" : sSql = "Update Acc_Ledger_Masters Set ALM_OBDebit=" & dOpenDebit & ",ALM_OBCredit=" & dOpenCredit & ",ALM_TrDebit=" & dTotalTransDebit & ",ALM_TrCredit=" & dTotalTransCredit & ",ALM_CloseDebit=" & dCloseDebit & ",ALM_CloseCredit=" & dCloseCredit & " "
    '                            sSql = sSql & " Where ALM_ID =" & iID & " And ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
    '                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

    '                            dOpenDebit = 0 : dOpenCredit = 0 : dTransDebit = 0 : dTransCredit = 0 : dPreviousTransDebit = 0 : dTotalTransDebit = 0
    '                            dPreviousTransCredit = 0 : dTotalTransCredit = 0 : dCloseDebit = 0 : dCloseCredit = 0
    '                        Next

    '                    End If
    '                End If

    '            Next
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function WriteGLTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasID As Integer, ByVal iyearID As Integer, ByVal iUserID As Integer, ByVal iPS As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMaxID As Integer
        Try
            If iPS = 0 Then 'Purchase
                sSql = "" : sSql = "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=5 And ATD_BillID=" & iMasID & " And ATD_YearID=" & iyearID & " And ATD_CompID=" & iCompID & " "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ATD_ID)+1,1) from GL_Transactions_Details")
                        sSql = "" : sSql = "Insert Into GL_Transactions_Details (ATD_ID,ATD_TransactionDate,ATD_TrType,ATD_BillId,ATD_PaymentType,ATD_Head,ATD_GL,ATD_SubGL,ATD_DbOrCr,ATD_Debit,ATD_Credit,ATD_CreatedBy,ATD_CreatedOn,ATD_ApprovedBy,ATD_ApprovedOn,ATD_Status,ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress,ATD_ZoneID,ATD_RegionID,ATD_AreaID,ATD_BranchID)"
                        sSql = sSql & "Values(" & iMaxID & ",'" & objGen.FormatDtForRDBMS(dt.Rows(i)("ATD_TransactionDate"), "CT") & "'," & dt.Rows(i)("ATD_TrType") & "," & dt.Rows(i)("ATD_BillId") & "," & dt.Rows(i)("ATD_PaymentType") & "," & dt.Rows(i)("ATD_Head") & "," & dt.Rows(i)("ATD_GL") & "," & dt.Rows(i)("ATD_SubGL") & "," & dt.Rows(i)("ATD_DbOrCr") & "," & dt.Rows(i)("ATD_Debit") & "," & dt.Rows(i)("ATD_Credit") & "," & iUserID & ",'" & objGen.FormatDtForRDBMS(dt.Rows(i)("ATD_CreatedOn"), "CT") & "'," & iUserID & ",GetDate(),'" & dt.Rows(i)("ATD_Status") & "'," & dt.Rows(i)("ATD_YearID") & "," & dt.Rows(i)("ATD_CompID") & ",'" & dt.Rows(i)("ATD_Operation") & "','" & dt.Rows(i)("ATD_IPAddress") & "'," & dt.Rows(i)("ATD_ZoneID") & "," & dt.Rows(i)("ATD_RegionID") & "," & dt.Rows(i)("ATD_AreaID") & "," & dt.Rows(i)("ATD_BranchID") & ")"
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Next
                End If
            ElseIf iPS = 1 Then 'Sales
                sSql = "" : sSql = "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=6 And ATD_BillID=" & iMasID & " And ATD_YearID=" & iyearID & " And ATD_CompID=" & iCompID & " "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ATD_ID)+1,1) from GL_Transactions_Details")
                        sSql = "" : sSql = "Insert Into GL_Transactions_Details (ATD_ID,ATD_TransactionDate,ATD_TrType,ATD_BillId,ATD_PaymentType,ATD_Head,ATD_GL,ATD_SubGL,ATD_DbOrCr,ATD_Debit,ATD_Credit,ATD_CreatedBy,ATD_CreatedOn,ATD_ApprovedBy,ATD_ApprovedOn,ATD_Status,ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress,ATD_ZoneID,ATD_RegionID,ATD_AreaID,ATD_BranchID)"
                        sSql = sSql & "Values(" & iMaxID & ",'" & objGen.FormatDtForRDBMS(dt.Rows(i)("ATD_TransactionDate"), "CT") & "'," & dt.Rows(i)("ATD_TrType") & "," & dt.Rows(i)("ATD_BillId") & "," & dt.Rows(i)("ATD_PaymentType") & "," & dt.Rows(i)("ATD_Head") & "," & dt.Rows(i)("ATD_GL") & "," & dt.Rows(i)("ATD_SubGL") & "," & dt.Rows(i)("ATD_DbOrCr") & "," & dt.Rows(i)("ATD_Debit") & "," & dt.Rows(i)("ATD_Credit") & "," & iUserID & ",'" & objGen.FormatDtForRDBMS(dt.Rows(i)("ATD_CreatedOn"), "CT") & "'," & iUserID & ",GetDate(),'" & dt.Rows(i)("ATD_Status") & "'," & dt.Rows(i)("ATD_YearID") & "," & dt.Rows(i)("ATD_CompID") & ",'" & dt.Rows(i)("ATD_Operation") & "','" & dt.Rows(i)("ATD_IPAddress") & "'," & dt.Rows(i)("ATD_ZoneID") & "," & dt.Rows(i)("ATD_RegionID") & "," & dt.Rows(i)("ATD_AreaID") & "," & dt.Rows(i)("ATD_BranchID") & ")"
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Next
                End If
            End If


        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
