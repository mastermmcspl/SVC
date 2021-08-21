Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsVehicleMaster
    Private objDBL As New DatabaseLayer.DBHelper

    Private iLVM_ID As Integer
    Private sLVM_RegNo As String
    Private sLVM_ChassisNo As String
    Private sLVM_EngineNo As String
    Private iLVM_VehicleType As Integer
    Private sLVM_OwnerName As String
    Private sLVM_ServiceCntrDtls As String
    Private sLVM_VehicleDetails As String
    Private iLVM_InsuranceType As Integer
    Private sLVM_InsuranceNo As String
    Private dLVM_InsuranceAmt As Double
    Private dLVM_InsuranceExpDate As DateTime
    Private sLVM_InsuranceDetails As String
    Private sLVM_DelFlag As String
    Private sLVM_Status As String
    Private iLVM_CreatedBy As Integer
    Private dLVM_CreatedOn As DateTime
    Private iLVM_UpdatedBy As Integer
    Private dLVM_UpdatedOn As DateTime
    Private iLVM_ApprovedBy As Integer
    Private dLVM_ApprovedOn As DateTime
    Private iLVM_DeletedBy As Integer
    Private dLVM_DeletedOn As DateTime
    Private iLVM_RecalldBy As Integer
    Private iLVM_Group As Integer
    Private iLVM_SubGroup As Integer
    Private iLVM_GL As Integer
    Private iLVM_SubGL As Integer
    Private iLVM_CompID As Integer
    Private iLVM_YearID As Integer
    Private sLVM_Operation As String
    Private sLVM_IPAddress As String

    Public Property LVM_ID() As Integer
        Get
            Return (iLVM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLVM_ID = Value
        End Set
    End Property
    Public Property LVM_RegNo() As String
        Get
            Return (sLVM_RegNo)
        End Get
        Set(ByVal Value As String)
            sLVM_RegNo = Value
        End Set
    End Property
    Public Property LVM_ChassisNo() As String
        Get
            Return (sLVM_ChassisNo)
        End Get
        Set(ByVal Value As String)
            sLVM_ChassisNo = Value
        End Set
    End Property
    Public Property LVM_EngineNo() As String
        Get
            Return (sLVM_EngineNo)
        End Get
        Set(ByVal Value As String)
            sLVM_EngineNo = Value
        End Set
    End Property
    Public Property LVM_VehicleType() As Integer
        Get
            Return (iLVM_VehicleType)
        End Get
        Set(ByVal Value As Integer)
            iLVM_VehicleType = Value
        End Set
    End Property
    Public Property LVM_OwnerName() As String
        Get
            Return (sLVM_OwnerName)
        End Get
        Set(ByVal Value As String)
            sLVM_OwnerName = Value
        End Set
    End Property
    Public Property LVM_ServiceCntrDtls() As String
        Get
            Return (sLVM_ServiceCntrDtls)
        End Get
        Set(ByVal Value As String)
            sLVM_ServiceCntrDtls = Value
        End Set
    End Property
    Public Property LVM_VehicleDetails() As String
        Get
            Return (sLVM_VehicleDetails)
        End Get
        Set(ByVal Value As String)
            sLVM_VehicleDetails = Value
        End Set
    End Property
    Public Property LVM_InsuranceType() As Integer
        Get
            Return (iLVM_InsuranceType)
        End Get
        Set(ByVal Value As Integer)
            iLVM_InsuranceType = Value
        End Set
    End Property
    Public Property LVM_InsuranceNo() As String
        Get
            Return (sLVM_InsuranceNo)
        End Get
        Set(ByVal Value As String)
            sLVM_InsuranceNo = Value
        End Set
    End Property
    Public Property LVM_InsuranceAmt() As Double
        Get
            Return (dLVM_InsuranceAmt)
        End Get
        Set(ByVal Value As Double)
            dLVM_InsuranceAmt = Value
        End Set
    End Property
    Public Property LVM_InsuranceExpDate() As Date
        Get
            Return (dLVM_InsuranceExpDate)
        End Get
        Set(ByVal Value As Date)
            dLVM_InsuranceExpDate = Value
        End Set
    End Property
    Public Property LVM_InsuranceDetails() As String
        Get
            Return (sLVM_InsuranceDetails)
        End Get
        Set(ByVal Value As String)
            sLVM_InsuranceDetails = Value
        End Set
    End Property
    Public Property LVM_Delflag() As String
        Get
            Return (sLVM_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLVM_DelFlag = Value
        End Set
    End Property
    Public Property LVM_Status() As String
        Get
            Return (sLVM_Status)
        End Get
        Set(ByVal Value As String)
            sLVM_Status = Value
        End Set
    End Property
    Public Property LVM_CreatedBy() As Integer
        Get
            Return (iLVM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVM_CreatedBy = Value
        End Set
    End Property

    Public Property LVM_CreatedOn() As DateTime
        Get
            Return (dLVM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVM_CreatedOn = Value
        End Set
    End Property
    Public Property LVM_UpdatedBy() As Integer
        Get
            Return (iLVM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVM_UpdatedBy = Value
        End Set
    End Property
    Public Property LVM_UpdatedOn() As DateTime
        Get
            Return (dLVM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVM_UpdatedOn = Value
        End Set
    End Property
    Public Property LVM_ApprovedBy() As Integer
        Get
            Return (iLVM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVM_ApprovedBy = Value
        End Set
    End Property



    Public Property LVM_ApprovedOn() As DateTime
        Get
            Return (dLVM_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVM_ApprovedOn = Value
        End Set
    End Property
    Public Property LVM_DeletedBy() As Integer
        Get
            Return (iLVM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVM_DeletedBy = Value
        End Set
    End Property
    Public Property LVM_DeletedOn() As DateTime
        Get
            Return (dLVM_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVM_DeletedOn = Value
        End Set
    End Property
    Public Property LVM_RecalldBy() As Integer
        Get
            Return (iLVM_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLVM_RecalldBy = Value
        End Set
    End Property
    Public Property LVM_Group() As Integer
        Get
            Return (iLVM_Group)
        End Get
        Set(ByVal Value As Integer)
            iLVM_Group = Value
        End Set
    End Property
    Public Property LVM_SubGroup() As Integer
        Get
            Return (iLVM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            iLVM_SubGroup = Value
        End Set
    End Property
    Public Property LVM_GL() As Integer
        Get
            Return (iLVM_GL)
        End Get
        Set(ByVal Value As Integer)
            iLVM_GL = Value
        End Set
    End Property
    Public Property LVM_SubGL() As Integer
        Get
            Return (iLVM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iLVM_SubGL = Value
        End Set
    End Property
    Public Property LVM_CompID() As Integer
        Get
            Return (iLVM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLVM_CompID = Value
        End Set
    End Property
    Public Property LVM_YearID() As Integer
        Get
            Return (iLVM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLVM_YearID = Value
        End Set
    End Property
    Public Property LVM_Operation() As String
        Get
            Return (sLVM_Operation)
        End Get
        Set(ByVal Value As String)
            sLVM_Operation = Value
        End Set
    End Property
    Public Property LVM_IPAddress() As String
        Get
            Return (sLVM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLVM_IPAddress = Value
        End Set
    End Property
    'Public Function LoadExistingVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select LVM_ID,LVM_RegNo From Lgst_Vehicle_Master Where LVM_CompID=" & iCompID & " and LVM_Status <> 'D' "
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadVehicleDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCSMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_Vehicle_Master Where LVM_ID=" & iCSMid & "  And LVM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDuplicateRegNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sRegNo As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From Lgst_Vehicle_Master Where LVM_RegNo='" & sRegNo & "' And LVM_DelFlag<>'D' And LVM_CompID=" & iCompID & "  "
            CheckDuplicateRegNo = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckDuplicateRegNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveVehicleDetails(ByVal sNameSpace As String, ByVal objVehicleMas As clsVehicleMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(33) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_RegNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_RegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_ChassisNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_ChassisNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_EngineNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_EngineNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_VehicleType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_VehicleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_OwnerName", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_OwnerName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_ServiceCntrDtls", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_ServiceCntrDtls
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_VehicleDetails", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_VehicleDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_InsuranceType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_InsuranceType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_InsuranceNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_InsuranceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_InsuranceAmt", OleDb.OleDbType.Double, 200)
            ObjParam(iParamCount).Value = objVehicleMas.dLVM_InsuranceAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_InsuranceExpDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objVehicleMas.dLVM_InsuranceExpDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_InsuranceDetails", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_InsuranceDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objVehicleMas.dLVM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objVehicleMas.dLVM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_ApprovedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objVehicleMas.dLVM_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_DeletedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_DeletedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_DeletedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objVehicleMas.dLVM_DeletedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_RecalldBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_RecalldBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_SubGroup ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objVehicleMas.iLVM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_Operation ", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objVehicleMas.sLVM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Vehicle_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateVehicleStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Vehicle_Master Set LVM_IPAddress='" & sIPAddress & "',"
            sSql = sSql & " LVM_Status='A',LVM_DelFlag='A',LVM_ApprovedBy= " & iUserID & ",LVM_ApprovedOn=GetDate()"
            sSql = sSql & " Where LVM_CompID=" & iCompID & " And LVM_ID = " & iMasId & ""
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
            dt.Columns.Add("LVM_ID")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("VehicleType")
            dt.Columns.Add("OwnerName")
            dt.Columns.Add("ChassisNo")
            dt.Columns.Add("Status")


            sSql = "select * from Lgst_Vehicle_Master where LVM_CompID=" & iACID & " "
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVM_ID")) = False Then
                    dRow("LVM_ID") = dtDetails.Rows(i)("LVM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_RegNo")) = False Then
                    dRow("RegistrationNo") = dtDetails.Rows(i)("LVM_RegNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_VehicleType")) = False Then
                    dRow("VehicleType") = getVechicleType(sAC, iACID, dtDetails.Rows(i)("LVM_VehicleType"))
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_OwnerName")) = False Then
                    dRow("OwnerName") = dtDetails.Rows(i)("LVM_OwnerName")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_ChassisNo")) = False Then
                    dRow("ChassisNo") = dtDetails.Rows(i)("LVM_ChassisNo")
                End If
                If dtDetails.Rows(i)("LVM_Status") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("LVm_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("LVM_Status") = "A" Then
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
            dt.Columns.Add("LVM_ID")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("VehicleType")
            dt.Columns.Add("OwnerName")
            dt.Columns.Add("ChassisNo")
            dt.Columns.Add("Status")


            sSql = "select * from Lgst_Vehicle_Master where LVM_CompID=" & iACID & " and LVM_Status='" & sStatus & "'"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVM_ID")) = False Then
                    dRow("LVM_ID") = dtDetails.Rows(i)("LVM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_RegNo")) = False Then
                    dRow("RegistrationNo") = dtDetails.Rows(i)("LVM_RegNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_VehicleType")) = False Then
                    dRow("VehicleType") = getVechicleType(sAC, iACID, dtDetails.Rows(i)("LVM_VehicleType"))
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_OwnerName")) = False Then
                    dRow("OwnerName") = dtDetails.Rows(i)("LVM_OwnerName")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_ChassisNo")) = False Then
                    dRow("ChassisNo") = dtDetails.Rows(i)("LVM_ChassisNo")
                End If
                If dtDetails.Rows(i)("LVM_Status") = "W" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("LVm_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("LVM_Status") = "A" Then
                    dRow("Status") = "Activated"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Vehicle Type' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getVechicleType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehicleTypeId As Integer) As String
        Dim sSql As String
        Dim sVehicleType As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select Mas_desc from ACC_General_Master where  Mas_id =" & iVehicleTypeId & "  "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Mas_desc").ToString()) = False Then
                    sVehicleType = dt.Rows(0)("Mas_desc").ToString()
                Else
                    sVehicleType = ""
                End If
            Else
                sVehicleType = ""
            End If
            Return sVehicleType
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVM_ID,LVM_RegNo From Lgst_Vehicle_Master Where LVM_CompID=" & iCompID & " and lvm_Delflag ='A'  and lvm_id not in (select LTGM_VehivleNo from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and ltgm_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVM_ID,LVM_RegNo From Lgst_Vehicle_Master Where LVM_CompID=" & iCompID & "" ' and lvm_id in (select LTGM_VehivleNo from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and ltgm_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateVehicleStatusDashboard(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Vehicle_Master Set LVM_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " LVM_Status='A',LVM_DelFlag='A',LVM_ApprovedBy= " & iUserID & " , LVM_ApprovedOn=GetDate()"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " LVM_Status='D',LVM_DelFlag='D',LVM_DeletedBy= " & iUserID & " , LVM_DeletedOn=GetDate()"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " LVM_Status='A',LVM_DelFlag='A' "
            End If
            sSql = sSql & " Where LVM_CompID=" & iCompID & " And LVM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateVehicleMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Lgst_Vehicle_Master Where LVM_DelFlag='" & sSelectedStatus & "' And LVM_CompID=" & iCompID & "  "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Lgst_Vehicle_Master Set LVM_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " LVM_Status='A',LVM_DelFlag='A',LVM_ApprovedBy= " & iUserID & ",LVM_ApprovedOn=GetDate()"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " LVM_Status='D',LVM_DelFlag='D',LVM_DeletedBy= " & iUserID & ",LVM_DeletedOn=GetDate()"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " LVM_Status='A',LVM_DelFlag='A' "
                    End If
                    sSql = sSql & " Where LVM_CompID=" & iCompID & " And LVM_ID = " & iMasId & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
