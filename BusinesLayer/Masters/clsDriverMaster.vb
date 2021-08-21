Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsDriverMaster
    Private objDBL As New DatabaseLayer.DBHelper

    Private iLDM_ID As Integer
    Private sLDM_DriverName As String
    Private sLDM_LicenseNo As String
    Private sLDM_AadharNo As String
    Private sLDM_ContactNo As String
    Private sLDM_City As String
    Private sLDM_PinCode As String
    Private iLDM_InsuranceType As Integer
    Private sLDM_InsuranceNo As String
    Private dLDM_InsuranceAmt As Double
    Private dLDM_InsuranceExpDate As DateTime
    Private sLDM_InsuranceDetails As String
    Private sLDM_Address As String
    Private sLDM_DelFlag As String
    Private sLdM_Status As String
    Private iLDM_CreatedBy As Integer
    Private dLDM_CreatedOn As DateTime
    Private iLDM_UpdatedBy As Integer
    Private dLDM_UpdatedOn As DateTime
    Private iLDM_ApprovedBy As Integer
    Private dLDM_ApprovedOn As DateTime
    Private iLDM_DeletedBy As Integer
    Private dLDM_DeletedOn As DateTime
    Private iLDM_RecalldBy As Integer
    Private iLDM_Group As Integer
    Private iLDM_SubGroup As Integer
    Private iLDM_GL As Integer
    Private iLDM_SubGL As Integer
    Private iLDM_CompID As Integer
    Private iLDM_YearID As Integer
    Private sLDM_Operation As String
    Private sLDM_IPAddress As String

    Public Property LDM_ID() As Integer
        Get
            Return (iLDM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLDM_ID = Value
        End Set
    End Property
    Public Property LDM_DriverName() As String
        Get
            Return (sLDM_DriverName)
        End Get
        Set(ByVal Value As String)
            sLDM_DriverName = Value
        End Set
    End Property
    Public Property LDM_LicenseNo() As String
        Get
            Return (sLDM_LicenseNo)
        End Get
        Set(ByVal Value As String)
            sLDM_LicenseNo = Value
        End Set
    End Property
    Public Property LDM_AadharNo() As String
        Get
            Return (sLDM_AadharNo)
        End Get
        Set(ByVal Value As String)
            sLDM_AadharNo = Value
        End Set
    End Property
    Public Property LDM_ContactNo() As String
        Get
            Return (sLDM_ContactNo)
        End Get
        Set(ByVal Value As String)
            sLDM_ContactNo = Value
        End Set
    End Property
    Public Property LDM_City() As String
        Get
            Return (sLDM_City)
        End Get
        Set(ByVal Value As String)
            sLDM_City = Value
        End Set
    End Property
    Public Property LDM_PinCode() As String
        Get
            Return (sLDM_PinCode)
        End Get
        Set(ByVal Value As String)
            sLDM_PinCode = Value
        End Set
    End Property
    Public Property LDM_InsuranceType() As Integer
        Get
            Return (iLDM_InsuranceType)
        End Get
        Set(ByVal Value As Integer)
            iLDM_InsuranceType = Value
        End Set
    End Property
    Public Property LDM_InsuranceNo() As String
        Get
            Return (sLDM_InsuranceNo)
        End Get
        Set(ByVal Value As String)
            sLDM_InsuranceNo = Value
        End Set
    End Property
    Public Property LDM_InsuranceAmt() As Double
        Get
            Return (dLDM_InsuranceAmt)
        End Get
        Set(ByVal Value As Double)
            dLDM_InsuranceAmt = Value
        End Set
    End Property
    Public Property LDM_InsuranceExpDate() As Date
        Get
            Return (dLDM_InsuranceExpDate)
        End Get
        Set(ByVal Value As Date)
            dLDM_InsuranceExpDate = Value
        End Set
    End Property
    Public Property LDM_InsuranceDetails() As String
        Get
            Return (sLDM_InsuranceDetails)
        End Get
        Set(ByVal Value As String)
            sLDM_InsuranceDetails = Value
        End Set
    End Property

    Public Property LDM_Address() As String
        Get
            Return (sLDM_Address)
        End Get
        Set(ByVal Value As String)
            sLDM_Address = Value
        End Set
    End Property
    Public Property LDM_Delflag() As String
        Get
            Return (sLDM_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLDM_DelFlag = Value
        End Set
    End Property
    Public Property LDM_Status() As String
        Get
            Return (sLdM_Status)
        End Get
        Set(ByVal Value As String)
            sLdM_Status = Value
        End Set
    End Property
    Public Property LDM_CreatedBy() As Integer
        Get
            Return (iLDM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLDM_CreatedBy = Value
        End Set
    End Property

    Public Property LDM_CreatedOn() As DateTime
        Get
            Return (dLDM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLDM_CreatedOn = Value
        End Set
    End Property
    Public Property LDM_UpdatedBy() As Integer
        Get
            Return (iLDM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLDM_UpdatedBy = Value
        End Set
    End Property
    Public Property LDM_UpdatedOn() As DateTime
        Get
            Return (dLDM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLDM_UpdatedOn = Value
        End Set
    End Property
    Public Property LDM_ApprovedBy() As Integer
        Get
            Return (iLDM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLDM_ApprovedBy = Value
        End Set
    End Property



    Public Property LDM_ApprovedOn() As DateTime
        Get
            Return (dLDM_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLDM_ApprovedOn = Value
        End Set
    End Property
    Public Property LDM_DeletedBy() As Integer
        Get
            Return (iLDM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLDM_DeletedBy = Value
        End Set
    End Property
    Public Property LDM_DeletedOn() As DateTime
        Get
            Return (dLDM_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLDM_DeletedOn = Value
        End Set
    End Property
    Public Property LDM_RecalldBy() As Integer
        Get
            Return (iLDM_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLDM_RecalldBy = Value
        End Set
    End Property
    Public Property LDM_Group() As Integer
        Get
            Return (iLDM_Group)
        End Get
        Set(ByVal Value As Integer)
            iLDM_Group = Value
        End Set
    End Property
    Public Property LDM_SubGroup() As Integer
        Get
            Return (iLDM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            iLDM_SubGroup = Value
        End Set
    End Property
    Public Property LDM_GL() As Integer
        Get
            Return (iLDM_GL)
        End Get
        Set(ByVal Value As Integer)
            iLDM_GL = Value
        End Set
    End Property
    Public Property LDM_SubGL() As Integer
        Get
            Return (iLDM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iLDM_SubGL = Value
        End Set
    End Property
    Public Property LDM_CompID() As Integer
        Get
            Return (iLDM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLDM_CompID = Value
        End Set
    End Property
    Public Property LDM_YearID() As Integer
        Get
            Return (iLDM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLDM_YearID = Value
        End Set
    End Property
    Public Property LDM_Operation() As String
        Get
            Return (sLDM_Operation)
        End Get
        Set(ByVal Value As String)
            sLDM_Operation = Value
        End Set
    End Property
    Public Property LDM_IPAddress() As String
        Get
            Return (sLDM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLDM_IPAddress = Value
        End Set
    End Property
    'Public Function LoadExistingDriver(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select LDM_ID,(LDM_DriverName + '-' + LDM_LicenseNo) as LDM_DriverName From Lgst_Driver_Master Where LDM_CompID=" & iCompID & " and LDM_Status <> 'D' "
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function CheckDuplicateAadharNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAADharNo As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Lgst_Driver_Master Where LDM_AadharNo='" & sAADharNo & "' And LDM_DelFlag<>'D' And LDM_CompID=" & iCompID & "  "
            CheckDuplicateAadharNo = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckDuplicateAadharNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDuplicateLicenseNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sLicenseNo As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Lgst_Driver_Master Where LDM_LicenseNo='" & sLicenseNo & "' And LDM_DelFlag<>'D' And LDM_CompID=" & iCompID & "  "
            CheckDuplicateLicenseNo = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckDuplicateLicenseNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDriverDetails(ByVal sNameSpace As String, ByVal objDriverMas As clsDriverMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(33) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_DriverName", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_DriverName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_LicenseNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_LicenseNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_AadharNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_AadharNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_ContactNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_ContactNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_City", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_City
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_PinCode", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_PinCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_InsuranceType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_InsuranceType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_InsuranceNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_InsuranceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_InsuranceAmt", OleDb.OleDbType.Double, 200)
            ObjParam(iParamCount).Value = objDriverMas.dLDM_InsuranceAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_InsuranceExpDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objDriverMas.dLDM_InsuranceExpDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_InsuranceDetails", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_InsuranceDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_Address", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDriverMas.sLdM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objDriverMas.dLDM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objDriverMas.dLDM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_ApprovedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objDriverMas.dLDM_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_DeletedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_DeletedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_DeletedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objDriverMas.dLDM_DeletedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_RecalldBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_RecalldBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_SubGroup ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDriverMas.iLDM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_Operation ", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LDM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDriverMas.sLDM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Driver_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDriverDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iLDMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_Driver_Master Where LDM_ID=" & iLDMid & "  And LDM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDriverStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Driver_Master Set LDM_IPAddress='" & sIPAddress & "',"
            sSql = sSql & " LDM_Status='A',LDM_DelFlag='A',LDM_ApprovedBy= " & iUserID & ",LDM_ApprovedOn=GetDate()"
            sSql = sSql & " Where LDM_CompID=" & iCompID & " And LDM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadAllDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LDM_ID")
            dt.Columns.Add("DriverName")
            dt.Columns.Add("LicenseNo")
            dt.Columns.Add("AadharNo")
            dt.Columns.Add("ContactNo")
            dt.Columns.Add("Status")


            sSql = "select LDM_ID,LDM_DriverName,LDM_LicenseNo,LDM_AadharNo,LDM_ContactNo,LDM_Status from Lgst_Driver_Master where LDM_CompID=" & iACID & " "
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LDM_ID")) = False Then
                    dRow("LDM_ID") = dtDetails.Rows(i)("LDM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LDM_DriverName")) = False Then
                    dRow("DriverName") = dtDetails.Rows(i)("LDM_DriverName")
                End If
                If IsDBNull(dtDetails.Rows(i)("LDM_LicenseNo")) = False Then
                    dRow("LicenseNo") = dtDetails.Rows(i)("LDM_LicenseNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LDM_AadharNo")) = False Then
                    dRow("AadharNo") = dtDetails.Rows(i)("LDM_AadharNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LDM_ContactNo")) = False Then
                    dRow("ContactNo") = dtDetails.Rows(i)("LDM_ContactNo")
                End If

                If dtDetails.Rows(i)("LDM_Status") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("LDM_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("LDM_Status") = "A" Then
                    dRow("Status") = "Activated"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails1(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sStatus As String) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LDM_ID")
            dt.Columns.Add("DriverName")
            dt.Columns.Add("LicenseNo")
            dt.Columns.Add("AadharNo")
            dt.Columns.Add("ContactNo")
            dt.Columns.Add("Status")


            sSql = "select LDM_ID,LDM_DriverName,LDM_LicenseNo,LDM_AadharNo,LDM_ContactNo,LDM_Status from Lgst_Driver_Master where LDM_CompID=" & iACID & "  and LDM_Status='" & sStatus & "'"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LDM_ID")) = False Then
                    dRow("LDM_ID") = dtDetails.Rows(i)("LDM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LDM_DriverName")) = False Then
                    dRow("DriverName") = dtDetails.Rows(i)("LDM_DriverName")
                End If
                If IsDBNull(dtDetails.Rows(i)("LDM_LicenseNo")) = False Then
                    dRow("LicenseNo") = dtDetails.Rows(i)("LDM_LicenseNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LDM_AadharNo")) = False Then
                    dRow("AadharNo") = dtDetails.Rows(i)("LDM_AadharNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LDM_ContactNo")) = False Then
                    dRow("ContactNo") = dtDetails.Rows(i)("LDM_ContactNo")
                End If

                If dtDetails.Rows(i)("LDM_Status") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("LDM_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("LDM_Status") = "A" Then
                    dRow("Status") = "Activated"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingDriver(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LDM_ID,(LDM_DriverName + ' - ' + LDM_LicenseNo) as LDM_DriverName From Lgst_Driver_Master Where LDM_CompID=" & iCompID & "  AND LDM_Status <>'D' and  ldm_id not in (select LTGM_Driver from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and ltgm_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDriver(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LDM_ID,(LDM_DriverName + ' - ' + LDM_LicenseNo) as LDM_DriverName From Lgst_Driver_Master Where LDM_CompID=" & iCompID & "" ' and  ldm_id in (select LTGM_Driver from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and ltgm_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Head=0 and GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOASubGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Head=1 and GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Head=2 and GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDriverStatusDashboard(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Driver_Master Set LDM_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " LDM_Status='A',LDM_DelFlag='A',LDM_ApprovedBy= " & iUserID & " , LDM_ApprovedOn=GetDate()"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " LDM_Status='D',LDM_DelFlag='D',LDM_DeletedBy= " & iUserID & " , LDM_DeletedOn=GetDate()"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " LDM_Status='A',LDM_DelFlag='A' "
            End If
            sSql = sSql & " Where LDM_CompID=" & iCompID & " And LDM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateDriverMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Lgst_Driver_Master Where LDM_DelFlag='" & sSelectedStatus & "' And LDM_CompID=" & iCompID & "  "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Lgst_Driver_Master Set LDM_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " LDM_Status='A',LDM_DelFlag='A',LDM_ApprovedBy= " & iUserID & ",LDM_ApprovedOn=GetDate()"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " LDM_Status='D',LDM_DelFlag='D',LDM_DeletedBy= " & iUserID & ",LDM_DeletedOn=GetDate()"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " LDM_Status='A',LDM_DelFlag='A' "
                    End If
                    sSql = sSql & " Where LDM_CompID=" & iCompID & " And LDM_ID = " & iMasId & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
