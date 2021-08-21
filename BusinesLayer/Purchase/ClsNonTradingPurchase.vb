Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsNonTradingPurchase
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Public Structure NonTradingDetails
        Dim iAcc_P_ID As Integer
        Dim iAcc_P_MasterID As Integer
        Dim sAcc_P_BillNo As String
        Dim dAcc_P_BillDate As DateTime
        Dim dAcc_P_Discount As Decimal
        Dim dAcc_P_DiscountAmt As Decimal
        Dim dAcc_P_OtherCharges As Decimal
        Dim dAcc_P_Amount As Decimal
        Dim dAcc_P_GST As Decimal
        Dim dAcc_P_GSTAmt As Decimal
        Dim dAcc_P_SGST As Decimal
        Dim dAcc_P_SGSTAmt As Decimal
        Dim dAcc_P_CGST As Decimal
        Dim dAcc_P_CGSTAmt As Decimal
        Dim dAcc_P_IGST As Decimal
        Dim dAcc_P_IGSTAmt As Decimal
        Dim dAcc_P_BillAmount As Decimal
        Dim iAcc_P_Year As Integer
        Dim iAcc_P_CompID As Integer
        Dim sAcc_P_Status As String
        Dim sAcc_P_DelFlag As String
        Dim dAcc_P_PendingAmount As Decimal
        Dim iAcc_P_CrBy As Integer
        Dim dAcc_P_CrOn As DateTime
        Dim sAcc_P_Operation As String
        Dim sAcc_P_IPAddress As String

        Dim iATD_ID As Integer
        Dim dATD_TransactionDate As Date
        Dim iATD_TrType As Integer
        Dim iATD_BillID As Integer
        Dim iATD_PaymentType As Integer
        Dim iATD_Head As Integer
        Dim iATD_GL As Integer
        Dim iATD_SubGL As Integer
        Dim iATD_DbOrCr As Integer
        Dim dATD_Debit As Decimal
        Dim dATD_Credit As Decimal
        Dim iATD_CreatedBy As Integer
        Dim dATD_CreatedOn As Date
        Dim iATD_UpdatedBy As Integer
        Dim dATD_UpdatedOn As Date
        Dim sATD_Status As String
        Dim iATD_YearID As Integer
        Dim iATD_CompID As Integer
        Dim sATD_Operation As String
        Dim sATD_IPAddress As String
    End Structure

    Private iC_ID As Integer
    Private iC_TRID As Integer
    Private sC_TRType As String
    Private iC_ChargeID As Integer
    Private sC_ChargeType As String
    Private iC_ChargeAmount As Double
    Private sC_DelFlag As String
    Private sC_Status As String
    Private iC_YearID As Integer
    Private iC_CompID As Integer
    Private iC_CreatedBy As Integer
    Private iC_CreatedOn As Date
    Private sC_Operation As String
    Private sC_IPAddress As String

    Private Acc_Purchase_ID As Integer
    Private Acc_Purchase_TransactionNo As String
    Private Acc_Purchase_TransactionDate As DateTime
    Private Acc_Purchase_Type As Integer
    Private Acc_Purchase_Party As Integer
    Private Acc_Purchase_BillNo As String
    Private Acc_Purchase_BillDate As Date
    Private Acc_Purchase_BillAmount As Decimal
    Private Acc_Purchase_CreatedBy As Integer
    Private Acc_Purchase_CreatedOn As Date
    Private Acc_Purchase_UpdatedBy As Integer
    Private Acc_Purchase_UpdatedOn As Date
    Private Acc_Purchase_ApprovedBy As Integer
    Private Acc_Purchase_ApprovedOn As Date
    Private Acc_Purchase_DeletedBy As Integer
    Private Acc_Purchase_DeletedOn As Date
    Private Acc_Purchase_RecalledBy As Integer
    Private Acc_Purchase_RecalledOn As Date
    Private Acc_Purchase_Year As Integer
    Private Acc_Purchase_CompID As Integer
    Private Acc_Purchase_Status As String
    Private Acc_Purchase_DelFlag As String
    Private Acc_Purchase_Operation As String
    Private Acc_Purchase_IPAddress As String
    Private Acc_Purchase_PaymentType As Integer
    Private Acc_Purchase_OtherCharges As Decimal
    Private Acc_Purchase_TradeDiscount As Decimal
    Private ACC_Purchase_ZoneID As Integer
    Private ACC_Purchase_RegionID As Integer
    Private ACC_Purchase_AreaID As Integer
    Private ACC_Purchase_BranchID As Integer

    Private sACC_Purchase_CompanyAddress As String
    Private sACC_Purchase_CompanyGSTNRegNo As String
    Private sACC_Purchase_BillingAddress As String
    Private sACC_Purchase_BillingGSTNRegNo As String
    Private sACC_Purchase_DeliveryFrom As String
    Private sACC_Purchase_DeliveryFromGSTNRegNo As String
    Private sACC_Purchase_ReceiveAddress As String
    Private sACC_Purchase_ReceiveGSTNRegNo As String
    Private sACC_Purchase_InvoiceStatus As String
    Private iACC_Purchase_CompanyType As Integer
    Private iACC_Purchase_GSTNCategory As Integer
    Private sAcc_Purchase_State As String
    Private Acc_Purchase_PendingAmount As Decimal
    Private Acc_Purchase_ChequeNo As String
    Private Acc_Purchase_ChequeDate As Date
    Private Acc_Purchase_IFSCCode As String
    Private Acc_Purchase_BankName As String
    Private Acc_Purchase_BranchName As String

    Public Property C_ID() As Integer
        Get
            Return (iC_ID)
        End Get
        Set(ByVal Value As Integer)
            iC_ID = Value
        End Set
    End Property
    Public Property C_TRID() As Integer
        Get
            Return (iC_TRID)
        End Get
        Set(ByVal Value As Integer)
            iC_TRID = Value
        End Set
    End Property
    Public Property C_TRType() As String
        Get
            Return (sC_TRType)
        End Get
        Set(ByVal Value As String)
            sC_TRType = Value
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


    Public Property sAcc_Purchase_BranchName() As String
        Get
            Return (Acc_Purchase_BranchName)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_BranchName = Value
        End Set
    End Property
    Public Property sAcc_Purchase_BankName() As String
        Get
            Return (Acc_Purchase_BankName)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_BankName = Value
        End Set
    End Property

    Public Property sAcc_Purchase_IFSCCode() As String
        Get
            Return (Acc_Purchase_IFSCCode)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_IFSCCode = Value
        End Set
    End Property
    Public Property dAcc_Purchase_ChequeDate() As Date
        Get
            Return (Acc_Purchase_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_ChequeDate = Value
        End Set
    End Property
    Public Property sAcc_Purchase_ChequeNo() As String
        Get
            Return (Acc_Purchase_ChequeNo)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_ChequeNo = Value
        End Set
    End Property

    Public Property dAcc_Purchase_PendingAmount() As Decimal
        Get
            Return (Acc_Purchase_PendingAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_Purchase_PendingAmount = Value
        End Set
    End Property
    Public Property Acc_Purchase_CompanyAddress() As String
        Get
            Return (sACC_Purchase_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_CompanyAddress = Value
        End Set
    End Property
    Public Property Acc_Purchase_CompanyGSTNRegNo() As String
        Get
            Return (sACC_Purchase_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property Acc_Purchase_BillingAddress() As String
        Get
            Return (sACC_Purchase_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_BillingAddress = Value
        End Set
    End Property
    Public Property Acc_Purchase_BillingGSTNRegNo() As String
        Get
            Return (sACC_Purchase_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property Acc_Purchase_DeliveryFrom() As String
        Get
            Return (sACC_Purchase_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_DeliveryFrom = Value
        End Set
    End Property
    Public Property Acc_Purchase_DeliveryFromGSTNRegNo() As String
        Get
            Return (sACC_Purchase_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_DeliveryFromGSTNRegNo = Value
        End Set
    End Property
    Public Property Acc_Purchase_ReceiveAddress() As String
        Get
            Return (sACC_Purchase_ReceiveAddress)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_ReceiveAddress = Value
        End Set
    End Property
    Public Property Acc_Purchase_ReceiveGSTNRegNo() As String
        Get
            Return (sACC_Purchase_ReceiveGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_ReceiveGSTNRegNo = Value
        End Set
    End Property
    Public Property Acc_Purchase_InvoiceStatus() As String
        Get
            Return (sACC_Purchase_InvoiceStatus)
        End Get
        Set(ByVal Value As String)
            sACC_Purchase_InvoiceStatus = Value
        End Set
    End Property
    Public Property Acc_Purchase_State() As String
        Get
            Return (sAcc_Purchase_State)
        End Get
        Set(ByVal Value As String)
            sAcc_Purchase_State = Value
        End Set
    End Property
    Public Property Acc_Purchase_CompanyType() As Integer
        Get
            Return (iACC_Purchase_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iACC_Purchase_CompanyType = Value
        End Set
    End Property
    Public Property Acc_Purchase_GSTNCategory() As Integer
        Get
            Return (iACC_Purchase_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iACC_Purchase_GSTNCategory = Value
        End Set
    End Property


    Public Property iACC_Purchase_ZoneID() As Integer
        Get
            Return (ACC_Purchase_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            ACC_Purchase_ZoneID = Value
        End Set
    End Property
    Public Property iACC_Purchase_RegionID() As Integer
        Get
            Return (ACC_Purchase_RegionID)
        End Get
        Set(ByVal Value As Integer)
            ACC_Purchase_RegionID = Value
        End Set
    End Property
    Public Property iACC_Purchase_AreaID() As Integer
        Get
            Return (ACC_Purchase_AreaID)
        End Get
        Set(ByVal Value As Integer)
            ACC_Purchase_AreaID = Value
        End Set
    End Property
    Public Property iACC_Purchase_BranchID() As Integer
        Get
            Return (ACC_Purchase_BranchID)
        End Get
        Set(ByVal Value As Integer)
            ACC_Purchase_BranchID = Value
        End Set
    End Property
    Public Property dAcc_Purchase_OtherCharges() As Decimal
        Get
            Return (Acc_Purchase_OtherCharges)
        End Get
        Set(ByVal Value As Decimal)
            Acc_Purchase_OtherCharges = Value
        End Set
    End Property
    Public Property dAcc_Purchase_TradeDiscount() As Decimal
        Get
            Return (Acc_Purchase_TradeDiscount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_Purchase_TradeDiscount = Value
        End Set
    End Property
    Public Property sAcc_Purchase_IPAddress() As String
        Get
            Return (Acc_Purchase_IPAddress)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_IPAddress = Value
        End Set
    End Property
    Public Property sAcc_Purchase_Operation() As String
        Get
            Return (Acc_Purchase_Operation)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_Operation = Value
        End Set
    End Property
    Public Property sAcc_Purchase_DelFlag() As String
        Get
            Return (Acc_Purchase_DelFlag)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_DelFlag = Value
        End Set
    End Property
    Public Property sAcc_Purchase_Status() As String
        Get
            Return (Acc_Purchase_Status)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_Status = Value
        End Set
    End Property
    Public Property iAcc_Purchase_CompID() As Integer
        Get
            Return (Acc_Purchase_CompID)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_CompID = Value
        End Set
    End Property
    Public Property iAcc_Purchase_Year() As Integer
        Get
            Return (Acc_Purchase_Year)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_Year = Value
        End Set
    End Property
    Public Property dAcc_Purchase_RecalledOn() As Date
        Get
            Return (Acc_Purchase_RecalledOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_RecalledOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_RecalledBy() As Integer
        Get
            Return (Acc_Purchase_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_RecalledBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_DeletedOn() As Date
        Get
            Return (Acc_Purchase_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_DeletedOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_DeletedBy() As Integer
        Get
            Return (Acc_Purchase_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_DeletedBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_ApprovedOn() As Date
        Get
            Return (Acc_Purchase_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_ApprovedOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_ApprovedBy() As Integer
        Get
            Return (Acc_Purchase_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_ApprovedBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_UpdatedOn() As Date
        Get
            Return (Acc_Purchase_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_UpdatedOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_UpdatedBy() As Integer
        Get
            Return (Acc_Purchase_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_UpdatedBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_CreatedOn() As Date
        Get
            Return (Acc_Purchase_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_CreatedOn = Value
        End Set
    End Property
    Public Property iAcc_Purchase_CreatedBy() As Integer
        Get
            Return (Acc_Purchase_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_CreatedBy = Value
        End Set
    End Property
    Public Property dAcc_Purchase_BillAmount() As Decimal
        Get
            Return (Acc_Purchase_BillAmount)
        End Get
        Set(ByVal Value As Decimal)
            Acc_Purchase_BillAmount = Value
        End Set
    End Property
    Public Property dAcc_Purchase_BillDate() As Date
        Get
            Return (Acc_Purchase_BillDate)
        End Get
        Set(ByVal Value As Date)
            Acc_Purchase_BillDate = Value
        End Set
    End Property
    Public Property sAcc_Purchase_BillNo() As String
        Get
            Return (Acc_Purchase_BillNo)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_BillNo = Value
        End Set
    End Property
    Public Property iAcc_Purchase_Party() As Integer
        Get
            Return (Acc_Purchase_Party)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_Party = Value
        End Set
    End Property
    Public Property iAcc_Purchase_Type() As Integer
        Get
            Return (Acc_Purchase_Type)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_Type = Value
        End Set
    End Property
    Public Property sAcc_Purchase_TransactionNo() As String
        Get
            Return (Acc_Purchase_TransactionNo)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_TransactionNo = Value
        End Set
    End Property
    Public Property dAcc_Purchase_TransactionDate() As DateTime
        Get
            Return (Acc_Purchase_TransactionDate)
        End Get
        Set(ByVal Value As DateTime)
            Acc_Purchase_TransactionDate = Value
        End Set
    End Property
    Public Property iAcc_Purchase_ID() As Integer
        Get
            Return (Acc_Purchase_ID)
        End Get
        Set(ByVal Value As Integer)
            Acc_Purchase_ID = Value
        End Set
    End Property
    Public Property iAcc_Purchase_PaymentType() As String
        Get
            Return (Acc_Purchase_PaymentType)
        End Get
        Set(ByVal Value As String)
            Acc_Purchase_PaymentType = Value
        End Set
    End Property
    Public Function GetDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Acc_NonTrading_Purchase_Details Where Acc_P_MasterID=" & iMasterID & " And Acc_P_CompID=" & iCompID & " And Acc_P_Year=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNonTradingMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPurchaseID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Acc_NonTrading_Purchase_Master where Acc_Purchase_ID =" & iPurchaseID & " And Acc_Purchase_Year=" & iYearID & " And Acc_Purchase_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingPurchases(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Acc_Purchase_ID,Acc_Purchase_TransactionNo From Acc_NonTrading_Purchase_Master Where Acc_Purchase_Year=" & iYearID & " And Acc_Purchase_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBranchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_Name=" & iBranchID & " And CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name From CustomerSupplierMaster Where CSM_CompID=" & iCompID & " "
            LoadSuppliers = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return LoadSuppliers
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplierID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From CustomerSupplierMaster Where CSM_ID=" & iSupplierID & " And CSM_CompID=" & iCompID & " "
            GetCustomerDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCustomerDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Acc_Purchase_ID)+1,1) from Acc_NonTrading_Purchase_Master")
            sPrefix = "NT-P" & "00" & iMax
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyGSTNRegNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From MST_Customer_Master Where CUST_ID=" & iCompID & " "
            GetCompanyGSTNRegNo = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCompanyGSTNRegNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGstCategory As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select GC_GSTCategory from GSTcategory_table where GC_ID=" & iGstCategory & " and GC_CompID=" & iCompID & ""
            GetGSTDescription = objDBL.SQLGetDescription(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGSTCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "select Mas_Id,Mas_Desc from Acc_General_Master where Mas_Master in(Select Mas_ID From Acc_Master_Type Where Mas_Type='Category Of TaxPayer') And Mas_Status='A' and Mas_CompID =" & iCompID & " "
            sSql = "Select GC_ID,GC_GSTCategory From GSTCategory_Table Where GC_CompanyType='" & sCompanyType & "' And GC_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 2 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranches(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where Org_Parent=1 And org_CompID=" & iCompID & ")))"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getBranchFromPO(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPodID As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select ACC_Purchase_BranchID From Acc_NonTrading_Purchase_Master where Acc_Purchase_TransactionNo='" & sPodID & "' and Acc_Purchase_CompID=" & iCompID & ""
            getBranchFromPO = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDetailsofBranchState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPodID As String) As String
        Dim sSql As String = ""
        Dim iPOMBranchID As Integer : Dim iCompBrnchID As Integer
        Try
            sSql = "Select ACC_Purchase_BranchID From Acc_NonTrading_Purchase_Master where Acc_Purchase_TransactionNo='" & sPodID & "' and Acc_Purchase_CompID=" & iCompID & ""
            iPOMBranchID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select CUSTB_STATE from MST_CUSTOMER_MASTER_Branch where CUSTB_Name='" & iPOMBranchID & "' and CUSTB_CompID=" & iCompID & ""
            iCompBrnchID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select Mas_desc From ACC_General_Master Where Mas_Id = " & iCompBrnchID & " And Mas_master=4 and Mas_CompID = " & iCompID & ""
            CheckDetailsofBranchState = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDetailsofCompState(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = ""
        Dim iStateID As Integer
        Try
            sSql = "Select CUST_COMM_STATE from MST_Customer_Master where CUST_ID = " & iCompID & " and CUST_CompID =" & iCompID & " "
            iStateID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select Mas_desc From ACC_General_Master Where Mas_Id = " & iStateID & " And Mas_master=4 and Mas_CompID = " & iCompID & ""
            CheckDetailsofCompState = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SElect GL_ID From Chart_Of_Accounts Where GL_Desc Like '%" & sDesc & "%' And GL_CompID=" & iCompID & " "
            GetGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindGSTRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & iCompID & " "
            BindGSTRates = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindGSTRates
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGSTNRegNo As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select GR_StateName From GSTN_RegNo_Master Where GR_TIN='" & sGSTNRegNo & "' And GR_CompID=" & iCompID & " "
            GetState = objDBL.SQLGetDescription(sNameSpace, sSql).ToString
            Return GetState
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseVoucher(ByVal sNameSpace As String, ByVal iCompID As Integer, objPurchase As ClsNonTradingPurchase)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(40) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_TransactionNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_BillNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_BillDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_BillAmount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_BillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_Year", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_Year
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_Purchase_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_OtherCharges", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_OtherCharges
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iACC_Purchase_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iACC_Purchase_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iACC_Purchase_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPurchase.iACC_Purchase_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_CompanyAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_BillingAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_BillingAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_BillingGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_BillingGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_DeliveryFrom", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_DeliveryFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_DeliveryFromGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_DeliveryFromGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_ReceiveAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_ReceiveAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_ReceiveGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPurchase.sACC_Purchase_ReceiveGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_InvoiceStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.Acc_Purchase_InvoiceStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.Acc_Purchase_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.Acc_Purchase_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACC_Purchase_State", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objPurchase.Acc_Purchase_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_PendingAmount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_PendingAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_ChequeNo", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_ChequeDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_Purchase_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_IFSCCode ", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_BankName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_Purchase_BranchName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPurchase.sAcc_Purchase_BranchName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_NonTrading_Purchase_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objPurchase As NonTradingDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_P_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_P_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_BillNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPurchase.sAcc_P_BillNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_BillDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_BillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_Discount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_Discount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_DiscountAmt", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_DiscountAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_OtherCharges", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_OtherCharges
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_Amount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_GST", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_GST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_GSTAmt", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_GSTAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_SGST", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_SGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_SGSTAmt", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_SGSTAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_CGST", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_CGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_CGSTAmt", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_CGSTAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_IGST", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_IGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_IGSTAmt", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_IGSTAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_BillAmount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_BillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_Year", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_P_Year
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_P_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPurchase.sAcc_P_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPurchase.sAcc_P_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_PendingAmount", OleDb.OleDbType.Double, 500)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_PendingAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iAcc_P_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_CrOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPurchase.dAcc_P_CrOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.sAcc_P_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Acc_P_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.sAcc_P_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_NonTrading_Purchase_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SubmitTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPTrID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_NonTrading_Purchase_Master Set Acc_Purchase_Status='S' Where Acc_Purchase_ID=" & iPTrID & " And Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAHeadID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_AccHead from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
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
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
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
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_id=" & iSubGL & " and gl_head=3"
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
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDb.SQLGetDescription(sNameSpace, sSql)

            sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & iPartyID & " And CSM_CompID=" & iCompID & " and CSM_Delflag='A' "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            GetPartySubGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPartySubGLID
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
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
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
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
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
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objPurchase As ClsNonTradingPurchase.NonTradingDetails)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_BillID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPurchase.dATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPurchase.sATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.iATD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.sATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.sATD_IPAddress
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

            If sExiJV.StartsWith("NT-P") Then
                sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
                sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
                sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 10 and "
                sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_YearID=" & iYearID & " And A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "
            ElseIf sExiJV.StartsWith("NT-S") Then
                sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
                sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
                sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 11 and "
                sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_YearID=" & iYearID & " and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "
            End If
            'sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
            'sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            'sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_trType = 4 and "
            'sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id "

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
    Public Sub UpdatePurchaseVoucherStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_NonTrading_Purchase_Master Set Acc_Purchase_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " Acc_Purchase_DelFlag='A',Acc_Purchase_Status ='A',Acc_Purchase_ApprovedBy= " & iUserID & ",Acc_Purchase_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " Acc_Purchase_DelFlag='D',Acc_Purchase_Status='D',Acc_Purchase_DeletedBy= " & iUserID & ",Acc_Purchase_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " Acc_Purchase_DelFlag='A',Acc_Purchase_Status='A' "
            End If
            sSql = sSql & " Where Acc_Purchase_ID = " & iMasId & " and Acc_Purchase_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
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

            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_TrType=10 And ATD_BillID=" & iMasId & " And ATD_CompID=" & iCompID & " And ATD_YearID=" & iYearID & " ").Tables(0)
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
                        sSql = sSql & " Where A.ATD_TrType=10 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_SubGL=" & iTrGLID & " And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & ""
                        dtValues = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Else
                        sSql = "" : sSql = "Select A.ATD_Debit,A.ATD_Credit,B.Opn_DebitAmt,B.Opn_CreditAmount,C.ALM_TrDebit,C.ALM_TrCredit"
                        sSql = sSql & " From Acc_Transactions_Details A"
                        sSql = sSql & " Left Join Acc_Opening_Balance B On B.Opn_AccHead=A.ATD_Head And B.Opn_GLID=A.ATD_GL"
                        sSql = sSql & " Join Acc_Ledger_Masters C On C.ALM_AccountType=A.ATD_Head And C.ALM_GL=A.ATD_GL"
                        sSql = sSql & " Where A.ATD_TrType=10 And A.ATD_BillID=" & iMasId & " And A.ATD_Head=" & iTrAccHead & " And A.ATD_GL=" & iTrGLID & " And A.ATD_SubGL=0 And A.ATD_CompID=" & iCompID & " And A.ATD_YearID=" & iYearID & " And C.ALM_Head=" & iTrHead & " "
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
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Acc_Purchase_Status From Acc_NonTrading_Purchase_Master Where Acc_Purchase_ID = " & iMasId & " and Acc_Purchase_CompID=" & iCompID & " And Acc_Purchase_Year=" & iYearID & " "
            GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatus
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
    Public Function DeleteCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTRID As Integer, ByVal sTRType As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Delete From ACC_Charges_Master Where C_TRID=" & iTRID & " And C_TRType='" & sTRType & "' And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAccCharges(ByVal sNameSpace As String, ByVal objPurchase As ClsNonTradingPurchase) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_TRID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_TRID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_TRType", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objPurchase.C_TRType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_ChargeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeType", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objPurchase.C_ChargeType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objPurchase.C_ChargeAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.C_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPurchase.C_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPurchase.C_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPurchase.C_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objPurchase.C_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPurchase.C_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_Charges_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function RemoveDublicate(ByVal dt As DataTable) As DataTable
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
    Public Function BindChargeData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTRID As Integer, ByVal sTRType As String) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("ChargeID")
            dtTab.Columns.Add("ChargeType")
            dtTab.Columns.Add("ChargeAmount")

            If iTRID > 0 Then
                sSql = "" : sSql = "Select * From ACC_Charges_Master Where C_TRID=" & iTRID & " And C_TRType='" & sTRType & "' And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ChargeID") = dt.Rows(i)("C_ChargeID")
                    dRow("ChargeType") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt.Rows(i)("C_ChargeID") & " And Mas_Master=24 And Mas_CompID = " & iCompID & "  ")
                    dRow("ChargeAmount") = dt.Rows(i)("C_ChargeAmount")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChargeType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=24 And Mas_DelFlag='A' "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
