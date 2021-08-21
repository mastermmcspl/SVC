Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsVehicleAddtlnMaster

    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iLVAM_ID As Integer
    Private iLVAM_MasterID As Integer
    Private sLVAM_RegNo As String
    Private dLVAM_VehiclePurchaseDate As DateTime
    Private dLVAM_TotalMeterValue As Double
    Private sLVAM_VehicleInvoiceNo As String
    Private dLVAM_VehicleAmt As Double
    Private sLVAM_VehicleDealer As String
    Private sLVAM_VehicleManufacturer As String
    Private dLVAM_DepreciationAmt As Double
    Private sLVAM_BatteryNo As String
    Private dLVAM_BatteryFreq As Double

    Private sLVAM_Delflag As String
    Private iLVAM_CompID As Integer
    Private iLVAM_YearID As Integer
    Private sLVAM_Status As String
    Private sLVAM_Operation As String
    Private sLVAM_IPAddress As String
    Private iLVAM_CreatedBy As Integer
    Private dLVAM_CreatedOn As DateTime
    Private iLVAM_ApprovedBy As Integer
    Private dLVAM_ApprovedOn As DateTime
    Private iLVAM_DeletedBy As Integer
    Private dLVAM_DeletedOn As DateTime
    Private iLVAM_UpdatedBy As Integer
    Private dLVAM_UpdatedOn As DateTime
    Private iLVAM_RecalldBy As Integer

    Public Property LVAM_ID() As Integer
        Get
            Return (iLVAM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_ID = Value
        End Set
    End Property
    Public Property LVAM_MasterID() As Integer
        Get
            Return (iLVAM_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_MasterID = Value
        End Set
    End Property

    Public Property LVAM_RegNo() As String
        Get
            Return (sLVAM_RegNo)
        End Get
        Set(ByVal Value As String)
            sLVAM_RegNo = Value
        End Set
    End Property
    Public Property LVAM_VehiclePurchaseDate() As Date
        Get
            Return (dLVAM_VehiclePurchaseDate)
        End Get
        Set(ByVal Value As Date)
            dLVAM_VehiclePurchaseDate = Value
        End Set
    End Property
    Public Property LVAM_TotalMeterValue() As Double
        Get
            Return (dLVAM_TotalMeterValue)
        End Get
        Set(ByVal Value As Double)
            dLVAM_TotalMeterValue = Value
        End Set
    End Property
    Public Property LVAM_VehicleInvoiceNo() As String
        Get
            Return (sLVAM_VehicleInvoiceNo)
        End Get
        Set(ByVal Value As String)
            sLVAM_VehicleInvoiceNo = Value
        End Set
    End Property
    Public Property LVAM_VehicleAmt() As Double
        Get
            Return (dLVAM_VehicleAmt)
        End Get
        Set(ByVal Value As Double)
            dLVAM_VehicleAmt = Value
        End Set
    End Property
    Public Property LVAM_VehicleDealer() As String
        Get
            Return (sLVAM_VehicleDealer)
        End Get
        Set(ByVal Value As String)
            sLVAM_VehicleDealer = Value
        End Set
    End Property
    Public Property LVAM_VehicleManufacturer() As String
        Get
            Return (sLVAM_VehicleManufacturer)
        End Get
        Set(ByVal Value As String)
            sLVAM_VehicleManufacturer = Value
        End Set
    End Property

    Public Property LVAM_DepreciationAmt() As Double
        Get
            Return (dLVAM_DepreciationAmt)
        End Get
        Set(ByVal Value As Double)
            dLVAM_DepreciationAmt = Value
        End Set
    End Property
    Public Property LVAM_BatteryNo() As String
        Get
            Return (sLVAM_BatteryNo)
        End Get
        Set(ByVal Value As String)
            sLVAM_BatteryNo = Value
        End Set
    End Property
    Public Property LVAM_BatteryFreq() As Double
        Get
            Return (dLVAM_BatteryFreq)
        End Get
        Set(ByVal Value As Double)
            dLVAM_BatteryFreq = Value
        End Set
    End Property
    Public Property LVAM_Delflag() As String
        Get
            Return (sLVAM_Delflag)
        End Get
        Set(ByVal Value As String)
            sLVAM_Delflag = Value
        End Set
    End Property
    Public Property LVAM_CompID() As Integer
        Get
            Return (iLVAM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_CompID = Value
        End Set
    End Property
    Public Property LVAM_YearID() As Integer
        Get
            Return (iLVAM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_YearID = Value
        End Set
    End Property
    Public Property LVAM_Status() As String
        Get
            Return (sLVAM_Status)
        End Get
        Set(ByVal Value As String)
            sLVAM_Status = Value
        End Set
    End Property
    Public Property LVAM_Operation() As String
        Get
            Return (sLVAM_Operation)
        End Get
        Set(ByVal Value As String)
            sLVAM_Operation = Value
        End Set
    End Property
    Public Property LVAM_IPAddress() As String
        Get
            Return (sLVAM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLVAM_IPAddress = Value
        End Set
    End Property
    Public Property LVAM_CreatedBy() As Integer
        Get
            Return (iLVAM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_CreatedBy = Value
        End Set
    End Property
    Public Property LVAM_CreatedOn() As Date
        Get
            Return (dLVAM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLVAM_CreatedOn = Value
        End Set
    End Property

    Public Property LVAM_ApprovedBy() As Integer
        Get
            Return (iLVAM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_ApprovedBy = Value
        End Set
    End Property
    Public Property LVAM_ApprovedOn() As Date
        Get
            Return (dLVAM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLVAM_ApprovedOn = Value
        End Set
    End Property
    Public Property LVAM_DeletedBy() As Integer
        Get
            Return (iLVAM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_DeletedBy = Value
        End Set
    End Property
    Public Property LVAM_DeletedOn() As Date
        Get
            Return (dLVAM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLVAM_DeletedOn = Value
        End Set
    End Property
    Public Property LVAM_UpdatedBy() As Integer
        Get
            Return (iLVAM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_UpdatedBy = Value
        End Set
    End Property
    Public Property LVAM_UpdatedOn() As Date
        Get
            Return (dLVAM_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLVAM_UpdatedOn = Value
        End Set
    End Property
    Public Property LVAM_RecalldBy() As Integer
        Get
            Return (iLVAM_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAM_RecalldBy = Value
        End Set
    End Property

    Private iLVTM_ID As Integer
    Private iLVTM_MasterID As Integer
    Private iLVTM_AddtlnVehicleID As Integer
    Private sLVTM_TyreSLNo As String
    Private dLVTM_TyreFreq As Double
    Private sLVTM_DelFlag As String
    Private sLVTM_Status As String
    Private iLVTM_CreatedBy As Integer
    Private dLVTM_CreatedOn As Date
    Private iLVTM_UpdatedBy As Integer
    Private dLVTM_UpdatedOn As Date
    Private iLVTM_CompID As Integer
    Private iLVTM_YearID As Integer
    Private sLVTM_Operation As String
    Private sLVTM_IPAddress As String




    Public Property LVTM_ID() As Integer
        Get
            Return (iLVTM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLVTM_ID = Value
        End Set
    End Property
    Public Property LVTM_MasterID() As Integer
        Get
            Return (iLVTM_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iLVTM_MasterID = Value
        End Set
    End Property
    Public Property LVTM_AddtlnVehicleID() As Integer
        Get
            Return (iLVTM_AddtlnVehicleID)
        End Get
        Set(ByVal Value As Integer)
            iLVTM_AddtlnVehicleID = Value
        End Set
    End Property
    Public Property LVTM_TyreSLNo() As String
        Get
            Return (sLVTM_TyreSLNo)
        End Get
        Set(ByVal Value As String)
            sLVTM_TyreSLNo = Value
        End Set
    End Property
    Public Property LVTM_TyreFreq() As Double
        Get
            Return (dLVTM_TyreFreq)
        End Get
        Set(ByVal Value As Double)
            dLVTM_TyreFreq = Value
        End Set
    End Property


    Public Property LVTM_DelFlag() As String
        Get
            Return (sLVTM_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLVTM_DelFlag = Value
        End Set
    End Property
    Public Property LVTM_Status() As String
        Get
            Return (sLVTM_Status)
        End Get
        Set(ByVal Value As String)
            sLVTM_Status = Value
        End Set
    End Property
    Public Property LVTM_CreatedBy() As Integer
        Get
            Return (iLVTM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVTM_CreatedBy = Value
        End Set
    End Property
    Public Property LVTM_CreatedOn() As DateTime
        Get
            Return (dLVTM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVTM_CreatedOn = Value
        End Set
    End Property
    Public Property LVTM_UpdatedBy() As Integer
        Get
            Return (iLVTM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVTM_UpdatedBy = Value
        End Set
    End Property
    Public Property LVTM_UpdatedOn() As DateTime
        Get
            Return (dLVTM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVTM_UpdatedOn = Value
        End Set
    End Property

    Public Property LVTM_CompID() As Integer
        Get
            Return (iLVTM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLVTM_CompID = Value
        End Set
    End Property
    Public Property LVTM_YearID() As Integer
        Get
            Return (iLVTM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLVTM_YearID = Value
        End Set
    End Property
    Public Property LVTM_Operation() As String
        Get
            Return (sLVTM_Operation)
        End Get
        Set(ByVal Value As String)
            sLVTM_Operation = Value
        End Set
    End Property
    Public Property LVTM_IPAddress() As String
        Get
            Return (sLVTM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLVTM_IPAddress = Value
        End Set
    End Property

    Private iLVCM_ID As Integer
    Private iLVCM_MasterID As Integer
    Private iLVCM_AddtlnVehicleID As Integer
    Private iLVCM_ComplianceID As Integer
    Private dLVCM_ComplianceFreqInKM As Double
    Private iLVCM_ComplianceFreqInYear As Integer
    Private sLVCM_DelFlag As String
    Private sLVCM_Status As String
    Private iLVCM_CreatedBy As Integer
    Private dLVCM_CreatedOn As Date
    Private iLVCM_UpdatedBy As Integer
    Private dLVCM_UpdatedOn As Date
    Private iLVCM_CompID As Integer
    Private iLVCM_YearID As Integer
    Private sLVCM_Operation As String
    Private sLVCM_IPAddress As String




    Public Property LVCM_ID() As Integer
        Get
            Return (iLVCM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_ID = Value
        End Set
    End Property
    Public Property LVCM_MasterID() As Integer
        Get
            Return (iLVCM_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_MasterID = Value
        End Set
    End Property
    Public Property LVCM_AddtlnVehicleID() As Integer
        Get
            Return (iLVCM_AddtlnVehicleID)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_AddtlnVehicleID = Value
        End Set
    End Property

    Public Property LVCM_ComplianceID() As Integer
        Get
            Return (iLVCM_ComplianceID)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_ComplianceID = Value
        End Set
    End Property

    Public Property LVCM_ComplianceFreqInKM() As Double
        Get
            Return (dLVCM_ComplianceFreqInKM)
        End Get
        Set(ByVal Value As Double)
            dLVCM_ComplianceFreqInKM = Value
        End Set
    End Property
    Public Property LVCM_ComplianceFreqInYear() As Integer
        Get
            Return (iLVCM_ComplianceFreqInYear)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_ComplianceFreqInYear = Value
        End Set
    End Property
    Public Property LVCM_DelFlag() As String
        Get
            Return (sLVCM_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLVCM_DelFlag = Value
        End Set
    End Property
    Public Property LVCM_Status() As String
        Get
            Return (sLVCM_Status)
        End Get
        Set(ByVal Value As String)
            sLVCM_Status = Value
        End Set
    End Property
    Public Property LVCM_CreatedBy() As Integer
        Get
            Return (iLVCM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_CreatedBy = Value
        End Set
    End Property
    Public Property LVCM_CreatedOn() As DateTime
        Get
            Return (dLVCM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVCM_CreatedOn = Value
        End Set
    End Property
    Public Property LVCM_UpdatedBy() As Integer
        Get
            Return (iLVCM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_UpdatedBy = Value
        End Set
    End Property
    Public Property LVCM_UpdatedOn() As DateTime
        Get
            Return (dLVCM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVCM_UpdatedOn = Value
        End Set
    End Property

    Public Property LVCM_CompID() As Integer
        Get
            Return (iLVCM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_CompID = Value
        End Set
    End Property
    Public Property LVCM_YearID() As Integer
        Get
            Return (iLVCM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLVCM_YearID = Value
        End Set
    End Property
    Public Property LVCM_Operation() As String
        Get
            Return (sLVCM_Operation)
        End Get
        Set(ByVal Value As String)
            sLVCM_Operation = Value
        End Set
    End Property
    Public Property LVCM_IPAddress() As String
        Get
            Return (sLVCM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLVCM_IPAddress = Value
        End Set
    End Property

    Public Function SaveVehicleDetails(ByVal sNameSpace As String, ByVal objVAM As clsVehicleAddtlnMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVAM.iLVAM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVAM.iLVAM_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_RegNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objVAM.sLVAM_RegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_VehiclePurchaseDate ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objVAM.dLVAM_VehiclePurchaseDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_TotalMeterValue ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objVAM.dLVAM_TotalMeterValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_VehicleInvoiceNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objVAM.sLVAM_VehicleInvoiceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_VehicleAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objVAM.dLVAM_VehicleAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_VehicleDealer ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objVAM.sLVAM_VehicleDealer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_VehicleManufacturer ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objVAM.sLVAM_VehicleManufacturer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_DepreciationAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objVAM.dLVAM_DepreciationAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_BatteryNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objVAM.sLVAM_BatteryNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_BatteryFreq ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objVAM.dLVAM_BatteryFreq
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objVAM.sLVAM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objVAM.sLVAM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVAM.iLVAM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objVAM.dLVAM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVAM.iLVAM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objVAM.dLVAM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVAM.iLVAM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVAM.iLVAM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objVAM.sLVAM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objVAM.sLVAM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Vehicle_AdditionalMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveTyreDetails(ByVal sNameSpace As String, ByVal objvam As clsVehicleAddtlnMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVTM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVTM_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_AddtlnVehicleID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVTM_AddtlnVehicleID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_TyreSLNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objvam.sLVTM_TyreSLNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_TyreFreq", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objvam.dLVTM_TyreFreq
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objvam.sLVTM_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objvam.sLVTM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVTM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objvam.dLVTM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVTM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVTM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objvam.sLVTM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVTM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objvam.sLVTM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Vehicle_TyreMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveComplianceDetails(ByVal sNameSpace As String, ByVal objvam As clsVehicleAddtlnMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVCM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVCM_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_AddtlnVehicleID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVCM_AddtlnVehicleID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_ComplianceID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objvam.iLVCM_ComplianceID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_ComplianceFreqInKM", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objvam.dLVCM_ComplianceFreqInKM
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_ComplianceFreqInYear", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objvam.iLVCM_ComplianceFreqInYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objvam.sLVCM_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objvam.sLVCM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVCM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objvam.dLVCM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVCM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objvam.iLVCM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objvam.sLVCM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVCM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objvam.sLVCM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Vehicle_ComplianceMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetRouteCustCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iVehID As Integer, ByVal iRegNo As String) As Integer
        Dim sSql As String = ""
        Dim iCount As Integer
        Try
            sSql = "select count(*) from Lgst_Vehicle_AdditionalMaster where LVAM_ID=" & iVehID & " and LVAM_RegNo= '" & iRegNo & "' and LVAM_Compid=" & iCompID & " "
            iCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadTyreDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehId As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("TyreId")
            dt.Columns.Add("TyreNo")
            dt.Columns.Add("TyreFreq")


            sSql = "Select * from Lgst_Vehicle_TyreMaster Where LVTM_DelFlag<>'D' And LVTM_AddtlnVehicleID=" & iVehId & " And LVTM_CompID=" & iCompID & "   Order by LVTM_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("TyreId") = dr("LVTM_ID")
                    dRow("TyreNo") = dr("LVTM_TyreSLNo")
                    dRow("TyreFreq") = dr("LVTM_TyreFreq")

                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTyreDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_Vehicle_TyreMaster Where LVTM_DelFlag<>'D' And LVTM_ID=" & iID & " And LVTM_MasterID=" & iMasterID & " And LVTM_CompID=" & iCompID & " and LVTM_YearID= " & iYearId & " Order by LVTM_ID"
            GetTyreDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetTyreDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteTyreValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Vehicle_TyreMaster Set LVTM_DelFlag='D' Where LVTM_ID=" & iID & " And LVTM_MasterID=" & iMasterID & " And LVTM_CompID=" & iCompID & " and LVTM_YearID= " & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadComplianceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehId As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("Id")
            dt.Columns.Add("ComplianceName")
            dt.Columns.Add("FrequencyInKm")
            dt.Columns.Add("FrequencyInYears")
            dt.Columns.Add("ComplianceID")


            sSql = "Select * from Lgst_Vehicle_ComplianceMaster Where LVCM_DelFlag<>'D' And LVCM_MasterID=" & iVehId & " And LVCM_CompID=" & iCompID & "   Order by LVCM_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("Id") = dr("LVCM_ID")
                    dRow("ComplianceName") = GetComplianceName(sNameSpace, dr("LVCM_ComplianceID"), iCompID)
                    If dr("LVCM_ComplianceFreqInKM") = 0 Then
                        dRow("FrequencyInKm") = ""
                    Else
                        dRow("FrequencyInKm") = dr("LVCM_ComplianceFreqInKM")
                    End If
                    If dr("LVCM_ComplianceFreqInYear") = 0 Then
                        dRow("FrequencyInYears") = ""
                    Else
                        dRow("FrequencyInYears") = dr("LVCM_ComplianceFreqInYear")
                    End If
                    dRow("ComplianceID") = dr("LVCM_ComplianceID")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_Vehicle_ComplianceMaster Where LVCM_DelFlag<>'D' And LVCM_ID=" & iID & " And LVCM_MasterID=" & iMasterID & " And LVCM_CompID=" & iCompID & " and LVCM_YearID= " & iYearId & " Order by LVCM_ID"
            GetComplianceDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetComplianceDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteComplianceValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Vehicle_ComplianceMaster Set LVCM_DelFlag='D' Where LVCM_ID=" & iID & " And LVCM_TripID=" & iMasterID & " And LVCM_CompID=" & iCompID & " and LVCM_YearID= " & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetComplianceName(ByVal sNameSpace As String, ByVal iMasID As Integer, ByVal iCompID As Integer) As String
        Dim sSQL As String = ""
        Dim sPump As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "Select Mas_desc from ACC_General_Master  where Mas_id=" & iMasID & " and Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Compliance' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Mas_desc").ToString()) = False Then
                    sPump = dt.Rows(0)("Mas_desc").ToString()
                Else
                    sPump = ""
                End If
            Else
                sPump = ""
            End If
            Return sPump
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleAdditionalDashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVAM_ID")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("InvoiceNo")
            dt.Columns.Add("VehicleManufacturer")
            dt.Columns.Add("Status")


            sSql = "select * from Lgst_Vehicle_AdditionalMaster where LVAM_CompID=" & iCompID & ""
            If iStatus = 0 Then
                sSql = sSql & " And LVAM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And LVAM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And LVAM_DelFlag='W'" 'Waiting for approval
            End If
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVAM_MasterID")) = False Then
                    dRow("LVAM_ID") = dtDetails.Rows(i)("LVAM_MasterID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_RegNo")) = False Then
                    dRow("RegistrationNo") = dtDetails.Rows(i)("LVAM_RegNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_VehicleInvoiceNo")) = False Then
                    dRow("InvoiceNo") = dtDetails.Rows(i)("LVAM_VehicleInvoiceNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_VehicleManufacturer")) = False Then
                    dRow("VehicleManufacturer") = dtDetails.Rows(i)("LVAM_VehicleManufacturer")
                End If
                If dtDetails.Rows(i)("LVAM_Status") = "C" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("LVAm_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("LVAM_Status") = "A" Then
                    dRow("Status") = "Activated"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAdditionalVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehId As Integer, ByVal iYearId As Integer)
        Dim sSql As String
        Try
            sSql = "Update Lgst_Vehicle_AdditionalMaster set LVAM_Status='A',LVAM_Delflag='A' where LVAM_MasterID=" & iVehId & " and LVAM_Compid=" & iCompID & " And LVAM_YearID =" & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function getVechicleType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehicleTypeId As Integer) As String
        Dim sSql As String
        Dim sVehicleType As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select Mas_desc from ACC_General_Master where  Mas_id =" & iVehicleTypeId & "  "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Mas_desc").ToString()) = False Then
                    sVehicleType = dt.Rows(0)("Mas_desc").ToString()
                Else
                    sVehicleType = ""
                End If
            Else
                sVehicleType = ""
            End If
            Return sVehicleType
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMeterReading(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iVehId As Integer) As Double
        Dim sSql As String = ""
        Dim dMeterVal As Double = 0.0
        Try
            sSql = "" : sSql = "select top 1  LTGM_MREnd from Lgst_TripGeneration_Master where "
            sSql = sSql & "LTGM_compid=" & iCompID & " and LTGM_VehivleNo = " & iVehId & "and LTGM_TripStatus=2 order by LTGM_id desc"
            dMeterVal = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return dMeterVal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCSMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_Vehicle_Master Where LVM_ID=" & iCSMid & "  And LVM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCSMid As Integer, ByVal iRegNo As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_Vehicle_AdditionalMaster Where LVAM_MasterID=" & iCSMid & " and LVAM_RegNo= '" & iRegNo & "' And LVAM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadVehicleType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Vehicle Type' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadComplianceType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Compliance' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVM_Id,LVM_RegNo From Lgst_Vehicle_Master Where LVM_CompID=" & iCompID & "" ' and lvm_id in (select LVAM_VehivleNo from Lgst_TripGeneration_Master where LVAM_TripStatus =1 and LVAM_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAdditionalVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVAM_MasterID,LVAM_RegNo From Lgst_Vehicle_AdditionalMaster Where LVAM_CompID=" & iCompID & "" ' and lvm_id in (select LVAM_VehivleNo from Lgst_TripGeneration_Master where LVAM_TripStatus =1 and LVAM_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function RemoveChargeDublicate(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("ChargeID"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("ChargeID"), String.Empty)
                End If
            Next
            For Each DataRow As DataRow In duplicateList
                dt.Rows.Remove(DataRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
