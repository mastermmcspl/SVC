Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsFolders
    Dim objDb As New DBHelper
    Dim objclsGRACeGeneral As New clsFASGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions

    Private iFolId As Integer
    Private iFolCab As Integer
    Private sFolName As String
    Private sFolNotes As String
    Private iFolCrby As Integer
    Private sFolStatus As String
    Private iFolPagecount As Integer
    Private sFolOperation As String
    Private iFolOppby As Integer
    Public Property iFol_Id() As Integer
        Get
            Return (iFolId)
        End Get
        Set(ByVal Value As Integer)
            iFolId = Value
        End Set
    End Property
    Public Property iFol_Cab() As Integer
        Get
            Return (iFolCab)
        End Get
        Set(ByVal Value As Integer)
            iFolCab = Value
        End Set
    End Property
    Public Property sFol_Name() As String
        Get
            Return (sFolName)
        End Get
        Set(ByVal Value As String)
            sFolName = Value
        End Set
    End Property
    Public Property sFol_Notes() As String
        Get
            Return (sFolNotes)
        End Get
        Set(ByVal Value As String)
            sFolNotes = Value
        End Set
    End Property
    Public Property iFol_Crby() As Integer
        Get
            Return (iFolCrby)
        End Get
        Set(ByVal Value As Integer)
            iFolCrby = Value
        End Set
    End Property
    Public Property sFol_Status() As String
        Get
            Return (sFolStatus)
        End Get
        Set(ByVal Value As String)
            sFolStatus = Value
        End Set
    End Property
    Public Property iFol_Pagecount() As Integer
        Get
            Return (iFolPagecount)
        End Get
        Set(ByVal Value As Integer)
            iFolPagecount = Value
        End Set
    End Property
    Public Property sFol_Operation() As String
        Get
            Return (sFolOperation)
        End Get
        Set(ByVal Value As String)
            sFolOperation = Value
        End Set
    End Property
    Public Property iFol_OppBy() As Integer
        Get
            Return (iFolOppby)
        End Get
        Set(ByVal Value As Integer)
            iFolOppby = Value
        End Set
    End Property
    Public Function LoadSubCab(ByVal sAC As String, ByVal iCBNPARID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Select CBN_NODE,CBN_NAME from edt_cabinet where CBN_PARENT =" & iCBNPARID & " and CBN_DelStatus='A' And CBN_PERMISSION <> '1' order by cbn_name"
            Return objDb.SQLExecuteDataSet(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExeFolders(ByVal sAC As String, ByVal iFol_cab As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Select FOL_FOLID,FOL_CABINET, FOL_NAME from EDT_FOLDER where FOL_CABINET =" & iFol_cab & ""
            Return objDb.SQLExecuteDataSet(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFolderDetails(ByVal sAC As String, ByVal iFolId As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select fol_folid, FOL_NAME, FOL_NOTES from edt_FOLDER where FOL_FOLID=" & iFolId & ""
            Return objDb.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFolders(ByVal sAC As String, ByVal sSearchIndex As String, ByVal iSubCBN_Id As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1, iID As Integer
        Dim dRow As DataRow
        Dim sSql As String, aSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dsFol As DataSet

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("FOL_FOLID")
        dtDisplay.Columns.Add("FOL_NAME")
        dtDisplay.Columns.Add("CBN_Name")
        dtDisplay.Columns.Add("PGE_DETAILS_ID")
        dtDisplay.Columns.Add("FOL_CRON")
        dtDisplay.Columns.Add("FOL_Status")
        dtDisplay.Columns.Add("PGE_CABINET")
        dtDisplay.Columns.Add("PGE_FOLDER")
        Try
            sSql = "Select FOL_FOLID,FOL_NAME,FOL_CABINET,FOL_CRON,FOL_STATUS,CBN_Name,CBN_NODE,PGE_DETAILS_ID,PGE_CABINET,PGE_FOLDER from EDT_FOLDER"
            sSql = sSql & " Left Join edt_cabinet  On FOL_CABINET=CBN_NODE"
            sSql = sSql & " Left Join edt_page On PGE_BASENAME=CBN_NODE"
            If iSubCBN_Id > 0 Or sSearchIndex < 3 Then
                sSql = sSql & " where"
            End If
            If iSubCBN_Id > 0 Then
                sSql = sSql & " FOL_CABINET='" & iSubCBN_Id & "' and"
            End If
            If sSearchIndex = 0 Then
                sSql = sSql & " FOL_STATUS = 'A'"
            ElseIf sSearchIndex = 1 Then
                sSql = sSql & " FOL_STATUS = 'D'"
            ElseIf sSearchIndex = 2 Then
                sSql = sSql & " FOL_STATUS = 'W'"
            End If
            sSql = sSql & " order by FOL_NAME"
            dr = objDb.SQLDataReader(sAC, sSql)
            dsFol = objDb.SQLExecuteDataSet(sAC, sSql)
            If (dsFol.Tables(0).Rows.Count > 0) Then
                For Each dRow In dsFol.Tables(0).Rows
                    If dr.HasRows Then
                        While dr.Read
                            aSql = "Select Count(distinct(PGE_Details_ID)) from edt_page where PGE_Folder='" & dr("Fol_FolId") & "'"
                            iID = objDb.SQLExecuteScalarInt(sAC, aSql)
                            dRow = dtDisplay.NewRow
                            dRow("SrNo") = i
                            dRow("FOL_FOLID") = dr("FOL_FOLID")
                            If IsDBNull(dr("FOL_NAME")) = False Then
                                dRow("FOL_NAME") = dr("FOL_NAME")
                            End If
                            If IsDBNull(dr("CBN_Name")) = False Then
                                dRow("CBN_Name") = dr("CBN_Name")
                            End If
                            If iID > 0 Then
                                dRow("PGE_DETAILS_ID") = iID
                            Else
                                dRow("PGE_DETAILS_ID") = "0"
                            End If
                            If IsDBNull(dr("FOL_CRON")) = False Then
                                dRow("FOL_CRON") = objclsGRACeGeneral.FormatDtForRDBMS(dr("FOL_CRON"), "F")
                            End If
                            If IsDBNull(dr("PGE_CABINET")) = False Then
                                dRow("PGE_CABINET") = dr("CBN_NODE")
                            End If
                            If IsDBNull(dr("PGE_FOLDER")) = False Then
                                dRow("PGE_FOLDER") = dr("FOL_FOLID")
                            End If
                            If IsDBNull(dr("FOL_STATUS")) = False Then
                                If dr("FOL_STATUS") = "A" Then
                                    dRow("FOL_STATUS") = "Activated"
                                ElseIf dr("FOL_STATUS") = "D" Then
                                    dRow("FOL_STATUS") = "De-Activated"
                                ElseIf dr("FOL_STATUS") = "W" Then
                                    dRow("FOL_STATUS") = "Waiting for Approval"
                                End If
                            End If
                            i = i + 1
                            dtDisplay.Rows.Add(dRow)
                        End While
                    End If
                Next
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStatus(ByVal sAC As String, ByVal sStatus As String, ByVal ifol_id As String, ByVal sDStatus As String)
        Dim sSql As String
        Try
            sSql = "Update edt_Folder set "
            If sStatus = "D" Then
                sSql = sSql & " FOL_STATUS='" & sDStatus & "' "
            ElseIf sStatus = "A" Then
                sSql = sSql & " FOL_STATUS='" & sDStatus & "' "
            ElseIf sStatus = "W" Then
                sSql = sSql & " FOL_STATUS='" & sDStatus & "' "
            End If
            sSql = sSql & " Where FOL_FOLID=' " & ifol_id & "'"
            objDb.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckFoldersName(ByVal sAC As String, ByVal sFolName As String, ByVal iCabID As Integer, ByVal iFolID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select FOL_NAME from edt_folder where FOL_NAME='" & sFolName & "' and FOL_CABINET='" & iCabID & "' and FOL_FOLID<>'" & iFolID & "'  and( FOL_STATUS='A'  or FOL_STATUS='W') "
            Return objDb.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFolderDetails(ByVal sAC As String, ByVal objFoldr As clsFolders) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_FolId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Cabinet", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_Cab
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Name", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objFoldr.sFol_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Notes", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFoldr.sFol_Notes
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_Crby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objFoldr.sFol_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_PageCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_Pagecount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_Operation", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objFoldr.sFol_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FOL_OperationBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFoldr.iFol_OppBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount = iParamCount + 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sAC, "SaveOrUpFolderDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateFolderCount(ByVal sAC As String, ByVal iCBN_NODE As Integer, ByVal iSCBN_NODE As Integer)
        Dim aSql As String
        Try
            'Update folder count to Cabinet
            aSql = "Update edt_cabinet set CBN_FolCount=(select count(Fol_folid) from edt_folder where fol_cabinet in (Select CBN_Node from Edt_Cabinet where CBN_Parent=" & iCBN_NODE & " and (CBN_DelStatus='A' or CBN_DelStatus='W')))where CBN_NODE=' " & iCBN_NODE & "'"
            objDb.SQLExecuteNonQuery(sAC, aSql)

            'Update folder count to Sub-Cabinet
            aSql = "Update edt_cabinet set CBN_FolCount=(select Count(Fol_folid) from edt_folder where fol_cabinet='" & iSCBN_NODE & "' and (Fol_status='A' or Fol_status='W')) where cbn_node='" & iSCBN_NODE & "'"
            objDb.SQLExecuteNonQuery(sAC, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetBaseID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCabinet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From EDT_Page Where PGE_CABINET=" & iCabinet & " And PGE_SUBCABINET=" & iSubCabinet & " And PGE_Folder=" & iFolder & " And PGE_CompID=" & iCompID & " "
            GetBaseID = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetBaseID
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
