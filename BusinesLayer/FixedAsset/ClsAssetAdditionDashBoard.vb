Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class ClsAssetAdditionDashBoard
    Dim objDB As New DBHelper
    Dim objClsFasgnrl As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions
    Private objFAS As New clsFASGeneral
    Public Function LoadAllDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AFAA_ID")
            dt.Columns.Add("AFAA_AssetNo")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetItemCode")
            dt.Columns.Add("AssetRefNo")
            dt.Columns.Add("Supplier")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("Status")


            sSql = "select * from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iACID & " and AFAA_YearID=" & iyearId & ""
            dtDetails = objDB.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("AFAA_ID")) = False Then
                    dRow("AFAA_ID") = dtDetails.Rows(i)("AFAA_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetNo")) = False Then
                    dRow("AFAA_AssetNo") = dtDetails.Rows(i)("AFAA_AssetNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetType")) = False Then
                    dRow("AssetType") = objDB.SQLExecuteScalar(sAC, "select GL_Desc from Chart_Of_Accounts where gl_ID= " & dtDetails.Rows(i)("AFAA_AssetType") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_ItemCode")) = False Then
                    dRow("AssetItemCode") = dtDetails.Rows(i)("AFAA_ItemCode")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetRefNo")) = False Then
                    dRow("AssetRefNo") = dtDetails.Rows(i)("AFAA_AssetRefNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_SupplierName")) = False Then
                    dRow("Supplier") = objDB.SQLExecuteScalar(sAC, "select CSM_Name from CustomerSupplierMaster where CSM_ID= " & dtDetails.Rows(i)("AFAA_SupplierCode") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                    dRow("PurchaseDate") = objFAS.FormatDtForRDBMS(dtDetails.Rows(i)("AFAA_PurchaseDate"), "D")
                End If

                'If IsDBNull(dtDetails.Rows(i)("AFAA_Delflag")) = False Then
                '    If dtDetails.Rows(i)("AFAA_Delflag") = "W" Then
                '        dRow("Status") = "Waiting for Approval"
                '    ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "D" Then
                '        dRow("Status") = "Deleted"
                '    ElseIf (dtDetails.Rows(i)("AFAA_Delflag") = "A") Then
                '        dRow("Status") = "Approved"
                '    End If
                'End If
                If dtDetails.Rows(i)("AFAA_Delflag") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "A" Then
                    dRow("Status") = "Approved"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "X" Then
                    dRow("Status") = "Transaction Deleted"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "Y" Then
                    dRow("Status") = "Recalled for Approval"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "D" Then
                    dRow("Status") = "Transaction Deleted"

                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails1(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sStatus As String) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AFAA_ID")
            dt.Columns.Add("AFAA_AssetNo")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetRefNo")
            dt.Columns.Add("AssetItemCode")
            dt.Columns.Add("Supplier")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("Status")

            sSql = "select * from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iACID & " and AFAA_YearID=" & iyearId & " and AFAA_Delflag='" & sStatus & "'"
            dtDetails = objDB.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("AFAA_ID")) = False Then
                    dRow("AFAA_ID") = dtDetails.Rows(i)("AFAA_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_ItemCode")) = False Then
                    dRow("AssetItemCode") = dtDetails.Rows(i)("AFAA_ItemCode")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetNo")) = False Then
                    dRow("AFAA_AssetNo") = dtDetails.Rows(i)("AFAA_AssetNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetType")) = False Then
                    dRow("AssetType") = objDB.SQLExecuteScalar(sAC, "select GL_Desc from Chart_Of_Accounts where gl_ID= " & dtDetails.Rows(i)("AFAA_AssetType") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetRefNo")) = False Then
                    dRow("AssetRefNo") = dtDetails.Rows(i)("AFAA_AssetRefNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_SupplierName")) = False Then
                    dRow("Supplier") = objDB.SQLExecuteScalar(sAC, "select CSM_Name from CustomerSupplierMaster where CSM_ID= " & dtDetails.Rows(i)("AFAA_SupplierName") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                    dRow("PurchaseDate") = objFAS.FormatDtForRDBMS(dtDetails.Rows(i)("AFAA_PurchaseDate"), "D")
                End If

                'If IsDBNull(dtDetails.Rows(i)("AFAA_Delflag")) = False Then
                '    If dtDetails.Rows(i)("AFAA_Delflag") = "W" Then
                '        dRow("Status") = "Waiting for Approval"
                '    ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "X" Then
                '        dRow("Status") = "Deleted"
                '    ElseIf (dtDetails.Rows(i)("AFAA_Delflag") = "A") Then
                '        dRow("Status") = "Approved"
                '    End If
                'End If
                If dtDetails.Rows(i)("AFAA_Delflag") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "A" Then
                    dRow("Status") = "Approved"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "X" Then
                    dRow("Status") = "Transaction Deleted"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "Y" Then
                    dRow("Status") = "Recalled for Approval"
                ElseIf dtDetails.Rows(i)("AFAA_Delflag") = "D" Then
                    dRow("Status") = "Transaction Deleted"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAssetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_FixedAssetAdditionDel Set AFAA_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & "AFAA_Delflag='A',AFAA_Status ='A',AFAA_ApprovedBy= " & iUserID & ",AFAA_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & "AFAA_Delflag='D',AFAA_Status ='D',AFAA_Deletedby= " & iUserID & ",AFAA_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " AFAA_Delflag='A',AFAA_Status ='A' "
            End If
            sSql = sSql & " Where AFAA_ID = " & iMasId & " and AFAA_CompID=" & iCompID & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select AFAA_Status From Acc_FixedAssetAdditionDel Where AFAA_ID = " & iMasId & " and AFAA_CompID=" & iCompID & " And AFAA_YearID=" & iYearID & " "
            GetStatus = objDB.SQLGetDescription(sNameSpace, sSql)
            Return GetStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("AFAA_ID")
            dt.Columns.Add("AFAA_AssetNo")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetRefNo")
            dt.Columns.Add("Supplier")
            dt.Columns.Add("PurchaseDate")


            sSql = "select * from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iACID & " and AFAA_YearID=" & iyearId & ""
            dtDetails = objDB.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("AFAA_ID")) = False Then
                    dRow("AFAA_ID") = dtDetails.Rows(i)("AFAA_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetNo")) = False Then
                    dRow("AFAA_AssetNo") = dtDetails.Rows(i)("AFAA_AssetNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetType")) = False Then
                    dRow("AssetType") = objDB.SQLExecuteScalar(sAC, "select GL_Desc from Chart_Of_Accounts where gl_ID= " & dtDetails.Rows(i)("AFAA_AssetType") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_AssetRefNo")) = False Then
                    dRow("AssetRefNo") = dtDetails.Rows(i)("AFAA_AssetRefNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_SupplierName")) = False Then
                    dRow("Supplier") = objDB.SQLExecuteScalar(sAC, "select CSM_Name from CustomerSupplierMaster where CSM_ID= " & dtDetails.Rows(i)("AFAA_SupplierName") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("AFAA_PurchaseDate")) = False Then
                    dRow("PurchaseDate") = objFAS.FormatDtForRDBMS(dtDetails.Rows(i)("AFAA_PurchaseDate"), "D")
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
