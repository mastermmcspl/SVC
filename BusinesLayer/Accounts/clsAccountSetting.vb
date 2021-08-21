Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsAccountSetting
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Private iAS_ID As Integer
    Private iAS_CompanyIID As Integer
    Private iAS_ZoneID As Integer
    Private iAS_RegionID As Integer
    Private iAS_AreaID As Integer
    Private iAS_BranchID As Integer
    Private iAS_CreatedBy As Integer
    Private iAS_UpdatedBy As Integer
    Private sAS_DelFlag As String
    Private sAS_Status As String
    Private iAS_YearID As Integer
    Private iAS_CompID As Integer
    Private sAS_Opeartion As String
    Private sAS_IPAddress As String
    Public Property AS_ID() As Integer
        Get
            Return (iAS_ID)
        End Get
        Set(ByVal Value As Integer)
            iAS_ID = Value
        End Set
    End Property
    Public Property AS_CompanyIID() As Integer
        Get
            Return (iAS_CompanyIID)
        End Get
        Set(ByVal Value As Integer)
            iAS_CompanyIID = Value
        End Set
    End Property
    Public Property AS_ZoneID() As Integer
        Get
            Return (iAS_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            iAS_ZoneID = Value
        End Set
    End Property
    Public Property AS_RegionID() As Integer
        Get
            Return (iAS_RegionID)
        End Get
        Set(ByVal Value As Integer)
            iAS_RegionID = Value
        End Set
    End Property
    Public Property AS_AreaID() As Integer
        Get
            Return (iAS_AreaID)
        End Get
        Set(ByVal Value As Integer)
            iAS_AreaID = Value
        End Set
    End Property
    Public Property AS_BranchID() As Integer
        Get
            Return (iAS_BranchID)
        End Get
        Set(ByVal Value As Integer)
            iAS_BranchID = Value
        End Set
    End Property
    Public Property AS_CreatedBy() As Integer
        Get
            Return (iAS_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAS_CreatedBy = Value
        End Set
    End Property
    Public Property AS_UpdatedBy() As Integer
        Get
            Return (iAS_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAS_UpdatedBy = Value
        End Set
    End Property
    Public Property AS_DelFlag() As String
        Get
            Return (sAS_DelFlag)
        End Get
        Set(ByVal Value As String)
            sAS_DelFlag = Value
        End Set
    End Property
    Public Property AS_Status() As String
        Get
            Return (sAS_Status)
        End Get
        Set(ByVal Value As String)
            sAS_Status = Value
        End Set
    End Property
    Public Property AS_YearID() As Integer
        Get
            Return (iAS_YearID)
        End Get
        Set(ByVal Value As Integer)
            iAS_YearID = Value
        End Set
    End Property
    Public Property AS_CompID() As Integer
        Get
            Return (iAS_CompID)
        End Get
        Set(ByVal Value As Integer)
            iAS_CompID = Value
        End Set
    End Property
    Public Property AS_Opeartion() As String
        Get
            Return (sAS_Opeartion)
        End Get
        Set(ByVal Value As String)
            sAS_Opeartion = Value
        End Set
    End Property
    Public Property AS_IPAddress() As String
        Get
            Return (sAS_IPAddress)
        End Get
        Set(ByVal Value As String)
            sAS_IPAddress = Value
        End Set
    End Property
    Public Function GetAccDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Account_Settings Where AS_CompanyIID = " & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccZone(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent=0 and Org_CompID=" & iCompID & " )"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccRgn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAccZone > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=2 And Org_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccArea(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccRgn As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAccRgn > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccRgn & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=3 And Org_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccBrnch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccarea As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAccarea > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccarea & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=4 And Org_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveChartofACC(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objAccSetting As clsAccountSetting)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim iRet As Integer
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_CompanyIID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAccSetting.AS_CompanyIID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAccSetting.AS_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAccSetting.AS_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAccSetting.AS_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAccSetting.AS_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_CreatedBy", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAccSetting.AS_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objAccSetting.AS_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAccSetting.AS_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_YearID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAccSetting.AS_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_CompID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAccSetting.AS_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AS_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAccSetting.AS_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iRet = objDBL.ExecuteSPForInsert(sNameSpace, "spAccount_Settings", "@iOper", ObjParam)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteChartofACC(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = ""
        Dim iMax As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "Delete from Account_Settings where AS_CompanyIID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
