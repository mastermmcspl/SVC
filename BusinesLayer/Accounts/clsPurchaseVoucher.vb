Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsPurchaseVoucher
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions
    Public Structure Purchase
        Dim PD_ID As Integer
        Dim PD_MasterID As Integer
        Dim PD_HSNCode As String
        Dim PD_Commodity As String
        Dim PD_Goods As String
        Dim PD_Unit As String
        Dim PD_Rate As Double
        Dim PD_Quantity As Double
        Dim PD_ChargePerItem As Double
        Dim PD_RateAmount As Double
        Dim PD_Discount As Double
        Dim PD_DiscountAmount As Double
        Dim PD_Amount As Double
        Dim PD_GSTRate As Double
        Dim PD_GSTAmount As Double
        Dim PD_SGST As Double
        Dim PD_SGSTAmount As Double
        Dim PD_CGST As Double
        Dim PD_CGSTAmount As Double
        Dim PD_IGST As Double
        Dim PD_IGSTAmount As Double
        Dim PD_FinalTotal As Double
        Dim PD_Status As String
        Dim PD_CompID As Integer
        Dim PD_YearID As Integer
        Dim PD_CreatedBy As Integer
        Dim PD_CreatedOn As DateTime
        Dim PD_UpdatedBy As Integer
        Dim PD_UpdatedOn As DateTime
        Dim PD_Operation As String
        Dim PD_IPAddress As String
        Dim PD_FETotalAmt As Double
        Dim PD_Currency As Integer
    End Structure

    Private Acc_Purchase_ID As Integer
    Private Acc_Purchase_TransactionNo As String
    Private Acc_Purchase_Type As Integer
    Private Acc_Purchase_Party As Integer
    Private Acc_Purchase_BillNo As String
    Private Acc_Purchase_BillDate As Date
    Private Acc_Purchase_BillAmount As Decimal
    Private Acc_Purchase_CreatedBy As Integer
    Private Acc_Purchase_CreatedOn As Date
    Private Acc_Purchase_UpdatedBy As Integer
    Private Acc_Purchase_UpdatedOn As Date
    Private Acc_Purchase_ApprovedBy As Integer
    Private Acc_Purchase_ApprovedOn As Date
    Private Acc_Purchase_DeletedBy As Integer
    Private Acc_Purchase_DeletedOn As Date
    Private Acc_Purchase_RecalledBy As Integer
    Private Acc_Purchase_RecalledOn As Date
    Private Acc_Purchase_Year As Integer
    Private Acc_Purchase_CompID As Integer
    Private Acc_Purchase_Status As String
    Private Acc_Purchase_DelFlag As String
    Private Acc_Purchase_Operation As String
    Private Acc_Purchase_IPAddress As String
    Private Acc_Purchase_ReceiptDate As Date
    Private Acc_Purchase_MisMatchFlag As Integer
    Private Acc_Purchase_PaymentStatus As String
    Private Acc_Purchase_OtherCharges As Decimal
    Private Acc_Purchase_TradeDiscount As Decimal
    Private ACC_Purchase_ZoneID As Integer
    Private ACC_Purchase_RegionID As Integer
    Private ACC_Purchase_AreaID As Integer
    Private ACC_Purchase_BranchID As Integer

    Private sACC_Purchase_CompanyAddress As String
    Private sACC_Purchase_CompanyGSTNRegNo As String
    Private sACC_Purchase_BillingAddress As String
    Private sACC_Purchase_BillingGSTNRegNo As String
    Private sACC_Purchase_DeliveryFrom As String
    Private sACC_Purchase_DeliveryFromGSTNRegNo As String
    Private sACC_Purchase_ReceiveAddress As String
    Private sACC_Purchase_ReceiveGSTNRegNo As String
    Private sACC_Purchase_InvoiceStatus As String
    Private iACC_Purchase_CompanyType As Integer
    Private iACC_Purchase_GSTNCategory As Integer
    Private sAcc_Purchase_State As String
    Private Acc_Purchase_PendingAmount As Decimal

    Private ATD_ID As Integer
    Private ATD_TransactionDate As Date
    Private ATD_TrType As Integer
    Private ATD_BillID As Integer
    Private ATD_PaymentType As Integer
    Private ATD_Head As Integer
    Private ATD_GL As Integer
    Private ATD_SubGL As Integer
    Private ATD_DbOrCr As Integer
    Private ATD_Debit As Decimal
    Private ATD_Credit As Decimal
    Private ATD_CreatedBy As Integer
    Private ATD_CreatedOn As Date
    Private ATD_UpdatedBy As Integer
    Private ATD_UpdatedOn As Date
    Private ATD_Status As String
    Private ATD_YearID As Integer
    Private ATD_CompID As Integer
    Private ATD_Operation As String
    Private ATD_IPAddress As String

    Private ATD_ZoneID As Integer
    Private ATD_RegionID As Integer
    Private ATD_AreaID As Integer
    Private ATD_BranchID As Integer

    Private Acc_PMD_ID As Integer
    Private Acc_PMD_MasterID As Integer
    Private Acc_PMD_Head As Integer
    Private Acc_PMD_GL As Integer
    Private Acc_PMD_SubGL As Integer
    Private Acc_PMD_Amount As Double
    Private Acc_PMD_TaxType As Integer
    Private Acc_PMD_TaxRate As Integer
    Private Acc_PMD_TaxAmount As Double
    Private Acc_PMD_TotalAmount As Double
    Private Acc_PMD_RoundOff As String
    Private Acc_PMD_Status As String
    Private Acc_PMD_CompID As Integer
    Private Acc_PMD_YearID As Integer
    Private Acc_PMD_PendingAmount As Double
    Private Acc_PMD_TradeDis As Decimal
    Private Acc_PMD_TradeDisAmt As Decimal
    Private Acc_PMD_NetAmount As Decimal

    Private iC_ID As Integer
    Private iC_TRID As Integer
    Private sC_TRType As String
    Private iC_ChargeID As Integer
    Private sC_ChargeType As String
    Private iC_ChargeAmount As Double
    Private sC_DelFlag As String
    Private sC_Status As String
    Private iC_YearID As Integer
    Private iC_CompID As Integer
    Private iC_CreatedBy As Integer
    Private iC_CreatedOn As Date
    Private sC_Operation As String
    Private sC_IPAddress As String

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

    Public Property dAcc_Purchase_PendingAmount() As Decimal
        Get
            Return (Acc_Purchase_PendingAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_Purchase_PendingAmount = Value
        End Set
    End Property
    Public Property Acc_Purchase_CompanyAddress() As String
        Get
            Return (sAcc_Purchase_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_CompanyAddress = Value
        End Set
    End Property
    Public Property Acc_Purchase_CompanyGSTNRegNo() As String
        Get
            Return (sAcc_Purchase_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property Acc_Purchase_BillingAddress() As String
        Get
            Return (sAcc_Purchase_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_BillingAddress = Value
        End Set
    End Property
    Public Property Acc_Purchase_BillingGSTNRegNo() As String
        Get
            Return (sAcc_Purchase_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property Acc_Purchase_DeliveryFrom() As String
        Get
            Return (sAcc_Purchase_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_DeliveryFrom = Value
        End Set
    End Property
    Public Property Acc_Purchase_DeliveryFromGSTNRegNo() As String
        Get
            Return (sAcc_Purchase_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_DeliveryFromGSTNRegNo = Value
        End Set
    End Property
    Public Property Acc_Purchase_ReceiveAddress() As String
        Get
            Return (sAcc_Purchase_ReceiveAddress)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_ReceiveAddress = Value
        End Set
    End Property
    Public Property Acc_Purchase_ReceiveGSTNRegNo() As String
        Get
            Return (sAcc_Purchase_ReceiveGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_ReceiveGSTNRegNo = Value
        End Set
    End Property
    Public Property Acc_Purchase_InvoiceStatus() As String
        Get
            Return (sAcc_Purchase_InvoiceStatus)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_InvoiceStatus = Value
        End Set
    End Property
    Public Property Acc_Purchase_State() As String
        Get
            Return (sAcc_Purchase_State)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_State = Value
        End Set
    End Property
    Public Property Acc_Purchase_CompanyType() As Integer
        Get
            Return (iAcc_Purchase_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iAcc_Purchase_CompanyType = Value
        End Set
    End Property
    Public Property Acc_Purchase_GSTNCategory() As Integer
        Get
            Return (iAcc_Purchase_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iAcc_Purchase_GSTNCategory = Value
        End Set
    End Property


    Public Property iACC_Purchase_ZoneID() As Integer
        Get
            Return (ACC_Purchase_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            ACC_Purchase_ZoneID = Value
        End Set
    End Property
    Public Property iACC_Purchase_RegionID() As Integer
        Get
            Return (ACC_Purchase_RegionID)
        End Get
        Set(ByVal Value As Integer)
            ACC_Purchase_RegionID = Value
        End Set
    End Property
    Public Property iACC_Purchase_AreaID() As Integer
        Get
            Return (ACC_Purchase_AreaID)
        End Get
        Set(ByVal Value As Integer)
            ACC_Purchase_AreaID = Value
        End Set
    End Property
    Public Property iACC_Purchase_BranchID() As Integer
        Get
            Return (ACC_Purchase_BranchID)
        End Get
        Set(ByVal Value As Integer)
            ACC_Purchase_BranchID = Value
        End Set
    End Property
    Public Property dAcc_PMD_TradeDis() As Decimal
        Get
            Return (Acc_PMD_TradeDis)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PMD_TradeDis = Value
        End Set
    End Property
    Public Property dAcc_PMD_TradeDisAmt() As Decimal
        Get
            Return (Acc_PMD_TradeDisAmt)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PMD_TradeDisAmt = Value
        End Set
    End Property
    Public Property dAcc_PMD_NetAmount() As Decimal
        Get
            Return (Acc_PMD_NetAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PMD_NetAmount = Value
        End Set
    End Property
    Public Property dAcc_PMD_PendingAmount() As Double
        Get
            Return (Acc_PMD_PendingAmount)
        End Get
        Set(ByVal Value As Double)
            Acc_PMD_PendingAmount = Value
        End Set
    End Property
    Public Property sAcc_Purchase_PaymentStatus() As String
        Get
            Return (Acc_Purchase_PaymentStatus)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_PaymentStatus = Value
        End Set
    End Property
    Public Property iAcc_Purchase_MisMatchFlag() As Integer
        Get
            Return (Acc_Purchase_MisMatchFlag)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_MisMatchFlag = Value
        End Set
    End Property

    Public Property dAcc_Purchase_ReceiptDate() As Date
        Get
            Return (Acc_Purchase_ReceiptDate)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_ReceiptDate = Value
        End Set
    End Property
    Public Property iAcc_PMD_YearID() As Integer
        Get
            Return (Acc_PMD_YearID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_YearID = Value
        End Set
    End Property
    Public Property iAcc_PMD_CompID() As Integer
        Get
            Return (Acc_PMD_CompID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_CompID = Value
        End Set
    End Property
    Public Property sAcc_PMD_Status() As String
        Get
            Return (Acc_PMD_Status)
        End Get
        Set(ByVal Value As String)
            Acc_PMD_Status = Value
        End Set
    End Property

    Public Property sAcc_PMD_RoundOff() As String
        Get
            Return (Acc_PMD_RoundOff)
        End Get
        Set(ByVal Value As String)
            Acc_PMD_RoundOff = Value
        End Set
    End Property
    Public Property dAcc_PMD_TotalAmount() As Double
        Get
            Return (Acc_PMD_TotalAmount)
        End Get
        Set(ByVal Value As Double)
            Acc_PMD_TotalAmount = Value
        End Set
    End Property
    Public Property dAcc_PMD_TaxAmount() As Double
        Get
            Return (Acc_PMD_TaxAmount)
        End Get
        Set(ByVal Value As Double)
            Acc_PMD_TaxAmount = Value
        End Set
    End Property
    Public Property iAcc_PMD_TaxRate() As Integer
        Get
            Return (Acc_PMD_TaxRate)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_TaxRate = Value
        End Set
    End Property
    Public Property iAcc_PMD_TaxType() As Integer
        Get
            Return (Acc_PMD_TaxType)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_TaxType = Value
        End Set
    End Property

    Public Property dAcc_PMD_Amount() As Double
        Get
            Return (Acc_PMD_Amount)
        End Get
        Set(ByVal Value As Double)
            Acc_PMD_Amount = Value
        End Set
    End Property
    Public Property iAcc_PMD_SubGL() As Integer
        Get
            Return (Acc_PMD_SubGL)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_SubGL = Value
        End Set
    End Property
    Public Property iAcc_PMD_GL() As Integer
        Get
            Return (Acc_PMD_GL)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_GL = Value
        End Set
    End Property
    Public Property iAcc_PMD_Head() As Integer
        Get
            Return (Acc_PMD_Head)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_Head = Value
        End Set
    End Property
    Public Property iAcc_PMD_MasterID() As Integer
        Get
            Return (Acc_PMD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_MasterID = Value
        End Set
    End Property
    Public Property iAcc_PMD_ID() As Integer
        Get
            Return (Acc_PMD_ID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PMD_ID = Value
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

    Public Property iATD_YearID() As Integer
        Get
            Return (ATD_YearID)
        End Get
        Set(ByVal Value As Integer)
            ATD_YearID = Value
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
    Public Property dATD_UpdatedOn() As Date
        Get
            Return (ATD_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_UpdatedOn = Value
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
    Public Property dATD_CreatedOn() As Date
        Get
            Return (ATD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            ATD_CreatedOn = Value
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
    Public Property iATD_DbOrCr() As Integer
        Get
            Return (ATD_DbOrCr)
        End Get
        Set(ByVal Value As Integer)
            ATD_DbOrCr = Value
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
    Public Property iATD_PaymentType() As Integer
        Get
            Return (ATD_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            ATD_PaymentType = Value
        End Set
    End Property
    Public Property iATD_BillID() As Integer
        Get
            Return (ATD_BillID)
        End Get
        Set(ByVal Value As Integer)
            ATD_BillID = Value
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

    Public Property dAcc_Purchase_OtherCharges() As Decimal
        Get
            Return (Acc_Purchase_OtherCharges)
        End Get
        Set(ByVal Value As Decimal)
            Acc_Purchase_OtherCharges = Value
        End Set
    End Property
    Public Property dAcc_Purchase_TradeDiscount() As Decimal
        Get
            Return (Acc_Purchase_TradeDiscount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_Purchase_TradeDiscount = Value
        End Set
    End Property
    Public Property sAcc_Purchase_IPAddress() As String
        Get
            Return (Acc_Purchase_IPAddress)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_IPAddress = Value
        End Set
    End Property
    Public Property sAcc_Purchase_Operation() As String
        Get
            Return (Acc_Purchase_Operation)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_Operation = Value
        End Set
    End Property
    Public Property sAcc_Purchase_DelFlag() As String
        Get
            Return (Acc_Purchase_DelFlag)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_DelFlag = Value
        End Set
    End Property
    Public Property sAcc_Purchase_Status() As String
        Get
            Return (Acc_Purchase_Status)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_Status = Value
        End Set
    End Property
    Public Property iAcc_Purchase_CompID() As Integer
        Get
            Return (Acc_Purchase_CompID)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_CompID = Value
        End Set
    End Property
    Public Property iAcc_Purchase_Year() As Integer
        Get
            Return (Acc_Purchase_Year)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_Year = Value
        End Set
    End Property
    Public Property dAcc_Purchase_RecalledOn() As Date
        Get
            Return (Acc_Purchase_RecalledOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_RecalledOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_RecalledBy() As Integer
        Get
            Return (Acc_Purchase_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_RecalledBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_DeletedOn() As Date
        Get
            Return (Acc_Purchase_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_DeletedOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_DeletedBy() As Integer
        Get
            Return (Acc_Purchase_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_DeletedBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_ApprovedOn() As Date
        Get
            Return (Acc_Purchase_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_ApprovedOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_ApprovedBy() As Integer
        Get
            Return (Acc_Purchase_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_ApprovedBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_UpdatedOn() As Date
        Get
            Return (Acc_Purchase_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_UpdatedOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_UpdatedBy() As Integer
        Get
            Return (Acc_Purchase_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_UpdatedBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_CreatedOn() As Date
        Get
            Return (Acc_Purchase_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_CreatedOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_CreatedBy() As Integer
        Get
            Return (Acc_Purchase_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_CreatedBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_BillAmount() As Decimal
        Get
            Return (Acc_Purchase_BillAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_Purchase_BillAmount = Value
        End Set
    End Property
    Public Property dAcc_Purchase_BillDate() As Date
        Get
            Return (Acc_Purchase_BillDate)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_BillDate = Value
        End Set
    End Property
    Public Property sAcc_Purchase_BillNo() As String
        Get
            Return (Acc_Purchase_BillNo)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_BillNo = Value
        End Set
    End Property
    Public Property iAcc_Purchase_Party() As Integer
        Get
            Return (Acc_Purchase_Party)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_Party = Value
        End Set
    End Property
    Public Property iAcc_Purchase_Type() As Integer
        Get
            Return (Acc_Purchase_Type)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_Type = Value
        End Set
    End Property
    Public Property sAcc_Purchase_TransactionNo() As String
        Get
            Return (Acc_Purchase_TransactionNo)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_TransactionNo = Value
        End Set
    End Property
    Public Property iAcc_Purchase_ID() As Integer
        Get
            Return (Acc_Purchase_ID)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_ID = Value
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
    Public Property C_TRID() As Integer
        Get
            Return (iC_TRID)
        End Get
        Set(ByVal Value As Integer)
            iC_TRID = Value
        End Set
    End Property
    Public Property C_TRType() As String
        Get
            Return (sC_TRType)
        End Get
        Set(ByVal Value As String)
            sC_TRType = Value
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




    Public Function LoadVCE(sNameSpace As String, ByVal sType As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = "", aSql As String = ""
        Dim dt As New DataTable
        Dim GLid As Integer = 0
        Try
            sSql = "Select Acc_GL from acc_application_settings where Acc_Types='Supplier' and Acc_LedgerType ='" & sType & "' and Acc_CompId=" & iCompID & ""
            GLid = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If GLid > 0 Then
                aSql = " Select gl_Id, GL_Desc From chart_of_accounts Where gl_parent = " & GLid & " order by gl_id"
            End If
            dt = objDBL.SQLExecuteDataTable(sNameSpace, aSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(Count(Acc_Purchase_ID)+1,1) from Acc_Purchase_Master")
            sPrefix = "P" & "00" & iMax
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPurchaseID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Acc_Purchase_Master where Acc_Purchase_ID =" & iPurchaseID & " And Acc_Purchase_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteTransactionPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPurchaseID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Delete acc_Transactions_Details where ATD_BillId =" & iPurchaseID & " And ATD_CompID =" & iCompID & " And ATD_TrType=8"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql)

            sSql = "Delete acc_Purchase_Master_Details where Acc_PMD_MasterID =" & iPurchaseID & " And Acc_PMD_CompID =" & iCompID & ""
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeletePurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPurchaseID As Integer) As DataSet
        Dim sSql As String = ""
        Try
            sSql = "Delete acc_Purchase_Master_Details where Acc_PMD_MasterID =" & iPurchaseID & " And Acc_PMD_CompID =" & iCompID & ""
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetchartofAccounts(ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select gl_id, gl_glCode, gl_Parent, gl_desc from chart_of_Accounts"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExePurchaseVoucherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYear As Integer, ByVal iPayment As Integer) As DataTable
        Dim dt As New DataTable, dtGLCODE As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("ID", GetType(String))
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

            dtGLCODE = GetchartofAccounts(sNameSpace)

            sSql = "Select * from acc_Transactions_Details where ATD_BillID=" & iPayment & " And ATD_YearID=" & iYear & " And ATD_CompID =" & iCompID & " And ATD_TrType=8"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)

            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = 0

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Head").ToString()) = False Then
                        dr("HeadID") = ds.Tables(0).Rows(i)("ATD_Head").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_GL").ToString()) = False Then
                        Dim dtGL As New DataTable
                        Dim DVGLCODE As New DataView(dtGLCODE)
                        DVGLCODE.RowFilter = "Gl_id=" & ds.Tables(0).Rows(i)("ATD_GL")
                        dtGL = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dr("GLID") = dtGL.Rows(0)("gl_id")
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_SubGL").ToString()) = False Then
                        Dim dtGL As New DataTable
                        Dim DVGLCODE As New DataView(dtGLCODE)
                        DVGLCODE.RowFilter = "Gl_id=" & ds.Tables(0).Rows(i)("ATD_SubGL")
                        dtGL = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dr("SubGLID") = dtGL.Rows(0)("gl_id")
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()) = False Then
                        dr("PaymentID") = ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()
                    End If

                    dr("SrNo") = i + 1

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "1" Then
                            dr("Type") = "Bill Amount"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "2" Then
                            dr("Type") = "VAT"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "3" Then
                            dr("Type") = "CST"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "4" Then
                            dr("Type") = "Excise"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "5" Then
                            dr("Type") = "Supplier"
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_GL").ToString()) = False Then
                        Dim dtGL As New DataTable
                        Dim DVGLCODE As New DataView(dtGLCODE)
                        DVGLCODE.RowFilter = "Gl_id=" & ds.Tables(0).Rows(i)("ATD_GL")
                        dtGL = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dr("GLCode") = dtGL.Rows(0)("gl_glCode")
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_GL").ToString()) = False Then
                        Dim dtGL As New DataTable
                        Dim DVGLCODE As New DataView(dtGLCODE)
                        DVGLCODE.RowFilter = "Gl_id=" & ds.Tables(0).Rows(i)("ATD_GL")
                        dtGL = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dr("GLDescription") = dtGL.Rows(0)("gl_Desc")
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_SubGL").ToString()) = False Then
                        Dim dtGL As New DataTable
                        Dim DVGLCODE As New DataView(dtGLCODE)
                        DVGLCODE.RowFilter = "Gl_id=" & ds.Tables(0).Rows(i)("ATD_SubGL")
                        dtGL = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dr("SubGL") = dtGL.Rows(0)("gl_glCode")
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_SubGL").ToString()) = False Then
                        Dim dtGL As New DataTable
                        Dim DVGLCODE As New DataView(dtGLCODE)
                        DVGLCODE.RowFilter = "Gl_id=" & ds.Tables(0).Rows(i)("ATD_SubGL")
                        dtGL = DVGLCODE.ToTable
                        If dtGL.Rows.Count > 0 Then
                            dr("SubGLDescription") = dtGL.Rows(0)("gl_Desc")
                        End If
                    End If

                    dr("OpeningBalance") = 0

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Debit").ToString()) = False Then
                        dr("Debit") = ds.Tables(0).Rows(i)("ATD_Debit").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Credit").ToString()) = False Then
                        dr("Credit") = ds.Tables(0).Rows(i)("ATD_Credit").ToString()
                    End If
                    dr("Balance") = 0
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseVoucher(ByVal sNameSpace As String, ByVal iCompID As Integer, objPurchase As clsPurchaseVoucher)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(36) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_TransactionNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_BillNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_BillDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_BillAmount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_BillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_Year", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_Year
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_ReceiptDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_ReceiptDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_MisMatchFlag", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_MisMatchFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_PaymentStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_PaymentStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_OtherCharges", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_OtherCharges
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iACC_Purchase_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iACC_Purchase_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iACC_Purchase_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iACC_Purchase_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_CompanyAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_BillingAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_BillingAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_BillingGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_BillingGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_DeliveryFrom", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_DeliveryFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_DeliveryFromGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_DeliveryFromGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_ReceiveAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_ReceiveAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_ReceiveGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_ReceiveGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_InvoiceStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.Acc_Purchase_InvoiceStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.Acc_Purchase_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.Acc_Purchase_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_State", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objPurchase.Acc_Purchase_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_PendingAmount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_PendingAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Purchase_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchaseVoucher(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYear As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransactionNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Party", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillAmount", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillStatus", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Acc_Purchase_Master where Acc_Purchase_Year=" & iYear & " And Acc_Purchase_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Acc_Purchase_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_Purchase_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_Purchase_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By Acc_Purchase_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_Purchase_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_Purchase_TransactionNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_BillNo").ToString()) = False Then
                        dr("BillNo") = ds.Tables(0).Rows(i)("Acc_Purchase_BillNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_BillDate").ToString()) = False Then
                        dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_Purchase_BillDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_BillAmount").ToString()) = False Then
                        dr("BillAmount") = ds.Tables(0).Rows(i)("Acc_Purchase_BillAmount").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_Purchase_Party").ToString()) = False Then
                        dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_Purchase_Party").ToString())
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_Purchase_MisMatchFlag") = "0") Then
                        dr("BillStatus") = "Matched"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_Purchase_MisMatchFlag") = "1") Then
                        dr("BillStatus") = "Mis Matched"
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_Purchase_DelFlag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_Purchase_DelFlag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_Purchase_DelFlag") = "D") Then
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
    Public Function GetPartyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As String
        Dim sSQL As String = ""
        Dim sParty As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "" : sSQL = "Select *  from CustomerSupplierMaster where CSM_Delflag='A' and CSM_ID = " & iParty & " and CSM_CompID= " & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("CSM_Name").ToString()) = False Then
                    sParty = dt.Rows(0)("CSM_Name").ToString() & " - " & dt.Rows(0)("CSM_Code").ToString()
                Else
                    sParty = ""
                End If
            Else
                sParty = ""
            End If
            Return sParty
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub UpdatePurchaseVoucherStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
        'Dim sSql As String = ""
        'Dim dt As New DataTable
        'Try
        '    sSql = "Update Acc_Purchase_Master Set Acc_Purchase_IPAddress='" & sIPAddress & "',"
        '    If sStatus = "W" Then
        '        sSql = sSql & " Acc_Purchase_DelFlag='A',Acc_Purchase_Status ='A',Acc_Purchase_ApprovedBy= " & iUserID & ",Acc_Purchase_ApprovedOn=GetDate()"
        '    ElseIf sStatus = "D" Then
        '        sSql = sSql & " Acc_Purchase_DelFlag='D',Acc_Purchase_Status='D',Acc_Purchase_DeletedBy= " & iUserID & ",Acc_Purchase_DeletedOn=GetDate()"
        '    ElseIf sStatus = "A" Then
        '        sSql = sSql & " Acc_Purchase_DelFlag='A',Acc_Purchase_Status='A' "
        '    End If
        '    sSql = sSql & " Where Acc_Purchase_ID = " & iMasId & " and Acc_Purchase_CompID=" & iCompID & ""
        '    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        '    dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=8 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
        '    If dt.Rows.Count > 0 Then
        '        For i = 0 To dt.Rows.Count - 1
        '            sSql = "" : sSql = "Update Acc_Transactions_Details Set ATD_IPAddress='" & sIPAddress & "' "
        '            If sStatus = "D" Then
        '                sSql = sSql & " ,ATD_Status='D',ATD_DeletedBy= " & iUserID & ",ATD_DeletedOn=GetDate()"
        '            ElseIf sStatus = "A" Then
        '                sSql = sSql & " ,ATD_Status='A',ATD_ApprovedBy=" & iUserID & ",ATD_ApprovedOn=GetDate() "
        '            End If
        '            sSql = sSql & " Where ATD_ID = " & dt.Rows(i)("ATD_ID") & ""
        '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        '        Next
        '    End If
        'Catch ex As Exception
        '    Throw
        'End Try
        Dim sSql As String = ""
        Dim dt, dtDebitCredit As New DataTable
        Dim dOpnDebit, dOpnCredit, dClosingDebit, dClosingCredit As Double
        Dim iSequenceNum As Integer
        Try
            sSql = "" : sSql = "Update Acc_Purchase_Master Set Acc_Purchase_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_Purchase_Status='A',Acc_Purchase_DelFlag ='A',Acc_Purchase_ApprovedBy= " & iUserID & ",Acc_Purchase_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_Purchase_Status='D',Acc_Purchase_DeletedBy= " & iUserID & ",Acc_Purchase_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_Purchase_Status='A',Acc_Purchase_DelFlag ='A' "
            End If
            sSql = sSql & " Where Acc_Purchase_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)


            dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=8 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
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
                            sSql = sSql & " Where ATD_ID = " & dt.Rows(i)("ATD_ID") & ""
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
                            sSql = sSql & " Where ATD_ID = " & dt.Rows(i)("ATD_ID") & ""
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                        End If
                    End If

                Next
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Function LoadExistingVoucherNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iParty = 0 Then
                sSql = "Select Acc_Purchase_TransactionNo,Acc_Purchase_ID from  Acc_Purchase_Master where Acc_Purchase_CompID=" & iCompID & " and Acc_Purchase_Year=" & iYearID & " order by Acc_Purchase_ID Desc"
            Else
                sSql = "Select Acc_Purchase_TransactionNo,Acc_Purchase_ID from  Acc_Purchase_Master where Acc_Purchase_CompID=" & iCompID & " and Acc_Purchase_Year=" & iYearID & " and Acc_Purchase_Party = " & iParty & " order by Acc_Purchase_ID Desc"
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objPurchase As clsPurchaseVoucher)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_BillID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPurchase.sATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.sATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.sATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iATD_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iATD_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iATD_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iATD_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_OpenDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_OpenCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_ClosingDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_ClosingCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iATD_SeqReferenceNum
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spacc_Transactions_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSupplierSubGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select CSM_SubGL From CustomerSupplierMaster Where CSM_ID=" & iPartyID & " And CSM_DelFlag='A' and CSM_CompID =" & iCompID & " "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                GetSupplierSubGLID = dt.Rows(0)("CSM_SubGL").ToString()
            Else
                GetSupplierSubGLID = 0
            End If
            Return GetSupplierSubGLID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetGLCodeAndDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGlID As Integer) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc  from  chart_of_Accounts   where gl_id =" & iGlID & " and gl_CompID =" & iCompID & " "
            sSql = sSql & "and gl_Delflag ='C' and gl_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                GetGLCodeAndDescription = dt.Rows(0)("GlDesc").ToString()
            Else
                GetGLCodeAndDescription = ""
            End If
            Return GetGLCodeAndDescription
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPerValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGlID As Integer) As String
        Dim sSql As String = "", iValue As String = ""
        Try
            sSql = "" : sSql = "Select gl_reason_creation from  chart_of_Accounts where gl_id =" & iGlID & " and gl_CompID =" & iCompID & ""
            sSql = sSql & "and gl_Delflag ='C' and gl_status='A'"
            iValue = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iValue
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCustomerGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iGL As Integer = 0
        Try
            sSql = "" : sSql = "Select * from CustomerSupplierMaster where CSM_ID =" & iSupplier & " and CSM_Delflag='A' and CSM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                iGL = dt.Rows(0)("CSM_SUbGL").ToString()
            End If
            Return iGL
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objPurchase As clsPurchaseVoucher)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_Amount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_PMD_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_TaxType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_TaxType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_TaxRate", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_TaxRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_TaxAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_PMD_TaxAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_TotalAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_PMD_TotalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_PendingAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_PMD_PendingAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPurchase.sAcc_PMD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_PMD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_RoundOff", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objPurchase.sAcc_PMD_RoundOff
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_TradeDis", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_PMD_TradeDis
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_TradeDisAmt", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_PMD_TradeDisAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PMD_NetAmount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_PMD_NetAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Purchase_Master_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccountsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer, ByVal dtCoA As DataTable)
        Dim sSql As String = ""
        Dim dc As New DataColumn
        Dim dt As New DataTable
        Dim iSubGL As Integer
        Dim dr As DataRow
        Dim objPSJEDetails As New ClsPurchaseSalesJEDetails
        Try
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)

            Dim dtDGL As New DataTable
            Dim DVGLCODE As New DataView(dtCoA)

            iSubGL = GetCustomerGLID(sNameSpace, iCompID, iParty)
            DVGLCODE.RowFilter = "Gl_id=" & iSubGL
            dtDGL = DVGLCODE.ToTable

            'Purchase
            dr = dt.NewRow
            dr("GLID") = objPSJEDetails.GetAccountDetails(sNameSpace, iCompID, "Supplier", "Purchase", "Acc_GL")
            dr("SubGLID") = objPSJEDetails.GetAccountDetails(sNameSpace, iCompID, "Supplier", "Purchase", "Acc_SubGL")

            If dtCoA.Rows.Count > 0 Then
                For i = 0 To dtCoA.Rows.Count - 1
                    If dtCoA.Rows(i)("gl_id").ToString() = dr("GLID") Then

                        If dtCoA.Rows(i)("gl_glCode").ToString() <> "" Then
                            dr("GLCode") = dtCoA.Rows(i)("gl_glCode").ToString() & " - " & dtCoA.Rows(i)("gl_Desc").ToString()
                        End If
                    End If

                    If dtCoA.Rows(i)("gl_id").ToString() = dr("SubGLID") Then

                        If dtCoA.Rows(i)("gl_glCode").ToString() <> "" Then
                            dr("SubGLCode") = dtCoA.Rows(i)("gl_glCode").ToString() & " - " & dtCoA.Rows(i)("gl_Desc").ToString()
                        End If

                    End If
                Next
            End If
            dt.Rows.Add(dr)

            'Supplier
            If dtCoA.Rows.Count > 0 Then
                dr = dt.NewRow
                For i = 0 To dtCoA.Rows.Count - 1
                    If dtCoA.Rows(i)("gl_id").ToString() = dtDGL.Rows(0)("gl_Parent") Then

                        If dtCoA.Rows(i)("gl_glCode").ToString() <> "" Then
                            dr("GLCode") = dtCoA.Rows(i)("gl_glCode").ToString() & " - " & dtCoA.Rows(i)("gl_Desc").ToString()
                        End If
                    End If

                    If dtCoA.Rows(i)("gl_id").ToString() = iSubGL Then

                        If dtCoA.Rows(i)("gl_glCode").ToString() <> "" Then
                            dr("SubGLCode") = dtCoA.Rows(i)("gl_glCode").ToString() & " - " & dtCoA.Rows(i)("gl_Desc").ToString()
                        End If
                    End If
                Next
                dt.Rows.Add(dr)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYear As Integer, ByVal iPayment As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("ID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TaxTypeID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TaxRateID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Amount", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TradeDis", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TradeDisAmt", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TotalNetAmt", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TaxType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TaxRate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TaxAmount", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Total", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Roundof", GetType(String))
            dt.Columns.Add(dc)


            sSql = "" : sSql = "Select * from acc_Purchase_Master_Details where Acc_PMD_MasterID=" & iPayment & " And Acc_PMD_YearID=" & iYear & " And Acc_PMD_CompID =" & iCompID & ""
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)

            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = 0

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_Head").ToString()) = False Then
                        dr("HeadID") = ds.Tables(0).Rows(i)("Acc_PMD_Head").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_GL").ToString()) = False Then
                        dr("GLID") = ds.Tables(0).Rows(i)("Acc_PMD_GL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_SubGL").ToString()) = False Then
                        dr("SubGLID") = ds.Tables(0).Rows(i)("Acc_PMD_SubGL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_Amount").ToString()) = False Then
                        dr("Amount") = Convert.ToDecimal(ds.Tables(0).Rows(i)("Acc_PMD_Amount").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_TradeDis").ToString()) = False Then
                        dr("TradeDis") = Convert.ToDecimal(ds.Tables(0).Rows(i)("Acc_PMD_TradeDis").ToString()).ToString("#,##0.00")
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_TradeDisAmt").ToString()) = False Then
                        dr("TradeDisAmt") = Convert.ToDecimal(ds.Tables(0).Rows(i)("Acc_PMD_TradeDisAmt").ToString()).ToString("#,##0.00")
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_NetAmount").ToString()) = False Then
                        dr("TotalNetAmt") = Convert.ToDecimal(ds.Tables(0).Rows(i)("Acc_PMD_NetAmount").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_TaxType").ToString()) = False Then
                        dr("TaxTypeID") = ds.Tables(0).Rows(i)("Acc_PMD_TaxType").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_TaxRate").ToString()) = False Then
                        dr("TaxRateID") = ds.Tables(0).Rows(i)("Acc_PMD_TaxRate").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_TaxType").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("Acc_PMD_TaxType").ToString() = "1" Then
                            dr("TaxType") = "VAT"
                        ElseIf ds.Tables(0).Rows(i)("Acc_PMD_TaxType").ToString() = "2" Then
                            dr("TaxType") = "CST"
                        ElseIf ds.Tables(0).Rows(i)("Acc_PMD_TaxType").ToString() = "3" Then
                            dr("TaxType") = "EXCISE"
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_TaxRate").ToString()) = False Then
                        dr("TaxRate") = objDBL.SQLExecuteScalar(sNameSpace, "Select gl_Desc from Chart_of_Accounts where gl_id =" & ds.Tables(0).Rows(i)("Acc_PMD_TaxRate").ToString() & " and gl_CompID=" & iCompID & "")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_TaxAmount").ToString()) = False Then
                        dr("TaxAmount") = Convert.ToDecimal(ds.Tables(0).Rows(i)("Acc_PMD_TaxAmount").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_TotalAmount").ToString()) = False Then
                        dr("Total") = Convert.ToDecimal(ds.Tables(0).Rows(i)("Acc_PMD_TotalAmount").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PMD_RoundOff").ToString()) = False Then
                        dr("Roundof") = Convert.ToDecimal(ds.Tables(0).Rows(i)("Acc_PMD_RoundOff").ToString()).ToString("#,##0.00")
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPaidAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPurchase As Integer) As Double
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim dTotal As Double = 0
        Try
            sSql = "" : sSql = "Select Sum(Acc_PMD_TotalAmount) as Total from acc_Purchase_Master_Details where Acc_PMD_MasterID =" & iPurchase & " and Acc_PMD_CompID=" & iCompID & ""
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                dTotal = dr("Total")
            End If
            Return dTotal
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetBillAccountsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal IPurchase As Integer, ByVal dtCoA As DataTable)
        Dim sSql As String = ""
        Dim dc As New DataColumn
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim objPSJEDetails As New ClsPurchaseSalesJEDetails
        Dim dtTab As New DataTable
        Dim dDebit As Double = 0, dCredit As Double = 0
        Try
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)

            sSql = "" : sSql = "Select * from acc_Transactions_Details where ATD_TrType= 8 and ATD_BillID=" & IPurchase & " and ATD_CompID =" & iCompID & " order by Atd_Id"
            dtTab = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtTab.Rows.Count > 0 Then

                For i = 0 To dtTab.Rows.Count - 1
                    dr = dt.NewRow

                    Dim dtDGL As New DataTable
                    Dim DVGLCODE As New DataView(dtCoA)

                    DVGLCODE.RowFilter = "Gl_id=" & dtTab.Rows(i)("ATD_GL").ToString()
                    dtDGL = DVGLCODE.ToTable

                    If dtDGL.Rows(0)("gl_glCode").ToString() <> "" Then
                        dr("GLCode") = dtDGL.Rows(0)("gl_glCode").ToString() & " - " & dtDGL.Rows(0)("gl_Desc").ToString()
                    End If


                    Dim dtDGL1 As New DataTable
                    Dim DVGLCODE1 As New DataView(dtCoA)

                    DVGLCODE1.RowFilter = "Gl_id=" & dtTab.Rows(i)("ATD_SubGL").ToString()
                    dtDGL1 = DVGLCODE1.ToTable

                    If dtDGL1.Rows.Count > 0 Then
                        If dtDGL1.Rows(0)("gl_glCode").ToString() <> "" Then
                            dr("SubGLCode") = dtDGL1.Rows(0)("gl_glCode").ToString() & " - " & dtDGL1.Rows(0)("gl_Desc").ToString()
                        End If
                    End If

                    dDebit = dDebit + Convert.ToDouble(dtTab.Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                    dCredit = dCredit + Convert.ToDouble(dtTab.Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")
                    dr("Debit") = Convert.ToDouble(dtTab.Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                    dr("Credit") = Convert.ToDouble(dtTab.Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")

                    dt.Rows.Add(dr)
                Next

                dr = dt.NewRow
                dr("GLCode") = "TOTAL"
                dr("Debit") = Convert.ToDouble(dDebit).ToString("#,##0.00")
                dr("Credit") = Convert.ToDouble(dCredit).ToString("#,##0.00")
                dt.Rows.Add(dr)
            End If
            Return dt
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
    Public Function SaveAccCharges(ByVal sNameSpace As String, ByVal objPurchase As clsPurchaseVoucher) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_TRID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_TRID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_TRType", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.C_TRType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_ChargeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeType", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objPurchase.C_ChargeType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPurchase.C_ChargeAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.C_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPurchase.C_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPurchase.C_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.C_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.C_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_Charges_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTRID As Integer, ByVal sTRType As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Delete From ACC_Charges_Master Where C_TRID=" & iTRID & " And C_TRType='" & sTRType & "' And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindChargeData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTRID As Integer, ByVal sTrType As String) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("ChargeID")
            dtTab.Columns.Add("ChargeType")
            dtTab.Columns.Add("ChargeAmount")

            If iTRID > 0 Then
                sSql = "" : sSql = "Select * From ACC_Charges_Master Where C_TRID=" & iTRID & " And C_TRType='" & sTrType & "' And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
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
    Public Function CheckLedgerTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim iMaxID As Integer
        Dim iAccHead, iHeadID, iGLID As Integer
        Dim dt As New DataTable
        Dim iID As Integer

        Dim dOpenDebit As Double : Dim dOpenCredit As Double
        Dim dTransDebit As Double : Dim dTransCredit As Double
        Dim dCloseDebit As Double : Dim dCloseCredit As Double
        Dim dtDetails As New DataTable
        Dim iTrAccHead, iTrHead, iTrGLID As Integer
        Dim dPreviousTransDebit, dTotalTransDebit As Double : Dim dPreviousTransCredit, dTotalTransCredit As Double

        Dim dtValues As New DataTable
        Try

            sSql = "" : sSql = "Select * From Chart_OF_Accounts A 
                                Left Join Acc_Ledger_Masters B On B.ALM_GL = A.GL_ID
                                Where A.gl_Head in (2,3) And A.gl_CompID=" & iCompID & " And A.GL_ID  Not In (Select ALM_GL From Acc_Ledger_Masters Where ALM_CompID=" & iCompID & ")"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iAccHead = dt.Rows(i)("GL_AccHead")
                    iHeadID = dt.Rows(i)("GL_Head")
                    iGLID = dt.Rows(i)("GL_ID")

                    iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ALM_ID)+1,1) from Acc_Ledger_Masters")
                    sSql = "" : sSql = "Insert Into Acc_Ledger_Masters(ALM_ID,ALM_AccountType,ALM_Head,ALM_GL,ALM_OBDebit,ALM_OBCredit,ALM_TrDebit,ALM_TrCredit,ALM_CloseDebit,ALM_CloseCredit,ALM_Year,ALM_CompID,ALM_Createdby,ALM_CreatedOn) "
                    sSql = sSql & " Values(" & iMaxID & "," & iAccHead & "," & iHeadID & "," & iGLID & ",0,0,0,0,0,0," & iYearID & "," & iCompID & "," & iUserID & ",GetDate() ) "
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_TrType=8 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
            If dtDetails.Rows.Count > 0 Then
                For j = 0 To dtDetails.Rows.Count - 1

                    iTrAccHead = dtDetails.Rows(j)("ATD_Head")
                    If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
                        iTrGLID = dtDetails.Rows(j)("ATD_SubGL")
                    Else
                        iTrGLID = dtDetails.Rows(j)("ATD_GL")
                    End If
                    iTrHead = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Gl_Head From Chart_Of_Accounts Where GL_ID=" & iTrGLID & " And Gl_AccHead=" & iTrAccHead & " And GL_CompID=" & iCompID & " ")

                    If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
                        sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                        sSql = sSql & " From Acc_Transactions_Details A"
                        sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_SUBGL"
                        sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_SUBGL"
                        sSql = sSql & " Where A.ATD_TrType=8 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
                        dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Else
                        sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                        sSql = sSql & " From Acc_Transactions_Details A"
                        sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
                        sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
                        sSql = sSql & " Where A.ATD_TrType=8 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_SubGL=0 And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
                        dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    End If

                    sSql = "" : sSql = "Select * From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)

                    If bCheck = True Then
                        iID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select ALM_ID From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " ")

                        If dtValues.Rows.Count > 0 Then

                            If IsDBNull(dtValues.Rows(0)("Opn_DebitAmt")) = False Then
                                dOpenDebit = dtValues.Rows(0)("Opn_DebitAmt")
                            Else
                                dOpenDebit = 0
                            End If
                            If IsDBNull(dtValues.Rows(0)("Opn_CreditAmount")) = False Then
                                dOpenCredit = dtValues.Rows(0)("Opn_CreditAmount")
                            Else
                                dOpenCredit = 0
                            End If

                            dTransDebit = dtValues.Rows(0)("ATD_Debit")
                            dTransCredit = dtValues.Rows(0)("ATD_Credit")

                            dPreviousTransDebit = dtValues.Rows(0)("ALM_TrDebit")
                            dTotalTransDebit = dPreviousTransDebit + dTransDebit

                            dPreviousTransCredit = dtValues.Rows(0)("ALM_TrCredit")
                            dTotalTransCredit = dPreviousTransCredit + dTransCredit

                            dCloseDebit = dOpenDebit + dTotalTransDebit
                            dCloseCredit = dOpenCredit + dTotalTransCredit

                            sSql = "" : sSql = "Update Acc_Ledger_Masters Set ALM_OBDebit=" & dOpenDebit & ",ALM_OBCredit=" & dOpenCredit & ",ALM_TrDebit=" & dTotalTransDebit & ",ALM_TrCredit=" & dTotalTransCredit & ",ALM_CloseDebit=" & dCloseDebit & ",ALM_CloseCredit=" & dCloseCredit & " "
                            sSql = sSql & " Where ALM_ID =" & iID & " And ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
                            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                            dOpenDebit = 0 : dOpenCredit = 0 : dTransDebit = 0 : dTransCredit = 0 : dPreviousTransDebit = 0 : dTotalTransDebit = 0
                            dPreviousTransCredit = 0 : dTotalTransCredit = 0 : dCloseDebit = 0 : dCloseCredit = 0
                        End If
                    End If

                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplierID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From CustomerSupplierMaster Where CSM_ID=" & iSupplierID & " And CSM_CompID=" & iCompID & " "
            GetCustomerDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCustomerDetails
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
    Public Function LoadBranches(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where Org_Parent=1 And org_CompID=" & iCompID & ")))"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBranchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_Name=" & iBranchID & " And CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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
    Public Function SaveAcceptedDetails(ByVal sNameSpace As String, ByVal objPur As Purchase) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPur.PD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPur.PD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_HSNCode", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objPur.PD_HSNCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Commodity", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objPur.PD_Commodity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Goods", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objPur.PD_Goods
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Unit", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objPur.PD_Unit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Rate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Quantity", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_ChargePerItem", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_ChargePerItem
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_RateAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_RateAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Discount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_Discount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_DiscountAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_DiscountAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Amount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_GSTRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_GSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_GSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_SGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_SGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_SGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_SGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_CGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_CGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_CGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_CGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_IGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_IGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_IGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_IGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_FinalTotal", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPur.PD_FinalTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPur.PD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPur.PD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPur.PD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPur.PD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPur.PD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_FETotalAmt", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPur.PD_FETotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Currency", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPur.PD_Currency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPur.PD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPur.PD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_Purchase_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindItemsData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTRID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("GoodsID")
            dtTab.Columns.Add("UnitID")

            dtTab.Columns.Add("HSNCode")
            dtTab.Columns.Add("Commodity")
            dtTab.Columns.Add("Goods")

            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("Qty")

            dtTab.Columns.Add("RateAMt")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmt")

            dtTab.Columns.Add("Charge")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("GST")
            dtTab.Columns.Add("GSTAmt")
            dtTab.Columns.Add("NetAmount")
            dtTab.Columns.Add("CCurrency")
            dtTab.Columns.Add("FETotalAmount")
            If iTRID > 0 Then
                sSql = "" : sSql = "Select * From ACC_Purchase_Details Where PD_MasterID=" & iTRID & " And PD_CompID =" & iCompID & " And PD_YearID =" & iYearID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("PD_Commodity")
                    dRow("GoodsID") = dt.Rows(i)("PD_Goods")
                    dRow("UnitID") = dt.Rows(i)("PD_Unit")

                    dRow("HSNCode") = dt.Rows(i)("PD_HSNCode")
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID=" & dt.Rows(i)("PD_Commodity") & " And INV_Parent=0 And INV_CompID=" & iCompID & " ")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID=" & dt.Rows(i)("PD_Goods") & " And INV_Parent=" & dt.Rows(i)("PD_Commodity") & " And INV_CompID=" & iCompID & " ")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_master Where Mas_ID=" & dt.Rows(i)("PD_Unit") & " And MAS_CompID=" & iCompID & " And MAS_DelFlag='A' And MAS_Master in(Select MAS_ID From Acc_Master_Type where Mas_Type='Unit of Measurement') ")

                    dRow("MRP") = dt.Rows(i)("PD_Rate")
                    dRow("Qty") = dt.Rows(i)("PD_Quantity")
                    dRow("RateAMt") = dt.Rows(i)("PD_RateAmount")
                    dRow("Discount") = dt.Rows(i)("PD_Discount")
                    dRow("DiscountAmt") = dt.Rows(i)("PD_DiscountAmount")
                    dRow("Charge") = dt.Rows(i)("PD_ChargePerItem")
                    dRow("Total") = dt.Rows(i)("PD_Amount")
                    dRow("GST") = dt.Rows(i)("PD_GSTRate")
                    dRow("GSTAmt") = dt.Rows(i)("PD_GSTAmount")
                    dRow("NetAmount") = dt.Rows(i)("PD_FinalTotal")
                    If IsDBNull(dt.Rows(i)("PD_Currency")) = False Then
                        dRow("CCurrency") = objDBL.SQLExecuteScalar(sNameSpace, "Select (CUR_CODE + ' [' + CUR_CountryName + ']') as OperateOn from Currency_master where CUR_ID=" & dt.Rows(i)("PD_Currency") & "")
                    End If
                    dRow("FETotalAmount") = dt.Rows(i)("PD_FETotalAmt")

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
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
            'sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & iPartyID & " And CSM_CompID=" & iCompID & " and CSM_Delflag='A' "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            GetPartySubGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPartySubGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccountDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String, ByVal sColumn As String) As Integer
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
    Public Function GetPartySubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & iPartyID & " And CSM_CompID=" & iCompID & " and CSM_Delflag='A' "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ApproveTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPTrID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_Purchase_Master Set Acc_Purchase_Status='S' Where Acc_Purchase_ID=" & iPTrID & " And Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPTrID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete ACC_Purchase_Details where PD_MasterID =" & iPTrID & " And PD_YearID=" & iYearID & " And PD_CompID =" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
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
                sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 8 and "
                sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_YearID=" & iYearID & " And A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "
            ElseIf sExiJV.StartsWith("S") Then
                sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
                sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
                sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 9 and "
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
    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescritionStart(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_CompID =" & iCompID & " And Inv_Code <> '' and Inv_Parent <> 0"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_Parent = " & iCommodity & " and Inv_CompID =" & iCompID & ""
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTRateFromHSNTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_GSTRate From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTRateFromHSNTable = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTRateFromHSNTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUnitOFMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtNew As New DataTable
        Try
            dtNew.Columns.Add("Mas_ID")
            dtNew.Columns.Add("Mas_Desc")

            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iInvID & " And InvH_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_Unit")
                dRow("Mas_Desc") = objDBL.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_Delflag='A' And Mas_ID='" & dt.Rows(0)("InvH_Unit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)

                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_AlterUnit")
                dRow("Mas_Desc") = objDBL.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_Delflag='A' And Mas_ID='" & dt.Rows(0)("InvH_AlterUnit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)
            End If
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getGSTRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String, ByVal sGSTNCategoryDesc As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GC_GSTRate From GSTCategory_Table Where GC_CompanyType='" & sCompanyType & "' And GC_GSTcategory= '" & sGSTNCategoryDesc & "' Order By GC_ID Desc "
            getGSTRate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return getGSTRate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_ID From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTID = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBrandValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "select Inv_Parent from inventory_master where Inv_ID=" & InvID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAlterNatePiceValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvID As String) As Decimal
        Dim sSql As String = ""
        Try
            sSql = "Select INVH_PerPieces From Inventory_master_History Where InvH_INV_ID ='" & InvID & "' And INVH_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUnitsValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvID As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select InvH_Unit From Inventory_master_History Where InvH_INV_ID ='" & InvID & "' And INVH_CompID=" & iCompID & " "
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTRatesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINV_ID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_CHST from GST_Rates where GST_ItemID =" & iINV_ID & " and GST_CompID =" & iCompID & " Order By GST_ID Desc"
            Return objDBL.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetINVH_Unit(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iItemID As Integer) As Integer
        Dim iUnit As String = ""
        Dim sSql As String = ""
        Try
            sSql = "Select INVH_AlterUnit From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " and INVH_CompID =" & iCompID & ""
            iUnit = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iUnit
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Acc_Purchase_Status From Acc_Purchase_Master Where Acc_Purchase_ID = " & iMasId & " and Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
            GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSavedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTRID As Integer, ByVal iCommodity As Integer, ByVal iGoodsID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iTRID > 0 And iCommodity > 0 And iGoodsID > 0 Then
                sSql = "" : sSql = "Select * From ACC_Purchase_Details Where PD_MasterID=" & iTRID & " And PD_Commodity=" & iCommodity & " And PD_Goods=" & iGoodsID & " And PD_CompID =" & iCompID & " And PD_YearID =" & iYearID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyGSTNRegNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From MST_Customer_Master Where CUST_ID=" & iCompID & " "
            GetCompanyGSTNRegNo = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCompanyGSTNRegNo
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
    Public Function getBranchFromPO(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPodID As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select ACC_Purchase_BranchID From Acc_Purchase_Master where Acc_Purchase_TransactionNo='" & sPodID & "' and Acc_Purchase_CompID=" & iCompID & ""
            getBranchFromPO = objDBL.SQLExecuteScalar(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckDetailsofBranchState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPodID As String) As String
        Dim sSql As String = ""
        Dim iPOMBranchID As Integer : Dim iCompBrnchID As Integer
        Try
            sSql = "Select ACC_Purchase_BranchID From Acc_Purchase_Master where Acc_Purchase_TransactionNo='" & sPodID & "' and Acc_Purchase_CompID=" & iCompID & ""
            iPOMBranchID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select CUSTB_STATE from MST_CUSTOMER_MASTER_Branch where CUSTB_Name='" & iPOMBranchID & "' and CUSTB_CompID=" & iCompID & ""
            iCompBrnchID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
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
    Public Function getOrgParent(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iNodeID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Parent From Sad_Org_Structure Where Org_Node=" & iNodeID & " And Org_CompID=" & iCompID & " "
            getOrgParent = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return getOrgParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDefaultBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Node From Sad_Org_Structure Where Org_LevelCode=4 And Org_Default=1 And Org_CompID=" & iCompID & " "
            GetDefaultBranch = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDefaultBranch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFERates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer, ByVal dTotal As Double) As String
        Dim sSql As String = "", sToday As String = ""
        Dim dValue As Double = 0, dFETot As Double
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_TTBuy from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            dValue = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            dFETot = dValue * dTotal
            Return Math.Round(dFETot, 2, MidpointRounding.AwayFromZero)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFEID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As Integer
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_PKID from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
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
    Public Function GetCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTRID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SElect PD_Currency From ACC_Purchase_Details Where PD_MasterID=" & iTRID & " And PD_CompID =" & iCompID & " And PD_YearID =" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveStockLedger(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sIPAddress As String, ByVal iUserID As Integer, ByVal dt As DataTable, ByVal OrderNo As String, ByVal InwardNo As String, ByVal iBranch As Integer) As Integer
        Dim sSql As String = ""
        Dim iMaxid As Integer, iQTY As Integer
        Try
            For i = 0 To dt.Rows.Count - 1
                If (objDBL.SQLCheckForRecord(sNameSpace, "Select * from Stock_Ledger where SL_historyId=0 And SL_ItemID = " & dt.Rows(i)("PD_Goods") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " And SL_OrderID=" & OrderNo & " and SL_GINID=" & InwardNo & " And SL_Branch=" & iBranch & " ")) Then
                    objDBL.SQLExecuteNonQuery(sNameSpace, "Update Stock_Ledger Set SL_Operation='U',SL_IPAddress='" & sIPAddress & "',SL_ClosingBalanceQty=SL_ClosingBalanceQty+" & dt.Rows(i)("PD_Quantity") & ", SL_PurchaseQty = SL_PurchaseQty + " & dt.Rows(i)("PD_Quantity") & " where  SL_historyId=0 And SL_ItemID = " & dt.Rows(i)("PD_Goods") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " and SL_GINID=" & InwardNo & " And SL_Branch=" & iBranch & " ")
                Else
                    iMaxid = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(SL_ID) + 1, 1) from Stock_Ledger")
                    sSql = "Insert Into Stock_Ledger (SL_ID,SL_Commodity,SL_Date,SL_ItemID,sl_PurchaseQty,SL_ClosingBalanceQty,SL_CompID,SL_YearID,SL_CrBy,SL_CrOn,SL_historyId,SL_Operation,SL_IPAddress,SL_Vat,SL_Exciese,SL_Cst,SL_OrderID,SL_SaleQnty,PurchaseRate,SL_OpeningBalanceQty,SL_GINID,SL_Branch)"
                    sSql = sSql & "Values(" & iMaxid & "," & dt.Rows(i)("PD_Commodity") & ", GetDate()," & dt.Rows(i)("PD_Goods") & "," & dt.Rows(i)("PD_Quantity") & "," & dt.Rows(i)("PD_Quantity") & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate(),0,'C','" & sIPAddress & "','0','0','0','" & OrderNo & "',0,'" & dt.Rows(i)("PD_Rate") & "',0," & InwardNo & "," & iBranch & ")"
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Acc_Purchase_Details Where PD_MasterID = " & iMasterID & " And PD_CompID = " & iCompID & " And PD_YearID = " & iYearID & " "
            GetItemDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetItemDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function WriteGLTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasID As Integer, ByVal iyearID As Integer, ByVal iUserID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=8 And ATD_BillID=" & iMasID & " And ATD_YearID=" & iyearID & " And ATD_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ATD_ID)+1,1) from GL_Transactions_Details")
                    sSql = "" : sSql = "Insert Into GL_Transactions_Details (ATD_ID,ATD_TransactionDate,ATD_TrType,ATD_BillId,ATD_PaymentType,ATD_Head,ATD_GL,ATD_SubGL,ATD_DbOrCr,ATD_Debit,ATD_Credit,ATD_CreatedBy,ATD_CreatedOn,ATD_ApprovedBy,ATD_ApprovedOn,ATD_Status,ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress,ATD_ZoneID,ATD_RegionID,ATD_AreaID,ATD_BranchID)"
                    sSql = sSql & "Values(" & iMaxID & ",'" & objGen.FormatDtForRDBMS(dt.Rows(i)("ATD_TransactionDate"), "CT") & "'," & dt.Rows(i)("ATD_TrType") & "," & dt.Rows(i)("ATD_BillId") & "," & dt.Rows(i)("ATD_PaymentType") & "," & dt.Rows(i)("ATD_Head") & "," & dt.Rows(i)("ATD_GL") & "," & dt.Rows(i)("ATD_SubGL") & "," & dt.Rows(i)("ATD_DbOrCr") & "," & dt.Rows(i)("ATD_Debit") & "," & dt.Rows(i)("ATD_Credit") & "," & iUserID & ",'" & objGen.FormatDtForRDBMS(dt.Rows(i)("ATD_CreatedOn"), "CT") & "'," & iUserID & ",GetDate(),'" & dt.Rows(i)("ATD_Status") & "'," & dt.Rows(i)("ATD_YearID") & "," & dt.Rows(i)("ATD_CompID") & ",'" & dt.Rows(i)("ATD_Operation") & "','" & dt.Rows(i)("ATD_IPAddress") & "'," & dt.Rows(i)("ATD_ZoneID") & "," & dt.Rows(i)("ATD_RegionID") & "," & dt.Rows(i)("ATD_AreaID") & "," & dt.Rows(i)("ATD_BranchID") & ")"
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
