Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsPurchaseOrder
    Dim objDB As New DBHelper
    Dim objClsFasgnrl As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions
    Private objFAS As New clsFASGeneral
    Private POM_ID As Integer
    Private POM_OrderDate As Date
    Private POM_OrderNo As String
    Private POM_InwardNo As String
    Private POM_Status As String
    Private POM_Supplier As Integer
    Private POM_DSchdule As Integer
    Private POM_ModeOfShipping As Integer
    Private POM_MethodofPayment As Integer
    Private POM_Paymentterms As Integer
    Private POM_CreatedBy As Integer
    Private POM_ApporvedBy As Integer
    Private POM_YearID As Integer
    Private POD_ID As Integer
    Private POD_MasterID As Integer
    Private POD_Commodity As Integer
    Private POD_DescriptionID As Integer
    Private POD_HistoryID As Integer
    Private POD_Unit As Integer
    Private POD_Rate As String
    Private POM_CSTCtgry As Integer
    Private POM_SaleType As Integer
    Private POD_Quantity As String
    Private POD_RateAmount As String
    Private POD_Discount As String
    Private POD_DiscountAmount As String
    Private POD_Excise As String
    Private POD_ExciseAmount As String
    Private POD_Frieght As String
    Private POD_FrieghtAmount As String
    Private POD_VAT As String
    Private POD_VATAmount As String
    Private POD_CST As String
    Private POD_CSTAmount As String
    Private POD_RequiredDate As Date
    Private POD_TotalAmount As String
    Private POD_CompID As Integer
    Private sPOM_DocRef As String
    Private iPOD_Rejected As Integer
    Private iPOD_Accepted As Integer
    Private iPOD_ReceivedQty As Integer
    Private iPOD_OrderedQty As Integer
    Private POD_IPAddress As String
    Private BatchNumber As String
    Private POM_DEliveryChlnNo As String
    Private OralOrPO As String
    Private POM_InvoiceRef As String
    Private POM_ESugam As String
    Private POD_FETotalAmt As String
    Private POD_Currency As Integer
    Private POD_CurrencyAmt As Double
    Private POD_CurrencyTime As String

    Private iPOM_TrType As Integer
    Private sPOM_CompanyAddress As String
    Private sPOM_CompanyGSTNRegNo As String
    Private sPOM_BillingAddress As String
    Private sPOM_BillingGSTNRegNo As String
    Private sPOM_DeliveryAddress As String
    Private sPOM_DeliveryGSTNRegNo As String
    Private sPOM_DeliveryFrom As String
    Private sPOM_DeliveryFromGSTNRegNo As String
    Private sPOM_PurchaseStatus As String
    Private iPOM_CompanyType As Integer
    Private iPOM_GSTNCategory As Integer

    Private iPOM_ZoneID As Integer
    Private iPOM_RegionID As Integer
    Private iPOM_AreaID As Integer
    Private iPOM_BranchID As Integer

    Private iPOM_BatchNo As Integer
    Private iPOM_BaseName As Integer

    Private iPOD_GST_ID As Integer
    Private sPOD_GSTRate As Double
    Private sPOD_GSTAmount As Double
    Private iPOD_SGST As Double
    Private sPOD_SGSTAmount As String
    Private iPOD_CGST As Double
    Private sPOD_CGSTAmount As String
    Private iPOD_IGST As Double
    Private sPOD_IGSTAmount As String

    Private iC_ID As Integer
    Private iC_POrderID As Integer
    Private iC_PGinID As Integer
    Private iC_PInvoiceDocRef As Integer
    Private sC_OrderType As String
    Private iC_ChargeID As Integer
    Private sC_ChargeType As String
    Private iC_ChargeAmount As Double
    Private sC_PSType As String
    Private sC_DelFlag As String
    Private sC_Status As String
    Private iC_YearID As Integer
    Private iC_CompID As Integer
    Private iC_CreatedBy As Integer
    Private iC_CreatedOn As Date
    Private sC_Operation As String
    Private sC_IPAddress As String

    Public Property POM_ZoneID() As Integer
        Get
            Return (iPOM_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            iPOM_ZoneID = Value
        End Set
    End Property
    Public Property POM_RegionID() As Integer
        Get
            Return (iPOM_RegionID)
        End Get
        Set(ByVal Value As Integer)
            iPOM_RegionID = Value
        End Set
    End Property
    Public Property POM_AreaID() As Integer
        Get
            Return (iPOM_AreaID)
        End Get
        Set(ByVal Value As Integer)
            iPOM_AreaID = Value
        End Set
    End Property
    Public Property POM_BranchID() As Integer
        Get
            Return (iPOM_BranchID)
        End Get
        Set(ByVal Value As Integer)
            iPOM_BranchID = Value
        End Set
    End Property
    Public Property POM_BatchNo() As Integer
        Get
            Return (iPOM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iPOM_BatchNo = Value
        End Set
    End Property
    Public Property POM_BaseName() As Integer
        Get
            Return (iPOM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iPOM_BaseName = Value
        End Set
    End Property

    Public Property sOralOrPO() As String
        Get
            Return (OralOrPO)
        End Get
        Set(ByVal Value As String)
            OralOrPO = Value
        End Set
    End Property
    Public Property sPOM_DEliveryChlnNo() As String
        Get
            Return (POM_DEliveryChlnNo)
        End Get
        Set(ByVal Value As String)
            POM_DEliveryChlnNo = Value
        End Set
    End Property
    Public Property sPOD_IPAddress() As String
        Get
            Return (POD_IPAddress)
        End Get
        Set(ByVal Value As String)
            POD_IPAddress = Value
        End Set
    End Property
    Public Property POD_Accepted() As Integer
        Get
            Return (iPOD_Accepted)
        End Get
        Set(ByVal Value As Integer)
            iPOD_Accepted = Value
        End Set
    End Property
    Public Property POD_Rejected() As Integer
        Get
            Return (iPOD_Rejected)
        End Get
        Set(ByVal Value As Integer)
            iPOD_Rejected = Value
        End Set
    End Property
    Public Property POM_DocRef() As String
        Get
            Return (sPOM_DocRef)
        End Get
        Set(ByVal Value As String)
            sPOM_DocRef = Value
        End Set
    End Property
    Public Property sPOD_FETotalAmt() As String
        Get
            Return (POD_FETotalAmt)
        End Get
        Set(ByVal Value As String)
            POD_FETotalAmt = Value
        End Set
    End Property
    Public Property iPOD_Currency() As Integer
        Get
            Return (POD_Currency)
        End Get
        Set(ByVal Value As Integer)
            POD_Currency = Value
        End Set
    End Property
    Public Property dPOD_CurrencyAmt() As Double
        Get
            Return (POD_CurrencyAmt)
        End Get
        Set(ByVal Value As Double)
            POD_CurrencyAmt = Value
        End Set
    End Property
    Public Property sPOD_CurrencyTime() As String
        Get
            Return (POD_CurrencyTime)
        End Get
        Set(ByVal Value As String)
            POD_CurrencyTime = Value
        End Set
    End Property
    Public Property sPOD_RateAmount() As String
        Get
            Return (POD_RateAmount)
        End Get
        Set(ByVal Value As String)
            POD_RateAmount = Value
        End Set
    End Property
    Public Property sPOD_TotalAmount() As String
        Get
            Return (POD_TotalAmount)
        End Get
        Set(ByVal Value As String)
            POD_TotalAmount = Value
        End Set
    End Property
    Public Property dPOD_RequiredDate() As DateTime
        Get
            Return (POD_RequiredDate)
        End Get
        Set(ByVal Value As DateTime)
            POD_RequiredDate = Value
        End Set
    End Property
    Public Property sPOD_CSTAmount() As String
        Get
            Return (POD_CSTAmount)
        End Get
        Set(ByVal Value As String)
            POD_CSTAmount = Value
        End Set
    End Property
    Public Property sPOD_CST() As String
        Get
            Return (POD_CST)
        End Get
        Set(ByVal Value As String)
            POD_CST = Value
        End Set
    End Property
    Public Property sPOD_VATAmount() As String
        Get
            Return (POD_VATAmount)
        End Get
        Set(ByVal Value As String)
            POD_VATAmount = Value
        End Set
    End Property
    Public Property sPOD_VAT() As String
        Get
            Return (POD_VAT)
        End Get
        Set(ByVal Value As String)
            POD_VAT = Value
        End Set
    End Property
    Public Property sPOD_ExciseAmount() As String
        Get
            Return (POD_ExciseAmount)
        End Get
        Set(ByVal Value As String)
            POD_ExciseAmount = Value
        End Set
    End Property
    Public Property sPOD_Excise() As String
        Get
            Return (POD_Excise)
        End Get
        Set(ByVal Value As String)
            POD_Excise = Value
        End Set
    End Property

    Public Property sPOD_Frieght() As String
        Get
            Return (POD_Frieght)
        End Get
        Set(ByVal Value As String)
            POD_Frieght = Value
        End Set
    End Property

    Public Property sPOD_FrieghtAmount() As String
        Get
            Return (POD_FrieghtAmount)
        End Get
        Set(ByVal Value As String)
            POD_FrieghtAmount = Value
        End Set
    End Property


    Public Property POD_ReceivedQty() As Integer
        Get
            Return (iPOD_ReceivedQty)
        End Get
        Set(ByVal Value As Integer)
            iPOD_ReceivedQty = Value
        End Set
    End Property

    Public Property POD_OrderedQty() As Integer
        Get
            Return (iPOD_OrderedQty)
        End Get
        Set(ByVal Value As Integer)
            iPOD_OrderedQty = Value
        End Set
    End Property
    Public Property sPOD_DiscountAmount() As String
        Get
            Return (POD_DiscountAmount)
        End Get
        Set(ByVal Value As String)
            POD_DiscountAmount = Value
        End Set
    End Property

    Public Property sPOD_Discount() As String
        Get
            Return (POD_Discount)
        End Get
        Set(ByVal Value As String)
            POD_Discount = Value
        End Set
    End Property
    Public Property sPOD_Quantity() As String
        Get
            Return (POD_Quantity)
        End Get
        Set(ByVal Value As String)
            POD_Quantity = Value
        End Set
    End Property

    Public Property sPOD_Rate() As String
        Get
            Return (POD_Rate)
        End Get
        Set(ByVal Value As String)
            POD_Rate = Value
        End Set
    End Property
    Public Property iPOD_Unit() As Integer
        Get
            Return (POD_Unit)
        End Get
        Set(ByVal Value As Integer)
            POD_Unit = Value
        End Set
    End Property
    Public Property iPOD_HistoryID() As Integer
        Get
            Return (POD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            POD_HistoryID = Value
        End Set
    End Property
    Public Property iPOD_DescriptionID() As Integer
        Get
            Return (POD_DescriptionID)
        End Get
        Set(ByVal Value As Integer)
            POD_DescriptionID = Value
        End Set
    End Property

    Public Property iPOD_Commodity() As Integer
        Get
            Return (POD_Commodity)
        End Get
        Set(ByVal Value As Integer)
            POD_Commodity = Value
        End Set
    End Property
    Public Property iPOD_MasterID() As Integer
        Get
            Return (POD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            POD_MasterID = Value
        End Set
    End Property

    Public Property iPOD_ID() As Integer
        Get
            Return (POD_ID)
        End Get
        Set(ByVal Value As Integer)
            POD_ID = Value
        End Set
    End Property

    Public Property iPOM_YearID() As Integer
        Get
            Return (POM_YearID)
        End Get
        Set(ByVal Value As Integer)
            POM_YearID = Value
        End Set
    End Property

    Public Property iPOM_SaleType() As Integer
        Get
            Return (POM_SaleType)
        End Get
        Set(ByVal Value As Integer)
            POM_SaleType = Value
        End Set
    End Property

    Public Property iPOM_iCSTCtgry() As Integer
        Get
            Return (POM_CSTCtgry)
        End Get
        Set(ByVal Value As Integer)
            POM_CSTCtgry = Value
        End Set
    End Property
    Public Property iPOM_ApporvedBy() As Integer
        Get
            Return (POM_ApporvedBy)
        End Get
        Set(ByVal Value As Integer)
            POM_ApporvedBy = Value
        End Set
    End Property
    Public Property iPOM_CreatedBy() As Integer
        Get
            Return (POM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            POM_CreatedBy = Value
        End Set
    End Property
    Public Property iPOM_MethodofPayment() As Integer
        Get
            Return (POM_MethodofPayment)
        End Get
        Set(ByVal Value As Integer)
            POM_MethodofPayment = Value
        End Set
    End Property

    Public Property iPOM_DSchdule() As Integer
        Get
            Return (POM_DSchdule)
        End Get
        Set(ByVal Value As Integer)
            POM_DSchdule = Value
        End Set
    End Property
    Public Property iPOM_Paymentterms() As Integer
        Get
            Return (POM_Paymentterms)
        End Get
        Set(ByVal Value As Integer)
            POM_Paymentterms = Value
        End Set
    End Property


    Public Property iPOM_ModeOfShipping() As Integer
        Get
            Return (POM_ModeOfShipping)
        End Get
        Set(ByVal Value As Integer)
            POM_ModeOfShipping = Value
        End Set
    End Property
    Public Property iPOM_Supplier() As Integer
        Get
            Return (POM_Supplier)
        End Get
        Set(ByVal Value As Integer)
            POM_Supplier = Value
        End Set
    End Property

    Public Property sBatchNumber() As String
        Get
            Return (BatchNumber)
        End Get
        Set(ByVal Value As String)
            BatchNumber = Value
        End Set
    End Property
    Public Property sPOM_OrderNo() As String
        Get
            Return (POM_OrderNo)
        End Get
        Set(ByVal Value As String)
            POM_OrderNo = Value
        End Set
    End Property

    Public Property sPOM_Status() As String
        Get
            Return (POM_Status)
        End Get
        Set(ByVal Value As String)
            POM_Status = Value
        End Set
    End Property

    Public Property sPOM_InwardNo() As String
        Get
            Return (POM_InwardNo)
        End Get
        Set(ByVal Value As String)
            POM_InwardNo = Value
        End Set
    End Property

    Public Property dPOM_OrderDate() As Date
        Get
            Return (POM_OrderDate)
        End Get
        Set(ByVal Value As Date)
            POM_OrderDate = Value
        End Set
    End Property
    Public Property iPOM_ID() As Integer
        Get
            Return (POM_ID)
        End Get
        Set(ByVal Value As Integer)
            POM_ID = Value
        End Set
    End Property

    Public Property sPOM_InvoiceRef() As String
        Get
            Return (POM_InvoiceRef)
        End Get
        Set(ByVal Value As String)
            POM_InvoiceRef = Value
        End Set
    End Property
    Public Property sPOM_ESugam() As String
        Get
            Return (POM_ESugam)
        End Get
        Set(ByVal Value As String)
            POM_ESugam = Value
        End Set
    End Property

    Public Property POM_TrType() As Integer
        Get
            Return (iPOM_TrType)
        End Get
        Set(ByVal Value As Integer)
            iPOM_TrType = Value
        End Set
    End Property
    Public Property POM_CompanyAddress() As String
        Get
            Return (sPOM_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sPOM_CompanyAddress = Value
        End Set
    End Property
    Public Property POM_CompanyGSTNRegNo() As String
        Get
            Return (sPOM_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPOM_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property POM_BillingAddress() As String
        Get
            Return (sPOM_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sPOM_BillingAddress = Value
        End Set
    End Property
    Public Property POM_BillingGSTNRegNo() As String
        Get
            Return (sPOM_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPOM_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property POM_DeliveryFrom() As String
        Get
            Return (sPOM_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sPOM_DeliveryFrom = Value
        End Set
    End Property
    Public Property POM_DeliveryFromGSTNRegNo() As String
        Get
            Return (sPOM_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPOM_DeliveryFromGSTNRegNo = Value
        End Set
    End Property

    Public Property POM_DeliveryAddress() As String
        Get
            Return (sPOM_DeliveryAddress)
        End Get
        Set(ByVal Value As String)
            sPOM_DeliveryAddress = Value
        End Set
    End Property
    Public Property POM_DeliveryGSTNRegNo() As String
        Get
            Return (sPOM_DeliveryGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPOM_DeliveryGSTNRegNo = Value
        End Set
    End Property
    Public Property POM_PurchaseStatus() As String
        Get
            Return (sPOM_PurchaseStatus)
        End Get
        Set(ByVal Value As String)
            sPOM_PurchaseStatus = Value
        End Set
    End Property
    Public Property POM_CompanyType() As Integer
        Get
            Return (iPOM_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iPOM_CompanyType = Value
        End Set
    End Property
    Public Property POM_GSTNCategory() As Integer
        Get
            Return (iPOM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iPOM_GSTNCategory = Value
        End Set
    End Property
    Public Property POD_GST_ID() As Integer
        Get
            Return (iPOD_GST_ID)
        End Get
        Set(ByVal Value As Integer)
            iPOD_GST_ID = Value
        End Set
    End Property
    Public Property POD_GSTRate() As Double
        Get
            Return (sPOD_GSTRate)
        End Get
        Set(ByVal Value As Double)
            sPOD_GSTRate = Value
        End Set
    End Property
    Public Property POD_GSTAmount() As Double
        Get
            Return (sPOD_GSTAmount)
        End Get
        Set(ByVal Value As Double)
            sPOD_GSTAmount = Value
        End Set
    End Property

    Public Property POD_SGST() As Double
        Get
            Return (iPOD_SGST)
        End Get
        Set(ByVal Value As Double)
            iPOD_SGST = Value
        End Set
    End Property
    Public Property POD_SGSTAmount() As Double
        Get
            Return (sPOD_SGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sPOD_SGSTAmount = Value
        End Set
    End Property
    Public Property POD_CGST() As Double
        Get
            Return (iPOD_CGST)
        End Get
        Set(ByVal Value As Double)
            iPOD_CGST = Value
        End Set
    End Property
    Public Property POD_CGSTAmount() As Double
        Get
            Return (sPOD_CGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sPOD_CGSTAmount = Value
        End Set
    End Property
    Public Property POD_IGST() As Double
        Get
            Return (iPOD_IGST)
        End Get
        Set(ByVal Value As Double)
            iPOD_IGST = Value
        End Set
    End Property
    Public Property POD_IGSTAmount() As Double
        Get
            Return (sPOD_IGSTAmount)
        End Get
        Set(ByVal Value As Double)
            sPOD_IGSTAmount = Value
        End Set
    End Property

    Public Property C_POrderID() As Integer
        Get
            Return (iC_POrderID)
        End Get
        Set(ByVal Value As Integer)
            iC_POrderID = Value
        End Set
    End Property
    Public Property C_PGinID() As Integer
        Get
            Return (iC_PGinID)
        End Get
        Set(ByVal Value As Integer)
            iC_PGinID = Value
        End Set
    End Property
    Public Property C_PInvoiceDocRef() As Integer
        Get
            Return (iC_PInvoiceDocRef)
        End Get
        Set(ByVal Value As Integer)
            iC_PInvoiceDocRef = Value
        End Set
    End Property
    Public Property C_OrderType() As String
        Get
            Return (sC_OrderType)
        End Get
        Set(ByVal Value As String)
            sC_OrderType = Value
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
    Public Property C_PSType() As String
        Get
            Return (sC_PSType)
        End Get
        Set(ByVal Value As String)
            sC_PSType = Value
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

    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_Parent = " & iCommodity & " and Inv_CompID =" & iCompID & ""
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadAllPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal icat As Integer, ByVal iorder As Integer, ByVal isuuplier As Integer, ByVal InvoiceId As Integer, ByVal iCommodity As Integer, ByVal iItem As Integer, ByVal ivat As String, ByVal iExcise As String, ByVal iCst As String, ByVal iDiscount As String, ByVal iFromDt As String, ByVal iToDt As String)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim dtDetails As New DataTable
    '    Dim flag2 As String = "" : Dim flag3 As String = "" : Dim OrderNo As String = 0 : Dim Orderdate As String = "" : Dim GinNo As String = 0 : Dim Supplier As String = 0 : Dim Description As String = 0 : Dim Commodity As String = 0
    '    Dim TotalAmt, BasicAmount As Double
    '    Dim pending As Double
    '    Try
    '        dt.Columns.Add("SlNo")
    '        dt.Columns.Add("Commodity")
    '        dt.Columns.Add("Description")
    '        dt.Columns.Add("PV_DocRefNo")
    '        dt.Columns.Add("OrderNo")
    '        dt.Columns.Add("Orderdate")
    '        dt.Columns.Add("GinNo")
    '        dt.Columns.Add("Supplier")
    '        dt.Columns.Add("AcceptedQntity")
    '        dt.Columns.Add("Orderedqty")
    '        dt.Columns.Add("ReceivedQnt")
    '        dt.Columns.Add("Excess")
    '        dt.Columns.Add("RejectedQty")
    '        dt.Columns.Add("PendingQty")
    '        dt.Columns.Add("Rate")
    '        dt.Columns.Add("VAT")
    '        dt.Columns.Add("VATAmt")
    '        dt.Columns.Add("CST")
    '        dt.Columns.Add("CSTAmt")
    '        dt.Columns.Add("Exise")
    '        dt.Columns.Add("ExiseAmt")
    '        dt.Columns.Add("Discount")
    '        dt.Columns.Add("DiscountAmt")
    '        dt.Columns.Add("TotalAmount")
    '        dt.Columns.Add("InvoiceDate")
    '        dt.Columns.Add("BasicAmount")
    '        sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_DocRefNo,PV_BillNo,PGM_InvoiceDate,"
    '        sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_MRP as Rate,b.PIA_Status,"
    '        sSql = sSql & " b.PIA_CompID,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Code,c.Inv_Color,c.Inv_Size,"
    '        sSql = sSql & " d.Inv_Description Commodity,"
    '        sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_RateAmount,g.POD_Discount as Discount,g.POD_DiscountAmount as DiscountAmount,g.POD_Excise as Excise,g.POD_ExciseAmount as ExciseAmount,"
    '        sSql = sSql & " g.POD_VAT as Vat,g.POD_VATAmount as VATAmount,g.POD_CST as CST,g.POD_CSTAmount as CSTAmount,g.POD_CompID,g.POD_Status,f.POM_Supplier,POM_OrderDate,"
    '        sSql = sSql & "  g.POD_Quantity,b.PIA_AcceptedQnt as AcceptedQnt,h.PGD_ReceivedQnt,h.PGD_RejectedQnt,h.PGD_Accepted,h.PGD_Excess,b.PIA_Excess,i.PIR_RejectedQty"
    '        sSql = sSql & "  from Purchase_verification"
    '        sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
    '        sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
    '        sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
    '        sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
    '        sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
    '        sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID  "
    '        sSql = sSql & " join Purchase_GIN_Details h on  PV_GinNo=h.PGD_MasterID and PIA_HistoryID=h.PGD_HistoryID"
    '        sSql = sSql & " left join Purchase_Invoice_Rejected i on PV_GinNo=i.PIR_GINID and PIA_HistoryID=i.PIR_HistoryID  where PIA_CompID=" & iCompID & ""
    '        If iorder <> 0 Then
    '            sSql = sSql & " And PV_OrderNo= " & iorder & " "
    '        End If
    '        If InvoiceId <> 0 Then
    '            sSql = sSql & " And PIA_GINID= " & InvoiceId & " "
    '        End If
    '        If isuuplier <> 0 Then
    '            sSql = sSql & " And POM_Supplier= " & isuuplier & " "
    '        End If

    '        If iCommodity <> 0 Then
    '            sSql = sSql & " And PIA_Commodity= " & iCommodity & " "
    '        End If
    '        If iItem <> 0 Then
    '            sSql = sSql & " And PIA_DescriptionID= " & iItem & " "
    '        End If
    '        If iFromDt <> "" And iToDt <> "" Then
    '            sSql = sSql & " and  POM_OrderDate between '" & iFromDt & "'  and '" & iToDt & " 23:59:59'"
    '        End If
    '        sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
    '        dtDetails = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        For i = 0 To dtDetails.Rows.Count - 1
    '            dRow = dt.NewRow()
    '            If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
    '                dRow("Commodity") = dtDetails.Rows(i)("Commodity")
    '            Else
    '                dRow("Commodity") = "Unknown"
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("Inv_Code")) = False Then
    '                dRow("SlNo") = i + 1
    '                dRow("Description") = dtDetails.Rows(i)("Inv_Code")
    '            Else
    '                dRow("Description") = "Unknown"
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_OrderNo")) = False Then
    '                dRow("OrderNo") = dtDetails.Rows(i)("POM_OrderNo")
    '            Else
    '                dRow("OrderNo") = "0"
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("PV_DocRefNo")) = False Then
    '                dRow("PV_DocRefNo") = dtDetails.Rows(i)("PV_DocRefNo")
    '            Else
    '                dRow("PV_DocRefNo") = "0"
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
    '                dRow("Orderdate") = clsTRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
    '            Else
    '                dRow("Orderdate") = "0"
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("PGM_GIN_Number")) = False Then
    '                dRow("GinNo") = dtDetails.Rows(i)("PGM_GIN_Number")
    '            Else
    '                dRow("GinNo") = "0"
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_Supplier")) = False Then
    '                dRow("Supplier") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "CSM_Name", "CSM_ID", dtDetails.Rows(i)("POM_Supplier"), "customerSupplierMaster")
    '            Else
    '                dRow("Supplier") = "Unknown"
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POD_Quantity")) = False Then
    '                dRow("Orderedqty") = dtDetails.Rows(i)("POD_Quantity")
    '            Else
    '                dRow("Orderedqty") = 0
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("PGD_Accepted")) = False Then
    '                dRow("AcceptedQntity") = Convert.ToDecimal(dtDetails.Rows(i)("PGD_Accepted"))
    '            Else
    '                dRow("AcceptedQntity") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("PGD_ReceivedQnt")) = False Then
    '                dRow("ReceivedQnt") = Convert.ToDecimal(dtDetails.Rows(i)("PGD_ReceivedQnt"))
    '            Else
    '                dRow("ReceivedQnt") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("PIA_Excess")) = False Then
    '                dRow("Excess") = Convert.ToDecimal(dtDetails.Rows(i)("PIA_Excess"))
    '            Else
    '                dRow("Excess") = 0
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("PIR_RejectedQty")) = False Then
    '                dRow("RejectedQty") = Convert.ToDecimal(dtDetails.Rows(i)("PIR_RejectedQty"))
    '            Else
    '                dRow("RejectedQty") = 0
    '            End If
    '            pending = dRow("Orderedqty") - dRow("ReceivedQnt")
    '            If pending < 0 Then
    '                dRow("PendingQty") = 0
    '            Else
    '                dRow("PendingQty") = pending
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("Rate")) = False Then
    '                dRow("Rate") = dtDetails.Rows(i)("Rate")
    '                TotalAmt = String.Format("{0:0.00}", Convert.ToDecimal(dRow("Rate") * dRow("AcceptedQntity")))
    '                BasicAmount = String.Format("{0:0.00}", Convert.ToDecimal(dRow("Rate") * dRow("AcceptedQntity")))
    '            Else
    '                dRow("Rate") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("Discount")) = False Then
    '                dRow("Discount") = dtDetails.Rows(i)("Discount")
    '            Else
    '                dRow("Discount") = "0"
    '            End If


    '            If IsDBNull(dtDetails.Rows(i)("DiscountAmount")) = False And dtDetails.Rows(i)("DiscountAmount") <> "" Then
    '                dRow("DiscountAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(BasicAmount) * Convert.ToDecimal(dtDetails.Rows(i)("Discount")) / 100))
    '            Else
    '                dRow("DiscountAmt") = "0"
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("Vat")) = False Then
    '                dRow("VAT") = dtDetails.Rows(i)("Vat")
    '            Else
    '                dRow("VAT") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("VATAmount")) = False Then
    '                dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("Vat")) / 100))
    '                'String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("VATAmount")))
    '                TotalAmt = TotalAmt + dRow("VATAmt")
    '            Else
    '                dRow("VATAmt") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("CST")) = False And dtDetails.Rows(i)("CST") <> "" Then
    '                dRow("CST") = dtDetails.Rows(i)("CST")
    '            Else
    '                dRow("CST") = 0
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("CSTAmount")) = False Then
    '                dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("CST")) / 100))
    '                'String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("CSTAmount")))
    '                TotalAmt = TotalAmt + dRow("CSTAmt")
    '            Else
    '                dRow("CSTAmt") = 0
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("Excise")) = False Then
    '                dRow("Exise") = dtDetails.Rows(i)("Excise")
    '            Else
    '                dRow("Exise") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("ExciseAmount")) = False Then
    '                dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("Excise")) / 100))
    '                TotalAmt = TotalAmt + dRow("ExiseAmt")
    '            Else
    '                dRow("ExiseAmt") = 0
    '            End If

    '            If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
    '                dRow("InvoiceDate") = clsTRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate"), "D")
    '            Else
    '                dRow("InvoiceDate") = "0"
    '            End If
    '            dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt) - Convert.ToDecimal(dRow("DiscountAmt")))
    '            dRow("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(BasicAmount) - Convert.ToDecimal(dRow("DiscountAmt")))

    '            dt.Rows.Add(dRow)
    '        Next
    '        ' sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " And CSM_DelFlag='A'"
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function LoadAllPurchaseDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtZoneRegionBranchAreaDetails As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String, sModuleRole As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("PoID")
            dt.Columns.Add("POnO")
            dt.Columns.Add("PoDate")
            dt.Columns.Add("Supplier")
            dt.Columns.Add("Status")
            dt.Columns.Add("TotaAmount")
            dt.Columns.Add("Branch")

            sSql = "SELECT POM_ID,POM_OrderDate,POM_OrderNo,POM_Supplier,POM_Status,POM_DelFlag,POM_TotAmount,POM_BranchID FROM PURCHASE_ORDER_MASTER where POM_OralStatus='P'"
            dtDetails = objDB.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("POM_ID")) = False Then
                    dRow("PoID") = dtDetails.Rows(i)("POM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_OrderNo")) = False Then
                    dRow("POnO") = dtDetails.Rows(i)("POM_OrderNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                    dRow("PoDate") = objFas.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_Supplier")) = False Then
                    dRow("Supplier") = objDB.SQLExecuteScalar(sAC, "select CSM_Name from CustomerSupplierMaster where CSM_ID= " & dtDetails.Rows(i)("POM_Supplier") & "")
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_DelFlag")) = False Then
                    If dtDetails.Rows(i)("POM_Status") = "W" Then
                        dRow("Status") = "Waiting for Approval"
                    ElseIf dtDetails.Rows(i)("POM_Status") = "D" Then
                        dRow("Status") = "De-Activated"
                    ElseIf (dtDetails.Rows(i)("POM_Status") = "A") Then
                        dRow("Status") = "Activated"
                    ElseIf dtDetails.Rows(i)("POM_Status") = "L" Then
                        dRow("Status") = "Lock"
                    ElseIf dtDetails.Rows(i)("POM_Status") = "B" Then
                        dRow("Status") = "Block"
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_TotAmount")) = False Then
                    dRow("TotaAmount") = objClsFasgnrl.ReplaceSafeSQL(dtDetails.Rows(i)("POM_TotAmount"))
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_BranchID")) = False Then
                    dRow("Branch") = objDB.SQLGetDescription(sAC, "Select Org_Name From Sad_Org_Structure Where org_Node=" & dtDetails.Rows(i)("POM_BranchID") & " ")
                Else
                    dRow("Branch") = ""
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAllOralPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim sl As Integer = 0
        Dim i As Integer = 0
        Try
            dc = New DataColumn("SLNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GinNumber", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            'sSql = "Select * from purchase_gin_master where PGM_YearID=" & iYearID & " And PGM_CompID =" & iCompID & " "
            'sSql = sSql & " Order By PGM_ID ASC"

            'ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            'If ds.Tables(0).Rows.Count > 0 Then

            sSql = "SELECT * FROM PURCHASE_ORDER_MASTER where POM_OralStatus='O'"
            ds = objDB.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    sl = sl + 1
                    dr("SLNo") = sl
                    If IsDBNull(ds.Tables(0).Rows(i)("POM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("POM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("POM_InvoiceRef").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("POM_InvoiceRef").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("POM_OrderDate").ToString()) = False Then
                        dr("InvoiceDate") = objClsFasgnrl.FormatDtForRDBMS(ds.Tables(0).Rows(i)("POM_OrderDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("POM_Supplier").ToString()) = False Then
                        dr("Customer") = objDB.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("POM_Supplier").ToString() & " And CSM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("POM_OrderNo").ToString()) = False Then
                        dr("GinNumber") = ds.Tables(0).Rows(i)("POM_OrderNo").ToString()
                    End If
                    '  objDB.SQLCheckForRecord(sNameSpace, "Select * From purchase_verification Where PV_OrderNo=" & ds.Tables(0).Rows(i)("POM_ID").ToString() & " And PV_DocRefNo=" & ds.Tables(0).Rows(i)("PV_DocRefNo").ToString() & " ")
                    ' If IsDBNull(objDB.SQLCheckForRecord(sNameSpace, "Select * From purchase_verification Where PV_OrderNo=" & ds.Tables(0).Rows(i)("POM_ID").ToString() & " And PV_DocRefNo=" & ds.Tables(0).Rows(i)("PV_DocRefNo").ToString() & " ")) = False Then
                    ' If (ds.Tables(0).Rows(i)("PGM_Status").ToString() = "W") Then
                    'dr("Status") = "Approved"

                    ' Else
                    '  dr("Status") = "Waiting For Approval"
                    '  End If
                    If IsDBNull(ds.Tables(0).Rows(i)("POM_DelFlag")) = False Then
                        If ds.Tables(0).Rows(i)("POM_Status") = "W" Then
                            dr("Status") = "Waiting for Approval"
                        ElseIf (ds.Tables(0).Rows(i)("POM_Status") = "A") Then
                            dr("Status") = "activated"
                        ElseIf (ds.Tables(0).Rows(i)("POM_Status") = "D") Then
                            dr("Status") = "De-activated"
                        End If
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadAllOralPurchaseDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
    '    Dim dt As New DataTable, dtZoneRegionBranchAreaDetails As New DataTable, dtDetails As New DataTable
    '    Dim dRow As DataRow
    '    Dim sSql As String, sModuleRole As String = ""
    '    Try
    '        dt.Columns.Add("SrNo")
    '        dt.Columns.Add("PoID")
    '        dt.Columns.Add("POnO")
    '        dt.Columns.Add("PoDate")
    '        dt.Columns.Add("Supplier")
    '        dt.Columns.Add("Status")
    '        dt.Columns.Add("TotaAmount")

    '        sSql = "SELECT POM_ID,POM_OrderDate,POM_OrderNo,POM_Supplier,POM_Status,POM_DelFlag,POM_TotAmount FROM PURCHASE_ORDER_MASTER where POM_OralStatus='O'"
    '        dtDetails = objDB.SQLExecuteDataTable(sAC, sSql)
    '        For i = 0 To dtDetails.Rows.Count - 1
    '            dRow = dt.NewRow()
    '            dRow("SrNo") = i + 1
    '            If IsDBNull(dtDetails.Rows(i)("POM_ID")) = False Then
    '                dRow("PoID") = dtDetails.Rows(i)("POM_ID")
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_OrderNo")) = False Then
    '                dRow("POnO") = dtDetails.Rows(i)("POM_OrderNo")
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
    '                dRow("PoDate") = objFAS.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_Supplier")) = False Then
    '                dRow("Supplier") = objDB.SQLExecuteScalar(sAC, "select CSM_Name from CustomerSupplierMaster where CSM_ID= " & dtDetails.Rows(i)("POM_Supplier") & "")
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_DelFlag")) = False Then
    '                If dtDetails.Rows(i)("POM_Status") = "W" Then
    '                    dRow("Status") = "Waiting for Approval"
    '                ElseIf dtDetails.Rows(i)("POM_Status") = "D" Then
    '                    dRow("Status") = "De-Activated"
    '                ElseIf (dtDetails.Rows(i)("POM_Status") = "A") Then
    '                    dRow("Status") = "Activated"
    '                ElseIf dtDetails.Rows(i)("POM_Status") = "L" Then
    '                    dRow("Status") = "Lock"
    '                ElseIf dtDetails.Rows(i)("POM_Status") = "B" Then
    '                    dRow("Status") = "Block"
    '                End If
    '            End If
    '            If IsDBNull(dtDetails.Rows(i)("POM_TotAmount")) = False Then
    '                dRow("TotaAmount") = objClsFasgnrl.ReplaceSafeSQL(dtDetails.Rows(i)("POM_TotAmount"))
    '            End If
    '            dt.Rows.Add(dRow)
    '        Next
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Public Function LoadDescritionStart(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_CompID =" & iCompID & " And Inv_Code <> '' and Inv_Parent <> 0"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetBrandValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "select Inv_Parent from inventory_master where Inv_ID=" & InvID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPrintFlagValue(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "select PS_RptType from print_settings where ps_status='P'"
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAlterNatePiceValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvHistryID As String) As Decimal
        Dim sSql As String = ""
        Try
            sSql = "Select INVH_PerPieces From Inventory_master_History Where InvH_ID ='" & InvHistryID & "' And INVH_CompID=" & iCompID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUnitsValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvHistryID As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select InvH_Unit From Inventory_master_History Where InvH_ID ='" & InvHistryID & "' And INVH_CompID=" & iCompID & " "
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDescriptionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer, ByVal Sdate As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtNew As New DataTable
        Dim dRow As DataRow
        Try
            dtNew.Columns.Add("InvH_ID")
            dtNew.Columns.Add("INVH_PreDeterminedPrice")
            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iDescriptionID & " And InvH_CompID =" & iCompID & " And INVH_PurchaseEffeFrom <= Convert(datetime,'" & objClsFasgnrl.FormatDtForRDBMS(Sdate, "CT") & "') "
                dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtNew.NewRow
                dRow("InvH_ID") = dt.Rows(i)("InvH_ID")
                dRow("INVH_PreDeterminedPrice") = dt.Rows(i)("INVH_PreDeterminedPrice") & " - " & objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("InvH_EffeFrom"), "D")
                dtNew.Rows.Add(dRow)
            Next
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function


    'Public Function LoadStockRateQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer, ByVal GInID As Integer, ByVal OrderId As Integer)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable, dtNew As New DataTable
    '    Dim dRow As DataRow
    '    Try
    '        dtNew.Columns.Add("SL_PurchaseQty")
    '        dtNew.Columns.Add("PurchaseRate")
    '        dtNew.Columns.Add("SL_HistoryID")
    '        sSql = "Select SL_PurchaseQty,PurchaseRate,SL_HistoryID from stock_ledger where SL_OrderID = " & OrderId & " And SL_CompID =" & iCompID & " And SL_GINID=" & GInID & ""
    '        dt = objDB.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        For i = 0 To dt.Rows.Count - 1
    '            dRow = dtNew.NewRow
    '            dRow("SL_PurchaseQty") = dt.Rows(i)("SL_PurchaseQty")
    '            dRow("PurchaseRate") = dt.Rows(i)("PurchaseRate")
    '            dRow("SL_HistoryID") = dt.Rows(i)("SL_HistoryID")
    '            dtNew.Rows.Add(dRow)
    '        Next
    '        Return dtNew
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Public Function GetPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from inventory_master_History where InvH_ID =" & iHistoryID & " And InvH_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Shared Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iTransactionID As String) As Integer
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "Update Purchase_Order_Master Set POM_Status='A',POM_ApporvedBy=" & iUserID & ",POM_ApprovedOn=GetDate() "
    '        sSql = sSql & "Where POM_OrderNo='" & iTransactionID & "' And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
    '        objDB.ExecuteNoNQuery(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As String) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select POM_Status From Purchase_Order_Master Where POM_ID='" & iTransactionID & "' And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
            sStatus = objDB.SQLGetDescription(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function


    ''Public Shared Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
    ''    Dim sSql As String = ""
    ''    Dim dt As New DataTable
    ''    Dim bCheckO As Boolean
    ''    Dim bCheckP As Boolean
    ''    Try
    ''        If sSearch <> "" Then
    ''            bCheckO = DBHelper.DBCheckForRecord(sNameSpace, "Select * From Purchase_Order_Master where POM_OrderNo ='" & sSearch & "' And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & "")
    ''            If bCheckO = True Then
    ''                sSql = "Select POM_ID,POM_OrderNo From Purchase_Order_Master where POM_OrderNo ='" & sSearch & "' And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & ""
    ''                dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    ''                'Else
    ''                '    bCheckP = DBHelper.DBCheckForRecord(sNameSpace, "Select * From Purchase_Order_Master where POM_Supplier='" & sSearch & "' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & "")
    ''                '    If bCheckP = True Then
    ''                '        sSql = "Select SPO_ID,SPO_OrderCode From Sales_Proforma_Order where SPO_OrderType='S' And SPO_PartyCode ='" & sSearch & "' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & ""
    ''                '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    ''                '    End If
    ''            End If
    ''        Else
    ''            sSql = "Select POM_ID,POM_OrderNo From Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & " Order By POM_ID Desc"
    ''            dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    ''        End If
    ''        Return dt
    ''    Catch ex As Exception
    ''        Throw
    ''    End Try
    ''End Function
    'Public Shared Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim bCheckO As Boolean
    '    Dim bCheckP As Boolean

    '    Try
    '        'If sSearch <> "" Then
    '        sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " and POM_Status<>'D' order by POM_ID desc"
    '        dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
    '        ' End If
    '        '    bCheckO = DBHelper.DBCheckForRecord(sNameSpace, "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
    '        '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
    '        '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
    '        '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
    '        '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " And POM_OrderNo ='" & sSearch & "' and POM_Status<>'D' order by POM_ID desc")
    '        '    If bCheckO = True Then
    '        '        sSql = "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
    '        '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
    '        '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
    '        '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
    '        '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " And POM_OrderNo ='" & sSearch & "' and POM_Status<>'D' order by POM_ID desc"
    '        '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        '    End If
    '        'Else
    '        '    sSql = "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
    '        '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
    '        '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
    '        '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
    '        '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " and POM_Status<>'D' order by POM_ID desc"
    '        '    dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        ' End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GeneratePurchaseOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = "", sMonth As String = "", sMonthCode As String = ""
        Dim sDate As String = "", sMaxID As String = "", sLastID As String = "", sSDate As String = ""
        Try
            sMaximumID = objDB.SQLGetDescription(sNameSpace, "Select Top 1 POM_ID From Purchase_Order_Master where POM_COmpID = " & iCompID & " Order By POM_ID Desc")
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
    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " and CSM_Delflag='A' order by CSM_Name"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Sub LoadCommodity()
    '    Try
    '        ddlCommodity.DataSource = clsCustomerOrder.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlCommodity.DataTextField = "Inv_Description"
    '        ddlCommodity.DataValueField = "Inv_ID"
    '        ddlCommodity.DataBind()
    '        ddlCommodity.Items.Insert(0, "--- Select Commodity ---")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Sub LoadCST()
    '    Try
    '        ddlCST.DataSource = clsInvenotryDetails.LoadCST(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlCST.DataTextField = "Mas_Desc"
    '        ddlCST.DataValueField = "Mas_ID"
    '        ddlCST.DataBind()
    '        ddlCST.Items.Insert(0, "--- Select CST ---")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBranchID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " and POM_Status<>'D' order by POM_ID desc"
            If iBranchID > 0 Then
                sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_BranchID=" & iBranchID & " And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " and POM_Status<>'D' and POM_OralStatus='P' order by POM_ID desc"
                Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Else
                sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " and POM_Status<>'D' and POM_OralStatus='P' order by POM_ID desc"
                Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dOrderDate As Date, ByVal objPO As clsPurchaseOrder) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Master where POM_OrderNo = '" & objPO.sPOM_OrderNo & "' and POM_CompID =" & iCompID & " and POM_YearID =" & objPO.iPOM_YearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then

                sSql = "" : sSql = "Update Purchase_Order_Master set POM_Supplier = " & objPO.iPOM_Supplier & ",POM_MPayment=" & objPO.iPOM_MethodofPayment & ",POM_PaymentTerms=" & objPO.iPOM_Paymentterms & ",POM_ModeOfShipping = " & objPO.iPOM_ModeOfShipping & ",POM_DSchdule=" & objPO.iPOM_DSchdule & ", "
                sSql = sSql & "POM_BillingAddress='" & objPO.sPOM_BillingAddress & "',POM_BillingGSTNRegNo='" & objPO.sPOM_BillingGSTNRegNo & "',POM_DeliveryFrom='" & objPO.sPOM_DeliveryFrom & "',POM_DeliveryFromGSTNRegNo='" & objPO.sPOM_DeliveryFromGSTNRegNo & "',"
                sSql = sSql & "POM_DeliveryAddress='" & objPO.sPOM_DeliveryAddress & "',POM_DeliveryGSTNRegNo='" & objPO.sPOM_DeliveryGSTNRegNo & "',POM_PurchaseStatus='" & objPO.POM_PurchaseStatus & "',POM_CompanyType=" & objPO.iPOM_CompanyType & ",POM_GSTNCategory=" & objPO.iPOM_GSTNCategory & ", "
                sSql = sSql & "POM_ZoneID=" & objPO.iPOM_ZoneID & ",POM_RegionID=" & objPO.iPOM_RegionID & ",POM_AreaID=" & objPO.iPOM_AreaID & ",POM_BranchID=" & objPO.iPOM_BranchID & ""
                sSql = sSql & " Where POM_OrderNo = '" & objPO.sPOM_OrderNo & "' and POM_CompID =" & iCompID & " and POM_YearID=" & objPO.iPOM_YearID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dt.Rows(0)("POM_ID")
            Else
                iMax = objGnrl.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Master", "POM_ID", "POM_CompID")
                sSql = "" : sSql = "Insert into Purchase_Order_Master(POM_ID,POM_OrderDate,POM_OrderNo,POM_Supplier,"
                sSql = sSql & "POM_ModeOfShipping,POM_Status,POM_CreatedBy,POM_CreatedOn,POM_YearID,POM_CompID,POM_MPayment,POM_PaymentTerms,"
                sSql = sSql & "POM_DSchdule,POM_TypeOfPurchase,POM_CstCategory,POM_DelFlag,POM_OralStatus,POM_TrType,POM_CompanyAddress,POM_CompanyGSTNRegNo,"
                sSql = sSql & "POM_BillingAddress,POM_BillingGSTNRegNo,POM_DeliveryFrom,POM_DeliveryFromGSTNRegNo,POM_DeliveryAddress,POM_DeliveryGSTNRegNo,POM_PurchaseStatus,"
                sSql = sSql & "POM_CompanyType,POM_GSTNCategory,POM_ZoneID,POM_RegionID,POM_AreaID,POM_BranchID,POM_BatchNo,POM_BaseName) "
                sSql = sSql & "Values(" & iMax & "," & objFAS.FormatDtForRDBMS(dOrderDate, "I") & ",'" & objPO.sPOM_OrderNo & "'," & objPO.iPOM_Supplier & ","
                sSql = sSql & "" & objPO.iPOM_ModeOfShipping & ",'" & objPO.sPOM_Status & "'," & objPO.iPOM_CreatedBy & ",GetDate()," & objPO.iPOM_YearID & "," & iCompID & "," & objPO.iPOM_MethodofPayment & "," & objPO.iPOM_Paymentterms & ","
                sSql = sSql & "" & objPO.iPOM_DSchdule & "," & objPO.iPOM_SaleType & "," & objPO.iPOM_iCSTCtgry & ",'W','" & objPO.OralOrPO & "'," & objPO.iPOM_TrType & ",'" & objPO.sPOM_CompanyAddress & "','" & objPO.sPOM_CompanyGSTNRegNo & "',"
                sSql = sSql & "'" & objPO.sPOM_BillingAddress & "','" & objPO.sPOM_BillingGSTNRegNo & "','" & objPO.sPOM_DeliveryFrom & "','" & objPO.sPOM_DeliveryFromGSTNRegNo & "','" & objPO.sPOM_DeliveryAddress & "',"
                sSql = sSql & "'" & objPO.sPOM_DeliveryGSTNRegNo & "','" & objPO.POM_PurchaseStatus & "'," & objPO.iPOM_CompanyType & "," & objPO.iPOM_GSTNCategory & ","
                sSql = sSql & " " & objPO.iPOM_ZoneID & "," & objPO.iPOM_RegionID & "," & objPO.iPOM_AreaID & "," & objPO.iPOM_BranchID & ",0,0)"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckApprovedOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select POM_Status from Purchase_Order_Master where POM_ID=" & OrderNo & " and POM_YearID =" & iYearID & " and POM_CompID =" & iCompID & " and POM_Status='W'"
            Return objDB.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dRequiredDate As Date, ByVal objPO As clsPurchaseOrder, ByVal iPKID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Details where POD_ID=" & iPKID & " And POD_MasterID = " & objPO.iPOD_MasterID & " and POD_Commodity = " & objPO.iPOD_Commodity & " and "
            sSql = sSql & "POD_DescriptionID = " & objPO.iPOD_DescriptionID & " and POD_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Purchase_Order_Details set POD_Unit = " & objPO.iPOD_Unit & ",POD_Rate='" & objPO.sPOD_Rate & "',POD_RateAmount = '" & objPO.sPOD_RateAmount & "',POD_Quantity='" & objPO.sPOD_Quantity & "',"
                sSql = sSql & "POD_Discount = '" & objPO.sPOD_Discount & "',POD_DiscountAmount='" & objPO.sPOD_DiscountAmount & "',POD_Excise='" & objPO.sPOD_Excise & "',"
                sSql = sSql & "POD_ExciseAmount = '" & objPO.sPOD_ExciseAmount & "',POD_VAT = '" & objPO.sPOD_VAT & "',POD_VATAmount='" & objPO.sPOD_VATAmount & "',"
                sSql = sSql & "POD_CST='" & objPO.sPOD_CST & "',POD_CSTAmount='" & objPO.sPOD_CSTAmount & "',"
                sSql = sSql & "POD_TotalAmount='" & objPO.sPOD_TotalAmount & "',POD_Status='W',POD_Frieght='" & objPO.sPOD_Frieght & "',POD_FrieghtAmount='" & objPO.sPOD_FrieghtAmount & "',POD_GST_ID=" & objPO.iPOD_GST_ID & ",POD_GSTRate=" & objPO.sPOD_GSTRate & ",POD_GSTAmount=" & objPO.sPOD_GSTAmount & ", "
                sSql = sSql & "POD_SGST=" & objPO.iPOD_SGST & ",POD_SGSTAmount=" & objPO.sPOD_SGSTAmount & ",POD_CGST=" & objPO.iPOD_CGST & ",POD_CGSTAmount=" & objPO.sPOD_CGSTAmount & ",POD_IGST=" & objPO.iPOD_IGST & ",POD_IGSTAmount=" & objPO.sPOD_IGSTAmount & ""
                sSql = sSql & " where POD_ID=" & iPKID & " And POD_MasterID = " & objPO.iPOD_MasterID & " and POD_Commodity = " & objPO.iPOD_Commodity & " and POD_DescriptionID = " & objPO.iPOD_DescriptionID & " and "
                sSql = sSql & "POD_HistoryID =" & objPO.iPOD_HistoryID & " and POD_CompID = " & iCompID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objGnrl.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Details", "POD_ID", "POD_CompID")
                sSql = "" : sSql = "Insert into Purchase_Order_Details(POD_ID,POD_MasterID,POD_Commodity,"
                sSql = sSql & " POD_DescriptionID,POD_HistoryID,POD_Unit,POD_Rate,POD_RateAmount,"
                sSql = sSql & " POD_Quantity,POD_Discount,POD_DiscountAmount,POD_Excise,"
                sSql = sSql & " POD_ExciseAmount,POD_VAT,POD_VATAmount,POD_CST,"
                sSql = sSql & " POD_CSTAmount,POD_TotalAmount,"
                sSql = sSql & " POD_CompID,POD_Status,POD_Frieght,POD_FrieghtAmount,POD_GST_ID,POD_GSTRate,POD_GSTAmount,POD_SGST,POD_SGSTAmount,"
                sSql = sSql & " POD_CGST,POD_CGSTAmount,POD_IGST,POD_IGSTAmount,POD_FETotalAmt,POD_Currency,POD_CurrencyAmt,POD_CurrencyTime)"
                sSql = sSql & " Values(" & iMax & "," & objPO.iPOD_MasterID & "," & objPO.iPOD_Commodity & ","
                sSql = sSql & " " & objPO.iPOD_DescriptionID & "," & objPO.iPOD_HistoryID & "," & objPO.iPOD_Unit & ",'" & objPO.sPOD_Rate & "','" & objPO.sPOD_RateAmount & "',"
                sSql = sSql & " '" & objPO.sPOD_Quantity & "','" & objPO.sPOD_Discount & "','" & objPO.POD_DiscountAmount & "','" & objPO.sPOD_Excise & "',"
                sSql = sSql & " '" & objPO.sPOD_ExciseAmount & "','" & objPO.sPOD_VAT & "','" & objPO.sPOD_VATAmount & "','" & objPO.sPOD_CST & "',"
                sSql = sSql & " '" & objPO.sPOD_CSTAmount & "','" & objPO.sPOD_TotalAmount & "'," & iCompID & ",'W','" & objPO.sPOD_Frieght & "','" & objPO.sPOD_FrieghtAmount & "'"
                sSql = sSql & " ," & objPO.iPOD_GST_ID & "," & objPO.sPOD_GSTRate & "," & objPO.sPOD_GSTAmount & "," & objPO.iPOD_SGST & "," & objPO.sPOD_SGSTAmount & ""
                sSql = sSql & " ," & objPO.iPOD_CGST & "," & objPO.sPOD_CGSTAmount & "," & objPO.iPOD_IGST & "," & objPO.sPOD_IGSTAmount & ",'" & objPO.sPOD_FETotalAmt & "'," & objPO.iPOD_Currency & ","
                sSql = sSql & "'" & objPO.dPOD_CurrencyAmt & "','" & objPO.sPOD_CurrencyTime & "')"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchaseORderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtPdetails As New DataTable
        Dim iSlNo As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("CommodityID")
            dt.Columns.Add("DescriptionID")
            dt.Columns.Add("HistoryID")
            dt.Columns.Add("UnitsID")
            dt.Columns.Add("SLNO")
            dt.Columns.Add("Goods")
            dt.Columns.Add("Units")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("RateAmount")

            dt.Columns.Add("Frieght")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")

            dt.Columns.Add("ExciseDuty")
            dt.Columns.Add("ExciseAmt")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")

            dt.Columns.Add("GSTID")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("SGST")
            dt.Columns.Add("SGSTAmount")
            dt.Columns.Add("CGST")
            dt.Columns.Add("CGSTAmount")
            dt.Columns.Add("IGST")
            dt.Columns.Add("IGSTAmount")

            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("CCurrency")
            dt.Columns.Add("FETotalAmount")
            sSql = "Select * from Purchase_Order_Details where POD_MasterID =" & iMasterID & " And POD_CompID=" & iCompID & " And  POD_Status='W' order by POD_ID"
            dtPdetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dtPdetails.Rows.Count > 0 Then
                For i = 0 To dtPdetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("ID") = dtPdetails.Rows(i)("POD_ID")
                    dRow("CommodityID") = dtPdetails.Rows(i)("POD_Commodity")
                    dRow("DescriptionID") = dtPdetails.Rows(i)("POD_DescriptionID")
                    dRow("HistoryID") = dtPdetails.Rows(i)("POD_HistoryID")
                    dRow("UnitsID") = dtPdetails.Rows(i)("POD_Unit")
                    iSlNo = iSlNo + 1
                    dRow("SLNO") = iSlNo
                    dRow("Goods") = objDB.SQLExecuteScalar(sNameSpace, "Select Inv_Code from Inventory_Master where Inv_ID='" & dtPdetails.Rows(i)("POD_DescriptionID") & "' and Inv_compid=" & iCompID & "")
                    dRow("Units") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dtPdetails.Rows(i)("POD_Unit") & "' and Mas_compid=" & iCompID & "")
                    dRow("Rate") = dtPdetails.Rows(i)("POD_Rate")
                    If IsDBNull(dtPdetails.Rows(i)("POD_Quantity")) = False Then
                        dRow("Quantity") = Math.Round(dtPdetails.Rows(i)("POD_Quantity"), 2)
                    End If
                    dRow("RateAmount") = dtPdetails.Rows(i)("POD_RateAmount")

                    dRow("Frieght") = dtPdetails.Rows(i)("POD_Frieght")
                    dRow("Discount") = dtPdetails.Rows(i)("POD_Discount")
                    dRow("DiscountAmt") = dtPdetails.Rows(i)("POD_DiscountAmount")

                    dRow("ExciseDuty") = ""
                    dRow("ExciseAmt") = ""
                    dRow("VAT") = ""
                    dRow("VATAmt") = ""
                    dRow("CST") = ""
                    dRow("CSTAmount") = ""

                    dRow("GSTID") = dtPdetails.Rows(i)("POD_GST_ID")
                    dRow("GSTRate") = dtPdetails.Rows(i)("POD_GSTRate")
                    dRow("GSTAmount") = dtPdetails.Rows(i)("POD_GSTAmount")
                    dRow("SGST") = dtPdetails.Rows(i)("POD_SGST")
                    dRow("SGSTAmount") = dtPdetails.Rows(i)("POD_SGSTAmount")
                    dRow("CGST") = dtPdetails.Rows(i)("POD_CGST")
                    dRow("CGSTAmount") = dtPdetails.Rows(i)("POD_CGSTAmount")
                    dRow("IGST") = dtPdetails.Rows(i)("POD_IGST")
                    dRow("IGSTAmount") = dtPdetails.Rows(i)("POD_IGSTAmount")

                    dRow("TotalAmount") = dtPdetails.Rows(i)("POD_TotalAmount")
                    If IsDBNull(dtPdetails.Rows(i)("POD_Currency")) = False Then
                        dRow("CCurrency") = objDB.SQLExecuteScalar(sNameSpace, "Select (CUR_CODE + ' [' + CUR_CountryName + ']') as OperateOn from Currency_master where CUR_ID=" & dtPdetails.Rows(i)("POD_Currency") & "")
                    End If
                    dRow("FETotalAmount") = dtPdetails.Rows(i)("POD_FETotalAmt")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteOrderValues(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal OrderNo As String, ByVal DcritionID As Integer, ByVal HistoryID As Integer, ByVal iPKID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_Order_Details set POD_Status='D' Where POD_ID=" & iPKID & " And POD_MasterID in(select POM_ID from Purchase_Order_Master "
            sSql = sSql & "where POM_OrderNo='" & OrderNo & "' and POM_YearID =" & iYearId & " and POM_COmpID =" & iCompID & " and  POM_Status='W') and POD_DescriptionID=" & DcritionID & " and POD_HistoryID=" & HistoryID & "   and POD_CompID = " & iCompID & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeleteOrderValuesFromMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal OrderNo As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_Order_Master set POM_Status='D' where POM_OrderNo='" & OrderNo & "' and POM_YearID =" & iYearId & " and POM_COmpID =" & iCompID & " and  POM_Status='W'"
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadUnitOFMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtNew As New DataTable
        Try
            dtNew.Columns.Add("Mas_ID")
            dtNew.Columns.Add("Mas_Desc")

            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iInvID & " And InvH_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_Unit")
                dRow("Mas_Desc") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_Delflag='A' And Mas_ID='" & dt.Rows(0)("InvH_Unit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)

                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_AlterUnit")
                dRow("Mas_Desc") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_Delflag='A' And Mas_ID='" & dt.Rows(0)("InvH_AlterUnit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)
            End If
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseOderMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPomID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Master where POM_ID = " & iPomID & " and POM_CompID = " & iCompID & " and POM_YearID =" & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseOderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iCommodity As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer, ByVal iPKID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Details where POD_ID=" & iPKID & " And POD_MasterID = " & iMasterID & " and POD_Commodity = " & iCommodity & " and "
            sSql = sSql & "POD_DescriptionID = " & iDescriptionID & " and  POD_HistoryID = " & iHistoryID & " and POD_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSupplierCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer)
        Dim sSql As String = "", sCOde As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select * from customerSupplierMaster where CSM_ID =" & iSupplier & " and CSM_CompID = " & iCompID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from inventory_Master_History where InvH_Id = " & iHistoryID & " and InvH_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetGeneralMasterValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal MasID As Integer, ByVal DescRiption As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_id from ACC_General_Master where Mas_master=" & MasID & " And Mas_desc ='" & DescRiption & "' "
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPaymentTerms(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=18 And Mas_Delflag='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDeliverySchdule(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=17 and Mas_Delflag='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadModeShiping(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=13 and Mas_Delflag='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadNumberOfDays(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=20 and Mas_Delflag='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMethodOfPayment(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=11 and Mas_Delflag='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdatePurchaseMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As String, ByVal iMasId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_order_master Set POM_IPAddress='" & sIPAddress & "',"
            If sStatus = "Created" Then
                sSql = sSql & " POM_DelFlag='A',POM_Status='A',POM_ApporvedBy= " & iUserID & ",POM_ApprovedOn=GetDate()"
            ElseIf sStatus = "DeActivated" Then
                sSql = sSql & " POM_DelFlag='D',POM_Status='D',POM_DeletedBy= " & iUserID & ",POM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " POM_DelFlag='A',POM_Status='A',POM_RecalledBy= " & iUserID & ""
            End If
            sSql = sSql & " Where POM_Id= " & iMasId & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPomID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtFRomTable As New DataTable
        Dim dRow As DataRow
        Dim subtotal As Double = 0
        Try
            dt.Columns.Add("POM_OrderNo")
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("POM_Supplier")
            dt.Columns.Add("POD_Commodity")
            dt.Columns.Add("POD_Rate")
            dt.Columns.Add("POD_Quantity")
            dt.Columns.Add("POD_CST")
            dt.Columns.Add("POD_VAT")
            dt.Columns.Add("POD_CSTAmount")
            dt.Columns.Add("POD_RateAmount")
            dt.Columns.Add("POD_Discount")
            dt.Columns.Add("POD_DiscountAmount")
            dt.Columns.Add("POD_TotalAmount")
            dt.Columns.Add("POD_VATAmount")
            dt.Columns.Add("POD_Unit")
            dt.Columns.Add("INV_Code")
            dt.Columns.Add("INV_Description")
            dt.Columns.Add("CSM_Name")
            dt.Columns.Add("CSM_Address")
            dt.Columns.Add("CSM_MobileNo")
            dt.Columns.Add("CSM_EmailID")
            dt.Columns.Add("POM_DeliveryFromGSTNRegNo")

            dt.Columns.Add("Mas_Desc")
            dt.Columns.Add("CUST_CODE")
            dt.Columns.Add("CUST_COMM_ADDRESS")
            dt.Columns.Add("CUST_EMAIL")
            dt.Columns.Add("CUST_COMM_TEL")
            dt.Columns.Add("CUST_NAME")
            dt.Columns.Add("POM_DeliveryGSTNRegNo")
            'dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Mdate")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("POD_Excise")
            dt.Columns.Add("POD_ExciseAmount")
            dt.Columns.Add("POD_Frieght")
            dt.Columns.Add("POD_FrieghtAmount")

            dt.Columns.Add("POD_GSTRate")
            dt.Columns.Add("POD_GSTAmount")
            dt.Columns.Add("POD_SGST")
            dt.Columns.Add("POD_SGSTAmount")
            dt.Columns.Add("POD_CGST")
            dt.Columns.Add("POD_CGSTAmount")
            dt.Columns.Add("POD_IGST")
            dt.Columns.Add("POD_IGSTAmount")

            sSql = "Select Distinct(b.POD_Quantity), POM_OrderNo, Convert(VARCHAR(10), POM_OrderDate, 103)As POM_OrderDate, POM_Supplier, b.POD_Commodity, b.POD_Rate,  "
            sSql = sSql & "b.POD_CST, Convert(money, b.POD_CSTAmount) As POD_CSTAmount, Convert(money, b.POD_RateAmount) As POD_RateAmount,"
            sSql = sSql & "Convert(money, b.POD_Discount)As POD_Discount,Convert(money,b.POD_DiscountAmount) As POD_DiscountAmount,"
            sSql = sSql & "Convert(money,b.POD_TotalAmount)As POD_TotalAmount,"
            sSql = sSql & "Convert(money,b.POD_VAT)As POD_VAT,Convert(money,b.POD_Excise)As POD_Excise,POD_Frieght,POD_FrieghtAmount,POD_ExciseAmount,Convert(money,b.POD_ExciseAmount)As POD_ExciseAmount,Convert(money,b.POD_VATAmount)As POD_VATAmount,b.POD_Unit,b.POD_GSTRate,b.POD_GSTAmount,b.POD_SGST,b.POD_SGSTAmount,b.POD_CGST,b.POD_CGSTAmount,b.POD_IGST,b.POD_IGSTAmount, "
            sSql = sSql & "c.INV_Code,c.INV_Description,d.CSM_Name,d.CSM_Address,d.CSM_MobileNo,d.CSM_EmailID,e.Mas_Desc,m.CUST_CODE,m.CUST_Name,m.CUST_COMM_ADDRESS,m.CUST_EMAIL,m.CUST_COMM_TEL,InvH.INVH_Mdate,InvH.INVH_Edate,POM_DeliveryFrom,POM_DeliveryFromGSTNRegNo,POM_DeliveryAddress,POM_DeliveryGSTNRegNo  "
            sSql = sSql & "From Purchase_Order_Master "
            sSql = sSql & "join Purchase_Order_Details b On POM_ID=" & iPomID & " And POM_ID=b.POD_MasterID And b.POD_Status <> 'D' "
            sSql = sSql & "Join Inventory_master_history InvH on  POD_DescriptionID=InvH.InvH_INV_ID  "
            sSql = sSql & "Join Inventory_master c on  POD_DescriptionID=c.INV_ID "
            sSql = sSql & "Join CustomerSupplierMaster d On POM_Supplier=d.CSM_ID "
            sSql = sSql & "Join Acc_General_master e on b.POD_Unit=e.Mas_ID "
            sSql = sSql & "Join MST_CUSTOMER_MASTER m on b.POD_CompID=m.CUST_ID "
            dtFRomTable = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dtFRomTable.Rows.Count > 0 Then
                For i = 0 To dtFRomTable.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtFRomTable.Rows(i)("POM_OrderNo")) = False Then
                        dRow("POM_OrderNo") = dtFRomTable.Rows(i)("POM_OrderNo")
                    Else
                        dRow("POM_OrderNo") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POM_OrderDate")) = False Then
                        dRow("POM_OrderDate") = dtFRomTable.Rows(i)("POM_OrderDate")
                    Else
                        dRow("POM_OrderDate") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Commodity")) = False Then
                        dRow("POD_Commodity") = dtFRomTable.Rows(i)("POD_Commodity")
                    Else
                        dRow("POD_Commodity") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Rate")) = False Then
                        dRow("POD_Rate") = dtFRomTable.Rows(i)("POD_Rate")
                    Else
                        dRow("POD_Rate") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Quantity")) = False Then
                        dRow("POD_Quantity") = dtFRomTable.Rows(i)("POD_Quantity")
                    Else
                        dRow("POD_Quantity") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_CST")) = False Then
                        dRow("POD_CST") = dtFRomTable.Rows(i)("POD_CST")
                    Else
                        dRow("POD_CST") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_VAT")) = False Then
                        dRow("POD_VAT") = dtFRomTable.Rows(i)("POD_VAT")
                    Else
                        dRow("POD_VAT") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("POD_CSTAmount") = Convert.ToDouble(dtFRomTable.Rows(i)("POD_CSTAmount"))
                    Else
                        dRow("POD_CSTAmount") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_RateAmount")) = False Then
                        dRow("POD_RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dtFRomTable.Rows(i)("POD_RateAmount") - dtFRomTable.Rows(i)("POD_DiscountAmount"))))
                        subtotal = subtotal + dRow("POD_RateAmount")
                    Else
                        dRow("POD_RateAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Discount")) = False Then
                        dRow("POD_Discount") = dtFRomTable.Rows(i)("POD_Discount")
                    Else
                        dRow("POD_Discount") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_DiscountAmount")) = False Then
                        dRow("POD_DiscountAmount") = dtFRomTable.Rows(i)("POD_DiscountAmount")
                    Else
                        dRow("POD_DiscountAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_TotalAmount")) = False Then
                        dRow("POD_TotalAmount") = dtFRomTable.Rows(i)("POD_TotalAmount")
                    Else
                        dRow("POD_TotalAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_VATAmount")) = False Then
                        dRow("POD_VATAmount") = dtFRomTable.Rows(i)("POD_VATAmount")
                    Else
                        dRow("POD_VATAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Unit")) = False Then
                        dRow("POD_Unit") = dtFRomTable.Rows(i)("POD_Unit")
                    Else
                        dRow("POD_Unit") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("INV_Code")) = False Then
                        dRow("INV_Code") = dtFRomTable.Rows(i)("INV_Code")
                    Else
                        dRow("INV_Code") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("INV_Description")) = False Then
                        dRow("INV_Description") = dtFRomTable.Rows(i)("INV_Description")
                    Else
                        dRow("INV_Description") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CSM_Name")) = False Then
                        dRow("CSM_Name") = dtFRomTable.Rows(i)("CSM_Name")
                    Else
                        dRow("CSM_Name") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POM_DeliveryFrom")) = False Then
                        dRow("CSM_Address") = dtFRomTable.Rows(i)("POM_DeliveryFrom")
                    Else
                        dRow("CSM_Address") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CSM_MobileNo")) = False Then
                        dRow("CSM_MobileNo") = dtFRomTable.Rows(i)("CSM_MobileNo")
                    Else
                        dRow("CSM_MobileNo") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("CSM_EmailID")) = False Then
                        dRow("CSM_EmailID") = dtFRomTable.Rows(i)("CSM_EmailID")
                    Else
                        dRow("CSM_EmailID") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POM_DeliveryFromGSTNRegNo")) = False Then
                        dRow("POM_DeliveryFromGSTNRegNo") = dtFRomTable.Rows(i)("POM_DeliveryFromGSTNRegNo")
                    Else
                        dRow("POM_DeliveryFromGSTNRegNo") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("Mas_Desc")) = False Then
                        dRow("Mas_Desc") = dtFRomTable.Rows(i)("Mas_Desc")
                    Else
                        dRow("Mas_Desc") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("CUST_CODE")) = False Then
                        dRow("CUST_CODE") = dtFRomTable.Rows(i)("CUST_CODE")
                    Else
                        dRow("CUST_CODE") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CUST_NAME")) = False Then
                        dRow("CUST_NAME") = dtFRomTable.Rows(i)("CUST_NAME")
                    Else
                        dRow("CUST_NAME") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POM_DeliveryAddress")) = False Then
                        dRow("CUST_COMM_ADDRESS") = dtFRomTable.Rows(i)("POM_DeliveryAddress")
                    Else
                        dRow("CUST_COMM_ADDRESS") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("CUST_EMAIL")) = False Then
                        dRow("CUST_EMAIL") = dtFRomTable.Rows(i)("CUST_EMAIL")
                    Else
                        dRow("CUST_EMAIL") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("CUST_COMM_TEL")) = False Then
                        dRow("CUST_COMM_TEL") = dtFRomTable.Rows(i)("CUST_COMM_TEL")
                    Else
                        dRow("CUST_COMM_TEL") = ""
                    End If
                    'If IsDBNull(dtFRomTable.Rows(i)("INVH_MRP")) = False Then
                    '    dRow("INVH_MRP") = dtFRomTable.Rows(i)("INVH_MRP")
                    'Else
                    '    dRow("INVH_MRP") = 0
                    'End If
                    If IsDBNull(dtFRomTable.Rows(i)("POM_DeliveryGSTNRegNo")) = False Then
                        dRow("POM_DeliveryGSTNRegNo") = dtFRomTable.Rows(i)("POM_DeliveryGSTNRegNo")
                    Else
                        dRow("POM_DeliveryGSTNRegNo") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("INVH_Mdate")) = False Then
                        dRow("INVH_Mdate") = dtFRomTable.Rows(i)("INVH_Mdate")
                    Else
                        dRow("INVH_Mdate") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("INVH_Edate")) = False Then
                        dRow("INVH_Edate") = dtFRomTable.Rows(i)("INVH_Edate")
                    Else
                        dRow("INVH_Edate") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_Excise")) = False Then
                        dRow("POD_Excise") = dtFRomTable.Rows(i)("POD_Excise")
                    Else
                        dRow("POD_Excise") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("POD_ExciseAmount") = dtFRomTable.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("POD_ExciseAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Frieght")) = False Then
                        dRow("POD_Frieght") = dtFRomTable.Rows(i)("POD_Frieght")
                    Else
                        dRow("POD_Frieght") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_FrieghtAmount")) = False Then
                        dRow("POD_FrieghtAmount") = dtFRomTable.Rows(i)("POD_FrieghtAmount")
                    Else
                        dRow("POD_FrieghtAmount") = 0
                    End If


                    If IsDBNull(dtFRomTable.Rows(i)("POD_GSTRate")) = False Then
                        dRow("POD_GSTRate") = dtFRomTable.Rows(i)("POD_GSTRate")
                    Else
                        dRow("POD_GSTRate") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_GSTAmount")) = False Then
                        dRow("POD_GSTAmount") = dtFRomTable.Rows(i)("POD_GSTAmount")
                    Else
                        dRow("POD_GSTAmount") = 0
                    End If


                    If IsDBNull(dtFRomTable.Rows(i)("POD_SGST")) = False Then
                        dRow("POD_SGST") = dtFRomTable.Rows(i)("POD_SGST")
                    Else
                        dRow("POD_SGST") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_SGSTAmount")) = False Then
                        dRow("POD_SGSTAmount") = dtFRomTable.Rows(i)("POD_SGSTAmount")
                    Else
                        dRow("POD_SGSTAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_CGST")) = False Then
                        dRow("POD_CGST") = dtFRomTable.Rows(i)("POD_CGST")
                    Else
                        dRow("POD_CGST") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_CGSTAmount")) = False Then
                        dRow("POD_CGSTAmount") = dtFRomTable.Rows(i)("POD_CGSTAmount")
                    Else
                        dRow("POD_CGSTAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_IGST")) = False Then
                        dRow("POD_IGST") = dtFRomTable.Rows(i)("POD_IGST")
                    Else
                        dRow("POD_IGST") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_IGSTAmount")) = False Then
                        dRow("POD_IGSTAmount") = dtFRomTable.Rows(i)("POD_IGSTAmount")
                    Else
                        dRow("POD_IGSTAmount") = 0
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
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
                If (hTable.Contains(DataRow("POD_VAT"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("POD_VAT"), String.Empty)
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
    Public Function GetGSTID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_ID From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTID = objDB.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyGSTNRegNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From MST_Customer_Master Where CUST_ID=" & iCompID & " "
            GetCompanyGSTNRegNo = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCompanyGSTNRegNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplierID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select CSM_GSTNCategory from CustomerSupplierMaster where CSM_ID=" & iSupplierID & " and CSM_CompID =" & iCompID & " "
            GetGSTCategory = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetGSTCategory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplierID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From CustomerSupplierMaster Where CSM_ID=" & iSupplierID & " And CSM_CompID=" & iCompID & " "
            GetCustomerDetails = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCustomerDetails
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
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
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
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getGSTRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String, ByVal sGSTNCategoryDesc As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GC_GSTRate From GSTCategory_Table Where GC_CompanyType='" & sCompanyType & "' And GC_GSTcategory= '" & sGSTNCategoryDesc & "' Order By GC_ID Desc "
            getGSTRate = objDB.SQLGetDescription(sNameSpace, sSql)
            Return getGSTRate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTRateFromHSNTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_GSTRate From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTRateFromHSNTable = objDB.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTRateFromHSNTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CUSTB_ID,CUSTB_Name From MST_CUSTOMER_MASTER_Branch Where CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
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
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBranchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_ID=" & iBranchID & " And CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_Name=" & iBranchID & " And CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function RemoveChargeDublicate(ByVal dt As DataTable) As DataTable
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
    Public Function DeleteCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Delete From Charges_Master Where C_POrderID=" & iOrderID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCharges(ByVal sNameSpace As String, ByVal objInvoiceForm As clsPurchaseOrder)
        Dim iMaxID As Integer
        Dim sSql As String = ""
        Try
            iMaxID = objDB.SQLExecuteScalar(sNameSpace, "select isnull(max(C_ID)+1,1) from Charges_master")
            sSql = "Insert Into Charges_master(C_ID, C_POrderID, C_PGinID, C_PInvoiceDocRef, C_OrderType, C_ChargeID, C_ChargeType, C_ChargeAmount, C_PSType, C_DelFlag, C_Status, C_CompID, C_YearID, C_CreatedBy, C_CreatedOn, C_Operation,C_IPAddress) "
            sSql = sSql & " Values(" & iMaxID & ", " & objInvoiceForm.C_POrderID & ", " & objInvoiceForm.C_PGinID & ", " & objInvoiceForm.C_PInvoiceDocRef & ", '" & objInvoiceForm.C_OrderType & "', " & objInvoiceForm.C_ChargeID & ", '" & objInvoiceForm.C_ChargeType & "', " & objInvoiceForm.C_ChargeAmount & ", '" & objInvoiceForm.C_PSType & "', '" & objInvoiceForm.C_DelFlag & "', '" & objInvoiceForm.C_Status & "', " & objInvoiceForm.C_CompID & ", " & objInvoiceForm.C_YearID & ", " & objInvoiceForm.C_CreatedBy & ", GetDate(), '" & objInvoiceForm.C_Operation & "','" & objInvoiceForm.C_IPAddress & "')"
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChargeType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=24 And Mas_DelFlag='A' "
            Return objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindChargeData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer, ByVal iGinID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("ChargeID")
            dtTab.Columns.Add("ChargeType")
            dtTab.Columns.Add("ChargeAmount")

            'If iInvoiceID > 0 Then
            '    sSql = "" : sSql = "Select * From Charges_Master,Purchase_Invoice_Master Where C_POrderID=PIM_OrderID And C_PGINID=PIM_PRegesterID And C_PInvoiceDocRef=PIM_ID And C_PSType='P' And C_POrderID=" & iOrderID & " And C_PGinID=" & iGinID & " And C_PInvoiceDocRef=" & iInvoiceID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            'Else
            sSql = "" : sSql = "Select * From Charges_Master,Purchase_Order_Master Where C_POrderID=POM_ID And C_PSType='P' And C_POrderID=" & iOrderID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " "
            'End If

            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ChargeID") = dt.Rows(i)("C_ChargeID")
                    dRow("ChargeType") = objDB.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt.Rows(i)("C_ChargeID") & " And Mas_Master=24 And Mas_CompID = " & iCompID & "  ")
                    dRow("ChargeAmount") = dt.Rows(i)("C_ChargeAmount")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGstCategory As Integer) As String
        Dim sSql As String = ""
        Try
            'sSql = "Select Mas_Desc from ACC_General_Master where mas_master = 2 and Mas_Id=" & iCompTypeId & " and Mas_CompID=" & iCompID & ""
            'Desc = objDB.SQLGetDescription(sNameSpace, sSql)

            sSql = "select GC_GSTCategory from GSTcategory_table where GC_ID=" & iGstCategory & " and GC_CompID=" & iCompID & ""
            GetGSTDescription = objDB.SQLGetDescription(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchaseOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("SrNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OrderNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OrderDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SupplierName", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from PURCHASE_ORDER_MASTER where POM_OralStatus='P' AND POM_YearID=" & iYearID & " And POM_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " and POM_Status = 'A'"
            ElseIf iStatus = 1 Then
                sSql = sSql & " and POM_Status = 'D'"
            ElseIf iStatus = 2 Then
                sSql = sSql & " and POM_Status = 'W'"
            End If


            ds = objDB.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    'If IsDBNull(ds.Tables(0).Rows(i)("SPO_ID").ToString()) = False Then
                    dr("SrNo") = i + 1
                    'End If

                    If IsDBNull(ds.Tables(0).Rows(i)("POM_OrderNo").ToString()) = False Then
                        dr("OrderNo") = ds.Tables(0).Rows(i)("POM_OrderNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("POM_OrderDate").ToString()) = False Then
                        dr("OrderDate") = objFAS.FormatDtForRDBMS(ds.Tables(0).Rows(i)("POM_OrderDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("POM_Supplier").ToString()) = False Then
                        dr("SupplierName") = objDB.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("POM_Supplier").ToString() & " And CSM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("POM_Status").ToString()) = False Then
                        Dim status As String
                        status = ds.Tables(0).Rows(i)("POM_Status").ToString()
                        If status = "A" Then
                            dr("Status") = "Activated"
                        ElseIf status = "D" Then
                            dr("Status") = "De-Activated"
                        ElseIf status = "W" Then
                            dr("Status") = "Waiting for Approval"
                        End If
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAttachFiles(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTrNo As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select pge_Orignalfilename,pge_ext,pge_createdon from EDT_Page Where PGE_FOLDER In (Select FOL_FOLID From EDT_FOLDER Where FOL_Name='" & sTrNo & "') And pge_CompID=" & iCompID & " "
            BindAttachFiles = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindAttachFiles
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckHistoryID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer, ByVal Sdate As String, ByVal sRate As String)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtNew As New DataTable
        Dim dRow As DataRow
        Try
            dtNew.Columns.Add("InvH_ID")
            dtNew.Columns.Add("INVH_PreDeterminedPrice")
            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iDescriptionID & " And INVH_PreDeterminedPrice='" & sRate & "' And InvH_CompID =" & iCompID & " And INVH_PurchaseEffeFrom <= Convert(datetime,'" & objClsFasgnrl.FormatDtForRDBMS(Sdate, "CT") & "') "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtNew.NewRow
                    dRow("InvH_ID") = dt.Rows(i)("InvH_ID")
                    dRow("INVH_PreDeterminedPrice") = dt.Rows(i)("INVH_PreDeterminedPrice") & " - " & objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("InvH_EffeFrom"), "D")
                    dtNew.Rows.Add(dRow)
                Next
            Else
                dRow = dtNew.NewRow
                dRow("InvH_ID") = 0
                dRow("INVH_PreDeterminedPrice") = ""
                dtNew.Rows.Add(dRow)
            End If

            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBaseID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCabinet As Integer, ByVal iSubCabinet As Integer, ByVal iFolder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From EDT_Page Where PGE_CABINET=" & iCabinet & " And PGE_SUBCABINET=" & iSubCabinet & " And PGE_Folder=" & iFolder & " And PGE_CompID=" & iCompID & " "
            GetBaseID = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetBaseID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getOrgParent(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iNodeID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Parent From Sad_Org_Structure Where Org_Node=" & iNodeID & " And Org_CompID=" & iCompID & " "
            getOrgParent = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return getOrgParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDefaultBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Node From Sad_Org_Structure Where Org_LevelCode=4 And Org_Default=1 And Org_CompID=" & iCompID & " "
            GetDefaultBranch = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDefaultBranch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFERates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer, ByVal dTotal As Double) As String
        Dim sSql As String = "", sToday As String = ""
        Dim dValue As Double = 0, dFETot As Double
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_TTBuy from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            dValue = objDB.SQLExecuteScalar(sNameSpace, sSql)
            dFETot = dValue * dTotal
            Return Math.Round(dFETot, 2, MidpointRounding.AwayFromZero)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFEID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As Integer
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_PKID from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDB.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBaseCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select sad_Config_Value from sad_config_settings Where sad_Config_Key='Currency' And SAD_CompID=" & iCompID & ""
            Return objDB.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCurrency(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iTRID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SElect POD_Currency From Purchase_Order_Details Where POD_MasterID=" & iTRID & " And POD_CompID =" & iCompID & ""
            Return objDB.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFECRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As String
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_TTBuy from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFECTime(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCurrency As Integer) As String
        Dim sSql As String = "", sToday As String = ""
        Try
            sToday = objGnrl.GetCurrentDate(sNameSpace)
            sSql = "Select CM_Time from MST_Currency_Masters where CM_OperateOn=" & iCurrency & " And CM_Date='" & sToday & "'  And CM_CompID=" & iCompID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
