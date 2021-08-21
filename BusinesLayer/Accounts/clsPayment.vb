Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsPayment
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private Acc_PM_ID As Integer
    Private Acc_PM_TransactionNo As String
    Private Acc_PM_Party As Integer
    Private Acc_PM_Location As Integer
    Private Acc_PM_TransactionType As Integer
    Private Acc_PM_BillType As Integer
    Private Acc_PM_BillNo As String
    Private Acc_PM_BillDate As Date
    Private Acc_PM_BillAmount As Decimal
    Private Acc_PM_AdvanceAmount As Decimal
    Private Acc_PM_AdvanceNaration As String
    Private Acc_PM_TDSType As Integer
    Private ACC_PM_TDSDeduct As Decimal
    Private Acc_PM_TDSAmount As Decimal
    Private Acc_PM_TDSNarration As String
    Private Acc_PM_NetAmount As Decimal
    Private Acc_PM_PaymentNarration As String
    Private Acc_PM_ChequeNo As String
    Private Acc_PM_ChequeDate As Date
    Private Acc_PM_IFSCCode As String
    Private Acc_PM_BankName As String
    Private Acc_PM_BranchName As String
    Private Acc_PM_CreatedBy As Integer
    Private Acc_PM_CreatedOn As Date
    Private Acc_PM_ApprovedBy As Integer
    Private Acc_PM_ApprovedOn As Date
    Private Acc_PM_DeletedBy As Integer
    Private Acc_PM_DeletedOn As Date
    Private Acc_PM_RecalledBy As Integer
    Private Acc_PM_RecalledOn As Date
    Private Acc_PM_YearID As Integer
    Private Acc_PM_CompID As Integer
    Private Acc_PM_Status As String
    Private Acc_PM_Operation As String
    Private Acc_PM_IPAddress As String
    Private Acc_PM_BillCreatedDate As Date
    Private Acc_PM_BalanceAmount As Decimal
    Private Acc_Bill_Narration As String
    Private Acc_PM_InvoiceDate As Date
    Private Acc_PM_PaidAmount As Double
    Private Acc_PM_AttachID As Integer
    Private ACC_PM_ZoneID As Integer
    Private ACC_PM_RegionID As Integer
    Private ACC_PM_AreaID As Integer
    Private ACC_PM_BranchID As Integer
    Private Acc_PM_OrderNO As Integer
    Private Acc_PM_OrderDate As Date
    Private Acc_PM_PaymentType As Integer
    Private Acc_PM_FETotalAmt As String
    Private Acc_PM_Currency As Integer
    Private Acc_PM_DiffAmount As Double
    Private Acc_PM_CurrencyAmt As Double
    Private Acc_PM_CurrencyTime As String
    Private Acc_PM_TrTypeDetails As Integer

    Private ATD_ID As Integer
    Private ATD_TransactionDate As Date
    Private ATD_TrType As Integer
    Private ATD_BillId As Integer
    Private ATD_PaymentType As Integer
    Private ATD_DbOrCr As Integer
    Private ATD_Head As Integer
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
    Private ATD_Operation As String
    Private ATD_CompID As Integer
    Private ATD_IPAddress As String

    Private ATD_ZoneID As Integer
    Private ATD_RegionID As Integer
    Private ATD_AreaID As Integer
    Private ATD_BranchID As Integer
    Private Acc_PM_BatchNo As Integer
    Private Acc_PM_BaseName As Integer

    Private ATD_OpenDebit As Decimal
    Private ATD_OpenCredit As Decimal
    Private ATD_ClosingDebit As Decimal
    Private ATD_ClosingCredit As Decimal
    Private ATD_SeqReferenceNum As Integer
    Public Property iAcc_PM_BatchNo() As Integer
        Get
            Return (Acc_PM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_BatchNo = Value
        End Set
    End Property
    Public Property iAcc_PM_BaseName() As Integer
        Get
            Return (Acc_PM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_BaseName = Value
        End Set
    End Property
    Public Property iAcc_PM_PaymentType() As Integer
        Get
            Return (Acc_PM_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_PaymentType = Value
        End Set
    End Property
    Public Property iAcc_PM_OrderNO() As Integer
        Get
            Return (Acc_PM_OrderNO)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_OrderNO = Value
        End Set
    End Property
    Public Property dAcc_PM_OrderDate() As DateTime
        Get
            Return (Acc_PM_OrderDate)
        End Get
        Set(ByVal Value As DateTime)
            Acc_PM_OrderDate = Value
        End Set
    End Property
    'Public Property iAcc_PM_POID() As Integer
    '    Get
    '        Return (Acc_PM_POID)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        Acc_PM_POID = Value
    '    End Set
    'End Property
    'Public Property dAcc_PM_PODate() As DateTime
    '    Get
    '        Return (Acc_PM_PODate)
    '    End Get
    '    Set(ByVal Value As DateTime)
    '        Acc_PM_PODate = Value
    '    End Set
    'End Property

    Public Property iACC_PM_ZoneID() As Integer
        Get
            Return (ACC_PM_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            ACC_PM_ZoneID = Value
        End Set
    End Property
    Public Property iACC_PM_RegionID() As Integer
        Get
            Return (ACC_PM_RegionID)
        End Get
        Set(ByVal Value As Integer)
            ACC_PM_RegionID = Value
        End Set
    End Property
    Public Property iACC_PM_AreaID() As Integer
        Get
            Return (ACC_PM_AreaID)
        End Get
        Set(ByVal Value As Integer)
            ACC_PM_AreaID = Value
        End Set
    End Property
    Public Property iACC_PM_BranchID() As Integer
        Get
            Return (ACC_PM_BranchID)
        End Get
        Set(ByVal Value As Integer)
            ACC_PM_BranchID = Value
        End Set
    End Property
    Public Property iAcc_PM_AttachID() As Integer
        Get
            Return (Acc_PM_AttachID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_AttachID = Value
        End Set
    End Property
    Public Property dAcc_PM_PaidAmount() As Double
        Get
            Return (Acc_PM_PaidAmount)
        End Get
        Set(ByVal Value As Double)
            Acc_PM_PaidAmount = Value
        End Set
    End Property
    Public Property dAcc_PM_InvoiceDate() As Date
        Get
            Return (Acc_PM_InvoiceDate)
        End Get
        Set(ByVal Value As Date)
            Acc_PM_InvoiceDate = Value
        End Set
    End Property
    Public Property sAcc_Bill_Narration() As String
        Get
            Return (Acc_Bill_Narration)
        End Get
        Set(ByVal Value As String)
            Acc_Bill_Narration = Value
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
    Public Property iATD_CompID() As Integer
        Get
            Return (ATD_CompID)
        End Get
        Set(ByVal Value As Integer)
            ATD_CompID = Value
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

    Public Property dAcc_PM_BalanceAmount() As Decimal
        Get
            Return (Acc_PM_BalanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PM_BalanceAmount = Value
        End Set
    End Property

    Public Property dAcc_PM_BillCreatedDate() As Date
        Get
            Return (Acc_PM_BillCreatedDate)
        End Get
        Set(ByVal Value As Date)
            Acc_PM_BillCreatedDate = Value
        End Set
    End Property

    Public Property sAcc_PM_IPAddress() As String
        Get
            Return (Acc_PM_IPAddress)
        End Get
        Set(ByVal Value As String)
            Acc_PM_IPAddress = Value
        End Set
    End Property

    Public Property sAcc_PM_Operation() As String
        Get
            Return (Acc_PM_Operation)
        End Get
        Set(ByVal Value As String)
            Acc_PM_Operation = Value
        End Set
    End Property
    Public Property sAcc_PM_Status() As String
        Get
            Return (Acc_PM_Status)
        End Get
        Set(ByVal Value As String)
            Acc_PM_Status = Value
        End Set
    End Property

    Public Property iAcc_PM_CompID() As Integer
        Get
            Return (Acc_PM_CompID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_CompID = Value
        End Set
    End Property

    Public Property iAcc_PM_YearID() As Integer
        Get
            Return (Acc_PM_YearID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_YearID = Value
        End Set
    End Property
    Public Property dAcc_PM_RecalledOn() As Date
        Get
            Return (Acc_PM_RecalledOn)
        End Get
        Set(ByVal Value As Date)
            Acc_PM_RecalledOn = Value
        End Set
    End Property

    Public Property iAcc_PM_RecalledBy() As Integer
        Get
            Return (Acc_PM_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_RecalledBy = Value
        End Set
    End Property
    Public Property dAcc_PM_DeletedOn() As Date
        Get
            Return (Acc_PM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_PM_DeletedOn = Value
        End Set
    End Property

    Public Property iAcc_PM_DeletedBy() As Integer
        Get
            Return (Acc_PM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_DeletedBy = Value
        End Set
    End Property

    Public Property dAcc_PM_ApprovedOn() As Date
        Get
            Return (Acc_PM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_PM_ApprovedOn = Value
        End Set
    End Property
    Public Property iAcc_PM_ApprovedBy() As Integer
        Get
            Return (Acc_PM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_ApprovedBy = Value
        End Set
    End Property

    Public Property dAcc_PM_CreatedOn() As Date
        Get
            Return (Acc_PM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_PM_CreatedOn = Value
        End Set
    End Property

    Public Property iAcc_PM_CreatedBy() As Integer
        Get
            Return (Acc_PM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_CreatedBy = Value
        End Set
    End Property
    Public Property sAcc_PM_BranchName() As String
        Get
            Return (Acc_PM_BranchName)
        End Get
        Set(ByVal Value As String)
            Acc_PM_BranchName = Value
        End Set
    End Property

    Public Property iAcc_PM_TrTypeDetails() As Integer
        Get
            Return (Acc_PM_TrTypeDetails)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_TrTypeDetails = Value
        End Set
    End Property
    Public Property sAcc_PM_BankName() As String
        Get
            Return (Acc_PM_BankName)
        End Get
        Set(ByVal Value As String)
            Acc_PM_BankName = Value
        End Set
    End Property

    Public Property sAcc_PM_IFSCCode() As String
        Get
            Return (Acc_PM_IFSCCode)
        End Get
        Set(ByVal Value As String)
            Acc_PM_IFSCCode = Value
        End Set
    End Property
    Public Property dAcc_PM_ChequeDate() As Date
        Get
            Return (Acc_PM_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            Acc_PM_ChequeDate = Value
        End Set
    End Property

    Public Property sAcc_PM_ChequeNo() As String
        Get
            Return (Acc_PM_ChequeNo)
        End Get
        Set(ByVal Value As String)
            Acc_PM_ChequeNo = Value
        End Set
    End Property
    Public Property sAcc_PM_PaymentNarration() As String
        Get
            Return (Acc_PM_PaymentNarration)
        End Get
        Set(ByVal Value As String)
            Acc_PM_PaymentNarration = Value
        End Set
    End Property
    Public Property dAcc_PM_NetAmount() As Decimal
        Get
            Return (Acc_PM_NetAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PM_NetAmount = Value
        End Set
    End Property

    Public Property sAcc_PM_TDSNarration() As String
        Get
            Return (Acc_PM_TDSNarration)
        End Get
        Set(ByVal Value As String)
            Acc_PM_TDSNarration = Value
        End Set
    End Property

    Public Property dAcc_PM_TDSAmount() As Decimal
        Get
            Return (Acc_PM_TDSAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PM_TDSAmount = Value
        End Set
    End Property
    Public Property dACC_PM_TDSDeduct() As Decimal
        Get
            Return (ACC_PM_TDSDeduct)
        End Get
        Set(ByVal Value As Decimal)
            ACC_PM_TDSDeduct = Value
        End Set
    End Property

    Public Property iAcc_PM_TDSType() As Integer
        Get
            Return (Acc_PM_TDSType)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_TDSType = Value
        End Set
    End Property
    Public Property sAcc_PM_AdvanceNaration() As String
        Get
            Return (Acc_PM_AdvanceNaration)
        End Get
        Set(ByVal Value As String)
            Acc_PM_AdvanceNaration = Value
        End Set
    End Property
    Public Property dAcc_PM_AdvanceAmount() As Decimal
        Get
            Return (Acc_PM_AdvanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PM_AdvanceAmount = Value
        End Set
    End Property
    Public Property dAcc_PM_BillAmount() As Decimal
        Get
            Return (Acc_PM_BillAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PM_BillAmount = Value
        End Set
    End Property

    Public Property dAcc_PM_BillDate() As Date
        Get
            Return (Acc_PM_BillDate)
        End Get
        Set(ByVal Value As Date)
            Acc_PM_BillDate = Value
        End Set
    End Property


    Public Property sAcc_PM_BillNo() As String
        Get
            Return (Acc_PM_BillNo)
        End Get
        Set(ByVal Value As String)
            Acc_PM_BillNo = Value
        End Set
    End Property

    Public Property iAcc_PM_BillType() As Integer
        Get
            Return (Acc_PM_BillType)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_BillType = Value
        End Set
    End Property
    Public Property iAcc_PM_TransactionType() As Integer
        Get
            Return (Acc_PM_TransactionType)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_TransactionType = Value
        End Set
    End Property
    Public Property iAcc_PM_Location() As Integer
        Get
            Return (Acc_PM_Location)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_Location = Value
        End Set
    End Property

    Public Property iAcc_PM_Party() As Integer
        Get
            Return (Acc_PM_Party)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_Party = Value
        End Set
    End Property


    Public Property sAcc_PM_TransactionNo() As String
        Get
            Return (Acc_PM_TransactionNo)
        End Get
        Set(ByVal Value As String)
            Acc_PM_TransactionNo = Value
        End Set
    End Property

    Public Property iAcc_PM_ID() As Integer
        Get
            Return (Acc_PM_ID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_ID = Value
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
    Public Property sAcc_PM_FETotalAmt() As String
        Get
            Return (Acc_PM_FETotalAmt)
        End Get
        Set(ByVal Value As String)
            Acc_PM_FETotalAmt = Value
        End Set
    End Property
    Public Property iAcc_PM_Currency() As Integer
        Get
            Return (Acc_PM_Currency)
        End Get
        Set(ByVal Value As Integer)
            Acc_PM_Currency = Value
        End Set
    End Property
    Public Property dAcc_PM_DiffAmount() As Double
        Get
            Return (Acc_PM_DiffAmount)
        End Get
        Set(ByVal Value As Double)
            Acc_PM_DiffAmount = Value
        End Set
    End Property
    Public Property dAcc_PM_CurrencyAmt() As Double
        Get
            Return (Acc_PM_CurrencyAmt)
        End Get
        Set(ByVal Value As Double)
            Acc_PM_CurrencyAmt = Value
        End Set
    End Property
    Public Property sAcc_PM_CurrencyTime() As String
        Get
            Return (Acc_PM_CurrencyTime)
        End Get
        Set(ByVal Value As String)
            Acc_PM_CurrencyTime = Value
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
    Public Function LoadPayment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iStatus As Integer, ByVal iYearID As Integer) As DataTable
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
            dc = New DataColumn("BillType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Acc_Payment_Master where Acc_PM_YearID=" & iYearID & " And Acc_PM_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Acc_PM_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_PM_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_PM_Status='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By Acc_PM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_PM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PM_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_PM_TransactionNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PM_BillNo").ToString()) = False Then
                        dr("BillNo") = ds.Tables(0).Rows(i)("Acc_PM_BillNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PM_InvoiceDate").ToString()) = False Then
                        dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_PM_InvoiceDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PM_BillType").ToString()) = False Then
                        dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_PM_BillType").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PM_Party").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("Acc_PM_Party") > 0 Then
                            dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_PM_Location").ToString(), ds.Tables(0).Rows(i)("Acc_PM_Party").ToString())
                        Else
                            dr("Party") = ""
                        End If
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_PM_Status") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_PM_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_PM_Status") = "D") Then
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
    'Public Sub UpdatePaymentMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Update Acc_Payment_Master Set Acc_PM_IPAddress='" & sIPAddress & "',"
    '        If sStatus = "W" Then
    '            sSql = sSql & " Acc_PM_Status='A',Acc_PM_ApprovedBy= " & iUserID & ",Acc_PM_ApprovedOn=GetDate()"
    '        ElseIf sStatus = "D" Then
    '            sSql = sSql & " Acc_PM_Status='D',Acc_PM_DeletedBy= " & iUserID & ",Acc_PM_DeletedOn=GetDate()"
    '        ElseIf sStatus = "A" Then
    '            sSql = sSql & " Acc_PM_Status='A' "
    '        End If
    '        sSql = sSql & " Where Acc_PM_ID = " & iMasId & ""
    '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=1 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                sSql = "" : sSql = "Update Acc_Transactions_Details Set ATD_IPAddress='" & sIPAddress & "' "
    '                If sStatus = "W" Then
    '                    sSql = sSql & " ,ATD_Status='A',ATD_ApprovedBy= " & iUserID & ",ATD_ApprovedOn=GetDate()"
    '                ElseIf sStatus = "D" Then
    '                    sSql = sSql & " ,ATD_Status='D',ATD_DeletedBy= " & iUserID & ",ATD_DeletedOn=GetDate()"
    '                ElseIf sStatus = "A" Then
    '                    sSql = sSql & " ,ATD_Status='A',ATD_ApprovedBy=" & iUserID & ",ATD_ApprovedOn=GetDate() "
    '                End If
    '                sSql = sSql & " Where ATD_ID = " & dt.Rows(i)("ATD_ID") & ""
    '                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Function CheckLedgerTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)
    '    Dim sSql As String = ""
    '    Dim bCheck As Boolean
    '    Dim iMaxID As Integer
    '    Dim iAccHead, iHeadID, iGLID As Integer
    '    Dim dt As New DataTable
    '    Dim iID As Integer

    '    Dim dOpenDebit As Double : Dim dOpenCredit As Double
    '    Dim dTransDebit As Double : Dim dTransCredit As Double
    '    Dim dCloseDebit As Double : Dim dCloseCredit As Double
    '    Dim dtDetails As New DataTable
    '    Dim iTrAccHead, iTrHead, iTrGLID As Integer
    '    Dim dPreviousTransDebit, dTotalTransDebit As Double : Dim dPreviousTransCredit, dTotalTransCredit As Double

    '    Dim dtValues As New DataTable
    '    Try

    '        sSql = "" : sSql = "Select * From Chart_OF_Accounts A 
    '                            Left Join Acc_Ledger_Masters B On B.ALM_GL = A.GL_ID
    '                            Where A.gl_Head in (2,3) And A.gl_CompID=" & iCompID & " And A.GL_ID  Not In (Select ALM_GL From Acc_Ledger_Masters Where ALM_CompID=" & iCompID & ")"
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                iAccHead = dt.Rows(i)("GL_AccHead")
    '                iHeadID = dt.Rows(i)("GL_Head")
    '                iGLID = dt.Rows(i)("GL_ID")

    '                iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ALM_ID)+1,1) from Acc_Ledger_Masters")
    '                sSql = "" : sSql = "Insert Into Acc_Ledger_Masters(ALM_ID,ALM_AccountType,ALM_Head,ALM_GL,ALM_OBDebit,ALM_OBCredit,ALM_TrDebit,ALM_TrCredit,ALM_CloseDebit,ALM_CloseCredit,ALM_Year,ALM_CompID,ALM_Createdby,ALM_CreatedOn,ALM_ZoneID,ALM_RegionID,ALM_AreaID,ALM_BranchID,ALM_Status) "
    '                sSql = sSql & " Values(" & iMaxID & "," & iAccHead & "," & iHeadID & "," & iGLID & ",0,0,0,0,0,0," & iYearID & "," & iCompID & "," & iUserID & ",GetDate(),0,0,0,0,'W' ) "
    '                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Next
    '        End If

    '        dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=1 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
    '        If dtDetails.Rows.Count > 0 Then
    '            For j = 0 To dtDetails.Rows.Count - 1

    '                iTrAccHead = dtDetails.Rows(j)("ATD_Head")
    '                If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
    '                    iTrGLID = dtDetails.Rows(j)("ATD_SubGL")
    '                Else
    '                    iTrGLID = dtDetails.Rows(j)("ATD_GL")
    '                End If
    '                iTrHead = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Gl_Head From Chart_Of_Accounts Where GL_ID=" & iTrGLID & " And Gl_AccHead=" & iTrAccHead & " And GL_CompID=" & iCompID & " ")

    '                If dtDetails.Rows(j)("ATD_SubGL") > 0 Then
    '                    sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit,A.ATD_TransactionDate,A.ATD_ZoneID,A.ATD_RegionID,A.ATD_AreaID,A.ATD_BranchID"
    '                    sSql = sSql & " From Acc_Transactions_Details A"
    '                    sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_SUBGL"
    '                    sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_SUBGL"
    '                    sSql = sSql & " Where A.ATD_Status='A' And A.ATD_TrType=1 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
    '                    dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '                Else
    '                    sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit,A.ATD_TransactionDate,A.ATD_ZoneID,A.ATD_RegionID,A.ATD_AreaID,A.ATD_BranchID"
    '                    sSql = sSql & " From Acc_Transactions_Details A"
    '                    sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
    '                    sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
    '                    sSql = sSql & " Where A.ATD_Status='A' And A.ATD_TrType=1 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
    '                    dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '                End If

    '                sSql = "" : sSql = "Select * From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
    '                bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)

    '                If bCheck = True Then
    '                    iID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select ALM_ID From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " ")

    '                    If dtValues.Rows.Count > 0 Then

    '                        If IsDBNull(dtValues.Rows(0)("Opn_DebitAmt")) = False Then
    '                            dOpenDebit = dtValues.Rows(0)("Opn_DebitAmt")
    '                        Else
    '                            dOpenDebit = 0
    '                        End If
    '                        If IsDBNull(dtValues.Rows(0)("Opn_CreditAmount")) = False Then
    '                            dOpenCredit = dtValues.Rows(0)("Opn_CreditAmount")
    '                        Else
    '                            dOpenCredit = 0
    '                        End If

    '                        dTransDebit = dtValues.Rows(0)("ATD_Debit")
    '                        dTransCredit = dtValues.Rows(0)("ATD_Credit")

    '                        dPreviousTransDebit = dtValues.Rows(0)("ALM_TrDebit")
    '                        dTotalTransDebit = dPreviousTransDebit + dTransDebit

    '                        dPreviousTransCredit = dtValues.Rows(0)("ALM_TrCredit")
    '                        dTotalTransCredit = dPreviousTransCredit + dTransCredit

    '                        dCloseDebit = dOpenDebit + dTotalTransDebit
    '                        dCloseCredit = dOpenCredit + dTotalTransCredit

    '                        sSql = "" : sSql = "Update Acc_Ledger_Masters Set ALM_OBDebit=" & dOpenDebit & ",ALM_OBCredit=" & dOpenCredit & ",ALM_TrDebit=" & dTotalTransDebit & ",ALM_TrCredit=" & dTotalTransCredit & ",ALM_CloseDebit=" & dCloseDebit & ",ALM_CloseCredit=" & dCloseCredit & ",ALM_TrDate='" & objGen.FormatDtForRDBMS(dtValues.Rows(0)("ATD_TransactionDate"), "CT") & "',ALM_ZoneID=" & dtValues.Rows(0)("ATD_ZoneID") & ",ALM_RegionID=" & dtValues.Rows(0)("ATD_RegionID") & ",ALM_AreaID=" & dtValues.Rows(0)("ATD_AreaID") & ",ALM_BranchID=" & dtValues.Rows(0)("ATD_BranchID") & " "
    '                        sSql = sSql & " Where ALM_ID =" & iID & " And ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
    '                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

    '                        dOpenDebit = 0 : dOpenCredit = 0 : dTransDebit = 0 : dTransCredit = 0 : dPreviousTransDebit = 0 : dTotalTransDebit = 0
    '                        dPreviousTransCredit = 0 : dTotalTransCredit = 0 : dCloseDebit = 0 : dCloseCredit = 0
    '                    End If
    '                End If

    '            Next
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Sub UpdatePaymentMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
        Dim sSql As String = ""
        Dim dt, dtDebitCredit As New DataTable
        Dim dOpnDebit, dOpnCredit, dClosingDebit, dClosingCredit As Double
        Dim iSequenceNum As Integer
        Try
            sSql = "" : sSql = "Update Acc_Payment_Master Set Acc_PM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_PM_Status='A',Acc_PM_ApprovedBy= " & iUserID & ",Acc_PM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_PM_Status='D',Acc_PM_DeletedBy= " & iUserID & ",Acc_PM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_PM_Status='A' "
            End If
            sSql = sSql & " Where Acc_PM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)



            dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=1 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
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
                                Where A.gl_Head in (2) And A.gl_CompID=" & iCompID & " And A.GL_ID  Not In (Select ALM_GL From Acc_Ledger_Masters Where ALM_Year=" & iYearID & " And ALM_CompID=" & iCompID & ")"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iAccHead = dt.Rows(i)("GL_AccHead")
                    iHeadID = dt.Rows(i)("GL_Head")
                    iGLID = dt.Rows(i)("GL_ID")

                    iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(ALM_ID)+1,1) from Acc_Ledger_Masters")
                    sSql = "" : sSql = "Insert Into Acc_Ledger_Masters(ALM_ID,ALM_AccountType,ALM_Head,ALM_GL,ALM_OBDebit,ALM_OBCredit,ALM_TrDebit,ALM_TrCredit,ALM_CloseDebit,ALM_CloseCredit,ALM_Year,ALM_CompID,ALM_Createdby,ALM_CreatedOn,ALM_ZoneID,ALM_RegionID,ALM_AreaID,ALM_BranchID,ALM_Status) "
                    sSql = sSql & " Values(" & iMaxID & "," & iAccHead & "," & iHeadID & "," & iGLID & ",0,0,0,0,0,0," & iYearID & "," & iCompID & "," & iUserID & ",GetDate(),0,0,0,0,'W' ) "
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=1 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
            If dtDetails.Rows.Count > 0 Then
                For j = 0 To dtDetails.Rows.Count - 1

                    iTrAccHead = dtDetails.Rows(j)("ATD_Head")
                    If dtDetails.Rows(j)("ATD_GL") > 0 Then
                        iTrGLID = dtDetails.Rows(j)("ATD_GL")
                    End If
                    iTrHead = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Gl_Head From Chart_Of_Accounts Where GL_ID=" & iTrGLID & " And Gl_AccHead=" & iTrAccHead & " And GL_CompID=" & iCompID & " ")

                    If dtDetails.Rows(j)("ATD_GL") > 0 Then
                        sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit,A.ATD_TransactionDate,A.ATD_ZoneID,A.ATD_RegionID,A.ATD_AreaID,A.ATD_BranchID"
                        sSql = sSql & " From Acc_Transactions_Details A"
                        sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
                        sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
                        sSql = sSql & " Where A.ATD_Status='A' And A.ATD_TrType=1 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
                        dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    End If

                    'Check for the last year closing balance'
                    Dim dtClosingBalance As New DataTable
                    sSql = "" : sSql = "Select * From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID - 1 & " "
                    dtClosingBalance = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    'Check for the last year closing balance'

                    sSql = "" : sSql = "Select * From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " "
                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)

                    If bCheck = True Then
                        iID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select ALM_ID From Acc_Ledger_Masters Where ALM_AccountType=" & iTrAccHead & " And ALM_Head=" & iTrHead & " And ALM_GL=" & iTrGLID & " And ALM_CompID=" & iCompID & " And ALM_Year=" & iYearID & " ")

                        If dtValues.Rows.Count > 0 Then

                            If dtClosingBalance.Rows.Count > 0 Then
                                If IsDBNull(dtValues.Rows(0)("ALM_CloseDebit")) = False Then
                                    dOpenDebit = dtValues.Rows(0)("ALM_CloseDebit")
                                Else
                                    dOpenDebit = 0
                                End If
                                If IsDBNull(dtValues.Rows(0)("ALM_CloseCredit")) = False Then
                                    dOpenCredit = dtValues.Rows(0)("ALM_CloseCredit")
                                Else
                                    dOpenCredit = 0
                                End If
                            Else
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

                            End If

                            dTransDebit = dtValues.Rows(0)("ATD_Debit")
                            dTransCredit = dtValues.Rows(0)("ATD_Credit")

                            dPreviousTransDebit = dtValues.Rows(0)("ALM_TrDebit")
                            dTotalTransDebit = dPreviousTransDebit + dTransDebit

                            dPreviousTransCredit = dtValues.Rows(0)("ALM_TrCredit")
                            dTotalTransCredit = dPreviousTransCredit + dTransCredit

                            dCloseDebit = dOpenDebit + dTotalTransDebit
                            dCloseCredit = dOpenCredit + dTotalTransCredit

                            sSql = "" : sSql = "Update Acc_Ledger_Masters Set ALM_OBDebit=" & dOpenDebit & ",ALM_OBCredit=" & dOpenCredit & ",ALM_TrDebit=" & dTotalTransDebit & ",ALM_TrCredit=" & dTotalTransCredit & ",ALM_CloseDebit=" & dCloseDebit & ",ALM_CloseCredit=" & dCloseCredit & ",ALM_TrDate='" & objGen.FormatDtForRDBMS(dtValues.Rows(0)("ATD_TransactionDate"), "CT") & "',ALM_ZoneID=" & dtValues.Rows(0)("ATD_ZoneID") & ",ALM_RegionID=" & dtValues.Rows(0)("ATD_RegionID") & ",ALM_AreaID=" & dtValues.Rows(0)("ATD_AreaID") & ",ALM_BranchID=" & dtValues.Rows(0)("ATD_BranchID") & " "
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
    Public Function GetPartyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iParty As Integer) As String
        Dim sSQL As String = ""
        Dim sParty As String = ""
        Dim dt As New DataTable
        Try
            If iMasterID = 1 Then 'Customer
                sSQL = "" : sSQL = "Select BM_Name As Name, BM_Code As Code from Sales_Buyers_Masters where BM_Delflag='A' and BM_ID = " & iParty & " and BM_CompID= " & iCompID & ""

            ElseIf iMasterID = 2 Then 'Supplier
                sSQL = "" : sSQL = "Select CSM_Name As Name, CSM_Code As Code from CustomerSupplierMaster where CSM_Delflag='A' and CSM_ID = " & iParty & " and CSM_CompID= " & iCompID & ""

            ElseIf iMasterID = 3 Then 'General ledger
                sSQL = "" : sSQL = "Select gl_desc As Name, gl_glcode As Code FROM chart_of_accounts where gl_ID=" & iParty & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' And gl_compid=" & iCompID & " order by gl_glcode"
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Name").ToString()) = False Then
                    sParty = dt.Rows(0)("Name").ToString() & " - " & dt.Rows(0)("Code").ToString()
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

    Public Function GetBillType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBillType As Integer) As String
        Dim sSQL As String = ""
        Dim sBillType As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "" : sSQL = "Select * from ACC_General_Master where mas_master = 9 and mas_Delflag ='A' and Mas_ID = " & iBillType & " and mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Mas_Desc").ToString()) = False Then
                    sBillType = dt.Rows(0)("Mas_Desc").ToString()
                Else
                    sBillType = ""
                End If
            Else
                sBillType = ""
            End If
            Return sBillType
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadFrequentlyUsed(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT ATR_GLCode,COUNT(ATR_GLCode) AS occurrence FROM Account_Transactions where Atr_CompID = " & iCompID & " GROUP BY ATR_GLCode ORDER BY occurrence DESC"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer, ByVal iPaymentType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtCheck As New DataTable
        Dim dtFinal As New DataTable
        Dim sBillNo As String = ""
        Dim i As Integer = 0
        Dim dRow As DataRow
        Try
            dtFinal.Columns.Add("BillID")
            dtFinal.Columns.Add("BillNo")

            If iParty > 0 Then

                If iPaymentType = 1 Or iPaymentType = 4 Then    'With Inventory Advance & With Inventory Payment
                    'sSql = "Select * from Acc_Purchase_JE_Master where Acc_PJE_Party = " & iParty & "  and Acc_PJE_Status ='A' and Acc_PJE_CompID= " & iCompID & ""
                    'dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                    'sSql = "" : sSql = "select * from Acc_Payment_Master where acc_pm_id in(select distinct(ATD_BillId) from Acc_Transactions_Details where ATD_TrType = 1)"  'and ATD_PaymentType = 3)
                    'dtCheck = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                    'If dtCheck.Rows.Count > 0 Then
                    '    For i = 0 To dtCheck.Rows.Count - 1
                    '        sBillNo = sBillNo & "," & dtCheck.Rows(i)("Acc_PM_BillNo").ToString()
                    '    Next
                    'End If

                    sSql = "Select * from Acc_Purchase_JE_Master where Acc_PJE_Party = " & iParty & " And Acc_PJE_PendingAmount > 0 and Acc_PJE_Status ='A' and Acc_PJE_CompID= " & iCompID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                    If sBillNo <> "" Then
                        sBillNo = sBillNo.Remove(0, 1)
                        For i = 0 To dt.Rows.Count - 1
                            If sBillNo.Contains(dt.Rows(i)("Acc_PJE_BillNo").ToString()) = False Then
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = dt.Rows(i)("Acc_PJE_ID").ToString()
                                dRow("BillNo") = "PI" & "-" & dt.Rows(i)("Acc_PJE_BillNo").ToString()
                                dtFinal.Rows.Add(dRow)
                            Else
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = ""
                                dRow("BillNo") = ""
                                dtFinal.Rows.Add(dRow)
                            End If
                        Next
                    Else
                        For i = 0 To dt.Rows.Count - 1
                            If sBillNo.Contains(dt.Rows(i)("Acc_PJE_BillNo").ToString()) = False Then
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = dt.Rows(i)("Acc_PJE_ID").ToString()
                                dRow("BillNo") = "PI" & "-" & dt.Rows(i)("Acc_PJE_BillNo").ToString()
                                dtFinal.Rows.Add(dRow)
                            End If
                        Next
                    End If
                ElseIf iPaymentType = 2 Or iPaymentType = 5 Then    'Without Inventory Advance & Without Inventory Payment
                    'sSql = "" : sSql = "select * from Acc_Payment_Master where acc_pm_id in(select distinct(ATD_BillId) from Acc_Transactions_Details where ATD_TrType = 1)"
                    'dtCheck = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                    'If dtCheck.Rows.Count > 0 Then
                    '    For i = 0 To dtCheck.Rows.Count - 1
                    '        sBillNo = sBillNo & "," & dtCheck.Rows(i)("Acc_PM_BillNo").ToString()
                    '    Next
                    'End If

                    sSql = "Select * from Acc_Purchase_Master where Acc_Purchase_Party = " & iParty & " And Acc_Purchase_PendingAmount > 0 and Acc_Purchase_Status ='A' and Acc_Purchase_CompID= " & iCompID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                    If sBillNo <> "" Then
                        sBillNo = sBillNo.Remove(0, 1)
                        For i = 0 To dt.Rows.Count - 1
                            If sBillNo.Contains(dt.Rows(i)("Acc_purchase_BillNo").ToString()) = False Then
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = dt.Rows(i)("Acc_purchase_ID").ToString()
                                dRow("BillNo") = "P" & "-" & dt.Rows(i)("Acc_purchase_BillNo").ToString()
                                dtFinal.Rows.Add(dRow)
                                'Else
                                '    dRow = dtFinal.NewRow()
                                '    dRow("BillID") = ""
                                '    dRow("BillNo") = ""
                                '    dtFinal.Rows.Add(dRow)
                            End If
                        Next
                    Else
                        For i = 0 To dt.Rows.Count - 1
                            If sBillNo.Contains(dt.Rows(i)("Acc_purchase_BillNo").ToString()) = False Then
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = dt.Rows(i)("Acc_purchase_ID").ToString()
                                dRow("BillNo") = "P" & "-" & dt.Rows(i)("Acc_purchase_BillNo").ToString()
                                dtFinal.Rows.Add(dRow)
                            End If
                        Next
                    End If
                End If

                If dtFinal.Rows.Count > 0 Then
                    Return dtFinal
                Else
                    Return dt
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExistingVoucherNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iParty = 0 Then
                sSql = "Select Acc_PM_TransactionNo,Acc_PM_ID from  Acc_Payment_Master where Acc_PM_CompID=" & iCompID & " And Acc_PM_YearID=" & iYearID & " order by Acc_PM_ID Desc"
            Else
                sSql = "Select Acc_PM_TransactionNo,Acc_PM_ID from  Acc_Payment_Master where Acc_PM_CompID=" & iCompID & " And Acc_PM_YearID=" & iYearID & " And Acc_PM_Party = " & iParty & " order by Acc_PM_ID Desc"
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadTDSType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc from ACC_General_Master where mas_master = 10 And mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadParty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iType = 1 Then
                sSql = "Select ACM_ID,ACM_Code + ' - ' + ACM_Name as Name  from Acc_Customer_Master where ACM_Status='A' and ACM_Type = 'C' and ACM_CompID =" & iCompID & ""
            ElseIf iType = 2 Then
                sSql = "Select ACM_ID,ACM_Code + ' - ' + ACM_Name as Name  from Acc_Customer_Master where ACM_Status='A' and ACM_Type ='S' and ACM_CompID =" & iCompID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSuppliers(sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CSM_ID,CSM_Code + ' - ' + CSM_Name as Name  from CustomerSupplierMaster where CSM_DelFlag='A' and CSM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCustomers(sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Code + ' - ' + BM_Name as Name  from sales_Buyers_Masters where BM_DelFlag='A' and BM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAllGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadLocations(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Description + ' - ' + Mas_Code as Name  from sad_location_general_master where Mas_CustID = " & iParty & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3 "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_head=3 order by gl_AccHead"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iAccHead & " order by gl_glcode"
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
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(Count(Acc_PM_ID)+1,1) from Acc_Payment_Master Where Acc_PM_YearID=" & iYearID & " And Acc_PM_CompID=" & iCompID & " ")

            sSql = "" : sSql = "Select * from ACC_Voucher_Settings where AVS_TransType = 1  and AVS_CompID = " & iCompID & ""
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                sPrefix = ds.Tables(0).Rows(0)("AVS_Prefix").ToString() & "00" & iMax
            Else
                sPrefix = ""
            End If
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBankNames(sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = "", aSql As String = ""
        Dim dt As New DataTable
        Dim GLid As Integer = 0
        Try
            sSql = "Select Acc_Gl from acc_application_settings where Acc_Types='Bank' and Acc_LedgerType='Bank' and Acc_CompID=" & iCompID & ""
            GLid = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If GLid > 0 Then
                aSql = " Select gl_Id, GL_Desc From chart_of_accounts Where gl_parent = " & GLid & " And gl_Status='A' order by gl_id"
                dt = objDBL.SQLExecuteDataTable(sNameSpace, aSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBIllType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc from ACC_General_Master where mas_master = 9 and mas_Delflag ='A' and mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdatePaymentMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPayment As clsPayment)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Acc_Payment_Master where Acc_PM_ID =" & objPayment.iAcc_PM_ID & " and Acc_PM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Acc_Payment_Master set Acc_PM_Party = " & objPayment.iAcc_PM_Party & ",Acc_PM_Location=" & objPayment.iAcc_PM_Location & ","
                sSql = sSql & "Acc_PM_TransactionType =" & objPayment.iAcc_PM_TransactionType & ","
                sSql = sSql & "Acc_PM_BillType = " & objPayment.iAcc_PM_BillType & ",Acc_PM_BillNo = '" & objGen.SafeSQL(objPayment.sAcc_PM_BillNo) & "',"
                sSql = sSql & "Acc_PM_BillDate = " & objGen.FormatDtForRDBMS(objPayment.dAcc_PM_BillDate, "I") & ",Acc_PM_BillAmount = " & objPayment.dAcc_PM_BillAmount & ","
                sSql = sSql & "Acc_Bill_Narration = '" & objGen.SafeSQL(objPayment.sAcc_Bill_Narration) & "' "

                If iPaymentType = 1 Then
                    sSql = sSql & ",Acc_PM_AdvanceAmount = " & objPayment.dAcc_PM_AdvanceAmount & ",Acc_PM_AdvanceNaration = '" & objGen.SafeSQL(objPayment.sAcc_PM_AdvanceNaration) & "',Acc_PM_BalanceAmount = " & objPayment.dAcc_PM_BalanceAmount & " "
                ElseIf iPaymentType = 2 Then
                    sSql = sSql & ",Acc_PM_TDSType = " & objPayment.iAcc_PM_TDSType & ",ACC_PM_TDSDeduct=" & objPayment.dACC_PM_TDSDeduct & ",Acc_PM_TDSAmount=" & objPayment.dAcc_PM_TDSAmount & ","
                    sSql = sSql & "Acc_PM_TDSNarration = '" & objGen.SafeSQL(objPayment.sAcc_PM_TDSNarration) & "' "
                ElseIf iPaymentType = 3 Then
                    sSql = sSql & ",Acc_PM_NetAmount = " & objPayment.dAcc_PM_NetAmount & ",Acc_PM_PaymentNarration = '" & objPayment.sAcc_PM_PaymentNarration & "' "
                ElseIf iPaymentType = 4 Then
                    sSql = sSql & ",Acc_PM_ChequeNo = " & objPayment.sAcc_PM_ChequeNo & ","
                    sSql = sSql & "Acc_PM_ChequeDate = " & objGen.FormatDtForRDBMS(Acc_PM_ChequeDate, "I") & ",Acc_PM_IFSCCode = '" & objPayment.sAcc_PM_IFSCCode & "',"
                    sSql = sSql & "Acc_PM_BankName = '" & objGen.SafeSQL(objPayment.sAcc_PM_BankName) & "',Acc_PM_BranchName = '" & objGen.SafeSQL(objPayment.sAcc_PM_BranchName) & "' "
                End If
                sSql = sSql & "Where Acc_PM_ID = " & objPayment.iAcc_PM_ID & " and Acc_PM_CompID =" & iCompID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return objPayment.iAcc_PM_ID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteTransactionsDetails(ByVal sNameSpace As String, ByVal iCompid As Integer, ByVal iTrans As Integer, ByVal iBillID As Integer)
        Dim sSql As String = "", aSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from acc_Transactions_Details where ATD_TrType=" & iTrans & " and ATD_BillID = " & iBillID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    aSql = "Delete from acc_Transactions_Details where ATD_ID =" & dt.Rows(i)("ATD_ID").ToString() & " and "
                    aSql = aSql & "ATD_TrType =" & iTrans & " and ATD_BillID = " & iBillID & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, aSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePaymentMaster(ByVal sAC As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPayment As clsPayment) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(39) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cc_PM_TransactionType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_TransactionType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_BillType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BillNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BillDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BillAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_BillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Bill_Narration", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objPayment.sAcc_Bill_Narration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BalanceAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_BalanceAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_ChequeNo", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_ChequeDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_IFSCCode ", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BankName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BranchName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_BranchName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_IPAddress ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@acc_PM_InvoiceDate ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_InvoiceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_PaidAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_PaidAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PM_ZoneID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iACC_PM_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PM_RegionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iACC_PM_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PM_AreaID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iACC_PM_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PM_BranchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iACC_PM_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_OrderNO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_OrderNO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_OrderDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_FETotalAmt", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_FETotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_Currency", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_Currency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_DiffAmount", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_DiffAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_CurrencyAmt", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objPayment.dAcc_PM_CurrencyAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_CurrencyTime", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPayment.sAcc_PM_CurrencyTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PM_TrTypeDetails", OleDb.OleDbType.Integer, 2)
            ObjParam(iParamCount).Value = objPayment.iAcc_PM_TrTypeDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAcc_Payment_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPayment As clsPayment)
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Try
    '        iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(ATD_ID)+1,1) from Acc_Transactions_Details")
    '        sSql = "" : sSql = "Insert into Acc_Transactions_Details(ATD_ID,ATD_TransactionDate,ATD_TrType,"
    '        sSql = sSql & "ATD_BillId,ATD_PaymentType,ATD_Head,"
    '        sSql = sSql & "ATD_GL,ATD_SubGL,ATD_DbOrCr,ATD_Debit,ATD_Credit,"
    '        sSql = sSql & "ATD_CreatedBy,ATD_CreatedOn,ATD_Status,"
    '        sSql = sSql & "ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress)"
    '        sSql = sSql & "Values(" & iMax & ",GetDate()," & objPayment.iATD_TrType & ","
    '        sSql = sSql & "" & objPayment.iATD_BillId & "," & objPayment.iATD_PaymentType & "," & objPayment.iATD_Head & ","
    '        sSql = sSql & "" & objPayment.iATD_GL & "," & objPayment.iATD_SubGL & "," & objPayment.iATD_DbOrCr & "," & objPayment.dATD_Debit & "," & objPayment.dATD_Credit & ","
    '        sSql = sSql & "" & objPayment.iATD_CreatedBy & ",GetDate(),'" & objPayment.sATD_Status & "',"
    '        sSql = sSql & "" & objPayment.iATD_YearID & "," & iCompID & ",'" & objPayment.sATD_Operation & "','" & objPayment.sATD_IPAddress & "')"
    '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function SaveTransactionDetails(ByVal sAC As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPayment As clsPayment) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPayment.dATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPayment.dATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPayment.dATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPayment.sATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPayment.iATD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPayment.sATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objPayment.sATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPayment.iATD_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPayment.iATD_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPayment.iATD_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPayment.iATD_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPayment.dATD_OpenDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPayment.dATD_OpenCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPayment.dATD_ClosingDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPayment.dATD_ClosingCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPayment.iATD_SeqReferenceNum
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spacc_Transactions_Details", 1, Arr, ObjParam)

            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPayment As clsPayment)
        Dim sSql As String = ""
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Update Acc_Transactions_Details set ATD_Head =" & objPayment.iATD_Head & ",ATD_GL=" & objPayment.iATD_GL & ","
            sSql = sSql & "ATD_SubGL =" & objPayment.iATD_SubGL & ",ATD_DbOrCr = " & objPayment.iATD_DbOrCr & ","
            sSql = sSql & "ATD_Debit = " & objPayment.dATD_Debit & ",ATD_Credit=" & objPayment.dATD_Credit & " where "
            sSql = sSql & "ATD_ID =" & objPayment.iATD_ID & " and ATD_TrType =" & objPayment.iATD_TrType & " and ATD_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetchartofAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CompID=" & iCompID & " and gl_DelFlag ='C'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSavedTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
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
            dc = New DataColumn("DebitOrCredit", GetType(String))
            dt.Columns.Add(dc)

            'sSql = "" : sSql = "select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,"
            'sSql = sSql & "A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc,"
            'sSql = sSql & "D.Opn_DebitAmt as GLDebit, D.Opn_CreditAmount as GLCredit "
            'sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on "
            'sSql = sSql & "A.ATD_BillId = " & iPaymentID & " and A.ATD_TRType = 1 and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on "
            'sSql = sSql & "A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " left join acc_Opening_Balance D on "
            'sSql = sSql & "D.Opn_GLID = A.ATD_Gl and D.Opn_YearID = " & iYearID & " order by a.Atd_id "

            sSql = "" : sSql = "select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,"
            sSql = sSql & "A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            'sSql = sSql & "D.Opn_DebitAmt as GLDebit, D.Opn_CreditAmount as GLCredit "
            sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on "
            sSql = sSql & "A.ATD_BillId = " & iPaymentID & " and A.ATD_TRType = 1 and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on "
            sSql = sSql & "A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " And A.ATD_YearID=" & iYearID & " order by a.Atd_id "

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = ds.Tables(0).Rows(i)("ATD_ID")

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Head").ToString()) = False Then
                        dr("HeadID") = ds.Tables(0).Rows(i)("ATD_Head").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_GL").ToString()) = False Then
                        dr("GLID") = ds.Tables(0).Rows(i)("ATD_GL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_SubGL").ToString()) = False Then
                        dr("SubGLID") = ds.Tables(0).Rows(i)("ATD_SubGL").ToString()
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

                        dr("OpeningBalance") = 0

                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLCode").ToString()) = False Then
                        dr("SubGL") = ds.Tables(0).Rows(i)("SubGLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLDesc").ToString()) = False Then
                        dr("SubGLDescription") = ds.Tables(0).Rows(i)("SubGLDesc").ToString()

                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Debit").ToString()) = False Then
                        dr("Debit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Credit").ToString()) = False Then
                        dr("Credit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_DbOrCr").ToString()) = False Then
                        dr("DebitOrCredit") = ds.Tables(0).Rows(i)("ATD_DbOrCr")
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTransactionDetails(ByVal sAC As String, ByVal sDHEad As String, ByVal iDGLID As Integer, ByVal iDSGLID As Integer, ByVal sCHEad As String, ByVal iCGLID As Integer, ByVal iCSGLID As Integer, ByVal dAmount As Double) As DataTable
        Dim dt As New DataTable, dtGLCODE As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim dr As DataRow
        Dim i As Integer = 0
        Try

            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
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
            dc = New DataColumn("DebitORCredit", GetType(Integer))
            dt.Columns.Add(dc)

            dtGLCODE = GetchartofAccounts(sAC, 1)


            dr = dt.NewRow
            dr("SrNo") = 1
            dr("DebitORCredit") = 1
            If sDHEad = "Asset" Then
                dr("HeadID") = 1
            ElseIf sDHEad = "Income" Then
                dr("HeadID") = 2
            ElseIf sDHEad = "Expenditure" Then
                dr("HeadID") = 3
            ElseIf sDHEad = "Liabilities" Then
                dr("HeadID") = 4
            End If

            dr("GLID") = iDGLID
            dr("SubGLID") = iDSGLID

            Dim dtDGL As New DataTable
            Dim DVGLCODE As New DataView(dtGLCODE)
            DVGLCODE.RowFilter = "Gl_id=" & iDGLID
            dtDGL = DVGLCODE.ToTable

            dr("GLCode") = dtDGL.Rows(0)("gl_glcode")
            dr("GLDescription") = dtDGL.Rows(0)("gl_desc")
            dr("OpeningBalance") = 0

            Dim dtDSUBGL As New DataTable
            Dim DVSUBGLCODE As New DataView(dtGLCODE)
            DVSUBGLCODE.RowFilter = "Gl_id=" & iDSGLID
            dtDSUBGL = DVSUBGLCODE.ToTable

            dr("SubGL") = dtDSUBGL.Rows(0)("gl_glcode")
            dr("SubGLDescription") = dtDSUBGL.Rows(0)("gl_desc")
            dr("Debit") = dAmount
            dr("Credit") = 0.00
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("SrNo") = 2
            dr("DebitORCredit") = 2

            If sCHEad = "Asset" Then
                dr("HeadID") = 1
            ElseIf sCHEad = "Income" Then
                dr("HeadID") = 2
            ElseIf sCHEad = "Expenditure" Then
                dr("HeadID") = 3
            ElseIf sCHEad = "Liabilities" Then
                dr("HeadID") = 4
            End If
            dr("GLID") = iCGLID
            dr("SubGLID") = iCSGLID

            Dim dtCGL As New DataTable
            Dim DVCGLCODE As New DataView(dtGLCODE)
            DVCGLCODE.RowFilter = "Gl_id=" & iCGLID
            dtCGL = DVCGLCODE.ToTable

            dr("GLCode") = dtCGL.Rows(0)("gl_glcode")
            dr("GLDescription") = dtCGL.Rows(0)("gl_desc")
            dr("OpeningBalance") = 0


            Dim dtCSUBGL As New DataTable
            Dim DVCSUBGLCODE As New DataView(dtGLCODE)
            DVCSUBGLCODE.RowFilter = "Gl_id=" & iCSGLID
            dtCSUBGL = DVCSUBGLCODE.ToTable

            dr("SubGL") = dtCSUBGL.Rows(0)("gl_glcode")
            dr("SubGLDescription") = dtCSUBGL.Rows(0)("gl_desc")
            dr("Debit") = 0.00
            dr("Credit") = dAmount
            dt.Rows.Add(dr)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPaymentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Acc_Payment_Master where Acc_PM_ID =" & iPaymentID & " And Acc_PM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetTransactionsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransType As Integer, ByVal iTransactionID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Acc_Transactions_Details where ATD_ID =" & iTransactionID & " And ATD_TrTYpe =" & iTransType & " And ATD_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeletePaymentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete from Acc_Transactions_Details where ATD_ID = " & iTransactionID & " And Atd_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveChequeDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objPayment As clsPayment)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_Payment_Master Set ACC_PM_ChequeNo = " & objPayment.sAcc_PM_ChequeNo & ",Acc_PM_ChequeDate=" & objGen.FormatDtForRDBMS(dAcc_PM_ChequeDate, "I") & ","
            sSql = sSql & "Acc_PM_IFSCCode = '" & objGen.SafeSQL(objPayment.sAcc_PM_IFSCCode) & "',ACC_PM_BankName='" & objGen.SafeSQL(objPayment.sAcc_PM_BankName) & "',"
            sSql = sSql & "Acc_PM_BranchName = '" & objGen.SafeSQL(objPayment.sAcc_PM_BranchName) & "' where Acc_PM_ID=" & objPayment.iAcc_PM_ID & " and Acc_PM_CompID =" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getCustomerLedgerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from sales_Buyers_Masters where BM_ID =" & iParty & " and BM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getSuppliersLedgerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from CustomerSupplierMaster where CSM_ID =" & iParty & " and CSM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getLedgerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Acc_Customer_Master where ACM_ID =" & iParty & " and ACM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetChartOfAccountHead(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccHead As Integer = 0
        Try
            sSql = "Select gl_AccHead from Chart_of_Accounts where gl_id =" & iGL & " and gl_CompID =" & iCompID & ""
            iAccHead = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iAccHead
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetParent(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSubGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iParent As Integer = 0
        Try
            sSql = "Select gl_Parent from Chart_of_Accounts where gl_id =" & iSubGL & " and gl_CompID =" & iCompID & ""
            iParent = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RemoveDublicate(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("Description"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("Description"), String.Empty)
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
    Public Function loadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sOrder As String, ByVal iInvoice As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dr As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim VAT As String = "", CST As String = "", Exise As String = ""
        Dim Cstval As String = "" : Dim PGM_DocumentRefNo As String = "" : Dim PGM_InvoiceDate As String = "" : Dim POM_OrderDate As String = ""
        Dim Total, TotalAmt, Totaltax As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtdiscount, gtdiscountAmt, GrandTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Dim sArray As Array
        Try

            If sOrder <> "" Then
                sOrder = sOrder.Remove(0, 1)
            End If

            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Colour")
            dt.Columns.Add("t0")
            dt.Columns.Add("t3")
            dt.Columns.Add("t4")
            dt.Columns.Add("t5")
            dt.Columns.Add("t6")
            dt.Columns.Add("t7")
            dt.Columns.Add("t8")
            dt.Columns.Add("t9")
            dt.Columns.Add("t10")
            dt.Columns.Add("t11")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("PGM_InvoiceDate")
            dt.Columns.Add("PGM_DocumentRefNo")
            dt.Columns.Add("POM_OrderDate")
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,f.POM_OrderDate,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_InvoiceDate,PV_CompID,PV_Status,PV_BillNo,"
            sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
            sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,"
            sSql = sSql & " d.Inv_Description Commodity,"
            sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status"
            sSql = sSql & "  from Purchase_verification"
            sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID  where PIA_CompID=" & iCompID & ""
            If sOrder <> "" Then
                sSql = sSql & " And PV_OrderNo in(" & sOrder & ")"
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If

                If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
                    dRow("SlNo") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then


                        If ((dtDetails.Rows(i)("Inv_Size").ToString() = "0") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "")) Then
                            dRow("t0") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t0") = 0
                        End If

                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "3") Then
                            dRow("t3") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t3") = 0
                        End If

                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "4") Then
                            dRow("t4") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t4") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "5") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "39") Then
                            dRow("t5") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t5") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "6") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "40") Then
                            dRow("t6") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t6") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "7") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "41") Then
                            dRow("t7") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t7") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "8") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "42") Then
                            dRow("t8") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t8") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "9") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "43") Then
                            dRow("t9") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t9") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "10") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "44") Then
                            dRow("t10") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t10") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "11") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "45") Then
                            dRow("t11") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t11") = 0
                        End If
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                    dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                    gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                Else
                    dRow("TotalQty") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
                    gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                    gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_VATAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If


                If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                    dRow("CST") = dtDetails.Rows(i)("POD_CST")
                    gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                Else
                    dRow("CST") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                Else
                    dRow("CSTAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                    gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                Else
                    dRow("Exise") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
                    dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                    gtdiscount = gtdiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If

                If (dtDetails.Rows(i)("POM_OrderDate").ToString() <> "") Then
                    If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                        dRow("POM_OrderDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate").ToString(), "D")
                        POM_OrderDate = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate").ToString(), "D")
                    Else
                        dRow("POM_OrderDate") = ""
                    End If
                Else
                    dRow("POM_OrderDate") = ""
                End If

                If (dtDetails.Rows(i)("PGM_InvoiceDate").ToString() <> "") Then
                    If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
                        dRow("PGM_InvoiceDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate").ToString(), "D")
                        PGM_InvoiceDate = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate").ToString(), "D")
                    Else
                        dRow("PGM_InvoiceDate") = ""
                    End If
                Else
                    dRow("PGM_InvoiceDate") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("PGM_DocumentRefNo")) = False Then
                    dRow("PGM_DocumentRefNo") = dtDetails.Rows(i)("PGM_DocumentRefNo")
                    PGM_DocumentRefNo = dtDetails.Rows(i)("PGM_DocumentRefNo")
                End If

                TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt))
                GrandTotal = GrandTotal + TotalAmt

                dt.Rows.Add(dRow)
            Next

            dtDetails.Clear()

            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,f.POM_OrderDate,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_InvoiceDate,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,
                                b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,
                                b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,
                            	c.Inv_Description,c.Inv_Color,c.Inv_Size,
                              	d.Inv_Description Commodity	
                                from Purchase_verification
	                            join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef
	                            join Inventory_Master c on b.PIE_Description=c.Inv_ID
                            	join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID
                            	join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID 
                            	join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"

            If sOrder <> "" Then
                sSql = sSql & " And PV_OrderNo in(" & sOrder & ")"
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("PIE_CommodityID")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Description")) = False Then
                    dRow("SlNo") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("PIE_AcceptQty")) = False Then


                        If ((dtDetails.Rows(i)("Inv_Size").ToString() = "0") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "")) Then
                            dRow("t0") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t0") = 0
                        End If

                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "3") Then
                            dRow("t3") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If

                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "4") Then
                            dRow("t4") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "5") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "39") Then
                            dRow("t5") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "6") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "40") Then
                            dRow("t6") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "7") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "41") Then
                            dRow("t7") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "8") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "42") Then
                            dRow("t8") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "9") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "43") Then
                            dRow("t9") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "10") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "44") Then
                            dRow("t10") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "11") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "45") Then
                            dRow("t11") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_AcceptQty")) = False Then
                    dRow("TotalQty") = dtDetails.Rows(i)("PIE_AcceptQty")
                    gtQty = gtQty + dtDetails.Rows(i)("PIE_AcceptQty")
                Else
                    dRow("TotalQty") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("PIE_Rate")
                    gtMRP = gtMRP + dtDetails.Rows(i)("PIE_Rate")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Vat")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("PIE_Vat")
                    gtVAT = gtVAT + dtDetails.Rows(i)("PIE_Vat")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_VatAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("PIE_VatAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If


                'If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                '    dRow("CST") = dtDetails.Rows(i)("POD_CST")
                '    gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                'Else
                dRow("CST") = 0
                'End If

                'If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                '    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                '    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                '    Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                'Else
                dRow("CSTAmt") = 0
                'End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("PIE_Excise")
                    gtExise = gtExise + dtDetails.Rows(i)("PIE_Excise")
                Else
                    dRow("Exise") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("PIE_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("PIE_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("PIE_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_Discount")) = False And dtDetails.Rows(i)("PIE_Discount") <> "" Then
                    dRow("Discount") = dtDetails.Rows(i)("PIE_Discount")
                    gtdiscount = gtdiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_DiscountAmount")) = False And dtDetails.Rows(i)("PIE_DiscountAmount") <> "" Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("PIE_DiscountAmount")
                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If


                If (dtDetails.Rows(i)("POM_OrderDate").ToString() <> "") Then
                    If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                        dRow("POM_OrderDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate").ToString(), "D")
                        POM_OrderDate = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate").ToString(), "D")
                    Else
                        dRow("POM_OrderDate") = ""
                    End If
                Else
                    dRow("POM_OrderDate") = ""
                End If

                If (dtDetails.Rows(i)("PGM_InvoiceDate").ToString() <> "") Then
                    If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
                        dRow("PGM_InvoiceDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate").ToString(), "D")
                        PGM_InvoiceDate = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate").ToString(), "D")
                    Else
                        dRow("PGM_InvoiceDate") = ""
                    End If
                Else
                    dRow("PGM_InvoiceDate") = ""
                End If

                If IsDBNull(dtDetails.Rows(i)("PGM_DocumentRefNo")) = False Then
                    dRow("PGM_DocumentRefNo") = dtDetails.Rows(i)("PGM_DocumentRefNo")
                    PGM_DocumentRefNo = dtDetails.Rows(i)("PGM_DocumentRefNo")
                End If

                TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt))
                GrandTotal = GrandTotal + TotalAmt
                dt.Rows.Add(dRow)
            Next
            dr = dt.NewRow()
            dr("Commodity") = <b>Total</b>
            dr("Description") = <b>Total</b>
            dr("TotalQty") = gtQty

            dr("PGM_DocumentRefNo") = PGM_DocumentRefNo
            dr("PGM_InvoiceDate") = PGM_InvoiceDate
            dr("POM_OrderDate") = POM_OrderDate

            dr("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(gtMRP))
            dr("VAT") = gtVAT
            dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
            dr("CST") = gtCST
            dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
            dr("Exise") = gtExise
            dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
            dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscount))
            dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscountAmt))
            dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
            dt.Rows.Add(dr)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function CheckNetAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentID As Integer) As Double
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dNetAmount As Double = 0, dAdvance As Double = 0, dBillAmount As Double = 0
    '    Try
    '        sSql = "" : sSql = "Select * from acc_Payment_Master where Acc_PM_ID =" & iPaymentID & " and Acc_PM_CompID = " & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        If dt.Rows.Count > 0 Then
    '            If IsDBNull(dt.Rows(0)("Acc_PM_AdvanceAmount").ToString()) = False Then
    '                dAdvance = dt.Rows(0)("Acc_PM_AdvanceAmount").ToString()
    '            Else
    '                dAdvance = 0
    '            End If

    '            If IsDBNull(dt.Rows(0)("Acc_PM_BillAmount").ToString()) = False Then
    '                dBillAmount = dt.Rows(0)("Acc_PM_BillAmount").ToString()
    '            Else
    '                dBillAmount = 0
    '            End If

    '            dNetAmount = dBillAmount - dAdvance
    '        End If
    '        Return dNetAmount
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Public Function GetBillAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sBillNo As String, ByVal sBillName As String) As String
        Dim sSql As String = ""
        Dim sArray As Array
        Try
            sBillName = sBillName.Remove(0, 1)
            sArray = sBillName.Split("-")
            If sArray.Length > 0 Then
                If sArray(0) = "PI" Then
                    sSql = "" : sSql = "Select sum(Acc_PJE_BillAmount) from  Acc_Purchase_JE_Master where Acc_PJE_ID in (" & sBillNo.Remove(0, 1) & ") and "
                    sSql = sSql & "Acc_PJE_CompID =" & iCompID & ""
                ElseIf sArray(0) = "P" Then
                    sSql = "" : sSql = "Select sum(Acc_Purchase_BillAmount) from  Acc_Purchase_Master where Acc_Purchase_ID in (" & sBillNo.Remove(0, 1) & ") and "
                    sSql = sSql & "Acc_Purchase_CompID =" & iCompID & ""
                End If
            End If
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
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

    Public Function BuildTable() As DataTable
        Dim dt As New DataTable
        Dim dc As New DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
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
            dc = New DataColumn("DebitORCredit", GetType(Integer))
            dt.Columns.Add(dc)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPaymentsMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer, ByVal dAmount As Double, ByVal iDbOrCr As Integer, ByVal dtPayment As DataTable, ByVal dtCOA As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt = BuildTable()

            dr = dt.NewRow
            dr("HeadID") = iHead
            dr("GLID") = iGLID
            dr("SubGLID") = iSubGL
            dr("DebitORCredit") = iDbOrCr

            If iGLID > 0 Then
                Dim dtDGL As New DataTable
                Dim DVGLCODE As New DataView(dtCOA)
                DVGLCODE.RowFilter = "Gl_id=" & iGLID
                dtDGL = DVGLCODE.ToTable

                dr("GLCode") = dtDGL.Rows(0)("gl_glcode")
                dr("GLDescription") = dtDGL.Rows(0)("gl_desc")

            Else
                dr("GLCode") = "" : dr("GLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("GLID") = "0"
            End If


            If iSubGL > 0 Then
                Dim dtDSUBGL As New DataTable
                Dim DVSUBGLCODE As New DataView(dtCOA)
                DVSUBGLCODE.RowFilter = "Gl_id=" & iSubGL
                dtDSUBGL = DVSUBGLCODE.ToTable

                dr("SubGL") = dtDSUBGL.Rows(0)("gl_glcode")
                dr("SubGLDescription") = dtDSUBGL.Rows(0)("gl_desc")
            Else
                dr("SubGL") = "" : dr("SubGLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("SubGLID") = "0"
            End If


            Dim iCount As Integer = 0
            iCount = dtPayment.Rows.Count + 1

            If iDbOrCr = 1 Then
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iGLID)
                End If

                dr("Debit") = dAmount
                dr("Credit") = 0.00
                dr("DebitOrCredit") = 1
            Else
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iGLID)
                End If
                dr("Debit") = 0.00
                dr("Credit") = dAmount
                dr("DebitOrCredit") = 2
            End If
            dt.Rows.Add(dr)

            If dtPayment.Rows.Count > 0 Then
                dtPayment.Merge(dt, True, MissingSchemaAction.Ignore)
            Else
                dtPayment.Merge(dt)
            End If
            'dtPayment.Merge(dt)
            Return dtPayment
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sColumn As String, ByVal iGlID As Integer) As Double
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dDebitOrCredit As Double = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from acc_Opening_Balance where Opn_GLID =" & iGlID & " and Opn_YearID =" & iYearID & " and Opn_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dDebitOrCredit = dt.Rows(0)(sColumn).ToString()
            End If
            Return dDebitOrCredit
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckBillForParty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer, ByVal iPaymentType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PaymentStatus='N' And Acc_Purchase_CompID=" & iCompID & ""
            If iPaymentType = 1 Or iPaymentType = 4 Then    'With Inventory Advance & With Inventory Payment
                sSql = "Select * from Acc_Purchase_JE_Master Where Acc_PJE_Party=" & iPartyID & " and Acc_PJE_Status ='A' And Acc_PJE_PendingAmount > 0 And Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            ElseIf iPaymentType = 2 Or iPaymentType = 5 Then    'Without Inventory Advance & Without Inventory Payment
                sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            ElseIf iPaymentType = 7 Or iPaymentType = 8 Then    'Without Inventory Advance & Without Inventory Payment
                sSql = "Select * from Acc_NonTrading_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim DRreader As OleDb.OleDbDataReader
        Try
            dtTab.Columns.Add("PMID")
            dtTab.Columns.Add("VoucherNO")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("BillAmount")
            'dtTab.Columns.Add("AmountPaid")
            dtTab.Columns.Add("Pending")

            'sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PaymentStatus='N' And Acc_Purchase_CompID=" & iCompID & " Order By Acc_Purchase_ID"
            sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PMID") = dt.Rows(i)("Acc_Purchase_ID")

                If (dt.Rows(i)("Acc_Purchase_TransactionNo").ToString() = "") Then
                    dr("VoucherNO") = ""
                Else
                    dr("VoucherNO") = dt.Rows(i)("Acc_Purchase_TransactionNo")
                End If

                If (dt.Rows(i)("Acc_Purchase_BillNo").ToString() = "") Then
                    dr("BillNo") = ""
                Else
                    dr("BillNo") = "P" & "-" & dt.Rows(i)("Acc_Purchase_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D")
                End If

                'DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'If DRreader.HasRows = True Then
                '    dr("BillAmount") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(PD_FinalTotal) from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'Else
                '    dr("BillAmount") = 0
                'End If

                If IsDBNull(dt.Rows(i)("Acc_Purchase_BillAmount")) = False Then
                    dr("BillAmount") = dt.Rows(i)("Acc_Purchase_BillAmount")
                Else
                    dr("BillAmount") = 0
                End If

                'dr("AmountPaid") = ""

                'DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'If DRreader.HasRows = True Then
                '    dr("Pending") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(PD_PendingAmount) from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'Else
                '    dr("Pending") = 0
                'End If


                If IsDBNull(dt.Rows(i)("Acc_Purchase_PendingAmount")) = False Then
                    dr("Pending") = dt.Rows(i)("Acc_Purchase_PendingAmount")
                Else
                    dr("Pending") = 0
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDerivedBillAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String, ByVal sBillName As String) As String
        Dim sSql As String = ""
        Dim sArray As Array
        Try
            sBillName = sBillName.Remove(0, 1)
            sArray = sBillName.Split("-")
            If sArray.Length > 0 Then
                If sArray(0) = "PI" Then
                    sSql = "" : sSql = "Select sum(Acc_PJE_BillAmount) from  Acc_Purchase_JE_Master where Acc_PJE_ID in (" & sBillNo.Remove(0, 1) & ") and Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " "

                ElseIf sArray(0) = "P" Then
                    sSql = "" : sSql = "Select SUM(Acc_Purchase_BillAmount) from Acc_Purchase_Master Where Acc_Purchase_ID in (" & sBillNo.Remove(0, 1) & ") And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
                End If
            End If
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function DataFromPurchase(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String) As DataTable
    '    Dim dt As New DataTable
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_ID In (" & sBillNo.Remove(0, 1) & ") And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID "
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function DataFromPurchase(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String, ByVal iPaymentType As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            If iPaymentType = 1 Or iPaymentType = 4 Then    'With Inventory Advance & With Inventory Payment
                sSql = "Select * from Acc_Purchase_JE_Master Where Acc_PJE_ID In (" & sBillNo.Remove(0, 1) & ") And Acc_PJE_CompID =" & iCompID & " And Acc_PJE_YearID=" & iYearID & " Order By Acc_PJE_ID "
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            ElseIf iPaymentType = 2 Or iPaymentType = 5 Then    'Without Inventory Advance & Without Inventory Payment
                sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_ID In (" & sBillNo.Remove(0, 1) & ") And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID "
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            ElseIf iPaymentType = 7 Or iPaymentType = 8 Then    'NonTrading Advance & NonTrading Payment
                sSql = "Select * from Acc_NonTrading_Purchase_Master Where Acc_Purchase_ID In (" & sBillNo.Remove(0, 1) & ") And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID "
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DataFromPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPurchaseID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Acc_Purchase_Details Where PD_MasterID in (" & iPurchaseID & ") And PD_CompID =" & iCompID & " Order By PD_ID "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateEditedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal id As Integer, ByVal iPurchaseID As Integer, ByVal dUpdateAmount As Double)
        Dim sSql As String = ""
        Dim dTptaolPendingAmt As Double
        Try
            sSql = "Update Acc_Purchase_Details set PD_PendingAmount=" & dUpdateAmount & " Where PD_CompID=" & iCompID & " And PD_ID = " & id & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            'If PendingAmount is zero update Purchase master status as 'N' '
            sSql = "" : sSql = "Select Sum(PD_PendingAmount) from Acc_Purchase_Details Where PD_MasterID in (" & iPurchaseID & ") And PD_CompID =" & iCompID & " "
            dTptaolPendingAmt = objDBL.SQLGetDescription(sNameSpace, sSql)
            If dTptaolPendingAmt = 0 Then
                sSql = "Update Acc_Purchase_Master set Acc_Purchase_PaymentStatus='P' Where Acc_Purchase_ID in (" & iPurchaseID & ") And Acc_Purchase_CompID =" & iCompID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
            'If PendingAmount is zero update Purchase master status as 'N' '
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DoPayment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sBillNo As String, ByVal dPaidAmount As Double)
        Dim sSql As String = ""
        Dim sArray As Array
        Dim dtMaster As New DataTable : Dim dt As New DataTable
        Dim dAmount As Double
        Try
            sBillNo = sBillNo.Remove(0, 1)
            sArray = sBillNo.Split(",")
            If sArray.Length > 0 Then

            End If

            sSql = "" : sSql = "Select PD_PendingAmount from Acc_Purchase_Details Where PD_MasterID in (" & sArray(0) & ") And PD_CompID =" & iCompID & " Order By PD_ID "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If dPaidAmount > dt.Rows(i)("PD_PendingAmount") Then
                        dAmount = dPaidAmount - dt.Rows(i)("PD_PendingAmount")
                        sSql = "" : sSql = "Update Acc_Purchase_Details set PD_PendingAmount=0 Where PD_ID =" & dt.Rows(i)("PD_ID") & " And PD_CompID =" & iCompID & " "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If

                Next
            End If


        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DataFromNextPurchaseInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String, ByVal iPartyID As Integer, ByVal iPaymentType As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_ID Not in (" & sBillNo.Remove(0, 1) & ") And Acc_Purchase_Party=" & iPartyID & " And Acc_Purchase_PaymentStatus = 'N' And Acc_Purchase_CompID =" & iCompID & " Order By Acc_Purchase_ID "
            If sBillNo <> "" Then
                If iPaymentType = 1 Or iPaymentType = 4 Then    'With Inventory Advance & With Inventory Payment
                    sSql = "Select * from Acc_Purchase_JE_Master Where Acc_PJE_ID Not in (" & sBillNo.Remove(0, 1) & ") And Acc_PJE_Party=" & iPartyID & " And Acc_PJE_PendingAmount > 0 And Acc_PJE_CompID =" & iCompID & " And Acc_PJE_YearID=" & iYearID & " Order By Acc_PJE_ID "
                    dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                ElseIf iPaymentType = 2 Or iPaymentType = 5 Then    'Without Inventory Advance & Without Inventory Payment
                    sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_ID Not in (" & sBillNo.Remove(0, 1) & ") And Acc_Purchase_Party=" & iPartyID & " And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID "
                    dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                ElseIf iPaymentType = 7 Or iPaymentType = 8 Then    'NonTrading Advance & NonTrading Payment
                    sSql = "Select * from Acc_NonTrading_Purchase_Master Where Acc_Purchase_ID Not in (" & sBillNo.Remove(0, 1) & ") And Acc_Purchase_Party=" & iPartyID & " And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID "
                    dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                End If
            Else
                If iPaymentType = 1 Or iPaymentType = 4 Then    'With Inventory Advance & With Inventory Payment
                    sSql = "Select * from Acc_Purchase_JE_Master Where Acc_PJE_Party=" & iPartyID & " And Acc_PJE_PendingAmount > 0 And Acc_PJE_CompID =" & iCompID & " And Acc_PJE_YearID=" & iYearID & " Order By Acc_PJE_ID "
                    dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                ElseIf iPaymentType = 2 Or iPaymentType = 5 Then    'Without Inventory Advance & Without Inventory Payment
                    sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID "
                    dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                ElseIf iPaymentType = 7 Or iPaymentType = 8 Then    'NonTrading Advance & NonTrading Payment
                    sSql = "Select * from Acc_NonTrading_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID "
                    dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("Ext")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")

            sSql = "Select Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE From edt_attachments where ATCH_CompID=" & iACID & " And "
            sSql = sSql & " ATCH_ID = " & iAttachID & " AND ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("Ext") = dt.Rows(i)("ATCH_EXT")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objGen.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objGenFun.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objGen.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function GetExtension(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer) As String
        Dim sSql As String, sExtn As String = ""
        Try
            sSql = "Select atch_ext from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
            sExtn = objDBL.SQLGetDescription(sAC, sSql)
            Return sExtn
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindManualBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim bCheck As Boolean
        Dim DRreader As OleDb.OleDbDataReader
        Try
            dtTab.Columns.Add("PMID")
            dtTab.Columns.Add("VoucherNO")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("BillAmount")
            dtTab.Columns.Add("AmountPaid")
            dtTab.Columns.Add("Pending")

            'sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PaymentStatus='N' And Acc_Purchase_CompID=" & iCompID & " Order By Acc_Purchase_ID"
            sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PMID") = dt.Rows(i)("Acc_Purchase_ID")

                If (dt.Rows(i)("Acc_Purchase_TransactionNo").ToString() = "") Then
                    dr("VoucherNO") = ""
                Else
                    dr("VoucherNO") = dt.Rows(i)("Acc_Purchase_TransactionNo")
                End If

                If (dt.Rows(i)("Acc_Purchase_BillNo").ToString() = "") Then
                    dr("BillNo") = ""
                Else
                    dr("BillNo") = "P" & "-" & dt.Rows(i)("Acc_Purchase_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D")
                End If
                'DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'If DRreader.HasRows = True Then
                '    dr("BillAmount") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(PD_FinalTotal) from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'Else
                '    dr("BillAmount") = 0
                'End If

                If IsDBNull(dt.Rows(i)("Acc_Purchase_BillAmount")) = False Then
                    dr("BillAmount") = dt.Rows(i)("Acc_Purchase_BillAmount")
                Else
                    dr("BillAmount") = 0
                End If

                dr("AmountPaid") = ""

                'DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'If DRreader.HasRows = True Then
                '    dr("Pending") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(PD_PendingAmount) from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'Else
                '    dr("Pending") = 0
                'End If

                If IsDBNull(dt.Rows(i)("Acc_Purchase_PendingAmount")) = False Then
                    dr("Pending") = dt.Rows(i)("Acc_Purchase_PendingAmount")
                Else
                    dr("Pending") = 0
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_OrderDate from Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID=" & iYearID & " and POM_ID=" & iID & " And POM_OralStatus='P'"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPaymentPKID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPartyName As String, ByVal iTYpe As Integer, ByVal iPODid As Integer, ByVal sBillNo As String, ByVal iPaymentID As Integer) As Integer
        Dim sSql As String = ""
        Try
            If iTYpe = 2 Then
                'sSql = "Select Sum(ATD_Debit) from Acc_Transactions_Details where ATD_DborCr=1 And"
                If iPaymentID = 1 Or iPaymentID = 4 Then
                    sSql = "Select Sum(ATD_Debit) from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And (Acc_PM_OrderNo>0 Or Acc_PM_BillNo Like 'PI%') And (Acc_PM_PaymentType=1 Or Acc_PM_PaymentType=4) And ATD_DborCr=1 And"
                ElseIf iPaymentID = 2 Or iPaymentID = 5 Then
                    sSql = "Select Sum(ATD_Debit) from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And (Acc_PM_OrderNo=0 Or Acc_PM_BillNo Like 'P-%') And (Acc_PM_PaymentType=2 Or Acc_PM_PaymentType=5) And ATD_DborCr=1 And"
                End If
            ElseIf iTYpe = 3 Then
                'sSql = "Select Sum(ATD_Credit) from Acc_Transactions_Details where ATD_DborCr=2 And"
                If iPaymentID = 1 Or iPaymentID = 4 Then
                    sSql = "Select Sum(ATD_Credit) from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And (Acc_PM_OrderNo>0 Or Acc_PM_BillNo Like 'PI%') And (Acc_PM_PaymentType=1 Or Acc_PM_PaymentType=4) And ATD_DborCr=2 And"
                ElseIf iPaymentID = 2 Or iPaymentID = 5 Then
                    sSql = "Select Sum(ATD_Credit) from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And (Acc_PM_OrderNo=0 Or Acc_PM_BillNo Like 'P-%') And (Acc_PM_PaymentType=2 Or Acc_PM_PaymentType=5) And ATD_DborCr=2 And"
                End If
            ElseIf iTYpe = 1 Then
                If iPODid > 0 And sBillNo = "" Then
                    sSql = "Select ATD_BillId from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And ACc_PM_OrderNo=" & iPODid & " And ATD_TrType=1 And"
                ElseIf iPODid = 0 And sBillNo <> "" Then
                    sSql = "Select ATD_BillId from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And ACc_PM_BillNo='" & sBillNo & "' And ATD_TrType=1 And"
                ElseIf iPODid > 0 And sBillNo <> "" Then
                    sSql = "Select ATD_BillId from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And ACc_PM_OrderNo=" & iPODid & " And ACc_PM_BillNo='" & sBillNo & "' And ATD_TrType=1 And"
                ElseIf iPODid = 0 And sBillNo = "" Then
                    sSql = "Select ATD_BillId from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And ATD_TrType=1 And"
                End If
                'sSql = "Select ATD_BillId from Acc_Transactions_Details where"
            End If
            sSql = sSql & " ATD_SUBGL IN(select gl_id From chart_of_Accounts"
            sSql = sSql & " Where gl_Desc Like '%" & sPartyName & " Advance%' And gl_CompID=" & iCompID & ") And ATD_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer, ByVal iCust As Integer) As String
        Dim sSql As String = ""
        Try
            If iCust = 1 Then
                sSql = "Select BM_Name From Sales_Buyers_Masters where BM_ID=" & iPartyID & " And BM_CompID=" & iCompID & " "
            ElseIf iCust = 2 Then
                sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & iPartyID & " And CSM_CompID=" & iCompID & ""
            ElseIf iCust = 3 Then
                sSql = "Select gl_desc from chart_of_Accounts where gl_id=" & iPartyID & " And gl_CompID=" & iCompID & ""
            End If
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadNewGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sPayment As String, ByVal dValue As Double, ByVal iPOID As Integer, ByVal sBillNo As String, ByVal iPaymentType As Integer, ByVal dCredit As Double) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable, dtWP As New DataTable
        Dim drow As DataRow
        Dim sSql As String
        Try
            dt.Columns.Add("ID", GetType(String))
            dt.Columns.Add("HeadID", GetType(String))
            dt.Columns.Add("GLID", GetType(String))
            dt.Columns.Add("SubGLID", GetType(String))
            dt.Columns.Add("GLCode", GetType(String))
            dt.Columns.Add("GLDescription", GetType(String))
            dt.Columns.Add("SubGL", GetType(String))
            dt.Columns.Add("SubGLDescription", GetType(String))
            dt.Columns.Add("Debit", GetType(String))
            dt.Columns.Add("Credit", GetType(String))
            dt.Columns.Add("Balance", GetType(String))
            dt.Columns.Add("DebitOrCredit", GetType(String))
            dt.Columns.Add("PO", GetType(String))
            dt.Columns.Add("BillNo", GetType(String))

            '   If iPOID > 0 And sBillNo = "" Then
            '       sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription,C.gl_glCode as SubGlCode, C.gl_Desc as SubGLDesc,ACC_PM_OrderNo,POM_OrderNO,ACc_PM_BillNo 
            '                           from Acc_Transactions_Details A 
            '                           Left join chart_of_Accounts B on B.gl_ID=A.ATD_GL
            'Left join chart_of_Accounts C on C.gl_ID=A.ATD_SubGL 
            '                           Left Join Acc_Payment_Master On Acc_PM_ID=A.ATD_BILLID 
            '                           Left Join Purchase_Order_Master ON POM_ID=Acc_PM_OrderNo Where ACC_PM_ID in (" & sPayment & ") And A.ATD_TRType = 1 and ATD_DBOrCR=1 And ACc_PM_OrderNo=" & iPOID & " order by a.Atd_id "
            '   ElseIf iPOID = 0 And sBillNo <> "" Then
            '       sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription,C.gl_glCode as SubGlCode, C.gl_Desc as SubGLDesc,ACC_PM_OrderNo,POM_OrderNO,ACc_PM_BillNo 
            '                           from Acc_Transactions_Details A 
            '                           Left join chart_of_Accounts B on B.gl_ID=A.ATD_GL
            'Left join chart_of_Accounts C on C.gl_ID=A.ATD_SubGL 
            '                           Left Join Acc_Payment_Master On Acc_PM_ID=A.ATD_BILLID 
            '                           Left Join Purchase_Order_Master ON POM_ID=Acc_PM_OrderNo Where ACC_PM_ID in (" & sPayment & ") And A.ATD_TRType = 1 and ATD_DBOrCR=1 And ACc_PM_BillNo='" & sBillNo & "' order by a.Atd_id "
            '   ElseIf iPOID > 0 And sBillNo <> "" Then
            '       sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription,C.gl_glCode as SubGlCode, C.gl_Desc as SubGLDesc,ACC_PM_OrderNo,POM_OrderNO,ACc_PM_BillNo 
            '                           from Acc_Transactions_Details A 
            '                           Left join chart_of_Accounts B on B.gl_ID=A.ATD_GL
            'Left join chart_of_Accounts C on C.gl_ID=A.ATD_SubGL 
            '                           Left Join Acc_Payment_Master On Acc_PM_ID=A.ATD_BILLID 
            '                           Left Join Purchase_Order_Master ON POM_ID=Acc_PM_OrderNo Where ACC_PM_ID in (" & sPayment & ") And A.ATD_TRType = 1 and ATD_DBOrCR=1 And ACc_PM_OrderNo=" & iPOID & " And ACc_PM_BillNo='" & sBillNo & "' order by a.Atd_id "
            '   ElseIf iPOID = 0 And sBillNo = "" Then
            '       sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription,C.gl_glCode as SubGlCode, C.gl_Desc as SubGLDesc,ACC_PM_OrderNo,POM_OrderNO,ACc_PM_BillNo 
            '                           from Acc_Transactions_Details A 
            '                           Left join chart_of_Accounts B on B.gl_ID=A.ATD_GL
            'Left join chart_of_Accounts C on C.gl_ID=A.ATD_SubGL 
            '                           Left Join Acc_Payment_Master On Acc_PM_ID=A.ATD_BILLID 
            '                           Left Join Purchase_Order_Master ON POM_ID=Acc_PM_OrderNo Where ACC_PM_ID in (" & sPayment & ") And A.ATD_TRType = 1 and ATD_DBOrCR=1 order by a.Atd_id "
            '   End If

            If iPaymentType = 1 Or iPaymentType = 4 Then    'With Inventory Advance & With Inventory Payment
                sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription,C.gl_glCode as SubGlCode, C.gl_Desc as SubGLDesc,ACC_PM_OrderNo,POM_OrderNO,ACc_PM_BillNo 
                                    from Acc_Transactions_Details A 
                                    Left join chart_of_Accounts B on B.gl_ID=A.ATD_GL
									Left join chart_of_Accounts C on C.gl_ID=A.ATD_SubGL 
                                    Left Join Acc_Payment_Master On Acc_PM_ID=A.ATD_BILLID 
                                    Left Join Purchase_Order_Master ON POM_ID=Acc_PM_OrderNo Where (Acc_PM_OrderNo>0 Or Acc_PM_BillNo Like 'PI%') And Acc_PM_PaymentType=1 And A.ATD_TRType = 1 and ATD_DBOrCR=1 order by a.Atd_id "
            ElseIf iPaymentType = 2 Or iPaymentType = 5 Then    'Without Inventory Advance & Without Inventory Payment
                sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription,C.gl_glCode as SubGlCode, C.gl_Desc as SubGLDesc,ACC_PM_OrderNo,POM_OrderNO,ACc_PM_BillNo 
                                    from Acc_Transactions_Details A 
                                    Left join chart_of_Accounts B on B.gl_ID=A.ATD_GL
									Left join chart_of_Accounts C on C.gl_ID=A.ATD_SubGL 
                                    Left Join Acc_Payment_Master On Acc_PM_ID=A.ATD_BILLID 
                                    Left Join Purchase_Order_Master ON POM_ID=Acc_PM_OrderNo Where (Acc_PM_OrderNo=0 And Acc_PM_BillNo Like 'P-%') And Acc_PM_PaymentType=2 And A.ATD_TRType = 1 and ATD_DBOrCR=1 order by a.Atd_id "
            ElseIf iPaymentType = 7 Or iPaymentType = 8 Then    'NonTrading Advance & NonTrading Payment
                sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription,C.gl_glCode as SubGlCode, C.gl_Desc as SubGLDesc,ACC_PM_OrderNo,POM_OrderNO,ACc_PM_BillNo 
                                    from Acc_Transactions_Details A 
                                    Left join chart_of_Accounts B on B.gl_ID=A.ATD_GL
									Left join chart_of_Accounts C on C.gl_ID=A.ATD_SubGL 
                                    Left Join Acc_Payment_Master On Acc_PM_ID=A.ATD_BILLID 
                                    Left Join Purchase_Order_Master ON POM_ID=Acc_PM_OrderNo Where (Acc_PM_OrderNo=0 And Acc_PM_BillNo Like 'NT-P%') And Acc_PM_PaymentType=2 And A.ATD_TRType = 1 and ATD_DBOrCR=1 order by a.Atd_id "
            End If


            'sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_DbOrCr,"
            'sSql = sSql & " A.ATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode,c.Gl_Desc as SubGlDesc,"
            'sSql = sSql & " D.Opn_DebitAmt as GLDebit, D.Opn_CreditAmount as GLCredit,ACC_PM_OrderNo,POM_OrderNO,ACc_PM_BillNo"
            'sSql = sSql & " from Acc_Transactions_Details A join chart_of_Accounts B on "
            'sSql = sSql & " A.ATD_BillId = " & iPayment & " and A.ATD_TRType = 1 and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on "
            'sSql = sSql & " A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPayment & " left join acc_Opening_Balance D on "
            'sSql = sSql & " D.Opn_GLID = A.ATD_Gl and D.Opn_YearID = " & iYearID & ""
            'sSql = sSql & " Left Join Acc_Payment_Master On ACC_PM_ID= " & iPayment & " And ACC_PM_CompID=" & iACID & ""
            'sSql = sSql & " Left Join Purchase_Order_Master ON POM_CompID=" & iACID & ""
            'sSql = sSql & " Where ATD_DBOrCR=1 order by a.Atd_id "

            dtWP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For j = 0 To dtWP.Rows.Count - 1
                drow = dt.NewRow
                If IsDBNull(dtWP.Rows(j)("ATD_ID").ToString()) = False Then
                    drow("ID") = dtWP.Rows(j)("ATD_ID").ToString()
                End If
                If IsDBNull(dtWP.Rows(j)("ATD_Head").ToString()) = False Then
                    drow("HeadID") = dtWP.Rows(j)("ATD_Head").ToString()
                End If

                If IsDBNull(dtWP.Rows(j)("ATD_GL").ToString()) = False Then
                    drow("GLID") = dtWP.Rows(j)("ATD_GL").ToString()
                End If

                If IsDBNull(dtWP.Rows(j)("ATD_SubGL").ToString()) = False Then
                    drow("SubGLID") = dtWP.Rows(j)("ATD_SubGL").ToString()
                End If

                If IsDBNull(dtWP.Rows(j)("GLCode").ToString()) = False Then
                    drow("GLCode") = dtWP.Rows(j)("GLCode").ToString()
                End If

                If IsDBNull(dtWP.Rows(j)("SubGLCode").ToString()) = False Then
                    drow("SubGL") = dtWP.Rows(j)("SubGLCode").ToString()
                End If

                If IsDBNull(dtWP.Rows(j)("SubGLDesc").ToString()) = False Then
                    drow("SubGLDescription") = dtWP.Rows(j)("SubGLDesc").ToString()
                End If
                If IsDBNull(dtWP.Rows(j)("GLDescription").ToString()) = False Then
                    drow("GLDescription") = dtWP.Rows(j)("GLDescription").ToString()
                End If

                'If dValue > 0 Then
                '    drow("Debit") = Convert.ToDecimal(dValue).ToString("#,##0.00")
                'Else
                '    If IsDBNull(dtWP.Rows(j)("ATD_Debit").ToString()) = False Then
                '        drow("Debit") = Convert.ToDecimal(dtWP.Rows(j)("ATD_Debit").ToString()).ToString("#,##0.00")
                '    End If
                'End If
                If dCredit > 0 Then
                    If IsDBNull(dtWP.Rows(j)("ATD_Debit").ToString()) = False Then
                        If dtWP.Rows(j)("ATD_Debit") = dCredit Then
                            drow("Debit") = Convert.ToDecimal((dtWP.Rows(j)("ATD_Debit").ToString()) - dCredit).ToString("#,##0.00")
                            dCredit = 0
                        ElseIf dtWP.Rows(j)("ATD_Debit") > dCredit Then
                            drow("Debit") = Convert.ToDecimal((dtWP.Rows(j)("ATD_Debit").ToString()) - dCredit).ToString("#,##0.00")
                            dCredit = 0
                        ElseIf dtWP.Rows(j)("ATD_Debit") < dCredit Then
                            drow("Debit") = 0
                            dCredit = Convert.ToDecimal(dCredit - (dtWP.Rows(j)("ATD_Debit").ToString())).ToString("#,##0.00")
                        End If
                    End If
                Else
                    If IsDBNull(dtWP.Rows(j)("ATD_Debit").ToString()) = False Then
                        drow("Debit") = Convert.ToDecimal(dtWP.Rows(j)("ATD_Debit").ToString()).ToString("#,##0.00")
                    End If
                End If

                If IsDBNull(dtWP.Rows(j)("ATD_Credit").ToString()) = False Then
                    drow("Credit") = Convert.ToDecimal(dtWP.Rows(j)("ATD_Credit").ToString()).ToString("#,##0.00")
                End If
                If IsDBNull(dtWP.Rows(j)("ATD_DbOrCr").ToString()) = False Then
                    drow("DebitOrCredit") = dtWP.Rows(j)("ATD_DbOrCr").ToString()
                End If

                If IsDBNull(dtWP.Rows(j)("POM_OrderNO").ToString()) = False Then
                    drow("PO") = dtWP.Rows(j)("POM_OrderNO").ToString()
                End If
                If IsDBNull(dtWP.Rows(j)("Acc_PM_BillNo").ToString()) = False Then
                    drow("BillNo") = dtWP.Rows(j)("Acc_PM_BillNo").ToString()
                End If
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPayment As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_Supplier=" & iParty & " And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & ""
            'sSql = sSql & " and POM_Status='A' and POM_OralStatus='P'"
            'sSql = sSql & " And POM_ID not in (Select Acc_PM_OrderNo from Acc_Payment_Master Where ACC_PM_CompID=" & iCompID & " And Acc_PM_OrderNo is not Null"
            'If iPayment > 0 Then
            '    sSql = sSql & " And ACC_PM_ID <> " & iPayment & ""
            'End If
            'sSql = sSql & " ) Order By POM_ID desc"

            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_Supplier=" & iParty & " And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & ""
            sSql = sSql & " and POM_Status='A' and POM_OralStatus='P' Order By POM_ID desc "
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindInventoryManualBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer, ByVal iPOID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim bCheck As Boolean
        Dim DRreader As OleDb.OleDbDataReader
        Try
            dtTab.Columns.Add("PMID")
            dtTab.Columns.Add("VoucherNO")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("BillAmount")
            dtTab.Columns.Add("AmountPaid")
            dtTab.Columns.Add("Pending")

            'sSql = "Select * from Acc_Purchase_JE_Master Where Acc_PJE_Party=" & iPartyID & " and Acc_PJE_Status ='A' And Acc_PJE_PaymentStatus='N' And Acc_PJE_CompID=" & iCompID & " Order By Acc_PJE_ID"
            If iPOID > 0 Then
                sSql = "Select * from Acc_Purchase_JE_Master,Purchase_Verification Where Acc_PJE_InvoiceID=PV_ID And PV_OrderNo=" & iPOID & " And Acc_PJE_Party=" & iPartyID & " and Acc_PJE_Status ='A' And Acc_PJE_PendingAmount > 0 And Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " Order By Acc_PJE_ID"
            Else
                sSql = "Select * from Acc_Purchase_JE_Master Where Acc_PJE_Party=" & iPartyID & " and Acc_PJE_Status ='A' And Acc_PJE_PendingAmount > 0 And Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " Order By Acc_PJE_ID"
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PMID") = dt.Rows(i)("Acc_PJE_ID")

                If (dt.Rows(i)("Acc_PJE_TransactionNo").ToString() = "") Then
                    dr("VoucherNO") = ""
                Else
                    dr("VoucherNO") = dt.Rows(i)("Acc_PJE_TransactionNo")
                End If

                If (dt.Rows(i)("Acc_PJE_BillNo").ToString() = "") Then
                    dr("BillNo") = ""
                Else
                    dr("BillNo") = "PI" & "-" & dt.Rows(i)("Acc_PJE_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_PJE_BillDate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_PJE_BillDate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_PJE_BillDate"), "D")
                End If

                If IsDBNull(dt.Rows(i)("Acc_PJE_BillAmount")) = False Then
                    dr("BillAmount") = dt.Rows(i)("Acc_PJE_BillAmount")
                Else
                    dr("BillAmount") = 0
                End If

                dr("AmountPaid") = ""

                If IsDBNull(dt.Rows(i)("Acc_PJE_PendingAmount")) = False Then
                    dr("Pending") = dt.Rows(i)("Acc_PJE_PendingAmount")
                Else
                    dr("Pending") = 0
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindInventoryBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer, ByVal iPOID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim DRreader As OleDb.OleDbDataReader
        Try
            dtTab.Columns.Add("PMID")
            dtTab.Columns.Add("VoucherNO")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("BillAmount")
            'dtTab.Columns.Add("AmountPaid")
            dtTab.Columns.Add("Pending")

            'sSql = "Select * from Acc_Purchase_JE_Master Where Acc_PJE_Party=" & iPartyID & " and Acc_PJE_Status ='A' And Acc_PJE_PaymentStatus='N' And Acc_PJE_CompID=" & iCompID & " Order By Acc_PJE_ID"
            If iPOID > 0 Then
                sSql = "Select * from Acc_Purchase_JE_Master,Purchase_Verification Where Acc_PJE_InvoiceID=PV_ID And PV_OrderNo=" & iPOID & " And Acc_PJE_Party=" & iPartyID & " and Acc_PJE_Status ='A' And Acc_PJE_PendingAmount > 0 And Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " Order By Acc_PJE_ID"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select * from Acc_Purchase_JE_Master Where Acc_PJE_Party=" & iPartyID & " and Acc_PJE_Status ='A' And Acc_PJE_PendingAmount > 0 And Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " Order By Acc_PJE_ID"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If

            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PMID") = dt.Rows(i)("Acc_PJE_ID")

                If (dt.Rows(i)("Acc_PJE_TransactionNo").ToString() = "") Then
                    dr("VoucherNO") = ""
                Else
                    dr("VoucherNO") = dt.Rows(i)("Acc_PJE_TransactionNo")
                End If

                If (dt.Rows(i)("Acc_PJE_BillNo").ToString() = "") Then
                    dr("BillNo") = ""
                Else
                    dr("BillNo") = "PI" & "-" & dt.Rows(i)("Acc_PJE_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_PJE_BillDate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_PJE_BillDate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_PJE_BillDate"), "D")
                End If

                If IsDBNull(dt.Rows(i)("Acc_PJE_BillAmount")) = False Then
                    dr("BillAmount") = dt.Rows(i)("Acc_PJE_BillAmount")
                Else
                    dr("BillAmount") = 0
                End If

                'dr("AmountPaid") = ""

                If IsDBNull(dt.Rows(i)("Acc_PJE_PendingAmount")) = False Then
                    dr("Pending") = dt.Rows(i)("Acc_PJE_PendingAmount")
                Else
                    dr("Pending") = 0
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdatePendingAmt(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPurchaseID As Integer, ByVal dUpdateAmount As Double, ByVal iPaymentType As Integer)
        Dim sSql As String = ""
        Try
            If iPaymentType = 4 Then    'With Inventory Payment
                sSql = "Update Acc_Purchase_JE_Master set Acc_PJE_PendingAmount=" & dUpdateAmount & " Where Acc_PJE_ID =" & iPurchaseID & " And Acc_PJE_CompID =" & iCompID & " And Acc_PJE_YearID=" & iYearID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            ElseIf iPaymentType = 5 Then    'Without Inventory Payment
                sSql = "Update Acc_Purchase_Master set Acc_Purchase_PendingAmount=" & dUpdateAmount & " Where Acc_Purchase_ID =" & iPurchaseID & " And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            ElseIf iPaymentType = 8 Then    'NonTrading Payment
                sSql = "Update Acc_NonTrading_Purchase_Master set Acc_Purchase_PendingAmount=" & dUpdateAmount & " Where Acc_Purchase_ID =" & iPurchaseID & " And Acc_Purchase_CompID =" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderNo(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select PV_OrderNo From Purchase_Verification Where PV_ID in (Select Acc_PJE_InvoiceID from Acc_Purchase_JE_Master Where Acc_PJE_ID=" & sBillNo.Remove(0, 1) & " And Acc_PJE_YearID=" & iYearID & " And Acc_PJE_CompID =" & iCompID & ") "
            GetOrderNo = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetOrderNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindNonTradingManualBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim bCheck As Boolean
        Dim DRreader As OleDb.OleDbDataReader
        Try
            dtTab.Columns.Add("PMID")
            dtTab.Columns.Add("VoucherNO")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("BillAmount")
            dtTab.Columns.Add("AmountPaid")
            dtTab.Columns.Add("Pending")

            'sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PaymentStatus='N' And Acc_Purchase_CompID=" & iCompID & " Order By Acc_Purchase_ID"
            sSql = "Select * from Acc_NonTrading_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PMID") = dt.Rows(i)("Acc_Purchase_ID")

                If (dt.Rows(i)("Acc_Purchase_TransactionNo").ToString() = "") Then
                    dr("VoucherNO") = ""
                Else
                    dr("VoucherNO") = dt.Rows(i)("Acc_Purchase_TransactionNo")
                End If

                If (dt.Rows(i)("Acc_Purchase_BillNo").ToString() = "") Then
                    dr("BillNo") = ""
                Else
                    dr("BillNo") = "P" & "-" & dt.Rows(i)("Acc_Purchase_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D")
                End If
                'DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'If DRreader.HasRows = True Then
                '    dr("BillAmount") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(PD_FinalTotal) from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'Else
                '    dr("BillAmount") = 0
                'End If

                If IsDBNull(dt.Rows(i)("Acc_Purchase_BillAmount")) = False Then
                    dr("BillAmount") = dt.Rows(i)("Acc_Purchase_BillAmount")
                Else
                    dr("BillAmount") = 0
                End If

                dr("AmountPaid") = ""

                'DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'If DRreader.HasRows = True Then
                '    dr("Pending") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(PD_PendingAmount) from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'Else
                '    dr("Pending") = 0
                'End If

                If IsDBNull(dt.Rows(i)("Acc_Purchase_PendingAmount")) = False Then
                    dr("Pending") = dt.Rows(i)("Acc_Purchase_PendingAmount")
                Else
                    dr("Pending") = 0
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindNonTradingBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim DRreader As OleDb.OleDbDataReader
        Try
            dtTab.Columns.Add("PMID")
            dtTab.Columns.Add("VoucherNO")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("BillAmount")
            'dtTab.Columns.Add("AmountPaid")
            dtTab.Columns.Add("Pending")

            'sSql = "Select * from Acc_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PaymentStatus='N' And Acc_Purchase_CompID=" & iCompID & " Order By Acc_Purchase_ID"
            sSql = "Select * from Acc_NonTrading_Purchase_Master Where Acc_Purchase_Party=" & iPartyID & " and Acc_Purchase_Status ='A' And Acc_Purchase_PendingAmount > 0 And Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " Order By Acc_Purchase_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PMID") = dt.Rows(i)("Acc_Purchase_ID")

                If (dt.Rows(i)("Acc_Purchase_TransactionNo").ToString() = "") Then
                    dr("VoucherNO") = ""
                Else
                    dr("VoucherNO") = dt.Rows(i)("Acc_Purchase_TransactionNo")
                End If

                If (dt.Rows(i)("Acc_Purchase_BillNo").ToString() = "") Then
                    dr("BillNo") = ""
                Else
                    dr("BillNo") = "P" & "-" & dt.Rows(i)("Acc_Purchase_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Purchase_BillDate"), "D")
                End If

                'DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'If DRreader.HasRows = True Then
                '    dr("BillAmount") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(PD_FinalTotal) from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'Else
                '    dr("BillAmount") = 0
                'End If

                If IsDBNull(dt.Rows(i)("Acc_Purchase_BillAmount")) = False Then
                    dr("BillAmount") = dt.Rows(i)("Acc_Purchase_BillAmount")
                Else
                    dr("BillAmount") = 0
                End If

                'dr("AmountPaid") = ""

                'DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'If DRreader.HasRows = True Then
                '    dr("Pending") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(PD_PendingAmount) from Acc_Purchase_Details Where PD_MasterID=" & dt.Rows(i)("Acc_Purchase_ID") & " And PD_CompID=" & iCompID & " ")
                'Else
                '    dr("Pending") = 0
                'End If


                If IsDBNull(dt.Rows(i)("Acc_Purchase_PendingAmount")) = False Then
                    dr("Pending") = dt.Rows(i)("Acc_Purchase_PendingAmount")
                Else
                    dr("Pending") = 0
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAttachFiles(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select pge_Orignalfilename,pge_ext,pge_createdon from EDT_Page Where PGE_FOLDER In (Select FOL_FOLID From EDT_FOLDER Where FOL_Name='" & sTrNo & "') And pge_CompID=" & iCompID & " "
            BindAttachFiles = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindAttachFiles
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBaseID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCabinet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From EDT_Page Where PGE_CABINET=" & iCabinet & " And PGE_SUBCABINET=" & iSubCabinet & " And PGE_Folder=" & iFolder & " And PGE_CompID=" & iCompID & " "
            GetBaseID = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetBaseID
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
    Public Function WriteGLTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasID As Integer, ByVal iyearID As Integer, ByVal iUserID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMaxID As Integer
        Try
            sSql = "" : sSql = "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=1 And ATD_BillID=" & iMasID & " And ATD_YearID=" & iyearID & " And ATD_CompID=" & iCompID & " "
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
    Public Function GetBuyValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iFromID As Integer) As String
        Dim sSql As String = ""
        Dim iValue As String
        Dim sToday As String = ""
        Try
            sToday = objGenFun.GetCurrentDate(sNameSpace)

            sSql = "Select sad_Config_Value from sad_config_settings Where SAD_CompID=" & iCompID & " And sad_Config_Key='Currency'"
            iValue = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            sSql = "SELECT CM_TTBuy FROM MST_Currency_Masters where CM_CompID = " & iCompID & " And CM_Currency=" & iValue & " And CM_OperateOn=" & iFromID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
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
    Public Function GetFERates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer, ByVal dTotal As Double) As String
        Dim sSql As String = "", sToday As String = ""
        Dim dValue As Double = 0, dFETot As Double
        Try
            sToday = objGenFun.GetCurrentDate(sNameSpace)
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
            sToday = objGenFun.GetCurrentDate(sNameSpace)
            sSql = "Select CM_PKID from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sJEID As String, ByVal iCurrency As Integer) As Integer
        Dim sSql As String = "", sIDs As String = ""
        Dim iCount As Integer = 0
        Dim dt As New DataTable, dtTable As New DataTable
        Try
            sSql = "Select Acc_PJE_InvoiceID from Acc_Purchase_JE_Master where Acc_PJE_ID IN(" & sJEID & ") And Acc_PJE_CompID=" & iCompID & ""
            dtTable = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtTable.Rows.Count > 0 Then
                For i = 0 To dtTable.Rows.Count - 1
                    If IsDBNull(dtTable.Rows(i)("Acc_PJE_InvoiceID").ToString()) = False Then
                        sIDs = sIDs & "," & dtTable.Rows(i)("Acc_PJE_InvoiceID").ToString()
                    End If
                Next
            End If
            If sIDs.StartsWith(",") Then
                sIDs = sIDs.Remove(0, 1)
            End If
            sSql = "Select PV_Currency from Purchase_Verification where PV_ID IN (" & sIDs & ") And PV_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("PV_Currency").ToString() = "" Then
                        Return 0
                    Else
                        If dt.Rows(i)("PV_Currency") = iCurrency Then
                            iCount = iCount + 1
                        Else
                            Return 0
                        End If
                    End If
                Next
            End If
            Return 1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFEDiffDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal dValue As Double, ByVal sType As String,
                                          ByVal iHeadID As Integer, ByVal iGLID As Integer, ByVal iSubGLID As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTable As New DataTable
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Try
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("DebitOrCredit")

            sSql = "" : sSql = "Select Acc_Head,Acc_GL,Acc_SubGL from acc_Application_Settings"
            sSql = sSql & " Where Acc_Types='FE' And Acc_LedgerType='" & sType & "' Order by Acc_GL"
            dtTable = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtTable.Rows.Count > 0 Then
                dr = dt.NewRow
                If IsDBNull(dtTable.Rows(0)("Acc_Head").ToString()) = False Then
                    dr("HeadID") = dtTable.Rows(0)("Acc_Head").ToString()
                End If

                If IsDBNull(dtTable.Rows(0)("Acc_GL").ToString()) = False Then
                    dr("GLID") = dtTable.Rows(0)("Acc_GL").ToString()
                End If

                If IsDBNull(dtTable.Rows(0)("Acc_SubGL").ToString()) = False Then
                    dr("SubGLID") = dtTable.Rows(0)("Acc_SubGL").ToString()
                End If

                If sType = "Gain" Then
                    dr("Debit") = 0.00
                    dr("Credit") = dValue
                    dr("DebitOrCredit") = 2
                Else
                    dr("Debit") = dValue
                    dr("Credit") = 0.00
                    dr("DebitOrCredit") = 1
                End If
                dt.Rows.Add(dr)
            End If
            If iHeadID > 0 And iGLID > 0 And iSubGLID > 0 Then
                dr = dt.NewRow
                dr("HeadID") = iHeadID
                dr("GLID") = iGLID
                dr("SubGLID") = iSubGLID
                If sType = "Gain" Then
                    dr("Debit") = dValue
                    dr("Credit") = 0.00
                    dr("DebitOrCredit") = 1
                Else
                    dr("Debit") = 0.00
                    dr("Credit") = dValue
                    dr("DebitOrCredit") = 2
                End If
                dt.Rows.Add(dr)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub UpdateFEKey(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTrans As Integer, ByVal id As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_Transactions_Details set ATD_FEKey=1 Where ATD_CompID=" & iCompID & " And ATD_TrType=" & iTrans & " And ATD_ID = " & id & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeleteFEData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTrans As Integer, ByVal id As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete from  Acc_Transactions_Details Where ATD_CompID=" & iCompID & " And ATD_TrType=" & iTrans & " And ATD_BillId = " & id & " And ATD_FEKey=1"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetFECRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As String
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGenFun.GetCurrentDate(sNameSpace)
            sSql = "Select CM_TTBuy from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
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

    Public Function GetSum(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sJEID As String) As Double
        Dim sSql As String = "", sIDs As String = ""
        Dim ID As Integer = 0
        Dim dtTable As New DataTable
        Try
            sSql = "Select Acc_PJE_InvoiceID from Acc_Purchase_JE_Master where Acc_PJE_ID IN(" & sJEID & ") And Acc_PJE_CompID=" & iCompID & ""
            dtTable = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtTable.Rows.Count > 0 Then
                For i = 0 To dtTable.Rows.Count - 1
                    If IsDBNull(dtTable.Rows(i)("Acc_PJE_InvoiceID").ToString()) = False Then
                        sIDs = sIDs & "," & dtTable.Rows(i)("Acc_PJE_InvoiceID").ToString()
                    End If
                Next
            End If
            If sIDs.StartsWith(",") Then
                sIDs = sIDs.Remove(0, 1)
            End If
            sSql = "Select SUM(PV_FETotalAmt) from Purchase_Verification where PV_ID IN (" & sIDs & ") And PV_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserType(ByVal sNameSpace As String, ByVal iUserId As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select usr_IsSuperuser from sad_userdetails where usr_id=" & iUserId & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub Recall(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sIPAddress As String, ByVal iUserID As Integer, ByVal iyearID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = " Select atd_id,atd_status From Acc_Transactions_Details Where  ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " and atd_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("atd_status") = "A" Then
                        sSql = "Update Acc_Transactions_Details set atd_status='W', ATD_OpenDebit=0, ATD_OpenCredit= 0, ATD_ClosingDebit= 0, ATD_ClosingCredit= 0 , ATD_SeqReferenceNum= 0 where atd_id=" & dt.Rows(i)("ATD_id") & " And ATD_YearID =" & iyearID & " And ATD_CompID=" & iCompID & " "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If

            sSql = " Select Acc_pcm_id,Acc_pcm_status From Acc_PettyCash_Master Where  Acc_pcm_YearID =" & iyearID & " and Acc_pcm_CompID=" & iCompID & " and Acc_pcm_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("Acc_pcm_status") = "A" Then
                        sSql = "Update Acc_PettyCash_Master set Acc_pcm_status='W' where Acc_pcm_id=" & dt.Rows(i)("Acc_pcm_id") & " And Acc_pcm_YearID =" & iyearID & " And Acc_pcm_CompID=" & iCompID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If

            sSql = " Select ACC_PJE_id,ACC_PJE_status From Acc_Purchase_JE_Master Where  ACC_PJE_YearID =" & iyearID & " and ACC_PJE_CompID=" & iCompID & " and ACC_PJE_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("ACC_PJE_status") = "A" Then
                        sSql = "Update Acc_Purchase_JE_Master set ACC_PJE_status='W' where ACC_PJE_id=" & dt.Rows(i)("ACC_PJE_id") & " And ACC_PJE_YearID =" & iyearID & " And ACC_PJE_CompID=" & iCompID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If

            sSql = " Select ACC_SJE_id,ACC_SJE_status From Acc_Sales_JE_Master Where ACC_SJE_YearID =" & iyearID & " and ACC_SJE_CompID=" & iCompID & " and ACC_SJE_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("ACC_SJE_status") = "A" Then
                        sSql = "Update Acc_Sales_JE_Master set ACC_SJE_status='W' where ACC_SJE_id=" & dt.Rows(i)("ACC_SJE_id") & " And ACC_SJE_YearID =" & iyearID & " And ACC_SJE_CompID=" & iCompID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If

            sSql = " Select Acc_Purchase_id,Acc_Purchase_status From Acc_Purchase_Master Where Acc_Purchase_Year =" & iyearID & " and Acc_Purchase_CompID=" & iCompID & " and Acc_Purchase_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("Acc_Purchase_status") = "A" Then
                        sSql = "Update Acc_Purchase_Master set Acc_Purchase_status='W' where Acc_Purchase_id=" & dt.Rows(i)("Acc_Purchase_id") & " And Acc_Purchase_Year =" & iyearID & " And Acc_Purchase_CompID=" & iCompID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If


            sSql = " Select acc_Sales_id,acc_Sales_status From acc_Sales_masters Where acc_Sales_Year =" & iyearID & " and acc_Sales_CompID=" & iCompID & " and acc_Sales_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("acc_Sales_status") = "A" Then
                        sSql = "Update acc_Sales_masters set acc_Sales_status='W' where acc_Sales_id=" & dt.Rows(i)("acc_Sales_id") & " And acc_Sales_Year =" & iyearID & " And acc_Sales_CompID=" & iCompID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If

            sSql = " Select ACC_PM_id,ACC_PM_status From acc_payment_master Where  ACC_PM_YearID =" & iyearID & " and ACC_PM_CompID=" & iCompID & " and ACC_PM_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("ACC_PM_status") = "A" Then
                        sSql = "Update acc_payment_master set ACC_PM_status='W' where ACC_PM_id=" & dt.Rows(i)("ACC_PM_id") & " And ACC_PM_YearID =" & iyearID & " And ACC_PM_CompID=" & iCompID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If

            sSql = " Select ACC_RM_id,ACC_RM_status From acc_Receipt_master Where  ACC_RM_YearID =" & iyearID & " and ACC_RM_CompID=" & iCompID & " and ACC_RM_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("ACC_RM_status") = "A" Then
                        sSql = "Update acc_Receipt_master set ACC_RM_status='W' where ACC_RM_id=" & dt.Rows(i)("ACC_RM_id") & " And ACC_RM_YearID =" & iyearID & " And ACC_RM_CompID=" & iCompID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If

            sSql = " Select ACC_JE_id,ACC_JE_status From acc_je_master Where  ACC_JE_YearID =" & iyearID & " and ACC_JE_CompID=" & iCompID & " and ACC_JE_status='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("ACC_JE_status") = "A" Then
                        sSql = "Update acc_je_master set ACC_JE_status='W' where ACC_JE_id=" & dt.Rows(i)("ACC_JE_id") & " And ACC_JE_YearID =" & iyearID & " And ACC_JE_CompID=" & iCompID & ""
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHeadID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_AccHead = " & iHeadID & " and gl_head=0 "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGrpIdID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_Parent = " & iGrpIdID & " and gl_head=1 "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSubGroup As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 2 and "
            sSql = sSql & "gl_Parent =" & iSubGroup & " and gl_status='A' and gl_Delflag ='C' and gl_CompId =" & iCompID & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGL As Integer, Optional ByVal iglindex As Integer = 0) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 3 and "
            sSql = sSql & "gl_Parent =" & iGL & " and gl_status='A' and gl_Delflag ='C' And gl_CompId =" & iCompID & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class

