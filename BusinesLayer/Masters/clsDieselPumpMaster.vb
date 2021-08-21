Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsDieselPumpMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iLPM_ID As Integer
    Private sLPM_PumpRegNo As String
    Private sLPM_PumpName As String
    Private sLPM_ContactPerson As String
    Private sLPM_MobNo As String
    Private sLPM_Details As String
    Private sLPM_GstNo As String

    Public Property LPM_ID() As Integer
        Get
            Return (iLPM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLPM_ID = Value
        End Set
    End Property
    Public Property LPM_PumpRegNo() As String
        Get
            Return (sLPM_PumpRegNo)
        End Get
        Set(ByVal Value As String)
            sLPM_PumpRegNo = Value
        End Set
    End Property
    Public Property LPM_PumpName() As String
        Get
            Return (sLPM_PumpName)
        End Get
        Set(ByVal Value As String)
            sLPM_PumpName = Value
        End Set
    End Property
    Public Property LPM_ContactPerson() As String
        Get
            Return (sLPM_ContactPerson)
        End Get
        Set(ByVal Value As String)
            sLPM_ContactPerson = Value
        End Set
    End Property
    Public Property LPM_MobNo() As String
        Get
            Return (sLPM_MobNo)
        End Get
        Set(ByVal Value As String)
            sLPM_MobNo = Value
        End Set
    End Property
    Public Property LPM_Details() As String
        Get
            Return (sLPM_Details)
        End Get
        Set(ByVal Value As String)
            sLPM_Details = Value
        End Set
    End Property
    Public Property LPM_GstNo() As String
        Get
            Return (sLPM_GstNo)
        End Get
        Set(ByVal Value As String)
            sLPM_GstNo = Value
        End Set
    End Property


    Private sLPM_Address As String
    Private sLPM_Pincode As String
    Private iLPM_City As Integer
    Private iLPM_State As Integer

    Public Property LPM_Address() As String
        Get
            Return (sLPM_Address)
        End Get
        Set(ByVal Value As String)
            sLPM_Address = Value
        End Set
    End Property
    Public Property LPM_Pincode() As String
        Get
            Return (sLPM_Pincode)
        End Get
        Set(ByVal Value As String)
            sLPM_Pincode = Value
        End Set
    End Property
    Public Property LPM_City() As Integer
        Get
            Return (iLPM_City)
        End Get
        Set(ByVal Value As Integer)
            iLPM_City = Value
        End Set
    End Property
    Public Property LPM_state() As Integer
        Get
            Return (iLPM_State)
        End Get
        Set(ByVal Value As Integer)
            iLPM_State = Value
        End Set
    End Property

    Private sLPM_Delflag As String
    Private iLPM_CompID As Integer
    Private iLPM_YearID As Integer
    Private sLPM_Status As String
    Private sLPM_Operation As String
    Private sLPM_IPAddress As String
    Private iLPM_CreatedBy As Integer
    Private dLPM_CreatedOn As DateTime
    Private iLPM_ApprovedBy As Integer
    Private dLPM_ApprovedOn As DateTime
    Private iLPM_DeletedBy As Integer
    Private dLPM_DeletedOn As DateTime
    Private iLPM_UpdatedBy As Integer
    Private dLPM_UpdatedOn As DateTime
    Private iLPM_RecalldBy As Integer

    Public Property LPM_Delflag() As String
        Get
            Return (sLPM_Delflag)
        End Get
        Set(ByVal Value As String)
            sLPM_Delflag = Value
        End Set
    End Property
    Public Property LPM_CompID() As Integer
        Get
            Return (iLPM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLPM_CompID = Value
        End Set
    End Property
    Public Property LPM_YearID() As Integer
        Get
            Return (iLPM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLPM_YearID = Value
        End Set
    End Property
    Public Property LPM_Status() As String
        Get
            Return (sLPM_Status)
        End Get
        Set(ByVal Value As String)
            sLPM_Status = Value
        End Set
    End Property
    Public Property LPM_Operation() As String
        Get
            Return (sLPM_Operation)
        End Get
        Set(ByVal Value As String)
            sLPM_Operation = Value
        End Set
    End Property
    Public Property LPM_IPAddress() As String
        Get
            Return (sLPM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLPM_IPAddress = Value
        End Set
    End Property
    Public Property LPM_CreatedBy() As Integer
        Get
            Return (iLPM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPM_CreatedBy = Value
        End Set
    End Property
    Public Property LPM_CreatedOn() As Date
        Get
            Return (dLPM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLPM_CreatedOn = Value
        End Set
    End Property

    Public Property LPM_ApprovedBy() As Integer
        Get
            Return (iLPM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPM_ApprovedBy = Value
        End Set
    End Property
    Public Property LPM_ApprovedOn() As Date
        Get
            Return (dLPM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLPM_ApprovedOn = Value
        End Set
    End Property
    Public Property LPM_DeletedBy() As Integer
        Get
            Return (iLPM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPM_DeletedBy = Value
        End Set
    End Property
    Public Property LPM_DeletedOn() As Date
        Get
            Return (dLPM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLPM_DeletedOn = Value
        End Set
    End Property
    Public Property LPM_UpdatedBy() As Integer
        Get
            Return (iLPM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPM_UpdatedBy = Value
        End Set
    End Property
    Public Property LPM_UpdatedOn() As Date
        Get
            Return (dLPM_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLPM_UpdatedOn = Value
        End Set
    End Property
    Public Property LPM_RecalldBy() As Integer
        Get
            Return (iLPM_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLPM_RecalldBy = Value
        End Set
    End Property

    Private iLPM_Group As Integer
    Private iLPM_SubGroup As Integer
    Private iLPM_GL As Integer
    Private iLPM_SubGL As Integer

    Public Property LPM_Group() As Integer
        Get
            Return (iLPM_Group)
        End Get
        Set(ByVal Value As Integer)
            iLPM_Group = Value
        End Set
    End Property
    Public Property LPM_SubGroup() As Integer
        Get
            Return (iLPM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            iLPM_SubGroup = Value
        End Set
    End Property
    Public Property LPM_GL() As Integer
        Get
            Return (iLPM_GL)
        End Get
        Set(ByVal Value As Integer)
            iLPM_GL = Value
        End Set
    End Property
    Public Property LPM_SubGL() As Integer
        Get
            Return (iLPM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iLPM_SubGL = Value
        End Set
    End Property

    Private iLPD_ID As Integer
    Private iLPD_PumpID As Integer
    Private sLPD_AccountNo As String
    Private sLPD_BankName As String
    Private sLPD_IFSC As String
    Private sLPD_Branch As String
    Private sLPD_DelFlag As String
    Private sLPD_Status As String
    Private iLPD_CreatedBy As Integer
    Private dLPD_CreatedOn As Date
    Private iLPD_UpdatedBy As Integer
    Private dLPD_UpdatedOn As Date
    Private iLPD_CompID As Integer
    Private iLPD_YearID As Integer
    Private sLPD_Operation As String
    Private sLPD_IPAddress As String




    Public Property LPD_ID() As Integer
        Get
            Return (iLPD_ID)
        End Get
        Set(ByVal Value As Integer)
            iLPD_ID = Value
        End Set
    End Property
    Public Property LPD_PumpID() As Integer
        Get
            Return (iLPD_PumpID)
        End Get
        Set(ByVal Value As Integer)
            iLPD_PumpID = Value
        End Set
    End Property
    Public Property LPD_AccountNo() As String
        Get
            Return (sLPD_AccountNo)
        End Get
        Set(ByVal Value As String)
            sLPD_AccountNo = Value
        End Set
    End Property
    Public Property LPD_BankName() As String
        Get
            Return (sLPD_BankName)
        End Get
        Set(ByVal Value As String)
            sLPD_BankName = Value
        End Set
    End Property
    Public Property LPD_IFSC() As String
        Get
            Return (sLPD_IFSC)
        End Get
        Set(ByVal Value As String)
            sLPD_IFSC = Value
        End Set
    End Property
    Public Property LPD_Branch() As String
        Get
            Return (sLPD_Branch)
        End Get
        Set(ByVal Value As String)
            sLPD_Branch = Value
        End Set
    End Property

    Public Property LPD_DelFlag() As String
        Get
            Return (sLPD_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLPD_DelFlag = Value
        End Set
    End Property
    Public Property LPD_Status() As String
        Get
            Return (sLPD_Status)
        End Get
        Set(ByVal Value As String)
            sLPD_Status = Value
        End Set
    End Property
    Public Property LPD_CreatedBy() As Integer
        Get
            Return (iLPD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPD_CreatedBy = Value
        End Set
    End Property
    Public Property LPD_CreatedOn() As DateTime
        Get
            Return (dLPD_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLPD_CreatedOn = Value
        End Set
    End Property
    Public Property LPD_UpdatedBy() As Integer
        Get
            Return (iLPD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLPD_UpdatedBy = Value
        End Set
    End Property
    Public Property LPD_UpdatedOn() As DateTime
        Get
            Return (dLPD_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLPD_UpdatedOn = Value
        End Set
    End Property

    Public Property LPD_CompID() As Integer
        Get
            Return (iLPD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLPD_CompID = Value
        End Set
    End Property
    Public Property LPD_YearID() As Integer
        Get
            Return (iLPD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLPD_YearID = Value
        End Set
    End Property
    Public Property LPD_Operation() As String
        Get
            Return (sLPD_Operation)
        End Get
        Set(ByVal Value As String)
            sLPD_Operation = Value
        End Set
    End Property
    Public Property LPD_IPAddress() As String
        Get
            Return (sLPD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLPD_IPAddress = Value
        End Set
    End Property





    Public Function LoadCity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='City' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadState(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in(Select Mas_ID From Acc_Master_Type Where Mas_Type='State' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePumpDetails(ByVal sNameSpace As String, ByVal objLPM As clsDieselPumpMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(26) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_PumpRegNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objLPM.sLPM_PumpRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_PumpName", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objLPM.sLPM_PumpName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_ContactPerson", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objLPM.sLPM_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_MobNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLPM.sLPM_MobNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_Details", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objLPM.sLPM_Details
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_GstNo", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objLPM.sLPM_GstNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_Address", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objLPM.sLPM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_Pincode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLPM.sLPM_Pincode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_City", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_State", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLPM.sLPM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLPM.sLPM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLPM.dLPM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLPM.dLPM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_SubGroup", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPM.iLPM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_Operation", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLPM.sLPM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLPM.sLPM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Pump_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePumpBankDetails(ByVal sNameSpace As String, ByVal objLPD As clsDieselPumpMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPD.iLPD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_PumpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPD.iLPD_PumpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_AccountNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLPD.sLPD_AccountNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_BankName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLPD.sLPD_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_IFSC", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLPD.sLPD_IFSC
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_Branch", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLPD.sLPD_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLPD.sLPD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLPD.sLPD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPD.iLPD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLPD.dLPD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPD.iLPD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLPD.iLPD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLPD.sLPD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LPD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objLPD.sLPD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_PumpBank_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDieseMasterlDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iLPMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select LPM_Id,LPM_PumpRegNo,LPM_PumpName,LPM_ContactPerson,LPM_MobNo,LPM_Details,LPM_GstNo,LPM_Address,LPM_PinCode,LPM_City,LPM_State,LPM_Delflag,LPM_SubGL from Lgst_Pump_Master Where LPM_ID=" & iLPMid & "  And LPM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPumpBankDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("AccountNo")
            dt.Columns.Add("BankName")
            dt.Columns.Add("IFSCCode")
            dt.Columns.Add("BranchName")

            sSql = "Select LPD_ID,LPD_AccountNo,LPD_BankName,LPD_IFSC,LPD_Branch from Lgst_PumpBank_Details Where LPD_DelFlag<>'D' And LPD_PumpID=" & iCustomerID & " And LPD_CompID=" & iCompID & " Order by LPD_BankName"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("ID") = dr("LPD_ID")
                    dRow("AccountNo") = dr("LPD_AccountNo")
                    dRow("BankName") = dr("LPD_BankName")
                    dRow("IFSCCode") = dr("LPD_IFSC")
                    dRow("BranchName") = dr("LPD_Branch")

                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingDieselRegNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LPM_ID,LPM_PumpRegNo From Lgst_Pump_Master Where LPM_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDesielPumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DieselName", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("RegNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ContactPerson", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("MobileNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select LPM_ID,LPM_PumpName,LPM_PumpRegNo,LPM_ContactPerson,LPM_MobNo,LPM_DelFlag from Lgst_Pump_Master where LPM_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And LPM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And LPM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And LPM_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By LPM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("LPM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("LPM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LPM_PumpName").ToString()) = False Then
                        dr("DieselName") = ds.Tables(0).Rows(i)("LPM_PumpName").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LPM_PumpRegNo").ToString()) = False Then
                        dr("RegNo") = ds.Tables(0).Rows(i)("LPM_PumpRegNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LPM_ContactPerson").ToString()) = False Then
                        dr("ContactPerson") = ds.Tables(0).Rows(i)("LPM_ContactPerson").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LPM_MobNo").ToString()) = False Then
                        dr("MobileNo") = ds.Tables(0).Rows(i)("LPM_MobNo").ToString()
                    End If



                    If (ds.Tables(0).Rows(i)("LPM_DelFlag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("LPM_DelFlag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("LPM_DelFlag") = "D") Then
                        dr("Status") = "De-Activated"
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDieselMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Pump_Master Set LPM_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " LPM_Status='A',LPM_DelFlag='A',LPM_ApprovedBy= " & iUserID & " , LPM_ApprovedOn=GetDate()"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " LPM_Status='D',LPM_DelFlag='D',LPM_DeletedBy= " & iUserID & " , LPM_DeletedOn=GetDate()"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " LPM_Status='A',LPM_DelFlag='A' "
            End If
            sSql = sSql & " Where LPM_CompID=" & iCompID & " And LPM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateDieselMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Lgst_Pump_Master Where LPM_DelFlag='" & sSelectedStatus & "' And LPM_CompID=" & iCompID & "  "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Lgst_Pump_Master Set LPM_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " LPM_Status='A',LPM_DelFlag='A',LPM_ApprovedBy= " & iUserID & ",LPM_ApprovedOn=GetDate()"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " LPM_Status='D',LPM_DelFlag='D',LPM_DeletedBy= " & iUserID & ",LPM_DeletedOn=GetDate()"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " LPM_Status='A',LPM_DelFlag='A' "
                    End If
                    sSql = sSql & " Where LPM_CompID=" & iCompID & " And LPM_ID = " & iMasId & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetBankDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_PumpBank_Details Where LPD_DelFlag<>'D' And LPD_ID=" & iID & " And LPD_PumpID=" & iCustomerID & " And LPD_CompID=" & iCompID & " Order by LPD_BankName"
            GetBankDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetBankDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteBankValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iCustomerID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_PumpBank_Details Set LPD_DelFlag='D' Where LPD_ID=" & iID & " And LPD_PumpID=" & iCustomerID & " And LPD_CompID=" & iCompID & "  "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckGSTNODuplicate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sGSTNO As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Lgst_Pump_Master Where LPM_GstNo='" & sGSTNO & "' And LPM_DelFlag<>'D' And LPM_CompID=" & iCompID & "  "
            CheckGSTNODuplicate = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckGSTNODuplicate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckPumpNameDuplicate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sPumpName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Lgst_Pump_Master Where LPM_PumpName='" & sPumpName & "' And LPM_DelFlag<>'D' And LPM_CompID=" & iCompID & "  "
            CheckPumpNameDuplicate = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckPumpNameDuplicate
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckRegNoDuplicate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sPumpRegNo As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Lgst_Pump_Master Where LPM_PumpRegNo='" & sPumpRegNo & "' And LPM_DelFlag<>'D' And LPM_CompID=" & iCompID & "  "
            CheckRegNoDuplicate = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckRegNoDuplicate
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
