Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsDispatchDetails
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
    Private dSDD_FETotalAmt As Double
    Private iSDD_Currency As Integer
    Private dSDD_CurrencyAmt As Double
    Private sSDD_CurrencyTime As String

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

    Private iSDM_BatchNo As Integer
    Private iSDM_BaseName As Integer
    Private sSDM_OrderNo As String
    Private sSDM_AllocationNo As String
    Private sSDM_DispatchNo As String

    Private ATD_OpenDebit As Decimal
    Private ATD_OpenCredit As Decimal
    Private ATD_ClosingDebit As Decimal
    Private ATD_ClosingCredit As Decimal
    Private ATD_SeqReferenceNum As Integer


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
    Public Property SDD_FETotalAmt() As Double
        Get
            Return (dSDD_FETotalAmt)
        End Get
        Set(ByVal Value As Double)
            dSDD_FETotalAmt = Value
        End Set
    End Property
    Public Property SDD_Currency() As Integer
        Get
            Return (iSDD_Currency)
        End Get
        Set(ByVal Value As Integer)
            iSDD_Currency = Value
        End Set
    End Property
    Public Property SDD_CurrencyAmt() As Double
        Get
            Return (dSDD_CurrencyAmt)
        End Get
        Set(ByVal Value As Double)
            dSDD_CurrencyAmt = Value
        End Set
    End Property
    Public Property SDD_CurrencyTime() As String
        Get
            Return (sSDD_CurrencyTime)
        End Get
        Set(ByVal Value As String)
            sSDD_CurrencyTime = Value
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
    Public Function BindParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
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
    Public Function LoadOrderNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select Distinct(SPO_ID),SPO_OrderCode from sales_Proforma_order,Sales_Allocate_Master where SPO_DispatchFlag <> 1 And SPO_OrderType='O' Or (SPO_ID=SAM_OrderNo And SPO_OrderType='S' And SAM_DispatchFlag <> 1 And SAM_Status='A' and SAM_YearID =" & iYearID & " And SAM_CompID=" & iCompID & ") and SPO_YearID =" & iYearID & " And SPO_CompID=" & iCompID & " Order By SPO_OrderCode Desc"
            'sSql = "Select Distinct(a.SPO_ID) As SPO_ID,a.SPO_OrderCode from sales_Proforma_order a
            '        Left Join Sales_Allocate_Master b On b.SAM_OrderNo=a.SPO_ID
            '        where (a.SPO_DispatchFlag <> 1 And a.SPO_OrderType='O') Or (a.SPO_OrderType='S' And b.SAM_DispatchFlag <> 1 And b.SAM_Status='A' and b.SAM_YearID =" & iYearID & " And b.SAM_CompID=" & iCompID & ") and a.SPO_YearID=" & iYearID & " And a.SPO_CompID=" & iCompID & " Order By a.SPO_OrderCode Desc"

            '       sSql = "Select Distinct(a.SPO_ID) As SPO_ID,a.SPO_OrderCode from sales_Proforma_order a
            '               Left Join Sales_Allocate_Master b On b.SAM_OrderNo=a.SPO_ID
            'Left Join Sales_Dispatch_Master c On c.SDM_OrderID=a.SPO_ID
            '               where (a.SPO_ID=c.SDM_OrderID And c.SDM_Status<>'A') Or (a.SPO_OrderType='S' And b.SAM_DispatchFlag <> 1 And b.SAM_Status='W' and b.SAM_YearID =" & iYearID & " And b.SAM_CompID=" & iCompID & ") and a.SPO_YearID=" & iYearID & " And a.SPO_CompID=" & iCompID & " Order By a.SPO_OrderCode Desc"

            'Working'
            '       sSql = "Select Distinct(a.SPO_ID) As SPO_ID,a.SPO_OrderCode from sales_Proforma_order a
            '               Left Join Sales_Allocate_Master b On b.SAM_OrderNo=a.SPO_ID
            'Left Join Sales_Dispatch_Master c On c.SDM_OrderID=a.SPO_ID
            '               where a.SPO_OrderType<>'O' And ((a.SPO_ID=c.SDM_OrderID And c.SDM_Status<>'A') Or (a.SPO_OrderType='S' And b.SAM_DispatchFlag <> 1 And b.SAM_Status='W' and b.SAM_YearID =" & iYearID & " And b.SAM_CompID=" & iCompID & " and a.SPO_YearID=" & iYearID & " And a.SPO_CompID=" & iCompID & ")) Order By a.SPO_OrderCode Desc"
            'Working'
            sSql = "Select Distinct(a.SPO_ID) As SPO_ID,a.SPO_OrderCode from sales_Proforma_order a
                    Left Join Sales_Allocate_Master b On b.SAM_OrderNo=a.SPO_ID
					Left Join Sales_Dispatch_Master c On c.SDM_OrderID=a.SPO_ID
                    where a.SPO_BranchID=" & iBranch & " And a.SPO_OrderType='O' Or ((a.SPO_ID=c.SDM_OrderID And c.SDM_Status<>'A') Or (a.SPO_OrderType='S' And b.SAM_DispatchFlag <> 1 And b.SAM_Status='W' and b.SAM_YearID =" & iYearID & " And b.SAM_CompID=" & iCompID & " and a.SPO_YearID=" & iYearID & " And a.SPO_CompID=" & iCompID & ")) Order By a.SPO_OrderCode Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAllocatedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iAllocateID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer

        Dim dNetAmt As Double = 0, dTAXAmt As Double = 0
        Dim dExciseAmt As Double = 0, dCSTAmt As Double = 0
        Dim dTAX As Double = 0
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
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")

            'dtTab.Columns.Add("VATAmount")
            'dtTab.Columns.Add("CSTAmount")
            'dtTab.Columns.Add("ExiceAmount")

            dtTab.Columns.Add("NetAmount")

            If iAllocateID > 0 Then
                sSql = "" : sSql = "Select * From Sales_Allocate_Details Where SAD_PlacedQnt > 0 and SAD_MasterID In(Select SAM_ID From Sales_Allocate_Master Where "
                sSql = sSql & "SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iMasterID & " And SAM_YearID =" & iYearID & " And SAM_CompID =" & iCompID & ") And SAD_CompID=" & iCompID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("SAD_Commodity")
                    dRow("ItemID") = dt.Rows(i)("SAD_DescID")
                    dRow("HistoryID") = dt.Rows(i)("SAD_HisotryID")
                    dRow("UnitID") = dt.Rows(i)("SAD_UnitID")
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SAD_Commodity") & " and Inv_CompID = " & iCompID & " And INV_Parent=0 ")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("SAD_DescID") & " And INV_Parent=" & dt.Rows(i)("SAD_Commodity") & " and Inv_CompID = " & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_UnitID") & " and Mas_CompID =" & iCompID & "")
                    dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_MRP")))
                    dRow("OrderedQty") = dt.Rows(i)("SAD_PlacedQnt")
                    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_PlacedQntAmount")))

                    dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_PlacedDiscountAmount")))

                    'If dt.Rows(i)("SAD_VAT") > 0 Then
                    '    'dRow("Tax") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_VAT") & " And MAS_Master=14 And Mas_DelFlag='A' And Mas_CompID=" & iCompID & " ")
                    '    dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_VATAmount")))
                    'Else
                    '    'dRow("Tax") = 0
                    '    dRow("VATAmount") = 0
                    'End If

                    'If dt.Rows(i)("SAD_CST") > 0 Then
                    '    'dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_CST") & " And MAS_Master=15 And Mas_DelFlag='A' And Mas_CompID=" & iCompID & " ")
                    '    dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_CSTAmount")))
                    'Else
                    '    'dRow("CST") = 0
                    '    dRow("CSTAmount") = 0
                    'End If

                    'If dt.Rows(i)("SAD_Exice") > 0 Then
                    '    'dRow("ExciseDuty") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_Exice") & " And MAS_Master=16 And Mas_DelFlag='A' And Mas_CompID=" & iCompID & " ")
                    '    dRow("ExiceAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_ExiceAmount")))
                    'Else
                    '    'dRow("ExciseDuty") = 0
                    '    dRow("ExiceAmount") = 0
                    'End If
                    dRow("GSTID") = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 GST_ID From GST_Rates Where GST_ItemID=" & dt.Rows(i)("SAD_DescID") & " And GST_CommodityID=" & dt.Rows(i)("SAD_Commodity") & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc ")
                    dRow("GSTRate") = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 GST_GSTRate From GST_Rates Where GST_ItemID=" & dt.Rows(i)("SAD_DescID") & " And GST_CommodityID=" & dt.Rows(i)("SAD_Commodity") & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc ")
                    dRow("GSTAmount") = ""

                    dRow("NetAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_PlacedTotalAmount")))

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDispatchMaster(ByVal sNameSpace As String, ByVal objDispatch As ClsDispatchDetails) As Array
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
            ObjParam(iParamCount).Value = objDispatch.iSDM_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDM_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iSDM_BaseName
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
    Public Function SaveDispatchDetails(ByVal sNameSpace As String, ByVal objDispatch As ClsDispatchDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(40) {}
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

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_FETotalAmt", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objDispatch.SDD_FETotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_Currency", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.SDD_Currency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CurrencyAmt", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objDispatch.SDD_CurrencyAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SDD_CurrencyTime", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.SDD_CurrencyTime
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
    Public Function GetShippingCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal sOrderType As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_ShippingCharge From sales_Proforma_order Where SPO_ID=" & iOrderNo & " And SPO_OrderType='" & sOrderType & "' and SPO_CompID =" & iCompID & " and SPO_YearID =" & iYearID & ""
            Return objDBL.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchMasterData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim dt As New DataTable
        Try
            If iDispatchID > 0 Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_CompID=" & iCompID & " And SDM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_CompID=" & iCompID & " And SDM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            Else
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderNo & " And SDM_CompID=" & iCompID & " And SDM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderNo & " And SDM_CompID=" & iCompID & " And SDM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From sales_Proforma_order Where SPO_ID=" & iOrderNo & " And SPO_CompID=" & iCompID & " And SPO_YearID = " & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As DataTable
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
            Else
                If iAllocatedID > 0 Then
                    sSql = "" : sSql = "Select * From Sales_Dispatch_Details Where SDD_MasterID In(Select SDM_ID From Sales_Dispatch_Master Where "
                    sSql = sSql & "SDM_OrderID =" & iMasterID & " And SDM_AllocateID=" & iAllocatedID & " And SDM_CompID =" & iCompID & " And SDM_YearID =" & iYearID & ") And SDD_CompID=" & iCompID & " "
                Else
                    sSql = "" : sSql = "Select * From Sales_Dispatch_Details Where SDD_MasterID In(Select SDM_ID From Sales_Dispatch_Master Where "
                    sSql = sSql & "SDM_OrderID =" & iMasterID & " And SDM_CompID =" & iCompID & " And SDM_YearID =" & iYearID & ") And SDD_CompID=" & iCompID & " "
                End If
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
    Public Function LoadMethodOfShiping(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=13 And Mas_DelFlag='A' "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChargeType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=24 And Mas_DelFlag='A' "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPaymentType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Payment Type') And Mas_CompID=" & iCompID & " and Mas_DelFlag='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindOralSalesData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer

        Dim dNetAmt As Double = 0, dTAXAmt As Double = 0
        Dim dExciseAmt As Double = 0, dCSTAmt As Double = 0
        Dim dTAX As Double = 0
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

            sSql = "" : sSql = "Select * From Sales_ProForma_Order_Details Where SPOD_Status<>'C' And SPOD_SOID =" & iMasterID & " And SPOD_YearID =" & iYearID & " And SPOD_CompID =" & iCompID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("SPOD_CommodityID")
                    dRow("ItemID") = dt.Rows(i)("SPOD_ItemID")
                    dRow("HistoryID") = dt.Rows(i)("SPOD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("SPOD_UnitOfMeasurement")
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SPOD_CommodityID") & " and Inv_CompID = " & iCompID & " And INV_Parent=0 ")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("SPOD_ItemID") & " And INV_Parent=" & dt.Rows(i)("SPOD_CommodityID") & " and Inv_CompID = " & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SPOD_UnitOfMeasurement") & " and Mas_CompID =" & iCompID & "")
                    dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_MRPRate")))
                    dRow("OrderedQty") = dt.Rows(i)("SPOD_Quantity")
                    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))

                    'dRow("Discount") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SPOD_Discount") & " And MAS_Master=19 And Mas_DelFlag='A' And Mas_CompID=" & iCompID & " ")
                    If IsDBNull(dt.Rows(i)("SPOD_Discount")) = False Then
                        If dt.Rows(i)("SPOD_Discount") > 0 Then
                            'dRow("Discount") = dt.Rows(i)("SPOD_Discount")
                            dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_DiscountRate")))
                            'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SPOD_Discount") & " And MAS_Master=19 And Mas_DelFlag='A' And Mas_CompID=" & iCompID & " ")
                        Else
                            'dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(0))
                            dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(0))
                        End If
                    Else
                        'dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(0))
                        dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(0))
                    End If

                    dRow("Charges") = ""
                    dRow("Amount") = ""

                    dRow("GSTID") = dt.Rows(i)("SPOD_GST_ID")
                    dRow("GSTRate") = dt.Rows(i)("SPOD_GSTRate")
                    dRow("GSTAmount") = dt.Rows(i)("SPOD_GSTAmount")

                    dRow("NetAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateStockLedgerClosingBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iQuantity As Integer, ByVal iIPAddress As String, ByVal SaleOrderID As Integer, ByVal iDispatchID As Integer, ByVal iFIFO As Integer, ByVal iBranch As Integer)
        Dim sSql As String = ""
        Dim iSalesQty As Integer
        Dim iSQty As Integer
        Dim iOpeningBalanceQty, iPurchaseQty, iClosingBalanceQty As Integer
        Dim iOrderID As Integer
        Dim dt As New DataTable
        Dim iDueItemQty As Integer
        Dim iClosedQty As Integer
        Dim iRequiredQty As Integer
        Dim iOrderQty As Integer
        Dim iMaxID As Integer

        Dim dCloseBal As Double
        Try

            If iFIFO = 1 Then   'First in first out  1

                'sSql = "" : sSql = "Select * From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_HistoryID=" & iHistoryID & " And SL_CompID=" & iCompID & " Order By SL_OrderID Asc "
                sSql = "" : sSql = "Select * From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " Order By SL_OrderID Asc "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    If iQuantity <= dt.Rows(0)("SL_ClosingBalanceQty") Then

                        'If IsDBNull(dt.Rows(0)("SL_SaleQnty")) = False Then
                        '    iSalesQty = dt.Rows(0)("SL_SaleQnty")
                        'Else
                        '    iSalesQty = 0
                        'End If
                        iSalesQty = 0
                        iSQty = iSalesQty + iQuantity

                        If IsDBNull(dt.Rows(0)("SL_OpeningBalanceQty")) = False Then
                            iOpeningBalanceQty = dt.Rows(0)("SL_OpeningBalanceQty")
                        End If

                        'iClosingBalanceQty = iOpeningBalanceQty - iSQty
                        iClosingBalanceQty = dt.Rows(0)("SL_ClosingBalanceQty") - iSQty

                        sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(0)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                        iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                        sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                        sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(0)("SL_ID") & "," & dt.Rows(0)("SL_Commodity") & "," & dt.Rows(0)("SL_ItemID") & "," & dt.Rows(0)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iClosingBalanceQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                    Else
                        iOrderQty = iQuantity


                        If dt.Rows.Count > 0 Then 'For Multiple Row
                            For i = 0 To dt.Rows.Count - 1

                                If iOrderQty <> iRequiredQty Then

                                    If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False And dt.Rows(i)("SL_ClosingBalanceQty") > 0 Then

                                        If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                            iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty")
                                        Else
                                            iClosingBalanceQty = 0
                                        End If

                                        If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                            iClosedQty = dt.Rows(i)("SL_ClosingBalanceQty")
                                        Else
                                            iClosedQty = 0
                                        End If

                                        'If IsDBNull(dt.Rows(i)("SL_SaleQnty")) = False Then
                                        '    iSalesQty = dt.Rows(i)("SL_SaleQnty")
                                        'Else
                                        '    iSalesQty = 0
                                        'End If
                                        iSalesQty = 0

                                        If iDueItemQty > 0 Then
                                            If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                                If iQuantity > dt.Rows(i)("SL_ClosingBalanceQty") Then
                                                    iDueItemQty = (iQuantity - dt.Rows(i)("SL_ClosingBalanceQty"))
                                                    iSQty = iSalesQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iRequiredQty = iRequiredQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                Else
                                                    iDueItemQty = (dt.Rows(i)("SL_ClosingBalanceQty") - iQuantity)
                                                    iSQty = iSalesQty + iQuantity
                                                    iRequiredQty = iRequiredQty + iQuantity
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                End If
                                            End If

                                        Else
                                            If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                                If iQuantity > dt.Rows(i)("SL_ClosingBalanceQty") Then
                                                    iDueItemQty = (iQuantity - dt.Rows(i)("SL_ClosingBalanceQty"))
                                                    iSQty = iSalesQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iRequiredQty = iRequiredQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                Else
                                                    iDueItemQty = (dt.Rows(i)("SL_ClosingBalanceQty") - iQuantity)
                                                    iSQty = iSalesQty + iQuantity
                                                    iRequiredQty = iRequiredQty + iQuantity
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                End If
                                            End If

                                        End If

                                        If IsDBNull(dt.Rows(i)("SL_OpeningBalanceQty")) = False Then
                                            iOpeningBalanceQty = dt.Rows(i)("SL_OpeningBalanceQty")
                                        Else
                                            iOpeningBalanceQty = 0
                                        End If

                                        'iClosingBalanceQty = iOpeningBalanceQty - iSQty
                                        iQuantity = iDueItemQty

                                        sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(i)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                        If i = dt.Rows.Count - 1 And iClosingBalanceQty = 0 Then
                                            iClosingBalanceQty = ((dt.Rows(i)("SL_ClosingBalanceQty") - iSQty) - iDueItemQty)
                                            iQuantity = iDueItemQty

                                            sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(i)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                            iSQty = iSQty + iDueItemQty

                                            iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                                            sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                                            sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(i)("SL_ID") & "," & dt.Rows(i)("SL_Commodity") & "," & dt.Rows(i)("SL_ItemID") & "," & dt.Rows(i)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iDueItemQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                                        Else
                                            iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                                            sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                                            sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(i)("SL_ID") & "," & dt.Rows(i)("SL_Commodity") & "," & dt.Rows(i)("SL_ItemID") & "," & dt.Rows(i)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iDueItemQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                                        End If

                                    ElseIf IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then 'Zero And Negative

                                        If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                            iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty")
                                        Else
                                            iClosingBalanceQty = 0
                                        End If

                                        If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                            iClosedQty = dt.Rows(i)("SL_ClosingBalanceQty")
                                        Else
                                            iClosedQty = 0
                                        End If

                                        'If IsDBNull(dt.Rows(i)("SL_SaleQnty")) = False Then
                                        '    iSalesQty = dt.Rows(i)("SL_SaleQnty")
                                        'Else
                                        '    iSalesQty = 0
                                        'End If
                                        iSalesQty = 0

                                        If iDueItemQty > 0 Then
                                            If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                                If iQuantity > dt.Rows(i)("SL_ClosingBalanceQty") Then
                                                    iDueItemQty = (iQuantity - dt.Rows(i)("SL_ClosingBalanceQty"))
                                                    iSQty = iSalesQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iRequiredQty = iRequiredQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                Else
                                                    iDueItemQty = (dt.Rows(i)("SL_ClosingBalanceQty") - iQuantity)
                                                    iSQty = iSalesQty + iQuantity
                                                    iRequiredQty = iRequiredQty + iQuantity
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                End If
                                            End If

                                        Else
                                            If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                                If iQuantity > dt.Rows(i)("SL_ClosingBalanceQty") Then
                                                    iDueItemQty = (iQuantity - dt.Rows(i)("SL_ClosingBalanceQty"))
                                                    iSQty = iSalesQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iRequiredQty = iRequiredQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                Else
                                                    iDueItemQty = (dt.Rows(i)("SL_ClosingBalanceQty") - iQuantity)
                                                    iSQty = iSalesQty + iQuantity
                                                    iRequiredQty = iRequiredQty + iQuantity
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                End If
                                            End If

                                        End If

                                        If IsDBNull(dt.Rows(i)("SL_OpeningBalanceQty")) = False Then
                                            iOpeningBalanceQty = dt.Rows(i)("SL_OpeningBalanceQty")
                                        Else
                                            iOpeningBalanceQty = 0
                                        End If

                                        'iClosingBalanceQty = iOpeningBalanceQty - iSQty
                                        iQuantity = iDueItemQty

                                        sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(i)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                        If iClosingBalanceQty = 0 Then
                                            iClosingBalanceQty = ((dt.Rows(i)("SL_ClosingBalanceQty") - iSQty) - iDueItemQty)
                                            iQuantity = iDueItemQty

                                            sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(i)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                            iSQty = iSQty + iDueItemQty

                                            iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                                            sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                                            sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(i)("SL_ID") & "," & dt.Rows(i)("SL_Commodity") & "," & dt.Rows(i)("SL_ItemID") & "," & dt.Rows(i)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iDueItemQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                        End If
                                        Exit Function

                                    End If

                                End If

                            Next

                        Else 'For Single Row

                            'If IsDBNull(dt.Rows(0)("SL_SaleQnty")) = False Then
                            '    iSalesQty = dt.Rows(0)("SL_SaleQnty")
                            'Else
                            '    iSalesQty = 0
                            'End If
                            iSalesQty = 0
                            iSQty = iSalesQty + dt.Rows(0)("SL_ClosingBalanceQty")

                            If IsDBNull(dt.Rows(0)("SL_OpeningBalanceQty")) = False Then
                                iOpeningBalanceQty = dt.Rows(0)("SL_OpeningBalanceQty")
                            End If

                            iDueItemQty = (iQuantity - dt.Rows(0)("SL_ClosingBalanceQty"))

                            iClosingBalanceQty = ((iQuantity - iSQty) - iDueItemQty)

                            sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(0)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                            iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                            sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                            sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(0)("SL_ID") & "," & dt.Rows(0)("SL_Commodity") & "," & dt.Rows(0)("SL_ItemID") & "," & dt.Rows(0)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iDueItemQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                        End If

                    End If
                End If

            ElseIf iFIFO = 2 Then   'Last in first out  2

                sSql = "" : sSql = "Select * From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_HistoryID=" & iHistoryID & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " Order By SL_OrderID Desc "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    If iQuantity <= dt.Rows(0)("SL_ClosingBalanceQty") Then

                        'If IsDBNull(dt.Rows(0)("SL_SaleQnty")) = False Then
                        '    iSalesQty = dt.Rows(0)("SL_SaleQnty")
                        'Else
                        '    iSalesQty = 0
                        'End If
                        iSalesQty = 0
                        iSQty = iSalesQty + iQuantity

                        If IsDBNull(dt.Rows(0)("SL_OpeningBalanceQty")) = False Then
                            iOpeningBalanceQty = dt.Rows(0)("SL_OpeningBalanceQty")
                        End If

                        'iClosingBalanceQty = iOpeningBalanceQty - iSQty
                        iClosingBalanceQty = dt.Rows(0)("SL_ClosingBalanceQty") - iSQty

                        sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(0)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                        iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                        sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                        sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(0)("SL_ID") & "," & dt.Rows(0)("SL_Commodity") & "," & dt.Rows(0)("SL_ItemID") & "," & dt.Rows(0)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iClosingBalanceQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                    Else
                        iOrderQty = iQuantity

                        If dt.Rows.Count > 0 Then 'For Multiple Row
                            For i = 0 To dt.Rows.Count - 1

                                If iOrderQty <> iRequiredQty Then

                                    If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False And dt.Rows(i)("SL_ClosingBalanceQty") <> 0 Then

                                        If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                            iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty")
                                        Else
                                            iClosingBalanceQty = 0
                                        End If

                                        If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                            iClosedQty = dt.Rows(i)("SL_ClosingBalanceQty")
                                        Else
                                            iClosedQty = 0
                                        End If

                                        'If IsDBNull(dt.Rows(i)("SL_SaleQnty")) = False Then
                                        '    iSalesQty = dt.Rows(i)("SL_SaleQnty")
                                        'Else
                                        '    iSalesQty = 0
                                        'End If
                                        iSalesQty = 0

                                        If iDueItemQty > 0 Then
                                            If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                                If iQuantity > dt.Rows(i)("SL_ClosingBalanceQty") Then
                                                    iDueItemQty = (iQuantity - dt.Rows(i)("SL_ClosingBalanceQty"))
                                                    iSQty = iSalesQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iRequiredQty = iRequiredQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                Else
                                                    iDueItemQty = (dt.Rows(i)("SL_ClosingBalanceQty") - iQuantity)
                                                    iSQty = iSalesQty + iQuantity
                                                    iRequiredQty = iRequiredQty + iQuantity
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                End If
                                            End If

                                        Else
                                            If IsDBNull(dt.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                                If iQuantity > dt.Rows(i)("SL_ClosingBalanceQty") Then
                                                    iDueItemQty = (iQuantity - dt.Rows(i)("SL_ClosingBalanceQty"))
                                                    iSQty = iSalesQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iRequiredQty = iRequiredQty + dt.Rows(i)("SL_ClosingBalanceQty")
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                Else
                                                    iDueItemQty = (dt.Rows(i)("SL_ClosingBalanceQty") - iQuantity)
                                                    iSQty = iSalesQty + iQuantity
                                                    iRequiredQty = iRequiredQty + iQuantity
                                                    iClosingBalanceQty = dt.Rows(i)("SL_ClosingBalanceQty") - iSQty
                                                End If
                                            End If

                                        End If

                                        If IsDBNull(dt.Rows(i)("SL_OpeningBalanceQty")) = False Then
                                            iOpeningBalanceQty = dt.Rows(i)("SL_OpeningBalanceQty")
                                        Else
                                            iOpeningBalanceQty = 0
                                        End If

                                        'iClosingBalanceQty = iOpeningBalanceQty - iSQty
                                        iQuantity = iDueItemQty

                                        sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(i)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                        If i = dt.Rows.Count - 1 And iClosingBalanceQty = 0 Then
                                            iClosingBalanceQty = ((dt.Rows(i)("SL_ClosingBalanceQty") - iSQty) - iDueItemQty)
                                            iQuantity = iDueItemQty

                                            sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(i)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                                            iSQty = iSQty + iDueItemQty

                                            iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                                            sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                                            sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(i)("SL_ID") & "," & dt.Rows(i)("SL_Commodity") & "," & dt.Rows(i)("SL_ItemID") & "," & dt.Rows(i)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iDueItemQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                                        Else
                                            iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                                            sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                                            sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(i)("SL_ID") & "," & dt.Rows(i)("SL_Commodity") & "," & dt.Rows(i)("SL_ItemID") & "," & dt.Rows(i)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iDueItemQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                                        End If

                                    End If

                                End If

                            Next

                        Else 'For Single Row

                            'If IsDBNull(dt.Rows(0)("SL_SaleQnty")) = False Then
                            '    iSalesQty = dt.Rows(0)("SL_SaleQnty")
                            'Else
                            '    iSalesQty = 0
                            'End If
                            iSalesQty = 0
                            iSQty = iSalesQty + dt.Rows(0)("SL_ClosingBalanceQty")

                            If IsDBNull(dt.Rows(0)("SL_OpeningBalanceQty")) = False Then
                                iOpeningBalanceQty = dt.Rows(0)("SL_OpeningBalanceQty")
                            End If

                            iDueItemQty = (iQuantity - dt.Rows(0)("SL_ClosingBalanceQty"))

                            iClosingBalanceQty = ((iQuantity - iSQty) - iDueItemQty)

                            sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iClosingBalanceQty & ",SL_Operation='U',SL_IPAddress='" & iIPAddress & "' Where SL_ID=" & dt.Rows(0)("SL_ID") & " And SL_CompID=" & iCompID & " And SL_Branch=" & iBranch & " "
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                            iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Stock_Ledger_SalesDetails", "SLS_ID", "SLS_CompID")
                            sSql = "" : sSql = "Insert Into Stock_Ledger_SalesDetails(SLS_ID,SLS_MasterID,SLS_Commodity,SLS_ItemID,SLS_HistoryID,SLS_SaleDate,SLS_SaleOrderID,SLS_SaleQnt,SLS_ClosingQnt,SLS_CompID,SLS_YearID,SLS_CreatedBy,SLS_CreatedOn,SLS_Operation,SLS_IPAddress,SLS_Status,SLS_DispatchID,SLS_Branch)"
                            sSql = sSql & "Values(" & iMaxID & "," & dt.Rows(0)("SL_ID") & "," & dt.Rows(0)("SL_Commodity") & "," & dt.Rows(0)("SL_ItemID") & "," & dt.Rows(0)("SL_HistoryID") & ",GetDate()," & SaleOrderID & "," & iSQty & "," & iDueItemQty & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),'C','" & iIPAddress & "','W'," & iDispatchID & "," & iBranch & " )"
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                        End If

                    End If
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckOrderForDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iAllocationID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sStr As String = ""
        Try
            If iAllocationID > 0 Then
                sSql = "Select * From Sales_Dispatch_Master Where SDM_AllocateID=" & iAllocationID & " And SDM_OrderID=" & iOrderID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Else
                sSql = "Select * From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            End If
            If bCheck = True Then
                sStr = "True"
            Else
                sStr = "False"
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal sOrderType As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_PROForma_Order Where SPO_ID=" & iOrderNo & " And SPO_OrderType='" & sOrderType & "' And SPO_CompID=" & iCompID & " and SPO_YearID =" & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllocatedOrderMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iAllocateID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAllocateID > 0 Then
                sSql = "Select * From Sales_Allocate_Master Where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iOrderNo & " And SAM_CompID=" & iCompID & " and SAM_YearID =" & iYearID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGrandTotal(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer) As Double
        Dim sSql As String = ""
        Dim dGrandTotal As New Double
        Try
            sSql = "Select Sum(SPOD_TotalAmount) From Sales_PROForma_Order_Details Where SPOD_SOID in(Select SPO_ID From Sales_PROForma_Order Where SPO_ID=" & iOrderNo & " And SPO_CompID=" & iCompID & " and SPO_YearID =" & iYearID & ") "
            dGrandTotal = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return dGrandTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Dim sSDate As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 SDM_ID From Sales_Dispatch_Master Order By SDM_ID Desc")
            sYear = objDBL.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDBL.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDBL.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
            If sMaximumID = Nothing Then
                sMaxID = "0001"
            Else
                sLastID = sMaximumID + 1
                If sLastID.Length = 1 Then
                    sMaxID = "000" & "" & sLastID & ""
                ElseIf sLastID.Length = 2 Then
                    sMaxID = "00" & "" & sLastID & ""
                ElseIf sLastID.Length = 3 Then
                    sMaxID = "0" & "" & sLastID & ""
                End If
            End If

            If sMonth.Length = 1 Then
                sMonthCode = "0" & "" & sMonth & ""
            Else
                sMonthCode = sMonth
            End If

            If sDate.Length = 1 Then
                sSDate = "0" & "" & sDate & ""
            Else
                sSDate = sDate
            End If
            sStr = "I-" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sSearch <> "" Then
                sSql = "Select SDM_ID,SDM_Code From Sales_Dispatch_Master where SDM_Code ='" & sSearch & "' And SDM_CompID=" & iCompID & " and SDM_YearID =" & iYearID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select SDM_ID,SDM_Code From Sales_Dispatch_Master where SDM_CompID=" & iCompID & " and SDM_YearID = " & iYearID & " Order By SDM_ID Desc"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateDispatchFlag(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iAllocationNo As Integer)
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Try
            If iAllocationNo > 0 Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Allocate_Master Where SAM_ID=" & iAllocationNo & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " ")
                sSql = "" : sSql = "Update Sales_Allocate_Master Set SAM_DispatchFlag=1 Where SAM_ID=" & iAllocationNo & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                sSql = "" : sSql = "Update Sales_ProForma_Order Set SPO_DispatchFlag=1 Where SPO_OrderType='O' And SPO_ID=" & iOrderNo & " And SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If

            'bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Allocate_Master Where SAM_ID=" & iAllocationNo & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " ")
            'If bCheck = True Then
            '    sSql = "" : sSql = "Update Sales_Allocate_Master Set SAM_DispatchFlag=1 Where SAM_ID=" & iAllocationNo & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " "
            '    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            '    'sSql = "" : sSql = "Update Sales_ProForma_Order Set SPO_DispatchFlag=1 Where SPO_ID=" & iOrderNo & " And SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & " "
            '    'objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            'Else
            '    sSql = "" : sSql = "Update Sales_ProForma_Order Set SPO_DispatchFlag=1 Where SPO_OrderType='O' And SPO_ID=" & iOrderNo & " And SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & " "
            '    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            'End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesMan(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select Usr_Id,Usr_FullName + '-' + Usr_Code as username From Sad_Userdetails where Usr_CompID=" & iCompID & " order by Usr_FullName "
            sSql = "Select Usr_Id,Usr_FullName + '-' + Usr_Code as username From Sad_Userdetails where USR_Designation in(Select MAS_ID From Acc_General_master where MAs_Desc='Sales Person' And mas_master=6) And Usr_CompID=" & iCompID & " order by Usr_FullName "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindExistingCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_DispatchFlag<>1 And SAM_Status='W' And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAllocationID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select SAM_OrderNo From Sales_Allocate_Master Where SAM_ID=" & iAllocationID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & ""
            GetOrderNo = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetOrderNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal dInvoiceDate As Date) As Double
        Dim sSql As String = ""
        Try
            'sSql = "Select Top 1 IMT_VAT From Inventory_Master_TaxDetails Where IMT_MasterID=" & iHistoryID & " And IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_CompID=" & iCompID & " Order By IMT_ID Desc "
            'sSql = "Select Top 1 IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo) And IMT_MasterID = " & iHistoryID & ") Or ((IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iHistoryID & ")  Order By IMT_ID Desc "

            'working'
            'sSql = "Select Top 1 IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') and IMT_EffectiveVATTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_MasterID = " & iHistoryID & ") Or (Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo And IMT_MasterID = " & iHistoryID & ")) Or (IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iHistoryID & "))  Order By IMT_ID Desc "
            'working'
            sSql = "Select Top 1 IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo And IMT_MasterID = " & iHistoryID & ")) Or (IMT_EffectiveVATFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iHistoryID & "))  Order By IMT_ID Desc "
            GetVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetVAT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExcise(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal dInvoiceDate As Date) As Double
        Dim sSql As String = ""
        Try
            'sSql = "Select Top 1 IMT_Excise From Inventory_Master_TaxDetails Where IMT_MasterID=" & iHistoryID & " And IMT_EffectiveExciseFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_CompID=" & iCompID & " Order By IMT_ID Desc "
            'sSql = "Select Top 1 IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFRom and IMT_EffectiveExciseTo) And IMT_MasterID = " & iHistoryID & ") Or ((IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iHistoryID & ")  Order By IMT_ID Desc "

            'working'
            'sSql = "Select Top 1 IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((IMT_EffectiveExciseFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') and IMT_EffectiveExciseTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_MasterID = " & iHistoryID & ") Or (Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFRom and IMT_EffectiveExciseTo And IMT_MasterID = " & iHistoryID & ")) Or (IMT_EffectiveExciseFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iHistoryID & "))  Order By IMT_ID Desc "
            'working'
            sSql = "Select Top 1 IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFRom and IMT_EffectiveExciseTo And IMT_MasterID = " & iHistoryID & ")) Or (IMT_EffectiveExciseFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iHistoryID & "))  Order By IMT_ID Desc "
            GetExcise = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetExcise
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function RemoveDublicate(ByVal dt As DataTable) As DataTable
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
    Public Function SaveCharges(ByVal sNameSpace As String, ByVal objDispatch As ClsDispatchDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_AllocatedID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_AllocatedID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_DispatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_DispatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_OrderType", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.C_OrderType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_ChargeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeType", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDispatch.C_ChargeType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.C_ChargeAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_PSType", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.C_PSType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.C_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.C_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.C_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.C_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.C_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_SalesReturnID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_SalesReturnID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_GoodsReturnID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_GoodsReturnID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCharges_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDefaultBranch As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select SDM_ID,SDM_Code From Sales_Dispatch_Master where SDM_Status<>'A' And SDM_CompID=" & iCompID & " and SDM_YearID = " & iYearID & " Order By SDM_ID Desc"
            sSql = "Select SDM_ID,SDM_Code From Sales_Dispatch_Master,Sales_Proforma_Order where SPO_BranchID=" & iDefaultBranch & " And SDM_OrderID=SPO_ID And SDM_CompID=" & iCompID & " and SDM_YearID = " & iYearID & " Order By SDM_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindChargeData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("ChargeID")
            dtTab.Columns.Add("ChargeType")
            dtTab.Columns.Add("ChargeAmount")

            'If iDispatchID > 0 And iMasterID > 0 Then
            '    sSql = "" : sSql = "Select * From Charges_Master,Dispatch_Master Where C_OrderID=DM_OrderID And C_AllocatedID=DM_AllocateID And C_DispatchID=DM_ID And C_DispatchID=" & iDispatchID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " And C_PSType='S' "
            'End If
            If iMasterID > 0 Then
                sSql = "" : sSql = "Select * From Charges_Master Where C_PSType='S' And C_OrderID=" & iMasterID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ChargeID") = dt.Rows(i)("C_ChargeID")
                    dRow("ChargeType") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt.Rows(i)("C_ChargeID") & " And Mas_Master=24 And Mas_CompID = " & iCompID & "  ")
                    dRow("ChargeAmount") = dt.Rows(i)("C_ChargeAmount")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DashBoardOrderNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_ID,SPO_OrderCode From Sales_ProForma_Order where SPO_YearID =" & iYearID & " And SPO_CompID=" & iCompID & " Order By SPO_ID Desc"
            'sSql = "Select Distinct(SPO_ID) As SPO_ID,SPO_OrderCode From Sales_Proforma_Order,Sales_Allocate_Master Where SPO_ID=SAM_OrderNo And SAM_Status='W' and SAM_YearID =" & iYearID & " And SAM_CompID=" & iCompID & " and SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & " Order By SPO_OrderCode Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDashBoardAllocationNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal dInvoiceDate As Date) As Double
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_Desc From Acc_General_Master Where MAS_ID In (Select Top 1 IMT_VAT From Inventory_Master_TaxDetails Where IMT_MasterID=" & iHistoryID & " And IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_CompID=" & iCompID & " Order By IMT_ID Desc) "
            'sSql = "SELECT MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select Top 1 IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo) And IMT_MasterID = " & iHistoryID & ") Or ((IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iHistoryID & ")  Order By IMT_ID Desc)"

            'Working'
            'sSql = "SELECT MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select Top 1 IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') and IMT_EffectiveVATTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_MasterID = " & iHistoryID & ") Or (Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo And IMT_MasterID = " & iHistoryID & ")) Or (IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iHistoryID & "))  Order By IMT_ID Desc)"
            'working'
            sSql = "SELECT MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select Top 1 IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo And IMT_MasterID = " & iHistoryID & ")) Or (IMT_EffectiveVATFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iHistoryID & "))  Order By IMT_ID Desc)"
            GetItemVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemExcise(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal dInvoiceDate As Date) As Double
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_Desc From Acc_General_Master Where MAS_ID In (Select Top 1 IMT_Excise From Inventory_Master_TaxDetails Where IMT_MasterID=" & iHistoryID & " And IMT_EffectiveExciseFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_CompID=" & iCompID & " Order By IMT_ID Desc) "
            'sSql = "SELECT MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select Top 1 IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFRom and IMT_EffectiveExciseTo) And IMT_MasterID = " & iHistoryID & ") Or ((IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iHistoryID & ")  Order By IMT_ID Desc)"

            'working'
            'sSql = "SELECT MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select Top 1 IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((IMT_EffectiveExciseFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') and IMT_EffectiveExciseTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_MasterID = " & iHistoryID & ") Or (Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFRom and IMT_EffectiveExciseTo And IMT_MasterID = " & iHistoryID & ")) Or (IMT_EffectiveExciseFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iHistoryID & "))  Order By IMT_ID Desc)"
            'working'
            sSql = "SELECT MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select Top 1 IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFRom and IMT_EffectiveExciseTo And IMT_MasterID = " & iHistoryID & ")) Or (IMT_EffectiveExciseFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iHistoryID & "))  Order By IMT_ID Desc)"
            GetItemExcise = objDBL.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ApproveDispatchMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iSDMID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Dispatch_Master Set SDM_Status='A',SDM_ApprovedBy=" & iUserID & ",SDM_ApprovedOn=GetDate() Where SDM_ID=" & iSDMID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDashBoardDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select SDM_ID,SDM_Code From Sales_Dispatch_Master where SDM_CompID=" & iCompID & " and SDM_YearID = " & iYearID & " Order By SDM_ID Desc"
            sSql = "Select SDM_ID,SDM_Code from Sales_Dispatch_Master,Sales_Proforma_Order where SDM_OrderID=SPO_ID And SDM_YearID=" & iYearID & " And SDM_CompID =" & iCompID & " Order By SDM_ID ASC"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatusOfDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select SDM_Status From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
            GetStatusOfDispatchNo = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatusOfDispatchNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindVAt(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=14 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDiscount(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=19 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindExice(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=16 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATOnInvoiceDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINVHID As Integer, ByVal dInvoiceDate As Date) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID,MAS_Desc From Acc_General_Master Where MAS_ID In (select IMT_VAT from Inventory_Master_TaxDetails A where A.IMT_MasterID=" & iINVHID & " And A.IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "')) "
            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo) And IMT_MasterID = " & iINVHID & ") Or ((IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iINVHID & "))"

            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_Master in (14) And MAS_CompID=" & iCompID & " "
            'Working'
            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') and IMT_EffectiveVATTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_MasterID = " & iINVHID & ") Or (Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo And IMT_MasterID = " & iINVHID & ")) Or (IMT_EffectiveVATFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iINVHID & ")))"
            'Working'
            sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_VAT From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveVATFRom and IMT_EffectiveVATTo And IMT_MasterID = " & iINVHID & ")) Or (IMT_EffectiveVATFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveVATTo='1900-01-01') And IMT_MasterID = " & iINVHID & ")))"
            GetVATOnInvoiceDate = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetVATOnInvoiceDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExciseOnInvoiceDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINVHID As Integer, ByVal dInvoiceDate As Date) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID,MAS_Desc From Acc_General_Master Where MAS_ID In (select IMT_Excise from Inventory_Master_TaxDetails A where A.IMT_MasterID=" & iINVHID & " And A.IMT_EffectiveExciseFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "')) "
            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFrom and IMT_EffectiveExciseTo) And IMT_MasterID = " & iINVHID & ") Or ((IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iINVHID & "))"

            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_Master in (16) And MAS_CompID=" & iCompID & " "
            'Working'
            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((IMT_EffectiveExciseFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') and IMT_EffectiveExciseTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_MasterID = " & iINVHID & ") Or (Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFrom and IMT_EffectiveExciseTo And IMT_MasterID = " & iINVHID & ")) Or (IMT_EffectiveExciseFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iINVHID & ")))"
            'Working'
            sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_Excise From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveExciseFrom and IMT_EffectiveExciseTo And IMT_MasterID = " & iINVHID & ")) Or (IMT_EffectiveExciseFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveExciseTo='1900-01-01') And IMT_MasterID = " & iINVHID & ")))"
            GetExciseOnInvoiceDate = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetExciseOnInvoiceDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATFromDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer, ByVal iAllocateID As Integer) As Integer
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=14 And MAS_Desc In (Select SDD_VAT From Sales_Dispatch_Details Where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_AllocateID=" & iAllocateID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " )) "
            sSql = "Select SDD_VAT From Sales_Dispatch_Details Where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_AllocateID=" & iAllocateID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " ) "
            GetVATFromDispatch = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetVATFromDispatch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDiscountFromDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer, ByVal iAllocateID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc In (Select SDD_Discount From Sales_Dispatch_Details Where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_AllocateID=" & iAllocateID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " )) "
            If iAllocateID > 0 Then
                sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc In (Select SDD_Discount From Sales_Dispatch_Details Where SDD_CommodityID=" & iCommodityID & " And SDD_DescID=" & iItemID & " And SDD_HistoryID=" & iHistoryID & " And SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_AllocateID=" & iAllocateID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " )) "
            Else
                sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc In (Select SDD_Discount From Sales_Dispatch_Details Where SDD_CommodityID=" & iCommodityID & " And SDD_DescID=" & iItemID & " And SDD_HistoryID=" & iHistoryID & " And SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " )) "
            End If
            GetDiscountFromDispatch = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDiscountFromDispatch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetiExciseFromDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer, ByVal iAllocateID As Integer) As Integer
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=16 And MAS_Desc In (Select SDD_Excise From Sales_Dispatch_Details Where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_AllocateID=" & iAllocateID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " )) "
            sSql = "Select SDD_Excise From Sales_Dispatch_Details Where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_AllocateID=" & iAllocateID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " ) "
            GetiExciseFromDispatch = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetiExciseFromDispatch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchedROWData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Sales_Dispatch_Details Where SDD_MasterID In(Select SDM_ID From Sales_Dispatch_Master Where "
                sSql = sSql & "SDM_ID =" & iDispatchID & " And SDM_CompID =" & iCompID & " And SDM_YearID =" & iYearID & ") And SDD_CommodityID=" & iCommodityID & " And SDD_DescID=" & iItemID & " And SDD_HistoryID=" & iHistoryID & " And SDD_CompID=" & iCompID & " "
            Else
                If iAllocatedID > 0 Then
                    sSql = "" : sSql = "Select * From Sales_Dispatch_Details Where SDD_MasterID In(Select SDM_ID From Sales_Dispatch_Master Where "
                    sSql = sSql & "SDM_OrderID =" & iMasterID & " And SDM_AllocateID=" & iAllocatedID & " And SDM_CompID =" & iCompID & " And SDM_YearID =" & iYearID & ") And SDD_CommodityID=" & iCommodityID & " And SDD_DescID=" & iItemID & " And SDD_HistoryID=" & iHistoryID & " And SDD_CompID=" & iCompID & " "
                Else
                    sSql = "" : sSql = "Select * From Sales_Dispatch_Details Where SDD_MasterID In(Select SDM_ID From Sales_Dispatch_Master Where "
                    sSql = sSql & "SDM_OrderID =" & iMasterID & " And SDM_CompID =" & iCompID & " And SDM_YearID =" & iYearID & ") And SDD_CommodityID=" & iCommodityID & " And SDD_DescID=" & iItemID & " And SDD_HistoryID=" & iHistoryID & " And SDD_CompID=" & iCompID & " "
                End If
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Acc_Head").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Head").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_Group").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Group").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGroup").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGroup").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_GL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_GL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                Return sPerm
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAllocationSaved(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iAllocateID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_AllocateID=" & iAllocateID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " "
            CheckAllocationSaved = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckAllocationSaved
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTOnInvoiceDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINVHID As Integer, ByVal dInvoiceDate As Date) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID,MAS_Desc From Acc_General_Master Where MAS_ID In (select IMT_CST from Inventory_Master_TaxDetails A where A.IMT_MasterID=" & iINVHID & " And A.IMT_EffectiveCSTFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "')) "
            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_CST From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveCSTFRom and IMT_EffectiveCSTTo) And IMT_MasterID = " & iINVHID & ") Or ((IMT_EffectiveCSTTo='1900-01-01') And IMT_MasterID = " & iINVHID & "))"

            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_Master in (14,15) And MAS_CompID=" & iCompID & " "
            'Working'
            'sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_CST From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((IMT_EffectiveCSTFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') and IMT_EffectiveCSTTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And IMT_MasterID = " & iINVHID & ") Or (Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveCSTFRom and IMT_EffectiveCSTTo And IMT_MasterID = " & iINVHID & ")) Or (IMT_EffectiveCSTFRom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveCSTTo='1900-01-01') And IMT_MasterID = " & iINVHID & ")))"
            'Working'
            sSql = "SELECT MAS_ID,MAS_Desc FROM Acc_General_Master WHERE MAS_ID In (Select IMT_CST From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') between IMT_EffectiveCSTFRom and IMT_EffectiveCSTTo And IMT_MasterID = " & iINVHID & ")) Or (IMT_EffectiveCSTFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dInvoiceDate, "CT") & "') And (IMT_EffectiveCSTTo='1900-01-01') And IMT_MasterID = " & iINVHID & ")))"
            GetCSTOnInvoiceDate = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCSTOnInvoiceDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTFromDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer, ByVal iAllocateID As Integer) As Integer
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc In (Select SDD_CST From Sales_Dispatch_Details Where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_AllocateID=" & iAllocateID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " )) "
            sSql = "Select SDD_CST From Sales_Dispatch_Details Where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_ID=" & iDispatchID & " And SDM_AllocateID=" & iAllocateID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " ) "
            GetCSTFromDispatch = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetCSTFromDispatch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATONCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHistoryID As Integer, ByVal iCSTID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select IMT_VAT From Inventory_Master_TaxDetails Where IMT_MasterID=" & iHistoryID & " And IMT_CST=" & iCSTID & " "
            GetVATONCST = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetVATONCST
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iAllocationID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Delete From Charges_Master Where C_OrderID=" & iOrderID & " And C_AllocatedID=" & iAllocationID & " And C_DispatchID=" & iDispatchID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " And C_PSType='S' "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllSalesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sDispatchNo As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select b.SDD_ID,a.SDM_ID,a.SDM_OrderID,a.SDM_Code,a.SDM_DispatchRefNo,a.SDM_OrderDate,a.SDM_SupplierID,a.SDM_DispatchDate,a.SDM_ShippingRate,a.SDM_GrandDiscount,a.SDM_GrandDiscountAmt,b.SDD_CommodityID,b.SDD_DescID,b.SDD_Rate,b.SDD_Quantity,b.SDD_Discount,b.SDD_DiscountAmount,b.SDD_VAT,b.SDD_VATAmount,b.SDD_CST,b.SDD_CSTAmount,b.SDD_Excise,b.SDD_ExciseAmount,b.SDD_Amount,b.SDD_SGST,b.SDD_SGSTAmount,b.SDD_CGST,b.SDD_CGSTAmount,b.SDD_IGST,b.SDD_IGSTAmount,b.SDD_TotalAmount,
                    c.SPO_OrderCode,d.SPOD_Quantity,f.SAD_PlacedQnt,f.SAD_PendingQty,g.INV_Description As Commodity,h.INV_Code As Item,i.BM_Name As party
                    From Sales_Dispatch_Master a
                    Join Sales_Dispatch_Details b On b.SDD_MasterID=a.SDM_ID 
                    Left Join Sales_PRoForma_Order c on c.SPO_ID=a.SDM_OrderID And c.SPO_YearID=a.SDM_YearID
                    Left Join Sales_PRoForma_Order_Details d On d.SPOD_SOID = a.SDM_OrderID and d.spod_commodityid = b.sdd_CommodityID and d.spod_itemid =b.sdd_DescID and d.spod_HistoryID = b.Sdd_historyID And d.SPOD_YearID=a.SDM_YearID
                    Left Join Sales_Allocate_Master e On e.SAM_OrderNo=a.SDM_OrderID And e.SAM_ID=a.SDM_AllocateID And e.SAM_YearID=a.SDM_YearID
                    Left Join Sales_Allocate_Details f On f.SAD_MasterID=e.SAM_ID and f.sad_commodity = b.sdd_CommodityID and f.sad_Descid =b.sdd_DescID and f.sad_HisotryID = b.Sdd_historyID And f.sad_YearID=a.SDM_YearID
                    Left Join Inventory_Master g On g.INV_ID=b.SDD_CommodityID 
                    Left Join Inventory_Master h On h.INV_ID=b.SDD_DescID And h.INV_ID=d.spod_itemid
                    Left Join Sales_Buyers_Masters i On i.BM_ID=SDM_SupplierID
                    where a.SDM_YearID=" & iYearID & " And a.SDM_CompID=" & iCompID & " "

            If sDispatchNo <> "" Then
                sSql = sSql & "And SDM_ID in (" & sDispatchNo & ") "
            End If
            sSql = sSql & " order by b.SDD_ID"

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt

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
    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Sstr As String) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            If Sstr = "S" Then
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_SJE_ID)+1,1) from Acc_Sales_JE_Master")
                sPrefix = "SJE00" & iMax
                Return sPrefix
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSalesJournalMaster(ByVal sNameSpace As String, ByVal ObjDispatch As ClsDispatchDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(31) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_TransactionNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_Party)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_Location)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_BillType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objGen.SafeSQL(ObjDispatch.sAcc_JE_BillNo))
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dAcc_JE_BillDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dAcc_JE_BillAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_AdvanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dAcc_JE_AdvanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_AdvanceNaration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_AdvanceNaration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BalanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dAcc_JE_BalanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_NetAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dAcc_JE_NetAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_PaymentNarration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_PaymentNarration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_ChequeNo", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_ChequeNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dAcc_JE_ChequeDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_IFSCCode", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_IFSCCode)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BankName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_BankName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BranchName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_BranchName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_CreatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillCreatedDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dAcc_JE_BillCreatedDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_UpdatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_UpdatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_InvoiceID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iAcc_JE_InvoiceID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Type", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sAcc_JE_Type)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Sales_JE_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal ObjDispatch As ClsDispatchDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_ZoneID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_RegionID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_AreaID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_BranchID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dATD_OpenDebit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dATD_OpenCredit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dATD_ClosingDebit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.dATD_ClosingCredit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjDispatch.iATD_SeqReferenceNum)
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
    Public Function LoadTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer, ByVal sExiJV As String) As DataTable
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

            'sSql = "" : sSql = "select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,"
            'sSql = sSql & "A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc,"
            'sSql = sSql & "D.Opn_DebitAmt as GLDebit, D.Opn_CreditAmount as GLCredit,E.Opn_DebitAmt as SubGLDebit, E.Opn_CreditAmount as SubGLCredit "
            'sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on "
            'sSql = sSql & "A.ATD_BillId = " & iPaymentID & " and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on "
            'sSql = sSql & "A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " left join acc_Opening_Balance D on "
            'sSql = sSql & "D.Opn_GLID = A.ATD_Gl and D.Opn_YearID = " & iYearID & " left join acc_Opening_Balance E on "
            'sSql = sSql & "E.Opn_GLID = A.ATD_SubGL and D.Opn_YearID = " & iYearID & " order by a.Atd_id"

            If sExiJV.StartsWith("P") Then
                sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
                sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
                sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 5 and "
                sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_YearID=" & iYearID & " And A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "
            ElseIf sExiJV.StartsWith("S") Then
                sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
                sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
                sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 6 and "
                sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_YearID=" & iYearID & " and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "
            End If
            'sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
            'sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            'sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 4 and "
            'sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "

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
                        dr("Debit") = ds.Tables(0).Rows(i)("ATD_Debit").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Credit").ToString()) = False Then
                        dr("Credit") = ds.Tables(0).Rows(i)("ATD_Credit").ToString()
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Sales_Buyers_Masters Where BM_ID=" & iCustomerID & " And BM_CompID=" & iCompID & " "
            GetCustomerDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCustomerDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_ID From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetGSTID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Double
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_GSTRate From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTRate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTRate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchFormNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDashBoard As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iDashBoard > 0 Then
                sSql = "Select DM_ID,DM_Code From Dispatch_Master,Sales_Proforma_Order where SPO_BranchID=" & iBranch & " And DM_OrderID=SPO_ID And SPO_OrderType='S' And DM_Status='A' And DM_CompID=" & iCompID & " and DM_YearID=" & iYearID & " Order By DM_ID Desc"
            Else
                sSql = "Select DM_ID,DM_Code From Dispatch_Master,Sales_Proforma_Order where SPO_BranchID=" & iBranch & " And DM_OrderID=SPO_ID And SPO_OrderType='S' And DM_Status='A' And DM_CompID=" & iCompID & " and DM_YearID=" & iYearID & " And DM_ID Not In (Select SDM_DispatchID From Sales_Dispatch_Master Where SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") Order By DM_ID Desc"
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchFormData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim dt As New DataTable
        Try
            If iDispatchID > 0 Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Dispatch_Master Where DM_ID=" & iDispatchID & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Dispatch_Master Where DM_ID=" & iDispatchID & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            Else
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Dispatch_Master Where DM_OrderID=" & iOrderNo & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Dispatch_Master Where DM_OrderID=" & iOrderNo & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchFROMData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As DataTable
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
            dtTab.Columns.Add("NetAmount")

            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Dispatch_Details Where DD_MasterID In(Select DM_ID From Dispatch_Master Where "
                sSql = sSql & "DM_ID =" & iDispatchID & " And DM_CompID =" & iCompID & " And DM_YearID =" & iYearID & ") And DD_CompID=" & iCompID & " "
            Else
                If iAllocatedID > 0 Then
                    sSql = "" : sSql = "Select * From Dispatch_Details Where DD_MasterID In(Select DM_ID From Dispatch_Master Where "
                    sSql = sSql & "DM_OrderID =" & iMasterID & " And DM_AllocateID=" & iAllocatedID & " And DM_CompID =" & iCompID & " And DM_YearID =" & iYearID & ") And DD_CompID=" & iCompID & " "
                Else
                    sSql = "" : sSql = "Select * From Dispatch_Details Where DD_MasterID In(Select DM_ID From Dispatch_Master Where "
                    sSql = sSql & "DM_OrderID =" & iMasterID & " And DM_CompID =" & iCompID & " And DM_YearID =" & iYearID & ") And DD_CompID=" & iCompID & " "
                End If
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
                    dRow("DiscountAmount") = ""
                    dRow("Charges") = ""
                    dRow("Amount") = ""
                    dRow("GSTID") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_GST_ID")))
                    dRow("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_GSTRate")))
                    dRow("GSTAmount") = ""
                    dRow("NetAmount") = ""
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchFromChargeData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("ChargeID")
            dtTab.Columns.Add("ChargeType")
            dtTab.Columns.Add("ChargeAmount")

            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Charges_Master,Dispatch_Master Where C_OrderID=DM_OrderID And C_AllocatedID=DM_AllocateID And C_DispatchID=DM_ID And C_DispatchID=" & iDispatchID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " And C_PSType='S' "
            End If
            If iMasterID > 0 Then
                sSql = "" : sSql = "Select * From Charges_Master Where C_PSType='S' And C_OrderID=" & iMasterID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ChargeID") = dt.Rows(i)("C_ChargeID")
                    dRow("ChargeType") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt.Rows(i)("C_ChargeID") & " And Mas_Master=24 And Mas_CompID = " & iCompID & "  ")
                    dRow("ChargeAmount") = dt.Rows(i)("C_ChargeAmount")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDispatchFromChargeAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As Double
        Dim sSql As String = ""
        Try
            If iDispatchID > 0 Then
                sSql = "Select Sum(C_ChargeAmount) From Charges_Master,Dispatch_Master Where C_OrderID=DM_OrderID And C_AllocatedID=DM_AllocateID And C_DispatchID=DM_ID And C_DispatchID=" & iDispatchID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " And C_PSType='S' "
                GetDispatchFromChargeAmount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return GetDispatchFromChargeAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemsTotalFromDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDispatchID As Integer) As Double
        Dim sSql As String = ""
        Try
            If iDispatchID > 0 Then
                sSql = "Select Sum(DD_RateAmount) From Dispatch_Details Where DD_MasterID=" & iDispatchID & " And DD_CompID =" & iCompID & " "
                GetItemsTotalFromDispatch = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return GetItemsTotalFromDispatch
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
    Public Function LoadCompanyType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 2 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGSTCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "select Mas_Id,Mas_Desc from Acc_General_Master where Mas_Master in(Select Mas_ID From Acc_Master_Type Where Mas_Type='Category Of TaxPayer') And Mas_Status='A' and Mas_CompID =" & iCompID & " "
            sSql = "Select GC_ID,GC_GSTCategory From GSTCategory_Table Where GC_CompanyType='" & sCompanyType & "' And GC_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select BM_GSTNCategory from Sales_Buyers_Masters where BM_ID=" & iCustomerID & " and BM_CompID =" & iCompID & " "
            GetGSTCategory = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetGSTCategory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GSTNDesc(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGSTNCategoryID As Integer) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Desc From Acc_General_Master Where Mas_ID=" & iGSTNCategoryID & " And MAS_Master in(Select Mas_ID From Acc_Master_Type Where Mas_Type='Category Of TaxPayer') And MAS_CompID =" & iCompID & " "
            GSTNDesc = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GSTNDesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOralChargeAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As Double
        Dim sSql As String = ""
        Try
            If iOrderID > 0 Then
                sSql = "Select Sum(C_ChargeAmount) From Charges_Master Where C_OrderID=" & iOrderID & " And C_AllocatedID=" & iAllocatedID & " And C_DispatchID=" & iDispatchID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " And C_PSType='S' "
                GetOralChargeAmount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return GetOralChargeAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOralItemsTotal(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As Double
        Dim sSql As String = ""
        Try
            If iOrderID > 0 Then
                sSql = "Select Sum(SPOD_RateAmount) From Sales_ProForma_Order_Details Where SPOD_SOID=" & iOrderID & " And SPOD_CompID =" & iCompID & " "
                GetOralItemsTotal = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return GetOralItemsTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTRateFromHSNTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Double
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_GSTRate From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTRateFromHSNTable = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTRateFromHSNTable
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
    Public Function GetCOAID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
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
    Public Function GetOrderType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_OrderType from Sales_ProForma_Order where SPO_ID = " & iOrderID & " And SPO_YearID=" & iYearID & " and SPO_CompID=" & iCompID & ""
            GetOrderType = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetOrderType
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDiscountFromOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc In (Select SPOD_Discount From Sales_Proforma_Order_Details Where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodityID & " And SPOD_ItemID=" & iItemID & " And SPOD_HistoryID=" & iHistoryID & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ")  "
            GetDiscountFromOral = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDiscountFromOral
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChargeAmountFromOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As Double
        Dim sSql As String = ""
        Try
            If iMasterID > 0 Then
                sSql = "Select Sum(C_ChargeAmount) From Charges_Master Where C_OrderID=" & iMasterID & " And C_PSType='S' And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
                GetChargeAmountFromOral = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return GetChargeAmountFromOral
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemsTotalFromOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As Double
        Dim sSql As String = ""
        Try
            If iMasterID > 0 Then
                sSql = "Select Sum(SPOD_RateAmount) From Sales_Proforma_Order_Details Where SPOD_SOID=" & iMasterID & " And SPOD_YearID=" & iYearID & " And SPOD_CompID =" & iCompID & " "
                GetItemsTotalFromOral = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return GetItemsTotalFromOral
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_BranchID From Sales_Proforma_Order Where SPO_ID in(SElect SDM_OrderID From Sales_Dispatch_Master Where SDM_ID=" & iInvID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & ") "
            GetBranchID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetBranchID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZone(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Sales_Proforma_Order Where SPO_ID in (Select SDM_OrderID From Sales_Dispatch_Master Where SDM_ID=" & iMasterID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " )"
            GetZone = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetZone
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select SPOD_Currency From Sales_Proforma_Order_Details Where SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & "  "
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFERates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer, ByVal dTotal As Double) As String
        Dim sSql As String = "", sToday As String = ""
        Dim dValue As Double = 0, dFEtotal As Double = 0
        Try
            sToday = objGenFun.GetCurrentDate(sNameSpace)
            sSql = "Select CM_TTSell from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            dValue = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If dValue > 0 Then
                dTotal = dValue * dTotal
                Return Math.Round(dTotal, 2, MidpointRounding.AwayFromZero)
            Else
                Return dValue
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBaseCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select sad_Config_Value from sad_config_settings Where sad_Config_Key='Currency' And SAD_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFECRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As String
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGenFun.GetCurrentDate(sNameSpace)
            sSql = "Select CM_TTSell from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFECTime(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As String
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGenFun.GetCurrentDate(sNameSpace)
            sSql = "Select CM_Time from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatusOfOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select SDM_Status From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & "  And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
            GetStatusOfOrder = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatusOfOrder
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
