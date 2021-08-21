Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsPartyMasters
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Private ACM_ID As Integer
    Private ACM_Type As String
    Private ACM_Name As String
    Private ACM_Code As String
    Private ACM_Group As Integer
    Private ACM_Head As Integer
    Private ACM_SubGroup As Integer
    Private ACM_GL As Integer
    Private ACM_SubGL As Integer
    Private ACM_Status As String
    Private ACM_IpAddress As String
    Private ACM_CreatedBy As Integer
    Private ACM_UpdatedBy As Integer
    Private ACM_ApprovedBy As Integer
    Private ACM_DeletedBy As Integer

    Private ACAD_ID As Integer
    Private ACAD_MasterID As Integer
    Private ACAD_Address As String
    Private ACAD_City As Integer
    Private ACAD_State As Integer
    Private ACAD_Country As Integer
    Private ACAD_Pincode As Integer
    Private ACAD_ContactPerson As String
    Private ACAD_Email As String
    Private ACAD_MobileNo As String
    Private ACAD_Landline As String
    Private ACAD_Fax As String
    Private ACAD_Status As String
    Private ACAD_CreatedBy As Integer
    Public Property iACAD_CreatedBy() As Integer
        Get
            Return (ACAD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            ACAD_CreatedBy = Value
        End Set
    End Property
    Public Property sACAD_Status() As String
        Get
            Return (ACAD_Status)
        End Get
        Set(ByVal Value As String)
            ACAD_Status = Value
        End Set
    End Property
    Public Property sACAD_Fax() As String
        Get
            Return (ACAD_Fax)
        End Get
        Set(ByVal Value As String)
            ACAD_Fax = Value
        End Set
    End Property
    Public Property sACAD_Landline() As String
        Get
            Return (ACAD_Landline)
        End Get
        Set(ByVal Value As String)
            ACAD_Landline = Value
        End Set
    End Property
    Public Property sACAD_MobileNo() As String
        Get
            Return (ACAD_MobileNo)
        End Get
        Set(ByVal Value As String)
            ACAD_MobileNo = Value
        End Set
    End Property
    Public Property sACAD_Email() As String
        Get
            Return (ACAD_Email)
        End Get
        Set(ByVal Value As String)
            ACAD_Email = Value
        End Set
    End Property
    Public Property sACAD_ContactPerson() As String
        Get
            Return (ACAD_ContactPerson)
        End Get
        Set(ByVal Value As String)
            ACAD_ContactPerson = Value
        End Set
    End Property
    Public Property iACAD_Pincode() As Integer
        Get
            Return (ACAD_Pincode)
        End Get
        Set(ByVal Value As Integer)
            ACAD_Pincode = Value
        End Set
    End Property
    Public Property iACAD_Country() As Integer
        Get
            Return (ACAD_Country)
        End Get
        Set(ByVal Value As Integer)
            ACAD_Country = Value
        End Set
    End Property
    Public Property iACAD_State() As Integer
        Get
            Return (ACAD_State)
        End Get
        Set(ByVal Value As Integer)
            ACAD_State = Value
        End Set
    End Property
    Public Property iACAD_City() As Integer
        Get
            Return (ACAD_City)
        End Get
        Set(ByVal Value As Integer)
            ACAD_City = Value
        End Set
    End Property
    Public Property sACAD_Address() As String
        Get
            Return (ACAD_Address)
        End Get
        Set(ByVal Value As String)
            ACAD_Address = Value
        End Set
    End Property
    Public Property iACAD_MasterID() As Integer
        Get
            Return (ACAD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            ACAD_MasterID = Value
        End Set
    End Property
    Public Property iACAD_ID() As Integer
        Get
            Return (ACAD_ID)
        End Get
        Set(ByVal Value As Integer)
            ACAD_ID = Value
        End Set
    End Property

    Public Property iACM_DeletedBy() As Integer
        Get
            Return (ACM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            ACM_DeletedBy = Value
        End Set
    End Property
    Public Property iACM_ApprovedBy() As Integer
        Get
            Return (ACM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            ACM_ApprovedBy = Value
        End Set
    End Property
    Public Property iACM_UpdatedBy() As Integer
        Get
            Return (ACM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ACM_UpdatedBy = Value
        End Set
    End Property
    Public Property iACM_CreatedBy() As Integer
        Get
            Return (ACM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            ACM_CreatedBy = Value
        End Set
    End Property

    Public Property sACM_IpAddress() As String
        Get
            Return (ACM_IpAddress)
        End Get
        Set(ByVal Value As String)
            ACM_IpAddress = Value
        End Set
    End Property
    Public Property sACM_Status() As String
        Get
            Return (ACM_Status)
        End Get
        Set(ByVal Value As String)
            ACM_Status = Value
        End Set
    End Property

    Public Property iACM_SubGL() As Integer
        Get
            Return (ACM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            ACM_SubGL = Value
        End Set
    End Property
    Public Property iACM_GL() As Integer
        Get
            Return (ACM_GL)
        End Get
        Set(ByVal Value As Integer)
            ACM_GL = Value
        End Set
    End Property
    Public Property iACM_SubGroup() As Integer
        Get
            Return (ACM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            ACM_SubGroup = Value
        End Set
    End Property
    Public Property iACM_Group() As Integer
        Get
            Return (ACM_Group)
        End Get
        Set(ByVal Value As Integer)
            ACM_Group = Value
        End Set
    End Property
    Public Property iACM_Head() As Integer
        Get
            Return (ACM_Head)
        End Get
        Set(ByVal Value As Integer)
            ACM_Head = Value
        End Set
    End Property
    Public Property sACM_Code() As String
        Get
            Return (ACM_Code)
        End Get
        Set(ByVal Value As String)
            ACM_Code = Value
        End Set
    End Property
    Public Property sACM_Name() As String
        Get
            Return (ACM_Name)
        End Get
        Set(ByVal Value As String)
            ACM_Name = Value
        End Set
    End Property
    Public Property sACM_Type() As String
        Get
            Return (ACM_Type)
        End Get
        Set(ByVal Value As String)
            ACM_Type = Value
        End Set
    End Property

    Public Property iACM_ID() As Integer
        Get
            Return (ACM_ID)
        End Get
        Set(ByVal Value As Integer)
            ACM_ID = Value
        End Set
    End Property
    Public Function LoadGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_head = 0 and gl_compid=" & iCompID & " and  gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iAccHead & " order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParent As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and  gl_Delflag ='C' and gl_status='A' and gl_Parent = " & iParent & " order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCity(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMaster As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_Id, Mas_desc as GlDesc FROM acc_General_Master where "
            sSql = sSql & "Mas_compid=" & iCompID & " and  Mas_status='A' and Mas_Master = " & iMaster & " order by Mas_desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveCustomerMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objParty As clsPartyMasters) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParty.iACM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Type", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objParty.sACM_Type
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Name", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParty.sACM_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Code", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objParty.sACM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Head", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Group", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_SubGroup", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_GL", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_SubGL", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objParty.sACM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CompID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CreatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CreatedOn", OleDb.OleDbType.Date, 100)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_UpdatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_UpdatedOn", OleDb.OleDbType.Date, 10)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ApprovedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ApprovedOn", OleDb.OleDbType.Date, 20)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_DeletedBy", OleDb.OleDbType.Integer, 20)
            ObjParam(iParamCount).Value = objParty.iACM_DeletedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_DeletedOn", OleDb.OleDbType.Date, 20)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_IpAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParty.sACM_IpAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Operations", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Customer_Masters", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iStatus As Integer, ByVal iCustomerType As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Dim sCustType As String = ""
        Try
            dc = New DataColumn("SrNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Type", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Code", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Name", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            If iCustomerType = 0 Then
                sCustType = "C"
            ElseIf iCustomerType = 1 Then
                sCustType = "S"
            ElseIf iCustomerType = 2 Then
                sCustType = "P"
            End If

            sSql = "Select * from Acc_Customer_Master where ACM_CompID =" & iCompID & " and ACM_Type ='" & sCustType & "' "
            If iStatus = 0 Then
                sSql = sSql & " And ACM_Status ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And ACM_Status='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And ACM_Status='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By ACM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("SrNo") = i + 1

                    If IsDBNull(ds.Tables(0).Rows(i)("ACM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("ACM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACM_Type").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ACM_Type").ToString() = "C" Then
                            dr("Type") = "Customer"
                        ElseIf ds.Tables(0).Rows(i)("ACM_Type").ToString() = "S" Then
                            dr("Type") = "Supplier"
                        ElseIf ds.Tables(0).Rows(i)("ACM_Type").ToString() = "P" Then
                            dr("Type") = "Party"
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACM_Name").ToString()) = False Then
                        dr("Name") = ds.Tables(0).Rows(i)("ACM_Name").ToString()
                    End If


                    If IsDBNull(ds.Tables(0).Rows(i)("ACM_Code").ToString()) = False Then
                        dr("Code") = ds.Tables(0).Rows(i)("ACM_Code").ToString()
                    End If

                    If (ds.Tables(0).Rows(i)("ACM_Status") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("ACM_Status") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("ACM_Status") = "D") Then
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

    Public Sub UpdatePaymentMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_Customer_Master Set ACM_IpAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " ACM_Status='A',ACM_ApprovedBy= " & iUserID & ",ACM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " ACM_Status='D',ACM_DeletedBy= " & iUserID & ",ACM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " ACM_Status='A' "
            End If
            sSql = sSql & " Where ACM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadExistingCustomer(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = "", sCustTyp As String = ""
        Dim dt As New DataTable
        Try

            If iParty = 0 Then
                sCustTyp = "C"
            ElseIf iParty = 1 Then
                sCustTyp = "S"
            ElseIf iParty = 2 Then
                sCustTyp = "P"
            End If

            sSql = "Select ACM_ID, ACM_Code + ' - ' + ACM_Name as Name from  Acc_Customer_Master where ACM_Type = '" & sCustTyp & "' and ACM_CompID=" & iCompID & " order by ACM_Name Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Acc_Customer_Master where ACM_ID =" & iParty & " and ACM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveAddressDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objParty As clsPartyMasters) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParty.iACAD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objParty.iACAD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Address", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objParty.sACAD_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_City", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_State", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_State
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Country", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_Country
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Pincode", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_Pincode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_ContactPerson", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objParty.sACAD_ContactPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Email", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objParty.sACAD_Email
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_MobileNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objParty.sACAD_MobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Landline", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objParty.sACAD_Landline
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Fax", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objParty.sACAD_Fax
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_CompID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objParty.sACAD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_CreatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objParty.iACAD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACAD_CreatedOn", OleDb.OleDbType.Date, 100)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Customer_Address_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteAddressDetails(ByVal sNamSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Delete from Acc_Customer_Address_Details where ACAD_MasterID =" & iParty & " and ACAD_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNamSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAddressDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("ACM_ID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Address", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("City", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CityID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("State", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("StateID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Country", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CountryID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PinCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ContactNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("EMail", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Mobile", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("LandLine", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Fax", GetType(String))
            dt.Columns.Add(dc)


            sSql = "" : sSql = "Select * from Acc_Customer_Address_Details where ACAD_MasterID =" & iParty & " and ACAD_CompID= " & iCompID & ""
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_ID").ToString()) = False Then
                        dr("ACM_ID") = ds.Tables(0).Rows(i)("ACAD_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_Address").ToString()) = False Then
                        dr("Address") = ds.Tables(0).Rows(i)("ACAD_Address").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_City").ToString()) = False Then
                        dr("City") = objDBL.SQLExecuteScalar(sNameSpace, "Select * from acc_General_Master where mas_id =" & ds.Tables(0).Rows(i)("ACAD_City").ToString() & "")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_City").ToString()) = False Then
                        dr("CityID") = ds.Tables(0).Rows(i)("ACAD_City").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_State").ToString()) = False Then
                        dr("State") = objDBL.SQLExecuteScalar(sNameSpace, "Select * from acc_General_Master where mas_id =" & ds.Tables(0).Rows(i)("ACAD_State").ToString() & "")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_State").ToString()) = False Then
                        dr("StateID") = ds.Tables(0).Rows(i)("ACAD_State").ToString()
                    End If


                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_Country").ToString()) = False Then
                        dr("Country") = objDBL.SQLExecuteScalar(sNameSpace, "Select * from acc_General_Master where mas_id =" & ds.Tables(0).Rows(i)("ACAD_Country").ToString() & "")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_Country").ToString()) = False Then
                        dr("CountryID") = ds.Tables(0).Rows(i)("ACAD_Country").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_Pincode").ToString()) = False Then
                        dr("PinCode") = ds.Tables(0).Rows(i)("ACAD_Pincode").ToString()
                    End If


                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_ContactPerson").ToString()) = False Then
                        dr("ContactNo") = ds.Tables(0).Rows(i)("ACAD_ContactPerson").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_Email").ToString()) = False Then
                        dr("EMail") = ds.Tables(0).Rows(i)("ACAD_Email").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_MobileNo").ToString()) = False Then
                        dr("Mobile") = ds.Tables(0).Rows(i)("ACAD_MobileNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_Landline").ToString()) = False Then
                        dr("LandLine") = ds.Tables(0).Rows(i)("ACAD_Landline").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACAD_Fax").ToString()) = False Then
                        dr("Fax") = ds.Tables(0).Rows(i)("ACAD_Fax").ToString()
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteOtherDetails(ByVal sNamSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Delete from Acc_Customer_Other_Details where ACOD_MasterID =" & iParty & " and ACOD_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNamSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer, ByVal iOtherID As Integer, ByVal sName As String, ByVal sValue As String, ByVal iUserID As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iParty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_OtherID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = iOtherID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_Name", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_Value", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_CompID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_CreatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACOD_CreatedOn", OleDb.OleDbType.Date, 100)
            ObjParam(iParamCount).Value = Date.Now
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Customer_Other_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadOthersDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("ID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Name", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Value", GetType(String))
            dt.Columns.Add(dc)

            sSql = "" : sSql = "Select * from Acc_Customer_Other_Details where ACOD_MasterID =" & iParty & " and ACOD_CompID= " & iCompID & ""
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("ACOD_OtherID").ToString()) = False Then
                        dr("ID") = ds.Tables(0).Rows(i)("ACOD_OtherID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACOD_Name").ToString()) = False Then
                        dr("Name") = ds.Tables(0).Rows(i)("ACOD_Name").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACOD_Value").ToString()) = False Then
                        dr("Value") = ds.Tables(0).Rows(i)("ACOD_Value").ToString()
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function CheckChartofAccountGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer, ByVal iHead As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer)
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select * from Acc_Customer_Master where "
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class

