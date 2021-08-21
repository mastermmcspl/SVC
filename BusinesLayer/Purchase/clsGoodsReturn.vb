Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsGoodsReturn

    Private objDb As New DBHelper
    Private objFasgnrl As New clsFASGeneral
    Private objFasAllgnrl As New clsGeneralFunctions
    Private objFAS As New clsFASGeneral
    Dim objGen As New clsFASGeneral

    Private PRM_ID As Integer
    Private PRM_OrderDate As Date
    Private PRM_ReturnNo As String
    Private PRM_ReturnRefNo As String
    Private PRM_OrderID As String
    Private PRM_GINInvID As String
    Private PRM_GINInvNo As String
    Private PRM_InwardNo As String
    Private PRM_Remarks As String
    Private PRM_Status As String
    Private PRM_Supplier As Integer
    Private PRM_DSchdule As Integer
    Private PRM_ModeOfShipping As Integer
    Private PRM_MethodofPayment As Integer
    Private PRM_Paymentterms As Integer
    Private PRM_CreatedBy As Integer
    Private PRM_ApporvedBy As Integer
    Private PRM_YearID As Integer
    Private PRD_ID As Integer
    Private PRD_MasterID As Integer
    Private PRD_Commodity As Integer
    Private PRD_DescriptionID As Integer
    Private PRD_HistoryID As Integer
    Private PRD_Unit As Integer
    Private PRD_Rate As String
    Private PRM_CSTCtgry As Integer
    Private PRM_SaleType As Integer
    Private PRD_Quantity As String
    Private PRD_RateAmount As String
    Private PRD_Discount As String
    Private PRD_DiscountAmount As String
    Private PRD_Excise As String
    Private PRD_ExciseAmount As String
    Private PRD_Frieght As String
    Private PRD_FrieghtAmount As String
    Private PRD_VAT As String
    Private PRD_VATAmount As String
    Private PRD_CST As String
    Private PRD_CSTAmount As String
    Private PRD_RequiredDate As Date
    Private PRD_TotalAmount As String
    Private PRD_CompID As Integer
    Private PRM_DocRef As String
    Private PRD_Rejected As Integer
    Private PRD_Accepted As Integer
    Private PRD_ReceivedQty As Integer
    Private PRD_OrderedQty As Integer
    Private PRD_IPAddress As String
    Private BatchNumber As String
    Private PRM_DEliveryChlnNo As String
    Private PRM_TypeOfReturn As Integer

    Private GRD_UnitID As Integer
    Private GRD_Rate As String
    Private GRD_Amount As String
    Private GRD_Total As String
    Private GRD_Quantity As Integer
    Private GRD_Discount As String
    Private GRD_DiscountAmount As String
    Private GRD_TotalAmount As String
    Private GRD_ChargesPerItem As String
    Private GRD_GST_ID As Integer
    Private GRD_GSTRate As String
    Private GRD_GSTAmount As String
    Private GRD_SGST As String
    Private GRD_SGSTAmount As String
    Private GRD_CGST As String
    Private GRD_CGSTAmount As String
    Private GRD_IGST As String
    Private GRD_IGSTAmount As String
    Private GRM_InvoiceSatus As String
    Private GRM_State As String
    Private GRM_GRID As Integer
    Private GRM_PRID As Integer

    Private Acc_JE_ID As Integer
    Private Acc_JE_TransactionNo As String
    Private Acc_JE_Party As Integer
    Private Acc_JE_Location As Integer
    Private Acc_JE_BillType As Integer
    Private Acc_JE_BillNo As String
    Private Acc_JE_BillDate As Date
    Private Acc_JE_BillAmount As Decimal
    Private Acc_JE_AdvanceAmount As Decimal
    Private Acc_JE_PendingAmount As Decimal
    Private Acc_JE_Type As String
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
    Private ATD_UpdatedOn As Date
    Private ATD_UpdatedBy As Integer

    Private ATD_ZoneID As Integer
    Private ATD_RegionID As Integer
    Private ATD_AreaID As Integer
    Private ATD_BranchID As Integer

    Private iC_ID As Integer
    Private iC_POrderID As Integer
    Private iC_GoodsReturnID As Integer
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

    Public Property iAcc_JE_Type() As String
        Get
            Return (Acc_JE_Type)
        End Get
        Set(ByVal Value As String)
            Acc_JE_Type = Value
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
    Public Property iAcc_JE_PendingAmount() As Decimal
        Get
            Return (Acc_JE_PendingAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_PendingAmount = Value
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




    Public Property sGRM_State() As String
        Get
            Return (GRM_State)
        End Get
        Set(ByVal Value As String)
            GRM_State = Value
        End Set
    End Property
    Public Property sGRM_InvoiceSatus() As String
        Get
            Return (GRM_InvoiceSatus)
        End Get
        Set(ByVal Value As String)
            GRM_InvoiceSatus = Value
        End Set
    End Property
    Public Property sGRD_SGST() As String
        Get
            Return (GRD_SGST)
        End Get
        Set(ByVal Value As String)
            GRD_SGST = Value
        End Set
    End Property
    Public Property sGRD_SGSTAmount() As String
        Get
            Return (GRD_SGSTAmount)
        End Get
        Set(ByVal Value As String)
            GRD_SGSTAmount = Value
        End Set
    End Property
    Public Property sGRD_CGST() As String
        Get
            Return (GRD_CGST)
        End Get
        Set(ByVal Value As String)
            GRD_CGST = Value
        End Set
    End Property
    Public Property sGRD_CGSTAmount() As String
        Get
            Return (GRD_CGSTAmount)
        End Get
        Set(ByVal Value As String)
            GRD_CGSTAmount = Value
        End Set
    End Property
    Public Property sGRD_IGST() As String
        Get
            Return (GRD_IGST)
        End Get
        Set(ByVal Value As String)
            GRD_IGST = Value
        End Set
    End Property
    Public Property sGRD_IGSTAmount() As String
        Get
            Return (GRD_IGSTAmount)
        End Get
        Set(ByVal Value As String)
            GRD_IGSTAmount = Value
        End Set
    End Property
    Public Property sGRD_UnitID() As Integer
        Get
            Return (GRD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            GRD_UnitID = Value
        End Set
    End Property
    Public Property sGRD_Total() As String
        Get
            Return (GRD_Total)
        End Get
        Set(ByVal Value As String)
            GRD_Total = Value
        End Set
    End Property
    Public Property sGRD_Rate() As String
        Get
            Return (GRD_Rate)
        End Get
        Set(ByVal Value As String)
            GRD_Rate = Value
        End Set
    End Property
    Public Property sGRD_Amount() As String
        Get
            Return (GRD_Amount)
        End Get
        Set(ByVal Value As String)
            GRD_Amount = Value
        End Set
    End Property
    Public Property sGRD_Quantity() As Integer
        Get
            Return (GRD_Quantity)
        End Get
        Set(ByVal Value As Integer)
            GRD_Quantity = Value
        End Set
    End Property
    Public Property sGRD_Discount() As String
        Get
            Return (GRD_Discount)
        End Get
        Set(ByVal Value As String)
            GRD_Discount = Value
        End Set
    End Property
    Public Property sGRD_DiscountAmount() As String
        Get
            Return (GRD_DiscountAmount)
        End Get
        Set(ByVal Value As String)
            GRD_DiscountAmount = Value
        End Set
    End Property
    Public Property sGRD_TotalAmount() As String
        Get
            Return (GRD_TotalAmount)
        End Get
        Set(ByVal Value As String)
            GRD_TotalAmount = Value
        End Set
    End Property
    Public Property sGRD_ChargesPerItem() As String
        Get
            Return (GRD_ChargesPerItem)
        End Get
        Set(ByVal Value As String)
            GRD_ChargesPerItem = Value
        End Set
    End Property
    Public Property sGRD_GST_ID() As Integer
        Get
            Return (GRD_GST_ID)
        End Get
        Set(ByVal Value As Integer)
            GRD_GST_ID = Value
        End Set
    End Property
    Public Property sGRD_GSTRate() As String
        Get
            Return (GRD_GSTRate)
        End Get
        Set(ByVal Value As String)
            GRD_GSTRate = Value
        End Set
    End Property
    Public Property sGRD_GSTAmount() As String
        Get
            Return (GRD_GSTAmount)
        End Get
        Set(ByVal Value As String)
            GRD_GSTAmount = Value
        End Set
    End Property

    Public Property sPRM_DEliveryChlnNo() As String
        Get
            Return (PRM_DEliveryChlnNo)
        End Get
        Set(ByVal Value As String)
            PRM_DEliveryChlnNo = Value
        End Set
    End Property
    Public Property sPRD_IPAddress() As String
        Get
            Return (PRD_IPAddress)
        End Get
        Set(ByVal Value As String)
            PRD_IPAddress = Value
        End Set
    End Property
    Public Property iPRD_Accepted() As Integer
        Get
            Return (PRD_Accepted)
        End Get
        Set(ByVal Value As Integer)
            PRD_Accepted = Value
        End Set
    End Property
    Public Property iPRD_Rejected() As Integer
        Get
            Return (PRD_Rejected)
        End Get
        Set(ByVal Value As Integer)
            PRD_Rejected = Value
        End Set
    End Property
    Public Property sPRM_DocRef() As String
        Get
            Return (PRM_DocRef)
        End Get
        Set(ByVal Value As String)
            PRM_DocRef = Value
        End Set
    End Property
    Public Property sPRD_RateAmount() As String
        Get
            Return (PRD_RateAmount)
        End Get
        Set(ByVal Value As String)
            PRD_RateAmount = Value
        End Set
    End Property
    Public Property sPRD_TotalAmount() As String
        Get
            Return (PRD_TotalAmount)
        End Get
        Set(ByVal Value As String)
            PRD_TotalAmount = Value
        End Set
    End Property
    Public Property dPRD_RequiredDate() As DateTime
        Get
            Return (PRD_RequiredDate)
        End Get
        Set(ByVal Value As DateTime)
            PRD_RequiredDate = Value
        End Set
    End Property
    Public Property sPRD_CSTAmount() As String
        Get
            Return (PRD_CSTAmount)
        End Get
        Set(ByVal Value As String)
            PRD_CSTAmount = Value
        End Set
    End Property
    Public Property sPRD_CST() As String
        Get
            Return (PRD_CST)
        End Get
        Set(ByVal Value As String)
            PRD_CST = Value
        End Set
    End Property
    Public Property sPRD_VATAmount() As String
        Get
            Return (PRD_VATAmount)
        End Get
        Set(ByVal Value As String)
            PRD_VATAmount = Value
        End Set
    End Property
    Public Property sPRD_VAT() As String
        Get
            Return (PRD_VAT)
        End Get
        Set(ByVal Value As String)
            PRD_VAT = Value
        End Set
    End Property
    Public Property sPRD_ExciseAmount() As String
        Get
            Return (PRD_ExciseAmount)
        End Get
        Set(ByVal Value As String)
            PRD_ExciseAmount = Value
        End Set
    End Property
    Public Property sPRD_Excise() As String
        Get
            Return (PRD_Excise)
        End Get
        Set(ByVal Value As String)
            PRD_Excise = Value
        End Set
    End Property

    Public Property sPRD_Frieght() As String
        Get
            Return (PRD_Frieght)
        End Get
        Set(ByVal Value As String)
            PRD_Frieght = Value
        End Set
    End Property

    Public Property sPRD_FrieghtAmount() As String
        Get
            Return (PRD_FrieghtAmount)
        End Get
        Set(ByVal Value As String)
            PRD_FrieghtAmount = Value
        End Set
    End Property


    Public Property iPRD_ReceivedQty() As Integer
        Get
            Return (PRD_ReceivedQty)
        End Get
        Set(ByVal Value As Integer)
            PRD_ReceivedQty = Value
        End Set
    End Property

    Public Property iPRD_OrderedQty() As Integer
        Get
            Return (PRD_OrderedQty)
        End Get
        Set(ByVal Value As Integer)
            PRD_OrderedQty = Value
        End Set
    End Property
    Public Property sPRD_DiscountAmount() As String
        Get
            Return (PRD_DiscountAmount)
        End Get
        Set(ByVal Value As String)
            PRD_DiscountAmount = Value
        End Set
    End Property

    Public Property sPRD_Discount() As String
        Get
            Return (PRD_Discount)
        End Get
        Set(ByVal Value As String)
            PRD_Discount = Value
        End Set
    End Property
    Public Property sPRD_Quantity() As String
        Get
            Return (PRD_Quantity)
        End Get
        Set(ByVal Value As String)
            PRD_Quantity = Value
        End Set
    End Property

    Public Property sPRD_Rate() As String
        Get
            Return (PRD_Rate)
        End Get
        Set(ByVal Value As String)
            PRD_Rate = Value
        End Set
    End Property
    Public Property iPRD_Unit() As Integer
        Get
            Return (PRD_Unit)
        End Get
        Set(ByVal Value As Integer)
            PRD_Unit = Value
        End Set
    End Property
    Public Property iPRD_HistoryID() As Integer
        Get
            Return (PRD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            PRD_HistoryID = Value
        End Set
    End Property
    Public Property iPRD_DescriptionID() As Integer
        Get
            Return (PRD_DescriptionID)
        End Get
        Set(ByVal Value As Integer)
            PRD_DescriptionID = Value
        End Set
    End Property

    Public Property iPRD_Commodity() As Integer
        Get
            Return (PRD_Commodity)
        End Get
        Set(ByVal Value As Integer)
            PRD_Commodity = Value
        End Set
    End Property
    Public Property iPRD_MasterID() As Integer
        Get
            Return (PRD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            PRD_MasterID = Value
        End Set
    End Property

    Public Property iPRD_ID() As Integer
        Get
            Return (PRD_ID)
        End Get
        Set(ByVal Value As Integer)
            PRD_ID = Value
        End Set
    End Property

    Public Property iPRM_YearID() As Integer
        Get
            Return (PRM_YearID)
        End Get
        Set(ByVal Value As Integer)
            PRM_YearID = Value
        End Set
    End Property

    Public Property iPRM_SaleType() As Integer
        Get
            Return (PRM_SaleType)
        End Get
        Set(ByVal Value As Integer)
            PRM_SaleType = Value
        End Set
    End Property

    Public Property iPRM_iCSTCtgry() As Integer
        Get
            Return (PRM_CSTCtgry)
        End Get
        Set(ByVal Value As Integer)
            PRM_CSTCtgry = Value
        End Set
    End Property
    Public Property iPRM_ApporvedBy() As Integer
        Get
            Return (PRM_ApporvedBy)
        End Get
        Set(ByVal Value As Integer)
            PRM_ApporvedBy = Value
        End Set
    End Property
    Public Property iPRM_CreatedBy() As Integer
        Get
            Return (PRM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            PRM_CreatedBy = Value
        End Set
    End Property
    Public Property iPRM_MethodofPayment() As Integer
        Get
            Return (PRM_MethodofPayment)
        End Get
        Set(ByVal Value As Integer)
            PRM_MethodofPayment = Value
        End Set
    End Property

    Public Property iPRM_DSchdule() As Integer
        Get
            Return (PRM_DSchdule)
        End Get
        Set(ByVal Value As Integer)
            PRM_DSchdule = Value
        End Set
    End Property
    Public Property iPRM_Paymentterms() As Integer
        Get
            Return (PRM_Paymentterms)
        End Get
        Set(ByVal Value As Integer)
            PRM_Paymentterms = Value
        End Set
    End Property


    Public Property iPRM_ModeOfShipping() As Integer
        Get
            Return (PRM_ModeOfShipping)
        End Get
        Set(ByVal Value As Integer)
            PRM_ModeOfShipping = Value
        End Set
    End Property
    Public Property iPRM_Supplier() As Integer
        Get
            Return (PRM_Supplier)
        End Get
        Set(ByVal Value As Integer)
            PRM_Supplier = Value
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
    Public Property sPRM_ReturnNo() As String
        Get
            Return (PRM_ReturnNo)
        End Get
        Set(ByVal Value As String)
            PRM_ReturnNo = Value
        End Set
    End Property
    Public Property sGRM_GRID() As Integer
        Get
            Return (GRM_GRID)
        End Get
        Set(ByVal Value As Integer)
            GRM_GRID = Value
        End Set
    End Property
    Public Property sGRM_PRID() As Integer
        Get
            Return (GRM_PRID)
        End Get
        Set(ByVal Value As Integer)
            GRM_PRID = Value
        End Set
    End Property

    Public Property sPRM_ReturnRefNo() As String
        Get
            Return (PRM_ReturnRefNo)
        End Get
        Set(ByVal Value As String)
            PRM_ReturnRefNo = Value
        End Set
    End Property
    Public Property sPRM_OrderID() As String
        Get
            Return (PRM_OrderID)
        End Get
        Set(ByVal Value As String)
            PRM_OrderID = Value
        End Set
    End Property
    Public Property sPRM_GINInvID() As String
        Get
            Return (PRM_GINInvID)
        End Get
        Set(ByVal Value As String)
            PRM_GINInvID = Value
        End Set
    End Property
    Public Property sPRM_GINInvNo() As String
        Get
            Return (PRM_GINInvNo)
        End Get
        Set(ByVal Value As String)
            PRM_GINInvNo = Value
        End Set
    End Property
    Public Property sPRM_Status() As String
        Get
            Return (PRM_Status)
        End Get
        Set(ByVal Value As String)
            PRM_Status = Value
        End Set
    End Property

    Public Property sPRM_InwardNo() As String
        Get
            Return (PRM_InwardNo)
        End Get
        Set(ByVal Value As String)
            PRM_InwardNo = Value
        End Set
    End Property

    Public Property dPRM_OrderDate() As Date
        Get
            Return (PRM_OrderDate)
        End Get
        Set(ByVal Value As Date)
            PRM_OrderDate = Value
        End Set
    End Property
    Public Property iPRM_ID() As Integer
        Get
            Return (PRM_ID)
        End Get
        Set(ByVal Value As Integer)
            PRM_ID = Value
        End Set
    End Property
    Public Property iPRM_TypeOfReturn() As Integer
        Get
            Return (PRM_TypeOfReturn)
        End Get
        Set(ByVal Value As Integer)
            PRM_TypeOfReturn = Value
        End Set
    End Property

    Public Property sPRM_Remarks() As String
        Get
            Return (PRM_Remarks)
        End Get
        Set(ByVal Value As String)
            PRM_Remarks = Value
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
    Public Property C_GoodsReturnID() As Integer
        Get
            Return (iC_GoodsReturnID)
        End Get
        Set(ByVal Value As Integer)
            iC_GoodsReturnID = Value
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

    Public Function GetCOAHeadID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_AccHead from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDb.SQLExecuteScalar(sNameSpace, sSql)
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
            iCOA = objDb.SQLExecuteScalar(sNameSpace, sSql)
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
            sGL = objDb.SQLGetDescription(sNameSpace, sSql)
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
            sGL = objDb.SQLGetDescription(sNameSpace, sSql)
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
            iCOA = objDb.SQLExecuteScalar(sNameSpace, sSql)
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
            iCOA = objDb.SQLExecuteScalar(sNameSpace, sSql)
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
            'sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & iPartyID & " And CSM_CompID=" & iCompID & " and CSM_Delflag='A' "
            sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            GetPartySubGLID = objDb.SQLExecuteScalarInt(sNameSpace, sSql)
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
            'sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & iPartyID & " And CSM_CompID=" & iCompID & " and CSM_Delflag='A' "
            sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            sGL = objDb.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSupplierID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal OrderID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String
        Dim Total As String
        Try
            sSql = "select CSM_ID from CustomerSupplierMaster where CSM_ID in(select POM_Supplier from Purchase_Order_Master where POM_ID= " & OrderID & " And POM_YearID =" & iYearID & " And POM_CompID =" & iCompID & ") And CSM_CompID =" & iCompID & ""
            Total = objDb.SQLGetDescription(sNameSpace, sSql)
            Return Total
        Catch ex As Exception
        End Try
    End Function

    Public Function SavePurchaseJournalMaster(ByVal sNameSpace As String, ByVal objVerification As clsGoodsReturn) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_TransactionNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_Party)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_Location)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_BillType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objGen.SafeSQL(objVerification.sAcc_JE_BillNo))
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dAcc_JE_BillDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dAcc_JE_BillAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dAcc_JE_AdvanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceNaration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_AdvanceNaration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BalanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dAcc_JE_BalanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_NetAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dAcc_JE_NetAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PaymentNarration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_PaymentNarration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeNo", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_ChequeNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dAcc_JE_ChequeDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IFSCCode", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_IFSCCode)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BankName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_BankName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BranchName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_BranchName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_CreatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sAcc_JE_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillCreatedDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dAcc_JE_BillCreatedDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_UpdatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_UpdatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_InvoiceID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_InvoiceID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PendingAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_PendingAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Type", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iAcc_JE_Type)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spAcc_Purchase_JE_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal objVerification As clsGoodsReturn) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_ZoneID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_RegionID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_AreaID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_BranchID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dATD_OpenDebit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dATD_OpenCredit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dATD_ClosingDebit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.dATD_ClosingCredit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objVerification.iATD_SeqReferenceNum)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spAcc_Transactions_Details", 1, Arr, ObjParam)
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

            ds = objDb.SQLExecuteDataSet(sNameSpace, sSql)
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

    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select GRM_ID,GRM_ReturnNo from Goods_Return_Master where GRM_CompID=" & iCompID & " and GRM_YearID = " & iYearID & " order by GRM_ID desc"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindInvoiceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal OrderID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select pgm_id, pgm_documentrefno from Purchase_GIN_Master where pgm_orderid = " & OrderID & " And PGM_CompID=" & iCompID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getInvoiceDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As String, ByVal orderNo As Integer, ByVal YearID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select PGM_InvoiceDate from  Purchase_GIN_Master where PGM_DocumentRefNo ='" & InvoiceNum & "' and PGM_YearID =" & YearID & " and PGM_CompID =" & iCompID & ""
            Return objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As Integer, ByVal orderNo As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_ID in(select PGD_DescriptionID from Purchase_GIN_details where PGD_MasterID=" & InvoiceNum & " and PGD_OrderID=" & orderNo & " and PGD_RejectedQnt > 0) and Inv_CompID =" & iCompID & " "
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_ID in(select PGD_DescriptionID from Purchase_GIN_details where (PGD_MasterID=" & InvoiceNum & " and PGD_OrderID=" & orderNo & " and PGD_RejectedQnt > 0) or (PGD_MasterID=" & InvoiceNum & " and PGD_OrderID=" & orderNo & " and PGD_Excess > 0)) and Inv_CompID =" & iCompID & " "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPVDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As Integer, ByVal orderNo As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_ID in(select PGD_DescriptionID from Purchase_GIN_details where PGD_MasterID=" & InvoiceNum & " and PGD_OrderID=" & orderNo & ") and Inv_CompID =" & iCompID & " "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadInvoiceDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As Integer, ByVal orderNo As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_ID in(select PID_DescID from PI_Accepted_Details where PID_MasterID=" & InvoiceNum & ") and Inv_CompID =" & iCompID & " "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVerificationOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master Where POM_ID In (SElect distinct PV_OrderNo From Purchase_Verification Where PV_CompID= " & iCompID & " and PV_YearID = " & iYearID & ")"
            'sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master Where POM_ID In (SElect distinct PGD_OrderID From Purchase_GIN_Details Where PGD_CompID= " & iCompID & ")"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master Where POM_ID In (SElect distinct PGD_OrderID From Purchase_GIN_Details Where PGD_CompID= " & iCompID & ") order by POM_ID desc"
            'sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master Where POM_ID In (SElect distinct PGD_OrderID From Purchase_GIN_Details Where PGD_CompID= " & iCompID & " And PGD_RejectedQnt > 0)"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
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
            sMaximumID = objDb.SQLGetDescription(sNameSpace, "Select Top 1 GRM_ID From Goods_Return_Master Order By GRM_ID Desc")
            sYear = objDb.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDb.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDb.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
            sStr = "GR" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadInwardNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim sSql As String = ""
        Try

            sSql = "Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_DocumentRefNo In"
            sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1  And PV_OrderNo=" & iTransactionID & ")  "
            sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
            sSql = sSql & "PGM_CompID=1 and PGM_YearID =" & iYearID & ""
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iInvoiceID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Goods_Return_Master Where GRM_ID=" & iMasterID & " And GRM_GINInvID = " & iInvoiceID & " And GRM_OrderID = " & iOrderID & " And GRM_CompID=" & iCompID & " And GRM_YearID = " & iYearID & ""
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoiceID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Goods_Return_Master Where GRM_GINInvID = " & iInvoiceID & " And GRM_OrderID = " & iOrderID & " And GRM_PR_ID = 1 And GRM_DelFlag = 'A' And GRM_CompID=" & iCompID & " And GRM_YearID = " & iYearID & ""
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SumQtyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iCommodityID As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Goods_Return_Details where GRD_MasterID = " & iMasterID & " and GRD_Commodity = " & iCommodityID & " and GRD_DescriptionID = " & iDescriptionID & " and GRD_HistoryID = " & iHistoryID & " and GRD_CompID = " & iCompID & " and GRD_YearID = " & iYearID & ""
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Goods_Return_Master Where GRM_ID=" & iMasterID & " And GRM_CompID=" & iCompID & " And GRM_YearID = " & iYearID & ""
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckReason(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Goods_Return_Details Where GRD_MasterID=" & iMasterID & " And GRD_CompID=" & iCompID & ""
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGoodsreturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim ds As New DataSet
        Dim iSlNo As Integer = 0
        Try
            dt.Columns.Add("CommodityID")
            dt.Columns.Add("ItemID")
            dt.Columns.Add("HistoryID")
            dt.Columns.Add("UnitID")
            dt.Columns.Add("Goods")
            dt.Columns.Add("Unit")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("Rate")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmount")
            dt.Columns.Add("Charges")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("GSTID")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("FinalTotal")

            sSql = "Select * from Goods_Return_Details where GRD_MasterID =" & iMasterID & " and GRD_CompID=" & iCompID & " order by GRD_ID"
            ds = objDb.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("CommodityID") = ds.Tables(0).Rows(i)("GRD_Commodity")
                    dRow("ItemID") = ds.Tables(0).Rows(i)("GRD_DescriptionID")
                    dRow("HistoryID") = ds.Tables(0).Rows(i)("GRD_HistoryID")
                    dRow("UnitID") = ds.Tables(0).Rows(i)("GRD_UnitID")
                    dRow("Goods") = objDb.SQLExecuteScalar(sNameSpace, "Select Inv_Code from Inventory_Master where Inv_ID='" & ds.Tables(0).Rows(i)("GRD_DescriptionID") & "' and Inv_compid=" & iCompID & "")
                    dRow("Unit") = objDb.SQLExecuteScalar(sNameSpace, "Select Mas_desc from acc_General_master where Mas_id='" & ds.Tables(0).Rows(i)("GRD_UnitID") & "' and Mas_CompId=" & iCompID & "")
                    dRow("Remarks") = ds.Tables(0).Rows(i)("GRD_Remarks")
                    dRow("Quantity") = ds.Tables(0).Rows(i)("GRD_Quantity")
                    dRow("Rate") = ds.Tables(0).Rows(i)("GRD_RateAmount")
                    dRow("RateAmount") = ds.Tables(0).Rows(i)("GRD_Total")
                    dRow("Discount") = ds.Tables(0).Rows(i)("GRD_Discount")
                    dRow("DiscountAmount") = ds.Tables(0).Rows(i)("GRD_DiscountAmount")
                    dRow("Charges") = ds.Tables(0).Rows(i)("GRD_ChargesPerItem")
                    dRow("TotalAmount") = ds.Tables(0).Rows(i)("GRD_Amount")
                    dRow("GSTID") = ds.Tables(0).Rows(i)("GRD_GST_ID")
                    dRow("GSTRate") = ds.Tables(0).Rows(i)("GRD_GSTRate")
                    dRow("GSTAmount") = ds.Tables(0).Rows(i)("GRD_GSTAmount")
                    dRow("FinalTotal") = ds.Tables(0).Rows(i)("GRD_TotalAmount")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetComodityID(ByVal sNameSpace As String, ByVal ItemID As Integer) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Inv_Parent from inventory_master where Inv_ID='" & ItemID & "'"
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHistoryID(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal DescriptionID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select * From Inventory_Master_History Where InvH_INV_ID = " & DescriptionID & " and InvH_CompID = " & ICompID & ""
            sSql = "Select InvH_ID From Inventory_Master_History Where InvH_INV_ID = " & DescriptionID & " and InvH_CompID = " & ICompID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetQuantity(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal MasterID As Integer, ByVal CommodityID As Integer, ByVal DescriptionID As Integer, ByVal HistoryID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select GRD_Quantity From Goods_Return_Details Where GRD_MasterID = " & MasterID & " and GRD_Commodity = " & CommodityID & " and GRD_DescriptionID = " & DescriptionID & " and GRD_HistoryID = " & HistoryID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPRDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer, ByVal iCommosityID As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Details where POD_MasterID = " & iOrderID & " and POD_Commodity = " & iCommosityID & " and POD_DescriptionID = " & iDescriptionID & " and POD_HistoryID = " & iHistoryID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStateCode(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal OrderID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select LEFT(POM_DeliveryGSTNRegNo, 2) from Purchase_Order_Master where POM_ID = " & OrderID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotChargeAmount(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal OrderID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select sum(C_ChargeAmount) from Charges_Master where C_OrderID = " & OrderID & ""
            Return objDb.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotRateAmount(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal OrderID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT sum(CAST(POD_RateAmount AS DECIMAL(10,2))) FROM Purchase_Order_Details where POD_MasterID = " & OrderID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvoiceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvoiceID As Integer, ByVal iCommosityID As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from PI_Accepted_Details where PID_MasterID =" & iInvoiceID & " and PID_CommodityID = " & iCommosityID & " and PID_DescID = " & iDescriptionID & " and PID_HistoryID = " & iHistoryID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGRDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iReturnID As Integer, ByVal iCommosityID As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Details where GRD_MasterID = " & iReturnID & " and GRD_Commodity = " & iCommosityID & " and GRD_DescriptionID = " & iDescriptionID & " and GRD_HistoryID = " & iHistoryID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPRMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Master where POM_ID = " & iOrderID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGoodsOderMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Master where GRM_OrderID = " & iOrderID & " and GRM_CompID = " & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckPurchaseVerification(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Verification where PV_OrderNo = " & iOrderID & " and PV_CompID = " & iCompID & " and PV_YearID = " & iYearID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckRecord(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer)
        Dim sSql As String = "", sCOde As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select GRM_ReturnNo from Goods_Return_Master where GRM_OrderID =" & iOrderID & " and GRM_GINInvID = " & iInvoiceID & " and GRM_PR_ID = 1 and GRM_CompID = " & iCompID & " and GRM_YearID = " & iYearID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetState(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal stateCode As Integer) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select GR_StateName from GSTN_RegNo_Master where GR_TIN = " & stateCode & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveGoodsReturn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dReturnDate As Date, ByVal dOrderDate As Date, ByVal dInvoiceDate As Date, ByVal objPO As clsGoodsReturn) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Master where GRM_ReturnNo = '" & objPO.sPRM_ReturnNo & "' and GRM_GR_ID is not null and GRM_PR_ID is not null and GRM_CompID =" & iCompID & " and GRM_YearID =" & objPO.iPRM_YearID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Goods_Return_Master set GRM_Supplier = " & objPO.iPRM_Supplier & ", GRM_GINInvID = " & objPO.sPRM_GINInvID & ", GRM_Operation = 'U', GRM_Status = 'U', GRM_InvoiceStatus = '" & objPO.sGRM_InvoiceSatus & "', GRM_State = '" & objPO.sGRM_State & "',  GRM_ReturnDate = " & objFasgnrl.FormatDtForRDBMS(dReturnDate, "U") & ", GRM_GINInvDate = " & objFasgnrl.FormatDtForRDBMS(dInvoiceDate, "U") & ", GRM_ReturnRefNo = '" & objPO.sPRM_ReturnRefNo & "', GRM_UpdatedBy = " & objPO.iPRM_CreatedBy & ", GRM_UpdatedOn = GetDate(), GRM_IPAddress = '" & objPO.sPRD_IPAddress & "'"
                sSql = sSql & "Where GRM_ReturnNo = '" & objPO.sPRM_ReturnNo & "' and GRM_CompID =" & iCompID & " and GRM_YearID=" & objPO.iPRM_YearID & ""
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dt.Rows(0)("GRM_ID")
            Else
                iMax = objFasAllgnrl.GetMaxID(sNameSpace, iCompID, "Goods_Return_Master", "GRM_ID", "GRM_CompID")
                sSql = "" : sSql = "Insert into Goods_Return_Master(GRM_ID,GRM_OrderID,GRM_GINInvID,GRM_ReturnNo,GRM_OrderDate,GRM_Supplier,GRM_InvoiceStatus,GRM_State,"
                sSql = sSql & "GRM_ReturnDate,GRM_GINInvNo,GRM_GINInvDate,GRM_ReturnRefNo,GRM_CompID,GRM_YearID,GRM_CreatedBy,GRM_CreatedOn,GRM_GR_ID,GRM_PR_ID,"
                sSql = sSql & "GRM_Operation,GRM_DelFlag,GRM_Status,GRM_IPAddress)Values(" & iMax & "," & objPO.sPRM_OrderID & "," & objPO.sPRM_GINInvID & ",'" & objPO.sPRM_ReturnNo & "'," & objFasgnrl.FormatDtForRDBMS(dOrderDate, "I") & "," & objPO.iPRM_Supplier & ",'" & objPO.sGRM_InvoiceSatus & "','" & objPO.sGRM_State & "',"
                sSql = sSql & "" & objFasgnrl.FormatDtForRDBMS(dReturnDate, "I") & ",'" & objPO.sPRM_GINInvNo & "'," & objFasgnrl.FormatDtForRDBMS(dInvoiceDate, "I") & ",'" & objPO.sPRM_ReturnRefNo & "'," & iCompID & "," & objPO.iPRM_YearID & "," & objPO.iPRM_CreatedBy & ",GetDate()," & objPO.sGRM_GRID & "," & objPO.sGRM_PRID & ","
                sSql = sSql & "'C','W','C','" & objPO.sPRD_IPAddress & "')"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveGoodsReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objPO As clsGoodsReturn, ByVal iMasterID As Integer)
        'Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        'Dim iParamCount As Integer
        'Dim Arr(1) As String
        'Try
        '    iParamCount = 0
        '    'ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_ID", OleDb.OleDbType.Integer, 4)
        '    'ObjParam(iParamCount).Value = objGReturn.grd_id
        '    'ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    'iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_MasterID", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = iMasterID
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_Commodity", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.iPRD_Commodity
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_DescriptionID", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.iPRD_DescriptionID
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_HistoryID", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.iPRD_HistoryID
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_Reason", OleDb.OleDbType.VarChar, 200)
        '    ObjParam(iParamCount).Value = objGReturn.iPRM_TypeOfReturn
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_Remarks", OleDb.OleDbType.VarChar, 200)
        '    ObjParam(iParamCount).Value = objGReturn.sPRM_Remarks
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_Frieght", OleDb.OleDbType.VarChar, 200)
        '    ObjParam(iParamCount).Value = objGReturn.sPRD_Frieght
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_FrieghtAmount", OleDb.OleDbType.VarChar, 200)
        '    ObjParam(iParamCount).Value = objGReturn.sPRD_FrieghtAmount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_UnitID", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_UnitID
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_Quantity", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_Quantity
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_RateAmount", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_Rate
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_Total", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_Total
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_Discount ", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_Discount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_DiscountAmount", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_DiscountAmount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_TotalAmount", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_TotalAmount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_ChargesPerItem", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_ChargesPerItem
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_Amount", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_Amount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_GST_ID", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_GST_ID
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_GSTRate", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_GSTRate
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_GSTAmount", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_GSTAmount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_SGST ", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_SGST
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_SGSTAmount ", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_SGSTAmount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_CGST ", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_CGST
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_CGSTAmount ", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_CGSTAmount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_IGST ", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_IGST
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_IGSTAmount ", OleDb.OleDbType.Decimal, 10)
        '    ObjParam(iParamCount).Value = objGReturn.sGRD_IGSTAmount
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_CompID ", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = iCompID
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_YearID ", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.iPRM_YearID
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_CreatedBy ", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.iPRM_CreatedBy
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_UpdatedBy ", OleDb.OleDbType.Integer, 4)
        '    ObjParam(iParamCount).Value = objGReturn.iPRM_CreatedBy
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GRD_IPAddress ", OleDb.OleDbType.VarChar, 25)
        '    ObjParam(iParamCount).Value = objGReturn.sPRD_IPAddress
        '    ObjParam(iParamCount).Direction = ParameterDirection.Input
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
        '    ObjParam(iParamCount).Direction = ParameterDirection.Output
        '    iParamCount += 1

        '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
        '    ObjParam(iParamCount).Direction = ParameterDirection.Output
        '    Arr(0) = "@iUpdateOrSave"
        '    Arr(1) = "@iOper"
        '    Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spGoodsReturnDetails", 1, Arr, ObjParam)
        '    Return Arr
        'Catch ex As Exception
        '    Throw
        'End Try

        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Details where GRD_MasterID = " & iMasterID & " and GRD_Commodity = " & objPO.iPRD_Commodity & " and "
            sSql = sSql & "GRD_DescriptionID = " & objPO.iPRD_DescriptionID & " and GRD_HistoryID =" & objPO.iPRD_HistoryID & " and GRD_CompID = " & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Goods_Return_Details set GRD_Reason = " & objPO.iPRM_TypeOfReturn & ",GRD_Remarks = '" & objPO.sPRM_Remarks & "',"
                sSql = sSql & "GRD_Status='U', GRD_YearID=" & objPO.iPRM_YearID & ", GRD_Operation = 'U', GRD_Frieght='" & objPO.sPRD_Frieght & "',GRD_FrieghtAmount='" & objPO.sPRD_FrieghtAmount & "',"
                sSql = sSql & "GRD_UnitID = '" & objPO.sGRD_UnitID & "', GRD_Amount = '" & objPO.sGRD_Amount & "', GRD_RateAmount = '" & objPO.sGRD_Rate & "', GRD_Total = '" & objPO.sGRD_Total & "', GRD_Quantity = '" & objPO.sGRD_Quantity & "',"
                sSql = sSql & "GRD_Discount = '" & objPO.sGRD_Discount & "', GRD_DiscountAmount = '" & objPO.sGRD_DiscountAmount & "', GRD_TotalAmount = '" & objPO.sGRD_TotalAmount & "',"
                sSql = sSql & "GRD_ChargesPerItem = '" & objPO.sGRD_ChargesPerItem & "', GRD_GST_ID = '" & objPO.sGRD_GST_ID & "', GRD_GSTRate = '" & objPO.sGRD_GSTRate & "', GRD_GSTAmount = '" & objPO.sGRD_GSTAmount & "',"
                sSql = sSql & "GRD_SGST = '" & objPO.sGRD_SGST & "',GRD_SGSTAmount = '" & objPO.sGRD_SGSTAmount & "',"
                sSql = sSql & "GRD_CGST = '" & objPO.sGRD_CGST & "',GRD_CGSTAmount = '" & objPO.sGRD_CGSTAmount & "',GRD_IGST = '" & objPO.sGRD_IGST & "',GRD_IGSTAmount = '" & objPO.sGRD_IGSTAmount & "',"
                sSql = sSql & "GRD_UpdatedBy = " & objPO.iPRM_CreatedBy & ", GRD_UpdatedOn = GetDate(), GRD_IPAddress = '" & objPO.sPRD_IPAddress & "' where GRD_MasterID = " & iMasterID & " and "
                sSql = sSql & "GRD_Commodity = " & objPO.iPRD_Commodity & " and GRD_DescriptionID = " & objPO.iPRD_DescriptionID & " and "
                sSql = sSql & "GRD_HistoryID =" & objPO.iPRD_HistoryID & " and GRD_CompID = " & iCompID & ""
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objFasAllgnrl.GetMaxID(sNameSpace, iCompID, "Goods_Return_Details", "GRD_ID", "GRD_CompID")
                sSql = "" : sSql = "Insert into Goods_Return_Details(GRD_ID,GRD_MasterID,GRD_Commodity,"
                sSql = sSql & "GRD_DescriptionID,GRD_HistoryID,"
                sSql = sSql & "GRD_Reason,GRD_Remarks,GRD_CreatedBy,GRD_CreatedOn,"
                sSql = sSql & "GRD_UnitID,GRD_Total,GRD_Quantity,GRD_Discount,"
                sSql = sSql & "GRD_DiscountAmount,GRD_TotalAmount,GRD_ChargesPerItem,GRD_GST_ID,"
                sSql = sSql & "GRD_RateAmount,GRD_Amount,GRD_GSTRate,GRD_GSTAmount,GRD_SGST,GRD_SGSTAmount,"
                sSql = sSql & "GRD_CGST,GRD_CGSTAmount,GRD_IGST,GRD_IGSTAmount,"
                sSql = sSql & "GRD_CompID,GRD_DelFlag,GRD_YearID,GRD_Status,GRD_Operation,GRD_Frieght,GRD_FrieghtAmount,GRD_IPAddress)"
                sSql = sSql & "Values(" & iMax & "," & iMasterID & "," & objPO.iPRD_Commodity & ","
                sSql = sSql & "" & objPO.iPRD_DescriptionID & "," & objPO.iPRD_HistoryID & ","
                sSql = sSql & "" & objPO.iPRM_TypeOfReturn & ",'" & objPO.sPRM_Remarks & "', " & objPO.iPRM_CreatedBy & ", GetDate(),"
                sSql = sSql & "'" & objPO.sGRD_UnitID & "','" & objPO.sGRD_Total & "','" & objPO.sGRD_Quantity & "','" & objPO.sGRD_Discount & "',"
                sSql = sSql & "'" & objPO.sGRD_DiscountAmount & "','" & objPO.sGRD_TotalAmount & "','" & objPO.sGRD_ChargesPerItem & "','" & objPO.sGRD_GST_ID & "',"
                sSql = sSql & "'" & objPO.sGRD_Rate & "','" & objPO.sGRD_Amount & "','" & objPO.sGRD_GSTRate & "','" & objPO.sGRD_GSTAmount & "','" & objPO.sGRD_SGST & "','" & objPO.sGRD_SGSTAmount & "',"
                sSql = sSql & "'" & objPO.sGRD_CGST & "','" & objPO.sGRD_CGSTAmount & "','" & objPO.sGRD_IGST & "','" & objPO.sGRD_IGSTAmount & "',"
                sSql = sSql & "" & iCompID & ",'W'," & objPO.iPRM_YearID & ",'C','C','" & objPO.sPRD_Frieght & "','" & objPO.sPRD_FrieghtAmount & "','" & objPO.sPRD_IPAddress & "')"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckGRStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iInvoiceID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Master where GRM_ID = " & iMasterID & " and GRM_GINInvID = " & iInvoiceID & " and GRM_OrderID = " & iOrderID & " and GRM_CompID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("GRM_Status")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckGoodsReturnMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGRMID As Integer, ByVal iGRMInvID As Integer, ByVal iGRMOrderID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Master where GRM_ID = " & iGRMID & " and GRM_CompID = " & iCompID & " and GRM_GINInvID = " & iGRMInvID & " and GRM_OrderID = " & iGRMOrderID & " and GRM_ReturnDate <> '' and GRM_ReturnRefNo <> ''"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckGoodsReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGRMID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Details where GRD_MasterID = " & iGRMID & " and GRD_CompID = " & iCompID & " and GRD_Commodity <> '' and GRD_DescriptionID <> '' and GRD_HistoryID <> '' and GRD_Reason <> '' and GRD_Remarks <> ''"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckNoOfGoodsReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGRMID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Details where GRD_MasterID = " & iGRMID & " and GRD_CompID = " & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveApproveGR(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iInvoiceID As Integer, ByVal iOrderID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Master where GRM_ID = " & iMasterID & " and GRM_GINInvID = " & iInvoiceID & " and GRM_OrderID = " & iOrderID & " and GRM_CompID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Goods_Return_Master set GRM_DelFlag = 'A', GRM_Status = 'A', GRM_Operation = 'A'"
                sSql = sSql & "Where GRM_ID = " & iMasterID & " and GRM_GINInvID = " & iInvoiceID & " and GRM_OrderID = " & iOrderID & " and GRM_CompID =" & iCompID & ""
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTransactionDetailsGR(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As String, ByVal iCommodity As Integer, ByVal ReturnNo As Integer, ByVal iInvoiceID As Integer) As DataTable
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
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("FinalTotal")

            sSql = "Select * From Goods_Return_Details Where GRD_MasterID in (Select GRM_ID From Goods_Return_Master Where GRM_ID=" & ReturnNo & " And GRM_OrderID=" & iOrderID & " and GRM_YearID =" & iYearID & " and GRM_CompID =" & iCompID & " ) "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

            For i = 0 To dt.Rows.Count - 1
                If (dt.Rows(i)("GRD_Quantity") <> 0) Then
                    dr = dtTab.NewRow

                    dr("CommodityID") = dt.Rows(i)("GRD_Commodity")
                    dr("ItemID") = dt.Rows(i)("GRD_DescriptionID")
                    dr("HistoryID") = dt.Rows(i)("GRD_HistoryID")

                    dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_master Where INV_ID =" & dt.Rows(i)("GRD_DescriptionID") & " and INV_Parent=" & dt.Rows(i)("GRD_Commodity") & " and Inv_CompID =" & iCompID & "")
                    dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("GRD_UnitID") & " and Mas_CompID =" & iCompID & "")
                    dr("Quantity") = dt.Rows(i)("GRD_Quantity")
                    dr("ExpectedDate") = objFasgnrl.FormatDtForRDBMS(Date.Today, "D")
                    dr("Rate") = dt.Rows(i)("GRD_Amount")
                    dr("Charges") = dt.Rows(i)("GRD_ChargesPerItem")
                    dr("RateAmount") = dt.Rows(i)("GRD_Total")

                    If dt.Rows(i)("GRD_Discount") > 0 Then
                        dr("Discount") = dt.Rows(i)("GRD_Discount")
                        dr("DiscountAmount") = dt.Rows(i)("GRD_DiscountAmount")
                    Else
                        dr("Discount") = 0
                        dr("DiscountAmount") = 0
                    End If
                    dr("Amount") = dt.Rows(i)("GRD_Amount")

                    dr("GSTID") = dt.Rows(i)("GRD_GST_ID")
                    dr("GSTRate") = dt.Rows(i)("GRD_GSTRate")
                    dr("GSTAmount") = dt.Rows(i)("GRD_GSTAmount")
                    dr("Remarks") = dt.Rows(i)("GRD_Remarks")
                    dr("FinalTotal") = dt.Rows(i)("GRD_TotalAmount")
                    dtTab.Rows.Add(dr)
                End If
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetGINID(ByVal sNameSpace As String, ByVal Inward As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select PGM_ID from purchase_gin_master Where PGM_DocumentRefNo='" & Inward & "' and PGM_YearID =" & iyearID & " and PGM_CompID =" & ICompID & " and PGM_OrderID =" & IOrderID & ""
            Return objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetActualQnt(ByVal sNameSpace As String, ByVal Inward As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select PGM_ID from purchase_gin_master Where PGM_DocumentRefNo='" & Inward & "' and PGM_YearID =" & iyearID & " and PGM_CompID =" & ICompID & " and PGM_OrderID =" & IOrderID & ""
            Return objDb.SQLGetDescription(sNameSpace, sSql)
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
    Public Function DeleteCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Delete From Charges_Master Where C_GoodsReturnID=" & iOrderID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCharges(ByVal sNameSpace As String, ByVal objInvoiceForm As clsGoodsReturn)
        Dim iMaxID As Integer
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            iMaxID = objDb.SQLExecuteScalar(sNameSpace, "select isnull(max(C_ID)+1,1) from Charges_master")
            sSql = "Insert Into Charges_master(C_ID, C_POrderID, C_PGinID, C_PInvoiceDocRef, C_OrderType, C_ChargeID, C_ChargeType, C_ChargeAmount, C_PSType, C_GoodsReturnID, C_DelFlag, C_Status, C_CompID, C_YearID, C_CreatedBy, C_CreatedOn, C_Operation,C_IPAddress) "
            sSql = sSql & " Values(" & iMaxID & ", " & objInvoiceForm.C_POrderID & ", " & objInvoiceForm.C_PGinID & ", " & objInvoiceForm.C_PInvoiceDocRef & ", '" & objInvoiceForm.C_OrderType & "', " & objInvoiceForm.C_ChargeID & ", '" & objInvoiceForm.C_ChargeType & "', " & objInvoiceForm.C_ChargeAmount & ", '" & objInvoiceForm.C_PSType & "'," & objInvoiceForm.C_GoodsReturnID & ", '" & objInvoiceForm.C_DelFlag & "', '" & objInvoiceForm.C_Status & "', " & objInvoiceForm.C_CompID & ", " & objInvoiceForm.C_YearID & ", " & objInvoiceForm.C_CreatedBy & ", GetDate(), '" & objInvoiceForm.C_Operation & "','" & objInvoiceForm.C_IPAddress & "')"
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChargeType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=24 And Mas_DelFlag='A' "
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
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

            'If iInvoiceID > 0 Then
            '    sSql = "" : sSql = "Select * From Charges_Master,Purchase_Invoice_Master Where C_POrderID=PIM_OrderID And C_PGINID=PIM_PRegesterID And C_PInvoiceDocRef=PIM_ID And C_PSType='P' And C_POrderID=" & iOrderID & " And C_PGinID=" & iGinID & " And C_PInvoiceDocRef=" & iInvoiceID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            'Else
            sSql = "" : sSql = "Select * From Charges_Master,Goods_Return_Master Where C_GoodsReturnID=GRM_ID And C_PSType='P' And C_GoodsReturnID=" & iOrderID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            'End If

            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ChargeID") = dt.Rows(i)("C_ChargeID")
                    dRow("ChargeType") = objDb.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt.Rows(i)("C_ChargeID") & " And Mas_Master=24 And Mas_CompID = " & iCompID & "  ")
                    dRow("ChargeAmount") = dt.Rows(i)("C_ChargeAmount")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    '-- Original --
    Public Function SaveStockLedger(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sIPAddress As String, ByVal iUserID As Integer, ByVal dt As DataTable, ByVal OrderNo As String, ByVal InwardNo As String) As Integer
        Dim sSql As String = ""
        Dim iMaxid As Integer, iQTY As Integer
        Try
            For i = 0 To dt.Rows.Count - 1



                If (objDb.SQLCheckForRecord(sNameSpace, "Select * from Stock_Ledger where SL_historyId=" & dt.Rows(i)("HistoryID") & " And SL_ItemID = " & dt.Rows(i)("ItemID") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " And SL_OrderID=" & OrderNo & " and SL_GINID=" & InwardNo & "")) Then
                    objDb.SQLExecuteNonQuery(sNameSpace, "Update Stock_Ledger Set SL_Operation='U',SL_IPAddress='" & sIPAddress & "',SL_ClosingBalanceQty=SL_ClosingBalanceQty-" & dt.Rows(i)("Quantity") & ", SL_PurchaseQty = SL_PurchaseQty where  SL_historyId=" & dt.Rows(i)("HistoryID") & " And SL_ItemID = " & dt.Rows(i)("ItemID") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " and SL_GINID=" & InwardNo & "")
                    ' + " & dt.Rows(i)("Quantity") & "
                Else
                    iMaxid = objDb.SQLExecuteScalar(sNameSpace, "Select isnull(max(SL_ID) + 1, 1) from Stock_Ledger")
                        sSql = "Insert Into Stock_Ledger (SL_ID,SL_Commodity,SL_Date,SL_ItemID,sl_PurchaseQty,SL_ClosingBalanceQty,SL_CompID,SL_YearID,SL_CrBy,SL_CrOn,SL_historyId,SL_Operation,SL_IPAddress,SL_Vat,SL_Exciese,SL_Cst,SL_OrderID,SL_SaleQnty,PurchaseRate,SL_OpeningBalanceQty,SL_GINID)"
                        sSql = sSql & "Values(" & iMaxid & "," & dt.Rows(i)("CommodityID") & ", GetDate()," & dt.Rows(i)("ItemID") & "," & dt.Rows(i)("Quantity") & "," & dt.Rows(i)("Quantity") & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate()," & dt.Rows(i)("HistoryID") & ",'C','" & sIPAddress & "','0','0','0','" & OrderNo & "',0,'" & dt.Rows(i)("Rate") & "',0," & InwardNo & ")"
                        objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If

             


            Next
        Catch ex As Exception
            Throw
        End Try
    End Function






    '-- copy --
    'Public Function SaveStockLedger(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sIPAddress As String, ByVal iUserID As Integer, ByVal dt As DataTable, ByVal OrderNo As String, ByVal InwardNo As String) As Integer
    '    Dim sSql As String = ""
    '    Dim iMaxid As Integer, iQTY As Integer, Qty As Integer, Qty1 As Integer
    '    Try
    '        For i = 0 To dt.Rows.Count - 1

    '            sSql = "select PGD_OrderQnt from Purchase_GIN_Details Where PGD_HistoryId =" & dt.Rows(i)("HistoryID") & " and PGD_DescriptionID = " & dt.Rows(i)("ItemID") & " and PGD_CompID =" & iCompID & " and PGD_MasterID =" & InwardNo & ""
    '            iQTY = objDb.SQLGetDescription(sNameSpace, sSql)
    '            sSql = ""
    '            Qty1 = dt.Rows(i)("Quantity")
    '            Qty = iQTY - Qty1

    '            If (objDb.SQLCheckForRecord(sNameSpace, "Select * from Stock_Ledger where SL_historyId=" & dt.Rows(i)("HistoryID") & " And SL_ItemID = " & dt.Rows(i)("ItemID") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " And SL_OrderID=" & OrderNo & " and SL_GINID=" & InwardNo & "")) Then
    '                objDb.SQLExecuteNonQuery(sNameSpace, "Update Stock_Ledger Set SL_Operation='U',SL_IPAddress='" & sIPAddress & "',SL_ClosingBalanceQty= " & iQTY - dt.Rows(i)("Quantity") & ", SL_PurchaseQty = SL_PurchaseQty + " & dt.Rows(i)("Quantity") & " where  SL_historyId=" & dt.Rows(i)("HistoryID") & " And SL_ItemID = " & dt.Rows(i)("ItemID") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " and SL_GINID=" & InwardNo & "")
    '            Else
    '                iMaxid = objDb.SQLExecuteScalar(sNameSpace, "Select isnull(max(SL_ID) + 1, 1) from Stock_Ledger")
    '                sSql = "Insert Into Stock_Ledger (SL_ID,SL_Commodity,SL_Date,SL_ItemID,sl_PurchaseQty,SL_ClosingBalanceQty,SL_CompID,SL_YearID,SL_CrBy,SL_CrOn,SL_historyId,SL_Operation,SL_IPAddress,SL_Vat,SL_Exciese,SL_Cst,SL_OrderID,SL_SaleQnty,PurchaseRate,SL_OpeningBalanceQty,SL_GINID)"
    '                sSql = sSql & "Values(" & iMaxid & "," & dt.Rows(i)("CommodityID") & ", GetDate()," & dt.Rows(i)("ItemID") & "," & dt.Rows(i)("Quantity") & "," & dt.Rows(i)("Quantity") & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate()," & dt.Rows(i)("HistoryID") & ",'C','" & sIPAddress & "','0','0','0','" & OrderNo & "',0,'" & dt.Rows(i)("Rate") & "',0," & InwardNo & ")"
    '                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function TakeHistoryID(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal DescriptionID As Integer, ByVal iMasterID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select PID_HistoryID From PI_Accepted_Details Where PID_MasterID in (Select PIM_ID From Purchase_Invoice_Master Where PIM_PRegesterID=" & iMasterID & ") And PID_DescID = " & DescriptionID & " and PID_CompID = " & ICompID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZone(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Purchase_Order_Master Where POM_ID in (Select GRM_OrderID From Goods_Return_Master Where GRM_ID=" & iID & " And GRM_CompID=" & iCompID & " And GRM_YearID=" & iYearID & " )"
            GetZone = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetZone
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
