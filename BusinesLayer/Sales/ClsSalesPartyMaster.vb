Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSalesPartyMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iCSM_ID As Integer
    Private iCSM_IndType As Integer
    Private sCSM_CustName As String
    Private sCSM_Name As String
    Private sCSM_Code As String
    Private iCSM_Inventry As Integer
    Private sCSM_ContactPerson As String

    Private sCSM_EmailID As String
    Private sCSM_MobileNo As String
    Private sCSM_LandLineNo As String
    Private sCSM_Fax As String
    Private sCSM_Address As String
    Private sCSM_Pincode As String
    Private iCSM_City As Integer
    Private iCSM_State As Integer

    Private sCSM_Delflag As String
    Private iCSM_CompID As Integer
    Private iCSM_YearID As Integer
    Private sCSM_Status As String
    Private sCSM_Operation As String
    Private sCSM_IPAddress As String
    Private iCSM_CreatedBy As Integer
    Private dCSM_CreatedOn As DateTime
    Private iCSM_ApprovedBy As Integer
    Private dCSM_ApprovedOn As DateTime
    Private iCSM_DeletedBy As Integer
    Private dCSM_DeletedOn As DateTime
    Private iCSM_UpdatedBy As Integer
    Private dCSM_UpdatedOn As DateTime

    Private iBM_Group As Integer
    Private iBM_SubGroup As Integer
    Private iBM_GL As Integer
    Private iBM_SubGL As Integer

    Private sCSM_Address1 As String
    Private sCSM_Address2 As String
    Private sCSM_Address3 As String
    Private iBM_GenCategory As Integer
    Public Property BM_GenCategory() As Integer
        Get
            Return (iBM_GenCategory)
        End Get
        Set(ByVal Value As Integer)
            iBM_GenCategory = Value
        End Set
    End Property
    Public Property CSM_Address1() As String
        Get
            Return (sCSM_Address1)
        End Get
        Set(ByVal Value As String)
            sCSM_Address1 = Value
        End Set
    End Property
    Public Property CSM_Address2() As String
        Get
            Return (sCSM_Address2)
        End Get
        Set(ByVal Value As String)
            sCSM_Address2 = Value
        End Set
    End Property
    Public Property CSM_Address3() As String
        Get
            Return (sCSM_Address3)
        End Get
        Set(ByVal Value As String)
            sCSM_Address3 = Value
        End Set
    End Property
    Public Property BM_GL() As Integer
        Get
            Return (iBM_GL)
        End Get
        Set(ByVal Value As Integer)
            iBM_GL = Value
        End Set
    End Property
    Public Property BM_SubGL() As Integer
        Get
            Return (iBM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iBM_SubGL = Value
        End Set
    End Property
    Public Property BM_SubGroup() As Integer
        Get
            Return (iBM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            iBM_SubGroup = Value
        End Set
    End Property
    Public Property BM_Group() As Integer
        Get
            Return (iBM_Group)
        End Get
        Set(ByVal Value As Integer)
            iBM_Group = Value
        End Set
    End Property
    Public Property CSM_City() As Integer
        Get
            Return (iCSM_City)
        End Get
        Set(ByVal Value As Integer)
            iCSM_City = Value
        End Set
    End Property
    Public Property CSM_Fax() As String
        Get
            Return (sCSM_Fax)
        End Get
        Set(ByVal Value As String)
            sCSM_Fax = Value
        End Set
    End Property
    Public Property CSM_LandLineNo() As String
        Get
            Return (sCSM_LandLineNo)
        End Get
        Set(ByVal Value As String)
            sCSM_LandLineNo = Value
        End Set
    End Property
    Public Property CSM_MobileNo() As String
        Get
            Return (sCSM_MobileNo)
        End Get
        Set(ByVal Value As String)
            sCSM_MobileNo = Value
        End Set
    End Property
    Public Property CSM_EmailID() As String
        Get
            Return (sCSM_EmailID)
        End Get
        Set(ByVal Value As String)
            sCSM_EmailID = Value
        End Set
    End Property
    Public Property CSM_ID() As Integer
        Get
            Return (iCSM_ID)
        End Get
        Set(ByVal Value As Integer)
            iCSM_ID = Value
        End Set
    End Property
    Public Property CSM_IndType() As Integer
        Get
            Return (iCSM_IndType)
        End Get
        Set(ByVal Value As Integer)
            iCSM_IndType = Value
        End Set
    End Property
    Public Property CSM_CustName() As String
        Get
            Return (sCSM_CustName)
        End Get
        Set(ByVal Value As String)
            sCSM_CustName = Value
        End Set
    End Property
    Public Property CSM_Name() As String
        Get
            Return (sCSM_Name)
        End Get
        Set(ByVal Value As String)
            sCSM_Name = Value
        End Set
    End Property
    Public Property CSM_Code() As String
        Get
            Return (sCSM_Code)
        End Get
        Set(ByVal Value As String)
            sCSM_Code = Value
        End Set
    End Property
    Public Property CSM_Inventry() As Integer
        Get
            Return (iCSM_Inventry)
        End Get
        Set(ByVal Value As Integer)
            iCSM_Inventry = Value
        End Set
    End Property
    Public Property CSM_ContactPerson() As String
        Get
            Return (sCSM_ContactPerson)
        End Get
        Set(ByVal Value As String)
            sCSM_ContactPerson = Value
        End Set
    End Property
    Public Property CSM_Address() As String
        Get
            Return (sCSM_Address)
        End Get
        Set(ByVal Value As String)
            sCSM_Address = Value
        End Set
    End Property
    Public Property CSM_State() As Integer
        Get
            Return (iCSM_State)
        End Get
        Set(ByVal Value As Integer)
            iCSM_State = Value
        End Set
    End Property
    Public Property CSM_Pincode() As String
        Get
            Return (sCSM_Pincode)
        End Get
        Set(ByVal Value As String)
            sCSM_Pincode = Value
        End Set
    End Property
    Public Property CSM_Delflag() As String
        Get
            Return (sCSM_Delflag)
        End Get
        Set(ByVal Value As String)
            sCSM_Delflag = Value
        End Set
    End Property
    Public Property CSM_CompID() As Integer
        Get
            Return (iCSM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iCSM_CompID = Value
        End Set
    End Property
    Public Property CSM_YearID() As Integer
        Get
            Return (iCSM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iCSM_YearID = Value
        End Set
    End Property
    Public Property CSM_Status() As String
        Get
            Return (sCSM_Status)
        End Get
        Set(ByVal Value As String)
            sCSM_Status = Value
        End Set
    End Property
    Public Property CSM_Operation() As String
        Get
            Return (sCSM_Operation)
        End Get
        Set(ByVal Value As String)
            sCSM_Operation = Value
        End Set
    End Property
    Public Property CSM_IPAddress() As String
        Get
            Return (sCSM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sCSM_IPAddress = Value
        End Set
    End Property
    Public Property CSM_CreatedBy() As Integer
        Get
            Return (iCSM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCSM_CreatedBy = Value
        End Set
    End Property
    Public Property CSM_CreatedOn() As DateTime
        Get
            Return (dCSM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dCSM_CreatedOn = Value
        End Set
    End Property

    Public Property CSM_ApprovedBy() As Integer
        Get
            Return (iCSM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iCSM_ApprovedBy = Value
        End Set
    End Property
    Public Property CSM_ApprovedOn() As DateTime
        Get
            Return (dCSM_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dCSM_ApprovedOn = Value
        End Set
    End Property

    Public Property CSM_DeletedBy() As Integer
        Get
            Return (iCSM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iCSM_DeletedBy = Value
        End Set
    End Property
    Public Property CSM_DeletedOn() As DateTime
        Get
            Return (dCSM_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dCSM_DeletedOn = Value
        End Set
    End Property
    Public Property CSM_UpdatedBy() As Integer
        Get
            Return (iCSM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCSM_UpdatedBy = Value
        End Set
    End Property
    Public Property CSM_UpdatedOn() As DateTime
        Get
            Return (dCSM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dCSM_UpdatedOn = Value
        End Set
    End Property
    Public Function LoadCity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='City' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCategory(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select * from chart_of_Accounts where gl_Parent = 584 and gl_AccHead = 1"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadState(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in(Select Mas_ID From Acc_Master_Type Where Mas_Type='State' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustomers(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Try
            sSql = "Select Cust_Name from MST_Customer_Master Where Cust_CompID =" & iCompID & ""
            sValue = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sValue
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPartyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCSMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Sales_Buyers_Masters Where BM_ID=" & iCSMid & "  And BM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetState(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Try
            sSql = "Select MAS_desc from ACC_General_Master  Where MAS_ID=" & iID & " And MAs_CompID= " & iCompID & " and Mas_Master = 4 and Mas_Delflag ='X'"
            sValue = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sValue
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePartyDetails(ByVal sNameSpace As String, ByVal objCMS As ClsSalesPartyMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(36) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_IndType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_IndType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCMS.sCSM_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Code", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objCMS.sCSM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Inventry", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_Inventry
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Address", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objCMS.sCSM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_State", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Pincode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCMS.sCSM_Pincode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objCMS.sCSM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objCMS.dCSM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_ApprovedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objCMS.dCSM_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_DeletedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_DeletedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_DeletedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objCMS.dCSM_DeletedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCMS.sCSM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objCMS.dCSM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_ContactPerson", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objCMS.sCSM_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_City ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_LandLineNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCMS.sCSM_LandLineNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_MobileNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCMS.sCSM_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_EmailID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCMS.sCSM_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Fax", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCMS.sCSM_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Year ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iCSM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            'ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Operation", OleDb.OleDbType.VarChar, 100)
            'ObjParam(iParamCount).Value = objCMS.sCSM_Operation
            'ObjParam(iParamCount).Direction = ParameterDirection.Input
            'iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCMS.sCSM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iBM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_SubGroup", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iBM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iBM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Address1", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objCMS.sCSM_Address1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Address2", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objCMS.sCSM_Address2
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_Address3", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objCMS.sCSM_Address3
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_GenCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iBM_GenCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@BM_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCMS.iBM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSalesPartyMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridStatutoryReferencesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("sID")
            dt.Columns.Add("Statutory Name")
            dt.Columns.Add("Statutory Value")

            sSql = "Select Buyer_PKID,Buyer_ID,Buyer_Desc,Buyer_Value,Buyer_Status from Sales_Buyer_Accounting_Template Where Buyer_ID=" & iPartyID & " and  Buyer_CompID=" & iCompID & "  Order by Buyer_Desc"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("sID") = dr("Buyer_PKID")
                    dRow("Statutory Name") = dr("Buyer_Desc")
                    dRow("Statutory Value") = dr("Buyer_Value")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSMStatutoryNameValueID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatutoryName As String, ByVal iPartID As Integer) As String
        Dim sSql As String = ""
        Dim iID As Integer = 0
        Try
            sSql = "Select Buyer_PKID From Sales_Buyer_Accounting_Template Where Buyer_ID=" & iPartID & " And Buyer_CompID=" & iCompID & " And Buyer_Desc='" & sStatutoryName & "'"
            iID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveCSMStatutoryNameValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatutoryName As String, ByVal sRegistrationNo As String, ByVal iID As Integer, ByVal iPartyID As Integer)
        Dim sSql As String = ""
        Dim iMaxID As Integer = 0
        Try
            iMaxID = objGenfun.GetMaxID(sNameSpace, iCompID, "Sales_Buyer_Accounting_Template", "Buyer_PKID", "Buyer_CompID")
            If iID = 0 Then
                sSql = "" : sSql = "Insert Into Sales_Buyer_Accounting_Template (Buyer_PKID,Buyer_ID,Buyer_Desc,Buyer_Value,Buyer_Status,Buyer_CompID) values"
                sSql = sSql & "(" & iMaxID & "," & iPartyID & ",'" & RemoveQuote(sStatutoryName) & "','" & RemoveQuote(sRegistrationNo) & "','W'," & iCompID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            ElseIf iID > 0 Then
                sSql = "" : sSql = "Update Sales_Buyer_Accounting_Template Set Buyer_Desc= '" & RemoveQuote(sStatutoryName) & "',Buyer_Value='" & RemoveQuote(sRegistrationNo) & "',Buyer_Status='U'"
                sSql = sSql & "Where  Buyer_ID=" & iPartyID & " and Buyer_CompID = " & iCompID & " And Buyer_PKID= " & iID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Shared Function RemoveQuote(ByVal sString As String) As String
        Try
            RemoveQuote = Trim(Replace(sString, "'", "`"))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteCSMStatutoryNameValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete From Sales_Buyer_Accounting_Template Where Buyer_CompID=" & iCompID & " And Buyer_PKID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeletePartyMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal MasterId As Integer, ByVal iUserId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "update Sales_Buyers_Masters set BM_Delflag ='X',BM_Status='D',BM_DeletedBy=" & iUserId & " where BM_ID=" & MasterId & " and BM_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReCallPartyMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal MasterId As Integer, ByVal iUserId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "update Sales_Buyers_Masters set BM_Delflag ='Y',BM_Status='R',BM_RecalldBy=" & iUserId & " where BM_ID=" & MasterId & " and BM_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception

        End Try
    End Sub
    Public Function LoadCMSDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sPartyName As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Sales_Buyers_Masters where  BM_Name = '" & sPartyName & "' and BM_Delflag <> 'D' and BM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("CSM_ID")
            dt.Columns.Add("SupplierName")
            dt.Columns.Add("Address")
            dt.Columns.Add("State")
            dt.Columns.Add("PinCode")
            dt.Columns.Add("FlagVal")
            sSql = "Select * from Sales_Buyers_Masters Where BM_CompID=" & iCompID & " Order by BM_Name"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("CSM_ID") = dr("BM_ID")
                    dRow("SupplierName") = dr("BM_Name")
                    dRow("Address") = dr("BM_Address")
                    dRow("State") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc from ACC_General_Master where Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='State' and Mas_DelFlag='X') and Mas_Delflag ='A' And Mas_ID=" & dr("BM_State") & "")
                    dRow("PinCode") = dr("BM_PinCode")
                    If (dr("BM_Delflag") = "W") Then
                        dRow("FlagVal") = "Waiting For Approval(After Create)"
                    ElseIf (dr("BM_Delflag") = "A") Then
                        dRow("FlagVal") = "Approved"
                    ElseIf (dr("BM_Delflag") = "X") Then
                        dRow("FlagVal") = "Waiting For Approval(After De-Activate)"
                    ElseIf (dr("BM_Delflag") = "D") Then
                        dRow("FlagVal") = "De-Activate"
                    ElseIf (dr("BM_Delflag") = "Y") Then
                        dRow("FlagVal") = "Waiting For Approval(After Activate)"
                    End If
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePartyToChartOfAccount(ByVal sNameSpace As String, ByVal compId As Integer, ByVal parentId As Integer, ByVal SuplierName As String)
        Dim ssql As String
        Dim id As Integer
        Dim glcode As String

        Try
            id = objDBL.SQLExecuteScalarInt(sNameSpace, "select isnull(max(gl_id)+1,1) from chart_of_Accounts")
            glcode = objDBL.SQLExecuteScalar(sNameSpace, "Select gl_glcode From chart_of_Accounts Where gl_id = " & parentId & "")
            glcode &= objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(gl_orderby) + 1,1) from chart_of_Accounts where gl_Parent =" & parentId & "")
            ssql = "Insert Into chart_of_Accounts (gl_id,gl_glcode,gl_parent,gl_delflag,gl_CompId,gl_desc,gl_head) values"
            ssql = ssql & "(" & id & ",'" & glcode & "'," & parentId & ",'C'," & compId & ",'" & SuplierName & "',1)"
            objDBL.SQLExecuteNonQuery(sNameSpace, ssql)
            'Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
            'Dim iParamCount As Integer
            'Dim Arr(1) As String
            'Try
            '    iParamCount = 0
            '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GL_ID", OleDb.OleDbType.Integer, 4)
            '    ObjParam(iParamCount).Value = 01
            '    ObjParam(iParamCount).Direction = ParameterDirection.Input
            '    iParamCount += 1

            '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GL_GLCODE", OleDb.OleDbType.VarChar, 1000)
            '    ObjParam(iParamCount).Value ='q'
            '    ObjParam(iParamCount).Direction = ParameterDirection.Input
            '    iParamCount += 1


            '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GL_PARENT", OleDb.OleDbType.Integer, 4)
            '    ObjParam(iParamCount).Value = parentId
            '    ObjParam(iParamCount).Direction = ParameterDirection.Input
            '    iParamCount += 1

            '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GL_DESC", OleDb.OleDbType.VarChar, 1000)
            '    ObjParam(iParamCount).Value = SuplierName
            '    ObjParam(iParamCount).Direction = ParameterDirection.Input
            '    iParamCount += 1

            '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GL_DELFLAG", OleDb.OleDbType.Char, 1)
            '    ObjParam(iParamCount).Value = 'C'
            '    ObjParam(iParamCount).Direction = ParameterDirection.Input
            '    iParamCount += 1

            '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@GL_COMPID", OleDb.OleDbType.Integer, 4)
            '    ObjParam(iParamCount).Value = compId
            '    ObjParam(iParamCount).Direction = ParameterDirection.Input
            '    iParamCount += 1

            '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            '    ObjParam(iParamCount).Direction = ParameterDirection.Output
            '    iParamCount += 1

            '    ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            '    ObjParam(iParamCount).Direction = ParameterDirection.Output
            '    Arr(0) = "@iUpdateOrSave"
            '    Arr(1) = "@iOper"

            '    Arr = objDBL.ExecStoredProcFrInsRetARR(sNameSpace, "InsertSuplierToChartOfAccount", 1, Arr, ObjParam)
            '    Return Arr(1)
        Catch
            Throw
        End Try

    End Function
    Public Function Approve(ByVal sNameSpace As String, iCompID As Integer, ByVal sIPAddress As String, ByVal iAppBy As String, ByVal Id As Integer)
        Dim sSql As String
        Try
            sSql = "Update Sales_Buyers_Masters set Mas_IPAddress='" & sIPAddress & "',Mas_Delflag='A',MAS_Status='A',Mas_AppBy=" & iAppBy & ","
            sSql = sSql & " Mas_AppOn=Getdate() Where Mas_Id='" & Id & "' And Mas_CompId= " & iCompID & ""

            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
        End Try
    End Function
    Public Function LoadGroup(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from chart_of_Accounts where gl_AccHead = 1 and gl_head = 0 and gl_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadChartofAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGroup As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from chart_of_Accounts where gl_AccHead = 1 and gl_Parent = " & iGroup & " and gl_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GeneratePartyCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sQueryString As String) As String
        Dim sSql As String = ""
        Dim iMaxID As Integer
        Dim sStr As String = ""
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "select isnull(max(BM_ID)+1,1) from Sales_Buyers_Masters")
            If sQueryString = "SO" Then
                sStr = "C - " & iMaxID
            Else
                sStr = "P - " & iMaxID
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Try
            sSql = "Select BM_Status From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_CompID=" & iCompID & " "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGeneralcategory(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
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
    Public Function getParentId(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sGlCode As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select gl_id from chart_of_accounts where gl_glcode='" & sGlCode & "' and Gl_CompID = " & iCompID & ""
            Return Convert.ToInt32(objDBL.SQLExecuteScalar(sNameSpace, sSql))
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
