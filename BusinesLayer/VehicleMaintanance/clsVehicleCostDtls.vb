Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsVehicleCostDtls
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Public Function LoadMilageDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCSMid As Integer) As DataTable
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
    Public Function GetFuelReading(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iVehId As Integer) As Double
        Dim sSql As String = ""
        Dim dFuelVal As Double = 0.0
        Try
            sSql = "" : sSql = "select sum(LTGDD_DieselinLtrs) as diesel from Lgst_TripGenDiesel_Details where "
            sSql = sSql & "LTGDD_TripID in ( select  LTGM_ID from Lgst_TripGeneration_Master where LTGM_VehivleNo= " & iVehId & ")"
            dFuelVal = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return dFuelVal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetKMReading(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iVehId As Integer) As Double
        Dim sSql As String = ""
        Dim dKMVal As Double = 0.0
        Try
            sSql = "" : sSql = "Select sum(LTGM_DistanceinKms) As distance from Lgst_TripGeneration_Master where "
            sSql = sSql & "LTGM_compid=" & iCompID & " And LTGM_VehivleNo = " & iVehId & "And LTGM_TripStatus=2"
            dKMVal = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return dKMVal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVAM_MasterId,LVAM_RegNo From Lgst_Vehicle_AdditionalMaster Where LVAM_CompID=" & iCompID & "" ' and lvm_id in (select LVAM_VehivleNo from Lgst_TripGeneration_Master where LVAM_TripStatus =1 and LVAM_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleCostDashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVAM_ID")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("InvoiceNo")
            dt.Columns.Add("VehicleManufacturer")
            dt.Columns.Add("Status")


            sSql = "select * from Lgst_Vehicle_AdditionalMaster where LVAM_CompID=" & iCompID & ""
            If iStatus = 0 Then
                sSql = sSql & " And LVAM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And LVAM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And LVAM_DelFlag='W'" 'Waiting for approval
            End If
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVAM_MasterID")) = False Then
                    dRow("LVAM_ID") = dtDetails.Rows(i)("LVAM_MasterID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_RegNo")) = False Then
                    dRow("RegistrationNo") = dtDetails.Rows(i)("LVAM_RegNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_VehicleInvoiceNo")) = False Then
                    dRow("InvoiceNo") = dtDetails.Rows(i)("LVAM_VehicleInvoiceNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_VehicleManufacturer")) = False Then
                    dRow("VehicleManufacturer") = dtDetails.Rows(i)("LVAM_VehicleManufacturer")
                End If
                If dtDetails.Rows(i)("LVAM_Status") = "C" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("LVAm_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("LVAM_Status") = "A" Then
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
