Imports System
Imports System.Data
Imports DatabaseLayer
Public Class ClsInvoiceForm
    Private objDb As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iPIM_ID As Integer
    Private sPIM_No As String
    Private iPIM_OrderID As Integer
    Private iPIM_PRegesterID As Integer
    Private dPIM_OrderDate As DateTime
    Private iPIM_SupplierID As Integer
    Private dPIM_InvoiceDate As DateTime
    Private iPIM_CreatedBy As Integer
    Private dPIM_CreatedOn As DateTime
    Private sPIM_Status As String
    Private iPIM_YearID As Integer
    Private iPIM_CompID As Integer
    Private iPIM_TrType As Integer
    Private sPIM_CompanyAddress As String
    Private sPIM_CompanyGSTNRegNo As String
    Private sPIM_BillingAddress As String
    Private sPIM_BillingGSTNRegNo As String
    Private sPIM_DeliveryFrom As String
    Private sPIM_DeliveryFromGSTNRegNo As String
    Private sPIM_ReceiveAddress As String
    Private sPIM_ReceiveGSTNRegNo As String
    Private sPIM_InvoiceStatus As String
    Private iPIM_CompanyType As Integer
    Private iPIM_GSTNCategory As Integer
    Private dPIM_ManualBillAmount As Double
    Private dPIM_ManualGST As Double
    Private sPIM_BillDifferenceStatus As String
    Private sPIM_Operation As String
    Private sPIM_IPAddress As String
    Private sPIM_State As String

    Private iPID_ID As Integer
    Private iPID_MasterID As Integer
    Private iPID_CommodityID As Integer
    Private iPID_DescID As Integer
    Private iPID_UnitID As Integer
    Private iPID_HistoryID As Integer
    Private sPID_Remarks As String
    Private dPID_Rate As Double
    Private dPID_Quantity As Double
    Private dPID_ChargePerItem As Double
    Private dPID_RateAmount As Double
    Private dPID_Discount As Double
    Private dPID_DiscountAmount As Double
    Private dPID_Amount As Double
    Private iPID_GSTID As Integer
    Private dPID_GSTRate As Double
    Private dPID_GSTAmount As Double
    Private dPID_SGST As Double
    Private dPID_SGSTAmount As Double
    Private dPID_CGST As Double
    Private dPID_CGSTAmount As Double
    Private dPID_IGST As Double
    Private dPID_IGSTAmount As Double
    Private dPID_FinalTotal As Double
    Private sPID_ItemStatus As String
    Private sPID_Status As String
    Private iPID_CompID As Integer
    Private iPID_CreatedBy As Integer
    Private dPID_CreatedOn As DateTime
    Private sPID_Operation As String
    Private sPID_IPAddress As String

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

    Private dPID_URD_GSTRate As Double
    Private dPID_URD_GSTAmt As Double
    Private dPID_URD_SGST As Double
    Private dPID_URD_SGSTAmt As Double
    Private dPID_URD_CGST As Double
    Private dPID_URD_CGSTAmt As Double

    Public Property PID_URD_GSTRate() As Double
        Get
            Return (dPID_URD_GSTRate)
        End Get
        Set(ByVal Value As Double)
            dPID_URD_GSTRate = Value
        End Set
    End Property
    Public Property PID_URD_GSTAmt() As Double
        Get
            Return (dPID_URD_GSTAmt)
        End Get
        Set(ByVal Value As Double)
            dPID_URD_GSTAmt = Value
        End Set
    End Property
    Public Property PID_URD_SGST() As Double
        Get
            Return (dPID_URD_SGST)
        End Get
        Set(ByVal Value As Double)
            dPID_URD_SGST = Value
        End Set
    End Property
    Public Property PID_URD_SGSTAmt() As Double
        Get
            Return (dPID_URD_SGSTAmt)
        End Get
        Set(ByVal Value As Double)
            dPID_URD_SGSTAmt = Value
        End Set
    End Property
    Public Property PID_URD_CGST() As Double
        Get
            Return (dPID_URD_CGST)
        End Get
        Set(ByVal Value As Double)
            dPID_URD_CGST = Value
        End Set
    End Property
    Public Property PID_URD_CGSTAmt() As Double
        Get
            Return (dPID_URD_CGSTAmt)
        End Get
        Set(ByVal Value As Double)
            dPID_URD_CGSTAmt = Value
        End Set
    End Property


    Public Property PIM_ManualBillAmount() As Double
        Get
            Return (dPIM_ManualBillAmount)
        End Get
        Set(ByVal Value As Double)
            dPIM_ManualBillAmount = Value
        End Set
    End Property
    Public Property PIM_ManualGST() As Double
        Get
            Return (dPIM_ManualGST)
        End Get
        Set(ByVal Value As Double)
            dPIM_ManualGST = Value
        End Set
    End Property
    Public Property PIM_BillDifferenceStatus() As String
        Get
            Return (sPIM_BillDifferenceStatus)
        End Get
        Set(ByVal Value As String)
            sPIM_BillDifferenceStatus = Value
        End Set
    End Property


    Public Property PIM_ID() As Integer
        Get
            Return (iPIM_ID)
        End Get
        Set(ByVal Value As Integer)
            iPIM_ID = Value
        End Set
    End Property
    Public Property PIM_No() As String
        Get
            Return (sPIM_No)
        End Get
        Set(ByVal Value As String)
            sPIM_No = Value
        End Set
    End Property
    Public Property PIM_OrderID() As Integer
        Get
            Return (iPIM_OrderID)
        End Get
        Set(ByVal Value As Integer)
            iPIM_OrderID = Value
        End Set
    End Property
    Public Property PIM_PRegesterID() As Integer
        Get
            Return (iPIM_PRegesterID)
        End Get
        Set(ByVal Value As Integer)
            iPIM_PRegesterID = Value
        End Set
    End Property
    Public Property PIM_OrderDate() As DateTime
        Get
            Return (dPIM_OrderDate)
        End Get
        Set(ByVal Value As DateTime)
            dPIM_OrderDate = Value
        End Set
    End Property
    Public Property PIM_SupplierID() As Integer
        Get
            Return (iPIM_SupplierID)
        End Get
        Set(ByVal Value As Integer)
            iPIM_SupplierID = Value
        End Set
    End Property
    Public Property PIM_InvoiceDate() As DateTime
        Get
            Return (dPIM_InvoiceDate)
        End Get
        Set(ByVal Value As DateTime)
            dPIM_InvoiceDate = Value
        End Set
    End Property
    Public Property PIM_CreatedBy() As Integer
        Get
            Return (iPIM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPIM_CreatedBy = Value
        End Set
    End Property
    Public Property PIM_CreatedOn() As DateTime
        Get
            Return (dPIM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dPIM_CreatedOn = Value
        End Set
    End Property
    Public Property PIM_Status() As String
        Get
            Return (sPIM_Status)
        End Get
        Set(ByVal Value As String)
            sPIM_Status = Value
        End Set
    End Property
    Public Property PIM_YearID() As Integer
        Get
            Return (iPIM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iPIM_YearID = Value
        End Set
    End Property
    Public Property PIM_CompID() As Integer
        Get
            Return (iPIM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iPIM_CompID = Value
        End Set
    End Property
    Public Property PIM_TrType() As Integer
        Get
            Return (iPIM_TrType)
        End Get
        Set(ByVal Value As Integer)
            iPIM_TrType = Value
        End Set
    End Property
    Public Property PIM_CompanyAddress() As String
        Get
            Return (sPIM_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sPIM_CompanyAddress = Value
        End Set
    End Property
    Public Property PIM_CompanyGSTNRegNo() As String
        Get
            Return (sPIM_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPIM_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property PIM_BillingAddress() As String
        Get
            Return (sPIM_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sPIM_BillingAddress = Value
        End Set
    End Property
    Public Property PIM_BillingGSTNRegNo() As String
        Get
            Return (sPIM_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPIM_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property PIM_DeliveryFrom() As String
        Get
            Return (sPIM_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sPIM_DeliveryFrom = Value
        End Set
    End Property
    Public Property PIM_DeliveryFromGSTNRegNo() As String
        Get
            Return (sPIM_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPIM_DeliveryFromGSTNRegNo = Value
        End Set
    End Property
    Public Property PIM_ReceiveAddress() As String
        Get
            Return (sPIM_ReceiveAddress)
        End Get
        Set(ByVal Value As String)
            sPIM_ReceiveAddress = Value
        End Set
    End Property
    Public Property PIM_ReceiveGSTNRegNo() As String
        Get
            Return (sPIM_ReceiveGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sPIM_ReceiveGSTNRegNo = Value
        End Set
    End Property
    Public Property PIM_InvoiceStatus() As String
        Get
            Return (sPIM_InvoiceStatus)
        End Get
        Set(ByVal Value As String)
            sPIM_InvoiceStatus = Value
        End Set
    End Property
    Public Property PIM_State() As String
        Get
            Return (sPIM_State)
        End Get
        Set(ByVal Value As String)
            sPIM_State = Value
        End Set
    End Property
    Public Property PIM_CompanyType() As Integer
        Get
            Return (iPIM_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iPIM_CompanyType = Value
        End Set
    End Property
    Public Property PIM_GSTNCategory() As Integer
        Get
            Return (iPIM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iPIM_GSTNCategory = Value
        End Set
    End Property
    Public Property PIM_Operation() As String
        Get
            Return (sPIM_Operation)
        End Get
        Set(ByVal Value As String)
            sPIM_Operation = Value
        End Set
    End Property
    Public Property PIM_IPADdress() As String
        Get
            Return (sPIM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sPIM_IPAddress = Value
        End Set
    End Property


    Public Property PID_ID() As Integer
        Get
            Return (iPID_ID)
        End Get
        Set(ByVal Value As Integer)
            iPID_ID = Value
        End Set
    End Property
    Public Property PID_MasterID() As Integer
        Get
            Return (iPID_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iPID_MasterID = Value
        End Set
    End Property
    Public Property PID_CommodityID() As Integer
        Get
            Return (iPID_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iPID_CommodityID = Value
        End Set
    End Property
    Public Property PID_DescID() As Integer
        Get
            Return (iPID_DescID)
        End Get
        Set(ByVal Value As Integer)
            iPID_DescID = Value
        End Set
    End Property
    Public Property PID_UnitID() As Integer
        Get
            Return (iPID_UnitID)
        End Get
        Set(ByVal Value As Integer)
            iPID_UnitID = Value
        End Set
    End Property
    Public Property PID_HistoryID() As Integer
        Get
            Return (iPID_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iPID_HistoryID = Value
        End Set
    End Property
    Public Property PID_Remarks() As String
        Get
            Return (sPID_Remarks)
        End Get
        Set(ByVal Value As String)
            sPID_Remarks = Value
        End Set
    End Property
    Public Property PID_Rate() As Double
        Get
            Return (dPID_Rate)
        End Get
        Set(ByVal Value As Double)
            dPID_Rate = Value
        End Set
    End Property
    Public Property PID_Quantity() As Double
        Get
            Return (dPID_Quantity)
        End Get
        Set(ByVal Value As Double)
            dPID_Quantity = Value
        End Set
    End Property
    Public Property PID_ChargePerItem() As Double
        Get
            Return (dPID_ChargePerItem)
        End Get
        Set(ByVal Value As Double)
            dPID_ChargePerItem = Value
        End Set
    End Property
    Public Property PID_RateAmount() As Double
        Get
            Return (dPID_RateAmount)
        End Get
        Set(ByVal Value As Double)
            dPID_RateAmount = Value
        End Set
    End Property
    Public Property PID_Discount() As Double
        Get
            Return (dPID_Discount)
        End Get
        Set(ByVal Value As Double)
            dPID_Discount = Value
        End Set
    End Property
    Public Property PID_DiscountAmount() As Double
        Get
            Return (dPID_DiscountAmount)
        End Get
        Set(ByVal Value As Double)
            dPID_DiscountAmount = Value
        End Set
    End Property
    Public Property PID_Amount() As Double
        Get
            Return (dPID_Amount)
        End Get
        Set(ByVal Value As Double)
            dPID_Amount = Value
        End Set
    End Property
    Public Property PID_GSTID() As Integer
        Get
            Return (iPID_GSTID)
        End Get
        Set(ByVal Value As Integer)
            iPID_GSTID = Value
        End Set
    End Property
    Public Property PID_GSTRate() As Double
        Get
            Return (dPID_GSTRate)
        End Get
        Set(ByVal Value As Double)
            dPID_GSTRate = Value
        End Set
    End Property
    Public Property PID_GSTAmount() As Double
        Get
            Return (dPID_GSTAmount)
        End Get
        Set(ByVal Value As Double)
            dPID_GSTAmount = Value
        End Set
    End Property
    Public Property PID_SGST() As Double
        Get
            Return (dPID_SGST)
        End Get
        Set(ByVal Value As Double)
            dPID_SGST = Value
        End Set
    End Property
    Public Property PID_SGSTAmount() As Double
        Get
            Return (dPID_SGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dPID_SGSTAmount = Value
        End Set
    End Property
    Public Property PID_CGST() As Double
        Get
            Return (dPID_CGST)
        End Get
        Set(ByVal Value As Double)
            dPID_CGST = Value
        End Set
    End Property
    Public Property PID_CGSTAmount() As Double
        Get
            Return (dPID_CGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dPID_CGSTAmount = Value
        End Set
    End Property
    Public Property PID_IGST() As Double
        Get
            Return (dPID_IGST)
        End Get
        Set(ByVal Value As Double)
            dPID_IGST = Value
        End Set
    End Property
    Public Property PID_IGSTAmount() As Double
        Get
            Return (dPID_IGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dPID_IGSTAmount = Value
        End Set
    End Property
    Public Property PID_FinalTotal() As Double
        Get
            Return (dPID_FinalTotal)
        End Get
        Set(ByVal Value As Double)
            dPID_FinalTotal = Value
        End Set
    End Property
    Public Property PID_ItemStatus() As String
        Get
            Return (sPID_ItemStatus)
        End Get
        Set(ByVal Value As String)
            sPID_ItemStatus = Value
        End Set
    End Property
    Public Property PID_Status() As String
        Get
            Return (sPID_Status)
        End Get
        Set(ByVal Value As String)
            sPID_Status = Value
        End Set
    End Property
    Public Property PID_CompID() As Integer
        Get
            Return (iPID_CompID)
        End Get
        Set(ByVal Value As Integer)
            iPID_CompID = Value
        End Set
    End Property
    Public Property PID_CreatedBy() As Integer
        Get
            Return (iPID_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iPID_CreatedBy = Value
        End Set
    End Property
    Public Property PID_CreatedOn() As DateTime
        Get
            Return (dPID_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dPID_CreatedOn = Value
        End Set
    End Property
    Public Property PID_Operation() As String
        Get
            Return (sPID_Operation)
        End Get
        Set(ByVal Value As String)
            sPID_Operation = Value
        End Set
    End Property
    Public Property PID_IPAddress() As String
        Get
            Return (sPID_IPAddress)
        End Get
        Set(ByVal Value As String)
            sPID_IPAddress = Value
        End Set
    End Property


    Public Property C_ID() As Integer
        Get
            Return (iC_ID)
        End Get
        Set(ByVal Value As Integer)
            iC_ID = Value
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
    Public Function GetSupplier(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal OrderID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "select CSM_ID from CustomerSupplierMaster where CSM_ID in(select POM_Supplier from Purchase_Order_Master where POM_ID= " & OrderID & " And POM_YearID =" & iYearID & " And POM_CompID =" & iCompID & ") And CSM_CompID =" & iCompID & ""
            GetSupplier = objDb.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetSupplier
        Catch ex As Exception
        End Try
    End Function
    Public Function LoadInwardNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            '---Working But Checking in Verification form---'

            'sSql = "select PRM_ID,PRM_DocumentRefNo from Purchase_Registry_master where PRM_DocumentRefNo Not In"
            'sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1 And PV_OrderNo=" & iTransactionID & ")"
            'sSql = sSql & "And PRM_OrderNo=" & iTransactionID & " And "
            'sSql = sSql & "PRM_Status ='A' and PRM_CompID=1"
            'sSql = sSql & " intersect Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_DocumentRefNo Not In"
            'sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1  And PV_OrderNo=" & iTransactionID & ")  "
            'sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
            'sSql = sSql & "PGM_CompID=" & iCompID & " and PGM_Status='A'"

            '---Working But Checking in Verification form---'

            '* working from both GIN & Purchase Register *'

            'sSql = "select PRM_ID,PRM_DocumentRefNo from Purchase_Registry_master where PRM_ID Not In"
            'sSql = sSql & "(Select PIM_PRegesterID From Purchase_Invoice_Master where PIM_CompID=" & iCompID & " And PIM_OrderID=" & iTransactionID & ")"
            'sSql = sSql & "And PRM_OrderNo=" & iTransactionID & " And "
            'sSql = sSql & "PRM_Status ='A' and PRM_CompID=" & iCompID & " "
            'sSql = sSql & " intersect Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_ID Not In"
            'sSql = sSql & "(Select PIM_PRegesterID From Purchase_Invoice_Master where PIM_CompID=" & iCompID & "  And PIM_OrderID=" & iTransactionID & ")  "
            'sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
            'sSql = sSql & "PGM_CompID=" & iCompID & " and PGM_Status='A'"

            '* working from both GIN & Purchase Register *'

            sSql = "select PRM_ID,PRM_DocumentRefNo from Purchase_Registry_master where PRM_ID Not In"
            sSql = sSql & "(Select PIM_PRegesterID From Purchase_Invoice_Master where PIM_CompID=" & iCompID & " And PIM_OrderID=" & iTransactionID & ")"
            sSql = sSql & "And PRM_OrderNo=" & iTransactionID & " And "
            sSql = sSql & "PRM_Status ='A' and PRM_CompID=" & iCompID & " "

            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOurRefNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = "", sTransactionType As String = ""
        Try
            '            sSql = "select POM_ID,POM_OrderNo from PURCHASE_ORDER_MASTER where POM_ID in(select PRM_OrderNo from purchase_registry_MASTER where 
            'PRM_DocumentRefNo not in(select PV_DocRefNo from purchase_verification)) intersect 
            'select POM_ID,POM_OrderNo from PURCHASE_ORDER_MASTER where POM_ID in(select PGM_OrderID from purchase_gin_MASTER where 
            'PGM_DocumentRefNo not in(select PV_DocRefNo from purchase_verification))"

            sSql = "select POM_ID,POM_OrderNo from PURCHASE_ORDER_MASTER where POM_BranchID=" & iBranch & " And POM_ID in(select PRM_OrderNo from purchase_registry_MASTER where 
                    PRM_DocumentRefNo not in(select PV_DocRefNo from purchase_verification)) Order By POM_ID Desc"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckVerificationNo(ByVal sNameSpace As String, ByVal Inward As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * from Purchase_verification where PV_DocRefNo='" & Inward & "' and PV_YearID =" & iyearID & " and PV_CompID =" & ICompID & " and PV_OrderNo=" & IOrderID & ""
            Return objDb.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDateFromInward(ByVal sNameSpace As String, ByVal Inward As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As DateTime
        Dim sSql As String = ""
        Try
            sSql = "select PGM_InvoiceDate from Purchase_GIN_Master where PGM_DocumentRefNo='" & Inward & "' and PGM_YearID = " & iyearID & " and PGM_CompID =" & ICompID & " and PGM_OrderID =" & IOrderID & " "
            Return objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRegisterNo(ByVal sNameSpace As String, ByVal REGDocRef As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "select * from Purchase_Registry_master where PRM_DocumentRefNo='" & REGDocRef & "' and PRM_YearID =" & iyearID & " and PRM_CompID =" & ICompID & " and PRM_Status = 'A' And PRM_OrderNo=" & IOrderID & ""
            'sSql = "Select * from Purchase_verification where PV_DocRefNo='" & Inward & "' and PV_YearID =" & iyearID & " and PV_CompID =" & ICompID & " and PV_OrderNo='" & IOrderID & "'"
            Return objDb.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckInwardNo(ByVal sNameSpace As String, ByVal REGDocRef As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "select * from Purchase_GIN_Master where PGM_DocumentRefNo='" & REGDocRef & "' and PGM_YearID =" & iyearID & " and PGM_CompID =" & ICompID & " and PGM_Status = 'A' And PGM_OrderID=" & IOrderID & ""
            'sSql = "Select * from Purchase_verification where PV_DocRefNo='" & Inward & "' and PV_YearID =" & iyearID & " and PV_CompID =" & ICompID & " and PV_OrderNo='" & IOrderID & "'"
            Return objDb.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMasterData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Purchase_GIN_Master Where PGM_DocumentRefNo='" & iMasterID & "' and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransactionDetailsPI(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt, dt1 As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim totAmount As Decimal = 0
        Try

            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")

            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("RateAmount")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("TotalAmount")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")

            dtTab.Columns.Add("SGST")
            dtTab.Columns.Add("SGSTAmount")
            dtTab.Columns.Add("CGST")
            dtTab.Columns.Add("CGSTAmount")
            dtTab.Columns.Add("IGST")
            dtTab.Columns.Add("IGSTAmount")
            dtTab.Columns.Add("FinalTotal")

            Dim dTotalCharge, dItemTotalFinalAmt As Double
            dTotalCharge = objDb.SQLGetDescription(sNameSpace, "Select C_ChargeAmount From Charges_Master Where C_POrderID=" & OrderNo & " and C_CompID =" & iCompID & "")
            dItemTotalFinalAmt = objDb.SQLGetDescription(sNameSpace, "Select POD_TotalAmount From Purchase_Order_Details Where POD_MasterID=" & OrderNo & " and POD_CompID =" & iCompID & "")

            'Working But checking in GIN Master'
            'sSql = "Select * From Purchase_Invoice_Accepted Where PIA_OrderID <> 0 And PIA_GINID In(Select PGM_ID from Purchase_GIN_Master where "
            'sSql = sSql & "PGM_DocumentRefNo ='" & iGINID & "' and PGM_OrderID =" & OrderNo & " and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & ") and PIA_CompID=" & iCompID & " and PIA_YearID =" & iYearID & ""
            'Working But checking in GIN Master'

            sSql = "Select * From Purchase_Invoice_Accepted Where PIA_OrderID <> 0 And PIA_GINID In(Select PRM_ID from Purchase_Registry_Master where "
            sSql = sSql & "PRM_DocumentRefNo ='" & iGINID & "' and PRM_OrderNo =" & OrderNo & " and PRM_YearID =" & iYearID & " and PRM_CompID =" & iCompID & ") and PIA_CompID=" & iCompID & " and PIA_YearID =" & iYearID & ""

            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If (dt.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                        dr = dtTab.NewRow
                        dr("CommodityID") = dt.Rows(i)("PIA_Commodity")
                        dr("ItemID") = dt.Rows(i)("PIA_DescriptionID")
                        dr("HistoryID") = dt.Rows(i)("PIA_HistoryID")
                        dr("UnitID") = dt.Rows(i)("PIA_UnitID")
                        dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_master Where INV_ID =" & dt.Rows(i)("PIA_DescriptionID") & " and Inv_CompID =" & iCompID & "")
                        dr("Unit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PIA_UnitID") & " and Mas_CompID =" & iCompID & "")
                        dr("Remarks") = ""
                        dr("Quantity") = dt.Rows(i)("PIA_AcceptedQnt")
                        dr("Rate") = dt.Rows(i)("PIA_MRP")
                        dr("RateAmount") = (dt.Rows(i)("PIA_AcceptedQnt") * dt.Rows(i)("PIA_MRP"))

                        dr("Discount") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Discount From Purchase_Order_Details Where POD_MasterID In(Select PRM_OrderNO From Purchase_Registry_Master Where PRM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                        'Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Discount From Purchase_Order_Details Where POD_MasterID In(Select PGM_OrderID From Purchase_GIN_Master Where PGM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                        If dr("Discount") <> "" Then
                            dr("DiscountAmount") = 0
                        Else
                            dr("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt"))) * Convert.ToDecimal(dr("Discount"))) / 100).ToString())
                        End If

                        dr("Charges") = ((dt.Rows(i)("PIA_AcceptedQnt") * dt.Rows(i)("PIA_MRP")) * dTotalCharge) / dItemTotalFinalAmt

                        dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))).ToString())

                        dr("GSTID") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GST_ID From Purchase_Order_Details Where POD_MasterID In(Select PRM_OrderNO From Purchase_Registry_Master Where PRM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                        dr("GSTRate") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GSTRate From Purchase_Order_Details Where POD_MasterID In(Select PRM_OrderNO From Purchase_Registry_Master Where PRM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                        dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("GSTRate"))) / 100).ToString())

                        dr("SGST") = 0
                        dr("SGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("SGST"))) / 100).ToString())

                        dr("CGST") = 0
                        dr("CGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("CGST"))) / 100).ToString())

                        dr("IGST") = 0
                        dr("IGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("IGST"))) / 100).ToString())

                        totAmount = String.Format("{0:0.00}", Convert.ToDecimal(((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt") + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) + dr("GSTAmount")).ToString())
                        dr("FinalTotal") = String.Format("{0:0.00}", totAmount)
                        dtTab.Rows.Add(dr)
                    End If
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransactionDetailsNewItemDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim vat As String
        Try
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Ounit")
            dtTab.Columns.Add("RUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("RejectedCozExcess")
            dtTab.Columns.Add("Flag")
            dtTab.Columns.Add("Statuss")
            sSql = "Select * From Purchase_Invoice_Excess Where PIE_GINID in(select PGM_GIN_Number from Purchase_GIN_Master where "
            sSql = sSql & "PGM_DocumentRefNo ='" & iGINID & "' and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & " and  PGM_OrderID=" & OrderNo & ") and PIE_CompID=" & iCompID & " and PIE_YearID =" & iYearID & " and PIE_Quantity<>0"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("ID") = dt.Rows(i)("PIE_ID")
                dr("Invoice_No") = dt.Rows(i)("PIE_GINID")
                dr("CommodityID") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("PIE_Description") & " and Inv_CompID =" & iCompID & "")
                dr("ItemID") = dt.Rows(i)("PIE_Description")
                dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("PIE_Description") & " and Inv_CompID =" & iCompID & "")
                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PIE_UnitID") & " and Mas_CompID =" & iCompID & "")
                dr("Pieces") = dt.Rows(i)("PIE_Quantity")
                dr("ExpectedDate") = Date.Today
                dr("Amount") = dt.Rows(i)("PIE_Rate")
                dr("ExiceDuty") = objDb.SQLGetDescription(sNameSpace, "select InvH_Excise from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")
                dr("Frieght") = "0"
                dr("Total") = "0"
                If IsDBNull(objDb.SQLGetDescription(sNameSpace, "select InvH_Vat from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")) Then
                    dr("Vat") = 0
                Else
                    dr("Vat") = objDb.SQLGetDescription(sNameSpace, "select InvH_Vat from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")
                    If (dr("Vat") = "") Then
                        dr("Vat") = 0
                    End If
                End If

                'and invH_YearID =" & iYearID & "
                'dr("CST") = DBHelper.GetDescription(sNameSpace, "select InvH_Excise from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")
                If IsDBNull(objDb.SQLGetDescription(sNameSpace, "select InvH_Excise from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")) Then
                    dr("CST") = 0
                Else
                    'and InvH_YearID =" & iYearId & "
                    dr("CST") = objDb.SQLGetDescription(sNameSpace, "select InvH_Excise from Inventory_master_history where InvH_ID=" & dt.Rows(i)("PIE_HistoryID") & " and InvH_CompID =" & iCompID & "")
                    If (dr("CST") = "") Then
                        dr("CST") = 0
                    End If
                End If
                dr("Flag") = dt.Rows(i)("PIE_Delflag")
                dr("Statuss") = "New Item"
                dr("RejectedCozExcess") = dt.Rows(i)("PIE_Quantity")
                vat = Convert.ToDecimal(((dt.Rows(i)("PIE_Rate") * dt.Rows(i)("PIE_Quantity")) * Convert.ToDecimal(dr("Vat"))) / 100)
                dr("TAXAmount") = vat
                dr("RowTotal") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PIE_Rate") * dt.Rows(i)("PIE_Quantity") + vat).ToString())
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getVerificationID(ByVal sNameSpace As String, ByVal Inward As String, ByVal iyearID As Integer, ByVal ICompID As Integer, ByVal IOrderID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select  top 1 PV_ID from  Purchase_Verification where PV_OrderNo=" & IOrderID & " and PV_DocRefNo='" & Inward & "' and PV_CompID=" & ICompID & " order by  PV_ID DESC"
            Return objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransactionDetailsDiffrence(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer) As DataTable
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Try
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("StdUnit")
            dtTab.Columns.Add("Ounit")
            dtTab.Columns.Add("RUnit")
            dtTab.Columns.Add("Pieces")
            dtTab.Columns.Add("ExpectedDate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("ExiceDuty")
            dtTab.Columns.Add("Frieght")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("Vat")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("TAXAmount")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RowTotal")
            dtTab.Columns.Add("Invoice_No")
            dtTab.Columns.Add("RejectedCozExcess")
            dtTab.Columns.Add("Flag")
            dtTab.Columns.Add("Statuss")
            sSql = "Select * From Transaction_Invoice_Details Where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & iGINID & "') and TID_CompID=" & iCompID & " and  TID_DiffFlag='df' and TID_Quantity<>0 "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("ID") = dt.Rows(i)("TID_ID")
                dr("Invoice_No") = objDb.SQLGetDescription(sNameSpace, "Select GIN_GoodInwardNo From Goods_InwardNote_Master Where GIN_ID =" & dt.Rows(i)("TID_GINRefID") & "")
                dr("CommodityID") = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_ID=" & dt.Rows(i)("TID_ItemID") & " ")
                dr("ItemID") = dt.Rows(i)("TID_ItemID")
                dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_master Where INV_ID =" & dt.Rows(i)("TID_ItemID") & "")
                dr("StdUnit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("TID_Per") & "")
                dr("Pieces") = dt.Rows(i)("TID_Quantity")
                dr("ExpectedDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("TID_ItemRequiredDate"), "D")
                dr("Amount") = dt.Rows(i)("TID_PurchaseRate")
                dr("ExiceDuty") = dt.Rows(i)("TID_ExciseDuty")
                dr("Frieght") = dt.Rows(i)("TID_Frieght")
                dr("Total") = dt.Rows(i)("TID_Total")
                If IsDBNull(dt.Rows(i)("TID_POVAT") <> "") = False Then
                    dr("Vat") = dt.Rows(i)("TID_POVAT")
                Else
                    dr("Vat") = ""
                End If
                If IsDBNull(dt.Rows(i)("TID_CST") <> "") = False Then
                    dr("CST") = dt.Rows(i)("TID_CST")
                Else
                    dr("CST") = ""
                End If
                dr("TAXAmount") = dt.Rows(i)("TID_TaxAmount")
                dr("Remarks") = dt.Rows(i)("TID_Remarks")
                dr("RowTotal") = dt.Rows(i)("TID_ItemRowTotal")
                dr("Flag") = dt.Rows(i)("TID_flag")
                dr("Statuss") = dt.Rows(i)("TID_Status")
                dr("RejectedCozExcess") = objDb.SQLExecuteScalar(sNameSpace, "Select GIND_RejectedBczExcess from Goods_InwardNote_Master_Details where GIND_ID='" & dt.Rows(i)("TID_ID") & "' And GIND_MasterID='" & dt.Rows(i)("TID_GINRefID") & "'")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer)
        Dim dt, dt1 As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim vat As Decimal
        Dim total, TotalWithCst, TotalWithVat, cst As Decimal
        Dim sSql As String = ""
        Try
            sSql = "Select * From Transaction_Invoice_Details Where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & iGINID & "') and TID_CompID=" & iCompID & " and TID_NewFlag='A' and TID_ExcessFlag='A'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1

                If IsDBNull(dt.Rows(i)("TID_POVAT")) Then
                    vat = 0
                Else
                    vat = dt.Rows(i)("TID_POVAT")
                End If
                If IsDBNull(dt.Rows(i)("TID_CST")) Then
                    cst = 0
                Else
                    cst = dt.Rows(i)("TID_CST")
                End If

                vat = Convert.ToDecimal(((dt.Rows(i)("TID_PredeterminedPrice") * dt.Rows(i)("TID_Quantity")) * Convert.ToDecimal(vat)) / 100)
                ' dr("RowTotal") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("TID_PredeterminedPrice") * dt.Rows(i)("TID_Quantity") + vat).ToString())
                Dim RegRefID As String = objDb.SQLGetDescription(sNameSpace, "select GIN_DocRefNo from Goods_InwardNote_Master where GIN_ID=" & dt.Rows(i)("TID_GINRefID") & "")
                If (RegRefID = "") Then
                    RegRefID = 0
                End If
                Dim rateInReg As String = objDb.SQLGetDescription(sNameSpace, "select PRD_MRP from purchase_register_details where PRD_MasterID in(select PR_ID from purchase_register where PR_DocRefNo='" & RegRefID & "') and PRD_ItemID=" & dt.Rows(i)("TID_ItemID") & "")
                If (rateInReg = "") Then
                    rateInReg = 0
                Else
                    If (Convert.ToDecimal(rateInReg) > Convert.ToDecimal(dt.Rows(i)("TID_PredeterminedPrice"))) Then
                        'UpdateInvoiceDetails(sNameSpace, RegRefID, dt.Rows(i)("TID_ItemID"))
                        sSql = "select * from purchase_register_details where PRD_MasterID in(select PR_ID from purchase_register where PR_DocRefNo='" & RegRefID & "') and PRD_ItemID=" & dt.Rows(i)("TID_ItemID") & ""
                        dt1 = objDb.SQLExecuteDataTable(sNameSpace, sSql)
                        total = dt1.Rows(0)("PRD_MRP") * dt.Rows(i)("TID_Quantity")
                        If IsDBNull(dt.Rows(i)("TID_CST")) Then
                            TotalWithCst = total
                        Else
                            TotalWithCst = total + dt.Rows(i)("TID_CST")
                        End If
                        vat = Convert.ToDecimal(((dt1.Rows(0)("PRD_MRP") * dt.Rows(i)("TID_Quantity")) * Convert.ToDecimal(vat)) / 100)
                        TotalWithVat = TotalWithCst + vat
                        sSql = "update Transaction_Invoice_Details set TID_PurchaseRate=" & total & ",TID_PredeterminedPrice=" & dt1.Rows(0)("PRD_MRP") & ",TID_Total=" & TotalWithCst & ",TID_ItemRowTotal=" & TotalWithVat & ",TID_DiffFlag='df'  where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "')  And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & ""
                        objDb.SQLExecuteNonQuery(sNameSpace, sSql)

                        'DBHelper.ExecuteNoNQuery(sNameSpace, "update Transaction_Invoice_Details set TID_PurchaseRate=" & total & ",TID_PredeterminedPrice=" & dt.Rows(0)("PRD_MRP") & ",TID_Total=" & TotalWithCst & ",TID_ItemRowTotal=" & TotalWithVat & ",TID_DiffFlag='W'  where TID_GINRefID in(select GIND_ID from Goods_InwardNote_Master_details where GIND_MasterID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "') and GIND_ItemID=" & dt.Rows(i)("TID_ItemID") & ") And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & "")
                    Else
                        sSql = "update Transaction_Invoice_Details set TID_DiffFlag='A'  where TID_GINRefID in(select GIN_ID from Goods_InwardNote_Master where GIN_DocRefNo='" & RegRefID & "')  And TID_ItemID = " & dt.Rows(i)("TID_ItemID") & ""
                        objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                End If

            Next

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveInvoiceMaster(ByVal sNameSpace As String, ByVal objInvoiceForm As ClsInvoiceForm) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(31) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_No", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_No
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_PRegesterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_PRegesterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_OrderDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_SupplierID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_SupplierID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_InvoiceDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_InvoiceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_CompanyAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objInvoiceForm.sPIM_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objInvoiceForm.sPIM_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_BillingAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objInvoiceForm.sPIM_BillingAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_BillingGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objInvoiceForm.sPIM_BillingGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_DeliveryFrom", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objInvoiceForm.sPIM_DeliveryFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_DeliveryFromGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objInvoiceForm.sPIM_DeliveryFromGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_ReceiveAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objInvoiceForm.sPIM_ReceiveAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_ReceiveGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objInvoiceForm.sPIM_ReceiveGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_InvoiceStatus ", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_InvoiceStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_ManualBillAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_ManualBillAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_ManualGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_ManualGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_BillDifferenceStatus", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_BillDifferenceStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_IPADdress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIM_State", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInvoiceForm.PIM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spPurchase_Invoice_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAcceptedDetails(ByVal sNameSpace As String, ByVal objInvoiceForm As ClsInvoiceForm) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_CommodityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_DescID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_Remarks", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_Rate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_Quantity", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_ChargePerItem", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_ChargePerItem
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_RateAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_RateAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_Discount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_Discount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_DiscountAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_DiscountAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_Amount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_GSTID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_GSTID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_GSTRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_GSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_GSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_SGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_SGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_SGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_SGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_CGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_CGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_CGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_CGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_IGST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_IGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_IGSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_IGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_FinalTotal", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_FinalTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_ItemStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_ItemStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objInvoiceForm.PID_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_URD_GSTRate", OleDb.OleDbType.Double, 4)
            'ObjParam(iParamCount).Value = objInvoiceForm.PID_URD_GSTRate
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_URD_GSTAmt", OleDb.OleDbType.Double, 4)
            'ObjParam(iParamCount).Value = objInvoiceForm.PID_URD_GSTAmt
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_URD_SGST", OleDb.OleDbType.Double, 4)
            'ObjParam(iParamCount).Value = objInvoiceForm.PID_URD_SGST
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_URD_SGSTAmt", OleDb.OleDbType.Double, 4)
            'ObjParam(iParamCount).Value = objInvoiceForm.PID_URD_SGSTAmt
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_URD_CGST", OleDb.OleDbType.Double, 4)
            'ObjParam(iParamCount).Value = objInvoiceForm.PID_URD_CGST
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@PID_URD_CGSTAmt", OleDb.OleDbType.Double, 4)
            'ObjParam(iParamCount).Value = objInvoiceForm.PID_URD_CGSTAmt
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "spPI_Accepted_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDiscount(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=19 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemsTotalFromPurchaseRegister(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPRID As Integer) As Double
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iPRID > 0 Then
                sSql = "Select * From Purchase_Registry_Details Where PRD_MasterID=" & iPRID & " And PRD_CompID =" & iCompID & " "
                dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                For i = 0 To dt.Rows.Count - 1
                    GetItemsTotalFromPurchaseRegister = GetItemsTotalFromPurchaseRegister + (dt.Rows(i)("PRD_AccptedQty") * dt.Rows(i)("PRD_MRP"))
                Next
            End If
            Return GetItemsTotalFromPurchaseRegister
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Delete From Charges_Master Where C_POrderID=" & iOrderID & " And C_PInvoiceDocRef=" & iInvoiceID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCharges(ByVal sNameSpace As String, ByVal objInvoiceForm As ClsInvoiceForm)
        Dim iMaxID As Integer
        Dim sSql As String = ""
        Try
            iMaxID = objDb.SQLExecuteScalar(sNameSpace, "select isnull(max(C_ID)+1,1) from Charges_master")
            sSql = "Insert Into Charges_master(C_ID, C_POrderID, C_PGinID, C_PInvoiceDocRef, C_OrderType, C_ChargeID, C_ChargeType, C_ChargeAmount, C_PSType, C_DelFlag, C_Status, C_CompID, C_YearID, C_CreatedBy, C_CreatedOn, C_Operation,C_IPAddress) "
            sSql = sSql & " Values(" & iMaxID & ", " & objInvoiceForm.C_POrderID & ", " & objInvoiceForm.C_PGinID & ", " & objInvoiceForm.C_PInvoiceDocRef & ", '" & objInvoiceForm.C_OrderType & "', " & objInvoiceForm.C_ChargeID & ", '" & objInvoiceForm.C_ChargeType & "', " & objInvoiceForm.C_ChargeAmount & ", '" & objInvoiceForm.C_PSType & "', '" & objInvoiceForm.C_DelFlag & "', '" & objInvoiceForm.C_Status & "', " & objInvoiceForm.C_CompID & ", " & objInvoiceForm.C_YearID & ", " & objInvoiceForm.C_CreatedBy & ", GetDate(), '" & objInvoiceForm.C_Operation & "','" & objInvoiceForm.C_IPAddress & "')"
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
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
    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " and CSM_Delflag='A' order by CSM_Name"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChargeType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=24 And Mas_DelFlag='A' "
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getGSTRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String, ByVal sGSTNCategoryDesc As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GC_GSTRate From GSTCategory_Table Where GC_CompanyType='" & sCompanyType & "' And GC_GSTcategory= '" & sGSTNCategoryDesc & "' Order By GC_ID Desc  "
            getGSTRate = objDb.SQLGetDescription(sNameSpace, sSql)
            Return getGSTRate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTIDFromHSNTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_ID From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTIDFromHSNTable = objDb.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTIDFromHSNTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTRateFromHSNTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Double
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GST_GSTRate From GST_Rates Where GST_ItemID=" & iItemID & " And GST_CommodityID=" & iCommodityID & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc "
            GetGSTRateFromHSNTable = objDb.SQLGetDescription(sNameSpace, sSql)
            Return GetGSTRateFromHSNTable
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
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from Acc_General_Master where Mas_Master in(Select Mas_ID From Acc_Master_Type Where Mas_Type='Company Type') And Mas_Delflag='A' and Mas_CompID =" & iCompID & " "
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckStateCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGSTNRegNo As String) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Dim bCheck As Boolean
        Dim sCode As String = ""
        Try
            sCode = sGSTNRegNo.Substring(0, 2)

            sSql = "Select * From GSTN_RegNo_Master Where GR_TIN='" & sCode & "' And GR_CompID=" & iCompID & " "
            bCheck = objDb.SQLCheckForRecord(sNameSpace, sSql).ToString
            Return bCheck
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyGSTNRegNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From MST_Customer_Master Where CUST_ID=" & iCompID & " "
            GetCompanyGSTNRegNo = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCompanyGSTNRegNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplierID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From CustomerSupplierMaster Where CSM_ID=" & iSupplierID & " And CSM_CompID=" & iCompID & " "
            GetSupplierDetails = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetSupplierDetails
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
            sMaximumID = objDb.SQLGetDescription(sNameSpace, "Select Top 1 PIM_ID From Purchase_Invoice_Master Order By PIM_ID Desc")
            sYear = objDb.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDb.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDb.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
    Public Function LoadExistingInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select PIM_ID,PIM_No from Purchase_Invoice_Master where PIM_CompID =" & iCompID & " "
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExistingDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPIMID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Purchase_Invoice_Master Where PIM_ID=" & iPIMID & " And PIM_CompID=" & iCompID & ""
            GetExistingDate = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetExistingDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ApproveInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPIMID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_Invoice_Master Set PIM_Status='A' Where PIM_ID=" & iPIMID & " And PIM_CompID=" & iCompID & " And PIM_YearID=" & iYearID & " "
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckApprove(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPIMID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select PIM_Status From Purchase_Invoice_Master Where PIM_ID=" & iPIMID & " And PIM_CompID=" & iCompID & " And PIM_YearID=" & iYearID & " "
            CheckApprove = objDb.SQLGetDescription(sNameSpace, sSql)
            Return CheckApprove
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindInvoiceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("Commodity")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("RateAmount")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("TotalAmount")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")
            dtTab.Columns.Add("FinalTotal")

            If iMasterID > 0 Then
                sSql = "" : sSql = "Select * From PI_Accepted_Details Where PID_MasterID=" & iMasterID & " And PID_CompID=" & iCompID & " "
                dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If

            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("PID_CommodityID")
                    dRow("ItemID") = dt.Rows(i)("PID_DescID")
                    dRow("HistoryID") = dt.Rows(i)("PID_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("PID_UnitID")
                    dRow("Commodity") = objDb.SQLGetDescription(sNameSpace, "Select INV_Description From  Inventory_Master Where INV_ID=" & dt.Rows(i)("PID_CommodityID") & " And INv_CompID = " & iCompID & " And INV_Parent=0 ")
                    dRow("Goods") = objDb.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("PID_DescID") & " And INV_Parent=" & dt.Rows(i)("PID_CommodityID") & " And INv_CompID = " & iCompID & "")
                    dRow("Unit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("PID_UnitID") & " And Mas_CompID =" & iCompID & "")
                    dRow("Remarks") = ""
                    dRow("Quantity") = dt.Rows(i)("PID_Quantity")
                    dRow("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_Rate")))
                    dRow("RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_RateAmount")))

                    dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_Discount")))
                    dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_DiscountAmount")))
                    dRow("Charges") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_ChargePerItem")))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_Amount")))

                    dRow("GSTID") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_GSTID")))
                    dRow("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_GSTRate")))
                    dRow("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_GSTAmount")))

                    dRow("FinalTotal") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_FinalTotal")))

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab

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

            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ChargeID") = dt.Rows(i)("C_ChargeID")
                    dRow("ChargeType") = objDb.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt.Rows(i)("C_ChargeID") & " And Mas_Master=24 And Mas_CompID = " & iCompID & "  ")
                    dRow("ChargeAmount") = dt.Rows(i)("C_ChargeAmount")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindInvoiceROWData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer, ByVal iRegisterID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iInvoiceID > 0 Then
                sSql = "" : sSql = "Select * From PI_Accepted_Details Where PID_MasterID In(Select PIM_ID From Purchase_Invoice_Master Where "
                sSql = sSql & "PIM_ID =" & iInvoiceID & " And PIM_OrderID=" & iOrderID & " And PIM_PRegesterID=" & iRegisterID & " And PIM_CompID =" & iCompID & " And PIM_YearID =" & iYearID & ") And PID_CommodityID=" & iCommodityID & " And PID_DescID=" & iItemID & " And PID_HistoryID=" & iHistoryID & " And PID_CompID=" & iCompID & " "
            End If
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sInvoice As String) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * From Purchase_Invoice_Master Where PIM_No='" & sInvoice & "' And PIM_CompID=" & iCompID & " "
            CheckInvoice = objDb.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckInvoice
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBreakUpDetailsPI(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGINID As String, ByVal iCommodity As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt, dt1 As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim sSql As String = ""
        Dim totAmount As Decimal = 0
        Try

            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")

            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("RateAmount")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("TotalAmount")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")

            dtTab.Columns.Add("SGST")
            dtTab.Columns.Add("SGSTAmount")
            dtTab.Columns.Add("CGST")
            dtTab.Columns.Add("CGSTAmount")
            dtTab.Columns.Add("IGST")
            dtTab.Columns.Add("IGSTAmount")
            dtTab.Columns.Add("FinalTotal")

            sSql = "Select * From Purchase_Invoice_Accepted Where PIA_OrderID <> 0 And PIA_GINID In(Select PGM_ID from Purchase_GIN_Master where "
            sSql = sSql & "PGM_DocumentRefNo ='" & iGINID & "' and PGM_OrderID =" & OrderNo & " and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & ") and PIA_CompID=" & iCompID & " and PIA_YearID =" & iYearID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If (dt.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                        dr = dtTab.NewRow
                        dr("CommodityID") = dt.Rows(i)("PIA_Commodity")
                        dr("ItemID") = dt.Rows(i)("PIA_DescriptionID")
                        dr("HistoryID") = dt.Rows(i)("PIA_HistoryID")
                        dr("UnitID") = dt.Rows(i)("PIA_UnitID")
                        dr("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_master Where INV_ID =" & dt.Rows(i)("PIA_DescriptionID") & " and Inv_CompID =" & iCompID & "")
                        dr("Unit") = objDb.SQLGetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("PIA_UnitID") & " and Mas_CompID =" & iCompID & "")
                        dr("Remarks") = ""
                        dr("Quantity") = dt.Rows(i)("PIA_AcceptedQnt")
                        dr("Rate") = dt.Rows(i)("PIA_MRP")
                        dr("Charges") = 0
                        'Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Frieght From Purchase_Order_Details Where POD_MasterID In(Select PGM_OrderID From Purchase_GIN_Master Where PGM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))

                        dr("RateAmount") = ((dt.Rows(i)("PIA_AcceptedQnt") * dt.Rows(i)("PIA_MRP")) + Convert.ToDecimal(dr("Charges")))
                        dr("Discount") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_Discount From Purchase_Order_Details Where POD_MasterID In(Select PGM_OrderID From Purchase_GIN_Master Where PGM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                        dr("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt") + Convert.ToDecimal(dr("Charges")))) * Convert.ToDecimal(dr("Discount"))) / 100).ToString())

                        dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) - Convert.ToDecimal(dr("DiscountAmount"))).ToString())

                        dr("GSTID") = Trim(objDb.SQLGetDescription(sNameSpace, "Select POD_GST_ID From Purchase_Order_Details Where POD_MasterID In(Select PGM_OrderID From Purchase_GIN_Master Where PGM_ID=" & dt.Rows(i)("PIA_GINID") & ") and POD_HistoryID =" & dt.Rows(i)("PIA_HistoryID") & " and POD_CompID =" & iCompID & ""))
                        dr("GSTRate") = 0
                        dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("GSTRate"))) / 100).ToString())

                        dr("SGST") = 0
                        dr("SGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("SGST"))) / 100).ToString())

                        dr("CGST") = 0
                        dr("CGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("CGST"))) / 100).ToString())

                        dr("IGST") = 0
                        dr("IGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt")) + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) * Convert.ToDecimal(dr("IGST"))) / 100).ToString())

                        totAmount = String.Format("{0:0.00}", Convert.ToDecimal(((dt.Rows(i)("PIA_MRP") * dt.Rows(i)("PIA_AcceptedQnt") + Convert.ToDecimal(dr("Charges"))) - Convert.ToDecimal(dr("DiscountAmount"))) + dr("GSTAmount")).ToString())
                        dr("FinalTotal") = String.Format("{0:0.00}", totAmount)
                        dtTab.Rows.Add(dr)
                    End If
                Next
            End If

            dr = dtTab.NewRow
            dr("CommodityID") = ""
            dr("ItemID") = ""
            dr("HistoryID") = ""
            dr("UnitID") = ""
            dr("Goods") = "Total"
            dr("Unit") = ""
            dr("Remarks") = ""
            dr("Quantity") = ""
            dr("Rate") = ""
            dr("Charges") = ""
            dr("RateAmount") = ""
            dr("Discount") = ""
            dr("DiscountAmount") = ""
            dr("TotalAmount") = ""
            dr("GSTID") = ""
            dr("GSTRate") = ""
            dr("GSTAmount") = ""
            dr("SGST") = ""
            dr("SGSTAmount") = ""
            dr("CGST") = ""
            dr("CGSTAmount") = ""
            dr("IGST") = ""
            dr("IGSTAmount") = ""
            dr("FinalTotal") = ""
            dtTab.Rows.Add(dr)

            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function OrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Purchase_Order_Master Where POM_ID=" & OrderNo & " And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDiscountFromInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iRegisterID As Integer, ByVal iInvoiceID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc In (Select PID_Discount From PI_Accepted_Details Where PID_MasterID In (Select PIM_ID From Purchase_Invoice_Master Where PIM_ID=" & iInvoiceID & " And PIM_OrderID=" & iOrderID & " And PIM_PRegesterID =" & iRegisterID & " And PIM_YearID=" & iYearID & " And PIM_CompID=" & iCompID & " )) "
            sSql = "Select PID_Discount From PI_Accepted_Details Where PID_CommodityID=" & iCommodityID & " And PID_DescID=" & iItemID & " And PID_MasterID In (Select PIM_ID From Purchase_Invoice_Master Where PIM_ID=" & iInvoiceID & " And PIM_OrderID=" & iOrderID & " And PIM_PRegesterID =" & iRegisterID & " And PIM_YearID=" & iYearID & " And PIM_CompID=" & iCompID & " ) "
            GetDiscountFromInvoice = objDb.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDiscountFromInvoice
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDiscountFromPOD(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            'sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc In (Select POD_Discount From Purchase_Order_Details Where POD_MasterID In (Select POM_ID From Purchase_Order_Master Where POM_OrderID=" & iOrderID & " And POM_YearID=" & iYearID & " And POM_CompID=" & iCompID & " )) "
            sSql = "Select POD_Discount From Purchase_Order_Details Where POD_Commodity=" & iCommodityID & " And POD_DescriptionID=" & iItemID & " And POD_MasterID In (Select POM_ID From Purchase_Order_Master Where POM_ID=" & iOrderID & " And POM_YearID=" & iYearID & " And POM_CompID=" & iCompID & " ) "
            GetDiscountFromPOD = objDb.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDiscountFromPOD
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DashBoardLoadInwardNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            '---Working But Checking in Verification form---'

            'sSql = "select PRM_ID,PRM_DocumentRefNo from Purchase_Registry_master where PRM_DocumentRefNo Not In"
            'sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1 And PV_OrderNo=" & iTransactionID & ")"
            'sSql = sSql & "And PRM_OrderNo=" & iTransactionID & " And "
            'sSql = sSql & "PRM_Status ='A' and PRM_CompID=1"
            'sSql = sSql & " intersect Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_DocumentRefNo Not In"
            'sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1  And PV_OrderNo=" & iTransactionID & ")  "
            'sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
            'sSql = sSql & "PGM_CompID=" & iCompID & " and PGM_Status='A'"

            '---Working But Checking in Verification form---'

            '* Working From GIN & Purchase Register *'

            'sSql = "select PRM_ID,PRM_DocumentRefNo from Purchase_Registry_master where PRM_ID In"
            'sSql = sSql & "(Select PIM_PRegesterID From Purchase_Invoice_Master where PIM_CompID=" & iCompID & " And PIM_OrderID=" & iTransactionID & ")"
            'sSql = sSql & "And PRM_OrderNo=" & iTransactionID & " And "
            'sSql = sSql & "PRM_Status ='A' and PRM_CompID=" & iCompID & " "
            'sSql = sSql & " intersect Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_ID In"
            'sSql = sSql & "(Select PIM_PRegesterID From Purchase_Invoice_Master where PIM_CompID=" & iCompID & "  And PIM_OrderID=" & iTransactionID & ")  "
            'sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
            'sSql = sSql & "PGM_CompID=" & iCompID & " and PGM_Status='A'"

            '* Working From GIN & Purchase Register *'

            sSql = "select PRM_ID,PRM_DocumentRefNo from Purchase_Registry_master where PRM_ID In"
            sSql = sSql & "(Select PIM_PRegesterID From Purchase_Invoice_Master where PIM_CompID=" & iCompID & " And PIM_OrderID=" & iTransactionID & ")"
            sSql = sSql & "And PRM_OrderNo=" & iTransactionID & " And "
            sSql = sSql & "PRM_Status ='A' and PRM_CompID=" & iCompID & " "

            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGSTNRegNo As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select GR_StateName From GSTN_RegNo_Master Where GR_TIN='" & sGSTNRegNo & "' And GR_CompID=" & iCompID & " "
            GetState = objDb.SQLGetDescription(sNameSpace, sSql).ToString
            Return GetState
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SElect GL_ID From Chart_Of_Accounts Where GL_Desc Like '%" & sDesc & "%' And GL_CompID=" & iCompID & " "
            GetGLID = objDb.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetGLID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindGSTRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & iCompID & " "
            BindGSTRates = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindGSTRates
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getBranchFromPO(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPodID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select POM_BranchID from  purchase_order_master where POM_ID=" & iPodID & " and POM_CompID=" & iCompID & ""
            getBranchFromPO = objDb.SQLExecuteScalar(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDetailsofBranchState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPodID As Integer) As String
        Dim sSql As String = ""
        Dim iPOMBranchID As Integer : Dim iCompBrnchID As Integer
        Try
            sSql = "Select POM_BranchID from  purchase_order_master where POM_ID=" & iPodID & " and POM_CompID=" & iCompID & ""
            iPOMBranchID = objDb.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select CUSTB_STATE from MST_CUSTOMER_MASTER_Branch where CUSTB_Name='" & iPOMBranchID & "' and CUSTB_CompID=" & iCompID & ""
            iCompBrnchID = objDb.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select Mas_desc From ACC_General_Master Where Mas_Id = " & iCompBrnchID & " And Mas_master=4 and Mas_CompID = " & iCompID & ""
            CheckDetailsofBranchState = objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDetailsofCompState(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = ""
        Dim iStateID As Integer
        Try
            sSql = "Select CUST_COMM_STATE from MST_Customer_Master where CUST_ID = " & iCompID & " and CUST_CompID =" & iCompID & " "
            iStateID = objDb.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "Select Mas_desc From ACC_General_Master Where Mas_Id = " & iStateID & " And Mas_master=4 and Mas_CompID = " & iCompID & ""
            CheckDetailsofCompState = objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
