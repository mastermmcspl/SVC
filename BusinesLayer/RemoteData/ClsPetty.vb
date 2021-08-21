Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsPetty
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private Acc_PCM_ID As Integer
    Private Acc_PCM_TransactionNo As String
    Private Acc_PCM_Party As Integer
    Private Acc_PCM_Location As Integer
    Private Acc_PCM_BillType As Integer
    Private Acc_PCM_BillNo As String
    Private Acc_PCM_BillDate As Date
    Private Acc_PCM_BillAmount As Decimal
    Private Acc_PCM_AdvanceAmount As Decimal
    Private Acc_PCM_AdvanceNaration As String
    Private Acc_PCM_TDSType As Integer
    Private ACC_PCM_TDSDeduct As Decimal
    Private Acc_PCM_TDSAmount As Decimal
    Private Acc_PCM_TDSNarration As String
    Private Acc_PCM_NetAmount As Decimal
    Private Acc_PCM_PaymentNarration As String
    Private Acc_PCM_CreatedBy As Integer
    Private Acc_PCM_UpdatedBy As Integer
    Private Acc_PCM_CreatedOn As Date
    Private Acc_PCM_ApprovedBy As Integer
    Private Acc_PCM_ApprovedOn As Date
    Private Acc_PCM_DeletedBy As Integer
    Private Acc_PCM_DeletedOn As Date
    Private Acc_PCM_RecalledBy As Integer
    Private Acc_PCM_RecalledOn As Date
    Private Acc_PCM_YearID As Integer
    Private Acc_PCM_CompID As Integer
    Private Acc_PCM_Status As String
    Private Acc_PCM_Operation As String
    Private Acc_PCM_IPAddress As String
    Private Acc_PCM_InvoiceDate As Date
    Private Acc_PCM_BalanceAmount As Decimal
    Private Acc_PCM_AttachID As Integer
    Private ACC_PCM_ZoneID As Integer
    Private ACC_PCM_RegionID As Integer
    Private ACC_PCM_AreaID As Integer
    Private ACC_PCM_BranchID As Integer

    Private iAcc_PCM_BatchNo As Integer
    Private iAcc_PCM_BaseName As Integer

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

    Public Structure Petty
        Dim iAPMD_ID As Integer
        Dim iAPMD_MasterID As Integer
        Dim sAPMD_BillNo As String
        Dim dAPMD_BillDate As Date
        Dim dAPMD_BillAmount As Double
        Dim sAPMD_Status As String
        Dim iAPMD_CreatedBy As Integer
        Dim dAPMD_CreatedOn As Date
        Dim iAPMD_CompID As Integer
        Dim iAPMD_YearID As Integer
        Dim sAPMD_Operation As String
        Dim sAPMD_IPAddress As String
    End Structure


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


    Public Property Acc_PCM_BatchNo() As Integer
        Get
            Return (iAcc_PCM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iAcc_PCM_BatchNo = Value
        End Set
    End Property
    Public Property Acc_PCM_BaseName() As Integer
        Get
            Return (iAcc_PCM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iAcc_PCM_BaseName = Value
        End Set
    End Property

    Public Property iACC_PCM_ZoneID() As Integer
        Get
            Return (ACC_PCM_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            ACC_PCM_ZoneID = Value
        End Set
    End Property
    Public Property iACC_PCM_RegionID() As Integer
        Get
            Return (ACC_PCM_RegionID)
        End Get
        Set(ByVal Value As Integer)
            ACC_PCM_RegionID = Value
        End Set
    End Property
    Public Property iACC_PCM_AreaID() As Integer
        Get
            Return (ACC_PCM_AreaID)
        End Get
        Set(ByVal Value As Integer)
            ACC_PCM_AreaID = Value
        End Set
    End Property
    Public Property iACC_PCM_BranchID() As Integer
        Get
            Return (ACC_PCM_BranchID)
        End Get
        Set(ByVal Value As Integer)
            ACC_PCM_BranchID = Value
        End Set
    End Property
    Public Property iAcc_PCM_AttachID() As Integer
        Get
            Return (Acc_PCM_AttachID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_AttachID = Value
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

    Public Property dAcc_PCM_BalanceAmount() As Decimal
        Get
            Return (Acc_PCM_BalanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PCM_BalanceAmount = Value
        End Set
    End Property

    Public Property dAcc_PCM_InvoiceDate() As Date
        Get
            Return (Acc_PCM_InvoiceDate)
        End Get
        Set(ByVal Value As Date)
            Acc_PCM_InvoiceDate = Value
        End Set
    End Property

    Public Property sAcc_PCM_IPAddress() As String
        Get
            Return (Acc_PCM_IPAddress)
        End Get
        Set(ByVal Value As String)
            Acc_PCM_IPAddress = Value
        End Set
    End Property

    Public Property sAcc_PCM_Operation() As String
        Get
            Return (Acc_PCM_Operation)
        End Get
        Set(ByVal Value As String)
            Acc_PCM_Operation = Value
        End Set
    End Property
    Public Property sAcc_PCM_Status() As String
        Get
            Return (Acc_PCM_Status)
        End Get
        Set(ByVal Value As String)
            Acc_PCM_Status = Value
        End Set
    End Property

    Public Property iAcc_PCM_CompID() As Integer
        Get
            Return (Acc_PCM_CompID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_CompID = Value
        End Set
    End Property

    Public Property iAcc_PCM_YearID() As Integer
        Get
            Return (Acc_PCM_YearID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_YearID = Value
        End Set
    End Property
    Public Property dAcc_PCM_RecalledOn() As Date
        Get
            Return (Acc_PCM_RecalledOn)
        End Get
        Set(ByVal Value As Date)
            Acc_PCM_RecalledOn = Value
        End Set
    End Property

    Public Property iAcc_PCM_RecalledBy() As Integer
        Get
            Return (Acc_PCM_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_RecalledBy = Value
        End Set
    End Property
    Public Property dAcc_PCM_DeletedOn() As Date
        Get
            Return (Acc_PCM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_PCM_DeletedOn = Value
        End Set
    End Property

    Public Property iAcc_PCM_DeletedBy() As Integer
        Get
            Return (Acc_PCM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_DeletedBy = Value
        End Set
    End Property
    Public Property iAcc_PCM_UpdatedBy() As Integer
        Get
            Return (Acc_PCM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_UpdatedBy = Value
        End Set
    End Property

    Public Property dAcc_PCM_ApprovedOn() As Date
        Get
            Return (Acc_PCM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_PCM_ApprovedOn = Value
        End Set
    End Property
    Public Property iAcc_PCM_ApprovedBy() As Integer
        Get
            Return (Acc_PCM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_ApprovedBy = Value
        End Set
    End Property

    Public Property dAcc_PCM_CreatedOn() As Date
        Get
            Return (Acc_PCM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_PCM_CreatedOn = Value
        End Set
    End Property

    Public Property iAcc_PCM_CreatedBy() As Integer
        Get
            Return (Acc_PCM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_CreatedBy = Value
        End Set
    End Property

    Public Property sAcc_PCM_PaymentNarration() As String
        Get
            Return (Acc_PCM_PaymentNarration)
        End Get
        Set(ByVal Value As String)
            Acc_PCM_PaymentNarration = Value
        End Set
    End Property
    Public Property dAcc_PCM_NetAmount() As Decimal
        Get
            Return (Acc_PCM_NetAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PCM_NetAmount = Value
        End Set
    End Property

    Public Property sAcc_PCM_TDSNarration() As String
        Get
            Return (Acc_PCM_TDSNarration)
        End Get
        Set(ByVal Value As String)
            Acc_PCM_TDSNarration = Value
        End Set
    End Property

    Public Property dAcc_PCM_TDSAmount() As Decimal
        Get
            Return (Acc_PCM_TDSAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PCM_TDSAmount = Value
        End Set
    End Property
    Public Property dACC_PCM_TDSDeduct() As Decimal
        Get
            Return (ACC_PCM_TDSDeduct)
        End Get
        Set(ByVal Value As Decimal)
            ACC_PCM_TDSDeduct = Value
        End Set
    End Property

    Public Property iAcc_PCM_TDSType() As Integer
        Get
            Return (Acc_PCM_TDSType)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_TDSType = Value
        End Set
    End Property
    Public Property sAcc_PCM_AdvanceNaration() As String
        Get
            Return (Acc_PCM_AdvanceNaration)
        End Get
        Set(ByVal Value As String)
            Acc_PCM_AdvanceNaration = Value
        End Set
    End Property
    Public Property dAcc_PCM_AdvanceAmount() As Decimal
        Get
            Return (Acc_PCM_AdvanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PCM_AdvanceAmount = Value
        End Set
    End Property
    Public Property dAcc_PCM_BillAmount() As Decimal
        Get
            Return (Acc_PCM_BillAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_PCM_BillAmount = Value
        End Set
    End Property

    Public Property dAcc_PCM_BillDate() As Date
        Get
            Return (Acc_PCM_BillDate)
        End Get
        Set(ByVal Value As Date)
            Acc_PCM_BillDate = Value
        End Set
    End Property

    Public Property sAcc_PCM_BillNo() As String
        Get
            Return (Acc_PCM_BillNo)
        End Get
        Set(ByVal Value As String)
            Acc_PCM_BillNo = Value
        End Set
    End Property

    Public Property iAcc_PCM_BillType() As Integer
        Get
            Return (Acc_PCM_BillType)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_BillType = Value
        End Set
    End Property
    Public Property iAcc_PCM_Location() As Integer
        Get
            Return (Acc_PCM_Location)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_Location = Value
        End Set
    End Property

    Public Property iAcc_PCM_Party() As Integer
        Get
            Return (Acc_PCM_Party)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_Party = Value
        End Set
    End Property


    Public Property sAcc_PCM_TransactionNo() As String
        Get
            Return (Acc_PCM_TransactionNo)
        End Get
        Set(ByVal Value As String)
            Acc_PCM_TransactionNo = Value
        End Set
    End Property

    Public Property iAcc_PCM_ID() As Integer
        Get
            Return (Acc_PCM_ID)
        End Get
        Set(ByVal Value As Integer)
            Acc_PCM_ID = Value
        End Set
    End Property

    Public Function LoadPettyCash(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iStatus As Integer, ByVal iYearID As Integer) As DataTable
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

            sSql = "Select * from Acc_PettyCash_Master where Acc_PCM_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Acc_PCM_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Acc_PCM_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Acc_PCM_Status='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By Acc_PCM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PCM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("Acc_PCM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PCM_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("Acc_PCM_TransactionNo").ToString()
                    End If

                    dtBillDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * from Acc_PettyCash_Master_Details Where APMD_MasterID=" & ds.Tables(0).Rows(i)("Acc_PCM_ID") & " And APMD_CompID=" & iCompID & " And APMD_yearID=" & iYearID & " ").Tables(0)
                    If dtBillDetails.Rows.Count > 0 Then
                        For j = 0 To dtBillDetails.Rows.Count - 1
                            sBillNo = sBillNo & "," & dtBillDetails.Rows(j)("APMD_BillNo")
                            sBillDate = sBillDate & "," & dtBillDetails.Rows(j)("APMD_BillDate")
                            sBillAmt = sBillAmt & "," & dtBillDetails.Rows(j)("APMD_BillAmount")
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

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PCM_BillType").ToString()) = False Then
                        dr("BillType") = GetBillType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_PCM_BillType").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PCM_Party").ToString()) = False Then
                        dr("Party") = GetPartyName(sNameSpace, iCompID, ds.Tables(0).Rows(i)("Acc_PCM_Location").ToString(), ds.Tables(0).Rows(i)("Acc_PCM_Party").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PCM_BillAmount").ToString()) = False Then
                        dr("TrAmt") = ds.Tables(0).Rows(i)("Acc_PCM_BillAmount").ToString()
                    End If

                    If (ds.Tables(0).Rows(i)("Acc_PCM_Status") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_PCM_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("Acc_PCM_Status") = "D") Then
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

    Public Sub UpdatePaymentMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_PettyCash_Master Set Acc_PCM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_PCM_Status='A',Acc_PCM_ApprovedBy= " & iUserID & ",Acc_PCM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_PCM_Status='D',Acc_PCM_DeletedBy= " & iUserID & ",Acc_PCM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_PCM_Status='A' "
            End If
            sSql = sSql & " Where Acc_PCM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Public Function GetPartyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As String
    '    Dim sSQL As String = ""
    '    Dim sParty As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSQL = "" : sSQL = "Select *  from Accounts_Party_Master where APM_Delflag='A' and APM_ID = " & iParty & " and APM_CompID= " & iCompID & ""
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            If IsDBNull(dt.Rows(0)("APM_Name").ToString()) = False Then
    '                sParty = dt.Rows(0)("APM_Name").ToString() & " - " & dt.Rows(0)("APM_Code").ToString()
    '            Else
    '                sParty = ""
    '            End If
    '        Else
    '            sParty = ""
    '        End If
    '        Return sParty
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
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
            'If dt.Rows.Count > 0 Then
            '    If IsDBNull(dt.Rows(0)("APM_Name").ToString()) = False Then
            '        sParty = dt.Rows(0)("APM_Name").ToString() & " - " & dt.Rows(0)("APM_Code").ToString()
            '    Else
            '        sParty = ""
            '    End If
            'Else
            '    sParty = ""
            'End If
            Return sParty
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingPettyCashVoucherNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iParty = 0 Then
                sSql = "Select Acc_PCM_TransactionNo,Acc_PCM_ID from  Acc_PettyCash_Master where Acc_PCM_CompID=" & iCompID & " and Acc_PCM_YearID=" & iYearID & " order by Acc_PCM_ID Desc"
            Else
                sSql = "Select Acc_PCM_TransactionNo,Acc_PCM_ID from  Acc_PettyCash_Master where Acc_PCM_CompID=" & iCompID & " and Acc_PCM_YearID=" & iYearID & " and Acc_PCM_Party = " & iParty & " order by Acc_PCM_ID Desc"
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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

    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_PCM_ID)+1,1) from Acc_PettyCash_Master")

            sSql = "" : sSql = "Select * from ACC_Voucher_Settings where AVS_TransType = 2  and AVS_CompID = " & iCompID & ""
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
    Public Function LoadTDSType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc from ACC_General_Master where mas_master = 10 and mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
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
            sSql = "Select Mas_ID,Mas_Desc from ACC_General_Master where mas_master = 9 and mas_Delflag ='A' and mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select ACM_ID,ACM_Code + ' - ' + ACM_Name as Name  from Acc_Customer_Master where ACM_Status='A' and ACM_CompID =" & iCompID & ""
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
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccGL As Integer
        Try
            sSql = "Select Acc_Gl from acc_application_settings where Acc_Types='Cash' and Acc_CompID=" & iCompID & " "
            iAccGL = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iAccGL
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
    Public Function SavePettyCashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objPC As ClsPetty)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_TransactionNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPC.sAcc_PCM_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_BillType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objPC.sAcc_PCM_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_BillDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPC.dAcc_PCM_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_BillAmount", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPC.dAcc_PCM_BillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_PaymentNarration", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objPC.sAcc_PCM_PaymentNarration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPC.sAcc_PCM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPC.sAcc_PCM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPC.sAcc_PCM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_InvoiceDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPC.dAcc_PCM_InvoiceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PCM_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PCM_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPC.iACC_PCM_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PCM_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPC.iACC_PCM_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PCM_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPC.iACC_PCM_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PCM_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPC.iACC_PCM_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PCM_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_PCM_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iAcc_PCM_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_PettyCash_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objPC As ClsPetty)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPC.dATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPC.dATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPC.dATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPC.sATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPC.iATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPC.sATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPC.sATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPC.iATD_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPC.iATD_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPC.iATD_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPC.iATD_BranchID
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
    'Public Function SavePettyCashMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPC As clsPettyCash) As Integer
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from Acc_PettyCash_Master where Acc_PCM_ID =" & objPC.iAcc_PCM_ID & " and Acc_PCM_CompID =" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        If dt.Rows.Count > 0 Then
    '            sSql = "" : sSql = "Update Acc_PettyCash_Master set Acc_PCM_Party = " & objPC.iAcc_PCM_Party & ",Acc_PCM_Location=" & objPC.iAcc_PCM_Location & ","
    '            sSql = sSql & "Acc_PCM_BillType = " & objPC.iAcc_PCM_BillType & ",Acc_PCM_BillNo = '" & objGen.SafeSQL(objPC.sAcc_PCM_BillNo) & "',"
    '            sSql = sSql & "Acc_PCM_BillDate = " & objGen.FormatDtForRDBMS(objPC.dAcc_PCM_BillDate, "I") & ",Acc_PCM_BillAmount = " & objPC.dAcc_PCM_BillAmount & " "

    '            If iPaymentType = 1 Then
    '                sSql = sSql & ",Acc_PCM_AdvanceAmount = " & objPC.dAcc_PCM_AdvanceAmount & ",Acc_PCM_AdvanceNaration = '" & objGen.SafeSQL(objPC.sAcc_PCM_AdvanceNaration) & "',Acc_PCM_BalanceAmount = " & objPC.dAcc_PCM_BalanceAmount & " "
    '            ElseIf iPaymentType = 2 Then
    '                sSql = sSql & ",Acc_PCM_TDSType = " & objPC.iAcc_PCM_TDSType & ",ACC_PCM_TDSDeduct=" & objPC.dACC_PCM_TDSDeduct & ",Acc_PCM_TDSAmount=" & objPC.dAcc_PCM_TDSAmount & ","
    '                sSql = sSql & "Acc_PCM_TDSNarration = '" & objGen.SafeSQL(objPC.sAcc_PCM_TDSNarration) & "' "
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & ",Acc_PCM_NetAmount = " & objPC.dAcc_PCM_NetAmount & ",Acc_PCM_PaymentNarration = '" & objPC.sAcc_PCM_PaymentNarration & "' "
    '            End If
    '            sSql = sSql & "Where Acc_PCM_ID = " & objPC.iAcc_PCM_ID & " and Acc_PCM_CompID =" & iCompID & ""
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return objPC.iAcc_PCM_ID
    '        Else
    '            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_PCM_ID)+1,1) from Acc_PettyCash_Master")
    '            sSql = "" : sSql = "Insert into Acc_PettyCash_Master(Acc_PCM_ID,Acc_PCM_TransactionNo,Acc_PCM_Party,Acc_PCM_Location,"
    '            sSql = sSql & "Acc_PCM_BillType,Acc_PCM_BillNo,Acc_PCM_BillDate,Acc_PCM_BillAmount,"

    '            If iPaymentType = 1 Then
    '                sSql = sSql & "Acc_PCM_AdvanceAmount,Acc_PCM_AdvanceNaration,Acc_PCM_BalanceAmount,"
    '            ElseIf iPaymentType = 2 Then
    '                sSql = sSql & "Acc_PCM_TDSType,ACC_PCM_TDSDeduct,Acc_PCM_TDSAmount,Acc_PCM_TDSNarration,"
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & "Acc_PCM_NetAmount,Acc_PCM_PaymentNarration,"
    '            End If

    '            sSql = sSql & "Acc_PCM_CreatedBy,Acc_PCM_CreatedOn,Acc_PCM_YearID,Acc_PCM_CompID,"
    '            sSql = sSql & "Acc_PCM_Status,Acc_PCM_Operation,Acc_PCM_IPAddress,Acc_PCM_BillCreatedDate)"
    '            sSql = sSql & "Values(" & iMax & ",'" & objPC.sAcc_PCM_TransactionNo & "'," & objPC.iAcc_PCM_Party & "," & objPC.iAcc_PCM_Location & ","
    '            sSql = sSql & "" & objPC.iAcc_PCM_BillType & ",'" & objGen.SafeSQL(objPC.sAcc_PCM_BillNo) & "'," & objGen.FormatDtForRDBMS(objPC.dAcc_PCM_BillDate, "I") & "," & objPC.dAcc_PCM_BillAmount & ","

    '            If iPaymentType = 1 Then
    '                sSql = sSql & "" & objPC.dAcc_PCM_AdvanceAmount & ",'" & objGen.SafeSQL(objPC.sAcc_PCM_AdvanceNaration) & "'," & objPC.dAcc_PCM_BalanceAmount & ","
    '            ElseIf iPaymentType = 2 Then
    '                sSql = sSql & "" & objPC.iAcc_PCM_TDSType & "," & objPC.dACC_PCM_TDSDeduct & "," & objPC.dAcc_PCM_TDSAmount & ",'" & objGen.SafeSQL(objPC.sAcc_PCM_TDSNarration) & "',"
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & "" & objPC.dAcc_PCM_NetAmount & ",'" & objGen.SafeSQL(objPC.sAcc_PCM_PaymentNarration) & "',"
    '            End If

    '            sSql = sSql & "" & objPC.iAcc_PCM_CreatedBy & ",GetDate()," & objPC.iAcc_PCM_YearID & "," & iCompID & ","
    '            sSql = sSql & "'" & objPC.sAcc_PCM_Status & "','" & objPC.sAcc_PCM_Operation & "','" & objPC.sAcc_PCM_IPAddress & "',GetDate())"
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return iMax
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPC As clsPettyCash)
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Try
    '        iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(ATD_ID)+1,1) from Acc_Transactions_Details")
    '        sSql = "" : sSql = "Insert into Acc_Transactions_Details(ATD_ID,ATD_TransactionDate,ATD_TrType,"
    '        sSql = sSql & "ATD_BillId,ATD_PaymentType,ATD_Head,"
    '        sSql = sSql & "ATD_GL,ATD_SubGL,ATD_Debit,ATD_Credit,"
    '        sSql = sSql & "ATD_CreatedOn,ATD_CreatedBy,ATD_Status,"
    '        sSql = sSql & "ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress)"
    '        sSql = sSql & "Values(" & iMax & ",GetDate()," & objPC.iATD_TrType & ","
    '        sSql = sSql & "" & objPC.iATD_BillId & "," & objPC.iATD_PaymentType & "," & objPC.iATD_Head & ","
    '        sSql = sSql & "" & objPC.iATD_GL & "," & objPC.iATD_SubGL & "," & objPC.dATD_Debit & "," & objPC.dATD_Credit & ","
    '        sSql = sSql & "GetDate()," & objPC.iATD_CreatedBy & ",'" & objPC.sATD_Status & "',"
    '        sSql = sSql & "" & objPC.iATD_YearID & "," & iCompID & ",'" & objPC.sATD_Operation & "','" & objPC.sATD_IPAddress & "')"
    '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetPaymentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iBatchNo > 0 And iBaseName > 0 Then
                sSql = "" : sSql = "Select * from acc_PettyCash_Master where Acc_PCM_BatchNo =" & iBatchNo & " And Acc_PCM_BaseName=" & iBaseName & " and Acc_PCM_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Else
                sSql = "" : sSql = "Select * from acc_PettyCash_Master where Acc_PCM_ID =" & iPaymentID & " and Acc_PCM_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

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
            'sSql = sSql & "A.ATD_BillId = " & iPaymentID & " and A.ATD_TrType = 2  and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on "
            'sSql = sSql & "A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " left join acc_Opening_Balance D on "
            'sSql = sSql & "D.Opn_GLID = A.ATD_Gl and D.Opn_YearID = " & iYearID & " left join acc_Opening_Balance E on "
            'sSql = sSql & "E.Opn_GLID = A.ATD_SubGL and D.Opn_YearID = " & iYearID & " order by a.Atd_id"

            sSql = "" : sSql = "select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
            sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_TrType = 2 "
            sSql = sSql & "and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id"

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
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "0" Then
                            dr("Type") = ""
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

    Public Function GetChartOfAccountHead(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccHead As Integer = 0
        Try
            sSql = "Select gl_AccHead from Chart_of_Accounts where gl_id =" & iGL & " and gl_CompID =" & iCompID & " "
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

    Public Function GetPaymentTypeDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from acc_PettyCash_Master where Acc_PCM_ID =" & iPaymentID & " and Acc_PCM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeletePettyCashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTransactionID As Integer)
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
    Public Function UpdatePaymentMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPC As clsPettyCash)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from acc_PettyCash_Master where Acc_PCM_ID =" & objPC.iAcc_PCM_ID & " and Acc_PCM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update acc_PettyCash_Master set Acc_PCM_Party = " & objPC.iAcc_PCM_Party & ",Acc_PCM_Location=" & objPC.iAcc_PCM_Location & ","
                sSql = sSql & "Acc_PCM_BillType = " & objPC.iAcc_PCM_BillType & ",Acc_PCM_BillNo = '" & objGen.SafeSQL(objPC.sAcc_PCM_BillNo) & "',"
                sSql = sSql & "Acc_PCM_BillDate = " & objGen.FormatDtForRDBMS(objPC.dAcc_PCM_BillDate, "I") & ",Acc_PCM_BillAmount = " & objPC.dAcc_PCM_BillAmount & " "

                If iPaymentType = 1 Then
                    sSql = sSql & ",Acc_PCM_AdvanceAmount = " & objPC.dAcc_PCM_AdvanceAmount & ",Acc_PCM_AdvanceNaration = '" & objGen.SafeSQL(objPC.sAcc_PCM_AdvanceNaration) & "',Acc_PCM_BalanceAmount = " & objPC.dAcc_PCM_BalanceAmount & " "
                ElseIf iPaymentType = 2 Then
                    sSql = sSql & ",Acc_PCM_TDSType = " & objPC.iAcc_PCM_TDSType & ",ACC_PCM_TDSDeduct=" & objPC.dACC_PCM_TDSDeduct & ",Acc_PCM_TDSAmount=" & objPC.dAcc_PCM_TDSAmount & ","
                    sSql = sSql & "Acc_PCM_TDSNarration = '" & objGen.SafeSQL(objPC.sAcc_PCM_TDSNarration) & "' "
                ElseIf iPaymentType = 3 Then
                    sSql = sSql & ",Acc_PCM_NetAmount = " & objPC.dAcc_PCM_NetAmount & ",Acc_PCM_PaymentNarration = '" & objPC.sAcc_PCM_PaymentNarration & "' "
                End If
                sSql = sSql & "Where Acc_PCM_ID = " & objPC.iAcc_PCM_ID & " and Acc_PCM_CompID =" & iCompID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return objPC.iAcc_PCM_ID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal ObjPC As clsPettyCash)
        Dim sSql As String = ""
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Update Acc_Transactions_Details set ATD_Head =" & ObjPC.iATD_Head & ",ATD_GL=" & ObjPC.iATD_GL & ","
            sSql = sSql & "ATD_SubGL =" & ObjPC.iATD_SubGL & ",ATD_DbOrCr = " & ObjPC.iATD_DbOrCr & ","
            sSql = sSql & "ATD_Debit = " & ObjPC.dATD_Debit & ",ATD_Credit=" & ObjPC.dATD_Credit & " where "
            sSql = sSql & "ATD_ID =" & ObjPC.iATD_ID & " and ATD_TrType =" & ObjPC.iATD_TrType & " and ATD_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
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
    Public Function DeleteExePCTransDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPCID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Delete acc_Transactions_Details where ATD_BillId =" & iPCID & " And ATD_CompID =" & iCompID & " And ATD_TrType=2"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & "  and gl_head = 2 and gl_Delflag ='C' and gl_status='A' order by gl_glcode"
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

            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_TrType=2 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
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
                        sSql = sSql & " Where A.ATD_TrType=2 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
                        dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Else
                        sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                        sSql = sSql & " From Acc_Transactions_Details A"
                        sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
                        sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
                        sSql = sSql & " Where A.ATD_TrType=2 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
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

            sSql = "Select * From Acc_PettyCash_Master_Details Where APMD_MasterID=" & iPattyID & " And APMD_CompID=" & iCompID & " "
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = dt1.Rows(i)("APMD_ID")
                    dr("PettyID") = dt1.Rows(i)("APMD_MasterID")
                    dr("BillNo") = dt1.Rows(i)("APMD_BillNo")
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("APMD_BillDate"), "D")
                    dr("BillAmount") = dt1.Rows(i)("APMD_BillAmount")

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePettyBreakUp(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objPett As Petty)
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Try
            iMaxID = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(APMD_ID)+1,1) from Acc_PettyCash_Master_Details")
            sSql = "" : sSql = "Insert Into Acc_PettyCash_Master_Details (APMD_ID,APMD_MasterID,APMD_BillNo,APMD_BillDate,APMD_BillAmount,APMD_Status,APMD_CreatedBy,APMD_CreatedOn,APMD_CompID,APMD_YearID,APMD_Operation,APMD_IPAddress)"
            sSql = sSql & "Values(" & iMaxID & "," & objPett.iAPMD_MasterID & ",'" & objPett.sAPMD_BillNo & "'," & objGen.FormatDtForRDBMS(objPett.dAPMD_BillDate, "I") & "," & objPett.dAPMD_BillAmount & ",'" & objPett.sAPMD_Status & "'," & objPett.iAPMD_CreatedBy & "," & objGen.FormatDtForRDBMS(objPett.dAPMD_CreatedOn, "I") & "," & objPett.iAPMD_CompID & "," & objPett.iAPMD_YearID & ",'" & objPett.sAPMD_Operation & "','" & objPett.sAPMD_IPAddress & "')"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer)
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_PettyCash_Master_Details Where APMD_MasterID=" & iMasterID & " ")
            If bCheck = True Then
                sSql = "" : sSql = "Delete From Acc_PettyCash_Master_Details Where APMD_MasterID=" & iMasterID & " And APMD_CompID=" & iCompID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteExeJETransDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPCID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Delete acc_Transactions_Details where ATD_BillId =" & iPCID & " And ATD_CompID =" & iCompID & " And ATD_TrType=2"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql)
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
            sSql = "select Acc_PCM_ID,Acc_PCM_TransactionNo from Acc_PettyCash_Master where Acc_PCM_ID=" & sTrNo & " and Acc_PCM_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTransactionDetails1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer) As DataTable
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
            'sSql = sSql & "A.ATD_BillId = " & iPaymentID & " and A.ATD_TrType = 2  and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on "
            'sSql = sSql & "A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " left join acc_Opening_Balance D on "
            'sSql = sSql & "D.Opn_GLID = A.ATD_Gl and D.Opn_YearID = " & iYearID & " left join acc_Opening_Balance E on "
            'sSql = sSql & "E.Opn_GLID = A.ATD_SubGL and D.Opn_YearID = " & iYearID & " order by a.Atd_id"

            sSql = "" : sSql = "select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
            sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_TrType = 2 "
            sSql = sSql & "and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id"

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
                        ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "0" Then
                            dr("Type") = ""
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
            sSql = "select Acc_PCM_Status from Acc_PettyCash_Master where Acc_PCM_ID=" & iMasId & ""
            iID = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return iID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdatePaymentMasterStatus1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Update Acc_PettyCash_Master Set Acc_PCM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_PCM_Status='A',Acc_PCM_ApprovedBy= " & iUserID & ",Acc_PCM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_PCM_Status='D',Acc_PCM_DeletedBy= " & iUserID & ",Acc_PCM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_PCM_Status='A' "
            End If
            sSql = sSql & " Where Acc_PCM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=2 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "" : sSql = "Update Acc_Transactions_Details Set ATD_IPAddress='" & sIPAddress & "' "
                    If sStatus = "D" Then
                        sSql = sSql & " ,ATD_Status='D',ATD_DeletedBy= " & iUserID & ",ATD_DeletedOn=GetDate()"
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
            Else
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iGLID)
                End If
                dr("Debit") = 0.00
                dr("Credit") = dAmount
            End If
            dt.Rows.Add(dr)

            If dtPayment.Rows.Count > 0 Then
                dtPayment.Merge(dt, True, MissingSchemaAction.Ignore)
            Else
                dtPayment.Merge(dt)
            End If

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
            dc = New DataColumn("SrNo", GetType(String))
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
