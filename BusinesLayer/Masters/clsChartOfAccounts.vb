Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsChartOfAccounts
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Private gl_id As Integer
    Private gl_glcode As String
    Private gl_Parent As Integer
    Private gl_Desc As String
    Private gl_head As Integer
    Private gl_Delflag As String
    Private gl_AccHead As Integer
    Private gl_reason_Creation As String
    Private gl_Crby As Integer
    Private gl_CrDate As Date
    Private gl_CompId As Integer
    Private gl_AppBy As Integer
    Private gl_AppOn As Date
    Private gl_Status As String
    Private gl_AccType As String
    Private gl_IPAddress As String
    Private gl_UpdatedBy As Integer
    Private gl_UpdatedOn As Date

    Public Property igl_UpdatedBy() As Integer
        Get
            Return (gl_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            gl_UpdatedBy = Value
        End Set
    End Property
    Public Property dgl_UpdatedOn() As Date
        Get
            Return (gl_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            gl_UpdatedOn = Value
        End Set
    End Property

    Public Property igl_id() As Integer
        Get
            Return (gl_id)
        End Get
        Set(ByVal Value As Integer)
            gl_id = Value
        End Set
    End Property

    Public Property sgl_glcode() As String
        Get
            Return (gl_glcode)
        End Get
        Set(ByVal Value As String)
            gl_glcode = Value
        End Set
    End Property
    Public Property igl_Parent() As Integer
        Get
            Return (gl_Parent)
        End Get
        Set(ByVal Value As Integer)
            gl_Parent = Value
        End Set
    End Property


    Public Property sgl_Desc() As String
        Get
            Return (gl_Desc)
        End Get
        Set(ByVal Value As String)
            gl_Desc = Value
        End Set
    End Property
    Public Property igl_head() As Integer
        Get
            Return (gl_head)
        End Get
        Set(ByVal Value As Integer)
            gl_head = Value
        End Set
    End Property

    Public Property sgl_Delflag() As String
        Get
            Return (gl_Delflag)
        End Get
        Set(ByVal Value As String)
            gl_Delflag = Value
        End Set
    End Property
    Public Property igl_AccHead() As Integer
        Get
            Return (gl_AccHead)
        End Get
        Set(ByVal Value As Integer)
            gl_AccHead = Value
        End Set
    End Property
    Public Property sgl_reason_Creation() As String
        Get
            Return (gl_reason_Creation)
        End Get
        Set(ByVal Value As String)
            gl_reason_Creation = Value
        End Set
    End Property

    Public Property igl_Crby() As Integer
        Get
            Return (gl_Crby)
        End Get
        Set(ByVal Value As Integer)
            gl_Crby = Value
        End Set
    End Property

    Public Property dgl_CrDate() As Date
        Get
            Return (gl_CrDate)
        End Get
        Set(ByVal Value As Date)
            gl_CrDate = Value
        End Set
    End Property

    Public Property igl_CompId() As Integer
        Get
            Return (gl_CompId)
        End Get
        Set(ByVal Value As Integer)
            gl_CompId = Value
        End Set
    End Property
    Public Property igl_AppBy() As Integer
        Get
            Return (gl_AppBy)
        End Get
        Set(ByVal Value As Integer)
            gl_AppBy = Value
        End Set
    End Property
    Public Property dgl_AppOn() As Date
        Get
            Return (gl_AppOn)
        End Get
        Set(ByVal Value As Date)
            gl_AppOn = Value
        End Set
    End Property

    Public Property sgl_Status() As String
        Get
            Return (gl_Status)
        End Get
        Set(ByVal Value As String)
            gl_Status = Value
        End Set
    End Property
    Public Property sGl_AccType() As String
        Get
            Return (gl_AccType)
        End Get
        Set(ByVal Value As String)
            gl_AccType = Value
        End Set
    End Property

    Public Property sgl_IPAddress() As String
        Get
            Return (gl_IPAddress)
        End Get
        Set(ByVal Value As String)
            gl_IPAddress = Value
        End Set
    End Property
    Public Function LoadChartOfAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            If iHead > 0 Then
                sSql = "select gl_id,gl_parent,gl_desc from  Chart_Of_Accounts where gl_AccHead = " & iHead & " and gl_CompId=" & iCompID & " and gl_id <> 0  order by gl_Desc Asc"
            Else
                sSql = "Select gl_id,gl_parent,gl_desc from  Chart_Of_Accounts where gl_CompId=" & iCompID & " and gl_id <> 0  order by gl_Desc Asc"
            End If

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 0 and "
            sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iCompID & "  order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGroup As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 1 and "
            sSql = sSql & "gl_Parent =" & iGroup & " And gl_CompId =" & iCompID & "  order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSubGroup As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 2 and "
            sSql = sSql & "gl_Parent =" & iSubGroup & " and gl_CompId =" & iCompID & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGL As Integer, Optional ByVal iglindex As Integer = 0) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 3 and "
            sSql = sSql & "gl_Parent =" & iGL & " And gl_CompId =" & iCompID & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateGrpCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer)
        Dim sSql As String = "", aSql As String = ""
        Dim Grp As String = "", prefix As String = ""
        Dim GrpLength As Integer = 0
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select IsNull(count(*),0)+1 from chart_of_accounts where gl_head = 0 and "
            sSql = sSql & "gl_acchead ='" & iHead & "' and gl_compId='" & iCompID & "' "
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sNameSpace, sSql))

            aSql = "" : aSql = "select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iCompID & "'"
            dr = objDBL.SQLDataReader(sNameSpace, aSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If

                If (Grp.Length < GrpLength) Then
                    Grp = Grp.PadLeft(GrpLength, "0")
                End If
            End If
            dr.Close()
            Return prefix + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateSubGrpCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer, ByVal GrpGl As Integer)
        Dim sSql As String = "", aSql As String = ""
        Dim Grp As String = "", prefix As String = "", SubGrp As String = ""
        Dim GrpLength As Integer = 0
        Dim dr As OleDb.OleDbDataReader
        Dim sGL As String = ""
        Try
            sSql = "" : sSql = "Select IsNull(count(*),0)+1 from chart_of_accounts where gl_head=1 and gl_acchead='" & iHead & "' and gl_compId='" & iCompID & "' and gl_parent = " & GrpGl & " "
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sNameSpace, sSql))

            sGL = objDBL.SQLExecuteScalar(sNameSpace, "Select gl_glCode from chart_of_accounts where gl_id = " & GrpGl & " and gl_compId='" & iCompID & "'")

            aSql = "" : aSql = "Select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iCompID & "'"
            dr = objDBL.SQLDataReader(sNameSpace, aSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("acs_subgroup")) = False Then
                    SubGrp = dr("acs_subgroup")
                End If

                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If

                If (Grp.Length < SubGrp) Then
                    Grp = Grp.PadLeft(SubGrp, "0")
                End If
            End If
            dr.Close()
            Return sGL + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateGLCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer, ByVal GrpGl As Integer)
        Dim sSqlGrp As String = "", sSql As String = ""
        Dim Grp As String = "", prefix As String = "", sGL As String = ""
        Dim GrpLength As Integer, SubGrp As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            sSqlGrp = "" : sSqlGrp = "select IsNull(count(*),0)+1 from chart_of_accounts where "
            sSqlGrp = sSqlGrp & "gl_acchead ='" & iHead & "' and gl_compId='" & iCompID & "' and gl_parent = " & GrpGl & ""
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sNameSpace, sSqlGrp))

            sGL = objDBL.SQLExecuteScalar(sNameSpace, "Select gl_glCode from chart_of_accounts where gl_id = " & GrpGl & " and gl_compId='" & iCompID & "'")

            sSql = "" : sSql = "Select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iCompID & "'"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("acs_subgroup")) = False Then
                    SubGrp = dr("acs_subgroup")
                End If

                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If

                If Grp.Length < SubGrp Then
                    Grp = Grp.PadLeft(SubGrp, "0")
                End If
            End If
            dr.Close()
            Return sGL + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function

        Public Function GenerateSubGLCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer, ByVal GrpGl As Integer)
        Dim sSql As String = "", aSql As String = ""
        Dim Grp As String = "", SubGrp As String = "", prefix As String = "", sGL As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim GrpLength As Integer
        Try
            sSql = "Select IsNull(count(*),0)+1 from chart_of_accounts where gl_head=3 and gl_acchead='" & iHead & "' and gl_delflag ='C' and "
            sSql = sSql & "gl_compId ='" & iCompID & "' and gl_parent = " & GrpGl & ""
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sNameSpace, sSql))

            sGL = objDBL.SQLExecuteScalar(sNameSpace, "Select gl_glCode from chart_of_accounts where gl_id = " & GrpGl & " and gl_delflag ='C' and gl_compId='" & iCompID & "'")

            aSql = "" : aSql = "Select * from acc_coa_settings where acs_acchead='" & iHead & "' and ACS_CompId='" & iCompID & "'"
            dr = objDBL.SQLDataReader(sNameSpace, aSql)
            If dr.HasRows = True Then
                dr.Read()

                If IsDBNull(dr("acs_subgroup")) = False Then
                    SubGrp = dr("acs_subgroup")
                End If

                If IsDBNull(dr("acs_accHeadPrefix")) = False Then
                    prefix = dr("acs_accHeadPrefix")
                End If

                If IsDBNull(dr("acs_group")) = False Then
                    GrpLength = dr("acs_group")
                End If
                If Grp.Length < SubGrp Then
                    Grp = Grp.PadLeft(SubGrp, "0")
                End If
            End If
            dr.Close()
            Return sGL + Grp
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCOADetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGlID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Chart_of_Accounts where gl_Id =" & iGlID & " and gl_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function SaveChartofACC(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objCOA As clsChartOfAccounts) As String
        Dim sSql As String = ""
        Dim iMax As Integer = 0
        Dim dt As New DataTable
        Try
            'To Check the Parent child Combination'
            'sSql = "" : sSql = "Select * from chart_of_accounts where gl_Desc = '" & objCOA.sgl_Desc & "' and  gl_accHead =" & objCOA.igl_AccHead & " and gl_Head =" & objCOA.igl_head & ""
            'To Check the Parent child Combination'
            sSql = "" : sSql = "Select * from chart_of_accounts where gl_Desc = '" & objCOA.sgl_Desc & "' and  gl_Parent =" & objCOA.igl_Parent & " and gl_Head =" & objCOA.igl_head & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count = 0 Then
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(gl_ID)+1,1) from Chart_of_Accounts")
                sSql = "Insert into chart_of_accounts(gl_id,gl_glcode,gl_parent,gl_desc,gl_head,"
                sSql = sSql & "gl_acchead,gl_delflag,gl_reason_Creation,"
                sSql = sSql & "gl_CrBy,gl_CrDate,gl_CompId,"
                sSql = sSql & "gl_Status,gl_IPAddress) values "
                sSql = sSql & "(" & iMax & ",'" & (objCOA.sgl_glcode) & "'," & (objCOA.igl_Parent) & ","
                sSql = sSql & "'" & objGen.SafeSQL(objCOA.sgl_Desc) & "'," & objCOA.igl_head & ","
                sSql = sSql & "" & (objCOA.igl_AccHead) & ",'" & (objCOA.sgl_Delflag) & "',"
                sSql = sSql & "'" & objGen.SafeSQL(objCOA.sgl_reason_Creation) & "',"
                sSql = sSql & "" & objCOA.igl_Crby & ",GetDate(),"
                sSql = sSql & "" & iCompID & ",'" & objCOA.sgl_Status & "','" & objCOA.sgl_IPAddress & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax & "," & 0
            Else
                iMax = dt.Rows(0)("gl_id").ToString()
                Return iMax & "," & 1
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateChartofAcc(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objCOA As clsChartOfAccounts) As String
        Dim sSql As String = ""
        Dim iMax As Integer = 0
        Try
            sSql = "Update Chart_of_Accounts set gl_desc = '" & objGen.SafeSQL(objCOA.sgl_Desc) & "',gl_reason_Creation='" & objGen.SafeSQL(objCOA.sgl_reason_Creation) & "',"
            sSql = sSql & "gl_IPAddress='" & objCOA.sgl_IPAddress & "',"
            sSql = sSql & "gl_UpdatedBy=" & objCOA.igl_UpdatedBy & ",gl_UpdatedOn=GetDate() "
            sSql = sSql & "where gl_id = " & objCOA.igl_id & " and gl_CompID =" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            iMax = objCOA.igl_id
            Return iMax & "," & 1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ApproveChartOFAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iGliD As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Chart_of_Accounts set gl_Status ='A', gl_AppBy =" & iUserID & ", gl_AppOn=GetDate() where gl_id =" & iGliD & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ActiveChartOFAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iGliD As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Chart_of_Accounts set gl_Status ='A', gl_AppBy =" & iUserID & ", gl_AppOn=GetDate() where gl_id =" & iGliD & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeActiveChartOFAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iGliD As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Chart_of_Accounts set gl_Status ='D', gl_AppBy =" & iUserID & ", gl_AppOn=GetDate() where gl_id =" & iGliD & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetchartofAccountPath(ByVal sNameSpace As String, ByVal iCompId As Integer, ByVal gl_id As Integer) As String
        Dim sPath As String = "", sSql As String = ""
        Dim iParent As Integer
        Dim myDataset As New DataSet
        Try
            sSql = "" : sSql = "Select gl_parent,gl_desc from Chart_Of_Accounts where gl_id = " & gl_id & " and  gl_CompId=" & iCompId & ""
            myDataset = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If myDataset.Tables(0).Rows.Count > 0 Then
                iParent = Convert.ToInt16(myDataset.Tables(0).Rows(0)("gl_parent").ToString())
                sPath = myDataset.Tables(0).Rows(0)("gl_desc").ToString()
                If iParent <> 0 Then
                    sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iCompId & ""
                    myDataset = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                    If myDataset.Tables(0).Rows.Count > 0 Then
                        sPath = myDataset.Tables(0).Rows(0)("gl_desc").ToString() & "/" & sPath
                        iParent = Convert.ToInt16(myDataset.Tables(0).Rows(0)("gl_parent").ToString())
                        If iParent <> 0 Then
                            sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iCompId & ""
                            myDataset = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                            If myDataset.Tables(0).Rows.Count > 0 Then
                                sPath = myDataset.Tables(0).Rows(0)("gl_desc").ToString() & "/" & sPath
                                iParent = Convert.ToInt16(myDataset.Tables(0).Rows(0)("gl_parent").ToString())
                                If iParent <> 0 Then
                                    sSql = "" : sSql = "Select gl_desc,gl_parent from Chart_Of_Accounts where gl_id=" & iParent & " and  gl_CompId=" & iCompId & ""
                                    myDataset = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                                    If myDataset.Tables(0).Rows.Count > 0 Then
                                        sPath = myDataset.Tables(0).Rows(0)("gl_desc").ToString() & "/" & sPath
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Return sPath
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetChartOfAccountsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGlID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Chart_Of_Accounts where gl_id =" & iGlID & " and gl_CompID =" & iCompID & " and gl_Delflag ='C'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadParent(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGlID As Integer) As Integer
        Dim sSql As String = ""
        Dim iParent As Integer = 0
        Try
            sSql = "" : sSql = "Select gl_Parent from Chart_Of_Accounts where gl_id =" & iGlID & " and gl_CompID =" & iCompID & " and gl_Delflag ='C'"
            iParent = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iParent
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadStandardChartOfAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id,gl_parent,gl_desc from Standard_chart_of_Accounts where gl_CompId=" & iCompID & " and gl_id <> 0  order by gl_Desc Asc"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub ImportCOA(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "truncate table Chart_Of_Accounts"
            objDBL.SQLExecuteNonQuery(sAC, sSql)


            sSql = "" : sSql = "Insert Into Chart_Of_Accounts Select gl_id,gl_glcode, gl_parent, gl_desc, gl_head, gl_reason, gl_subglexist, gl_delflag, gl_acchead, "
            sSql = sSql & " gl_reason_Creation," & iUserID & ", GetDate(), gl_SortOrder, " & iACID & "," & iUserID & ", GetDate(), gl_Status, gl_AccType, gl_Orderby, gl_updatedby, gl_updatedon, "
            sSql = sSql & " gl_operation,'" & sIPAddress & "'"
            sSql = sSql & " From Standard_chart_of_Accounts Where gl_id <> 0"
            sSql = sSql & " Group BY gl_id,gl_glcode, gl_parent, gl_desc, gl_head, gl_reason, gl_subglexist, gl_delflag, gl_acchead,gl_reason_Creation, gl_SortOrder,gl_Status, gl_AccType, gl_Orderby, gl_updatedby, gl_updatedon, gl_operation"

            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Function ChartOfAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
    '    Dim sSql As String = "", aSql As String = "", iSql As String = ""
    '    Dim mSql As String = "", kSql As String = "", lSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dtGL As New DataTable, dtSubGL As New DataTable
    '    Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
    '    Dim dRow As DataRow
    '    Dim dtOB As New DataTable
    '    Dim dtTrans As New DataTable
    '    Dim sStr As String = ""

    '    Try
    '        dt.Columns.Add("gl_id")
    '        dt.Columns.Add("gl_Code")
    '        dt.Columns.Add("gl_Desc")
    '        dt.Columns.Add("gl_Head")
    '        dt.Columns.Add("gl_AccHead")
    '        dt.Columns.Add("gl_Parent")

    '        sSql = "" : sSql = "Select Distinct(A.gl_id) As gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
    '        sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
    '        sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
    '        sSql = sSql & "and A.gl_head in (2) and A.gl_Delflag='C' and A.gl_Status ='A' order by A.gl_glcode"
    '        dtGL = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dtGL.Rows.Count > 0 Then
    '            For i = 0 To dtGL.Rows.Count - 1

    '                dRow = dt.NewRow()

    '                If IsDBNull(dtGL.Rows(i)("gl_id").ToString()) = False Then
    '                    dRow("gl_id") = dtGL.Rows(i)("gl_id").ToString()
    '                End If

    '                'If dtGL.Rows(i)("gl_glCode").ToString() = "A020301" Then
    '                '    dRow("gl_id") = ""
    '                'End If

    '                If IsDBNull(dtGL.Rows(i)("gl_glCode").ToString()) = False Then
    '                    dRow("gl_Code") = "<B>" & dtGL.Rows(i)("gl_glCode").ToString() & "</B>"
    '                End If

    '                'If (dtGL.Rows(i)("gl_glCode").ToString() = "A020202" Or dtGL.Rows(i)("gl_glCode").ToString() = "L030201") Then
    '                '    If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
    '                '        dRow("gl_Desc") = "<B>" & dtGL.Rows(i)("Gl_Desc").ToString() & "</B>"
    '                '    End If
    '                'Else
    '                '    If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
    '                '        dRow("gl_Desc") = dtGL.Rows(i)("Gl_Desc").ToString()
    '                '    End If
    '                'End If

    '                If IsDBNull(dtGL.Rows(i)("Gl_Desc").ToString()) = False Then
    '                    dRow("gl_Desc") = "<B>" & dtGL.Rows(i)("Gl_Desc").ToString() & "</B>"
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("gl_head").ToString()) = False Then
    '                    dRow("gl_Head") = dtGL.Rows(i)("gl_head").ToString()
    '                End If

    '                If IsDBNull(dtGL.Rows(i)("gl_Parent").ToString()) = False Then
    '                    dRow("gl_Parent") = dtGL.Rows(i)("gl_Parent").ToString()
    '                End If

    '                dt.Rows.Add(dRow)


    '                '' SUb GL
    '                mSql = "" : mSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGL.Rows(i)("gl_id").ToString() & " and gl_CompID =" & iCompID & " and "
    '                mSql = mSql & "gl_Delflag='C' and gl_Status ='A' Order by gl_glcode"
    '                dtSubGL = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
    '                If dtSubGL.Rows.Count > 0 Then
    '                    For j = 0 To dtSubGL.Rows.Count - 1

    '                        dRow = dt.NewRow()
    '                        If IsDBNull(dtSubGL.Rows(j)("gl_id").ToString()) = False Then
    '                            dRow("gl_id") = dtSubGL.Rows(j)("gl_id").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_glCode").ToString()) = False Then
    '                            dRow("gl_Code") = dtSubGL.Rows(j)("gl_glCode").ToString()
    '                        End If

    '                        'If IsDBNull(dtSubGL.Rows(j)("Gl_Desc").ToString()) = False Then
    '                        '    dRow("gl_Desc") = dtSubGL.Rows(j)("Gl_Desc").ToString()
    '                        'End If
    '                        If (dtSubGL.Rows(j)("gl_Parent").ToString() = "141" Or dtSubGL.Rows(j)("gl_Parent").ToString() = "67") Then
    '                            If IsDBNull(dtSubGL.Rows(j)("Gl_Desc").ToString()) = False Then
    '                                dRow("gl_Desc") = "<b>" & dtSubGL.Rows(j)("Gl_Desc").ToString() & "</b>"
    '                            End If
    '                        Else
    '                            If IsDBNull(dtSubGL.Rows(j)("Gl_Desc").ToString()) = False Then
    '                                dRow("gl_Desc") = dtSubGL.Rows(j)("Gl_Desc").ToString()
    '                            End If
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_head").ToString()) = False Then
    '                            dRow("gl_Head") = dtSubGL.Rows(j)("gl_head").ToString()
    '                        End If

    '                        If IsDBNull(dtSubGL.Rows(j)("gl_Parent").ToString()) = False Then
    '                            dRow("gl_Parent") = dtSubGL.Rows(j)("gl_Parent").ToString()
    '                        End If

    '                        dt.Rows.Add(dRow)
    '                    Next

    '                End If
    '            Next
    '        End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function ChartOfAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = "", aSql As String = "", iSql As String = ""
        Dim mSql As String = "", kSql As String = "", lSql As String = ""
        Dim dt As New DataTable
        Dim dtGL As New DataTable, dtSubGL As New DataTable
        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
        Dim dRow As DataRow
        Dim dtOB As New DataTable
        Dim dtTrans As New DataTable
        Dim sStr As String = ""
        Dim dtGrp, dtSubGrp As New DataTable
        Try
            dt.Columns.Add("gl_id")
            dt.Columns.Add("gl_Code")
            dt.Columns.Add("gl_Desc")
            dt.Columns.Add("gl_Head")
            dt.Columns.Add("gl_AccHead")
            dt.Columns.Add("gl_Parent")


            sSql = "" : sSql = "Select Distinct(A.gl_id) As gl_id,A.gl_glCode,A.gl_Parent,A.Gl_Desc,A.gl_head,A.gl_AccHead "
            sSql = sSql & "from chart_of_Accounts A join chart_of_Accounts B On "
            sSql = sSql & "A.gl_glcode = B.gl_glcode And A.gl_head = B.gl_head And A.gl_glcode <> '' "
            sSql = sSql & "and A.gl_head in (0) and A.gl_Delflag='C' and A.gl_Status ='A' order by A.gl_glcode"
            dtGrp = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtGrp.Rows.Count > 0 Then
                For i = 0 To dtGrp.Rows.Count - 1
                    dRow = dt.NewRow()
                    'Group
                    If IsDBNull(dtGrp.Rows(i)("gl_id").ToString()) = False Then
                        dRow("gl_id") = dtGrp.Rows(i)("gl_id").ToString()
                    End If

                    If IsDBNull(dtGrp.Rows(i)("gl_glCode").ToString()) = False Then
                        dRow("gl_Code") = "<B>" & dtGrp.Rows(i)("gl_glCode").ToString() & "</B>"
                    End If

                    If IsDBNull(dtGrp.Rows(i)("Gl_Desc").ToString()) = False Then
                        dRow("gl_Desc") = "<B>" & dtGrp.Rows(i)("Gl_Desc").ToString() & "</B>"
                    End If

                    If IsDBNull(dtGrp.Rows(i)("gl_head").ToString()) = False Then
                        dRow("gl_Head") = dtGrp.Rows(i)("gl_head").ToString()
                    End If

                    If IsDBNull(dtGrp.Rows(i)("gl_Parent").ToString()) = False Then
                        dRow("gl_Parent") = dtGrp.Rows(i)("gl_Parent").ToString()
                    End If

                    dt.Rows.Add(dRow)

                    '' SUb Group
                    mSql = "" : mSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGrp.Rows(i)("gl_id").ToString() & " and gl_Head=1 and gl_CompID =" & iCompID & " and "
                    mSql = mSql & "gl_Delflag='C' and gl_Status ='A' Order by gl_glcode"
                    dtSubGrp = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                    If dtSubGrp.Rows.Count > 0 Then
                        For j = 0 To dtSubGrp.Rows.Count - 1

                            dRow = dt.NewRow()
                            If IsDBNull(dtSubGrp.Rows(j)("gl_id").ToString()) = False Then
                                dRow("gl_id") = dtSubGrp.Rows(j)("gl_id").ToString()
                            End If

                            If IsDBNull(dtSubGrp.Rows(j)("gl_glCode").ToString()) = False Then
                                dRow("gl_Code") = dtSubGrp.Rows(j)("gl_glCode").ToString()
                            End If

                            If (dtSubGrp.Rows(j)("gl_Parent").ToString() = "141" Or dtSubGrp.Rows(j)("gl_Parent").ToString() = "67") Then
                                If IsDBNull(dtSubGrp.Rows(j)("Gl_Desc").ToString()) = False Then
                                    dRow("gl_Desc") = "<b>" & dtSubGrp.Rows(j)("Gl_Desc").ToString() & "</b>"
                                End If
                            Else
                                If IsDBNull(dtSubGrp.Rows(j)("Gl_Desc").ToString()) = False Then
                                    dRow("gl_Desc") = dtSubGrp.Rows(j)("Gl_Desc").ToString()
                                End If
                            End If

                            If IsDBNull(dtSubGrp.Rows(j)("gl_head").ToString()) = False Then
                                dRow("gl_Head") = dtSubGrp.Rows(j)("gl_head").ToString()
                            End If

                            If IsDBNull(dtSubGrp.Rows(j)("gl_Parent").ToString()) = False Then
                                dRow("gl_Parent") = dtSubGrp.Rows(j)("gl_Parent").ToString()
                            End If

                            dt.Rows.Add(dRow)

                            '' GL
                            mSql = "" : mSql = "Select * from chart_of_Accounts where gl_Parent = " & dtSubGrp.Rows(j)("gl_id").ToString() & " and gl_Head=2 and gl_CompID =" & iCompID & " and "
                            mSql = mSql & "gl_Delflag='C' and gl_Status ='A' Order by gl_glcode"
                            dtGL = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                            If dtGL.Rows.Count > 0 Then
                                For m = 0 To dtGL.Rows.Count - 1

                                    dRow = dt.NewRow()
                                    If IsDBNull(dtGL.Rows(m)("gl_id").ToString()) = False Then
                                        dRow("gl_id") = dtGL.Rows(m)("gl_id").ToString()
                                    End If

                                    If IsDBNull(dtGL.Rows(m)("gl_glCode").ToString()) = False Then
                                        dRow("gl_Code") = dtGL.Rows(m)("gl_glCode").ToString()
                                    End If

                                    If (dtGL.Rows(m)("gl_Parent").ToString() = "141" Or dtGL.Rows(m)("gl_Parent").ToString() = "67") Then
                                        If IsDBNull(dtGL.Rows(m)("Gl_Desc").ToString()) = False Then
                                            dRow("gl_Desc") = "<b>" & dtGL.Rows(m)("Gl_Desc").ToString() & "</b>"
                                        End If
                                    Else
                                        If IsDBNull(dtGL.Rows(m)("Gl_Desc").ToString()) = False Then
                                            dRow("gl_Desc") = dtGL.Rows(m)("Gl_Desc").ToString()
                                        End If
                                    End If

                                    If IsDBNull(dtGL.Rows(m)("gl_head").ToString()) = False Then
                                        dRow("gl_Head") = dtGL.Rows(m)("gl_head").ToString()
                                    End If

                                    If IsDBNull(dtGL.Rows(m)("gl_Parent").ToString()) = False Then
                                        dRow("gl_Parent") = dtGL.Rows(m)("gl_Parent").ToString()
                                    End If

                                    dt.Rows.Add(dRow)


                                    '' Sub GL
                                    mSql = "" : mSql = "Select * from chart_of_Accounts where gl_Parent = " & dtGL.Rows(m)("gl_id").ToString() & " and gl_Head=3 and gl_CompID =" & iCompID & " and "
                                    mSql = mSql & "gl_Delflag='C' and gl_Status ='A' Order by gl_glcode"
                                    dtSubGL = objDBL.SQLExecuteDataSet(sNameSpace, mSql).Tables(0)
                                    If dtSubGL.Rows.Count > 0 Then
                                        For k = 0 To dtSubGL.Rows.Count - 1

                                            dRow = dt.NewRow()
                                            If IsDBNull(dtSubGL.Rows(k)("gl_id").ToString()) = False Then
                                                dRow("gl_id") = dtSubGL.Rows(k)("gl_id").ToString()
                                            End If

                                            If IsDBNull(dtSubGL.Rows(k)("gl_glCode").ToString()) = False Then
                                                dRow("gl_Code") = dtSubGL.Rows(k)("gl_glCode").ToString()
                                            End If

                                            If (dtSubGL.Rows(k)("gl_Parent").ToString() = "141" Or dtSubGL.Rows(k)("gl_Parent").ToString() = "67") Then
                                                If IsDBNull(dtSubGL.Rows(k)("Gl_Desc").ToString()) = False Then
                                                    dRow("gl_Desc") = "<b>" & dtSubGL.Rows(k)("Gl_Desc").ToString() & "</b>"
                                                End If
                                            Else
                                                If IsDBNull(dtSubGL.Rows(k)("Gl_Desc").ToString()) = False Then
                                                    dRow("gl_Desc") = dtSubGL.Rows(k)("Gl_Desc").ToString()
                                                End If
                                            End If

                                            If IsDBNull(dtSubGL.Rows(k)("gl_head").ToString()) = False Then
                                                dRow("gl_Head") = dtSubGL.Rows(k)("gl_head").ToString()
                                            End If

                                            If IsDBNull(dtSubGL.Rows(k)("gl_Parent").ToString()) = False Then
                                                dRow("gl_Parent") = dtSubGL.Rows(k)("gl_Parent").ToString()
                                            End If

                                            dt.Rows.Add(dRow)
                                        Next

                                    End If


                                Next

                            End If


                        Next

                    End If

                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
