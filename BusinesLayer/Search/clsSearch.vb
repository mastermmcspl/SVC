Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Security.Cryptography
Public Structure strCollationDoc
    Dim iCLD_COLLATENO As Integer
    Dim iCLD_DOCID As Integer
    Dim iCLD_PAGEID As Integer
    Public Property iCOLLATENO() As Integer
        Get
            Return (iCLD_COLLATENO)
        End Get
        Set(ByVal Value As Integer)
            iCLD_COLLATENO = Value
        End Set
    End Property
    Public Property iDOCID() As Integer
        Get
            Return (iCLD_DOCID)
        End Get
        Set(ByVal Value As Integer)
            iCLD_DOCID = Value
        End Set
    End Property
    Public Property iPAGEID() As Integer
        Get
            Return (iCLD_PAGEID)
        End Get
        Set(ByVal Value As Integer)
            iCLD_PAGEID = Value
        End Set
    End Property
End Structure
Public Class clsSearch
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsFASGeneral As New clsFASGeneral
    Dim dtPageX As New DataTable
    Dim dtPage As New DataTable
    Dim Permdt As New DataTable
    Dim dtPerm As New DataTable
    Dim dtGrp As New DataTable
    Dim dtMain As New DataTable
    Public sLocation() As String
    Dim sFolPerm As String = "", sCabPerm As String = "", sCabName As String = "", sDTPerm As String = ""
    Dim iParGrp As Integer, iUsrParGrp As Integer = 0, iUsrType As Integer = 0
    Public Function GetFolderNames() As DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("Id")
            dt.Columns.Add("Fields")
            dt.Columns.Add("SelectedID")
            dt.Columns.Add("SelectedName")
            dRow = dt.NewRow
            dRow("Id") = "CB"
            dRow("Fields") = "Cabinets"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "SC"
            dRow("Fields") = "SubCabinets"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "FD"
            dRow("Fields") = "Folders"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "DE"
            dRow("Fields") = "Date"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "TT"
            dRow("Fields") = "Title"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "KW"
            dRow("Fields") = "Keywords"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "OC"
            dRow("Fields") = "OCRText"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "FT"
            dRow("Fields") = "Format"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "CR"
            dRow("Fields") = "Created by"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "AD"
            dRow("Fields") = "Any Descriptor"
            dt.Rows.Add(dRow)
            dRow = dt.NewRow
            dRow("Id") = "DT"
            dRow("Fields") = "DocumentTypes"
            dt.Rows.Add(dRow)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SetRows(ByVal dt As DataTable, Optional ByVal i As Integer = 24) As DataTable
        Dim iRow As Integer
        Dim dr As DataRow
        Try
            If dt.Rows.Count <> 0 Then
                If dt.Rows.Count - 1 < i Then
                    iRow = dt.Rows.Count - 1
                Else
                    Return dt
                End If
            Else
                iRow = 0
            End If
            For iRow = iRow To i
                dr = dt.NewRow
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Cabinets
    Public Function LoadCabinets(ByVal sAC As String, ByVal LogUsrId As Int16, Optional ByVal iCabID As Integer = 0, Optional ByVal sPerm As String = "VCB") As DataTable
        Dim dt As New DataTable
        Dim sSql As String
        Try
            iUsrType = GetUserType(sAC, LogUsrId)
            iUsrParGrp = GetUserParGrp(sAC, LogUsrId)
            If (iUsrType = 1) Then
                'User Logged is Super User
                If (iCabID = 0) Then
                    sSql = "Select *,CBN_Node As ID,CBN_Parent As CabPar from edt_cabinet where CBN_DelStatus='A' and CBN_Parent=-1 order by CBN_Name"
                    UpdateFolderCount(sAC, sSql)
                Else
                    sSql = "Select *,CBN_Node As ID,CBN_Parent As CabPar from edt_cabinet where CBN_DelStatus='A' and CBN_Parent = " & iCabID & " order by CBN_Name "
                End If
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildPermTable() As DataTable
        Dim PermDt As New DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("Name", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabPar", GetType(Int16))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabNote", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabCrtGrp", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabCrtUsr", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabParGrp", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabCrOn", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("CabPer", GetType(String))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("SubCabNo", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("FolNo", GetType(Integer))
            PermDt.Columns.Add(dc)
            dc = New DataColumn("PLevel", GetType(String))
            PermDt.Columns.Add(dc)
            Return PermDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetMemberGroups(ByVal sAC As String, ByVal iUsrId As Integer) As String
    '    Dim sSql As String
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select gld_grplvlid from sad_grplvl_members where gld_userid=" & iUsrId
    '        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
    '        sSql = ""
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                sSql = sSql & "," & dt.Rows(i)("Gld_grplvlid")
    '            Next
    '            If sSql.Length > 0 Then
    '                sSql = sSql.Remove(0, 1)
    '            Else
    '                sSql = 0
    '            End If
    '        End If
    '        Return sSql
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetUserType(ByVal sAC As String, ByVal iUserId As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select usr_IsSuperuser from sad_userdetails where usr_id=" & iUserId & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserParGrp(ByVal sAC As String, ByVal iLogUsrID As Integer) As Integer
        Dim sSql As String
        Try
            'sSql = "Select usr_LevelGrp from sad_Userdetails where usr_id=" & iLogUsrID & ""
            sSql = "Select Case When usr_LevelGrp Is NULL then '' else usr_LevelGrp End as usr_LevelGrp from sad_Userdetails where usr_id=" & iLogUsrID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateFolderCount(ByVal sAC As String, ByVal sSql As String)
        Dim dt As New DataTable, dtCab As New DataTable
        Dim iSql As String, mSql As String
        Dim i As Integer
        Try
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iSql = "Select count(*) as Count from edt_folder where fol_cabinet in (select cbn_node from edt_cabinet where cbn_parent=" & dt.Rows(i)("CBN_NODE") & ") and Fol_status='A'"
                    dtCab = objDBL.SQLExecuteDataTable(sAC, iSql)
                    If dtCab.Rows.Count > 0 Then
                        For j = 0 To dtCab.Rows.Count - 1
                            mSql = "Update edt_cabinet set CBN_FolCount=" & dtCab.Rows(j)("Count") & " where CBN_NODE=" & dt.Rows(i)("CBN_NODE") & ""
                            objDBL.SQLExecuteNonQuery(sAC, mSql)
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function GetParGrpID(ByVal sAC As String, ByVal iDTId As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Dot_PGroup from edt_document_type where Dot_DocTypeID=" & iDTId & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function CheckForGrpHead(ByVal sAC As String, ByVal iGrpId As Int16, ByVal iUsrId As Int16) As Integer
    '    Dim sSql As String
    '    Try
    '        sSql = "Select Gld_GrpLvlPosn from sad_grplvl_members where Gld_userId=" & iUsrId & " and GLD_GrpLvlId=" & iGrpId & ""
    '        Return objDBL.SQLExecuteScalar(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Private Function GetGroupName(ByVal sAC As String, ByVal GrpId As Int16) As String
        Dim sSql As String
        Try
            sSql = "Select Mas_description from sad_grporlvl_general_master where Mas_Id=" & GrpId & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Sub Cabinet
    Public Function LoadSubCabinets(ByVal sAC As String, ByVal LogUsrId As Int16, Optional ByVal sPerm As String = "VCB") As DataTable
        Dim sSql As String
        Dim dtSubCab As New DataTable
        Try
            'Permdt = BuildPermTable()
            iUsrType = GetUserType(sAC, LogUsrId)
            iUsrParGrp = GetUserParGrp(sAC, LogUsrId)
            If (iUsrType = 1) Then
                sSql = "" : sSql = "Select *,CBN_Node As ID,CBN_Parent As CabPar from edt_cabinet where CBN_DelStatus='A' and CBN_Parent <> -1 order by CBN_Name"
                dtSubCab = objDBL.SQLExecuteDataTable(sAC, sSql)
                Return dtSubCab
                Exit Function
            End If

            sSql = "" : sSql = "Select *,CBN_Node As ID,CBN_Parent As CabPar from edt_cabinet where CBN_DelStatus='A' and CBN_Parent <> -1 "
            If Len((sCabPerm)) <> 0 Then
                sSql = sSql & " and cbn_node in (" & sCabPerm & ")"
            Else
                sSql = sSql & " and cbn_node in (0)"
            End If
            sSql = sSql & " order by CBN_Name"
            dtSubCab = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtSubCab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabOrSC(ByVal sAC As String, ByVal sLevel As String, ByVal iIndexID As Integer, ByVal iUserID As Integer, ByVal dtCab As DataTable, ByVal dtSubCab As DataTable, ByVal dtFol As DataTable) As String
        Dim iRetVal As Integer, iCabId As Integer
        Dim sStr As String = ""
        Try
            Select Case sLevel
                Case "SC"
                    iRetVal = GetTableContents(dtSubCab, "ID", "CabPar", iIndexID)
                    If (iRetVal = String.Empty) Then
                        Return 0
                        Exit Try
                    End If
                Case "FD"
                    iCabId = GetTableContents(dtFol, "ID", "FolCabId", iIndexID)
                    iRetVal = CheckCabOrSC(iCabId, dtCab, dtSubCab, "ID")
                    If (iRetVal = "SC") Then
                        sStr = iCabId
                        iRetVal = GetTableContents(dtSubCab, "ID", "CabPar", iCabId)
                        sStr = sStr & "|" & iRetVal
                    ElseIf (iRetVal = "CB") Then
                        sStr = iRetVal.ToString
                    End If
            End Select
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTableContents(ByVal RefDt As DataTable, ByVal sSourceCol As String, ByVal sDestCol As String, ByVal sSourceVal As String) As String
        Dim dRow As DataRow
        Try
            For Each dRow In RefDt.Rows
                If (IsDBNull(dRow(sSourceCol)) = False) Then
                    If (dRow(sSourceCol) = sSourceVal) Then
                        Return dRow(sDestCol)
                    End If
                End If
            Next
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CheckCabOrSC(ByVal iCabID As Integer, ByVal dtCab As DataTable, ByVal dtSC As DataTable, ByVal sColName As String) As String
        Dim dr As DataRow
        Try
            For Each dr In dtCab.Rows
                If (IsDBNull(dr(sColName)) = False) Then
                    If (dr(sColName) = iCabID) Then
                        Return "CB"
                    End If
                End If
            Next
            For Each dr In dtSC.Rows
                If (IsDBNull(dr(sColName)) = False) Then
                    If (dr(sColName) = iCabID) Then
                        Return "SC"
                    End If
                End If
            Next
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Folder
    Public Function LoadFolForSrh(ByVal sAC As String, ByVal LogUsrId As Int16, ByVal iCompID As Integer, Optional ByVal sPerm As String = "VFD") As DataTable
        Dim dtFol As New DataTable
        Dim dRow As DataRow
        Dim sSql As String
        Dim Grpdv As DataView
        Try
            dtGrp = BuildGroupDt(sAC)
            Grpdv = dtGrp.DefaultView
            iUsrType = GetUserType(sAC, LogUsrId)
            iUsrParGrp = GetUserParGrp(sAC, LogUsrId)
            If (iUsrType = 1) Then
                sSql = "Select FOL_FolID As ID,CBN_Name,CBN_ParGrp,CBN_Node,FOL_Name from EDT_FOLDER Left Join EDT_Cabinet On CBN_Node=FOL_Cabinet where FOL_STATUS='A'"
                dtFol = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtFol.Rows.Count > 0 Then
                    For Each dRow In dtFol.Rows
                        iParGrp = dRow("CBN_ParGrp")
                        sCabName = dRow("CBN_Name")
                    Next
                End If
            End If
            Return dtFol
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function BuildGroupDt(ByVal sAC As String) As DataTable
        Dim dt As New DataTable
        Dim sSql As String
        Try
            sSql = "select Mas_Id,Mas_Description  from sad_grporlvl_general_master where Mas_DelFlag= 'A' "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildFolderTable() As DataTable
        Dim LocalDt As New DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("Name", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolNote", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolCab", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolSubCab", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolCrOn", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolCrBy", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolGroup", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("FolCabId", GetType(Integer))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("PageCount", GetType(Integer))
            LocalDt.Columns.Add(dc)

            'Permissions
            dc = New DataColumn("PLevel", GetType(String))
            LocalDt.Columns.Add(dc)
            Return LocalDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Document Type
    Public Function LoadIndexedFolders(ByVal sAC As String) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Distinct(pge_folder) from edt_page"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                sSql = String.Empty
                For i = 0 To dt.Rows.Count - 1
                    If IsDBNull(dt.Rows(i).Item("pge_folder")) = False Then
                        sSql = sSql & "," & dt.Rows(i).Item("pge_folder")
                    End If
                Next
                Return sSql
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDocTypes(ByVal sAC As String, ByVal iUserId As Integer, ByVal sPerm As String, ByVal sFolID As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDocType As New DataTable
        Try
            dtDocType.Columns.Add("Id")
            dtDocType.Columns.Add("Name")
            dtDocType.Columns.Add("Dot_Pgroup")
            dtDocType.Columns.Add("dot_Crby")
            dtDocType.Columns.Add("dot_Note")
            dtDocType.Columns.Add("dot_CrOn")
            dtDocType.Columns.Add("DtGrpName")

            sSql = "Select a.* From edt_document_type a Where a.Dot_Status='A' And a.Dot_DocTypeID"
            sSql = sSql & " in (" & " Select distinct(pge_Document_type) From edt_page Where pge_folder in (" & sFolID & "))"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function AddDocTypes(ByVal dt As DataTable, ByVal dRow As DataRow) As DataTable
        Dim dr As DataRow
        Try
            dr = dt.NewRow
            dr("Id") = dRow("Dot_DocTypeID")
            dr("Name") = dRow("Dot_DocName")
            dr("dot_Crby") = dRow("dot_Crby")
            dr("Dot_Pgroup") = dRow("Dot_Pgroup")
            dr("dot_Note") = dRow("dot_Note")
            dr("dot_CrOn") = dRow("dot_CrOn")
            dr("DtGrpName") = dRow("Mas_Description")
            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Keywords
    Public Function LoadKeyWords(ByVal sAC As String) As DataTable
        Dim sSql As String = "", sDisKw As String = ""
        Dim dtKey As New DataTable, dt As New DataTable
        Dim sArr() As String
        Dim i As Integer
        Dim dc As DataColumn
        Dim dr As DataRow
        Try
            sSql = "Select distinct(pge_keyword) from edt_page where pge_details_Id in (select distinct(pge_details_Id) from edt_page)"
            dtKey = objDBL.SQLExecuteDataTable(sAC, sSql)
            sDisKw = BuildDistinctKWs(dtKey)
            dt = New DataTable
            dc = New DataColumn("Name", GetType(String))
            dt.Columns.Add(dc)
            sArr = Split(sDisKw, ";")
            For i = 0 To UBound(sArr)
                If Convert.ToString(sArr(i)).Length <> 0 Then
                    dr = dt.NewRow
                    dr("Name") = sArr(i)
                    dt.Rows.Add(dr)
                End If
            Next
            dc = New DataColumn("ID", GetType(Int16))
            dt.Columns.Add(dc)
            dt = CreateIndexForTable("ID", dt)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildDistinctKWs(ByVal dtKw As DataTable) As String
        Dim sArr() As String
        Dim i As Integer
        Dim sStr As String = ""
        Try
            If (dtKw.Rows.Count > 0) Then
                For i = 0 To dtKw.Rows.Count - 1
                    sStr = sStr & ";" & dtKw.Rows(i).Item("pge_keyword").ToString
                Next
                sArr = Split(sStr, ";")
                sStr = String.Empty
                For i = 0 To UBound(sArr)
                    If (CheckForElmt(sArr(i), sStr) = False) Then
                        sStr = sStr & ";" & sArr(i)
                    End If
                Next
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CreateIndexForTable(ByVal sColName As String, ByVal dt As DataTable) As DataTable
        Dim i As Integer
        Dim dr As DataRow
        Try
            For i = 0 To dt.Rows.Count - 1
                dr = dt.Rows(i)
                dr.BeginEdit()
                dr(sColName) = i + 1
                dr.EndEdit()
                dr.AcceptChanges()
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForElmt(ByVal sKW As String, ByVal sStr As String) As Boolean
        Try
            If (InStr(sStr & ";", ";" & sKW & ";") > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Format
    Public Function LoadFormat(ByVal sAC As String) As DataTable
        Dim sSql As String
        Dim dtFormat As New DataTable
        Dim dc As DataColumn
        Dim dt As DataTable
        Try
            sSql = "Select distinct(PGE_EXT) as Name from edt_page where pge_status='A'"
            dtFormat = objDBL.SQLExecuteDataTable(sAC, sSql)
            dt = dtFormat
            dc = New DataColumn("ID", GetType(Int16))
            dt.Columns.Add(dc)
            dt = CreateIndexForTable("ID", dtFormat)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Users
    Public Function LoadUsers(ByVal sAC As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select usr_id as id,usr_fullname as Name from sad_userdetails where usr_id in (select distinct(pge_crby) from edt_page)"
            Return objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'btnSearch
    Public Function SearchDocuments(ByVal sAC As String, ByVal iUserId As Integer, ByVal sCab As String, ByVal sSubCab As String, ByVal sFol As String, ByVal sDocType As String, ByVal sKW As String, ByVal sDesc As String, ByVal sFDate As String, ByVal sTDate As String, ByVal sOCRText As String, ByVal sAnyDesc As String, ByVal sFrmt As String, Optional ByVal sDetailID As String = "", Optional ByVal sCrBY As String = "", Optional ByVal cComOrIndDesc As Char = "I", Optional ByVal sTitle As String = "") As DataTable
        Dim dtFdoc As DataTable, dtDoc As DataTable, dt As DataTable
        Dim strsql As String, sSql As String, sBaseID = "", sDetID = ""
        Dim sArr() As String
        Dim i As Integer, iRet As Integer = 0
        Dim dfDate As DateTime, dtDate As DateTime
        Try
            If (Len(sDetailID) <> 0) Then
                dtFdoc = BuildDocTable()
                sSql = "Select distinct(PGE_Details_ID) from edt_page where PGE_Details_ID in (" & sDetailID & ") and PGE_Status='A'"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sSql = "Select * from edt_page where PGE_Details_ID in (" & dt.Rows(i).Item("PGE_Details_ID") & ") and PGE_Status='A'"
                        dtDoc = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dtDoc.Rows.Count > 0 Then
                            dtFdoc = BuildFinalDocTable(sAC, dtDoc.Rows(0), dtFdoc, iUserId)
                        End If
                    Next
                End If
                Return dtFdoc
            Else
                strsql = " Select distinct(PGE_Details_ID) from edt_page where PGE_Status='A' "
                If (sCab.Length > 0) Then
                    strsql = strsql & " and PGE_Cabinet in (" & sCab & ")"
                End If
                If (sSubCab.Length > 0) Then
                    strsql = strsql & " and PGE_SubCabinet in (" & sSubCab & ")"
                End If
                If (sFol.Length > 0) Then
                    strsql = strsql & " and PGE_Folder in (" & sFol & ")"
                End If
                If (sKW.Length > 0) Then
                    strsql = strsql & " and ( PGE_KeyWord like " & sKW & ")"
                End If
                If (sOCRText.Length > 0) Then
                    strsql = strsql & " and PGE_OCRText like  " & sOCRText & " "
                End If
                If (sDocType.Length > 0) Then
                    strsql = strsql & " and PGE_Document_Type in (" & sDocType & ")"
                End If
                If (sFrmt.Length > 0) Then
                    strsql = strsql & " and PGE_Ext in (" & sFrmt & ")"
                End If
                If (sTitle.Length > 0) Then
                    strsql = strsql & " and ( pge_Title like  '%" & sTitle & "%' or pge_Title like '" & sTitle & "%'  or pge_Title like '%" & sTitle & "' or pge_Title='" & sTitle & "')"
                End If
                If (sFDate.Length > 0) Then
                    dfDate = CDate(clsGeneralFunctions.FormatMyDate(sFDate))
                    sFDate = objclsFASGeneral.FormatDtForRDBMS(dfDate, "I")
                    If (sTDate.Length = 0) Then
                        dtDate = dfDate.AddDays(1)
                    Else
                        dtDate = CDate(clsGeneralFunctions.FormatMyDate(sTDate))
                        dtDate = dtDate.AddDays(1)
                    End If
                    sTDate = objclsFASGeneral.FormatDtForRDBMS(dtDate, "I")
                    strsql = strsql & " and PGE_CrOn >= " & sFDate & " and  PGE_CrOn <= " & sTDate & ""
                End If
                If (sDesc.Length > 0) Then
                    sDetID = GetPageDetID(sAC, sDocType, sDesc)
                    sArr = Split(sDetID, ",")
                    sDetID = String.Empty
                    For i = 0 To UBound(sArr)
                        If (sArr(i).Length > 0) Then
                            sDetID = sDetID & "," & sArr(i)
                        End If
                    Next
                    If (sDetID.Length > 0) Then
                        If (sDetID.Chars(0).ToString = ",") Then
                            sDetID = sDetID.Remove(0, 1)
                            strsql = strsql & " and PGE_Details_ID in (" & sDetID & ")"
                        End If
                    Else
                        strsql = strsql & " and PGE_Details_ID in (0)"
                    End If
                End If
                If (sAnyDesc.Length > 0) Then
                    If (cComOrIndDesc = "C") Then
                        sSql = "Select distinct(epd_baseid) from edt_page_details where epd_baseid in "
                        sArr = Split(sAnyDesc, ",")
                        For i = 0 To UBound(sArr)
                            sArr(i) = Trim(sArr(i))
                            If (i > 0) Then
                                sSql = sSql & " and epd_baseid in "
                            End If
                            sSql = sSql & "(select distinct(epd_baseid) from edt_page_Details where EPD_Value like"
                            sSql = sSql & "'%" & sArr(i) & "%' or EPD_Value like '" & sArr(i) & "%'  or EPD_Value like '%" & sArr(i) & "' or EPD_Value='" & sArr(i) & "')"
                        Next
                        strsql = strsql & " and PGE_Details_ID in (" & sSql & ")"
                    ElseIf (cComOrIndDesc = "I") Then
                        sSql = "Select distinct(epd_baseid) from edt_page_details where EPD_Value Like "
                        sArr = Split(sAnyDesc, ",")
                        For i = 0 To UBound(sArr)
                            sArr(i) = Trim(sArr(i))
                            If (i > 0) Then
                                sSql = sSql & " or EPD_Value like "
                            End If
                            sSql = sSql & "'%" & sArr(i) & "%' or EPD_Value like '" & sArr(i) & "%'  or EPD_Value like '%" & sArr(i) & "' or EPD_Value='" & sArr(i) & "'"
                        Next
                        strsql = strsql & " and PGE_Details_ID in (" & sSql & ")"
                    End If
                End If

                dt = objDBL.SQLExecuteDataTable(sAC, strsql)
                If dt.Rows.Count > 0 Then
                    dtFdoc = BuildDocTable()
                    For i = 0 To dt.Rows.Count - 1
                        sSql = " Select * from edt_page where PGE_Details_ID= " & dt.Rows(i).Item("PGE_Details_ID") & " and PGE_Status='A'"
                        If (sCrBY.Length > 0) Then
                            sSql = sSql & " and  PGE_CrBy in (" & sCrBY & ")"
                        End If
                        dtDoc = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dtDoc.Rows.Count > 0 Then
                            dtFdoc = BuildFinalDocTable(sAC, dtDoc.Rows(0), dtFdoc, iUserId)
                        End If
                    Next
                    If dt.Rows.Count > 0 Then
                        Return dtFdoc
                    Else
                        dtFdoc = New DataTable
                        Return dtFdoc
                    End If
                Else
                    dtFdoc = New DataTable
                    Return dtFdoc
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildDocTable() As DataTable
        Dim dtDoc As DataTable
        Dim dc As DataColumn
        Try
            dtDoc = New DataTable
            dc = New DataColumn("BaseName", GetType(Integer))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("CabName", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("SubCabName", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("FolName", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("DocType", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("Title", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("IndexDate", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("CrBy", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("CrOn", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("PgeSize", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("Desc", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("DetailsId", GetType(String))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("FolID", GetType(Integer))
            dtDoc.Columns.Add(dc)
            dc = New DataColumn("DocTypeID", GetType(Integer))
            dtDoc.Columns.Add(dc)
            Return dtDoc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildFinalDocTable(ByVal sAC As String, ByVal drDoc As DataRow, ByVal dtFDoc As DataTable, ByVal iUserID As Integer) As DataTable
        Dim sDoc As String, sDocDet() As String
        Dim drFDoc As DataRow
        Dim iRet As Integer
        Try
            drFDoc = dtFDoc.NewRow
            drFDoc("BaseName") = drDoc("PGE_BaseName")
            'drFDoc("Title") = drDoc("PGE_Title")
            drFDoc("Title") = drDoc("PGE_OrignalFileName")
            drFDoc("CrOn") = objclsFASGeneral.FormatDtForRDBMS(drDoc("PGE_Date"), "F")
            drFDoc("PgeSize") = drDoc("PGE_Size")
            drFDoc("DetailsId") = drDoc("PGE_Details_Id")
            sDoc = RetPageDetails(sAC, drDoc("PGE_BaseName"))
            sDocDet = Split(sDoc, "|")
            drFDoc("CabName") = sDocDet(0)
            drFDoc("SubCabName") = sDocDet(1)
            drFDoc("FolName") = sDocDet(2)
            drFDoc("DocType") = sDocDet(3)
            drFDoc("FolID") = sDocDet(4)
            drFDoc("DocTypeID") = sDocDet(5)
            dtFDoc.Rows.Add(drFDoc)
            Return dtFDoc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function RetPageDetails(ByVal sAC As String, ByVal PgeBaseNme As Integer) As String
        Dim objParam() As OleDb.OleDbParameter
        Dim iCount As Integer
        Dim cmd As OleDb.OleDbCommand
        Try
            objParam = New OleDb.OleDbParameter(6) {}
            objParam(iCount) = New OleDb.OleDbParameter("@PGE_BaseName", OleDb.OleDbType.Integer)
            objParam(iCount).Value = PgeBaseNme
            objParam(iCount).Direction = ParameterDirection.Input
            objParam(iCount).Size = 20
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_Cabinet", OleDb.OleDbType.VarChar)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_SubCabinet", OleDb.OleDbType.VarChar)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_Folder", OleDb.OleDbType.VarChar)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_Document_Type", OleDb.OleDbType.VarChar)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_FolID", OleDb.OleDbType.Integer)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@PGE_DocTypeID", OleDb.OleDbType.Integer)
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 200
            iCount = iCount + 1

            cmd = objDBL.SpFrInsertionUsingCmd(sAC, "GetPageDetails", objParam)
            Return cmd.Parameters("@PGE_Cabinet").Value & "|" & cmd.Parameters("@PGE_SubCabinet").Value &
                      "|" & cmd.Parameters("@PGE_Folder").Value & "|" & cmd.Parameters("@PGE_document_type").Value &
                "|" & cmd.Parameters("@PGE_FolID").Value & "|" & cmd.Parameters("@PGE_DocTypeID").Value
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPageDetID(ByVal sAC As String, ByVal sDocType As String, ByVal sDesc As String) As String
        Dim sSql As String = "", strsql As String = "", sDetID = ""
        Dim dt As New DataTable
        Dim aDoc() As String
        Dim i As Integer, j As Integer, k As Integer
        Dim descArr(), sArr() As String
        Dim htDt As Hashtable
        Dim bRet As Boolean
        Try
            If (sDocType.Length <= 0) Then
                descArr = Split(sDesc, "$")
                For i = 0 To UBound(descArr)
                    sArr = Split(descArr(i), ",")
                    If (sArr.Length > 1) Then
                        sArr(1) = Trim(sArr(1))
                        If (i > 1) Then
                            sSql = sSql & " and EPD_BaseID in "
                        End If
                        sSql = sSql & " ( Select distinct(epd_baseid) from edt_page_details where  "
                        sSql = sSql & " EPD_DescID=" & sArr(0) & " and  (EPD_Value Like  " &
                               "'% " & sArr(1) & " %' or EPD_Value like '" & sArr(1) & " %'  or EPD_Value like '% " & sArr(1) & "' or EPD_Value='" & sArr(1) & "')) "
                    End If
                Next
                sSql = "" : sSql = "Select distinct(epd_baseID) from edt_page_details where EPD_BaseID in  " & sSql
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sDetID = sDetID & "," & dt.Rows(i).Item("EPD_BaseID")
                    Next
                    Return sDetID
                Else
                    Return ""
                End If
            Else
                Dim iDesc As Integer
                Dim bFoundOrNot As Boolean 'To check value sin Each Descriptor - #11
                Dim sLocalVal As String
                htDt = GetDescriptor(sAC, sDocType)
                If (sDesc.Length > 0) Then
                    aDoc = Split(sDocType, ",")
                    descArr = Split(sDesc, "$")
                    For j = 0 To UBound(aDoc)  'For each Document types
                        sSql = String.Empty
                        iDesc = 0
                        For i = 0 To UBound(descArr) 'For Each descriptor in DocType
                            If (descArr(i).Length > 0) Then
                                bFoundOrNot = False '#11
                                sArr = Split(descArr(i), ",")
                                bRet = CheckDescForDT(htDt, aDoc(j), sArr(0))
                                If (bRet = True) Then
                                    sArr(1) = Trim(sArr(1))
                                    strsql = "  EPD_DescID=" & sArr(0) & " and  (EPD_Value Like  " &
                                              "'% " & sArr(1) & " %' or EPD_Value like '" & sArr(1) & " %'  or EPD_Value like '% " & sArr(1) & "' or EPD_Value='" & sArr(1) & "')  "

                                    strsql = strsql & " and   EPD_DocType in (" & aDoc(j) & ") "
                                    strsql = "Select epd_baseid from edt_page_details where  " & strsql
                                    dt = objDBL.SQLExecuteDataTable(sAC, strsql)
                                    sLocalVal = String.Empty
                                    If dt.Rows.Count > 0 Then
                                        For k = 0 To dt.Rows.Count - 1
                                            bRet = "," & dt.Rows(k).Item("EPD_BaseID")
                                            If iDesc = 0 Then
                                                sLocalVal = sLocalVal & bRet
                                                bFoundOrNot = True  '#11
                                            Else
                                                If (InStr(sSql & ",", bRet & ",") <> 0) Then
                                                    sLocalVal = sLocalVal & bRet
                                                    bFoundOrNot = True  '#11
                                                End If
                                            End If
                                        Next
                                        iDesc = iDesc + 1
                                        If bFoundOrNot = False Then '#11
                                            GoTo DocTypeLP
                                        Else
                                            sSql = sLocalVal
                                        End If
                                    Else
                                        GoTo DocTypeLP
                                    End If
                                End If
                            End If
DescLP:                 Next      'End of Descriptors Loop
                        sDetID = sDetID & sSql
DocTypeLP:          Next   'End of DocumentType Loop
                End If
                If (sDetID.Length > 0) Then
                    If (sDetID.Chars(0).ToString = ",") Then
                        sDetID = sDetID.Remove(0, 1)
                    End If
                End If
                Return sDetID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetDescriptor(ByVal sAC As String, ByVal sDocType As String) As Hashtable
        Dim sDT() As String
        Dim i As Integer
        Dim sSql As String = "", sDtDesc As String = ""
        Dim dt As New DataTable
        Dim htDt As Hashtable
        Try
            sDT = Split(sDocType, ",")
            If (sDT.Length > 0) Then
                htDt = New Hashtable
                For i = 0 To UBound(sDT)
                    If sDT(i).Length > 0 Then
                        sSql = "select des_id,desc_name from edt_descriptios where des_id in (select distinct(EDD_DPTRID) & From edt_doctype_link"
                        sSql = sSql & " Where EDD_DocTypeID In (" & sDT(i) & "))"
                        dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dt.Rows.Count > 0 Then
                            For j = 0 To dt.Rows.Count - 1
                                sDtDesc = sDtDesc & "+" & dt.Rows(j)("des_ID")
                            Next
                            htDt.Add(sDT(i), sDtDesc)
                        End If
                    End If
                    sDtDesc = String.Empty
                Next
            End If
            Return htDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CheckDescForDT(ByVal dtHT As Hashtable, ByVal iDocType As Integer, ByVal iDescID As String) As Boolean
        Dim coll As Collections.IDictionaryEnumerator
        Dim sDesc As String = ""
        Dim i As Integer
        Try
            coll = dtHT.GetEnumerator()
            coll.Reset()
            coll.MoveNext()
            For i = 0 To dtHT.Count - 1
                If (iDocType = coll.Key) Then
                    sDesc = coll.Value
                Else
                    coll.MoveNext()
                End If
            Next
            If (InStr(sDesc & "+", "+" & iDescID & "+") > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckFolDocTypePerm(ByVal sAC As String, ByVal sFol As String, ByVal sDocType As String, ByVal iUserID As Integer) As Boolean
        Dim sSQL As String
        Dim iRet As Integer = 0
        Try
            sSQL = "Select * from edt_folder_rights where Fer_DocTypeId In (" & sDocType & ") And Fer_Folderid In (" & sFol & ") And Fer_UserId=" & iUserID & " And Fer_search=0"
            iRet = objDBL.SQLExecuteScalar(sAC, sSQL)
            If iRet = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFolderID(ByVal dtSearch As DataTable) As String
        Dim dr As DataRow
        Dim sFol As String = ""
        Try
            For Each dr In dtSearch.Rows
                sFol = sFol & "," & dr("FolID")
            Next
            If (sFol.Length > 0) Then
                If (sFol.Chars(0).ToString = ",") Then
                    sFol = sFol.Remove(0, 1)
                End If
            End If
            Return sFol
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocTypeID(ByVal dtSearch As DataTable) As String
        Dim dr As DataRow
        Dim sDT As String = ""
        Try
            For Each dr In dtSearch.Rows
                sDT = sDT & "," & dr("DocTypeId")
            Next
            If (sDT.Length > 0) Then
                If (sDT.Chars(0).ToString = ",") Then
                    sDT = sDT.Remove(0, 1)
                End If
            End If
            Return sDT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDescType(ByVal sAC As String, ByVal iDoc As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select a.dt_name from EDT_DESC_TYPE a ,EDT_DESCRIPTIOS b where b.desc_datatype=a.dt_id And b.des_id=" & iDoc)
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function

    'chkselect
    Public Function GetDescUnion(ByVal sAC As String, Optional ByVal sDocTypeID As String = "") As DataTable
        Dim sSql As String
        Dim sArr() As String
        Dim i As Integer
        Dim RefDt As New DataTable, dt As New DataTable
        Dim dc As DataColumn
        Try
            If (sDocTypeID <> "") Then
                sArr = Split(sDocTypeID, ";")
                sDocTypeID = String.Empty
                For i = 0 To UBound(sArr)
                    If (sArr(i).Length > 0) Then
                        sDocTypeID = sDocTypeID & "," & sArr(i)
                    End If
                Next
                If (sDocTypeID.Length > 0) Then
                    sDocTypeID = sDocTypeID.Remove(0, 1)
                End If
                sSql = "Select b.DES_ID As ID,b.DESC_Name As Name from edt_doctype_link a,edt_descriptios b where edd_doctypeID In ( " & sDocTypeID & " ) And a.edd_dptrid=b.des_id order by b.EDD_Pk"
            Else
                sSql = "Select DES_ID As ID,DESC_Name As Name from edt_descriptios where des_id In (Select distinct(EDD_DPTRID) " & " from edt_doctype_link"
                sSql = sSql & " where EDD_DocTypeID In (Select Dot_doctypeID from edt_document_type ))"
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Columns.Count - 1
                    dc = New DataColumn(dt.Columns(i).ColumnName, dt.Columns(i).DataType)
                    RefDt.Columns.Add(dc)
                Next
                RefDt = CopyRowsToFirstDT(dt, RefDt)
            End If
            Return RefDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CopyRowsToFirstDT(ByVal dt1 As DataTable, ByVal dt2 As DataTable) As DataTable
        Dim dr1 As DataRow, dr2 As DataRow, drRef2 As DataRow
        Dim i As Integer, j As Integer = 0
        Try
            For Each dr1 In dt1.Rows
                j = 0
                For Each drRef2 In dt2.Rows
                    If (dr1("ID") = drRef2("ID")) Then
                        j = 1
                    End If
                Next
                If (j = 0) Then
                    dr2 = dt2.NewRow
                    For i = 0 To dt1.Columns.Count - 1
                        dr2(dt1.Columns(i).ColumnName) = dr1(dt1.Columns(i).ColumnName)
                    Next
                    dt2.Rows.Add(dr2)
                End If
            Next
            Return dt2
        Catch ex As Exception
            Throw
        End Try
    End Function


    'SearchView
    Public Function GetCrBy(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select usr_fullname from sad_userdetails where usr_id = (Select pge_crby from edt_page where pge_basename=" & iDetails_id & ")")
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCrOn(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sCrOn As String = ""
        Try
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, "Select pge_cron from edt_page where pge_basename=" & iDetails_id & "")) = False Then
                sCrOn = objclsFASGeneral.FormatDtForRDBMS(objDBL.SQLGetDescription(sAC, "Select pge_cron from edt_page where pge_basename=" & iDetails_id & ""), "D")
            End If
            Return sCrOn
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetModOn(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sModOn As String = ""
        Try
            If IsDBNull(objDBL.SQLExecuteScalar(sAC, "Select pge_modon from edt_page where pge_details_id=" & iDetails_id & "")) = False Then
                sModOn = objclsFASGeneral.FormatDtForRDBMS(objDBL.SQLGetDescription(sAC, "Select pge_modon from edt_page where pge_details_id=" & iDetails_id & ""), "D")
            End If
            Return sModOn
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatus(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select pge_status from edt_page where pge_Details_id= " & iDetails_id & "")
            If sName = "A" Then
                sName = "Active"
            Else
                sName = "In-Active"
            End If
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalPage(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select count(*) from edt_page where pge_status='A'and pge_details_id= " & iDetails_id & "")
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFileSize(ByVal sAC As String, ByVal iDetails_id As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "select sum(pge_size) from edt_page where pge_details_id= " & iDetails_id & "")
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetModBy(ByVal sAC As String, ByVal iBasename As Integer) As String
        Dim sName As String = ""
        Try
            sName = objDBL.SQLGetDescription(sAC, "Select a. usr_fullname from sad_userdetails a,edt_page b where a.usr_id= b.pge_modby and b.pge_details_id = " & iBasename & "")
            Return sName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetKWords(ByVal sAC As String, ByVal ipDetails As Integer) As String
        Dim sKeyWords As String = ""
        Try
            sKeyWords = objDBL.SQLGetDescription(sAC, "Select distinct epd_keyword from edt_page_details where  epd_baseid =" & ipDetails & "")
            Return sKeyWords
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDescName(ByVal sAC As String, ByVal ipDetailsId As Integer) As String
        Dim sSql As String, sDescName As String = ""
        Dim iCount As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "Select b.desc_name +' : ' + a.epd_value as desc_name  from edt_page_details a,EDT_DESCRIPTIOS b where a.epd_descid=b.des_id and "
            sSql = sSql & " a.epd_baseid = " & ipDetailsId & " And a.epd_doctype In (Select pge_document_type from edt_page where pge_details_id =" & ipDetailsId & ")"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iCount = iCount + 1
                    If iCount > 1 Then
                        sDescName = sDescName & "'" & dt.Rows(i)("desc_name")
                    Else
                        sDescName = dt.Rows(i)("desc_name")
                    End If
                Next
            End If
            Return sDescName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetScanDetails(ByVal sAC As String, ByVal ipDetails As Integer) As String
        Dim sSql As String, sScanDt As String = "", sLocation As String = "", sBuilding As String = "", sFloor As String = ""
        Dim sRoomNo As String = "", sRow As String = "", sColumn As String = "", sRackNo As String = "", sDescription As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from edt_ScanDoc_Details where SCAN_BatchID in(select pge_BatchID from edt_page where pge_basename=" & ipDetails & ")"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("SCAN_LOCATION") <> "" Then
                    sLocation = "Location : " & dt.Rows(0)("SCAN_LOCATION")
                End If

                If dt.Rows(0)("SCAN_BUILDING") <> "" Then
                    sBuilding = "Building : " & dt.Rows(0)("SCAN_BUILDING")
                End If

                If dt.Rows(0)("SCAN_FLOOR") <> "" Then
                    sFloor = "Floor : " & dt.Rows(0)("SCAN_FLOOR")
                End If

                If dt.Rows(0)("SCAN_ROOMNO") <> "" Then
                    sRoomNo = "RoomNo : " & dt.Rows(0)("SCAN_ROOMNO")
                End If

                If dt.Rows(0)("SCAN_ROW") <> "" Then
                    sRow = "Row : " & dt.Rows(0)("SCAN_ROW")
                End If

                If dt.Rows(0)("SCAN_COLUMN") <> "" Then
                    sColumn = "Column : " & dt.Rows(0)("SCAN_COLUMN")
                End If

                If dt.Rows(0)("SCAN_RACKNO") <> "" Then
                    sRackNo = "RackNo : " & dt.Rows(0)("SCAN_RACKNO")
                End If

                If dt.Rows(0)("SCAN_DESCRIPTION") <> "" Then
                    sDescription = "Description : " & dt.Rows(0)("SCAN_DESCRIPTION")
                End If
            End If
            If sLocation <> "" Then
                sScanDt = sScanDt & "|" & sLocation
            End If

            If sBuilding <> "" Then
                sScanDt = sScanDt & "|" & sBuilding
            End If

            If sFloor <> "" Then
                sScanDt = sScanDt & "|" & sFloor
            End If

            If sRoomNo <> "" Then
                sScanDt = sScanDt & "|" & sRoomNo
            End If

            If sRow <> "" Then
                sScanDt = sScanDt & "|" & sRow
            End If

            If sColumn <> "" Then
                sScanDt = sScanDt & "|" & sColumn
            End If

            If sRackNo <> "" Then
                sScanDt = sScanDt & "|" & sRackNo
            End If

            If sDescription <> "" Then
                sScanDt = sScanDt & "|" & sDescription
            End If
            If sScanDt.StartsWith("|") Then
                sScanDt = sScanDt.Remove(0, 1)
            End If
            Return sScanDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPageFromEdict(ByVal sAC As String, ByVal iSelectedIndexID As Integer) As String
        Dim sImagePath As String = "", sExt As String = "", sFIleInDB As String = "", sBool As String = ""
        Dim files() As String
        Try
            sFIleInDB = objDBL.SQLExecuteScalar(sAC, "Select sad_Config_Value from sad_config_settings where sad_Config_Key = 'FilesInDB'")
            If UCase(sFIleInDB) = "FALSE" Then
                sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
                sBool = GetImageSettings(sAC, "ImgPath")
                sImagePath = sBool & "\" & "BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sImagePath) = False Then
                    System.IO.Directory.CreateDirectory(sImagePath)
                End If
                sImagePath = sImagePath & iSelectedIndexID & sExt
            ElseIf UCase(sFIleInDB) = "TRUE" Then
                sExt = objDBL.SQLExecuteScalar(sAC, "Select pge_ext from EDT_PAGE where pge_basename=" & iSelectedIndexID & "")
                Dim sPath As String = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sPath) = True Then
                    files = Directory.GetFileSystemEntries(sPath)
                    For Each element As String In files
                        If (Not Directory.Exists(element)) Then
                            File.Delete(Path.Combine(sPath, Path.GetFileName(element)))
                        End If
                    Next
                End If

                If System.IO.Directory.Exists(sPath) = False Then
                    System.IO.Directory.CreateDirectory(sPath)
                End If

                Updateimage(sAC, iSelectedIndexID, sExt)
                sImagePath = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
                If System.IO.Directory.Exists(sImagePath) = False Then
                    System.IO.Directory.CreateDirectory(sImagePath)
                End If
                sImagePath = sImagePath & iSelectedIndexID & "." & sExt
            End If
            If sExt = "" Then
                sImagePath = ""
            End If
            GetPageFromEdict = sImagePath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImageSettings(ByVal sAC As String, ByVal sCode As String)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from sad_config_settings where sad_Config_Key ='" & sCode & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("sad_Config_Value").ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Updateimage(ByVal sAC As String, ByVal iSelectedIndexID As Integer, ByVal sExt As String)
        Dim pdr As OleDb.OleDbDataReader
        Dim sSql As String
        Dim iAtchOle As Integer
        Try
            Dim sPath As String = "C:\Temp\MMCS\BITMAPS\" & iSelectedIndexID \ 301 & "\"
            If Not System.IO.Directory.Exists(sPath) Then
                Directory.CreateDirectory(sPath)
            End If
            sPath = sPath & iSelectedIndexID & "." & sExt
            If System.IO.File.Exists(sPath) = False Then
                sSql = "Select BDT_BIGDATA,BDT_BASENAME from EDT_BIGDATA where BDT_BASENAME=" & Val(iSelectedIndexID) & ""
                pdr = objDBL.SQLDataReader(sAC, sSql)
                If pdr.HasRows Then
                    While pdr.Read()
                        Dim BUFFER(pdr.GetBytes(iAtchOle, 0, BUFFER, 0, Integer.MaxValue)) As Byte
                        pdr.GetBytes(iAtchOle, 0, BUFFER, 0, BUFFER.Length)
                        Dim BlobData As New IO.FileStream(sPath, IO.FileMode.Create, IO.FileAccess.Write)
                        BlobData.Write(BUFFER, 0, BUFFER.Length)
                        BlobData.Close()
                    End While
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function


    'ImageView
    Public Function LoadListFiles(ByVal sAC As String, ByVal iBaseID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select pge_basename from EDT_PAGE where pge_status <> 'X' and Pge_Details_Id=" & iBaseID & " ORDER BY PGE_PAGENO"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetName(ByVal sAC As String, ByVal iBaseId As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sType = "Size" Then
                sSql = "Select BDT_Size from EDT_BIGDATA where BDT_BASENAME=" & iBaseId & ""
            ElseIf sType = "FullName" Then
                sSql = "Select Usr_FullName from sad_userDetails where usr_Id in(Select Pge_CreatedBy from EDT_PAGE where PGE_BaseName=" & iBaseId & ")"
            ElseIf sType = "DocName" Then
                sSql = "Select DOT_DocName from edt_document_type where Dot_DoctypeID=" & iBaseId & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocTypeID(ByVal sAC As String, ByVal iBaseId As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select PGE_Document_type from EDT_PAGE where PGE_BaseName=" & iBaseId & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSearchCrON(ByVal sAC As String, ByVal iBaseId As Integer) As String
        Dim sSql As String, sDate As String
        Try
            sSql = "Select Case When Pge_CreatedOn Is NULL then '' else Pge_CreatedOn End As Pge_CreatedOn From EDT_PAGE Where PGE_BaseName=" & iBaseId & ""
            sDate = objDBL.SQLExecuteScalar(sAC, sSql)
            Return objclsFASGeneral.FormatDtForRDBMS(sDate, "D")
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIndexDetails(ByVal sAC As String, ByVal iDocType As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable, dtPage As New DataTable
        Dim dr As DataRow
        Dim iBaseId As Integer
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("Descriptor")
            sSql = "select a.des_id,a.Desc_name,a.Desc_Datatype,a.Desc_size ,b.edd_isrequired "
            sSql = sSql & "from EDT_DESCRIPTIOS a,EDT_DOCTYPE_LINK b "
            sSql = sSql & "where a.des_id=b.edd_dptrid and b.edd_doctypeid= " & iDocType & " order by a.des_id"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dr = dt.NewRow
                    If IsDBNull(dtDetails.Rows(i)("DES_ID")) = False Then
                        dr("ID") = dtDetails.Rows(i)("DES_ID")
                    End If

                    If iBaseName <> 0 Then
                        iBaseId = Val(objDBL.SQLExecuteScalar(sAC, "select Pge_DETAILS_ID from edt_page where pge_basename = " & iBaseName) & "")
                        sSql = "select * from EDT_PAGE where pge_details_id = " & iBaseId & ""
                        dtPage = objDBL.SQLExecuteDataTable(sAC, sSql)
                        If dtPage.Rows.Count > 0 Then
                            For j = 0 To dtPage.Rows.Count - 1
                                dr("Descriptor") = dtDetails.Rows(i)("Desc_Name") & " : " & objDBL.SQLExecuteScalar(sAC, "select epd_value from EDT_PAGE_DETAILS where EPD_BaseID = " & Val(dtPage.Rows(j)("Pge_DETAILS_ID")) & " and EPD_Doctype = " & Val(dtPage.Rows(j)("PGE_DOCUMENT_TYPE")) & " and EPD_DescID = " & dtDetails.Rows(i)("DES_ID") & "")
                            Next
                        Else
                            dr("Descriptor") = ""
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
    Private Function BuildMasterTable() As DataTable
        Dim dt As New DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("ID")
            dt.Columns.Add(dc)
            dc = New DataColumn("Descriptor")
            dt.Columns.Add(dc)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExtension(ByVal sAC As String, ByVal iBaseID As Integer) As String
        Dim sSql As String, sExtension As String
        Try
            sSql = "Select PGE_Ext from edt_page where pge_basename=" & iBaseID & ""
            sExtension = objDBL.SQLGetDescription(sAC, sSql)
            Return sExtension
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPageFromEdict(ByVal sAC As String, ByVal iBaseName As Long, ByVal sExt As String) As String
        Dim sImagePath As String = "", sFIleInDB As String = "", sIPath As String = "", sPath As String = ""
        Try
            sFIleInDB = objDBL.SQLExecuteScalar(sAC, "Select sad_Config_Value from sad_config_settings where sad_Config_Key = 'FilesInDB'")
            If UCase(sFIleInDB) = "FALSE" Then
                sIPath = GetImgPath(sAC, "ImgPath")
                sImagePath = sIPath & "\" & "BITMAPS\" & iBaseName \ 301 & "\"
                sExt = objDBL.SQLGetDescription(sAC, "Select pge_ext from EDT_PAGE where pge_basename =  " & iBaseName & "")
                If sExt.StartsWith(".") Then
                    sExt = sExt.Remove(0, 1)
                End If
                sImagePath = sImagePath & iBaseName & "." & sExt   'Actual File Name
                sPath = "C:\Temp\MMCS\BITMAPS\VERSION\0\" & iBaseName & "." & sExt
                'Decrypt(sImagePath, sPath)
            ElseIf UCase(sFIleInDB) = "TRUE" Then
                sImagePath = Updateimage(sAC, iBaseName, sExt)
            End If
            GetPageFromEdict = sImagePath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImgPath(ByVal sAC As String, ByVal sKEy As String) As String
        Dim sSql As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key = '" & sKEy & "'"
                GetImgPath = objDBL.SQLExecuteScalar(sAC, sSql)
            Return GetImgPath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Sub Decrypt(ByVal sInputFilePath As String, ByVal sOutputFilePath As String)
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using fs As New FileStream(sInputFilePath, FileMode.Open)
                Using cs As New CryptoStream(fs, encryptor.CreateDecryptor(), CryptoStreamMode.Read)
                    Using fsOutput As New FileStream(sOutputFilePath, FileMode.Create)
                        Dim data As Integer
                        While (Assign(data, cs.ReadByte())) <> -1
                            fsOutput.WriteByte(CByte(data))
                        End While
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Private Shared Function Assign(Of T)(ByRef source As T, ByVal value As T) As T
        source = value
        Return value
    End Function
    Public Function GetFileNames(ByVal sAC As String, ByVal iACID As Integer, ByVal iBaseID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select pge_OrignalFileName from EDT_PAGE where pge_status= 'A' and PGE_BASENAME=" & iBaseID & " And Pge_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
