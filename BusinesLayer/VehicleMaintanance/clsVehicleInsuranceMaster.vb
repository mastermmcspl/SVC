Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsVehicleInsuranceMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iLVID_ID As Integer
    Private iLVID_MasterID As Integer
    Private sLVID_RegNo As String
    Private sLVID_PolicyNo As String
    Private sLVID_InsCompany As String
    Private dLVID_InsFromDate As Date
    Private dLVID_InsToDate As Date
    Private dLVID_InsPaidDt As Date
    Private dLVID_InsPaidAmt As Double
    Private dLVID_InsInterestAmt As Double
    Private dLVID_TotalAmt As Double
    Private sLVID_Reference As String
    Private sLVID_DelFlag As String
    Private sLVID_Status As String
    Private iLVID_CreatedBy As Integer
    Private dLVID_CreatedOn As Date
    Private iLVID_UpdatedBy As Integer
    Private dLVID_UpdatedOn As Date
    Private iLVID_CompID As Integer
    Private iLVID_YearID As Integer
    Private sLVID_Operation As String
    Private sLVID_IPAddress As String




    Public Property LVID_ID() As Integer
        Get
            Return (iLVID_ID)
        End Get
        Set(ByVal Value As Integer)
            iLVID_ID = Value
        End Set
    End Property
    Public Property LVID_MasterID() As Integer
        Get
            Return (iLVID_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iLVID_MasterID = Value
        End Set
    End Property
    Public Property LVID_RegNo() As String
        Get
            Return (sLVID_RegNo)
        End Get
        Set(ByVal Value As String)
            sLVID_RegNo = Value
        End Set
    End Property
    Public Property LVID_PolicyNo() As String
        Get
            Return (sLVID_PolicyNo)
        End Get
        Set(ByVal Value As String)
            sLVID_PolicyNo = Value
        End Set
    End Property
    Public Property LVID_InsCompany() As String
        Get
            Return (sLVID_InsCompany)
        End Get
        Set(ByVal Value As String)
            sLVID_InsCompany = Value
        End Set
    End Property
    Public Property LVID_InsFromDate() As Date
        Get
            Return (dLVID_InsFromDate)
        End Get
        Set(ByVal Value As Date)
            dLVID_InsFromDate = Value
        End Set
    End Property
    Public Property LVID_InsToDate() As Date
        Get
            Return (dLVID_InsToDate)
        End Get
        Set(ByVal Value As Date)
            dLVID_InsToDate = Value
        End Set
    End Property
    Public Property LVID_InsPaidDt() As Date
        Get
            Return (dLVID_InsPaidDt)
        End Get
        Set(ByVal Value As Date)
            dLVID_InsPaidDt = Value
        End Set
    End Property
    Public Property LVID_InsPaidAmt() As Double
        Get
            Return (dLVID_InsPaidAmt)
        End Get
        Set(ByVal Value As Double)
            dLVID_InsPaidAmt = Value
        End Set
    End Property
    Public Property LVID_InsInterestAmt() As Double
        Get
            Return (dLVID_InsInterestAmt)
        End Get
        Set(ByVal Value As Double)
            dLVID_InsInterestAmt = Value
        End Set
    End Property

    Public Property LVID_TotalAmt() As Double
        Get
            Return (dLVID_TotalAmt)
        End Get
        Set(ByVal Value As Double)
            dLVID_TotalAmt = Value
        End Set
    End Property
    Public Property LVID_Reference() As String
        Get
            Return (sLVID_Reference)
        End Get
        Set(ByVal Value As String)
            sLVID_Reference = Value
        End Set
    End Property
    Public Property LVID_DelFlag() As String
        Get
            Return (sLVID_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLVID_DelFlag = Value
        End Set
    End Property
    Public Property LVID_Status() As String
        Get
            Return (sLVID_Status)
        End Get
        Set(ByVal Value As String)
            sLVID_Status = Value
        End Set
    End Property
    Public Property LVID_CreatedBy() As Integer
        Get
            Return (iLVID_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVID_CreatedBy = Value
        End Set
    End Property
    Public Property LVID_CreatedOn() As DateTime
        Get
            Return (dLVID_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVID_CreatedOn = Value
        End Set
    End Property
    Public Property LVID_UpdatedBy() As Integer
        Get
            Return (iLVID_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVID_UpdatedBy = Value
        End Set
    End Property
    Public Property LVID_UpdatedOn() As DateTime
        Get
            Return (dLVID_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVID_UpdatedOn = Value
        End Set
    End Property

    Public Property LVID_CompID() As Integer
        Get
            Return (iLVID_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLVID_CompID = Value
        End Set
    End Property
    Public Property LVID_YearID() As Integer
        Get
            Return (iLVID_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLVID_YearID = Value
        End Set
    End Property
    Public Property LVID_Operation() As String
        Get
            Return (sLVID_Operation)
        End Get
        Set(ByVal Value As String)
            sLVID_Operation = Value
        End Set
    End Property
    Public Property LVID_IPAddress() As String
        Get
            Return (sLVID_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLVID_IPAddress = Value
        End Set
    End Property

    Public Function SaveInsuranceDetails(ByVal sNameSpace As String, ByVal objLVID As clsVehicleInsuranceMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVID.iLVID_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVID.iLVID_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_RegNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVID.sLVID_RegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_PolicyNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVID.sLVID_PolicyNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_InsCompany ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVID.sLVID_InsCompany
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_InsFromDate ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVID.dLVID_InsFromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_InsToDate ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVID.dLVID_InsToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_InsPaidDt ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVID.dLVID_InsPaidDt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_InsPaidAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLVID.dLVID_InsPaidAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_InsInterestAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLVID.dLVID_InsInterestAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_TotalAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLVID.dLVID_TotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_Reference ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVID.sLVID_Reference
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVID.sLVID_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVID.sLVID_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVID.iLVID_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVID.dLVID_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVID.iLVID_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVID.dLVID_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVID.iLVID_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVID.iLVID_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVID.sLVID_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVID_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLVID.sLVID_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Vehicle_InsuranceDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInsVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select distinct LVID_MasterID,LVID_RegNo From Lgst_Vehicle_InsuranceDetails Where LVID_CompID=" & iCompID & "" ' and lvm_id in (select LVLM_VehivleNo from Lgst_TripGeneration_Master where LVLM_TripStatus =1 and LVLM_compid= " & iCompID & ") "
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
            sSql = "Select LVM_ID,LVM_RegNo From Lgst_Vehicle_Master Where LVM_CompID=" & iCompID & " and LVM_ID in (select LVAM_MasterID from Lgst_Vehicle_AdditionalMaster)" ' and lvm_id in (select LVLM_VehivleNo from Lgst_TripGeneration_Master where LVLM_TripStatus =1 and LVLM_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
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
    Public Function LoadInsuranceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehId As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("InsID")
            dt.Columns.Add("InsPolicyNO")
            dt.Columns.Add("InsCompany")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("PaidDate")
            dt.Columns.Add("PaidAmt")
            dt.Columns.Add("InterestAmt")
            dt.Columns.Add("TotalAmt")
            dt.Columns.Add("ReferenceDet")

            sSql = "Select * from Lgst_Vehicle_InsuranceDetails Where LVID_DelFlag<>'D' And LVID_MasterID=" & iVehId & " And LVID_CompID=" & iCompID & "   Order by LVID_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("InsID") = dr("LVID_ID")
                    dRow("InsPolicyNO") = dr("LVID_PolicyNo")
                    dRow("InsCompany") = dr("LVID_InsCompany")
                    dRow("FromDate") = objGen.FormatDtForRDBMS(dr("LVID_InsFromDate").ToString(), "D")
                    dRow("ToDate") = objGen.FormatDtForRDBMS(dr("LVID_InsToDate").ToString(), "D")
                    dRow("PaidDate") = objGen.FormatDtForRDBMS(dr("LVID_InsPaidDt").ToString(), "D")
                    dRow("PaidAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LVID_InsPaidAmt").ToString()))
                    dRow("InterestAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LVID_InsInterestAmt").ToString()))
                    dRow("TotalAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LVID_TotalAmt").ToString()))
                    dRow("ReferenceDet") = dr("LVID_Reference")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleInsuranceDashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVM_ID")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("InsuranceType")
            dt.Columns.Add("InsuranceNo")
            dt.Columns.Add("InsuranceAmt")
            dt.Columns.Add("Status")


            sSql = "select * from Lgst_Vehicle_Master where LVM_CompID=" & iCompID & " and LVM_ID in (select LVAM_MasterID from Lgst_Vehicle_AdditionalMaster)"
            'If iStatus = 0 Then
            '    sSql = sSql & " And LVM_DelFlag ='A'" 'Activated
            'ElseIf iStatus = 1 Then
            '    sSql = sSql & " And LVM_DelFlag='D'" 'De-Activated
            'ElseIf iStatus = 2 Then
            '    sSql = sSql & " And LVM_DelFlag='W'" 'Waiting for approval
            'End If
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVM_ID")) = False Then
                    dRow("LVM_ID") = dtDetails.Rows(i)("LVM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_RegNo")) = False Then
                    dRow("RegistrationNo") = dtDetails.Rows(i)("LVM_RegNo")
                End If
                If dtDetails.Rows(i)("LVM_InsuranceType") = "1" Then
                    dRow("InsuranceType") = "1st Party"
                ElseIf dtDetails.Rows(i)("LVM_InsuranceType") = "2" Then
                    dRow("InsuranceType") = "2nd Party"
                ElseIf dtDetails.Rows(i)("LVM_InsuranceType") = "3" Then
                    dRow("InsuranceType") = "3rd Party"
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_InsuranceNo")) = False Then
                    dRow("InsuranceNo") = dtDetails.Rows(i)("LVM_InsuranceNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVM_InsuranceAmt")) = False Then
                    dRow("InsuranceAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("LVM_InsuranceAmt").ToString()))

                End If
                If dtDetails.Rows(i)("LVM_Status") = "C" Then
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
    Public Function GetInsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_Vehicle_InsuranceDetails Where LVID_DelFlag<>'D' And LVID_ID=" & iID & " And LVID_MasterID=" & iMasterID & " And LVID_CompID=" & iCompID & " and LVID_YearID= " & iYearId & " Order by LVID_ID"
            GetInsDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetInsDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteInsValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Vehicle_TyreMaster Set LVTM_DelFlag='D' Where LVTM_ID=" & iID & " And LVTM_MasterID=" & iMasterID & " And LVTM_CompID=" & iCompID & " and LVTM_YearID= " & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    '*****
    Public Function GetDebitAmt(ByVal sNameSpace As String, ByVal iCompID As Integer) As Double
        Dim sSql As String = ""
        Dim dDebitAmt As Double = 0.0
        Dim dtDetails As New DataTable
        Try
            sSql = " select gl_AccHead,gl_id from chart_of_Accounts where gl_desc= 'Vehicle Insurance'"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            sSql = "" : sSql = "select sum(ATD_Debit) as TotalDebit from acc_Transactions_Details where ATD_Head='" & dtDetails.Rows(0)("gl_AccHead").ToString() & "'  and atd_gl='" & dtDetails.Rows(0)("gl_id").ToString() & "' And ATD_DbOrCr=1 and ATD_CompID=" & iCompID & ""
            '   sSql = "" : sSql = "select sum(ATD_Debit) as TotalDebit from acc_Transactions_Details where ATD_Head=3 and atd_gl=228 and ATD_SubGL=229 and ATD_DbOrCr=1 and ATD_CompID=" & iCompID & ""
            dDebitAmt = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return dDebitAmt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInsuranceAmt(ByVal sNameSpace As String, ByVal iCompID As Integer) As Double
        Dim sSql As String = ""
        Dim dInsuranceAmt As Double = 0.0
        Try
            sSql = "" : sSql = "select sum(LVID_TotalAmt) as totalAmt from Lgst_Vehicle_InsuranceDetails where LVID_CompID=" & iCompID & ""
            dInsuranceAmt = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return dInsuranceAmt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
