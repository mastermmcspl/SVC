Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsTripGeneration
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iLTGM_ID As Integer
    Private sLTGM_TransactionNo As String
    Private iLTGM_RouteID As Integer
    Private iLTGM_VehicleType As Integer
    Private iLTGM_VehicleNo As Integer
    Private sLTGM_StartCity As String
    Private sLTGM_DestinationCity As String
    Private dLTGM_DistanceinKms As Double
    Private dLTGM_Rate As Double
    Private dLTGM_PetrolQty As Double
    Private iLTGM_StartCustomer As Integer
    Private iLTGM_DestinationCustomer As Integer
    Private sLTGM_SVCNo As String
    Private sLTGM_ClientRefNo As String
    Private iLTGM_Driver As Integer
    Private dLTGM_DriverAmount As Double
    Private dLTGM_AdvancePaidToDriver As Double
    Private sLTGM_Remarks As String
    Private sLTGM_StartTime As String
    Private sLTGM_StopTime As String
    Private dLTGM_StartDate As DateTime
    Private dLTGM_StopDate As DateTime
    Private iLTGM_TripStatus As Integer
    Private sLTGM_EWayBillNo As String
    Private sLTGM_AllottedTime As String
    Private sLTGM_TripTakenTime As String
    Private sLTGM_TimeStatus As String
    Private dLTGM_MRStart As Double
    Private dLTGM_MREnd As Double
    Private sLTGM_MRStatus As String

    Private sLTGM_CompanyAddress As String
    Private sLTGM_CompanyGSTNRegNo As String
    Private sLTGM_CustomerAddress As String
    Private sLTGM_CustomerGSTNRegNo As String
    Private iLTGM_GSTNCategory As Integer
    Private dLTGM_GSTRate As Double
    Private dLTGM_GSTAmount As Double
    Private dLTGM_SGST As Double
    Private dLTGM_SGSTAmount As Double
    Private dLTGM_CGST As Double
    Private dLTGM_CGSTAmount As Double
    Private dLTGM_IGST As Double
    Private dLTGM_IGSTAmount As Double
    Private sLTGM_GSTCustBillStatus As String
    Private sLTGM_State As String
    Private dLTGM_EscAmt As Double
    Public Property LTGM_ID() As Integer
        Get
            Return (iLTGM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_ID = Value
        End Set
    End Property
    Public Property LTGM_TransactionNo() As String
        Get
            Return (sLTGM_TransactionNo)
        End Get
        Set(ByVal Value As String)
            sLTGM_TransactionNo = Value
        End Set
    End Property

    Public Property LTGM_RouteID() As Integer
        Get
            Return (iLTGM_RouteID)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_RouteID = Value
        End Set
    End Property
    Public Property LTGM_VehicleType() As Integer
        Get
            Return (iLTGM_VehicleType)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_VehicleType = Value
        End Set
    End Property
    Public Property LTGM_VehicleNo() As Integer
        Get
            Return (iLTGM_VehicleNo)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_VehicleNo = Value
        End Set
    End Property
    Public Property LTGM_StartCity() As String
        Get
            Return (sLTGM_StartCity)
        End Get
        Set(ByVal Value As String)
            sLTGM_StartCity = Value
        End Set
    End Property
    Public Property LTGM_DestinationCity() As String
        Get
            Return (sLTGM_DestinationCity)
        End Get
        Set(ByVal Value As String)
            sLTGM_DestinationCity = Value
        End Set
    End Property
    Public Property LTGM_DistanceinKms() As Double
        Get
            Return (dLTGM_DistanceinKms)
        End Get
        Set(ByVal Value As Double)
            dLTGM_DistanceinKms = Value
        End Set
    End Property
    Public Property LTGM_Rate() As Double
        Get
            Return (dLTGM_Rate)
        End Get
        Set(ByVal Value As Double)
            dLTGM_Rate = Value
        End Set
    End Property

    Public Property LTGM_PetrolQty() As Double
        Get
            Return (dLTGM_PetrolQty)
        End Get
        Set(ByVal Value As Double)
            dLTGM_PetrolQty = Value
        End Set
    End Property
    Public Property LTGM_StartCustomer() As Integer
        Get
            Return (iLTGM_StartCustomer)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_StartCustomer = Value
        End Set
    End Property
    Public Property LTGM_DestinationCustomer() As Integer
        Get
            Return (iLTGM_DestinationCustomer)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_DestinationCustomer = Value
        End Set
    End Property
    Public Property LTGM_SVCNo() As String
        Get
            Return (sLTGM_SVCNo)
        End Get
        Set(ByVal Value As String)
            sLTGM_SVCNo = Value
        End Set
    End Property
    Public Property LTGM_ClientRefNo() As String
        Get
            Return (sLTGM_ClientRefNo)
        End Get
        Set(ByVal Value As String)
            sLTGM_ClientRefNo = Value
        End Set
    End Property
    Public Property LTGM_Driver() As Integer
        Get
            Return (iLTGM_Driver)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_Driver = Value
        End Set
    End Property

    Public Property LTGM_DriverAmount() As Double
        Get
            Return (dLTGM_DriverAmount)
        End Get
        Set(ByVal Value As Double)
            dLTGM_DriverAmount = Value
        End Set
    End Property
    Public Property LTGM_AdvancePaidToDriver() As Double
        Get
            Return (dLTGM_AdvancePaidToDriver)
        End Get
        Set(ByVal Value As Double)
            dLTGM_AdvancePaidToDriver = Value
        End Set
    End Property
    Public Property LTGM_Remarks() As String
        Get
            Return (sLTGM_Remarks)
        End Get
        Set(ByVal Value As String)
            sLTGM_Remarks = Value
        End Set
    End Property
    Public Property LTGM_StartTime() As String
        Get
            Return (sLTGM_StartTime)
        End Get
        Set(ByVal Value As String)
            sLTGM_StartTime = Value
        End Set
    End Property
    Public Property LTGM_StopTime() As String
        Get
            Return (sLTGM_StopTime)
        End Get
        Set(ByVal Value As String)
            sLTGM_StopTime = Value
        End Set
    End Property

    Public Property LTGM_StartDate() As Date
        Get
            Return (dLTGM_StartDate)
        End Get
        Set(ByVal Value As Date)
            dLTGM_StartDate = Value
        End Set
    End Property
    Public Property LTGM_StopDate() As Date
        Get
            Return (dLTGM_StopDate)
        End Get
        Set(ByVal Value As Date)
            dLTGM_StopDate = Value
        End Set
    End Property
    Public Property LTGM_TripStatus() As Integer
        Get
            Return (iLTGM_TripStatus)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_TripStatus = Value
        End Set
    End Property

    Public Property LTGM_EWayBillNo() As String
        Get
            Return (sLTGM_EWayBillNo)
        End Get
        Set(ByVal Value As String)
            sLTGM_EWayBillNo = Value
        End Set
    End Property

    Public Property LTGM_AllottedTime() As String
        Get
            Return (sLTGM_AllottedTime)
        End Get
        Set(ByVal Value As String)
            sLTGM_AllottedTime = Value
        End Set
    End Property
    Public Property LTGM_TripTakenTime() As String
        Get
            Return (sLTGM_TripTakenTime)
        End Get
        Set(ByVal Value As String)
            sLTGM_TripTakenTime = Value
        End Set
    End Property
    Public Property LTGM_TimeStatus() As String
        Get
            Return (sLTGM_TimeStatus)
        End Get
        Set(ByVal Value As String)
            sLTGM_TimeStatus = Value
        End Set
    End Property
    Public Property LTGM_MRStart() As Double
        Get
            Return (dLTGM_MRStart)
        End Get
        Set(ByVal Value As Double)
            dLTGM_MRStart = Value
        End Set
    End Property
    Public Property LTGM_MREnd() As Double
        Get
            Return (dLTGM_MREnd)
        End Get
        Set(ByVal Value As Double)
            dLTGM_MREnd = Value
        End Set
    End Property
    Public Property LTGM_MRStatus() As String
        Get
            Return (sLTGM_MRStatus)
        End Get
        Set(ByVal Value As String)
            sLTGM_MRStatus = Value
        End Set
    End Property
    Public Property LTGM_CompanyAddress() As String
        Get
            Return (sLTGM_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sLTGM_CompanyAddress = Value
        End Set
    End Property


    Public Property LTGM_CompanyGSTNRegNo() As String
        Get
            Return (sLTGM_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sLTGM_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property LTGM_CustomerAddress() As String
        Get
            Return (sLTGM_CustomerAddress)
        End Get
        Set(ByVal Value As String)
            sLTGM_CustomerAddress = Value
        End Set
    End Property

    Public Property LTGM_CustomerGSTNRegNo() As String
        Get
            Return (sLTGM_CustomerGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sLTGM_CustomerGSTNRegNo = Value
        End Set
    End Property
    Public Property LTGM_GSTNCategory() As Integer
        Get
            Return (iLTGM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_GSTNCategory = Value
        End Set
    End Property

    Public Property LTGM_GSTRate() As Double
        Get
            Return (dLTGM_GSTRate)
        End Get
        Set(ByVal Value As Double)
            dLTGM_GSTRate = Value
        End Set
    End Property
    Public Property LTGM_GSTAmount() As Double
        Get
            Return (dLTGM_GSTAmount)
        End Get
        Set(ByVal Value As Double)
            dLTGM_GSTAmount = Value
        End Set
    End Property

    Public Property LTGM_SGST() As Double
        Get
            Return (dLTGM_SGST)
        End Get
        Set(ByVal Value As Double)
            dLTGM_SGST = Value
        End Set
    End Property
    Public Property LTGM_SGSTAmount() As Double
        Get
            Return (dLTGM_SGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dLTGM_SGSTAmount = Value
        End Set
    End Property


    Public Property LTGM_CGST() As Double
        Get
            Return (dLTGM_CGST)
        End Get
        Set(ByVal Value As Double)
            dLTGM_CGST = Value
        End Set
    End Property
    Public Property LTGM_CGSTAmount() As Double
        Get
            Return (dLTGM_CGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dLTGM_CGSTAmount = Value
        End Set
    End Property

    Public Property LTGM_IGST() As Double
        Get
            Return (dLTGM_IGST)
        End Get
        Set(ByVal Value As Double)
            dLTGM_IGST = Value
        End Set
    End Property
    Public Property LTGM_IGSTAmount() As Double
        Get
            Return (dLTGM_IGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dLTGM_IGSTAmount = Value
        End Set
    End Property

    Public Property LTGM_GSTCustBillStatus() As String
        Get
            Return (sLTGM_GSTCustBillStatus)
        End Get
        Set(ByVal Value As String)
            sLTGM_GSTCustBillStatus = Value
        End Set
    End Property
    Public Property LTGM_State() As String
        Get
            Return (sLTGM_State)
        End Get
        Set(ByVal Value As String)
            sLTGM_State = Value
        End Set
    End Property
    Public Property LTGM_EscAmt() As Double
        Get
            Return (dLTGM_EscAmt)
        End Get
        Set(ByVal Value As Double)
            dLTGM_EscAmt = Value
        End Set
    End Property
    Private sLTGM_Delflag As String
    Private iLTGM_CompID As Integer
    Private iLTGM_YearID As Integer
    Private sLTGM_Status As String
    Private sLTGM_Operation As String
    Private sLTGM_IPAddress As String
    Private iLTGM_CreatedBy As Integer
    Private dLTGM_CreatedOn As DateTime
    Private iLTGM_ApprovedBy As Integer
    Private dLTGM_ApprovedOn As DateTime
    Private iLTGM_DeletedBy As Integer
    Private dLTGM_DeletedOn As DateTime
    Private iLTGM_UpdatedBy As Integer
    Private dLTGM_UpdatedOn As DateTime
    Private iLTGM_RecalldBy As Integer


    Public Property LTGM_Delflag() As String
        Get
            Return (sLTGM_Delflag)
        End Get
        Set(ByVal Value As String)
            sLTGM_Delflag = Value
        End Set
    End Property
    Public Property LTGM_CompID() As Integer
        Get
            Return (iLTGM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_CompID = Value
        End Set
    End Property
    Public Property LTGM_YearID() As Integer
        Get
            Return (iLTGM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_YearID = Value
        End Set
    End Property
    Public Property LTGM_Status() As String
        Get
            Return (sLTGM_Status)
        End Get
        Set(ByVal Value As String)
            sLTGM_Status = Value
        End Set
    End Property
    Public Property LTGM_Operation() As String
        Get
            Return (sLTGM_Operation)
        End Get
        Set(ByVal Value As String)
            sLTGM_Operation = Value
        End Set
    End Property
    Public Property LTGM_IPAddress() As String
        Get
            Return (sLTGM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLTGM_IPAddress = Value
        End Set
    End Property
    Public Property LTGM_CreatedBy() As Integer
        Get
            Return (iLTGM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_CreatedBy = Value
        End Set
    End Property
    Public Property LTGM_CreatedOn() As Date
        Get
            Return (dLTGM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLTGM_CreatedOn = Value
        End Set
    End Property

    Public Property LTGM_ApprovedBy() As Integer
        Get
            Return (iLTGM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_ApprovedBy = Value
        End Set
    End Property
    Public Property LTGM_ApprovedOn() As Date
        Get
            Return (dLTGM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLTGM_ApprovedOn = Value
        End Set
    End Property
    Public Property LTGM_DeletedBy() As Integer
        Get
            Return (iLTGM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_DeletedBy = Value
        End Set
    End Property
    Public Property LTGM_DeletedOn() As Date
        Get
            Return (dLTGM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLTGM_DeletedOn = Value
        End Set
    End Property
    Public Property LTGM_UpdatedBy() As Integer
        Get
            Return (iLTGM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_UpdatedBy = Value
        End Set
    End Property
    Public Property LTGM_UpdatedOn() As Date
        Get
            Return (dLTGM_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLTGM_UpdatedOn = Value
        End Set
    End Property
    Public Property LTGM_RecalldBy() As Integer
        Get
            Return (iLTGM_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGM_RecalldBy = Value
        End Set
    End Property


    Private iLTGHD_ID As Integer
    Private iLTGHD_TripID As Integer
    Private sLTGHD_InTime As String
    Private sLTGHD_OutTime As String
    Private dLTGHD_InDate As DateTime
    Private dLTGHD_OutDate As DateTime

    Private sLTGHD_HopOnDetails As String
    Private sLTGHD_DelFlag As String
    Private sLTGHD_Status As String
    Private iLTGHD_CreatedBy As Integer
    Private dLTGHD_CreatedOn As Date
    Private iLTGHD_UpdatedBy As Integer
    Private dLTGHD_UpdatedOn As Date
    Private iLTGHD_CompID As Integer
    Private iLTGHD_YearID As Integer
    Private sLTGHD_Operation As String
    Private sLTGHD_IPAddress As String




    Public Property LTGHD_ID() As Integer
        Get
            Return (iLTGHD_ID)
        End Get
        Set(ByVal Value As Integer)
            iLTGHD_ID = Value
        End Set
    End Property
    Public Property LTGHD_TripID() As Integer
        Get
            Return (iLTGHD_TripID)
        End Get
        Set(ByVal Value As Integer)
            iLTGHD_TripID = Value
        End Set
    End Property
    Public Property LTGHD_OutTime() As String
        Get
            Return (sLTGHD_OutTime)
        End Get
        Set(ByVal Value As String)
            sLTGHD_OutTime = Value
        End Set
    End Property
    Public Property LTGHD_InDate() As Date
        Get
            Return (dLTGHD_InDate)
        End Get
        Set(ByVal Value As Date)
            dLTGHD_InDate = Value
        End Set
    End Property
    Public Property LTGHD_OutDate() As Date
        Get
            Return (dLTGHD_OutDate)
        End Get
        Set(ByVal Value As Date)
            dLTGHD_OutDate = Value
        End Set
    End Property

    Public Property LTGHD_HopOnDetails() As String
        Get
            Return (sLTGHD_HopOnDetails)
        End Get
        Set(ByVal Value As String)
            sLTGHD_HopOnDetails = Value
        End Set
    End Property
    Public Property LTGHD_InTime() As String
        Get
            Return (sLTGHD_InTime)
        End Get
        Set(ByVal Value As String)
            sLTGHD_InTime = Value
        End Set
    End Property

    Public Property LTGHD_DelFlag() As String
        Get
            Return (sLTGHD_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLTGHD_DelFlag = Value
        End Set
    End Property
    Public Property LTGHD_Status() As String
        Get
            Return (sLTGHD_Status)
        End Get
        Set(ByVal Value As String)
            sLTGHD_Status = Value
        End Set
    End Property
    Public Property LTGHD_CreatedBy() As Integer
        Get
            Return (iLTGHD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGHD_CreatedBy = Value
        End Set
    End Property
    Public Property LTGHD_CreatedOn() As DateTime
        Get
            Return (dLTGHD_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLTGHD_CreatedOn = Value
        End Set
    End Property
    Public Property LTGHD_UpdatedBy() As Integer
        Get
            Return (iLTGHD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGHD_UpdatedBy = Value
        End Set
    End Property
    Public Property LTGHD_UpdatedOn() As DateTime
        Get
            Return (dLTGHD_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLTGHD_UpdatedOn = Value
        End Set
    End Property

    Public Property LTGHD_CompID() As Integer
        Get
            Return (iLTGHD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLTGHD_CompID = Value
        End Set
    End Property
    Public Property LTGHD_YearID() As Integer
        Get
            Return (iLTGHD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLTGHD_YearID = Value
        End Set
    End Property
    Public Property LTGHD_Operation() As String
        Get
            Return (sLTGHD_Operation)
        End Get
        Set(ByVal Value As String)
            sLTGHD_Operation = Value
        End Set
    End Property
    Public Property LTGHD_IPAddress() As String
        Get
            Return (sLTGHD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLTGHD_IPAddress = Value
        End Set
    End Property


    Private iLTGDD_ID As Integer
    Private iLTGDD_TripID As Integer
    Private iLTGDD_PumpId As Integer
    Private dLTGDD_IndDate As DateTime
    Private dLTGDD_DieselinLtrs As Double
    Private dLTGDD_DieselAmount As Double
    Private dLTGDD_DieselRatePerltr As Double
    Private dLTGDD_DriverAdvancGvnByPump As Double
    Private dLTGDD_OtherExpenses As Double
    Private dLTGDD_OilInltr As Double
    Private dLTGDD_OilAmountInLtr As Double
    Private sLTGDD_Remarks As String
    Private sLTGDD_DelFlag As String
    Private sLTGDD_Status As String
    Private iLTGDD_CreatedBy As Integer
    Private dLTGDD_CreatedOn As Date
    Private iLTGDD_UpdatedBy As Integer
    Private dLTGDD_UpdatedOn As Date
    Private iLTGDD_CompID As Integer
    Private iLTGDD_YearID As Integer
    Private sLTGDD_Operation As String
    Private sLTGDD_IPAddress As String




    Public Property LTGDD_ID() As Integer
        Get
            Return (iLTGDD_ID)
        End Get
        Set(ByVal Value As Integer)
            iLTGDD_ID = Value
        End Set
    End Property
    Public Property LTGDD_TripID() As Integer
        Get
            Return (iLTGDD_TripID)
        End Get
        Set(ByVal Value As Integer)
            iLTGDD_TripID = Value
        End Set
    End Property
    Public Property LTGDD_DieselinLtrs() As Double
        Get
            Return (dLTGDD_DieselinLtrs)
        End Get
        Set(ByVal Value As Double)
            dLTGDD_DieselinLtrs = Value
        End Set
    End Property

    Public Property LTGDD_DieselAmount() As Double
        Get
            Return (dLTGDD_DieselAmount)
        End Get
        Set(ByVal Value As Double)
            dLTGDD_DieselAmount = Value
        End Set
    End Property

    Public Property LTGDD_DriverAdvancGvnByPump() As Double
        Get
            Return (dLTGDD_DriverAdvancGvnByPump)
        End Get
        Set(ByVal Value As Double)
            dLTGDD_DriverAdvancGvnByPump = Value
        End Set
    End Property
    Public Property LTGDD_DieselRatePerltr() As Double
        Get
            Return (dLTGDD_DieselRatePerltr)
        End Get
        Set(ByVal Value As Double)
            dLTGDD_DieselRatePerltr = Value
        End Set
    End Property

    Public Property LTGDD_PumpId() As Integer
        Get
            Return (iLTGDD_PumpId)
        End Get
        Set(ByVal Value As Integer)
            iLTGDD_PumpId = Value
        End Set
    End Property
    Public Property LTGDD_IndDate() As DateTime
        Get
            Return (dLTGDD_IndDate)
        End Get
        Set(ByVal Value As DateTime)
            dLTGDD_IndDate = Value
        End Set
    End Property
    Public Property LTGDD_OtherExpenses() As Double
        Get
            Return (dLTGDD_OtherExpenses)
        End Get
        Set(ByVal Value As Double)
            dLTGDD_OtherExpenses = Value
        End Set
    End Property
    Public Property LTGDD_OilInltr() As Double
        Get
            Return (dLTGDD_OilInltr)
        End Get
        Set(ByVal Value As Double)
            dLTGDD_OilInltr = Value
        End Set
    End Property
    Public Property LTGDD_OilAmountInLtr() As Double
        Get
            Return (dLTGDD_OilAmountInLtr)
        End Get
        Set(ByVal Value As Double)
            dLTGDD_OilAmountInLtr = Value
        End Set
    End Property
    Public Property LTGDD_Remarks() As String
        Get
            Return (sLTGDD_Remarks)
        End Get
        Set(ByVal Value As String)
            sLTGDD_Remarks = Value
        End Set
    End Property
    Public Property LTGDD_DelFlag() As String
        Get
            Return (sLTGDD_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLTGDD_DelFlag = Value
        End Set
    End Property
    Public Property LTGDD_Status() As String
        Get
            Return (sLTGDD_Status)
        End Get
        Set(ByVal Value As String)
            sLTGDD_Status = Value
        End Set
    End Property
    Public Property LTGDD_CreatedBy() As Integer
        Get
            Return (iLTGDD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGDD_CreatedBy = Value
        End Set
    End Property
    Public Property LTGDD_CreatedOn() As DateTime
        Get
            Return (dLTGDD_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLTGDD_CreatedOn = Value
        End Set
    End Property
    Public Property LTGDD_UpdatedBy() As Integer
        Get
            Return (iLTGDD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLTGDD_UpdatedBy = Value
        End Set
    End Property
    Public Property LTGDD_UpdatedOn() As DateTime
        Get
            Return (dLTGDD_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLTGDD_UpdatedOn = Value
        End Set
    End Property

    Public Property LTGDD_CompID() As Integer
        Get
            Return (iLTGDD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLTGDD_CompID = Value
        End Set
    End Property
    Public Property LTGDD_YearID() As Integer
        Get
            Return (iLTGDD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLTGDD_YearID = Value
        End Set
    End Property
    Public Property LTGDD_Operation() As String
        Get
            Return (sLTGDD_Operation)
        End Get
        Set(ByVal Value As String)
            sLTGDD_Operation = Value
        End Set
    End Property
    Public Property LTGDD_IPAddress() As String
        Get
            Return (sLTGDD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLTGDD_IPAddress = Value
        End Set
    End Property



    Private ATD_ID As Integer
    Private ATD_TransactionDate As Date
    Private ATD_TrType As Integer
    Private ATD_BillId As Integer
    Private ATD_PaymentType As Integer
    Private ATD_Head As Integer
    Private ATD_DbOrCr As Integer
    Private ATD_GL As Integer
    Private ATD_SubGL As Integer
    Private ATD_Debit As Decimal
    Private ATD_Credit As Decimal
    Private ATD_CreatedOn As Date
    Private ATD_CreatedBy As Integer
    Private ATD_ApprovedBy As Integer
    Private ATD_ApprovedOn As Date
    Private ATD_Deletedby As Integer
    Private ATD_DeletedOn As Date
    Private ATD_Status As String
    Private ATD_YearID As Integer
    Private ATD_CompID As Integer
    Private ATD_Operation As String
    Private ATD_IPAddress As String

    Private ATD_ZoneID As Integer
    Private ATD_RegionID As Integer
    Private ATD_AreaID As Integer
    Private ATD_BranchID As Integer

    Private ATD_UpdatedOn As Date
    Private ATD_UpdatedBy As Integer


    Private ATD_OpenDebit As Decimal
    Private ATD_OpenCredit As Decimal
    Private ATD_ClosingDebit As Decimal
    Private ATD_ClosingCredit As Decimal
    Private ATD_SeqReferenceNum As Integer

    Public Property sATD_IPAddress() As String
        Get
            Return (ATD_IPAddress)
        End Get
        Set(ByVal Value As String)
            ATD_IPAddress = Value
        End Set
    End Property
    Public Property sATD_Operation() As String
        Get
            Return (ATD_Operation)
        End Get
        Set(ByVal Value As String)
            ATD_Operation = Value
        End Set
    End Property
    Public Property iATD_YearID() As Integer
        Get
            Return (ATD_YearID)
        End Get
        Set(ByVal Value As Integer)
            ATD_YearID = Value
        End Set
    End Property
    Public Property iATD_CompID() As Integer
        Get
            Return (ATD_CompID)
        End Get
        Set(ByVal Value As Integer)
            ATD_CompID = Value
        End Set
    End Property
    Public Property iATD_ZoneID() As Integer
        Get
            Return (ATD_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            ATD_ZoneID = Value
        End Set
    End Property
    Public Property iATD_RegionID() As Integer
        Get
            Return (ATD_RegionID)
        End Get
        Set(ByVal Value As Integer)
            ATD_RegionID = Value
        End Set
    End Property
    Public Property iATD_AreaID() As Integer
        Get
            Return (ATD_AreaID)
        End Get
        Set(ByVal Value As Integer)
            ATD_AreaID = Value
        End Set
    End Property
    Public Property iATD_BranchID() As Integer
        Get
            Return (ATD_BranchID)
        End Get
        Set(ByVal Value As Integer)
            ATD_BranchID = Value
        End Set
    End Property
    Public Property sATD_Status() As String
        Get
            Return (ATD_Status)
        End Get
        Set(ByVal Value As String)
            ATD_Status = Value
        End Set
    End Property
    Public Property dATD_DeletedOn() As Date
        Get
            Return (ATD_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_DeletedOn = Value
        End Set
    End Property
    Public Property iATD_Deletedby() As Integer
        Get
            Return (ATD_Deletedby)
        End Get
        Set(ByVal Value As Integer)
            ATD_Deletedby = Value
        End Set
    End Property
    Public Property dATD_ApprovedOn() As Date
        Get
            Return (ATD_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_ApprovedOn = Value
        End Set
    End Property
    Public Property iATD_ApprovedBy() As Integer
        Get
            Return (ATD_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            ATD_ApprovedBy = Value
        End Set
    End Property
    Public Property iATD_CreatedBy() As Integer
        Get
            Return (ATD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            ATD_CreatedBy = Value
        End Set
    End Property
    Public Property dATD_CreatedOn() As Date
        Get
            Return (ATD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_CreatedOn = Value
        End Set
    End Property
    Public Property iATD_UpdatedBy() As Integer
        Get
            Return (ATD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ATD_UpdatedBy = Value
        End Set
    End Property
    Public Property dATD_UpdatedOn() As Date
        Get
            Return (ATD_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_UpdatedOn = Value
        End Set
    End Property
    Public Property dATD_Credit() As Decimal
        Get
            Return (ATD_Credit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_Credit = Value
        End Set
    End Property
    Public Property dATD_Debit() As Decimal
        Get
            Return (ATD_Debit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_Debit = Value
        End Set
    End Property
    Public Property iATD_SubGL() As Integer
        Get
            Return (ATD_SubGL)
        End Get
        Set(ByVal Value As Integer)
            ATD_SubGL = Value
        End Set
    End Property
    Public Property iATD_GL() As Integer
        Get
            Return (ATD_GL)
        End Get
        Set(ByVal Value As Integer)
            ATD_GL = Value
        End Set
    End Property
    Public Property iATD_Head() As Integer
        Get
            Return (ATD_Head)
        End Get
        Set(ByVal Value As Integer)
            ATD_Head = Value
        End Set
    End Property

    Public Property iATD_DbOrCr() As Integer
        Get
            Return (ATD_DbOrCr)
        End Get
        Set(ByVal Value As Integer)
            ATD_DbOrCr = Value
        End Set
    End Property
    Public Property iATD_PaymentType() As Integer
        Get
            Return (ATD_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            ATD_PaymentType = Value
        End Set
    End Property
    Public Property iATD_BillId() As Integer
        Get
            Return (ATD_BillId)
        End Get
        Set(ByVal Value As Integer)
            ATD_BillId = Value
        End Set
    End Property
    Public Property iATD_TrType() As Integer
        Get
            Return (ATD_TrType)
        End Get
        Set(ByVal Value As Integer)
            ATD_TrType = Value
        End Set
    End Property
    Public Property dATD_TransactionDate() As Date
        Get
            Return (ATD_TransactionDate)
        End Get
        Set(ByVal Value As Date)
            ATD_TransactionDate = Value
        End Set
    End Property
    Public Property iATD_ID() As Integer
        Get
            Return (ATD_ID)
        End Get
        Set(ByVal Value As Integer)
            ATD_ID = Value
        End Set
    End Property

    Public Property dATD_OpenDebit() As Decimal
        Get
            Return (ATD_OpenDebit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_OpenDebit = Value
        End Set
    End Property
    Public Property dATD_OpenCredit() As Decimal
        Get
            Return (ATD_OpenCredit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_OpenCredit = Value
        End Set
    End Property
    Public Property dATD_ClosingDebit() As Decimal
        Get
            Return (ATD_ClosingDebit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_ClosingDebit = Value
        End Set
    End Property
    Public Property dATD_ClosingCredit() As Decimal
        Get
            Return (ATD_ClosingCredit)
        End Get
        Set(ByVal Value As Decimal)
            ATD_ClosingCredit = Value
        End Set
    End Property
    Public Property iATD_SeqReferenceNum() As Integer
        Get
            Return (ATD_SeqReferenceNum)
        End Get
        Set(ByVal Value As Integer)
            ATD_SeqReferenceNum = Value
        End Set
    End Property
    Public Function GetRouteMasterDetails(ByVal sNameSpace As String, ByVal iRouteID As Integer) As DataTable   ', ByVal iVehicleTypeID As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LRM_VehicleType,LRM_DistinKms,LRM_Rate,LRM_DriverAlnceAmt,LRM_StartPlace,LRM_DestPlace,LRM_PetrolQty,LRM_AllottedTime From Lgst_Route_Master Where LRM_ID=" & iRouteID & " and LRM_Delflag = 'A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindStartCustomer(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Cust_ID,Cust_Name From MST_Customer_Master Where Cust_CompID=" & iCompID & "  order by Cust_Name "
            'sSql = "Select ACM_ID,ACM_Name From Acc_Customer_Master Where ACM_CompID=" & iCompID & " and ACM_Status='A' order by ACM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDestinationCustomer(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            'sSql = "Select ACM_ID,ACM_Name From Acc_Customer_Master Where ACM_CompID=" & iCompID & " and ACM_Status='A' order by ACM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTripGenerationDetails(ByVal sNameSpace As String, ByVal objTGM As clsTripGeneration) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(56) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objTGM.sLTGM_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_RouteID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_RouteID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_VehicleType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_VehicleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_VehicleNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_VehicleNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_StartCity", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objTGM.sLTGM_StartCity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_DestinationCity", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objTGM.sLTGM_DestinationCity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_DistanceinKms", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_DistanceinKms
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_Rate", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_PetrolQty", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_PetrolQty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_StartCustomer", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_StartCustomer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_DestinationCustomer", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_DestinationCustomer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_SVCNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objTGM.sLTGM_SVCNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_ClientRefNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objTGM.sLTGM_ClientRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_Driver", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_Driver
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_DriverAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_DriverAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_Remarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objTGM.sLTGM_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_StartTime", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objTGM.sLTGM_StartTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_StopTime", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objTGM.sLTGM_StopTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_StartDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objTGM.dLTGM_StartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_StopDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objTGM.dLTGM_StopDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_TripStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_TripStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_EWayBillNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objTGM.sLTGM_EWayBillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_AllottedTime", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objTGM.sLTGM_AllottedTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_TripTakenTime", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objTGM.sLTGM_TripTakenTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_TimeStatus", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objTGM.sLTGM_TimeStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_MRStart", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_MRStart
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_MREnd", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_MREnd
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_MRStatus", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objTGM.sLTGM_MRStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CompanyAddress", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objTGM.sLTGM_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objTGM.sLTGM_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CustomerAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objTGM.sLTGM_CustomerAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CustomerGSTNRegNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objTGM.sLTGM_CustomerGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_GSTRate", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_GSTAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_GSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_SGST", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_SGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_SGSTAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_SGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CGST", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_CGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CGSTAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_CGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_IGST", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_IGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_IGSTAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_IGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_GSTCustBillStatus", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objTGM.sLTGM_GSTCustBillStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_State", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objTGM.sLTGM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objTGM.sLTGM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objTGM.sLTGM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objTGM.dLTGM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objTGM.dLTGM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGM.iLTGM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objTGM.sLTGM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objTGM.sLTGM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGM_EscAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGM.dLTGM_EscAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_TripGeneration_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTGHopOnDetails(ByVal sNameSpace As String, ByVal objTGD As clsTripGeneration) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGHD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_TripID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGHD_TripID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_InDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objTGD.dLTGHD_InDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_OutDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objTGD.dLTGHD_OutDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_InTime", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objTGD.sLTGHD_InTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_OutTime", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objTGD.sLTGHD_OutTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_HopOnDetails", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objTGD.sLTGHD_HopOnDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objTGD.sLTGHD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objTGD.sLTGHD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGHD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objTGD.dLTGHD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGHD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGHD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objTGD.sLTGHD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGHD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objTGD.sLTGHD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_TripGenHopOn_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTGDieselPumpDetails(ByVal sNameSpace As String, ByVal objTGD As clsTripGeneration) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGDD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_TripID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGDD_TripID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_PumpId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGDD_PumpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_IndDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_IndDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_DieselinLtrs", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_DieselinLtrs
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_DieselRatePerltr", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_DieselRatePerltr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_OilInltr", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_OilInltr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_OilAmountInLtr", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_OilAmountInLtr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_DieselAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_DieselAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1



            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_DriverAdvancGvnByPump", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_DriverAdvancGvnByPump
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_OtherExpenses", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_OtherExpenses
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_Remarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objTGD.sLTGDD_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objTGD.sLTGDD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objTGD.sLTGDD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGDD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objTGD.dLTGDD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGDD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objTGD.iLTGDD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objTGD.sLTGDD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LTGDD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objTGD.sLTGDD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_TripGenDiesel_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal ObjTG As clsTripGeneration) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_ZoneID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_RegionID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_AreaID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_BranchID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.dATD_OpenDebit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.dATD_OpenCredit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.dATD_ClosingDebit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.dATD_ClosingCredit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjTG.iATD_SeqReferenceNum)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Transactions_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateTripMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
        Dim sSql As String = ""
        Dim dt, dtDebitCredit As New DataTable
        Dim dOpnDebit, dOpnCredit, dClosingDebit, dClosingCredit As Double
        Dim iSequenceNum As Integer
        Try
            sSql = "" : sSql = "Update Lgst_TripGeneration_Master Set LTGM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " LTGM_Status='A',LTGM_Delflag='A'"
            ElseIf sStatus = "D" Then
                sSql = sSql & " LTGM_Status='D',LTGM_DeletedBy= " & iUserID & ",LTGM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " LTGM_Status='A' "
            End If
            sSql = sSql & " Where LTGM_ID = " & iMasId & " And LTGM_YearID=" & iyearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)



            dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=15 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "" : sSql = "Select Count(Atd_id) from Acc_Transactions_Details where atd_gl=" & dt.Rows(i)("ATD_GL") & " and ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0"
                    Dim iCountGl As Integer = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                    sSql = "" : sSql = "Select Count(Atd_id) from Acc_Transactions_Details where  ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0"
                    iSequenceNum = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                    iSequenceNum = iSequenceNum + 1
                    If iCountGl = 0 Then

                        sSql = "" : sSql = "Select Opn_CreditAmount,Opn_DebitAmt from acc_opening_balance where Opn_glId=" & dt.Rows(i)("ATD_GL") & " and Opn_YearID =" & iyearID & " and Opn_CompID=" & iCompID & ""
                        dtDebitCredit = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                        If dtDebitCredit.Rows.Count > 0 Then
                            dOpnDebit = dtDebitCredit.Rows(0)("Opn_DebitAmt") + dt.Rows(i)("ATD_Debit")
                            dOpnCredit = dtDebitCredit.Rows(0)("Opn_CreditAmount") + dt.Rows(i)("ATD_Credit")
                            If dOpnDebit > dOpnCredit Then
                                dClosingDebit = dOpnDebit - dOpnCredit
                                dClosingCredit = "0.00"
                            End If
                            If dOpnCredit > dOpnDebit Then
                                dClosingCredit = dOpnCredit - dOpnDebit
                                dClosingDebit = "0.00"
                            End If
                            sSql = "" : sSql = "Update Acc_Transactions_Details Set ATD_IPAddress='" & sIPAddress & "'"
                            sSql = sSql & ",ATD_OpenDebit='" & dtDebitCredit.Rows(0)("Opn_DebitAmt") & "',ATD_OpenCredit='" & dtDebitCredit.Rows(0)("Opn_CreditAmount") & "',ATD_ClosingDebit='" & dClosingDebit & "',ATD_ClosingCredit='" & dClosingCredit & "',ATD_SeqReferenceNum=" & iSequenceNum & ""
                            If sStatus = "W" Then
                                sSql = sSql & " ,ATD_Status='A',ATD_ApprovedBy= " & iUserID & ",ATD_ApprovedOn=GetDate()"
                            ElseIf sStatus = "D" Then
                                sSql = sSql & " ,ATD_Status='D',ATD_DeletedBy= " & iUserID & ",ATD_DeletedOn=GetDate()"
                            ElseIf sStatus = "A" Then
                                sSql = sSql & " ,ATD_Status='A',ATD_ApprovedBy=" & iUserID & ",ATD_ApprovedOn=GetDate() "
                            End If
                            sSql = sSql & " Where ATD_ID = " & dt.Rows(i)("ATD_ID") & " And ATD_YearID =" & iyearID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                        End If
                    Else
                        sSql = "" : sSql = "Select top 1  Atd_ClosingCredit,ATD_ClosingDebit from Acc_Transactions_Details where atd_gl=" & dt.Rows(i)("ATD_GL") & " and Atd_YearID =" & iyearID & " and Atd_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0 order by ATD_SeqReferenceNum desc"
                        dtDebitCredit = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                        If dt.Rows.Count > 0 Then
                            dOpnDebit = dtDebitCredit.Rows(0)("ATD_ClosingDebit") + dt.Rows(i)("ATD_Debit")
                            dOpnCredit = dtDebitCredit.Rows(0)("Atd_ClosingCredit") + dt.Rows(i)("ATD_Credit")

                            If dOpnDebit > dOpnCredit Then
                                dClosingDebit = dOpnDebit - dOpnCredit
                                dClosingCredit = "0.00"
                            End If
                            If dOpnCredit > dOpnDebit Then
                                dClosingCredit = dOpnCredit - dOpnDebit
                                dClosingDebit = "0.00"
                            End If

                            sSql = "" : sSql = "Update Acc_Transactions_Details Set ATD_IPAddress='" & sIPAddress & "'"
                            sSql = sSql & ",ATD_OpenDebit='" & dtDebitCredit.Rows(0)("ATD_ClosingDebit") & "',ATD_OpenCredit='" & dtDebitCredit.Rows(0)("Atd_ClosingCredit") & "',ATD_ClosingDebit='" & dClosingDebit & "',ATD_ClosingCredit='" & dClosingCredit & "',ATD_SeqReferenceNum=" & iSequenceNum & ""
                            If sStatus = "W" Then
                                sSql = sSql & " ,ATD_Status='A',ATD_ApprovedBy= " & iUserID & ",ATD_ApprovedOn=GetDate()"
                            ElseIf sStatus = "D" Then
                                sSql = sSql & " ,ATD_Status='D',ATD_DeletedBy= " & iUserID & ",ATD_DeletedOn=GetDate()"
                            ElseIf sStatus = "A" Then
                                sSql = sSql & " ,ATD_Status='A',ATD_ApprovedBy=" & iUserID & ",ATD_ApprovedOn=GetDate() "
                            End If
                            sSql = sSql & " Where ATD_ID = " & dt.Rows(i)("ATD_ID") & " And ATD_YearID =" & iyearID & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    End If

                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadTGMasterlDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iLPMid As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_TripGeneration_Master Where LTGM_ID=" & iLPMid & "  And LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function BindHopOnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim dr As OleDb.OleDbDataReader
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Try
    '        dt.Columns.Add("HoponID")
    '        dt.Columns.Add("InTime")
    '        dt.Columns.Add("OutTime")
    '        dt.Columns.Add("Remarks")

    '        sSql = "Select * from Lgst_TripGenHopOn_Details Where LTGHD_DelFlag<>'D' And LTGHD_TripID=" & iCustomerID & " And LTGHD_CompID=" & iCompID & " Order by LTGHD_ID"
    '        dr = objDBL.SQLDataReader(sNameSpace, sSql)

    '        If dr.HasRows Then
    '            While dr.Read
    '                dRow = dt.NewRow
    '                dRow("HoponID") = dr("LTGHD_ID")
    '                dRow("InTime") = dr("LTGHD_InTime")
    '                dRow("OutTime") = dr("LTGHD_OutTime")
    '                dRow("Remarks") = dr("LTGHD_HopOnDetails")

    '                dt.Rows.Add(dRow)
    '            End While
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function BindHopOnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("HoponID")
            dt.Columns.Add("InDate")
            dt.Columns.Add("InTime")
            dt.Columns.Add("OutDate")
            dt.Columns.Add("OutTime")
            dt.Columns.Add("Remarks")

            sSql = "Select * from Lgst_TripGenHopOn_Details Where LTGHD_DelFlag<>'D' And LTGHD_TripID=" & iCustomerID & " And LTGHD_CompID=" & iCompID & " and LTGHD_YearID= " & iYearId & "  Order by LTGHD_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("HoponID") = dr("LTGHD_ID")
                    dRow("InDate") = objGen.FormatDtForRDBMS(dr("LTGHD_InDate").ToString(), "D")
                    dRow("InTime") = dr("LTGHD_InTime")
                    dRow("OutDate") = objGen.FormatDtForRDBMS(dr("LTGHD_OutDate").ToString(), "D")
                    dRow("OutTime") = dr("LTGHD_OutTime")
                    dRow("Remarks") = dr("LTGHD_HopOnDetails")

                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHopOnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iTripID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_TripGenHopOn_Details Where LTGHD_DelFlag<>'D' And LTGHD_ID=" & iID & " And LTGHD_TripID=" & iTripID & " And LTGHD_CompID=" & iCompID & " and LTGHD_YearID= " & iYearId & " Order by LTGHD_ID"
            GetHopOnDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetHopOnDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteHopOnValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iTripID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_TripGenHopOn_Details Set LTGHD_DelFlag='D' Where LTGHD_ID=" & iID & " And LTGHD_TripID=" & iTripID & " And LTGHD_CompID=" & iCompID & " and LTGHD_YearID= " & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDieselPumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iTripID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_TripGenDiesel_Details Where LTGDD_DelFlag<>'D' And LTGDD_ID=" & iID & " And LTGDD_TripID=" & iTripID & " And LTGDD_CompID=" & iCompID & " and LTGDD_YearID=" & iYearId & " Order by LTGDD_ID"
            GetDieselPumpDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetDieselPumpDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteDieselPumpValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iTripID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_TripGenDiesel_Details Set LTGDD_DelFlag='D' Where LTGDD_ID=" & iID & " And LTGDD_TripID=" & iTripID & " And LTGDD_CompID=" & iCompID & " and LTGDD_YearID=" & iYearId & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadExistingTripGenNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LTGM_ID,LTGM_TransactionNo From Lgst_TripGeneration_Master Where LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDieselPump(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iRouteId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select LPM_Id,LPM_PumpName from lgst_pump_master where LPM_ID in (Select distinct (LRD_PumpID) From lgst_routepump_details Where LRD_CompID=" & iCompID & "  and LRD_RouteID=" & iRouteId & " and LRD_DElflag='X')"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub UpdateTGStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_TripGeneration_Master Set LTGM_IPAddress='" & sIPAddress & "',"
            sSql = sSql & " LTGM_Status='A',LTGM_DelFlag='A',LTGM_ApprovedBy= " & iUserID & ",LTGM_ApprovedOn=GetDate()"
            sSql = sSql & " Where LTGM_CompID=" & iCompID & " And LTGM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadCity(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCityID As Integer) As String
        Dim sSql As String
        Dim sCity As String
        Try
            sSql = "Select Mas_desc from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='City' and Mas_DelFlag='X') and Mas_Id= " & iCityID & " and Mas_Delflag ='A' Order By Mas_Desc"
            sCity = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sCity
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function BindDieselPumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As DataTable
    '    Dim sSql As String
    '    Dim dr As OleDb.OleDbDataReader
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Try
    '        dt.Columns.Add("ID")
    '        dt.Columns.Add("DieselName")
    '        dt.Columns.Add("DieselinLtrs")
    '        dt.Columns.Add("Amount")

    '        sSql = "Select * from Lgst_TripGenDiesel_Details Where LTGDD_DelFlag<>'D' And LTGDD_TripID=" & iCustomerID & " And LTGDD_CompID=" & iCompID & " Order by LTGDD_ID"
    '        dr = objDBL.SQLDataReader(sNameSpace, sSql)

    '        If dr.HasRows Then
    '            While dr.Read
    '                dRow = dt.NewRow
    '                dRow("ID") = dr("LTGDD_ID")
    '                dRow("DieselName") = GetDieselPumpName(sNameSpace, dr("LTGDD_PumpID"), iCompID)
    '                dRow("DieselinLtrs") = dr("LTGDD_DieselinLtrs")
    '                dRow("Amount") = dr("LTGDD_DieselAmount")

    '                dt.Rows.Add(dRow)
    '            End While
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function BindDieselPumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DieselName")
            dt.Columns.Add("Date")
            dt.Columns.Add("DieselinLtrs")
            dt.Columns.Add("RateperLtrs")
            dt.Columns.Add("AdvancePaidToDriver")
            dt.Columns.Add("Amount")
            dt.Columns.Add("OtherExp")
            dt.Columns.Add("OtherRemarks")
            dt.Columns.Add("OilInLtr")
            dt.Columns.Add("OilRateLtr")
            dt.Columns.Add("CreatedBy")
            dt.Columns.Add("PumpID")

            sSql = "Select * from Lgst_TripGenDiesel_Details Where LTGDD_DelFlag<>'D' And LTGDD_TripID=" & iCustomerID & " And LTGDD_CompID=" & iCompID & " and LTGDD_YearID=" & iYearId & " Order by LTGDD_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                i = 0
                While dr.Read
                    i = i + 1
                    dRow = dt.NewRow
                    dRow("ID") = dr("LTGDD_ID")
                    dRow("SrNo") = i
                    dRow("Date") = objGen.FormatDtForRDBMS(dr("LTGDD_IndDate").ToString(), "D")
                    dRow("DieselName") = GetDieselPumpName(sNameSpace, dr("LTGDD_PumpID"), iCompID)
                    dRow("DieselinLtrs") = dr("LTGDD_DieselinLtrs")
                    dRow("RateperLtrs") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_DieselRatePerltr")))
                    If dr("LTGDD_DriverAdvancGvnByPump").ToString = 0 Then
                        dRow("AdvancePaidToDriver") = 0.0
                    Else
                        dRow("AdvancePaidToDriver") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_DriverAdvancGvnByPump")))
                    End If
                    '  dRow("AdvancePaidToDriver") = dr("LTGDD_DriverAdvancGvnByPump")
                    dRow("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_DieselAmount")))
                    If dr("LTGDD_OtherExpenses").ToString = 0 Then
                        dRow("OtherExp") = ""
                    Else
                        dRow("OtherExp") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_OtherExpenses")))
                    End If
                    dRow("OtherRemarks") = dr("LTGDD_Remarks")
                    If dr("LTGDD_OilInltr").ToString = 0 Then
                        dRow("OilInLtr") = ""
                    Else
                        dRow("OilInLtr") = dr("LTGDD_OilInltr")
                    End If
                    If dr("LTGDD_OilAmountInLtr").ToString = 0 Then
                        dRow("OilRateLtr") = ""
                    Else
                        dRow("OilRateLtr") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_OilAmountInLtr")))
                    End If
                    If IsDBNull(dr("LTGDD_CreatedBy").ToString()) = False Then
                        dRow("CreatedBy") = GetUserName(sNameSpace, dr("LTGDD_CreatedBy").ToString())
                    End If
                    dRow("PumpID") = dr("LTGDD_PumpID")
                    dt.Rows.Add(dRow)

                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDieselPumpName(ByVal sNameSpace As String, ByVal iPumpID As Integer, ByVal iCompID As Integer) As String
        Dim sSQL As String = ""
        Dim sPump As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LPM_PumpName from Lgst_Pump_Master where LPM_ID = " & iPumpID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LPM_PumpName").ToString()) = False Then
                    sPump = dt.Rows(0)("LPM_PumpName").ToString()
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
    Public Function LoadTripGenDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Dim sVehicleType As String = ""
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TripNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Route", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("StartDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("VechicleNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CreatedBy", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TripStatus", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Lgst_TripGeneration_Master where LTGM_CompID =" & iCompID & " and  LTGM_YearID=" & iYearID & ""
            If iStatus = 0 Then
                sSql = sSql & " And LTGM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And LTGM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And LTGM_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By LTGM_StartDate DESC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("LTGM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("LTGM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("LTGM_TransactionNo").ToString()) = False Then
                        dr("TripNo") = ds.Tables(0).Rows(i)("LTGM_TransactionNo").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("LTGM_VehicleType").ToString()) = False Then
                        sVehicleType = getVechicleType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("LTGM_VehicleType").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LTGM_DestinationCustomer").ToString()) = False Then
                        dr("Customer") = GetCustomerName(sNameSpace, ds.Tables(0).Rows(i)("LTGM_DestinationCustomer").ToString())
                    End If

                    If ((IsDBNull(ds.Tables(0).Rows(i)("LTGM_StartCity").ToString()) = False) And (IsDBNull(ds.Tables(0).Rows(i)("LTGM_DestinationCity").ToString()) = False)) Then
                        dr("Route") = ds.Tables(0).Rows(i)("LTGM_StartCity").ToString() & " - " & ds.Tables(0).Rows(i)("LTGM_DestinationCity").ToString() & " " & sVehicleType
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LTGM_StartDate").ToString()) = False Then
                        dr("StartDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("LTGM_StartDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LTGM_VehivleNo").ToString()) = False Then
                        dr("VechicleNo") = GetVechicleNo(sNameSpace, ds.Tables(0).Rows(i)("LTGM_VehivleNo").ToString())
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("LTGM_CreatedBy").ToString()) = False Then
                        dr("CreatedBy") = GetUserName(sNameSpace, ds.Tables(0).Rows(i)("LTGM_CreatedBy").ToString())
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("LTGM_TripStatus").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("LTGM_TripStatus").ToString() = 1 Then
                            dr("TripStatus") = "Start"
                        Else
                            dr("TripStatus") = "End"
                        End If

                    End If


                    If (ds.Tables(0).Rows(i)("LTGM_DelFlag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("LTGM_DelFlag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("LTGM_DelFlag") = "D") Then
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
    Public Sub UpdateTripGenerationMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_TripGeneration_Master Set LTGM_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " LTGM_Status='A',LTGM_DelFlag='A',LTGM_ApprovedBy= " & iUserID & " , LTGM_ApprovedOn=GetDate()"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " LTGM_Status='D',LTGM_DelFlag='D',LTGM_DeletedBy= " & iUserID & " , LTGM_DeletedOn=GetDate()"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " LTGM_Status='A',LTGM_DelFlag='A' "
            End If
            sSql = sSql & " Where LTGM_CompID=" & iCompID & " And LTGM_ID = " & iMasId & " and LTGM_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateTripGenMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Lgst_TripGeneration_Master Where LTGM_DelFlag='" & sSelectedStatus & "' And LTGM_CompID=" & iCompID & "  "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Lgst_TripGeneration_Master Set LTGM_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " LTGM_Status='A',LTGM_DelFlag='A',LTGM_ApprovedBy= " & iUserID & ",LTGM_ApprovedOn=GetDate()"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " LTGM_Status='D',LTGM_DelFlag='D',LTGM_DeletedBy= " & iUserID & ",LTGM_DeletedOn=GetDate()"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " LTGM_Status='A',LTGM_DelFlag='A' "
                    End If
                    sSql = sSql & " Where LTGM_CompID=" & iCompID & " And LTGM_ID = " & iMasId & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetCustomerName(ByVal sNameSpace As String, ByVal iCustID As Integer) As String
        Dim sSQL As String = ""
        Dim sCustomerName As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select BM_Name  from Sales_Buyers_Masters where BM_ID = " & iCustID & " "
            'sSQL = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("BM_Name").ToString()) = False Then
                    sCustomerName = dt.Rows(0)("BM_Name").ToString()
                Else
                    sCustomerName = ""
                End If
            Else
                sCustomerName = ""
            End If
            Return sCustomerName
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetVechicleNo(ByVal sNameSpace As String, ByVal iLVMID As Integer) As String
        Dim sSQL As String = ""
        Dim sVehicleRegNo As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LVM_RegNo  from Lgst_Vehicle_Master where LVM_ID = " & iLVMID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LVM_RegNo").ToString()) = False Then
                    sVehicleRegNo = dt.Rows(0)("LVM_RegNo").ToString()
                Else
                    sVehicleRegNo = ""
                End If
            Else
                sVehicleRegNo = ""
            End If
            Return sVehicleRegNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserName(ByVal sNameSpace As String, ByVal iLVMUsrID As Integer) As String
        Dim sSQL As String = ""
        Dim sUsrName As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select Usr_FullName  from Sad_UserDetails where usr_Id = " & iLVMUsrID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Usr_FullName").ToString()) = False Then
                    sUsrName = dt.Rows(0)("Usr_FullName").ToString()
                Else
                    sUsrName = ""
                End If
            Else
                sUsrName = ""
            End If
            Return sUsrName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GettotalNoOfPetrolInltrs(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustomerID As Integer) As Double
        Dim sSQL As String = ""
        Dim dTotalDieselAmount As Double
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select sum(LTGDD_DieselinLtrs) from Lgst_TripGenDiesel_Details Where LTGDD_DelFlag<>'D' And LTGDD_TripID=" & iCustomerID & " And LTGDD_CompID=" & iCompID & ""
            dTotalDieselAmount = objDBL.SQLExecuteScalar(sNameSpace, sSQL)
            Return dTotalDieselAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GettotalDriverAdvance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustomerID As Integer) As Double
        Dim sSQL As String = ""
        Dim dDriverAdvance As Double
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select sum(LTGDD_DriverAdvancGvnByPump) from Lgst_TripGenDiesel_Details Where LTGDD_DelFlag<>'D' And LTGDD_TripID=" & iCustomerID & " And LTGDD_CompID=" & iCompID & ""
            dDriverAdvance = objDBL.SQLExecuteScalar(sNameSpace, sSQL)
            Return dDriverAdvance
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GettotalPetrolinLtrs(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iRouteID As Integer) As Double
        Dim sSQL As String = ""
        Dim dTotalPetrolLtrs As Double
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LRM_PetrolQty from Lgst_Route_Master Where LRM_DelFlag<>'D' And LRM_ID=" & iRouteID & " And LTGDD_CompID=" & iCompID & ""
            dTotalPetrolLtrs = objDBL.SQLExecuteScalar(sNameSpace, sSQL)
            Return dTotalPetrolLtrs
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
    Public Function LoadExistingRouteNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LRM_ID,LRM_StartDestPlace From Lgst_Route_Master Where LRM_CompID=" & iCompID & " and LRM_Delflag='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingRouteAll(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LRM_ID,LRM_StartDestPlace From Lgst_Route_Master Where LRM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(Count(LTGM_ID)+1,1) from Lgst_TripGeneration_Master Where LTGM_YearID=" & iYearID & " And LTGM_CompID=" & iCompID & " ")
            sPrefix = "TR" & "0000" & iMax
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select BM_SubGL,BM_GL,BM_GSTNRegNo,BM_Address1,BM_GSTNCategory From Sales_Buyers_Masters Where BM_ID=" & iCustomerID & " And BM_CompID=" & iCompID & " "
            GetCustomerDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCustomerDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGstCategory As Integer) As String
        Dim sSql As String = ""
        Try
            'sSql = "Select Mas_Desc from ACC_General_Master where mas_master = 2 and Mas_Id=" & iCompTypeId & " and Mas_CompID=" & iCompID & ""
            'Desc = objDB.SQLGetDescription(sNameSpace, sSql)

            sSql = "select GC_GSTCategory from GSTcategory_table where GC_ID=" & iGstCategory & " and GC_CompID=" & iCompID & ""
            GetGSTDescription = objDBL.SQLGetDescription(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Cust_FinalNo,Cust_comm_address From MST_Customer_Master Where CUST_CompID=" & iCompID & " "
            GetCompanyDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCompanyDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SElect GL_ID From Chart_Of_Accounts Where GL_Desc Like '%" & sDesc & "%' And GL_CompID=" & iCompID & " "
            GetGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGSTNRegNo As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select GR_StateName From GSTN_RegNo_Master Where GR_TIN='" & sGSTNRegNo & "' And GR_CompID=" & iCompID & " "
            GetState = objDBL.SQLGetDescription(sNameSpace, sSql).ToString
            Return GetState
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAHeadID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_AccHead from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_head=2 and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & " and gl_Status <> 'D' order by gl_id desc"
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHeadID As Integer, ByVal iGLID As Integer) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Try
            sSql = "Select gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iHeadID & " And gl_Id=" & iGLID & " order by gl_glcode"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Try
            sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_id=" & iSubGL & " and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccountDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String, ByVal sColumn As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from Acc_Application_Settings where Acc_Types = '" & sType & "' and "
            sSql = sSql & " Acc_LedgerType ='" & sLedgerType & "' and Acc_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyAccountDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String, ByVal sColumn As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from Acc_Application_Settings where Acc_Types = '" & sType & "' and "
            sSql = sSql & " Acc_LedgerType ='" & sLedgerType & "' and Acc_CompID=" & iCompID & " "
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartySubGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As Integer
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select BM_NAme From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_DelFlag='A' And BM_CompID=" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            GetPartySubGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPartySubGLID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDieselSubGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As Integer
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select LPM_PumpName From Lgst_Pump_Master Where LPM_ID=" & iPartyID & " And LPM_DelFlag='A' And LPM_CompID=" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            GetDieselSubGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDieselSubGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDieselSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try

            sSql = "" : sSql = "Select LPM_PumpName From Lgst_Pump_Master Where LPM_ID=" & iPartyID & " And LPM_DelFlag='A' And LPM_CompID=" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOtherSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try

            'sSql = "" : sSql = "Select LPM_PumpName From Lgst_Pump_Master Where LPM_ID=" & iPartyID & " And LPM_DelFlag='A' And LPM_CompID=" & iCompID & " "
            'sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sACM_Type & "' and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDriverSubGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As Integer
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select LDM_DriverName From Lgst_Driver_Master Where LDM_ID=" & iPartyID & " And LDM_DelFlag='A' And LDM_CompID=" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sACM_Type & "' and gl_head=3"
            GetDriverSubGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDriverSubGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDriverlSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sParty As String) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try

            sSql = "" : sSql = "Select LDM_DriverName From Lgst_Driver_Master Where LDM_ID=" & iPartyID & " And LDM_DelFlag='A' And LDM_CompID=" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sParty & "' and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartySubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select BM_NAme From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_DelFlag='A' And BM_CompID=" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PaymentID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SrNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Type", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGL", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpeningBalance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Balance", GetType(String))
            dt.Columns.Add(dc)


            sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
            sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iInvID & " and A.ATD_trType = 15 and "
            sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_YearID=" & iYearID & " And A.ATD_BillId = " & iInvID & " order by a.Atd_id "


            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("SrNo") = i + 1

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("ATD_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Head").ToString()) = False Then
                        dr("HeadID") = ds.Tables(0).Rows(i)("ATD_Head").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_GL").ToString()) = False Then
                        dr("GLID") = ds.Tables(0).Rows(i)("ATD_GL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_SubGL").ToString()) = False Then
                        dr("SubGLID") = ds.Tables(0).Rows(i)("ATD_SubGL").ToString()
                    End If

                    'If IsDBNull(ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()) = False Then
                    '    dr("PaymentID") = ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()

                    '    If ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "1" Then
                    '        dr("Type") = "Advance Payment"
                    '    ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "2" Then
                    '        dr("Type") = "Bill Passing"
                    '    ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "3" Then
                    '        dr("Type") = "Payment"
                    '    ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "4" Then
                    '        dr("Type") = "Cheque"
                    '    End If
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()) = False Then
                        dr("PaymentID") = ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()

                        If ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "5" Then
                            dr("Type") = "Bill Amount"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "6" Then
                            dr("Type") = "SGST"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "7" Then
                            dr("Type") = "CGST"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "8" Then
                            dr("Type") = "IGST"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "9" Then
                            dr("Type") = "Party/Customer"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "16" Then
                            dr("Type") = "Pump Owner"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "17" Then
                            dr("Type") = "Diesel Expenses"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "18" Then
                            dr("Type") = "Driver Advance"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "19" Then
                            dr("Type") = "Other Advance"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "20" Then
                            dr("Type") = "Escallated Amount"
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GLCode").ToString()) = False Then
                        dr("GLCode") = ds.Tables(0).Rows(i)("GLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GLDescription").ToString()) = False Then
                        dr("GLDescription") = ds.Tables(0).Rows(i)("GLDescription").ToString()

                        'If IsDBNull(ds.Tables(0).Rows(i)("GLDebit").ToString()) = False Then
                        '    If ds.Tables(0).Rows(i)("GLDebit").ToString() <> "0.00" Then
                        '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("GLDebit").ToString()
                        '    End If
                        'End If

                        'If IsDBNull(ds.Tables(0).Rows(i)("GLCredit").ToString()) = False Then
                        '    If ds.Tables(0).Rows(i)("GLCredit").ToString() <> "0.00" Then
                        '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("GLCredit").ToString()
                        '    End If
                        'End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLCode").ToString()) = False Then
                        dr("SubGL") = ds.Tables(0).Rows(i)("SubGLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLDesc").ToString()) = False Then
                        dr("SubGLDescription") = ds.Tables(0).Rows(i)("SubGLDesc").ToString()

                        'If IsDBNull(ds.Tables(0).Rows(i)("SubGLDebit").ToString()) = False Then
                        '    If ds.Tables(0).Rows(i)("SubGLDebit").ToString() <> "0.00" Then
                        '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("SubGLDebit").ToString()
                        '    End If
                        'End If

                        'If IsDBNull(ds.Tables(0).Rows(i)("SubGLCredit").ToString()) = False Then
                        '    If ds.Tables(0).Rows(i)("SubGLCredit").ToString() <> "0.00" Then
                        '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("SubGLCredit").ToString()
                        '    End If
                        'End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Debit").ToString()) = False Then
                        dr("Debit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Credit").ToString()) = False Then
                        dr("Credit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDieselPumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iTripID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("SlNo")
            dt.Columns.Add("DieselName")
            dt.Columns.Add("DieselinLtrs")
            dt.Columns.Add("RateperLtrs")
            dt.Columns.Add("AdvancePaidToDriver")
            dt.Columns.Add("OtherExpense")
            dt.Columns.Add("OilInLtr")
            dt.Columns.Add("OilRateLtr")
            dt.Columns.Add("Amount")
            dt.Columns.Add("Date")
            dt.Columns.Add("Remarks")
            sSql = "Select * from Lgst_TripGenDiesel_Details Where LTGDD_DelFlag<>'D' And LTGDD_ID=" & iID & " And LTGDD_TripID=" & iTripID & " And LTGDD_CompID=" & iCompID & " and LTGDD_YearID=" & iYearId & " Order by LTGDD_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                i = 0
                While dr.Read
                    dRow = dt.NewRow
                    dRow("ID") = dr("LTGDD_ID")
                    dRow("SlNo") = i + 1
                    dRow("Date") = objGen.FormatDtForRDBMS(dr("LTGDD_IndDate").ToString(), "D")
                    dRow("DieselName") = GetDieselPumpName(sNameSpace, dr("LTGDD_PumpID"), iCompID)
                    dRow("DieselinLtrs") = dr("LTGDD_DieselinLtrs")
                    dRow("RateperLtrs") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_DieselRatePerltr").ToString()))
                    dRow("AdvancePaidToDriver") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_DriverAdvancGvnByPump").ToString()))
                    dRow("OtherExpense") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_OtherExpenses").ToString()))
                    dRow("OilInLtr") = dr("LTGDD_OilInltr")
                    dRow("OilRateLtr") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_OilAmountInLtr").ToString()))
                    dRow("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LTGDD_DieselAmount").ToString()))
                    dRow("Remarks") = dr("LTGDD_Remarks")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMeterReading(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iVehId As Integer) As Double
        Dim sSql As String = ""
        Dim dMeterVal As Double = 0.0
        Try
            sSql = "" : sSql = "Select LTGM_MREnd from Lgst_TripGeneration_Master where "
            sSql = sSql & "LTGM_compid=" & iCompID & " and LTGM_VehivleNo = " & iVehId & " order by ltgm_id desc"
            dMeterVal = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return dMeterVal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerBilldate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iRouteId As Integer) As String
        Dim sSql As String = ""
        Dim sDate As String = ""
        Try
            sSql = "" : sSql = "Select Top 1 LCB_ToDate From Lgst_CustomerBilling where LCB_CustomerID=" & iCustID & "  and LCB_RouteID=" & iRouteId & "  and  LCB_Compid=" & iCompID & " and LCB_YearId=" & iYearID & " order by lcb_id desc"
            GetCustomerBilldate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetCustomerBilldate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDriverBilldate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDriverID As Integer) As String
        Dim sSql As String = ""
        Dim sDate As String = ""
        Try
            sSql = "" : sSql = "Select Top 1 LDB_ToDate From Lgst_DriverBilling where LDB_DriverID=" & iDriverID & "  and  LDB_Compid=" & iCompID & " and LDB_YearId=" & iYearID & " order by lDb_id desc"
            GetDriverBilldate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetDriverBilldate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPumpBilldate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPumpID As Integer) As String
        Dim sSql As String = ""
        Dim sDate As String = ""
        Try
            sSql = "" : sSql = "Select Top 1 LPB_ToDate From Lgst_PumpBilling where LPB_PumpID=" & iPumpID & "  and  LPB_Compid=" & iCompID & " and LPB_YearId=" & iYearID & " order by lPb_id desc"
            GetPumpBilldate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetPumpBilldate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccessCode(ByVal sAccessName As String) As DataTable
        Dim sSql As String
        Dim sAccessCode As DataTable
        Try
            sSql = "Select SAD_CMS_AccessCode,Sad_CMS_Name from Sad_CompanyMaster_Settings"
            sAccessCode = objDBL.SQLExecuteDataSet(sAccessName, sSql).Tables(0)
            Return sAccessCode
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
