Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsCashSales
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iSPO_ID As Integer
    Private sSPO_OrderCode As String
    Private dSPO_OrderDate As DateTime
    Private sSPO_PartyCode As String
    Private sSPO_PartyName As String
    Private sSPO_Address As String
    Private sSPO_ContantNo As String
    Private iSPO_ModeOfDispatch As Integer
    Private dSPO_ShippingDate As DateTime
    Private iSPO_PaymentType As Integer
    Private sSPO_Status As String
    Private iSPO_CompID As Integer
    Private iSPO_CreatedBy As Integer
    Private dSPO_CreatedOn As DateTime
    Private iSPO_ModeOfCommunication As Integer
    Private sSPO_InputBy As String
    Private iSPO_ApprovedBy As Integer
    Private dSPO_ApprovedOn As DateTime
    Private iSPO_ShippingCharge As Integer
    Private sSPO_OrderType As String
    Private iSPO_Category As Integer
    Private sSPO_Remarks As String
    Private dSPO_BuyerOrderDate As DateTime

    Private iSPO_SalesType As Integer
    Private iSPO_OtherType As Integer
    Private sSPO_ChequeNo As String
    Private dSPO_ChequeDate As DateTime
    Private sSPO_IFSCCode As String
    Private sSPO_BankName As String
    Private sSPO_Branch As String
    Private iSPO_GoThroughDispatch As Integer
    Private sSPO_ESugamNo As String
    Private sSPO_DispatchRefNo As String
    Private dSPO_DispatchDate As DateTime

    Private iSPO_ZoneID As Integer
    Private iSPO_RegionID As Integer
    Private iSPO_AreaID As Integer
    Private iSPO_BranchID As Integer

    Private iSPO_TrType As Integer
    Private sSPO_CompanyAddress As String
    Private sSPO_CompanyGSTNRegNo As String
    Private sSPO_BillingAddress As String
    Private sSPO_BillingGSTNRegNo As String
    Private sSPO_DeliveryAddress As String
    Private sSPO_DeliveryGSTNRegNo As String
    Private sSPO_DeliveryFrom As String
    Private sSPO_DeliveryFromGSTNRegNo As String
    Private sSPO_DispatchStatus As String
    Private iSPO_CompanyType As Integer
    Private iSPO_GSTNCategory As Integer
    Private sSPO_State As String

    Private iSPOD_Id As Integer
    Private iSPOD_SOID As Integer
    Private iSPOD_CommodityID As Integer
    Private iSPOD_ItemID As Integer
    Private iSPOD_Quantity As Double
    Private iSPOD_Discount As Double
    Private iSPOD_UnitofMeasurement As Integer
    Private iSPOD_RateAmount As Double
    Private iSPOD_DiscountRate As Double
    Private iSPOD_CompiD As Integer
    Private sSPOD_Status As String
    Private iSPOD_HistoryID As Integer
    Private iSPOD_MRPRate As Double
    Private iSPOD_TotalAmount As Double
    Private iSPOD_VAT As Double
    Private iSPOD_VATAmount As Double
    Private sSPO_Operation As String
    Private sSPO_IPAddress As String
    Private sSPOD_Operation As String
    Private sSPOD_IPAddress As String
    Private iSPOD_CST As Double
    Private iSPOD_CSTAmount As Double
    Private iSPOD_Excise As Double
    Private iSPOD_ExciseAmount As Double
    Private sSPO_DispatchFlag As String
    Private iSPO_SalesManID As Integer
    Private sSPO_BuyerOrderNo As String
    Private iSPOD_Category As Integer

    Private iSPOD_CreatedBy As Integer
    Private dSPOD_CreatedOn As DateTime
    Private iSPOD_UpdatedBy As Integer
    Private dSPOD_UpdatedOn As DateTime

    Private sSPOD_ChargesPeritem As Double
    Private sSPOD_Amount As Double

    Private iSPOD_GST_ID As Integer
    Private sSPOD_GSTRate As Double
    Private sSPOD_GSTAmount As Double

    Private iSPOD_SGST As Double
    Private sSPOD_SGSTAmount As String
    Private iSPOD_CGST As Double
    Private sSPOD_CGSTAmount As String
    Private iSPOD_IGST As Double
    Private sSPOD_IGSTAmount As String

    Private iSPO_BatchNo As Integer
    Private iSPO_BaseName As Integer
    Public Property SPO_BatchNo() As Integer
        Get
            Return (iSPO_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iSPO_BatchNo = Value
        End Set
    End Property
    Public Property SPO_BaseName() As Integer
        Get
            Return (iSPO_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iSPO_BaseName = Value
        End Set
    End Property
    Public Property SPOD_ChargesPeritem() As Double
        Get
            Return (sSPOD_ChargesPeritem)
        End Get
        Set(ByVal Value As Double)
            sSPOD_ChargesPeritem = Value
        End Set
    End Property
    Public Property SPOD_Amount() As Double
        Get
            Return (sSPOD_Amount)
        End Get
        Set(ByVal Value As Double)
            sSPOD_Amount = Value
        End Set
    End Property
    Public Property SPO_ZoneID() As Integer
        Get
            Return (iSPO_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            iSPO_ZoneID = Value
        End Set
    End Property
    Public Property SPO_RegionID() As Integer
        Get
            Return (iSPO_RegionID)
        End Get
        Set(ByVal Value As Integer)
            iSPO_RegionID = Value
        End Set
    End Property
    Public Property SPO_AreaID() As Integer
        Get
            Return (iSPO_AreaID)
        End Get
        Set(ByVal Value As Integer)
            iSPO_AreaID = Value
        End Set
    End Property
    Public Property SPO_BranchID() As Integer
        Get
            Return (iSPO_BranchID)
        End Get
        Set(ByVal Value As Integer)
            iSPO_BranchID = Value
        End Set
    End Property
    Public Property SPOD_GST_ID() As Integer
        Get
            Return (iSPOD_GST_ID)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_GST_ID = Value
        End Set
    End Property
    Public Property SPOD_GSTRate() As Double
        Get
            Return (sSPOD_GSTRate)
        End Get
        Set(ByVal Value As Double)
            sSPOD_GSTRate = Value
        End Set
    End Property
    Public Property SPOD_GSTAmount() As Double
        Get
            Return (sSPOD_GSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSPOD_GSTAmount = Value
        End Set
    End Property

    Public Property SPOD_SGST() As Double
        Get
            Return (iSPOD_SGST)
        End Get
        Set(ByVal Value As Double)
            iSPOD_SGST = Value
        End Set
    End Property
    Public Property SPOD_SGSTAmount() As Double
        Get
            Return (sSPOD_SGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSPOD_SGSTAmount = Value
        End Set
    End Property
    Public Property SPOD_CGST() As Double
        Get
            Return (iSPOD_CGST)
        End Get
        Set(ByVal Value As Double)
            iSPOD_CGST = Value
        End Set
    End Property
    Public Property SPOD_CGSTAmount() As Double
        Get
            Return (sSPOD_CGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSPOD_CGSTAmount = Value
        End Set
    End Property
    Public Property SPOD_IGST() As Double
        Get
            Return (iSPOD_IGST)
        End Get
        Set(ByVal Value As Double)
            iSPOD_IGST = Value
        End Set
    End Property
    Public Property SPOD_IGSTAmount() As Double
        Get
            Return (sSPOD_IGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sSPOD_IGSTAmount = Value
        End Set
    End Property

    Public Property SPO_TrType() As Integer
        Get
            Return (iSPO_TrType)
        End Get
        Set(ByVal Value As Integer)
            iSPO_TrType = Value
        End Set
    End Property
    Public Property SPO_CompanyAddress() As String
        Get
            Return (sSPO_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sSPO_CompanyAddress = Value
        End Set
    End Property
    Public Property SPO_CompanyGSTNRegNo() As String
        Get
            Return (sSPO_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sSPO_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property SPO_BillingAddress() As String
        Get
            Return (sSPO_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sSPO_BillingAddress = Value
        End Set
    End Property
    Public Property SPO_BillingGSTNRegNo() As String
        Get
            Return (sSPO_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sSPO_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property SPO_DeliveryFrom() As String
        Get
            Return (sSPO_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sSPO_DeliveryFrom = Value
        End Set
    End Property
    Public Property SPO_DeliveryFromGSTNRegNo() As String
        Get
            Return (sSPO_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sSPO_DeliveryFromGSTNRegNo = Value
        End Set
    End Property

    Public Property SPO_DeliveryAddress() As String
        Get
            Return (sSPO_DeliveryAddress)
        End Get
        Set(ByVal Value As String)
            sSPO_DeliveryAddress = Value
        End Set
    End Property
    Public Property SPO_DeliveryGSTNRegNo() As String
        Get
            Return (sSPO_DeliveryGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sSPO_DeliveryGSTNRegNo = Value
        End Set
    End Property
    Public Property SPO_DispatchStatus() As String
        Get
            Return (sSPO_DispatchStatus)
        End Get
        Set(ByVal Value As String)
            sSPO_DispatchStatus = Value
        End Set
    End Property
    Public Property SPO_State() As String
        Get
            Return (sSPO_State)
        End Get
        Set(ByVal Value As String)
            sSPO_State = Value
        End Set
    End Property
    Public Property SPO_CompanyType() As Integer
        Get
            Return (iSPO_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iSPO_CompanyType = Value
        End Set
    End Property
    Public Property SPO_GSTNCategory() As Integer
        Get
            Return (iSPO_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iSPO_GSTNCategory = Value
        End Set
    End Property


    Public Property SPO_DispatchDate() As DateTime
        Get
            Return (dSPO_DispatchDate)
        End Get
        Set(ByVal Value As DateTime)
            dSPO_DispatchDate = Value
        End Set
    End Property
    Public Property SPO_ESugamNo() As String
        Get
            Return (sSPO_ESugamNo)
        End Get
        Set(ByVal Value As String)
            sSPO_ESugamNo = Value
        End Set
    End Property
    Public Property SPO_DispatchRefNo() As String
        Get
            Return (sSPO_DispatchRefNo)
        End Get
        Set(ByVal Value As String)
            sSPO_DispatchRefNo = Value
        End Set
    End Property
    Public Property SPO_BuyerOrderDate() As DateTime
        Get
            Return (dSPO_BuyerOrderDate)
        End Get
        Set(ByVal Value As DateTime)
            dSPO_BuyerOrderDate = Value
        End Set
    End Property
    Public Property SPO_Remarks() As String
        Get
            Return (sSPO_Remarks)
        End Get
        Set(ByVal Value As String)
            sSPO_Remarks = Value
        End Set
    End Property
    Public Property SPO_Category() As Integer
        Get
            Return (iSPO_Category)
        End Get
        Set(ByVal Value As Integer)
            iSPO_Category = Value
        End Set
    End Property
    Public Property SPOD_Category() As Integer
        Get
            Return (iSPOD_Category)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_Category = Value
        End Set
    End Property
    Public Property SPO_BuyerOrderNo() As String
        Get
            Return (sSPO_BuyerOrderNo)
        End Get
        Set(ByVal Value As String)
            sSPO_BuyerOrderNo = Value
        End Set
    End Property
    Public Property SPO_SalesManID() As Integer
        Get
            Return (iSPO_SalesManID)
        End Get
        Set(ByVal Value As Integer)
            iSPO_SalesManID = Value
        End Set
    End Property
    Public Property SPO_DispatchFlag() As String
        Get
            Return (sSPO_DispatchFlag)
        End Get
        Set(ByVal Value As String)
            sSPO_DispatchFlag = Value
        End Set
    End Property

    Public Property SPO_OrderType() As String
        Get
            Return (sSPO_OrderType)
        End Get
        Set(ByVal Value As String)
            sSPO_OrderType = Value
        End Set
    End Property
    Public Property SPO_Operation() As String
        Get
            Return (sSPO_Operation)
        End Get
        Set(ByVal Value As String)
            sSPO_Operation = Value
        End Set
    End Property
    Public Property SPO_IPAddress() As String
        Get
            Return (sSPO_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSPO_IPAddress = Value
        End Set
    End Property

    Public Property SPOD_Operation() As String
        Get
            Return (sSPOD_Operation)
        End Get
        Set(ByVal Value As String)
            sSPOD_Operation = Value
        End Set
    End Property
    Public Property SPOD_IPAddress() As String
        Get
            Return (sSPOD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSPOD_IPAddress = Value
        End Set
    End Property


    Public Property SPOD_VAT() As Double
        Get
            Return (iSPOD_VAT)
        End Get
        Set(ByVal Value As Double)
            iSPOD_VAT = Value
        End Set
    End Property
    Public Property SPOD_VATAmount() As Double
        Get
            Return (iSPOD_VATAmount)
        End Get
        Set(ByVal Value As Double)
            iSPOD_VATAmount = Value
        End Set
    End Property
    Public Property SPOD_CST() As Double
        Get
            Return (iSPOD_CST)
        End Get
        Set(ByVal Value As Double)
            iSPOD_CST = Value
        End Set
    End Property
    Public Property SPOD_CSTAmount() As Double
        Get
            Return (iSPOD_CSTAmount)
        End Get
        Set(ByVal Value As Double)
            iSPOD_CSTAmount = Value
        End Set
    End Property
    Public Property SPOD_Excise() As Double
        Get
            Return (iSPOD_Excise)
        End Get
        Set(ByVal Value As Double)
            iSPOD_Excise = Value
        End Set
    End Property
    Public Property SPOD_ExciseAmount() As Double
        Get
            Return (iSPOD_ExciseAmount)
        End Get
        Set(ByVal Value As Double)
            iSPOD_ExciseAmount = Value
        End Set
    End Property

    Public Property SPO_ID() As Integer
        Get
            Return (iSPO_ID)
        End Get
        Set(ByVal Value As Integer)
            iSPO_ID = Value
        End Set
    End Property
    Public Property SPO_OrderCode() As String
        Get
            Return (sSPO_OrderCode)
        End Get
        Set(ByVal Value As String)
            sSPO_OrderCode = Value
        End Set
    End Property
    Public Property SPO_OrderDate() As DateTime
        Get
            Return (dSPO_OrderDate)
        End Get
        Set(ByVal Value As DateTime)
            dSPO_OrderDate = Value
        End Set
    End Property
    Public Property SPO_PartyCode() As String
        Get
            Return (sSPO_PartyCode)
        End Get
        Set(ByVal Value As String)
            sSPO_PartyCode = Value
        End Set
    End Property
    Public Property SPO_PartyName() As String
        Get
            Return (sSPO_PartyName)
        End Get
        Set(ByVal Value As String)
            sSPO_PartyName = Value
        End Set
    End Property
    Public Property SPO_Address() As String
        Get
            Return (sSPO_Address)
        End Get
        Set(ByVal Value As String)
            sSPO_Address = Value
        End Set
    End Property
    Public Property SPO_ContantNo() As String
        Get
            Return (sSPO_ContantNo)
        End Get
        Set(ByVal Value As String)
            sSPO_ContantNo = Value
        End Set
    End Property
    Public Property SPO_ModeOfDispatch() As Integer
        Get
            Return (iSPO_ModeOfDispatch)
        End Get
        Set(ByVal Value As Integer)
            iSPO_ModeOfDispatch = Value
        End Set
    End Property
    Public Property SPO_ShippingDate() As DateTime
        Get
            Return (dSPO_ShippingDate)
        End Get
        Set(ByVal Value As DateTime)
            dSPO_ShippingDate = Value
        End Set
    End Property
    Public Property SPO_PaymentType() As Integer
        Get
            Return (iSPO_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            iSPO_PaymentType = Value
        End Set
    End Property
    Public Property SPO_Status() As String
        Get
            Return (sSPO_Status)
        End Get
        Set(ByVal Value As String)
            sSPO_Status = Value
        End Set
    End Property
    Public Property SPO_CompID() As Integer
        Get
            Return (iSPO_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSPO_CompID = Value
        End Set
    End Property
    Public Property SPO_CreatedBy() As Integer
        Get
            Return (iSPO_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSPO_CreatedBy = Value
        End Set
    End Property
    Public Property SPO_CreatedOn() As DateTime
        Get
            Return (dSPO_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSPO_CreatedOn = Value
        End Set
    End Property
    Public Property SPO_ModeOfCommunication() As Integer
        Get
            Return (iSPO_ModeOfCommunication)
        End Get
        Set(ByVal Value As Integer)
            iSPO_ModeOfCommunication = Value
        End Set
    End Property
    Public Property SPO_InputBy() As String
        Get
            Return (sSPO_InputBy)
        End Get
        Set(ByVal Value As String)
            sSPO_InputBy = Value
        End Set
    End Property
    Public Property SPO_ApprovedBy() As Integer
        Get
            Return (iSPO_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iSPO_ApprovedBy = Value
        End Set
    End Property
    Public Property SPO_ApprovedOn() As DateTime
        Get
            Return (dSPO_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSPO_ApprovedOn = Value
        End Set
    End Property
    Public Property SPO_ShippingCharge() As Integer
        Get
            Return (iSPO_ShippingCharge)
        End Get
        Set(ByVal Value As Integer)
            iSPO_ShippingCharge = Value
        End Set
    End Property

    Public Property SPOD_Id() As Integer
        Get
            Return (iSPOD_Id)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_Id = Value
        End Set
    End Property
    Public Property SPOD_SOID() As Integer
        Get
            Return (iSPOD_SOID)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_SOID = Value
        End Set
    End Property
    Public Property SPOD_CommodityID() As Integer
        Get
            Return (iSPOD_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_CommodityID = Value
        End Set
    End Property
    Public Property SPOD_ItemID() As Integer
        Get
            Return (iSPOD_ItemID)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_ItemID = Value
        End Set
    End Property
    Public Property SPOD_Quantity() As Double
        Get
            Return (iSPOD_Quantity)
        End Get
        Set(ByVal Value As Double)
            iSPOD_Quantity = Value
        End Set
    End Property
    Public Property SPOD_Discount() As Double
        Get
            Return (iSPOD_Discount)
        End Get
        Set(ByVal Value As Double)
            iSPOD_Discount = Value
        End Set
    End Property
    Public Property SPOD_UnitofMeasurement() As Integer
        Get
            Return (iSPOD_UnitofMeasurement)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_UnitofMeasurement = Value
        End Set
    End Property
    Public Property SPOD_RateAmount() As Double
        Get
            Return (iSPOD_RateAmount)
        End Get
        Set(ByVal Value As Double)
            iSPOD_RateAmount = Value
        End Set
    End Property
    Public Property SPOD_DiscountRate() As Double
        Get
            Return (iSPOD_DiscountRate)
        End Get
        Set(ByVal Value As Double)
            iSPOD_DiscountRate = Value
        End Set
    End Property
    Public Property SPOD_CompiD() As Integer
        Get
            Return (iSPOD_CompiD)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_CompiD = Value
        End Set
    End Property
    Public Property SPOD_Status() As String
        Get
            Return (sSPOD_Status)
        End Get
        Set(ByVal Value As String)
            sSPOD_Status = Value
        End Set
    End Property
    Public Property SPOD_HistoryID() As Integer
        Get
            Return (iSPOD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_HistoryID = Value
        End Set
    End Property
    Public Property SPOD_MRPRate() As Double
        Get
            Return (iSPOD_MRPRate)
        End Get
        Set(ByVal Value As Double)
            iSPOD_MRPRate = Value
        End Set
    End Property
    Public Property SPOD_TotalAmount() As Double
        Get
            Return (iSPOD_TotalAmount)
        End Get
        Set(ByVal Value As Double)
            iSPOD_TotalAmount = Value
        End Set
    End Property
    Public Property SPO_SalesType() As Integer
        Get
            Return (iSPO_SalesType)
        End Get
        Set(ByVal Value As Integer)
            iSPO_SalesType = Value
        End Set
    End Property
    Public Property SPO_OtherType() As Integer
        Get
            Return (iSPO_OtherType)
        End Get
        Set(ByVal Value As Integer)
            iSPO_OtherType = Value
        End Set
    End Property

    Public Property SPO_ChequeNo() As String
        Get
            Return (sSPO_ChequeNo)
        End Get
        Set(ByVal Value As String)
            sSPO_ChequeNo = Value
        End Set
    End Property
    Public Property SPO_ChequeDate() As Date
        Get
            Return (dSPO_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            dSPO_ChequeDate = Value
        End Set
    End Property
    Public Property SPO_IFSCCode() As String
        Get
            Return (sSPO_IFSCCode)
        End Get
        Set(ByVal Value As String)
            sSPO_IFSCCode = Value
        End Set
    End Property
    Public Property SPO_BankName() As String
        Get
            Return (sSPO_BankName)
        End Get
        Set(ByVal Value As String)
            sSPO_BankName = Value
        End Set
    End Property
    Public Property SPO_Branch() As String
        Get
            Return (sSPO_Branch)
        End Get
        Set(ByVal Value As String)
            sSPO_Branch = Value
        End Set
    End Property
    Public Property SPO_GoThroughDispatch() As Integer
        Get
            Return (iSPO_GoThroughDispatch)
        End Get
        Set(ByVal Value As Integer)
            iSPO_GoThroughDispatch = Value
        End Set
    End Property

    Public Property SPOD_CreatedBy() As Integer
        Get
            Return (iSPOD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_CreatedBy = Value
        End Set
    End Property
    Public Property SPOD_CreatedOn() As DateTime
        Get
            Return (dSPOD_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSPOD_CreatedOn = Value
        End Set
    End Property
    Public Property SPOD_UpdatedBy() As Integer
        Get
            Return (iSPOD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSPOD_UpdatedBy = Value
        End Set
    End Property
    Public Property SPOD_UpdatedOn() As DateTime
        Get
            Return (dSPOD_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSPOD_UpdatedOn = Value
        End Set
    End Property

    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPaymentType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Payment Type') And Mas_CompID=" & iCompID & " and Mas_Delflag='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, Optional ByVal iCommodityID As Integer = 0) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            'If iCommodityID > 0 Then
            '    'sSql = "Select INV_ID,INV_Description From Inventory_Master Where INV_Parent=" & iCommodityID & " And INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & ")"
            '    sSql = "Select INV_ID,INV_Code From Inventory_Master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & " and SL_Commodity =" & iCommodityID & ") and INV_Code <> '' order by Inv_Code"
            'Else
            '    'sSql = "Select INV_ID,INV_Description From Inventory_Master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & ")"
            '    sSql = "Select INV_ID,INV_Code From Inventory_Master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & ") and INV_Code <> '' order by Inv_Code"
            'End If
            If iCommodityID > 0 Then
                sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_Parent=" & iCommodityID & " And Inv_CompID =" & iCompID & " And Inv_Code <> '' and Inv_Parent <> 0"
            Else
                sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_CompID =" & iCompID & " And Inv_Code <> '' and Inv_Parent <> 0"
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindModeOfCommunication(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Mode Of Communication') And Mas_CompID=" & iCompID & " and Mas_Status='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindCategory(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Type Of Retailer')  And Mas_CompID=" & iCompID & " and Mas_Status='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            'sSql = "Select ACM_ID,ACM_Name From Acc_Customer_Master Where ACM_CompID=" & iCompID & " and ACM_Status='A' order by ACM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMethodOfShiping(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=13 And Mas_Delflag='A'"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesMan(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select Usr_Id,Usr_FullName + '-' + Usr_Code as username From Sad_Userdetails where Usr_CompID=" & iCompID & " order by Usr_FullName "
            sSql = "Select Usr_Id,Usr_FullName + '-' + Usr_Code as username From Sad_Userdetails where USR_Designation in(Select MAS_ID From Acc_General_master where MAs_Desc='Sales Man' And mas_master=6) And Usr_CompID=" & iCompID & " order by Usr_FullName "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
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
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 SPO_ID From Sales_Proforma_Order Order By SPO_ID Desc")
            sYear = objDBL.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDBL.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDBL.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
    Public Function GetGSTDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGstCategory As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select GC_GSTCategory from GSTcategory_table where GC_ID=" & iGstCategory & " and GC_CompID=" & iCompID & ""
            GetGSTDescription = objDBL.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " and BM_CompID =" & iCompID & ""
            'sSql = "Select Top 1 *, ACM_Code  From Acc_Customer_Address_Details,Acc_Customer_Master Where ACAD_MasterID=ACM_ID And ACM_ID=" & iPartyID & " and ACM_Status='A' And ACAD_CompID=" & iCompID & "  "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim dRow As DataRow
        Dim iUnit, iAlterUnit As Integer
        Dim sArray As String()
        Dim sValue As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            'dt.Columns.Add("MAS_ID")
            'dt.Columns.Add("MAS_Desc")

            'iUnit = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_Unit From inventory_master_history Where InvH_INV_ID=" & iID & " and InvH_CompID =" & iCompID & "")
            'iAlterUnit = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_AlterUnit From inventory_master_history Where InvH_INV_ID=" & iID & " and InvH_CompID =" & iCompID & "")
            'sValue = iUnit & "," & iAlterUnit
            'sArray = sValue.Split(",")

            'For i = 0 To sArray.Length - 1
            '    sSql = "Select MAS_ID,MAS_Desc from Acc_General_Master Where MAS_ID =" & sArray(i) & " and Mas_CompId = " & iCompID & ""
            '    dr = objDBL.SQLDataReader(sNameSpace, sSql)
            '    If dr.HasRows = True Then
            '        While dr.Read()
            '            dRow = dt.NewRow
            '            dRow("MAS_ID") = dr("MAS_ID")
            '            dRow("MAS_Desc") = dr("MAS_Desc")
            '            dt.Rows.Add(dRow)
            '        End While
            '    End If
            'Next

            sSql = "Select MAS_ID,MAS_Desc from Acc_General_Master Where MAS_Master=1 And Mas_Delflag='A' and Mas_CompId = " & iCompID & ""
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
    Public Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SPO_ID,SPO_OrderCode From Sales_Proforma_Order where SPO_OrderType='O' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & " Order By SPO_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDiscounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=19 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select DM_ID,DM_Code From Dispatch_Master where DM_CompID=" & iCompID & " and DM_YearID = " & iYearID & " Order By DM_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSalesInvoiceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SDM_ID,SDM_Code From Sales_Dispatch_Master where SDM_CompID=" & iCompID & " and SDM_YearID = " & iYearID & " Order By SDM_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateDispatchCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Dim sSDate As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 DM_ID From Dispatch_Master Order By DM_ID Desc")
            sYear = objDBL.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDBL.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDBL.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
            sStr = "D-" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateSalesInvoiceCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Dim sSDate As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 SDM_ID From Sales_Dispatch_Master Order By SDM_ID Desc")
            sYear = objDBL.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDBL.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDBL.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
            sStr = "I-" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePROFormaMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objProForma As ClsCashSales) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(57) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_OrderCode", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objProForma.SPO_OrderCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_OrderDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objProForma.SPO_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_PartyCode", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objProForma.SPO_PartyCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_PartyName", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objProForma.SPO_PartyName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_Address", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objProForma.SPO_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ContantNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objProForma.SPO_ContantNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ModeOfDispatch ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_ModeOfDispatch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ShippingDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objProForma.SPO_ShippingDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objProForma.SPO_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objProForma.SPO_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ModeOfCommunication", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_ModeOfCommunication
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_InputBy", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objProForma.SPO_InputBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ShippingCharge", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_ShippingCharge
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objProForma.SPO_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objProForma.SPO_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_OrderType", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objProForma.SPO_OrderType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_DispatchFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objProForma.SPO_DispatchFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_SalesManID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_SalesManID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BuyerOrderNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objProForma.SPO_BuyerOrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_Category", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_Category
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_Remarks", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objProForma.SPO_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BuyerOrderDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objProForma.SPO_BuyerOrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_SalesType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_SalesType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_OtherType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_OtherType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ChequeNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objProForma.SPO_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objProForma.SPO_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_IFSCCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objProForma.SPO_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BankName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objProForma.SPO_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_Branch", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objProForma.SPO_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_GoThroughDispatch", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_GoThroughDispatch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_DispatchRefNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objProForma.SPO_DispatchRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ESugamNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objProForma.SPO_ESugamNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_DispatchDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objProForma.SPO_DispatchDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_ZoneID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProForma.SPO_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_RegionID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProForma.SPO_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_AreaID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProForma.SPO_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BranchID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProForma.SPO_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_CompanyAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objProForma.sSPO_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objProForma.sSPO_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BillingAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objProForma.SPO_BillingAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BillingGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objProForma.SPO_BillingGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_DeliveryFrom", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objProForma.sSPO_DeliveryFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_DeliveryFromGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objProForma.sSPO_DeliveryFromGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_DeliveryAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objProForma.SPO_DeliveryAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_DeliveryGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objProForma.SPO_DeliveryGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_DispatchStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objProForma.sSPO_DispatchStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_State", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objProForma.sSPO_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.iSPO_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.iSPO_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spPROFormaMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePROFormaMasterDetails(ByVal sNameSpace As String, ByVal objProForma As ClsCashSales, ByVal iYearID As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(38) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_SOID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_SOID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CommodityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_ItemID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_ItemID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_UnitofMeasurement", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_UnitofMeasurement
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_MRPRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_MRPRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_Quantity", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_Discount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_Discount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_RateAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_RateAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_DiscountRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_DiscountRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_TotalAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_TotalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objProForma.SPOD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_CompiD
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objProForma.SPOD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objProForma.SPOD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_VAT", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_VAT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_VATAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_VATAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_CST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_CSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_Excise", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_Excise
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_ExciseAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_ExciseAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_Category", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_Category
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objProForma.SPOD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objProForma.SPOD_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_GST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_GST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_GSTRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_GSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_GSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_SGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_SGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_SGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_SGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_CGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_CGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_CGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_IGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_IGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_IGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objProForma.SPOD_IGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spPROFormaMasterDetails", 1, Arr, ObjParam)
            Return Arr
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
    Public Function CheckDetailsofBranchState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPodID As String) As String
        Dim sSql As String = ""
        Dim iPOMBranchID As Integer : Dim iCompBrnchID As Integer
        Try
            sSql = "Select SPO_BranchID from  Sales_Proforma_Order where SPO_OrderCode='" & sPodID & "' and SPO_CompID=" & iCompID & ""
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
    Public Function getBranchFromPO(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPodID As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_BranchID from  Sales_Proforma_Order where SPO_OrderCode='" & sPodID & "' and SPO_CompID=" & iCompID & ""
            getBranchFromPO = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iBatchNo > 0 And iBaseName > 0 Then
                sSql = "Select * From Sales_Proforma_Order Where SPO_BatchNo=" & iBatchNo & " And SPO_BaseName=" & iBaseName & " And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindExistingDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iOrderNO As Integer, ByVal iPKID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_Proforma_Order_Details Where SPOD_ID=" & iPKID & " And SPOD_Status<>'C' And SPOD_SOID=" & iOrderNO & " And SPOD_ItemID=" & iItemID & " And SPOD_CompiD=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDiscountID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_ID From Acc_General_Master Where Mas_Master=19 And Mas_Desc='" & sDesc & "' And Mas_CompID=" & iCompID & " "
            GetDiscountID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
