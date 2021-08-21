Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsOralOrder
    Dim objDB As New DBHelper
    Dim objClsFasgnrl As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions
    Dim objGen As New clsFASGeneral
    Dim objFasGnrl As New clsFASGeneral

    Dim objFas As New clsFASGeneral
    Private POM_ID As Integer
    Private POM_OrderDate As Date
    Private POM_OrderNo As String
    Private POM_InwardNo As String
    Private POM_Status As String
    Private POM_Supplier As Integer
    Private POM_DSchdule As Integer
    Private POM_ModeOfShipping As Integer
    Private POM_MethodofPayment As Integer
    Private POM_Paymentterms As Integer
    Private POM_CreatedBy As Integer
    Private POM_UpdatedBy As Integer
    Private POM_ApporvedBy As Integer
    Private POM_YearID As Integer
    Private POD_ID As Integer
    Private POD_MasterID As Integer
    Private POD_Commodity As Integer
    Private POD_DescriptionID As Integer
    Private POD_HistoryID As Integer
    Private POD_Unit As Integer
    Private POD_Rate As String
    Private POM_CSTCtgry As Integer
    Private POM_SaleType As Integer
    Private POD_Quantity As String
    Private POD_RateAmount As String
    Private POD_Discount As String
    Private POD_DiscountAmount As String
    Private POD_Excise As String
    Private POD_ExciseAmount As String
    Private POD_Frieght As String
    Private POD_FrieghtAmount As String
    Private POD_VAT As String
    Private POD_VATAmount As String
    Private POD_CST As String
    Private POD_CSTAmount As String
    Private POD_RequiredDate As Date
    Private POD_TotalAmount As String
    Private POD_FETotalAmt As String
    Private POD_CompID As Integer

    Private iPOD_CreatedBy As Integer
    Private iPOD_UpdatedBy As Integer

    Private sPOM_DocRef As String
    Private iPOD_Rejected As Integer
    Private iPOD_Accepted As Integer
    Private iPOD_ReceivedQty As Integer
    Private iPOD_OrderedQty As Integer
    Private POD_IPAddress As String
    Private BatchNumber As String
    Private POM_DEliveryChlnNo As String
    Private POM_DcNo As String
    Private OralOrPO As String
    Private POM_InvoiceRef As String
    Private POM_ESugam As String
    Private POM_ExpryDate As Date
    Private POM_ManfctreDate As Date
    Private POM_BatchNumber As String
    Private POM_InvoiceDate As Date
    Private POD_GST_ID As Integer
    Private POD_GSTRate As Double
    Private POD_GSTAmount As Double
    Private POD_SGST As Double
    Private POD_SGSTAmount As Double
    Private POD_CGST As Double
    Private POD_CGSTAmount As Double
    Private POD_IGST As Double
    Private POD_IGSTAmount As Double


    Public iPOD_Currency As Integer
    Public iPOD_CurrencyAmt As Integer
    Public sPOD_CurrencyTime As String


    Private iPOM_ZoneID As Integer
    Private iPOM_RegionID As Integer
    Private iPOM_AreaID As Integer
    Private iPOM_BranchID As Integer

    Private iPOM_TrType As Integer
    Private sPOM_CompanyAddress As String
    Private sPOM_CompanyGSTNRegNo As String
    Private sPOM_BillingAddress As String
    Private sPOM_BillingGSTNRegNo As String
    Private sPOM_DeliveryAddress As String
    Private sPOM_DeliveryGSTNRegNo As String
    Private sPOM_DeliveryFrom As String
    Private sPOM_DeliveryFromGSTNRegNo As String
    Private sPOM_PurchaseStatus As String
    Private iPOM_CompanyType As Integer
    Private iPOM_GSTNCategory As Integer

    Private iC_ID As Integer
    Private iC_OrderID As Integer
    Private iC_AllocatedID As Integer
    Private iC_DispatchID As Integer
    Private iC_POrderID As Integer
    Private iC_PGinID As Integer
    Private iC_PInvoiceDocRef As Integer
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
    Private dC_CreatedOn As Date
    Private iC_UpdatedBy As Integer
    Private dC_UpdatedOn As Date
    Private sC_Operation As String
    Private sC_IPAddress As String

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
    Private Acc_JE_PendingAmount As Decimal
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

    Private iPOM_BatchNo As Integer
    Private iPOM_BaseName As Integer

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
    Public Property POM_BatchNo() As Integer
        Get
            Return (iPOM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iPOM_BatchNo = Value
        End Set
    End Property
    Public Property POM_BaseName() As Integer
        Get
            Return (iPOM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iPOM_BaseName = Value
        End Set
    End Property
    Public Property dAcc_JE_PendingAmount() As Decimal
        Get
            Return (Acc_JE_PendingAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_PendingAmount = Value
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
    Public Property C_POrderID() As Integer
        Get
            Return (iC_POrderID)
        End Get
        Set(ByVal Value As Integer)
            iC_POrderID = Value
        End Set
    End Property
    Public Property C_PGinID() As Integer
        Get
            Return (iC_PGinID)
        End Get
        Set(ByVal Value As Integer)
            iC_PGinID = Value
        End Set
    End Property
    Public Property C_PInvoiceDocRef() As Integer
        Get
            Return (iC_PInvoiceDocRef)
        End Get
        Set(ByVal Value As Integer)
            iC_PInvoiceDocRef = Value
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
            Return (dC_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dC_CreatedOn = Value
        End Set
    End Property
    Public Property C_UpdatedBy() As Integer
        Get
            Return (iC_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iC_UpdatedBy = Value
        End Set
    End Property
    Public Property C_UpdatedOn() As DateTime
        Get
            Return (dC_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dC_UpdatedOn = Value
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
    Public Property POM_TrType() As Integer
        Get
            Return (iPOM_TrType)
        End Get
        Set(ByVal Value As Integer)
            iPOM_TrType = Value
        End Set
    End Property
    Public Property POM_CompanyAddress() As String
        Get
            Return (sPOM_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sPOM_CompanyAddress = Value
        End Set
    End Property
    Public Property POM_CompanyGSTNRegNo() As String
        Get
            Return (sPOM_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPOM_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property POM_BillingAddress() As String
        Get
            Return (sPOM_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sPOM_BillingAddress = Value
        End Set
    End Property
    Public Property POM_BillingGSTNRegNo() As String
        Get
            Return (sPOM_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPOM_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property POM_DeliveryFrom() As String
        Get
            Return (sPOM_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sPOM_DeliveryFrom = Value
        End Set
    End Property
    Public Property POM_DeliveryFromGSTNRegNo() As String
        Get
            Return (sPOM_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPOM_DeliveryFromGSTNRegNo = Value
        End Set
    End Property

    Public Property POM_DeliveryAddress() As String
        Get
            Return (sPOM_DeliveryAddress)
        End Get
        Set(ByVal Value As String)
            sPOM_DeliveryAddress = Value
        End Set
    End Property
    Public Property POM_DeliveryGSTNRegNo() As String
        Get
            Return (sPOM_DeliveryGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPOM_DeliveryGSTNRegNo = Value
        End Set
    End Property
    Public Property POM_PurchaseStatus() As String
        Get
            Return (sPOM_PurchaseStatus)
        End Get
        Set(ByVal Value As String)
            sPOM_PurchaseStatus = Value
        End Set
    End Property
    Public Property POM_CompanyType() As Integer
        Get
            Return (iPOM_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iPOM_CompanyType = Value
        End Set
    End Property
    Public Property POM_GSTNCategory() As Integer
        Get
            Return (iPOM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iPOM_GSTNCategory = Value
        End Set
    End Property
    Public Property POM_ZoneID() As Integer
        Get
            Return (iPOM_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            iPOM_ZoneID = Value
        End Set
    End Property
    Public Property POM_RegionID() As Integer
        Get
            Return (iPOM_RegionID)
        End Get
        Set(ByVal Value As Integer)
            iPOM_RegionID = Value
        End Set
    End Property
    Public Property POM_AreaID() As Integer
        Get
            Return (iPOM_AreaID)
        End Get
        Set(ByVal Value As Integer)
            iPOM_AreaID = Value
        End Set
    End Property
    Public Property POM_BranchID() As Integer
        Get
            Return (iPOM_BranchID)
        End Get
        Set(ByVal Value As Integer)
            iPOM_BranchID = Value
        End Set
    End Property
    Public Property iPOD_GST_ID() As Integer
        Get
            Return (POD_GST_ID)
        End Get
        Set(ByVal Value As Integer)
            POD_GST_ID = Value
        End Set
    End Property
    Public Property dPOD_GSTRate() As Double
        Get
            Return (POD_GSTRate)
        End Get
        Set(ByVal Value As Double)
            POD_GSTRate = Value
        End Set
    End Property
    Public Property dPOD_GSTAmount() As Double
        Get
            Return (POD_GSTAmount)
        End Get
        Set(ByVal Value As Double)
            POD_GSTAmount = Value
        End Set
    End Property
    Public Property dPOD_CGST() As Double
        Get
            Return (POD_CGST)
        End Get
        Set(ByVal Value As Double)
            POD_CGST = Value
        End Set
    End Property
    Public Property dPOD_CGSTAmount() As Double
        Get
            Return (POD_CGSTAmount)
        End Get
        Set(ByVal Value As Double)
            POD_CGSTAmount = Value
        End Set
    End Property
    Public Property dPOD_IGST() As Double
        Get
            Return (POD_IGST)
        End Get
        Set(ByVal Value As Double)
            POD_IGST = Value
        End Set
    End Property
    Public Property dPOD_IGSTAmount() As Double
        Get
            Return (POD_IGSTAmount)
        End Get
        Set(ByVal Value As Double)
            POD_IGSTAmount = Value
        End Set
    End Property
    Public Property dPOD_SGST() As Double
        Get
            Return (POD_SGST)
        End Get
        Set(ByVal Value As Double)
            POD_SGST = Value
        End Set
    End Property
    Public Property dPOD_SGSTAmount() As Double
        Get
            Return (POD_SGSTAmount)
        End Get
        Set(ByVal Value As Double)
            POD_SGSTAmount = Value
        End Set
    End Property
    Public Property sPOM_DcNo() As String
        Get
            Return (POM_DcNo)
        End Get
        Set(ByVal Value As String)
            POM_DcNo = Value
        End Set
    End Property

    Public Property sPOM_DEliveryChlnNo() As String
        Get
            Return (POM_DEliveryChlnNo)
        End Get
        Set(ByVal Value As String)
            POM_DEliveryChlnNo = Value
        End Set
    End Property
    Public Property sPOD_IPAddress() As String
        Get
            Return (POD_IPAddress)
        End Get
        Set(ByVal Value As String)
            POD_IPAddress = Value
        End Set
    End Property
    Public Property POD_Accepted() As Integer
        Get
            Return (iPOD_Accepted)
        End Get
        Set(ByVal Value As Integer)
            iPOD_Accepted = Value
        End Set
    End Property
    Public Property POD_Rejected() As Integer
        Get
            Return (iPOD_Rejected)
        End Get
        Set(ByVal Value As Integer)
            iPOD_Rejected = Value
        End Set
    End Property
    Public Property POM_DocRef() As String
        Get
            Return (sPOM_DocRef)
        End Get
        Set(ByVal Value As String)
            sPOM_DocRef = Value
        End Set
    End Property
    Public Property sPOD_RateAmount() As String
        Get
            Return (POD_RateAmount)
        End Get
        Set(ByVal Value As String)
            POD_RateAmount = Value
        End Set
    End Property
    Public Property sPOD_TotalAmount() As String
        Get
            Return (POD_TotalAmount)
        End Get
        Set(ByVal Value As String)
            POD_TotalAmount = Value
        End Set
    End Property

    Public Property sPOD_FETotalAmt() As String
        Get
            Return (POD_FETotalAmt)
        End Get
        Set(ByVal Value As String)
            POD_FETotalAmt = Value
        End Set
    End Property
    Public Property sOralOrPO() As String
        Get
            Return (OralOrPO)
        End Get
        Set(ByVal Value As String)
            OralOrPO = Value
        End Set
    End Property
    Public Property dPOD_RequiredDate() As DateTime
        Get
            Return (POD_RequiredDate)
        End Get
        Set(ByVal Value As DateTime)
            POD_RequiredDate = Value
        End Set
    End Property
    Public Property sPOD_CSTAmount() As String
        Get
            Return (POD_CSTAmount)
        End Get
        Set(ByVal Value As String)
            POD_CSTAmount = Value
        End Set
    End Property
    Public Property sPOD_CST() As String
        Get
            Return (POD_CST)
        End Get
        Set(ByVal Value As String)
            POD_CST = Value
        End Set
    End Property
    Public Property sPOD_VATAmount() As String
        Get
            Return (POD_VATAmount)
        End Get
        Set(ByVal Value As String)
            POD_VATAmount = Value
        End Set
    End Property
    Public Property sPOD_VAT() As String
        Get
            Return (POD_VAT)
        End Get
        Set(ByVal Value As String)
            POD_VAT = Value
        End Set
    End Property
    Public Property sPOD_ExciseAmount() As String
        Get
            Return (POD_ExciseAmount)
        End Get
        Set(ByVal Value As String)
            POD_ExciseAmount = Value
        End Set
    End Property
    Public Property sPOD_Excise() As String
        Get
            Return (POD_Excise)
        End Get
        Set(ByVal Value As String)
            POD_Excise = Value
        End Set
    End Property

    Public Property sPOD_Frieght() As String
        Get
            Return (POD_Frieght)
        End Get
        Set(ByVal Value As String)
            POD_Frieght = Value
        End Set
    End Property

    Public Property sPOD_FrieghtAmount() As String
        Get
            Return (POD_FrieghtAmount)
        End Get
        Set(ByVal Value As String)
            POD_FrieghtAmount = Value
        End Set
    End Property



    Public Property POD_ReceivedQty() As Integer
        Get
            Return (iPOD_ReceivedQty)
        End Get
        Set(ByVal Value As Integer)
            iPOD_ReceivedQty = Value
        End Set
    End Property

    Public Property POD_OrderedQty() As Integer
        Get
            Return (iPOD_OrderedQty)
        End Get
        Set(ByVal Value As Integer)
            iPOD_OrderedQty = Value
        End Set
    End Property
    Public Property sPOD_DiscountAmount() As String
        Get
            Return (POD_DiscountAmount)
        End Get
        Set(ByVal Value As String)
            POD_DiscountAmount = Value
        End Set
    End Property

    Public Property sPOD_Discount() As String
        Get
            Return (POD_Discount)
        End Get
        Set(ByVal Value As String)
            POD_Discount = Value
        End Set
    End Property
    Public Property sPOD_Quantity() As String
        Get
            Return (POD_Quantity)
        End Get
        Set(ByVal Value As String)
            POD_Quantity = Value
        End Set
    End Property

    Public Property sPOD_Rate() As String
        Get
            Return (POD_Rate)
        End Get
        Set(ByVal Value As String)
            POD_Rate = Value
        End Set
    End Property
    Public Property iPOD_Unit() As Integer
        Get
            Return (POD_Unit)
        End Get
        Set(ByVal Value As Integer)
            POD_Unit = Value
        End Set
    End Property
    Public Property iPOD_HistoryID() As Integer
        Get
            Return (POD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            POD_HistoryID = Value
        End Set
    End Property
    Public Property iPOD_DescriptionID() As Integer
        Get
            Return (POD_DescriptionID)
        End Get
        Set(ByVal Value As Integer)
            POD_DescriptionID = Value
        End Set
    End Property

    Public Property iPOD_Commodity() As Integer
        Get
            Return (POD_Commodity)
        End Get
        Set(ByVal Value As Integer)
            POD_Commodity = Value
        End Set
    End Property
    Public Property iPOD_MasterID() As Integer
        Get
            Return (POD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            POD_MasterID = Value
        End Set
    End Property

    Public Property iPOD_ID() As Integer
        Get
            Return (POD_ID)
        End Get
        Set(ByVal Value As Integer)
            POD_ID = Value
        End Set
    End Property

    Public Property iPOM_YearID() As Integer
        Get
            Return (POM_YearID)
        End Get
        Set(ByVal Value As Integer)
            POM_YearID = Value
        End Set
    End Property

    Public Property iPOM_SaleType() As Integer
        Get
            Return (POM_SaleType)
        End Get
        Set(ByVal Value As Integer)
            POM_SaleType = Value
        End Set
    End Property

    Public Property iPOM_iCSTCtgry() As Integer
        Get
            Return (POM_CSTCtgry)
        End Get
        Set(ByVal Value As Integer)
            POM_CSTCtgry = Value
        End Set
    End Property
    Public Property iPOM_ApporvedBy() As Integer
        Get
            Return (POM_ApporvedBy)
        End Get
        Set(ByVal Value As Integer)
            POM_ApporvedBy = Value
        End Set
    End Property
    Public Property iPOM_CreatedBy() As Integer
        Get
            Return (POM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            POM_CreatedBy = Value
        End Set
    End Property
    Public Property iPOM_UpdatedBy() As Integer
        Get
            Return (POM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            POM_UpdatedBy = Value
        End Set
    End Property
    Public Property POD_CreatedBy() As Integer
        Get
            Return (iPOD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPOD_CreatedBy = Value
        End Set
    End Property
    Public Property POD_UpdatedBy() As Integer
        Get
            Return (iPOD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPOD_UpdatedBy = Value
        End Set
    End Property



    Public Property iPOM_MethodofPayment() As Integer
        Get
            Return (POM_MethodofPayment)
        End Get
        Set(ByVal Value As Integer)
            POM_MethodofPayment = Value
        End Set
    End Property

    Public Property iPOM_DSchdule() As Integer
        Get
            Return (POM_DSchdule)
        End Get
        Set(ByVal Value As Integer)
            POM_DSchdule = Value
        End Set
    End Property
    Public Property iPOM_Paymentterms() As Integer
        Get
            Return (POM_Paymentterms)
        End Get
        Set(ByVal Value As Integer)
            POM_Paymentterms = Value
        End Set
    End Property


    Public Property iPOM_ModeOfShipping() As Integer
        Get
            Return (POM_ModeOfShipping)
        End Get
        Set(ByVal Value As Integer)
            POM_ModeOfShipping = Value
        End Set
    End Property
    Public Property iPOM_Supplier() As Integer
        Get
            Return (POM_Supplier)
        End Get
        Set(ByVal Value As Integer)
            POM_Supplier = Value
        End Set
    End Property

    Public Property sBatchNumber() As String
        Get
            Return (BatchNumber)
        End Get
        Set(ByVal Value As String)
            BatchNumber = Value
        End Set
    End Property
    Public Property sPOM_OrderNo() As String
        Get
            Return (POM_OrderNo)
        End Get
        Set(ByVal Value As String)
            POM_OrderNo = Value
        End Set
    End Property

    Public Property DPOM_ExpryDate() As Date
        Get
            Return (POM_ExpryDate)
        End Get
        Set(ByVal Value As Date)
            POM_ExpryDate = Value
        End Set
    End Property
    Public Property DPOM_ManfctreDate() As Date
        Get
            Return (POM_ManfctreDate)
        End Get
        Set(ByVal Value As Date)
            POM_ManfctreDate = Value
        End Set
    End Property
    Public Property DPOM_InvoiceDate() As Date
        Get
            Return (POM_InvoiceDate)
        End Get
        Set(ByVal Value As Date)
            POM_InvoiceDate = Value
        End Set
    End Property
    Public Property sPOM_BatchNumber() As String
        Get
            Return (POM_BatchNumber)
        End Get
        Set(ByVal Value As String)
            POM_BatchNumber = Value
        End Set
    End Property

    Public Property sPOM_Status() As String
        Get
            Return (POM_Status)
        End Get
        Set(ByVal Value As String)
            POM_Status = Value
        End Set
    End Property

    Public Property sPOM_InwardNo() As String
        Get
            Return (POM_InwardNo)
        End Get
        Set(ByVal Value As String)
            POM_InwardNo = Value
        End Set
    End Property

    Public Property dPOM_OrderDate() As Date
        Get
            Return (POM_OrderDate)
        End Get
        Set(ByVal Value As Date)
            POM_OrderDate = Value
        End Set
    End Property
    Public Property iPOM_ID() As Integer
        Get
            Return (POM_ID)
        End Get
        Set(ByVal Value As Integer)
            POM_ID = Value
        End Set
    End Property

    Public Property sPOM_InvoiceRef() As String
        Get
            Return (POM_InvoiceRef)
        End Get
        Set(ByVal Value As String)
            POM_InvoiceRef = Value
        End Set
    End Property
    Public Property sPOM_ESugam() As String
        Get
            Return (POM_ESugam)
        End Get
        Set(ByVal Value As String)
            POM_ESugam = Value
        End Set
    End Property



    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_Parent = " & iCommodity & " and Inv_CompID =" & iCompID & ""
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplierID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From CustomerSupplierMaster Where CSM_ID=" & iSupplierID & " And CSM_CompID=" & iCompID & " "
            GetCustomerDetails = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCustomerDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 2 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGSTCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select GC_ID,GC_GSTCategory From GSTCategory_Table Where GC_CompanyType='" & sCompanyType & "' And GC_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBranchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_ID=" & iBranchID & " And CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_Name=" & iBranchID & " And CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyGSTNRegNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From MST_Customer_Master Where CUST_ID=" & iCompID & " "
            GetCompanyGSTNRegNo = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCompanyGSTNRegNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranches(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where Org_Parent=1 And org_CompID=" & iCompID & ")))"
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChargeType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=24 And Mas_DelFlag='A' "
            Return objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPrintFlagValue(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "select PS_RptType from print_settings where ps_status='P'"
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RemoveChargeDublicate(ByVal dt As DataTable) As DataTable
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
    'Public Function LoadAllPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal icat As Integer, ByVal iorder As Integer, ByVal isuuplier As Integer, ByVal InvoiceId As Integer, ByVal iCommodity As Integer, ByVal iItem As Integer, ByVal ivat As String, ByVal iExcise As String, ByVal iCst As String, ByVal iDiscount As String, ByVal iFromDt As String, ByVal iToDt As String)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim dtDetails As New DataTable
    '    Dim flag2 As String = "" : Dim flag3 As String = "" : Dim OrderNo As String = 0 : Dim Orderdate As String = "" : Dim GinNo As String = 0 : Dim Supplier As String = 0 : Dim Description As String = 0 : Dim Commodity As String = 0
    '    Dim TotalAmt, BasicAmount As Double
    '    Dim pending As Double
    '    Try
    '        dt.Columns.Add("SlNo")
    '        dt.Columns.Add("Commodity")
    '        dt.Columns.Add("Description")
    '        dt.Columns.Add("PV_DocRefNo")
    '        dt.Columns.Add("OrderNo")
    '        dt.Columns.Add("Orderdate")
    '        dt.Columns.Add("GinNo")
    '        dt.Columns.Add("Supplier")
    '        dt.Columns.Add("AcceptedQntity")
    '        dt.Columns.Add("Orderedqty")
    '        dt.Columns.Add("ReceivedQnt")
    '        dt.Columns.Add("Excess")
    '        dt.Columns.Add("RejectedQty")
    '        dt.Columns.Add("PendingQty")
    '        dt.Columns.Add("Rate")
    '        dt.Columns.Add("VAT")
    '        dt.Columns.Add("VATAmt")
    '        dt.Columns.Add("CST")
    '        dt.Columns.Add("CSTAmt")
    '        dt.Columns.Add("Exise")
    '        dt.Columns.Add("ExiseAmt")
    '        dt.Columns.Add("Discount")
    '        dt.Columns.Add("DiscountAmt")
    '        dt.Columns.Add("TotalAmount")
    '        dt.Columns.Add("InvoiceDate")
    '        dt.Columns.Add("BasicAmount")
    '        sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_DocRefNo,PV_BillNo,PGM_InvoiceDate,"
    '        sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_MRP as Rate,b.PIA_Status,"
    '        sSql = sSql & " b.PIA_CompID,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Code,c.Inv_Color,c.Inv_Size,"
    '        sSql = sSql & " d.Inv_Description Commodity,"
    '        sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_RateAmount,g.POD_Discount as Discount,g.POD_DiscountAmount as DiscountAmount,g.POD_Excise as Excise,g.POD_ExciseAmount as ExciseAmount,"
    '        sSql = sSql & " g.POD_VAT as Vat,g.POD_VATAmount as VATAmount,g.POD_CST as CST,g.POD_CSTAmount as CSTAmount,g.POD_CompID,g.POD_Status,f.POM_Supplier,POM_OrderDate,"
    '        sSql = sSql & "  g.POD_Quantity,b.PIA_AcceptedQnt as AcceptedQnt,h.PGD_ReceivedQnt,h.PGD_RejectedQnt,h.PGD_Accepted,h.PGD_Excess,b.PIA_Excess,i.PIR_RejectedQty"
    '        sSql = sSql & "  from Purchase_verification"
    '        sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
    '        sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
    '        sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
    '        sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
    '        sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
    '        sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID  "
    '        sSql = sSql & " join Purchase_GIN_Details h on  PV_GinNo=h.PGD_MasterID and PIA_HistoryID=h.PGD_HistoryID"
    '        sSql = sSql & " left join Purchase_Invoice_Rejected i on PV_GinNo=i.PIR_GINID and PIA_HistoryID=i.PIR_HistoryID  where PIA_CompID=" & iCompID & ""
    '        If iorder <> 0 Then
    '            sSql = sSql & " And PV_OrderNo= " & iorder & " "
    '        End If
    '        If InvoiceId <> 0 Then
    '            sSql = sSql & " And PIA_GINID= " & InvoiceId & " "
    '        End If
    '        If isuuplier <> 0 Then
    '            sSql = sSql & " And POM_Supplier= " & isuuplier & " "
    '        End If

    '        If iCommodity <> 0 Then
    '            sSql = sSql & " And PIA_Commodity= " & iCommodity & " "
    '        End If
    '        If iItem <> 0 Then
    '            sSql = sSql & " And PIA_DescriptionID= " & iItem & " "
    '        End If
    '        If iFromDt <> "" And iToDt <> "" Then
    '            sSql = sSql & " and  POM_OrderDate between '" & iFromDt & "'  and '" & iToDt & " 23:59:59'"
    '        End If
    '        sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
    '        dtDetails = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        For i = 0 To dtDetails.Rows.Count - 1
    '            dRow = dt.NewRow()
    '            If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
    '                dRow("Commodity") = dtDetails.Rows(i)("Commodity")
    '            Else
    '                dRow("Commodity") = "Unknown"
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("Inv_Code")) = False Then
    '                dRow("SlNo") = i + 1
    '                dRow("Description") = dtDetails.Rows(i)("Inv_Code")
    '            Else
    '                dRow("Description") = "Unknown"
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_OrderNo")) = False Then
    '                dRow("OrderNo") = dtDetails.Rows(i)("POM_OrderNo")
    '            Else
    '                dRow("OrderNo") = "0"
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("PV_DocRefNo")) = False Then
    '                dRow("PV_DocRefNo") = dtDetails.Rows(i)("PV_DocRefNo")
    '            Else
    '                dRow("PV_DocRefNo") = "0"
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
    '                dRow("Orderdate") = clsTRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
    '            Else
    '                dRow("Orderdate") = "0"
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("PGM_GIN_Number")) = False Then
    '                dRow("GinNo") = dtDetails.Rows(i)("PGM_GIN_Number")
    '            Else
    '                dRow("GinNo") = "0"
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_Supplier")) = False Then
    '                dRow("Supplier") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "CSM_Name", "CSM_ID", dtDetails.Rows(i)("POM_Supplier"), "customerSupplierMaster")
    '            Else
    '                dRow("Supplier") = "Unknown"
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POD_Quantity")) = False Then
    '                dRow("Orderedqty") = dtDetails.Rows(i)("POD_Quantity")
    '            Else
    '                dRow("Orderedqty") = 0
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("PGD_Accepted")) = False Then
    '                dRow("AcceptedQntity") = Convert.ToDecimal(dtDetails.Rows(i)("PGD_Accepted"))
    '            Else
    '                dRow("AcceptedQntity") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("PGD_ReceivedQnt")) = False Then
    '                dRow("ReceivedQnt") = Convert.ToDecimal(dtDetails.Rows(i)("PGD_ReceivedQnt"))
    '            Else
    '                dRow("ReceivedQnt") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("PIA_Excess")) = False Then
    '                dRow("Excess") = Convert.ToDecimal(dtDetails.Rows(i)("PIA_Excess"))
    '            Else
    '                dRow("Excess") = 0
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("PIR_RejectedQty")) = False Then
    '                dRow("RejectedQty") = Convert.ToDecimal(dtDetails.Rows(i)("PIR_RejectedQty"))
    '            Else
    '                dRow("RejectedQty") = 0
    '            End If
    '            pending = dRow("Orderedqty") - dRow("ReceivedQnt")
    '            If pending < 0 Then
    '                dRow("PendingQty") = 0
    '            Else
    '                dRow("PendingQty") = pending
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("Rate")) = False Then
    '                dRow("Rate") = dtDetails.Rows(i)("Rate")
    '                TotalAmt = String.Format("{0:0.00}", Convert.ToDecimal(dRow("Rate") * dRow("AcceptedQntity")))
    '                BasicAmount = String.Format("{0:0.00}", Convert.ToDecimal(dRow("Rate") * dRow("AcceptedQntity")))
    '            Else
    '                dRow("Rate") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("Discount")) = False Then
    '                dRow("Discount") = dtDetails.Rows(i)("Discount")
    '            Else
    '                dRow("Discount") = "0"
    '            End If


    '            If IsDBNull(dtDetails.Rows(i)("DiscountAmount")) = False And dtDetails.Rows(i)("DiscountAmount") <> "" Then
    '                dRow("DiscountAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(BasicAmount) * Convert.ToDecimal(dtDetails.Rows(i)("Discount")) / 100))
    '            Else
    '                dRow("DiscountAmt") = "0"
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("Vat")) = False Then
    '                dRow("VAT") = dtDetails.Rows(i)("Vat")
    '            Else
    '                dRow("VAT") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("VATAmount")) = False Then
    '                dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("Vat")) / 100))
    '                'String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("VATAmount")))
    '                TotalAmt = TotalAmt + dRow("VATAmt")
    '            Else
    '                dRow("VATAmt") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("CST")) = False And dtDetails.Rows(i)("CST") <> "" Then
    '                dRow("CST") = dtDetails.Rows(i)("CST")
    '            Else
    '                dRow("CST") = 0
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("CSTAmount")) = False Then
    '                dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("CST")) / 100))
    '                'String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("CSTAmount")))
    '                TotalAmt = TotalAmt + dRow("CSTAmt")
    '            Else
    '                dRow("CSTAmt") = 0
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("Excise")) = False Then
    '                dRow("Exise") = dtDetails.Rows(i)("Excise")
    '            Else
    '                dRow("Exise") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("ExciseAmount")) = False Then
    '                dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("Excise")) / 100))
    '                TotalAmt = TotalAmt + dRow("ExiseAmt")
    '            Else
    '                dRow("ExiseAmt") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
    '                dRow("InvoiceDate") = clsTRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate"), "D")
    '            Else
    '                dRow("InvoiceDate") = "0"
    '            End If
    '            dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt) - Convert.ToDecimal(dRow("DiscountAmt")))
    '            dRow("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(BasicAmount) - Convert.ToDecimal(dRow("DiscountAmt")))

    '            dt.Rows.Add(dRow)
    '        Next
    '        ' sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " And CSM_DelFlag='A'"
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function LoadAllPurchaseDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtZoneRegionBranchAreaDetails As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String, sModuleRole As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("PoID")
            dt.Columns.Add("POnO")
            dt.Columns.Add("PoDate")
            dt.Columns.Add("Supplier")
            dt.Columns.Add("Status")
            dt.Columns.Add("TotaAmount")

            sSql = "SELECT POM_ID,POM_OrderDate,POM_OrderNo,POM_Supplier,POM_Status,POM_DelFlag,POM_TotAmount FROM PURCHASE_ORDER_MASTER"
            dtDetails = objDB.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("POM_ID")) = False Then
                    dRow("PoID") = dtDetails.Rows(i)("POM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_OrderNo")) = False Then
                    dRow("POnO") = dtDetails.Rows(i)("POM_OrderNo")
                End If

                If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                    dRow("PoDate") = objFas.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_Supplier")) = False Then
                    dRow("Supplier") = objDB.SQLExecuteScalar(sAC, "select CSM_Name from CustomerSupplierMaster where CSM_ID= " & dtDetails.Rows(i)("POM_Supplier") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_DelFlag")) = False Then
                    If dtDetails.Rows(i)("POM_Status") = "W" Then
                        dRow("Status") = "Waiting for Approval"
                    ElseIf dtDetails.Rows(i)("POM_Status") = "D" Then
                        dRow("Status") = "De-Activated"
                    ElseIf (dtDetails.Rows(i)("POM_Status") = "A") Then
                        dRow("Status") = "Activated"
                    ElseIf dtDetails.Rows(i)("POM_Status") = "L" Then
                        dRow("Status") = "Lock"
                    ElseIf dtDetails.Rows(i)("POM_Status") = "B" Then
                        dRow("Status") = "Block"
                    End If
                End If

                If IsDBNull(dtDetails.Rows(i)("POM_TotAmount")) = False Then
                    dRow("TotaAmount") = objClsFasgnrl.ReplaceSafeSQL(dtDetails.Rows(i)("POM_TotAmount"))
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function




    Public Function LoadDescritionStart(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_CompID =" & iCompID & " And Inv_Code <> '' and Inv_Parent <> 0"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetBrandValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "select Inv_Parent from inventory_master where Inv_ID=" & InvID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvCode_ID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "select GST_GSTRate From GST_Rates Where GST_ItemID = " & InvCode_ID & " And GST_CompID = " & iCompID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetAlterNatePiceValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvHistryID As String) As Decimal
        Dim sSql As String = ""
        Try
            sSql = "Select INVH_PerPieces From Inventory_master_History Where InvH_ID ='" & InvHistryID & "' And INVH_CompID=" & iCompID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUnitsValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvHistryID As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select InvH_Unit From Inventory_master_History Where InvH_ID ='" & InvHistryID & "' And INVH_CompID=" & iCompID & " "
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function CheckDescriptionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtNew As New DataTable
        Dim dRow As DataRow
        Try
            dtNew.Columns.Add("InvH_ID")
            dtNew.Columns.Add("INVH_PreDeterminedPrice")

            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iDescriptionID & " And InvH_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtNew.NewRow
                dRow("InvH_ID") = dt.Rows(i)("InvH_ID")
                dRow("INVH_PreDeterminedPrice") = dt.Rows(i)("INVH_PreDeterminedPrice") & " - " & objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("InvH_EffeFrom"), "D")
                dtNew.Rows.Add(dRow)
            Next
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadStockRateQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer, ByVal GInID As Integer, ByVal OrderId As Integer)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable, dtNew As New DataTable
    '    Dim dRow As DataRow
    '    Try
    '        dtNew.Columns.Add("SL_PurchaseQty")
    '        dtNew.Columns.Add("PurchaseRate")
    '        dtNew.Columns.Add("SL_HistoryID")
    '        sSql = "Select SL_PurchaseQty,PurchaseRate,SL_HistoryID from stock_ledger where SL_OrderID = " & OrderId & " And SL_CompID =" & iCompID & " And SL_GINID=" & GInID & ""
    '        dt = objDB.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        For i = 0 To dt.Rows.Count - 1
    '            dRow = dtNew.NewRow
    '            dRow("SL_PurchaseQty") = dt.Rows(i)("SL_PurchaseQty")
    '            dRow("PurchaseRate") = dt.Rows(i)("PurchaseRate")
    '            dRow("SL_HistoryID") = dt.Rows(i)("SL_HistoryID")
    '            dtNew.Rows.Add(dRow)
    '        Next
    '        Return dtNew
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Public Function GetPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from inventory_master_History where InvH_ID =" & iHistoryID & " And InvH_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Shared Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iTransactionID As String) As Integer
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "Update Purchase_Order_Master Set POM_Status='A',POM_ApporvedBy=" & iUserID & ",POM_ApprovedOn=GetDate() "
    '        sSql = sSql & "Where POM_OrderNo='" & iTransactionID & "' And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
    '        objDB.ExecuteNoNQuery(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As String) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select POM_Status From Purchase_Order_Master Where POM_ID='" & iTransactionID & "' And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
            sStatus = objDB.SQLGetDescription(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Getstatuus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select POM_Status From Purchase_Order_Master  Where POM_ID = " & iMasId & " and POM_CompID=" & iCompID & " "
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    ''Public Shared Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
    ''    Dim sSql As String = ""
    ''    Dim dt As New DataTable
    ''    Dim bCheckO As Boolean
    ''    Dim bCheckP As Boolean
    ''    Try
    ''        If sSearch <> "" Then
    ''            bCheckO = DBHelper.DBCheckForRecord(sNameSpace, "Select * From Purchase_Order_Master where POM_OrderNo ='" & sSearch & "' And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & "")
    ''            If bCheckO = True Then
    ''                sSql = "Select POM_ID,POM_OrderNo From Purchase_Order_Master where POM_OrderNo ='" & sSearch & "' And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & ""
    ''                dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    ''                'Else
    ''                '    bCheckP = DBHelper.DBCheckForRecord(sNameSpace, "Select * From Purchase_Order_Master where POM_Supplier='" & sSearch & "' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & "")
    ''                '    If bCheckP = True Then
    ''                '        sSql = "Select SPO_ID,SPO_OrderCode From Sales_Proforma_Order where SPO_OrderType='S' And SPO_PartyCode ='" & sSearch & "' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & ""
    ''                '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    ''                '    End If
    ''            End If
    ''        Else
    ''            sSql = "Select POM_ID,POM_OrderNo From Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & " Order By POM_ID Desc"
    ''            dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    ''        End If
    ''        Return dt
    ''    Catch ex As Exception
    ''        Throw
    ''    End Try
    ''End Function
    'Public Shared Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim bCheckO As Boolean
    '    Dim bCheckP As Boolean

    '    Try
    '        'If sSearch <> "" Then
    '        sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " and POM_Status<>'D' order by POM_ID desc"
    '        dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
    '        ' End If
    '        '    bCheckO = DBHelper.DBCheckForRecord(sNameSpace, "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
    '        '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
    '        '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
    '        '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
    '        '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " And POM_OrderNo ='" & sSearch & "' and POM_Status<>'D' order by POM_ID desc")
    '        '    If bCheckO = True Then
    '        '        sSql = "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
    '        '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
    '        '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
    '        '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
    '        '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " And POM_OrderNo ='" & sSearch & "' and POM_Status<>'D' order by POM_ID desc"
    '        '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        '    End If
    '        'Else
    '        '    sSql = "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
    '        '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
    '        '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
    '        '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
    '        '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " and POM_Status<>'D' order by POM_ID desc"
    '        '    dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        ' End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GeneratePurchaseOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = "", Oral As String = "O"
        Dim sMaximumID As String = "", sMonth As String = "", sMonthCode As String = ""
        Dim sDate As String = "", sMaxID As String = "", sLastID As String = "", sSDate As String = ""
        Try
            sMaximumID = objDB.SQLGetDescription(sNameSpace, "Select Top 1 POM_ID From Purchase_Order_Master where POM_COmpID = " & iCompID & " Order By POM_ID Desc")
            sYear = objDB.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDB.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDB.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
            sStr = "" & Oral & "" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " and CSM_Delflag='A' order by CSM_Name"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Sub LoadCommodity()
    '    Try
    '        ddlCommodity.DataSource = clsCustomerOrder.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlCommodity.DataTextField = "Inv_Description"
    '        ddlCommodity.DataValueField = "Inv_ID"
    '        ddlCommodity.DataBind()
    '        ddlCommodity.Items.Insert(0, "--- Select Commodity ---")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Sub LoadCST()
    '    Try
    '        ddlCST.DataSource = clsInvenotryDetails.LoadCST(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlCST.DataTextField = "Mas_Desc"
    '        ddlCST.DataValueField = "Mas_ID"
    '        ddlCST.DataBind()
    '        ddlCST.Items.Insert(0, "--- Select CST ---")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " and POM_Status<>'D' and POM_OralStatus='O' order by POM_ID desc"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dOrderDate As Date, ByVal objOral As clsOralOrder) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Master where POM_OrderNo = '" & objOral.sPOM_OrderNo & "' and POM_CompID =" & iCompID & " and POM_YearID =" & objOral.iPOM_YearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then

                sSql = "" : sSql = "Update Purchase_Order_Master set POM_OrderDate=" & objFas.FormatDtForRDBMS(dOrderDate, "I") & ","
                sSql = sSql & "POM_Supplier = " & objOral.iPOM_Supplier & ",POM_MPayment=" & objOral.iPOM_MethodofPayment & ","
                sSql = sSql & "POM_Paymentterms =" & objOral.iPOM_Paymentterms & ",POM_ModeOfShipping = " & objOral.iPOM_ModeOfShipping & ","
                sSql = sSql & "POM_DSchdule = " & objOral.iPOM_DSchdule & ",POM_TypeOfPurchase=" & objOral.iPOM_SaleType & ", "
                sSql = sSql & "POM_CstCategory = " & objOral.iPOM_iCSTCtgry & ",POM_OralStatus='" & objOral.sOralOrPO & "',"
                sSql = sSql & "POM_TrType = " & objOral.iPOM_TrType & ",POM_CompanyAddress='" & objOral.sPOM_CompanyAddress & "',"
                sSql = sSql & "POM_CompanyGSTNRegNo ='" & objOral.sPOM_CompanyGSTNRegNo & "',POM_BillingAddress='" & objOral.sPOM_BillingAddress & "',"
                sSql = sSql & "POM_UpdatedBy = " & objOral.iPOM_UpdatedBy & ",POM_UpdatedOn=getdate(),POM_BillingGSTNRegNo='" & objOral.sPOM_BillingGSTNRegNo & "',"
                sSql = sSql & "POM_DeliveryFrom ='" & objOral.sPOM_DeliveryFrom & "',POM_DeliveryFromGSTNRegNo='" & objOral.sPOM_DeliveryFromGSTNRegNo & "',"
                sSql = sSql & "POM_DeliveryAddress='" & objOral.sPOM_DeliveryAddress & "',POM_DeliveryGSTNRegNo='" & objOral.sPOM_DeliveryGSTNRegNo & "',"
                sSql = sSql & "POM_PurchaseStatus ='" & objOral.POM_PurchaseStatus & "',POM_CompanyType=" & objOral.iPOM_CompanyType & ",POM_GSTNCategory=" & objOral.iPOM_GSTNCategory & ", "
                sSql = sSql & "POM_ZoneID=" & objOral.iPOM_ZoneID & ",POM_RegionID=" & objOral.iPOM_RegionID & ",POM_AreaID=" & objOral.iPOM_AreaID & ","
                sSql = sSql & "POM_BranchID = " & objOral.iPOM_BranchID & ",POM_InvoiceRef='" & objOral.sPOM_InvoiceRef & "',POM_ESugamNo='" & objOral.sPOM_ESugam & "',"
                sSql = sSql & "POM_InvoiceDate = " & objFas.FormatDtForRDBMS(objOral.DPOM_InvoiceDate, "I") & ",POM_DcNo='" & objOral.sPOM_DcNo & "'"
                sSql = sSql & "Where POM_OrderNo = '" & objOral.sPOM_OrderNo & "' and POM_CompID =" & iCompID & " and POM_YearID=" & objOral.iPOM_YearID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dt.Rows(0)("POM_ID")
            Else
                iMax = objGnrl.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Master", "POM_ID", "POM_CompID")
                sSql = "" : sSql = "Insert into Purchase_Order_Master(POM_ID,POM_OrderDate,POM_OrderNo,POM_Supplier,"
                sSql = sSql & "POM_ModeOfShipping,POM_Status,POM_CreatedBy,POM_CreatedOn,"
                sSql = sSql & "POM_YearID,POM_CompID,POM_MPayment,POM_PaymentTerms,POM_DSchdule,POM_TypeOfPurchase,"
                sSql = sSql & "POM_CstCategory, POM_DelFlag, POM_OralStatus, POM_TrType, POM_CompanyAddress, POM_CompanyGSTNRegNo, "
                sSql = sSql & "POM_BillingAddress, POM_BillingGSTNRegNo, POM_DeliveryFrom, POM_DeliveryFromGSTNRegNo, POM_DeliveryAddress,"
                sSql = sSql & "POM_DeliveryGSTNRegNo, POM_PurchaseStatus, POM_CompanyType, POM_GSTNCategory, POM_InvoiceRef, POM_DcNo, "
                sSql = sSql & "POM_ESugamNo, POM_InvoiceDate, POM_ZoneID, POM_RegionID, POM_AreaID, POM_BranchID,POM_BatchNo,POM_BaseName) "
                sSql = sSql & " Values(" & iMax & ", " & objFas.FormatDtForRDBMS(dOrderDate, "I") & ",'" & objOral.sPOM_OrderNo & "'," & objOral.iPOM_Supplier & ","
                sSql = sSql & "" & objOral.iPOM_ModeOfShipping & ",'" & objOral.sPOM_Status & "'," & objOral.iPOM_CreatedBy & ",GetDate()," & objOral.iPOM_YearID & ","
                sSql = sSql & "" & iCompID & "," & objOral.iPOM_MethodofPayment & "," & objOral.iPOM_Paymentterms & "," & objOral.iPOM_DSchdule & ","
                sSql = sSql & "" & objOral.iPOM_SaleType & "," & objOral.iPOM_iCSTCtgry & ",'W','" & objOral.sOralOrPO & "'," & objOral.iPOM_TrType & ",'" & objOral.sPOM_CompanyAddress & "',"
                sSql = sSql & "'" & objOral.sPOM_CompanyGSTNRegNo & "','" & objOral.sPOM_BillingAddress & "','" & objOral.sPOM_BillingGSTNRegNo & "',"
                sSql = sSql & "'" & objOral.sPOM_DeliveryFrom & "','" & objOral.sPOM_DeliveryFromGSTNRegNo & "',"
                sSql = sSql & "'" & objOral.sPOM_DeliveryAddress & "','" & objOral.sPOM_DeliveryGSTNRegNo & "','" & objOral.POM_PurchaseStatus & "',"
                sSql = sSql & "" & objOral.iPOM_CompanyType & "," & objOral.iPOM_GSTNCategory & ",'" & objOral.sPOM_InvoiceRef & "',"
                sSql = sSql & "'" & objOral.sPOM_DcNo & "','" & objOral.sPOM_ESugam & "',"
                sSql = sSql & "" & objFas.FormatDtForRDBMS(objOral.DPOM_InvoiceDate, "I") & "," & objOral.iPOM_ZoneID & ","
                sSql = sSql & "" & objOral.iPOM_RegionID & "," & objOral.iPOM_AreaID & "," & objOral.iPOM_BranchID & "," & objOral.iPOM_BatchNo & "," & objOral.iPOM_BaseName & ")"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iyearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete purchase_order_details where POD_MasterID in (select POM_ID from Purchase_Order_master where POM_ID=" & iMasterID & " and  POM_CompID=" & iCompID & " and POM_YearID=" & iyearID & ") And POD_CompID =" & iCompID & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckApprovedOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer) As String
        Dim sSql As String = ""
        Try
            'sSql = "Select POM_Status from Purchase_Order_Master where POM_ID=" & OrderNo & " and POM_YearID =" & iYearID & " and POM_CompID =" & iCompID & " and POM_Status='W'"
            sSql = "Select POM_Status from Purchase_Order_Master where POM_ID=" & OrderNo & " and POM_YearID =" & iYearID & " and POM_CompID =" & iCompID & " "
            CheckApprovedOrNot = objDB.SQLGetDescription(sNameSpace, sSql)
            Return CheckApprovedOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objOral As clsOralOrder, ByVal iPKID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Details where POD_ID=" & iPKID & " And POD_MasterID = " & objOral.iPOD_MasterID & " and POD_Commodity = " & objOral.iPOD_Commodity & " and "
            sSql = sSql & "POD_DescriptionID = " & objOral.iPOD_DescriptionID & " and POD_HistoryID =" & objOral.iPOD_HistoryID & " and POD_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Purchase_Order_Details set POD_RequiredDate=" & objOral.dPOD_RequiredDate & ", POD_Unit = " & objOral.iPOD_Unit & ",POD_Rate='" & objOral.sPOD_Rate & "',POD_RateAmount = '" & objOral.sPOD_RateAmount & "',POD_Quantity='" & objOral.sPOD_Quantity & "',"
                sSql = sSql & "POD_Discount = '" & objOral.sPOD_Discount & "',POD_DiscountAmount='" & objOral.sPOD_DiscountAmount & "',POD_Excise='" & objOral.sPOD_Excise & "',POD_UpdatedBy=" & objOral.iPOD_UpdatedBy & ",POD_UpdatedOn=getdate(),"
                sSql = sSql & "POD_ExciseAmount = '" & objOral.sPOD_ExciseAmount & "',POD_VAT = '" & objOral.sPOD_VAT & "',POD_VATAmount='" & objOral.sPOD_VATAmount & "',"
                sSql = sSql & "POD_CST='" & objOral.sPOD_CST & "',POD_CSTAmount='" & objOral.sPOD_CSTAmount & "',POD_ManufactureDate=" & objFas.FormatDtForRDBMS(objOral.DPOM_ManfctreDate, "I") & ",POD_ExpiryDate=" & objFas.FormatDtForRDBMS(objOral.DPOM_ExpryDate, "I") & ","
                sSql = sSql & "POD_TotalAmount='" & objOral.sPOD_TotalAmount & "',POD_Status='W',POD_Frieght='" & objOral.sPOD_Frieght & "',POD_FrieghtAmount='" & objOral.sPOD_FrieghtAmount & "' ,POD_ReceivedQty=" & objOral.POD_ReceivedQty & ",POD_RejectedQty=" & objOral.POD_Rejected & ",POD_AcceptedQty=" & objOral.iPOD_Accepted & ",POD_BatchNumber='" & objOral.sPOM_BatchNumber & "',"
                sSql = sSql & "POD_GST_ID = " & objOral.POD_GST_ID & ",POD_GSTRate=" & objOral.POD_GSTRate & ", POD_GSTAmount=" & objOral.POD_GSTAmount & ",POD_SGST=" & objOral.POD_SGST & ", POD_SGSTAmount=" & objOral.POD_SGSTAmount & ","
                sSql = sSql & "POD_CGST=" & objOral.POD_CGST & ",POD_CGSTAmount=" & objOral.POD_CGSTAmount & ",POD_IGST=" & objOral.POD_IGST & ",POD_IGSTAmount=" & objOral.POD_IGSTAmount & ", POD_Currency= '" & objOral.iPOD_Currency & "', POD_CurrencyAmt=" & objOral.iPOD_CurrencyAmt & ",POD_CurrencyTime='" & objOral.sPOD_CurrencyTime & "',POD_FETotalAmt='" & objOral.sPOD_FETotalAmt & "' where POD_ID=" & iPKID & " And POD_MasterID = " & objOral.iPOD_MasterID & " And "
                sSql = sSql & "POD_Commodity = " & objOral.iPOD_Commodity & " And POD_DescriptionID = " & objOral.iPOD_DescriptionID & " And "
                sSql = sSql & "POD_HistoryID =" & objOral.iPOD_HistoryID & " And POD_CompID = " & iCompID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)

            Else
                iMax = objGnrl.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Details", "POD_ID", "POD_CompID")
                sSql = "" : sSql = "Insert into Purchase_Order_Details(POD_ID,POD_MasterID,POD_Commodity,"
                sSql = sSql & "POD_DescriptionID,POD_HistoryID,POD_RequiredDate,POD_Unit,POD_Rate,POD_RateAmount,"
                sSql = sSql & "POD_Quantity,POD_Discount,POD_DiscountAmount,POD_Excise,"
                sSql = sSql & "POD_ExciseAmount,POD_VAT,POD_VATAmount,POD_CST,POD_CreatedBy,POD_CreatedOn,"
                sSql = sSql & "POD_CSTAmount,POD_TotalAmount,POD_CompID,POD_Status,POD_Frieght,POD_FrieghtAmount,POD_ReceivedQty,POD_RejectedQty,POD_AcceptedQty,POD_BatchNumber,POD_ExpiryDate,POD_ManufactureDate,"
                sSql = sSql & "POD_GST_ID,POD_GSTRate,POD_GSTAmount,POD_SGST,POD_SGSTAmount,POD_CGST,POD_CGSTAmount,POD_IGST,POD_IGSTAmount,POD_Currency, POD_CurrencyAmt,POD_CurrencyTime,POD_FETotalAmt)"

                sSql = sSql & "Values(" & iMax & "," & objOral.iPOD_MasterID & "," & objOral.iPOD_Commodity & ","
                sSql = sSql & "" & objOral.iPOD_DescriptionID & "," & objOral.iPOD_HistoryID & "," & objFas.FormatDtForRDBMS(objOral.dPOD_RequiredDate, "I") & "," & objOral.iPOD_Unit & ",'" & objOral.sPOD_Rate & "','" & objOral.sPOD_RateAmount & "',"
                sSql = sSql & "'" & objOral.sPOD_Quantity & "','" & objOral.sPOD_Discount & "','" & objOral.POD_DiscountAmount & "','" & objOral.sPOD_Excise & "',"
                sSql = sSql & "'" & objOral.sPOD_ExciseAmount & "','" & objOral.sPOD_VAT & "','" & objOral.sPOD_VATAmount & "','" & objOral.sPOD_CST & "'," & objOral.iPOD_CreatedBy & ",getdate(),"
                sSql = sSql & "'" & objOral.sPOD_CSTAmount & "','" & objOral.sPOD_TotalAmount & "'," & iCompID & ",'W','" & objOral.sPOD_Frieght & "',"
                sSql = sSql & "'" & objOral.sPOD_FrieghtAmount & "'," & objOral.POD_ReceivedQty & "," & objOral.POD_Rejected & ","
                sSql = sSql & "" & objOral.iPOD_Accepted & ",'" & objOral.sPOM_BatchNumber & "'," & objFas.FormatDtForRDBMS(objOral.DPOM_ExpryDate, "I") & ","
                sSql = sSql & " " & objFas.FormatDtForRDBMS(objOral.DPOM_ManfctreDate, "I") & "," & objOral.POD_GST_ID & "," & objOral.POD_GSTRate & ","
                sSql = sSql & " " & objOral.POD_GSTAmount & "," & objOral.POD_SGST & "," & objOral.POD_SGSTAmount & "," & objOral.POD_CGST & "," & objOral.POD_CGSTAmount & "," & objOral.POD_IGST & "," & objOral.POD_IGSTAmount & "," & objOral.iPOD_Currency & "," & objOral.iPOD_CurrencyAmt & ",'" & objOral.sPOD_CurrencyTime & "','" & objOral.sPOD_FETotalAmt & "')"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)

            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSavedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTRID As Integer, ByVal iCommodity As Integer, ByVal iUnitID As Integer, ByVal iHistoryID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iTRID > 0 And iCommodity > 0 And iUnitID > 0 Then
                'sSql = "" : sSql = "Select * From Purchase_Order_Details Where POD_MasterID=" & iTRID & " And POD_Commodity=" & iCommodity & " And POD_Unit=" & iUnitID & " And POD_CompID =" & iCompID & "  and POD_HistoryID=" & iHistoryID & ""
                sSql = "" : sSql = "Select * From Purchase_Order_Details Where POD_MasterID in (select POM_ID from Purchase_Order_master where POM_ID=" & iTRID & " and  POM_CompID=" & iCompID & " and POM_YearID=" & iYearID & ") And POD_Commodity=" & iCommodity & " And POD_Unit=" & iUnitID & " And POD_CompID =" & iCompID & "  and POD_HistoryID=" & iHistoryID & ""


                dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Delete From Charges_Master Where C_PSType='P' and C_POrderID=" & iOrderID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveChargess(ByVal sNameSpace As String, ByVal objOral As clsOralOrder) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_AllocatedID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_AllocatedID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_DispatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_DispatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_OrderType", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objOral.C_OrderType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_ChargeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeType", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOral.C_ChargeType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objOral.C_ChargeAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_PSType", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objOral.C_PSType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objOral.C_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOral.C_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOral.C_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOral.C_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objOral.C_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objOral.C_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_POrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOral.C_POrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_PGinID ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOral.C_PGinID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_PInvoiceDocRef ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOral.C_PInvoiceDocRef
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spCharges_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCharges(ByVal sNameSpace As String, ByVal objInvoiceForm As clsOralOrder)
        Dim iMaxID As Integer
        Dim sSql As String = ""
        Try
            iMaxID = objDB.SQLExecuteScalar(sNameSpace, "select isnull(max(C_ID)+1,1) from Charges_master")
            sSql = "Insert Into Charges_master(C_ID, C_POrderID, C_PGinID, C_PInvoiceDocRef, C_OrderType, C_ChargeID, C_ChargeType, C_ChargeAmount, C_PSType, C_DelFlag, C_Status, C_CompID, C_YearID, C_CreatedBy, C_CreatedOn, C_Operation,C_IPAddress) "
            sSql = sSql & " Values(" & iMaxID & ", " & objInvoiceForm.C_POrderID & ", " & objInvoiceForm.C_PGinID & ", " & objInvoiceForm.C_PInvoiceDocRef & ", '" & objInvoiceForm.C_OrderType & "', " & objInvoiceForm.C_ChargeID & ", '" & objInvoiceForm.C_ChargeType & "', " & objInvoiceForm.C_ChargeAmount & ", '" & objInvoiceForm.C_PSType & "', '" & objInvoiceForm.C_DelFlag & "', '" & objInvoiceForm.C_Status & "', " & objInvoiceForm.C_CompID & ", " & objInvoiceForm.C_YearID & ", " & objInvoiceForm.C_CreatedBy & ", GetDate(), '" & objInvoiceForm.C_Operation & "','" & objInvoiceForm.C_IPAddress & "')"
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindChargeData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer, ByVal iGinID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("ChargeID")
            dtTab.Columns.Add("ChargeType")
            dtTab.Columns.Add("ChargeAmount")

            sSql = "" : sSql = "Select * From Charges_Master,Purchase_Order_Master Where C_POrderID=POM_ID And C_PSType='P' And C_POrderID=" & iOrderID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "

            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ChargeID") = dt.Rows(i)("C_ChargeID")
                    dRow("ChargeType") = objDB.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt.Rows(i)("C_ChargeID") & " And Mas_Master=24 And Mas_CompID = " & iCompID & "  ")
                    dRow("ChargeAmount") = dt.Rows(i)("C_ChargeAmount")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchaseORderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iyearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtPdetails As New DataTable
        Dim iSlNo As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("CommodityID")
            dt.Columns.Add("DescriptionID")
            dt.Columns.Add("HistoryID")
            dt.Columns.Add("UnitsID")
            dt.Columns.Add("SLNO")
            dt.Columns.Add("Goods")
            dt.Columns.Add("Units")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("ExciseDuty")
            dt.Columns.Add("ExciseAmt")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("POD_ReceivedQty")
            dt.Columns.Add("POD_RejectedQty")
            dt.Columns.Add("POD_AcceptedQty")
            dt.Columns.Add("POD_BatchNumber")
            dt.Columns.Add("POD_ExpiryDate")
            dt.Columns.Add("POD_ManufactureDate")
            dt.Columns.Add("Charge")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("SGST")
            dt.Columns.Add("SGSTAmount")
            dt.Columns.Add("CGST")
            dt.Columns.Add("CGSTAmount")

            'sSql = "Select * from Purchase_Order_Details where POD_MasterID =" & iMasterID & " And POD_CompID=" & iCompID & " And  POD_Status='W' order by POD_ID"
            sSql = "Select * from Purchase_Order_Details where POD_MasterID in (select POM_ID from Purchase_Order_master where POM_ID =" & iMasterID & " and  POM_CompID=" & iCompID & " and POM_YearID=" & iyearID & ") And POD_CompID=" & iCompID & " And  POD_Status='W' order by POD_ID"
            dtPdetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dtPdetails.Rows.Count > 0 Then
                For i = 0 To dtPdetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("ID") = dtPdetails.Rows(i)("POD_ID")
                    dRow("CommodityID") = dtPdetails.Rows(i)("POD_Commodity")
                    dRow("DescriptionID") = dtPdetails.Rows(i)("POD_DescriptionID")
                    dRow("HistoryID") = dtPdetails.Rows(i)("POD_HistoryID")
                    dRow("UnitsID") = dtPdetails.Rows(i)("POD_Unit")
                    iSlNo = iSlNo + 1
                    dRow("SLNO") = iSlNo
                    dRow("Goods") = objDB.SQLExecuteScalar(sNameSpace, "Select Inv_Code from Inventory_Master where Inv_ID='" & dtPdetails.Rows(i)("POD_DescriptionID") & "' and Inv_compid=" & iCompID & "")
                    dRow("Units") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dtPdetails.Rows(i)("POD_Unit") & "' and Mas_compid=" & iCompID & "")
                    dRow("Rate") = dtPdetails.Rows(i)("POD_Rate")
                    If IsDBNull(dtPdetails.Rows(i)("POD_Quantity")) = False Then
                        dRow("Quantity") = Math.Round(dtPdetails.Rows(i)("POD_Quantity"), 2)
                    End If
                    dRow("RateAmount") = dtPdetails.Rows(i)("POD_RateAmount")
                    dRow("Discount") = dtPdetails.Rows(i)("POD_Discount")
                    dRow("DiscountAmt") = dtPdetails.Rows(i)("POD_DiscountAmount")
                    dRow("ExciseDuty") = dtPdetails.Rows(i)("POD_Excise")
                    dRow("ExciseAmt") = dtPdetails.Rows(i)("POD_ExciseAmount")
                    dRow("VAT") = dtPdetails.Rows(i)("POD_VAT")
                    dRow("VATAmt") = dtPdetails.Rows(i)("POD_VATAmount")
                    dRow("CST") = dtPdetails.Rows(i)("POD_CST")
                    dRow("CSTAmount") = dtPdetails.Rows(i)("POD_CSTAmount")
                    dRow("TotalAmount") = dtPdetails.Rows(i)("POD_TotalAmount")

                    dRow("Charge") = dtPdetails.Rows(i)("POD_FrieghtAmount")

                    If IsDBNull(dtPdetails.Rows(i)("POD_GSTRate")) = False Then
                        dRow("GSTRate") = dtPdetails.Rows(i)("POD_GSTRate")
                    Else
                        dRow("GSTRate") = 0
                    End If

                    If IsDBNull(dtPdetails.Rows(i)("POD_GSTAmount")) = False Then
                        dRow("GSTAmount") = dtPdetails.Rows(i)("POD_GSTAmount")
                    Else
                        dRow("GSTAmount") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_SGST")) = False Then
                        dRow("SGST") = dtPdetails.Rows(i)("POD_SGST")
                    Else
                        dRow("SGST") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_SGSTAmount")) = False Then
                        dRow("SGSTAmount") = dtPdetails.Rows(i)("POD_SGSTAmount")
                    Else
                        dRow("SGSTAmount") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_CGST")) = False Then
                        dRow("CGST") = dtPdetails.Rows(i)("POD_CGST")
                    Else
                        dRow("CGST") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_CGSTAmount")) = False Then
                        dRow("CGSTAmount") = dtPdetails.Rows(i)("POD_CGSTAmount")
                    Else
                        dRow("CGSTAmount") = 0
                    End If

                    If IsDBNull(dtPdetails.Rows(i)("POD_ReceivedQty")) = False Then
                        dRow("POD_ReceivedQty") = Math.Round(dtPdetails.Rows(i)("POD_ReceivedQty"), 2)
                    Else
                        dRow("POD_ReceivedQty") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_RejectedQty")) = False Then
                        dRow("POD_RejectedQty") = Math.Round(dtPdetails.Rows(i)("POD_RejectedQty"), 2)
                    Else
                        dRow("POD_RejectedQty") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_AcceptedQty")) = False Then
                        dRow("POD_AcceptedQty") = Math.Round(dtPdetails.Rows(i)("POD_AcceptedQty"), 2)
                    Else
                        dRow("POD_AcceptedQty") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_BatchNumber")) = False Then
                        dRow("POD_BatchNumber") = dtPdetails.Rows(i)("POD_BatchNumber")
                    Else
                        dRow("POD_BatchNumber") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_ExpiryDate")) = False Then
                        dRow("POD_ExpiryDate") = objClsFasgnrl.FormatDtForRDBMS(dtPdetails.Rows(i)("POD_ExpiryDate"), "D")
                    Else
                        dRow("POD_ExpiryDate") = 0
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_ManufactureDate")) = False Then
                        dRow("POD_ManufactureDate") = objClsFasgnrl.FormatDtForRDBMS(dtPdetails.Rows(i)("POD_ManufactureDate"), "D")
                    Else
                        dRow("POD_ManufactureDate") = 0
                    End If

                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub DeleteOrderValues(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal OrderNo As String, ByVal iCommodity As Integer, ByVal DcritionID As Integer)
        Dim sSql As String = ""
        Try
            'sSql = "Update Purchase_Order_Details set POD_Status='D' Where POD_MasterID in(select POM_ID from Purchase_Order_Master "
            'sSql = sSql & "where POM_OrderNo='" & OrderNo & "' and POM_YearID =" & iYearId & " and POM_COmpID =" & iCompID & " and  POM_Status='W') and POD_DescriptionID=" & DcritionID & " and POD_CompID = " & iCompID & ""
            sSql = "Update Purchase_Order_Details set POD_Status='D' Where POD_MasterID in(select POM_ID from Purchase_Order_Master "
            sSql = sSql & "where POM_OrderNo='" & OrderNo & "' and POM_YearID =" & iYearId & " and POM_COmpID =" & iCompID & " and  POM_Status='W') And POD_Commodity=" & iCommodity & " and POD_DescriptionID=" & DcritionID & " and POD_CompID = " & iCompID & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub DeleteOrderValuesFromMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal OrderNo As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_Order_Master set POM_Status='D' where POM_OrderNo='" & OrderNo & "' and POM_YearID =" & iYearId & " and POM_COmpID =" & iCompID & " and  POM_Status='W'"
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub



    Public Function LoadUnitOFMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtNew As New DataTable
        Try
            dtNew.Columns.Add("Mas_ID")
            dtNew.Columns.Add("Mas_Desc")

            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iInvID & " And InvH_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_Unit")
                dRow("Mas_Desc") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dt.Rows(0)("InvH_Unit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)

                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_AlterUnit")
                dRow("Mas_Desc") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dt.Rows(0)("InvH_AlterUnit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)
            End If
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseOderMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPomID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Master where POM_ID = " & iPomID & " and POM_CompID = " & iCompID & " and POM_YearID =" & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchaseCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Charges_Master where C_POrderID = " & iPID & " and C_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseOderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iCommodity As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Details where POD_ID=" & iPKID & " And POD_MasterID in (select POM_ID from Purchase_Order_master where POM_ID= " & iMasterID & " and  POM_CompID=" & iCompID & " and POM_YearID=" & iYearID & ") and POD_Commodity = " & iCommodity & " and "
            sSql = sSql & "POD_DescriptionID = " & iDescriptionID & " and  POD_HistoryID = " & iHistoryID & " and POD_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSupplierCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer)
        Dim sSql As String = "", sCOde As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select * from customerSupplierMaster where CSM_ID =" & iSupplier & " and CSM_CompID = " & iCompID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from inventory_Master_History where InvH_Id = " & iHistoryID & " and InvH_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetGeneralMasterValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal MasID As Integer, ByVal DescRiption As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_id from ACC_General_Master where Mas_master=" & MasID & " And Mas_desc ='" & DescRiption & "' "
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPaymentTerms(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=18 And Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDeliverySchdule(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=17 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadModeShiping(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=13 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadNumberOfDays(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=20 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMethodOfPayment(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=11 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdatePurchaseMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As String, ByVal iMasId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_order_master Set POM_IPAddress='" & sIPAddress & "',"
            If sStatus = "Created" Then
                sSql = sSql & " POM_DelFlag='A',POM_Status='A',POM_ApporvedBy= " & iUserID & ",POM_ApprovedOn=GetDate()"
            ElseIf sStatus = "DeActivated" Then
                sSql = sSql & " POM_DelFlag='D',POM_Status='AD',POM_DeletedBy= " & iUserID & ",POM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " POM_DelFlag='A',POM_Status='AR',POM_RecalledBy= " & iUserID & ",POM_RecalledOn=GetDate()"
            End If
            sSql = sSql & " Where POM_Id= " & iMasId & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetCOAHeadID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_AccHead from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
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
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
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
            sGL = objDB.SQLGetDescription(sNameSpace, sSql)
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
            sGL = objDB.SQLGetDescription(sNameSpace, sSql)
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
            sSql = sSql & " Acc_LedgerType ='" & sLedgerType & "' and Acc_CompID=" & iCompID & " "
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
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
            iCOA = objDB.SQLExecuteScalar(sNameSpace, sSql)
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

            sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & iPartyID & " And CSM_CompID=" & iCompID & " and CSM_Delflag='A' "
            sPartyName = objDB.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            GetPartySubGLID = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
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

            sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & iPartyID & " And CSM_CompID=" & iCompID & " and CSM_Delflag='A' "
            sPartyName = objDB.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            sGL = objDB.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPOID As Integer) As String
        Dim sSql As String = ""
        Dim Count As String = ""
        Try
            sSql = "select POM_OrderNo from purchase_order_master where POM_ID=" & iPOID & " and POM_CompID=" & iCompID & ""
            Count = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return Count
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Shared Function GetAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPomID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dtFRomTable As New DataTable
    '    Dim dRow As DataRow
    '    Dim subtotal As Double = 0
    '    Try
    '        dt.Columns.Add("POM_OrderNo")
    '        dt.Columns.Add("POM_OrderDate")
    '        dt.Columns.Add("POM_Supplier")
    '        dt.Columns.Add("POD_Commodity")
    '        dt.Columns.Add("POD_Rate")
    '        dt.Columns.Add("POD_Quantity")
    '        dt.Columns.Add("POD_CST")
    '        dt.Columns.Add("POD_VAT")
    '        dt.Columns.Add("POD_CSTAmount")
    '        dt.Columns.Add("POD_RateAmount")
    '        dt.Columns.Add("POD_Discount")
    '        dt.Columns.Add("POD_DiscountAmount")
    '        dt.Columns.Add("POD_TotalAmount")
    '        dt.Columns.Add("POD_VATAmount")
    '        dt.Columns.Add("POD_Unit")
    '        dt.Columns.Add("INV_Code")
    '        dt.Columns.Add("INV_Description")
    '        dt.Columns.Add("CSM_Name")
    '        dt.Columns.Add("CSM_Address")
    '        dt.Columns.Add("CSM_MobileNo")
    '        dt.Columns.Add("CSM_EmailID")
    '        dt.Columns.Add("Mas_Desc")
    '        dt.Columns.Add("CUST_CODE")
    '        dt.Columns.Add("CUST_COMM_ADDRESS")
    '        dt.Columns.Add("CUST_EMAIL")
    '        dt.Columns.Add("CUST_COMM_TEL")
    '        dt.Columns.Add("CUST_NAME")
    '        dt.Columns.Add("INVH_MRP")
    '        dt.Columns.Add("INVH_Mdate")
    '        dt.Columns.Add("INVH_Edate")
    '        dt.Columns.Add("POD_Excise")
    '        dt.Columns.Add("POD_ExciseAmount")
    '        dt.Columns.Add("POD_Frieght")
    '        dt.Columns.Add("POD_FrieghtAmount")

    '        sSql = "Select POM_OrderNo, Convert(VARCHAR(10), POM_OrderDate, 103)As POM_OrderDate, POM_Supplier, b.POD_Commodity, b.POD_Rate, b.POD_Quantity, "
    '        sSql = sSql & "b.POD_CST, Convert(money, b.POD_CSTAmount) As POD_CSTAmount, Convert(money, b.POD_RateAmount) As POD_RateAmount,"
    '        sSql = sSql & "Convert(money, b.POD_Discount)As POD_Discount,Convert(money,b.POD_DiscountAmount) As POD_DiscountAmount,"
    '        sSql = sSql & "Convert(money,b.POD_TotalAmount)As POD_TotalAmount,"
    '        sSql = sSql & "Convert(money,b.POD_VAT)As POD_VAT,Convert(money,b.POD_Excise)As POD_Excise,POD_Frieght,POD_FrieghtAmount,POD_ExciseAmount,Convert(money,b.POD_ExciseAmount)As POD_ExciseAmount,Convert(money,b.POD_VATAmount)As POD_VATAmount,b.POD_Unit, "
    '        sSql = sSql & "c.INV_Code,c.INV_Description,d.CSM_Name,d.CSM_Address,d.CSM_MobileNo,d.CSM_EmailID,e.Mas_Desc,m.CUST_CODE,m.CUST_Name,m.CUST_COMM_ADDRESS,m.CUST_EMAIL,m.CUST_COMM_TEL,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate "
    '        sSql = sSql & "From Purchase_Order_Master "
    '        sSql = sSql & "join Purchase_Order_Details b On POM_ID=" & iPomID & " And POM_ID=b.POD_MasterID And b.POD_Status <> 'D' "
    '        sSql = sSql & "Join Inventory_master_history InvH on  POD_HistoryID=InvH.InvH_ID "
    '        sSql = sSql & "Join Inventory_master c on  POD_DescriptionID=c.INV_ID "
    '        sSql = sSql & "Join CustomerSupplierMaster d On POM_Supplier=d.CSM_ID "
    '        sSql = sSql & "Join Acc_General_master e on b.POD_Unit=e.Mas_ID "
    '        sSql = sSql & "Join MST_CUSTOMER_MASTER m on b.POD_CompID=m.CUST_ID "
    '        dtFRomTable = objDB.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dtFRomTable.Rows.Count > 0 Then
    '            For i = 0 To dtFRomTable.Rows.Count - 1
    '                dRow = dt.NewRow()
    '                If IsDBNull(dtFRomTable.Rows(i)("POM_OrderNo")) = False Then
    '                    dRow("POM_OrderNo") = dtFRomTable.Rows(i)("POM_OrderNo")
    '                Else
    '                    dRow("POM_OrderNo") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POM_OrderDate")) = False Then
    '                    dRow("POM_OrderDate") = dtFRomTable.Rows(i)("POM_OrderDate")
    '                Else
    '                    dRow("POM_OrderDate") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_Commodity")) = False Then
    '                    dRow("POD_Commodity") = dtFRomTable.Rows(i)("POD_Commodity")
    '                Else
    '                    dRow("POD_Commodity") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_Rate")) = False Then
    '                    dRow("POD_Rate") = dtFRomTable.Rows(i)("POD_Rate")
    '                Else
    '                    dRow("POD_Rate") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_Quantity")) = False Then
    '                    dRow("POD_Quantity") = dtFRomTable.Rows(i)("POD_Quantity")
    '                Else
    '                    dRow("POD_Quantity") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_CST")) = False Then
    '                    dRow("POD_CST") = dtFRomTable.Rows(i)("POD_CST")
    '                Else
    '                    dRow("POD_CST") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_VAT")) = False Then
    '                    dRow("POD_VAT") = dtFRomTable.Rows(i)("POD_VAT")
    '                Else
    '                    dRow("POD_VAT") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_CSTAmount")) = False Then
    '                    dRow("POD_CSTAmount") = Convert.ToDouble(dtFRomTable.Rows(i)("POD_CSTAmount"))
    '                Else
    '                    dRow("POD_CSTAmount") = 0
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("POD_RateAmount")) = False Then
    '                    dRow("POD_RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dtFRomTable.Rows(i)("POD_RateAmount") - dtFRomTable.Rows(i)("POD_DiscountAmount"))))
    '                    subtotal = subtotal + dRow("POD_RateAmount")
    '                Else
    '                    dRow("POD_RateAmount") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_Discount")) = False Then
    '                    dRow("POD_Discount") = dtFRomTable.Rows(i)("POD_Discount")
    '                Else
    '                    dRow("POD_Discount") = 0
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("POD_DiscountAmount")) = False Then
    '                    dRow("POD_DiscountAmount") = dtFRomTable.Rows(i)("POD_DiscountAmount")
    '                Else
    '                    dRow("POD_DiscountAmount") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_TotalAmount")) = False Then
    '                    dRow("POD_TotalAmount") = dtFRomTable.Rows(i)("POD_TotalAmount")
    '                Else
    '                    dRow("POD_TotalAmount") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_VATAmount")) = False Then
    '                    dRow("POD_VATAmount") = dtFRomTable.Rows(i)("POD_VATAmount")
    '                Else
    '                    dRow("POD_VATAmount") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_Unit")) = False Then
    '                    dRow("POD_Unit") = dtFRomTable.Rows(i)("POD_Unit")
    '                Else
    '                    dRow("POD_Unit") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("INV_Code")) = False Then
    '                    dRow("INV_Code") = dtFRomTable.Rows(i)("INV_Code")
    '                Else
    '                    dRow("INV_Code") = ""
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("INV_Description")) = False Then
    '                    dRow("INV_Description") = dtFRomTable.Rows(i)("INV_Description")
    '                Else
    '                    dRow("INV_Description") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("CSM_Name")) = False Then
    '                    dRow("CSM_Name") = dtFRomTable.Rows(i)("CSM_Name")
    '                Else
    '                    dRow("CSM_Name") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("CSM_Address")) = False Then
    '                    dRow("CSM_Address") = dtFRomTable.Rows(i)("CSM_Address")
    '                Else
    '                    dRow("CSM_Address") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("CSM_Address")) = False Then
    '                    dRow("CSM_Address") = dtFRomTable.Rows(i)("CSM_Address")
    '                Else
    '                    dRow("CSM_Address") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("CSM_MobileNo")) = False Then
    '                    dRow("CSM_MobileNo") = dtFRomTable.Rows(i)("CSM_MobileNo")
    '                Else
    '                    dRow("CSM_MobileNo") = ""
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("CSM_EmailID")) = False Then
    '                    dRow("CSM_EmailID") = dtFRomTable.Rows(i)("CSM_EmailID")
    '                Else
    '                    dRow("CSM_EmailID") = ""
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("Mas_Desc")) = False Then
    '                    dRow("Mas_Desc") = dtFRomTable.Rows(i)("Mas_Desc")
    '                Else
    '                    dRow("Mas_Desc") = ""
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("CUST_CODE")) = False Then
    '                    dRow("CUST_CODE") = dtFRomTable.Rows(i)("CUST_CODE")
    '                Else
    '                    dRow("CUST_CODE") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("CUST_NAME")) = False Then
    '                    dRow("CUST_NAME") = dtFRomTable.Rows(i)("CUST_NAME")
    '                Else
    '                    dRow("CUST_NAME") = ""
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("CUST_COMM_ADDRESS")) = False Then
    '                    dRow("CUST_COMM_ADDRESS") = dtFRomTable.Rows(i)("CUST_COMM_ADDRESS")
    '                Else
    '                    dRow("CUST_COMM_ADDRESS") = ""
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("CUST_EMAIL")) = False Then
    '                    dRow("CUST_EMAIL") = dtFRomTable.Rows(i)("CUST_EMAIL")
    '                Else
    '                    dRow("CUST_EMAIL") = ""
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("CUST_COMM_TEL")) = False Then
    '                    dRow("CUST_COMM_TEL") = dtFRomTable.Rows(i)("CUST_COMM_TEL")
    '                Else
    '                    dRow("CUST_COMM_TEL") = ""
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("INVH_MRP")) = False Then
    '                    dRow("INVH_MRP") = dtFRomTable.Rows(i)("INVH_MRP")
    '                Else
    '                    dRow("INVH_MRP") = 0
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("INVH_Mdate")) = False Then
    '                    dRow("INVH_Mdate") = dtFRomTable.Rows(i)("INVH_Mdate")
    '                Else
    '                    dRow("INVH_Mdate") = 0
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("INVH_Edate")) = False Then
    '                    dRow("INVH_Edate") = dtFRomTable.Rows(i)("INVH_Edate")
    '                Else
    '                    dRow("INVH_Edate") = 0
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("POD_Excise")) = False Then
    '                    dRow("POD_Excise") = dtFRomTable.Rows(i)("POD_Excise")
    '                Else
    '                    dRow("POD_Excise") = 0
    '                End If
    '                If IsDBNull(dtFRomTable.Rows(i)("POD_ExciseAmount")) = False Then
    '                    dRow("POD_ExciseAmount") = dtFRomTable.Rows(i)("POD_ExciseAmount")
    '                Else
    '                    dRow("POD_ExciseAmount") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_Frieght")) = False Then
    '                    dRow("POD_Frieght") = dtFRomTable.Rows(i)("POD_Frieght")
    '                Else
    '                    dRow("POD_Frieght") = 0
    '                End If

    '                If IsDBNull(dtFRomTable.Rows(i)("POD_FrieghtAmount")) = False Then
    '                    dRow("POD_FrieghtAmount") = dtFRomTable.Rows(i)("POD_FrieghtAmount")
    '                Else
    '                    dRow("POD_FrieghtAmount") = 0
    '                End If
    '                dt.Rows.Add(dRow)
    '            Next
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    'Public Shared Function RemoveDublicate(ByVal dt As DataTable) As DataTable
    '    Dim sSql As String = ""
    '    Dim hTable As New Hashtable
    '    Dim duplicateList As New ArrayList
    '    Try
    '        For Each DataRow As DataRow In dt.Rows
    '            If (hTable.Contains(DataRow("POD_VAT"))) Then
    '                duplicateList.Add(DataRow)
    '            Else
    '                hTable.Add(DataRow("POD_VAT"), String.Empty)
    '            End If
    '        Next
    '        For Each DataRow As DataRow In duplicateList
    '            dt.Rows.Remove(DataRow)
    '        Next
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadPurchase_OderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPodID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_details where POD_MasterID in(select POM_ID from Purchase_Order_master where POM_ID=" & iPodID & " and  POM_CompID=" & iCompID & " and POM_YearID=" & iYearID & ")  and POD_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal OrderID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String
        Dim Total As String
        Try
            sSql = "select CSM_ID from CustomerSupplierMaster where CSM_ID in(select POM_Supplier from Purchase_Order_Master where POM_ID= " & OrderID & " And POM_YearID =" & iYearID & " And POM_CompID =" & iCompID & ") And CSM_CompID =" & iCompID & ""
            Total = objDB.SQLGetDescription(sNameSpace, sSql)
            Return Total
        Catch ex As Exception
        End Try
    End Function
    Public Function SavePurchaseJournalMaster(ByVal sNameSpace As String, ByVal objOral As clsOralOrder) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_TransactionNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_Party)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_Location)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_BillType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objGen.SafeSQL(objOral.sAcc_JE_BillNo))
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dAcc_JE_BillDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dAcc_JE_BillAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dAcc_JE_AdvanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceNaration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_AdvanceNaration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BalanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dAcc_JE_BalanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_NetAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dAcc_JE_NetAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PaymentNarration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_PaymentNarration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeNo", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_ChequeNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dAcc_JE_ChequeDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IFSCCode", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_IFSCCode)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BankName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_BankName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BranchName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_BranchName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_CreatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillCreatedDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dAcc_JE_BillCreatedDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_UpdatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_UpdatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_InvoiceID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iAcc_JE_InvoiceID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PendingAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dAcc_JE_PendingAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Type", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sAcc_JE_Type)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spAcc_Purchase_JE_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal objOral As clsOralOrder) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_ZoneID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_RegionID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_AreaID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objOral.iATD_BranchID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objOral.dATD_OpenDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objOral.dATD_OpenCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objOral.dATD_ClosingDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objOral.dATD_ClosingCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objOral.iATD_SeqReferenceNum
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spAcc_Transactions_Details", 1, Arr, ObjParam)
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
            ds = objDB.SQLExecuteDataSet(sNameSpace, sSql)
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

                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLCode").ToString()) = False Then
                        dr("SubGL") = ds.Tables(0).Rows(i)("SubGLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLDesc").ToString()) = False Then
                        dr("SubGLDescription") = ds.Tables(0).Rows(i)("SubGLDesc").ToString()

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
    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iExistInwardID As Integer) As Integer
        Dim sSql As String = ""
        Dim i As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_GIN_master where PGM_ID=" & iExistInwardID & " And PGM_CompID=" & iCompID & " And PGM_YearID =" & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & "Update Purchase_GIN_master Set PGM_Status='A',PGM_ApprovedBy=" & iUserID & ",PGM_ApprovedOn=GetDate() where pgm_id = " & dt.Rows(i)("pgm_id") & ""
                    sSql = sSql & " End"
                    objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
            sSql = "" : sSql = "Select * from Purchase_GIN_details  Where PGD_MasterID=" & iExistInwardID & " and PGD_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & "Update Purchase_GIN_details Set PGD_Status='A',PGM_ApprovedBy=" & iUserID & ",PGM_ApprovedOn=GetDate() where pgd_id = " & dt.Rows(i)("pgd_id") & ""
                    sSql = sSql & " End"
                    objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
            sSql = "" : sSql = "Select * from Purchase_Order_Master  Where POM_ID=" & iExistInwardID & " and POM_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & "update Purchase_Order_Master set  POM_Status='A',POM_ApporvedBy=" & iUserID & ",POM_ApprovedOn=GetDate() where POM_ID = " & dt.Rows(i)("POM_ID") & ""
                    sSql = sSql & " End"
                    objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransactionDetailsPI(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iExtOrderNo As Integer) As DataTable
        Dim dt, dt1 As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim vat As Decimal = 0
        Dim cst As Decimal = 0
        Dim sSql As String = ""
        Dim totAmount As Decimal = 0
        Try

            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("RateAmount")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")
            'dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("FinalTotal")

            'sSql = "Select * From Purchase_Invoice_Accepted Where PIA_OrderID=" & OrderNo & " and PIA_GINID=" & iGINID & " and PIA_YearID=" & iYearID & " and PIA_CompID=" & iCompID & ""
            'sSql = "Select * From purchase_order_details Where POD_MasterID=" & iExtOrderNo & " and POD_CompID=" & iCompID & ""
            sSql = "Select * From purchase_order_details Where POD_MasterID in (select POM_ID from Purchase_Order_master where POM_ID=" & iExtOrderNo & " and  POM_CompID=" & iCompID & " and POM_YearID=" & iYearID & ") and POD_CompID=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)

            For i = 0 To dt.Rows.Count - 1
                If (dt.Rows(i)("POD_AcceptedQty") <> 0) Then
                    dr = dtTab.NewRow
                    dr("CommodityID") = dt.Rows(i)("POD_Commodity")
                    dr("ItemID") = dt.Rows(i)("POD_DescriptionID")
                    dr("HistoryID") = dt.Rows(i)("POD_HistoryID")

                    dr("Goods") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_master Where INV_ID =" & dt.Rows(i)("POD_DescriptionID") & " And INV_Parent=" & dt.Rows(i)("POD_Commodity") & " And Inv_CompID =" & iCompID & "")
                    dr("StdUnit") = objDB.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("POD_Unit") & " And Mas_CompID =" & iCompID & "")
                    dr("Quantity") = dt.Rows(i)("POD_Quantity")
                    dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(Date.Today, "D")
                    dr("Rate") = dt.Rows(i)("POD_Rate")
                    dr("Charges") = 0
                    dr("RateAmount") = dt.Rows(i)("POD_RateAmount")

                    If dt.Rows(i)("POD_Discount") > 0 Then
                        dr("Discount") = dt.Rows(i)("POD_Discount")
                        dr("DiscountAmount") = dt.Rows(i)("POD_DiscountAmount")
                    Else
                        dr("Discount") = 0
                        dr("DiscountAmount") = 0
                    End If
                    dr("Amount") = dt.Rows(i)("POD_RateAmount") - dt.Rows(i)("POD_DiscountAmount")
                    dr("GSTID") = dt.Rows(i)("POD_GST_ID")
                    dr("GSTRate") = dt.Rows(i)("POD_GSTRate")
                    dr("GSTAmount") = dt.Rows(i)("POD_GSTAmount")
                    'dr("Remarks") = dt.Rows(i)("PID_Remarks")
                    dr("FinalTotal") = dt.Rows(i)("POD_TotalAmount")
                    dtTab.Rows.Add(dr)
                End If
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveStockLedger(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sIPAddress As String, ByVal iUserID As Integer, ByVal dt As DataTable, ByVal OrderNo As String, ByVal InwardNo As String) As Integer
        Dim sSql As String = ""
        Dim iMaxid As Integer
        Try
            For i = 0 To dt.Rows.Count - 1
                If (objDB.SQLCheckForRecord(sNameSpace, "Select * from Stock_Ledger where SL_historyId=" & dt.Rows(i)("HistoryID") & " And SL_ItemID = " & dt.Rows(i)("ItemID") & " And SL_YearID =" & iYearID & " And SL_CompID =" & iCompID & " And SL_OrderID=" & OrderNo & " And SL_GINID=" & InwardNo & "")) Then
                    objDB.SQLExecuteNonQuery(sNameSpace, "Update Stock_Ledger Set SL_Operation='U',SL_IPAddress='" & sIPAddress & "',SL_ClosingBalanceQty=SL_ClosingBalanceQty+" & dt.Rows(i)("Quantity") & ", SL_PurchaseQty = SL_PurchaseQty + " & dt.Rows(i)("Quantity") & " where  SL_historyId=" & dt.Rows(i)("HistoryID") & " And SL_ItemID = " & dt.Rows(i)("ItemID") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " and SL_GINID=" & InwardNo & "")
                Else
                    iMaxid = objDB.SQLExecuteScalar(sNameSpace, "Select isnull(max(SL_ID) + 1, 1) from Stock_Ledger")
                    sSql = "Insert Into Stock_Ledger (SL_ID,SL_Commodity,SL_Date,SL_ItemID,sl_PurchaseQty,SL_ClosingBalanceQty,SL_CompID,SL_YearID,SL_CrBy,SL_CrOn,SL_historyId,SL_Operation,SL_IPAddress,SL_Vat,SL_Exciese,SL_Cst,SL_OrderID,SL_SaleQnty,PurchaseRate,SL_OpeningBalanceQty,SL_GINID)"
                    sSql = sSql & "Values(" & iMaxid & "," & dt.Rows(i)("CommodityID") & ", GetDate()," & dt.Rows(i)("ItemID") & "," & dt.Rows(i)("Quantity") & "," & dt.Rows(i)("Quantity") & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate()," & dt.Rows(i)("HistoryID") & ",'C','" & sIPAddress & "','0','0','0','" & OrderNo & "',0,'" & dt.Rows(i)("Rate") & "',0," & InwardNo & ")"
                    objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetTotalCharge(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer, ByVal iGinID As Integer) As String
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "Select sum(c_chargeamount) From Charges_Master,Purchase_Order_Master Where C_POrderID=POM_ID And C_PSType='P' And C_POrderID=" & iOrderID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
    '        GetTotalCharge = objDB.SQLGetDescription(sNameSpace, sSql)
    '        Return GetTotalCharge
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetTotalCharge(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer, ByVal iGinID As Integer) As String
        Dim sSql As String = ""
        Dim dcheck As New Boolean
        Dim dSum As Double
        Try
            'sSql = "Select sum(c_chargeamount) From Charges_Master,Purchase_Order_Master Where C_POrderID=POM_ID And C_PSType='P' And C_POrderID=" & iOrderID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            'GetTotalCharge = objDB.SQLGetDescription(sNameSpace, sSql)
            'Return GetTotalCharge
            sSql = "Select * From Charges_Master,Purchase_Order_Master Where C_POrderID=POM_ID And C_PSType='P' And C_POrderID=" & iOrderID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            dcheck = objDB.SQLCheckForRecord(sNameSpace, sSql)
            If dcheck = True Then
                sSql = "Select sum(c_chargeamount) From Charges_Master,Purchase_Order_Master Where C_POrderID=POM_ID And C_PSType='P' And C_POrderID=" & iOrderID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
                dSum = objDB.SQLExecuteScalar(sNameSpace, sSql)
                Return dSum
            Else
                dSum = 0
                Return dSum
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGstCategory As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select GC_GSTCategory from GSTcategory_table where GC_ID=" & iGstCategory & " and GC_CompID=" & iCompID & ""
            GetGSTDescription = objDB.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getBranchFromPO(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPodID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select POM_BranchID from  purchase_order_master where POM_ID=" & iPodID & " and POM_CompID=" & iCompID & ""
            getBranchFromPO = objDB.SQLExecuteScalar(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckDetailsofBranchState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPodID As Integer) As String
        Dim sSql As String = ""
        Dim iPOMBranchID As Integer : Dim iCompBrnchID As Integer
        Try
            sSql = "Select POM_BranchID from  purchase_order_master where POM_ID=" & iPodID & " and POM_CompID=" & iCompID & ""
            iPOMBranchID = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select CUSTB_STATE from MST_CUSTOMER_MASTER_Branch where CUSTB_Name='" & iPOMBranchID & "' and CUSTB_CompID=" & iCompID & ""
            iCompBrnchID = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select Mas_desc From ACC_General_Master Where Mas_Id = " & iCompBrnchID & " And Mas_master=4 and Mas_CompID = " & iCompID & ""
            CheckDetailsofBranchState = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDetailsofCompState(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = ""
        Dim iStateID As Integer
        Try
            sSql = "Select CUST_COMM_STATE from MST_Customer_Master where CUST_ID = " & iCompID & " and CUST_CompID =" & iCompID & " "
            iStateID = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select Mas_desc From ACC_General_Master Where Mas_Id = " & iStateID & " And Mas_master=4 and Mas_CompID = " & iCompID & ""
            CheckDetailsofCompState = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGSTNRegNo As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select GR_StateName From GSTN_RegNo_Master Where GR_TIN='" & sGSTNRegNo & "' And GR_CompID=" & iCompID & " "
            GetState = objDB.SQLGetDescription(sNameSpace, sSql).ToString
            Return GetState
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SElect GL_ID From Chart_Of_Accounts Where GL_Desc Like '%" & sDesc & "%' And GL_CompID=" & iCompID & " "
            GetGLID = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindGSTRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & iCompID & " "
            BindGSTRates = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindGSTRates
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTRateFromHSNTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Double
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_GSTRate From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTRateFromHSNTable = objDB.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTRateFromHSNTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getOrgParent(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iNodeID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Parent From Sad_Org_Structure Where Org_Node=" & iNodeID & " And Org_CompID=" & iCompID & " "
            getOrgParent = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return getOrgParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDefaultBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Node From Sad_Org_Structure Where Org_LevelCode=4 And Org_Default=1 And Org_CompID=" & iCompID & " "
            GetDefaultBranch = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDefaultBranch
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
