Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsVehicleAccidentDetails
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iLVAD_ID As Integer
    Private iLVAD_MasterID As Integer
    Private sLVAD_RegNo As String
    Private dLVAD_AccidentDt As Date
    Private sLVAD_DamageDtls As String
    Private sLVAD_ComplaintDtls As String
    Private sLVAD_CaseDtls As String
    Private sLVAD_VehcileDtls As String

    Private sLVAD_Delflag As String
    Private iLVAD_CompID As Integer
    Private iLVAD_YearID As Integer
    Private sLVAD_Status As String
    Private sLVAD_Operation As String
    Private sLVAD_IPAddress As String
    Private iLVAD_CreatedBy As Integer
    Private dLVAD_CreatedOn As DateTime
    Private iLVAD_ApprovedBy As Integer
    Private dLVAD_ApprovedOn As DateTime
    Private iLVAD_DeletedBy As Integer
    Private dLVAD_DeletedOn As DateTime
    Private iLVAD_UpdatedBy As Integer
    Private dLVAD_UpdatedOn As DateTime
    Private iLVAD_RecalldBy As Integer
    Public Property LVAD_ID() As Integer
        Get
            Return (iLVAD_ID)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_ID = Value
        End Set
    End Property
    Public Property LVAD_MasterID() As Integer
        Get
            Return (iLVAD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_MasterID = Value
        End Set
    End Property

    Public Property LVAD_RegNo() As String
        Get
            Return (sLVAD_RegNo)
        End Get
        Set(ByVal Value As String)
            sLVAD_RegNo = Value
        End Set
    End Property
    Public Property LVAD_AccidentDt() As Date
        Get
            Return (dLVAD_AccidentDt)
        End Get
        Set(ByVal Value As Date)
            dLVAD_AccidentDt = Value
        End Set
    End Property
    Public Property LVAD_DamageDtls() As String
        Get
            Return (sLVAD_DamageDtls)
        End Get
        Set(ByVal Value As String)
            sLVAD_DamageDtls = Value
        End Set
    End Property
    Public Property LVAD_ComplaintDtls() As String
        Get
            Return (sLVAD_ComplaintDtls)
        End Get
        Set(ByVal Value As String)
            sLVAD_ComplaintDtls = Value
        End Set
    End Property
    Public Property LVAD_CaseDtls() As String
        Get
            Return (sLVAD_CaseDtls)
        End Get
        Set(ByVal Value As String)
            sLVAD_CaseDtls = Value
        End Set
    End Property
    Public Property LVAD_VehcileDtls() As String
        Get
            Return (sLVAD_VehcileDtls)
        End Get
        Set(ByVal Value As String)
            sLVAD_VehcileDtls = Value
        End Set
    End Property
    Public Property LVAD_Delflag() As String
        Get
            Return (sLVAD_Delflag)
        End Get
        Set(ByVal Value As String)
            sLVAD_Delflag = Value
        End Set
    End Property
    Public Property LVAD_CompID() As Integer
        Get
            Return (iLVAD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_CompID = Value
        End Set
    End Property
    Public Property LVAD_YearID() As Integer
        Get
            Return (iLVAD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_YearID = Value
        End Set
    End Property
    Public Property LVAD_Status() As String
        Get
            Return (sLVAD_Status)
        End Get
        Set(ByVal Value As String)
            sLVAD_Status = Value
        End Set
    End Property
    Public Property LVAD_Operation() As String
        Get
            Return (sLVAD_Operation)
        End Get
        Set(ByVal Value As String)
            sLVAD_Operation = Value
        End Set
    End Property
    Public Property LVAD_IPAddress() As String
        Get
            Return (sLVAD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLVAD_IPAddress = Value
        End Set
    End Property
    Public Property LVAD_CreatedBy() As Integer
        Get
            Return (iLVAD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_CreatedBy = Value
        End Set
    End Property
    Public Property LVAD_CreatedOn() As Date
        Get
            Return (dLVAD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLVAD_CreatedOn = Value
        End Set
    End Property

    Public Property LVAD_ApprovedBy() As Integer
        Get
            Return (iLVAD_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_ApprovedBy = Value
        End Set
    End Property
    Public Property LVAD_ApprovedOn() As Date
        Get
            Return (dLVAD_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLVAD_ApprovedOn = Value
        End Set
    End Property
    Public Property LVAD_DeletedBy() As Integer
        Get
            Return (iLVAD_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_DeletedBy = Value
        End Set
    End Property
    Public Property LVAD_DeletedOn() As Date
        Get
            Return (dLVAD_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLVAD_DeletedOn = Value
        End Set
    End Property
    Public Property LVAD_UpdatedBy() As Integer
        Get
            Return (iLVAD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_UpdatedBy = Value
        End Set
    End Property
    Public Property LVAD_UpdatedOn() As Date
        Get
            Return (dLVAD_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLVAD_UpdatedOn = Value
        End Set
    End Property
    Public Property LVAD_RecalldBy() As Integer
        Get
            Return (iLVAD_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLVAD_RecalldBy = Value
        End Set
    End Property

    Public Function SaveLoanMater(ByVal sNameSpace As String, ByVal objLVAD As clsVehicleAccidentDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVAD.iLVAD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVAD.iLVAD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_RegNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_RegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_AccidentDt ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVAD.dLVAD_AccidentDt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_DamageDtls ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_DamageDtls
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_ComplaintDtls ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_ComplaintDtls
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_CaseDtls ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_CaseDtls
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_VehcileDtls ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_VehcileDtls
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVAD.iLVAD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVAD.dLVAD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVAD.iLVAD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVAD.dLVAD_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVAD.iLVAD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVAD.iLVAD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVAD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLVAD.sLVAD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Vehicle_AccidentDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVAM_ID,LVAM_RegNo From Lgst_Vehicle_AdditionalMaster Where LVAM_CompID=" & iCompID & "" ' and lvm_id in (select LVLM_VehivleNo from Lgst_TripGeneration_Master where LVLM_TripStatus =1 and LVLM_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccidentVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVAD_MasterID,LVAD_RegNo From Lgst_Vehicle_AccidentDetails Where LVAD_CompID=" & iCompID & "" ' and lvm_id in (select LVLM_VehivleNo from Lgst_TripGeneration_Master where LVLM_TripStatus =1 and LVLM_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccidentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehId As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("AccID")
            dt.Columns.Add("DateOfAccident")
            dt.Columns.Add("DamageDetails")
            dt.Columns.Add("PoliceCompliantDetails")
            dt.Columns.Add("CaseDetails")
            dt.Columns.Add("VehicleStatus")
            sSql = "Select * from Lgst_Vehicle_AccidentDetails Where LVAD_DelFlag<>'D' And LVAD_MasterID=" & iVehId & " And LVAD_CompID=" & iCompID & "   Order by LVAD_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("AccID") = dr("LVAD_ID")
                    dRow("DateOfAccident") = objGen.FormatDtForRDBMS(dr("LVAD_AccidentDt").ToString(), "D")
                    dRow("DamageDetails") = dr("LVAD_DamageDtls")
                    dRow("PoliceCompliantDetails") = dr("LVAD_ComplaintDtls")
                    dRow("CaseDetails") = dr("LVAD_CaseDtls")
                    dRow("VehicleStatus") = dr("LVAD_VehcileDtls")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVehCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iVehID As Integer, ByVal sVehNo As String) As Integer
        Dim sSql As String = ""
        Dim iCount As Integer
        Try
            sSql = "select count(*) from Lgst_Vehicle_AccidentDetails where LVAD_MasterID=" & iVehID & " and LVAD_RegNo='" & sVehNo & "' and LVAD_Compid=" & iCompID & " and LVAD_YearId=" & iYearID & ""
            iCount = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccidentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_Vehicle_AccidentDetails Where LVAD_DelFlag<>'D' And LVAD_ID=" & iID & " And LVAD_MasterID=" & iMasterID & " And LVAD_CompID=" & iCompID & " and LVAD_YearID= " & iYearId & " Order by LVAD_ID"
            GetAccidentDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetAccidentDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteComplianceValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Vehicle_AccidentDetails Set LVAD_DelFlag='D' Where LVAD_ID=" & iID & " And LVAD_TripID=" & iMasterID & " And LVAD_CompID=" & iCompID & " and LVAD_YearID= " & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadVehicleAccidentDashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVAD_ID")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("AccidentDt")
            dt.Columns.Add("CaseDtls")
            dt.Columns.Add("Status")


            sSql = "select * from Lgst_Vehicle_AccidentDetails where LVAD_CompID=" & iCompID & ""
            'If iStatus = 0 Then
            '    sSql = sSql & " And LVAD_DelFlag ='A'" 'Activated
            'ElseIf iStatus = 1 Then
            '    sSql = sSql & " And LVAD_DelFlag='D'" 'De-Activated
            'ElseIf iStatus = 2 Then
            '    sSql = sSql & " And LVAD_DelFlag='W'" 'Waiting for approval
            'End If
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVAD_MasterID")) = False Then
                    dRow("LVAD_ID") = dtDetails.Rows(i)("LVAD_MasterID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAD_RegNo")) = False Then
                    dRow("RegistrationNo") = dtDetails.Rows(i)("LVAD_RegNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAD_AccidentDt")) = False Then
                    dRow("AccidentDt") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LVAD_AccidentDt").ToString(), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAD_CaseDtls")) = False Then
                    dRow("CaseDtls") = dtDetails.Rows(i)("LVAD_CaseDtls")
                End If
                If dtDetails.Rows(i)("LVAD_Status") = "C" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("LVAD_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("LVAD_Status") = "A" Then
                    dRow("Status") = "Activated"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
