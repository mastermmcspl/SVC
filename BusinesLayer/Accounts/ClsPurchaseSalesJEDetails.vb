Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsPurchaseSalesJEDetails
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

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
    Private Acc_JE_Type As String
    Private Acc_JE_PendingAmount As Decimal

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
    Public Property dAcc_JE_PendingAmount() As Decimal
        Get
            Return (Acc_JE_PendingAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_JE_PendingAmount = Value
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
    Public Property sAcc_JE_Type() As String
        Get
            Return (Acc_JE_Type)
        End Get
        Set(ByVal Value As String)
            Acc_JE_Type = Value
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
    Public Function GetAllSalesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sDispatchNo As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select b.SDD_ID,a.SDM_ID,a.SDM_OrderID,a.SDM_Code,a.SDM_DispatchRefNo,a.SDM_OrderDate,a.SDM_SupplierID,a.SDM_DispatchDate,a.SDM_ShippingRate,a.SDM_GrandDiscount,a.SDM_GrandDiscountAmt,b.SDD_CommodityID,b.SDD_DescID,b.SDD_Rate,b.SDD_Quantity,b.SDD_Discount,b.SDD_DiscountAmount,b.SDD_VAT,b.SDD_VATAmount,b.SDD_CST,b.SDD_CSTAmount,b.SDD_Excise,b.SDD_ExciseAmount,b.SDD_TotalAmount,
                    c.SPO_OrderCode,d.SPOD_Quantity,f.SAD_PlacedQnt,f.SAD_PendingQty,g.INV_Description As Commodity,h.INV_Code As Item,i.BM_Name As party
                    From Sales_Dispatch_Master a
                    Join Sales_Dispatch_Details b On b.SDD_MasterID=a.SDM_ID 
                    Left Join Sales_PRoForma_Order c on c.SPO_ID=a.SDM_OrderID And c.SPO_YearID=a.SDM_YearID
                    Left Join Sales_PRoForma_Order_Details d On d.SPOD_SOID = a.SDM_OrderID and d.spod_commodityid = b.sdd_CommodityID and d.spod_itemid =b.sdd_DescID and d.spod_HistoryID = b.Sdd_historyID And d.SPOD_YearID=a.SDM_YearID
                    Left Join Sales_Allocate_Master e On e.SAM_OrderNo=a.SDM_OrderID And e.SAM_ID=a.SDM_AllocateID And e.SAM_YearID=a.SDM_YearID
                    Left Join Sales_Allocate_Details f On f.SAD_MasterID=e.SAM_ID and f.sad_commodity = b.sdd_CommodityID and f.sad_Descid =b.sdd_DescID and f.sad_HisotryID = b.Sdd_historyID And f.sad_YearID=a.SDM_YearID
                    Left Join Inventory_Master g On g.INV_ID=b.SDD_CommodityID 
                    Left Join Inventory_Master h On h.INV_ID=b.SDD_DescID And h.INV_ID=d.spod_itemid
                    Left Join Sales_Buyers_Masters i On i.BM_ID=SDM_SupplierID
                    where a.SDM_YearID=" & iYearID & " And a.SDM_CompID=" & iCompID & " "

            If sDispatchNo <> "" Then
                sSql = sSql & "And SDM_ID in (" & sDispatchNo & ") "
            End If
            sSql = sSql & " order by b.SDD_ID"

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dtCheck As New DataTable
    '    Dim dtFinal As New DataTable
    '    Dim sBillNo As String = ""
    '    Dim i As Integer = 0
    '    Dim dRow As DataRow

    '    Dim sStr As String = ""
    '    Dim dtPV As New DataTable
    '    Dim sStrPV As String = ""
    '    Dim sArray As Array
    '    Dim sPVRefNo As String = ""
    '    Try
    '        dtFinal.Columns.Add("PV_OrderNo")
    '        dtFinal.Columns.Add("PV_DocRefNo")
    '        sSql = "" : sSql = "Select Acc_PJE_BillNo From Acc_Purchase_JE_Master Where Acc_PJE_YearID=" & iYearID & " And ACc_PJE_CompID=" & iCompID & " "
    '        dtPV = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        If dtPV.Rows.Count > 0 Then
    '            For k = 0 To dtPV.Rows.Count - 1
    '                sStr = dtPV.Rows(k)("Acc_PJE_BillNo")

    '                If sStr.StartsWith(",") Then
    '                    sStr = sStr.Remove(0, 1)
    '                End If
    '                If sStr.EndsWith(",") Then
    '                    sStr = sStr.Remove(Len(sStr) - 1, 1)
    '                End If
    '                sStrPV = sStrPV & "," & sStr
    '            Next

    '        End If

    '        If sStrPV.StartsWith(",") Then
    '            sStrPV = sStrPV.Remove(0, 1)
    '        End If
    '        If sStrPV.EndsWith(",") Then
    '            sStrPV = sStrPV.Remove(Len(sStrPV) - 1, 1)
    '        End If

    '        sArray = sStrPV.Split(",")
    '        For z = 0 To sArray.Length - 1
    '            sPVRefNo = sPVRefNo & ",'" & sArray(z) & "'"
    '        Next
    '        If sPVRefNo.StartsWith(",") Then
    '            sPVRefNo = sPVRefNo.Remove(0, 1)
    '        End If
    '        If sPVRefNo.EndsWith(",") Then
    '            sPVRefNo = sPVRefNo.Remove(Len(sPVRefNo) - 1, 1)
    '        End If

    '        If iParty > 0 Then
    '            sSql = "SELECT PV_OrderNo,PV_DocRefNo FROM purchase_Verification where PV_YearID=" & iYearID & " And PV_CompID = " & iCompID & " and "
    '            sSql = sSql & "PV_OrderNo in (Select POM_ID from Purchase_Order_Master where POM_Supplier =" & iParty & " And POM_YearID=" & iYearID & " And POM_CompId =" & iCompID & ") And PV_DocRefNo Not in(" & sPVRefNo & ") ORDER BY PV_DocRefNo"

    '            'Else
    '            '    sSql = "select A.PV_OrderNo,A.PV_DocRefNo from purchase_Verification A Left join Acc_Payment_Master B on A.PV_DocRefNo <> B.Acc_PM_BillNo "
    '            '    sSql = sSql & "and A.PV_CompId =" & iCompID & " order by A.PV_DocRefNo"

    '            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

    '            sSql = "" : sSql = "select * from Acc_Payment_Master where acc_pm_id in(select distinct(ATD_BillId) from Acc_Transactions_Details where ATD_TrType = 1 and ATD_PaymentType = 3)"
    '            dtCheck = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '            If dtCheck.Rows.Count > 0 Then
    '                For i = 0 To dtCheck.Rows.Count - 1
    '                    sBillNo = sBillNo & "," & dtCheck.Rows(i)("Acc_PM_BillNo").ToString()
    '                Next
    '            End If

    '            If sBillNo <> "" Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    If sBillNo.Contains(dt.Rows(i)("PV_DocRefNo").ToString()) = False Then
    '                        dRow = dtFinal.NewRow()
    '                        dRow("PV_OrderNo") = dt.Rows(i)("PV_OrderNo").ToString()
    '                        dRow("PV_DocRefNo") = dt.Rows(i)("PV_DocRefNo").ToString()
    '                        dtFinal.Rows.Add(dRow)
    '                    End If
    '                Next
    '            End If

    '            If dtFinal.Rows.Count > 0 Then
    '                Return dtFinal
    '            Else
    '                Return dt
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtCheck As New DataTable
        Dim dtFinal As New DataTable
        Dim sBillNo As String = ""
        Dim i As Integer = 0
        Dim dRow As DataRow

        Dim sStr As String = ""
        Dim dtPV As New DataTable

        Try
            dtFinal.Columns.Add("PV_OrderNo")
            dtFinal.Columns.Add("PV_DocRefNo")
            sSql = "" : sSql = "Select Acc_PJE_InvoiceID From Acc_Purchase_JE_Master Where Acc_PJE_YearID=" & iYearID & " And ACc_PJE_CompID=" & iCompID & " "
            dtPV = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtPV.Rows.Count > 0 Then
                For k = 0 To dtPV.Rows.Count - 1
                    sStr = sStr & "," & dtPV.Rows(k)("Acc_PJE_InvoiceID")
                    If sStr.StartsWith(",") Then
                        sStr = sStr.Remove(0, 1)
                    End If
                    If sStr.EndsWith(",") Then
                        sStr = sStr.Remove(Len(sStr) - 1, 1)
                    End If
                Next
            End If

            If sStr <> "" Then
                If iParty > 0 Then
                    sSql = "SELECT PV_OrderNo,PV_DocRefNo FROM purchase_Verification where PV_YearID=" & iYearID & " And PV_CompID = " & iCompID & " and "
                    sSql = sSql & "PV_OrderNo in (Select POM_ID from Purchase_Order_Master where POM_Supplier =" & iParty & " And POM_YearID=" & iYearID & " And POM_CompId =" & iCompID & ") And PV_OrderNo Not in(" & sStr & ") ORDER BY PV_DocRefNo"
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                Else
                    sSql = "SELECT PV_OrderNo,PV_DocRefNo FROM purchase_Verification where PV_YearID=" & iYearID & " And PV_CompID = " & iCompID & " and "
                    sSql = sSql & "PV_OrderNo in (Select POM_ID from Purchase_Order_Master where POM_YearID=" & iYearID & " And POM_CompId =" & iCompID & ") And PV_OrderNo Not in(" & sStr & ") ORDER BY PV_DocRefNo"
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                End If
            Else
                If iParty > 0 Then
                    sSql = "SELECT PV_OrderNo,PV_DocRefNo FROM purchase_Verification where PV_YearID=" & iYearID & " And PV_CompID = " & iCompID & " and "
                    sSql = sSql & "PV_OrderNo in (Select POM_ID from Purchase_Order_Master where POM_Supplier =" & iParty & " And POM_YearID=" & iYearID & " And POM_CompId =" & iCompID & ") ORDER BY PV_DocRefNo"
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                Else
                    sSql = "SELECT PV_OrderNo,PV_DocRefNo FROM purchase_Verification where PV_YearID=" & iYearID & " And PV_CompID = " & iCompID & " and "
                    sSql = sSql & "PV_OrderNo in (Select POM_ID from Purchase_Order_Master where POM_YearID=" & iYearID & " And POM_CompId =" & iCompID & ") ORDER BY PV_DocRefNo"
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                End If
            End If


            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSales(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iParty > 0 Then
                sSql = "Select SDM_ID,SDM_CODE From Sales_Dispatch_Master Where SDM_SupplierID=" & iParty & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Else
                sSql = "Select SDM_ID,SDM_CODE From Sales_Dispatch_Master Where SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            End If
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
    Public Function loadSalesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sOrder As String, ByVal iInvoice As Integer) As DataTable
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
            sSql = "" : sSql = "select SDM_ID,SDM_Code,f.SPO_OrderNo,f.SPO_OrderDate,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_InvoiceDate,PV_CompID,PV_Status,PV_BillNo,"
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
            sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID where PIA_CompID=" & iCompID & ""
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
                If IsDBNull(dtDetails.Rows(i)("SDM_Commodity")) = False Then
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
    Public Function GetPaymentTypeDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer, ByVal sExiJV As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sExiJV.StartsWith("P") Then
                sSql = "" : sSql = "Select Acc_PJE_TransactionNo As TrNo,Acc_PJE_Party As Party,Acc_PJE_Location As Location,Acc_PJE_BillType As BillType,Acc_PJE_BillNo As BillNo,Acc_PJE_BillDate As BillDate,Acc_PJE_BillAmount As BillAmount,Acc_PJE_BalanceAmount As BalanceAmount,Acc_PJE_AdvanceNaration As AdvanceNaration,Acc_PJE_PaymentNarration As PaymentNarration,Acc_PJE_AdvanceAmount As AdvanceAmount,Acc_PJE_Status As Status,ACC_PJE_ChequeNo As ChequeNo,Acc_PJE_ChequeDate As ChequeDate,Acc_PJE_IFSCCode As IFSCCODE,ACC_PJE_BankName As BankName,Acc_PJE_BranchName As BranchName,Acc_PJE_Type As Type,Acc_PJE_InvoiceID As InvoiceID from Acc_Purchase_JE_Master where Acc_PJE_ID =" & iPaymentID & " And Acc_PJE_YearID=" & iYearID & " and Acc_PJE_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            ElseIf sExiJV.StartsWith("S") Then
                sSql = "" : sSql = "Select Acc_SJE_TransactionNo As TrNo,Acc_SJE_Party As Party,Acc_SJE_Location As Location,Acc_SJE_BillType As BillType,Acc_SJE_BillNo As BillNo,Acc_SJE_BillDate As BillDate,Acc_SJE_BillAmount As BillAmount,Acc_SJE_BalanceAmount As BalanceAmount,Acc_SJE_AdvanceNaration As AdvanceNaration,Acc_SJE_PaymentNarration As PaymentNarration,Acc_SJE_AdvanceAmount As AdvanceAmount,Acc_SJE_Status As Status,ACC_SJE_ChequeNo As ChequeNo,Acc_SJE_ChequeDate As ChequeDate,Acc_SJE_IFSCCode As IFSCCODE,ACC_SJE_BankName As BankName,Acc_SJE_BranchName As BranchName,Acc_SJE_Type As Type,Acc_SJE_InvoiceID As InvoiceID from Acc_Sales_JE_Master where Acc_SJE_ID =" & iPaymentID & " And Acc_SJE_YearID=" & iYearID & " and Acc_SJE_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            End If
            Return dt
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
                'sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
                'sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
                'sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 5 and "
                'sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID  and A.ATD_YearID=" & iYearID & " And A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "

                sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit
                        from Acc_Transactions_Details A where A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 5 and A.ATD_YearID=" & iYearID & " order by a.Atd_id "
            ElseIf sExiJV.StartsWith("S") Then
                'sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
                'sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
                'sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 6 and "
                'sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_YearID=" & iYearID & " and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "

                sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit
                        from Acc_Transactions_Details A where A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 6 and A.ATD_YearID=" & iYearID & " order by a.Atd_id "
            End If

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

                    'If IsDBNull(ds.Tables(0).Rows(i)("GLCode").ToString()) = False Then
                    '    dr("GLCode") = ds.Tables(0).Rows(i)("GLCode").ToString()
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_GL").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ATD_GL") > 0 Then
                            dr("GLCode") = objDBL.SQLGetDescription(sNameSpace, "Select gl_glCode From Chart_Of_Accounts Where gl_id=" & ds.Tables(0).Rows(i)("ATD_GL") & " ")
                        Else
                            dr("GLCode") = ""
                        End If
                    Else
                        dr("GLCode") = ""
                    End If


                    'If IsDBNull(ds.Tables(0).Rows(i)("GLDescription").ToString()) = False Then
                    '    dr("GLDescription") = ds.Tables(0).Rows(i)("GLDescription").ToString()
                    '    'If IsDBNull(ds.Tables(0).Rows(i)("GLDebit").ToString()) = False Then
                    '    '    If ds.Tables(0).Rows(i)("GLDebit").ToString() <> "0.00" Then
                    '    '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("GLDebit").ToString()
                    '    '    End If
                    '    'End If

                    '    'If IsDBNull(ds.Tables(0).Rows(i)("GLCredit").ToString()) = False Then
                    '    '    If ds.Tables(0).Rows(i)("GLCredit").ToString() <> "0.00" Then
                    '    '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("GLCredit").ToString()
                    '    '    End If
                    '    'End If
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_GL").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ATD_GL") > 0 Then
                            dr("GLDescription") = objDBL.SQLGetDescription(sNameSpace, "Select gl_Desc From Chart_Of_Accounts Where gl_id=" & ds.Tables(0).Rows(i)("ATD_GL") & " ")
                        Else
                            dr("GLDescription") = ""
                        End If
                    Else
                        dr("GLDescription") = ""
                    End If


                    'If IsDBNull(ds.Tables(0).Rows(i)("SubGLCode").ToString()) = False Then
                    '    dr("SubGL") = ds.Tables(0).Rows(i)("SubGLCode").ToString()
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_SubGL").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ATD_SubGL") > 0 Then
                            dr("SubGL") = objDBL.SQLGetDescription(sNameSpace, "Select gl_glCode From Chart_Of_Accounts Where gl_id=" & ds.Tables(0).Rows(i)("ATD_SubGL") & " ")
                        Else
                            dr("SubGL") = ""
                        End If
                    Else
                        dr("SubGL") = ""
                    End If


                    'If IsDBNull(ds.Tables(0).Rows(i)("SubGLDesc").ToString()) = False Then
                    '    dr("SubGLDescription") = ds.Tables(0).Rows(i)("SubGLDesc").ToString()

                    '    'If IsDBNull(ds.Tables(0).Rows(i)("SubGLDebit").ToString()) = False Then
                    '    '    If ds.Tables(0).Rows(i)("SubGLDebit").ToString() <> "0.00" Then
                    '    '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("SubGLDebit").ToString()
                    '    '    End If
                    '    'End If

                    '    'If IsDBNull(ds.Tables(0).Rows(i)("SubGLCredit").ToString()) = False Then
                    '    '    If ds.Tables(0).Rows(i)("SubGLCredit").ToString() <> "0.00" Then
                    '    '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("SubGLCredit").ToString()
                    '    '    End If
                    '    'End If
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_SubGL").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ATD_SubGL") > 0 Then
                            dr("SubGLDescription") = objDBL.SQLGetDescription(sNameSpace, "Select gl_Desc From Chart_Of_Accounts Where gl_id=" & ds.Tables(0).Rows(i)("ATD_SubGL") & " ")
                        Else
                            dr("SubGLDescription") = ""
                        End If
                    Else
                        dr("SubGLDescription") = ""
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Debit").ToString()) = False Then
                        dr("Debit") = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("ATD_Debit").ToString()))
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Credit").ToString()) = False Then
                        dr("Credit") = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables(0).Rows(i)("ATD_Credit").ToString()))
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function SavePurchaseJournalMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objJE As ClsPurchaseSalesJE) As Integer
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from Acc_Purchase_JE_Master where Acc_PJE_ID =" & objJE.iAcc_JE_ID & " and Acc_PJE_CompID =" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        If dt.Rows.Count > 0 Then
    '            sSql = "" : sSql = "Update Acc_Purchase_JE_Master set Acc_PJE_Party = " & objJE.iAcc_JE_Party & ",Acc_PJE_Location=" & objJE.iAcc_JE_Location & ","
    '            sSql = sSql & "Acc_PJE_BillType = " & objJE.iAcc_JE_BillType & ",Acc_PJE_BillNo = '" & objGen.SafeSQL(objJE.sAcc_JE_BillNo) & "',"
    '            sSql = sSql & "Acc_PJE_BillDate = " & objGen.FormatDtForRDBMS(objJE.dAcc_JE_BillDate, "I") & ",Acc_PJE_BillAmount = " & objJE.dAcc_JE_BillAmount & " "

    '            If iPaymentType = 1 Then
    '                sSql = sSql & ",Acc_PJE_AdvanceAmount = " & objJE.dAcc_JE_AdvanceAmount & ",Acc_PJE_AdvanceNaration = '" & objGen.SafeSQL(objJE.sAcc_JE_AdvanceNaration) & "',Acc_PJE_BalanceAmount = " & objJE.dAcc_JE_BalanceAmount & " "
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & ",Acc_PJE_NetAmount = " & objJE.dAcc_JE_NetAmount & ",Acc_PJE_PaymentNarration = '" & objJE.sAcc_JE_PaymentNarration & "' "
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & ",Acc_PJE_ChequeNo = " & objJE.sAcc_JE_ChequeNo & ","
    '                sSql = sSql & "Acc_PJE_ChequeDate = " & objGen.FormatDtForRDBMS(Acc_JE_ChequeDate, "I") & ",Acc_PJE_IFSCCode = '" & objJE.sAcc_JE_IFSCCode & "',"
    '                sSql = sSql & "Acc_PJE_BankName = '" & objGen.SafeSQL(objJE.sAcc_JE_BankName) & "',Acc_PJE_BranchName = '" & objGen.SafeSQL(objJE.sAcc_JE_BranchName) & "' "
    '            End If
    '            sSql = sSql & "Where Acc_PJE_ID = " & objJE.iAcc_JE_ID & " and Acc_PJE_CompID =" & iCompID & ""
    '            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '            Return objJE.iAcc_JE_ID
    '        Else
    '            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_PJE_ID)+1,1) from Acc_Purchase_JE_Master")
    '            sSql = "" : sSql = "Insert into Acc_Purchase_JE_Master(Acc_PJE_ID,Acc_PJE_TransactionNo,Acc_PJE_Party,Acc_PJE_Location,"
    '            sSql = sSql & "Acc_PJE_BillType,Acc_PJE_BillNo,Acc_PJE_BillDate,Acc_PJE_BillAmount,"

    '            If iPaymentType = 1 Then
    '                sSql = sSql & "Acc_PJE_AdvanceAmount,Acc_PJE_AdvanceNaration,Acc_PJE_BalanceAmount,"
    '            ElseIf iPaymentType = 2 Then
    '                sSql = sSql & "Acc_PJE_TDSType,ACC_PJE_TDSDeduct,Acc_PJE_TDSAmount,Acc_PJE_TDSNarration,"
    '            ElseIf iPaymentType = 3 Then
    '                sSql = sSql & "Acc_PJE_NetAmount,Acc_PJE_PaymentNarration,"
    '            ElseIf iPaymentType = 4 Then
    '                sSql = sSql & "Acc_PJE_ChequeNo,Acc_PJE_ChequeDate,Acc_PJE_IFSCCode,Acc_PJE_BankName,Acc_PJE_BranchName,"
    '            End If

    '            sSql = sSql & "Acc_PJE_CreatedBy,Acc_PJE_CreatedOn,Acc_PJE_YearID,Acc_PJE_CompID,"
    '            sSql = sSql & "Acc_PJE_Status,Acc_PJE_Operation,Acc_PJE_IPAddress,Acc_PJE_BillCreatedDate)"
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
    Public Function SavePurchaseJournalMaster(ByVal sNameSpace As String, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(31) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_TransactionNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_Party)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_Location)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_BillType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objGen.SafeSQL(objPSJEDetails.sAcc_JE_BillNo))
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_BillDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_BillAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_AdvanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_AdvanceNaration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_AdvanceNaration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BalanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_BalanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_NetAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_NetAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PaymentNarration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_PaymentNarration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeNo", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_ChequeNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_ChequeDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IFSCCode", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_IFSCCode)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BankName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_BankName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BranchName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_BranchName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_CreatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_BillCreatedDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_BillCreatedDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_UpdatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_UpdatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_InvoiceID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_InvoiceID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_PJE_PendingAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_PendingAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Purchase_JE_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSalesJournalMaster(ByVal sNameSpace As String, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(31) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_TransactionNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_TransactionNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_Party)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_Location)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_BillType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objGen.SafeSQL(objPSJEDetails.sAcc_JE_BillNo))
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_BillDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_BillAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_AdvanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_AdvanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_AdvanceNaration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_AdvanceNaration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BalanceAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_BalanceAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_NetAmount", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_NetAmount)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_PaymentNarration", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_PaymentNarration)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_ChequeNo", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_ChequeNo)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_ChequeDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_IFSCCode", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_IFSCCode)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BankName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_BankName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BranchName", OleDb.OleDbType.VarChar, 20000)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_BranchName)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_CreatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_IPAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_BillCreatedDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dAcc_JE_BillCreatedDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_UpdatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_UpdatedOn)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_InvoiceID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iAcc_JE_InvoiceID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_SJE_Type", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sAcc_JE_Type)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Sales_JE_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails)
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Try
    '        iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(ATD_ID)+1,1) from Acc_Transactions_Details")
    '        sSql = "" : sSql = "Insert into Acc_Transactions_Details(ATD_ID,ATD_TransactionDate,ATD_TrType,"
    '        sSql = sSql & "ATD_BillId,ATD_PaymentType,ATD_Head,"
    '        sSql = sSql & "ATD_GL,ATD_SubGL,ATD_Debit,ATD_Credit,"
    '        sSql = sSql & "ATD_CreatedOn,ATD_CreatedBy,ATD_Status,"
    '        sSql = sSql & "ATD_YearID,ATD_CompID,ATD_Operation,ATD_IPAddress)"
    '        sSql = sSql & "Values(" & iMax & ",GetDate()," & objPSJEDetails.iATD_TrType & ","
    '        sSql = sSql & "" & objPSJEDetails.iATD_BillId & "," & objPSJEDetails.iATD_PaymentType & "," & objPSJEDetails.iATD_Head & ","
    '        sSql = sSql & "" & objPSJEDetails.iATD_GL & "," & objPSJEDetails.iATD_SubGL & "," & objPSJEDetails.dATD_Debit & "," & objPSJEDetails.dATD_Credit & ","
    '        sSql = sSql & "" & objPSJEDetails.iATD_CreatedOn & ",GetDate(),'" & objPSJEDetails.sATD_Status & "',"
    '        sSql = sSql & "" & objPSJEDetails.iATD_YearID & "," & iCompID & ",'" & objPSJEDetails.sATD_Operation & "','" & objPSJEDetails.sATD_IPAddress & "')"
    '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function UpdateTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails)
    '    Dim sSql As String = ""
    '    Dim iMax As Integer = 0
    '    Try
    '        sSql = "" : sSql = "Update Acc_Transactions_Details set ATD_Head =" & objPSJEDetails.iATD_Head & ",ATD_GL=" & objPSJEDetails.iATD_GL & ","
    '        sSql = sSql & "ATD_SubGL =" & objPSJEDetails.iATD_SubGL & ","
    '        sSql = sSql & "ATD_Debit = " & objPSJEDetails.dATD_Debit & ",ATD_Credit=" & objPSJEDetails.dATD_Credit & " where "
    '        sSql = sSql & "ATD_ID =" & objPSJEDetails.iATD_ID & " and ATD_TrType =" & objPSJEDetails.iATD_TrType & " and ATD_CompID = " & iCompID & ""
    '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objGen.SafeSQL(objPSJEDetails.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPSJEDetails.dATD_OpenDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPSJEDetails.dATD_OpenCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPSJEDetails.dATD_ClosingDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPSJEDetails.dATD_ClosingCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPSJEDetails.iATD_SeqReferenceNum
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


    Public Function UpdatePaymentMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from acc_JE_Master where Acc_JE_ID =" & objPSJEDetails.iAcc_JE_ID & " and Acc_JE_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update acc_JE_Master set Acc_JE_Party = " & objPSJEDetails.iAcc_JE_Party & ",Acc_JE_Location=" & objPSJEDetails.iAcc_JE_Location & ","
                sSql = sSql & "Acc_JE_BillType = " & objPSJEDetails.iAcc_JE_BillType & ",Acc_JE_BillNo = '" & objGen.SafeSQL(objPSJEDetails.sAcc_JE_BillNo) & "',"
                sSql = sSql & "Acc_JE_BillDate = " & objGen.FormatDtForRDBMS(objPSJEDetails.dAcc_JE_BillDate, "I") & ",Acc_JE_BillAmount = " & objPSJEDetails.dAcc_JE_BillAmount & " "

                If iPaymentType = 1 Then
                    sSql = sSql & ",Acc_JE_AdvanceAmount = " & objPSJEDetails.dAcc_JE_AdvanceAmount & ",Acc_JE_AdvanceNaration = '" & objGen.SafeSQL(objPSJEDetails.sAcc_JE_AdvanceNaration) & "',Acc_JE_BalanceAmount = " & objPSJEDetails.dAcc_JE_BalanceAmount & " "
                ElseIf iPaymentType = 3 Then
                    sSql = sSql & ",Acc_JE_NetAmount = " & objPSJEDetails.dAcc_JE_NetAmount & ",Acc_JE_PaymentNarration = '" & objPSJEDetails.sAcc_JE_PaymentNarration & "' "
                ElseIf iPaymentType = 4 Then
                    sSql = sSql & ",Acc_JE_ChequeNo = " & objPSJEDetails.sAcc_JE_ChequeNo & ","
                    sSql = sSql & "Acc_JE_ChequeDate = " & objGen.FormatDtForRDBMS(objPSJEDetails.Acc_JE_ChequeDate, "I") & ",Acc_JE_IFSCCode = '" & objPSJEDetails.sAcc_JE_IFSCCode & "',"
                    sSql = sSql & "Acc_JE_BankName = '" & objGen.SafeSQL(objPSJEDetails.sAcc_JE_BankName) & "',Acc_JE_BranchName = '" & objGen.SafeSQL(objPSJEDetails.sAcc_JE_BranchName) & "' "
                End If
                sSql = sSql & "Where Acc_JE_ID = " & objPSJEDetails.iAcc_JE_ID & " and Acc_JE_CompID =" & iCompID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Return objPSJEDetails.iAcc_JE_ID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveChequeDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objPSJEDetails As ClsPurchaseSalesJEDetails)
        Dim sSql As String = ""
        Try
            sSql = "Update acc_JE_Master set ACC_JE_ChequeNo = " & objPSJEDetails.sAcc_JE_ChequeNo & ",Acc_JE_ChequeDate=" & objGen.FormatDtForRDBMS(objPSJEDetails.dAcc_JE_ChequeDate, "I") & ","
            sSql = sSql & "Acc_JE_IFSCCode = '" & objGen.SafeSQL(objPSJEDetails.sAcc_JE_IFSCCode) & "',ACC_JE_BankName='" & objGen.SafeSQL(objPSJEDetails.sAcc_JE_BankName) & "',"
            sSql = sSql & "Acc_JE_BranchName = '" & objGen.SafeSQL(objPSJEDetails.sAcc_JE_BranchName) & "' where Acc_JE_ID=" & objPSJEDetails.iAcc_JE_ID & " and Acc_JE_CompID =" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPaymentTypeID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer, ByVal sExiJV As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sExiJV.StartsWith("P") Then
                sSql = "" : sSql = "SElect ATD_PaymentType From Acc_Transactions_Details where ATD_trType=5 And ATD_YearID=" & iYearID & " And ATD_BillId =" & iPaymentID & " and ATD_CompID =" & iCompID & ""
                GetPaymentTypeID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            ElseIf sExiJV.StartsWith("S") Then
                sSql = "" : sSql = "SElect ATD_PaymentType From Acc_Transactions_Details where ATD_trType=6 And ATD_YearID=" & iYearID & " And ATD_BillId =" & iPaymentID & " and ATD_CompID =" & iCompID & ""
                GetPaymentTypeID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return GetPaymentTypeID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHeadID As Integer, ByVal iGLID As Integer) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Try
            sSql = "Select gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iHeadID & " And gl_Id=" & iGLID & "  order by gl_glcode"
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
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_id=" & iSubGL & "  and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartySubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As String
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            sSql = "" : sSql = "Select BM_SubGL From sales_Buyers_Masters Where BM_ID=" & iPartyID & "  And BM_Delflag='A' and BM_CompID =" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_id='" & sPartyName & "' and gl_head=3"
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
            sSql = "" : sSql = "Select BM_SubGL From sales_Buyers_Masters Where BM_ID=" & iPartyID & "  And BM_Delflag='A' and BM_CompID =" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_id='" & sPartyName & "' and gl_head=3"
            GetPartySubGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPartySubGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPurchasePartyID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoiceID As Integer) As Integer
        Dim sSql As String = ""
        Dim iPartyID As Integer
        Try
            'sSql = sSql & "Select ACM_ID From Acc_Customer_Master Where ACM_Type='S' And ACM_ID in (Select POM_Supplier from Purchase_Order_Master,purchase_Verification where POM_ID=PV_OrderNo And POM_ID=" & iInvoiceID & " And POM_YearID=" & iYearID & " And POM_CompId =" & iCompID & ")  "
            sSql = "" : sSql = "Select CSM_ID from CustomerSupplierMaster where CSM_ID in(Select POM_Supplier from Purchase_Order_Master,purchase_Verification where POM_ID=PV_OrderNo And POM_ID=" & iInvoiceID & " And POM_YearID=" & iYearID & " And POM_CompId =" & iCompID & ")"
            iPartyID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iPartyID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSalesPartyID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoiceID As Integer) As Integer
        Dim sSql As String = ""
        Dim iPartyID As Integer
        Try
            'sSql = sSql & "Select ACM_ID From Acc_Customer_Master Where ACM_Type='C' And ACM_ID in (Select SDM_SupplierID from Sales_Dispatch_Master where SDM_ID=" & iInvoiceID & " And SDM_YearID=" & iYearID & " And SDM_CompId =" & iCompID & ")  "
            sSql = "" : sSql = "Select BM_ID from sales_Buyers_Masters where BM_ID in(Select SDM_SupplierID from Sales_Dispatch_Master where SDM_ID=" & iInvoiceID & " And SDM_YearID=" & iYearID & " And SDM_CompId =" & iCompID & ")"
            iPartyID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iPartyID
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetAccountDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String, ByVal sColumn As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from Acc_Application_Settings where Acc_Types = '" & sType & "' and "
            sSql = sSql & " Acc_LedgerType ='" & sLedgerType & "' and Acc_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sBillNo As String, ByVal sExiJV As String, ByVal sType As String, ByVal iInvoiceID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SrNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PurchaseInvoiceNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Item", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ItemCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ItemBasic", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GST", GetType(String))
            dt.Columns.Add(dc)


            If sExiJV.StartsWith("P") Then
                If sType = "GR" Then
                    sSql = "Select * From Goods_Return_Details Where GRD_MasterID in (Select GRM_ID From Goods_Return_Master Where GRM_ReturnNo='" & sBillNo & "' And GRM_CompID=" & iCompID & " And GRM_YearID=" & iYearID & ")"
                    ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            dr = dt.NewRow

                            If IsDBNull(ds.Tables(0).Rows(i)("GRD_DescriptionID").ToString()) = False Then
                                dr("Id") = ds.Tables(0).Rows(i)("GRD_DescriptionID").ToString()
                            End If

                            dr("SrNo") = i + 1

                            dr("PurchaseInvoiceNo") = objDBL.SQLGetDescription(sNameSpace, "Select GRM_GINInvNo From Goods_Return_Master Where GRM_ReturnNo='" & sBillNo & "' And GRM_CompID=" & iCompID & " And GRM_YearID=" & iYearID & "")

                            If IsDBNull(ds.Tables(0).Rows(i)("GRD_DescriptionID").ToString()) = False Then
                                dr("Item") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("GRD_DescriptionID").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("GRD_DescriptionID").ToString()) = False Then
                                dr("ItemCode") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("GRD_DescriptionID").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("GRD_Amount").ToString()) = False Then
                                dr("ItemBasic") = ds.Tables(0).Rows(i)("GRD_Amount").ToString()
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("GRD_GSTRate").ToString()) = False Then
                                If ds.Tables(0).Rows(i)("GRD_GSTRate") > 0 Then
                                    dr("GST") = ds.Tables(0).Rows(i)("GRD_GSTRate")
                                Else
                                    dr("GST") = ""
                                End If
                            Else
                                dr("GST") = ""
                            End If

                            dt.Rows.Add(dr)
                        Next
                    End If
                ElseIf sType = "CP" Then
                    sSql = "Select * From Purchase_Order_Details Where POD_MasterID=" & iInvoiceID & " And POD_CompID=" & iCompID & " "
                    ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            dr = dt.NewRow

                            If IsDBNull(ds.Tables(0).Rows(i)("POD_DescriptionID").ToString()) = False Then
                                dr("Id") = ds.Tables(0).Rows(i)("POD_DescriptionID").ToString()
                            End If

                            dr("SrNo") = i + 1

                            dr("PurchaseInvoiceNo") = sBillNo

                            If IsDBNull(ds.Tables(0).Rows(i)("POD_DescriptionID").ToString()) = False Then
                                dr("Item") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("POD_DescriptionID").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("POD_DescriptionID").ToString()) = False Then
                                dr("ItemCode") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("POD_DescriptionID").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("POD_TotalAmount").ToString()) = False Then
                                dr("ItemBasic") = (ds.Tables(0).Rows(i)("POD_TotalAmount") - ds.Tables(0).Rows(i)("POD_GSTAmount"))
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("POD_GSTRate").ToString()) = False Then
                                If ds.Tables(0).Rows(i)("POD_GSTRate") > 0 Then
                                    dr("GST") = ds.Tables(0).Rows(i)("POD_GSTRate")
                                Else
                                    dr("GST") = ""
                                End If
                            Else
                                dr("GST") = ""
                            End If

                            dt.Rows.Add(dr)
                        Next
                    End If

                ElseIf sType = "PI" Then
                    sSql = "Select * From PI_Accepted_Details Where PID_MasterID in (Select PV_InvoiceID From Purchase_Verification Where PV_BillNo='" & sBillNo & "' And PV_CompID=" & iCompID & " And PV_YearID=" & iYearID & ")"
                    ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            dr = dt.NewRow

                            If IsDBNull(ds.Tables(0).Rows(i)("PID_DescID").ToString()) = False Then
                                dr("Id") = ds.Tables(0).Rows(i)("PID_DescID").ToString()
                            End If

                            dr("SrNo") = i + 1

                            dr("PurchaseInvoiceNo") = objDBL.SQLGetDescription(sNameSpace, "Select PGM_DocumentRefNo From Purchase_GIN_Master Where PGM_ID in (Select PV_GinNo from purchase_Verification Where PV_BillNo='" & sBillNo & "' And PV_CompID=" & iCompID & " And PV_YearID=" & iYearID & ") ")

                            If IsDBNull(ds.Tables(0).Rows(i)("PID_DescID").ToString()) = False Then
                                dr("Item") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("PID_DescID").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("PID_DescID").ToString()) = False Then
                                dr("ItemCode") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("PID_DescID").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("PID_Amount").ToString()) = False Then
                                dr("ItemBasic") = ds.Tables(0).Rows(i)("PID_Amount").ToString()
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("PID_GSTRate").ToString()) = False Then
                                If ds.Tables(0).Rows(i)("PID_GSTRate") > 0 Then
                                    dr("GST") = ds.Tables(0).Rows(i)("PID_GSTRate")
                                Else
                                    dr("GST") = ""
                                End If
                            Else
                                dr("GST") = ""
                            End If

                            dt.Rows.Add(dr)
                        Next
                    End If
                End If

            ElseIf sExiJV.StartsWith("S") Then

                If sType = "SR" Then
                    sSql = "Select * from Sales_ReturnDetails A where SRD_MasterID in (Select Sales_Return_ID From Sales_Return_Masters Where Sales_Return_InvoiceNo=" & iInvoiceID & " And Sales_Return_CompID=" & iCompID & " And Sales_Return_Year=" & iYearID & ") "

                    ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            dr = dt.NewRow

                            If IsDBNull(ds.Tables(0).Rows(i)("SRD_Item").ToString()) = False Then
                                dr("Id") = ds.Tables(0).Rows(i)("SRD_Item").ToString()
                            End If

                            dr("SrNo") = i + 1

                            dr("PurchaseInvoiceNo") = ""
                            objDBL.SQLGetDescription(sNameSpace, "Select PGM_DocumentRefNo From Purchase_GIN_Master Where PGM_ID in (Select PV_GinNo from purchase_Verification Where PV_BillNo='" & sBillNo & "' And PV_CompID=" & iCompID & " And PV_YearID=" & iYearID & ") ")

                            If IsDBNull(ds.Tables(0).Rows(i)("SRD_Item").ToString()) = False Then
                                dr("Item") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("SRD_Item").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("SRD_Item").ToString()) = False Then
                                dr("ItemCode") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("SRD_Item").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("SRD_Amount").ToString()) = False Then
                                dr("ItemBasic") = ds.Tables(0).Rows(i)("SRD_Amount").ToString()
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("SRD_GSTRate").ToString()) = False Then
                                If ds.Tables(0).Rows(i)("SRD_GSTRate") > 0 Then
                                    dr("GST") = ds.Tables(0).Rows(i)("SRD_GSTRate")
                                Else
                                    dr("GST") = ""
                                End If
                            Else
                                dr("GST") = ""
                            End If

                            dt.Rows.Add(dr)
                        Next
                    End If

                Else
                    sSql = "Select * from Sales_Dispatch_Details A where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_Code='" & sBillNo & "' And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
                    ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            dr = dt.NewRow

                            If IsDBNull(ds.Tables(0).Rows(i)("SDD_DescID").ToString()) = False Then
                                dr("Id") = ds.Tables(0).Rows(i)("SDD_DescID").ToString()
                            End If

                            dr("SrNo") = i + 1

                            dr("PurchaseInvoiceNo") = ""
                            objDBL.SQLGetDescription(sNameSpace, "Select PGM_DocumentRefNo From Purchase_GIN_Master Where PGM_ID in (Select PV_GinNo from purchase_Verification Where PV_BillNo='" & sBillNo & "' And PV_CompID=" & iCompID & " And PV_YearID=" & iYearID & ") ")

                            If IsDBNull(ds.Tables(0).Rows(i)("SDD_DescID").ToString()) = False Then
                                dr("Item") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("SDD_DescID").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("SDD_DescID").ToString()) = False Then
                                dr("ItemCode") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & ds.Tables(0).Rows(i)("SDD_DescID").ToString() & " ")
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("SDD_Amount").ToString()) = False Then
                                dr("ItemBasic") = ds.Tables(0).Rows(i)("SDD_Amount").ToString()
                            End If

                            If IsDBNull(ds.Tables(0).Rows(i)("SDD_GSTRate").ToString()) = False Then
                                If ds.Tables(0).Rows(i)("SDD_GSTRate") > 0 Then
                                    dr("GST") = ds.Tables(0).Rows(i)("SDD_GSTRate")
                                Else
                                    dr("GST") = ""
                                End If
                            Else
                                dr("GST") = ""
                            End If

                            dt.Rows.Add(dr)
                        Next
                    End If
                End If

            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateJEMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal iPS As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        'Dim sSql As String = ""
        'Dim dt As New DataTable
        'Try
        '    If iPS = 0 Then 'Purchase
        '        sSql = "Update Acc_Purchase_JE_Master Set Acc_PJE_IPAddress='" & sIPAddress & "',"
        '        If sStatus = "W" Then
        '            sSql = sSql & " Acc_PJE_Status='A',Acc_PJE_ApprovedBy= " & iUserID & ",Acc_PJE_ApprovedOn=GetDate()"
        '        End If
        '        sSql = sSql & " Where Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " And Acc_PJE_ID = " & iMasId & ""
        '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        '        dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=5 And ATD_YearID =" & iYearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
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

        '    ElseIf iPS = 1 Then 'Sales
        '        sSql = "Update Acc_Sales_JE_Master Set Acc_SJE_IPAddress='" & sIPAddress & "',"
        '        If sStatus = "W" Then
        '            sSql = sSql & " Acc_SJE_Status='A',Acc_SJE_ApprovedBy= " & iUserID & ",Acc_SJE_ApprovedOn=GetDate()"
        '        End If
        '        sSql = sSql & " Where Acc_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " And Acc_SJE_ID = " & iMasId & ""
        '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        '        dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=6 And ATD_YearID =" & iYearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
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

        '    End If
        'Catch ex As Exception
        '    Throw
        'End Try


        Dim sSql As String = ""
        Dim dt, dtDebitCredit As New DataTable
        Dim dOpnDebit, dOpnCredit, dClosingDebit, dClosingCredit As Double
        Dim iSequenceNum As Integer
        Try
            If iPS = 0 Then 'Purchase
                sSql = "" : sSql = "Update Acc_Purchase_JE_Master Set Acc_PJE_IPAddress='" & sIPAddress & "',"
                If sStatus = "W" Then
                    sSql = sSql & " Acc_PJE_Status='A',Acc_PJE_ApprovedBy= " & iUserID & ",Acc_PJE_ApprovedOn=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " Acc_PJE_Status='D',Acc_PJE_DeletedBy= " & iUserID & ",Acc_PJE_DeletedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " Acc_PJE_Status='A' "
                End If
                sSql = sSql & " Where Acc_PJE_ID = " & iMasId & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)


                dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=5 And ATD_YearID =" & iYearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sSql = "" : sSql = "Select Count(Atd_id) from Acc_Transactions_Details where atd_gl=" & dt.Rows(i)("ATD_GL") & " and ATD_YearID =" & iYearID & " and ATD_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0"
                        Dim iCountGl As Integer = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                        sSql = "" : sSql = "Select Count(Atd_id) from Acc_Transactions_Details where  ATD_YearID =" & iYearID & " and ATD_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0"
                        iSequenceNum = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                        iSequenceNum = iSequenceNum + 1
                        If iCountGl = 0 Then

                            sSql = "" : sSql = "Select Opn_CreditAmount,Opn_DebitAmt from acc_opening_balance where Opn_glId=" & dt.Rows(i)("ATD_GL") & " and Opn_YearID =" & iYearID & " and Opn_CompID=" & iCompID & ""
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
                            sSql = "" : sSql = "Select top 1  Atd_ClosingCredit,ATD_ClosingDebit from Acc_Transactions_Details where atd_gl=" & dt.Rows(i)("ATD_GL") & " and Atd_YearID =" & iYearID & " and Atd_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0 order by ATD_SeqReferenceNum desc"
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
            ElseIf iPS = 1 Then 'Sales
                sSql = "" : sSql = "Update Acc_sales_JE_Master Set Acc_SJE_IPAddress='" & sIPAddress & "',"
                If sStatus = "W" Then
                    sSql = sSql & " Acc_SJE_Status='A',Acc_SJE_ApprovedBy= " & iUserID & ",Acc_SJE_ApprovedOn=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " Acc_SJE_Status='D',Acc_SJE_DeletedBy= " & iUserID & ",Acc_SJE_DeletedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " Acc_SJE_Status='A' "
                End If
                sSql = sSql & " Where Acc_SJE_ID = " & iMasId & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)


                dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=6 And ATD_YearID =" & iYearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sSql = "" : sSql = "Select Count(Atd_id) from Acc_Transactions_Details where atd_gl=" & dt.Rows(i)("ATD_GL") & " and ATD_YearID =" & iYearID & " and ATD_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0"
                        Dim iCountGl As Integer = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                        sSql = "" : sSql = "Select Count(Atd_id) from Acc_Transactions_Details where  ATD_YearID =" & iYearID & " and ATD_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0"
                        iSequenceNum = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                        iSequenceNum = iSequenceNum + 1
                        If iCountGl = 0 Then

                            sSql = "" : sSql = "Select Opn_CreditAmount,Opn_DebitAmt from acc_opening_balance where Opn_glId=" & dt.Rows(i)("ATD_GL") & " and Opn_YearID =" & iYearID & " and Opn_CompID=" & iCompID & ""
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
                            sSql = "" : sSql = "Select top 1  Atd_ClosingCredit,ATD_ClosingDebit from Acc_Transactions_Details where atd_gl=" & dt.Rows(i)("ATD_GL") & " and Atd_YearID =" & iYearID & " and Atd_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0 order by ATD_SeqReferenceNum desc"
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
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal iPS As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        Try
            If iPS = 0 Then 'Purchase
                sSql = "Select Acc_PJE_Status from Acc_Purchase_JE_Master Where Acc_PJE_CompID=" & iCompID & " And Acc_PJE_YearID=" & iYearID & " And Acc_PJE_ID = " & iMasId & " "
                GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
            ElseIf iPS = 1 Then 'Sales
                sSql = "Select Acc_SJE_Status From Acc_Sales_JE_Master  Where Acc_SJE_CompID=" & iCompID & " And Acc_SJE_YearID=" & iYearID & " And Acc_SJE_ID = " & iMasId & " "
                GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
            End If
            Return GetStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
