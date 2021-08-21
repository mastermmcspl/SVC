Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsFixedAssetMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iFAM_ID As Integer
    Private iFAM_DespreaciationType As Integer
    Private iFAM_glDescID As Integer
    Private sFAM_DepRate As String
    Private sFAM_Status As String
    Private iFAM_CompID As Integer
    Private iFAM_YearID As Integer
    Private iFAM_CreatedBy As Integer
    Private dFAM_CreatedOn As DateTime
    Private iFAM_UpdatedBy As Integer
    Private dFAM_UpdatedOn As DateTime
    Private sFAM_Operation As String
    Private sFAM_IPAddress As String


    Public Property FAM_ID() As Integer
        Get
            Return (iFAM_ID)
        End Get
        Set(ByVal Value As Integer)
            iFAM_ID = Value
        End Set
    End Property
    Public Property FAM_DespreaciationType() As Integer
        Get
            Return (iFAM_DespreaciationType)
        End Get
        Set(ByVal Value As Integer)
            iFAM_DespreaciationType = Value
        End Set
    End Property
    Public Property FAM_glDescID() As Integer
        Get
            Return (iFAM_glDescID)
        End Get
        Set(ByVal Value As Integer)
            iFAM_glDescID = Value
        End Set
    End Property
    Public Property FAM_DepRate() As String
        Get
            Return (sFAM_DepRate)
        End Get
        Set(ByVal Value As String)
            sFAM_DepRate = Value
        End Set
    End Property
    Public Property FAM_Status() As String
        Get
            Return (sFAM_Status)
        End Get
        Set(ByVal Value As String)
            sFAM_Status = Value
        End Set
    End Property
    Public Property FAM_CompID() As Integer
        Get
            Return (iFAM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iFAM_CompID = Value
        End Set
    End Property
    Public Property FAM_YearID() As Integer
        Get
            Return (iFAM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iFAM_YearID = Value
        End Set
    End Property
    Public Property FAM_CreatedBy() As Integer
        Get
            Return (iFAM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iFAM_CreatedBy = Value
        End Set
    End Property
    Public Property FAM_CreatedOn() As DateTime
        Get
            Return (dFAM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dFAM_CreatedOn = Value
        End Set
    End Property
    Public Property FAM_UpdatedBy() As Integer
        Get
            Return (iFAM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iFAM_UpdatedBy = Value
        End Set
    End Property
    Public Property FAM_UpdatedOn() As DateTime
        Get
            Return (dFAM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dFAM_UpdatedOn = Value
        End Set
    End Property
    Public Property FAM_Operation() As String
        Get
            Return (sFAM_Operation)
        End Get
        Set(ByVal Value As String)
            sFAM_Operation = Value
        End Set
    End Property
    Public Property FAM_IPAddress() As String
        Get
            Return (sFAM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sFAM_IPAddress = Value
        End Set
    End Property

    Public Function BindCADesc(ByVal sNameSpace As String, iCompID As Integer, iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select gl_ID,gl_Desc From Chart_Of_Accounts Where gl_CompID =" & iCompID & " and gl_Status='A' and gl_Delflag='C' And gl_acchead=1 And gl_Parent in (Select gl_ID From Chart_Of_Accounts Where gl_acchead=1 And gl_Parent In (Select gl_ID From Chart_Of_Accounts Where gl_CompID =" & iCompID & " and gl_Desc='Fixed Assets' and gl_Delflag='C' and gl_Status='A')) And gl_Desc Not Like 'Depreciation - %' Order By gl_Desc "
            BindCADesc = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindCADesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFixedMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objFAM As clsFixedAssetMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAM.FAM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_DespreaciationType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAM.FAM_DespreaciationType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_glDescID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAM.FAM_glDescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_DepRate", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFAM.FAM_DepRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFAM.FAM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAM.FAM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAM.FAM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAM.FAM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFAM.FAM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAM.FAM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFAM.FAM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objFAM.FAM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@FAM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFAM.FAM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spFixed_Asset_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("DepType")
            dtTab.Columns.Add("glDesc")
            dtTab.Columns.Add("DepRate")

            sSql = "Select FAM_ID,FAM_DespreaciationType,FAM_glDescID,FAM_DepRate From Fixed_Asset_Master Where FAM_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ID") = dt.Rows(i)("FAM_ID")
                    If dt.Rows(i)("FAM_DespreaciationType") = 1 Then
                        dRow("DepType") = "Straight Line"
                    ElseIf dt.Rows(i)("FAM_DespreaciationType") = 2 Then
                        dRow("DepType") = "Written Down"
                    ElseIf dt.Rows(i)("FAM_DespreaciationType") = 3 Then
                        dRow("DepType") = "2nd Shift & 3rd Shift"
                    End If
                    dRow("glDesc") = objDBL.SQLGetDescription(sNameSpace, "Select gl_Desc From Chart_Of_Accounts Where gl_id=" & dt.Rows(i)("FAM_glDescID") & " ")
                    dRow("DepRate") = dt.Rows(i)("FAM_DepRate")

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
