Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsPurchaseVrification
    Private dTD_ExciseDuty As Decimal
    Private dTD_Frieght As Decimal
    Private dTD_Total As Decimal
    Private dTD_VATOrCST As Decimal
    Private dTD_TaxAmount As Decimal
    Private dTD_Rate As Decimal
    Dim objDb As New DBHelper
    Dim objFasGnrl As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions

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
    Private Acc_PJE_Type As String

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

    Public Property sAcc_PJE_Type() As String
        Get
            Return (Acc_PJE_Type)
        End Get
        Set(ByVal Value As String)
            Acc_PJE_Type = Value
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
    Public Property dAcc_JE_PendingAmount() As Decimal
        Get
            Return (Acc_JE_PendingAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_PendingAmount = Value
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


    Public Property TD_ExciseDuty() As Decimal
        Get
            Return (dTD_ExciseDuty)
        End Get
        Set(ByVal Value As Decimal)
            dTD_ExciseDuty = Value
        End Set
    End Property
    Public Property TD_Frieght() As Decimal
        Get
            Return (dTD_Frieght)
        End Get
        Set(ByVal Value As Decimal)
            dTD_Frieght = Value
        End Set
    End Property
    Public Property TD_Total() As Decimal
        Get
            Return (dTD_Total)
        End Get
        Set(ByVal Value As Decimal)
            dTD_Total = Value
        End Set
    End Property
    Public Property TD_VATOrCST() As Decimal
        Get
            Return (dTD_VATOrCST)
        End Get
        Set(ByVal Value As Decimal)
            dTD_VATOrCST = Value
        End Set
    End Property
    Public Property TD_TaxAmount() As Decimal
        Get
            Return (dTD_TaxAmount)
        End Get
        Set(ByVal Value As Decimal)
            dTD_TaxAmount = Value
        End Set
    End Property
    Public Property TD_Rate() As Decimal
        Get
            Return (dTD_Rate)
        End Get
        Set(ByVal Value As Decimal)
            dTD_Rate = Value
        End Set
    End Property

    Public Function GetMasterData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Purchase_GIN_Master Where PGM_DocumentRefNo='" & iMasterID & "' and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTransactionType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select TT_ID,TT_Description From Transaction_Type Where TT_Parent = 1 And TT_CompID=" & iCompID & " order by TT_Description"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllInvoices(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal GinID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select TRD_ID,TRD_PINo From Transaction_returns_Details Where TRD_GINRefID = " & GinID & " And TRD_CompID=" & iCompID & " order by TRD_ID"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOurRefNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = "", sTransactionType As String = ""
        Try
            sSql = "select POM_ID,POM_OrderNo from PURCHASE_ORDER_MASTER where POM_ID in(select PRM_OrderNo from purchase_registry_MASTER where 
PRM_DocumentRefNo not in(select PV_DocRefNo from purchase_verification)) intersect 
select POM_ID,POM_OrderNo from PURCHASE_ORDER_MASTER where POM_ID in(select PGM_OrderID from purchase_gin_MASTER where 
PGM_DocumentRefNo not in(select PV_DocRefNo from purchase_verification))"
            '      sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_Status<>'W' And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & " Order By POM_OrderNo desc"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMethodOfPayment(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=11 and Mas_Status='A'"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadPurchaseBill(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "Select PV_ID,PV_BillNo From Purchase_Verification Where PV_OrderNo=" & iTransactionID & " and PV_CompID=" & iCompID & ""
    '        Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadPurchaseBill(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select PV_ID,PV_BillNo From Purchase_Verification Where PV_OrderNo=" & iTransactionID & " and PV_CompID=" & iCompID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDateFromInward(ByVal sNameSpace As String, ByVal Inward As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As DateTime
        Dim sSql As String = ""
        Try
            sSql = "select PGM_InvoiceDate from Purchase_GIN_Master where PGM_DocumentRefNo='" & Inward & "' and PGM_YearID = " & iyearID & " and PGM_CompID =" & ICompID & " and PGM_OrderID =" & IOrderID & " "
            Return objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getVerificationID(ByVal sNameSpace As String, ByVal Inward As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select  top 1 PV_ID from  Purchase_Verification where PV_OrderNo=" & IOrderID & " and PV_DocRefNo='" & Inward & "' and PV_CompID=" & ICompID & " order by  PV_ID DESC"
            Return objDb.SQLGetDescription(sNameSpace, sSql)
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


    Public Function CheckVerificationNo(ByVal sNameSpace As String, ByVal Inward As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * from Purchase_verification where PV_DocRefNo='" & Inward & "' and PV_YearID =" & iyearID & " and PV_CompID =" & ICompID & " and PV_OrderNo=" & IOrderID & ""
            Return objDb.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckRegisterNo(ByVal sNameSpace As String, ByVal REGDocRef As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "select * from Purchase_Registry_master where PRM_DocumentRefNo='" & REGDocRef & "' and PRM_YearID =" & iyearID & " and PRM_CompID =" & ICompID & " and PRM_Status = 'A' And PRM_OrderNo=" & IOrderID & ""
            'sSql = "Select * from Purchase_verification where PV_DocRefNo='" & Inward & "' and PV_YearID =" & iyearID & " and PV_CompID =" & ICompID & " and PV_OrderNo='" & IOrderID & "'"
            Return objDb.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function CheckInwardNo(ByVal sNameSpace As String, ByVal REGDocRef As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "select * from Purchase_GIN_Master where PGM_DocumentRefNo='" & REGDocRef & "' and PGM_YearID =" & iyearID & " and PGM_CompID =" & ICompID & " and PGM_Status = 'A' And PGM_OrderID=" & IOrderID & ""
            'sSql = "Select * from Purchase_verification where PV_DocRefNo='" & Inward & "' and PV_YearID =" & iyearID & " and PV_CompID =" & ICompID & " and PV_OrderNo='" & IOrderID & "'"
            Return objDb.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    'Public Function UpdateTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer)
    '    Dim dt, dt1 As New DataTable
    '    Dim dtTab As New DataTable
    '    Dim dr As DataRow
    '    Dim vat As Decimal
    '    Dim total, TotalWithCst, TotalWithVat, cst As Decimal
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "Select * From Transaction_Invoice_Details Where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & iGINID & "') and TID_CompID=" & iCompID & " and TID_NewFlag='A' and TID_ExcessFlag='A'"
    '        dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
    '        For i = 0 To dt.Rows.Count - 1

    '            If IsDBNull(dt.Rows(i)("TID_POVAT")) Then
    '                vat = 0
    '            Else
    '                vat = dt.Rows(i)("TID_POVAT")
    '            End If
    '            If IsDBNull(dt.Rows(i)("TID_CST")) Then
    '                cst = 0
    '            Else
    '                cst = dt.Rows(i)("TID_CST")
    '            End If

    '            vat = Convert.ToDecimal(((dt.Rows(i)("TID_PredeterminedPrice") * dt.Rows(i)("TID_Quantity")) * Convert.ToDecimal(vat)) / 100)
    '            ' dr("RowTotal") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("TID_PredeterminedPrice") * dt.Rows(i)("TID_Quantity") + vat).ToString())
    '            Dim RegRefID As String = objDb.SQLGetDescription(sNameSpace, "select GIN_DocRefNo from Goods_InwardNote_Master where GIN_ID=" & dt.Rows(i)("TID_GINRefID") & "")
    '            If (RegRefID = "") Then
    '                RegRefID = 0
    '            End If
    '            Dim rateInReg As String = objDb.SQLGetDescription(sNameSpace, "select PRD_MRP from purchase_register_details where PRD_MasterID in(select PR_ID from purchase_register where PR_DocRefNo='" & RegRefID & "') and PRD_ItemID=" & dt.Rows(i)("TID_ItemID") & "")
    '            If (rateInReg = "") Then
    '                rateInReg = 0
    '            Else
    '                If (Convert.ToDecimal(rateInReg) > Convert.ToDecimal(dt.Rows(i)("TID_PredeterminedPrice"))) Then
    '                    'UpdateInvoiceDetails(sNameSpace, RegRefID, dt.Rows(i)("TID_ItemID"))
    '                    sSql = "select * from purchase_register_details where PRD_MasterID in(select PR_ID from purchase_register where PR_DocRefNo='" & RegRefID & "') and PRD_ItemID=" & dt.Rows(i)("TID_ItemID") & ""
    '                    dt1 = objDb.SQLExecuteDataTable(sNameSpace, sSql)
    '                    total = dt1.Rows(0)("PRD_MRP") * dt.Rows(i)("TID_Quantity")
    '                    If IsDBNull(dt.Rows(i)("TID_CST")) Then
    '                        TotalWithCst = total
    '                    Else
    '                        TotalWithCst = total + dt.Rows(i)("TID_CST")
    '                    End If
    '                    vat = Convert.ToDecimal(((dt1.Rows(0)("PRD_MRP") * dt.Rows(i)("TID_Quantity")) * Convert.ToDecimal(vat)) / 100)
    '                    TotalWithVat = TotalWithCst + vat
    '                    sSql = "update Transaction_Invoice_Details set TID_PurchaseRate=" & total & ",TID_PredeterminedPrice=" & dt1.Rows(0)("PRD_MRP") & ",TID_Total=" & TotalWithCst & ",TID_ItemRowTotal=" & TotalWithVat & ",TID_DiffFlag='df'  where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "')  And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & ""
    '                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)

    '                    'DBHelper.ExecuteNoNQuery(sNameSpace, "update Transaction_Invoice_Details set TID_PurchaseRate=" & total & ",TID_PredeterminedPrice=" & dt.Rows(0)("PRD_MRP") & ",TID_Total=" & TotalWithCst & ",TID_ItemRowTotal=" & TotalWithVat & ",TID_DiffFlag='W'  where TID_GINRefID in(select GIND_ID from Goods_InwardNote_Master_details where GIND_MasterID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "') and GIND_ItemID=" & dt.Rows(i)("TID_ItemID") & ") And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & "")
    '                Else
    '                    sSql = "update Transaction_Invoice_Details set TID_DiffFlag='A'  where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "')  And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & ""
    '                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
    '                End If
    '            End If

    '        Next

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadInwardNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            If iTransactionID > 0 Then
                sSql = "" : sSql = "select PRM_ID,PRM_DocumentRefNo from Purchase_Registry_master where PRM_DocumentRefNo Not In"
                sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=" & iCompID & " And PV_OrderNo=" & iTransactionID & ")"
                sSql = sSql & "And PRM_OrderNo=" & iTransactionID & " And "
                sSql = sSql & "PRM_Status ='A' and PRM_CompID=" & iCompID & " "

                'sSql = sSql & "Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_DocumentRefNo Not In"
                'sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1  And PV_OrderNo=" & iTransactionID & ")  "
                'sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
                'sSql = sSql & "PGM_CompID=" & iCompID & " and PGM_Status='A'"
            Else
                sSql = "" : sSql = "select PRM_ID,PRM_DocumentRefNo from Purchase_Registry_master where PRM_Status ='A' and PRM_CompID=" & iCompID & " "
                'sSql = sSql & "Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_CompID=" & iCompID & " and PGM_Status='A'"
            End If
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadForApproval(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "select PRM_DocumentRefNo from Purchase_Registry_master where PRM_DocumentRefNo Not In"
            'sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1 And PV_OrderNo=" & iTransactionID & ")"
            'sSql = sSql & "And PRM_OrderNo=" & iTransactionID & " And "
            'sSql = sSql & "PRM_Status ='A' and PRM_CompID=1"
            'sSql = sSql & " intersect Select PGM_DocumentRefNo from Purchase_GIN_Master where PGM_DocumentRefNo Not In"
            'sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1  And PV_OrderNo=" & iTransactionID & ")  "
            'sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
            sSql = sSql & "select pgm_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_DocumentRefNo "
            sSql = sSql & " In( Select PRM_DocumentRefNo from Purchase_Registry_master) and"
            sSql = sSql & " PGM_CompID=" & iCompID & " And PGM_Status='A' and PGM_OrderID=" & iTransactionID & " "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function Order(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_Status='A' And POM_CompID=" & iCompID & " Order By POM_ID desc"

            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPIID As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Try
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Commodity")
            dtTab.Columns.Add("ItemName")
            dtTab.Columns.Add("Per")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TaxAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            sSql = "Select * From Transaction_Master_Details Where TMD_MasterID=" & iPIID & " And TMD_CompID=" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("ItemID") = dt.Rows(i)("TMD_ItemID")
                dr("Commodity") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("TMD_CommodityID") & " ")
                dr("ItemName") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("TMD_ItemID") & " ")
                dr("Per") = dt.Rows(i)("TMD_Per")
                dr("Quantity") = dt.Rows(i)("TMD_Quantity")
                dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("TMD_ItemRequiredDate"), "D")
                If IsDBNull(dt.Rows(i)("TMD_Quantity")) = False And dt.Rows(i)("TMD_Quantity") > 0 Then
                    dr("Rate") = dt.Rows(i)("TMD_PurchaseRate")
                Else
                    dr("Rate") = ""
                End If

                If IsDBNull(objDb.SQLCheckForRecord(sNameSpace, "Select INV_Excise From Inventory_Master Where INV_ID=" & dt.Rows(i)("TMD_ItemID") & " ")) = False Then
                    If objDb.SQLExecuteScalar(sNameSpace, "Select INV_Excise From Inventory_Master Where INV_ID=" & dt.Rows(i)("TMD_ItemID") & " ") > 0 Then
                        dr("ExiceDuty") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Excise From Inventory_Master Where INV_ID=" & dt.Rows(i)("TMD_ItemID") & " ")
                    Else
                        dr("ExiceDuty") = ""
                    End If
                Else
                    dr("ExiceDuty") = ""
                End If
                If IsDBNull(dt.Rows(i)("TMD_Frieght")) = False Then
                    dr("Frieght") = dt.Rows(i)("TMD_Frieght")
                Else
                    dr("Frieght") = ""
                End If
                If IsDBNull(dt.Rows(i)("TMD_Total")) = False Then
                    dr("Total") = dt.Rows(i)("TMD_Total")
                Else
                    dr("Total") = ""
                End If
                If IsDBNull(dt.Rows(i)("TMD_POVAT")) = False Then
                    dr("Vat") = dt.Rows(i)("TMD_POVAT")
                Else
                    dr("Vat") = ""
                End If
                If IsDBNull(dt.Rows(i)("TMD_CST")) = False Then
                    dr("CST") = dt.Rows(i)("TMD_CST")
                Else
                    dr("CST") = ""
                End If
                If IsDBNull(dt.Rows(i)("TMD_TaxAmount")) = False Then
                    dr("TaxAmount") = dt.Rows(i)("TMD_TaxAmount")
                Else
                    dr("TaxAmount") = ""
                End If
                If IsDBNull(dt.Rows(i)("TMD_Remarks")) = False Then
                    dr("Remarks") = dt.Rows(i)("TMD_Remarks")
                Else
                    dr("Remarks") = ""
                End If
                If IsDBNull(dt.Rows(i)("TMD_ItemRowTotal")) = False Then
                    dr("RowTotal") = dt.Rows(i)("TMD_ItemRowTotal")
                Else
                    dr("RowTotal") = ""
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetApprovedTransactionToPrint(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal BillNo As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim bill As String
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("HistoryID")
            ' bill = DBHelper.GetDescription(sNameSpace, "select PV_BillNo from Purchase_Verification where PV_ID=" & BillNo & "")
            sSql = " Select * From Transaction_Invoice_Details Where TID_GinRefID In(Select PV_GinNo from Purchase_Verification where PV_ID=" & BillNo & ") and TID_CompID=" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("Invoice_No") = objDb.SQLGetDescription(sNameSpace, "Select GIN_GoodInwardNo From Goods_InwardNote_Master Where GIN_ID =" & dt.Rows(i)("TID_GINRefID") & "")
                dr("CommodityID") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("TID_ItemID") & " ")
                dr("ItemID") = dt.Rows(i)("TID_ItemID")
                dr("Goods") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("TID_ItemID") & "")
                dr("StdUnit") = objDb.SQLExecuteScalar(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("TID_Per") & "")
                dr("Pieces") = dt.Rows(i)("TID_Quantity")
                dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("TID_ItemRequiredDate"), "D")
                dr("Amount") = dt.Rows(i)("TID_PredeterminedPrice")
                dr("ExiceDuty") = dt.Rows(i)("TID_ExciseDuty")
                dr("Frieght") = dt.Rows(i)("TID_Frieght")
                dr("Total") = dt.Rows(i)("TID_Total")

                If IsDBNull(dt.Rows(i)("TID_POVAT")) Then
                    dr("Vat") = ""
                Else
                    dr("Vat") = dt.Rows(i)("TID_POVAT")
                End If
                If IsDBNull(dt.Rows(i)("TID_CST")) Then
                    dr("CST") = ""
                Else
                    dr("CST") = dt.Rows(i)("TID_CST")
                End If
                dr("TAXAmount") = dt.Rows(i)("TID_TaxAmount")
                dr("Remarks") = dt.Rows(i)("TID_Remarks")
                dr("RowTotal") = dt.Rows(i)("TID_ItemRowTotal")
                dr("HistoryID") = dt.Rows(i)("TID_HistoryID")
                ' dr("ExcessAmount") = Convert.ToDecimal((dt.Rows(i)("TID_PurchaseRate")) - Convert.ToDecimal(rateInReg))
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Function GetApprovedTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal BillNo As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim bill As String
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("HistoryID")
            ' bill = DBHelper.GetDescription(sNameSpace, "select PV_BillNo from Purchase_Verification where PV_ID=" & BillNo & "")
            sSql = " Select * From Transaction_Invoice_Details Where TID_GinRefID In(Select PV_GinNo from Purchase_Verification where PV_ID=" & BillNo & ") and TID_CompID=" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("Invoice_No") = objDb.SQLExecuteScalar(sNameSpace, "Select GIN_GoodInwardNo From Goods_InwardNote_Master Where GIN_ID =" & dt.Rows(i)("TID_GINRefID") & "")
                dr("CommodityID") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("TID_ItemID") & " ")
                dr("ItemID") = dt.Rows(i)("TID_ItemID")
                dr("Goods") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("TID_ItemID") & "")
                dr("StdUnit") = objDb.SQLExecuteScalar(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("TID_Per") & "")
                dr("Pieces") = dt.Rows(i)("TID_Quantity")
                dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("TID_ItemRequiredDate"), "D")
                dr("Amount") = dt.Rows(i)("TID_PredeterminedPrice")
                dr("ExiceDuty") = dt.Rows(i)("TID_ExciseDuty")
                dr("Frieght") = dt.Rows(i)("TID_Frieght")
                dr("Total") = dt.Rows(i)("TID_Total")

                If IsDBNull(dt.Rows(i)("TID_POVAT")) Then
                    dr("Vat") = ""
                Else
                    dr("Vat") = dt.Rows(i)("TID_POVAT")
                End If
                If IsDBNull(dt.Rows(i)("TID_CST")) Then
                    dr("CST") = ""
                Else
                    dr("CST") = dt.Rows(i)("TID_CST")
                End If
                dr("TAXAmount") = dt.Rows(i)("TID_TaxAmount")
                dr("Remarks") = dt.Rows(i)("TID_Remarks")
                dr("RowTotal") = dt.Rows(i)("TID_ItemRowTotal")
                dr("HistoryID") = dt.Rows(i)("TID_HistoryID")
                ' dr("ExcessAmount") = Convert.ToDecimal((dt.Rows(i)("TID_PurchaseRate")) - Convert.ToDecimal(rateInReg))
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function GetOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal BillNo As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim bill As String
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("discount")
            dtTab.Columns.Add("DiscountAmount")
            ' bill = DBHelper.GetDescription(sNameSpace, "select PV_BillNo from Purchase_Verification where PV_ID=" & BillNo & "")
            sSql = " Select * From Transaction_Master_details Where TMD_MasterID =" & BillNo & " and TMD_CompID=" & iCompID & ""
            dt = objDb.SQLExecuteScalar(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("Invoice_No") = "" 'DBHelper.GetDescription(sNameSpace, "Select GIN_GoodInwardNo From Goods_InwardNote_Master Where GIN_ID =" & dt.Rows(i)("TMD_GINRefID") & "")
                dr("CommodityID") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("TMD_ItemID") & " ")
                dr("ItemID") = dt.Rows(i)("TMD_ItemID")
                dr("Goods") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("TMD_ItemID") & "")
                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("TMD_Per") & "")
                dr("Pieces") = dt.Rows(i)("TMD_Quantity")
                dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("TMD_ItemRequiredDate"), "D")
                dr("Amount") = dt.Rows(i)("TMD_PredeterminedPrice")
                dr("ExiceDuty") = dt.Rows(i)("TMD_ExciseDuty")
                dr("Frieght") = dt.Rows(i)("TMD_Frieght")
                dr("Total") = dt.Rows(i)("TMD_PurchaseRate")

                If IsDBNull(dt.Rows(i)("TMD_POVAT")) Then
                    dr("Vat") = ""
                Else
                    dr("Vat") = dt.Rows(i)("TMD_POVAT")
                End If
                If IsDBNull(dt.Rows(i)("TMD_CST")) Then
                    dr("CST") = ""
                Else
                    dr("CST") = dt.Rows(i)("TMD_CST")
                End If

                If IsDBNull(dt.Rows(i)("TMD_discount")) Then
                    dr("discount") = ""
                Else
                    dr("discount") = dt.Rows(i)("TMD_discount")
                End If

                If IsDBNull(dt.Rows(i)("TMD_DiscountAmount")) Then
                    dr("DiscountAmount") = ""
                Else
                    dr("DiscountAmount") = dt.Rows(i)("TMD_DiscountAmount")
                End If
                dr("TAXAmount") = dt.Rows(i)("TMD_TaxAmount")
                dr("Remarks") = dt.Rows(i)("TMD_Remarks")
                dr("RowTotal") = dt.Rows(i)("TMD_ItemRowTotal")
                dr("HistoryID") = dt.Rows(i)("TMD_HistoryID")
                ' dr("ExcessAmount") = Convert.ToDecimal((dt.Rows(i)("TID_PurchaseRate")) - Convert.ToDecimal(rateInReg))
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetApprovedTransactionDabitNote(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As String) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim bill As String
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("Flag")
            dtTab.Columns.Add("Status")
            sSql = "Select * From Transaction_Invoice_Details Where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & iGINID & "') and TID_Status in('N','E')  and TID_CompID=" & iCompID & " and  TID_flag in('A','N','E') and TID_Quantity<>0 "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("Invoice_No") = objDb.SQLGetDescription(sNameSpace, "Select GIN_GoodInwardNo From Goods_InwardNote_Master Where GIN_ID =" & dt.Rows(i)("TID_GINRefID") & "")
                dr("CommodityID") = objDb.SQLGetDescription(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("TID_ItemID") & " ")
                dr("ItemID") = dt.Rows(i)("TID_ItemID")
                dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("TID_ItemID") & "")
                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("TID_Per") & "")
                dr("Pieces") = dt.Rows(i)("TID_Quantity")
                dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("TID_ItemRequiredDate"), "D")
                dr("Amount") = dt.Rows(i)("TID_PredeterminedPrice")
                dr("ExiceDuty") = dt.Rows(i)("TID_ExciseDuty")
                dr("Frieght") = dt.Rows(i)("TID_Frieght")
                dr("Total") = dt.Rows(i)("TID_Total")
                If IsDBNull(dt.Rows(i)("TID_POVAT")) Then
                    dr("Vat") = ""
                Else
                    dr("Vat") = dt.Rows(i)("TID_POVAT")
                End If
                If IsDBNull(dt.Rows(i)("TID_CST")) Then
                    dr("CST") = ""
                Else
                    dr("CST") = dt.Rows(i)("TID_CST")
                End If
                dr("TAXAmount") = dt.Rows(i)("TID_TaxAmount")
                dr("Remarks") = dt.Rows(i)("TID_Remarks")
                dr("RowTotal") = dt.Rows(i)("TID_ItemRowTotal")
                dr("HistoryID") = dt.Rows(i)("TID_HistoryID")
                dr("Flag") = dt.Rows(i)("TID_Flag")
                dr("Status") = dt.Rows(i)("TID_Status")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetApprovedTransactionCreditNote(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As String) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim bill As String
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("Flag")
            dtTab.Columns.Add("Status")
            sSql = "Select * From Transaction_Invoice_Details Where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & iGINID & "') and TID_Status in('N','E')  and TID_CompID=" & iCompID & " and  TID_flag in('A','N','E') and TID_Quantity<>0 "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("Invoice_No") = objDb.SQLGetDescription(sNameSpace, "Select GIN_GoodInwardNo From Goods_InwardNote_Master Where GIN_ID =" & dt.Rows(i)("TID_GINRefID") & "")
                dr("CommodityID") = objDb.SQLGetDescription(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("TID_ItemID") & " ")
                dr("ItemID") = dt.Rows(i)("TID_ItemID")
                dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("TID_ItemID") & "")
                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("TID_Per") & "")
                dr("Pieces") = dt.Rows(i)("TID_Quantity")
                dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("TID_ItemRequiredDate"), "D")
                dr("Amount") = dt.Rows(i)("TID_PredeterminedPrice")
                dr("ExiceDuty") = dt.Rows(i)("TID_ExciseDuty")
                dr("Frieght") = dt.Rows(i)("TID_Frieght")
                dr("Total") = dt.Rows(i)("TID_Total")
                If IsDBNull(dt.Rows(i)("TID_POVAT")) Then
                    dr("Vat") = ""
                Else
                    dr("Vat") = dt.Rows(i)("TID_POVAT")
                End If
                If IsDBNull(dt.Rows(i)("TID_CST")) Then
                    dr("CST") = ""
                Else
                    dr("CST") = dt.Rows(i)("TID_CST")
                End If
                dr("TAXAmount") = dt.Rows(i)("TID_TaxAmount")
                dr("Remarks") = dt.Rows(i)("TID_Remarks")
                dr("RowTotal") = dt.Rows(i)("TID_ItemRowTotal")
                dr("HistoryID") = dt.Rows(i)("TID_HistoryID")
                dr("Flag") = dt.Rows(i)("TID_Flag")
                dr("Status") = dt.Rows(i)("TID_Status")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPOID As Integer) As String
        Dim sSql As String = ""
        Dim Count As String = ""
        Try
            sSql = "select IsNull(count(*),0)+1 from Purchase_Verification where PV_CompID=" & iCompID & " "
            Count = objDb.SQLExecuteScalar(sNameSpace, sSql)
            Return "-00" & Count
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUsers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select usr_Id,usr_FullName From Sad_Userdetails Where usr_Status='A' and usr_CompID=" & iCompID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetTransactionDetailsPI(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer, ByVal OrderNo As Integer) As DataTable
    '    Dim dt, dt1 As New DataTable
    '    Dim dtTab As New DataTable
    '    Dim dr As DataRow
    '    Dim vat As Decimal = 0
    '    Dim cst As Decimal = 0
    '    Dim sSql As String = ""
    '    Dim totAmount As Decimal = 0
    '    Try

    '        dtTab.Columns.Add("CommodityID")
    '        dtTab.Columns.Add("ItemID")
    '        dtTab.Columns.Add("Goods")
    '        dtTab.Columns.Add("StdUnit")
    '        dtTab.Columns.Add("Pieces")
    '        dtTab.Columns.Add("ExpectedDate")
    '        dtTab.Columns.Add("Amount")
    '        dtTab.Columns.Add("ExiceDuty")
    '        dtTab.Columns.Add("Frieght")
    '        dtTab.Columns.Add("Total")
    '        dtTab.Columns.Add("Vat")
    '        dtTab.Columns.Add("CST")

    '        dtTab.Columns.Add("TotalAmount")
    '        dtTab.Columns.Add("Discount")
    '        dtTab.Columns.Add("DiscountAmount")
    '        dtTab.Columns.Add("GSTID")
    '        dtTab.Columns.Add("GSTRate")
    '        dtTab.Columns.Add("GSTAmount")

    '        dtTab.Columns.Add("TAXAmount")
    '        dtTab.Columns.Add("Remarks")
    '        dtTab.Columns.Add("RowTotal")
    '        dtTab.Columns.Add("Invoice_No")
    '        dtTab.Columns.Add("HistoryID")
    '        sSql = "Select * From Purchase_Invoice_Accepted Where PIA_OrderID <> 0 And PIA_GINID In(Select PGM_ID from Purchase_GIN_Master where "
    '        sSql = sSql & "PGM_DocumentRefNo ='" & iGINID & "' and PGM_OrderID =" & OrderNo & " and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & ") and PIA_CompID=" & iCompID & " and PIA_YearID =" & iYearID & ""
    '        dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
    '        For i = 0 To dt.Rows.Count - 1
    '            If (dt.Rows(i)("PIA_AcceptedQnt") <> 0) Then
    '                dr = dtTab.NewRow
    '                dr("Invoice_No") = objDb.SQLGetDescription(sNameSpace, "Select PGM_GIN_Number From Purchase_GIN_Master Where PGM_ID ='" & dt.Rows(i)("PIA_GINID") & "' and PGM_CompID=" & iCompID & "")
    '                dr("ItemID") = objDb.SQLGetDescription(sNameSpace, "Select InvH_INV_ID From Inventory_master_history Where InvH_ID =" & dt.Rows(i)("PIA_HistoryID") & " and InvH_CompID =" & iCompID & "")
    '                dr("CommodityID") = objDb.SQLGetDescription(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dr("ItemID") & " and Inv_CompID =" & iCompID & "")
    '                dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_master Where INV_ID =" & dr("ItemID") & " and Inv_CompID =" & iCompID & "")
    '                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PIA_UnitID") & " and Mas_CompID =" & iCompID & "")
    '                dr("Pieces") = dt.Rows(i)("PIA_AcceptedQnt")
    '                dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(Date.Today, "D")
    '                dr("Amount") = dt.Rows(i)("PIA_MRP")
    '                dr("ExiceDuty") = 0 'objDb.SQLGetDescription(sNameSpace, "Select InvH_Excise From Inventory_master_history Where InvH_ID =" & dt.Rows(i)("PIA_HistoryID") & " and InvH_CompID =" & iCompID & "")
    '                If IsDBNull(objDb.SQLGetDescription(sNameSpace, "Select POD_Excise From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & "")) Then
    '                    dr("ExiceDuty") = 0
    '                Else
    '                    dr("ExiceDuty") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_VAT From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))

    '                    If (dr("ExiceDuty") = "") Then
    '                        dr("ExiceDuty") = 0
    '                    Else
    '                        dr("ExiceDuty") = String.Format("{0:0.00}", dr("ExiceDuty"))
    '                    End If
    '                End If

    '                dr("Frieght") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Frieght From Purchase_Order_Details Where POD_MasterID In(Select PGM_OrderID From Purchase_GIN_Master Where PGM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
    '                'dr("Frieght") = 0
    '                dr("Total") = 0
    '                dr("HistoryID") = dt.Rows(i)("PIA_HistoryID")
    '                dr("Vat") = 0
    '                If IsDBNull(objDb.SQLGetDescription(sNameSpace, "Select POD_VAT From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & "")) Then
    '                    dr("Vat") = 0
    '                Else
    '                    dr("Vat") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_VAT From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))

    '                    If (dr("Vat") = "") Then
    '                        dr("Vat") = 0
    '                    Else
    '                        dr("Vat") = String.Format("{0:0.00}", dr("Vat"))
    '                    End If

    '                End If
    '                If IsDBNull(objDb.SQLGetDescription(sNameSpace, "Select POD_CST From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and  POD_CompID =" & iCompID & "")) Then
    '                    dr("CST") = 0
    '                Else
    '                    dr("CST") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_CST From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
    '                    If (dr("CST") = "") Then
    '                        dr("CST") = 0
    '                    Else
    '                        dr("CST") = String.Format("{0:0.00}", dr("CST"))
    '                    End If
    '                End If

    '                dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")).ToString())

    '                dr("Discount") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Discount From Purchase_Order_Details Where POD_MasterID In(Select PGM_OrderID From Purchase_GIN_Master Where PGM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
    '                dr("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt") + Convert.ToDecimal(dr("Frieght")))) * Convert.ToDecimal(dr("Discount"))) / 100).ToString())

    '                dr("GSTID") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GST_ID From Purchase_Order_Details Where POD_MasterID In(Select PGM_OrderID From Purchase_GIN_Master Where PGM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
    '                dr("GSTRate") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GSTRate From Purchase_Order_Details Where POD_MasterID In(Select PGM_OrderID From Purchase_GIN_Master Where PGM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
    '                dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Frieght"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("GSTRate"))) / 100).ToString())

    '                dr("Remarks") = "O"
    '                vat = String.Format("{0:0.00}", Convert.ToDecimal(((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) * Convert.ToDecimal(dr("Vat"))) / 100).ToString())
    '                dr("TAXAmount") = String.Format("{0:0.00}", vat)
    '                cst = String.Format("{0:0.00}", Convert.ToDecimal(((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) * Convert.ToDecimal(dr("cst"))) / 100).ToString())
    '                dr("TAXAmount") = String.Format("{0:0.00}", (vat + cst))
    '                totAmount = String.Format("{0:0.00}", Convert.ToDecimal(((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt") + Convert.ToDecimal(dr("Frieght"))) - Convert.ToDecimal(dr("DiscountAmount"))) + dr("GSTAmount")).ToString())
    '                dr("RowTotal") = String.Format("{0:0.00}", totAmount)
    '                dtTab.Rows.Add(dr)
    '            End If
    '        Next
    '        Return dtTab
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function UpdateTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer)
        Dim dt, dt1 As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim vat As Decimal
        Dim total, TotalWithCst, TotalWithVat, cst As Decimal
        Dim sSql As String = ""
        Try
            sSql = "Select * From Transaction_Invoice_Details Where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & iGINID & "') and TID_CompID=" & iCompID & " and TID_NewFlag='A' and TID_ExcessFlag='A'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1

                If IsDBNull(dt.Rows(i)("TID_POVAT")) Then
                    vat = 0
                Else
                    vat = dt.Rows(i)("TID_POVAT")
                End If
                If IsDBNull(dt.Rows(i)("TID_CST")) Then
                    cst = 0
                Else
                    cst = dt.Rows(i)("TID_CST")
                End If

                vat = Convert.ToDecimal(((dt.Rows(i)("TID_PredeterminedPrice") * dt.Rows(i)("TID_Quantity")) * Convert.ToDecimal(vat)) / 100)
                ' dr("RowTotal") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("TID_PredeterminedPrice") * dt.Rows(i)("TID_Quantity") + vat).ToString())
                Dim RegRefID As String = objDb.SQLGetDescription(sNameSpace, "select GIN_DocRefNo from Goods_InwardNote_Master where GIN_ID=" & dt.Rows(i)("TID_GINRefID") & "")
                If (RegRefID = "") Then
                    RegRefID = 0
                End If
                Dim rateInReg As String = objDb.SQLGetDescription(sNameSpace, "select PRD_MRP from purchase_register_details where PRD_MasterID in(select PR_ID from purchase_register where PR_DocRefNo='" & RegRefID & "') and PRD_ItemID=" & dt.Rows(i)("TID_ItemID") & "")
                If (rateInReg = "") Then
                    rateInReg = 0
                Else
                    If (Convert.ToDecimal(rateInReg) > Convert.ToDecimal(dt.Rows(i)("TID_PredeterminedPrice"))) Then
                        'UpdateInvoiceDetails(sNameSpace, RegRefID, dt.Rows(i)("TID_ItemID"))
                        sSql = "select * from purchase_register_details where PRD_MasterID in(select PR_ID from purchase_register where PR_DocRefNo='" & RegRefID & "') and PRD_ItemID=" & dt.Rows(i)("TID_ItemID") & ""
                        dt1 = objDb.SQLExecuteDataTable(sNameSpace, sSql)
                        total = dt1.Rows(0)("PRD_MRP") * dt.Rows(i)("TID_Quantity")
                        If IsDBNull(dt.Rows(i)("TID_CST")) Then
                            TotalWithCst = total
                        Else
                            TotalWithCst = total + dt.Rows(i)("TID_CST")
                        End If
                        vat = Convert.ToDecimal(((dt1.Rows(0)("PRD_MRP") * dt.Rows(i)("TID_Quantity")) * Convert.ToDecimal(vat)) / 100)
                        TotalWithVat = TotalWithCst + vat
                        sSql = "update Transaction_Invoice_Details set TID_PurchaseRate=" & total & ",TID_PredeterminedPrice=" & dt1.Rows(0)("PRD_MRP") & ",TID_Total=" & TotalWithCst & ",TID_ItemRowTotal=" & TotalWithVat & ",TID_DiffFlag='df'  where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "')  And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & ""
                        objDb.SQLExecuteNonQuery(sNameSpace, sSql)

                        'DBHelper.ExecuteNoNQuery(sNameSpace, "update Transaction_Invoice_Details set TID_PurchaseRate=" & total & ",TID_PredeterminedPrice=" & dt.Rows(0)("PRD_MRP") & ",TID_Total=" & TotalWithCst & ",TID_ItemRowTotal=" & TotalWithVat & ",TID_DiffFlag='W'  where TID_GINRefID in(select GIND_ID from Goods_InwardNote_Master_details where GIND_MasterID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "') and GIND_ItemID=" & dt.Rows(i)("TID_ItemID") & ") And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & "")
                    Else
                        sSql = "update Transaction_Invoice_Details set TID_DiffFlag='A'  where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "')  And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & ""
                        objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                End If

            Next

        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function UpdateInvoiceDetails(ByVal sNameSpace As String, ByVal DocRefID As String, ByVal ItemID As Integer)
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim dtTMD As New DataTable
        Dim total, TotalWithCst, TotalWithVat As Decimal
        Try
            sSql = "Select * from purchase_register_details where PRD_MasterID In(Select PR_ID from purchase_register where PR_DocRefNo='" & DocRefID & "') and PRD_ItemID=" & ItemID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            total = dt.Rows(0)("PRD_MRP")
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetDifference(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim dtTMD As New DataTable
        Dim bCheck As Boolean
        Try
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("Boxes")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("Remarks")

            sSql = "Select * From Transaction_Master_Details Where TMD_MasterID=" & iTransactionID & " And TMD_CompID=" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For j = 0 To dt.Rows.Count - 1
                bCheck = objDb.SQLCheckForRecord(sNameSpace, "Select GIND_ID From Goods_InwardNote_Master_Details Where GIND_ItemID=" & dt.Rows(j)("TMD_ItemID") & " And GIND_GIN_OurRefNo In (Select GIN_ID From Goods_InwardNote_Master Where GIN_OurRefNo=" & iTransactionID & " ) ")
                If bCheck = True Then
                Else
                    dr = dtTab.NewRow
                    dr("ItemID") = dt.Rows(j)("TMD_ItemID")
                    dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(j)("TMD_ItemID") & " ")
                    dr("Quantity") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_PerPieces From Inventory_Master Where INV_ID=" & dt.Rows(j)("TMD_ItemID") & " ")
                    dr("MRP") = 10
                    If dt.Rows(j)("TMD_Per") = "Boxes" Then
                        dr("Boxes") = dt.Rows(j)("TMD_Quantity")
                        dr("Pieces") = ""
                    End If
                    If dt.Rows(j)("TMD_Per") = "Pieces" Then
                        dr("Boxes") = ""
                        dr("Pieces") = dt.Rows(j)("TMD_Quantity")
                    End If
                    dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(j)("TMD_ItemRequiredDate"), "D")
                    dr("Amount") = dt.Rows(j)("TMD_PurchaseRate")
                    dr("Remarks") = dt.Rows(j)("TMD_Remarks")
                    dtTab.Rows.Add(dr)
                End If
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function PurchaseInvoices(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select TM_ID, TM_BillNO from Transaction_Master where TM_PoRefNo=" & iGINID & " And (TM_TransactionType='PI' Or TM_TransactionType='PR') And TM_CompID=" & iCompID & " Order By TM_BillNO "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseVerification(ByVal sNameSpace As String, ByVal CompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal sIpAddress As String, ByVal OrderNo As Integer, ByVal DocRef As String, ByVal yearId As Integer, ByVal BillNo As String, ByVal PaymntMode As Integer, ByVal iInvoiceID As Integer) As Integer
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Dim bCheck As Boolean
        Dim ginID As Integer
        Try
            bCheck = objDb.SQLCheckForRecord(sNameSpace, "Select * From Purchase_Verification Where PV_InvoiceID=" & iInvoiceID & " And PV_OrderNo=" & OrderNo & " And PV_DocRefNo='" & DocRef & "' And PV_CompID=" & CompID & " and PV_YearID =" & iYearID & "")
            If bCheck = False Then
                sSql = ""
                ginID = objDb.SQLGetDescription(sNameSpace, "select PGM_ID from Purchase_GIN_Master where PGM_OrderID=" & OrderNo & " and  PGM_DocumentRefNo='" & DocRef & "' and PGM_YearID =" & iYearID & " and PGM_CompID =" & CompID & "")
                iMaxID = objDb.SQLExecuteScalar(sNameSpace, "select isnull(max(PV_ID)+1,1) from Purchase_Verification")
                sSql = "Insert Into Purchase_Verification(PV_ID,PV_OrderNo,PV_GinNo ,PV_CompID ,PV_Status,PV_BillNo,PV_DocRefNo,PV_YearID,PV_Operation,PV_IPAddress,PV_PaymentMode,PV_InvoiceID,PV_CreatedBy,PV_CreatedOn,PV_AppBy,PV_AppOn)"
                sSql = sSql & "Values(" & iMaxID & "," & OrderNo & "," & DocRef & "," & CompID & ",'A','" & BillNo & "','" & DocRef & "'," & iYearID & ",'C','" & sIpAddress & "'," & PaymntMode & "," & iInvoiceID & "," & iUserID & ",GetDate()," & iUserID & ",GetDate())"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
            Return iMaxID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPISum(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPIID As Integer) As String
        Dim sSql As String
        Dim Total As String
        Try
            sSql = "Select sum(convert(decimal(10,2), TMD_ItemRowTotal)) From Transaction_Master_Details Where TMD_MasterID=" & iPIID & " And TMD_CompID=" & iCompID & "  "
            Total = objDb.SQLGetDescription(sNameSpace, sSql)
            Return Total
        Catch ex As Exception
        End Try
    End Function

    Public Function GetSupplier(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal OrderID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String
        Dim Total As String
        Try
            'sSql = "select CSM_Name from CustomerSupplierMaster where CSM_ID in(select POM_Supplier from Purchase_Order_Master where POM_ID= " & OrderID & " And POM_YearID =" & iYearID & " And POM_CompID =" & iCompID & ") And CSM_CompID =" & iCompID & ""
            sSql = "select POM_Supplier from Purchase_Order_Master where POM_ID= " & OrderID & " And POM_YearID =" & iYearID & " And POM_CompID =" & iCompID & " "
            Total = objDb.SQLGetDescription(sNameSpace, sSql)
            Return Total
        Catch ex As Exception
        End Try
    End Function

    Public Function GetGINSum(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPIID As Integer) As String
        Dim sSql As String
        Dim Total As String
        Try
            sSql = "Select sum(convert(decimal(10,2), GIND_Rate)) From Goods_InwardNote_master_Details Where GIND_GIN_OurRefNo in (Select TM_PORefNo From Transaction_Master Where TM_ID=" & iPIID & ")"
            Total = objDb.SQLGetDescription(sNameSpace, sSql)
            Return Total
        Catch ex As Exception
        End Try
    End Function
    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iTMID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Transaction_Master Set TM_Status='A',TM_ApprovedBy=" & iUserID & ",TM_ApprovedOn=GetDate() Where TM_ID=" & iTMID & " And TM_CompID=" & iCompID & " "
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateDBNNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPIID As Integer) As String
        Dim sSql As String = ""
        Dim Count As String = ""
        Try
            sSql = "select IsNull(count(*),0)+1 from Transaction_Master where TM_TransactionType='DBN' And TM_PoRefNo=" & iPIID & " "
            Count = objDb.SQLExecuteScalar(sNameSpace, sSql)
            Return "-00" & Count
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPIID As Integer, ByVal iGINID As Integer) As String
        Dim sSql As String = ""
        Dim sStatus As String = ""
        Try
            sSql = "select TM_Status From Transaction_Master Where TM_ID=" & iPIID & " And TM_PORefNo=" & iGINID & " And TM_CompID=" & iCompID & " "
            sStatus = objDb.SQLGetDescription(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransactionDetailsPR(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iGINID As String, ByVal iCommodity As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim vat As Decimal
        Dim sSql As String = ""
        Try

            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("RateAmount")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("FinalTotal")

            Dim dCharges, dItemsTotal As Double
            dCharges = objDb.SQLExecuteScalarInt(sNameSpace, "Select Sum(C_ChargeAmount) From Charges_Master Where C_POrderID=" & OrderNo & " And C_CompID=" & iCompID & " ")
            dItemsTotal = objDb.SQLGetDescription(sNameSpace, "Select Sum(Convert(money, POD_TotalAmount)) As POD_TotalAmount From Purchase_Order_Details Where POD_MasterID=" & OrderNo & " And POD_CompID=" & iCompID & " ")

            sSql = "  Select * From Purchase_Invoice_Rejected Where PIR_GINID In(Select PGM_ID from Purchase_GIN_Master where "
            sSql = sSql & "PGM_DocumentRefNo ='" & iGINID & "' and PGM_YearID =" & iYearId & " and  PGM_OrderID =" & OrderNo & " and PGM_CompID=" & iCompID & ") and PIR_CompID=" & iCompID & " and PIR_YearID =" & iYearId & " and PIR_Status='W'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                If (dt.Rows(i)("PIR_RejectedQty") <> 0) Then
                    dr = dtTab.NewRow

                    dr("CommodityID") = dt.Rows(i)("PIR_Commodity")
                    dr("ItemID") = dt.Rows(i)("PIR_DescriptionID")
                    dr("HistoryID") = dt.Rows(i)("PIR_HistoryID")

                    dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("PIR_DescriptionID") & " and Inv_CompID =" & iCompID & "")
                    dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PIR_UnitID") & " and Mas_CompID =" & iCompID & "")
                    dr("Quantity") = dt.Rows(i)("PIR_RejectedQty")
                    dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(Date.Today, "D")
                    dr("Rate") = dt.Rows(i)("PIR_MRP")
                    dr("RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PIR_MRP") * dt.Rows(i)("PIR_RejectedQty")).ToString())
                    dr("Discount") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Discount From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIR_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIR_HistoryID") & " and POD_CompID =" & iCompID & ""))
                    dr("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal((((dt.Rows(i)("PIR_MRP") * dt.Rows(i)("PIR_RejectedQty"))) * Convert.ToDecimal(dr("Discount"))) / 100).ToString())
                    dr("Charges") = String.Format("{0:0.00}", Convert.ToDecimal((Convert.ToDecimal(dr("RateAmount")) * dCharges) / dItemsTotal))
                    'Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Frieght From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIR_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIR_HistoryID") & " and POD_CompID =" & iCompID & ""))
                    dr("Amount") = ((dr("RateAmount") - dr("DiscountAmount")) + dr("Charges"))
                    dr("GSTID") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GST_ID From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIR_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIR_HistoryID") & " and POD_CompID =" & iCompID & ""))
                    dr("GSTRate") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GSTRate From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIR_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIR_HistoryID") & " and POD_CompID =" & iCompID & ""))
                    dr("GSTAmount") = Convert.ToDecimal(((((dt.Rows(i)("PIR_MRP") * dt.Rows(i)("PIR_RejectedQty")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("GSTRate"))) / 100)

                    dr("Remarks") = ""
                    dr("FinalTotal") = String.Format("{0:0.00}", Convert.ToDecimal(((dt.Rows(i)("PIR_MRP") * dt.Rows(i)("PIR_RejectedQty") + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) + dr("GSTAmount")).ToString())
                    dtTab.Rows.Add(dr)
                End If
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTransactionDetailsExcess(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim vat As String
        Try

            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("RateAmount")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("FinalTotal")

            Dim dCharges, dItemsTotal As Double
            dCharges = objDb.SQLExecuteScalarInt(sNameSpace, "Select Sum(C_ChargeAmount) From Charges_Master Where C_POrderID=" & OrderNo & " And C_CompID=" & iCompID & " ")
            dItemsTotal = objDb.SQLGetDescription(sNameSpace, "Select Sum(Convert(money, POD_TotalAmount)) As POD_TotalAmount From Purchase_Order_Details Where POD_MasterID=" & OrderNo & " And POD_CompID=" & iCompID & " ")

            sSql = "" : sSql = "Select * From Purchase_Invoice_Accepted Where PIA_GINID in(select PGM_ID from Purchase_GIN_Master where "
            sSql = sSql & "PGM_DocumentRefNo ='" & iGINID & "' and PGM_YearID =" & iYearID & " and  PGM_OrderID =" & OrderNo & " and PGM_CompID =" & iCompID & ") and "
            sSql = sSql & "PIA_CompID =" & iCompID & "and PIA_YearID = " & iYearID & " And PIA_Excess <> 0 And PIA_Status='W'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("CommodityID") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("PIA_DescriptionID") & " and Inv_CompID =" & iCompID & " ")
                dr("ItemID") = dt.Rows(i)("PIA_DescriptionID")
                dr("HistoryID") = dt.Rows(i)("PIA_HistoryID")
                dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("PIA_DescriptionID") & " and Inv_CompID =" & iCompID & " ")
                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PIA_UnitID") & " and Mas_CompID =" & iCompID & "")
                dr("Quantity") = dt.Rows(i)("PIA_Excess")
                dr("ExpectedDate") = Date.Today
                dr("Rate") = dt.Rows(i)("PIA_MRP")
                dr("RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_Excess")).ToString())

                dr("Discount") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Discount From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                dr("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_Excess"))) * Convert.ToDecimal(dr("Discount"))) / 100).ToString())
                dr("Charges") = String.Format("{0:0.00}", Convert.ToDecimal((Convert.ToDecimal(dr("RateAmount")) * dCharges) / dItemsTotal))
                'Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Frieght From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                dr("Amount") = ((dr("RateAmount") - dr("DiscountAmount")) + dr("Charges"))

                dr("GSTID") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GST_ID From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                dr("GSTRate") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GSTRate From Purchase_Order_Details Where POD_MasterID =" & dt.Rows(i)("PIA_OrderID") & " and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                dr("GSTAmount") = Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_Excess")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("GSTRate"))) / 100)
                dr("Remarks") = ""
                dr("FinalTotal") = String.Format("{0:0.00}", Convert.ToDecimal(((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_Excess") + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) + dr("GSTAmount")).ToString())
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTransactionDetailsNewItemDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim vat As String
        Try
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Ounit")
            dtTab.Columns.Add("RUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("RejectedCozExcess")
            dtTab.Columns.Add("Flag")
            dtTab.Columns.Add("Statuss")
            sSql = "Select * From Purchase_Invoice_Excess Where PIE_GINID in(select PGM_GIN_Number from Purchase_GIN_Master where "
            sSql = sSql & "PGM_DocumentRefNo ='" & iGINID & "' and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & " and  PGM_OrderID=" & OrderNo & ") and PIE_CompID=" & iCompID & " and PIE_YearID =" & iYearID & " and PIE_Quantity<>0"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("ID") = dt.Rows(i)("PIE_ID")
                dr("Invoice_No") = dt.Rows(i)("PIE_GINID")
                dr("CommodityID") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("PIE_Description") & " and Inv_CompID =" & iCompID & "")
                dr("ItemID") = dt.Rows(i)("PIE_Description")
                dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("PIE_Description") & " and Inv_CompID =" & iCompID & "")
                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PIE_UnitID") & " and Mas_CompID =" & iCompID & "")
                dr("Pieces") = dt.Rows(i)("PIE_Quantity")
                dr("ExpectedDate") = Date.Today
                dr("Amount") = dt.Rows(i)("PIE_Rate")
                dr("ExiceDuty") = objDb.SQLGetDescription(sNameSpace, "select InvH_Excise from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")
                dr("Frieght") = "0"
                dr("Total") = "0"
                If IsDBNull(objDb.SQLGetDescription(sNameSpace, "select InvH_Vat from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")) Then
                    dr("Vat") = 0
                Else
                    dr("Vat") = objDb.SQLGetDescription(sNameSpace, "select InvH_Vat from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")
                    If (dr("Vat") = "") Then
                        dr("Vat") = 0
                    End If
                End If

                'and invH_YearID =" & iYearID & "
                'dr("CST") = DBHelper.GetDescription(sNameSpace, "select InvH_Excise from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")
                If IsDBNull(objDb.SQLGetDescription(sNameSpace, "select InvH_Excise from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")) Then
                    dr("CST") = 0
                Else
                    'and InvH_YearID =" & iYearId & "
                    dr("CST") = objDb.SQLGetDescription(sNameSpace, "select InvH_Excise from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")
                    If (dr("CST") = "") Then
                        dr("CST") = 0
                    End If
                End If
                dr("Flag") = dt.Rows(i)("PIE_Delflag")
                dr("Statuss") = "New Item"
                dr("RejectedCozExcess") = dt.Rows(i)("PIE_Quantity")
                vat = Convert.ToDecimal(((dt.Rows(i)("PIE_Rate") * dt.Rows(i)("PIE_Quantity")) * Convert.ToDecimal(dr("Vat"))) / 100)
                dr("TAXAmount") = vat
                dr("RowTotal") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PIE_Rate") * dt.Rows(i)("PIE_Quantity") + vat).ToString())
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTransactionDetailsDiffrence(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Try
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Ounit")
            dtTab.Columns.Add("RUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("RejectedCozExcess")
            dtTab.Columns.Add("Flag")
            dtTab.Columns.Add("Statuss")
            sSql = "Select * From Transaction_Invoice_Details Where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & iGINID & "') and TID_CompID=" & iCompID & " and  TID_DiffFlag='df' and TID_Quantity<>0 "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("ID") = dt.Rows(i)("TID_ID")
                dr("Invoice_No") = objDb.SQLGetDescription(sNameSpace, "Select GIN_GoodInwardNo From Goods_InwardNote_Master Where GIN_ID =" & dt.Rows(i)("TID_GINRefID") & "")
                dr("CommodityID") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("TID_ItemID") & " ")
                dr("ItemID") = dt.Rows(i)("TID_ItemID")
                dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("TID_ItemID") & "")
                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("TID_Per") & "")
                dr("Pieces") = dt.Rows(i)("TID_Quantity")
                dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("TID_ItemRequiredDate"), "D")
                dr("Amount") = dt.Rows(i)("TID_PurchaseRate")
                dr("ExiceDuty") = dt.Rows(i)("TID_ExciseDuty")
                dr("Frieght") = dt.Rows(i)("TID_Frieght")
                dr("Total") = dt.Rows(i)("TID_Total")
                If IsDBNull(dt.Rows(i)("TID_POVAT") <> "") = False Then
                    dr("Vat") = dt.Rows(i)("TID_POVAT")
                Else
                    dr("Vat") = ""
                End If
                If IsDBNull(dt.Rows(i)("TID_CST") <> "") = False Then
                    dr("CST") = dt.Rows(i)("TID_CST")
                Else
                    dr("CST") = ""
                End If
                dr("TAXAmount") = dt.Rows(i)("TID_TaxAmount")
                dr("Remarks") = dt.Rows(i)("TID_Remarks")
                dr("RowTotal") = dt.Rows(i)("TID_ItemRowTotal")
                dr("Flag") = dt.Rows(i)("TID_flag")
                dr("Statuss") = dt.Rows(i)("TID_Status")
                dr("RejectedCozExcess") = objDb.SQLExecuteScalar(sNameSpace, "Select GIND_RejectedBczExcess from Goods_InwardNote_Master_Details where GIND_ID='" & dt.Rows(i)("TID_ID") & "' And GIND_MasterID='" & dt.Rows(i)("TID_GINRefID") & "'")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dt As DataTable) As String
        Dim str, sSql, sSql1, sql3, s As String
        Dim slno, i, spId As Integer
        Dim subtotal, VatAmount, vatSubtotal, Exceise, total, cst, amount As Decimal
        Dim ds, ds1 As DataSet
        Dim dview As DataView
        str = ""
        sSql = ""
        sSql1 = ""
        slno = 0
        sql3 = ""
        i = 0
        subtotal = 0
        VatAmount = 0
        vatSubtotal = 0
        Exceise = 0
        dview = dt.DefaultView
        Try
            ds = New DataSet
            ds1 = New DataSet
            ds.Tables.Add(dt)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    slno = slno + 1
                    VatAmount = 0
                    VatAmount = ((Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString())) / 100)
                    VatAmount = VatAmount * Convert.ToDecimal(ds.Tables(0).Rows(i)("Vat").ToString())
                    str = str & "<tr>"
                    str = str & "<td width='10%' align='center'>" & slno & "</td>"
                    sql3 = "select Inv_Description from Inventory_Master where Inv_Id=" & ds.Tables(0).Rows(i)("ItemID").ToString() & ""
                    s = objDb.SQLGetDescription(sNameSpace, sql3)
                    str = str & "<td width='30%'>" & s & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("Vat").ToString())) & "</td>"
                    str = str & "<td width='10%'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("ExiceDuty").ToString())) & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("discount").ToString())) & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", (Convert.ToDecimal(ds.Tables(0).Rows(i)("CST").ToString()))) & "</td>"
                    str = str & "<td width='10%' align='center'>" & ds.Tables(0).Rows(i)("Pieces") & "</td>"
                    If (ds.Tables(0).Rows(i)("DiscountAmount").ToString() <> "") Then
                        amount = (Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) - Convert.ToDecimal(ds.Tables(0).Rows(i)("DiscountAmount").ToString()))
                    Else
                        amount = Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString())
                    End If
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Convert.ToDecimal(amount)) & "</td>"
                    str = str & " < td width='10%'>" & ds.Tables(0).Rows(i)("StdUnit") & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("TaxAmount").ToString())) & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", (Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces")))) & "</td>"
                    str = str & "</tr>"
                    Exceise = Exceise + (((Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces"))) * Convert.ToDecimal(ds.Tables(0).Rows(i)("ExiceDuty").ToString())) / 100)
                    subtotal = subtotal + (Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces")))
                    vatSubtotal = vatSubtotal + Convert.ToDecimal(ds.Tables(0).Rows(i)("TaxAmount").ToString())
                    cst = cst + (((Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces"))) * Convert.ToDecimal(ds.Tables(0).Rows(i)("CST").ToString())) / 100)
                Next
            End If
            If slno > 0 Then
                str = str & "<tr>"

                str = str & "<td  colspan='10'><b>Sub Total</b></td>"
                str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", subtotal) & "</td>"
                str = str & "</tr>"
                total = subtotal + vatSubtotal
            End If
            str = str & "</table>"
            str = str & "<br>"
            str = str & "<table border='2' order-collapse='collapse' cellspacing='0' cellpadding='0' width='1000' align='center'>"
            If slno > 0 Then
                str = str & "<tr>"
                str = str & "<td  colspan='6'><b>Total</b></td>"
                str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", vatSubtotal) & "</td>"
                str = str & "<td width='10%' align='right'></td>"
                str = str & "</tr>"
                str = str & "<tr>"
                str = str & "<td  colspan='6'><b>Total Excise Duty</b></td>"
                str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Exceise) & "</td>"
                str = str & "<td width='10%' align='right'></td>"
                str = str & "</tr>"

                str = str & "<tr>"
                str = str & "<td  colspan='6'><b>Total CST</b></td>"
                str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", cst) & "</td>"
                str = str & "<td width='10%' align='right'></td>"
                str = str & "</tr>"

                total = subtotal + vatSubtotal + Exceise + cst
                str = str & "<tr>"
                str = str & "<td  colspan='7'><b>Grand Total</b></td>"
                str = str & "<td  align='center'><b>" & String.Format("{0:0.00}", total) & "</b></td>"
                str = str & "</tr>"
            End If
            str = str & "</table>"
            str = str & "<br>"
            str = str & "<table border='2' order-collapse='collapse' cellspacing='0' cellpadding='0' width='1000' align='center'>"
            str = str & "<tr>"
            str = str & "<td  colspan='8'><b>Amount In Words</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:" & NumberToWord(String.Format("{0:0.00}", total)) & "</td>"
            str = str & "</tr>"
            Return str
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseDetailsVeri(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dt As DataTable) As String
        Dim str, sSql, sSql1, sql3, s As String
        Dim slno, i, spId As Integer
        Dim subtotal, VatAmount, vatSubtotal, Exceise, total, cst As Decimal
        Dim ds, ds1 As DataSet
        Dim dview As DataView
        str = ""
        sSql = ""
        sSql1 = ""
        slno = 0
        sql3 = ""
        i = 0
        subtotal = 0
        VatAmount = 0
        vatSubtotal = 0
        Exceise = 0
        dview = dt.DefaultView
        Try
            ds = New DataSet
            ds1 = New DataSet
            ds.Tables.Add(dt)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    slno = slno + 1
                    VatAmount = 0
                    VatAmount = ((Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString())) / 100)
                    VatAmount = VatAmount * Convert.ToDecimal(ds.Tables(0).Rows(i)("Vat").ToString())
                    str = str & "<tr>"
                    str = str & "<td width='10%' align='center'>" & slno & "</td>"
                    sql3 = "select Inv_Description from Inventory_Master where Inv_Id=" & ds.Tables(0).Rows(i)("ItemID").ToString() & ""
                    s = objDb.SQLGetDescription(sNameSpace, sql3)
                    str = str & "<td width='30%'>" & s & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("Vat").ToString())) & "</td>"
                    str = str & "<td width='10%'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("ExiceDuty").ToString())) & "</td>"
                    'str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("discount").ToString())) & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", (Convert.ToDecimal(ds.Tables(0).Rows(i)("CST").ToString()))) & "</td>"
                    str = str & "<td width='10%' align='center'>" & ds.Tables(0).Rows(i)("Pieces") & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString())) & "</td>"
                    str = str & "<td width='10%'>" & ds.Tables(0).Rows(i)("StdUnit") & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("TaxAmount").ToString())) & "</td>"
                    str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", (Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces")))) & "</td>"
                    str = str & "</tr>"
                    Exceise = Exceise + (((Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces"))) * Convert.ToDecimal(ds.Tables(0).Rows(i)("ExiceDuty").ToString())) / 100)
                    subtotal = subtotal + (Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces")))
                    vatSubtotal = vatSubtotal + Convert.ToDecimal(ds.Tables(0).Rows(i)("TaxAmount").ToString())
                    cst = cst + (((Convert.ToDecimal(ds.Tables(0).Rows(i)("Amount").ToString()) * Convert.ToDecimal(ds.Tables(0).Rows(i)("Pieces"))) * Convert.ToDecimal(ds.Tables(0).Rows(i)("CST").ToString())) / 100)
                Next
            End If
            If slno > 0 Then
                str = str & "<tr>"

                str = str & "<td  colspan='9'><b>Sub Total</b></td>"
                str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", subtotal) & "</td>"
                str = str & "</tr>"
                total = subtotal + vatSubtotal
            End If
            str = str & "</table>"
            str = str & "<br>"
            str = str & "<table border='2' order-collapse='collapse' cellspacing='0' cellpadding='0' width='1000' align='center'>"
            If slno > 0 Then
                str = str & "<tr>"
                str = str & "<td  colspan='5'><b>Total</b></td>"
                str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", vatSubtotal) & "</td>"
                str = str & "<td width='10%' align='right'></td>"
                str = str & "</tr>"
                str = str & "<tr>"
                str = str & "<td  colspan='5'><b>Total Excise Duty</b></td>"
                str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Exceise) & "</td>"
                str = str & "<td width='10%' align='right'></td>"
                str = str & "</tr>"

                str = str & "<tr>"
                str = str & "<td  colspan='5'><b>Total CST</b></td>"
                str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", cst) & "</td>"
                str = str & "<td width='10%' align='right'></td>"
                str = str & "</tr>"

                'str = str & "<tr>"
                'str = str & "<td  colspan='6'><b>Total Discount</b></td>"
                'str = str & "<td width='10%' align='right'>" & String.Format("{0:0.00}", Exceise) & "</td>"
                'str = str & "<td width='10%' align='right'></td>"
                'str = str & "</tr>"
                total = subtotal + vatSubtotal + Exceise + cst
                str = str & "<tr>"
                str = str & "<td  colspan='6'><b>Grand Total</b></td>"
                str = str & "<td  align='center'><b>" & String.Format("{0:0.00}", total) & "</b></td>"
                str = str & "</tr>"
            End If
            str = str & "</table>"
            str = str & "<br>"
            str = str & "<table border='2' order-collapse='collapse' cellspacing='0' cellpadding='0' width='1000' align='center'>"
            str = str & "<tr>"
            str = str & "<td  colspan='7'><b>Amount In Words</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:" & NumberToWord(String.Format("{0:0.00}", total)) & "</td>"
            str = str & "</tr>"
            Return str
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function NumberToWord(ByVal num1 As String) As String
        Dim words, strones(100), strtens(100), aftrdecimalWord As String
        Dim crore, lakhs, thousands, hundreds, tens, ssingle, aftrDecimal1, aftrDecimal, num As Double
        Try
            If (num1.Contains(".")) Then
                Dim str1 As String() = Strings.Split(num1, ".")
                num = Convert.ToDouble(str1(0))
            Else
                num = Convert.ToDouble(num1)
            End If
            aftrDecimal1 = num

            If num = 0 Then
                Return ""
            End If


            If num < 0 Then
                Return "Not supported"
            End If

            words = ""
            strones = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
            strtens = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
            crore = 0
            lakhs = 0
            thousands = 0
            hundreds = 0
            tens = 0
            ssingle = 0

            If (num > 10000000) Then

                If ((Convert.ToString(num / 10000000)).Contains(".")) Then
                    crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
                    num = num - (hundreds * 10000000)
                Else
                    crore = num / 100
                    num = num - (hundreds * 10000000)
                End If
            End If

            If (num > 100000) Then

                If ((Convert.ToString(num / 100000)).Contains(".")) Then
                    lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                    num = num - (hundreds * 100000)
                Else
                    lakhs = num / 100000
                    num = num - (hundreds * 100000)
                End If
            End If


            'lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))        
            If (num > 1000) Then

                If ((Convert.ToString(num / 1000)).Contains(".")) Then
                    thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                    num = num - (thousands * 1000)
                Else
                    thousands = num / 1000
                    num = num - (thousands * 1000)
                End If
            End If
            'thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))        

            If (num > 100) Then

                If ((Convert.ToString(num / 100)).Contains(".")) Then
                    hundreds = Convert.ToInt32((num / 100).ToString().Substring(0, (num / 100).ToString().IndexOf(".")))
                    num = num - (hundreds * 100)
                Else
                    hundreds = num / 100
                    num = num - (hundreds * 100)
                End If
            End If
            If num > 19 Then
                If ((Convert.ToString(num / 10)).Contains(".")) Then
                    tens = Convert.ToInt32((num / 10).ToString().Substring(0, (num / 10).ToString().IndexOf(".")))
                    num = num - (tens * 10)
                Else
                    tens = num / 10
                    num = num - (tens * 10)
                End If

            End If

            ssingle = num

            If crore > 0 Then
                If crore > 19 Then
                    words += NumberToWord(crore) + "Crore "
                Else
                    words += strones(crore - 1) + " Crore "
                End If
            End If
            If lakhs > 0 Then

                If lakhs > 19 Then
                    words += NumberToWord(lakhs) + "Lakh "
                Else
                    words += strones(lakhs - 1) + " Lakh "
                End If
            End If

            If thousands > 0 Then

                If thousands > 19 Then
                    words += NumberToWord(thousands) + "Thousand "
                Else
                    words += strones(thousands - 1) + " Thousand "
                End If
            End If


            If hundreds > 0 Then
                words += strones(hundreds - 1) + " Hundred "
            End If


            If tens > 0 Then
                words += strtens(tens - 2) + " "
            End If

            If ssingle > 0 Then
                words += strones(ssingle - 1) + " "
            End If

            If (num1.Contains(".")) Then
                Dim str As String() = Strings.Split(num1, ".")
                aftrDecimal = Convert.ToDouble(str(1))
                aftrdecimalWord = AfterDecimalfunction(aftrDecimal)
                If aftrdecimalWord = "zero" Then
                    words += ""
                Else
                    aftrdecimalWord += " Paise"
                    words += " and " + aftrdecimalWord

                End If
            End If
            Return words
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AfterDecimalfunction(ByVal num As Decimal) As String
        Dim words, strones(100), strtens(100) As String
        Dim crore, lakhs, thousands, hundreds, tens, ssingle As Decimal
        Try
            If num = 0 Then
                Return "Zero"
            End If

            If num < 0 Then
                Return "Not supported"
            End If
            words = ""
            strones = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
            strtens = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
            crore = 0
            lakhs = 0
            thousands = 0
            hundreds = 0
            tens = 0
            ssingle = 0

            If ((Convert.ToString(num / 10000000)).Contains(".")) Then
                crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
                num = num - (hundreds * 10000000)
            Else
                crore = num / 10000000
                num = num - (hundreds * 10000000)
            End If

            'crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
            'num = num - (crore * 10000000)


            If ((Convert.ToString(num / 100000)).Contains(".")) Then
                lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                num = num - (hundreds * 100000)
            Else
                lakhs = num / 100000
                num = num - (hundreds * 100000)
            End If

            'lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))

            'num = num - (lakhs * 100000)

            If ((Convert.ToString(num / 1000)).Contains(".")) Then
                thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                num = num - (thousands * 1000)
            Else
                thousands = num / 1000
                num = num - (thousands * 1000)
            End If

            thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
            num = num - (thousands * 1000)


            If ((Convert.ToString(num / 100)).Contains(".")) Then
                hundreds = Convert.ToInt32((num / 100).ToString().Substring(0, (num / 100).ToString().IndexOf(".")))
                num = num - (hundreds * 100)
            Else
                hundreds = num / 100
                num = num - (hundreds * 100)
            End If
            If num > 19 Then
                If ((Convert.ToString(num / 10)).Contains(".")) Then
                    tens = Convert.ToInt32((num / 10).ToString().Substring(0, (num / 10).ToString().IndexOf(".")))
                    num = num - (tens * 10)
                Else
                    tens = num / 10
                    num = num - (tens * 10)
                End If

            End If

            ssingle = num

            If crore > 0 Then
                If crore > 19 Then
                    words += NumberToWord(crore) + "Crore "
                Else
                    words += strones(crore - 1) + " Crore "
                End If
            End If
            If lakhs > 0 Then

                If lakhs > 19 Then
                    words += NumberToWord(lakhs) + "Lakh "
                Else
                    words += strones(lakhs - 1) + " Lakh "
                End If
            End If

            If thousands > 0 Then

                If thousands > 19 Then
                    words += NumberToWord(thousands) + "Thousand "
                Else
                    words += strones(thousands - 1) + " Thousand "
                End If
            End If

            If hundreds > 0 Then
                words += strones(hundreds - 1) + " Hundred "
            End If

            If tens > 0 Then
                words += strtens(tens - 2) + " "
            End If

            If ssingle > 0 Then
                words += strones(ssingle - 1) + " "
            End If
            Return words
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCompany_BuyerInfo(ByVal sNameSpce As String, ByVal CompId As Integer, ByVal orderNo As Integer) As String
        Dim pan, vatSup, cstSup, tin, cst, str, inwords, sSql As String
        Dim i As Integer
        Dim dt, dt1 As DataTable
        Dim BuyId As Integer
        pan = " "
        tin = ""
        cst = ""
        str = ""
        inwords = ""
        sSql = ""
        Try
            BuyId = objDb.SQLGetDescription(sNameSpce, "select TM_Supplier from Transaction_Master where TM_ID=" & orderNo & "")
            sSql = "Select * from Company_Accounting_Template where Cmp_ID='" & sNameSpce & "'"
            dt = objDb.SQLExecuteDataTable(sNameSpce, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If dt.Rows(i)("Cmp_Desc").ToString() = "tin" Or dt.Rows(i)("Cmp_Desc").ToString() = "TIN" Then
                        tin = dt.Rows(i)("Cmp_Value").ToString()
                    End If
                    If dt.Rows(i)("Cmp_Desc").ToString() = "cst" Or dt.Rows(i)("Cmp_Desc").ToString() = "CST" Then
                        cst = dt.Rows(i)("Cmp_Value").ToString()
                    End If
                    If dt.Rows(i)("Cmp_Desc").ToString() = "pan" Or dt.Rows(i)("Cmp_Desc").ToString() = "PAN" Then
                        pan = dt.Rows(i)("Cmp_Value").ToString()
                    End If
                Next
            End If
            sSql = "Select * from Buyer_Accounting_Template where Buyer_ID=" & BuyId & ""
            dt1 = objDb.SQLExecuteDataTable(sNameSpce, sSql)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    'If ds1.Tables(0).Rows(i)("CST_Description").ToString() = "tin" Then
                    '    tinSup = ds1.Tables(0).Rows(i)("CST_Value").ToString()
                    'End If
                    If dt1.Rows(i)("Buyer_Desc").ToString() = "cst" Or dt1.Rows(i)("Buyer_Desc").ToString() = "CST" Then
                        cstSup = dt1.Rows(i)("Buyer_Value").ToString()
                    End If
                    If dt1.Rows(i)("Buyer_Desc").ToString() = "vat" Or dt1.Rows(i)("Buyer_Desc").ToString() = "VAT" Then
                        vatSup = dt1.Rows(i)("Buyer_Value").ToString()
                    End If
                Next
            End If
            str = "</tr>"
            str = str & "<tr>"
            str = str & "<td  colspan='8'>Companys Vat Tin&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:" & tin & "</td>"
            str = str & "</tr>"
            str = str & "<tr>"
            str = str & "<td  colspan='8'>Companys CST No&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:" & cst & "</td>"
            str = str & "</tr>"
            str = str & "<tr>"
            str = str & "<td  colspan='8'>Suplier Vat&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:" & vatSup & "</td>"
            str = str & "</tr>"
            str = str & "<tr>"
            str = str & "<td  colspan='8'>Suplier CST No&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:" & cstSup & "</td>"
            str = str & "</tr>"
            str = str & "<tr>"
            str = str & "<td  colspan='8'>Companys Pan NO&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:" & pan & "</td>"
            str = str & "</tr>"
            str = str & "</table>"
            str = str & "<br>"
            Return str
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveStockLedger(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sIPAddress As String, ByVal iUserID As Integer, ByVal dt As DataTable, ByVal OrderNo As String, ByVal InwardNo As String, ByVal iBranch As Integer) As Integer
        Dim sSql As String = ""
        Dim iMaxid As Integer, iQTY As Integer
        Try
            For i = 0 To dt.Rows.Count - 1
                If (objDb.SQLCheckForRecord(sNameSpace, "Select * from Stock_Ledger where SL_historyId=" & dt.Rows(i)("HistoryID") & " And SL_ItemID = " & dt.Rows(i)("ItemID") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " And SL_OrderID=" & OrderNo & " and SL_GINID=" & InwardNo & " And SL_Branch=" & iBranch & " ")) Then
                    objDb.SQLExecuteNonQuery(sNameSpace, "Update Stock_Ledger Set SL_Operation='U',SL_IPAddress='" & sIPAddress & "',SL_ClosingBalanceQty=SL_ClosingBalanceQty+" & dt.Rows(i)("Quantity") & ", SL_PurchaseQty = SL_PurchaseQty + " & dt.Rows(i)("Quantity") & " where  SL_historyId=" & dt.Rows(i)("HistoryID") & " And SL_ItemID = " & dt.Rows(i)("ItemID") & " and SL_YearID =" & iYearID & " and SL_CompID =" & iCompID & " and SL_GINID=" & InwardNo & " And SL_Branch=" & iBranch & " ")
                Else
                    iMaxid = objDb.SQLExecuteScalar(sNameSpace, "Select isnull(max(SL_ID) + 1, 1) from Stock_Ledger")
                    sSql = "Insert Into Stock_Ledger (SL_ID,SL_Commodity,SL_Date,SL_ItemID,sl_PurchaseQty,SL_ClosingBalanceQty,SL_CompID,SL_YearID,SL_CrBy,SL_CrOn,SL_historyId,SL_Operation,SL_IPAddress,SL_Vat,SL_Exciese,SL_Cst,SL_OrderID,SL_SaleQnty,PurchaseRate,SL_OpeningBalanceQty,SL_GINID,SL_Branch)"
                    sSql = sSql & "Values(" & iMaxid & "," & dt.Rows(i)("CommodityID") & ", GetDate()," & dt.Rows(i)("ItemID") & "," & dt.Rows(i)("Quantity") & "," & dt.Rows(i)("Quantity") & "," & iCompID & "," & iYearID & "," & iUserID & ",GetDate()," & dt.Rows(i)("HistoryID") & ",'C','" & sIPAddress & "','0','0','0','" & OrderNo & "',0,'" & dt.Rows(i)("Rate") & "',0," & InwardNo & "," & iBranch & ")"
                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getTotalValueFromGin(ByVal sNameSpace As String, ByVal dt As DataTable) As DataTable
        Dim GiVal As Decimal = 0
        Dim PiVal As Decimal = 0
        Dim docRef As String
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Dim sSql As String = ""
        Dim sTransactionType As String = ""
        Try
            dtTab.Columns.Add("GiVal")
            dtTab.Columns.Add("PiVal")
            dr = dtTab.NewRow
            For i = 0 To dt.Rows.Count - 1
                sSql = "select GINd_Rate from Goods_InwardNote_Master_Details where GIND_MasterID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo=001)"
                GiVal += objDb.SQLGetDescription(sNameSpace, "select SUM(cast(isnull(nullif(GINd_Rate, ''),0) as decimal(12,2))) from Goods_InwardNote_Master_Details where  GIND_MasterID in(select GIN_ID from Goods_InwardNote_Master where GIN_ID=" & dt.Rows(i)("GIN_ID") & ")")
                docRef = objDb.SQLGetDescription(sNameSpace, "select GIN_DocRefNo from Goods_InwardNote_Master where GIN_ID=" & dt.Rows(i)("GIN_ID") & "")
                If (objDb.SQLCheckForRecord(sNameSpace, "select * from purchase_register where PR_DocRefNo='" & docRef & "'")) Then
                    PiVal += objDb.SQLGetDescription(sNameSpace, "select SUM(cast(isnull(nullif(PRD_Rate, ''),0) as decimal(12,2))) from purchase_register_details where  PRD_MasterID in(select PR_ID from purchase_register where PR_DocRefNo='" & docRef & "')")
                End If
            Next
            dr("GiVal") = GiVal
            dr("PiVal") = PiVal
            dtTab.Rows.Add(dr)
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iorder As Integer, ByVal iDocRefNo As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "  select PV_ID from Purchase_verification where PV_OrderNo=" & iorder & " and PV_DocRefNo='" & iDocRefNo & "' and PV_CompID =" & iCompID & " and PV_YearID = " & iYearID & ""
            getBillNo = objDb.SQLExecuteScalarInt(sNameSpace, sSql)
            Return getBillNo
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
    'Public Function GetAccountDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String, ByVal sColumn As String, ByVal sTypeOfBill As String) As Integer
    '    Dim sSql As String = ""
    '    Dim iCOA As Integer = 0
    '    Try
    '        sSql = "" : sSql = "Select " & sColumn & " from Acc_Application_Settings where Acc_Types = '" & sType & "' and "
    '        sSql = sSql & " Acc_LedgerType ='" & sLedgerType & "' and Acc_CompID=" & iCompID & " And Acc_TypeOfBill='" & sTypeOfBill & "' "
    '        iCOA = objDb.SQLExecuteScalar(sNameSpace, sSql)
    '        Return iCOA
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
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
    Public Function SavePurchaseJournalMaster(ByVal sNameSpace As String, ByVal objVerification As clsPurchaseVrification) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_TransactionNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_Party)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_Location)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_BillType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objFasGnrl.SafeSQL(objVerification.sAcc_JE_BillNo))
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dAcc_JE_BillDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dAcc_JE_BillAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dAcc_JE_AdvanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceNaration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_AdvanceNaration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BalanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dAcc_JE_BalanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_NetAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dAcc_JE_NetAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PaymentNarration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_PaymentNarration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeNo", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_ChequeNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dAcc_JE_ChequeDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IFSCCode", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_IFSCCode)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BankName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_BankName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BranchName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_BranchName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_CreatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_JE_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillCreatedDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dAcc_JE_BillCreatedDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_UpdatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_UpdatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_InvoiceID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iAcc_JE_InvoiceID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PendingAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dAcc_JE_PendingAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Type", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sAcc_PJE_Type)
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
    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal objVerification As clsPurchaseVrification) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_ZoneID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_RegionID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_AreaID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objFasGnrl.SafeSQL(objVerification.iATD_BranchID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objVerification.dATD_OpenDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objVerification.dATD_OpenCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objVerification.dATD_ClosingDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objVerification.dATD_ClosingCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objVerification.iATD_SeqReferenceNum
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
    Public Function GetTransactionDetailsPI(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer, ByVal OrderNo As Integer, ByVal iInvoiceID As Integer) As DataTable
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

            sSql = "Select * From PI_Accepted_Details Where PID_MasterID in (Select PIM_ID From Purchase_Invoice_Master Where PIM_ID=" & iInvoiceID & " And PIM_OrderID=" & OrderNo & " And PIM_PRegesterID=" & iGINID & " and PIM_YearID =" & iYearID & " and PIM_CompID =" & iCompID & " ) "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)

            For i = 0 To dt.Rows.Count - 1
                If (dt.Rows(i)("PID_Quantity") <> 0) Then
                    dr = dtTab.NewRow
                    'dr("Invoice_No") = objDb.SQLGetDescription(sNameSpace, "Select PGM_GIN_Number From Purchase_GIN_Master Where PGM_ID ='" & dt.Rows(i)("PIA_GINID") & "' and PGM_CompID=" & iCompID & "")

                    dr("CommodityID") = dt.Rows(i)("PID_CommodityID")
                    dr("ItemID") = dt.Rows(i)("PID_DescID")
                    dr("HistoryID") = dt.Rows(i)("PID_HistoryID")

                    dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_master Where INV_ID =" & dt.Rows(i)("PID_DescID") & " and INV_Parent=" & dt.Rows(i)("PID_CommodityID") & " and Inv_CompID =" & iCompID & "")
                    dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PID_UnitID") & " and Mas_CompID =" & iCompID & "")
                    dr("Quantity") = dt.Rows(i)("PID_Quantity")
                    dr("ExpectedDate") = objFasGnrl.FormatDtForRDBMS(Date.Today, "D")
                    dr("Rate") = dt.Rows(i)("PID_Rate")
                    dr("Charges") = dt.Rows(i)("PID_ChargePerItem")
                    dr("RateAmount") = dt.Rows(i)("PID_RateAmount")

                    If dt.Rows(i)("PID_Discount") > 0 Then
                        dr("Discount") = dt.Rows(i)("PID_Discount")
                        'objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PID_Discount") & " and Mas_CompID =" & iCompID & "")
                        dr("DiscountAmount") = dt.Rows(i)("PID_DiscountAmount")
                    Else
                        dr("Discount") = 0
                        dr("DiscountAmount") = 0
                    End If
                    dr("Amount") = dt.Rows(i)("PID_Amount")

                    dr("GSTID") = dt.Rows(i)("PID_GSTID")
                    dr("GSTRate") = dt.Rows(i)("PID_GSTRate")
                    dr("GSTAmount") = dt.Rows(i)("PID_GSTAmount")
                    dr("Remarks") = dt.Rows(i)("PID_Remarks")
                    dr("FinalTotal") = dt.Rows(i)("PID_FinalTotal")
                    dtTab.Rows.Add(dr)
                End If
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvoicesNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select PIM_ID,PIM_No From Purchase_Invoice_Master Where PIM_OrderID in (Select POM_ID From Purchase_Order_Master Where POM_BranchID=" & iBranch & ") And PIM_Status='A' And PIM_YearID=" & iYearID & " And PIM_CompID=" & iCompID & " And PIM_ID Not In(Select PV_InvoiceID From Purchase_Verification Where PV_CompID=" & iCompID & " And PV_YearID=" & iYearID & ") order by PIM_ID Desc"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExistingDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPIMID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Purchase_Invoice_Master Where PIM_ID=" & iPIMID & " And PIM_CompID=" & iCompID & ""
            GetExistingDate = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetExistingDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " and CSM_Delflag='A' order by CSM_Name"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function AllGSTRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As Boolean
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim sRates As String = ""
    '    Try
    '        sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & iCompID & " "
    '        dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                sRates = sRates & "," & dt.Rows(i)("GST_GSTRate")
    '            Next
    '        End If
    '        If sRates.StartsWith(",") Then
    '            sRates = sRates.Remove(0, 1)
    '        End If
    '        If sRates.EndsWith(",") Then
    '            sRates = sRates.Remove(Len(sRates) - 1, 1)
    '        End If

    '        sSql = "" : sSql = "Select * From Acc_Application_Settings Where Acc_Types in ('" & dt.Rows(0)("GST_GSTRate") & "') And Acc_CompID=" & iCompID & " "
    '        AllGSTRates = objDb.SQLCheckForRecord(sNameSpace, sSql)

    '        Return AllGSTRates
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
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
    Public Function GetBranchID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPVID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select POM_BranchID From Purchase_Order_Master Where POM_ID in(SElect PV_OrderNO From Purchase_Verification Where PV_ID=" & iPVID & " And PV_YearID=" & iYearID & " And PV_CompID=" & iCompID & ") "
            GetBranchID = objDb.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetBranchID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZone(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPVID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Purchase_Order_Master Where POM_ID in (Select PV_OrderNo From Purchase_Verification Where PV_ID=" & iPVID & " And PV_CompID=" & iCompID & " And PV_YearID=" & iYearID & " )"
            GetZone = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetZone
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select POD_Currency From Purchase_Order_Details Where POD_MasterID=" & iOrderID & " And POD_CompID=" & iCompID & "  "
            Return objDb.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFERates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer, ByVal dTotal As Double) As String
        Dim sSql As String = "", sToday As String = ""
        Dim dValue As Double = 0, dFEtotal As Double = 0
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_TTBuy from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            dValue = objDb.SQLExecuteScalar(sNameSpace, sSql)
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
            Return objDb.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdatePurchaseVerification(ByVal sNameSpace As String, ByVal CompID As Integer, ByVal dTotal As Double, ByVal iCurrency As Integer, ByVal dRate As Double, ByVal sTime As String, ByVal iPVID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_Verification Set PV_FETotalAmt='" & dTotal & "',PV_Currency=" & iCurrency & ", PV_CurrencyAmt='" & dRate & "',PV_CurrencyTime='" & sTime & "' Where PV_ID=" & iPVID & " And PV_CompID=" & CompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetFECRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As String
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_TTBuy from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFECTime(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As String
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_Time from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
