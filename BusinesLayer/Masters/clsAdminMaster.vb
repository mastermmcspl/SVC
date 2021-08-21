Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsAdminMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function LoadAdminMasterDesgRoleDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTableName As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mas_ID AS PKID,Mas_Description AS Name From " & sTableName & " where Mas_CompID=" & iAcID & " Order By Mas_Description ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAdminMasterOtherDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & " Order By CMM_Desc ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAdminMasterDesgRoleDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iDescID As Integer, ByVal sTableName As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * From " & sTableName & " Where Mas_id=" & iDescID & " and Mas_CompID= " & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAdminMasterOtherDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iDescID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CMM_Code,CMM_Desc,CMS_Remarks,CMM_DelFlag From Content_Management_Master Where CMM_ID=" & iDescID & " And CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDeleteorNot(ByVal sAc As String, ByVal iAcID As Integer, ByVal sDesc As Object, ByVal sTableName As String, ByVal sCoulmnName As String,
                                     ByVal iMasID As Integer, ByVal sType As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from " & sTableName & " where " & sCoulmnName & "= '" & sDesc & "'"
            If sType = "DESGROLE" Then
                If iMasID > 0 Then
                    sSql = sSql & " And Mas_ID= " & iMasID & " and Mas_Delflag= 'D'"
                End If
            Else
                If iMasID > 0 Then
                    sSql = sSql & " And CMM_ID= " & iMasID & " and CMM_DelFlag= 'D'"
                End If
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckExistingDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sDesc As Object, ByVal sTableName As String, ByVal sCoulmnName As String,
                                         ByVal iMasID As Integer, ByVal sType As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from " & sTableName & " where " & sCoulmnName & "= '" & sDesc & "'"
            If sType = "DESGROLE" Then
                If iMasID > 0 Then
                    sSql = sSql & " And Mas_ID <> " & iMasID & ""
                End If
            Else
                If iMasID > 0 Then
                    sSql = sSql & " And CMM_ID <> " & iMasID & ""
                End If
            End If
            CheckExistingDetails = objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveOrUpdateDtls(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasID As Integer, ByVal sMasCode As String, ByVal sMasDesc As String, ByVal sMasNotes As String, ByVal sTableName As String, ByVal iUserID As Integer, ByVal sIPAddress As String) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iMasID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Code", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = sMasCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Description", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sMasDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Notes", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sMasNotes
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@mas_Createdby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@mas_Updatedby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iAcID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            If sTableName = "SAD_GRPDESGN_GENERAL_MASTER" Then
                Arr = objDBL.ExecuteSPForInsertARR(sAc, "spSAD_GRPDESGN_General_Master", 1, Arr, ObjParam)
            ElseIf sTableName = "SAD_GRPORLVL_GENERAL_MASTER" Then
                Arr = objDBL.ExecuteSPForInsertARR(sAc, "spSAD_GrpOrLvl_General_Master", 1, Arr, ObjParam)
            End If
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateGeneralMasterStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasId As Integer, ByVal sTableName As String, ByVal iUserId As Integer,
                                         ByVal sIPAddress As String, ByVal sStatus As String, ByVal sType As String)
        Dim sSql As String = ""
        Try
            If sType = "DESGROLE" Then
                sSql = "Update " & sTableName & " Set Mas_IPAddress='" & sIPAddress & "',"
                If sStatus = "W" Then
                    sSql = sSql & " Mas_delflag='A',Mas_Status='A',mas_Approvedby= " & iUserId & ",mas_Approvedon=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " Mas_delflag='D',Mas_Status='AD',Mas_DeletedBy= " & iUserId & ",Mas_DeletedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " Mas_delflag='A',Mas_Status='AR',Mas_RecalledBy= " & iUserId & ",Mas_RecalledOn=GetDate()"
                End If
                sSql = sSql & " Where Mas_Id= " & iMasId & ""
            ElseIf sType = "OTHERS" Then
                sSql = "Update Content_Management_Master Set CMM_IPAddress='" & sIPAddress & "',"
                If sStatus = "W" Then
                    sSql = sSql & " CMM_DelFlag='A',CMM_Status='A',CMM_ApprovedBy= " & iUserId & ",CMM_ApprovedOn=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " CMM_DelFlag='D',CMM_Status='AD',CMM_DeletedBy= " & iUserId & ",CMM_DeletedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " CMM_DelFlag='A',CMM_Status='AR',CMM_RecallBy= " & iUserId & ",CMM_RecallOn=GetDate()"
                End If
                sSql = sSql & " Where CMM_ID= " & iMasId & ""
            End If
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadGeneralMasterDESGROLEGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTableName As String, ByVal iStatus As Integer, ByVal sSearchText As String) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescID")
            dt.Columns.Add("DescName")
            dt.Columns.Add("Description")
            dt.Columns.Add("Status")
            sSql = "Select * From " & sTableName & " Where Mas_CompID=" & iAcID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Mas_delflag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Mas_delflag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Mas_delflag='W'" 'Waiting for approval
            End If
            If sSearchText <> "" Then
                sSql = sSql & " And Mas_Description like '" & sSearchText & "%' " '
            End If
            sSql = sSql & " Order By  Mas_Description ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DescID") = ds.Tables(0).Rows(i)("Mas_id")
                dr("DescName") = ds.Tables(0).Rows(i)("Mas_Description")
                dr("Description") = ds.Tables(0).Rows(i)("Mas_Description")
                If IsDBNull(ds.Tables(0).Rows(i)("Mas_delflag")) = False Then
                    If ds.Tables(0).Rows(i)("Mas_delflag") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("Mas_delflag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("Mas_delflag") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGeneralMasterOTHERGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTableName As String, ByVal iStatus As Integer, ByVal sSearchText As String, ByVal iType As Integer) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescID")
            dt.Columns.Add("DescName")
            dt.Columns.Add("Description")
            dt.Columns.Add("Status")
            sSql = "Select * From Acc_General_Master Where MAS_Master=" & iType & " And Mas_CompID=" & iAcID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Mas_DelFlag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Mas_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Mas_DelFlag='W'" 'Waiting for approval
            End If
            If sSearchText <> "" Then
                sSql = sSql & " And Mas_Desc like '" & sSearchText & "%' " '
            End If
            sSql = sSql & " Order By Mas_Desc ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DescID") = ds.Tables(0).Rows(i)("Mas_ID")
                dr("DescName") = ds.Tables(0).Rows(i)("Mas_Desc")
                dr("Description") = ds.Tables(0).Rows(i)("Mas_Desc")
                If IsDBNull(ds.Tables(0).Rows(i)("Mas_DelFlag")) = False Then
                    If ds.Tables(0).Rows(i)("Mas_DelFlag") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("Mas_DelFlag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("Mas_DelFlag") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
