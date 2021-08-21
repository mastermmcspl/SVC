Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsRemoteReceipt
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private Acc_RM_ID As Integer
    Private Acc_RM_TransactionNo As String
    Private Acc_RM_Party As Integer
    Private Acc_RM_Location As Integer
    Private Acc_RM_TransactionType As Integer
    Private Acc_RM_BillType As Integer
    Private Acc_RM_BillNo As String
    Private Acc_RM_BillDate As Date
    Private Acc_RM_BillAmount As Decimal
    Private Acc_RM_BillNarration As String
    Private Acc_RM_AdvanceAmount As Decimal
    Private Acc_RM_AdvanceNaration As String
    Private Acc_RM_TDSType As Integer
    Private ACC_RM_TDSDeduct As Decimal
    Private Acc_RM_TDSAmount As Decimal
    Private Acc_RM_TDSNarration As String
    Private Acc_RM_NetAmount As Decimal
    Private Acc_RM_PaymentNarration As String
    Private Acc_RM_ChequeNo As String
    Private Acc_RM_ChequeDate As Date
    Private Acc_RM_IFSCCode As String
    Private Acc_RM_BankName As String
    Private Acc_RM_BranchName As String
    Private Acc_RM_CreatedBy As Integer
    Private Acc_RM_CreatedOn As Date
    Private Acc_RM_ApprovedBy As Integer
    Private Acc_RM_ApprovedOn As Date
    Private Acc_RM_DeletedBy As Integer
    Private Acc_RM_DeletedOn As Date
    Private Acc_RM_RecalledBy As Integer
    Private Acc_RM_RecalledOn As Date
    Private Acc_RM_YearID As Integer
    Private Acc_RM_CompID As Integer
    Private Acc_RM_Status As String
    Private Acc_RM_Operation As String
    Private Acc_RM_IPAddress As String
    Private Acc_RM_BalanceAmount As Decimal
    Private Acc_RM_InvoiceDate As Date
    Private Acc_RM_PaidAmount As Double
    Private Acc_RM_AttachID As Integer
    Private ACC_RM_ZoneID As Integer
    Private ACC_RM_RegionID As Integer
    Private ACC_RM_AreaID As Integer
    Private ACC_RM_BranchID As Integer
    Private Acc_RM_OrderNO As Integer
    Private Acc_RM_OrderDate As Date
    Private Acc_RM_PaymentType As Integer

    Private iAcc_RM_BatchNo As Integer
    Private iAcc_RM_BaseName As Integer

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
    Private ATD_IPAddress As String

    Private ATD_ZoneID As Integer
    Private ATD_RegionID As Integer
    Private ATD_AreaID As Integer
    Private ATD_BranchID As Integer

    Private Acc_RM_FETotalAmt As Double
    Private Acc_RM_Currency As Integer
    Private Acc_RM_DiffAmount As Double
    Private Acc_RM_CurrencyAmt As Double
    Private Acc_RM_CurrencyTime As String
    Public Property dAcc_RM_FETotalAmt() As Double
        Get
            Return (Acc_RM_FETotalAmt)
        End Get
        Set(ByVal Value As Double)
            Acc_RM_FETotalAmt = Value
        End Set
    End Property
    Public Property iAcc_RM_Currency() As Integer
        Get
            Return (Acc_RM_Currency)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_Currency = Value
        End Set
    End Property
    Public Property dAcc_RM_DiffAmount() As Double
        Get
            Return (Acc_RM_DiffAmount)
        End Get
        Set(ByVal Value As Double)
            Acc_RM_DiffAmount = Value
        End Set
    End Property
    Public Property dAcc_RM_CurrencyAmt() As Double
        Get
            Return (Acc_RM_CurrencyAmt)
        End Get
        Set(ByVal Value As Double)
            Acc_RM_CurrencyAmt = Value
        End Set
    End Property
    Public Property sAcc_RM_CurrencyTime() As String
        Get
            Return (Acc_RM_CurrencyTime)
        End Get
        Set(ByVal Value As String)
            Acc_RM_CurrencyTime = Value
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

    Public Property Acc_RM_BatchNo() As Integer
        Get
            Return (iAcc_RM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iAcc_RM_BatchNo = Value
        End Set
    End Property
    Public Property Acc_RM_BaseName() As Integer
        Get
            Return (iAcc_RM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iAcc_RM_BaseName = Value
        End Set
    End Property

    Public Property iAcc_RM_PaymentType() As Integer
        Get
            Return (Acc_RM_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_PaymentType = Value
        End Set
    End Property
    Public Property iAcc_RM_OrderNO() As Integer
        Get
            Return (Acc_RM_OrderNO)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_OrderNO = Value
        End Set
    End Property
    Public Property dAcc_RM_OrderDate() As DateTime
        Get
            Return (Acc_RM_OrderDate)
        End Get
        Set(ByVal Value As DateTime)
            Acc_RM_OrderDate = Value
        End Set
    End Property


    Public Property iACC_RM_ZoneID() As Integer
        Get
            Return (ACC_RM_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            ACC_RM_ZoneID = Value
        End Set
    End Property
    Public Property iACC_RM_RegionID() As Integer
        Get
            Return (ACC_RM_RegionID)
        End Get
        Set(ByVal Value As Integer)
            ACC_RM_RegionID = Value
        End Set
    End Property
    Public Property iACC_RM_AreaID() As Integer
        Get
            Return (ACC_RM_AreaID)
        End Get
        Set(ByVal Value As Integer)
            ACC_RM_AreaID = Value
        End Set
    End Property
    Public Property iACC_RM_BranchID() As Integer
        Get
            Return (ACC_RM_BranchID)
        End Get
        Set(ByVal Value As Integer)
            ACC_RM_BranchID = Value
        End Set
    End Property

    Public Property iAcc_RM_AttachID() As Integer
        Get
            Return (Acc_RM_AttachID)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_AttachID = Value
        End Set
    End Property
    Public Property dAcc_RM_PaidAmount() As Double
        Get
            Return (Acc_RM_PaidAmount)
        End Get
        Set(ByVal Value As Double)
            Acc_RM_PaidAmount = Value
        End Set
    End Property
    Public Property dAcc_RM_InvoiceDate() As Date
        Get
            Return (Acc_RM_InvoiceDate)
        End Get
        Set(ByVal Value As Date)
            Acc_RM_InvoiceDate = Value
        End Set
    End Property
    Public Property sAcc_RM_BillNarration() As String
        Get
            Return (Acc_RM_BillNarration)
        End Get
        Set(ByVal Value As String)
            Acc_RM_BillNarration = Value
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

    Public Property dAcc_RM_BalanceAmount() As Decimal
        Get
            Return (Acc_RM_BalanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_RM_BalanceAmount = Value
        End Set
    End Property


    Public Property sAcc_RM_IPAddress() As String
        Get
            Return (Acc_RM_IPAddress)
        End Get
        Set(ByVal Value As String)
            Acc_RM_IPAddress = Value
        End Set
    End Property

    Public Property sAcc_RM_Operation() As String
        Get
            Return (Acc_RM_Operation)
        End Get
        Set(ByVal Value As String)
            Acc_RM_Operation = Value
        End Set
    End Property
    Public Property sAcc_RM_Status() As String
        Get
            Return (Acc_RM_Status)
        End Get
        Set(ByVal Value As String)
            Acc_RM_Status = Value
        End Set
    End Property

    Public Property iAcc_RM_CompID() As Integer
        Get
            Return (Acc_RM_CompID)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_CompID = Value
        End Set
    End Property

    Public Property iAcc_RM_YearID() As Integer
        Get
            Return (Acc_RM_YearID)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_YearID = Value
        End Set
    End Property
    Public Property dAcc_RM_RecalledOn() As Date
        Get
            Return (Acc_RM_RecalledOn)
        End Get
        Set(ByVal Value As Date)
            Acc_RM_RecalledOn = Value
        End Set
    End Property

    Public Property iAcc_RM_RecalledBy() As Integer
        Get
            Return (Acc_RM_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_RecalledBy = Value
        End Set
    End Property
    Public Property dAcc_RM_DeletedOn() As Date
        Get
            Return (Acc_RM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_RM_DeletedOn = Value
        End Set
    End Property

    Public Property iAcc_RM_DeletedBy() As Integer
        Get
            Return (Acc_RM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_DeletedBy = Value
        End Set
    End Property

    Public Property dAcc_RM_ApprovedOn() As Date
        Get
            Return (Acc_RM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_RM_ApprovedOn = Value
        End Set
    End Property
    Public Property iAcc_RM_ApprovedBy() As Integer
        Get
            Return (Acc_RM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_ApprovedBy = Value
        End Set
    End Property

    Public Property dAcc_RM_CreatedOn() As Date
        Get
            Return (Acc_RM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_RM_CreatedOn = Value
        End Set
    End Property

    Public Property iAcc_RM_CreatedBy() As Integer
        Get
            Return (Acc_RM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_CreatedBy = Value
        End Set
    End Property
    Public Property sAcc_RM_BranchName() As String
        Get
            Return (Acc_RM_BranchName)
        End Get
        Set(ByVal Value As String)
            Acc_RM_BranchName = Value
        End Set
    End Property
    Public Property sAcc_RM_BankName() As String
        Get
            Return (Acc_RM_BankName)
        End Get
        Set(ByVal Value As String)
            Acc_RM_BankName = Value
        End Set
    End Property

    Public Property sAcc_RM_IFSCCode() As String
        Get
            Return (Acc_RM_IFSCCode)
        End Get
        Set(ByVal Value As String)
            Acc_RM_IFSCCode = Value
        End Set
    End Property
    Public Property dAcc_RM_ChequeDate() As Date
        Get
            Return (Acc_RM_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            Acc_RM_ChequeDate = Value
        End Set
    End Property

    Public Property sAcc_RM_ChequeNo() As String
        Get
            Return (Acc_RM_ChequeNo)
        End Get
        Set(ByVal Value As String)
            Acc_RM_ChequeNo = Value
        End Set
    End Property
    Public Property sAcc_RM_PaymentNarration() As String
        Get
            Return (Acc_RM_PaymentNarration)
        End Get
        Set(ByVal Value As String)
            Acc_RM_PaymentNarration = Value
        End Set
    End Property
    Public Property dAcc_RM_NetAmount() As Decimal
        Get
            Return (Acc_RM_NetAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_RM_NetAmount = Value
        End Set
    End Property

    Public Property sAcc_RM_TDSNarration() As String
        Get
            Return (Acc_RM_TDSNarration)
        End Get
        Set(ByVal Value As String)
            Acc_RM_TDSNarration = Value
        End Set
    End Property

    Public Property dAcc_RM_TDSAmount() As Decimal
        Get
            Return (Acc_RM_TDSAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_RM_TDSAmount = Value
        End Set
    End Property
    Public Property dACC_RM_TDSDeduct() As Decimal
        Get
            Return (ACC_RM_TDSDeduct)
        End Get
        Set(ByVal Value As Decimal)
            ACC_RM_TDSDeduct = Value
        End Set
    End Property

    Public Property iAcc_RM_TDSType() As Integer
        Get
            Return (Acc_RM_TDSType)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_TDSType = Value
        End Set
    End Property
    Public Property sAcc_RM_AdvanceNaration() As String
        Get
            Return (Acc_RM_AdvanceNaration)
        End Get
        Set(ByVal Value As String)
            Acc_RM_AdvanceNaration = Value
        End Set
    End Property
    Public Property dAcc_RM_AdvanceAmount() As Decimal
        Get
            Return (Acc_RM_AdvanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_RM_AdvanceAmount = Value
        End Set
    End Property
    Public Property dAcc_RM_BillAmount() As Decimal
        Get
            Return (Acc_RM_BillAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_RM_BillAmount = Value
        End Set
    End Property

    Public Property dAcc_RM_BillDate() As Date
        Get
            Return (Acc_RM_BillDate)
        End Get
        Set(ByVal Value As Date)
            Acc_RM_BillDate = Value
        End Set
    End Property


    Public Property sAcc_RM_BillNo() As String
        Get
            Return (Acc_RM_BillNo)
        End Get
        Set(ByVal Value As String)
            Acc_RM_BillNo = Value
        End Set
    End Property

    Public Property iAcc_RM_BillType() As Integer
        Get
            Return (Acc_RM_BillType)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_BillType = Value
        End Set
    End Property
    Public Property iAcc_RM_Location() As Integer
        Get
            Return (Acc_RM_Location)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_Location = Value
        End Set
    End Property
    Public Property iAcc_RM_TransactionType() As Integer
        Get
            Return (Acc_RM_TransactionType)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_TransactionType = Value
        End Set
    End Property

    Public Property iAcc_RM_Party() As Integer
        Get
            Return (Acc_RM_Party)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_Party = Value
        End Set
    End Property

    Public Property sAcc_RM_TransactionNo() As String
        Get
            Return (Acc_RM_TransactionNo)
        End Get
        Set(ByVal Value As String)
            Acc_RM_TransactionNo = Value
        End Set
    End Property

    Public Property iAcc_RM_ID() As Integer
        Get
            Return (Acc_RM_ID)
        End Get
        Set(ByVal Value As Integer)
            Acc_RM_ID = Value
        End Set
    End Property
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

    Public Function LoadReceiptDashboard(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iStatus As Integer) As DataTable
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


            sSql = "" : sSql = "Select * from Acc_Receipt_Master where Acc_RM_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Acc_RM_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_RM_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_RM_Status='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By Acc_RM_ID Desc"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_RM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_RM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_RM_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_RM_TransactionNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_RM_BillNo").ToString()) = False Then
                        dr("BillNo") = ds.Tables(0).Rows(i)("Acc_RM_BillNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_RM_BillDate").ToString()) = False Then
                        dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_RM_BillDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_RM_BillType").ToString()) = False Then
                        dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_RM_BillType").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_RM_Party").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("Acc_RM_Party") > 0 Then
                            dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_RM_Location").ToString(), ds.Tables(0).Rows(i)("Acc_RM_Party").ToString())
                        Else
                            dr("Party") = ""
                        End If
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_RM_Status") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_RM_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_RM_Status") = "D") Then
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

    Public Sub UpdateReceiptStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_Receipt_Master Set Acc_RM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_RM_Status='A',Acc_RM_ApprovedBy= " & iUserID & ",Acc_RM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_RM_Status='D',Acc_RM_DeletedBy= " & iUserID & ",Acc_RM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_RM_Status='A' "
            End If
            sSql = sSql & " Where Acc_RM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
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
            sSQL = "" : sSQL = "Select * from ACC_General_Master where mas_master = 25 and mas_Delflag ='A' and Mas_ID = " & iBillType & " and mas_CompID =" & iCompID & ""
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

    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_RM_ID)+1,1) from Acc_Receipt_Master")

            sSql = "" : sSql = "Select * from ACC_Voucher_Settings where AVS_TransType = 3  and AVS_CompID = " & iCompID & ""
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

    Public Function LoadExistingVoucherNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iParty = 0 Then
                sSql = "Select Acc_RM_TransactionNo,Acc_RM_ID from  Acc_Receipt_Master where Acc_RM_CompID=" & iCompID & " and Acc_RM_YearID=" & iYearID & " order by Acc_RM_ID Desc"
            Else
                sSql = "Select Acc_RM_TransactionNo,Acc_RM_ID from  Acc_Receipt_Master where Acc_RM_CompID=" & iCompID & " and Acc_RM_YearID=" & iYearID & " and Acc_RM_Party = " & iParty & " order by Acc_RM_ID Desc"
            End If
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

    Public Function LoadBankNames(sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = "", aSql As String = ""
        Dim dt As New DataTable
        Dim GLid As Integer = 0
        Try
            'sSql = "Select Acc_Gl from acc_application_settings where Acc_Types='Bank' and Acc_LedgerType='Bank' and Acc_CompID=" & iCompID & ""
            'GLid = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            'If GLid > 0 Then
            '    aSql = " Select gl_Id, GL_Desc From chart_of_accounts Where gl_parent = " & GLid & " order by gl_id"
            'End If
            aSql = "Select Mas_ID,Mas_Desc From Acc_General_Master Where Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Bank Name' And Mas_Delflag='X') And Mas_DelFlag='A'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, aSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DataFromNextPurchaseInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String, ByVal iPartyID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Acc_Sales_Masters Where Acc_Sales_ID Not in (" & sBillNo.Remove(0, 1) & ") And Acc_Sales_Party=" & iPartyID & " And Acc_Sales_PaymentStatus = 'N' And Acc_Sales_CompID =" & iCompID & " Order By Acc_Sales_ID "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
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
    Public Function LoadBIllType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc from ACC_General_Master where mas_master = 25 and mas_Delflag ='A' and mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveReceiptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objRecp As ClsRemoteReceipt)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(41) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_TransactionNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_TransactionType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_TransactionType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_BillType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BillNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BillDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BillAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_BillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BalanceAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_BalanceAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_ChequeNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_ChequeDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_IFSCCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BankName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BranchName", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_BranchName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_CreatedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BillNarration", OleDb.OleDbType.VarChar, 7999)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_BillNarration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_InvoiceDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_InvoiceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_PaidAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_PaidAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_RM_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objRecp.iACC_RM_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_RM_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objRecp.iACC_RM_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_RM_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objRecp.iACC_RM_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_RM_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objRecp.iACC_RM_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_OrderNO", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_OrderNO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_OrderDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_FETotalAmt", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_FETotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_Currency", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iAcc_RM_Currency
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_DiffAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_DiffAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_CurrencyAmt", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objRecp.dAcc_RM_CurrencyAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_RM_CurrencyTime", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objRecp.sAcc_RM_CurrencyTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Receipt_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteTransactionsDetails1(ByVal sNameSpace As String, ByVal iCompid As Integer, ByVal iTrans As Integer, ByVal iBillID As Integer)
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

    Public Function SaveTransactionsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objRecp As ClsRemoteReceipt)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objRecp.dATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objRecp.dATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objRecp.dATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRecp.iATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objRecp.sATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objRecp.iATD_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objRecp.iATD_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objRecp.iATD_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objRecp.iATD_BranchID
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

    Public Function GetReceiptDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iReceiptID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iBatchNo > 0 And iBaseName > 0 Then
                sSql = "" : sSql = "Select * from Acc_Receipt_Master where Acc_RM_BatchNo =" & iBatchNo & " And Acc_RM_BaseName=" & iBaseName & " and Acc_RM_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Else
                sSql = "" : sSql = "Select * from Acc_Receipt_Master where Acc_RM_ID =" & iReceiptID & " and Acc_RM_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function DeleteReceiptDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete from Acc_Transactions_Details where ATD_ID = " & iTransactionID & " And Atd_CompID = " & iCompID & ""
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

    'Public Function UpdateReceiptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal ObjReceipt As clsReceipt)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from Acc_Receipt_Master where Acc_RM_ID =" & ObjReceipt.iAcc_RM_ID & " and Acc_RM_CompID =" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        If dt.Rows.Count > 0 Then
    '            sSql = "" : sSql = "Update Acc_Receipt_Master set Acc_RM_Party = " & ObjReceipt.iAcc_RM_Party & ",Acc_RM_Location=" & ObjReceipt.iAcc_RM_Location & ","
    '            sSql = sSql & "Acc_RM_TransactionType =" & ObjReceipt.iAcc_RM_TransactionType & ","
    '            sSql = sSql & "Acc_RM_BillType = " & ObjReceipt.iAcc_RM_BillType & ",Acc_RM_BillNo = '" & objGen.SafeSQL(ObjReceipt.sAcc_RM_BillNo) & "',"
    '            sSql = sSql & "Acc_RM_BillDate = " & objGen.FormatDtForRDBMS(ObjReceipt.dAcc_RM_BillDate, "I") & ",Acc_RM_BillAmount = " & ObjReceipt.dAcc_RM_BillAmount & ","
    '            sSql = sSql & "Acc_RM_BillNarration = '" & objGen.SafeSQL(ObjReceipt.sAcc_RM_BillNarration) & "' "

    '            If iPaymentType = 1 Then
    '                sSql = sSql & ",Acc_RM_AdvanceAmount = " & ObjReceipt.dAcc_RM_AdvanceAmount & ",Acc_RM_AdvanceNaration = '" & objGen.SafeSQL(ObjReceipt.sAcc_RM_AdvanceNaration) & "',Acc_RM_BalanceAmount = " & ObjReceipt.dAcc_RM_BalanceAmount & " "
    '            ElseIf iPaymentType = 2 Then
    '                sSql = sSql & ",Acc_RM_TDSType = " & ObjReceipt.iAcc_RM_TDSType & ",ACC_RM_TDSDeduct=" & ObjReceipt.dACC_RM_TDSDeduct & ",Acc_RM_TDSAmount=" & ObjReceipt.dAcc_RM_TDSAmount & ","
    '                sSql = sSql & "Acc_RM_TDSNarration = '" & objGen.SafeSQL(ObjReceipt.sAcc_RM_TDSNarration) & "' "
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & ",Acc_RM_NetAmount = " & ObjReceipt.dAcc_RM_NetAmount & ",Acc_RM_PaymentNarration = '" & ObjReceipt.sAcc_RM_PaymentNarration & "' "
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & ",Acc_RM_ChequeNo = " & ObjReceipt.sAcc_RM_ChequeNo & ","
    '                sSql = sSql & "Acc_RM_ChequeDate = " & objGen.FormatDtForRDBMS(ObjReceipt.dAcc_RM_ChequeDate, "I") & ",Acc_RM_IFSCCode = '" & ObjReceipt.sAcc_RM_IFSCCode & "',"
    '                sSql = sSql & "Acc_RM_BankName = '" & objGen.SafeSQL(ObjReceipt.sAcc_RM_BankName) & "',Acc_RM_BranchName = '" & objGen.SafeSQL(ObjReceipt.sAcc_RM_BranchName) & "' "
    '            End If
    '            sSql = sSql & "Where Acc_RM_ID = " & ObjReceipt.iAcc_RM_ID & " and Acc_RM_CompID =" & iCompID & ""
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return ObjReceipt.iAcc_RM_ID
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetTransactionsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransType As Integer, ByVal iTransactionID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Acc_Transactions_Details where ATD_ID =" & iTransactionID & " and ATD_TrTYpe =" & iTransType & " and ATD_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
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
                If iPaymentType = 1 Or iPaymentType = 4 Then
                    'sSql = "Select * from Acc_Sales_JE_Master where Acc_SJE_Party = " & iParty & " And Acc_SJE_PendingAmount > 0 and Acc_SJE_Status ='A' and Acc_SJE_CompID= " & iCompID & ""
                    sSql = "Select * from Acc_Sales_JE_Master where Acc_SJE_Party = " & iParty & " And Acc_SJE_Status ='A' and Acc_SJE_CompID= " & iCompID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                    If sBillNo <> "" Then
                        For i = 0 To dt.Rows.Count - 1
                            If sBillNo.Contains(dt.Rows(i)("Acc_SJE_BillNo").ToString()) = False Then
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = dt.Rows(i)("Acc_SJE_ID").ToString()
                                dRow("BillNo") = "SI" & "-" & dt.Rows(i)("Acc_SJE_BillNo").ToString()
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
                            If sBillNo.Contains(dt.Rows(i)("Acc_SJE_BillNo").ToString()) = False Then
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = dt.Rows(i)("Acc_SJE_ID").ToString()
                                dRow("BillNo") = "SI" & "-" & dt.Rows(i)("Acc_SJE_BillNo").ToString()
                                dtFinal.Rows.Add(dRow)
                            End If
                        Next
                    End If

                ElseIf iPaymentType = 2 Or iPaymentType = 5 Then
                    'sSql = "Select * from Acc_Sales_Masters where Acc_Sales_Party = " & iParty & " And Acc_Sales_PendingAmount > 0 and Acc_Sales_Status ='A' and Acc_Sales_CompID= " & iCompID & ""
                    sSql = "Select * from Acc_Sales_Masters where Acc_Sales_Party = " & iParty & " And Acc_Sales_Status ='A' and Acc_Sales_CompID= " & iCompID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                    If sBillNo <> "" Then
                        For i = 0 To dt.Rows.Count - 1
                            If sBillNo.Contains(dt.Rows(i)("Acc_Sales_BillNo").ToString()) = False Then
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = dt.Rows(i)("Acc_Sales_ID").ToString()
                                dRow("BillNo") = "S" & "-" & dt.Rows(i)("Acc_Sales_BillNo").ToString()
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
                            If sBillNo.Contains(dt.Rows(i)("Acc_Sales_BillNo").ToString()) = False Then
                                dRow = dtFinal.NewRow()
                                dRow("BillID") = dt.Rows(i)("Acc_Sales_ID").ToString()
                                dRow("BillNo") = "S" & "-" & dt.Rows(i)("Acc_Sales_BillNo").ToString()
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


                'sSql = "Select * from Acc_Sales_JE_Master where Acc_SJE_Party = " & iParty & " and Acc_SJE_Status ='A' and Acc_SJE_CompID= " & iCompID & ""
                'dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                'sSql = "" : sSql = "select * from Acc_Receipt_Master where acc_rm_id in(Select distinct(ATD_BillId) from Acc_Transactions_Details where ATD_TrType = 3)"
                'dtCheck = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                'If dtCheck.Rows.Count > 0 Then
                '    For i = 0 To dtCheck.Rows.Count - 1
                '        sBillNo = sBillNo & "," & dtCheck.Rows(i)("Acc_RM_BillNo").ToString()
                '    Next
                'End If

                'Without Inventory

                'sSql = "Select * from acc_Sales_masters where Acc_Sales_Party = " & iParty & " and Acc_Sales_Status ='A' and Acc_Sales_CompID= " & iCompID & ""
                'dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

                'sSql = "" : sSql = "select * from Acc_Receipt_Master where acc_rm_id in(Select distinct(ATD_BillId) from Acc_Transactions_Details where ATD_TrType = 3)"
                'dtCheck = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                'If dtCheck.Rows.Count > 0 Then
                '    For i = 0 To dtCheck.Rows.Count - 1
                '        sBillNo = sBillNo & "," & dtCheck.Rows(i)("Acc_RM_BillNo").ToString()
                '    Next
                'End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBillAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sBillNo As String, ByVal sBillName As String) As String
        Dim sSql As String = ""
        Dim sArray As Array
        Try
            sBillName = sBillName.Remove(0, 1)
            sArray = sBillName.Split("-")
            If sArray.Length > 0 Then
                If sArray(0) = "SI" Then
                    sSql = "" : sSql = "Select sum(Acc_SJE_BillAmount) from  Acc_Sales_JE_Master where Acc_SJE_ID in (" & sBillNo.Remove(0, 1) & ") and "
                    sSql = sSql & "Acc_SJE_CompID =" & iCompID & ""
                ElseIf sArray(0) = "S" Then
                    sSql = "" : sSql = "Select sum(Acc_Sales_BillAmount) from  acc_Sales_masters where Acc_Sales_ID in (" & sBillNo.Remove(0, 1) & ") and "
                    sSql = sSql & "Acc_Sales_CompID =" & iCompID & ""
                End If
            End If
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
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
            dc = New DataColumn("Id", GetType(String))
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
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReceipts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer, ByVal dAmount As Double, ByVal iDbOrCr As Integer, ByVal dtReceipt As DataTable, ByVal dtCOA As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt = BuildTable()

            dr = dt.NewRow
            dr("HeadID") = iHead
            dr("GLID") = iGLID
            dr("SubGLID") = iSubGL

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
            iCount = dtReceipt.Rows.Count + 1

            If iDbOrCr = 1 Then
                dr("ID") = iCount
                dr("OpeningBalance") = 0
                dr("Debit") = dAmount
                dr("Credit") = 0.00
                dr("DebitOrCredit") = 1
            Else
                dr("ID") = iCount
                dr("OpeningBalance") = 0
                dr("Debit") = 0.00
                dr("Credit") = dAmount
                dr("DebitOrCredit") = 2
            End If

            dt.Rows.Add(dr)
            If dtReceipt.Rows.Count > 0 Then
                dtReceipt.Merge(dt, True, MissingSchemaAction.Ignore)
            Else
                dtReceipt.Merge(dt)
            End If
            'dtReceipt.Merge(dt)
            Return dtReceipt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetchartofAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CompID=" & iCompID & " And gl_DelFlag ='C'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iReceipt As Integer) As DataTable
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

            sSql = "" : sSql = "select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,"
                sSql = sSql & "A.ATD_Credit,A.ATD_DbOrCr,B.gl_glCode as GlCode, B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
                sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on "
                sSql = sSql & "A.ATD_BillId = " & iReceipt & " and A.ATD_TrType = 3  and A.ATD_GL = B.gl_ID  Left join chart_of_Accounts C on "
                sSql = sSql & "A.ATD_SubGL = C.gl_id order by a.Atd_id "

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

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

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_DbOrCr").ToString()) = False Then
                        dr("DebitOrCredit") = ds.Tables(0).Rows(i)("ATD_DbOrCr").ToString()
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
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

    Public Function CheckBillForParty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer, ByVal iPaymentType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select * from Acc_Sales_Masters Where Acc_Sales_Party=" & iPartyID & " and Acc_Sales_Status ='A' And Acc_Sales_PaymentStatus='N' And Acc_Sales_CompID=" & iCompID & ""
            'dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If iPaymentType = 1 Or iPaymentType = 4 Then    'With Inventory Advance & With Inventory Payment
                'sSql = "Select * from Acc_Sales_JE_Master Where Acc_SJE_Party=" & iPartyID & " and Acc_SJE_Status ='A' And Acc_SJE_PendingAmount > 0 And Acc_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " "
                sSql = "Select * from Acc_Sales_JE_Master Where Acc_SJE_Party=" & iPartyID & " and Acc_SJE_Status ='A' And Acc_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            ElseIf iPaymentType = 2 Or iPaymentType = 5 Then    'Without Inventory Advance & Without Inventory Payment
                'sSql = "Select * from Acc_Sales_Masters Where Acc_Sales_Party=" & iPartyID & " and Acc_Sales_Status ='A' And Acc_Sales_PendingAmount > 0 And Acc_Sales_CompID=" & iCompID & " And Acc_Sales_Year=" & iYearID & " "
                sSql = "Select * from Acc_Sales_Masters Where Acc_Sales_Party=" & iPartyID & " and Acc_Sales_Status ='A' And Acc_Sales_CompID=" & iCompID & " And Acc_Sales_Year=" & iYearID & " "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim DRreader As OleDb.OleDbDataReader
        Try
            dtTab.Columns.Add("SMID")
            dtTab.Columns.Add("VoucherNO")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("BillAmount")
            'dtTab.Columns.Add("AmountPaid")
            dtTab.Columns.Add("Pending")

            sSql = "Select * from Acc_Sales_Masters Where Acc_Sales_Party=" & iPartyID & " and Acc_Sales_Status ='A' And Acc_Sales_PaymentStatus='N' And Acc_Sales_CompID=" & iCompID & " Order By Acc_Sales_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("SMID") = dt.Rows(i)("Acc_Sales_ID")

                If (dt.Rows(i)("Acc_Sales_TransactionNo").ToString() = "") Then
                    dr("VoucherNO") = ""
                Else
                    dr("VoucherNO") = dt.Rows(i)("Acc_Sales_TransactionNo")
                End If

                If (dt.Rows(i)("Acc_Sales_BillNo").ToString() = "") Then
                    dr("BillNo") = ""
                Else
                    dr("BillNo") = "S" & "-" & dt.Rows(i)("Acc_Sales_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Sales_BillDate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Sales_BillDate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Sales_BillDate"), "D")
                End If

                DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Sales_Masters_Details Where Acc_SMD_MasterID=" & dt.Rows(i)("Acc_Sales_ID") & " And Acc_SMD_CompID=" & iCompID & " ")
                If DRreader.HasRows = True Then
                    dr("BillAmount") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(Acc_SMD_TotalAmount) from Acc_Sales_Masters_Details Where Acc_SMD_MasterID=" & dt.Rows(i)("Acc_Sales_ID") & " And Acc_SMD_CompID=" & iCompID & " ")
                Else
                    dr("BillAmount") = 0
                End If

                'dr("AmountPaid") = ""

                DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Sales_Masters_Details Where Acc_SMD_MasterID=" & dt.Rows(i)("Acc_Sales_ID") & " And Acc_SMD_CompID=" & iCompID & " ")
                If DRreader.HasRows = True Then
                    dr("Pending") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(Acc_SMD_PendingAmount) from Acc_Sales_Masters_Details Where Acc_SMD_MasterID=" & dt.Rows(i)("Acc_Sales_ID") & " And Acc_SMD_CompID=" & iCompID & " ")
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
    Public Function GetDerivedBillAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sBillNo As String, ByVal sBillName As String) As String
        Dim sSql As String = ""
        Dim sArray As Array
        Try
            sBillName = sBillName.Remove(0, 1)
            sArray = sBillName.Split("-")
            If sArray.Length > 0 Then
                If sArray(0) = "SI" Then
                    sSql = "" : sSql = "Select sum(Acc_SJE_BillAmount) from  Acc_Sales_JE_Master where Acc_SJE_ID in (" & sBillNo.Remove(0, 1) & ") and "
                    sSql = sSql & "Acc_SJE_CompID =" & iCompID & ""
                ElseIf sArray(0) = "S" Then
                    sSql = "" : sSql = "Select SUM(Acc_SMD_PendingAmount) from Acc_Sales_Masters_Details Where Acc_SMD_MasterID in (" & sBillNo.Remove(0, 1) & ") And Acc_SMD_CompID =" & iCompID & " "
                End If
            End If
            Return objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DataFromPurchase(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Acc_Sales_Masters Where Acc_Sales_ID in (" & sBillNo.Remove(0, 1) & ") And Acc_Sales_CompID =" & iCompID & " Order By Acc_Sales_ID "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DataFromPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPurchaseID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Acc_Sales_Masters_Details Where Acc_SMD_MasterID in (" & iPurchaseID & ") And Acc_SMD_CompID =" & iCompID & " Order By Acc_SMD_ID "
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
            sSql = "Update Acc_Sales_Masters_Details set Acc_SMD_PendingAmount=" & dUpdateAmount & " Where Acc_SMD_CompID=" & iCompID & " And Acc_SMD_ID = " & id & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            'If PendingAmount is zero update Purchase master status as 'N' '
            sSql = "" : sSql = "Select Sum(Acc_SMD_PendingAmount) from Acc_Sales_Masters_Details Where Acc_SMD_MasterID in (" & iPurchaseID & ") And Acc_SMD_CompID =" & iCompID & " "
            dTptaolPendingAmt = objDBL.SQLGetDescription(sNameSpace, sSql)
            If dTptaolPendingAmt = 0 Then
                sSql = "Update Acc_Sales_Masters set Acc_Sales_PaymentStatus='P' Where Acc_Sales_ID in (" & iPurchaseID & ") And Acc_Sales_CompID =" & iCompID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
            'If PendingAmount is zero update Purchase master status as 'N' '
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

            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_TrType=3 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
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
                        sSql = sSql & " Where A.ATD_TrType=3 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
                        dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Else
                        sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                        sSql = sSql & " From Acc_Transactions_Details A"
                        sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
                        sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
                        sSql = sSql & " Where A.ATD_TrType=3 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
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
    Public Function BindManualBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim DRreader As OleDb.OleDbDataReader
        Try
            dtTab.Columns.Add("SMID")
            dtTab.Columns.Add("VoucherNO")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("BillAmount")
            dtTab.Columns.Add("AmountPaid")
            dtTab.Columns.Add("Pending")

            sSql = "Select * from Acc_Sales_Masters Where Acc_Sales_Party=" & iPartyID & " and Acc_Sales_Status ='A' And Acc_Sales_PaymentStatus='N' And Acc_Sales_CompID=" & iCompID & " Order By Acc_Sales_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("SMID") = dt.Rows(i)("Acc_Sales_ID")

                If (dt.Rows(i)("Acc_Sales_TransactionNo").ToString() = "") Then
                    dr("VoucherNO") = ""
                Else
                    dr("VoucherNO") = dt.Rows(i)("Acc_Sales_TransactionNo")
                End If

                If (dt.Rows(i)("Acc_Sales_BillNo").ToString() = "") Then
                    dr("BillNo") = ""
                Else
                    dr("BillNo") = "S" & "-" & dt.Rows(i)("Acc_Sales_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Sales_BillDate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Sales_BillDate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("Acc_Sales_BillDate"), "D")
                End If

                DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Sales_Masters_Details Where Acc_SMD_MasterID=" & dt.Rows(i)("Acc_Sales_ID") & " And Acc_SMD_CompID=" & iCompID & " ")
                If DRreader.HasRows = True Then
                    dr("BillAmount") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(Acc_SMD_TotalAmount) from Acc_Sales_Masters_Details Where Acc_SMD_MasterID=" & dt.Rows(i)("Acc_Sales_ID") & " And Acc_SMD_CompID=" & iCompID & " ")
                Else
                    dr("BillAmount") = 0
                End If

                dr("AmountPaid") = ""

                DRreader = objDBL.SQLDataReader(sNameSpace, "Select * from Acc_Sales_Masters_Details Where Acc_SMD_MasterID=" & dt.Rows(i)("Acc_Sales_ID") & " And Acc_SMD_CompID=" & iCompID & " ")
                If DRreader.HasRows = True Then
                    dr("Pending") = objDBL.SQLGetDescription(sNameSpace, "Select SUM(Acc_SMD_PendingAmount) from Acc_Sales_Masters_Details Where Acc_SMD_MasterID=" & dt.Rows(i)("Acc_Sales_ID") & " And Acc_SMD_CompID=" & iCompID & " ")
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
    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_ID,SPO_OrderCode from Sales_Proforma_Order where SPO_PartyName=" & iParty & " And SPO_CompID=" & iCompID & " and SPO_YearID =" & iYearID & ""
            sSql = sSql & " and SPO_Status='A' and SPO_OrderType='S' Order By SPO_ID desc "
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Sales_Proforma_Order where SPO_CompID=" & iCompID & " and SPO_YearID=" & iYearID & " and SPO_ID=" & iID & " And SPO_OrderType='S'"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderNo(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String) As Integer
        Dim sSql As String = ""
        Try

            sSql = "Select SDM_OrderID From Sales_Dispatch_Master Where SDM_ID in (Select Acc_SJE_InvoiceID from Acc_Sales_JE_Master Where Acc_SJE_ID=" & sBillNo.Remove(0, 1) & " And Acc_SJE_YearID=" & iYearID & " And Acc_SJE_CompID =" & iCompID & ") "
            GetOrderNo = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetOrderNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPaidAmount(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillName As String) As Double
        Dim sSql As String = ""
        Try
            sSql = "Select Sum(Acc_RM_PaidAmount) From ACc_Receipt_Master where ACc_RM_BillNo='" & (sBillName.Remove(0, 1)) & "' And Acc_RM_YearID=" & iYearID & " And Acc_RM_CompID=" & iCompID & " "
            GetPaidAmount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPaidAmount
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
    Public Function LoadExistingTrnNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Acc_RM_ID,Acc_RM_TransactionNo from Acc_Receipt_Master where Acc_RM_ID=" & sTrNo & " and Acc_RM_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBillId(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim iID As Integer
        Try
            sSql = "select ATD_BillId from Acc_Transactions_Details where ATD_ID=" & iMasId & ""
            iID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim iID As String
        Try
            sSql = "select Acc_RM_Status from Acc_Receipt_Master where Acc_RM_ID=" & iMasId & ""
            iID = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return iID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub UpdateReceiptStatus1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Update Acc_Receipt_Master Set Acc_RM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_RM_Status='A',Acc_RM_ApprovedBy= " & iUserID & ",Acc_RM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_RM_Status='D',Acc_RM_DeletedBy= " & iUserID & ",Acc_RM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_RM_Status='A' "
            End If
            sSql = sSql & " Where Acc_RM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=3 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "" : sSql = "Update Acc_Transactions_Details Set ATD_IPAddress='" & sIPAddress & "' "
                    If sStatus = "D" Then
                        sSql = sSql & " ,ATD_Status='D',ATD_DeletedBy= " & iUserID & ",ATD_DeletedOn=GetDate() "
                    ElseIf sStatus = "A" Then
                        sSql = sSql & " ,ATD_Status='A',ATD_ApprovedBy=" & iUserID & ",ATD_ApprovedOn=GetDate() "
                    End If
                    sSql = sSql & " Where ATD_ID = " & dt.Rows(i)("ATD_ID") & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadPaymentsMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer, ByVal dAmount As Double, ByVal iDbOrCr As Integer, ByVal dtPayment As DataTable, ByVal dtCOA As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt = BuildTable1()

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
    Public Function BuildTable1() As DataTable
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
End Class
