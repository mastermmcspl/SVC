Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsSupplierUpload
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
    Private sCSM_ProductDescription
    Private sCSM_EmailID As String
    Private sCSM_MobileNo As String
    Private sCSM_LandLineNo As String
    Private sCSM_Fax As String

    Private sCSM_Address As String
    Private sCSM_Address1 As String
    Private sCSM_Address2 As String
    Private sCSM_Address3 As String

    Private sCSM_Pincode As String
    Private iCSM_City As Integer
    Private iCSM_State As Integer


    Private sCSM_Delflag As String
    Private iCSM_CompID As Integer
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
    Private iCSM_Group As Integer
    Private iCSM_SubGroup As Integer
    Private iCSM_GL As Integer
    Private iCSM_SubGL As Integer
    Public Property CSM_SubGL() As Integer
        Get
            Return (iCSM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iCSM_SubGL = Value
        End Set
    End Property
    Public Property CSM_GL() As Integer
        Get
            Return (iCSM_GL)
        End Get
        Set(ByVal Value As Integer)
            iCSM_GL = Value
        End Set
    End Property
    Public Property CSM_SubGroup() As Integer
        Get
            Return (iCSM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            iCSM_SubGroup = Value
        End Set
    End Property

    Public Property CSM_Group() As Integer
        Get
            Return (iCSM_Group)
        End Get
        Set(ByVal Value As Integer)
            iCSM_Group = Value
        End Set
    End Property

    Public Property CSM_ProductDescription() As String
        Get
            Return (sCSM_ProductDescription)
        End Get
        Set(ByVal Value As String)
            sCSM_ProductDescription = Value
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
    Public Function SaveCumsterSupplierDetails(ByVal sNameSpace As String, ByVal ObjSU As clsSupplierUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(36) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_IndType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_IndType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Code", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Inventry", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_Inventry
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ContactPerson", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_EmailID", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_EmailID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_MobileNo", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_LandLineNo", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_LandLineNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Fax", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Address", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Address1", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Address2", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Address2
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Address3", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Address3
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Pincode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Pincode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_City ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_State", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Operation", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjSU.sCSM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjSU.sCSM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = ObjSU.dCSM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ApprovedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = ObjSU.dCSM_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_DeletedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_DeletedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_DeletedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = ObjSU.dCSM_DeletedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = ObjSU.dCSM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_ProductDescription", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjSU.CSM_ProductDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_SubGroup", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CSM_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjSU.iCSM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCustomerSupplierMasterUpload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSupplierMasters(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dtUpload As DataTable, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sCode As String = "", sName As String = "", sContactPerson As String = "", sEmail As String = ""
        Dim sMobileNo As String = "", sLandLineNo As String = "", sFax As String = "", sAddress As String = "", sAddress1 As String = "", sAddress2 As String = "", sAddress3 As String = "", Tin As String = ""
        Dim sPincode As String = "", sCity As String = "", sState As String = ""
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0
        Dim iid As Integer
        Try
            For i = 0 To dtUpload.Rows.Count - 1

                'Code
                If dtUpload.Rows(i)(0).ToString() = "" Then
                    sCode = ""
                Else
                    sCode = dtUpload.Rows(i)(0).ToString()
                End If

                'Name
                If dtUpload.Rows(i)(1).ToString() = "" Then
                    sName = ""
                Else
                    sName = dtUpload.Rows(i)(1).ToString()
                End If

                'ContactPerson
                If dtUpload.Rows(i)(2).ToString() = "" Then
                    sContactPerson = ""
                Else
                    sContactPerson = dtUpload.Rows(i)(2).ToString()
                End If

                'Email
                If dtUpload.Rows(i)(3).ToString() = "" Then
                    sEmail = ""
                Else
                    sEmail = dtUpload.Rows(i)(3).ToString()
                End If

                'MobileNo
                If dtUpload.Rows(i)(4).ToString() = "" Then
                    sMobileNo = ""
                Else
                    sMobileNo = dtUpload.Rows(i)(4).ToString()
                End If

                'LandLineNo
                If dtUpload.Rows(i)(5).ToString() = "" Then
                    sLandLineNo = ""
                Else
                    sLandLineNo = dtUpload.Rows(i)(5).ToString()
                End If

                'Fax
                If dtUpload.Rows(i)(6).ToString() = "" Then
                    sFax = ""
                Else
                    sFax = dtUpload.Rows(i)(6).ToString()
                End If

                'Address
                If dtUpload.Rows(i)(7).ToString() = "" Then
                    sAddress = ""
                Else
                    sAddress = dtUpload.Rows(i)(7).ToString()
                End If

                'Address1
                If dtUpload.Rows(i)(8).ToString() = "" Then
                    sAddress1 = ""
                Else
                    sAddress1 = dtUpload.Rows(i)(8).ToString()
                End If

                'Address2
                If dtUpload.Rows(i)(9).ToString() = "" Then
                    sAddress2 = ""
                Else
                    sAddress2 = dtUpload.Rows(i)(9).ToString()
                End If

                'Address3
                If dtUpload.Rows(i)(10).ToString() = "" Then
                    sAddress3 = ""
                Else
                    sAddress3 = dtUpload.Rows(i)(10).ToString()
                End If

                'Pincode
                If dtUpload.Rows(i)(11).ToString() = "" Then
                    sPincode = ""
                Else
                    sPincode = dtUpload.Rows(i)(11).ToString()
                End If

                'City
                If dtUpload.Rows(i)(12).ToString() = "" Then
                    sCity = "0"
                Else
                    sCity = CheckCityExistOrNot(sNameSpace, iCompID, dtUpload.Rows(i)(12).ToString(), 0)
                End If

                'State
                If dtUpload.Rows(i)(13).ToString() = "" Then
                    sState = "0"
                Else
                    sState = CheckCityExistOrNot(sNameSpace, iCompID, dtUpload.Rows(i)(13).ToString(), 1)
                End If

                'Tin
                If dtUpload.Rows(i)(14).ToString() = "" Then
                    Tin = "0"
                Else
                    Tin = CheckCityExistOrNot(sNameSpace, iCompID, dtUpload.Rows(i)(14).ToString(), 1)
                    iid = GetStatutoryNameValueID(sNameSpace, iCompID, "Tin")
                    SaveStatutoryNameValue(sNameSpace, iCompID, "Tin", dtUpload.Rows(i)(14).ToString(), iid)
                End If

                'Saving Supplier to chart of accounts and taking the ID'
                Dim sPerm As String = ""
                Dim sArray1 As Array
                sPerm = LoadDetailsSetttings(sNameSpace, iCompID, "Supplier", "Supplier")
                sPerm = sPerm.Remove(0, 1)
                sArray1 = sPerm.Split(",")

                Dim iHead, iGroup, iSubGroup, iGL, iSubGL As Integer
                iHead = sArray1(0)
                iGroup = sArray1(1)
                iSubGroup = sArray1(2)
                iGL = sArray1(3)
                iSubGL = CreateChartOfAccounts(sNameSpace, iCompID, iUserID, sIPAddress, sName, 3, sArray1(3), 4)
                'Saving Supplier to chart of accounts and taking the ID'

                sSql = "" : sSql = "Select * from CustomerSupplierMaster where CSM_Code ='" & sCode & "' and CSM_CompID=" & iCompID & " "
                dr = objDBL.SQLDataReader(sNameSpace, sSql)
                If dr.HasRows = True Then
                    sSql = "" : sSql = "Update CustomerSupplierMaster set CSM_Name = '" & sName & "',CSM_ContactPerson = '" & sContactPerson & "',CSM_EmailID = '" & sEmail & "',"
                    sSql = sSql & "CSM_MobileNo = '" & sMobileNo & "',CSM_LandLineNo = '" & sLandLineNo & "',CSM_Fax = '" & sFax & "',CSM_Address = '" & sAddress & "',"
                    sSql = sSql & "CSM_PinCode = '" & sPincode & "',CSM_City = " & sCity & ",CSM_State = " & sState & ",CSM_Address1='" & sAddress1 & "',CSM_Address2='" & sAddress2 & "',CSM_Address3='" & sAddress3 & "',CSM_Group=" & iGroup & ",CSM_SubGroup=" & iSubGroup & ",CSM_GL=" & iGL & ",CSM_SubGL=" & iSubGL & " where CSM_Code ='" & sCode & "' and CSM_CompID = " & iCompID & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Else
                    iMax = objGenFun.GetMaxID(sNameSpace, iCompID, "CustomerSupplierMaster", "CSM_ID", "CSM_CompID")
                    sSql = "" : sSql = "Insert into CustomerSupplierMaster(CSM_ID,CSM_Name,CSM_Code,CSM_Inventry,"
                    sSql = sSql & "CSM_ContactPerson,CSM_EmailID,CSM_MobileNo,CSM_LandLineNo,"
                    sSql = sSql & "CSM_Fax,CSM_Address,CSM_PinCode,CSM_City,CSM_State,"
                    sSql = sSql & "CSM_Delflag,CSM_CompID,CSM_Status,CSM_Operation,CSM_IPAddress,"
                    sSql = sSql & "CSM_CreatedBy,CSM_CreatedOn,CSM_Address1,CSM_Address2,CSM_Address3,CSM_Group,CSM_SubGroup,CSM_GL,CSM_SubGL)"
                    sSql = sSql & "Values(" & iMax & ",'" & RemoveQuote(sName) & "','" & RemoveQuote(sCode) & "',0,"
                    sSql = sSql & "'" & RemoveQuote(sContactPerson) & "','" & RemoveQuote(sEmail) & "','" & sMobileNo & "','" & sLandLineNo & "',"
                    sSql = sSql & "'" & sFax & "','" & RemoveQuote(sAddress) & "','" & sPincode & "'," & sCity & "," & sState & ","
                    sSql = sSql & "'A'," & iCompID & ",'A','W','" & sIPAddress & "'," & iUserID & ",GetDate(),'" & sAddress1 & "','" & sAddress2 & "','" & sAddress3 & "'," & iGroup & "," & iSubGroup & "," & iGL & "," & iSubGL & ")"
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function CreateChartOfAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer) As Integer
        Dim sRet As String
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try

            objCOA.igl_id = 0
            objCOA.igl_head = iHead
            objCOA.igl_Parent = iParent
            objCOA.sgl_glcode = objCOA.GenerateSubGLCode(sNameSpace, iCompID, iAccHead, iParent)
            objCOA.sgl_Desc = objGen.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(sName)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = iAccHead
            objCOA.igl_Crby = iUserID
            objCOA.igl_CompId = iCompID
            objCOA.sgl_Status = "A"
            objCOA.sgl_IPAddress = sIPAddress
            sRet = objCOA.SaveChartofACC(sNameSpace, iCompID, objCOA)
            sArray = sRet.Split(",")
            Return sArray(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatutoryNameValueID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatutoryName As String) As String
        Dim sSql As String
        Dim iID As Integer
        Try
            sSql = "Select Cmp_PKID From Company_Accounting_Template Where Cmp_ID=" & iCompID & " And Cmp_Desc='" & sStatutoryName & "'"
            iID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveStatutoryNameValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStatutoryName As String, ByVal sStatutoryValue As String, ByVal iID As Integer)
        Dim sSql As String
        Dim iMaxID As Integer
        Try
            'clsCustomerMaster.GetMaxIDCmpValue(sNameSpace, iCompID, "Company_Accounting_Template", "Cmp_PKID", "Cmp_ID")
            If iID = 0 Then
                iMaxID = objGenFun.GetMaxID(sNameSpace, iCompID, "Company_Accounting_Template", "Cmp_PKID", "Cmp_ID")
                sSql = "" : sSql = "Insert Into Company_Accounting_Template (Cmp_PKID,Cmp_Desc,Cmp_Value,Cmp_ID,Cmp_Status) values"
                sSql = sSql & "(" & iMaxID & ",'" & RemoveQuote(sStatutoryName) & "','" & RemoveQuote(sStatutoryValue) & "'," & iCompID & ",'W')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            ElseIf iID > 0 Then
                sSql = "" : sSql = "Update Company_Accounting_Template Set Cmp_Desc= '" & RemoveQuote(sStatutoryName) & "',Cmp_Value='" & RemoveQuote(sStatutoryValue) & "',Cmp_Status='U'"
                sSql = sSql & " Where Cmp_ID = " & iCompID & " And Cmp_PKID=  " & iID & ""
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckCityExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCity As String, ByVal iMaster As Integer) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            If iMaster = 0 Then
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCity & "' and Mas_Master = 3"
            Else
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCity & "' and Mas_Master = 4"
            End If

            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckCityExistOrNot = dr("Mas_Id")
            Else
                CheckCityExistOrNot = 0
            End If
            dr.Close()
            Return CheckCityExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function RemoveQuote(ByVal sString As String) As String
        Try
            RemoveQuote = Trim(Replace(sString, "'", "`"))
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
End Class
