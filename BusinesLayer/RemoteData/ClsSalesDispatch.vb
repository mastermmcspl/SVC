Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSalesDispatch
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iDM_ID As Integer
    Private iDM_OrderID As Integer
    Private dDM_OrderDate As DateTime
    Private iDM_SupplierID As Integer
    Private dDM_DispatchDate As DateTime
    Private iDM_ModeOfShipping As Integer
    Private iDM_ExpectedDays As Integer
    Private iDM_PaymentType As Integer
    Private iDM_CreatedBy As Integer
    Private dDM_CreatedOn As DateTime
    Private sDM_Status As String
    Private iDM_YearID As Integer
    Private iDM_CompID As Integer
    Private dDM_ShippingRate As Double
    Private sDM_ChequeNo As String
    Private dDM_ChequeDate As DateTime
    Private sDM_IFSCCode As String
    Private sDM_BankName As String
    Private sDM_Branch As String

    Private iDM_GrandDiscount As Double
    Private iDM_GrandDiscountAmt As Double
    Private iDM_GrandTotal As Double
    Private iDM_GrandTotalAmt As Double
    Private sDM_Code As String
    Private iDM_SalesManID As Integer
    Private sDM_DispatchRefNo As String
    Private sDM_ESugamNo As String
    Private sDM_Remarks As String
    Private iDM_SaleType As Integer
    Private iDM_OtherType As Integer
    Private iDM_AllocateID As Integer
    Private sDM_Operation As String
    Private sDM_IPAddress As String

    Private iDM_TrType As Integer
    Private sDM_CompanyAddress As String
    Private sDM_CompanyGSTNRegNo As String
    Private sDM_BillingAddress As String
    Private sDM_BillingGSTNRegNo As String
    Private sDM_DeliveryAddress As String
    Private sDM_DeliveryGSTNRegNo As String
    Private sDM_DeliveryFrom As String
    Private sDM_DeliveryFromGSTNRegNo As String
    Private sDM_DispatchStatus As String
    Private iDM_CompanyType As Integer
    Private iDM_GSTNCategory As Integer
    Private sDM_State As String

    Private iDD_ID As Integer
    Private iDD_MasterID As Integer
    Private iDD_CommodityID As Integer
    Private iDD_DescID As Integer
    Private iDD_UnitID As Integer
    Private iDD_HistoryID As Integer
    Private sDD_Rate As String
    Private iDD_Quantity As Double
    Private sDD_RateAmount As String
    Private sDD_Status As String
    Private iDD_CompID As Integer

    Private sDD_Operation As String
    Private sDD_IPAddress As String
    Private iDD_CreatedBy As Integer
    Private iDD_CreatedOn As Date

    Private iDD_GST_ID As Integer
    Private sDD_GSTRate As Double

    Private iC_ID As Integer
    Private iC_OrderID As Integer
    Private iC_AllocatedID As Integer
    Private iC_DispatchID As Integer
    Private sC_OrderType As String
    Private iC_ChargeID As Integer
    Private sC_ChargeType As String
    Private iC_ChargeAmount As Double
    Private sC_PSType As String
    Private sC_DelFlag As String
    Private sC_Status As String
    Private iC_YearID As Integer
    Private iC_CompID As Integer
    Private iC_CreatedBy As Integer
    Private iC_CreatedOn As Date
    Private sC_Operation As String
    Private sC_IPAddress As String
    Private iC_SalesReturnID As Integer
    Private iC_GoodsReturnID As Integer

    Private sDM_OrderNo As String
    Private sDM_AllocationNo As String
    Private iDM_BatchNo As Integer
    Private iDM_BaseName As Integer

    Public Property DM_OrderNo() As String
        Get
            Return (sDM_OrderNo)
        End Get
        Set(ByVal Value As String)
            sDM_OrderNo = Value
        End Set
    End Property
    Public Property DM_AllocationNo() As String
        Get
            Return (sDM_AllocationNo)
        End Get
        Set(ByVal Value As String)
            sDM_AllocationNo = Value
        End Set
    End Property


    Public Property DM_BatchNo() As Integer
        Get
            Return (iDM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iDM_BatchNo = Value
        End Set
    End Property
    Public Property DM_BaseName() As Integer
        Get
            Return (iDM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iDM_BaseName = Value
        End Set
    End Property
    Public Property C_ID() As Integer
        Get
            Return (iC_ID)
        End Get
        Set(ByVal Value As Integer)
            iC_ID = Value
        End Set
    End Property
    Public Property C_OrderID() As Integer
        Get
            Return (iC_OrderID)
        End Get
        Set(ByVal Value As Integer)
            iC_OrderID = Value
        End Set
    End Property
    Public Property C_AllocatedID() As Integer
        Get
            Return (iC_AllocatedID)
        End Get
        Set(ByVal Value As Integer)
            iC_AllocatedID = Value
        End Set
    End Property
    Public Property C_DispatchID() As Integer
        Get
            Return (iC_DispatchID)
        End Get
        Set(ByVal Value As Integer)
            iC_DispatchID = Value
        End Set
    End Property
    Public Property C_OrderType() As String
        Get
            Return (sC_OrderType)
        End Get
        Set(ByVal Value As String)
            sC_OrderType = Value
        End Set
    End Property
    Public Property C_ChargeID() As Integer
        Get
            Return (iC_ChargeID)
        End Get
        Set(ByVal Value As Integer)
            iC_ChargeID = Value
        End Set
    End Property
    Public Property C_ChargeType() As String
        Get
            Return (sC_ChargeType)
        End Get
        Set(ByVal Value As String)
            sC_ChargeType = Value
        End Set
    End Property
    Public Property C_ChargeAmount() As Double
        Get
            Return (iC_ChargeAmount)
        End Get
        Set(ByVal Value As Double)
            iC_ChargeAmount = Value
        End Set
    End Property
    Public Property C_PSType() As String
        Get
            Return (sC_PSType)
        End Get
        Set(ByVal Value As String)
            sC_PSType = Value
        End Set
    End Property
    Public Property C_DelFlag() As String
        Get
            Return (sC_DelFlag)
        End Get
        Set(ByVal Value As String)
            sC_DelFlag = Value
        End Set
    End Property
    Public Property C_Status() As String
        Get
            Return (sC_Status)
        End Get
        Set(ByVal Value As String)
            sC_Status = Value
        End Set
    End Property
    Public Property C_YearID() As Integer
        Get
            Return (iC_YearID)
        End Get
        Set(ByVal Value As Integer)
            iC_YearID = Value
        End Set
    End Property
    Public Property C_CompID() As Integer
        Get
            Return (iC_CompID)
        End Get
        Set(ByVal Value As Integer)
            iC_CompID = Value
        End Set
    End Property
    Public Property C_CreatedBy() As Integer
        Get
            Return (iC_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iC_CreatedBy = Value
        End Set
    End Property
    Public Property C_CreatedOn() As DateTime
        Get
            Return (iC_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            iC_CreatedOn = Value
        End Set
    End Property
    Public Property C_Operation() As String
        Get
            Return (sC_Operation)
        End Get
        Set(ByVal Value As String)
            sC_Operation = Value
        End Set
    End Property
    Public Property C_IPAddress() As String
        Get
            Return (sC_IPAddress)
        End Get
        Set(ByVal Value As String)
            sC_IPAddress = Value
        End Set
    End Property
    Public Property C_SalesReturnID() As Integer
        Get
            Return (iC_SalesReturnID)
        End Get
        Set(ByVal Value As Integer)
            iC_SalesReturnID = Value
        End Set
    End Property
    Public Property C_GoodsReturnID() As Integer
        Get
            Return (iC_GoodsReturnID)
        End Get
        Set(ByVal Value As Integer)
            iC_GoodsReturnID = Value
        End Set
    End Property
    Public Property DM_AllocateID() As Integer
        Get
            Return (iDM_AllocateID)
        End Get
        Set(ByVal Value As Integer)
            iDM_AllocateID = Value
        End Set
    End Property
    Public Property DM_SaleType() As Integer
        Get
            Return (iDM_SaleType)
        End Get
        Set(ByVal Value As Integer)
            iDM_SaleType = Value
        End Set
    End Property
    Public Property DM_OtherType() As Integer
        Get
            Return (iDM_OtherType)
        End Get
        Set(ByVal Value As Integer)
            iDM_OtherType = Value
        End Set
    End Property
    Public Property DM_Remarks() As String
        Get
            Return (sDM_Remarks)
        End Get
        Set(ByVal Value As String)
            sDM_Remarks = Value
        End Set
    End Property
    Public Property DM_ESugamNo() As String
        Get
            Return (sDM_ESugamNo)
        End Get
        Set(ByVal Value As String)
            sDM_ESugamNo = Value
        End Set
    End Property
    Public Property DM_DispatchRefNo() As String
        Get
            Return (sDM_DispatchRefNo)
        End Get
        Set(ByVal Value As String)
            sDM_DispatchRefNo = Value
        End Set
    End Property
    Public Property DM_SalesManID() As Integer
        Get
            Return (iDM_SalesManID)
        End Get
        Set(ByVal Value As Integer)
            iDM_SalesManID = Value
        End Set
    End Property
    Public Property DM_Code() As String
        Get
            Return (sDM_Code)
        End Get
        Set(ByVal Value As String)
            sDM_Code = Value
        End Set
    End Property
    Public Property DM_GrandDiscount() As Double
        Get
            Return (iDM_GrandDiscount)
        End Get
        Set(ByVal Value As Double)
            iDM_GrandDiscount = Value
        End Set
    End Property
    Public Property DM_GrandDiscountAmt() As Double
        Get
            Return (iDM_GrandDiscountAmt)
        End Get
        Set(ByVal Value As Double)
            iDM_GrandDiscountAmt = Value
        End Set
    End Property
    Public Property DM_GrandTotal() As Double
        Get
            Return (iDM_GrandTotal)
        End Get
        Set(ByVal Value As Double)
            iDM_GrandTotal = Value
        End Set
    End Property
    Public Property DM_GrandTotalAmt() As Double
        Get
            Return (iDM_GrandTotalAmt)
        End Get
        Set(ByVal Value As Double)
            iDM_GrandTotalAmt = Value
        End Set
    End Property
    Public Property DM_ChequeNo() As String
        Get
            Return (sDM_ChequeNo)
        End Get
        Set(ByVal Value As String)
            sDM_ChequeNo = Value
        End Set
    End Property
    Public Property DM_ChequeDate() As Date
        Get
            Return (dDM_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            dDM_ChequeDate = Value
        End Set
    End Property
    Public Property DM_IFSCCode() As String
        Get
            Return (sDM_IFSCCode)
        End Get
        Set(ByVal Value As String)
            sDM_IFSCCode = Value
        End Set
    End Property
    Public Property DM_BankName() As String
        Get
            Return (sDM_BankName)
        End Get
        Set(ByVal Value As String)
            sDM_BankName = Value
        End Set
    End Property
    Public Property DM_Branch() As String
        Get
            Return (sDM_Branch)
        End Get
        Set(ByVal Value As String)
            sDM_Branch = Value
        End Set
    End Property
    Public Property DM_Operation() As String
        Get
            Return (sDM_Operation)
        End Get
        Set(ByVal Value As String)
            sDM_Operation = Value
        End Set
    End Property
    Public Property DM_IPAddress() As String
        Get
            Return (sDM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sDM_IPAddress = Value
        End Set
    End Property
    Public Property DM_ID() As Integer
        Get
            Return (iDM_ID)
        End Get
        Set(ByVal Value As Integer)
            iDM_ID = Value
        End Set
    End Property
    Public Property DM_OrderID() As Integer
        Get
            Return (iDM_OrderID)
        End Get
        Set(ByVal Value As Integer)
            iDM_OrderID = Value
        End Set
    End Property
    Public Property DM_OrderDate() As DateTime
        Get
            Return (dDM_OrderDate)
        End Get
        Set(ByVal Value As DateTime)
            dDM_OrderDate = Value
        End Set
    End Property
    Public Property DM_SupplierID() As Integer
        Get
            Return (iDM_SupplierID)
        End Get
        Set(ByVal Value As Integer)
            iDM_SupplierID = Value
        End Set
    End Property
    Public Property DM_DispatchDate() As DateTime
        Get
            Return (dDM_DispatchDate)
        End Get
        Set(ByVal Value As DateTime)
            dDM_DispatchDate = Value
        End Set
    End Property
    Public Property DM_ModeOfShipping() As Integer
        Get
            Return (iDM_ModeOfShipping)
        End Get
        Set(ByVal Value As Integer)
            iDM_ModeOfShipping = Value
        End Set
    End Property
    Public Property DM_ExpectedDays() As Integer
        Get
            Return (iDM_ExpectedDays)
        End Get
        Set(ByVal Value As Integer)
            iDM_ExpectedDays = Value
        End Set
    End Property
    Public Property DM_PaymentType() As Integer
        Get
            Return (iDM_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            iDM_PaymentType = Value
        End Set
    End Property
    Public Property DM_CreatedBy() As Integer
        Get
            Return (iDM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iDM_CreatedBy = Value
        End Set
    End Property
    Public Property DM_CreatedOn() As DateTime
        Get
            Return (dDM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dDM_CreatedOn = Value
        End Set
    End Property
    Public Property DM_Status() As String
        Get
            Return (sDM_Status)
        End Get
        Set(ByVal Value As String)
            sDM_Status = Value
        End Set
    End Property
    Public Property DM_YearID() As Integer
        Get
            Return (iDM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iDM_YearID = Value
        End Set
    End Property
    Public Property DM_CompID() As Integer
        Get
            Return (iDM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iDM_CompID = Value
        End Set
    End Property
    Public Property DM_ShippingRate() As Double
        Get
            Return (dDM_ShippingRate)
        End Get
        Set(ByVal Value As Double)
            dDM_ShippingRate = Value
        End Set
    End Property

    Public Property DD_ID() As Integer
        Get
            Return (iDD_ID)
        End Get
        Set(ByVal Value As Integer)
            iDD_ID = Value
        End Set
    End Property
    Public Property DD_MasterID() As Integer
        Get
            Return (iDD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iDD_MasterID = Value
        End Set
    End Property
    Public Property DD_CommodityID() As Integer
        Get
            Return (iDD_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iDD_CommodityID = Value
        End Set
    End Property
    Public Property DD_DescID() As Integer
        Get
            Return (iDD_DescID)
        End Get
        Set(ByVal Value As Integer)
            iDD_DescID = Value
        End Set
    End Property
    Public Property DD_UnitID() As Integer
        Get
            Return (iDD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            iDD_UnitID = Value
        End Set
    End Property
    Public Property DD_HistoryID() As Integer
        Get
            Return (iDD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iDD_HistoryID = Value
        End Set
    End Property
    Public Property DD_Rate() As Double
        Get
            Return (sDD_Rate)
        End Get
        Set(ByVal Value As Double)
            sDD_Rate = Value
        End Set
    End Property
    Public Property DD_Quantity() As Double
        Get
            Return (iDD_Quantity)
        End Get
        Set(ByVal Value As Double)
            iDD_Quantity = Value
        End Set
    End Property
    Public Property DD_RateAmount() As Double
        Get
            Return (sDD_RateAmount)
        End Get
        Set(ByVal Value As Double)
            sDD_RateAmount = Value
        End Set
    End Property
    Public Property DD_Status() As String
        Get
            Return (sDD_Status)
        End Get
        Set(ByVal Value As String)
            sDD_Status = Value
        End Set
    End Property
    Public Property DD_CompID() As Integer
        Get
            Return (iDD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iDD_CompID = Value
        End Set
    End Property
    Public Property DM_TrType() As Integer
        Get
            Return (iDM_TrType)
        End Get
        Set(ByVal Value As Integer)
            iDM_TrType = Value
        End Set
    End Property
    Public Property DM_CompanyAddress() As String
        Get
            Return (sDM_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sDM_CompanyAddress = Value
        End Set
    End Property
    Public Property DM_CompanyGSTNRegNo() As String
        Get
            Return (sDM_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sDM_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property DM_BillingAddress() As String
        Get
            Return (sDM_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sDM_BillingAddress = Value
        End Set
    End Property
    Public Property DM_BillingGSTNRegNo() As String
        Get
            Return (sDM_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sDM_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property DM_DeliveryFrom() As String
        Get
            Return (sDM_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sDM_DeliveryFrom = Value
        End Set
    End Property
    Public Property DM_DeliveryFromGSTNRegNo() As String
        Get
            Return (sDM_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sDM_DeliveryFromGSTNRegNo = Value
        End Set
    End Property

    Public Property DM_DeliveryAddress() As String
        Get
            Return (sDM_DeliveryAddress)
        End Get
        Set(ByVal Value As String)
            sDM_DeliveryAddress = Value
        End Set
    End Property
    Public Property DM_DeliveryGSTNRegNo() As String
        Get
            Return (sDM_DeliveryGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sDM_DeliveryGSTNRegNo = Value
        End Set
    End Property
    Public Property DM_DispatchStatus() As String
        Get
            Return (sDM_DispatchStatus)
        End Get
        Set(ByVal Value As String)
            sDM_DispatchStatus = Value
        End Set
    End Property
    Public Property DM_State() As String
        Get
            Return (sDM_State)
        End Get
        Set(ByVal Value As String)
            sDM_State = Value
        End Set
    End Property
    Public Property DM_CompanyType() As Integer
        Get
            Return (iDM_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iDM_CompanyType = Value
        End Set
    End Property
    Public Property DM_GSTNCategory() As Integer
        Get
            Return (iDM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iDM_GSTNCategory = Value
        End Set
    End Property

    Public Property DD_GST_ID() As Integer
        Get
            Return (iDD_GST_ID)
        End Get
        Set(ByVal Value As Integer)
            iDD_GST_ID = Value
        End Set
    End Property
    Public Property DD_GSTRate() As Double
        Get
            Return (sDD_GSTRate)
        End Get
        Set(ByVal Value As Double)
            sDD_GSTRate = Value
        End Set
    End Property
    Public Property DD_CreatedBy() As Integer
        Get
            Return (iDD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iDD_CreatedBy = Value
        End Set
    End Property
    Public Property DD_CreatedOn() As Date
        Get
            Return (iDD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            iDD_CreatedOn = Value
        End Set
    End Property
    Public Property DD_Operation() As String
        Get
            Return (sDD_Operation)
        End Get
        Set(ByVal Value As String)
            sDD_Operation = Value
        End Set
    End Property
    Public Property DD_IPAddress() As String
        Get
            Return (sDD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sDD_IPAddress = Value
        End Set
    End Property
    Public Function SaveDispatchMaster(ByVal sNameSpace As String, ByVal objDispatch As ClsSalesDispatch) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(51) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_OrderDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DM_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_SupplierID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_SupplierID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DispatchDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DM_DispatchDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ModeOfShipping", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_ModeOfShipping
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ExpectedDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_ExpectedDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_PaymentType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Status ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDispatch.DM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ShippingRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_ShippingRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ChequeNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.DM_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DM_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_IFSCCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDispatch.DM_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BankName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDispatch.DM_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Branch", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDispatch.DM_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.DM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.DM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GrandDiscount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_GrandDiscount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GrandDiscountAmt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_GrandDiscountAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GrandTotal", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_GrandTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GrandTotalAmt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_GrandTotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Code", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDispatch.sDM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_SalesManID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_SalesManID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DispatchRefNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDispatch.DM_DispatchRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ESugamNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDispatch.DM_ESugamNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objDispatch.DM_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_SaleType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_SaleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_OtherType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_OtherType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_AllocateID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_AllocateID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CompanyAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.sDM_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.sDM_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BillingAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.DM_BillingAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BillingGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.DM_BillingGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DeliveryFrom", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.sDM_DeliveryFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DeliveryFromGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.sDM_DeliveryFromGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DeliveryAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.DM_DeliveryAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DeliveryGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.DM_DeliveryGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DispatchStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.sDM_DispatchStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_State", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sDM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_OrderNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sDM_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_AllocationNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sDM_AllocationNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spDispatch_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDispatchDetails(ByVal sNameSpace As String, ByVal objDispatch As ClsSalesDispatch) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_CommodityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_DescID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_Rate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_Quantity", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_RateAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_RateAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDispatch.DD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.DD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.DD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_GST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_GST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_GSTRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spDispatch_Details", 1, Arr, ObjParam)
            Return Arr
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
    Public Function BindGSTRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & iCompID & " "
            BindGSTRates = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindGSTRates
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDetailsofBranchState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCompBrnchID As Integer) As String
        Dim sSql As String = ""
        Dim iPOMBranchID As Integer
        Try
            'sSql = "Select SPO_BranchID from  Sales_Proforma_Order where SPO_ID=" & iPodID & " and SPO_CompID=" & iCompID & ""
            'iPOMBranchID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            'sSql = "Select CUSTB_STATE from MST_CUSTOMER_MASTER_Branch where CUSTB_Name='" & iPOMBranchID & "' and CUSTB_CompID=" & iCompID & ""
            'iCompBrnchID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select Mas_desc From ACC_General_Master Where Mas_Id = " & iCompBrnchID & " And Mas_master=4 and Mas_CompID = " & iCompID & ""
            CheckDetailsofBranchState = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDetailsofCompState(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = ""
        Dim iStateID As Integer
        Try
            sSql = "Select CUST_COMM_STATE from MST_Customer_Master where CUST_ID = " & iCompID & " and CUST_CompID =" & iCompID & " "
            iStateID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select Mas_desc From ACC_General_Master Where Mas_Id = " & iStateID & " And Mas_master=4 and Mas_CompID = " & iCompID & ""
            CheckDetailsofCompState = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer

        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("Commodity")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("OrderedQty")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")

            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Dispatch_Details Where DD_MasterID In(Select DM_ID From Dispatch_Master Where "
                sSql = sSql & "DM_ID =" & iDispatchID & " And DM_CompID =" & iCompID & " And DM_YearID =" & iYearID & ") And DD_CompID=" & iCompID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("DD_CommodityID")
                    dRow("ItemID") = dt.Rows(i)("DD_DescID")
                    dRow("HistoryID") = dt.Rows(i)("DD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("DD_UnitID")
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From  Inventory_Master Where INV_ID=" & dt.Rows(i)("DD_CommodityID") & " And INv_CompID = " & iCompID & " And INV_Parent=0 ")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("DD_DescID") & " And INV_Parent=" & dt.Rows(i)("DD_CommodityID") & " And INv_CompID = " & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("DD_UnitID") & " And Mas_CompID =" & iCompID & "")
                    dRow("MRP") = dt.Rows(i)("DD_Rate")
                    dRow("OrderedQty") = dt.Rows(i)("DD_Quantity")
                    dRow("Total") = dt.Rows(i)("DD_RateAmount")
                    dRow("GSTID") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_GST_ID")))
                    dRow("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_GSTRate")))
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Dispatch_Details Where DD_MasterID In(Select DM_ID From Dispatch_Master Where "
                sSql = sSql & "DM_ID =" & iDispatchID & " And DM_CompID =" & iCompID & " And DM_YearID =" & iYearID & ") And DD_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGstCategory As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select GC_GSTCategory from GSTcategory_table where GC_ID=" & iGstCategory & " and GC_CompID=" & iCompID & ""
            GetGSTDescription = objDBL.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckStateCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGSTNRegNo As String) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Dim bCheck As Boolean
        Dim sCode As String = ""
        Try
            sCode = sGSTNRegNo.Substring(0, 2)
            sSql = "Select * From GSTN_RegNo_Master Where GR_TIN='" & sCode & "' And GR_CompID=" & iCompID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql).ToString
            Return bCheck
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Try
            sSql = "Select BM_CODE From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_CompID=" & iCompID & " "
            'sSql = "Select ACM_CODE From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_CompID=" & iCompID & " "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            If sStr.StartsWith("C") Then
                GetPartyCode = "C"
            End If
            If sStr.StartsWith("P") Then
                GetPartyCode = "P"
            End If
            Return GetPartyCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchMasterData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim dt As New DataTable
        Try
            If iBatchNo > 0 And iBaseName > 0 Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Dispatch_Master Where DM_BatchNo=" & iBatchNo & " And DM_BaseName=" & iBaseName & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Dispatch_Master Where DM_BatchNo=" & iBatchNo & " And DM_BaseName=" & iBaseName & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
