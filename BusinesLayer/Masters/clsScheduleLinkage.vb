Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsScheduleLinkage
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 0 and "
            sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iCompID & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer, ByVal iParent As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_AccHead = " & iHead & " and "
            sSql = sSql & "gl_head = 1 and gl_CompId =" & iCompID & ""
            If iParent <> 0 Then
                sSql = sSql & " and gl_Parent =" & iParent & ""
            End If
            sSql = sSql & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGeneralLedger(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select gl_ID,gl_Desc From Chart_Of_Accounts Where gl_AccHead = " & iHead & " and gl_head in(2,3) and gl_CompID=" & iCompID & " and gl_delflag ='C' order by gl_ID"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllGeneralLedger(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select gl_ID,gl_Desc From Chart_Of_Accounts Where gl_head in(2,3) and gl_CompID=" & iCompID & " and gl_delflag ='C' and (gl_parent<> 0 and gl_head <> 0) order by gl_ID"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveScheduleLinkageMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal sGeneralLedger As String, ByVal iHead As Integer, ByVal iNote As Integer, ByVal iIPAddress As String)
        Dim sSql As String = ""
        Dim iMaxId As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head =" & iHead & " and SLM_GroupID=" & iGroup & " and "
            sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_GLId=" & iGL & " and SLM_CompID =" & iCompID & "" ' and SLM_YearID =" & iYearID & ""
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                sSql = "" : sSql = "Update Schedule_Linkage_Master set SLM_GLLedger='" & sGeneralLedger & "',SLM_NoteNo =" & iNote & ",SLM_Operation='U',SLM_IPAddress='" & iIPAddress & "' where "
                sSql = sSql & "SLM_Head =" & iHead & " and SLM_GroupID=" & iGroup & " and "
                sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_GLId=" & iGL & " and SLM_CompID =" & iCompID & "" 'and SLM_YearID =" & iYearID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMaxId = objDBL.SQLExecuteScalar(sNameSpace, "Select IsNull(MAX(SLM_ID),0)+1 from Schedule_Linkage_Master")
                sSql = "" : sSql = "Insert into Schedule_Linkage_Master(SLM_ID,SLM_Head,SLM_GroupID,SLM_SubGroupID,"
                sSql = sSql & "SLM_GLId,SLM_GLLedger,SLM_CreatedBy,"
                sSql = sSql & "SLM_CreatedOn,SLM_Status,SLM_CompID,SLM_NoteNo,SLM_Operation,SLM_IPAddress)"
                sSql = sSql & "Values(" & iMaxId & "," & iHead & "," & iGroup & "," & iSubGroup & ","
                sSql = sSql & "" & iGL & ",'" & sGeneralLedger & "'," & iUserID & ","
                sSql = sSql & "GetDate(),'A'," & iCompID & "," & iNote & ",'C','" & iIPAddress & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
            dr.Close()
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSavedInventoryDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer)
        Dim sSql As String = "", sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head = " & iHead & " and "
            sSql = sSql & " SLM_CompID =" & iCompID & " And SLM_Status ='A'" ' and SLM_YearID =" & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sStr = sStr & "," & dt.Rows(i)("SLM_GLLedger")
                Next
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function GetSavedGLS(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer) As String
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim sStr As String = ""
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head =" & iHead & " and "
            sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_CompID =" & iCompID & "" 'and SLM_YearID =" & iYearID & ""
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sStr = dr("SLM_GLLedger")
            End If
            dr.Close()
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetNoteNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer) As String
        Dim sSql As String = "", sStr As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head =" & iHead & "  and "
            sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_CompID =" & iCompID & "" 'and SLM_YearID =" & iYearID & ""
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("SLM_GLLedger")) = False Then
                    If dr("SLM_GLLedger") <> "" Then
                        If IsDBNull(dr("SLM_NoteNo")) = False Then
                            sStr = dr("SLM_NoteNo")
                        Else
                            sStr = ""
                        End If
                    End If
                End If
            End If
            dr.Close()
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSavedGLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer) As DataTable
        Dim sSql As String = "", sStr As String = "", sLedger As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim sArray As Array
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where SLM_Head =" & iHead & " and "
            sSql = sSql & "SLM_SubGroupID =" & iSubGroup & " and SLM_CompID =" & iCompID & "" 'and SLM_YearID =" & iYearID & ""

            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sStr = dr("SLM_GLLedger")
                dr.Close()
            End If

            If sStr <> "" Then
                sArray = sStr.Split(",")
                For i = 0 To sArray.Length - 1
                    If sArray(i) <> "" Then
                        sLedger = sLedger & "," & sArray(i)
                    End If
                Next
                sLedger = sLedger.Remove(0, 1)
                sSql = "Select gl_ID,gl_Desc From Chart_Of_Accounts Where gl_id In(" & sLedger & ") And gl_CompID=" & iCompID & " order by gl_Desc"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteGeneralLedger(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iHead As Integer, ByVal sGeneralLedger As String, ByVal iIPAddress As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "" : sSql = "Update Schedule_Linkage_Master set SLM_GLLedger='" & sGeneralLedger & "',SLM_Operation='D',SLM_IPAddress='" & iIPAddress & "' where "
            'sSql = sSql & "SLM_GLId=" & iGL & " and SLM_CompID =" & iCompID & "" 'and SLM_YearID =" & iYearID & ""

            sSql = "" : sSql = "Select * From Schedule_Linkage_Master where "
            sSql = sSql & "SLM_Head=" & iHead & " And SLM_GroupID=" & iGroup & " And SLM_SubGroupID=" & iSubGroup & " And SLM_GLId=" & iGL & " and SLM_CompID =" & iCompID & "" 'and SLM_YearID =" & iYearID & ""

            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "" : sSql = "Update Schedule_Linkage_Master set SLM_GLLedger='" & sGeneralLedger & "',SLM_Operation='D',SLM_IPAddress='" & iIPAddress & "' where SLM_ID =" & dt.Rows(i)("SLM_ID").ToString() & " and SLM_CompID =" & iCompID & " "
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSavedLinkageDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = "", sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Schedule_Linkage_Master where "
            sSql = sSql & " SLM_CompID =" & iCompID & " And SLM_Status ='A'" ' and SLM_YearID =" & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sStr = sStr & "," & dt.Rows(i)("SLM_GLLedger")
                Next
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try

    End Function
End Class
