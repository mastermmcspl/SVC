Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsInvoiceSales
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iSDM_ID As Integer
    Private iSDM_OrderID As Integer
    Private dSDM_OrderDate As DateTime
    Private iSDM_SupplierID As Integer
    Private dSDM_DispatchDate As DateTime
    Private iSDM_ModeOfShipping As Integer
    Private iSDM_ExpectedDays As Integer
    Private iSDM_PaymentType As Integer
    Private iSDM_CreatedBy As Integer
    Private dSDM_CreatedOn As DateTime
    Private sSDM_Status As String
    Private iSDM_YearID As Integer
    Private iSDM_CompID As Integer
    Private dSDM_ShippingRate As Double
    Private sSDM_ChequeNo As String
    Private dSDM_ChequeDate As DateTime
    Private sSDM_IFSCCode As String
    Private sSDM_BankName As String
    Private sSDM_Branch As String

    Private iSDD_ID As Integer
    Private iSDD_MasterID As Integer
    Private iSDD_CommodityID As Integer
    Private iSDD_DescID As Integer
    Private iSDD_UnitID As Integer
    Private iSDD_HistoryID As Integer
    Private sSDD_Rate As String
    Private iSDD_Quantity As Double
    Private sSDD_RateAmount As String
    Private iSDD_Discount As Double
    Private sSDD_DiscountAmount As String
    Private iSDD_VAT As Double
    Private sSDD_VATAmount As String
    Private iSDD_Excise As Double
    Private sSDD_ExciseAmount As String
    Private iSDD_CST As Double
    Private sSDD_CSTAmount As String
    Private sSDD_TotalAmount As String
    Private sSDD_Status As String
    Private iSDD_CompID As Integer

    Private sSDM_Operation As String
    Private sSDM_IPAddress As String
    Private sSDD_Operation As String
    Private sSDD_IPAddress As String
    Private iSDD_CreatedBy As Integer
    Private iSDD_CreatedOn As Date

    Private sSDD_ChargesPeritem As Double
    Private sSDD_Amount As Double
    Private iSDD_GST_ID As Integer
    Private sSDD_GSTRate As Double
    Private sSDD_GSTAmount As Double

    Private iSDD_SGST As Double
    Private sSDD_SGSTAmount As String
    Private iSDD_CGST As Double
    Private sSDD_CGSTAmount As String
    Private iSDD_IGST As Double
    Private sSDD_IGSTAmount As String

    Private iSDM_GrandDiscount As Double
    Private iSDM_GrandDiscountAmt As Double
    Private iSDM_GrandTotal As Double
    Private iSDM_GrandTotalAmt As Double
    Private sSDM_Code As String
    Private iSDM_SalesManID As Integer
    Private sSDM_DispatchRefNo As String
    Private sSDM_ESugamNo As String
    Private sSDM_Remarks As String
    Private iSDM_SaleType As Integer
    Private iSDM_OtherType As Integer
    Private iSDM_AllocateID As Integer

    Private iSDM_TrType As Integer
    Private sSDM_CompanyAddress As String
    Private sSDM_CompanyGSTNRegNo As String

    Private sSDM_BillingAddress As String
    Private sSDM_BillingGSTNRegNo As String

    Private sSDM_DeliveryAddress As String
    Private sSDM_DeliveryGSTNRegNo As String

    Private sSDM_DeliveryFrom As String
    Private sSDM_DeliveryFromGSTNRegNo As String
    Private sSDM_DispatchStatus As String
    Private iSDM_DispatchID As Integer
    Private iSDM_CompanyType As Integer
    Private iSDM_GSTNCategory As Integer

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

    Private Acc_JE_ID As Integer
    Private Acc_JE_TransactionNo As String
    Private Acc_JE_Party As Integer
    Private Acc_JE_Location As Integer
    Private Acc_JE_BillType As Integer
    Private Acc_JE_BillNo As String
    Private Acc_JE_BillDate As Date
    Private Acc_JE_BillAmount As Decimal
    Private Acc_JE_AdvanceAmount As Decimal
    Private Acc_JE_AdvanceNaration As String
    Private Acc_JE_NetAmount As Decimal
    Private Acc_JE_PaymentNarration As String
    Private Acc_JE_ChequeNo As String
    Private Acc_JE_ChequeDate As Date
    Private Acc_JE_IFSCCode As String
    Private Acc_JE_BankName As String
    Private Acc_JE_BranchName As String
    Private Acc_JE_CreatedBy As Integer
    Private Acc_JE_CreatedOn As Date
    Private Acc_JE_ApprovedBy As Integer
    Private Acc_JE_ApprovedOn As Date
    Private Acc_JE_DeletedBy As Integer
    Private Acc_JE_DeletedOn As Date
    Private Acc_JE_RecalledBy As Integer
    Private Acc_JE_RecalledOn As Date
    Private Acc_JE_YearID As Integer
    Private Acc_JE_CompID As Integer
    Private Acc_JE_Status As String
    Private Acc_JE_Operation As String
    Private Acc_JE_IPAddress As String
    Private Acc_JE_BillCreatedDate As Date
    Private Acc_JE_BalanceAmount As Decimal
    Private Acc_PJE_BillCreatedDate As Date
    Private Acc_JE_UpdatedBy As Integer
    Private Acc_JE_UpdatedOn As Date
    Private Acc_SJE_PaymentType As Integer
    Private Acc_JE_InvoiceID As Integer
    Private Acc_JE_Type As String

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

    Private sSDM_OrderNo As String
    Private sSDM_AllocationNo As String
    Private sSDM_DispatchNo As String
    Private iSDM_BatchNo As Integer
    Private iSDM_BaseName As Integer

    Public Property SDM_OrderNo() As String
        Get
            Return (sSDM_OrderNo)
        End Get
        Set(ByVal Value As String)
            sSDM_OrderNo = Value
        End Set
    End Property
    Public Property SDM_AllocationNo() As String
        Get
            Return (sSDM_AllocationNo)
        End Get
        Set(ByVal Value As String)
            sSDM_AllocationNo = Value
        End Set
    End Property
    Public Property SDM_DispatchNo() As String
        Get
            Return (sSDM_DispatchNo)
        End Get
        Set(ByVal Value As String)
            sSDM_DispatchNo = Value
        End Set
    End Property


    Public Property SDM_BatchNo() As Integer
        Get
            Return (iSDM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iSDM_BatchNo = Value
        End Set
    End Property
    Public Property SDM_BaseName() As Integer
        Get
            Return (iSDM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iSDM_BaseName = Value
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

    Public Property dAcc_JE_BalanceAmount() As Decimal
        Get
            Return (Acc_JE_BalanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_BalanceAmount = Value
        End Set
    End Property

    'Public Property dAcc_JE_BillCreatedDate() As Date
    '    Get
    '        Return (Acc_JE_BillCreatedDate)
    '    End Get
    '    Set(ByVal Value As Date)
    '        Acc_JE_BillCreatedDate = Value
    '    End Set
    'End Property

    Public Property sAcc_JE_IPAddress() As String
        Get
            Return (Acc_JE_IPAddress)
        End Get
        Set(ByVal Value As String)
            Acc_JE_IPAddress = Value
        End Set
    End Property

    Public Property sAcc_JE_Operation() As String
        Get
            Return (Acc_JE_Operation)
        End Get
        Set(ByVal Value As String)
            Acc_JE_Operation = Value
        End Set
    End Property
    Public Property sAcc_JE_Status() As String
        Get
            Return (Acc_JE_Status)
        End Get
        Set(ByVal Value As String)
            Acc_JE_Status = Value
        End Set
    End Property

    Public Property iAcc_JE_CompID() As Integer
        Get
            Return (Acc_JE_CompID)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_CompID = Value
        End Set
    End Property
    Public Property iAcc_JE_PaymentType() As Integer
        Get
            Return (Acc_SJE_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            Acc_SJE_PaymentType = Value
        End Set
    End Property

    Public Property iAcc_JE_YearID() As Integer
        Get
            Return (Acc_JE_YearID)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_YearID = Value
        End Set
    End Property
    Public Property dAcc_JE_RecalledOn() As Date
        Get
            Return (Acc_JE_RecalledOn)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_RecalledOn = Value
        End Set
    End Property

    Public Property iAcc_JE_RecalledBy() As Integer
        Get
            Return (Acc_JE_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_RecalledBy = Value
        End Set
    End Property
    Public Property dAcc_JE_DeletedOn() As Date
        Get
            Return (Acc_JE_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_DeletedOn = Value
        End Set
    End Property

    Public Property iAcc_JE_DeletedBy() As Integer
        Get
            Return (Acc_JE_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_DeletedBy = Value
        End Set
    End Property

    Public Property dAcc_JE_ApprovedOn() As Date
        Get
            Return (Acc_JE_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_ApprovedOn = Value
        End Set
    End Property
    Public Property iAcc_JE_ApprovedBy() As Integer
        Get
            Return (Acc_JE_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_ApprovedBy = Value
        End Set
    End Property

    Public Property dAcc_JE_CreatedOn() As Date
        Get
            Return (Acc_JE_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_CreatedOn = Value
        End Set
    End Property
    Public Property dAcc_JE_BillCreatedDate() As Date
        Get
            Return (Acc_JE_BillCreatedDate)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_BillCreatedDate = Value
        End Set
    End Property
    Public Property iAcc_JE_CreatedBy() As Integer
        Get
            Return (Acc_JE_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_CreatedBy = Value
        End Set
    End Property
    Public Property iAcc_JE_CreatedOn() As Date
        Get
            Return (Acc_JE_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_CreatedOn = Value
        End Set
    End Property
    Public Property iAcc_JE_UpdatedBy() As Integer
        Get
            Return (Acc_JE_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_UpdatedBy = Value
        End Set
    End Property
    Public Property iAcc_JE_InvoiceID() As Integer
        Get
            Return (Acc_JE_InvoiceID)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_InvoiceID = Value
        End Set
    End Property
    Public Property sAcc_JE_Type() As String
        Get
            Return (Acc_JE_Type)
        End Get
        Set(ByVal Value As String)
            Acc_JE_Type = Value
        End Set
    End Property
    Public Property iAcc_JE_UpdatedOn() As Date
        Get
            Return (Acc_JE_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_UpdatedOn = Value
        End Set
    End Property
    Public Property sAcc_JE_BranchName() As String
        Get
            Return (Acc_JE_BranchName)
        End Get
        Set(ByVal Value As String)
            Acc_JE_BranchName = Value
        End Set
    End Property
    Public Property sAcc_JE_BankName() As String
        Get
            Return (Acc_JE_BankName)
        End Get
        Set(ByVal Value As String)
            Acc_JE_BankName = Value
        End Set
    End Property

    Public Property sAcc_JE_IFSCCode() As String
        Get
            Return (Acc_JE_IFSCCode)
        End Get
        Set(ByVal Value As String)
            Acc_JE_IFSCCode = Value
        End Set
    End Property
    Public Property dAcc_JE_ChequeDate() As Date
        Get
            Return (Acc_JE_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_ChequeDate = Value
        End Set
    End Property

    Public Property sAcc_JE_ChequeNo() As String
        Get
            Return (Acc_JE_ChequeNo)
        End Get
        Set(ByVal Value As String)
            Acc_JE_ChequeNo = Value
        End Set
    End Property
    Public Property sAcc_JE_PaymentNarration() As String
        Get
            Return (Acc_JE_PaymentNarration)
        End Get
        Set(ByVal Value As String)
            Acc_JE_PaymentNarration = Value
        End Set
    End Property
    Public Property dAcc_JE_NetAmount() As Decimal
        Get
            Return (Acc_JE_NetAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_NetAmount = Value
        End Set
    End Property

    Public Property sAcc_JE_AdvanceNaration() As String
        Get
            Return (Acc_JE_AdvanceNaration)
        End Get
        Set(ByVal Value As String)
            Acc_JE_AdvanceNaration = Value
        End Set
    End Property
    Public Property dAcc_JE_AdvanceAmount() As Decimal
        Get
            Return (Acc_JE_AdvanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_AdvanceAmount = Value
        End Set
    End Property
    Public Property dAcc_JE_BillAmount() As Decimal
        Get
            Return (Acc_JE_BillAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_BillAmount = Value
        End Set
    End Property

    Public Property dAcc_JE_BillDate() As Date
        Get
            Return (Acc_JE_BillDate)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_BillDate = Value
        End Set
    End Property


    Public Property sAcc_JE_BillNo() As String
        Get
            Return (Acc_JE_BillNo)
        End Get
        Set(ByVal Value As String)
            Acc_JE_BillNo = Value
        End Set
    End Property

    Public Property iAcc_JE_BillType() As Integer
        Get
            Return (Acc_JE_BillType)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_BillType = Value
        End Set
    End Property
    Public Property iAcc_JE_Location() As Integer
        Get
            Return (Acc_JE_Location)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_Location = Value
        End Set
    End Property
    Public Property iAcc_JE_Party() As Integer
        Get
            Return (Acc_JE_Party)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_Party = Value
        End Set
    End Property
    Public Property sAcc_JE_TransactionNo() As String
        Get
            Return (Acc_JE_TransactionNo)
        End Get
        Set(ByVal Value As String)
            Acc_JE_TransactionNo = Value
        End Set
    End Property
    Public Property iAcc_JE_ID() As Integer
        Get
            Return (Acc_JE_ID)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_ID = Value
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

    Public Property SDM_AllocateID() As Integer
        Get
            Return (iSDM_AllocateID)
        End Get
        Set(ByVal Value As Integer)
            iSDM_AllocateID = Value
        End Set
    End Property
    Public Property SDM_SaleType() As Integer
        Get
            Return (iSDM_SaleType)
        End Get
        Set(ByVal Value As Integer)
            iSDM_SaleType = Value
        End Set
    End Property
    Public Property SDM_OtherType() As Integer
        Get
            Return (iSDM_OtherType)
        End Get
        Set(ByVal Value As Integer)
            iSDM_OtherType = Value
        End Set
    End Property
    Public Property SDM_Remarks() As String
        Get
            Return (sSDM_Remarks)
        End Get
        Set(ByVal Value As String)
            sSDM_Remarks = Value
        End Set
    End Property
    Public Property SDM_ESugamNo() As String
        Get
            Return (sSDM_ESugamNo)
        End Get
        Set(ByVal Value As String)
            sSDM_ESugamNo = Value
        End Set
    End Property
    Public Property SDM_DispatchRefNo() As String
        Get
            Return (sSDM_DispatchRefNo)
        End Get
        Set(ByVal Value As String)
            sSDM_DispatchRefNo = Value
        End Set
    End Property
    Public Property SDM_SalesManID() As Integer
        Get
            Return (iSDM_SalesManID)
        End Get
        Set(ByVal Value As Integer)
            iSDM_SalesManID = Value
        End Set
    End Property
    Public Property SDM_Code() As String
        Get
            Return (sSDM_Code)
        End Get
        Set(ByVal Value As String)
            sSDM_Code = Value
        End Set
    End Property
    Public Property SDM_GrandDiscount() As Double
        Get
            Return (iSDM_GrandDiscount)
        End Get
        Set(ByVal Value As Double)
            iSDM_GrandDiscount = Value
        End Set
    End Property
    Public Property SDM_GrandDiscountAmt() As Double
        Get
            Return (iSDM_GrandDiscountAmt)
        End Get
        Set(ByVal Value As Double)
            iSDM_GrandDiscountAmt = Value
        End Set
    End Property
    Public Property SDM_GrandTotal() As Double
        Get
            Return (iSDM_GrandTotal)
        End Get
        Set(ByVal Value As Double)
            iSDM_GrandTotal = Value
        End Set
    End Property
    Public Property SDM_GrandTotalAmt() As Double
        Get
            Return (iSDM_GrandTotalAmt)
        End Get
        Set(ByVal Value As Double)
            iSDM_GrandTotalAmt = Value
        End Set
    End Property
    Public Property SDM_ChequeNo() As String
        Get
            Return (sSDM_ChequeNo)
        End Get
        Set(ByVal Value As String)
            sSDM_ChequeNo = Value
        End Set
    End Property
    Public Property SDM_ChequeDate() As Date
        Get
            Return (dSDM_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            dSDM_ChequeDate = Value
        End Set
    End Property
    Public Property SDM_IFSCCode() As String
        Get
            Return (sSDM_IFSCCode)
        End Get
        Set(ByVal Value As String)
            sSDM_IFSCCode = Value
        End Set
    End Property
    Public Property SDM_BankName() As String
        Get
            Return (sSDM_BankName)
        End Get
        Set(ByVal Value As String)
            sSDM_BankName = Value
        End Set
    End Property
    Public Property SDM_Branch() As String
        Get
            Return (sSDM_Branch)
        End Get
        Set(ByVal Value As String)
            sSDM_Branch = Value
        End Set
    End Property
    Public Property SDD_CreatedBy() As Integer
        Get
            Return (iSDD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSDD_CreatedBy = Value
        End Set
    End Property
    Public Property SDD_CreatedOn() As Date
        Get
            Return (iSDD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            iSDD_CreatedOn = Value
        End Set
    End Property
    Public Property SDM_Operation() As String
        Get
            Return (sSDM_Operation)
        End Get
        Set(ByVal Value As String)
            sSDM_Operation = Value
        End Set
    End Property
    Public Property SDM_IPAddress() As String
        Get
            Return (sSDM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSDM_IPAddress = Value
        End Set
    End Property
    Public Property SDD_Operation() As String
        Get
            Return (sSDD_Operation)
        End Get
        Set(ByVal Value As String)
            sSDD_Operation = Value
        End Set
    End Property
    Public Property SDD_IPAddress() As String
        Get
            Return (sSDD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSDD_IPAddress = Value
        End Set
    End Property
    Public Property SDM_ID() As Integer
        Get
            Return (iSDM_ID)
        End Get
        Set(ByVal Value As Integer)
            iSDM_ID = Value
        End Set
    End Property
    Public Property SDM_OrderID() As Integer
        Get
            Return (iSDM_OrderID)
        End Get
        Set(ByVal Value As Integer)
            iSDM_OrderID = Value
        End Set
    End Property
    Public Property SDM_OrderDate() As DateTime
        Get
            Return (dSDM_OrderDate)
        End Get
        Set(ByVal Value As DateTime)
            dSDM_OrderDate = Value
        End Set
    End Property
    Public Property SDM_SupplierID() As Integer
        Get
            Return (iSDM_SupplierID)
        End Get
        Set(ByVal Value As Integer)
            iSDM_SupplierID = Value
        End Set
    End Property
    Public Property SDM_DispatchDate() As DateTime
        Get
            Return (dSDM_DispatchDate)
        End Get
        Set(ByVal Value As DateTime)
            dSDM_DispatchDate = Value
        End Set
    End Property
    Public Property SDM_ModeOfShipping() As Integer
        Get
            Return (iSDM_ModeOfShipping)
        End Get
        Set(ByVal Value As Integer)
            iSDM_ModeOfShipping = Value
        End Set
    End Property
    Public Property SDM_ExpectedDays() As Integer
        Get
            Return (iSDM_ExpectedDays)
        End Get
        Set(ByVal Value As Integer)
            iSDM_ExpectedDays = Value
        End Set
    End Property
    Public Property SDM_PaymentType() As Integer
        Get
            Return (iSDM_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            iSDM_PaymentType = Value
        End Set
    End Property
    Public Property SDM_CreatedBy() As Integer
        Get
            Return (iSDM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSDM_CreatedBy = Value
        End Set
    End Property
    Public Property SDM_CreatedOn() As DateTime
        Get
            Return (dSDM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSDM_CreatedOn = Value
        End Set
    End Property
    Public Property SDM_Status() As String
        Get
            Return (sSDM_Status)
        End Get
        Set(ByVal Value As String)
            sSDM_Status = Value
        End Set
    End Property
    Public Property SDM_YearID() As Integer
        Get
            Return (iSDM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSDM_YearID = Value
        End Set
    End Property
    Public Property SDM_CompID() As Integer
        Get
            Return (iSDM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSDM_CompID = Value
        End Set
    End Property
    Public Property SDM_ShippingRate() As Double
        Get
            Return (dSDM_ShippingRate)
        End Get
        Set(ByVal Value As Double)
            dSDM_ShippingRate = Value
        End Set
    End Property
    Public Property SDM_DispatchID() As Integer
        Get
            Return (iSDM_DispatchID)
        End Get
        Set(ByVal Value As Integer)
            iSDM_DispatchID = Value
        End Set
    End Property
    Public Property SDM_CompanyType() As Integer
        Get
            Return (iSDM_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iSDM_CompanyType = Value
        End Set
    End Property
    Public Property SDM_GSTNCategory() As Integer
        Get
            Return (iSDM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iSDM_GSTNCategory = Value
        End Set
    End Property
    Public Property SDD_ID() As Integer
        Get
            Return (iSDD_ID)
        End Get
        Set(ByVal Value As Integer)
            iSDD_ID = Value
        End Set
    End Property
    Public Property SDD_MasterID() As Integer
        Get
            Return (iSDD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iSDD_MasterID = Value
        End Set
    End Property
    Public Property SDD_CommodityID() As Integer
        Get
            Return (iSDD_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iSDD_CommodityID = Value
        End Set
    End Property
    Public Property SDD_DescID() As Integer
        Get
            Return (iSDD_DescID)
        End Get
        Set(ByVal Value As Integer)
            iSDD_DescID = Value
        End Set
    End Property
    Public Property SDD_UnitID() As Integer
        Get
            Return (iSDD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            iSDD_UnitID = Value
        End Set
    End Property
    Public Property SDD_HistoryID() As Integer
        Get
            Return (iSDD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iSDD_HistoryID = Value
        End Set
    End Property
    Public Property SDD_Rate() As Double
        Get
            Return (sSDD_Rate)
        End Get
        Set(ByVal Value As Double)
            sSDD_Rate = Value
        End Set
    End Property
    Public Property SDD_Quantity() As Double
        Get
            Return (iSDD_Quantity)
        End Get
        Set(ByVal Value As Double)
            iSDD_Quantity = Value
        End Set
    End Property
    Public Property SDD_RateAmount() As Double
        Get
            Return (sSDD_RateAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_RateAmount = Value
        End Set
    End Property
    Public Property SDD_Discount() As Double
        Get
            Return (iSDD_Discount)
        End Get
        Set(ByVal Value As Double)
            iSDD_Discount = Value
        End Set
    End Property
    Public Property SDD_DiscountAmount() As Double
        Get
            Return (sSDD_DiscountAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_DiscountAmount = Value
        End Set
    End Property
    Public Property SDD_VAT() As Double
        Get
            Return (iSDD_VAT)
        End Get
        Set(ByVal Value As Double)
            iSDD_VAT = Value
        End Set
    End Property
    Public Property SDD_VATAmount() As Double
        Get
            Return (sSDD_VATAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_VATAmount = Value
        End Set
    End Property
    Public Property SDD_Excise() As Double
        Get
            Return (iSDD_Excise)
        End Get
        Set(ByVal Value As Double)
            iSDD_Excise = Value
        End Set
    End Property
    Public Property SDD_ExciseAmount() As Double
        Get
            Return (sSDD_ExciseAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_ExciseAmount = Value
        End Set
    End Property
    Public Property SDD_CST() As Double
        Get
            Return (iSDD_CST)
        End Get
        Set(ByVal Value As Double)
            iSDD_CST = Value
        End Set
    End Property
    Public Property SDD_CSTAmount() As Double
        Get
            Return (sSDD_CSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_CSTAmount = Value
        End Set
    End Property
    Public Property SDD_TotalAmount() As Double
        Get
            Return (sSDD_TotalAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_TotalAmount = Value
        End Set
    End Property
    Public Property SDD_Status() As String
        Get
            Return (sSDD_Status)
        End Get
        Set(ByVal Value As String)
            sSDD_Status = Value
        End Set
    End Property
    Public Property SDD_CompID() As Integer
        Get
            Return (iSDD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSDD_CompID = Value
        End Set
    End Property


    Public Property SDM_TrType() As Integer
        Get
            Return (iSDM_TrType)
        End Get
        Set(ByVal Value As Integer)
            iSDM_TrType = Value
        End Set
    End Property
    Public Property SDM_CompanyAddress() As String
        Get
            Return (sSDM_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sSDM_CompanyAddress = Value
        End Set
    End Property
    Public Property SDM_CompanyGSTNRegNo() As String
        Get
            Return (sSDM_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sSDM_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property SDM_BillingAddress() As String
        Get
            Return (sSDM_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sSDM_BillingAddress = Value
        End Set
    End Property
    Public Property SDM_BillingGSTNRegNo() As String
        Get
            Return (sSDM_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sSDM_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property SDM_DeliveryFrom() As String
        Get
            Return (sSDM_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sSDM_DeliveryFrom = Value
        End Set
    End Property
    Public Property SDM_DeliveryFromGSTNRegNo() As String
        Get
            Return (sSDM_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sSDM_DeliveryFromGSTNRegNo = Value
        End Set
    End Property

    Public Property SDM_DeliveryAddress() As String
        Get
            Return (sSDM_DeliveryAddress)
        End Get
        Set(ByVal Value As String)
            sSDM_DeliveryAddress = Value
        End Set
    End Property
    Public Property SDM_DeliveryGSTNRegNo() As String
        Get
            Return (sSDM_DeliveryGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sSDM_DeliveryGSTNRegNo = Value
        End Set
    End Property
    Public Property SDM_DispatchStatus() As String
        Get
            Return (sSDM_DispatchStatus)
        End Get
        Set(ByVal Value As String)
            sSDM_DispatchStatus = Value
        End Set
    End Property

    Public Property SDD_GST_ID() As Integer
        Get
            Return (iSDD_GST_ID)
        End Get
        Set(ByVal Value As Integer)
            iSDD_GST_ID = Value
        End Set
    End Property
    Public Property SDD_GSTRate() As Double
        Get
            Return (sSDD_GSTRate)
        End Get
        Set(ByVal Value As Double)
            sSDD_GSTRate = Value
        End Set
    End Property
    Public Property SDD_GSTAmount() As Double
        Get
            Return (sSDD_GSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_GSTAmount = Value
        End Set
    End Property

    Public Property SDD_SGST() As Double
        Get
            Return (iSDD_SGST)
        End Get
        Set(ByVal Value As Double)
            iSDD_SGST = Value
        End Set
    End Property
    Public Property SDD_SGSTAmount() As Double
        Get
            Return (sSDD_SGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_SGSTAmount = Value
        End Set
    End Property
    Public Property SDD_CGST() As Double
        Get
            Return (iSDD_CGST)
        End Get
        Set(ByVal Value As Double)
            iSDD_CGST = Value
        End Set
    End Property
    Public Property SDD_CGSTAmount() As Double
        Get
            Return (sSDD_CGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_CGSTAmount = Value
        End Set
    End Property
    Public Property SDD_IGST() As Double
        Get
            Return (iSDD_IGST)
        End Get
        Set(ByVal Value As Double)
            iSDD_IGST = Value
        End Set
    End Property
    Public Property SDD_IGSTAmount() As Double
        Get
            Return (sSDD_IGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSDD_IGSTAmount = Value
        End Set
    End Property
    Public Property SDD_ChargesPeritem() As Double
        Get
            Return (sSDD_ChargesPeritem)
        End Get
        Set(ByVal Value As Double)
            sSDD_ChargesPeritem = Value
        End Set
    End Property
    Public Property SDD_Amount() As Double
        Get
            Return (sSDD_Amount)
        End Get
        Set(ByVal Value As Double)
            sSDD_Amount = Value
        End Set
    End Property
    Public Function SaveDispatchMaster(ByVal sNameSpace As String, ByVal objDispatch As ClsInvoiceSales) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(52) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_OrderDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.SDM_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_SupplierID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_SupplierID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DispatchDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.SDM_DispatchDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_ModeOfShipping", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_ModeOfShipping
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_ExpectedDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_ExpectedDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_PaymentType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.SDM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_Status ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDispatch.SDM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_ShippingRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_ShippingRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_ChequeNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.SDM_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.SDM_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_IFSCCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDispatch.SDM_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_BankName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDispatch.SDM_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_Branch", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDispatch.SDM_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.SDM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.SDM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_GrandDiscount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_GrandDiscount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_GrandDiscountAmt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_GrandDiscountAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_GrandTotal", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_GrandTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_GrandTotalAmt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_GrandTotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_Code", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDispatch.sSDM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_SalesManID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_SalesManID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DispatchRefNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDispatch.sSDM_DispatchRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_ESugamNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDispatch.sSDM_ESugamNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objDispatch.sSDM_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_SaleType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_SaleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_OtherType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_OtherType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_AllocateID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_AllocateID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_CompanyAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.sSDM_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.sSDM_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_BillingAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.sSDM_BillingAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_BillingGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.sSDM_BillingGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DeliveryFrom", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.sSDM_DeliveryFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DeliveryFromGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.sSDM_DeliveryFromGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DeliveryAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.sSDM_DeliveryAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DeliveryGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.sSDM_DeliveryGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DispatchStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.sSDM_DispatchStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DispatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_DispatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_OrderNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sSDM_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_AllocationNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sSDM_AllocationNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_DispatchNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sSDM_DispatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDM_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"



            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spDispatchMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDispatchDetails(ByVal sNameSpace As String, ByVal objDispatch As ClsInvoiceSales) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(36) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CommodityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_DescID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_Rate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_Quantity", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_RateAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_RateAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_Discount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_Discount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_DiscountAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_DiscountAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_VAT", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_VAT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_VATAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_VATAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_Excise", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_Excise
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_ExciseAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_ExciseAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_CST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_CSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_TotalAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_TotalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDispatch.SDD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.SDD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.SDD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.SDD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_ChargesPeritem", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_ChargesPeritem
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_Amount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_GST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_GST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_GSTRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_GSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_GSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_SGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_SGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_SGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_SGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_CGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_CGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_IGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_IGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_IGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_IGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spDispatchDetails", 1, Arr, ObjParam)
            Return Arr
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
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")

            'dtTab.Columns.Add("VATAmount")
            'dtTab.Columns.Add("CSTAmount")
            'dtTab.Columns.Add("ExiceAmount")

            dtTab.Columns.Add("NetAmount")

            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Sales_Dispatch_Details Where SDD_MasterID In(Select SDM_ID From Sales_Dispatch_Master Where "
                sSql = sSql & "SDM_ID =" & iDispatchID & " And SDM_CompID =" & iCompID & " And SDM_YearID =" & iYearID & ") And SDD_CompID=" & iCompID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("SDD_CommodityID")
                    dRow("ItemID") = dt.Rows(i)("SDD_DescID")
                    dRow("HistoryID") = dt.Rows(i)("SDD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("SDD_UnitID")
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SDD_CommodityID") & " And INv_CompID = " & iCompID & " And INV_Parent=0 ")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("SDD_DescID") & " And INV_Parent=" & dt.Rows(i)("SDD_CommodityID") & " And INv_CompID = " & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SDD_UnitID") & " And Mas_CompID =" & iCompID & "")
                    dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_Rate")))
                    dRow("OrderedQty") = dt.Rows(i)("SDD_Quantity")
                    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_RateAmount")))


                    dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_DiscountAmount")))
                    dRow("Charges") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_ChargesPerItem")))
                    dRow("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_Amount")))

                    'dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dt.Rows(i)("SDD_VATAmount"))))
                    'dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_CSTAmount")))
                    'dRow("ExiceAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_ExciseAmount")))

                    dRow("GSTID") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_GST_ID")))
                    dRow("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_GSTRate")))
                    dRow("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_GSTAmount")))

                    dRow("NetAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SDD_TotalAmount")))

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
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
    Public Function BindDispatchMasterData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim dt As New DataTable
        Try
            If iBatchNo > 0 And iBaseName > 0 Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Dispatch_Master Where SDM_BatchNo=" & iBatchNo & " And SDM_Basename=" & iBaseName & " And SDM_CompID=" & iCompID & " And SDM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Sales_Dispatch_Master Where SDM_BatchNo=" & iBatchNo & " And SDM_Basename=" & iBaseName & " And SDM_CompID=" & iCompID & " And SDM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            End If
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
    Public Function BindInvoiceData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Sales_Dispatch_Details Where SDD_MasterID In(Select SDM_ID From Sales_Dispatch_Master Where "
                sSql = sSql & "SDM_ID =" & iDispatchID & " And SDM_CompID =" & iCompID & " And SDM_YearID =" & iYearID & ") And SDD_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
