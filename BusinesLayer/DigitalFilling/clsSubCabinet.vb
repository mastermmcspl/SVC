Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsSubCabinet
    Dim objDb As New DBHelper
    Private objclsGRACeGeneral As New clsFASGeneral
    Private iCBNNODE As Integer
    Private sCBNName As String
    Private iCBNParGrp As Integer
    Private sCBNNote As String
    Private sCBNDelStatus As String
    Private iCBNPARENT As Integer
    Private iCBNUSERID As Integer
    Private sCBNOperation As String
    Private iCBNSCCount As Integer
    Private iCBNFolCount As Integer
    Private iCBNUSERGROUP As Integer
    Private iCBNPERMISSION As Integer

    Public Property iCBN_NODE() As Integer
        Get
            Return (iCBNNODE)
        End Get
        Set(ByVal Value As Integer)
            iCBNNODE = Value
        End Set
    End Property
    Public Property sCBN_Name() As String
        Get
            Return (sCBNName)
        End Get
        Set(ByVal Value As String)
            sCBNName = Value
        End Set
    End Property
    Public Property iCBN_ParGrp() As Integer
        Get
            Return (iCBNParGrp)
        End Get
        Set(ByVal Value As Integer)
            iCBNParGrp = Value
        End Set
    End Property
    Public Property sCBN_Note() As String
        Get
            Return (sCBNNote)
        End Get
        Set(ByVal Value As String)
            sCBNNote = Value
        End Set
    End Property
    Public Property sCBN_Delstatus() As String
        Get
            Return (sCBNDelStatus)
        End Get
        Set(ByVal Value As String)
            sCBNDelStatus = Value
        End Set
    End Property
    Public Property iCBN_PARENT() As Integer
        Get
            Return (iCBNPARENT)
        End Get
        Set(ByVal Value As Integer)
            iCBNPARENT = Value
        End Set
    End Property
    Public Property iCBN_USERID() As Integer
        Get
            Return (iCBNUSERID)
        End Get
        Set(ByVal Value As Integer)
            iCBNUSERID = Value
        End Set
    End Property
    Public Property sCBN_Operation() As String
        Get
            Return (sCBNOperation)
        End Get
        Set(ByVal Value As String)
            sCBNOperation = Value
        End Set
    End Property
    Public Property iCBN_SCCount() As Integer
        Get
            Return (iCBNSCCount)
        End Get
        Set(ByVal Value As Integer)
            iCBNSCCount = Value
        End Set
    End Property
    Public Property iCBN_FolCount() As Integer
        Get
            Return (iCBNFolCount)
        End Get
        Set(ByVal Value As Integer)
            iCBNFolCount = Value
        End Set
    End Property
    Public Property iCBN_USERGROUP() As Integer
        Get
            Return (iCBNUSERGROUP)
        End Get
        Set(ByVal Value As Integer)
            iCBNUSERGROUP = Value
        End Set
    End Property
    Public Property iCBN_PERMISSION() As Integer
        Get
            Return (iCBNPERMISSION)
        End Get
        Set(ByVal Value As Integer)
            iCBNPERMISSION = Value
        End Set
    End Property
    Public Function LoadCabinet(ByVal sNameSpace As String, ByVal sSearch As String) As DataSet
        Dim sSql As String
        Try
            sSql = "Select CBN_NODE,CBN_NAME from edt_cabinet WHERE CBN_PARENT=-1 and cbn_DelStatus='A'  And  CBN_PERMISSION <> '1'  order by cbn_name"
            If sSearch <> "" Then
                sSql = sSql & "and CBN_NAME like '" & sSearch & "%' "
            End If
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubCabGrid(ByVal sNameSpace As String, ByVal iDescID As Integer, ByVal sSearchIndex As String, ByVal iCabinet As Integer) As DataTable
        Dim dtDisplay As New DataTable
        Dim i As Integer = 1
        Dim dRow As DataRow
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader

        dtDisplay.Columns.Add("SrNo")
        dtDisplay.Columns.Add("CBN_NODE")
        dtDisplay.Columns.Add("CBN_NAME")
        dtDisplay.Columns.Add("CBN_NOTE")
        dtDisplay.Columns.Add("CBN_FolCount")
        dtDisplay.Columns.Add("CBN_CRON")
        dtDisplay.Columns.Add("cbn_DelStatus")
        Try
            sSql = "Select CBN_NODE,CBN_NAME,CBN_NOTE,CBN_Parent,CBN_SCCount,CBN_FolCount,CBN_ParGrp,CBN_CRON,cbn_DelStatus "
            sSql = sSql & "from edt_cabinet where"
            If iCabinet > 0 Then
                sSql = sSql & " CBN_Parent='" & iCabinet & "'"
            End If
            If iDescID > 0 Then
                sSql = sSql & " And CBN_NODE=" & iDescID & ""
            End If
            If sSearchIndex = 0 Then
                sSql = sSql & " And cbn_DelStatus = 'A'"
            ElseIf sSearchIndex = 1 Then
                sSql = sSql & " And cbn_DelStatus = 'D'"
            ElseIf sSearchIndex = 2 Then
                sSql = sSql & " And cbn_DelStatus = 'W'"
            End If
            sSql = sSql & " order by CBN_NAME"
            dr = objDb.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dtDisplay.NewRow
                    dRow("SrNo") = i
                    dRow("CBN_NODE") = dr("CBN_NODE")
                    If IsDBNull(dr("CBN_NAME")) = False Then
                        dRow("CBN_NAME") = dr("CBN_NAME")
                    End If
                    If IsDBNull(dr("CBN_NOTE")) = False Then
                        dRow("CBN_NOTE") = dr("CBN_NOTE")
                    End If
                    If IsDBNull(dr("CBN_FolCount")) = False Then
                        dRow("CBN_FolCount") = dr("CBN_FolCount")
                    End If
                    If IsDBNull(dr("CBN_CRON")) = False Then
                        dRow("CBN_CRON") = objclsGRACeGeneral.FormatDtForRDBMS(dr("CBN_CRON"), "F")
                    End If
                    If IsDBNull(dr("cbn_DelStatus")) = False Then
                        If dr("cbn_DelStatus") = "A" Then
                            dRow("cbn_DelStatus") = "Activated"
                        ElseIf dr("cbn_DelStatus") = "D" Then
                            dRow("cbn_DelStatus") = "De-Activated"
                        ElseIf dr("cbn_DelStatus") = "W" Then
                            dRow("cbn_DelStatus") = "Waiting for Approval"
                        End If
                    End If
                    i = i + 1
                    dtDisplay.Rows.Add(dRow)
                End While
            End If
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStatus(ByVal sNameSpace As String, ByVal sStatus As String, ByVal iCBN_NODE As String, ByVal sDStatus As String)
        Dim sSql As String
        Try
            sSql = "Update edt_cabinet set "
            If sStatus = "D" Then
                sSql = sSql & " cbn_DelStatus='" & sDStatus & "' "
            ElseIf sStatus = "A" Then
                sSql = sSql & " cbn_DelStatus='" & sDStatus & "' "
            ElseIf sStatus = "W" Then
                sSql = sSql & "cbn_DelStatus='" & sDStatus & "' "
            End If
            sSql = sSql & " Where CBN_NODE=' " & iCBN_NODE & "'"
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadSubCabinet(ByVal sNameSpace As String, ByVal sSearch As String, ByVal iCBN_NodeId As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Select CBN_NODE,CBN_NAME from edt_cabinet WHERE CBN_PARENT<>1 and cbn_DelStatus='A'"
            If sSearch <> "" Then
                sSql = sSql & " and CBN_Parent= '" & iCBN_NodeId & "' and CBN_NAME like '" & sSearch & "%'"
            End If
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCabUserPer(ByVal sNameSpace As String, ByVal iMasID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Select Usr_ID,Usr_LoginName from Sad_UserDetails Left join SAD_GrpOrLvl_General_Master on Mas_ID=usr_LevelGrp"
            sSql = sSql & " where usr_LevelGrp =" & iMasID & ""
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubCabdetails(ByVal iCBNID As Integer, ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select  CBN_NAME, CBN_ParGrp,CBN_Note from edt_cabinet where CBN_NODE=" & iCBNID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DepartmentId(ByVal sNameSpace As String, ByVal iCBNID As Integer)
        Dim sSql As String
        Try
            sSql = "Select CBN_ParGrp from edt_cabinet where CBN_NODE='" & iCBNID & "'"
            Return objDb.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubCabName(ByVal sAC As String, ByVal sCabName As String, ByVal iCabID As Integer, ByVal iCabNODE As Integer) As Boolean
        Dim sSql As String
        Try
            'Check cabinet name only for that group
            sSql = "Select CBN_Name from edt_cabinet where CBN_Name='" & sCabName & "' and CBN_Node<>'" & iCabNODE & "'  and (CBN_DelStatus='A' or CBN_DelStatus='W') and CBN_Parent='" & iCabID & "'"
            Return objDb.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubCabDetails(ByVal sNameSpace As String, ByVal objCab As clsSubCabinet) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_NODE", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_NODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_NAME", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objCab.sCBN_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_PARENT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_PARENT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_Note", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCab.sCBN_Note
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_USERGROUP", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_USERGROUP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_ParGrp", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_ParGrp
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_USERID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_USERID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_PERMISSION", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_PERMISSION
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cbn_DelStatus", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objCab.sCBN_Delstatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_SCCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_SCCount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CBN_FolCount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_FolCount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cbn_Operation", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objCab.sCBN_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cbn_OperationBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCab.iCBN_USERID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spEdt_CabinetDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateSubCabDetails(ByVal sNameSpace As String, ByVal iCBN_ParId As Integer, ByVal iCBN_NODE As Integer)
        Dim strsql As String, aSql As String
        Try
            'Update Sub cabinet count
            strsql = "Update edt_cabinet set CBN_SCCount=(Select count (CBN_Node) from Edt_Cabinet where CBN_Parent=' " & iCBN_NODE & "' and (CBN_DelStatus='A' or CBN_DelStatus='W')) where CBN_NODE=' " & iCBN_NODE & "'"
            objDb.SQLExecuteNonQuery(sNameSpace, strsql)

            'Update folder count
            aSql = "Update edt_cabinet set CBN_FolCount=(select count(Fol_folid) from edt_folder where fol_cabinet in (Select CBN_Node from Edt_Cabinet where CBN_Parent=" & iCBN_NODE & " and (CBN_DelStatus='A' or CBN_DelStatus='W')))where CBN_NODE=' " & iCBN_NODE & "'"
            objDb.SQLExecuteNonQuery(sNameSpace, aSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
