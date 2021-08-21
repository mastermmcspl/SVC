Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsCompanyMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Private objGen As New clsGeneralFunctions
    Private sCUST_NAME As String
    Private sCUST_CODE As String
    Private sCUST_EMAIL As String
    Private iCUST_INDTYPEID As Integer
    Private sCUST_COMM_ADDRESS As String
    Private sCUST_COMM_CITY As String
    Private sCUST_COMM_PIN As String
    Private sCUST_COMM_STATE As String
    Private sCUST_COMM_COUNTRY As String
    Private sCUST_COMM_FAX As String
    Private sCUST_COMM_TEL As String
    Private sCUST_ADDRESS As String
    Private sCUST_CITY As String
    Private sCUST_PIN As String
    Private sCUST_STATE As String
    Private sCUST_COUNTRY As String
    Private sCUST_FAX As String
    Private sCUST_TELPHONE As String
    Private sCUST_STATUS As String
    Private sCUST_DELFLG As String
    Private dCUST_CRON As Date
    Private iCUST_CRBY As Integer
    Private iCust_SaleType As Integer
    Private iCUST_CompID As Integer
    Private iCUST_UpdatedBy As Integer
    Private dCUST_UpdatedOn As Date
    Private sCUST_Operation As String
    Private sCUST_IPAddress As String
    Private sCUST_COMM_PhFirst As String
    Private sCUST_COMM_PhSecond As String
    Private sCUST_PhFirst As String
    Private sCUST_PhSecond As String
    Private iCUST_ID As Integer
    Private iCUSTB_CUST_ID As Integer
    Private sCUSTB_NAME As String
    Private sCUSTB_ContactPerson As String
    Private sCUSTB_CITY As String
    Private sCUSTB_STATE As String
    Private sCUSTB_COUNTRY As String
    Private sCUSTB_PIN As String
    Private sCUSTB_TELPHONE As String
    Private sCUSTB_FAX As String
    Private sCUSTB_ADDRESS As String
    Private sCUSTB_STATUS As String
    Private sCUSTB_DELFLG As String
    Private iCUSTB_CompID As Integer
    Private iCUSTB_CRBY As Integer
    Private dCUSTB_CRON As Date
    Private iCUSTB_UpdatedBy As Integer
    Private dCUSTB_UpdatedOn As Date
    Private sCUSTB_Operation As String
    Private sCUSTB_IPAddress As String
    Private iCUSTB_ID As Integer
    Private iCUSTB_CompanyType As Integer
    Private iCUSTB_GSTNCategory As Integer
    Private sCUSTB_GSTNRegNo As String

    Private iCUST_TAXPayableCategory As Integer
    Private iCUST_GSTRForm As Integer
    Private iCUST_Periodicity As Integer
    Private sCUST_ProvisionalNo As String
    Private sCUST_FinalNo As String

    Private iBD_ID As Integer
    Private iBD_CUSTID As Integer
    Private iBD_BranchID As Integer
    Private sBD_BankName As String
    'Private iBD_AccountNo As Integer
    Private iBD_AccountNo As Int64
    Private sBD_IFSCCode As String
    Private sBD_BranchName As String
    Private sBD_DelFlag As String
    Private iBD_CompID As Integer
    Private iBD_CreatedBy As Integer
    Private dBD_CreatedOn As Date
    Private iBD_UpdatedBy As Integer
    Private dBD_UpdatedOn As Date
    Private sBD_Opeartion As String
    Private sBD_IPAddress As String
    Private iBD_YearID As Integer

    Public Property BD_YearID() As Integer
        Get
            Return (iBD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iBD_YearID = Value
        End Set
    End Property

    Public Property BD_ID() As Integer
        Get
            Return (iBD_ID)
        End Get
        Set(ByVal Value As Integer)
            iBD_ID = Value
        End Set
    End Property
    Public Property BD_CUSTID() As Integer
        Get
            Return (iBD_CUSTID)
        End Get
        Set(ByVal Value As Integer)
            iBD_CUSTID = Value
        End Set
    End Property
    Public Property BD_BranchID() As Integer
        Get
            Return (iBD_BranchID)
        End Get
        Set(ByVal Value As Integer)
            iBD_BranchID = Value
        End Set
    End Property
    Public Property BD_BankName() As String
        Get
            Return (sBD_BankName)
        End Get
        Set(ByVal Value As String)
            sBD_BankName = Value
        End Set
    End Property
    'Public Property BD_AccountNo() As Integer
    '    Get
    '        Return (iBD_AccountNo)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        iBD_AccountNo = Value
    '    End Set
    'End Property
    Public Property BD_AccountNo() As Int64
        Get
            Return (iBD_AccountNo)
        End Get
        Set(ByVal Value As Int64)
            iBD_AccountNo = Value
        End Set
    End Property
    Public Property BD_IFSCCode() As String
        Get
            Return (sBD_IFSCCode)
        End Get
        Set(ByVal Value As String)
            sBD_IFSCCode = Value
        End Set
    End Property
    Public Property BD_BranchName() As String
        Get
            Return (sBD_BranchName)
        End Get
        Set(ByVal Value As String)
            sBD_BranchName = Value
        End Set
    End Property
    Public Property BD_DelFlag() As String
        Get
            Return (sBD_DelFlag)
        End Get
        Set(ByVal Value As String)
            sBD_DelFlag = Value
        End Set
    End Property
    Public Property BD_CompID() As Integer
        Get
            Return (iBD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iBD_CompID = Value
        End Set
    End Property

    Public Property BD_CreatedBy() As Integer
        Get
            Return (iBD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iBD_CreatedBy = Value
        End Set
    End Property
    Public Property BD_CreatedOn() As DateTime
        Get
            Return (dBD_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dBD_CreatedOn = Value
        End Set
    End Property
    Public Property BD_UpdatedBy() As Integer
        Get
            Return (iBD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iBD_UpdatedBy = Value
        End Set
    End Property
    Public Property BD_UpdatedOn() As DateTime
        Get
            Return (dBD_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dBD_UpdatedOn = Value
        End Set
    End Property
    Public Property BD_Opeartion() As String
        Get
            Return (sBD_Opeartion)
        End Get
        Set(ByVal Value As String)
            sBD_Opeartion = Value
        End Set
    End Property
    Public Property BD_IPAddress() As String
        Get
            Return (sBD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sBD_IPAddress = Value
        End Set
    End Property

    Public Property CUST_TAXPayableCategory() As Integer
        Get
            Return (iCUST_TAXPayableCategory)
        End Get
        Set(ByVal Value As Integer)
            iCUST_TAXPayableCategory = Value
        End Set
    End Property
    Public Property CUST_GSTRForm() As Integer
        Get
            Return (iCUST_GSTRForm)
        End Get
        Set(ByVal Value As Integer)
            iCUST_GSTRForm = Value
        End Set
    End Property
    Public Property CUST_Periodicity() As Integer
        Get
            Return (iCUST_Periodicity)
        End Get
        Set(ByVal Value As Integer)
            iCUST_Periodicity = Value
        End Set
    End Property
    Public Property CUST_ProvisionalNo() As String
        Get
            Return (sCUST_ProvisionalNo)
        End Get
        Set(ByVal Value As String)
            sCUST_ProvisionalNo = Value
        End Set
    End Property
    Public Property CUST_FinalNo() As String
        Get
            Return (sCUST_FinalNo)
        End Get
        Set(ByVal Value As String)
            sCUST_FinalNo = Value
        End Set
    End Property

    Public Property CUSTB_ID() As Integer
        Get
            Return (iCUSTB_ID)
        End Get
        Set(ByVal Value As Integer)
            iCUSTB_ID = Value
        End Set
    End Property
    Public Property CUSTB_IPAddress() As String
        Get
            Return (sCUSTB_IPAddress)
        End Get
        Set(ByVal Value As String)
            sCUSTB_IPAddress = Value
        End Set
    End Property
    Public Property CUSTB_Operation() As String
        Get
            Return (sCUSTB_Operation)
        End Get
        Set(ByVal Value As String)
            sCUSTB_Operation = Value
        End Set
    End Property
    Public Property CUSTB_UpdatedOn() As Date
        Get
            Return (dCUSTB_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dCUSTB_UpdatedOn = Value
        End Set
    End Property
    Public Property CUSTB_UpdatedBy() As Integer
        Get
            Return (iCUSTB_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCUSTB_UpdatedBy = Value
        End Set
    End Property
    Public Property CUSTB_CRON() As Date
        Get
            Return (dCUSTB_CRON)
        End Get
        Set(ByVal Value As Date)
            dCUSTB_CRON = Value
        End Set
    End Property
    Public Property CUSTB_CRBY() As Integer
        Get
            Return (iCUSTB_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iCUSTB_CRBY = Value
        End Set
    End Property
    Public Property CUSTB_CompID() As Integer
        Get
            Return (iCUSTB_CompID)
        End Get
        Set(ByVal Value As Integer)
            iCUSTB_CompID = Value
        End Set
    End Property
    Public Property CUSTB_DELFLG() As String
        Get
            Return (sCUSTB_DELFLG)
        End Get
        Set(ByVal Value As String)
            sCUSTB_DELFLG = Value
        End Set
    End Property
    Public Property CUSTB_STATUS() As String
        Get
            Return (sCUSTB_STATUS)
        End Get
        Set(ByVal Value As String)
            sCUSTB_STATUS = Value
        End Set
    End Property
    Public Property CUSTB_ADDRESS() As String
        Get
            Return (sCUSTB_ADDRESS)
        End Get
        Set(ByVal Value As String)
            sCUSTB_ADDRESS = Value
        End Set
    End Property
    Public Property CUSTB_FAX() As String
        Get
            Return (sCUSTB_FAX)
        End Get
        Set(ByVal Value As String)
            sCUSTB_FAX = Value
        End Set
    End Property
    Public Property CUSTB_TELPHONE() As String
        Get
            Return (sCUSTB_TELPHONE)
        End Get
        Set(ByVal Value As String)
            sCUSTB_TELPHONE = Value
        End Set
    End Property
    Public Property CUSTB_PIN() As String
        Get
            Return (sCUSTB_PIN)
        End Get
        Set(ByVal Value As String)
            sCUSTB_PIN = Value
        End Set
    End Property

    Public Property CUSTB_COUNTRY() As String
        Get
            Return (sCUSTB_COUNTRY)
        End Get
        Set(ByVal Value As String)
            sCUSTB_COUNTRY = Value
        End Set
    End Property
    Public Property CUSTB_STATE() As String
        Get
            Return (sCUSTB_STATE)
        End Get
        Set(ByVal Value As String)
            sCUSTB_STATE = Value
        End Set
    End Property

    Public Property CUSTB_CITY() As String
        Get
            Return (sCUSTB_CITY)
        End Get
        Set(ByVal Value As String)
            sCUSTB_CITY = Value
        End Set
    End Property

    Public Property CUSTB_ContactPerson() As String
        Get
            Return (sCUSTB_ContactPerson)
        End Get
        Set(ByVal Value As String)
            sCUSTB_ContactPerson = Value
        End Set
    End Property

    Public Property CUSTB_NAME() As String
        Get
            Return (sCUSTB_NAME)
        End Get
        Set(ByVal Value As String)
            sCUSTB_NAME = Value
        End Set
    End Property
    Public Property CUSTB_CUST_ID() As Integer
        Get
            Return (iCUSTB_CUST_ID)
        End Get
        Set(ByVal Value As Integer)
            iCUSTB_CUST_ID = Value
        End Set
    End Property

    Public Property CUST_PhSecond() As String
        Get
            Return (sCUST_PhSecond)
        End Get
        Set(ByVal Value As String)
            sCUST_PhSecond = Value
        End Set
    End Property
    Public Property CUST_PhFirst() As String
        Get
            Return (sCUST_PhFirst)
        End Get
        Set(ByVal Value As String)
            sCUST_PhFirst = Value
        End Set
    End Property
    Public Property CUST_COMM_PhSecond() As String
        Get
            Return (sCUST_COMM_PhSecond)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_PhSecond = Value
        End Set
    End Property
    Public Property CUST_COMM_PhFirst() As String
        Get
            Return (sCUST_COMM_PhFirst)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_PhFirst = Value
        End Set
    End Property
    Public Property CUST_IPAddress() As String
        Get
            Return (sCUST_IPAddress)
        End Get
        Set(ByVal Value As String)
            sCUST_IPAddress = Value
        End Set
    End Property
    Public Property CUST_Operation() As String
        Get
            Return (sCUST_Operation)
        End Get
        Set(ByVal Value As String)
            sCUST_Operation = Value
        End Set
    End Property
    Public Property CUST_UpdatedOn() As Date
        Get
            Return (dCUST_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dCUST_UpdatedOn = Value
        End Set
    End Property
    Public Property CUST_UpdatedBy() As Integer
        Get
            Return (iCUST_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCUST_UpdatedBy = Value
        End Set
    End Property
    Public Property CUST_CompID() As Integer
        Get
            Return (iCUST_CompID)
        End Get
        Set(ByVal Value As Integer)
            iCUST_CompID = Value
        End Set
    End Property
    Public Property Cust_SaleType() As Integer
        Get
            Return (iCust_SaleType)
        End Get
        Set(ByVal Value As Integer)
            iCust_SaleType = Value
        End Set
    End Property
    Public Property CUST_CRBY() As Integer
        Get
            Return (iCUST_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iCUST_CRBY = Value
        End Set
    End Property
    Public Property CUST_CRON() As Date
        Get
            Return (dCUST_CRON)
        End Get
        Set(ByVal Value As Date)
            dCUST_CRON = Value
        End Set
    End Property
    Public Property CUST_DELFLG() As String
        Get
            Return (sCUST_DELFLG)
        End Get
        Set(ByVal Value As String)
            sCUST_DELFLG = Value
        End Set
    End Property
    Public Property CUST_STATUS() As String
        Get
            Return (sCUST_STATUS)
        End Get
        Set(ByVal Value As String)
            sCUST_STATUS = Value
        End Set
    End Property
    Public Property CUST_TELPHONE() As String
        Get
            Return (sCUST_TELPHONE)
        End Get
        Set(ByVal Value As String)
            sCUST_TELPHONE = Value
        End Set
    End Property
    Public Property CUST_FAX() As String
        Get
            Return (sCUST_FAX)
        End Get
        Set(ByVal Value As String)
            sCUST_FAX = Value
        End Set
    End Property
    Public Property CUST_COUNTRY() As String
        Get
            Return (sCUST_COUNTRY)
        End Get
        Set(ByVal Value As String)
            sCUST_COUNTRY = Value
        End Set
    End Property
    Public Property CUST_STATE() As String
        Get
            Return (sCUST_STATE)
        End Get
        Set(ByVal Value As String)
            sCUST_STATE = Value
        End Set
    End Property
    Public Property CUST_PIN() As String
        Get
            Return (sCUST_PIN)
        End Get
        Set(ByVal Value As String)
            sCUST_PIN = Value
        End Set
    End Property
    Public Property CUST_CITY() As String
        Get
            Return (sCUST_CITY)
        End Get
        Set(ByVal Value As String)
            sCUST_CITY = Value
        End Set
    End Property
    Public Property CUST_ADDRESS() As String
        Get
            Return (sCUST_ADDRESS)
        End Get
        Set(ByVal Value As String)
            sCUST_ADDRESS = Value
        End Set
    End Property
    Public Property CUST_COMM_TEL() As String
        Get
            Return (sCUST_COMM_TEL)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_TEL = Value
        End Set
    End Property
    Public Property CUST_COMM_FAX() As String
        Get
            Return (sCUST_COMM_FAX)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_FAX = Value
        End Set
    End Property
    Public Property CUST_COMM_COUNTRY() As String
        Get
            Return (sCUST_COMM_COUNTRY)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_COUNTRY = Value
        End Set
    End Property
    Public Property CUST_COMM_STATE() As String
        Get
            Return (sCUST_COMM_STATE)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_STATE = Value
        End Set
    End Property

    Public Property CUST_COMM_PIN() As String
        Get
            Return (sCUST_COMM_PIN)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_PIN = Value
        End Set
    End Property
    Public Property CUST_COMM_CITY() As String
        Get
            Return (sCUST_COMM_CITY)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_CITY = Value
        End Set
    End Property
    Public Property CUST_COMM_ADDRESS() As String
        Get
            Return (sCUST_COMM_ADDRESS)
        End Get
        Set(ByVal Value As String)
            sCUST_COMM_ADDRESS = Value
        End Set
    End Property
    Public Property CUST_INDTYPEID() As Integer
        Get
            Return (iCUST_INDTYPEID)
        End Get
        Set(ByVal Value As Integer)
            iCUST_INDTYPEID = Value
        End Set
    End Property
    Public Property CUST_EMAIL() As String
        Get
            Return (sCUST_EMAIL)
        End Get
        Set(ByVal Value As String)
            sCUST_EMAIL = Value
        End Set
    End Property
    Public Property CUST_CODE() As String
        Get
            Return (sCUST_CODE)
        End Get
        Set(ByVal Value As String)
            sCUST_CODE = Value
        End Set
    End Property
    Public Property CUST_NAME() As String
        Get
            Return (sCUST_NAME)
        End Get
        Set(ByVal Value As String)
            sCUST_NAME = Value
        End Set
    End Property

    Public Property CUSTB_CompanyType() As Integer
        Get
            Return (iCUSTB_CompanyType)
        End Get
        Set(ByVal Value As Integer)
            iCUSTB_CompanyType = Value
        End Set
    End Property
    Public Property CUSTB_GSTNCategory() As Integer
        Get
            Return (iCUSTB_GSTNCategory)
        End Get
        Set(ByVal Value As Integer)
            iCUSTB_GSTNCategory = Value
        End Set
    End Property
    Public Property CUSTB_GSTNRegNo() As String
        Get
            Return (sCUSTB_GSTNRegNo)
        End Get
        Set(ByVal Value As String)
            sCUSTB_GSTNRegNo = Value
        End Set
    End Property
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
    Public Function LoadCity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 3 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadState(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 4 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCountry(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Mas_Id,Mas_Desc from ACC_General_Master where mas_master = 5 and Mas_Delflag ='A' and mas_CompID =" & iCompID & " Order by Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCompanyNameAndCode(ByVal sNameSpace As String, ByVal sTableName As String, ByVal sColumn As String) As String
        Dim sSql As String = "", sResult As String = ""
        Try
            sSql = "" : sSql = "Select " & sColumn & " from " & sTableName & ""
            sResult = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sResult
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCumsterDetails(ByVal sNameSpace As String, ByVal objCumster As clsCompanyMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(39) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCUST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_NAME", OleDb.OleDbType.VarChar, 150)
            ObjParam(iParamCount).Value = objCumster.sCUST_NAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_CODE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_EMAIL", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_EMAIL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_INDTYPEID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCUST_INDTYPEID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_ADDRESS", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_ADDRESS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_CITY", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_CITY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_PIN", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_PIN
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_STATE", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_STATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_COUNTRY", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_COUNTRY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_FAX", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_FAX
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_TEL", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_TEL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_ADDRESS", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCumster.sCUST_ADDRESS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_CITY", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCumster.sCUST_CITY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_PIN", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objCumster.sCUST_PIN
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_STATE", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCumster.sCUST_STATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COUNTRY", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCumster.sCUST_COUNTRY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_FAX", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_FAX
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_TELPHONE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_TELPHONE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_STATUS", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objCumster.sCUST_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objCumster.sCUST_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_CRON", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objCumster.dCUST_CRON
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCUST_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Cust_SaleType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCust_SaleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCUST_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCUST_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objCumster.dCUST_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_Operation", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCumster.sCUST_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCumster.sCUST_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_PhFirst", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_PhFirst
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_COMM_PhSecond", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_COMM_PhSecond
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_PhFirst", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_PhFirst
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_PhSecond", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sCUST_PhSecond
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_TAXPayableCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCUST_TAXPayableCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_GSTRForm", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCUST_GSTRForm
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_Periodicity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iCUST_Periodicity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_ProvisionalNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCumster.sCUST_ProvisionalNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUST_FinalNo", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCumster.sCUST_FinalNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spMSTCustomerMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateCompanyName(ByVal sNameSpace As String, ByVal sCMName As String)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Update Sad_CompanyMaster_Settings set Sad_CMS_Name = '" & sCMName & "'"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from MST_Customer_Master where Cust_Code = '" & sCode & "' and Cust_Delflg <> 'D' and CUST_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sName As String) As String
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select Cmp_PKID From Company_Accounting_Template Where Cmp_ID=" & iCompID & " And Cmp_Desc='" & sName & "'"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckValue = dr("Cmp_PKID")
            Else
                CheckValue = "0"
            End If
            dr.Close()
            Return CheckValue
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub SaveOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sName As String, ByVal sValue As String, ByVal iID As Integer)
        Dim sSql As String
        Dim iMaxID As Integer
        Try
            If iID = 0 Then
                iMaxID = objGen.GetMaxID(sNameSpace, iCompID, "Company_Accounting_Template", "Cmp_PKID", "Cmp_ID")
                sSql = "" : sSql = "Insert Into Company_Accounting_Template (Cmp_PKID,Cmp_Desc,Cmp_Value,Cmp_ID,Cmp_Status) values"
                sSql = sSql & "(" & iMaxID & ",'" & sName & "','" & sValue & "'," & iCompID & ",'W')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            ElseIf iID > 0 Then
                sSql = "" : sSql = "Update Company_Accounting_Template Set Cmp_Desc= '" & sName & "',Cmp_Value='" & sValue & "',Cmp_Status='U'"
                sSql = sSql & "Where Cmp_ID = " & iCompID & " And Cmp_PKID=  " & iID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("Name")
            dt.Columns.Add("Value")

            sSql = "" : sSql = "Select Cmp_PKID,Cmp_Desc,Cmp_Value,Cmp_Status,Cmp_ID from Company_Accounting_Template Where Cmp_ID=" & iCompID & " Order by Cmp_Desc"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    If IsDBNull(dr("Cmp_PKID")) = False Then
                        dRow("ID") = dr("Cmp_PKID")
                    End If

                    If IsDBNull(dr("Cmp_Desc")) = False Then
                        dRow("Name") = dr("Cmp_Desc")
                    End If

                    If IsDBNull(dr("Cmp_Value")) = False Then
                        dRow("Value") = dr("Cmp_Value")
                    End If

                    dt.Rows.Add(dRow)
                End While
            End If
            dr.Close()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub DeleteOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Delete From Company_Accounting_Template Where Cmp_ID =" & iCompID & " And Cmp_PKID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadBranchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBranchID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("Branch_ID")
            dt.Columns.Add("Branch")
            dt.Columns.Add("ContactPerson")
            dt.Columns.Add("Address")
            dt.Columns.Add("City")
            dt.Columns.Add("State")
            dt.Columns.Add("PinCode")

            sSql = "Select * from MST_CUSTOMER_MASTER_Branch where CUSTB_CUST_ID=" & iCompID & " And CUSTB_CompID =" & iCompID & " "
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow

                    If IsDBNull(dr("CUSTB_ID")) = False Then
                        dRow("Branch_ID") = dr("CUSTB_ID")
                    End If

                    If IsDBNull(dr("CUSTB_Name")) = False Then
                        dRow("Branch") = objDBL.SQLGetDescription(sNameSpace, "Select Org_Name From Sad_Org_Structure Where Org_Node=" & dr("CUSTB_Name") & " And Org_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(dr("CUSTB_ContactPerson")) = False Then
                        dRow("ContactPerson") = dr("CUSTB_ContactPerson")
                    End If

                    If IsDBNull(dr("CUSTB_Address")) = False Then
                        dRow("Address") = dr("CUSTB_Address")
                    End If

                    If IsDBNull(dr("CUSTB_City")) = False Then
                        dRow("City") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc from ACC_General_Master where Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='City' and Mas_DelFlag='X') and Mas_Delflag ='A' And Mas_ID=" & dr("CUSTB_City") & "")
                    End If

                    If IsDBNull(dr("CUSTB_State")) = False Then
                        dRow("State") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc from ACC_General_Master where Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='State' and Mas_DelFlag='X') and Mas_Delflag ='A' And Mas_ID=" & dr("CUSTB_State") & "")
                    End If

                    If IsDBNull(dr("CUSTB_Pin")) = False Then
                        dRow("PinCode") = dr("CUSTB_Pin")
                    End If
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetBranchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBranchID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            'sSql = "Select * from MST_CUSTOMER_MASTER_Branch where CUSTB_CUST_ID=" & iCompID & " And CUSTB_ID=" & iBranchID & " And CUSTB_CompID =" & iCompID & " "
            sSql = "Select * from MST_CUSTOMER_MASTER_Branch where CUSTB_CUST_ID=" & iCompID & " And CUSTB_Name=" & iBranchID & " And CUSTB_CompID =" & iCompID & " "
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            ' dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveCumsterBranchDetails(ByVal sNameSpace As String, ByVal objCusterBranch As clsCompanyMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(24) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCusterBranch.iCUSTB_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_CUST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCusterBranch.iCUSTB_CUST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_NAME", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_NAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_ContactPerson", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_CITY", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_CITY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_STATE", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_STATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_COUNTRY", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_COUNTRY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_PIN", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_PIN
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_TELPHONE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_TELPHONE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_FAX", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_FAX
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_ADDRESS", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_ADDRESS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_STATUS", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCusterBranch.iCUSTB_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCusterBranch.iCUSTB_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_CRON", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objCusterBranch.dCUSTB_CRON
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCusterBranch.iCUSTB_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objCusterBranch.dCUSTB_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_Operation", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_CompanyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCusterBranch.iCUSTB_CompanyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_GSTNCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCusterBranch.iCUSTB_GSTNCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CUSTB_GSTNRegNo", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCusterBranch.sCUSTB_GSTNRegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spMST_CUSTOMER_MASTER_Branch", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCompanyType As String) As DataTable
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
    Public Function LoadForms(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMaster As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from Acc_General_Forms_GST where Mas_Gen_ID=" & iMaster & " And Mas_CompID =" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPeriodicity(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMaster As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from Acc_General_Periodicity_GST where Mas_Gen_ID=" & iMaster & " And Mas_CompID =" & iCompID & " "
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
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where org_Parent in (Select Org_Node From Sad_Org_Structure Where Org_Parent=1 And org_CompID=" & iCompID & ")))"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBank_Details(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankName As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select * from Acc_Company_BankDetails Where BD_BankName=" & iBankName & " And BD_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBnkDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Dim dr As DataRow
        Dim ds As New DataSet
        Try
            dt.Columns.Add("BankID")
            dt.Columns.Add("BankName")
            dt.Columns.Add("AccountNo")
            dt.Columns.Add("IFSCCode")
            dt.Columns.Add("BranchName")

            sSql = "select * from Acc_Company_BankDetails where BD_CompID=" & iCompID & " order by BD_ID"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then '
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("BankID") = ds.Tables(0).Rows(i)("BD_ID")
                    'dr("BankName") = ds.Tables(0).Rows(i)("BD_BankName")
                    dr("BankName") = objDBL.SQLExecuteScalar(sNameSpace, "select gl_desc from chart_of_Accounts where gl_id=" & ds.Tables(0).Rows(i)("BD_BankName") & " and gl_CompId=" & iCompID & "")
                    dr("AccountNo") = ds.Tables(0).Rows(i)("BD_AccountNo")
                    dr("IFSCCode") = ds.Tables(0).Rows(i)("BD_IFSCCode")
                    dr("BranchName") = ds.Tables(0).Rows(i)("BD_BranchName")
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBanksName(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iAcc_GL As New Integer

        Try
            sSql = "select Acc_GL From Acc_Application_Settings Where Acc_Types='Bank' And Acc_LedgerType='Bank'"
            iAcc_GL = objDBL.SQLExecuteScalar(sNameSpace, sSql)

            sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_parent=" & iAcc_GL & " and gl_CompId=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavecomapanyBank_Details(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objCumster As clsCompanyMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iBD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_CUSTID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iBD_CUSTID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_BranchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iBD_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_BankName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sBD_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_AccountNo", OleDb.OleDbType.BigInt, 8)
            ObjParam(iParamCount).Value = objCumster.iBD_AccountNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_IFSCCode", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objCumster.sBD_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_BranchName", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCumster.sBD_BranchName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iBD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objCumster.dBD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iBD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objCumster.dBD_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objCumster.sBD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iBD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCumster.iBD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objCumster.sBD_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCumster.sBD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Company_BankDetails", 1, Arr, ObjParam)
            Return Arr
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
End Class
