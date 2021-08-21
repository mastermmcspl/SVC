Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class ClsReturnPurchase
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

    Private PRM_OrderNo As String
    Private PRM_InvoiceNo As String

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

    Private iPRM_BatchNo As Integer
    Private iPRM_BaseName As Integer

    Public Property PRM_BatchNo() As Integer
        Get
            Return (iPRM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iPRM_BatchNo = Value
        End Set
    End Property
    Public Property PRM_BaseName() As Integer
        Get
            Return (iPRM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iPRM_BaseName = Value
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
    Public Property sPRM_OrderNo() As String
        Get
            Return (PRM_OrderNo)
        End Get
        Set(ByVal Value As String)
            PRM_OrderNo = Value
        End Set
    End Property
    Public Property sPRM_InvoiceNo() As String
        Get
            Return (PRM_InvoiceNo)
        End Get
        Set(ByVal Value As String)
            PRM_InvoiceNo = Value
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
    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select GRM_ID,GRM_ReturnNo from Goods_Return_Master where GRM_CompID=" & iCompID & " and GRM_YearID = " & iYearID & " order by GRM_ID desc"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iInvoiceID As Integer, ByVal iOrderID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iBatchNo > 0 And iBaseName > 0 Then
                sSql = "Select GRM_DelFlag From Goods_Return_Master Where GRM_ID=" & iMasterID & " And GRM_BatchNo = " & iBatchNo & " And GRM_BaseName = " & iBaseName & " And GRM_CompID=" & iCompID & " And GRM_YearID = " & iYearID & ""
                GetStatus = objDb.SQLGetDescription(sNameSpace, sSql)
            Else
                sSql = "Select GRM_DelFlag From Goods_Return_Master Where GRM_ID=" & iMasterID & " And GRM_GINInvID = " & iInvoiceID & " And GRM_OrderID = " & iOrderID & " And GRM_CompID=" & iCompID & " And GRM_YearID = " & iYearID & ""
                GetStatus = objDb.SQLGetDescription(sNameSpace, sSql)
            End If
            Return GetStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGoodsReturn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dReturnDate As Date, ByVal dOrderDate As Date, ByVal dInvoiceDate As Date, ByVal objPO As ClsReturnPurchase) As Integer
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
                sSql = sSql & "GRM_Operation,GRM_DelFlag,GRM_Status,GRM_IPAddress,GRM_BatchNo,GRM_BaseName,GRM_OrderNo,GRM_InvoiceNo)Values(" & iMax & "," & objPO.sPRM_OrderID & "," & objPO.sPRM_GINInvID & ",'" & objPO.sPRM_ReturnNo & "'," & objFasgnrl.FormatDtForRDBMS(dOrderDate, "I") & "," & objPO.iPRM_Supplier & ",'" & objPO.sGRM_InvoiceSatus & "','" & objPO.sGRM_State & "',"
                sSql = sSql & "" & objFasgnrl.FormatDtForRDBMS(dReturnDate, "I") & ",'" & objPO.sPRM_GINInvNo & "'," & objFasgnrl.FormatDtForRDBMS(dInvoiceDate, "I") & ",'" & objPO.sPRM_ReturnRefNo & "'," & iCompID & "," & objPO.iPRM_YearID & "," & objPO.iPRM_CreatedBy & ",GetDate()," & objPO.sGRM_GRID & "," & objPO.sGRM_PRID & ","
                sSql = sSql & "'C','W','C','" & objPO.sPRD_IPAddress & "'," & objPO.iPRM_BatchNo & "," & objPO.iPRM_BaseName & ",'" & objPO.sPRM_OrderNo & "','" & objPO.sPRM_InvoiceNo & "')"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGoodsReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objPO As ClsReturnPurchase, ByVal iMasterID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Dim iID As Integer
        Try
            sSql = "" : sSql = "Select GRD_ID from Goods_Return_Details where GRD_MasterID = " & iMasterID & " and GRD_Commodity = " & objPO.iPRD_Commodity & " and "
            sSql = sSql & "GRD_DescriptionID = " & objPO.iPRD_DescriptionID & " and GRD_HistoryID =" & objPO.iPRD_HistoryID & " and GRD_CompID = " & iCompID & ""
            iID = objDb.SQLExecuteScalarInt(sNameSpace, sSql)
            If iID > 0 Then
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
                Return iID
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
                iID = 0
                Return iID
            End If
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
            dt.Columns.Add("ID")
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
                    dRow("ID") = ds.Tables(0).Rows(i)("GRD_ID")
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
    Public Function SaveCharges(ByVal sNameSpace As String, ByVal objInvoiceForm As ClsReturnPurchase)
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
    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iBatchNo > 0 And iBaseName > 0 Then
                sSql = "Select * From Goods_Return_Master Where GRM_BatchNo=" & iBatchNo & " And GRM_BaseName=" & iBaseName & " And GRM_CompID=" & iCompID & " And GRM_YearID = " & iYearID & ""
                dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select * From Goods_Return_Master Where GRM_ID=" & iMasterID & " And GRM_CompID=" & iCompID & " And GRM_YearID = " & iYearID & ""
                dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
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
    Public Function LoadGRDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Details where GRD_MasterID=" & iMasterID & " And GRD_ID = " & iID & " And GRD_CompID=" & iCompID & " "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveApproveGR(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iInvoiceID As Integer, ByVal iOrderID As Integer, ByVal iBatchNo As Integer, ByVal iBasename As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Master where GRM_ID = " & iMasterID & " and GRM_BatchNo = " & iBatchNo & " and GRM_BaseName = " & iBasename & " and GRM_CompID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Goods_Return_Master set GRM_DelFlag = 'A', GRM_Status = 'A', GRM_Operation = 'A'"
                sSql = sSql & "Where GRM_ID = " & iMasterID & " and GRM_BatchNo = " & iBatchNo & " and GRM_BaseName = " & iBasename & " and GRM_CompID =" & iCompID & ""
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
