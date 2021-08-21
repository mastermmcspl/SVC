
Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsBankReconciliation
    Private clsgeneral As New clsFASGeneral
    Dim objDB As New DBHelper
    Dim FASGeneral As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions

    Private ABR_ID As Integer
    Private ABR_Bank As Integer
    Private ABR_FromDate As Date
    Private ABR_ToDate As Date
    Private ABR_SerialNo As String
    Private ABR_TrType As Integer
    Private ABR_TransactionNo As String
    Private ABR_TransactionDate As Date
    Private ABR_ChequeNo As String
    Private ABR_ChequeDate As Date
    Private ABR_IFSCCode As String
    Private ABR_BankTransDate As Date
    Private ABR_ValueDate As Date
    Private ABR_Description As String
    Private ABR_RefNo As String
    Private ABR_BranchCode As String
    Private ABR_Debit As Double
    Private ABR_Credit As Double
    Private ABR_Balance As Double
    Private ABR_CreatedBy As Integer
    Private ABR_CreatedOn As Date
    Private ABR_YearID As Integer
    Private ABR_CompID As Integer
    Private ABR_Status As String
    Private ABR_flag As String
    Private ABR_UpdatedBy As Integer
    Private ABR_UpdatedOn As Date
    Private ABR_CDabit As Double
    Private ABR_CCradit As Double
    Private ABR_JID As Integer
    Private ABR_Comment As String
    Private ABR_DifferenceAmt As String
    Private ABR_Party As Integer
    Private ABRM_BRID As String


    Private ABRM_ID As Integer
    Private ABRM_Bank As Integer
    Private ABRM_BankBranch As Integer
    Private ABRM_CreatedBy As Integer
    Private ABRM_CreatedOn As Date
    Private ABRM_ApprovedBy As Integer
    Private ABRM_ApprovedOn As Date
    Private ABRM_DeletedBy As Integer
    Private ABRM_DeletedOn As Date
    Private ABRM_UpdatedBy As Integer
    Private ABRM_UpdatedOn As Date
    Private ABRM_YearID As Integer
    Private ABRM_CompID As Integer
    Private ABRM_Status As String
    Private ABRM_Operation As String
    Private ABRM_IPAddress As String
    Private ABR_Vouchertype As String
    Private ABR_ClosingBal As Double
    Public Property dABR_ClosingBal() As Double
        Get
            Return ABR_ClosingBal
        End Get
        Set(ByVal value As Double)
            ABR_ClosingBal = value
        End Set
    End Property
    Public Property sABR_Vouchertype() As String
        Get
            Return ABR_Vouchertype
        End Get
        Set(ByVal value As String)
            ABR_Vouchertype = value
        End Set
    End Property
    Public Property sABRM_BRID() As String
        Get
            Return ABRM_BRID
        End Get
        Set(ByVal value As String)
            ABRM_BRID = value
        End Set
    End Property
    Public Property sABRM_IPAddress() As String
        Get
            Return ABRM_IPAddress
        End Get
        Set(ByVal value As String)
            ABRM_IPAddress = value
        End Set
    End Property
    Public Property sABRM_Operation() As String
        Get
            Return ABRM_Operation
        End Get
        Set(ByVal value As String)
            ABRM_Operation = value
        End Set
    End Property
    Public Property sABRM_Status() As String
        Get
            Return ABRM_Status
        End Get
        Set(ByVal value As String)
            ABRM_Status = value
        End Set
    End Property
    Public Property iABRM_CompID() As Integer
        Get
            Return ABRM_CompID
        End Get
        Set(ByVal value As Integer)
            ABRM_CompID = value
        End Set
    End Property
    Public Property iABRM_YearID() As Integer
        Get
            Return ABRM_YearID
        End Get
        Set(ByVal value As Integer)
            ABRM_YearID = value
        End Set
    End Property
    Public Property dABRM_UpdatedOn() As Date
        Get
            Return ABRM_UpdatedOn
        End Get
        Set(ByVal value As Date)
            ABRM_UpdatedOn = value
        End Set
    End Property
    Public Property iABRM_UpdatedBy() As Integer
        Get
            Return ABRM_UpdatedBy
        End Get
        Set(ByVal value As Integer)
            ABRM_UpdatedBy = value
        End Set
    End Property
    Public Property dABRM_DeletedOn() As Date
        Get
            Return ABRM_DeletedOn
        End Get
        Set(ByVal value As Date)
            ABRM_DeletedOn = value
        End Set
    End Property
    Public Property iABRM_DeletedBy() As Integer
        Get
            Return ABRM_DeletedBy
        End Get
        Set(ByVal value As Integer)
            ABRM_DeletedBy = value
        End Set
    End Property
    Public Property dABRM_ApprovedOn() As Date
        Get
            Return ABRM_ApprovedOn
        End Get
        Set(ByVal value As Date)
            ABRM_ApprovedOn = value
        End Set
    End Property
    Public Property iABRM_ApprovedBy() As Integer
        Get
            Return ABRM_ApprovedBy
        End Get
        Set(ByVal value As Integer)
            ABRM_ApprovedBy = value
        End Set
    End Property
    Public Property dABRM_CreatedOn() As Date
        Get
            Return ABRM_CreatedOn
        End Get
        Set(ByVal value As Date)
            ABRM_CreatedOn = value
        End Set
    End Property
    Public Property iABRM_CreatedBy() As Integer
        Get
            Return ABRM_CreatedBy
        End Get
        Set(ByVal value As Integer)
            ABRM_CreatedBy = value
        End Set
    End Property
    Public Property iABRM_BankBranch() As Integer
        Get
            Return ABRM_BankBranch
        End Get
        Set(ByVal value As Integer)
            ABRM_BankBranch = value
        End Set
    End Property
    Public Property iABRM_Bank() As Integer
        Get
            Return ABRM_Bank
        End Get
        Set(ByVal value As Integer)
            ABRM_Bank = value
        End Set
    End Property
    Public Property iABRM_ID() As Integer
        Get
            Return ABRM_ID
        End Get
        Set(ByVal value As Integer)
            ABRM_ID = value
        End Set
    End Property


    Public Property iABR_Party() As Integer
        Get
            Return ABR_Party
        End Get
        Set(ByVal value As Integer)
            ABR_Party = value
        End Set
    End Property
    Public Property dABR_CDabit() As Double
        Get
            Return ABR_CDabit
        End Get
        Set(ByVal Value As Double)
            ABR_CDabit = Value
        End Set
    End Property
    Public Property dABR_CCradit() As Double
        Get
            Return ABR_CCradit
        End Get
        Set(ByVal Value As Double)
            ABR_CCradit = Value
        End Set
    End Property
    Public Property iABR_JID() As Integer
        Get
            Return ABR_JID
        End Get
        Set(ByVal Value As Integer)
            ABR_JID = Value
        End Set
    End Property
    Public Property iABR_UpdatedBy() As Integer
        Get
            Return ABR_UpdatedBy
        End Get
        Set(ByVal Value As Integer)
            ABR_UpdatedBy = Value
        End Set
    End Property
    Public Property dABR_UpdatedOn() As Date
        Get
            Return ABR_UpdatedOn
        End Get
        Set(ByVal Value As Date)
            ABR_UpdatedOn = Value
        End Set
    End Property
    Public Property iABR_ID() As Integer
        Get
            Return ABR_ID
        End Get
        Set(ByVal Value As Integer)
            ABR_ID = Value
        End Set
    End Property


    Public Property iABR_Bank() As Integer
        Get
            Return ABR_Bank
        End Get
        Set(ByVal Value As Integer)
            ABR_Bank = Value
        End Set
    End Property

    Public Property dABR_FromDate() As Date
        Get
            Return ABR_FromDate
        End Get
        Set(ByVal Value As Date)
            ABR_FromDate = Value
        End Set
    End Property

    Public Property dABR_ToDate() As Date
        Get
            Return ABR_ToDate
        End Get
        Set(ByVal Value As Date)
            ABR_ToDate = Value
        End Set
    End Property
    Public Property sABR_SerialNo() As String
        Get
            Return ABR_SerialNo
        End Get
        Set(ByVal Value As String)
            ABR_SerialNo = Value
        End Set
    End Property

    Public Property sABR_DifferenceAmt() As String
        Get
            Return ABR_DifferenceAmt
        End Get
        Set(ByVal Value As String)
            ABR_DifferenceAmt = Value
        End Set
    End Property
    Public Property iABR_TrType() As Integer
        Get
            Return ABR_TrType
        End Get
        Set(ByVal Value As Integer)
            ABR_TrType = Value
        End Set
    End Property

    Public Property sABR_TransactionNo() As String
        Get
            Return ABR_TransactionNo
        End Get
        Set(ByVal Value As String)
            ABR_TransactionNo = Value
        End Set
    End Property
    Public Property dABR_TransactionDate() As Date
        Get
            Return ABR_TransactionDate
        End Get
        Set(ByVal Value As Date)
            ABR_TransactionDate = Value
        End Set
    End Property

    Public Property sABR_ChequeNo() As String
        Get
            Return ABR_ChequeNo
        End Get
        Set(ByVal Value As String)
            ABR_ChequeNo = Value
        End Set
    End Property

    Public Property dABR_ChequeDate() As Date
        Get
            Return ABR_ChequeDate
        End Get
        Set(ByVal Value As Date)
            ABR_ChequeDate = Value
        End Set
    End Property

    Public Property sABR_IFSCCode() As String
        Get
            Return ABR_IFSCCode
        End Get
        Set(ByVal Value As String)
            ABR_IFSCCode = Value
        End Set
    End Property

    Public Property dABR_BankTransDate() As Date
        Get
            Return ABR_BankTransDate
        End Get
        Set(ByVal Value As Date)
            ABR_BankTransDate = Value
        End Set
    End Property

    Public Property dABR_ValueDate() As Date
        Get
            Return ABR_ValueDate
        End Get
        Set(ByVal Value As Date)
            ABR_ValueDate = Value
        End Set
    End Property

    Public Property sABR_Description() As String
        Get
            Return ABR_Description
        End Get
        Set(ByVal Value As String)
            ABR_Description = Value
        End Set
    End Property

    Public Property sABR_RefNo() As String
        Get
            Return ABR_RefNo
        End Get
        Set(ByVal Value As String)
            ABR_RefNo = Value
        End Set
    End Property

    Public Property sABR_BranchCode() As String
        Get
            Return ABR_BranchCode
        End Get
        Set(ByVal Value As String)
            ABR_BranchCode = Value
        End Set
    End Property

    Public Property dABR_Debit() As Double
        Get
            Return ABR_Debit
        End Get
        Set(ByVal Value As Double)
            ABR_Debit = Value
        End Set
    End Property

    Public Property dABR_Credit() As Double
        Get
            Return ABR_Credit
        End Get
        Set(ByVal Value As Double)
            ABR_Credit = Value
        End Set
    End Property


    Public Property dABR_Balance() As Double
        Get
            Return ABR_Balance
        End Get
        Set(ByVal Value As Double)
            ABR_Balance = Value
        End Set
    End Property


    Public Property iABR_CreatedBy() As Integer
        Get
            Return ABR_CreatedBy
        End Get
        Set(ByVal Value As Integer)
            ABR_CreatedBy = Value
        End Set
    End Property



    Public Property dABR_CreatedOn() As Date
        Get
            Return ABR_CreatedOn
        End Get
        Set(ByVal Value As Date)
            ABR_CreatedOn = Value
        End Set
    End Property


    Public Property iABR_YearID() As Integer
        Get
            Return ABR_YearID
        End Get
        Set(ByVal Value As Integer)
            ABR_YearID = Value
        End Set
    End Property

    Public Property iABR_CompID() As Integer
        Get
            Return ABR_CompID
        End Get
        Set(ByVal Value As Integer)
            ABR_CompID = Value
        End Set
    End Property

    Public Property sABR_Status() As String
        Get
            Return ABR_Status
        End Get
        Set(ByVal Value As String)
            ABR_Status = Value
        End Set
    End Property
    Public Property sABR_Comment() As String
        Get
            Return ABR_Comment
        End Get
        Set(ByVal Value As String)
            ABR_Comment = Value
        End Set
    End Property
    Public Property sABR_flag() As String
        Get
            Return ABR_flag
        End Get
        Set(ByVal Value As String)
            ABR_flag = Value
        End Set
    End Property
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
    Private Acc_JE_UpdatedBy As Integer
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
    Private Acc_JE_InvoiceDate As Date
    Private Acc_JE_BalanceAmount As Decimal
    Private Acc_JE_AttachID As Integer
    Private ACC_JE_ZoneID As Integer
    Private ACC_JE_RegionID As Integer
    Private ACC_JE_AreaID As Integer
    Private ACC_JE_BranchID As Integer


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
    Private ATD_UpdatedOn As Date
    Private ATD_UpdatedBy As Integer
    Private ATD_ApprovedBy As Integer
    Private ATD_ApprovedOn As Date
    Private ATD_Deletedby As Integer
    Private ATD_DeletedOn As Date
    Private ATD_Status As String
    Private ATD_YearID As Integer
    Private ATD_Operation As String
    Private ATD_IPAddress As String
    Private ABR_PostedDateIO As Date
    Public Property dABR_PostedDateIO() As Date
        Get
            Return (ABR_PostedDateIO)
        End Get
        Set(ByVal Value As Date)
            ABR_PostedDateIO = Value
        End Set
    End Property

    Public Property iACC_JE_ZoneID() As Integer
        Get
            Return (ACC_JE_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            ACC_JE_ZoneID = Value
        End Set
    End Property
    Public Property iACC_JE_RegionID() As Integer
        Get
            Return (ACC_JE_RegionID)
        End Get
        Set(ByVal Value As Integer)
            ACC_JE_RegionID = Value
        End Set
    End Property
    Public Property iACC_JE_AreaID() As Integer
        Get
            Return (ACC_JE_AreaID)
        End Get
        Set(ByVal Value As Integer)
            ACC_JE_AreaID = Value
        End Set
    End Property
    Public Property iACC_JE_BranchID() As Integer
        Get
            Return (ACC_JE_BranchID)
        End Get
        Set(ByVal Value As Integer)
            ACC_JE_BranchID = Value
        End Set
    End Property
    Public Property iAcc_JE_AttachID() As Integer
        Get
            Return (Acc_JE_AttachID)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_AttachID = Value
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

    Public Property dAcc_JE_InvoiceDate() As Date
        Get
            Return (Acc_JE_InvoiceDate)
        End Get
        Set(ByVal Value As Date)
            Acc_JE_InvoiceDate = Value
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

    Public Property iAcc_JE_CreatedBy() As Integer
        Get
            Return (Acc_JE_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_CreatedBy = Value
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


    Public Function LoadCompanyAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal ibank As Integer, ByVal dfrom As String, ByVal dto As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim val As Integer = 0
        Try
            dt1.Columns.Add("SrNo")
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("ATD_TrType")
            dt1.Columns.Add("ATD_TransactionDate")
            dt1.Columns.Add("ATD_Debit")
            dt1.Columns.Add("ATD_Credit")
            dt1.Columns.Add("Acc_PM_TransactionNo")
            dt1.Columns.Add("Acc_PM_ChequeNo")
            dt1.Columns.Add("Acc_PM_ChequeDate")
            dt1.Columns.Add("Acc_PM_IFSCCode")
            dt1.Columns.Add("Acc_PM_BankName")
            dt1.Columns.Add("Acc_PM_BranchName")
            sSql = "select a.ATD_TrType,a.ATD_TransactionDate,a.ATD_Debit,a.ATD_Credit,c.Acc_PM_TransactionNo,c.Acc_PM_ChequeNo,c.Acc_PM_ChequeDate,c.Acc_PM_IFSCCode,c.Acc_PM_BankName,c.Acc_PM_BranchName from Acc_Transactions_Details a join Acc_Payment_Master c on a.ATD_BillId=c.acc_PM_ID where a.ATD_GL =" & ibank & " And c.Acc_PM_ChequeNo Not in (select ABR_ChequeNo from Acc_Bank_Reconcilation) "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow
                    val = val + 1
                    dRow("SrNo") = val
                    dRow("SerialNo") = GeneratePurchaseOrderCode(sNameSpace, iCompID, val)
                    If IsDBNull(dt.Rows(i)("ATD_TrType")) = False Then
                        If (dt.Rows(i)("ATD_TrType").ToString() = 1) Then
                            dRow("ATD_TrType") = "payment"
                        ElseIf (dt.Rows(i)("ATD_TrType").ToString() = 2) Then
                            dRow("ATD_TrType") = ""
                        End If
                    Else
                        dRow("ATD_TrType") = ""
                    End If

                    If IsDBNull(dt.Rows(i)("ATD_TransactionDate")) = False Then
                        dRow("ATD_TransactionDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ATD_TransactionDate"), "D")
                    End If

                    If IsDBNull(dt.Rows(i)("ATD_Debit")) = False Then

                        dRow("ATD_Debit") = dt.Rows(i)("ATD_Debit").ToString()
                    Else
                        dRow("ATD_Debit") = ""
                    End If

                    If IsDBNull(dt.Rows(i)("ATD_Credit")) = False Then
                        dRow("ATD_Credit") = dt.Rows(i)("ATD_Credit").ToString()
                    Else
                        dRow("ATD_Credit") = ""
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_TransactionNo")) = False Then
                        dRow("Acc_PM_TransactionNo") = dt.Rows(i)("Acc_PM_TransactionNo").ToString()
                    Else
                        dRow("Acc_PM_TransactionNo") = ""
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_ChequeNo")) = False Then
                        dRow("Acc_PM_ChequeNo") = dt.Rows(i)("Acc_PM_ChequeNo").ToString()
                    Else
                        dRow("Acc_PM_ChequeNo") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("Acc_PM_ChequeDate")) = False Then
                        dRow("Acc_PM_ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("Acc_PM_ChequeDate"), "D")
                    Else
                        dRow("Acc_PM_ChequeDate") = ""
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_IFSCCode")) = False Then
                        dRow("Acc_PM_IFSCCode") = dt.Rows(i)("Acc_PM_IFSCCode").ToString()
                    Else
                        dRow("Acc_PM_IFSCCode") = ""
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_BankName")) = False Then
                        dRow("Acc_PM_BankName") = dt.Rows(i)("Acc_PM_BankName").ToString()
                    Else
                        dRow("Acc_PM_BankName") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("Acc_PM_BranchName")) = False Then
                        dRow("Acc_PM_BranchName") = dt.Rows(i)("Acc_PM_BranchName").ToString()
                    Else
                        dRow("Acc_PM_BranchName") = ""
                    End If
                    dt1.Rows.Add(dRow)
                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetData(ByVal sNameSpace As String, ByVal dbID As String, ByVal cBID As String)
        Dim dt As DataTable
        Dim sSql As String
        Try
            sSql = "" : sSql = "Select * from Acc_Bank_Reconcilation where " & dbID & " and " & dbID
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GeneratePurchaseOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sMaximumID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMonth As String = "", sMonthCode As String = ""
        Dim sDate As String = "", sMaxID As String = "", sLastID As String = "", sSDate As String = ""
        Try

            sYear = objDB.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDB.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDB.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
            If sMaximumID = Nothing Then
                sMaxID = "0001"
            Else
                sLastID = sMaximumID
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
            sStr = "BR" & "" & "" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyAccountsb(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal ibank As Integer, ByVal dfrom As String, ByVal dto As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim val As Integer = 0
        Try
            dt1.Columns.Add("SrNo")
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("ATD_TrType")
            dt1.Columns.Add("ATD_TransactionDate")
            dt1.Columns.Add("ATD_Debit")
            dt1.Columns.Add("ATD_Credit")
            dt1.Columns.Add("Acc_PM_TransactionNo")
            dt1.Columns.Add("Acc_PM_ChequeNo")
            dt1.Columns.Add("Acc_PM_ChequeDate")
            dt1.Columns.Add("Acc_PM_IFSCCode")
            dt1.Columns.Add("Acc_PM_BankName")
            dt1.Columns.Add("Acc_PM_BranchName")
            dt1.Columns.Add("Acc_PM_Party")
            dt1.Columns.Add("Acc_PM_BillType")
            dt1.Columns.Add("ComDEbitSUM1")
            dt1.Columns.Add("COmpCreditSUm1")
            dt1.Columns.Add("closingBal_Comp")

            'sSql = "Select a.ATD_TrType,a.ATD_TransactionDate, a.ATD_Debit, a.ATD_Credit, c.Acc_PM_BillType,c.Acc_PM_Party, c.Acc_PM_TransactionNo, c.Acc_PM_ChequeNo, c.Acc_PM_ChequeDate, c.Acc_PM_IFSCCode, c.Acc_PM_BankName, c.Acc_PM_BranchName, d.Acc_RM_Party, d.Acc_RM_TransactionNo, d.Acc_RM_ChequeNo, d.Acc_RM_ChequeDate,d.Acc_RM_BillType,
            'd.Acc_RM_IFSCCode, d.Acc_RM_BankName, d.Acc_RM_BranchName   from Acc_Transactions_Details a left join Acc_Payment_Master c On a.ATD_BillId=c.acc_PM_ID and ATD_TrType=1 left join Acc_Receipt_Master d On a.ATD_BillId=d.Acc_RM_ID and ATD_TrType=3 where a.ATD_SubGL =" & ibank & " And c.Acc_PM_ChequeNo Not In (Select ABR_ChequeNo from Acc_Bank_Reconcilation) "
            'If (dfrom <> "" And dto <> "") Then
            '    dto = FASGeneral.FormatDtForRDBMS(dto, "CT")
            '    dto = Replace(dto, "00:00:00.000", "23:59:59.456")
            '    sSql = sSql & "And a.ATD_TransactionDate between '" & FASGeneral.FormatDtForRDBMS(dfrom, "CT") & "'  and  '" & dto & "'"
            'End If
            sSql = "Select a.ATD_TrType,a.ATD_TransactionDate, a.ATD_Debit, a.ATD_Credit,d.Acc_RM_Location,c.Acc_PM_Location,c.Acc_PM_BillType,c.Acc_PM_Party, c.Acc_PM_TransactionNo, c.Acc_PM_ChequeNo, c.Acc_PM_ChequeDate, c.Acc_PM_IFSCCode, c.Acc_PM_BankName, c.Acc_PM_BranchName, d.Acc_RM_Party, d.Acc_RM_TransactionNo, d.Acc_RM_ChequeNo, d.Acc_RM_ChequeDate,d.Acc_RM_BillType,
            d.Acc_RM_IFSCCode, d.Acc_RM_BankName, d.Acc_RM_BranchName   from Acc_Transactions_Details a left join Acc_Payment_Master c On a.ATD_BillId=c.acc_PM_ID and ATD_TrType=1 left join Acc_Receipt_Master d On a.ATD_BillId=d.Acc_RM_ID and ATD_TrType=3 where a.ATD_SubGL =" & ibank & " And c.Acc_PM_ChequeNo Not In (Select ABR_ChequeNo from Acc_Bank_Reconcilation) "
            If (dfrom <> "" And dto <> "") Then
                dto = FASGeneral.FormatDtForRDBMS(dto, "CT")
                dto = Replace(dto, "00:00:00.000", "23:59:59.456")
                sSql = sSql & "And a.ATD_TransactionDate between '" & FASGeneral.FormatDtForRDBMS(dfrom, "CT") & "'  and  '" & dto & "'"
            End If
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow
                    val = val + 1
                    dRow("SrNo") = val
                    dRow("SerialNo") = GeneratePurchaseOrderCode(sNameSpace, iCompID, val)
                    If IsDBNull(dt.Rows(i)("ATD_TrType")) = False Then
                        If (dt.Rows(i)("ATD_TrType").ToString() = 1) Then
                            dRow("ATD_TrType") = "PAYMENT"
                        ElseIf (dt.Rows(i)("ATD_TrType").ToString() = 3) Then
                            dRow("ATD_TrType") = "RECEIPT"
                        End If
                    End If

                    If (dt.Rows(i)("ATD_TrType").ToString() = 1) Then
                        dRow("Acc_PM_BillType") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from  ACC_General_Master Where mas_master=9 and mas_Delflag ='A' and Mas_ID=" & dt.Rows(i)("Acc_PM_BillType").ToString() & " ")
                    ElseIf (dt.Rows(i)("ATD_TrType").ToString() = 3) Then
                        dRow("Acc_PM_BillType") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from  ACC_General_Master Where mas_master=25 and mas_Delflag ='A' and Mas_ID=" & dt.Rows(i)("Acc_RM_BillType").ToString() & " ")
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_Party")) = False Then
                        dRow("Acc_PM_Party") = dt.Rows(i)("Acc_PM_Party")
                    Else
                        dRow("Acc_PM_Party") = dt.Rows(i)("Acc_RM_Party")
                    End If

                    'If IsDBNull(dt.Rows(i)("Acc_PM_Party")) = False Then
                    '    If (dt.Rows(i)("Acc_PM_Location") = 1) Then
                    '        dRow("Acc_PM_Party") = objDB.SQLExecuteScalar(sNameSpace, "Select BM_Name   from sales_Buyers_Masters where BM_ID=" & dt.Rows(i)("Acc_PM_Party") & " and BM_DelFlag='A' and BM_CompID =" & iCompID & "")
                    '    ElseIf (dt.Rows(i)("Acc_PM_Location") = 2) Then
                    '        dRow("Acc_PM_Party") = objDB.SQLExecuteScalar(sNameSpace, "select CSM_Name  from CustomerSupplierMaster where CSM_ID=" & dt.Rows(i)("Acc_PM_Party") & " and CSM_DelFlag='A' and CSM_CompID =" & iCompID & "")
                    '    ElseIf (dt.Rows(i)("Acc_PM_Location") = 3) Then
                    '        dRow("Acc_PM_Party") = objDB.SQLExecuteScalar(sNameSpace, "select gl_desc  FROM chart_of_accounts where gl_Id=" & dt.Rows(i)("Acc_PM_Party") & " and gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A'")
                    '    End If
                    'Else
                    '    If (dt.Rows(i)("Acc_RM_Location") = 1) Then
                    '        dRow("Acc_PM_Party") = objDB.SQLExecuteScalar(sNameSpace, "Select BM_Name  from sales_Buyers_Masters where BM_ID=" & dt.Rows(i)("Acc_RM_Party") & " and BM_DelFlag='A' and BM_CompID =" & iCompID & "")
                    '    ElseIf (dt.Rows(i)("Acc_RM_Location") = 2) Then
                    '        dRow("Acc_PM_Party") = objDB.SQLExecuteScalar(sNameSpace, "select CSM_Name  from CustomerSupplierMaster where CSM_ID=" & dt.Rows(i)("Acc_RM_Party") & " and CSM_DelFlag='A' and CSM_CompID =" & iCompID & "")
                    '    ElseIf (dt.Rows(i)("Acc_RM_Location") = 3) Then
                    '        dRow("Acc_PM_Party") = objDB.SQLExecuteScalar(sNameSpace, "select gl_desc  FROM chart_of_accounts where gl_Id=" & dt.Rows(i)("Acc_RM_Party") & " and gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A'")
                    '    End If
                    'End If

                    If IsDBNull(dt.Rows(i)("ATD_TransactionDate")) = False Then
                        dRow("ATD_TransactionDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ATD_TransactionDate"), "D")
                    End If

                    If IsDBNull(dt.Rows(i)("ATD_Debit")) = False Then
                        dRow("ATD_Debit") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("ATD_Debit")))
                        'dt.Rows(i)("ATD_Debit")
                    Else
                        dRow("ATD_Debit") = "0.0000"
                    End If

                    If IsDBNull(dt.Rows(i)("ATD_Credit")) = False Then
                        dRow("ATD_Credit") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("ATD_Credit")))
                        'dt.Rows(i)("ATD_Credit")
                    Else
                        dRow("ATD_Credit") = "0.0000"
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_TransactionNo")) = False Then
                        dRow("Acc_PM_TransactionNo") = dt.Rows(i)("Acc_PM_TransactionNo").ToString()
                    Else
                        dRow("Acc_PM_TransactionNo") = dt.Rows(i)("Acc_RM_TransactionNo").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_ChequeNo")) = False Then
                        dRow("Acc_PM_ChequeNo") = dt.Rows(i)("Acc_PM_ChequeNo").ToString()
                    Else
                        dRow("Acc_PM_ChequeNo") = dt.Rows(i)("Acc_RM_ChequeNo").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_ChequeDate")) = False Then
                        dRow("Acc_PM_ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("Acc_PM_ChequeDate"), "D")
                    Else
                        dRow("Acc_PM_ChequeDate") = "01/01/1900"
                        'FASGeneral.FormatDtForRDBMS(dt.Rows(i)("Acc_RM_ChequeDate"), "D")
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_IFSCCode")) = False Then
                        dRow("Acc_PM_IFSCCode") = dt.Rows(i)("Acc_PM_IFSCCode").ToString()
                    Else
                        dRow("Acc_PM_IFSCCode") = dt.Rows(i)("Acc_RM_IFSCCode").ToString()
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_BankName")) = False Then
                        dRow("Acc_PM_BankName") = objDB.SQLExecuteScalar(sNameSpace, "select gl_desc  FROM chart_of_accounts where gl_Id=" & dt.Rows(i)("Acc_PM_BankName") & " and gl_compid=" & iCompID & " and gl_status='A'")
                    Else
                        dRow("Acc_PM_BankName") = ""
                        'objDB.SQLExecuteScalar(sNameSpace, "select gl_desc  FROM chart_of_accounts where gl_Id=" & dt.Rows(i)("Acc_RM_BankName") & " and gl_compid=" & iCompID & " and gl_status='A'")
                    End If

                    If IsDBNull(dt.Rows(i)("Acc_PM_BranchName")) = False Then
                        dRow("Acc_PM_BranchName") = dt.Rows(i)("Acc_PM_BranchName").ToString()
                    Else
                        dRow("Acc_PM_BranchName") = dt.Rows(i)("Acc_RM_BranchName").ToString()
                    End If
                    dRow("ComDEbitSUM1") = "0.00"
                    dRow("COmpCreditSUm1") = "0.00"
                    dRow("closingBal_Comp") = "0.00"
                    dt1.Rows.Add(dRow)
                Next
            End If

            dRow = dt1.NewRow
            sSql = "select * from Acc_Bank_ReconcilationInAndOut where ABR_CompIDIO=" & iCompID & " and ABR_StatusIO='C' "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dRow = dt1.NewRow
                    val = val + 1
                    dRow("SrNo") = val
                    dRow("SerialNo") = dt.Rows(i)("ABR_IDIO")
                    If IsDBNull(dt.Rows(i)("ABR_TrTypeIO")) = False Then
                        If dt.Rows(i)("ABR_TrTypeIO") = 1 Then
                            dRow("ATD_TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrTypeIO") = 3 Then
                            dRow("ATD_TrType") = "RECEIPT"
                        End If
                    Else
                        dRow("ATD_TrType") = ""
                    End If
                    dRow("Acc_PM_TransactionNo") = dt.Rows(i)("ABR_TransactionNoIO")
                    dRow("ATD_TransactionDate") = dt.Rows(i)("ABR_TransactionDateIO")
                    dRow("Acc_PM_ChequeNo") = dt.Rows(i)("ABR_ChequeNoIO")
                    dRow("Acc_PM_ChequeDate") = dt.Rows(i)("ABR_ChequeDateIO")
                    dRow("Acc_PM_IFSCCode") = dt.Rows(i)("ABR_IFSCCodeIO")
                    dRow("Acc_PM_BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_BankIO") & " And gl_CompId=" & iCompID & "")
                    dRow("Acc_PM_BranchName") = ""
                    dRow("ATD_Debit") = dt.Rows(i)("ABR_DebitIO")
                    dRow("ATD_Credit") = dt.Rows(i)("ABR_CreditIO")
                    dRow("Acc_PM_Party") = 0
                    dRow("Acc_PM_BillType") = ""
                    dRow("ComDEbitSUM1") = "0.00"
                    dRow("COmpCreditSUm1") = "0.00"
                    dRow("closingBal_Comp") = "0.00"
                    dt1.Rows.Add(dRow)
                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBankD(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = "", aSql As String = ""
        Dim dt As New DataTable
        Dim GLid As Integer = 0
        Try
            sSql = "Select Acc_Gl from acc_application_settings where Acc_Types='Bank' and Acc_LedgerType='Bank' and Acc_CompID=" & iCompID & ""
            GLid = objDB.SQLExecuteScalar(sNameSpace, sSql)
            If GLid > 0 Then
                aSql = " Select gl_Id, GL_Desc From chart_of_accounts Where gl_parent = " & GLid & " And gl_Status='A' order by gl_id"
                dt = objDB.SQLExecuteDataTable(sNameSpace, aSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadBanckSAvedDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal rowmatched_data As DataTable) As DataTable
        Dim dt1 As New DataTable
        Dim sSql As String = ""
        Dim dbhelper As New DBHelper
        Dim dRow As DataRow
        Try
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("TrType")
            dt1.Columns.Add("TransactionNo")
            dt1.Columns.Add("TxnDate")
            dt1.Columns.Add("ChequeNo")
            dt1.Columns.Add("IFSCCODEBNK")
            dt1.Columns.Add("BankName")
            dt1.Columns.Add("BranchName")
            dt1.Columns.Add("Debit")
            dt1.Columns.Add("Credit")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("RefNo")
            dt1.Columns.Add("BranchCode")
            dt1.Columns.Add("Balance")
            dt1.Columns.Add("Status")
            dt1.Columns.Add("CreditDiff")
            dt1.Columns.Add("DabitDiff")
            If rowmatched_data.Rows.Count > 0 Then
                For i = 0 To rowmatched_data.Rows.Count - 1

                    If IsDBNull(rowmatched_data.Rows(i)("Status")) = False Then
                        If rowmatched_data.Rows(i)("Status") = "EAM" Then
                            dRow = dt1.NewRow
                            If IsDBNull(rowmatched_data.Rows(i)("SerialNo")) = False Then
                                dRow("SerialNo") = rowmatched_data.Rows(i)("SerialNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("TrType")) = False Then
                                dRow("TrType") = rowmatched_data.Rows(i)("TrType")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("TransactionNo")) = False Then
                                dRow("TransactionNo") = rowmatched_data.Rows(i)("TransactionNo")
                            End If

                            If IsDBNull(rowmatched_data.Rows(i)("CreditDiff")) = False Then
                                dRow("CreditDiff") = rowmatched_data.Rows(i)("CreditDiff")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("DabitDiff")) = False Then
                                dRow("DabitDiff") = rowmatched_data.Rows(i)("DabitDiff")
                            End If

                            If IsDBNull(rowmatched_data.Rows(i)("TxnDate")) = False Then
                                dRow("TxnDate") = rowmatched_data.Rows(i)("TxnDate")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("ChequeNo")) = False Then
                                dRow("ChequeNo") = rowmatched_data.Rows(i)("ChequeNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("IFSCCODEBNK")) = False Then
                                dRow("IFSCCODEBNK") = rowmatched_data.Rows(i)("IFSCCODEBNK")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("BankName")) = False Then
                                dRow("BankName") = rowmatched_data.Rows(i)("BankName")
                            End If
                            'If IsDBNull(rowmatched_data.Rows(i)("BranchName")) = False Then
                            '    dRow("BranchName") = rowmatched_data.Rows(i)("BranchName")
                            'End If
                            If IsDBNull(rowmatched_data.Rows(i)("Debit")) = False Then
                                dRow("Debit") = rowmatched_data.Rows(i)("Debit")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Credit")) = False Then
                                dRow("Credit") = rowmatched_data.Rows(i)("Credit")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Description")) = False Then
                                dRow("Description") = rowmatched_data.Rows(i)("Description")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("RefNo")) = False Then
                                dRow("RefNo") = rowmatched_data.Rows(i)("RefNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("BranchCode")) = False Then
                                dRow("BranchCode") = rowmatched_data.Rows(i)("BranchCode")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Balance")) = False Then
                                dRow("Balance") = rowmatched_data.Rows(i)("Balance")
                            End If

                        ElseIf rowmatched_data.Rows(i)("Status") = "EAN" Or rowmatched_data.Rows(i)("Status") = "N" Then
                            GoTo gotoreturn
                        End If
                        dt1.Rows.Add(dRow)
                    End If
                Next
            End If
gotoreturn: Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadBanckSAvedDetailsNOtMatched(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal rowmatched_data As DataTable) As DataTable
        Dim dt1 As New DataTable
        Dim sSql As String = ""
        Dim dbhelper As New DBHelper
        Dim dRow As DataRow
        Try
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("TrType")
            dt1.Columns.Add("TransactionNo")
            dt1.Columns.Add("TxnDate")
            dt1.Columns.Add("ChequeNo")
            dt1.Columns.Add("ValueDate")
            dt1.Columns.Add("BankName")
            dt1.Columns.Add("BranchName")
            dt1.Columns.Add("Debit")
            dt1.Columns.Add("Credit")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("RefNo")
            dt1.Columns.Add("BranchCode")
            dt1.Columns.Add("Balance")
            dt1.Columns.Add("Status")
            dt1.Columns.Add("CreditDiff")
            dt1.Columns.Add("DabitDiff")
            If rowmatched_data.Rows.Count > 0 Then
                For i = 0 To rowmatched_data.Rows.Count - 1

                    If IsDBNull(rowmatched_data.Rows(i)("Status")) = False Then
                        If rowmatched_data.Rows(i)("Status") = "EAN" Then
                            dRow = dt1.NewRow
                            If IsDBNull(rowmatched_data.Rows(i)("SerialNo")) = False Then
                                dRow("SerialNo") = rowmatched_data.Rows(i)("SerialNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("TrType")) = False Then
                                dRow("TrType") = rowmatched_data.Rows(i)("TrType")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("TransactionNo")) = False Then
                                dRow("TransactionNo") = rowmatched_data.Rows(i)("TransactionNo")
                            End If

                            If IsDBNull(rowmatched_data.Rows(i)("TxnDate")) = False Then
                                dRow("TxnDate") = rowmatched_data.Rows(i)("TxnDate")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("ChequeNo")) = False Then
                                dRow("ChequeNo") = rowmatched_data.Rows(i)("ChequeNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("ValueDate")) = False Then
                                dRow("ValueDate") = rowmatched_data.Rows(i)("ValueDate")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("BankName")) = False Then
                                dRow("BankName") = rowmatched_data.Rows(i)("BankName")
                            End If
                            'If IsDBNull(rowmatched_data.Rows(i)("BranchName")) = False Then
                            '    dRow("BranchName") = rowmatched_data.Rows(i)("BranchName")
                            'End If
                            If IsDBNull(rowmatched_data.Rows(i)("Debit")) = False Then
                                dRow("Debit") = rowmatched_data.Rows(i)("Debit")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Credit")) = False Then
                                dRow("Credit") = rowmatched_data.Rows(i)("Credit")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("CreditDiff")) = False Then
                                dRow("CreditDiff") = rowmatched_data.Rows(i)("CreditDiff")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("DabitDiff")) = False Then
                                dRow("DabitDiff") = rowmatched_data.Rows(i)("DabitDiff")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Description")) = False Then
                                dRow("Description") = rowmatched_data.Rows(i)("Description")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("RefNo")) = False Then
                                dRow("RefNo") = rowmatched_data.Rows(i)("RefNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("BranchCode")) = False Then
                                dRow("BranchCode") = rowmatched_data.Rows(i)("BranchCode")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Balance")) = False Then
                                dRow("Balance") = rowmatched_data.Rows(i)("Balance")
                            End If

                        ElseIf rowmatched_data.Rows(i)("Status") = "EAM" Or rowmatched_data.Rows(i)("Status") = "N" Then
                            GoTo gotoreturn
                        End If
                        dt1.Rows.Add(dRow)
                    End If
                Next
            End If
gotoreturn: Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadBanckSAvedDetailsNOtExit(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal rowmatched_data As DataTable) As DataTable
        Dim dt1 As New DataTable
        Dim sSql As String = ""
        Dim dbhelper As New DBHelper
        Dim dRow As DataRow
        Try
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("TrType")
            dt1.Columns.Add("TransactionNo")
            dt1.Columns.Add("TxnDate")
            dt1.Columns.Add("ChequeNo")
            dt1.Columns.Add("ValueDate")
            dt1.Columns.Add("BankName")
            dt1.Columns.Add("BranchName")
            dt1.Columns.Add("Debit")
            dt1.Columns.Add("Credit")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("RefNo")
            dt1.Columns.Add("BranchCode")
            dt1.Columns.Add("Balance")
            dt1.Columns.Add("Status")
            dt1.Columns.Add("CreditDiff")
            dt1.Columns.Add("DabitDiff")

            If rowmatched_data.Rows.Count > 0 Then
                For i = 0 To rowmatched_data.Rows.Count - 1

                    If IsDBNull(rowmatched_data.Rows(i)("Status")) = False Then
                        If rowmatched_data.Rows(i)("Status") = "N" Then
                            dRow = dt1.NewRow
                            If IsDBNull(rowmatched_data.Rows(i)("SerialNo")) = False Then
                                dRow("SerialNo") = rowmatched_data.Rows(i)("SerialNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("TrType")) = False Then
                                dRow("TrType") = rowmatched_data.Rows(i)("TrType")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("TransactionNo")) = False Then
                                dRow("TransactionNo") = rowmatched_data.Rows(i)("TransactionNo")
                            End If

                            If IsDBNull(rowmatched_data.Rows(i)("TxnDate")) = False Then
                                dRow("TxnDate") = rowmatched_data.Rows(i)("TxnDate")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("ChequeNo")) = False Then
                                dRow("ChequeNo") = rowmatched_data.Rows(i)("ChequeNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("ValueDate")) = False Then
                                dRow("ValueDate") = rowmatched_data.Rows(i)("ValueDate")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("BankName")) = False Then
                                dRow("BankName") = rowmatched_data.Rows(i)("BankName")
                            End If
                            'If IsDBNull(rowmatched_data.Rows(i)("BranchName")) = False Then
                            '    dRow("BranchName") = rowmatched_data.Rows(i)("BranchName")
                            'End If

                            If IsDBNull(rowmatched_data.Rows(i)("CreditDiff")) = False Then
                                dRow("CreditDiff") = rowmatched_data.Rows(i)("CreditDiff")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("DabitDiff")) = False Then
                                dRow("DabitDiff") = rowmatched_data.Rows(i)("DabitDiff")
                            End If

                            If IsDBNull(rowmatched_data.Rows(i)("Debit")) = False Then
                                dRow("Debit") = rowmatched_data.Rows(i)("Debit")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Credit")) = False Then
                                dRow("Credit") = rowmatched_data.Rows(i)("Credit")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Description")) = False Then
                                dRow("Description") = rowmatched_data.Rows(i)("Description")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("RefNo")) = False Then
                                dRow("RefNo") = rowmatched_data.Rows(i)("RefNo")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("BranchCode")) = False Then
                                dRow("BranchCode") = rowmatched_data.Rows(i)("BranchCode")
                            End If
                            If IsDBNull(rowmatched_data.Rows(i)("Balance")) = False Then
                                dRow("Balance") = rowmatched_data.Rows(i)("Balance")
                            End If

                        ElseIf rowmatched_data.Rows(i)("Status") = "EAM" Or rowmatched_data.Rows(i)("Status") = "EAN" Then
                            GoTo gotoreturn
                        End If
                        dt1.Rows.Add(dRow)
                    End If
                Next
            End If
gotoreturn: Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadReport(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal DBID As String, ByVal CID As String) As DataTable
        Dim rowmatched_data As New DataTable
        Dim dt1 As New DataTable
        Dim sSql As String = ""
        Dim dbhelper As New DBHelper
        Dim dRow As DataRow
        Try
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("TrType")
            dt1.Columns.Add("TransactionNo")
            dt1.Columns.Add("TxnDate")
            dt1.Columns.Add("ChequeNo")
            dt1.Columns.Add("IFSCCODEBNK")
            dt1.Columns.Add("BankName")
            dt1.Columns.Add("BranchName")
            dt1.Columns.Add("Debit")
            dt1.Columns.Add("Credit")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("RefNo")
            dt1.Columns.Add("BranchCode")
            dt1.Columns.Add("Balance")
            dt1.Columns.Add("Status")
            dt1.Columns.Add("CreditDiff")
            dt1.Columns.Add("DabitDiff")
            dt1.Columns.Add("CCredit")
            dt1.Columns.Add("CDebit")
            'dt1.Columns.Add("AmuntNtMatched")

            rowmatched_data = GetData(sNameSpace, DBID, CID)
            If rowmatched_data.Rows.Count > 0 Then
                For i = 0 To rowmatched_data.Rows.Count - 1
                    dRow = dt1.NewRow
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_SerialNo")) = False Then
                        dRow("SerialNo") = rowmatched_data.Rows(i)("ABR_SerialNo")
                    End If
                    'If IsDBNull(rowmatched_data.Rows(i)("ABR_TrType")) = False Then
                    '    If rowmatched_data.Rows(i)("ABR_TrType") = 1 Then
                    '        dRow("TrType") = "PAYMENT"
                    '    Else
                    '        dRow("TrType") = "RECEIPT"
                    '    End If
                    'End If

                    If IsDBNull(rowmatched_data.Rows(i)("ABR_TrType")) = False Then
                        If rowmatched_data.Rows(i)("ABR_TrType") = 1 Then
                            dRow("TrType") = "PAYMENT"
                        ElseIf rowmatched_data.Rows(i)("ABR_TrType") = 3 Then
                            dRow("TrType") = "RECEIPT"
                        End If
                    Else
                        dRow("TrType") = ""
                    End If

                    If IsDBNull(rowmatched_data.Rows(i)("ABR_TransactionNo")) = False Then
                        dRow("TransactionNo") = rowmatched_data.Rows(i)("ABR_TransactionNo")
                    End If

                    If IsDBNull(rowmatched_data.Rows(i)("ABR_TransactionDate")) = False Then
                        dRow("TxnDate") = rowmatched_data.Rows(i)("ABR_TransactionDate")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_ChequeNo")) = False Then
                        dRow("ChequeNo") = rowmatched_data.Rows(i)("ABR_ChequeNo")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_IFSCCode")) = False Then
                        dRow("IFSCCODEBNK") = rowmatched_data.Rows(i)("ABR_IFSCCode")
                    End If
                    'If IsDBNull(rowmatched_data.Rows(i)("ABR_ValueDate")) = False Then
                    '    dRow("ValueDate") = rowmatched_data.Rows(i)("ABR_ValueDate")
                    'End If

                    If IsDBNull(rowmatched_data.Rows(i)("ABR_Bank")) = False Then
                        dRow("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & rowmatched_data.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & "")

                    End If

                    'If IsDBNull(rowmatched_data.Rows(i)("BranchName")) = False Then
                    '    dRow("BranchName") = rowmatched_data.Rows(i)("BranchName")
                    'End If

                    If IsDBNull(rowmatched_data.Rows(i)("ABR_CCradit")) = False Then
                        dRow("CCredit") = rowmatched_data.Rows(i)("ABR_CCradit")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_CDabit")) = False Then
                        dRow("CDebit") = rowmatched_data.Rows(i)("ABR_CDabit")
                    End If

                    If IsDBNull(rowmatched_data.Rows(i)("ABR_Debit")) = False Then
                        dRow("Debit") = rowmatched_data.Rows(i)("ABR_Debit")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_Credit")) = False Then
                        dRow("Credit") = rowmatched_data.Rows(i)("ABR_Credit")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_Description")) = False Then
                        dRow("Description") = rowmatched_data.Rows(i)("ABR_Description")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_RefNo")) = False Then
                        dRow("RefNo") = rowmatched_data.Rows(i)("ABR_RefNo")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_BranchCode")) = False Then
                        dRow("BranchCode") = rowmatched_data.Rows(i)("ABR_BranchCode")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_Balance")) = False Then
                        dRow("Balance") = rowmatched_data.Rows(i)("ABR_Balance")
                    End If
                    If IsDBNull(rowmatched_data.Rows(i)("ABR_Status")) = False Then
                        dRow("Status") = rowmatched_data.Rows(i)("ABR_Status")
                    End If
                    dt1.Rows.Add(dRow)
                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckChequeNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sChequeNo As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select ABR_ChequeNo from Acc_Bank_Reconcilation where ABR_ChequeNo ='" & sChequeNo & "' and ABR_CompID=" & iCompID & ""
            CheckChequeNo = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReconciliationNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select ABRM_ID,ABRM_BRID from Acc_Bank_Reconcilation_Master where ABRM_CompID=" & iCompID & " and ABRM_YearID =" & iYearID & " Order By ABRM_ID desc "
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveBankReconciliationMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objBank As clsBankReconciliation) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Dim brCode As String
        Try
            'sSql = "" : sSql = "Select * from Acc_Bank_Reconcilation_Master where  ABRM_CompID =" & iCompID & " and ABRM_YearID =" & objPO.iABR_YearID & ""
            'dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            'If dt.Rows.Count > 0 Then
            'Else
            sSql = ""
            brCode = GenerateReconciliationCode(sNameSpace, iCompID)
            iMax = objGnrl.GetMaxID(sNameSpace, iCompID, "Acc_Bank_Reconcilation_Master", "ABRM_ID", "ABRM_CompID")
            sSql = "" : sSql = "Insert into Acc_Bank_Reconcilation_Master(ABRM_ID,ABRM_BRID,ABRM_Bank,ABRM_BankBranch,ABRM_CreatedBy,ABRM_CreatedOn,ABRM_CompID,ABRM_YearID,ABRM_Status,ABRM_Operation,ABRM_IPAddress)"
            sSql = sSql & "Values(" & iMax & ",'" & brCode & "'," & objBank.iABRM_Bank & "," & objBank.iABRM_BankBranch & "," & objBank.iABRM_CreatedBy & "," & objBank.dABRM_CreatedOn & "," & iCompID & "," & objBank.iABR_YearID & ",'" & objBank.sABRM_Status & "','" & objBank.sABRM_Operation & "','" & objBank.sABRM_IPAddress & "')"
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            Return iMax
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateReconciliationCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = "", sMonth As String = "", sMonthCode As String = ""
        Dim sDate As String = "", sMaxID As String = "", sLastID As String = "", sSDate As String = ""
        Try
            sMaximumID = objDB.SQLGetDescription(sNameSpace, "Select Top 1 ABRM_ID From Acc_Bank_Reconcilation_Master where ABRM_CompID = " & iCompID & " Order By ABRM_ID Desc")
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
            sStr = "" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveBankReconciliation1(ByVal sNamespace As String, ByVal iCompID As Integer, ByVal objBank As clsBankReconciliation) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(33) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Bank", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_Bank
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_FromDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_FromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_ToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_SerialNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objBank.ABR_SerialNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objBank.ABR_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ChequeNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objBank.ABR_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_IFSCCode", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objBank.ABR_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_BankTransDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_BankTransDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ValueDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_ValueDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Description", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objBank.ABR_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_RefNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objBank.ABR_RefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_BranchCode", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objBank.ABR_BranchCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Debit", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Credit", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Balance", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_Balance
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Status", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objBank.ABR_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_BDabit", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_CDabit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_BCradit", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_CCradit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_JID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_JID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Vouchertype", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objBank.ABR_Vouchertype
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ClosingBal", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_ClosingBal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNamespace, "spAcc_Bank_Reconcilation", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranchNames(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BD_ID,BD_BranchName From Acc_Company_BankDetails Where BD_BankName=" & iBankID & " And BD_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateChequeNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sFSCCode As String, ByVal sChequeno As String, ByVal sTransionNo As String, ByVal dTrnDate As String, ByVal iParty As Integer) As Integer
        Dim sSql, sSql1, sSql2, sSql3 As String
        Dim PM_ID As Integer
        Dim dt As New DataTable
        Dim dbhelper As New DBHelper
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from Acc_Payment_Master where Acc_PM_TransactionNo = '" & sTransionNo & "'"
            dr = dbhelper.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sSql = "update Acc_Payment_Master set Acc_PM_ChequeNo='" & sChequeno & "',Acc_PM_IFSCCode='" & sFSCCode & "',Acc_PM_Party=" & iParty & " where Acc_PM_CompID=" & iCompID & " and Acc_PM_TransactionNo='" & sTransionNo & "'"
                objDB.SQLExecuteScalar(sNameSpace, sSql)
                sSql1 = "select Acc_PM_ID from Acc_Payment_Master  where Acc_PM_CompID=" & iCompID & " and Acc_PM_TransactionNo='" & sTransionNo & "'"
                PM_ID = objDB.SQLExecuteScalar(sNameSpace, sSql1)
                sSql3 = "select ATD_ID from Acc_Transactions_Details where ATD_TrType=1 and ATD_BillId=" & PM_ID & " and ATD_CompID=" & iCompID & ""
                dt = objDB.SQLExecuteDataTable(sNameSpace, sSql3)
                For i = 0 To dt.Rows.Count - 1
                    sSql2 = "update Acc_Transactions_Details set ATD_TransactionDate='" & clsgeneral.FormatDtForRDBMS(dTrnDate, "CT") & "' where ATD_ID=" & dt.Rows(i).Item("ATD_ID") & " and  ATD_TrType=1 and ATD_BillId=" & PM_ID & " and ATD_CompID=" & iCompID & " "
                    UpdateChequeNo = objDB.SQLExecuteScalar(sNameSpace, sSql2)
                Next
            Else
                sSql = "update Acc_Receipt_Master set Acc_RM_ChequeNo='" & sChequeno & "',Acc_RM_IFSCCode='" & sFSCCode & "',Acc_RM_Party=" & iParty & " where Acc_RM_CompID=" & iCompID & " and Acc_RM_TransactionNo='" & sTransionNo & "'"
                objDB.SQLExecuteScalar(sNameSpace, sSql)
                sSql1 = "select Acc_RM_ID from Acc_Receipt_Master  where Acc_RM_CompID=" & iCompID & " and Acc_RM_TransactionNo='" & sTransionNo & "'"
                PM_ID = objDB.SQLExecuteScalar(sNameSpace, sSql1)
                sSql3 = "select ATD_ID from Acc_Transactions_Details where ATD_TrType=3 and ATD_BillId=" & PM_ID & " and ATD_CompID=" & iCompID & ""
                dt = objDB.SQLExecuteDataTable(sNameSpace, sSql3)
                For i = 0 To dt.Rows.Count - 1
                    sSql2 = "update Acc_Transactions_Details set ATD_TransactionDate='" & clsgeneral.FormatDtForRDBMS(dTrnDate, "CT") & "' where ATD_ID=" & dt.Rows(i).Item("ATD_ID") & " and ATD_TrType=3 and ATD_BillId=" & PM_ID & " and ATD_CompID=" & iCompID & " "
                    UpdateChequeNo = objDB.SQLExecuteScalarInt(sNameSpace, sSql2)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDebitGreaterAmount(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select ABR_Debit from Acc_Bank_Reconcilation where ABR_Debit>0 and ABR_CompID=" & iCompID & ""
            LoadDebitGreaterAmount = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return LoadDebitGreaterAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DiffDebitSum(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String
        Try
            sSql = "SELECT SUM()FROM Acc_Bank_Reconcilation"
            DiffDebitSum = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return DiffDebitSum
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadComp_UNmatcheddata(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sBankdt As String) As DataTable
        Dim sSql As String
        Try
            sSql = "select * from Acc_Transactions_Details a left join Acc_Payment_Master 
c On a.ATD_BillId=c.acc_PM_ID and ATD_TrType=1 left join Acc_Receipt_Master d 
On a.ATD_BillId=d.Acc_RM_ID and ATD_TrType=3 where a.ATD_SubGL ='88' and c.Acc_PM_ChequeNo<>'" & sBankdt & "' or d.Acc_RM_ChequeNo<>'" & sBankdt & "'"

            LoadComp_UNmatcheddata = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return LoadComp_UNmatcheddata
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBanckSAvedDetailsofBank(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select * from Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_CompID=" & iCompID & ""
            LoadBanckSAvedDetailsofBank = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return LoadBanckSAvedDetailsofBank
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBanckSAvedDetailsofBank1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select * from acc_bank_reconcilationinandout where ABRM_IDIO=" & iMasterID & " and ABR_CompIDIO=" & iCompID & ""
            LoadBanckSAvedDetailsofBank1 = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return LoadBanckSAvedDetailsofBank1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReconciliation1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dr As DataRow
        Try
            dt1.Columns.Add("SlrNo")
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("TrType")
            dt1.Columns.Add("TransactionNo")
            dt1.Columns.Add("TxnDate")
            dt1.Columns.Add("ChequeNo")
            dt1.Columns.Add("ChequeDate")
            dt1.Columns.Add("IFSCCODEBNK")
            dt1.Columns.Add("BankName")
            dt1.Columns.Add("BranchName")
            dt1.Columns.Add("Debit")
            dt1.Columns.Add("Credit")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("RefNo")
            dt1.Columns.Add("BranchCode")
            dt1.Columns.Add("Balance")
            dt1.Columns.Add("Status")
            dt1.Columns.Add("DabitDiff")
            dt1.Columns.Add("CreditDiff")
            dt1.Columns.Add("CDebit")
            dt1.Columns.Add("CCredit")
            dt1.Columns.Add("BCredit")
            dt1.Columns.Add("BDebit")
            dt1.Columns.Add("Creditresult")
            dt1.Columns.Add("Debitresult")
            dt1.Columns.Add("CompDebitresult")
            dt1.Columns.Add("CompCreditresult")

            sSql = "select * from Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_CompID=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dr = dt1.NewRow
                    dr("SerialNo") = dt.Rows(i)("ABR_ID")
                    If IsDBNull(dt.Rows(i)("ABR_TrType")) = False Then
                        If dt.Rows(i)("ABR_TrType") = 1 Then
                            dr("TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrType") = 3 Then
                            dr("TrType") = "RECEIPT"
                        End If
                    Else
                        dr("TrType") = ""
                    End If
                    dr("TransactionNo") = dt.Rows(i)("ABR_TransactionNo")
                    dr("TxnDate") = dt.Rows(i)("ABR_TransactionDate")
                    dr("ChequeNo") = dt.Rows(i)("ABR_ChequeNo")
                    dr("ChequeDate") = dt.Rows(i)("ABR_ChequeDate")
                    dr("IFSCCODEBNK") = dt.Rows(i)("ABR_IFSCCode")
                    dr("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & "")
                    dr("BranchName") = ""
                    dr("Debit") = dt.Rows(i)("ABR_Debit")
                    dr("Credit") = dt.Rows(i)("ABR_Credit")
                    dr("Description") = dt.Rows(i)("ABR_Description")
                    dr("RefNo") = dt.Rows(i)("ABR_RefNo")
                    dr("BranchCode") = dt.Rows(i)("ABR_BranchCode")
                    dr("Balance") = dt.Rows(i)("ABR_Balance")
                    dr("Status") = dt.Rows(i)("ABR_Status")
                    dr("DabitDiff") = "0"
                    dr("CreditDiff") = "0"
                    dr("CDebit") = dt.Rows(i)("ABR_CDabit")
                    dr("CCredit") = dt.Rows(i)("ABR_CCradit")
                    dr("Creditresult") = "0.00"
                    dr("Debitresult") = "0.00"
                    dr("CompDebitresult") = "0.00"
                    dr("CompCreditresult") = "0.00"
                    dt1.Rows.Add(dr)
                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBankReconciliation2(ByVal sNamespace As String, ByVal iCompID As Integer, ByVal objBank As clsBankReconciliation) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_BRID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objBank.ABRM_BRID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_Bank", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_Bank
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_BankBranch", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_BankBranch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABRM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABRM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objBank.ABRM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_Operation", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objBank.ABRM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objBank.ABRM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input

            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNamespace, "spAcc_Bank_Reconcilation_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function NotExistinBankBookDetail(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal sdebit As String, ByVal scredit As String) As String
        Dim sSql As String
        Dim Credit, Debit As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "select * from Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & ""
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then

                sSql = "SELECT SUM(" & sdebit & ") from  Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_Status='C'"
                Debit = objDB.SQLExecuteScalar(sNameSpace, sSql)
                sSql = "SELECT SUM(" & scredit & ") from  Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_Status='C'"
                Credit = objDB.SQLExecuteScalar(sNameSpace, sSql)

                If Debit > Credit Then
                    NotExistinBankBookDetail = Debit - Credit
                    Return NotExistinBankBookDetail
                Else
                    NotExistinBankBookDetail = Credit - Debit
                    Return NotExistinBankBookDetail
                End If
            Else
                NotExistinBankBookDetail = 0
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function NotExistinCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal sdebit As String, ByVal scredit As String) As String
        Dim sSql As String
        Dim Credit, Debit As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "select * from Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & ""
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                sSql = "SELECT SUM(" & sdebit & ") from  Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_Status='B'"
                Debit = objDB.SQLExecuteScalar(sNameSpace, sSql)
                sSql = "SELECT SUM(" & scredit & ") from  Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_Status='B'"
                Credit = objDB.SQLExecuteScalar(sNameSpace, sSql)

                If Debit > Credit Then
                    NotExistinCompanyDetails = Debit - Credit
                    Return NotExistinCompanyDetails
                Else
                    NotExistinCompanyDetails = Credit - Debit
                    Return NotExistinCompanyDetails
                End If
            Else
                NotExistinCompanyDetails = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function MatchedCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal sdebit As String, ByVal scredit As String) As String
        Dim sSql As String
        Dim Credit, Debit As String
        Try
            sSql = "SELECT SUM(" & sdebit & ") from  Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_Status='A'"
            Debit = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "SELECT SUM(" & scredit & ") from  Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_Status='A'"
            Credit = objDB.SQLExecuteScalar(sNameSpace, sSql)

            If Debit > Credit Then
                MatchedCompanyDetails = Debit - Credit
                Return MatchedCompanyDetails
            Else
                MatchedCompanyDetails = Credit - Debit
                Return MatchedCompanyDetails
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UnmathedCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal sdebit As String, ByVal scredit As String) As String
        Dim sSql As String
        Dim Credit, Debit As String
        Try
            sSql = "SELECT SUM(" & sdebit & ") from  Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_Status='W'"
            Debit = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "SELECT SUM(" & scredit & ") from  Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_Status='W'"
            Credit = objDB.SQLExecuteScalar(sNameSpace, sSql)

            If Debit > Credit Then
                UnmathedCompanyDetails = Debit - Credit
                Return UnmathedCompanyDetails
            Else
                UnmathedCompanyDetails = Credit - Debit
                Return UnmathedCompanyDetails
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateDescriptionAmountNotexist(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankId As Integer, ByVal sComment As String) As Integer
        Dim sSql As String
        Try
            sSql = "update Acc_Bank_Reconcilation set ABR_Comment='" & sComment & "',ABR_Status='A' where ABR_CompID=" & iCompID & " and ABR_ID=" & iBankId & "  "
            UpdateDescriptionAmountNotexist = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransactionType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTransctionNo As String) As Integer
        Dim sSql As String
        Dim ID As Integer
        Try

            sSql = "select Acc_PM_ID from Acc_payment_master where Acc_PM_TransactionNo='" & sTransctionNo & "'"
            ID = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "select ATD_TrType from Acc_transactions_details where ATD_BillId=" & ID & ""
            GetTransactionType = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankId As Integer, ByVal sComment As String, ByVal iJEID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "update Acc_Bank_Reconcilation set ABR_Comment='" & sComment & "',ABR_Status='A',ABR_JID=" & iJEID & " where ABR_CompID=" & iCompID & " and ABR_ID=" & iBankId & " "
            UpdateDescription = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sql As String
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim slno As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("BankName")
            dt.Columns.Add("fromDate")
            dt.Columns.Add("Todate")
            dt.Columns.Add("SerialNo")
            dt.Columns.Add("TrType")
            dt.Columns.Add("ChequeDate")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Status")
            dt.Columns.Add("BnkID")
            dt.Columns.Add("CCredit")
            dt.Columns.Add("CDabit")
            dt.Columns.Add("ABR_ValueDate")
            dt.Columns.Add("Remark")
            dt.Columns.Add("lblchequeno")
            sql = "select * from Acc_Bank_Reconcilation where ABR_CompID=" & iCompID & " and ABR_YearID=" & iYearId & " and ABR_Status<>'A'"
            dt1 = objDB.SQLExecuteDataSet(sNameSpace, sql).Tables(0)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    slno = slno + 1
                    dRow("SrNo") = slno
                    If IsDBNull(dt1.Rows(i)("ABR_ID")) = False Then
                        dRow("BnkID") = dt1.Rows(i)("ABR_ID")
                    Else
                        dRow("BnkID") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Bank")) = False Then
                        dRow("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt1.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & " ")
                    Else
                        dRow("BankName") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_FromDate")) = False Then
                        dRow("fromDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_FromDate"), "D")
                    Else
                        dRow("fromDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ToDate")) = False Then
                        dRow("Todate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ToDate"), "D")
                    Else
                        dRow("Todate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TransactionNo")) = False Then
                        dRow("SerialNo") = dt1.Rows(i)("ABR_TransactionNo")
                    Else
                        dRow("SerialNo") = ""
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_ValueDate")) = False Then
                        dRow("ABR_ValueDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ValueDate"), "D")
                    Else
                        dRow("ABR_ValueDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TrType")) = False Then
                        If dt1.Rows(i)("ABR_TrType") = 1 Then
                            dRow("TrType") = "PAYMENT"
                        ElseIf dt1.Rows(i)("ABR_TrType") = 3 Then
                            dRow("TrType") = "RECEIPT"
                        End If
                    Else
                        dRow("TrType") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ChequeDate")) = False Then
                        dRow("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ChequeDate"), "D")
                    Else
                        dRow("ChequeDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Debit")) = False Then
                        dRow("Debit") = Convert.ToDecimal(dt1.Rows(i)("ABR_Debit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("Debit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Credit")) = False Then
                        dRow("Credit") = Convert.ToDecimal(dt1.Rows(i)("ABR_Credit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("Credit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CCradit")) = False Then
                        dRow("CCredit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CCradit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CCredit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CDabit")) = False Then
                        dRow("CDabit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CDabit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CDabit") = "0.00"
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_Comment")) = False Then
                        dRow("Remark") = dt1.Rows(i)("ABR_Comment").ToString()
                    Else
                        dRow("Remark") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Status")) = False Then
                        If dt1.Rows(i)("ABR_Status").ToString() = "W" Then
                            dRow("Status") = "Amount Not Matched"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "C" Then
                            dRow("Status") = "amount not exist in Bank"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "B" Then
                            dRow("Status") = "amount not exist in company"
                        End If
                    Else
                        dRow("ABR_Status") = ""
                    End If
                    dRow("lblchequeno") = dt1.Rows(i)("lblchequeno").ToString()
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReconciliation1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dr As DataRow
        Try
            dt1.Columns.Add("SlrNo")
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("TrType")
            dt1.Columns.Add("TransactionNo")
            dt1.Columns.Add("TxnDate")
            dt1.Columns.Add("ChequeNo")
            dt1.Columns.Add("ChequeDate")
            dt1.Columns.Add("IFSCCODEBNK")
            dt1.Columns.Add("BankName")
            dt1.Columns.Add("BranchName")
            dt1.Columns.Add("Debit")
            dt1.Columns.Add("Credit")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("RefNo")
            dt1.Columns.Add("BranchCode")
            dt1.Columns.Add("Balance")
            dt1.Columns.Add("Status")
            dt1.Columns.Add("CDebit")
            dt1.Columns.Add("CCredit")
            dt1.Columns.Add("Creditresult")
            dt1.Columns.Add("Debitresult")
            dt1.Columns.Add("CompDebitresult")
            dt1.Columns.Add("CompCreditresult")

            sSql = "select * from Acc_Bank_Reconcilation where  ABR_CompID=" & iCompID & " and ABR_YearID=" & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dr = dt1.NewRow
                    dr("SerialNo") = dt.Rows(i)("ABR_ID")
                    If IsDBNull(dt.Rows(i)("ABR_TrType")) = False Then
                        If dt.Rows(i)("ABR_TrType") = 1 Then
                            dr("TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrType") = 3 Then
                            dr("TrType") = "RECEIPT"
                        End If
                    Else
                        dr("TrType") = ""
                    End If
                    dr("TransactionNo") = dt.Rows(i)("ABR_TransactionNo")
                    dr("TxnDate") = dt.Rows(i)("ABR_TransactionDate")
                    dr("ChequeNo") = dt.Rows(i)("ABR_ChequeNo")
                    dr("ChequeDate") = dt.Rows(i)("ABR_ChequeDate")
                    dr("IFSCCODEBNK") = dt.Rows(i)("ABR_IFSCCode")
                    dr("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & "")
                    dr("BranchName") = ""
                    dr("Debit") = dt.Rows(i)("ABR_Debit")
                    dr("Credit") = dt.Rows(i)("ABR_Credit")
                    dr("Description") = dt.Rows(i)("ABR_Description")
                    dr("RefNo") = dt.Rows(i)("ABR_RefNo")
                    dr("BranchCode") = dt.Rows(i)("ABR_BranchCode")
                    dr("Balance") = dt.Rows(i)("ABR_Balance")
                    dr("Status") = dt.Rows(i)("ABR_Status")
                    dr("CDebit") = dt.Rows(i)("ABR_CDabit")
                    dr("CCredit") = dt.Rows(i)("ABR_CCradit")
                    dt1.Rows.Add(dr)
                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ADjustedAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sdebit As String, ByVal scredit As String, ByVal iJEID As Integer) As String
        Dim sSql As String
        Dim Credit, Debit As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "select * from acc_transactions_details where ATD_ID=" & iJEID & ""
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                ' sSql = "SELECT SUM(" & sdebit & ") from  acc_transactions_details where ATD_ID=" & iJEID & "  "
                sSql = "Select  sum(ATD_Debit) from Acc_Transactions_Details  left join acc_bank_reconcilation  On ATD_BillId=ABR_JID where ABR_JID<>0"
                Debit = objDB.SQLExecuteScalar(sNameSpace, sSql)
                sSql = "Select  sum(ATD_Credit) from Acc_Transactions_Details  left join acc_bank_reconcilation  On ATD_BillId=ABR_JID whre ABR_JID<>0"
                'sSql = "SELECT SUM(" & scredit & ") from  acc_transactions_details where  ATD_ID=" & iJEID & ""
                Credit = objDB.SQLExecuteScalar(sNameSpace, sSql)

                If Debit > Credit Then
                    ADjustedAmount = Debit - Credit
                    Return ADjustedAmount
                Else
                    ADjustedAmount = Credit - Debit
                    Return ADjustedAmount
                End If
            Else
                ADjustedAmount = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBankReconciliation3(ByVal sNamespace As String, ByVal iCompID As Integer, ByVal objBank As clsBankReconciliation) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Bank", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_Bank
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_FromDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_FromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_ToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_SerialNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objBank.ABR_SerialNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objBank.ABR_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ChequeNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objBank.ABR_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_IFSCCode", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objBank.ABR_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_BankTransDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_BankTransDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_ValueDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_ValueDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Description", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objBank.ABR_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_RefNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objBank.ABR_RefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_BranchCode", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objBank.ABR_BranchCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Debit", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Credit", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Balance", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_Balance
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_Status", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objBank.ABR_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABR_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.ABR_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_CDabit", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_CDabit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_CCradit", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objBank.ABR_CCradit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_VouchertypeIO", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objBank.ABR_Vouchertype
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABR_PostedDateIO", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objBank.dABR_PostedDateIO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ABRM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objBank.ABRM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNamespace, "spAcc_Bank_ReconcilationINOUT", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReconciliation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dr As DataRow
        Try
            dt1.Columns.Add("SlrNo")
            dt1.Columns.Add("SerialNo")
            dt1.Columns.Add("TrType")
            dt1.Columns.Add("TransactionNo")
            dt1.Columns.Add("TxnDate")
            dt1.Columns.Add("ChequeNo")
            dt1.Columns.Add("ChequeDate")
            dt1.Columns.Add("IFSCCODEBNK")
            dt1.Columns.Add("BankName")
            dt1.Columns.Add("BranchName")
            dt1.Columns.Add("Debit")
            dt1.Columns.Add("Credit")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("RefNo")
            dt1.Columns.Add("BranchCode")
            dt1.Columns.Add("Balance")
            dt1.Columns.Add("Status")
            dt1.Columns.Add("DabitDiff")
            dt1.Columns.Add("CreditDiff")
            dt1.Columns.Add("CDebit")
            dt1.Columns.Add("CCredit")
            dt1.Columns.Add("BCredit")
            dt1.Columns.Add("BDebit")
            dt1.Columns.Add("Creditresult")
            dt1.Columns.Add("Debitresult")
            dt1.Columns.Add("CompDebitresult")
            dt1.Columns.Add("CompCreditresult")
            dt1.Columns.Add("BillType")
            dt1.Columns.Add("Opening_Bal")

            sSql = "select * from Acc_Bank_Reconcilation where ABRM_ID=" & iMasterID & " and ABR_CompID=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dr = dt1.NewRow
                    dr("SerialNo") = dt.Rows(i)("ABR_ID")
                    If IsDBNull(dt.Rows(i)("ABR_TrType")) = False Then
                        If dt.Rows(i)("ABR_TrType") = 1 Then
                            dr("TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrType") = 3 Then
                            dr("TrType") = "RECEIPT"
                        End If
                    Else
                        dr("TrType") = ""
                    End If
                    dr("TransactionNo") = dt.Rows(i)("ABR_TransactionNo")
                    dr("TxnDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_TransactionDate"), "D")
                    dr("ChequeNo") = dt.Rows(i)("ABR_ChequeNo")
                    dr("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_ChequeDate"), "D")
                    dr("IFSCCODEBNK") = dt.Rows(i)("ABR_IFSCCode")
                    dr("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & "")
                    dr("BranchName") = objDB.SQLExecuteScalar(sNameSpace, "Select BD_BranchName From Acc_Company_BankDetails Where BD_BankName=" & dt.Rows(i)("ABR_Bank") & " And BD_CompID=" & iCompID & "")
                    dr("Debit") = dt.Rows(i)("ABR_Debit")
                    dr("Credit") = dt.Rows(i)("ABR_Credit")
                    dr("Description") = dt.Rows(i)("ABR_Description")
                    dr("RefNo") = dt.Rows(i)("ABR_RefNo")
                    dr("BranchCode") = dt.Rows(i)("ABR_BranchCode")
                    dr("Balance") = dt.Rows(i)("ABR_Balance")
                    dr("Status") = dt.Rows(i)("ABR_Status")
                    dr("DabitDiff") = "0"
                    dr("CreditDiff") = "0"
                    dr("CDebit") = dt.Rows(i)("ABR_CDabit")
                    dr("CCredit") = dt.Rows(i)("ABR_CCradit")
                    dr("Creditresult") = "0.00"
                    dr("Debitresult") = "0.00"
                    dr("CompDebitresult") = "0.00"
                    dr("CompCreditresult") = "0.00"
                    dr("BillType") = dt.Rows(i)("ABR_Vouchertype")
                    dr("Opening_Bal") = "0.00"
                    'dr("Opening_Bal") = dt.Rows(i)("ABR_ClosingBal")
                    dt1.Rows.Add(dr)
                Next
            End If
            dr = dt1.NewRow
            dr("SerialNo") = "" : dr("TrType") = "" : dr("TransactionNo") = ""
            dr("ChequeNo") = ""
            dr("ChequeDate") = ""
            dr("IFSCCODEBNK") = ""
            dr("BankName") = ""
            dr("BranchName") = ""
            dr("Debit") = ""
            dr("Credit") = ""
            dr("Description") = ""
            dr("RefNo") = ""
            dr("BranchCode") = ""
            dr("Balance") = ""
            dr("Status") = ""
            dr("DabitDiff") = "0"
            dr("CreditDiff") = "0"
            dr("CDebit") = ""
            dr("CCredit") = ""
            dr("BCredit") = ""
            dr("BDebit") = ""
            dr("Creditresult") = "0.00"
            dr("Debitresult") = "0.00"
            dr("CompDebitresult") = "0.00"
            dr("CompCreditresult") = "0.00"
            dr("BillType") = ""
            dr("Opening_Bal") = "0.00"
            dt1.Rows.Add(dr)

            sSql = "select * from Acc_Bank_ReconcilationInAndOut where ABR_CompIDIO=" & iCompID & " "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dr = dt1.NewRow
                    dr("SerialNo") = dt.Rows(i)("ABR_IDIO")
                    If IsDBNull(dt.Rows(i)("ABR_TrTypeIO")) = False Then
                        If dt.Rows(i)("ABR_TrTypeIO") = 1 Then
                            dr("TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrTypeIO") = 3 Then
                            dr("TrType") = "RECEIPT"
                        End If
                    Else
                        dr("TrType") = ""
                    End If
                    dr("TransactionNo") = dt.Rows(i)("ABR_TransactionNoIO")
                    dr("TxnDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_TransactionDateIO"), "D")
                    dr("ChequeNo") = dt.Rows(i)("ABR_ChequeNoIO")
                    dr("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_ChequeDateIO"), "D")
                    dr("IFSCCODEBNK") = dt.Rows(i)("ABR_IFSCCodeIO")
                    dr("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_BankIO") & " And gl_CompId=" & iCompID & "")
                    dr("BranchName") = objDB.SQLExecuteScalar(sNameSpace, "Select BD_BranchName From Acc_Company_BankDetails Where BD_BankName=" & dt.Rows(i)("ABR_BankIO") & " And BD_CompID=" & iCompID & "")
                    dr("Debit") = dt.Rows(i)("ABR_DebitIO")
                    dr("Credit") = dt.Rows(i)("ABR_CreditIO")
                    dr("Description") = dt.Rows(i)("ABR_DescriptionIO")
                    dr("RefNo") = dt.Rows(i)("ABR_RefNoIO")
                    dr("BranchCode") = dt.Rows(i)("ABR_BranchCodeIO")
                    dr("Balance") = dt.Rows(i)("ABR_BalanceIO")
                    dr("Status") = dt.Rows(i)("ABR_StatusIO")
                    dr("DabitDiff") = "0"
                    dr("CreditDiff") = "0"
                    dr("CDebit") = dt.Rows(i)("ABR_CDebitIO")
                    dr("CCredit") = dt.Rows(i)("ABR_CCreditIO")
                    dr("Creditresult") = "0.00"
                    dr("Debitresult") = "0.00"
                    dr("CompDebitresult") = "0.00"
                    dr("CompCreditresult") = "0.00"
                    dr("BillType") = dt.Rows(i)("ABR_VouchertypeIO")
                    dr("Opening_Bal") = "0.00"
                    dt1.Rows.Add(dr)
                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranchName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBbankId As Integer, ByVal branchname As String) As Integer
        Dim sSql As String = ""
        Dim iID As Integer
        Try
            sSql = "Select BD_ID From Acc_Company_BankDetails Where BD_BankName=" & iBbankId & " And BD_BranchName='" & branchname & "' and BD_CompID=" & iCompID & " "
            iID = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function PosttonextMonth(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatus As String, ByVal dpostdate As String, ByVal islrno As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "update Acc_Bank_ReconcilationInAndOut Set ABR_TransactionDateIO='" & clsgeneral.FormatDtForRDBMS(dpostdate, "CT") & "' where ABR_StatusIO='" & sStatus & "' and ABR_IDIO=" & islrno & " and ABR_CompIDIO=" & iCompID & ""
            PosttonextMonth = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return PosttonextMonth
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteMAtchedINOUTData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINoutID As Integer) As String
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "select ABR_IDIO from Acc_Bank_ReconcilationInAndOut where ABR_IDIO=" & iINoutID & " and ABR_StatusIO='C'"
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                sSql = "delete from Acc_Bank_ReconcilationInAndOut  where ABR_StatusIO='C' and ABR_IDIO=" & iINoutID & " and ABR_CompIDIO=" & iCompID & ""
                DeleteMAtchedINOUTData = objDB.SQLExecuteScalar(sNameSpace, sSql)
                Return DeleteMAtchedINOUTData
            Else
                DeleteMAtchedINOUTData = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function AmountAdjustedCompany(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sdebit As String, ByVal scredit As String) As String
        Dim sSql As String
        Dim Credit, Debit As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "select ABR_JID from acc_bank_reconcilation where ABR_JID<>0"
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                ' sSql = "SELECT SUM(" & sdebit & ") from  acc_transactions_details where ATD_ID=" & iJEID & "  "
                sSql = "Select  sum(ATD_Debit) from Acc_Transactions_Details  left join acc_bank_reconcilation  On ATD_ID=ABR_JID where ABR_JID<>0"
                Debit = objDB.SQLExecuteScalar(sNameSpace, sSql)
                sSql = "Select  sum(ATD_Credit) from Acc_Transactions_Details  left join acc_bank_reconcilation  On ATD_ID=ABR_JID where ABR_JID<>0"
                'sSql = "SELECT SUM(" & scredit & ") from  acc_transactions_details where  ATD_ID=" & iJEID & ""
                Credit = objDB.SQLExecuteScalar(sNameSpace, sSql)

                If Debit > Credit Then
                    AmountAdjustedCompany = Debit - Credit
                    Return AmountAdjustedCompany
                Else
                    AmountAdjustedCompany = Credit - Debit
                    Return AmountAdjustedCompany
                End If
            Else
                AmountAdjustedCompany = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function BRSReport(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal ibankID As Integer, ByVal imasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dr As DataRow
        Try
            dt1.Columns.Add("SlrNo")
            dt1.Columns.Add("TrType")
            dt1.Columns.Add("TransactionNo")
            dt1.Columns.Add("TxnDate")
            dt1.Columns.Add("ChequeNo")
            dt1.Columns.Add("ChequeDate")
            dt1.Columns.Add("BankName")
            dt1.Columns.Add("BranchName")
            dt1.Columns.Add("Debit")
            dt1.Columns.Add("Credit")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("CDebit")
            dt1.Columns.Add("CCredit")
            dt1.Columns.Add("BillType")
            dt1.Columns.Add("Status")

            sSql = "select * from Acc_Bank_Reconcilation where ABRM_ID=" & imasterID & " and ABR_Status='A' and ABR_Bank=" & ibankID & " and ABR_CompID=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dr = dt1.NewRow
                    dr("SlrNo") = dt.Rows(i)("ABR_ID")
                    If IsDBNull(dt.Rows(i)("ABR_TrType")) = False Then
                        If dt.Rows(i)("ABR_TrType") = 1 Then
                            dr("TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrType") = 3 Then
                            dr("TrType") = "RECEIPT"
                        End If
                    Else
                        dr("TrType") = ""
                    End If
                    dr("TransactionNo") = dt.Rows(i)("ABR_TransactionNo")
                    dr("TxnDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_TransactionDate"), "D")
                    dr("ChequeNo") = dt.Rows(i)("ABR_ChequeNo")
                    dr("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_ChequeDate"), "D")
                    dr("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & "")
                    dr("BranchName") = objDB.SQLExecuteScalar(sNameSpace, "Select BD_ID,BD_BranchName From Acc_Company_BankDetails Where BD_BankName=" & dt.Rows(i)("ABR_Bank") & " And BD_CompID=" & iCompID & "")
                    dr("Debit") = dt.Rows(i)("ABR_Debit")
                    dr("Credit") = dt.Rows(i)("ABR_Credit")
                    dr("Description") = dt.Rows(i)("ABR_Description")
                    dr("CDebit") = dt.Rows(i)("ABR_CDabit")
                    dr("CCredit") = dt.Rows(i)("ABR_CCradit")
                    dr("BillType") = dt.Rows(i)("ABR_Vouchertype")
                    dr("Status") = dt.Rows(i)("ABR_Status")
                    If dt.Rows(i)("ABR_Status") = "A" Then
                        dr("Status") = "Approved"
                    ElseIf dt.Rows(i)("ABR_Status") = "W" Then
                        dr("Status") = "Not Matched"
                    ElseIf dt.Rows(i)("ABR_Status") = "B" Then
                        dr("Status") = "Amount Exist in Bank but not in company"
                    ElseIf dt.Rows(i)("ABR_Status") = "C" Then
                        dr("Status") = "Amount Exist in Company but not in Bank"
                    End If


                    dt1.Rows.Add(dr)
                Next
            End If

            dr = dt1.NewRow
            dr("SlrNo") = "" : dr("TrType") = "" : dr("TransactionNo") = "" : dr("TxnDate") = ""
            dr("ChequeNo") = ""
            dr("ChequeDate") = ""
            dr("BankName") = ""
            dr("BranchName") = ""
            dr("Debit") = ""
            dr("Credit") = ""
            dr("Description") = ""
            dr("CDebit") = ""
            dr("CCredit") = ""
            dr("BillType") = ""
            dr("Status") = ""
            dt1.Rows.Add(dr)

            sSql = "select * from Acc_Bank_Reconcilation where ABRM_ID=" & imasterID & " and ABR_Status='W'and ABR_Bank=" & ibankID & " and ABR_CompID=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dr = dt1.NewRow
                    dr("SlrNo") = dt.Rows(i)("ABR_ID")
                    If IsDBNull(dt.Rows(i)("ABR_TrType")) = False Then
                        If dt.Rows(i)("ABR_TrType") = 1 Then
                            dr("TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrType") = 3 Then
                            dr("TrType") = "RECEIPT"
                        End If
                    Else
                        dr("TrType") = ""
                    End If
                    dr("TransactionNo") = dt.Rows(i)("ABR_TransactionNo")
                    dr("TxnDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_TransactionDate"), "D")
                    dr("ChequeNo") = dt.Rows(i)("ABR_ChequeNo")
                    dr("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_ChequeDate"), "D")
                    dr("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & "")
                    dr("BranchName") = objDB.SQLExecuteScalar(sNameSpace, "Select BD_ID,BD_BranchName From Acc_Company_BankDetails Where BD_BankName=" & dt.Rows(i)("ABR_Bank") & " And BD_CompID=" & iCompID & "")
                    dr("Debit") = dt.Rows(i)("ABR_Debit")
                    dr("Credit") = dt.Rows(i)("ABR_Credit")
                    dr("Description") = dt.Rows(i)("ABR_Description")
                    dr("CDebit") = dt.Rows(i)("ABR_CDabit")
                    dr("CCredit") = dt.Rows(i)("ABR_CCradit")
                    dr("BillType") = dt.Rows(i)("ABR_Vouchertype")
                    'dr("Status") = dt.Rows(i)("ABR_Status")
                    If dt.Rows(i)("ABR_Status") = "A" Then
                        dr("Status") = "Approved"
                    ElseIf dt.Rows(i)("ABR_Status") = "W" Then
                        dr("Status") = "Not Matched"
                    ElseIf dt.Rows(i)("ABR_Status") = "B" Then
                        dr("Status") = "Amount Exist in Bank but not in company"
                    ElseIf dt.Rows(i)("ABR_Status") = "C" Then
                        dr("Status") = "Amount Exist in Company but not in Bank"
                    End If
                    dt1.Rows.Add(dr)
                Next
            End If
            dr = dt1.NewRow
            dr("SlrNo") = "" : dr("TrType") = "" : dr("TransactionNo") = "" : dr("TxnDate") = ""
            dr("ChequeNo") = ""
            dr("ChequeDate") = ""
            dr("BankName") = ""
            dr("BranchName") = ""
            dr("Debit") = ""
            dr("Credit") = ""
            dr("Description") = ""
            dr("CDebit") = ""
            dr("CCredit") = ""
            dr("BillType") = ""
            dr("Status") = ""
            dt1.Rows.Add(dr)

            sSql = "select * from acc_bank_reconcilationINANDOUT where ABRM_IDIO=" & imasterID & " and ABR_StatusIO='B' and ABR_BankIO=" & ibankID & " and ABR_CompIDIO=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dt1.NewRow
                    dr("SlrNo") = dt.Rows(i)("ABR_IDIO")
                    If IsDBNull(dt.Rows(i)("ABR_TrTypeIO")) = False Then
                        If dt.Rows(i)("ABR_TrTypeIO") = 1 Then
                            dr("TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrTypeIO") = 3 Then
                            dr("TrType") = "RECEIPT"
                        End If
                    Else
                        dr("TrType") = ""
                    End If
                    dr("TransactionNo") = dt.Rows(i)("ABR_TransactionNoIO")
                    dr("TxnDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_TransactionDateIO"), "D")
                    dr("ChequeNo") = dt.Rows(i)("ABR_ChequeNoIO")
                    dr("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_ChequeDateIO"), "D")
                    dr("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_BankIO") & " And gl_CompId=" & iCompID & "")
                    dr("BranchName") = objDB.SQLExecuteScalar(sNameSpace, "Select BD_ID,BD_BranchName From Acc_Company_BankDetails Where BD_BankName=" & dt.Rows(i)("ABR_BankIO") & " And BD_CompID=" & iCompID & "")
                    dr("Debit") = dt.Rows(i)("ABR_DebitIO")
                    dr("Credit") = dt.Rows(i)("ABR_CreditIO")
                    dr("Description") = dt.Rows(i)("ABR_DescriptionIO")
                    dr("CDebit") = dt.Rows(i)("ABR_CDebitIO")
                    dr("CCredit") = dt.Rows(i)("ABR_CCreditIO")
                    dr("BillType") = dt.Rows(i)("ABR_VouchertypeIO")
                    'dr("Status") = dt.Rows(i)("ABR_StatusIO")
                    If dt.Rows(i)("ABR_StatusIO") = "A" Then
                        dr("Status") = "Approved"
                    ElseIf dt.Rows(i)("ABR_StatusIO") = "W" Then
                        dr("Status") = "Not Matched"
                    ElseIf dt.Rows(i)("ABR_StatusIO") = "B" Then
                        dr("Status") = "Amount Exist in Bank but not in company"
                    ElseIf dt.Rows(i)("ABR_StatusIO") = "C" Then
                        dr("Status") = "Amount Exist in Company but not in Bank"
                    End If
                    dt1.Rows.Add(dr)
                Next
            End If
            dr = dt1.NewRow
            dr("SlrNo") = "" : dr("TrType") = "" : dr("TransactionNo") = "" : dr("TxnDate") = ""
            dr("ChequeNo") = ""
            dr("ChequeDate") = ""
            dr("BankName") = ""
            dr("BranchName") = ""
            dr("Debit") = ""
            dr("Credit") = ""
            dr("Description") = ""
            dr("CDebit") = ""
            dr("CCredit") = ""
            dr("BillType") = ""
            dr("Status") = ""
            dt1.Rows.Add(dr)
            sSql = "select * from acc_bank_reconcilationINANDOUT where ABRM_IDIO=" & imasterID & " and ABR_StatusIO='C' and ABR_BankIO=" & ibankID & " and ABR_CompIDIO=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dt1.NewRow
                    dr("SlrNo") = dt.Rows(i)("ABR_IDIO")
                    If IsDBNull(dt.Rows(i)("ABR_TrTypeIO")) = False Then
                        If dt.Rows(i)("ABR_TrTypeIO") = 1 Then
                            dr("TrType") = "PAYMENT"
                        ElseIf dt.Rows(i)("ABR_TrTypeIO") = 3 Then
                            dr("TrType") = "RECEIPT"
                        End If
                    Else
                        dr("TrType") = ""
                    End If
                    dr("TransactionNo") = dt.Rows(i)("ABR_TransactionNoIO")
                    dr("TxnDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_TransactionDateIO"), "D")
                    dr("ChequeNo") = dt.Rows(i)("ABR_ChequeNoIO")
                    dr("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("ABR_ChequeDateIO"), "D")
                    dr("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt.Rows(i)("ABR_BankIO") & " And gl_CompId=" & iCompID & "")
                    dr("BranchName") = objDB.SQLExecuteScalar(sNameSpace, "Select BD_ID,BD_BranchName From Acc_Company_BankDetails Where BD_BankName=" & dt.Rows(i)("ABR_BankIO") & " And BD_CompID=" & iCompID & "")
                    dr("Debit") = dt.Rows(i)("ABR_DebitIO")
                    dr("Credit") = dt.Rows(i)("ABR_CreditIO")
                    dr("Description") = dt.Rows(i)("ABR_DescriptionIO")
                    dr("CDebit") = dt.Rows(i)("ABR_CDebitIO")
                    dr("CCredit") = dt.Rows(i)("ABR_CCreditIO")
                    dr("BillType") = dt.Rows(i)("ABR_VouchertypeIO")
                    'dr("Status") = dt.Rows(i)("ABR_StatusIO")
                    If dt.Rows(i)("ABR_StatusIO") = "A" Then
                        dr("Status") = "Approved"
                    ElseIf dt.Rows(i)("ABR_StatusIO") = "W" Then
                        dr("Status") = "Not Matched"
                    ElseIf dt.Rows(i)("ABR_StatusIO") = "B" Then
                        dr("Status") = "Amount Exist in Bank but not in company"
                    ElseIf dt.Rows(i)("ABR_StatusIO") = "C" Then
                        dr("Status") = "Amount Exist in Company but not in Bank"
                    End If
                    dt1.Rows.Add(dr)
                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BankName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal ibankID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select gl_desc from  chart_of_Accounts Where gl_id=" & ibankID & " And gl_CompId=" & iCompID & ""
            BankName = objDB.SQLGetDescription(sNameSpace, sSql)
            Return BankName
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class


