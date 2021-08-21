Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSR
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iSales_Return_ID As Integer
    Private iSales_Return_Year As Integer
    Private sSales_Return_ReturnNo As String
    Private dSales_Return_RetrunDate As DateTime
    Private iSales_Return_InvoiceNo As Integer
    Private dSales_Return_InvoiceDate As DateTime
    Private iSales_Return_OrderNo As Integer
    Private iSales_Return_DispatchNo As Integer
    Private iSales_Return_Customer As Integer
    Private sSales_Return_ShipTo As String
    Private iSales_Return_CreatedBy As Integer
    Private iSales_Return_UpdatedBy As Integer
    Private sSales_Return_Status As String
    Private sSales_Return_DelFlag As String
    Private sSales_Return_Operation As String
    Private sSales_Return_IPAddress As String
    Private iSales_Return_CompID As Integer
    Private sSales_Return_DispatchStatus As String
    Private sSales_Return_State As String
    Private sSales_Return_GoodsReturnNo As String

    Private sSales_Return_Order As String
    Private sSales_Return_Dispatch As String
    Private sSales_Return_Invoice As String
    Private iSales_Return_BatchNo As Integer
    Private iSales_Return_BaseName As Integer

    Private iSRD_ID As Integer
    Private iSRD_MasterID As Integer
    Private iSRD_Commodity As Integer
    Private iSRD_Item As Integer
    Private iSRD_UnitID As Integer
    Private iSRD_HistoryID As Integer
    Private dSRD_Rate As Double
    Private dSRD_Quantity As Double
    Private dSRD_RateAmount As Double
    Private dSRD_Discount As Double
    Private dSRD_DiscountAmount As Double
    Private dSRD_TotalAmount As Double
    Private sSRD_Status As String
    Private sSRD_Operation As String
    Private dSRD_Amount As Double
    Private iSRD_Reason As Integer
    Private dSRD_Charges As Double
    Private iSRD_GST_ID As Integer
    Private dSRD_GSTRate As Double
    Private dSRD_GSTAmount As Double
    Private dSRD_SGST As Double
    Private dSRD_SGSTAmount As Double
    Private dSRD_CGST As Double
    Private dSRD_CGSTAmount As Double
    Private dSRD_IGST As Double
    Private dSRD_IGSTAmount As Double
    Private sSRD_Remarks As String
    Private sSRD_IPAddress As String
    Private iSRD_CompID As Integer


    Private iC_ID As Integer
    Private iC_OrderID As Integer
    Private iC_AllocatedID As Integer
    Private iC_DispatchID As Integer
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
    Private iC_SalesReturnID As Integer
    Private iC_GoodsReturnID As Integer


    Public Property Sales_Return_Order() As String
        Get
            Return (sSales_Return_Order)
        End Get
        Set(ByVal Value As String)
            sSales_Return_Order = Value
        End Set
    End Property
    Public Property Sales_Return_Dispatch() As String
        Get
            Return (sSales_Return_Dispatch)
        End Get
        Set(ByVal Value As String)
            sSales_Return_Dispatch = Value
        End Set
    End Property
    Public Property Sales_Return_Invoice() As String
        Get
            Return (sSales_Return_Invoice)
        End Get
        Set(ByVal Value As String)
            sSales_Return_Invoice = Value
        End Set
    End Property
    Public Property Sales_Return_BatchNo() As Integer
        Get
            Return (iSales_Return_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_BatchNo = Value
        End Set
    End Property
    Public Property Sales_Return_BaseName() As Integer
        Get
            Return (iSales_Return_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_BaseName = Value
        End Set
    End Property


    Public Property C_SalesReturnID() As Integer
        Get
            Return (iC_SalesReturnID)
        End Get
        Set(ByVal Value As Integer)
            iC_SalesReturnID = Value
        End Set
    End Property
    Public Property C_GoodsReturnID() As Integer
        Get
            Return (iC_GoodsReturnID)
        End Get
        Set(ByVal Value As Integer)
            iC_GoodsReturnID = Value
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
    Public Property C_OrderID() As Integer
        Get
            Return (iC_OrderID)
        End Get
        Set(ByVal Value As Integer)
            iC_OrderID = Value
        End Set
    End Property
    Public Property C_AllocatedID() As Integer
        Get
            Return (iC_AllocatedID)
        End Get
        Set(ByVal Value As Integer)
            iC_AllocatedID = Value
        End Set
    End Property
    Public Property C_DispatchID() As Integer
        Get
            Return (iC_DispatchID)
        End Get
        Set(ByVal Value As Integer)
            iC_DispatchID = Value
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

    Public Property Sales_Return_ID() As Integer
        Get
            Return (iSales_Return_ID)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_ID = Value
        End Set
    End Property
    Public Property Sales_Return_Year() As Integer
        Get
            Return (iSales_Return_Year)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_Year = Value
        End Set
    End Property
    Public Property Sales_Return_ReturnNo() As String
        Get
            Return (sSales_Return_ReturnNo)
        End Get
        Set(ByVal Value As String)
            sSales_Return_ReturnNo = Value
        End Set
    End Property
    Public Property Sales_Return_RetrunDate() As Date
        Get
            Return (dSales_Return_RetrunDate)
        End Get
        Set(ByVal Value As Date)
            dSales_Return_RetrunDate = Value
        End Set
    End Property
    Public Property Sales_Return_InvoiceNo() As Integer
        Get
            Return (iSales_Return_InvoiceNo)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_InvoiceNo = Value
        End Set
    End Property
    Public Property Sales_Return_InvoiceDate() As Date
        Get
            Return (dSales_Return_InvoiceDate)
        End Get
        Set(ByVal Value As Date)
            dSales_Return_InvoiceDate = Value
        End Set
    End Property
    Public Property Sales_Return_OrderNo() As Integer
        Get
            Return (iSales_Return_OrderNo)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_OrderNo = Value
        End Set
    End Property
    Public Property Sales_Return_DispatchNo() As Integer
        Get
            Return (iSales_Return_DispatchNo)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_DispatchNo = Value
        End Set
    End Property
    Public Property Sales_Return_Customer() As Integer
        Get
            Return (iSales_Return_Customer)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_Customer = Value
        End Set
    End Property
    Public Property Sales_Return_ShipTo() As String
        Get
            Return (sSales_Return_ShipTo)
        End Get
        Set(ByVal Value As String)
            sSales_Return_ShipTo = Value
        End Set
    End Property
    Public Property Sales_Return_CreatedBy() As Integer
        Get
            Return (iSales_Return_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_CreatedBy = Value
        End Set
    End Property
    Public Property Sales_Return_UpdatedBy() As Integer
        Get
            Return (iSales_Return_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_UpdatedBy = Value
        End Set
    End Property
    Public Property Sales_Return_Status() As String
        Get
            Return (sSales_Return_Status)
        End Get
        Set(ByVal Value As String)
            sSales_Return_Status = Value
        End Set
    End Property
    Public Property Sales_Return_DelFlag() As String
        Get
            Return (sSales_Return_DelFlag)
        End Get
        Set(ByVal Value As String)
            sSales_Return_DelFlag = Value
        End Set
    End Property
    Public Property Sales_Return_Operation() As String
        Get
            Return (sSales_Return_Operation)
        End Get
        Set(ByVal Value As String)
            sSales_Return_Operation = Value
        End Set
    End Property
    Public Property Sales_Return_IPAddress() As String
        Get
            Return (sSales_Return_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSales_Return_IPAddress = Value
        End Set
    End Property
    Public Property Sales_Return_CompID() As Integer
        Get
            Return (iSales_Return_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSales_Return_CompID = Value
        End Set
    End Property
    Public Property Sales_Return_DispatchStatus() As String
        Get
            Return (sSales_Return_DispatchStatus)
        End Get
        Set(ByVal Value As String)
            sSales_Return_DispatchStatus = Value
        End Set
    End Property
    Public Property Sales_Return_State() As String
        Get
            Return (sSales_Return_State)
        End Get
        Set(ByVal Value As String)
            sSales_Return_State = Value
        End Set
    End Property
    Public Property Sales_Return_GoodsReturnNo() As String
        Get
            Return (sSales_Return_GoodsReturnNo)
        End Get
        Set(ByVal Value As String)
            sSales_Return_GoodsReturnNo = Value
        End Set
    End Property

    Public Property SRD_ID() As Integer
        Get
            Return (iSRD_ID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_ID = Value
        End Set
    End Property
    Public Property SRD_MasterID() As Integer
        Get
            Return (iSRD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_MasterID = Value
        End Set
    End Property
    Public Property SRD_Commodity() As Integer
        Get
            Return (iSRD_Commodity)
        End Get
        Set(ByVal Value As Integer)
            iSRD_Commodity = Value
        End Set
    End Property
    Public Property SRD_Item() As Integer
        Get
            Return (iSRD_Item)
        End Get
        Set(ByVal Value As Integer)
            iSRD_Item = Value
        End Set
    End Property
    Public Property SRD_UnitID() As Integer
        Get
            Return (iSRD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_UnitID = Value
        End Set
    End Property
    Public Property SRD_HistoryID() As Integer
        Get
            Return (iSRD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_HistoryID = Value
        End Set
    End Property
    Public Property SRD_Rate() As Double
        Get
            Return (dSRD_Rate)
        End Get
        Set(ByVal Value As Double)
            dSRD_Rate = Value
        End Set
    End Property
    Public Property SRD_Quantity() As Double
        Get
            Return (dSRD_Quantity)
        End Get
        Set(ByVal Value As Double)
            dSRD_Quantity = Value
        End Set
    End Property
    Public Property SRD_RateAmount() As Double
        Get
            Return (dSRD_RateAmount)
        End Get
        Set(ByVal Value As Double)
            dSRD_RateAmount = Value
        End Set
    End Property
    Public Property SRD_Discount() As Double
        Get
            Return (dSRD_Discount)
        End Get
        Set(ByVal Value As Double)
            dSRD_Discount = Value
        End Set
    End Property
    Public Property SRD_DiscountAmount() As Double
        Get
            Return (dSRD_DiscountAmount)
        End Get
        Set(ByVal Value As Double)
            dSRD_DiscountAmount = Value
        End Set
    End Property
    Public Property SRD_TotalAmount() As Double
        Get
            Return (dSRD_TotalAmount)
        End Get
        Set(ByVal Value As Double)
            dSRD_TotalAmount = Value
        End Set
    End Property
    Public Property SRD_Status() As String
        Get
            Return (sSRD_Status)
        End Get
        Set(ByVal Value As String)
            sSRD_Status = Value
        End Set
    End Property
    Public Property SRD_Operation() As String
        Get
            Return (sSRD_Operation)
        End Get
        Set(ByVal Value As String)
            sSRD_Operation = Value
        End Set
    End Property
    Public Property SRD_Amount() As Double
        Get
            Return (dSRD_Amount)
        End Get
        Set(ByVal Value As Double)
            dSRD_Amount = Value
        End Set
    End Property
    Public Property SRD_Reason() As Integer
        Get
            Return (iSRD_Reason)
        End Get
        Set(ByVal Value As Integer)
            iSRD_Reason = Value
        End Set
    End Property
    Public Property SRD_Charges() As Double
        Get
            Return (dSRD_Charges)
        End Get
        Set(ByVal Value As Double)
            dSRD_Charges = Value
        End Set
    End Property
    Public Property SRD_GST_ID() As Integer
        Get
            Return (iSRD_GST_ID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_GST_ID = Value
        End Set
    End Property
    Public Property SRD_GSTRate() As Double
        Get
            Return (dSRD_GSTRate)
        End Get
        Set(ByVal Value As Double)
            dSRD_GSTRate = Value
        End Set
    End Property
    Public Property SRD_GSTAmount() As Double
        Get
            Return (dSRD_GSTAmount)
        End Get
        Set(ByVal Value As Double)
            dSRD_GSTAmount = Value
        End Set
    End Property
    Public Property SRD_SGST() As Double
        Get
            Return (dSRD_SGST)
        End Get
        Set(ByVal Value As Double)
            dSRD_SGST = Value
        End Set
    End Property
    Public Property SRD_SGSTAmount() As Double
        Get
            Return (dSRD_SGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dSRD_SGSTAmount = Value
        End Set
    End Property
    Public Property SRD_CGST() As Double
        Get
            Return (dSRD_CGST)
        End Get
        Set(ByVal Value As Double)
            dSRD_CGST = Value
        End Set
    End Property
    Public Property SRD_CGSTAmount() As Double
        Get
            Return (dSRD_CGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dSRD_CGSTAmount = Value
        End Set
    End Property
    Public Property SRD_IGST() As Double
        Get
            Return (dSRD_IGST)
        End Get
        Set(ByVal Value As Double)
            dSRD_IGST = Value
        End Set
    End Property
    Public Property SRD_IGSTAmount() As Double
        Get
            Return (dSRD_IGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dSRD_IGSTAmount = Value
        End Set
    End Property
    Public Property SRD_Remarks() As String
        Get
            Return (sSRD_Remarks)
        End Get
        Set(ByVal Value As String)
            sSRD_Remarks = Value
        End Set
    End Property

    Public Property SRD_IPAddress() As String
        Get
            Return (sSRD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSRD_IPAddress = Value
        End Set
    End Property
    Public Property SRD_CompID() As Integer
        Get
            Return (iSRD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_CompID = Value
        End Set
    End Property
    Public Function LoadExistingReturnNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Sales_Return_ID,Sales_Return_ReturnNo From Sales_Return_Masters Where Sales_Return_CompID=" & iCompID & " and Sales_Return_Year=" & iYearID & " order by Sales_Return_ReturnNo"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateReturnNo(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(Sales_Return_ID)+1,1) from Sales_Return_Masters")
            sPrefix = "SR" & "00" & iMax
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID,MAS_Desc from Acc_General_Master Where MAS_Master=1 And Mas_DelFlag='A' and Mas_CompId = " & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSalesReturnMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, objSaleReturn As ClsSR)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_Year", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_Year
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_ReturnNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_ReturnNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_GoodsReturnNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_GoodsReturnNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_RetrunDate", OleDb.OleDbType.Date, 12)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_RetrunDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_InvoiceNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_InvoiceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_InvoiceDate", OleDb.OleDbType.Date, 12)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_InvoiceDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_OrderNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_DispatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_DispatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_Customer", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_Customer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_ShipTo", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_ShipTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_DispatchStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_DispatchStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_State", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_Order", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_Order
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_Dispatch", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_Dispatch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_Invoice", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_Invoice
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Sales_Return_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.Sales_Return_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSales_Return_Masters", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSalesReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objSaleReturn As ClsSR)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Commodity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Commodity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Item", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Item
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Rate", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Quantity", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_RateAmount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_RateAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Discount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Discount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_DiscountAmount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_DiscountAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_TotalAmount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_TotalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Amount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Reason", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Reason
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Charges", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Charges
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_GST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_GST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_GSTRate", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_GSTAmount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_GSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_SGST", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_SGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_SGSTAmount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_SGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_CGST", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_CGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_CGSTAmount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_CGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_IGST", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_IGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_IGSTAmount", OleDb.OleDbType.Double, 15)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_IGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Remarks", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSaleReturn.SRD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSales_ReturnDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesReturn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer) As DataTable
        Dim dt As New DataTable, dtTable As New DataTable
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("ID")
            dt.Columns.Add("MasterID")
            dt.Columns.Add("CommodityID")
            dt.Columns.Add("ItemID")
            dt.Columns.Add("ReasonID")
            dt.Columns.Add("UnitID")
            dt.Columns.Add("DiscountID")
            dt.Columns.Add("GSTID")
            dt.Columns.Add("HistoryID")

            dt.Columns.Add("Commodity")
            dt.Columns.Add("Goods")
            dt.Columns.Add("Unit")
            dt.Columns.Add("QTY")
            dt.Columns.Add("Amount")
            dt.Columns.Add("Rate")
            dt.Columns.Add("RReturn")
            dt.Columns.Add("Discount")
            dt.Columns.Add("TotDiscount")
            dt.Columns.Add("Charges")
            dt.Columns.Add("Gst")
            dt.Columns.Add("GSTAmt")
            dt.Columns.Add("TotAmt")
            dt.Columns.Add("Remarks")


            sSql = "Select *,A.Inv_Description As Commodity,B.Inv_Description as Goods,C.Mas_Desc as Unit, D.Mas_Desc as Discount from Sales_ReturnDetails"
            sSql = sSql & " Left Join Inventory_Master A On A.Inv_ID=SRD_Commodity And A.Inv_Parent=0 and A.Inv_Flag='X' And A.Inv_CompID=" & iCompID & ""
            sSql = sSql & " Left Join Inventory_Master B On B.Inv_ID=SRD_Item And B.INV_Code <> '' And B.Inv_CompID=" & iCompID & ""
            sSql = sSql & " Left Join Acc_General_Master C On C.Mas_ID=SRD_UnitID And C.Mas_CompID=" & iCompID & ""
            sSql = sSql & " Left Join Acc_General_Master D On D.Mas_ID=SRD_Discount And D.Mas_Master=19 And D.Mas_CompID=" & iCompID & ""
            sSql = sSql & " Where SRD_MasterID=" & iID & " And SRD_CompID=" & iCompID & ""
            dtTable = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtTable.Rows.Count > 0 Then
                For i = 0 To dtTable.Rows.Count - 1
                    dr = dt.NewRow
                    dr("SlNo") = i + 1
                    If IsDBNull(dtTable.Rows(i)("SRD_ID").ToString()) = False Then
                        dr("Id") = dtTable.Rows(i)("SRD_ID").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_MasterID").ToString()) = False Then
                        dr("MasterID") = dtTable.Rows(i)("SRD_MasterID").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_Commodity").ToString()) = False Then
                        dr("CommodityID") = dtTable.Rows(i)("SRD_Commodity").ToString()
                    End If

                    If IsDBNull(dtTable.Rows(i)("SRD_Item").ToString()) = False Then
                        dr("ItemID") = dtTable.Rows(i)("SRD_Item").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_Reason").ToString()) = False Then
                        dr("ReasonID") = dtTable.Rows(i)("SRD_Reason").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_UnitID").ToString()) = False Then
                        dr("UnitID") = dtTable.Rows(i)("SRD_UnitID").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_Discount").ToString()) = False Then
                        dr("DiscountID") = dtTable.Rows(i)("SRD_Discount").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_GST_ID").ToString()) = False Then
                        dr("GSTID") = dtTable.Rows(i)("SRD_GST_ID").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_HistoryID").ToString()) = False Then
                        dr("HistoryID") = dtTable.Rows(i)("SRD_HistoryID").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("Commodity").ToString()) = False Then
                        dr("Commodity") = dtTable.Rows(i)("Commodity").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("Goods").ToString()) = False Then
                        dr("Goods") = dtTable.Rows(i)("Goods").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("Unit").ToString()) = False Then
                        dr("Unit") = dtTable.Rows(i)("Unit").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_Quantity").ToString()) = False Then
                        dr("QTY") = dtTable.Rows(i)("SRD_Quantity").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_Rate").ToString()) = False Then
                        dr("Rate") = dtTable.Rows(i)("SRD_Rate").ToString()
                        dr("Amount") = dtTable.Rows(i)("SRD_RateAmount").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_Reason").ToString()) = False Then
                        If dtTable.Rows(i)("SRD_Reason").ToString() = 1 Then
                            dr("RReturn") = "Expired"
                        ElseIf dtTable.Rows(i)("SRD_Reason").ToString() = 2 Then
                            dr("RReturn") = "Damaged"
                        ElseIf dtTable.Rows(i)("SRD_Reason").ToString() = 3 Then
                            dr("RReturn") = "Price Difference"
                        End If
                    End If
                    If IsDBNull(dtTable.Rows(i)("Discount").ToString()) = False Then
                        If dtTable.Rows(i)("Discount").ToString() = "" Then
                            dr("Discount") = 0
                        Else
                            dr("Discount") = dtTable.Rows(i)("Discount").ToString()
                        End If
                    Else
                        dr("Discount") = 0
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_DiscountAmount").ToString()) = False Then
                        dr("TotDiscount") = dtTable.Rows(i)("SRD_DiscountAmount").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_Charges").ToString()) = False Then
                        dr("Charges") = dtTable.Rows(i)("SRD_Charges").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_GSTRate").ToString()) = False Then
                        dr("Gst") = dtTable.Rows(i)("SRD_GSTRate").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_GSTAmount").ToString()) = False Then
                        dr("GSTAmt") = dtTable.Rows(i)("SRD_GSTAmount").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_TotalAmount").ToString()) = False Then
                        dr("TotAmt") = dtTable.Rows(i)("SRD_TotalAmount").ToString()
                    End If
                    If IsDBNull(dtTable.Rows(i)("SRD_Remarks").ToString()) = False Then
                        dr("Remarks") = dtTable.Rows(i)("SRD_Remarks").ToString()
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBatchNo As Integer, ByVal iBaseName As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Sales_Return_Masters Where Sales_Return_BatchNo=" & iBatchNo & " And Sales_Return_BaseName=" & iBaseName & " And Sales_Return_Year=" & iYearID & " And Sales_Return_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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
End Class
