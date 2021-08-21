Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsCustBilling
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iLCB_ID As Integer
    Private iLCB_CustomerID As Integer
    Private iLCB_RouteID As Integer
    Private dLCB_FromDate As DateTime
    Private dLCB_ToDate As DateTime
    Private dLCB_InvDate As DateTime
    Private sLCB_InvNo As String
    Private sLCB_CustOrderRef As String
    Private sLCB_Agreement As String
    Private dLCB_TotalAmt As Double
    Private sLCB_CompanyAddress As String
    Private sLCB_CompanyGSTNRegNo As String
    Private sLCB_CustomerAddress As String
    Private sLCB_CustomerGSTNRegNo As String
    Private iLCB_GSTNCategory As Integer
    Private dLCB_GSTRate As Double
    Private dLCB_GSTAmount As Double
    Private dLCB_SGST As Double
    Private dLCB_SGSTAmount As Double
    Private dLCB_CGST As Double
    Private dLCB_CGSTAmount As Double
    Private dLCB_IGST As Double
    Private dLCB_IGSTAmount As Double
    Private sLCB_GSTCustBillStatus As String
    Private sLCB_State As String
    Private dLCB_EscAmt As Double
    Private dLCB_TotalEscAmt As Double
    Public Property LCB_ID() As Integer
        Get
            Return (iLCB_ID)
        End Get
        Set(ByVal Value As Integer)
            iLCB_ID = Value
        End Set
    End Property

    Public Property LCB_CustomerID() As Integer
        Get
            Return (iLCB_CustomerID)
        End Get
        Set(ByVal Value As Integer)
            iLCB_CustomerID = Value
        End Set
    End Property
    Public Property LCB_RouteID() As Integer
        Get
            Return (iLCB_RouteID)
        End Get
        Set(ByVal Value As Integer)
            iLCB_RouteID = Value
        End Set
    End Property
    Public Property LCB_FromDate() As Date
        Get
            Return (dLCB_FromDate)
        End Get
        Set(ByVal Value As Date)
            dLCB_FromDate = Value
        End Set
    End Property
    Public Property LCB_ToDate() As Date
        Get
            Return (dLCB_ToDate)
        End Get
        Set(ByVal Value As Date)
            dLCB_ToDate = Value
        End Set
    End Property
    Public Property LCB_InvDate() As Date
        Get
            Return (dLCB_InvDate)
        End Get
        Set(ByVal Value As Date)
            dLCB_InvDate = Value
        End Set
    End Property

    Public Property LCB_InvNo() As String
        Get
            Return (sLCB_InvNo)
        End Get
        Set(ByVal Value As String)
            sLCB_InvNo = Value
        End Set
    End Property

    Public Property LCB_CustOrderRef() As String
        Get
            Return (sLCB_CustOrderRef)
        End Get
        Set(ByVal Value As String)
            sLCB_CustOrderRef = Value
        End Set
    End Property
    Public Property LCB_Agreement() As String
        Get
            Return (sLCB_Agreement)
        End Get
        Set(ByVal Value As String)
            sLCB_Agreement = Value
        End Set
    End Property

    Public Property LCB_TotalAmt() As Double
        Get
            Return (dLCB_TotalAmt)
        End Get
        Set(ByVal Value As Double)
            dLCB_TotalAmt = Value
        End Set
    End Property
    Public Property LCB_CompanyAddress() As String
        Get
            Return (sLCB_CompanyAddress)
        End Get
        Set(ByVal Value As String)
            sLCB_CompanyAddress = Value
        End Set
    End Property


    Public Property LCB_CompanyGSTNRegNo() As String
        Get
            Return (sLCB_CompanyGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sLCB_CompanyGSTNRegNo = Value
        End Set
    End Property
    Public Property LCB_CustomerAddress() As String
        Get
            Return (sLCB_CustomerAddress)
        End Get
        Set(ByVal Value As String)
            sLCB_CustomerAddress = Value
        End Set
    End Property

    Public Property LCB_CustomerGSTNRegNo() As String
        Get
            Return (sLCB_CustomerGSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sLCB_CustomerGSTNRegNo = Value
        End Set
    End Property
    Public Property LCB_GSTNCategory() As Integer
        Get
            Return (iLCB_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iLCB_GSTNCategory = Value
        End Set
    End Property

    Public Property LCB_GSTRate() As Double
        Get
            Return (dLCB_GSTRate)
        End Get
        Set(ByVal Value As Double)
            dLCB_GSTRate = Value
        End Set
    End Property
    Public Property LCB_GSTAmount() As Double
        Get
            Return (dLCB_GSTAmount)
        End Get
        Set(ByVal Value As Double)
            dLCB_GSTAmount = Value
        End Set
    End Property

    Public Property LCB_SGST() As Double
        Get
            Return (dLCB_SGST)
        End Get
        Set(ByVal Value As Double)
            dLCB_SGST = Value
        End Set
    End Property
    Public Property LCB_SGSTAmount() As Double
        Get
            Return (dLCB_SGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dLCB_SGSTAmount = Value
        End Set
    End Property


    Public Property LCB_CGST() As Double
        Get
            Return (dLCB_CGST)
        End Get
        Set(ByVal Value As Double)
            dLCB_CGST = Value
        End Set
    End Property
    Public Property LCB_CGSTAmount() As Double
        Get
            Return (dLCB_CGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dLCB_CGSTAmount = Value
        End Set
    End Property

    Public Property LCB_IGST() As Double
        Get
            Return (dLCB_IGST)
        End Get
        Set(ByVal Value As Double)
            dLCB_IGST = Value
        End Set
    End Property
    Public Property LCB_IGSTAmount() As Double
        Get
            Return (dLCB_IGSTAmount)
        End Get
        Set(ByVal Value As Double)
            dLCB_IGSTAmount = Value
        End Set
    End Property

    Public Property LCB_GSTCustBillStatus() As String
        Get
            Return (sLCB_GSTCustBillStatus)
        End Get
        Set(ByVal Value As String)
            sLCB_GSTCustBillStatus = Value
        End Set
    End Property
    Public Property LCB_State() As String
        Get
            Return (sLCB_State)
        End Get
        Set(ByVal Value As String)
            sLCB_State = Value
        End Set
    End Property

    Private sLCB_Delflag As String
    Private iLCB_CompID As Integer
    Private iLCB_YearID As Integer
    Private sLCB_Status As String
    Private sLCB_Operation As String
    Private sLCB_IPAddress As String
    Private iLCB_CreatedBy As Integer
    Private dLCB_CreatedOn As DateTime
    Private iLCB_ApprovedBy As Integer
    Private dLCB_ApprovedOn As DateTime
    Private iLCB_DeletedBy As Integer
    Private dLCB_DeletedOn As DateTime
    Private iLCB_UpdatedBy As Integer
    Private dLCB_UpdatedOn As DateTime
    Private iLCB_RecalldBy As Integer


    Public Property LCB_Delflag() As String
        Get
            Return (sLCB_Delflag)
        End Get
        Set(ByVal Value As String)
            sLCB_Delflag = Value
        End Set
    End Property
    Public Property LCB_CompID() As Integer
        Get
            Return (iLCB_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLCB_CompID = Value
        End Set
    End Property
    Public Property LCB_YearID() As Integer
        Get
            Return (iLCB_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLCB_YearID = Value
        End Set
    End Property
    Public Property LCB_Status() As String
        Get
            Return (sLCB_Status)
        End Get
        Set(ByVal Value As String)
            sLCB_Status = Value
        End Set
    End Property
    Public Property LCB_Operation() As String
        Get
            Return (sLCB_Operation)
        End Get
        Set(ByVal Value As String)
            sLCB_Operation = Value
        End Set
    End Property
    Public Property LCB_IPAddress() As String
        Get
            Return (sLCB_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLCB_IPAddress = Value
        End Set
    End Property
    Public Property LCB_CreatedBy() As Integer
        Get
            Return (iLCB_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLCB_CreatedBy = Value
        End Set
    End Property
    Public Property LCB_CreatedOn() As Date
        Get
            Return (dLCB_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLCB_CreatedOn = Value
        End Set
    End Property

    Public Property LCB_ApprovedBy() As Integer
        Get
            Return (iLCB_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLCB_ApprovedBy = Value
        End Set
    End Property
    Public Property LCB_ApprovedOn() As Date
        Get
            Return (dLCB_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLCB_ApprovedOn = Value
        End Set
    End Property
    Public Property LCB_DeletedBy() As Integer
        Get
            Return (iLCB_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLCB_DeletedBy = Value
        End Set
    End Property
    Public Property LCB_DeletedOn() As Date
        Get
            Return (dLCB_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLCB_DeletedOn = Value
        End Set
    End Property
    Public Property LCB_UpdatedBy() As Integer
        Get
            Return (iLCB_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLCB_UpdatedBy = Value
        End Set
    End Property
    Public Property LCB_UpdatedOn() As Date
        Get
            Return (dLCB_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLCB_UpdatedOn = Value
        End Set
    End Property
    Public Property LCB_RecalldBy() As Integer
        Get
            Return (iLCB_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLCB_RecalldBy = Value
        End Set
    End Property
    Public Property LCB_EscAmt() As Double
        Get
            Return (dLCB_EscAmt)
        End Get
        Set(ByVal Value As Double)
            dLCB_EscAmt = Value
        End Set
    End Property
    Public Property LCB_TotalEscAmt() As Double
        Get
            Return (dLCB_TotalEscAmt)
        End Get
        Set(ByVal Value As Double)
            dLCB_TotalEscAmt = Value
        End Set
    End Property

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

    Private ATD_ZoneID As Integer
    Private ATD_RegionID As Integer
    Private ATD_AreaID As Integer
    Private ATD_BranchID As Integer

    Private ATD_UpdatedOn As Date
    Private ATD_UpdatedBy As Integer


    Private ATD_OpenDebit As Decimal
    Private ATD_OpenCredit As Decimal
    Private ATD_ClosingDebit As Decimal
    Private ATD_ClosingCredit As Decimal
    Private ATD_SeqReferenceNum As Integer

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

    Public Function LoadCustomer(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & "  order by BM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadCustomerAll(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & "  order by BM_Name "
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadRoute(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LRM_ID,LRM_StartDestPlace From lgst_route_Master Where LRM_CompID=" & iCompID & " " 'order by LRM_Name 
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadRouteAll(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select LRM_ID,LRM_StartDestPlace From lgst_route_Master Where LRM_CompID=" & iCompID & "" 'order by LRM_Name 
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadExistingInvoiceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LCB_ID,LCB_InvNo From lgst_Customerbilling Where LCB_CompID=" & iCompID & " and LCB_YearId=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRouteIDs(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sRouteName As String)
        Dim sSql As String
        Dim dt As New DataTable
        Dim sRouteID As String
        Try
            sSql = "" : sSql = "Select LRM_Id from lgst_route_Master where LRM_StartDestPlace='" & sRouteName & "' and LRM_Status='A' and LRM_CompID=" & iCompID & " and LRM_YearID=" & iYearID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If i = 0 Then
                        sRouteID = dt.Rows(i).Item("LRM_Id")
                    Else
                        sRouteID = sRouteID & "," & dt.Rows(i).Item("LRM_Id")
                    End If

                Next

            End If
            Return sRouteID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sFromDate As String, ByVal sToDate As String, ByVal iCustomerId As Integer, ByVal sRouteID As String)
        Dim sSql As String
        Dim dTotalAmount As Double = 0.00
        Try
            'sFromDate = objGen.FormatDtForRDBMS(sFromDate, "T")
            sSql = "" : sSql = "Select sum(LTGM_Rate) from Lgst_TripGeneration_Master where LTGM_DestinationCustomer=" & iCustomerId & " and LTGM_Status='A' and LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearID & " and LTGM_RouteID in (" & sRouteID & ") and LTGM_TripStatus=2 "
            If (sFromDate <> "01/01/1900") And (sToDate <> "01-01-1900") Then
                sSql = sSql & " and LTGM_StopDate Between '" & sFromDate & "' And '" & sToDate & "'  "
            End If
            'sSql = sSql & " order by LTGM_ID"
            dTotalAmount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return dTotalAmount
        Catch ex As Exception
            'Throw
        End Try
    End Function
    Public Function GetEscAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sFromDate As String, ByVal sToDate As String, ByVal iCustomerId As Integer, ByVal sRouteID As String, ByVal dEscAmt As Double)
        Dim sSql As String
        Dim dTotalAmount As Double = 0.00
        Dim dCount As Double = 0.00
        Try
            ''sFromDate = objGen.FormatDtForRDBMS(sFromDate, "T")
            'sSql = "" : sSql = "Select sum(LTGM_Rate) from Lgst_TripGeneration_Master where LTGM_DestinationCustomer=" & iCustomerId & " and LTGM_Status='A' and LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearID & " and LTGM_RouteID in (" & sRouteID & ") and LTGM_TripStatus=2 "
            'If (sFromDate <> "01/01/1900") And (sToDate <> "01-01-1900") Then
            '    sSql = sSql & " and LTGM_StopDate Between '" & sFromDate & "' And '" & sToDate & "'  "
            'End If
            ''sSql = sSql & " order by LTGM_ID"
            'dTotalAmount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            '   If dTotalAmount <> 0 Then
            sSql = "" : sSql = "Select count(LTGM_ID) from Lgst_TripGeneration_Master where LTGM_DestinationCustomer=" & iCustomerId & " and LTGM_Status='A' and LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearID & " and LTGM_RouteID in (" & sRouteID & ") and LTGM_TripStatus=2 "
                If (sFromDate <> "01/01/1900") And (sToDate <> "01-01-1900") Then
                    sSql = sSql & " and LTGM_StopDate Between '" & sFromDate & "' And '" & sToDate & "'  "
                    dCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                    dCount = dCount * dEscAmt
                End If
            '    dTotalAmount = dTotalAmount + dCount
            ' End If
            Return dCount
        Catch ex As Exception
            'Throw
        End Try
    End Function
    Public Function LoadCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select *,BM_Name from Sales_Buyers_Masters where BM_ID = " & sCode & " and bm_Delflag <> 'D' and bm_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from MST_Customer_Master where Cust_Code = '" & sCode & "' and Cust_Delflg <> 'D' and CUST_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateInvoiceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Dim sSDate As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select count(LCB_ID) From Lgst_CustomerBilling where LCB_YearID=" & iYearId & "")
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
            sStr = "INV-" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomerDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select BM_SubGL,BM_GL,BM_GSTNRegNo,BM_Address1,BM_GSTNCategory From Sales_Buyers_Masters Where BM_ID=" & iCustomerID & " And BM_CompID=" & iCompID & " "
            GetCustomerDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCustomerDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChartOfAccountHead(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGL As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccHead As Integer = 0
        Try
            sSql = "Select gl_AccHead from Chart_of_Accounts where gl_id =" & iGL & " and gl_CompID =" & iCompID & ""
            iAccHead = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iAccHead
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Cust_FinalNo,Cust_comm_address From MST_Customer_Master Where CUST_CompID=" & iCompID & " "
            GetCompanyDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetCompanyDetails
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
            GetGSTDescription = objDBL.SQLGetDescription(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCustomerBilling(ByVal sNameSpace As String, ByVal objLCB As clsCustBilling) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(38) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLCB.iLCB_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CustomerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLCB.iLCB_CustomerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_RouteID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLCB.iLCB_RouteID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_FromDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLCB.dLCB_FromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_ToDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLCB.dLCB_ToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_InvDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLCB.dLCB_InvDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_InvNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLCB.sLCB_InvNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CustOrderRef", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLCB.sLCB_CustOrderRef
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_Agreement", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLCB.sLCB_Agreement
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_TotalAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_TotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CompanyAddress", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objLCB.sLCB_CompanyAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CompanyGSTNRegNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objLCB.sLCB_CompanyGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CustomerAddress", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLCB.sLCB_CustomerAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CustomerGSTNRegNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objLCB.sLCB_CustomerGSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLCB.iLCB_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_GSTRate", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_GSTAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_GSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_SGST", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_SGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_SGSTAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_SGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CGST", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_CGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CGSTAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_CGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_IGST", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_IGST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_IGSTAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_IGSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_GSTCustBillStatus", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objLCB.sLCB_GSTCustBillStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_State", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objLCB.sLCB_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLCB.sLCB_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLCB.sLCB_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLCB.iLCB_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLCB.dLCB_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLCB.iLCB_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLCB.dLCB_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLCB.iLCB_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLCB.iLCB_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLCB.sLCB_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLCB.sLCB_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_EscAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_EscAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LCB_TotalEscAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLCB.dLCB_TotalEscAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_CustomerBilling", 1, Arr, ObjParam)
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
    Public Function BindCustomerTripDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iDispatchID As Integer, ByVal iAllocatedID As Integer) As DataTable
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
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("GSTID")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")
            dtTab.Columns.Add("NetAmount")

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
                    dRow("DiscountAmount") = ""
                    dRow("Charges") = ""
                    dRow("Amount") = ""
                    dRow("GSTID") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_GST_ID")))
                    dRow("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("DD_GSTRate")))
                    dRow("GSTAmount") = ""
                    dRow("NetAmount") = ""
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAmountDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoiceID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim dTrpAmt As Double = 0.0
        Dim dGstAmount As Double
        Dim dTotalAmount As Double

        Try
            dtTab.Columns.Add("TripAmount")
            dtTab.Columns.Add("TotalEscAmount")
            dtTab.Columns.Add("TotalTripAmount")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")
            dtTab.Columns.Add("SGST")
            dtTab.Columns.Add("SGSTAmount")
            dtTab.Columns.Add("CGST")
            dtTab.Columns.Add("CGSTAmount")
            dtTab.Columns.Add("IGST")
            dtTab.Columns.Add("IGSTAmount")
            dtTab.Columns.Add("GrandTotal")

            If iInvoiceID > 0 Then
                sSql = "" : sSql = "Select * From lgst_Customerbilling Where LCB_ID=" & iInvoiceID & " and LCB_CompID =" & iCompID & " And LCB_YearID =" & iYearID & ""
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    '    dTrpAmt = dt.Rows(i)("LCB_TotalAmt") - dt.Rows(i)("LCB_GSTAmount")
                    dRow("TripAmount") = Val(dt.Rows(i)("LCB_TotalAmt")) - Val(dt.Rows(i)("LCB_TotalEscAmt"))
                    dRow("TotalEscAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_TotalEscAmt")))
                    dRow("TotalTripAmount") = Val(dt.Rows(i)("LCB_TotalAmt")) + Val(dt.Rows(i)("LCB_TotalEscAmt"))
                    dRow("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_GSTRate")))
                    'dTotalAmount = (dRow("TotalTripAmount") * dRow("GSTRate"))
                    'dGstAmount = (dTotalAmount / 100)
                    'dRow("GSTAmount") = dGstAmount
                    dRow("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_GSTAmount")))
                    dRow("SGST") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_SGST")))
                    dRow("SGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_SGSTAmount")))
                    dRow("CGST") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_CGST")))
                    dRow("CGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_CGSTAmount")))
                    dRow("IGST") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_IGST")))
                    dRow("IGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("LCB_IGSTAmount")))
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(Val(dt.Rows(i)("LCB_TotalAmt")) + Val(dt.Rows(i)("LCB_GSTAmount"))))

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustBillingDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_CustomerBilling Where LCB_ID=" & iInvid & "  And LCB_CompID=" & iCompID & " and LCB_Yearid= " & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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
    Public Function LoadSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3 "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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
    Public Function GetStatusOfInvoiceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoiceID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select LCB_Status From lgst_customerBilling Where LCB_ID=" & iInvoiceID & " And LCB_CustomerID=" & iCustID & " And LCB_YearID=" & iYearID & " And LCB_CompID=" & iCompID & " "
            GetStatusOfInvoiceNo = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatusOfInvoiceNo
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
    Public Function GetCOAGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Desc = '" & sDesc & "' and gl_head=2 and gl_CompID=" & iCompID & ""
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
    Public Function GetPartySubGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer, ByVal iPartyID As Integer, ByVal sACM_Type As String) As Integer
        Dim sSql As String = ""
        Dim sGL As String = ""
        Dim sPartyName As String = ""
        Try
            'sSql = "" : sSql = "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_Type='" & sACM_Type & "' And ACM_Status='A' and ACM_CompID =" & iCompID & " "
            'sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select BM_NAme From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_DelFlag='A' And BM_CompID=" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_ID from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            GetPartySubGLID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPartySubGLID
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
            'sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select BM_NAme From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_DelFlag='A' And BM_CompID=" & iCompID & " "
            sPartyName = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "Select gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iGLID & " And gl_Desc='" & sPartyName & "' and gl_head=3"
            sGL = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUpdateTransactionDetails(ByVal sNameSpace As String, ByVal ObjCLB As clsCustBilling) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_ID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.dATD_TransactionDate)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_TrType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_BillId)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_PaymentType)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_Head)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_GL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_SubGL)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_DbOrCr)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.dATD_Debit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.dATD_Credit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_CreatedBy)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.sATD_Status)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_YearID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_CompID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.sATD_Operation)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.sATD_IPAddress)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_ZoneID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_RegionID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_AreaID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_BranchID)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.dATD_OpenDebit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.dATD_OpenCredit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.dATD_ClosingDebit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.dATD_ClosingCredit)
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objGen.SafeSQL(ObjCLB.iATD_SeqReferenceNum)
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
    Public Function LoadTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvID As Integer) As DataTable
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


            sSql = "" : sSql = "Select A.ATD_ID,A.ATD_Head,A.ATD_PaymentType,A.ATD_Gl,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
            sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iInvID & " and A.ATD_trType = 15 and "
            sSql = sSql & "A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_YearID=" & iYearID & " And A.ATD_BillId = " & iInvID & " order by a.Atd_id "


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
                        dr("Debit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ATD_Credit").ToString()) = False Then
                        dr("Credit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateInvStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvId As Integer, ByVal iYearId As Integer)
        Dim sSql As String
        Try
            sSql = "Update lgst_customerBilling set LCB_Status='A',LCB_Delflag='A' where LCB_Id=" & iInvId & " and LCB_Compid=" & iCompID & " And LCB_YearID =" & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdatePaymentMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iyearID As Integer)
        Dim sSql As String = ""
        Dim dt, dtDebitCredit As New DataTable
        Dim dOpnDebit, dOpnCredit, dClosingDebit, dClosingCredit As Double
        Dim iSequenceNum As Integer
        Try
            sSql = "" : sSql = "Update lgst_customerBilling Set LCB_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " LCB_Status='A',LCB_Delflag='A'"
            ElseIf sStatus = "D" Then
                sSql = sSql & " LCB_Status='D',LCB_DeletedBy= " & iUserID & ",LCB_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " LCB_Status='A' "
            End If
            sSql = sSql & " Where LCB_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)



            dt = objDBL.SQLExecuteDataSet(sNameSpace, "Select * From Acc_Transactions_Details Where ATD_BillID=" & iMasId & " And ATD_TrType=15 And ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " ").Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "" : sSql = "Select Count(Atd_id) from Acc_Transactions_Details where atd_gl=" & dt.Rows(i)("ATD_GL") & " and ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0"
                    Dim iCountGl As Integer = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                    sSql = "" : sSql = "Select Count(Atd_id) from Acc_Transactions_Details where  ATD_YearID =" & iyearID & " and ATD_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0"
                    iSequenceNum = objDBL.SQLExecuteScalar(sNameSpace, sSql)
                    iSequenceNum = iSequenceNum + 1
                    If iCountGl = 0 Then

                        sSql = "" : sSql = "Select Opn_CreditAmount,Opn_DebitAmt from acc_opening_balance where Opn_glId=" & dt.Rows(i)("ATD_GL") & " and Opn_YearID =" & iyearID & " and Opn_CompID=" & iCompID & ""
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
                        sSql = "" : sSql = "Select top 1  Atd_ClosingCredit,ATD_ClosingDebit from Acc_Transactions_Details where atd_gl=" & dt.Rows(i)("ATD_GL") & " and Atd_YearID =" & iyearID & " and Atd_CompID=" & iCompID & " and ATD_SeqReferenceNum <> 0 order by ATD_SeqReferenceNum desc"
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
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadCustomerBillingDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Dim sVehicleType As String = ""
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("InvoiceDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Route", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Lgst_CustomerBilling where LCB_CompID =" & iCompID & " and LCB_YearID= " & iYearID & ""
            If iStatus = 0 Then
                sSql = sSql & " And LCB_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And LCB_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And LCB_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By LCB_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("LCB_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("LCB_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LCB_CustomerID").ToString()) = False Then
                        dr("Customer") = GetCustomerName(sNameSpace, ds.Tables(0).Rows(i)("LCB_CustomerID").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LCB_InvNo").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("LCB_InvNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LCB_InvDate").ToString()) = False Then
                        dr("InvoiceDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("LCB_InvDate").ToString(), "D")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LCB_RouteID").ToString()) = False Then
                        dr("Route") = GetRouteName(sNameSpace, ds.Tables(0).Rows(i)("LCB_RouteID").ToString())
                    End If


                    If (ds.Tables(0).Rows(i)("LCB_DelFlag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("LCB_DelFlag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("LCB_DelFlag") = "D") Then
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
    Public Function GetCustomerName(ByVal sNameSpace As String, ByVal iCustID As Integer) As String
        Dim sSQL As String = ""
        Dim sCustomerName As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select BM_Name  from Sales_Buyers_Masters where BM_ID = " & iCustID & " "
            'sSQL = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("BM_Name").ToString()) = False Then
                    sCustomerName = dt.Rows(0)("BM_Name").ToString()
                Else
                    sCustomerName = ""
                End If
            Else
                sCustomerName = ""
            End If
            Return sCustomerName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateCustomerBillingMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_CustomerBilling Set LCB_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " LCB_Status='A',LCB_DelFlag='A'"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " LCB_Status='D',LCB_DelFlag='D'"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " LCB_Status='A',LCB_DelFlag='A' "
            End If
            sSql = sSql & " Where LCB_CompID=" & iCompID & " And LCB_ID = " & iMasId & " and LCB_YearID= " & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateCustomerBillingStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Lgst_CustomerBilling Where LCB_DelFlag='" & sSelectedStatus & "' And LCB_CompID=" & iCompID & " and LCB_YearID= " & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Lgst_CustomerBilling Set LCB_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " LCB_Status='A',LCB_DelFlag='A'"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " LCB_Status='D',LCB_DelFlag='D'"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " LCB_Status='A',LCB_DelFlag='A' "
                    End If
                    sSql = sSql & " Where LCB_CompID=" & iCompID & " And LCB_ID = " & iMasId & " and LCB_YearID= " & iYearID & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub


    Public Function GetRouteCustCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iRouteID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCount As Integer
        Try
            sSql = "select count(*) from lgst_customerBilling where LCB_CustomerID=" & iCustID & " and LCB_RouteID=" & iRouteID & " and LCB_Compid=" & iCompID & " and LCB_YearId=" & iYearID & ""
            iCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRouteCustdate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iRouteID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sDate As String = ""

        Try

            sSql = "" : sSql = "Select Top 1 LCB_ToDate From lgst_customerBilling where LCB_CustomerID=" & iCustID & " and LCB_RouteID=" & iRouteID & " and LCB_Compid=" & iCompID & " and LCB_YearId=" & iYearID & " order by lcb_id desc"
            sDate = objDBL.SQLGetDescription(sNameSpace, sSql)

            sSql = "" : sSql = "SELECT DATEADD(day, 1, '" & objGen.FormatDtForRDBMS(sDate, "CT") & "')"
            GetRouteCustdate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetRouteCustdate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRouteName(ByVal sNameSpace As String, ByVal iRouteID As Integer) As String
        Dim sSQL As String = ""
        Dim sRouteName As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LRM_StartDestPlace  from lgst_route_master where LRM_ID = " & iRouteID & " and LRM_Delflag='A' "
            'sSQL = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LRM_StartDestPlace").ToString()) = False Then
                    sRouteName = dt.Rows(0)("LRM_StartDestPlace").ToString()
                Else
                    sRouteName = ""
                End If
            Else
                sRouteName = ""
            End If
            Return sRouteName
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetTotalTripDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sFromDate As String, ByVal sToDate As String, ByVal iCustomerId As Integer, ByVal sRouteID As String, ByVal dEscAmt As Double)
        Dim sSql As String
        Dim dtotal As Double = 0.00
        Dim dt As New DataTable
        Dim dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sBillStatus As String = ""
        Dim dGtotal As Double = 0.00
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ID")
            dt.Columns.Add("TripNo")
            dt.Columns.Add("ContractAmount")
            dt.Columns.Add("EscAmt")
            dt.Columns.Add("TripRateAmount")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("SGST")
            dt.Columns.Add("SGSTAmount")
            dt.Columns.Add("CGST")
            dt.Columns.Add("CGSTAmount")
            dt.Columns.Add("IGST")
            dt.Columns.Add("IGSTAmount")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("GrandTotal")
            sSql = "" : sSql = "Select * from Lgst_TripGeneration_Master where LTGM_DestinationCustomer=" & iCustomerId & " and LTGM_Status='A' and LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearID & " and LTGM_RouteID in (" & sRouteID & ") and LTGM_TripStatus=2 "
            If (sFromDate <> "01/01/1900") And (sToDate <> "01-01-1900") Then
                sSql = sSql & " and LTGM_StopDate Between '" & sFromDate & "' And '" & sToDate & "'  "
            End If
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    dRow("ID") = dtDetails.Rows(i)("LTGM_ID")
                    dRow("TripNo") = dtDetails.Rows(i)("LTGM_TransactionNo")
                    dRow("ContractAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(0).Item("LTGM_Rate").ToString()))
                    dRow("EscAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dEscAmt))
                    dGtotal = dEscAmt + String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(0).Item("LTGM_Rate").ToString()))
                    dRow("TripRateAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dEscAmt + String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(0).Item("LTGM_Rate").ToString()))))
                    dRow("GSTRate") = dtDetails.Rows(i)("LTGM_GSTRate")
                    sBillStatus = dtDetails.Rows(i)("LTGM_GSTCustBillStatus")
                    dtotal = Val((dtDetails.Rows(i)("LTGM_GSTRate") * (dtDetails.Rows(0).Item("LTGM_Rate") + dEscAmt) / 100))
                    If sBillStatus = "Local" Then
                        dRow("SGST") = (dRow("GSTRate") / 2)
                        dRow("SGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dtotal / 2)))
                        dRow("CGST") = (dRow("GSTRate") / 2)
                        dRow("CGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dtotal / 2)))
                        dRow("IGST") = 0
                        dRow("IGSTAmount") = 0.0
                    ElseIf sBillStatus = "Inter State" Then
                        dRow("SGST") = 0
                        dRow("SGSTAmount") = 0.0
                        dRow("CGST") = 0
                        dRow("CGSTAmount") = 0.0
                        dRow("IGST") = dRow("GSTRate")
                        dRow("IGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dtotal))
                    End If
                    dRow("GSTAmount") = dtotal
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(dGtotal + dtotal))
                    dtotal = 0
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
        End Try
    End Function

    Public Sub UpdateTripMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvId As Integer, ByVal iYearId As Integer)
        Dim sSql As String
        Try
            sSql = "Update lgst_customerBilling set LCB_Status='A',LCB_Delflag='A' where LCB_Id=" & iInvId & " and LCB_Compid=" & iCompID & " And LCB_YearID =" & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
