Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsAssetMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iAM_ID As Integer
    Private iAM_AssetID As Integer
    Private iAM_CreatedBy As Integer
    Private dAM_CreatedOn As DateTime
    Private dAM_UpdatedOn As DateTime
    Private iAM_UpdatedBy As Integer
    Private sAM_DelFlag As String
    Private sAM_Status As String
    Private iAM_YearID As Integer
    Private iAM_CompID As Integer
    Private dAM_Deprate As Double
    Private sAM_Opeartion As String
    Private sAM_IPAddress As String
    Private dAM_ITRate As Double
    Public Property AM_ID() As Integer
        Get
            Return (iAM_ID)
        End Get
        Set(ByVal Value As Integer)
            iAM_ID = Value
        End Set
    End Property
    Public Property AM_AssetID() As Integer
        Get
            Return (iAM_AssetID)
        End Get
        Set(ByVal Value As Integer)
            iAM_AssetID = Value
        End Set
    End Property
    Public Property AM_CreatedBy() As Integer
        Get
            Return (iAM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAM_CreatedBy = Value
        End Set
    End Property

    Public Property AM_CreatedOn() As DateTime
        Get
            Return (dAM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAM_CreatedOn = Value
        End Set
    End Property
    Public Property AM_UpdatedOn() As DateTime
        Get
            Return (dAM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAM_UpdatedOn = Value
        End Set
    End Property
    Public Property AM_UpdatedBy() As Integer
        Get
            Return (iAM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAM_UpdatedBy = Value
        End Set
    End Property
    Public Property AM_DelFlag() As String
        Get
            Return (sAM_DelFlag)
        End Get
        Set(ByVal Value As String)
            sAM_DelFlag = Value
        End Set
    End Property
    Public Property AM_Status() As String
        Get
            Return (sAM_Status)
        End Get
        Set(ByVal Value As String)
            sAM_Status = Value
        End Set
    End Property
    Public Property AM_YearID() As Integer
        Get
            Return (iAM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iAM_YearID = Value
        End Set
    End Property
    Public Property AM_CompID() As Integer
        Get
            Return (iAM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iAM_CompID = Value
        End Set
    End Property
    Public Property AM_Deprate() As Double
        Get
            Return (dAM_Deprate)
        End Get
        Set(ByVal Value As Double)
            dAM_Deprate = Value
        End Set
    End Property


    Public Property AM_Opeartion() As String
        Get
            Return (sAM_Opeartion)
        End Get
        Set(ByVal Value As String)
            sAM_Opeartion = Value
        End Set
    End Property
    Public Property AM_IPAddress() As String
        Get
            Return (sAM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sAM_IPAddress = Value
        End Set
    End Property

    Public Property AM_ITRate() As String
        Get
            Return (dAM_ITRate)
        End Get
        Set(ByVal Value As String)
            dAM_ITRate = Value
        End Set
    End Property

    Public Function LoadAssets(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select GL_Desc,GL_ID From Chart_Of_Accounts Where GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Fixed assets'))"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objAsst As ClsAssetMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_AssetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_AssetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsst.AM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsst.AM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objAsst.AM_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsst.AM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Deprate", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objAsst.AM_Deprate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsst.AM_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsst.AM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ITRate", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsst.AM_ITRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_AssetMaster", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AssetRetrieve(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAM_AsstType As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select * from Acc_AssetMaster Where AM_AssetID=" & iAM_AsstType & " And AM_CompID=" & iCompID & " And AM_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetDescriptionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDescID As Integer, ByVal iMasterType As Integer, ByVal iAsset As Integer) As DataTable
    '    Dim dt As New DataTable
    '    Dim sSql As String = ""
    '    Dim dbcheck As New Boolean
    '    Try


    '        If iAsset = 1 Then
    '            sSql = "Select * from ACC_General_Master where Mas_desc ='" & iDescID & "' and Mas_master = " & iMasterType & " and Mas_CompID =" & iCompID & " "
    '            dbcheck = objDB.SQLCheckForRecord(sNameSpace, sSql)

    '            If dbcheck = True Then

    '                sSql = "Select * from ACC_General_Master where Mas_desc ='" & iDescID & "' and Mas_master = " & iMasterType & " and Mas_CompID =" & iCompID & " "
    '                dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '            End If
    '        Else

    '            sSql = "Select * from ACC_General_Master where Mas_id =" & iDescID & " and Mas_master = " & iMasterType & " and Mas_CompID =" & iCompID & " "

    '            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function CheckDepreciationRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAssettype As String, ByVal dDeprate As Double, ByVal objAsst As ClsAssetMaster) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dcheck As New Boolean
        Dim AssetID As New Integer
        Dim iID As String = ""
        Dim iMax As Integer = 0
        Try

            sSql = "Select GL_ID From Chart_Of_Accounts Where GL_DESC='" & sAssettype & "' And GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Fixed assets'))"

            AssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            sSql = "select * from Acc_AssetMaster where AM_Deprate=" & dDeprate & " and AM_AssetID=" & AssetID & ""
            dcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If dcheck = True Then

            Else
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(AM_ID)+1,1) from Acc_AssetMaster")
                sSql = "insert into Acc_AssetMaster(AM_ID,AM_AssetID,AM_CreatedBy,AM_CreatedOn,AM_DelFlag ,"
                sSql = sSql & " AM_Status,AM_YearID,AM_CompID,AM_Deprate,AM_Opeartion,AM_IPAddress) values (" & iMax & "," & AssetID & ","
                sSql = sSql & " " & objAsst.AM_CreatedBy & ",getdate(),'X','W'," & iYearID & "," & iCompID & "," & dDeprate & ",'C','" & objAsst.AM_IPAddress & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
