Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsDispatchForm
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iDM_ID As Integer
    Private iDM_OrderID As Integer
    Private dDM_OrderDate As DateTime
    Private iDM_SupplierID As Integer
    Private dDM_DispatchDate As DateTime
    Private iDM_ModeOfShipping As Integer
    Private iDM_ExpectedDays As Integer
    Private iDM_PaymentType As Integer
    Private iDM_CreatedBy As Integer
    Private dDM_CreatedOn As DateTime
    Private sDM_Status As String
    Private iDM_YearID As Integer
    Private iDM_CompID As Integer
    Private dDM_ShippingRate As Double
    Private sDM_ChequeNo As String
    Private dDM_ChequeDate As DateTime
    Private sDM_IFSCCode As String
    Private sDM_BankName As String
    Private sDM_Branch As String

    Private iDM_GrandDiscount As Double
    Private iDM_GrandDiscountAmt As Double
    Private iDM_GrandTotal As Double
    Private iDM_GrandTotalAmt As Double
    Private sDM_Code As String
    Private iDM_SalesManID As Integer
    Private sDM_DispatchRefNo As String
    Private sDM_ESugamNo As String
    Private sDM_Remarks As String
    Private iDM_SaleType As Integer
    Private iDM_OtherType As Integer
    Private iDM_AllocateID As Integer
    Private sDM_Operation As String
    Private sDM_IPAddress As String

    Private iDM_TrType As Integer
    Private sDM_CompanyAddress As String
    Private sDM_CompanyGSTNRegNo As String
    Private sDM_BillingAddress As String
    Private sDM_BillingGSTNRegNo As String
    Private sDM_DeliveryAddress As String
    Private sDM_DeliveryGSTNRegNo As String
    Private sDM_DeliveryFrom As String
    Private sDM_DeliveryFromGSTNRegNo As String
    Private sDM_DispatchStatus As String
    Private iDM_CompanyType As Integer
    Private iDM_GSTNCategory As Integer
    Private sDM_State As String

    Private iDD_ID As Integer
    Private iDD_MasterID As Integer
    Private iDD_CommodityID As Integer
    Private iDD_DescID As Integer
    Private iDD_UnitID As Integer
    Private iDD_HistoryID As Integer
    Private sDD_Rate As String
    Private iDD_Quantity As Double
    Private sDD_RateAmount As String
    Private sDD_Status As String
    Private iDD_CompID As Integer

    Private sDD_Operation As String
    Private sDD_IPAddress As String
    Private iDD_CreatedBy As Integer
    Private iDD_CreatedOn As Date

    Private iDD_GST_ID As Integer
    Private sDD_GSTRate As Double

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

    Private iDM_BatchNo As Integer
    Private iDM_BaseName As Integer
    Private sDM_OrderNo As String
    Private sDM_AllocationNo As String
    Public Property DM_OrderNo() As String
        Get
            Return (sDM_OrderNo)
        End Get
        Set(ByVal Value As String)
            sDM_OrderNo = Value
        End Set
    End Property
    Public Property DM_AllocationNo() As String
        Get
            Return (sDM_AllocationNo)
        End Get
        Set(ByVal Value As String)
            sDM_AllocationNo = Value
        End Set
    End Property
    Public Property DM_BatchNo() As Integer
        Get
            Return (iDM_BatchNo)
        End Get
        Set(ByVal Value As Integer)
            iDM_BatchNo = Value
        End Set
    End Property
    Public Property DM_BaseName() As Integer
        Get
            Return (iDM_BaseName)
        End Get
        Set(ByVal Value As Integer)
            iDM_BaseName = Value
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
    Public Property DM_AllocateID() As Integer
        Get
            Return (iDM_AllocateID)
        End Get
        Set(ByVal Value As Integer)
            iDM_AllocateID = Value
        End Set
    End Property
    Public Property DM_SaleType() As Integer
        Get
            Return (iDM_SaleType)
        End Get
        Set(ByVal Value As Integer)
            iDM_SaleType = Value
        End Set
    End Property
    Public Property DM_OtherType() As Integer
        Get
            Return (iDM_OtherType)
        End Get
        Set(ByVal Value As Integer)
            iDM_OtherType = Value
        End Set
    End Property
    Public Property DM_Remarks() As String
        Get
            Return (sDM_Remarks)
        End Get
        Set(ByVal Value As String)
            sDM_Remarks = Value
        End Set
    End Property
    Public Property DM_ESugamNo() As String
        Get
            Return (sDM_ESugamNo)
        End Get
        Set(ByVal Value As String)
            sDM_ESugamNo = Value
        End Set
    End Property
    Public Property DM_DispatchRefNo() As String
        Get
            Return (sDM_DispatchRefNo)
        End Get
        Set(ByVal Value As String)
            sDM_DispatchRefNo = Value
        End Set
    End Property
    Public Property DM_SalesManID() As Integer
        Get
            Return (iDM_SalesManID)
        End Get
        Set(ByVal Value As Integer)
            iDM_SalesManID = Value
        End Set
    End Property
    Public Property DM_Code() As String
        Get
            Return (sDM_Code)
        End Get
        Set(ByVal Value As String)
            sDM_Code = Value
        End Set
    End Property
    Public Property DM_GrandDiscount() As Double
        Get
            Return (iDM_GrandDiscount)
        End Get
        Set(ByVal Value As Double)
            iDM_GrandDiscount = Value
        End Set
    End Property
    Public Property DM_GrandDiscountAmt() As Double
        Get
            Return (iDM_GrandDiscountAmt)
        End Get
        Set(ByVal Value As Double)
            iDM_GrandDiscountAmt = Value
        End Set
    End Property
    Public Property DM_GrandTotal() As Double
        Get
            Return (iDM_GrandTotal)
        End Get
        Set(ByVal Value As Double)
            iDM_GrandTotal = Value
        End Set
    End Property
    Public Property DM_GrandTotalAmt() As Double
        Get
            Return (iDM_GrandTotalAmt)
        End Get
        Set(ByVal Value As Double)
            iDM_GrandTotalAmt = Value
        End Set
    End Property
    Public Property DM_ChequeNo() As String
        Get
            Return (sDM_ChequeNo)
        End Get
        Set(ByVal Value As String)
            sDM_ChequeNo = Value
        End Set
    End Property
    Public Property DM_ChequeDate() As Date
        Get
            Return (dDM_ChequeDate)
        End Get
        Set(ByVal Value As Date)
            dDM_ChequeDate = Value
        End Set
    End Property
    Public Property DM_IFSCCode() As String
        Get
            Return (sDM_IFSCCode)
        End Get
        Set(ByVal Value As String)
            sDM_IFSCCode = Value
        End Set
    End Property
    Public Property DM_BankName() As String
        Get
            Return (sDM_BankName)
        End Get
        Set(ByVal Value As String)
            sDM_BankName = Value
        End Set
    End Property
    Public Property DM_Branch() As String
        Get
            Return (sDM_Branch)
        End Get
        Set(ByVal Value As String)
            sDM_Branch = Value
        End Set
    End Property
    Public Property DM_Operation() As String
        Get
            Return (sDM_Operation)
        End Get
        Set(ByVal Value As String)
            sDM_Operation = Value
        End Set
    End Property
    Public Property DM_IPAddress() As String
        Get
            Return (sDM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sDM_IPAddress = Value
        End Set
    End Property
    Public Property DM_ID() As Integer
        Get
            Return (iDM_ID)
        End Get
        Set(ByVal Value As Integer)
            iDM_ID = Value
        End Set
    End Property
    Public Property DM_OrderID() As Integer
        Get
            Return (iDM_OrderID)
        End Get
        Set(ByVal Value As Integer)
            iDM_OrderID = Value
        End Set
    End Property
    Public Property DM_OrderDate() As DateTime
        Get
            Return (dDM_OrderDate)
        End Get
        Set(ByVal Value As DateTime)
            dDM_OrderDate = Value
        End Set
    End Property
    Public Property DM_SupplierID() As Integer
        Get
            Return (iDM_SupplierID)
        End Get
        Set(ByVal Value As Integer)
            iDM_SupplierID = Value
        End Set
    End Property
    Public Property DM_DispatchDate() As DateTime
        Get
            Return (dDM_DispatchDate)
        End Get
        Set(ByVal Value As DateTime)
            dDM_DispatchDate = Value
        End Set
    End Property
    Public Property DM_ModeOfShipping() As Integer
        Get
            Return (iDM_ModeOfShipping)
        End Get
        Set(ByVal Value As Integer)
            iDM_ModeOfShipping = Value
        End Set
    End Property
    Public Property DM_ExpectedDays() As Integer
        Get
            Return (iDM_ExpectedDays)
        End Get
        Set(ByVal Value As Integer)
            iDM_ExpectedDays = Value
        End Set
    End Property
    Public Property DM_PaymentType() As Integer
        Get
            Return (iDM_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            iDM_PaymentType = Value
        End Set
    End Property
    Public Property DM_CreatedBy() As Integer
        Get
            Return (iDM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iDM_CreatedBy = Value
        End Set
    End Property
    Public Property DM_CreatedOn() As DateTime
        Get
            Return (dDM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dDM_CreatedOn = Value
        End Set
    End Property
    Public Property DM_Status() As String
        Get
            Return (sDM_Status)
        End Get
        Set(ByVal Value As String)
            sDM_Status = Value
        End Set
    End Property
    Public Property DM_YearID() As Integer
        Get
            Return (iDM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iDM_YearID = Value
        End Set
    End Property
    Public Property DM_CompID() As Integer
        Get
            Return (iDM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iDM_CompID = Value
        End Set
    End Property
    Public Property DM_ShippingRate() As Double
        Get
            Return (dDM_ShippingRate)
        End Get
        Set(ByVal Value As Double)
            dDM_ShippingRate = Value
        End Set
    End Property

    Public Property DD_ID() As Integer
        Get
            Return (iDD_ID)
        End Get
        Set(ByVal Value As Integer)
            iDD_ID = Value
        End Set
    End Property
    Public Property DD_MasterID() As Integer
        Get
            Return (iDD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iDD_MasterID = Value
        End Set
    End Property
    Public Property DD_CommodityID() As Integer
        Get
            Return (iDD_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iDD_CommodityID = Value
        End Set
    End Property
    Public Property DD_DescID() As Integer
        Get
            Return (iDD_DescID)
        End Get
        Set(ByVal Value As Integer)
            iDD_DescID = Value
        End Set
    End Property
    Public Property DD_UnitID() As Integer
        Get
            Return (iDD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            iDD_UnitID = Value
        End Set
    End Property
    Public Property DD_HistoryID() As Integer
        Get
            Return (iDD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iDD_HistoryID = Value
        End Set
    End Property
    Public Property DD_Rate() As Double
        Get
            Return (sDD_Rate)
        End Get
        Set(ByVal Value As Double)
            sDD_Rate = Value
        End Set
    End Property
    Public Property DD_Quantity() As Double
        Get
            Return (iDD_Quantity)
        End Get
        Set(ByVal Value As Double)
            iDD_Quantity = Value
        End Set
    End Property
    Public Property DD_RateAmount() As Double
        Get
            Return (sDD_RateAmount)
        End Get
        Set(ByVal Value As Double)
            sDD_RateAmount = Value
        End Set
    End Property
    Public Property DD_Status() As String
        Get
            Return (sDD_Status)
        End Get
        Set(ByVal Value As String)
            sDD_Status = Value
        End Set
    End Property
    Public Property DD_CompID() As Integer
        Get
            Return (iDD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iDD_CompID = Value
        End Set
    End Property
    Public Property DM_TrType() As Integer
        Get
            Return (iDM_TrType)
        End Get
        Set(ByVal Value As Integer)
            iDM_TrType = Value
        End Set
    End Property
    Public Property DM_CompanyAddress() As String
        Get
            Return (sDM_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sDM_CompanyAddress = Value
        End Set
    End Property
    Public Property DM_CompanyGSTNRegNo() As String
        Get
            Return (sDM_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sDM_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property DM_BillingAddress() As String
        Get
            Return (sDM_BillingAddress)
        End Get
        Set(ByVal Value As String)
            sDM_BillingAddress = Value
        End Set
    End Property
    Public Property DM_BillingGSTNRegNo() As String
        Get
            Return (sDM_BillingGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sDM_BillingGSTNRegNo = Value
        End Set
    End Property

    Public Property DM_DeliveryFrom() As String
        Get
            Return (sDM_DeliveryFrom)
        End Get
        Set(ByVal Value As String)
            sDM_DeliveryFrom = Value
        End Set
    End Property
    Public Property DM_DeliveryFromGSTNRegNo() As String
        Get
            Return (sDM_DeliveryFromGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sDM_DeliveryFromGSTNRegNo = Value
        End Set
    End Property

    Public Property DM_DeliveryAddress() As String
        Get
            Return (sDM_DeliveryAddress)
        End Get
        Set(ByVal Value As String)
            sDM_DeliveryAddress = Value
        End Set
    End Property
    Public Property DM_DeliveryGSTNRegNo() As String
        Get
            Return (sDM_DeliveryGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sDM_DeliveryGSTNRegNo = Value
        End Set
    End Property
    Public Property DM_DispatchStatus() As String
        Get
            Return (sDM_DispatchStatus)
        End Get
        Set(ByVal Value As String)
            sDM_DispatchStatus = Value
        End Set
    End Property
    Public Property DM_State() As String
        Get
            Return (sDM_State)
        End Get
        Set(ByVal Value As String)
            sDM_State = Value
        End Set
    End Property
    Public Property DM_CompanyType() As Integer
        Get
            Return (iDM_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iDM_CompanyType = Value
        End Set
    End Property
    Public Property DM_GSTNCategory() As Integer
        Get
            Return (iDM_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iDM_GSTNCategory = Value
        End Set
    End Property

    Public Property DD_GST_ID() As Integer
        Get
            Return (iDD_GST_ID)
        End Get
        Set(ByVal Value As Integer)
            iDD_GST_ID = Value
        End Set
    End Property
    Public Property DD_GSTRate() As Double
        Get
            Return (sDD_GSTRate)
        End Get
        Set(ByVal Value As Double)
            sDD_GSTRate = Value
        End Set
    End Property
    Public Property DD_CreatedBy() As Integer
        Get
            Return (iDD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iDD_CreatedBy = Value
        End Set
    End Property
    Public Property DD_CreatedOn() As Date
        Get
            Return (iDD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            iDD_CreatedOn = Value
        End Set
    End Property
    Public Property DD_Operation() As String
        Get
            Return (sDD_Operation)
        End Get
        Set(ByVal Value As String)
            sDD_Operation = Value
        End Set
    End Property
    Public Property DD_IPAddress() As String
        Get
            Return (sDD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sDD_IPAddress = Value
        End Set
    End Property
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
    Public Function BindDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDefaultBranch As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select DM_ID,DM_Code From Dispatch_Master,Sales_Proforma_Order where SPO_BranchID=" & iDefaultBranch & " And DM_OrderID=SPO_ID And SPO_OrderType='S' And DM_CompID=" & iCompID & " and DM_YearID = " & iYearID & " Order By DM_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDashBoardDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select DM_ID,DM_Code from Dispatch_Master,Sales_Proforma_Order where DM_OrderID=SPO_ID And SPO_OrderType='S' And DM_YearID=" & iYearID & " And DM_CompID =" & iCompID & " Order By DM_ID ASC"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindExistingCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_DispatchFlag<>1 And SAM_Status='W' And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesMan(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_Id,Usr_FullName + '-' + Usr_Code as username From Sad_Userdetails where USR_Designation in(Select MAS_ID From Acc_General_master where MAs_Desc='Sales Person' And mas_master=6) And Usr_CompID=" & iCompID & " order by Usr_FullName "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMethodOfShiping(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=13 And Mas_DelFlag='A' "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
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
    Public Function BindPaymentType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Payment Type') And Mas_CompID=" & iCompID & " and Mas_DelFlag='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOrderNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDefaultBranch As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Distinct(a.SPO_ID) As SPO_ID,a.SPO_OrderCode from sales_Proforma_order a
                           Left Join Sales_Allocate_Master b On b.SAM_OrderNo=a.SPO_ID
            Left Join Dispatch_Master c On c.DM_OrderID=a.SPO_ID
                           where a.SPO_BranchID=" & iDefaultBranch & " And a.SPO_OrderType<>'O' And ((a.SPO_ID=c.DM_OrderID And c.DM_Status<>'A') Or (a.SPO_OrderType='S' And b.SAM_DispatchFlag <> 1 And b.SAM_Status='W' and b.SAM_YearID =" & iYearID & " And b.SAM_CompID=" & iCompID & " and a.SPO_YearID=" & iYearID & " And a.SPO_CompID=" & iCompID & ")) Order By a.SPO_OrderCode Desc"

            'sSql = "Select Distinct(a.SPO_ID) As SPO_ID,a.SPO_OrderCode from sales_Proforma_order a
            '               Left Join Sales_Allocate_Master b On b.SAM_OrderNo=a.SPO_ID
            'Left Join Dispatch_Master c On c.DM_OrderID=a.SPO_ID
            '               where a.SPO_OrderType<>'O' And ((a.SPO_ID=c.DM_OrderID And c.DM_Status<>'A') Or (a.SPO_OrderType='S' And b.SAM_DispatchFlag <> 1 And b.SAM_Status='W' and b.SAM_YearID =" & iYearID & " And b.SAM_CompID=" & iCompID & " and a.SPO_YearID=" & iYearID & " And a.SPO_CompID=" & iCompID & ")) And (b.SAM_ID not in (Select DM_AllocateID From Dispatch_Master,Sales_Allocate_Master Where DM_OrderID = SAM_OrderNo) Or (b.SAM_ID in (Select DM_AllocateID From Dispatch_Master,Sales_Allocate_Master Where DM_OrderID = SAM_OrderNo And DM_Status='W'))) Order By a.SPO_OrderCode Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DashBoardOrderNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Distinct(SPO_ID) As SPO_ID,SPO_OrderCode From Sales_Proforma_Order,Sales_Allocate_Master Where SPO_ID=SAM_OrderNo And SPO_OrderType='S' And SAM_Status='W' and SAM_YearID =" & iYearID & " And SAM_CompID=" & iCompID & " and SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & " Order By SPO_OrderCode Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDashBoardAllocationNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
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
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckOrderForDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iAllocationID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sStr As String = ""
        Try
            If iAllocationID > 0 Then
                sSql = "Select * From Dispatch_Master Where DM_AllocateID=" & iAllocationID & " And DM_OrderID=" & iOrderID & " And DM_YearID=" & iYearID & " And DM_CompID=" & iCompID & " "
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Else
                sSql = "Select * From Dispatch_Master Where DM_OrderID=" & iOrderID & " And DM_YearID=" & iYearID & " And DM_CompID=" & iCompID & " "
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            End If
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
    Public Function BindDispatchedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As DataTable
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
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("OrderedQty")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")

            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Dispatch_Details Where DD_MasterID In(Select DM_ID From Dispatch_Master Where "
                sSql = sSql & "DM_ID =" & iDispatchID & " And DM_CompID =" & iCompID & " And DM_YearID =" & iYearID & ") And DD_CompID=" & iCompID & " "
            Else
                If iAllocatedID > 0 Then
                    sSql = "" : sSql = "Select * From Dispatch_Details Where DD_MasterID In(Select DM_ID From Dispatch_Master Where "
                    sSql = sSql & "DM_OrderID =" & iMasterID & " And DM_AllocateID=" & iAllocatedID & " And DM_CompID =" & iCompID & " And DM_YearID =" & iYearID & ") And DD_CompID=" & iCompID & " "
                Else
                    sSql = "" : sSql = "Select * From Dispatch_Details Where DD_MasterID In(Select DM_ID From Dispatch_Master Where "
                    sSql = sSql & "DM_OrderID =" & iMasterID & " And DM_CompID =" & iCompID & " And DM_YearID =" & iYearID & ") And DD_CompID=" & iCompID & " "
                End If
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("DD_CommodityID")
                    dRow("ItemID") = dt.Rows(i)("DD_DescID")
                    dRow("HistoryID") = dt.Rows(i)("DD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("DD_UnitID")
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From  Inventory_Master Where INV_ID=" & dt.Rows(i)("DD_CommodityID") & " And INv_CompID = " & iCompID & " And INV_Parent=0 ")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("DD_DescID") & " And INV_Parent=" & dt.Rows(i)("DD_CommodityID") & " And INv_CompID = " & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("DD_UnitID") & " And Mas_CompID =" & iCompID & "")
                    dRow("MRP") = dt.Rows(i)("DD_Rate")
                    dRow("OrderedQty") = dt.Rows(i)("DD_Quantity")
                    dRow("Total") = dt.Rows(i)("DD_RateAmount")
                    dRow("GSTID") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_GST_ID")))
                    dRow("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_GSTRate")))
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iAllocationID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Delete From Charges_Master Where C_PSType='S' And C_OrderID=" & iOrderID & " And C_AllocatedID=" & iAllocationID & " And C_DispatchID=" & iDispatchID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Acc_Head").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Head").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_Group").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Group").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGroup").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGroup").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_GL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_GL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                Return sPerm
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAllocationID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select SAM_OrderNo From Sales_Allocate_Master Where SAM_ID=" & iAllocationID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & ""
            GetOrderNo = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetOrderNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAllocationSaved(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iAllocateID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * From Dispatch_Master Where DM_OrderID=" & iOrderID & " And DM_AllocateID=" & iAllocateID & " And DM_CompID=" & iCompID & " And DM_YearID=" & iYearID & " "
            CheckAllocationSaved = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckAllocationSaved
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From sales_Proforma_order Where SPO_ID=" & iOrderNo & " And SPO_CompID=" & iCompID & " And SPO_YearID = " & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Try
            sSql = "Select BM_CODE From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_CompID=" & iCompID & " "
            'sSql = "Select ACM_CODE From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_CompID=" & iCompID & " "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            If sStr.StartsWith("C") Then
                GetPartyCode = "C"
            End If
            If sStr.StartsWith("P") Then
                GetPartyCode = "P"
            End If
            Return GetPartyCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Sales_Buyers_Masters Where BM_ID=" & iCustomerID & " And BM_CompID=" & iCompID & " "
            GetCustomerDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCustomerDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllocatedOrderMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iAllocateID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAllocateID > 0 Then
                sSql = "Select * From Sales_Allocate_Master Where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iOrderNo & " And SAM_CompID=" & iCompID & " and SAM_YearID =" & iYearID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAllocatedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iAllocateID As Integer, ByVal sCompanyType As String, ByVal sGSTNCategory As String) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer

        Dim dNetAmt As Double = 0, dTAXAmt As Double = 0
        Dim dExciseAmt As Double = 0, dCSTAmt As Double = 0
        Dim dTAX As Double = 0
        Dim sGSTRate As String = ""
        Try
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("Commodity")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("OrderedQty")
            dtTab.Columns.Add("Total")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")

            If iAllocateID > 0 Then
                sSql = "" : sSql = "Select * From Sales_Allocate_Details Where SAD_PlacedQnt > 0 and SAD_MasterID In(Select SAM_ID From Sales_Allocate_Master Where "
                sSql = sSql & "SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iMasterID & " And SAM_YearID =" & iYearID & " And SAM_CompID =" & iCompID & ") And SAD_CompID=" & iCompID & " "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("SAD_Commodity")
                    dRow("ItemID") = dt.Rows(i)("SAD_DescID")
                    dRow("HistoryID") = dt.Rows(i)("SAD_HisotryID")
                    dRow("UnitID") = dt.Rows(i)("SAD_UnitID")
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("SAD_Commodity") & " and Inv_CompID = " & iCompID & " And INV_Parent=0 ")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("SAD_DescID") & " And INV_Parent=" & dt.Rows(i)("SAD_Commodity") & " and Inv_CompID = " & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_UnitID") & " and Mas_CompID =" & iCompID & "")
                    dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_MRP")))
                    dRow("OrderedQty") = dt.Rows(i)("SAD_PlacedQnt")
                    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_PlacedQntAmount")))

                    sGSTRate = getGSTRate(sNameSpace, iCompID, sCompanyType, sGSTNCategory)
                    If sGSTRate <> "HSN" Then
                        'dRow("GSTID") = 0
                        dRow("GSTID") = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 GST_ID From GST_Rates Where GST_ItemID=" & dt.Rows(i)("SAD_DescID") & " And GST_CommodityID=" & dt.Rows(i)("SAD_Commodity") & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc ")
                        dRow("GSTRate") = 0
                        'getGSTRate(sNameSpace, iCompID, sCompanyType, sGSTNCategory)
                    Else
                        dRow("GSTID") = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 GST_ID From GST_Rates Where GST_ItemID=" & dt.Rows(i)("SAD_DescID") & " And GST_CommodityID=" & dt.Rows(i)("SAD_Commodity") & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc ")
                        dRow("GSTRate") = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 GST_GSTRate From GST_Rates Where GST_ItemID=" & dt.Rows(i)("SAD_DescID") & " And GST_CommodityID=" & dt.Rows(i)("SAD_Commodity") & " and  GST_CompID= " & iCompID & " Order By GST_ID Desc ")
                    End If

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getGSTRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String, ByVal sGSTNCategoryDesc As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 GC_GSTRate From GSTCategory_Table Where GC_CompanyType='" & sCompanyType & "' And GC_GSTcategory= '" & sGSTNCategoryDesc & "' Order By GC_ID Desc  "
            getGSTRate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return getGSTRate
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
    Public Function BindDispatchMasterData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim dt As New DataTable
        Try
            If iDispatchID > 0 Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Dispatch_Master Where DM_ID=" & iDispatchID & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Dispatch_Master Where DM_ID=" & iDispatchID & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            Else
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Dispatch_Master Where DM_OrderID=" & iOrderNo & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Dispatch_Master Where DM_OrderID=" & iOrderNo & " And DM_CompID=" & iCompID & " And DM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindChargeData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("ChargeID")
            dtTab.Columns.Add("ChargeType")
            dtTab.Columns.Add("ChargeAmount")

            If iDispatchID > 0 Then
                sSql = "" : sSql = "Select * From Charges_Master,Dispatch_Master Where C_OrderID=DM_OrderID And C_AllocatedID=DM_AllocateID And C_DispatchID=DM_ID And C_DispatchID=" & iDispatchID & " And C_CompID =" & iCompID & " And C_YearID =" & iYearID & " And C_PSType='S' "
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
    Public Function SaveCharges(ByVal sNameSpace As String, ByVal objDispatch As ClsDispatchForm) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_AllocatedID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_AllocatedID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_DispatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_DispatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_OrderType", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.C_OrderType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_ChargeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeType", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDispatch.C_ChargeType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_ChargeAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.C_ChargeAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_PSType", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.C_PSType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.C_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.C_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.C_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.C_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.C_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_SalesReturnID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_SalesReturnID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@C_GoodsReturnID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.C_GoodsReturnID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCharges_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDispatchMaster(ByVal sNameSpace As String, ByVal objDispatch As ClsDispatchForm) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(51) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_OrderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_OrderDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DM_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_SupplierID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_SupplierID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DispatchDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DM_DispatchDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ModeOfShipping", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_ModeOfShipping
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ExpectedDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_ExpectedDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_PaymentType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Status ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDispatch.DM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ShippingRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_ShippingRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ChequeNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.DM_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ChequeDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DM_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_IFSCCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDispatch.DM_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BankName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDispatch.DM_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Branch", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDispatch.DM_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.DM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.DM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GrandDiscount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_GrandDiscount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GrandDiscountAmt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_GrandDiscountAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GrandTotal", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_GrandTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GrandTotalAmt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_GrandTotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Code", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDispatch.sDM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_SalesManID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_SalesManID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DispatchRefNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDispatch.DM_DispatchRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_ESugamNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDispatch.DM_ESugamNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objDispatch.DM_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_SaleType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_SaleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_OtherType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_OtherType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_AllocateID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_AllocateID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CompanyAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.sDM_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.sDM_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BillingAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.DM_BillingAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BillingGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.DM_BillingGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DeliveryFrom", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.sDM_DeliveryFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DeliveryFromGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.sDM_DeliveryFromGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DeliveryAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDispatch.DM_DeliveryAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DeliveryGSTNRegNo", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objDispatch.DM_DeliveryGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_DispatchStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.sDM_DispatchStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DM_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_State", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sDM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_OrderNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sDM_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_AllocationNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDispatch.sDM_AllocationNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BatchNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DM_BaseName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.iDM_BaseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spDispatch_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDispatchDetails(ByVal sNameSpace As String, ByVal objDispatch As ClsDispatchForm) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_CommodityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_DescID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_Rate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_Quantity", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_RateAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_RateAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDispatch.DD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_CompiD", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDispatch.DD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDispatch.DD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDispatch.DD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_GST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_GST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DD_GSTRate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objDispatch.DD_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spDispatch_Details", 1, Arr, ObjParam)
            Return Arr
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
    Public Function CheckStateCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGSTNRegNo As String) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Dim bCheck As Boolean
        Dim sCode As String = ""
        Try
            sCode = sGSTNRegNo.Substring(0, 2)

            sSql = "Select * From GSTN_RegNo_Master Where GR_TIN='" & sCode & "' And GR_CompID=" & iCompID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql).ToString
            Return bCheck
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
    Public Function GetGSTCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select BM_GSTNCategory from Sales_Buyers_Masters where BM_ID=" & iCustomerID & " and BM_CompID =" & iCompID & " "
            GetGSTCategory = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetGSTCategory
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
    Public Function GetBranchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBranchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_ID=" & iBranchID & " And CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_Name=" & iBranchID & " And CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CUSTB_ID,CUSTB_Name From MST_CUSTOMER_MASTER_Branch Where CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID=" & iCompID & " "
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
    Public Function CheckApprove(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDMID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select DM_Status From Dispatch_Master Where DM_ID=" & iDMID & " And DM_CompID=" & iCompID & " And DM_YearID=" & iYearID & " "
            CheckApprove = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return CheckApprove
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ApproveInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDMID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Dispatch_Master Set DM_Status='A' Where DM_ID=" & iDMID & " And DM_CompID=" & iCompID & " And DM_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
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
    Public Function GetGSTDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGstCategory As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "select GC_GSTCategory from GSTcategory_table where GC_ID=" & iGstCategory & " and GC_CompID=" & iCompID & ""
            GetGSTDescription = objDBL.SQLGetDescription(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getBranchFromPO(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPodID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_BranchID from  Sales_Proforma_Order where SPO_ID=" & iPodID & " and SPO_CompID=" & iCompID & ""
            getBranchFromPO = objDBL.SQLExecuteScalar(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function CheckDetailsofBranchState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPodID As Integer) As String
        Dim sSql As String = ""
        Dim iPOMBranchID As Integer : Dim iCompBrnchID As Integer
        Try
            sSql = "Select SPO_BranchID from  Sales_Proforma_Order where SPO_ID=" & iPodID & " and SPO_CompID=" & iCompID & ""
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
End Class
