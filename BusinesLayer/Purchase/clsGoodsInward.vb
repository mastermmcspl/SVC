Imports System
Imports System.Data
Imports DatabaseLayer
Imports BusinesLayer
Public Class ClsGoodsInward
    Private iTID_ID As Integer
    Private iTID_MasterID As Integer
    Private iTID_PORefID As Integer
    Private iTID_GINRefID As Integer
    Private iTID_ItemID As Integer
    Private iTID_Quantity As Integer
    Private sTID_PurchaseRate As String
    Private iTID_CompID As Integer
    Private sTID_Per As String
    Private sTID_TransactionType As String
    Private dTID_ItemRequiredDate As DateTime
    Private iTID_CommodityID As Integer
    Private sTID_Remarks As String
    Private dTID_ExciseDuty As Decimal
    Private dTID_Total As Decimal
    Private dTID_POVAT As Decimal
    Private dTID_CST As Decimal
    Private dTID_TaxAmount As Decimal
    Private sTID_ItemRowTotal As String
    Private sTID_Frieght As String
    Private sTID_Reason As String
    Private iTID_PredeterminedPrice As String
    Private iTID_YearID As Integer
    Private iTID_HistoryID As Integer
    Private iTID_DiffFlag As String
    Private iTID_ExcessFlag As String
    Private iTID_NewFlag As String
    Private iTRD_DiffFlag As String
    Private iTRD_ExcessFlag As String
    Private iTRD_NewFlag As String
    Private iTID_Flag As String
    Private iTID_Status As String
    Private iTRD_Flag As String
    Private iTRD_Status As String
    Private iTRD_PINo As String
    Private iTID_PINo As String
    Private iTRD_ID As Integer
    Private iTRD_MasterID As Integer
    Private iTRD_PORefID As Integer
    Private iTRD_GINRefID As Integer
    Private iTRD_ItemID As Integer
    Private iTRD_Quantity As Integer
    Private sTRD_PurchaseRate As String
    Private iTRD_CompID As Integer
    Private sTRD_Per As String
    Private sTRD_TransactionType As String
    Private dTRD_ItemRequiredDate As DateTime
    Private iTRD_CommodityID As Integer
    Private sTRD_Remarks As String
    Private dTRD_ExciseDuty As Decimal
    Private dTRD_Total As Decimal
    Private dTRD_POVAT As Decimal
    Private dTRD_CST As Decimal
    Private dTRD_TaxAmount As Decimal
    Private sTRD_ItemRowTotal As String
    Private sTRD_Frieght As String
    Private sTRD_Reason As String
    Private iTRD_PredeterminedPrice As String
    Private iTRD_YearID As Integer
    Private iTRD_HistoryID As Integer

    Private iPGD_HistoryID As Integer
    Private iPGD_OrderID As Integer
    Private iPGD_CommodityID As Integer
    Private iPGD_UnitID As Integer
    Private dPGD_MRP As Double
    Private dPGD_OrderQnt As Decimal
    Private dPGD_ReceivedQnt As Decimal
    Private dPGD_RejectedQnt As Decimal
    Private dPGD_Accepted As Decimal
    Private dPGD_Excess As Decimal
    Private dPGD_ManufactureDate As DateTime
    Private dPGD_ExpireDate As DateTime
    Private sPGD_Status As String
    Private iPGD_PGD As Integer
    Private sPGD_Operation As String
    Private sPGD_IPAddress As String


    Private dPGM_OrderDate As DateTime
    Private sPGM_PO_ID As String
    Private iPGM_CreatedBy As Integer

    Private sPGM_DocRefNo As String
    Private dPGM_SupplierReceiptDate As DateTime
    Private sPGM_Carrier As String
    Private sPGM_SupplierRefNo As String
    Private sPGM_FreightPrePaid As String
    Private sPGM_FreightPayable As String
    Private sGIND_HistoryID As String
    Private sPGM_Gin_Number As String
    Private sPGM_ModeOfShiping As String
    Private sPGM_Supplier As String
    Private sPGM_Operation As String
    Private sPGM_IPAddress As String
    Private dPGM_InvoiceDate As DateTime
    Private sPGM_ESugamNo As String

    Private iPGM_BatchNo As Integer
    Private iPGM_BaseName As Integer
    Private sPGM_OrderNo As String

    Private iPGD_CompID As Integer
    Private iPGM_CompID As Integer
    Private iPGM_OrderID As Integer
    Private iPGD_PendingItem As Integer
    Private iPGM_CrBy As Integer
    Private dPGM_CrOn As DateTime
    Private cPGM_DelFlag As Char
    Private sPGM_Status As String
    Private iPGM_ID As Integer
    Private iPGM_YearID As Integer
    Private iPGD_MasterID As Integer
    Private iGIND_CommodityID As Integer
    Private iPGD_DescriptionID As Integer
    Private sGIND_Code As String
    Private sGIND_Description As String
    Private sGIND_StdUnitofMessure As String
    Private sGIND_StdAltUnit As String
    Private sGIND_ReceivedUnitofMeassure As String
    Private dGIND_OrderdQty As Decimal
    Private dGIND_ReceivedQty As Decimal
    Private dGIND_AcceptedQty As Decimal
    Private dGIND_RejectedQty As Decimal
    Private iGIND_HistoryID As Integer
    Private dGIND_RejectedQtyExcess As Decimal
    Private sGIND_Remarks As String
    Private dGIND_ManufactureDate As DateTime
    Private dGIND_ExpireDate As DateTime
    Private sGIND_BatchNo As String
    Private sGIND_Rate As String
    Private sGIND_MRP As String
    Private iGIND_ID As Integer
    Private iGIND_CrBy As Integer
    Private dGIND_CrOn As DateTime
    Private iGIND_CompID As Integer
    Private cGIND_DelFlag As Char
    Private sGIND_Status As String
    Private iGIND_YearID As Integer
    Private sPGD_Delflag As String
    Private sGIND_DCNO As String

    Private POD_Discount As String
    Private POD_GSTRate As String
    Private POD_GSTId As String
    Private DiscountAmount As String
    Private Charges As String
    Private Amount As String
    Private GSTAmount As String
    Private TotalAmount As String
    Private RateAmount As String

    Private iPGD_ID As Integer
    Private iPGD_YearID As Integer



    Public Property PGD_YearID() As Integer
        Get
            Return (iPGD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iPGD_YearID = Value
        End Set
    End Property
    Public Property PGD_ID() As Integer
        Get
            Return (iPGD_ID)
        End Get
        Set(ByVal Value As Integer)
            iPGD_ID = Value
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
    Public Property sPOD_GSTRate() As String
        Get
            Return (POD_GSTRate)
        End Get
        Set(ByVal Value As String)
            POD_GSTRate = Value
        End Set
    End Property
    Public Property sPOD_GSTId() As String
        Get
            Return (POD_GSTId)
        End Get
        Set(ByVal Value As String)
            POD_GSTId = Value
        End Set
    End Property
    Public Property iDiscountAmount() As String
        Get
            Return (DiscountAmount)
        End Get
        Set(ByVal Value As String)
            DiscountAmount = Value
        End Set
    End Property
    Public Property iCharges() As String
        Get
            Return (Charges)
        End Get
        Set(ByVal Value As String)
            Charges = Value
        End Set
    End Property
    Public Property iAmount() As String
        Get
            Return (Amount)
        End Get
        Set(ByVal Value As String)
            Amount = Value
        End Set
    End Property
    Public Property iGSTAmount() As String
        Get
            Return (GSTAmount)
        End Get
        Set(ByVal Value As String)
            GSTAmount = Value
        End Set
    End Property
    Public Property iTotalAmount() As String
        Get
            Return (TotalAmount)
        End Get
        Set(ByVal Value As String)
            TotalAmount = Value
        End Set
    End Property
    Public Property iRateAmount() As String
        Get
            Return (RateAmount)
        End Get
        Set(ByVal Value As String)
            RateAmount = Value
        End Set
    End Property


    Public Property PGD_CommodityID() As Integer
        Get
            Return (iPGD_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iPGD_CommodityID = Value
        End Set
    End Property
    Public Property PGD_OrderID() As Integer
        Get
            Return (iPGD_OrderID)
        End Get
        Set(ByVal Value As Integer)
            iPGD_OrderID = Value
        End Set
    End Property
    Public Property PGD_HistoryID() As Integer
        Get
            Return (iPGD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iPGD_HistoryID = Value
        End Set
    End Property
    Public Property PGD_UnitID() As Integer
        Get
            Return (iPGD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            iPGD_UnitID = Value
        End Set
    End Property

    Public Property PGD_OrderQnt() As Decimal
        Get
            Return (dPGD_OrderQnt)
        End Get
        Set(ByVal Value As Decimal)
            dPGD_OrderQnt = Value
        End Set
    End Property

    Public Property PGD_ReceivedQnt() As Decimal
        Get
            Return (dPGD_ReceivedQnt)
        End Get
        Set(ByVal Value As Decimal)
            dPGD_ReceivedQnt = Value
        End Set
    End Property

    Public Property PGD_RejectedQnt() As Decimal
        Get
            Return (dPGD_RejectedQnt)
        End Get
        Set(ByVal Value As Decimal)
            dPGD_RejectedQnt = Value
        End Set
    End Property

    Public Property PGD_Accepted() As Decimal
        Get
            Return (dPGD_Accepted)
        End Get
        Set(ByVal Value As Decimal)
            dPGD_Accepted = Value
        End Set
    End Property
    Public Property PGD_Excess() As Decimal
        Get
            Return (dPGD_Excess)
        End Get
        Set(ByVal Value As Decimal)
            dPGD_Excess = Value
        End Set
    End Property

    Public Property PGD_ManufactureDate() As DateTime
        Get
            Return (dPGD_ManufactureDate)
        End Get
        Set(ByVal Value As DateTime)
            dPGD_ManufactureDate = Value
        End Set
    End Property
    Public Property PGD_ExpireDate() As DateTime
        Get
            Return (dPGD_ExpireDate)
        End Get
        Set(ByVal Value As DateTime)
            dPGD_ExpireDate = Value
        End Set
    End Property

    Public Property PGD_CompID() As String
        Get
            Return (iPGD_CompID)
        End Get
        Set(ByVal Value As String)
            iPGD_CompID = Value
        End Set
    End Property

    Public Property PGD_PendingItem() As Integer
        Get
            Return (iPGD_PendingItem)
        End Get
        Set(ByVal Value As Integer)
            iPGD_PendingItem = Value
        End Set
    End Property
    Public Property PGD_Status() As String
        Get
            Return (sPGD_Status)
        End Get
        Set(ByVal Value As String)
            sPGD_Status = Value
        End Set
    End Property

    Public Property PGD_Operation() As String
        Get
            Return (sPGD_Operation)
        End Get
        Set(ByVal Value As String)
            sPGD_Operation = Value
        End Set
    End Property
    Public Property PGD_IPAddress() As String
        Get
            Return (sPGD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sPGD_IPAddress = Value
        End Set
    End Property

    Public Property PGD_Delflag() As String
        Get
            Return (sPGD_Delflag)
        End Get
        Set(ByVal Value As String)
            sPGD_Delflag = Value
        End Set
    End Property
    Public Property PGD_PGD() As Integer
        Get
            Return (iPGD_PGD)
        End Get
        Set(ByVal Value As Integer)
            iPGD_PGD = Value
        End Set
    End Property
    Public Property PGD_MRP() As Double
        Get
            Return (dPGD_MRP)
        End Get
        Set(ByVal Value As Double)
            dPGD_MRP = Value
        End Set
    End Property
    Public Property TID_YearID() As Integer
        Get
            Return (iTID_YearID)
        End Get
        Set(ByVal Value As Integer)
            iTID_YearID = Value
        End Set
    End Property
    Public Property TID_ExciseDuty() As Decimal
        Get
            Return (dTID_ExciseDuty)
        End Get
        Set(ByVal Value As Decimal)
            dTID_ExciseDuty = Value
        End Set
    End Property

    Public Property TID_Total() As Decimal
        Get
            Return (dTID_Total)
        End Get
        Set(ByVal Value As Decimal)
            dTID_Total = Value
        End Set
    End Property
    Public Property TID_POVAT() As Decimal
        Get
            Return (dTID_POVAT)
        End Get
        Set(ByVal Value As Decimal)
            dTID_POVAT = Value
        End Set
    End Property
    Public Property TID_CST() As Decimal
        Get
            Return (dTID_CST)
        End Get
        Set(ByVal Value As Decimal)
            dTID_CST = Value
        End Set
    End Property
    Public Property TID_TaxAmount() As Decimal
        Get
            Return (dTID_TaxAmount)
        End Get
        Set(ByVal Value As Decimal)
            dTID_TaxAmount = Value
        End Set
    End Property
    Public Property TID_ItemRowTotal() As String
        Get
            Return (sTID_ItemRowTotal)
        End Get
        Set(ByVal Value As String)
            sTID_ItemRowTotal = Value
        End Set
    End Property
    Public Property PGM_ESugamNo() As String
        Get
            Return (sPGM_ESugamNo)
        End Get
        Set(ByVal Value As String)
            sPGM_ESugamNo = Value
        End Set
    End Property
    Public Property TID_PredeterminedPrice() As String
        Get
            Return (iTID_PredeterminedPrice)
        End Get
        Set(ByVal Value As String)
            iTID_PredeterminedPrice = Value
        End Set
    End Property

    Public Property TID_PINo() As String
        Get
            Return (iTID_PINo)
        End Get
        Set(ByVal Value As String)
            iTID_PINo = Value
        End Set
    End Property

    Public Property TRD_PINo() As String
        Get
            Return (iTRD_PINo)
        End Get
        Set(ByVal Value As String)
            iTRD_PINo = Value
        End Set
    End Property

    Public Property TID_HistoryID() As Integer
        Get
            Return (iTID_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iTID_HistoryID = Value
        End Set
    End Property

    Public Property TID_Per() As String
        Get
            Return (sTID_Per)
        End Get
        Set(ByVal Value As String)
            sTID_Per = Value
        End Set
    End Property
    Public Property TID_ItemRequiredDate() As DateTime
        Get
            Return (dTID_ItemRequiredDate)
        End Get
        Set(ByVal Value As DateTime)
            dTID_ItemRequiredDate = Value
        End Set
    End Property

    Public Property PGM_InvoiceDate() As DateTime
        Get
            Return (dPGM_InvoiceDate)
        End Get
        Set(ByVal Value As DateTime)
            dPGM_InvoiceDate = Value
        End Set
    End Property

    Public Property TID_Frieght() As String
        Get
            Return (sTID_Frieght)
        End Get
        Set(ByVal Value As String)
            sTID_Frieght = Value
        End Set
    End Property
    Public Property TID_Remarks() As String
        Get
            Return (sTID_Remarks)
        End Get
        Set(ByVal Value As String)
            sTID_Remarks = Value
        End Set
    End Property
    Public Property TID_CompID() As Integer
        Get
            Return (iTID_CompID)
        End Get
        Set(ByVal Value As Integer)
            iTID_CompID = Value
        End Set
    End Property
    Public Property TID_PurchaseRate() As String
        Get
            Return (sTID_PurchaseRate)
        End Get
        Set(ByVal Value As String)
            sTID_PurchaseRate = Value
        End Set
    End Property
    Public Property TID_Quantity() As Integer
        Get
            Return (iTID_Quantity)
        End Get
        Set(ByVal Value As Integer)
            iTID_Quantity = Value
        End Set
    End Property
    Public Property TID_ItemID() As Integer
        Get
            Return (iTID_ItemID)
        End Get
        Set(ByVal Value As Integer)
            iTID_ItemID = Value
        End Set
    End Property

    Public Property TID_CommodityID() As Integer
        Get
            Return (iTID_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iTID_CommodityID = Value
        End Set
    End Property
    Public Property TID_MasterID() As Integer
        Get
            Return (iTID_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iTID_MasterID = Value
        End Set
    End Property

    Public Property TID_PORefID() As Integer
        Get
            Return (iTID_PORefID)
        End Get
        Set(ByVal Value As Integer)
            iTID_PORefID = Value
        End Set
    End Property
    Public Property TID_GINRefID() As Integer
        Get
            Return (iTID_GINRefID)
        End Get
        Set(ByVal Value As Integer)
            iTID_GINRefID = Value
        End Set
    End Property

    Public Property TID_ID() As Integer
        Get
            Return (iTID_ID)
        End Get
        Set(ByVal Value As Integer)
            iTID_ID = Value
        End Set
    End Property

    Public Property TID_Flag() As String
        Get
            Return (iTID_Flag)
        End Get
        Set(ByVal Value As String)
            iTID_Flag = Value
        End Set
    End Property

    Public Property TID_NewFlag() As String
        Get
            Return (iTID_NewFlag)
        End Get
        Set(ByVal Value As String)
            iTID_NewFlag = Value
        End Set
    End Property

    Public Property TID_ExcessFlag() As String
        Get
            Return (iTID_ExcessFlag)
        End Get
        Set(ByVal Value As String)
            iTID_ExcessFlag = Value
        End Set
    End Property

    Public Property TID_DiffFlag() As String
        Get
            Return (iTID_DiffFlag)
        End Get
        Set(ByVal Value As String)
            iTID_DiffFlag = Value
        End Set
    End Property

    Public Property TID_Status() As String
        Get
            Return (iTID_Status)
        End Get
        Set(ByVal Value As String)
            iTID_Status = Value
        End Set
    End Property
    Public Property TRD_Flag() As String
        Get
            Return (iTRD_Flag)
        End Get
        Set(ByVal Value As String)
            iTRD_Flag = Value
        End Set
    End Property

    Public Property TRD_NewFlag() As String
        Get
            Return (iTRD_NewFlag)
        End Get
        Set(ByVal Value As String)
            iTRD_NewFlag = Value
        End Set
    End Property

    Public Property TRD_ExcessFlag() As String
        Get
            Return (iTRD_ExcessFlag)
        End Get
        Set(ByVal Value As String)
            iTRD_ExcessFlag = Value
        End Set
    End Property

    Public Property TRD_Status() As String
        Get
            Return (iTRD_Status)
        End Get
        Set(ByVal Value As String)
            iTRD_Status = Value
        End Set
    End Property
    Public Property TRD_YearID() As Integer
        Get
            Return (iTRD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_YearID = Value
        End Set
    End Property
    Public Property TRD_ExciseDuty() As Decimal
        Get
            Return (dTRD_ExciseDuty)
        End Get
        Set(ByVal Value As Decimal)
            dTRD_ExciseDuty = Value
        End Set
    End Property
    Public Property TRD_Total() As Decimal
        Get
            Return (dTRD_Total)
        End Get
        Set(ByVal Value As Decimal)
            dTRD_Total = Value
        End Set
    End Property
    Public Property TRD_POVAT() As Decimal
        Get
            Return (dTRD_POVAT)
        End Get
        Set(ByVal Value As Decimal)
            dTRD_POVAT = Value
        End Set
    End Property
    Public Property TRD_CST() As Decimal
        Get
            Return (dTRD_CST)
        End Get
        Set(ByVal Value As Decimal)
            dTRD_CST = Value
        End Set
    End Property
    Public Property TRD_TaxAmount() As Decimal
        Get
            Return (dTRD_TaxAmount)
        End Get
        Set(ByVal Value As Decimal)
            dTRD_TaxAmount = Value
        End Set
    End Property
    Public Property TRD_ItemRowTotal() As String
        Get
            Return (sTRD_ItemRowTotal)
        End Get
        Set(ByVal Value As String)
            sTRD_ItemRowTotal = Value
        End Set
    End Property
    Public Property TRD_PredeterminedPrice() As String
        Get
            Return (iTRD_PredeterminedPrice)
        End Get
        Set(ByVal Value As String)
            iTRD_PredeterminedPrice = Value
        End Set
    End Property
    Public Property TRD_Per() As String
        Get
            Return (sTRD_Per)
        End Get
        Set(ByVal Value As String)
            sTRD_Per = Value
        End Set
    End Property
    Public Property TRD_ItemRequiredDate() As DateTime
        Get
            Return (dTRD_ItemRequiredDate)
        End Get
        Set(ByVal Value As DateTime)
            dTRD_ItemRequiredDate = Value
        End Set
    End Property
    Public Property TRD_Frieght() As String
        Get
            Return (sTRD_Frieght)
        End Get
        Set(ByVal Value As String)
            sTRD_Frieght = Value
        End Set
    End Property
    Public Property TRD_Remarks() As String
        Get
            Return (sTRD_Remarks)
        End Get
        Set(ByVal Value As String)
            sTRD_Remarks = Value
        End Set
    End Property

    Public Property TRD_HistoryID() As Integer
        Get
            Return (iTRD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_HistoryID = Value
        End Set
    End Property

    Public Property TRD_CompID() As Integer
        Get
            Return (iTRD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_CompID = Value
        End Set
    End Property
    Public Property TRD_PurchaseRate() As String
        Get
            Return (sTRD_PurchaseRate)
        End Get
        Set(ByVal Value As String)
            sTRD_PurchaseRate = Value
        End Set
    End Property

    Public Property TRD_Quantity() As Integer
        Get
            Return (iTRD_Quantity)
        End Get
        Set(ByVal Value As Integer)
            iTRD_Quantity = Value
        End Set
    End Property
    Public Property TRD_ItemID() As Integer
        Get
            Return (iTRD_ItemID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_ItemID = Value
        End Set
    End Property

    Public Property TRD_CommodityID() As Integer
        Get
            Return (iTRD_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_CommodityID = Value
        End Set
    End Property
    Public Property TRD_MasterID() As Integer
        Get
            Return (iTRD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_MasterID = Value
        End Set
    End Property
    Public Property TRD_PORefID() As Integer
        Get
            Return (iTRD_PORefID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_PORefID = Value
        End Set
    End Property
    Public Property TRD_GINRefID() As Integer
        Get
            Return (iTRD_GINRefID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_GINRefID = Value
        End Set
    End Property
    Public Property TRD_ID() As Integer
        Get
            Return (iTRD_ID)
        End Get
        Set(ByVal Value As Integer)
            iTRD_ID = Value
        End Set
    End Property

    Public Property PGM_ID() As Integer
        Get
            Return (iPGM_ID)
        End Get
        Set(ByVal Value As Integer)
            iPGM_ID = Value
        End Set
    End Property
    Public Property PGM_CompID() As Integer
        Get
            Return (iPGM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iPGM_CompID = Value
        End Set
    End Property
    Public Property PGM_OrderID() As Integer
        Get
            Return (iPGM_OrderID)
        End Get
        Set(ByVal Value As Integer)
            iPGM_OrderID = Value
        End Set
    End Property
    Public Property PGM_BatchNo() As Integer
        Get
            Return (iPGM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iPGM_BatchNo = Value
        End Set
    End Property
    Public Property PGM_BaseName() As Integer
        Get
            Return (iPGM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iPGM_BaseName = Value
        End Set
    End Property
    Public Property PGM_OrderNo() As String
        Get
            Return (sPGM_OrderNo)
        End Get
        Set(ByVal Value As String)
            sPGM_OrderNo = Value
        End Set
    End Property
    Public Property PGM_CrBy() As Integer
        Get
            Return (iPGM_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iPGM_CrBy = Value
        End Set
    End Property
    Public Property PGM_CrOn() As DateTime
        Get
            Return (dPGM_CrOn)
        End Get
        Set(ByVal Value As DateTime)
            dPGM_CrOn = Value
        End Set
    End Property
    Public Property PGM_DelFlag() As Char
        Get
            Return (cPGM_DelFlag)
        End Get
        Set(ByVal Value As Char)
            cPGM_DelFlag = Value
        End Set
    End Property
    Public Property PGM_Status() As String
        Get
            Return (sPGM_Status)
        End Get
        Set(ByVal Value As String)
            sPGM_Status = Value
        End Set
    End Property

    Public Property PGM_Operation() As String
        Get
            Return (sPGM_Operation)
        End Get
        Set(ByVal Value As String)
            sPGM_Operation = Value
        End Set
    End Property

    Public Property PGM_IPAddress() As String
        Get
            Return (sPGM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sPGM_IPAddress = Value
        End Set
    End Property


    Public Property PGM_Supplier() As String
        Get
            Return (sPGM_Supplier)
        End Get
        Set(ByVal Value As String)
            sPGM_Supplier = Value
        End Set
    End Property
    Public Property PGM_OrderDate() As DateTime
        Get
            Return (dPGM_OrderDate)
        End Get
        Set(ByVal Value As DateTime)
            dPGM_OrderDate = Value
        End Set
    End Property
    Public Property PGM_PO_ID() As String
        Get
            Return (sPGM_PO_ID)
        End Get
        Set(ByVal Value As String)
            sPGM_PO_ID = Value
        End Set
    End Property
    Public Property PGM_CreatedBy() As Integer
        Get
            Return (iPGM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPGM_CreatedBy = Value
        End Set
    End Property

    Public Property PGM_YearID() As Integer
        Get
            Return (iPGM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iPGM_YearID = Value
        End Set
    End Property

    Public Property PGM_DocRefNo() As String
        Get
            Return (sPGM_DocRefNo)
        End Get
        Set(ByVal Value As String)
            sPGM_DocRefNo = Value
        End Set
    End Property
    Public Property PGM_SupplierReceiptDate() As DateTime
        Get
            Return (dPGM_SupplierReceiptDate)
        End Get
        Set(ByVal Value As DateTime)
            dPGM_SupplierReceiptDate = Value
        End Set
    End Property
    Public Property PGM_Carrier() As String
        Get
            Return (sPGM_Carrier)
        End Get
        Set(ByVal Value As String)
            sPGM_Carrier = Value
        End Set
    End Property
    Public Property PGM_SupplierRefNo() As String
        Get
            Return (sPGM_SupplierRefNo)
        End Get
        Set(ByVal Value As String)
            sPGM_SupplierRefNo = Value
        End Set
    End Property
    Public Property PGM_FreightPrePaid() As String
        Get
            Return (sPGM_FreightPrePaid)
        End Get
        Set(ByVal Value As String)
            sPGM_FreightPrePaid = Value
        End Set
    End Property
    Public Property PGM_FreightPayable() As String
        Get
            Return (sPGM_FreightPayable)
        End Get
        Set(ByVal Value As String)
            sPGM_FreightPayable = Value
        End Set
    End Property
    Public Property PGM_ModeOfShiping() As String
        Get
            Return (sPGM_ModeOfShiping)
        End Get
        Set(ByVal Value As String)
            sPGM_ModeOfShiping = Value
        End Set
    End Property

    Public Property PGM_Gin_Number() As String
        Get
            Return (sPGM_Gin_Number)
        End Get
        Set(ByVal Value As String)
            sPGM_Gin_Number = Value
        End Set
    End Property

    Public Property PGD_MasterID() As String
        Get
            Return (iPGD_MasterID)
        End Get
        Set(ByVal Value As String)
            iPGD_MasterID = Value
        End Set
    End Property
    Public Property GIND_CommodityID() As Integer
        Get
            Return (iGIND_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iGIND_CommodityID = Value
        End Set
    End Property
    Public Property PGD_DescriptionID() As Integer
        Get
            Return (iPGD_DescriptionID)
        End Get
        Set(ByVal Value As Integer)
            iPGD_DescriptionID = Value
        End Set
    End Property
    Public Property GIND_YearID() As Integer
        Get
            Return (iGIND_YearID)
        End Get
        Set(ByVal Value As Integer)
            iGIND_YearID = Value
        End Set
    End Property

    Public Property GIND_StdUnitofMeassurement() As String
        Get
            Return (sGIND_StdUnitofMessure)
        End Get
        Set(ByVal Value As String)
            sGIND_StdUnitofMessure = Value
        End Set
    End Property
    Public Property GIND_MRP() As String
        Get
            Return (sGIND_MRP)
        End Get
        Set(ByVal Value As String)
            sGIND_MRP = Value
        End Set
    End Property
    Public Property GIND_StdAlternateUnit() As String
        Get
            Return (sGIND_StdAltUnit)
        End Get
        Set(ByVal Value As String)
            sGIND_StdAltUnit = Value
        End Set
    End Property
    Public Property GIND_ReceivedUnitofMeassurement() As String
        Get
            Return (sGIND_ReceivedUnitofMeassure)
        End Get
        Set(ByVal Value As String)
            sGIND_ReceivedUnitofMeassure = Value
        End Set
    End Property
    Public Property GIND_OrderedQuantity() As Decimal
        Get
            Return (dGIND_OrderdQty)
        End Get
        Set(ByVal Value As Decimal)
            dGIND_OrderdQty = Value
        End Set
    End Property
    Public Property GIND_Received() As Decimal
        Get
            Return (dGIND_ReceivedQty)
        End Get
        Set(ByVal Value As Decimal)
            dGIND_ReceivedQty = Value
        End Set
    End Property
    Public Property GIND_Accepted() As Decimal
        Get
            Return (dGIND_AcceptedQty)
        End Get
        Set(ByVal Value As Decimal)
            dGIND_AcceptedQty = Value
        End Set
    End Property
    Public Property GIND_Rejected() As Decimal
        Get
            Return (dGIND_RejectedQty)
        End Get
        Set(ByVal Value As Decimal)
            dGIND_RejectedQty = Value
        End Set
    End Property
    Public Property GIND_Remarks() As String
        Get
            Return (sGIND_Remarks)
        End Get
        Set(ByVal Value As String)
            sGIND_Remarks = Value
        End Set
    End Property
    Public Property GIND_RejectedBczExcess() As Decimal
        Get
            Return (dGIND_RejectedQtyExcess)
        End Get
        Set(ByVal Value As Decimal)
            dGIND_RejectedQtyExcess = Value
        End Set
    End Property
    Public Property GIND_ManufactureDate() As DateTime
        Get
            Return (dGIND_ManufactureDate)
        End Get
        Set(ByVal Value As DateTime)
            dGIND_ManufactureDate = Value
        End Set
    End Property
    Public Property GIND_ExpireDate() As DateTime
        Get
            Return (dGIND_ExpireDate)
        End Get
        Set(ByVal Value As DateTime)
            dGIND_ExpireDate = Value
        End Set
    End Property
    Public Property GIND_BatchNo() As String
        Get
            Return (sGIND_BatchNo)
        End Get
        Set(ByVal Value As String)
            sGIND_BatchNo = Value
        End Set
    End Property
    Public Property GIND_Rate() As String
        Get
            Return (sGIND_Rate)
        End Get
        Set(ByVal Value As String)
            sGIND_Rate = Value
        End Set
    End Property
    Public Property GIND_CompID() As Integer
        Get
            Return (iGIND_CompID)
        End Get
        Set(ByVal Value As Integer)
            iGIND_CompID = Value
        End Set
    End Property
    Public Property GIND_CrBy() As Integer
        Get
            Return (iGIND_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iGIND_CrBy = Value
        End Set
    End Property
    Public Property GIND_CrOn() As DateTime
        Get
            Return (dGIND_CrOn)
        End Get
        Set(ByVal Value As DateTime)
            dGIND_CrOn = Value
        End Set
    End Property
    Public Property GIND_DelFlag() As Char
        Get
            Return (cGIND_DelFlag)
        End Get
        Set(ByVal Value As Char)
            cGIND_DelFlag = Value
        End Set
    End Property
    Public Property GIND_Status() As String
        Get
            Return (sGIND_Status)
        End Get
        Set(ByVal Value As String)
            sGIND_Status = Value
        End Set
    End Property
    Public Property GIND_HistoryID() As Integer
        Get
            Return (iGIND_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iGIND_HistoryID = Value
        End Set
    End Property

    Public Property GIND_ID() As Integer
        Get
            Return (iGIND_ID)
        End Get
        Set(ByVal Value As Integer)
            iGIND_ID = Value
        End Set
    End Property

    Public Property GIND_DCNO() As String
        Get
            Return (sGIND_DCNO)
        End Get
        Set(ByVal Value As String)
            sGIND_DCNO = Value
        End Set
    End Property

    Dim objDB As New DBHelper
    Dim objClsFasgnrl As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions
    Dim objGnrlFnction As New clsGeneralFunctions
    Public Function LoadOurRefNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_BranchID=" & iBranch & " And POM_Status='A' and POM_OralStatus='P' And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " Order By POM_ID desc "
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExistingInward(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal docRef As String, ByVal OrderNo As String) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select PGM_GIN_Number From Purchase_gin_master Where PGM_DocumentRefNo ='" & docRef & "' And PGM_CompID=" & iCompID & " and PGM_YearID =" & iYearID & " and PGM_OrderID =" & OrderNo & ""

            sStatus = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function OrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt, dtTab As New DataTable
        Dim txtorderqty As String = "", sSql As String = ""
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("POM_Supplier")
            dtTab.Columns.Add("POM_OrderDate")
            dtTab.Columns.Add("POM_ModeOfShipping")
            dtTab.Columns.Add("POM_PaymentTerms")
            dtTab.Columns.Add("POM_MPayment")
            dtTab.Columns.Add("POM_DSchdule")
            sSql = "" : sSql = "Select * From Purchase_Order_Master Where POM_ID=" & OrderNo & " And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("POM_OrderDate")) = False Then
                    dr("POM_OrderDate") = dt.Rows(i)("POM_OrderDate")
                Else
                    dr("POM_OrderDate") = ""
                End If
                dr("POM_PaymentTerms") = dt.Rows(i)("POM_PaymentTerms")
                dr("POM_MPayment") = dt.Rows(i)("POM_MPayment")
                dr("POM_DSchdule") = dt.Rows(i)("POM_DSchdule")
                dr("POM_Supplier") = dt.Rows(i)("POM_Supplier")              ' DBHelper.GetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID =" & dt.Rows(i)("POM_Supplier") & " and CSM_CompID =" & iCompID & "")
                dr("POM_ModeOfShipping") = dt.Rows(i)("POM_ModeOfShipping")  ' DBHelper.GetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("POM_ModeOfShipping") & " and Mas_CompID = " & iCompID & " and Mas_master=13")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExistingInwardDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal GinNo As Integer) As DataTable
        Dim dt, dtTab As New DataTable
        Dim txtorderqty As String = "", sSql As String = ""
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("PGM_DocumentRefNo")
            dtTab.Columns.Add("PGM_Gin_Number")
            dtTab.Columns.Add("PGM_OrderDate")
            dtTab.Columns.Add("PGM_ESugamNo")
            dtTab.Columns.Add("PGM_InvoiceDate")
            dtTab.Columns.Add("PGM_OrderID")
            dtTab.Columns.Add("PGM_DcNo")
            dtTab.Columns.Add("PGM_Supplier")
            dtTab.Columns.Add("PGM_Status")
            sSql = "" : sSql = "Select * From Purchase_GIN_master Where PGM_CompID=" & iCompID & " and PGM_YearID =" & iYearID & " and PGM_ID=" & GinNo & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("PGM_DocumentRefNo")) = False Then
                    dr("PGM_DocumentRefNo") = dt.Rows(i)("PGM_DocumentRefNo")
                Else
                    dr("PGM_DocumentRefNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_Gin_Number")) = False Then
                    dr("PGM_Gin_Number") = dt.Rows(i)("PGM_Gin_Number")
                Else
                    dr("PGM_Gin_Number") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_OrderDate")) = False Then
                    dr("PGM_OrderDate") = dt.Rows(i)("PGM_OrderDate")
                Else
                    dr("PGM_OrderDate") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_ESugamNo")) = False Then
                    dr("PGM_ESugamNo") = dt.Rows(i)("PGM_ESugamNo")
                Else
                    dr("PGM_ESugamNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_InvoiceDate")) = False Then
                    dr("PGM_InvoiceDate") = dt.Rows(i)("PGM_InvoiceDate")
                Else
                    dr("PGM_InvoiceDate") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_OrderID")) = False Then
                    dr("PGM_OrderID") = dt.Rows(i)("PGM_OrderID")
                Else
                    dr("PGM_OrderID") = 0
                End If

                If IsDBNull(dt.Rows(i)("PGM_DcNo")) = False Then
                    dr("PGM_DcNo") = dt.Rows(i)("PGM_DcNo")
                Else
                    dr("PGM_DcNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_Supplier")) = False Then
                    dr("PGM_Supplier") = dt.Rows(i)("PGM_Supplier")
                Else
                    dr("PGM_Supplier") = 0
                End If

                If IsDBNull(dt.Rows(i)("PGM_ESugamNo")) = False Then
                    dr("PGM_ESugamNo") = dt.Rows(i)("PGM_ESugamNo")
                Else
                    dr("PGM_ESugamNo") = 0
                End If

                If IsDBNull(dt.Rows(i)("PGM_Status")) = False Then
                    If (dt.Rows(i)("PGM_Status") = "W") Then
                        dr("PGM_Status") = "Waiting For Approval"
                    Else
                        dr("PGM_Status") = "Approved"
                    End If
                    'dr("PGM_Status") = dt.Rows(i)("PGM_Status")
                Else
                    dr("PGM_Status") = ""
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveMaster(ByVal sNameSpace As String, ByVal ObjGoods As ClsGoodsInward, ByVal iSaveOrUpdate As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(24) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_OrderDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = ObjGoods.PGM_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_GIN_Number", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = ObjGoods.PGM_Gin_Number
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_Supplier", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_Supplier
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_DocumentRefNo", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjGoods.PGM_DocRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_ModeOFshiping", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_InvoiceDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = ObjGoods.PGM_InvoiceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_ESugamNo", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjGoods.PGM_ESugamNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_ApprovedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_Status", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjGoods.PGM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_YearID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = ObjGoods.PGM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_Operation", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.PGM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.PGM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_DcNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.sGIND_DCNO
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGM_OrderNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ObjGoods.PGM_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Debug", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iSaveOrUpdate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spInwardMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateGINNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPOID As Integer) As String
        Dim sSql As String = ""
        Dim Count As String = ""
        Try
            sSql = "select IsNull(count(*),0)+1 from Purchase_GIN_Master where PGM_OrderID=" & iPOID & " And PGM_CompID=" & iCompID & " "
            Count = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return "-00" & Count
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateInvoiceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPOID As Integer, ByVal iGin As Integer) As String
        Dim sSql As String = ""
        Dim Count As String = ""
        Try
            sSql = "select IsNull(count(*),0)+1 from Transaction_Invoice_Details where TID_PORefID=" & iPOID & " And TID_CompID=" & iCompID & ""
            Count = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return "-00" & Count
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GenerateReturn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPOID As Integer, ByVal iGin As Integer) As String
        Dim sSql As String = ""
        Dim Count As String = ""
        Try
            sSql = "select IsNull(count(*),0)+1 from Transaction_Returns_Details where TRD_PORefID=" & iPOID & " And TRD_CompID=" & iCompID & " And TRD_GINRefID = " & iGin & ""
            Count = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return " - 00" & Count
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GenerateInwardCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Try
            sMaximumID = objDB.SQLGetDescription(sNameSpace, "Select Top 1 PGM_ID From Purchase_GIN_Master Order By PGM_ID Desc")
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
            sStr = "GIN" & "" & sYear & "" & "" & sMonthCode & "" & "" & sDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveMasterDetails(ByVal sNameSpace As String, ByVal ObjGoods As ClsGoodsInward) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.GIND_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_CommodityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_DescriptionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_DescriptionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_HistoryID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_UnitID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_MRP", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = ObjGoods.PGD_MRP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_OrderQnt", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = ObjGoods.PGD_OrderQnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_ReceivedQnt", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = ObjGoods.PGD_ReceivedQnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_RejectedQnt", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = ObjGoods.PGD_RejectedQnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_Accepted", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Accepted
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_Excess", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Excess
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_ManufactureDate ", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = ObjGoods.PGD_ManufactureDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_ExpireDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = ObjGoods.PGD_ExpireDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_PendingQnt", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_PendingItem
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = ObjGoods.PGD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PGD_BatchNumber ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = ObjGoods.GIND_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spInwardMasterDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public  Function GetOrderdQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iRefID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Dim iQty As Integer
        Try
            sSql = "Select TMD_Quantity From Transaction_Master_Details Where TMD_MasterID=" & iRefID & " And TMD_ItemID=" & iItemID & " And TMD_CompID=" & iCompID & " "
            iQty = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iQty
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetINVDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iItemID As Integer, ByVal iINVHID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Inventory_Master_History Where INVH_ID=" & iINVHID & " And INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & " and INVH_YearID =" & iYearId & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iExistInwardID As Integer) As Integer
        Dim sSql As String = ""
        Dim i As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_GIN_master where PGM_ID=" & iExistInwardID & " And PGM_CompID=" & iCompID & " And PGM_YearID =" & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & "Update Purchase_GIN_master Set PGM_Status='A',PGM_ApprovedBy=" & iUserID & ",PGM_ApprovedOn=GetDate() where pgm_id = " & dt.Rows(i)("pgm_id") & ""
                    sSql = sSql & " End"
                    objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
            sSql = "" : sSql = "Select * from Purchase_GIN_details  Where PGD_MasterID=" & iExistInwardID & " and PGD_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & "Update Purchase_GIN_details Set PGD_Status='A',PGM_ApprovedBy=" & iUserID & ",PGM_ApprovedOn=GetDate() where pgd_id = " & dt.Rows(i)("pgd_id") & ""
                    sSql = sSql & " End"
                    objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Purchase_GIN_Master Where PGM_ID=" & iMasterID & " And PGM_CompID=" & iCompID & " And PGM_YearID = " & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGridDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("SlNo", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("gl_ID", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Commodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Description", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Code", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("StdUnit", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MRP", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedUnit", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQuantity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQuantity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AcceptedQuantity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQuantity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQuantityExcess", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Remarks", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ManufactureDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExpireDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("BatchNo", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Rate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryID", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * From Purchase_GIN_Details Where PGD_MasterID=" & iMasterID & " And PGD_CompID=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("SlNo") = i + 1
                    dr("gl_ID") = dt.Rows(i)("PGD_ItemID")
                    dr("Commodity") = objDB.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PGD_CommodityID") & "' and INV_CompID = " & iCompID & "")
                    dr("Description") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & dt.Rows(i)("PGD_ItemID") & "and INV_CompID = " & iCompID & " ") & " - " & objDB.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("GIND_ItemID") & " and INV_CompID = " & iCompID & " ")
                    dr("Code") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & dt.Rows(i)("PGD_ItemID") & " and INV_CompID = " & iCompID & "")
                    dr("StdUnit") = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("PGD_StdUnitofMeassurement") & " and Mas_CompID =" & iCompID & "")
                    dr("MRP") = dt.Rows(i)("PGD_MRP")
                    dr("ReceivedUnit") = dt.Rows(i)("PGD_ReceivedUnitofMeassurement")
                    dr("OrderedQuantity") = dt.Rows(i)("PGD_OrderedQuantity")
                    If IsDBNull(dt.Rows(i)("PGD_Received")) = False Then
                        dr("ReceivedQuantity") = dt.Rows(i)("PGD_Received")
                    Else
                        dr("ReceivedQuantity") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("GIND_Accepted")) = False Then
                        If dt.Rows(i)("GIND_Accepted") > 0 Then
                            dr("AcceptedQuantity") = dt.Rows(i)("PGD_Accepted")
                        Else
                            dr("AcceptedQuantity") = ""
                        End If
                    Else
                        dr("AcceptedQuantity") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_Rejected")) = False Then
                        If dt.Rows(i)("GIND_Rejected") > 0 Then
                            dr("RejectedQuantity") = dt.Rows(i)("PGD_Rejected")
                        Else
                            dr("RejectedQuantity") = ""
                        End If
                    Else
                        dr("RejectedQuantity") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_RejectedBczExcess")) = False Then
                        If dt.Rows(i)("GIND_RejectedBczExcess") > 0 Then
                            dr("RejectedQuantityExcess") = dt.Rows(i)("PGD_RejectedBczExcess")
                        Else
                            dr("RejectedQuantityExcess") = ""
                        End If
                    Else
                        dr("RejectedQuantityExcess") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_Remarks")) = False Then
                        dr("Remarks") = dt.Rows(i)("PGD_Remarks")
                    Else
                        dr("Remarks") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_ManufactureDate")) = False Then
                        dr("ManufactureDate") = ""
                    Else
                        dr("ManufactureDate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PGD_ManufactureDate"), "D")
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_ExpireDate")) = False Then
                        dr("ExpireDate") = ""
                    Else
                        dr("ExpireDate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PGD_ExpireDate"), "D")
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_BatchNo")) = False Then
                        dr("BatchNo") = dt.Rows(i)("PGD_BatchNo")
                    Else
                        dr("BatchNo") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_Rate")) = False Then
                        dr("Rate") = dt.Rows(i)("PGD_Rate")
                    Else
                        dr("Rate") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_HistoryID")) = False Then
                        dr("HistoryID") = dt.Rows(i)("PGD_HistoryID")
                    Else
                        dr("HistoryID") = ""
                    End If
                    dr("PendingItem") = 1
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindUnits(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iUnit As Integer
        Dim iAlterUnit As Integer
        Try

            iUnit = objDB.SQLExecuteScalar(sNameSpace, "Select INVH_Unit From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " And INVH_compID=" & iCompID & "")
            iAlterUnit = objDB.SQLExecuteScalar(sNameSpace, "Select INVH_AlterUnit From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " And INVH_compID=" & iCompID & "")
            sSql = "Select Mas_ID,MAS_Desc From Acc_General_Master Where Mas_ID in(" & iUnit & "," & iAlterUnit & ")"
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GeneratePINo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As Integer) As String
        Dim sSql As String = ""
        Dim Count As String = ""
        Try
            sSql = "select IsNull(count(*),0)+1 from Transaction_Master where TM_TransactionType='PI' And TM_PoRefNo=" & iGINID & " "
            Count = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return "-00" & Count
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GeneratePRNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As Integer) As String
        Dim sSql As String = ""
        Dim Count As String = ""
        Try
            sSql = "select IsNull(count(*),0)+1 from Transaction_Master where TM_TransactionType='PR' And TM_PoRefNo=" & iGINID & " "
            Count = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            Return "-00" & Count
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAccaptedQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As Integer, ByVal iItemID As Integer) As String
        Dim sSql As String = ""
        Dim sCheck As String = ""
        Dim iCheck As Integer
        Try
            sSql = "Select GIND_Accepted From Goods_InwardNote_Master_Details Where GIND_ItemID=" & iItemID & " And GIND_MasterID=" & iGINID & " And GIND_CompID=" & iCompID & " "
            iCheck = objDB.SQLExecuteScalarInt(sNameSpace, sSql)
            If iCheck > 0 Then
                sCheck = "True"
            Else
                sCheck = "False"
            End If
            Return sCheck
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRejectedQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As Integer, ByVal iItemID As Integer) As String
        Dim sSql As String = ""
        Dim sCheck As String = ""
        Dim iRejectedQuality As Integer
        Dim iRejectedExcess As Integer
        Try
            iRejectedQuality = objDB.SQLExecuteScalarInt(sNameSpace, "Select GIND_Rejected From Goods_InwardNote_Master_Details Where GIND_ItemID=" & iItemID & " And GIND_MasterID=" & iGINID & " And GIND_CompID=" & iCompID & "")
            iRejectedExcess = objDB.SQLExecuteScalarInt(sNameSpace, "Select GIND_RejectedBczExcess From Goods_InwardNote_Master_Details Where GIND_ItemID=" & iItemID & " And GIND_MasterID=" & iGINID & " And GIND_CompID=" & iCompID & "")
            If iRejectedQuality > 0 Then
                sCheck = "True"
            ElseIf iRejectedExcess > 0 Then
                sCheck = "True"
            Else
                sCheck = "False"
            End If
            Return sCheck
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As Integer) As String
        Dim sSql As String = ""
        Dim sStatus As String = ""
        Try
            sSql = "select GIN_Status From Goods_InwardNote_Master Where GIN_ID=" & iGINID & " And GIN_CompID=" & iCompID & " "
            sStatus = objDB.SQLGetDescription(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * From Purchase_Order_details Where POD_MasterID =" & iMasterID & " And POD_CompID=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("POD_Commodity")
                    dr("ItemId") = dt.Rows(i)("POD_DescriptionID")
                    dr("HistoryId") = dt.Rows(i)("POD_HistoryID")
                    dr("UnitId") = dt.Rows(i)("POD_Unit")
                    dr("Comodity") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("POD_Commodity") & "' and Inv_CompID =" & iCompID & "")
                    dr("Units") = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("POD_Unit") & " and Mas_CompID =" & iCompID & "")
                    dr("Descriptions") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("POD_DescriptionID") & "' and Inv_CompID =" & iCompID & "")
                    dr("Mrp") = dt.Rows(i)("POD_Rate")
                    dr("OrderedQty") = dt.Rows(i)("POD_Quantity")
                    dr("ReceivedQty") = ""
                    dr("AccpetedQty") = ""
                    dr("RejectedQty") = ""
                    dr("ExcessQty") = ""
                    dr("MDate") = ""
                    dr("Edate") = ""
                    dr("PendingItem") = ""
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckVerifiedOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer, ByVal DcoRefNo As String) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * from Purchase_verification where PV_OrderNo=" & OrderNo & " and PV_DocRefNo='" & DcoRefNo & "' and PV_YearID =" & iYearID & " and PV_CompID =" & iCompID & ""
            Return objDB.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckInwardedOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer, ByVal DcoRefNo As String) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "select * from Purchase_GIN_Master where PGM_OrderID=" & OrderNo & " and PGM_DocumentRefNo='" & DcoRefNo & "' and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & " and PGM_Status='A'"
            Return objDB.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSupplierName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID from CustomerSupplierMaster where CSM_ID In(Select POM_Supplier from Purchase_Order_Master where POM_YearID = " & iYearID & " and POM_CompID =" & iCompID & " and POM_ID In(Select POD_MasterID from Purchase_Order_Details where POD_MasterID =" & OrderNo & " and POD_CompID =" & iCompID & "))"
            Return objDB.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetCommodityID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal InvDescription As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select INV_ID From Inventory_Master Where INV_Description='" & InvDescription & "' and Inv_CompID =" & iCompID & ""
            Return objDB.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCurrentMonthID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SELECT MONTH(GETDATE())"
            Return objDB.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUnitID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal InvDescription As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_ID From Acc_General_Master Where Mas_Desc='" & InvDescription & "' and Mas_CompID = " & iCompID & ""
            Return objDB.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPODetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim TReceivedQty As Integer
        Dim dtTab As New DataTable
        'iYearID = 1
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("BatchNumber", GetType(String))
            dtTab.Columns.Add(dc)

            sSql = "" : sSql = "Select * From Purchase_Order_details Where POD_MasterID = " & iMasterID & " And POD_CompID=" & iCompID & " and POD_Status <> 'D'"
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    'TReceivedQty = objDB.SQLExecuteScalarInt(sNameSpace, "Select sum(PGD_ReceivedQnt) from Purchase_GIN_details where PGD_MasterID in (select  PGM_ID from Purchase_GIN_master where PGM_OrderID=" & iMasterID & " and PGM_YearID = " & iYearID & " and PGM_CompID =" & iCompID & ") And PGD_DescriptionID ='" & dt.Rows(i)("POD_DescriptionID") & "' and PGD_CompID =" & iCompID & "")
                    'If ((dt.Rows(i)("POD_Quantity") - TReceivedQty) > 0) Then
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("POD_Commodity")
                    dr("ItemId") = dt.Rows(i)("POD_DescriptionID")
                    dr("HistoryId") = dt.Rows(i)("POD_HistoryID")
                    dr("UnitId") = dt.Rows(i)("POD_Unit")
                    dr("Comodity") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("POD_Commodity") & "' and Inv_CompID =" & iCompID & "")
                    dr("Units") = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("POD_Unit") & " ")
                    dr("Descriptions") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("POD_DescriptionID") & "' and Inv_CompID =" & iCompID & "")
                    dr("Mrp") = dt.Rows(i)("POD_Rate")
                    dr("OrderedQty") = dt.Rows(i)("POD_Quantity")
                    If IsDBNull(dt.Rows(i)("POD_Quantity")) = False Then
                        dr("OrderedQty") = Math.Round(dt.Rows(i)("POD_Quantity"), 2)
                    End If
                    dr("ReceivedQty") = Convert.ToDecimal(dt.Rows(i)("POD_Quantity")) - TReceivedQty
                    dr("AccpetedQty") = Convert.ToDecimal(dt.Rows(i)("POD_Quantity")) - TReceivedQty
                    dr("RejectedQty") = 0
                    dr("ExcessQty") = 0
                    dr("MDate") = ""
                    dr("Edate") = ""
                    dr("BatchNumber") = ""
                    dr("PendingItem") = Convert.ToDecimal(dt.Rows(i)("POD_Quantity")) - TReceivedQty
                    dtTab.Rows.Add(dr)
                    'End If
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExistDocRefNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select PRM_ID,PRM_DocumentRefNo From Purchase_Registry_master Where PRM_OrderNo=" & iID & " and PRM_CompID=" & iCompID & " and PRM_YearID =" & iYearID & " order by PRM_ID"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExistingInwardNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select PGM_ID,PGM_GIN_Number From Purchase_GIN_Master Where PGM_OrderID in(select POM_ID from purchase_order_master where POM_OralStatus='P') and PGM_CompID=" & iCompID & " and PGM_YearID = " & iYearID & " order by PGM_ID desc"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDescriptionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDescriptionID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtNew As New DataTable
        Try
            dtNew.Columns.Add("InvH_ID")
            dtNew.Columns.Add("INVH_PreDeterminedPrice")

            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iDescriptionID & " and InvH_CompID =" & iCompID & ""
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

    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_Parent = " & iCommodity & " and Inv_CompID =" & iCompID & " and Inv_Code<>''"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadInwardNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select PGM_ID,PGM_GIN_Number From Purchase_GIN_Master Where PGM_OrderID=" & iID & " and PGM_CompID=" & iCompID & " order by PGM_ID"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 And Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPODetailsFromRegister(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal DocRefID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim TReceivedQty As Integer
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("BatchNumber", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * From Purchase_Registry_details Where PRD_MasterID In(Select PRM_ID from Purchase_Registry_master where PRM_ID=" & DocRefID & " and PRM_OrderNo=" & iMasterID & " and PRM_YearID =" & iYearID & " and PRM_CompID =" & iCompID & ") and PRD_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    TReceivedQty = objDB.SQLExecuteScalarInt(sNameSpace, "Select sum(PGD_ReceivedQnt) from Purchase_GIN_details where PGD_MasterID In (Select PGM_ID from Purchase_GIN_master where PGM_OrderID=" & iMasterID & " and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & ") And PGD_DescriptionID='" & dt.Rows(i)("PRD_DescID") & "' and PGD_CompID =" & iCompID & "")
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("PRD_Commodity")
                    dr("ItemId") = dt.Rows(i)("PRD_DescID")
                    dr("HistoryId") = dt.Rows(i)("PRD_HistoryID")
                    dr("UnitId") = dt.Rows(i)("PRD_UnitID")
                    dr("Comodity") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PRD_Commodity") & "' and Inv_CompID = " & iCompID & "")
                    dr("Units") = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("PRD_UnitID") & " and Mas_CompID =" & iCompID & "")
                    dr("Descriptions") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("PRD_DescID") & "' and Inv_CompID = " & iCompID & "")
                    dr("Mrp") = dt.Rows(i)("PRD_MRP")
                    dr("OrderedQty") = dt.Rows(i)("PRD_OrderQuntity")
                    dr("ReceivedQty") = dt.Rows(i)("PRD_RecievedQnt")
                    dr("AccpetedQty") = dt.Rows(i)("PRD_AccptedQty")
                    dr("RejectedQty") = dt.Rows(i)("PRD_Rejected")
                    dr("ExcessQty") = dt.Rows(i)("PRD_ExcessQty")

                    If (dt.Rows(i)("PRD_ManufactureDate") <> "#1/1/1900 12:00:00 AM#") Then
                        dr("MDate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ManufactureDate"), "D")
                        If (dr("MDate") = "01/01/1900") Then
                            dr("MDate") = ""
                        End If
                    Else
                        dr("MDate") = ""
                    End If
                    If (dt.Rows(i)("PRD_ExpireDate") <> "#1/1/1900 12:00:00 AM#") Then
                        dr("Edate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ExpireDate"), "D")
                        If (dr("Edate") = "01/01/1900") Then
                            dr("Edate") = ""
                        End If
                    Else
                        dr("Edate") = ""
                    End If

                    dr("PendingItem") = ""
                    dr("PendingItem") = Convert.ToDecimal(dt.Rows(i)("PRD_OrderQuntity")) - TReceivedQty
                    dr("BatchNumber") = ""
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGINID As Integer) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select GIN_Status From Goods_InwardNote_Master Where GIN_ID=" & iGINID & " And GIN_CompID=" & iCompID & " and GIN_YearID =" & iYearID & ""
            sStatus = objDB.SQLGetDescription(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckGinExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal docRef As String, ByVal OrderNo As String) As Boolean
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select * From Purchase_GIN_Master Where PGM_DocumentRefNo ='" & docRef & "' And PGM_CompID=" & iCompID & " and PGM_YearID =" & iYearID & " and PGM_OrderID =" & OrderNo & ""
            sStatus = objDB.SQLCheckForRecord(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function CheckVerifiedOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal docRef As String, ByVal OrderID As Integer) As Boolean
    '    Dim sSql As String = "", sStatus As String = ""
    '    Try
    '        sSql = "Select * from Purchase_verification where PV_OrderNo=" & OrderID & " and PV_DocRefNo='" & docRef & "' PV_YearID =" & iYearID & " and PV_CompID=" & iCompID & " "
    '        sStatus = objDB.SQLCheckForRecord(sNameSpace, sSql)
    '        Return sStatus
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    Public Function SaveTransactionInvoiceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal ObjGoods As ClsGoodsInward) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_GINID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_Gin_Number
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_Commodity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_DescriptionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_DescriptionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_InvoiceRefNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.PGM_DocRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_AcceptedQnt", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Accepted
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_MRP", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.PGD_MRP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_Status", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_Excess", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Excess
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "sPPurchaseInvoiceAccepted", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInwardDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal InwardNo As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Dim qnt As Integer = 0
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("BatchNumber", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * From Purchase_GIN_Details Where PGD_CompID=" & iCompID & " and PGD_MasterID=" & InwardNo & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("PGD_CommodityID")
                    dr("ItemId") = dt.Rows(i)("PGD_DescriptionID")
                    dr("HistoryId") = dt.Rows(i)("PGD_HistoryID")
                    dr("UnitId") = dt.Rows(i)("PGD_UnitID")
                    dr("Comodity") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PGD_CommodityID") & "' and inv_CompID =" & iCompID & "")
                    dr("Units") = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID='" & dt.Rows(i)("PGD_UnitID") & "' and Mas_CompID = " & iCompID & "")
                    dr("Descriptions") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("PGD_DescriptionID") & "' and inv_CompID =" & iCompID & "")
                    dr("Mrp") = dt.Rows(i)("PGD_MRP")
                    dr("OrderedQty") = dt.Rows(i)("PGD_OrderQnt")
                    dr("ReceivedQty") = dt.Rows(i)("PGD_ReceivedQnt")
                    dr("AccpetedQty") = dt.Rows(i)("PGD_Accepted")
                    dr("RejectedQty") = dt.Rows(i)("PGD_RejectedQnt")
                    dr("ExcessQty") = dt.Rows(i)("PGD_Excess")

                    If (dt.Rows(i)("PGD_ManufactureDate") <> "#1/1/1900 12:00:00 AM#") Then
                        dr("MDate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PGD_ManufactureDate"), "D")
                    Else
                        dr("MDate") = ""
                    End If
                    If (dt.Rows(i)("PGD_ExpireDate") <> "#1/1/1900 12:00:00 AM#") Then
                        dr("Edate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PGD_ExpireDate"), "D")
                    Else
                        dr("Edate") = ""
                    End If
                    qnt = dt.Rows(i)("PGD_PendingQnt") - dt.Rows(i)("PGD_ReceivedQnt")
                    If (qnt > 0) Then
                        dr("PendingItem") = dt.Rows(i)("PGD_PendingQnt") - dt.Rows(i)("PGD_ReceivedQnt")
                    Else
                        dr("PendingItem") = 0
                    End If
                    dr("BatchNumber") = dt.Rows(i)("PGD_BatchNumber")
                    dr("Status") = objDB.SQLGetDescription(sNameSpace, "Select PGM_Status From Purchase_GIN_master Where PGM_ID='" & dt.Rows(i)("PGD_MasterID") & "' and PGM_YearID =" & iYearId & " and PGM_CompID =" & iCompID & "")
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseORderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal OrderNo As Integer, ByVal DocRef As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * From Purchase_Invoice_Excess Where PIE_CompID=" & iCompID & " and PIE_YearID =" & iYearID & " and PIE_OrderID=" & OrderNo & " and PIE_DocRef='" & DocRef & "' "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("PIE_CommodityID")
                    dr("ItemId") = dt.Rows(i)("PIE_Description")
                    dr("HistoryId") = dt.Rows(i)("PIE_HistoryID")
                    dr("UnitId") = dt.Rows(i)("PIE_UnitID")
                    dr("Comodity") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PIE_CommodityID") & "' ")
                    dr("Units") = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID='" & dt.Rows(i)("PIE_UnitID") & "' ")
                    dr("Descriptions") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("PIE_Description") & "' ")
                    dr("Mrp") = dt.Rows(i)("PIE_Rate")
                    dr("OrderedQty") = dt.Rows(i)("PIE_Quantity")
                    dr("ReceivedQty") = dt.Rows(i)("PIE_Quantity")
                    dr("AccpetedQty") = dt.Rows(i)("PIE_AcceptQty")
                    dr("RejectedQty") = dt.Rows(i)("PIE_Rejected")
                    dr("ExcessQty") = ""
                    dr("Edate") = dt.Rows(i)("PIE_ExpireDate")
                    dr("MDate") = dt.Rows(i)("PIE_ManufactureDate")
                    If (IsDBNull(dt.Rows(i)("PIE_ManufactureDate")) = False) Then
                        If (objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PIE_ManufactureDate"), "D") = "01/01/1900") Then
                            dr("MDate") = ""
                        Else
                            dr("MDate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PIE_ManufactureDate"), "D")
                        End If
                    End If


                    If (dt.Rows(i)("PIE_ExpireDate") <> "") Then
                        If (objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PIE_ExpireDate"), "D") = "01/01/1900") Then
                            dr("Edate") = ""
                        Else
                            dr("Edate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PIE_ExpireDate"), "D")
                        End If
                    End If
                    dr("PendingItem") = ""
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function OralLoadPurchaseORderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal OrderNo As Integer, ByVal DocRef As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * From Purchase_Invoice_Excess Where PIE_CompID=" & iCompID & " and PIE_YearID =" & iYearID & " and PIE_OrderID=" & OrderNo & " and PIE_DocRef='" & DocRef & "' "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("PIE_CommodityID")
                    dr("ItemId") = dt.Rows(i)("PIE_Description")
                    dr("HistoryId") = dt.Rows(i)("PIE_HistoryID")
                    dr("UnitId") = dt.Rows(i)("PIE_UnitID")
                    dr("Comodity") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PIE_CommodityID") & "' ")
                    dr("Units") = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID='" & dt.Rows(i)("PIE_UnitID") & "' ")
                    dr("Descriptions") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("PIE_Description") & "' ")
                    dr("Mrp") = dt.Rows(i)("PIE_Rate")
                    dr("OrderedQty") = dt.Rows(i)("PIE_Quantity")
                    dr("ReceivedQty") = dt.Rows(i)("PIE_Quantity")
                    dr("AccpetedQty") = dt.Rows(i)("PIE_AcceptQty")
                    dr("RejectedQty") = dt.Rows(i)("PIE_Rejected")
                    dr("ExcessQty") = ""
                    dr("Edate") = dt.Rows(i)("PIE_ExpireDate")
                    If (dt.Rows(i)("PIE_ManufactureDate") <> "") Then
                        If (objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PIE_ManufactureDate"), "D") = "01/01/1900") Then
                            dr("MDate") = ""
                        Else
                            dr("MDate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PIE_ManufactureDate"), "D")
                        End If
                    End If
                    If (dt.Rows(i)("PIE_ExpireDate") <> "") Then
                        If (objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PIE_ExpireDate"), "D") = "01/01/1900") Then
                            dr("Edate") = ""
                        Else
                            dr("Edate") = objClsFasgnrl.FormatDtForRDBMS(dt.Rows(i)("PIE_ExpireDate"), "D")
                        End If
                    End If
                    dr("PendingItem") = ""
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadInwardNewItemDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal OrderNo As Integer, ByVal InwardNo As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * From Purchase_Invoice_Excess Where PIE_CompID=" & iCompID & " and PIE_OrderID=" & OrderNo & " and PIE_YearID = " & iYearId & " and PIE_GINID='" & InwardNo & "' "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("PIE_CommodityID")
                    dr("ItemId") = dt.Rows(i)("PIE_Description")
                    dr("HistoryId") = dt.Rows(i)("PIE_HistoryID")
                    dr("UnitId") = dt.Rows(i)("PIE_UnitID")
                    dr("Comodity") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PIE_CommodityID") & "' and Inv_CompID =" & iCompID & "")
                    dr("Units") = objDB.SQLExecuteDataTable(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID='" & dt.Rows(i)("PIE_UnitID") & "' and Mas_CompID =" & iCompID & "")
                    dr("Descriptions") = objDB.SQLExecuteDataTable(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("PIE_Description") & "' and Inv_CompID =" & iCompID & "")
                    dr("Mrp") = dt.Rows(i)("PIE_Rate")
                    dr("OrderedQty") = dt.Rows(i)("PIE_Quantity")
                    dr("ReceivedQty") = dt.Rows(i)("PIE_Quantity")
                    dr("AccpetedQty") = dt.Rows(i)("PIE_AcceptQty")
                    dr("RejectedQty") = dt.Rows(i)("PIE_Rejected")
                    dr("ExcessQty") = ""
                    dr("MDate") = ""
                    dr("Edate") = ""
                    dr("PendingItem") = ""
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadInwardNewItemDetailsUsingDocRef(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer, ByVal DocRef As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)

            sSql = "Select * From Purchase_Invoice_Excess Where PIE_CompID=" & iCompID & " and PIE_YearID =" & iYearID & " and PIE_OrderID=" & OrderNo & " and PIE_DocRef='" & DocRef & "' "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("PIE_CommodityID")
                    dr("ItemId") = dt.Rows(i)("PIE_Description")
                    dr("HistoryId") = dt.Rows(i)("PIE_HistoryID")
                    dr("UnitId") = dt.Rows(i)("PIE_UnitID")
                    dr("Comodity") = objDB.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PIE_CommodityID") & "' and Inv_CompID =" & iCompID & "")
                    dr("Units") = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID='" & dt.Rows(i)("PIE_UnitID") & "' and Mas_CompID =" & iCompID & "")
                    dr("Descriptions") = objDB.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("PIE_Description") & "' and Inv_CompID =" & iCompID & "")
                    dr("Mrp") = dt.Rows(i)("PIE_Rate")
                    dr("OrderedQty") = dt.Rows(i)("PIE_Quantity")
                    dr("ReceivedQty") = dt.Rows(i)("PIE_Quantity")
                    dr("AccpetedQty") = dt.Rows(i)("PIE_AcceptQty")
                    dr("RejectedQty") = dt.Rows(i)("PIE_Rejected")
                    dr("ExcessQty") = ""
                    dr("MDate") = ""
                    dr("Edate") = ""
                    dr("PendingItem") = ""
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveNewOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal dRequiredDate As Date, ByVal Mdate As Date, ByVal Edate As Date, ByVal objPO As clsPurchaseOrder)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try

            sSql = "" : sSql = "Select * from Purchase_Invoice_Excess where PIE_CommodityID = " & objPO.iPOD_Commodity & " and "
            sSql = sSql & "PIE_Description = " & objPO.iPOD_DescriptionID & " and PIE_HistoryID =" & objPO.iPOD_HistoryID & " and PIE_CompID = " & iCompID & " and PIE_YearID =" & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Purchase_Invoice_Excess set PIE_Rate='" & objPO.sPOD_Rate & "',PIE_RateAmount = '" & objPO.sPOD_RateAmount & "',PIE_Quantity='" & objPO.sPOD_Quantity & "',"
                sSql = sSql & "PIE_Discount = '" & objPO.sPOD_Discount & "',PIE_DiscountAmount='" & objPO.sPOD_DiscountAmount & "',PIE_Excise='" & objPO.sPOD_Excise & "',"
                sSql = sSql & "PIE_ExciseAmount = '" & objPO.sPOD_ExciseAmount & "',PIE_VAT = '" & objPO.sPOD_VAT & "',PIE_VATAmount='" & objPO.sPOD_VATAmount & "',"
                sSql = sSql & "PIE_TotalAmount='" & objPO.sPOD_TotalAmount & "' where  "
                sSql = sSql & "PIE_CommodityID = " & objPO.iPOD_DescriptionID & " and "
                sSql = sSql & "PIE_HistoryID =" & objPO.iPOD_HistoryID & " and PIE_CompID = " & iCompID & " and PIE_OrderID ='" & objPO.sPOM_OrderNo & "' and PIE_Description= '" & objPO.iPOD_DescriptionID & "' and PIE_DocRef='" & objPO.POM_DocRef & "'"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objGnrlFnction.GetMaxID(sNameSpace, iCompID, "Purchase_Invoice_Excess", "PIE_ID", "PIE_CompID")
                sSql = "" : sSql = "Insert into Purchase_Invoice_Excess(PIE_ID,PIE_CommodityID,"
                sSql = sSql & "PIE_HistoryID,PIE_Rate,PIE_RateAmount,"
                sSql = sSql & "PIE_Quantity,PIE_Discount,PIE_DiscountAmount,PIE_Excise,"
                sSql = sSql & "PIE_ExciseAmount,PIE_VAT,PIE_VATAmount,"
                sSql = sSql & "PIE_TotalAmount,PIE_CompID,PIE_UnitID,PIE_Rejected,PIE_AcceptQty,"
                sSql = sSql & "PIE_DocRef,PIE_OrderID,PIE_Description,PIE_GINID,PIE_OrderedQty,PIE_RecivedQty,"
                sSql = sSql & "PIE_ManufactureDate,PIE_ExpireDate,PIE_Status,PIE_Delflag,PIE_YearID)"
                sSql = sSql & "Values(" & iMax & "," & objPO.iPOD_Commodity & ","
                sSql = sSql & "" & objPO.iPOD_HistoryID & ",'" & objPO.sPOD_Rate & "','" & objPO.sPOD_RateAmount & "',"
                sSql = sSql & "'" & objPO.sPOD_Quantity & "','" & objPO.sPOD_Discount & "','" & objPO.sPOD_DiscountAmount & "','" & objPO.sPOD_Excise & "',"
                sSql = sSql & "'" & objPO.sPOD_ExciseAmount & "','" & objPO.sPOD_VAT & "','" & objPO.sPOD_VATAmount & "',"
                sSql = sSql & "'" & objPO.sPOD_TotalAmount & "'," & iCompID & ",'" & objPO.iPOD_Unit & "','" & objPO.POD_Rejected & "','" & objPO.POD_Accepted & "',"
                sSql = sSql & "'" & objPO.POM_DocRef & "','" & objPO.sPOM_OrderNo & "','" & objPO.iPOD_DescriptionID & "','" & objPO.sPOM_InwardNo & "',"
                sSql = sSql & "" & objPO.POD_OrderedQty & "," & objPO.POD_ReceivedQty & ",'" & Mdate & "','" & Edate & "','N','W'," & iYearID & ")"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePurchaseOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal dOrderDate As Date, ByVal objPO As clsPurchaseOrder) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Invoice_Excess where PIE_OrderID  = '" & objPO.sPOM_OrderNo & "' and PIE_YearID =" & iYearID & " and PIE_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then

                sSql = "" : sSql = "Update Purchase_Invoice_Excess set PIE_SuplierId = " & objPO.iPOM_Supplier & ",PIE_ModeOfShipping = " & objPO.iPOM_ModeOfShipping & " "
                sSql = sSql & "Where PIE_OrderNo = '" & objPO.sPOM_OrderNo & "' and PIE_CompID =" & iCompID & " and PIE_YearID = " & iYearID & ""
                objDB.SQLExecuteDataTable(sNameSpace, sSql)
                Return dt.Rows(0)("POM_ID")
            Else
                iMax = objGnrlFnction.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Master", "PIE_ID", "PIE_CompID")
                sSql = "" : sSql = "Insert into Purchase_Invoice_Excess(PIE_ID,PIE_OrderDate,PIE_OrderNo,PIE_Supplier,"
                sSql = sSql & "PIE_ModeOfShipping,PIE_Status,PIE_CreatedBy,PIE_CreatedOn,"
                sSql = sSql & "PIE_YearID,PIE_CompID)Values(" & iMax & ",GetDate(),'" & objPO.sPOM_OrderNo & "'," & objPO.iPOM_Supplier & ","
                sSql = sSql & "" & objPO.iPOM_ModeOfShipping & ",'A'," & objPO.iPOM_CreatedBy & ",GetDate(),"
                sSql = sSql & "" & objPO.iPOM_YearID & "," & iCompID & ")"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionReturnsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal ObjGoods As ClsGoodsInward) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_GINID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_Commodity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_DescriptionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_DescriptionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_InvoiceRefNo", OleDb.OleDbType.VarChar, 100)
            'ObjParam(iParamCount).Value = ObjGoods.PGM_DocRefNo
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_InvoiceRefNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.PGM_DocRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_AcceptedQnt", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Accepted
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_RejectedQty", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = ObjGoods.PGD_RejectedQnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_MRP", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.PGD_MRP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_Status", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjGoods.PGD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "sPPurchaseInvoiceRejected", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGINInvID(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal IYearID As Integer, ByVal IOrderID As Integer, ByVal GINDocRefNo As String) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = " Select  PGM_ID From Purchase_GIN_Master Where PGM_DocumentRefNo='" & GINDocRefNo & "' and PGM_OrderID = " & IOrderID & " and PGM_CompID=" & ICompID & " and PGM_YearID = " & IYearID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveReturnMaster(ByVal sNameSpace As String, ByVal iYearID As Integer, ByVal ObjGoods As ClsGoodsInward, ByVal purchaseReturnNo As String, ByVal GINInvID As Integer) As Array
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Goods_Return_Master where GRM_OrderID = " & ObjGoods.PGD_OrderID & " and GRM_GINInvId = " & GINInvID & " and GRM_GINInvNo = '" & ObjGoods.PGM_DocRefNo & "' and GRM_CompID = " & ObjGoods.PGD_CompID & " and GRM_YearId = " & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Goods_Return_Master set GRM_OrderDate = " & objClsFasgnrl.FormatDtForRDBMS(ObjGoods.PGM_OrderDate, "U") & "  ,GRM_Supplier=" & ObjGoods.PGM_Supplier & ", GRM_Status='C', GRM_DelFlag='W', GRM_Operation='C', GRM_CreatedBy = " & ObjGoods.PGM_CrBy & ",GRM_CreatedOn = GETDATE(),GRM_YearID = " & ObjGoods.PGM_YearID & ",GRM_CompID = " & ObjGoods.PGD_CompID & ",GRM_IPAddress = '" & ObjGoods.PGM_IPAddress & "' where GRM_OrderID = " & ObjGoods.PGD_OrderID & " and GRM_GINInvId = " & GINInvID & " and GRM_GINInvNo = '" & ObjGoods.PGM_DocRefNo & "' and GRM_CompID = " & ObjGoods.PGD_CompID & " and GRM_YearId = " & iYearID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objGnrlFnction.GetMaxID(sNameSpace, ObjGoods.PGD_CompID, "Goods_Return_Master", "GRM_ID", "GRM_CompID")
                sSql = "" : sSql = "Insert into Goods_Return_Master(GRM_ID,GRM_OrderID,GRM_OrderDate,GRM_GINInvID,GRM_GINInvNo,GRM_ReturnNo,GRM_Supplier,GRM_CreatedBy,GRM_CreatedOn,GRM_YearID,GRM_CompID,GRM_IPAddress,GRM_Status,GRM_DelFlag,GRM_Operation,GRM_GR_ID,GRM_PR_ID)"
                sSql = sSql & "Values(" & iMax & "," & ObjGoods.PGD_OrderID & ", " & objClsFasgnrl.FormatDtForRDBMS(ObjGoods.PGM_OrderDate, "I") & " ," & GINInvID & ",'" & ObjGoods.PGM_DocRefNo & "','" & purchaseReturnNo & "'," & ObjGoods.PGM_Supplier & "," & ObjGoods.PGM_CrBy & ", GETDATE() ," & ObjGoods.PGM_YearID & "," & ObjGoods.PGD_CompID & ",'" & ObjGoods.PGM_IPAddress & "','C','W','C',1,0)"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPRMiD(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer, ByVal GINInvID As Integer, ByVal ObjGoods As ClsGoodsInward) As String
        Dim sSql As String = ""
        Try
            sSql = "Select GRM_ID from Goods_Return_Master where GRM_OrderID =" & OrderNo & " and GRM_GINInvId = " & GINInvID & " and GRM_GINInvNo = '" & ObjGoods.PGM_DocRefNo & "' and GRM_CompID = " & iCompID & " and GRM_YearId = " & iYearID & ""
            Return objDB.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveReturnDetails(ByVal sNameSpace As String, ByVal ObjGoods As ClsGoodsInward, ByVal iMasterID As Integer) As Array
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            iMax = objGnrlFnction.GetMaxID(sNameSpace, ObjGoods.PGD_CompID, "Goods_Return_Details", "GRD_ID", "GRD_CompID")
            sSql = "" : sSql = "Select * from Goods_Return_Details where GRD_MasterID = " & iMasterID & " and GRD_Commodity = " & ObjGoods.PGD_CommodityID & " and GRD_DescriptionID = " & ObjGoods.PGD_DescriptionID & " and GRD_HistoryID = " & ObjGoods.PGD_HistoryID & " and GRD_YearId = " & ObjGoods.PGM_YearID & " and GRD_CompID = " & ObjGoods.PGD_CompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Goods_Return_Details set GRD_Commodity = " & ObjGoods.PGD_CommodityID & ",GRD_DescriptionID=" & ObjGoods.PGD_DescriptionID & ",GRD_HistoryID = " & ObjGoods.PGD_HistoryID & ", GRD_UnitID = " & ObjGoods.PGD_UnitID & ", GRD_RateAmount = " & ObjGoods.PGD_MRP & ", GRD_Quantity = " & ObjGoods.PGD_RejectedQnt & ", GRD_Total = " & ObjGoods.iRateAmount & ", GRD_CreatedBy = " & ObjGoods.PGM_CrBy & ",GRD_CreatedOn = GETDATE(), GRD_CompID = " & ObjGoods.PGD_CompID & ", GRD_Status='C', GRD_DelFlag='W', GRD_Operation='C', "
                sSql = sSql & "GRD_Discount = " & ObjGoods.POD_Discount & ",GRD_DiscountAmount = " & ObjGoods.iDiscountAmount & ",GRD_TotalAmount = " & ObjGoods.iTotalAmount & ",GRD_ChargesPerItem = " & ObjGoods.iCharges & ","
                sSql = sSql & "GRD_Amount = " & ObjGoods.iAmount & ",GRD_GST_ID = " & ObjGoods.sPOD_GSTId & ",GRD_GSTRate = " & ObjGoods.sPOD_GSTRate & ",GRD_GSTAmount = " & ObjGoods.iGSTAmount & ""
                sSql = sSql & "where GRD_MasterID = " & iMasterID & " and GRD_Commodity = " & ObjGoods.PGD_CommodityID & " and GRD_DescriptionID = " & ObjGoods.PGD_DescriptionID & " and GRD_HistoryID = " & ObjGoods.PGD_HistoryID & " and GRD_YearId = " & ObjGoods.PGM_YearID & " and GRD_CompID = " & ObjGoods.PGD_CompID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                sSql = "" : sSql = "Insert into Goods_Return_Details(GRD_ID,GRD_MasterID,GRD_Commodity,GRD_DescriptionID,GRD_HistoryID,GRD_Quantity,GRD_Total,"
                sSql = sSql & "GRD_Discount,GRD_DiscountAmount,GRD_TotalAmount,GRD_ChargesPerItem,"
                sSql = sSql & "GRD_Amount,GRD_GST_ID,GRD_GSTRate,GRD_GSTAmount,GRD_UnitID,GRD_RateAmount,"
                sSql = sSql & "GRD_YearId,GRD_CompID,GRD_Status,GRD_DelFlag,GRD_Operation,GRD_CreatedBy,GRD_CreatedOn)"
                sSql = sSql & "Values(" & iMax & "," & iMasterID & "," & ObjGoods.PGD_CommodityID & "," & ObjGoods.PGD_DescriptionID & ", " & ObjGoods.PGD_HistoryID & ", " & ObjGoods.PGD_RejectedQnt & "," & ObjGoods.iRateAmount & ","
                sSql = sSql & "" & ObjGoods.POD_Discount & "," & ObjGoods.iDiscountAmount & "," & ObjGoods.iTotalAmount & "," & ObjGoods.iCharges & ","
                sSql = sSql & "" & ObjGoods.iAmount & "," & ObjGoods.sPOD_GSTId & "," & ObjGoods.sPOD_GSTRate & "," & ObjGoods.iGSTAmount & "," & ObjGoods.PGD_UnitID & "," & ObjGoods.PGD_MRP & ","
                sSql = sSql & "" & ObjGoods.PGM_YearID & "," & ObjGoods.PGD_CompID & ",'C','W','C'," & ObjGoods.PGM_CrBy & ",GETDATE())"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotChargeAmount(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal iYearID As Integer, ByVal OrderID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select sum(C_ChargeAmount) from Charges_Master where C_PSType='P' And C_POrderID = " & OrderID & " and C_CompID = " & ICompID & " and C_YearID = " & iYearID & ""
            Return objDB.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotRateAmount(ByVal sNameSpace As String, ByVal ICompID As Integer, ByVal OrderID As Integer, ByVal iCommosityID As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT sum(CAST(POD_RateAmount AS DECIMAL(10,2))) FROM Purchase_Order_Details where POD_MasterID = " & OrderID & " and POD_Commodity = " & iCommosityID & " and POD_DescriptionID = " & iDescriptionID & " and POD_HistoryID = " & iHistoryID & " and POD_CompID = " & ICompID & ""
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPRDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer, ByVal iCommosityID As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Details where POD_MasterID = " & iOrderID & " and POD_Commodity = " & iCommosityID & " and POD_DescriptionID = " & iDescriptionID & " and POD_HistoryID = " & iHistoryID & " and POD_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
