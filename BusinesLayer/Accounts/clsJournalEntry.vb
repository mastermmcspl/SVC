﻿Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsJournalEntry
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

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
    Private Acc_JE_JEType As Integer

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

    Private ATD_ZoneID As Integer
    Private ATD_RegionID As Integer
    Private ATD_AreaID As Integer
    Private ATD_BranchID As Integer


    Private ATD_OpenDebit As Decimal
    Private ATD_OpenCredit As Decimal
    Private ATD_ClosingDebit As Decimal
    Private ATD_ClosingCredit As Decimal
    Private ATD_SeqReferenceNum As Integer
    Public Structure JE
        Dim iJE_ID As Integer
        Dim iJE_MasterID As Integer
        Dim sJE_BillNo As String
        Dim dJE_BillDate As Date
        Dim dJE_BillAmount As Double
        Dim sJE_Status As String
        Dim iJE_CreatedBy As Integer
        Dim dJE_CreatedOn As Date
        Dim iJE_CompID As Integer
        Dim iJE_YearID As Integer
        Dim sJE_Operation As String
        Dim sJE_IPAddress As String
    End Structure

    Private Acc_JE_BatchNo As Integer
    Private Acc_JE_BaseName As Integer
    Public Property iAcc_JE_BatchNo() As Integer
        Get
            Return (Acc_JE_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_BatchNo = Value
        End Set
    End Property
    Public Property iAcc_JE_BaseName() As Integer
        Get
            Return (Acc_JE_BaseName)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_BaseName = Value
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
    Public Property iAcc_JE_JEType() As Integer
        Get
            Return (Acc_JE_JEType)
        End Get
        Set(ByVal Value As Integer)
            Acc_JE_JEType = Value
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

    Public Function LoadJournalEntry(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iStatus As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Dim dtBillDetails As New DataTable : Dim sBillNo As String = "" : Dim sBillDate As String = "" : Dim sBillAmt As String = ""
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransactionNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillAmount", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Party", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TrAmt", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Acc_JE_Master where Acc_JE_YearID=" & iYearID & " And Acc_JE_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Acc_JE_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_JE_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_JE_Status='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By Acc_JE_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_JE_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_JE_TransactionNo").ToString()
                    End If

                    'If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillNo").ToString()) = False Then
                    '    dr("BillNo") = ds.Tables(0).Rows(i)("Acc_JE_BillNo").ToString()
                    'End If

                    'If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_InvoiceDate").ToString()) = False Then
                    '    dr("BillDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("Acc_JE_InvoiceDate").ToString(), "D")
                    'End If

                    dtBillDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * from Acc_JE_Master_Details Where AJE_MasterID=" & ds.Tables(0).Rows(i)("Acc_JE_ID") & " And AJE_CompID=" & iCompID & " And AJE_yearID=" & iYearID & " ").Tables(0)
                    If dtBillDetails.Rows.Count > 0 Then
                        For j = 0 To dtBillDetails.Rows.Count - 1
                            sBillNo = sBillNo & "," & dtBillDetails.Rows(j)("AJE_BillNo")
                            sBillDate = sBillDate & "," & dtBillDetails.Rows(j)("AJE_BillDate")
                            sBillAmt = sBillAmt & "," & dtBillDetails.Rows(j)("AJE_BillAmount")
                        Next
                        If sBillNo.StartsWith(",") Then
                            sBillNo = sBillNo.Remove(0, 1)
                        End If
                        If sBillNo.EndsWith(",") Then
                            sBillNo = sBillNo.Remove(Len(sBillNo) - 1, 1)
                        End If

                        If sBillDate.StartsWith(",") Then
                            sBillDate = sBillDate.Remove(0, 1)
                        End If
                        If sBillDate.EndsWith(",") Then
                            sBillDate = sBillDate.Remove(Len(sBillNo) - 1, 1)
                        End If

                        If sBillAmt.StartsWith(",") Then
                            sBillAmt = sBillAmt.Remove(0, 1)
                        End If
                        If sBillAmt.EndsWith(",") Then
                            sBillAmt = sBillAmt.Remove(Len(sBillNo) - 1, 1)
                        End If
                    End If

                    If sBillNo <> "" Then
                        dr("BillNo") = sBillNo
                    Else
                        dr("BillNo") = ""
                    End If
                    If sBillDate <> "" Then
                        dr("BillDate") = sBillDate
                    Else
                        dr("BillDate") = ""
                    End If
                    If sBillAmt <> "" Then
                        dr("BillAmount") = sBillAmt
                    Else
                        dr("BillAmount") = ""
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillType").ToString()) = False Then
                        dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_JE_BillType").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_Party").ToString()) = False Then
                        dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_JE_Location").ToString(), ds.Tables(0).Rows(i)("Acc_JE_Party").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_JE_BillAmount").ToString()) = False Then
                        dr("TrAmt") = ds.Tables(0).Rows(i)("Acc_JE_BillAmount").ToString()
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_JE_Status") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_JE_Status") = "D") Then
                        dr("Status") = "De-Activated"
                    End If
                    sBillNo = "" : sBillDate = "" : sBillAmt = ""
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Sub UpdateJEMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Update Acc_JE_Master Set Acc_JE_IPAddress='" & sIPAddress & "',"
    '        If sStatus = "W" Then
    '            sSql = sSql & " Acc_JE_Status='A',Acc_JE_ApprovedBy= " & iUserID & ",Acc_JE_ApprovedOn=GetDate()"
    '        ElseIf sStatus = "D" Then
    '            sSql = sSql & " Acc_JE_Status='D',Acc_JE_DeletedBy= " & iUserID & ",Acc_JE_DeletedOn=GetDate()"
    '        ElseIf sStatus = "A" Then
    '            sSql = sSql & " Acc_JE_Status='A' "
    '        End If
    '        sSql = sSql & " Where Acc_JE_ID = " & iMasId & ""
    '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=4 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
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


    Public Sub UpdateJEMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
        Dim sSql As String = ""
        Dim dt, dtDebitCredit As New DataTable
        Dim dOpnDebit, dOpnCredit, dClosingDebit, dClosingCredit As Double
        Dim iSequenceNum As Integer
        Try
            sSql = "Update Acc_JE_Master Set Acc_JE_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_JE_Status='A',Acc_JE_ApprovedBy= " & iUserID & ",Acc_JE_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_JE_Status='D',Acc_JE_DeletedBy= " & iUserID & ",Acc_JE_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_JE_Status='A' "
            End If
            sSql = sSql & " Where Acc_JE_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=4 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
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

    Public Function GetPartyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCSGLid As Integer, ByVal iParty As Integer) As String
        Dim sSQL As String = ""
        Dim sParty As String = ""
        Dim dt As New DataTable
        Try
            If iCSGLid = 1 Then
                sSQL = "" : sSQL = "Select BM_Name from sales_Buyers_Masters where BM_Id=" & iParty & " and BM_DelFlag='A' and BM_CompID =" & iCompID & ""
            ElseIf iCSGLid = 2 Then
                sSQL = "" : sSQL = " Select CSM_Name from CustomerSupplierMaster where CSM_Id=" & iParty & " and CSM_DelFlag='A' and CSM_CompID =" & iCompID & ""
            ElseIf iCSGLid = 3 Then
                sSQL = "" : sSQL = "Select gl_desc FROM chart_of_accounts where gl_id=" & iParty & " and "
                sSQL = sSQL & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A'"
            End If

            sParty = objDBL.SQLExecuteScalar(sNameSpace, sSQL)

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

    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(Count(Acc_JE_ID)+1,1) from Acc_JE_Master Where Acc_JE_YearID=" & iYearID & " And Acc_JE_CompID=" & iCompID & " ")

            sSql = "" : sSql = "Select * from ACC_Voucher_Settings where AVS_TransType = 4  and AVS_CompID = " & iCompID & ""
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
    Public Function DeleteJEDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete from Acc_Transactions_Details where ATD_ID = " & iTransactionID & " and Atd_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
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
    Public Function DeleteExeJETransDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPCID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Delete acc_Transactions_Details where ATD_BillId =" & iPCID & " And ATD_CompID =" & iCompID & " And ATD_TrType=4"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExistingVoucherNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iParty = 0 Then
                sSql = "Select Acc_JE_TransactionNo,Acc_JE_ID from  Acc_JE_Master where Acc_JE_CompID=" & iCompID & " and Acc_JE_YearID=" & iYearID & " order by Acc_JE_ID Desc"
            Else
                sSql = "Select Acc_JE_TransactionNo,Acc_JE_ID from  Acc_JE_Master where  Acc_JE_CompID=" & iCompID & " and Acc_JE_YearID=" & iYearID & " and Acc_JE_Party = " & iParty & " order by Acc_JE_ID Desc"
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
            sSql = "Select Mas_ID,Mas_Desc from ACC_General_Master where mas_master = 26 and mas_Delflag ='A' and mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select APM_ID,APM_Name + ' - ' + APM_Code as Name  from Accounts_Party_Master where APM_Delflag='A' and APM_CompID =" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select ACM_ID,ACM_Name + ' - ' + ACM_Code as Name  from Acc_Customer_Master where ACM_Status='A' and ACM_CompID =" & iCompID & ""
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


    Public Function LoadGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iAccHead & "  order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function SaveJournalMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objJE As clsJournalEntry) As Integer
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from Acc_JE_Master where Acc_JE_ID =" & objJE.iAcc_JE_ID & " and Acc_JE_CompID =" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        If dt.Rows.Count > 0 Then
    '            sSql = "" : sSql = "Update Acc_JE_Master set Acc_JE_Party = " & objJE.iAcc_JE_Party & ",Acc_JE_Location=" & objJE.iAcc_JE_Location & ","
    '            sSql = sSql & "Acc_JE_BillType = " & objJE.iAcc_JE_BillType & ",Acc_JE_BillNo = '" & objGen.SafeSQL(objJE.sAcc_JE_BillNo) & "',"
    '            sSql = sSql & "Acc_JE_BillDate = " & objGen.FormatDtForRDBMS(objJE.dAcc_JE_BillDate, "I") & ",Acc_JE_BillAmount = " & objJE.dAcc_JE_BillAmount & " "

    '            If iPaymentType = 1 Then
    '                sSql = sSql & ",Acc_JE_AdvanceAmount = " & objJE.dAcc_JE_AdvanceAmount & ",Acc_JE_AdvanceNaration = '" & objGen.SafeSQL(objJE.sAcc_JE_AdvanceNaration) & "',Acc_JE_BalanceAmount = " & objJE.dAcc_JE_BalanceAmount & " "
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & ",Acc_JE_NetAmount = " & objJE.dAcc_JE_NetAmount & ",Acc_JE_PaymentNarration = '" & objJE.sAcc_JE_PaymentNarration & "' "
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & ",Acc_JE_ChequeNo = " & objJE.sAcc_JE_ChequeNo & ","
    '                sSql = sSql & "Acc_JE_ChequeDate = " & objGen.FormatDtForRDBMS(Acc_JE_ChequeDate, "I") & ",Acc_JE_IFSCCode = '" & objJE.sAcc_JE_IFSCCode & "',"
    '                sSql = sSql & "Acc_JE_BankName = '" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "',Acc_JE_BranchName = '" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "' "
    '            End If
    '            sSql = sSql & "Where Acc_JE_ID = " & objJE.iAcc_JE_ID & " and Acc_JE_CompID =" & iCompID & ""
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return objJE.iAcc_JE_ID
    '        Else
    '            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_JE_ID)+1,1) from Acc_JE_Master")
    '            sSql = "" : sSql = "Insert into Acc_JE_Master(Acc_JE_ID,Acc_JE_TransactionNo,Acc_JE_Party,Acc_JE_Location,"
    '            sSql = sSql & "Acc_JE_BillType,Acc_JE_BillNo,Acc_JE_BillDate,Acc_JE_BillAmount,"

    '            If iPaymentType = 1 Then
    '                sSql = sSql & "Acc_JE_AdvanceAmount,Acc_JE_AdvanceNaration,Acc_JE_BalanceAmount,"
    '            ElseIf iPaymentType = 2 Then
    '                sSql = sSql & "Acc_JE_TDSType,ACC_JE_TDSDeduct,Acc_JE_TDSAmount,Acc_JE_TDSNarration,"
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & "Acc_JE_NetAmount,Acc_JE_PaymentNarration,"
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & "Acc_JE_ChequeNo,Acc_JE_ChequeDate,Acc_JE_IFSCCode,Acc_JE_BankName,Acc_JE_BranchName,"
    '            End If

    '            sSql = sSql & "Acc_JE_CreatedBy,Acc_JE_CreatedOn,Acc_JE_YearID,Acc_JE_CompID,"
    '            sSql = sSql & "Acc_JE_Status,Acc_JE_Operation,Acc_JE_IPAddress,Acc_JE_BillCreatedDate)"
    '            sSql = sSql & "Values(" & iMax & ",'" & objJE.sAcc_JE_TransactionNo & "'," & objJE.iAcc_JE_Party & "," & objJE.iAcc_JE_Location & ","
    '            sSql = sSql & "" & objJE.iAcc_JE_BillType & ",'" & objGen.SafeSQL(objJE.sAcc_JE_BillNo) & "'," & objGen.FormatDtForRDBMS(objJE.dAcc_JE_BillDate, "I") & "," & objJE.dAcc_JE_BillAmount & ","

    '            If iPaymentType = 1 Then
    '                sSql = sSql & "" & objJE.dAcc_JE_AdvanceAmount & ",'" & objGen.SafeSQL(objJE.sAcc_JE_AdvanceNaration) & "'," & objJE.dAcc_JE_BalanceAmount & ","
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & "" & objJE.dAcc_JE_NetAmount & ",'" & objGen.SafeSQL(objJE.sAcc_JE_PaymentNarration) & "',"
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & "'" & objJE.sAcc_JE_ChequeNo & "'," & objGen.FormatDtForRDBMS(objJE.dAcc_JE_ChequeDate, "I") & ","
    '                sSql = sSql & "'" & objGen.SafeSQL(objJE.sAcc_JE_IFSCCode) & "','" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "','" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "',"
    '            End If

    '            sSql = sSql & "" & objJE.iAcc_JE_CreatedBy & ",GetDate()," & objJE.iAcc_JE_YearID & "," & iCompID & ","
    '            sSql = sSql & "'" & objJE.sAcc_JE_Status & "','" & objJE.sAcc_JE_Operation & "','" & objJE.sAcc_JE_IPAddress & "',GetDate())"
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return iMax
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function SaveJournalMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, objJE As clsJournalEntry)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(31) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_TransactionNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_BillType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BillAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_BillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_AdvanceNaration", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_AdvanceNaration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_ChequeNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_ChequeDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_IFSCCode", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BankName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BranchName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_BranchName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objJE.sAcc_JE_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_InvoiceDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objJE.dAcc_JE_InvoiceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_JE_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iACC_JE_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_JE_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iACC_JE_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_JE_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iACC_JE_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_JE_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iACC_JE_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_JEType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_JEType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_JE_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iAcc_JE_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_JE_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objJE As clsJournalEntry)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objJE.dATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objJE.dATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objJE.dATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objJE.sATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objJE.iATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objJE.sATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objJE.sATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iATD_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iATD_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iATD_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iATD_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objJE.dATD_OpenDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objJE.dATD_OpenCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objJE.dATD_ClosingDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objJE.dATD_ClosingCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objJE.iATD_SeqReferenceNum
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

    'Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objJE As clsJournalEntry)
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Try
    '        iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(ATD_ID)+1,1) from Acc_Transactions_Details")
    '        sSql = "" : sSql = "Insert into Acc_Transactions_Details(ATD_ID,ATD_TransactionDate,ATD_TrType,"
    '        sSql = sSql & "ATD_BillId,ATD_PaymentType,ATD_Head,"
    '        sSql = sSql & "ATD_GL,ATD_SubGL,ATD_Debit,ATD_Credit,"
    '        sSql = sSql & "ATD_CreatedOn,ATD_CreatedBy,ATD_Status,"
    '        sSql = sSql & "ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress)"
    '        sSql = sSql & "Values(" & iMax & ",GetDate()," & objJE.iATD_TrType & ","
    '        sSql = sSql & "" & objJE.iATD_BillId & "," & objJE.iATD_PaymentType & "," & objJE.iATD_Head & ","
    '        sSql = sSql & "" & objJE.iATD_GL & "," & objJE.iATD_SubGL & "," & objJE.dATD_Debit & "," & objJE.dATD_Credit & ","
    '        sSql = sSql & "" & objJE.iATD_CreatedOn & ",GetDate(),'" & objJE.sATD_Status & "',"
    '        sSql = sSql & "" & objJE.iATD_YearID & "," & iCompID & ",'" & objJE.sATD_Operation & "','" & objJE.sATD_IPAddress & "')"
    '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Public Function LoadTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer) As DataTable
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

            sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
            sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 4 and "
            sSql = sSql & "A.ATD_GL = B.gl_ID  Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "

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

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()) = False Then
                        dr("PaymentID") = ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()

                        If ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "1" Then
                            dr("Type") = "Advance Payment"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "2" Then
                            dr("Type") = "Bill Passing"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "3" Then
                            dr("Type") = "Payment"
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "4" Then
                            dr("Type") = "Cheque"
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

    Public Function GetPaymentTypeDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Acc_JE_Master where Acc_JE_ID =" & iPaymentID & " and Acc_JE_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
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

    Public Function UpdatePaymentMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objJE As clsJournalEntry)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from acc_JE_Master where Acc_JE_ID =" & objJE.iAcc_JE_ID & " and Acc_JE_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update acc_JE_Master set Acc_JE_Party = " & objJE.iAcc_JE_Party & ",Acc_JE_Location=" & objJE.iAcc_JE_Location & ","
                sSql = sSql & "Acc_JE_BillType = " & objJE.iAcc_JE_BillType & ",Acc_JE_BillNo = '" & objGen.SafeSQL(objJE.sAcc_JE_BillNo) & "',"
                sSql = sSql & "Acc_JE_BillDate = " & objGen.FormatDtForRDBMS(objJE.dAcc_JE_BillDate, "I") & ",Acc_JE_BillAmount = " & objJE.dAcc_JE_BillAmount & " "

                If iPaymentType = 1 Then
                    sSql = sSql & ",Acc_JE_AdvanceAmount = " & objJE.dAcc_JE_AdvanceAmount & ",Acc_JE_AdvanceNaration = '" & objGen.SafeSQL(objJE.sAcc_JE_AdvanceNaration) & "',Acc_JE_BalanceAmount = " & objJE.dAcc_JE_BalanceAmount & " "
                ElseIf iPaymentType = 3 Then
                    sSql = sSql & ",Acc_JE_NetAmount = " & objJE.dAcc_JE_NetAmount & ",Acc_JE_PaymentNarration = '" & objJE.sAcc_JE_PaymentNarration & "' "
                ElseIf iPaymentType = 4 Then
                    sSql = sSql & ",Acc_JE_ChequeNo = " & objJE.sAcc_JE_ChequeNo & ","
                    sSql = sSql & "Acc_JE_ChequeDate = " & objGen.FormatDtForRDBMS(objJE.Acc_JE_ChequeDate, "I") & ",Acc_JE_IFSCCode = '" & objJE.sAcc_JE_IFSCCode & "',"
                    sSql = sSql & "Acc_JE_BankName = '" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "',Acc_JE_BranchName = '" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "' "
                End If
                sSql = sSql & "Where Acc_JE_ID = " & objJE.iAcc_JE_ID & " and Acc_JE_CompID =" & iCompID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return objJE.iAcc_JE_ID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objJE As clsJournalEntry)
        Dim sSql As String = ""
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Update Acc_Transactions_Details set ATD_Head =" & objJE.iATD_Head & ",ATD_GL=" & objJE.iATD_GL & ","
            sSql = sSql & "ATD_SubGL =" & objJE.iATD_SubGL & ",ATD_DbOrCr = " & objJE.iATD_DbOrCr & ","
            sSql = sSql & "ATD_Debit = " & objJE.dATD_Debit & ",ATD_Credit=" & objJE.dATD_Credit & " where "
            sSql = sSql & "ATD_ID =" & objJE.iATD_ID & " and ATD_TrType =" & objJE.iATD_TrType & " and ATD_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeletePaymentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete from Acc_Transactions_Details where ATD_ID = " & iTransactionID & " and Atd_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

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
    Public Function SaveChequeDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objJE As clsJournalEntry)
        Dim sSql As String = ""
        Try
            sSql = "Update acc_JE_Master set ACC_JE_ChequeNo = " & objJE.sAcc_JE_ChequeNo & ",Acc_JE_ChequeDate=" & objGen.FormatDtForRDBMS(objJE.dAcc_JE_ChequeDate, "I") & ","
            sSql = sSql & "Acc_JE_IFSCCode = '" & objGen.SafeSQL(objJE.sAcc_JE_IFSCCode) & "',ACC_JE_BankName='" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "',"
            sSql = sSql & "Acc_JE_BranchName = '" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "' where Acc_JE_ID=" & objJE.iAcc_JE_ID & " and Acc_JE_CompID =" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
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

    '        dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=4 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
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
    '                    sSql = sSql & " Where A.ATD_Status='A' And A.ATD_TrType=4 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
    '                    dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '                Else
    '                    sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit,A.ATD_TransactionDate,A.ATD_ZoneID,A.ATD_RegionID,A.ATD_AreaID,A.ATD_BranchID"
    '                    sSql = sSql & " From Acc_Transactions_Details A"
    '                    sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
    '                    sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
    '                    sSql = sSql & " Where A.ATD_Status='A' And A.ATD_TrType=4 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
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

    '                        sSql = "" : sSql = "Update Acc_Ledger_Masters Set ALM_OBDebit=" & dOpenDebit & ",ALM_OBCredit=" & dOpenCredit & ",ALM_TrDebit=" & dTotalTransDebit & ",ALM_TrCredit=" & dTotalTransCredit & ",ALM_CloseDebit=" & dCloseDebit & ",ALM_CloseCredit=" & dCloseCredit & ",ALM_TrDate='" & objGen.FormatDtForRDBMS(dtValues.Rows(0)("ATD_TransactionDate"), "D") & "',ALM_ZoneID=" & dtValues.Rows(0)("ATD_ZoneID") & ",ALM_RegionID=" & dtValues.Rows(0)("ATD_RegionID") & ",ALM_AreaID=" & dtValues.Rows(0)("ATD_AreaID") & ",ALM_BranchID=" & dtValues.Rows(0)("ATD_BranchID") & " "
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
                                Where A.gl_Head in (2) And A.gl_CompID=" & iCompID & " And A.GL_ID  Not In (Select ALM_GL From Acc_Ledger_Masters Where ALM_CompID=" & iCompID & ")"
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

            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=4 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
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
                        sSql = sSql & " Where A.ATD_Status='A' And A.ATD_TrType=4 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
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
    Public Function SaveJEBreakUp(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objJEE As JE)
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_JE_Master_Details Where AJE_MasterID=" & objJEE.iJE_MasterID & " And AJE_BillNo='" & objJEE.sJE_BillNo & "' And AJE_CompID=" & objJEE.iJE_CompID & " And AJE_YearID=" & objJEE.iJE_YearID & " ")
            If bCheck = True Then

            Else
                iMaxID = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Aje_ID)+1,1) from Acc_JE_Master_Details")
                sSql = "" : sSql = "Insert Into Acc_JE_Master_Details (AJE_ID,AJE_MasterID,AJE_BillNo,AJE_BillDate,AJE_BillAmount,AJE_Status,AJE_CreatedBy,AJE_CreatedOn,AJE_CompID,AJE_YearID,AJE_Operation,AJE_IPAddress)"
                sSql = sSql & "Values(" & iMaxID & "," & objJEE.iJE_MasterID & ",'" & objJEE.sJE_BillNo & "'," & objGen.FormatDtForRDBMS(objJEE.dJE_BillDate, "I") & "," & objJEE.dJE_BillAmount & ",'" & objJEE.sJE_Status & "'," & objJEE.iJE_CreatedBy & "," & objGen.FormatDtForRDBMS(objJEE.dJE_CreatedOn, "I") & "," & objJEE.iJE_CompID & "," & objJEE.iJE_YearID & ",'" & objJEE.sJE_Operation & "','" & objJEE.sJE_IPAddress & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer)
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_JE_Master_Details Where AJE_MasterID=" & iMasterID & " ")
            If bCheck = True Then
                sSql = "" : sSql = "Delete From Acc_JE_Master_Details Where AJE_MasterID=" & iMasterID & " And AJE_CompID=" & iCompID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPettyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPattyID As Integer) As DataTable
        Dim dt, dt1 As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("PettyID")
            dt.Columns.Add("BillNo")
            dt.Columns.Add("BillDate")
            dt.Columns.Add("BillAmount")

            sSql = "Select * From Acc_JE_Master_Details Where AJE_MasterID=" & iPattyID & " And AJE_CompID=" & iCompID & " "
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = dt1.Rows(i)("AJE_ID")
                    dr("PettyID") = dt1.Rows(i)("AJE_MasterID")
                    dr("BillNo") = dt1.Rows(i)("AJE_BillNo")
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("AJE_BillDate"), "D")
                    dr("BillAmount") = dt1.Rows(i)("AJE_BillAmount")

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteALLTransactions(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iJEID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Update ACC_JE_Master Set Acc_JE_BillAmount=0 Where Acc_JE_ID=" & iJEID & " And Acc_JE_CompID=" & iCompID & " And Acc_JE_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            sSql = "" : sSql = "Delete From ACC_JE_Master_Details Where AJE_MasterID=" & iJEID & " And AJE_CompID=" & iCompID & " And AJE_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            sSql = "" : sSql = "Delete From Acc_Transactions_Details Where ATD_TrType=4 And ATD_BillID=" & iJEID & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
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
            sSql = "" : sSql = "Select * From Acc_Transactions_Details Where ATD_Status='A' And ATD_TrType=4 And ATD_BillID=" & iMasID & " And ATD_YearID=" & iyearID & " And ATD_CompID=" & iCompID & " "
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
    Public Function DeleteBillNoTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iJEBillID As Integer, ByVal iJEDetailsPkiid As Integer, ByVal iYearId As Integer, ByVal sAddBillAmount As String)
        Dim sSql As String = ""
        Dim sSumBillAmount As String
        Dim iCountJE As Integer
        Try
            If sAddBillAmount = "DeleteBill" Then
                sSql = "delete from Acc_JE_Master_Details  Where AJE_ID=" & iJEDetailsPkiid & " And AJE_CompID=" & iCompID & " and AJE_Yearid=" & iYearId & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)


                sSql = "select count(Atd_id) from Acc_Transactions_Details  Where ATD_BillID=" & iJEBillID & " and ATD_TrType = 4 And ATD_CompID=" & iCompID & " and ATD_Yearid=" & iYearId & " and atd_status='W' "
                iCountJE = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

                If iCountJE > 0 Then
                    sSql = "delete from Acc_Transactions_Details  Where ATD_BillID=" & iJEBillID & " and ATD_TrType = 4 And ATD_CompID=" & iCompID & " and ATD_Yearid=" & iYearId & " and atd_status='W' "
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                End If

                sSql = "Select sum(AJE_BillAmount) from Acc_JE_Master_Details  Where AJE_MasterID=" & iJEBillID & " And AJE_CompID=" & iCompID & " and AJE_Yearid=" & iYearId & ""
                sSumBillAmount = objDBL.SQLExecuteScalar(sNameSpace, sSql).ToString()

                If sSumBillAmount = "" Then
                    sSumBillAmount = "0.00"
                End If

                sSql = "Update Acc_JE_Master set Acc_Je_BillAmount=" & sSumBillAmount & "  Where Acc_JE_ID=" & iJEBillID & " And Acc_JE_CompID=" & iCompID & " and Acc_JE_Yearid=" & iYearId & " and ACC_Je_Status='W'"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            ElseIf sAddBillAmount = "AddBill" Then
                sSql = "select count(Atd_id) from Acc_Transactions_Details  Where ATD_BillID=" & iJEBillID & " and ATD_TrType = 4 And ATD_CompID=" & iCompID & " and ATD_Yearid=" & iYearId & " and atd_status='W' "
                iCountJE = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

                If iCountJE > 0 Then
                    sSql = "delete from Acc_Transactions_Details  Where ATD_BillID=" & iJEBillID & " and ATD_TrType = 4 And ATD_CompID=" & iCompID & " and ATD_Yearid=" & iYearId & " and atd_status='W'"
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
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