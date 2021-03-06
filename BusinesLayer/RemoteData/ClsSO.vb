Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSO
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

    Private iSPO_ZoneID As Integer
    Private iSPO_RegionID As Integer
    Private iSPO_AreaID As Integer
    Private iSPO_BranchID As Integer

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

    Private iSPO_SalesType As Integer
    Private iSPO_OtherType As Integer
    Private sSPO_ChequeNo As String
    Private dSPO_ChequeDate As DateTime
    Private sSPO_IFSCCode As String
    Private sSPO_BankName As String
    Private sSPO_Branch As String
    Private iSPO_GoThroughDispatch As Integer
    Private sSPO_DispatchRefNo As String
    Private sSPO_ESugamNo As String
    Private dSPO_DispatchDate As DateTime

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
    Public Property SPO_GoThroughDispatch() As Integer
        Get
            Return (iSPO_GoThroughDispatch)
        End Get
        Set(ByVal Value As Integer)
            iSPO_GoThroughDispatch = Value
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
    Public Property SPO_ESugamNo() As String
        Get
            Return (sSPO_ESugamNo)
        End Get
        Set(ByVal Value As String)
            sSPO_ESugamNo = Value
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
    Public Function GetPieceCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As Double
        Dim sSql As String = ""
        Try
            sSql = "Select INVH_PerPieces From Inventory_master_History Where INVH_INV_ID =" & iItemID & " And INVH_CompID=" & iCompID & ""
            GetPieceCount = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetPieceCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPartyCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select BM_ID From Sales_Buyers_Masters Where BM_Code='" & Trim(sPartyCode) & "' and BM_CompID =" & iCompID & ""
            'sSql = "Select ACM_ID From Acc_Customer_Master Where ACM_Code='" & Trim(sPartyCode) & "' and ACM_CompID =" & iCompID & " "
            GetPartyID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPartyID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("UnitOfMeassurement")
            dtTab.Columns.Add("MRPAmount")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("NetAmount")
            dtTab.Columns.Add("INVH_Unit")

            sSql = "Select * From Stock_Ledger Where SL_Commodity=" & iCommodity & " And SL_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("SL_Commodity")
                    dRow("ItemID") = dt.Rows(i)("SL_ItemID")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SL_ItemID") & " And INV_Parent=" & dt.Rows(i)("SL_Commodity") & "")
                    dRow("Quantity") = ""
                    dRow("Discount") = ""
                    dRow("UnitOfMeassurement") = ""
                    dRow("MRPAmount") = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_MRP From Inventory_Master_History Where INVH_INV_ID=" & dt.Rows(i)("SL_ItemID") & "")
                    dRow("Amount") = ""
                    dRow("DiscountAmount") = ""
                    dRow("NetAmount") = ""
                    dRow("INVH_Unit") = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_AlterUnit From Inventory_Master_History Where INVH_INV_ID=" & dt.Rows(i)("SL_ItemID") & "")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
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
            dt.Columns.Add("MAS_ID")
            dt.Columns.Add("MAS_Desc")

            iUnit = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_Unit From inventory_master_history Where InvH_INV_ID=" & iID & " and InvH_CompID =" & iCompID & "")
            iAlterUnit = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_AlterUnit From inventory_master_history Where InvH_INV_ID=" & iID & " and InvH_CompID =" & iCompID & "")
            sValue = iUnit & "," & iAlterUnit
            sArray = sValue.Split(",")

            For i = 0 To sArray.Length - 1
                sSql = "Select MAS_ID,MAS_Desc from Acc_General_Master Where MAS_ID =" & sArray(i) & " and Mas_CompId = " & iCompID & ""
                dr = objDBL.SQLDataReader(sNameSpace, sSql)
                If dr.HasRows = True Then
                    While dr.Read()
                        dRow = dt.NewRow
                        dRow("MAS_ID") = dr("MAS_ID")
                        dRow("MAS_Desc") = dr("MAS_Desc")
                        dt.Rows.Add(dRow)
                    End While
                End If
            Next
            Return dt
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
    Public Function GetMRPAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Dim iMRPAmount As Integer
        Try
            sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " "
            iMRPAmount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iMRPAmount
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePROFormaMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objProForma As ClsSO) As Array
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

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_Status ", OleDb.OleDbType.VarChar, 1)
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
            ObjParam(iParamCount).Value = objProForma.SPO_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objProForma.SPO_BaseName
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
    Public Function BindExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("SlNo")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("UnitOfMeassurement")
            dtTab.Columns.Add("MRPAmount")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATAmount")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTAmount")
            dtTab.Columns.Add("Excise")
            dtTab.Columns.Add("ExciseAmount")
            dtTab.Columns.Add("NetAmount")
            dtTab.Columns.Add("PKID")

            If iBatchNo > 0 Then
                sSql = "Select * From Sales_Proforma_Order_Details Where SPOD_Status <>'C' And SPOD_SOID in (Select SPO_ID From Sales_Proforma_Order Where SPO_BatchNo=" & iBatchNo & " And SPO_BaseName=" & iBaseName & ") And SPOD_CompiD=" & iCompID & " Order By SPOD_ID "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("SPOD_CommodityID")
                    dRow("ItemID") = dt.Rows(i)("SPOD_ItemID")
                    dRow("SlNo") = i + 1
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("SPOD_ItemID") & " And INV_Parent=" & dt.Rows(i)("SPOD_CommodityID") & " and Inv_CompID = " & iCompID & "")
                    dRow("Quantity") = dt.Rows(i)("SPOD_Quantity")
                    'If IsDBNull(dt.Rows(i)("SPOD_Discount")) = False Then
                    '    dRow("Discount") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_Master=19 And Mas_ID=" & dt.Rows(i)("SPOD_Discount") & " ")
                    'End If

                    'dRow("Discount") = dt.Rows(i)("SPOD_Discount")
                    dRow("Discount") = 0

                    dRow("UnitOfMeassurement") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SPOD_UnitOfMeasurement") & " and Mas_CompID =" & iCompID & "")
                    dRow("MRPAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_MRPRate")))
                    dRow("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_RateAmount")))
                    'If IsDBNull(dt.Rows(i)("SPOD_DiscountRate")) = False Then
                    '    dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_DiscountRate")))
                    'End If
                    dRow("DiscountAmount") = 0
                    'If IsDBNull(dt.Rows(i)("SPOD_VAT")) = False Then
                    '    dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_Master=14 And Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " ")
                    'End If
                    'If IsDBNull(dt.Rows(i)("SPOD_VATAmount")) = False Then
                    '    dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_VATAmount")))
                    'End If
                    dRow("VAT") = 0
                    dRow("VATAmount") = 0

                    'If IsDBNull(dt.Rows(i)("SPOD_CST")) = False Then
                    '    dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_Master=15 And Mas_ID=" & dt.Rows(i)("SPOD_CST") & " ")
                    'End If
                    'If IsDBNull(dt.Rows(i)("SPOD_CSTAmount")) = False Then
                    '    dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_CSTAmount")))
                    'End If
                    dRow("CST") = 0
                    dRow("CSTAmount") = 0

                    'If IsDBNull(dt.Rows(i)("SPOD_Excise")) = False Then
                    '    dRow("Excise") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_Master=16 And Mas_ID=" & dt.Rows(i)("SPOD_Excise") & " ")
                    'End If
                    'If IsDBNull(dt.Rows(i)("SPOD_ExciseAmount")) = False Then
                    '    dRow("ExciseAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_ExciseAmount")))
                    'End If
                    dRow("Excise") = 0
                    dRow("ExciseAmount") = 0

                    If IsDBNull(dt.Rows(i)("SPOD_TotalAmount")) = False Then
                        dRow("NetAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_TotalAmount")))
                    End If
                    dRow("PKID") = dt.Rows(i)("SPOD_ID")

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iItemID As Integer, ByVal iCommodity As Integer, ByVal iOrderID As Integer, ByVal iIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Proforma_Order_Details Set SPOD_DeletedBy=" & iUserID & ",SPOD_DeletedOn=GetDate(),SPOD_Status='C',SPOD_Operation='D',SPOD_IPAddress='" & iIPAddress & "' Where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodity & " And SPOD_ItemID=" & iItemID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iItemID As Integer, ByVal iCommodity As Integer, ByVal iOrderID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim i As Integer
        Try
            'sSql = "Update Sales_Proforma_Order_Details Set SPOD_UpdatedBy=" & iUserID & ",SPOD_UpdatedOn=GetDate() Where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodity & " And SPOD_ItemID=" & iItemID & " and SPOD_CompID =" & iCompID & ""
            sSql = "Select * From Sales_Proforma_Order_Details Where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodity & " And SPOD_ItemID=" & iItemID & " and SPOD_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = ""
                    sSql = "Begin "
                    sSql = sSql & " Update Sales_Proforma_Order_Details Set SPOD_Operation='U',SPOD_IPAddress='" & iIPAddress & "',SPOD_UpdatedBy=" & iUserID & ",SPOD_UpdatedOn=GetDate() Where SPOD_ID=" & dt.Rows(i)("SPOD_ID") & " "
                    sSql = sSql & " End "
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
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
    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBatchID As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_Proforma_Order Where SPO_BatchNo=" & iBatchID & " And SPO_BaseName=" & iBaseName & " And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & ""
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
    Public Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SPO_ID,SPO_OrderCode From Sales_Proforma_Order where SPO_DispatchFlag <> 1 And SPO_OrderType='S' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & " Order By SPO_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUnitInDetailsTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim bCheck As Boolean
        Dim iUnit As Integer
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Proforma_Order_Details Where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodityID & " And SPOD_ItemID=" & iItemID & " And SPOD_CompID=" & iCompID & "")
            If bCheck = True Then
                sSql = "Select SPOD_UnitOfMeasurement From Sales_Proforma_Order_Details Where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodityID & " And SPOD_ItemID=" & iItemID & " And SPOD_CompID=" & iCompID & " "
                iUnit = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iUnit
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCommodity(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim bCheck As Boolean
        Dim iCommodity As Integer
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Proforma_Order_Details Where SPOD_ItemID=" & iItemID & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & "")
            If bCheck = True Then
                sSql = "Select SPOD_CommodityID From Sales_Proforma_Order_Details Where SPOD_ItemID=" & iItemID & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " "
                iCommodity = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iCommodity
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

    'Public Shared Function GetMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iItemID As Integer, ByVal iParty As Integer) As String
    '    Dim sMRP As String = "", sSql As String = "", sCode As String = ""
    '    Try
    '        sCode = objDBL.SQLGetDescription(sNameSpace, "Select BM_Code From Sales_Buyers_Masters Where BM_ID=" & iParty & " And BM_CompID=" & iCompID & " ")
    '        If sCode.StartsWith("P") Then
    '            'sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " and INVH_CompID =" & iCompID & ""
    '            sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " and INVH_CompID =" & iCompID & ""
    '        ElseIf sCode.StartsWith("C") Then
    '            sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " and INVH_CompID =" & iCompID & ""
    '        End If
    '        sMRP = objDBL.SQLGetDescription(sNameSpace, sSql)
    '        Return sMRP
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function GetMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sHistoryID As String, ByVal sCode As String, ByVal iCategoryID As Integer, ByVal iItemID As Integer, ByVal dOrderDate As Date) As String
        Dim sMRP As String = "", sSql As String = ""
        Try
            'If sCode.StartsWith("P") Then
            '    sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_ID in (" & sHistoryID & ") and INVH_CompID =" & iCompID & ""
            'ElseIf sCode.StartsWith("C") Then
            '    sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_ID in (" & sHistoryID & ") and INVH_CompID =" & iCompID & ""
            'End If
            'Commented Bcz of new category Table'
            'If iCategoryID > 0 Then
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select IMC_RetailRate From Inventory_master_Category Where IMC_Category=" & iCategoryID & " And IMC_ItemID in (" & iItemID & ") and IMC_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select IMC_MRP From Inventory_master_Category Where IMC_Category=" & iCategoryID & " And IMC_ItemID in (" & iItemID & ") and IMC_CompID =" & iCompID & ""
            '    End If
            'Else
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_ID in (" & sHistoryID & ") and INVH_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_ID in (" & sHistoryID & ") and INVH_CompID =" & iCompID & ""
            '    End If
            'End If
            'Commented Bcz of new category Table'

            'Working'
            'If iCategoryID > 0 Then
            '    If sCode.StartsWith("P") Then
            '        'sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") And INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
            '        'sSql = "SELECT INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_RetailEffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
            '        sSql = "SELECT INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_RetailEffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    ElseIf sCode.StartsWith("C") Then
            '        'sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") And INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
            '        'sSql = "SELECT INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
            '        sSql = "SELECT INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_EffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    End If
            'Else
            '    If sCode.StartsWith("P") Then
            '        'sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") And INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
            '        'sSql = "SELECT INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_RetailEffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
            '        sSql = "SELECT INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_RetailEffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    ElseIf sCode.StartsWith("C") Then
            '        'sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") And INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
            '        'sSql = "SELECT INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
            '        sSql = "SELECT INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_EffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    End If
            'End If
            'Working'

            If iCategoryID > 0 Then
                If sCode.StartsWith("P") Then
                    'sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") And INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
                    'sSql = "SELECT INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_RetailEffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
                    sSql = "SELECT INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                ElseIf sCode.StartsWith("C") Then
                    'sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") And INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
                    'sSql = "SELECT INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
                    sSql = "SELECT INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                End If
            Else
                If sCode.StartsWith("P") Then
                    'sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") And INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
                    'sSql = "SELECT INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_RetailEffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
                    sSql = "SELECT INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                ElseIf sCode.StartsWith("C") Then
                    'sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") And INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
                    'sSql = "SELECT INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
                    sSql = "SELECT INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                End If
            End If

            sMRP = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sMRP
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetINVH_Unit(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iItemID As Integer) As Integer
        Dim iUnit As String = ""
        Dim sSql As String = ""
        Try
            sSql = "Select INVH_AlterUnit From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " and INVH_CompID =" & iCompID & ""
            iUnit = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iUnit
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
    Public Function GetHistoryID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iCategory As Integer, ByVal sCode As String, ByVal dOrderDate As Date) As Integer
        Dim iHistoryID As String = "", sSql As String = ""
        Try
            'working'
            'If iCategory > 0 Then
            '    'sSql = "" : sSql = "Select INVH_ID From Inventory_Master_History Where INVH_CategoryID=" & iCategory & " And INVH_INV_ID In (Select INV_ID From Inventory_Master Where "
            '    'sSql = sSql & "INV_ID =" & iItemID & " And INV_Parent=" & iCommodityID & " And Inv_CompID = " & iCompID & ") And "
            '    'sSql = sSql & "InvH_CompID =" & iCompID & ""
            '    'iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            '    If sCode = "P" Then
            '        sSql = "Select INVH_ID From Inventory_Master_History Where InvH_CompID =" & iCompID & " And INVH_CategoryID=" & iCategory & " And ((INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_RetailEffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    ElseIf sCode = "C" Then
            '        sSql = "SELECT INVH_ID FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategory & " And ((INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_EffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    End If
            '    iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            'Else
            '    'sSql = "" : sSql = "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID In (Select INV_ID From Inventory_Master Where "
            '    'sSql = sSql & "INV_ID =" & iItemID & " And INV_Parent=" & iCommodityID & " And Inv_CompID = " & iCompID & ") And "
            '    'sSql = sSql & "InvH_CompID =" & iCompID & ""
            '    'iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            '    If sCode = "P" Then
            '        sSql = "Select INVH_ID From Inventory_Master_History Where InvH_CompID =" & iCompID & " And ((INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_RetailEffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    ElseIf sCode = "C" Then
            '        sSql = "SELECT INVH_ID FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_EffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    End If
            '    iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            'End If
            'working'

            If iCategory > 0 Then
                'sSql = "" : sSql = "Select INVH_ID From Inventory_Master_History Where INVH_CategoryID=" & iCategory & " And INVH_INV_ID In (Select INV_ID From Inventory_Master Where "
                'sSql = sSql & "INV_ID =" & iItemID & " And INV_Parent=" & iCommodityID & " And Inv_CompID = " & iCompID & ") And "
                'sSql = sSql & "InvH_CompID =" & iCompID & ""
                'iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
                If sCode = "P" Then
                    sSql = "Select INVH_ID From Inventory_Master_History Where InvH_CompID =" & iCompID & " And INVH_CategoryID=" & iCategory & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                ElseIf sCode = "C" Then
                    sSql = "SELECT INVH_ID FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategory & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                End If
                iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                'sSql = "" : sSql = "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID In (Select INV_ID From Inventory_Master Where "
                'sSql = sSql & "INV_ID =" & iItemID & " And INV_Parent=" & iCommodityID & " And Inv_CompID = " & iCompID & ") And "
                'sSql = sSql & "InvH_CompID =" & iCompID & ""
                'iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
                If sCode = "P" Then
                    sSql = "Select INVH_ID From Inventory_Master_History Where InvH_CompID =" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                ElseIf sCode = "C" Then
                    sSql = "SELECT INVH_ID FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                End If
                iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iHistoryID
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

    Public Function GetCommodityID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCommodity As Integer
        Try
            'iCommodity = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SL_Commodity From Stock_Ledger Where SL_ItemID=" & iItemID & " And SL_CompID=" & iCompID & " ")
            iCommodity = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INV_Parent From Inventory_Master Where Inv_ID=" & iItemID & " And Inv_CompID=" & iCompID & " ")
            Return iCommodity
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As String
        Dim sSql As String = ""
        Dim sCode As String = ""
        Try
            sCode = objDBL.SQLGetDescription(sNameSpace, "Select BM_Code From Sales_Buyers_Masters Where BM_ID=" & iParty & " And BM_CompID=" & iCompID & " ")
            'sCode = objDBL.SQLGetDescription(sNameSpace, "Select ACM_Code From Acc_Customer_Master Where ACM_ID=" & iParty & " And ACM_CompID=" & iCompID & "  ")
            Return sCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSPOID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SPO_OrderCode,CONVERT(VARCHAR(10),SPO_OrderDate,103)As SPO_OrderDate,SPO_BuyerOrderNo,SPO_PartyName,SPO_GrandDiscount,SPO_GrandDiscountAmt,SPO_GrandTotal,SPO_GrandTotalAmt,b.SPOD_CommodityID,b.SPOD_UnitOfMeasurement,b.SPOD_MRPRate,b.SPOD_Quantity,b.SPOD_TotalAmount,"
            sSql = sSql & "Convert(money, b.SPOD_Discount)As SPOD_Discount,Convert(money,b.SPOD_DiscountRate) As SPOD_DiscountRate,"
            sSql = sSql & "Convert(money, b.SPOD_CST) AS SPOD_CST, CONVERT(money, b.SPOD_CSTAmount) AS SPOD_CSTAmount,"
            sSql = sSql & "Convert(money, b.SPOD_VAT) as VAT,Convert(money,b.SPOD_VATAmount) as VATAmount,"
            sSql = sSql & "Convert(money, b.SPOD_Excise) as Excise,Convert(money,b.SPOD_ExciseAmount) as ExciseAmount,"
            sSql = sSql & "Convert(money, b.SPOD_RateAmount)As SPOD_RateAmount,"
            sSql = sSql & "c.INV_Code, c.INV_Description, d.BM_Name, d.BM_Address, d.BM_MobileNo, d.BM_EmailID, e.Mas_Desc, f.Mas_desc As VATDesc,g.CUST_NAME,g.CUST_CODE,g.CUST_COMM_ADDRESS,g.CUST_COMM_TEL,g.CUST_EMAIL,h.Cmp_Value,i.Buyer_Value,j.Mas_desc As CSTDesc,k.Mas_desc As ExciseDesc,l.Mas_desc As DiscountDesc,m.Mas_Desc As TermsCondtions "
            sSql = sSql & "From Sales_Proforma_Order"
            sSql = sSql & " Join Sales_Proforma_Order_Details b On SPO_ID=" & iSPOID & " And SPO_OrderType='S' And SPO_ID=b.SPOD_SOID And b.SPOD_Status <> 'C' "
            sSql = sSql & " Join Inventory_master c on b.SPOD_ItemID=c.INV_ID "
            sSql = sSql & " Join Sales_Buyers_Masters d On SPO_PartyName=d.BM_ID"
            sSql = sSql & " Join Acc_General_Master e On b.SPOD_UnitOfMeasurement=e.Mas_ID"
            sSql = sSql & " Left Join ACC_General_Master f ON b.SPOD_VAT = f.Mas_id And f.Mas_Master=14"
            sSql = sSql & " Join MST_Customer_Master g ON g.CUST_ID = b.SPOD_CompID "
            sSql = sSql & " Left Join Company_Accounting_Template h ON g.CUST_ID = h.Cmp_ID And Cmp_Desc='TIN' "
            sSql = sSql & " Left Join Sales_Buyer_Accounting_Template i ON d.BM_ID = i.Buyer_ID And Buyer_Desc='TIN' "
            sSql = sSql & " Left Join ACC_General_Master j ON b.SPOD_CST = j.Mas_id And j.Mas_Master=15"
            sSql = sSql & " Left Join ACC_General_Master k ON b.SPOD_Excise = k.Mas_id And k.Mas_Master=16"
            sSql = sSql & " Left Join ACC_General_Master l ON b.SPOD_Discount = l.Mas_id And l.Mas_Master=19"
            sSql = sSql & " Left Join ACC_General_Master m ON m.Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Terms & Conditions')"

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadVAT(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_ID,Mas_Desc from Acc_General_Master where Mas_Master=14 And Mas_DelFlag='A' and Mas_CompID=" & iCompID & " order by Mas_Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sHistoryID As String, ByVal sCode As String, ByVal iCategoryID As Integer, ByVal iItemID As Integer, ByVal dOrderDate As Date) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'If sCode.StartsWith("P") Then
            '    sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_ID in (" & sHistoryID & ") and INVH_CompID =" & iCompID & ""
            'ElseIf sCode.StartsWith("C") Then
            '    sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_ID in (" & sHistoryID & ") and INVH_CompID =" & iCompID & ""
            'End If
            'Commented bcz of separate category table'
            'If iCategoryID > 0 Then
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select IMC_MasterID,IMC_RetailRate From Inventory_master_Category Where IMC_Category=" & iCategoryID & " And IMC_ItemID in (" & iItemID & ") and IMC_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select IMC_MasterID,IMC_MRP From Inventory_master_Category Where IMC_Category=" & iCategoryID & " And IMC_ItemID in (" & iItemID & ") and IMC_CompID =" & iCompID & ""
            '    End If
            'Else
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_ID in (" & sHistoryID & ") and INVH_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_ID in (" & sHistoryID & ") and INVH_CompID =" & iCompID & ""
            '    End If
            'End If
            'Commented bcz of separate category table'

            'Working'
            'If iCategoryID > 0 Then
            '    If sCode.StartsWith("P") Then
            '        'sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") And INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
            '        'sSql = "SELECT INVH_ID,INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_RetailEffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
            '        sSql = "SELECT INVH_ID,INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_RetailEffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    ElseIf sCode.StartsWith("C") Then
            '        'sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") And INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
            '        'sSql = "SELECT INVH_ID,INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
            '        sSql = "SELECT INVH_ID,INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_EffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    End If
            'Else
            '    If sCode.StartsWith("P") Then
            '        'sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") And INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
            '        'sSql = "SELECT INVH_ID,INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_RetailEffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
            '        sSql = "SELECT INVH_ID,INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_RetailEffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    ElseIf sCode.StartsWith("C") Then
            '        'sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") And INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
            '        'sSql = "SELECT INVH_ID,INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
            '        sSql = "SELECT INVH_ID,INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_EffeTo >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And INVH_INV_ID in (" & iItemID & ")) Or ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
            '    End If
            'End If
            'Working'

            If iCategoryID > 0 Then
                If sCode.StartsWith("P") Then
                    'sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") And INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
                    'sSql = "SELECT INVH_ID,INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_RetailEffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
                    sSql = "SELECT INVH_ID,INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                ElseIf sCode.StartsWith("C") Then
                    'sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") And INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
                    'sSql = "SELECT INVH_ID,INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
                    sSql = "SELECT INVH_ID,INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategoryID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                End If
            Else
                If sCode.StartsWith("P") Then
                    'sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") And INVH_RetailEffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
                    'sSql = "SELECT INVH_ID,INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_RetailEffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
                    sSql = "SELECT INVH_ID,INVH_Retail FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                ElseIf sCode.StartsWith("C") Then
                    'sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") And INVH_EffeFrom >= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') and INVH_CompID =" & iCompID & ""
                    'sSql = "SELECT INVH_ID,INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And ((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo) And INVH_INV_ID in (" & iItemID & ")) Or ((INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & "))"
                    sSql = "SELECT INVH_ID,INVH_MRP FROM Inventory_Master_History WHERE INVH_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                End If
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVATAndCSTFromGeneralMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sStr = "VAT" Then
                sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=14 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            Else
                sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=15 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVATAndCSTFromHistory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sStr = "VAT" Then
                sSql = "select INVH_ID,INVH_VAT from Inventory_Master_History where INVH_INV_ID =" & iItemID & " and INVH_CompID =" & iCompID & ""
            Else
                sSql = "select INVH_ID,INVH_CST from Inventory_Master_History where INVH_INV_ID =" & iItemID & " and INVH_CompID =" & iCompID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Shared Function GetINVHID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal sRate As String, ByVal sPartyCode As String) As Integer
    '    Dim sSql As String = ""
    '    Dim iINVHID As Integer
    '    Try
    '        If sPartyCode = "P" Then
    '            sSql = "Select INVH_ID From Inventory_Master_History Where INVH_Retail=" & sRate & " And INVH_INV_ID=" & iItemID & " "
    '        ElseIf sPartyCode = "C" Then
    '            sSql = "Select INVH_ID From Inventory_Master_History Where INVH_MRP=" & sRate & " And INVH_INV_ID=" & iItemID & " "
    '        End If
    '        iINVHID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Return iINVHID
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetINVHID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iCategoryID As Integer, ByVal sPartyCode As String) As Integer
        Dim sSql As String = ""
        Dim iINVHID As Integer
        Try
            If iCategoryID > 0 Then
                If sPartyCode = "P" Then
                    sSql = "Select INVH_ID From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID=" & iItemID & " "
                ElseIf sPartyCode = "C" Then
                    sSql = "Select INVH_ID From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID=" & iItemID & " "
                End If
            Else
                If sPartyCode = "P" Then
                    sSql = "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " "
                ElseIf sPartyCode = "C" Then
                    sSql = "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " "
                End If
            End If

            iINVHID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iINVHID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATOFThisRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Dim iVATID As Integer
        Try
            sSql = "Select Mas_ID From Acc_General_Master Where Mas_Master=14 And MAs_Desc in (Select INVH_VAT From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & ") "
            iVATID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iVATID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTOFThisRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCSTID As Integer
        Try
            sSql = "Select Mas_ID From Acc_General_Master Where Mas_Master=15 And MAs_Desc in (Select INVH_CST From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & ") "
            iCSTID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iCSTID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckOrderForDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sStr As String = ""
        Try
            sSql = "Select * From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sStr = "True"
            Else
                sStr = "False"
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExciseFromGeneralMaster(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=16 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExciseOFThisRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCSTID As Integer
        Try
            sSql = "Select Mas_ID From Acc_General_Master Where Mas_Master=16 And MAs_Desc in (Select INVH_Excise From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & ") "
            iCSTID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iCSTID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGrandTotalToOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iIPAddress As String, ByVal iOrderID As Integer, ByVal sGrandDiscount As String, ByVal sGrandDiscountAmt As String, ByVal sGrandTotal As String, ByVal sGrandTotalAmt As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Proforma_Order set SPO_GrandDiscount=" & sGrandDiscount & ",SPO_GrandDiscountAmt=" & sGrandDiscountAmt & ", "
            sSql = sSql & "SPO_GrandTotal=" & sGrandTotal & ",SPO_GrandTotalAmt=" & sGrandTotalAmt & ",SPO_Operation='U',SPO_IPAddress='" & iIPAddress & "' "
            sSql = sSql & "Where SPO_ID=" & iOrderID & " And SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATorCSTFromHistory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal sRate As String, ByVal sStr As String) As String
        Dim sSql As String = ""
        Dim sVAT As String = ""
        Dim dVAT As String = ""
        Try
            If sStr = "P" Then
                dVAT = objDBL.SQLGetDescription(sNameSpace, "Select INVH_VAT From Inventory_Master_History Where INVH_Retail=" & sRate & " And INVH_INV_ID=" & iItemID & " ")
                If dVAT > 0 Then
                    sVAT = dVAT
                Else
                    dVAT = objDBL.SQLGetDescription(sNameSpace, "Select INVH_CST From Inventory_Master_History Where INVH_Retail=" & sRate & " And INVH_INV_ID=" & iItemID & " ")
                    sVAT = dVAT
                End If
            Else
                dVAT = objDBL.SQLGetDescription(sNameSpace, "Select INVH_VAT From Inventory_Master_History Where INVH_MRP=" & sRate & " And INVH_INV_ID=" & iItemID & "")
                If dVAT > 0 Then
                    sVAT = dVAT
                Else
                    dVAT = objDBL.SQLGetDescription(sNameSpace, "Select INVH_CST From Inventory_Master_History Where INVH_MRP=" & sRate & " And INVH_INV_ID=" & iItemID & "")
                    sVAT = dVAT
                End If
            End If
            Return sVAT
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
    'Public Shared Function GetALLVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
    '    Dim sSql As String
    '    Dim sValue As String
    '    Dim dt As New DataTable
    '    Dim sVatDesc, sVATAmt As String
    '    Dim sStr As String = ""
    '    Try
    '        sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
    '                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
    '                sValue = "VAT@" & sVatDesc & " - " & sVATAmt
    '                sStr = sStr & "," & sValue
    '            Next
    '        End If
    '        If sStr.StartsWith(",") Then
    '            sStr = sStr.Remove(0, 1)
    '        End If
    '        Return sStr
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetALLVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc As String = "" : Dim sVATAmt As String = ""
        Dim sStr As String = ""
        Dim sTotalAmt As String = "" : Dim sTradeDis As String = ""
        Dim dExciseAmt As Double
        Try
            sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SPO_GrandDiscountAmt From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")
                    dExciseAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_ExciseAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    If sTradeDis <> "" Then
                        sVATAmt = (((sTotalAmt - sTradeDis) + dExciseAmt) * sVatDesc) / 100
                    End If
                    sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    sStr = sStr & "," & sValue
                Next
            ElseIf dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    sStr = sStr & "," & sValue
                Next
            End If
            If sStr.StartsWith(",") Then
                sStr = sStr.Remove(0, 1)
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetALLVATAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc As Double : Dim sVATAmt As Double
        Dim sStr As String = ""
        Dim sTotalAmt As Double : Dim sTradeDis As Double
        Dim dVATAmount As Double
        Dim dExciseAmt As Double
        Try
            sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SPO_GrandDiscountAmt From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")
                    dExciseAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_ExciseAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    If sTradeDis > 0 Then
                        sVATAmt = (((sTotalAmt - sTradeDis) + dExciseAmt) * sVatDesc) / 100
                    Else
                        sVATAmt = ((sTotalAmt + dExciseAmt) * sVatDesc) / 100
                    End If
                    dVATAmount = sVATAmt
                    'sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    'sStr = sStr & "," & sValue
                Next
            ElseIf dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    'sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    'sStr = sStr & "," & sValue
                    dVATAmount = dVATAmount + sVATAmt
                Next
            End If
            'If sStr.StartsWith(",") Then
            '    sStr = sStr.Remove(0, 1)
            'End If
            Return dVATAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSearchPartyList(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sSearch <> "" Then
                sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_Name Like '" & sSearch & "%' And BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            'If sSearch <> "" Then
            '    sSql = "Select ACM_ID,ACM_Name From Acc_Customer_Master Where ACM_Name Like '" & sSearch & "%' And ACM_CompID=" & iCompID & " and ACM_Status='A' order by ACM_Name "
            '    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            'Else
            '    sSql = "Select ACM_ID,ACM_Name From Acc_Customer_Master Where ACM_CompID=" & iCompID & " and ACM_Status='A' order by ACM_Name "
            '    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            'End If

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
    Public Function DeleteWholeOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iOrderID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Update Sales_Proforma_Order Set SPO_DeletedBy=" & iUserID & ",SPO_DeletedOn=GetDate(),SPO_Status='D',SPO_Operation='D',SPO_IPAddress='" & iIPAddress & "' Where SPO_ID=" & iOrderID & " And SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            sSql = "" : sSql = "Delete From Sales_Proforma_Order Where SPO_ID=" & iOrderID & " And SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            sSql = "" : sSql = "Select * From Sales_Proforma_Order_Details Where SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & " Update Sales_Proforma_Order_Details Set SPOD_Operation='D',SPOD_DeletedBy=" & iUserID & ",SPOD_DeletedOn=GetDate() Where SPOD_ID= " & dt.Rows(i)("SPOD_ID") & " "
                    sSql = sSql & " End"
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next

                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & " Delete From Sales_Proforma_Order_Details Where SPOD_ID= " & dt.Rows(i)("SPOD_ID") & " "
                    sSql = sSql & " End"
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteAllocationOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iOrderID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " ")
            If bCheck = True Then

                sSql = "" : sSql = "Select * From Sales_Allocate_Details Where SAD_MasterID In (select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & ") "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        sSql = "Begin "
                        sSql = sSql & " Update Sales_Allocate_Details Set SAD_Operation='D',SAD_DeletedBy=" & iUserID & ",SAD_DeletedOn=GetDate() Where SAD_ID= " & dt.Rows(i)("SAD_ID") & " "
                        sSql = sSql & " End"
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Next

                    For i = 0 To dt.Rows.Count - 1
                        sSql = "Begin "
                        sSql = sSql & " Delete From Sales_Allocate_Details Where SAD_ID= " & dt.Rows(i)("SAD_ID") & " "
                        sSql = sSql & " End"
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Next
                End If

                sSql = "" : sSql = "Update Sales_Allocate_Master Set SAM_DeletedBy=" & iUserID & ",SAM_DeletedOn=GetDate(),SAM_Status='D',SAM_Operation='D',SAM_IPAddress='" & iIPAddress & "' Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

                sSql = "" : sSql = "Delete From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZeroVAT(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='0' And Mas_Master=14 And Mas_CompID=" & iCompID & " And Mas_DelFlag='A' "
            GetZeroVAT = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZeroCST(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where  MAS_Desc='0' And Mas_Master=15 And Mas_CompID=" & iCompID & " And Mas_DelFlag='A' "
            GetZeroCST = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZeroExcise(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where  MAS_Desc='0' And Mas_Master=16 And Mas_CompID=" & iCompID & " And Mas_DelFlag='A' "
            GetZeroExcise = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATOFEachItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As String
        Dim sSql As String = ""
        Dim sVAT As String = ""
        Try
            sSql = "Select INVH_VAT From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & " "
            sVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sVAT
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePROFormaMasterDetails(ByVal sNameSpace As String, ByVal objProForma As ClsSO, ByVal iYearID As Integer) As Array
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

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPOD_Status ", OleDb.OleDbType.VarChar, 1)
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
    Public Function CheckOrderForAllocationApprove(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sStr As String = ""
        Try
            sSql = "Select * From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                'sStr = objDBL.SQLGetDescription(sNameSpace, "Select SAM_Status From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " ")
                sStr = "True"
            Else
                sStr = "False"
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetALLCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sCstDesc As String = "" : Dim sCsTAmt As String = ""
        Dim sStr As String = ""
        Dim sTotalAmt As String = "" : Dim sTradeDis As String = ""
        Dim dExciseAmt As Double
        Try
            sSql = "Select Distinct(SPOD_CST) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SPO_GrandDiscountAmt From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")
                    dExciseAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_ExciseAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    If sTradeDis <> "" Then
                        sCsTAmt = (((sTotalAmt - sTradeDis) + dExciseAmt) * sCstDesc) / 100
                    End If
                    sValue = "CST@" & sCstDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sCsTAmt))
                    sStr = sStr & "," & sValue
                Next
            ElseIf dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sCsTAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_CSTAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sValue = "CST@" & sCstDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sCsTAmt))
                    sStr = sStr & "," & sValue
                Next
            End If
            If sStr.StartsWith(",") Then
                sStr = sStr.Remove(0, 1)
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetALLCSTAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sCstDesc As Double : Dim sCsTAmt As Double
        Dim sStr As String = ""
        Dim sTotalAmt As Double : Dim sTradeDis As Double
        Dim dVATAmount As Double
        Dim dExciseAmt As Double
        Try
            sSql = "Select Distinct(SPOD_CST) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SPO_GrandDiscountAmt From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")
                    dExciseAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_ExciseAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    If sTradeDis > 0 Then
                        sCsTAmt = (((sTotalAmt - sTradeDis) + dExciseAmt) * sCstDesc) / 100
                    Else
                        sCsTAmt = ((sTotalAmt + dExciseAmt) * sCstDesc) / 100
                    End If
                    dVATAmount = sCsTAmt
                    'sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    'sStr = sStr & "," & sValue
                Next
            ElseIf dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sCsTAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_CSTAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    'sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    'sStr = sStr & "," & sValue
                    dVATAmount = dVATAmount + sCsTAmt
                Next
            End If
            'If sStr.StartsWith(",") Then
            '    sStr = sStr.Remove(0, 1)
            'End If
            Return dVATAmount
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
    Public Function GetStockHistoryID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As String
        Dim sSql As String = ""
        Dim dtHistory As New DataTable
        Dim sINVHID As String = ""
        Try
            sSql = "Select Distinct(SL_HistoryID) As SL_HistoryID From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " "
            dtHistory = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtHistory.Rows.Count > 0 Then
                For i = 0 To dtHistory.Rows.Count - 1
                    sINVHID = sINVHID & "," & dtHistory.Rows(i)("SL_HistoryID")
                Next
            End If

            If sINVHID.StartsWith(",") Then
                sINVHID = sINVHID.Remove(0, 1)
            End If
            If sINVHID.EndsWith(",") Then
                sINVHID = sINVHID.Remove(Len(sINVHID) - 1, 1)
            End If
            Return sINVHID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPrintData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Print_Settings Where PS_Status='" & sStr & "' "
            GetPrintData = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetPrintData
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVendorDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSPOID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select c.CUST_NAME,c.CUST_CODE,c.CUST_COMM_ADDRESS,c.CUST_COMM_TEL,c.CUST_EMAIL,c.CUST_PIN,d.Cmp_Value As CVAT,e.Cmp_Value As CTAX,f.Cmp_Value As CPAN,g.Cmp_Value As CTAN,h.Cmp_Value As CTIN,i.Cmp_Value As CCIN,
                    j.BM_Name,j.BM_Address,j.BM_MobileNo,j.BM_EmailID,j.BM_PinCode,k.Buyer_Value As PVAT,l.Buyer_Value As PTAX,m.Buyer_Value As PPAN,n.Buyer_Value As PTAN,o.Buyer_Value As PTIN,p.Buyer_Value As PCIN
                    From Sales_ProForma_Order 
                    Join Sales_Proforma_Order_Details b On SPO_ID=" & iSPOID & " And SPO_OrderType='S' And SPO_ID=b.SPOD_SOID And b.SPOD_Status <> 'C'
                    Left Join MST_Customer_Master c ON c.CUST_ID=SPO_CompID
                    Left Join Company_Accounting_Template d ON d.Cmp_ID=c.CUST_ID And d.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template e ON e.Cmp_ID=c.CUST_ID And e.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template f ON f.Cmp_ID=c.CUST_ID And f.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template g ON g.Cmp_ID=c.CUST_ID And g.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template h ON h.Cmp_ID=c.CUST_ID And h.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template i ON i.Cmp_ID=c.CUST_ID And i.Cmp_Desc='CIN' 
                    Left Join Sales_Buyers_Masters j On j.BM_ID=SPO_PartyName
                    Left Join Sales_Buyer_Accounting_Template k ON k.Buyer_ID=j.BM_ID  And k.Buyer_Desc='VAT'
                    Left Join Sales_Buyer_Accounting_Template l ON l.Buyer_ID=j.BM_ID  And l.Buyer_Desc='TAX'
                    Left Join Sales_Buyer_Accounting_Template m ON m.Buyer_ID=j.BM_ID  And m.Buyer_Desc='PAN'
                    Left Join Sales_Buyer_Accounting_Template n ON n.Buyer_ID=j.BM_ID  And n.Buyer_Desc='TAN'
                    Left Join Sales_Buyer_Accounting_Template o ON o.Buyer_ID=j.BM_ID  And o.Buyer_Desc='TIN'
                    Left Join Sales_Buyer_Accounting_Template p ON p.Buyer_ID=j.BM_ID  And p.Buyer_Desc='CIN' "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAvailableStockOfThisItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Double
        Dim sSql As String = ""
        Dim dAvailableStock As Double
        Try
            sSql = "Select Sum(SL_ClosingBalanceQty) As SL_ClosingBalanceQty From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_CompID=" & iCompID & " "
            dAvailableStock = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return dAvailableStock
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
    Public Function GetApplicationStartDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        Dim sDate As String = ""
        Try
            sSql = "Select AS_StartDate From Application_Settings Where AS_YearID=" & iYearID & " And AS_CompID=" & iCompID & " "
            sDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getImageName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As String
        Dim sSql As String = ""
        Dim sImageName As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT (PS_FileName + '.' + PS_Extn) As PS_FileName FROM Print_Settings WHERE PS_Status='" & sStr & "'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If IsDBNull(dt.Rows(0)("PS_FileName")) = False Then
                sImageName = dt.Rows(0)("PS_FileName")
                If sImageName = "NULL.NULL" Then
                    sImageName = ""
                End If
            Else
                sImageName = ""
            End If

            'If IsDBNull(objDBL.SQLGetDescription(sNameSpace, sSql)) = False Then
            '    sImageName = ""
            'Else
            '    sImageName = objDBL.SQLGetDescription(sNameSpace, sSql)
            'End If
            Return sImageName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBuyerOrderDtae(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String = ""
        Dim sBuyerOrderDate As String = ""
        Try
            sSql = "Select CONVERT(VARCHAR(10),SPO_BuyerOrderDate,103)As SPO_BuyerOrderDate From Sales_ProForma_Order Where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " ANd SPO_YearID=" & iYearID & " "
            sBuyerOrderDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            If sBuyerOrderDate <> "30/12/1899" Then
            Else
                sBuyerOrderDate = ""
            End If
            Return sBuyerOrderDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow

        Dim dTradeDiscountAmt As Double
        Try

            dt1.Columns.Add("VAT")
            dt1.Columns.Add("Amount")
            dt1.Columns.Add("VATAmount")

            sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")

                    dRow("VAT") = sVatDesc
                    dRow("Amount") = sAmountTot
                    dRow("VATAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            ElseIf dt.Rows.Count = 1 Then
                dRow = dt1.NewRow()
                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(0)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_VAT=" & dt.Rows(0)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_VAT=" & dt.Rows(0)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                dTradeDiscountAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPO_GrandDiscountAmt) From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")

                dRow("VAT") = sVatDesc
                dRow("Amount") = sAmountTot
                dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((sAmountTot - dTradeDiscountAmt) * sVatDesc) / 100))

                dt1.Rows.Add(dRow)
                sVatDesc = "" : sAmountTot = "" : sVATAmt = "" : dTradeDiscountAmt = 0
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt As String
        Dim sAmountTot As String = ""
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow

        Dim dTradeDiscountAmt As Double
        Try

            dt1.Columns.Add("CST")
            dt1.Columns.Add("CAmount")
            dt1.Columns.Add("CSTAmount")

            sSql = "Select Distinct(SPOD_CST) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")

                    If dt.Rows(i)("SPOD_CST") > 0 Then
                        sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    End If
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_CSTAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")

                    dRow("CST") = sVatDesc
                    dRow("CAmount") = sAmountTot
                    dRow("CSTAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            ElseIf dt.Rows.Count = 1 Then
                dRow = dt1.NewRow()
                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(0)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")

                If dt.Rows(0)("SPOD_CST") > 0 Then
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_CST=" & dt.Rows(0)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                End If
                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_CSTAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_CST=" & dt.Rows(0)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                dTradeDiscountAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPO_GrandDiscountAmt) From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")

                dRow("CST") = sVatDesc
                dRow("CAmount") = sAmountTot
                dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((sAmountTot - dTradeDiscountAmt) * sVatDesc) / 100))

                dt1.Rows.Add(dRow)
                sVatDesc = "" : sAmountTot = "" : sVATAmt = "" : dTradeDiscountAmt = 0
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getImagePath(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As String
        Dim sSql As String = ""
        Dim sImageName As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT (PS_FileName + '.' + PS_Extn) As PS_FileName FROM Print_Settings WHERE PS_Status='" & sStr & "'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If IsDBNull(dt.Rows(0)("PS_FileName")) = False Then
                sImageName = "~/Images/" & dt.Rows(0)("PS_FileName")
                If sImageName = "NULL.NULL" Then
                    sImageName = ""
                End If
            Else
                sImageName = ""
            End If

            Return sImageName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetReportTypeFromPrintSettings(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Dim iReportTypeID As Integer
        Try
            sSql = "Select PS_RptType From print_settings Where PS_Status='S' "
            iReportTypeID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iReportTypeID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEffectiveDates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINVHID As Integer) As String
        Dim sSql As String = ""
        Dim sEffectiveToDate As String = ""
        Try
            sEffectiveToDate = objDBL.SQLGetDescription(sNameSpace, "Select CONVERT(VARCHAR(10),INVH_EffeTo,103) From Inventory_Master_History Where INVH_ID=" & iINVHID & " And INVH_CompID=" & iCompID & " ")
            If sEffectiveToDate <> "01/01/1900" And sEffectiveToDate <> "01-01-1900" Then
                sSql = "" : sSql = "Select CONVERT(VARCHAR(10),INVH_EffeFrom,103) + '  To  ' + CONVERT(VARCHAR(10),INVH_EffeTo,103) As INVH_Dates  From Inventory_Master_History Where INVH_ID=" & iINVHID & " And INVH_CompID=" & iCompID & " "
                GetEffectiveDates = objDBL.SQLGetDescription(sNameSpace, sSql)
            Else
                sSql = "" : sSql = "Select CONVERT(VARCHAR(10),INVH_EffeFrom,103) As INVH_Dates From Inventory_Master_History Where INVH_ID=" & iINVHID & " And INVH_CompID=" & iCompID & " "
                GetEffectiveDates = objDBL.SQLGetDescription(sNameSpace, sSql)
            End If
            Return GetEffectiveDates
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAttachFiles(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As String, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select pge_Orignalfilename,pge_ext,pge_createdon from EDT_Page Where PGE_BaseName=" & iBaseName & " And PGE_FOLDER=" & iBatchNo & " And pge_CompID=" & iCompID & " "
            BindAttachFiles = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindAttachFiles
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCheckHistoryID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iCategory As Integer, ByVal sCode As String, ByVal dOrderDate As Date, ByVal sRate As String) As Integer
        Dim iHistoryID As String = "", sSql As String = ""
        Try
            If iCategory > 0 Then
                If sCode = "P" Then
                    sSql = "Select INVH_ID From Inventory_Master_History Where INVH_Retail='" & sRate & "' And InvH_CompID =" & iCompID & " And INVH_CategoryID=" & iCategory & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                ElseIf sCode = "C" Then
                    sSql = "SELECT INVH_ID FROM Inventory_Master_History WHERE INVH_MRP='" & sRate & "' And INVH_CompID=" & iCompID & " And INVH_CategoryID=" & iCategory & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                End If
                iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                If sCode = "P" Then
                    sSql = "Select INVH_ID From Inventory_Master_History Where INVH_Retail='" & sRate & "' And InvH_CompID =" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_RetailEffeFrom and INVH_RetailEffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_RetailEffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_RetailEffeTo ='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                ElseIf sCode = "C" Then
                    sSql = "SELECT INVH_ID FROM Inventory_Master_History WHERE INVH_MRP='" & sRate & "' And INVH_CompID=" & iCompID & " And (((Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') between INVH_EffeFrom and INVH_EffeTo And INVH_INV_ID in (" & iItemID & "))) Or (INVH_EffeFrom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(dOrderDate, "CT") & "') And (INVH_EffeTo='1900-01-01') And INVH_INV_ID in (" & iItemID & ")))"
                End If
                iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iHistoryID
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
End Class
