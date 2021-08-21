Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsDynamicReport
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadTimeStatusDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sStatus As String) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim sVehicleType As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LTGM_ID")
            dt.Columns.Add("TripNo")
            dt.Columns.Add("DriverName")
            dt.Columns.Add("VehicleType")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("AllottedTime")
            dt.Columns.Add("TakenTime")
            dt.Columns.Add("Status")
            dt.Columns.Add("Customer")
            dt.Columns.Add("Route")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("StartTime")
            dt.Columns.Add("StopDate")
            dt.Columns.Add("StopTime")
            dt.Columns.Add("DistanceinKms")
            dt.Columns.Add("Rate")
            dt.Columns.Add("PetrolQty")
            dt.Columns.Add("MRStart")
            dt.Columns.Add("MREnd")
            dt.Columns.Add("MtrStatus")
            dt.Columns.Add("TripStatus")
            sSql = "select * from Lgst_TripGeneration_Master where LTGM_CompID=" & iACID & " and LTGM_TripStatus=2 "
            If sStatus = "All" Then
            Else
                sSql = sSql & " and LTGM_TimeStatus ='" & sStatus & "'"
            End If
            sSql = sSql & " order by ltgm_id asc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LTGM_ID")) = False Then
                    dRow("LTGM_ID") = dtDetails.Rows(i)("LTGM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TransactionNo")) = False Then
                    dRow("TripNo") = dtDetails.Rows(i)("LTGM_TransactionNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo")) = False Then
                    dRow("RegistrationNo") = GetVehicleNo(sAC, iACID, dtDetails.Rows(i)("LTGM_VehivleNo").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo")) = False Then
                    dRow("DriverName") = GetDriverName(sAC, dtDetails.Rows(i)("LTGM_Driver").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_AllottedTime")) = False Then
                    dRow("AllottedTime") = dtDetails.Rows(i)("LTGM_AllottedTime")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TripTakenTime")) = False Then
                    dRow("TakenTime") = dtDetails.Rows(i)("LTGM_TripTakenTime")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TimeStatus")) = False Then
                    dRow("Status") = dtDetails.Rows(i)("LTGM_TimeStatus")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCustomer").ToString()) = False Then
                    dRow("Customer") = GetCustomerName(sAC, dtDetails.Rows(i)("LTGM_DestinationCustomer").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehicleType").ToString()) = False Then
                    sVehicleType = getVechicleType(sAC, iACID, dtDetails.Rows(i)("LTGM_VehicleType").ToString())
                    dRow("VehicleType") = sVehicleType
                End If
                If ((IsDBNull(dtDetails.Rows(i)("LTGM_StartCity").ToString()) = False) And (IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCity").ToString()) = False)) Then
                    dRow("Route") = dtDetails.Rows(i)("LTGM_StartCity").ToString() & " - " & dtDetails.Rows(i)("LTGM_DestinationCity").ToString() & " " & sVehicleType
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_StartDate").ToString()) = False Then
                    dRow("StartDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StartDate").ToString(), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_StartTime").ToString()) = False Then
                    dRow("StartTime") = dtDetails.Rows(i)("LTGM_StartTime").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_StopDate").ToString()) = False Then
                    dRow("StopDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StopDate").ToString(), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_StopTime").ToString()) = False Then
                    dRow("StopTime") = dtDetails.Rows(i)("LTGM_StopTime").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_DistanceinKms").ToString()) = False Then
                    dRow("DistanceinKms") = dtDetails.Rows(i)("LTGM_DistanceinKms").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_Rate").ToString()) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("LTGM_Rate").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_PetrolQty").ToString()) = False Then
                    dRow("PetrolQty") = dtDetails.Rows(i)("LTGM_PetrolQty").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MRStart").ToString()) = False Then
                    dRow("MRStart") = dtDetails.Rows(i)("LTGM_MRStart").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MREnd").ToString()) = False Then
                    dRow("MREnd") = dtDetails.Rows(i)("LTGM_MREnd").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MRStatus").ToString()) = False Then
                    dRow("MtrStatus") = dtDetails.Rows(i)("LTGM_MRStatus").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TripStatus").ToString()) = False Then
                    If dtDetails(i)("LTGM_TripStatus").ToString() = 1 Then
                        dRow("TripStatus") = "Start"
                    Else
                        dRow("TripStatus") = "End"
                    End If

                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVehicleNo(ByVal sNameSpace As String, ByVal iCompId As Integer, ByVal iVehID As Integer) As String
        Dim sSql As String = ""
        Dim sVehicleNo As String = ""
        Try
            sSql = "" : sSql = "Select LVM_RegNo from Lgst_Vehicle_Master where LVM_ID = '" & iVehID & "' and LVM_CompID=" & iCompId & ""
            sVehicleNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sVehicleNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIndentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal dFromDate As String, ByVal dToDate As String, ByVal iTrNo As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""

        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LTGDD_ID")
            dt.Columns.Add("TripNo")
            dt.Columns.Add("PumpName")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("IndDate")
            dt.Columns.Add("DieselinLtrs")
            dt.Columns.Add("DieselRatePerltr")
            dt.Columns.Add("OilInltr")
            dt.Columns.Add("OilAmountInLtr")
            dt.Columns.Add("DieselAmount")
            dt.Columns.Add("DriverAdvancGvnByPump")
            dt.Columns.Add("CreatedBy")
            dt.Columns.Add("Remarks")
            sSql = "Select * from Lgst_TripGenDiesel_Details where LTGDD_CompID=" & iACID & " and LTGDD_DelFlag<>'D'"
            If dFromDate = "01/01/1900" And dToDate = "01/01/1900" Then
                '  sSql = sSql & " And LTGDD_IndDate = getdate() "
            Else
                sSql = sSql & " And LTGDD_IndDate between '" & dFromDate & "' and '" & dFromDate & "' "
            End If
            If iTrNo <> 0 Then
                sSql = sSql & " And LTGDD_TripID = " & iTrNo & " "
            End If
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LTGDD_ID")) = False Then
                    dRow("LTGDD_ID") = dtDetails.Rows(i)("LTGDD_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_TripID")) = False Then
                    dRow("TripNo") = GetTripNo(sAC, iACID, dtDetails.Rows(i)("LTGDD_TripID").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_TripID")) = False Then
                    dRow("PumpName") = GetDieselPumpName(sAC, dtDetails.Rows(i)("LTGDD_PumpID").ToString(), iACID)
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_TripID")) = False Then
                    dRow("RegistrationNo") = GetVehicleNo(sAC, iACID, dtDetails.Rows(i)("LTGDD_TripID").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_IndDate")) = False Then
                    dRow("IndDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGDD_IndDate").ToString(), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_DieselinLtrs")) = False Then
                    dRow("DieselinLtrs") = dtDetails.Rows(i)("LTGDD_DieselinLtrs")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_DieselRatePerltr")) = False Then
                    dRow("DieselRatePerltr") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i).Item("LTGDD_DieselRatePerltr").ToString()))
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_OilInltr")) = False Then
                    dRow("OilInltr") = dtDetails.Rows(i)("LTGDD_OilInltr")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_OilAmountInLtr")) = False Then
                    dRow("OilAmountInLtr") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i).Item("LTGDD_OilAmountInLtr").ToString()))
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_DieselAmount")) = False Then
                    dRow("DieselAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i).Item("LTGDD_DieselAmount").ToString()))
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_DriverAdvancGvnByPump")) = False Then
                    dRow("DriverAdvancGvnByPump") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i).Item("LTGDD_DriverAdvancGvnByPump").ToString()))
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_CreatedBy").ToString()) = False Then
                    dRow("CreatedBy") = GetUserName(sAC, dtDetails.Rows(i)("LTGDD_CreatedBy").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGDD_Remarks")) = False Then
                    dRow("Remarks") = dtDetails.Rows(i)("LTGDD_Remarks")
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserName(ByVal sNameSpace As String, ByVal iLVMUsrID As Integer) As String
        Dim sSQL As String = ""
        Dim sUsrName As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select Usr_FullName  from Sad_UserDetails where usr_Id = " & iLVMUsrID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Usr_FullName").ToString()) = False Then
                    sUsrName = dt.Rows(0)("Usr_FullName").ToString()
                Else
                    sUsrName = ""
                End If
            Else
                sUsrName = ""
            End If
            Return sUsrName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDieselPumpName(ByVal sNameSpace As String, ByVal iPumpID As Integer, ByVal iCompID As Integer) As String
        Dim sSQL As String = ""
        Dim sPump As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LPM_PumpName from Lgst_Pump_Master where LPM_ID = " & iPumpID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LPM_PumpName").ToString()) = False Then
                    sPump = dt.Rows(0)("LPM_PumpName").ToString()
                Else
                    sPump = ""
                End If
            Else
                sPump = ""
            End If
            Return sPump
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTripNo(ByVal sNameSpace As String, ByVal iCompId As Integer, ByVal iDieselID As Integer) As String
        Dim sSql As String = ""
        Dim sVehicleNo As String = ""
        Try
            sSql = "" : sSql = "Select LTGM_TransactionNo from Lgst_TripGeneration_Master where LTGM_ID = '" & iDieselID & "' and LTGM_CompID=" & iCompId & ""
            sVehicleNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sVehicleNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTripDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iTripStatus As Integer, ByVal sFromDate As String, ByVal sToDate As String) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim sVehicleType As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LTGM_ID")
            dt.Columns.Add("TripNo")
            dt.Columns.Add("Customer")
            dt.Columns.Add("VehicleType")
            dt.Columns.Add("Route")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("VechicleNo")
            dt.Columns.Add("TripStatus")
            dt.Columns.Add("Status")
            dt.Columns.Add("DriverName")
            dt.Columns.Add("StartTime")
            dt.Columns.Add("StopDate")
            dt.Columns.Add("StopTime")
            dt.Columns.Add("DistanceinKms")
            dt.Columns.Add("Rate")
            dt.Columns.Add("PetrolQty")
            dt.Columns.Add("AllottedTime")
            dt.Columns.Add("TakenTime")
            dt.Columns.Add("TimeStatus")
            dt.Columns.Add("MRStart")
            dt.Columns.Add("MREnd")
            dt.Columns.Add("MtrStatus")
            sSql = "Select * from Lgst_TripGeneration_Master where LTGM_CompID =" & iACID & " and  LTGM_YearID=" & iyearId & ""
            If iTripStatus = 1 Then
                sSql = sSql & " And LTGM_TripStatus ='" & iTripStatus & "'"
                If sFromDate <> "01/01/1900" And sToDate <> "01/01/1900" Then
                    sSql = sSql & " And LTGM_StartDate between '" & sFromDate & "' and '" & sToDate & "'"
                End If
            ElseIf iTripStatus = 2 Then
                sSql = sSql & " And LTGM_TripStatus ='" & iTripStatus & "'"
                If sFromDate <> "01/01/1900" And sToDate <> "01/01/1900" Then
                    sSql = sSql & " And LTGM_StopDate between '" & sFromDate & "' and '" & sToDate & "'"
                End If
            ElseIf iTripStatus = 0 Then
                '  sSql = sSql & " And LTGM_TripStatus ='" & iTripStatus & "'"
                If sFromDate <> "01/01/1900" And sToDate <> "01/01/1900" Then
                    sSql = sSql & " And LTGM_StartDate ='" & sFromDate & "' or LTGM_StopDate ='" & sToDate & "'"
                End If
            Else
                ' sSql = sSql & " And LTGM_TripStatus ='" & iTripStatus & "'"
                '   If sDate <> "" Then
                'sSql = sSql & " And LTGM_StartDate ='" & sDate & "'"
                'End If
            End If
            sSql = sSql & " Order By LTGM_StartDate DESC"

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LTGM_ID").ToString()) = False Then
                    dRow("LTGM_ID") = dtDetails.Rows(i)("LTGM_ID").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TransactionNo").ToString()) = False Then
                    dRow("TripNo") = dtDetails.Rows(i)("LTGM_TransactionNo").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehicleType").ToString()) = False Then
                    sVehicleType = getVechicleType(sAC, iACID, dtDetails.Rows(i)("LTGM_VehicleType").ToString())
                    dRow("VehicleType") = sVehicleType
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCustomer").ToString()) = False Then
                    dRow("Customer") = GetCustomerName(sAC, dtDetails.Rows(i)("LTGM_DestinationCustomer").ToString())
                End If

                If ((IsDBNull(dtDetails.Rows(i)("LTGM_StartCity").ToString()) = False) And (IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCity").ToString()) = False)) Then
                    dRow("Route") = dtDetails.Rows(i)("LTGM_StartCity").ToString() & " - " & dtDetails.Rows(i)("LTGM_DestinationCity").ToString() & " " & sVehicleType
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_StartDate").ToString()) = False Then
                    dRow("StartDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StartDate").ToString(), "D")
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo").ToString()) = False Then
                    dRow("VechicleNo") = GetVechicleNo(sAC, dtDetails.Rows(i)("LTGM_VehivleNo").ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_TripStatus").ToString()) = False Then
                    If dtDetails(i)("LTGM_TripStatus").ToString() = 1 Then
                        dRow("TripStatus") = "Start"
                    Else
                        dRow("TripStatus") = "End"
                    End If

                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo")) = False Then
                    dRow("DriverName") = GetDriverName(sAC, dtDetails.Rows(i)("LTGM_Driver").ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_StartTime").ToString()) = False Then
                    dRow("StartTime") = dtDetails.Rows(i)("LTGM_StartTime").ToString()
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_StopDate").ToString()) = False Then
                    dRow("StopDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StopDate").ToString(), "D")
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_StopTime").ToString()) = False Then
                    dRow("StopTime") = dtDetails.Rows(i)("LTGM_StopTime").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_DistanceinKms").ToString()) = False Then
                    dRow("DistanceinKms") = dtDetails.Rows(i)("LTGM_DistanceinKms").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_Rate").ToString()) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("LTGM_Rate").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_PetrolQty").ToString()) = False Then
                    dRow("PetrolQty") = dtDetails.Rows(i)("LTGM_PetrolQty").ToString()
                End If

                If (dtDetails.Rows(i)("LTGM_DelFlag") = "W") Then
                    dRow("Status") = "Waiting for Approval"
                ElseIf (dtDetails.Rows(i)("LTGM_DelFlag") = "A") Then
                    dRow("Status") = "Activated"
                ElseIf (dtDetails.Rows(i)("LTGM_DelFlag") = "D") Then
                    dRow("Status") = "De-Activated"
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_AllottedTime")) = False Then
                    dRow("AllottedTime") = dtDetails.Rows(i)("LTGM_AllottedTime")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TripTakenTime")) = False Then
                    dRow("TakenTime") = dtDetails.Rows(i)("LTGM_TripTakenTime")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TimeStatus")) = False Then
                    dRow("TimeStatus") = dtDetails.Rows(i)("LTGM_TimeStatus")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MRStart").ToString()) = False Then
                    dRow("MRStart") = dtDetails.Rows(i)("LTGM_MRStart").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MREnd").ToString()) = False Then
                    dRow("MREnd") = dtDetails.Rows(i)("LTGM_MREnd").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MRStatus").ToString()) = False Then
                    dRow("MtrStatus") = dtDetails.Rows(i)("LTGM_MRStatus").ToString()
                End If
                dt.Rows.Add(dRow)
            Next
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
    Public Function GetVechicleNo(ByVal sNameSpace As String, ByVal iLVMID As Integer) As String
        Dim sSQL As String = ""
        Dim sVehicleRegNo As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LVM_RegNo  from Lgst_Vehicle_Master where LVM_ID = " & iLVMID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LVM_RegNo").ToString()) = False Then
                    sVehicleRegNo = dt.Rows(0)("LVM_RegNo").ToString()
                Else
                    sVehicleRegNo = ""
                End If
            Else
                sVehicleRegNo = ""
            End If
            Return sVehicleRegNo
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
    Public Function LoadMeterDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal sMeterStatus As String) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim sVehicleType As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LTGM_ID")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("TripNo")
            dt.Columns.Add("VehicleType")
            dt.Columns.Add("DistanceinKms")
            dt.Columns.Add("MRStart")
            dt.Columns.Add("MREnd")
            dt.Columns.Add("Route")
            dt.Columns.Add("Status")
            dt.Columns.Add("DriverName")
            dt.Columns.Add("Customer")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("StartTime")
            dt.Columns.Add("StopDate")
            dt.Columns.Add("StopTime")
            dt.Columns.Add("Rate")
            dt.Columns.Add("PetrolQty")
            dt.Columns.Add("AllottedTime")
            dt.Columns.Add("TakenTime")
            dt.Columns.Add("TimeStatus")
            dt.Columns.Add("TripStatus")
            sSql = "Select * from Lgst_TripGeneration_Master where LTGM_CompID =" & iACID & " and  LTGM_YearID=" & iyearId & ""
            If sMeterStatus = "All" Then
            Else
                sSql = sSql & " And LTGM_MrStatus ='" & sMeterStatus & "'"
            End If
            sSql = sSql & "  Order By LTGM_StartDate DESC"

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LTGM_ID").ToString()) = False Then
                    dRow("LTGM_ID") = dtDetails.Rows(i)("LTGM_ID").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TransactionNo").ToString()) = False Then
                    dRow("TripNo") = dtDetails.Rows(i)("LTGM_TransactionNo").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo").ToString()) = False Then
                    dRow("VehicleNo") = GetVechicleNo(sAC, dtDetails.Rows(i)("LTGM_VehivleNo").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehicleType").ToString()) = False Then
                    sVehicleType = getVechicleType(sAC, iACID, dtDetails.Rows(i)("LTGM_VehicleType").ToString())
                    dRow("VehicleType") = sVehicleType
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_DistanceinKms").ToString()) = False Then
                    dRow("DistanceinKms") = dtDetails.Rows(i)("LTGM_DistanceinKms").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MRStart").ToString()) = False Then
                    dRow("MRStart") = dtDetails.Rows(i)("LTGM_MRStart").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MREnd").ToString()) = False Then
                    dRow("MREnd") = dtDetails.Rows(i)("LTGM_MREnd").ToString()
                End If
                If ((IsDBNull(dtDetails.Rows(i)("LTGM_StartCity").ToString()) = False) And (IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCity").ToString()) = False)) Then
                    dRow("Route") = dtDetails.Rows(i)("LTGM_StartCity").ToString() & " - " & dtDetails.Rows(i)("LTGM_DestinationCity").ToString() & " " & sVehicleType
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MRStatus").ToString()) = False Then
                    dRow("Status") = dtDetails.Rows(i)("LTGM_MRStatus").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo")) = False Then
                    dRow("DriverName") = GetDriverName(sAC, dtDetails.Rows(i)("LTGM_Driver").ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCustomer").ToString()) = False Then
                    dRow("Customer") = GetCustomerName(sAC, dtDetails.Rows(i)("LTGM_DestinationCustomer").ToString())
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_StartDate").ToString()) = False Then
                    dRow("StartDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StartDate").ToString(), "D")
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_StartTime").ToString()) = False Then
                    dRow("StartTime") = dtDetails.Rows(i)("LTGM_StartTime").ToString()
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_StopDate").ToString()) = False Then
                    dRow("StopDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StopDate").ToString(), "D")
                End If

                If IsDBNull(dtDetails.Rows(i)("LTGM_StopTime").ToString()) = False Then
                    dRow("StopTime") = dtDetails.Rows(i)("LTGM_StopTime").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_Rate").ToString()) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("LTGM_Rate").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_PetrolQty").ToString()) = False Then
                    dRow("PetrolQty") = dtDetails.Rows(i)("LTGM_PetrolQty").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_AllottedTime")) = False Then
                    dRow("AllottedTime") = dtDetails.Rows(i)("LTGM_AllottedTime")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TripTakenTime")) = False Then
                    dRow("TakenTime") = dtDetails.Rows(i)("LTGM_TripTakenTime")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TimeStatus")) = False Then
                    dRow("TimeStatus") = dtDetails.Rows(i)("LTGM_TimeStatus")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TripStatus").ToString()) = False Then
                    If dtDetails(i)("LTGM_TripStatus").ToString() = 1 Then
                        dRow("TripStatus") = "Start"
                    Else
                        dRow("TripStatus") = "End"
                    End If
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iAvailability As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim sVehicleType As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("VehicleType")
            dt.Columns.Add("TripNo")
            dt.Columns.Add("Route")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("StartTime")
            dt.Columns.Add("AllottedTime")
            dt.Columns.Add("Status")


            If iAvailability = 1 Then
                sSql = "select * from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and LTGM_VehivleNo in( "
                sSql = sSql & "Select LVM_ID from Lgst_Vehicle_Master where LVM_CompID =" & iACID & " and lvm_Delflag ='A'"
                sSql = sSql & " and lvm_id in (select LTGM_VehivleNo from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and ltgm_compid= " & iACID & ") ) "
                sSql = sSql & "  Order By LTGM_ID DESC"
                dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo").ToString()) = False Then
                        dRow("VehicleNo") = GetVehicleNo(sAC, iACID, dtDetails.Rows(i)("LTGM_VehivleNo").ToString())
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_TransactionNo").ToString()) = False Then
                        dRow("TripNo") = dtDetails.Rows(i)("LTGM_TransactionNo").ToString()
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_VehicleType").ToString()) = False Then
                        sVehicleType = getVechicleType(sAC, iACID, dtDetails.Rows(i)("LTGM_VehicleType").ToString())
                        dRow("VehicleType") = sVehicleType
                    End If
                    If ((IsDBNull(dtDetails.Rows(i)("LTGM_StartCity").ToString()) = False) And (IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCity").ToString()) = False)) Then
                        dRow("Route") = dtDetails.Rows(i)("LTGM_StartCity").ToString() & " - " & dtDetails.Rows(i)("LTGM_DestinationCity").ToString() & " " & sVehicleType
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_StartDate").ToString()) = False Then
                        dRow("StartDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StartDate").ToString(), "D")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_StartTime").ToString()) = False Then
                        dRow("StartTime") = dtDetails.Rows(i)("LTGM_StartTime").ToString()
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_AllottedTime").ToString()) = False Then
                        dRow("AllottedTime") = dtDetails.Rows(i)("LTGM_AllottedTime").ToString()
                    End If
                    dRow("Status") = "On Trip"
                    dt.Rows.Add(dRow)
                Next
            ElseIf iAvailability = 2 Then
                sSql = "Select * from Lgst_Vehicle_Master where LVM_CompID =" & iACID & " and lvm_Delflag ='A'"
                sSql = sSql & " and lvm_id not in (select LTGM_VehivleNo from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and ltgm_compid= " & iACID & ") "
                sSql = sSql & "  Order By LVM_ID DESC"
                    dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("LVM_RegNo").ToString()) = False Then
                        dRow("VehicleNo") = dtDetails.Rows(i)("LVM_RegNo").ToString()
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LVM_VehicleType").ToString()) = False Then
                        sVehicleType = getVechicleType(sAC, iACID, dtDetails.Rows(i)("LVM_VehicleType").ToString())
                        dRow("VehicleType") = sVehicleType
                    End If
                    dRow("TripNo") = ""
                    dRow("Route") = ""
                    dRow("StartDate") = ""
                    dRow("StartTime") = ""
                    dRow("AllottedTime") = ""
                    dRow("Status") = "Available"
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDriverDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iAvailability As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim sVehicleType As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DriverName")
            dt.Columns.Add("TripNo")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("Route")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("StartTime")
            dt.Columns.Add("AllottedTime")
            dt.Columns.Add("Status")
            If iAvailability = 1 Then
                sSql = "select * from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and LTGM_Driver in( "
                sSql = sSql & " Select LDM_ID from Lgst_Driver_Master where LDM_CompID =" & iACID & " and lDm_Delflag ='A'"
                sSql = sSql & " and lDm_id in (select LTGM_Driver from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and ltgm_compid= " & iACID & ") ) "
                sSql = sSql & "  Order By LTGM_ID DESC"
                dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("LTGM_Driver").ToString()) = False Then
                        dRow("DriverName") = GetDriverName(sAC, dtDetails.Rows(i)("LTGM_Driver").ToString())
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo").ToString()) = False Then
                        dRow("VehicleNo") = GetVehicleNo(sAC, iACID, dtDetails.Rows(i)("LTGM_VehivleNo").ToString())
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_TransactionNo").ToString()) = False Then
                        dRow("TripNo") = dtDetails.Rows(i)("LTGM_TransactionNo").ToString()
                    End If
                    If ((IsDBNull(dtDetails.Rows(i)("LTGM_StartCity").ToString()) = False) And (IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCity").ToString()) = False)) Then
                        dRow("Route") = dtDetails.Rows(i)("LTGM_StartCity").ToString() & " - " & dtDetails.Rows(i)("LTGM_DestinationCity").ToString() & " " & sVehicleType
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_StartDate").ToString()) = False Then
                        dRow("StartDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StartDate").ToString(), "D")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_StartTime").ToString()) = False Then
                        dRow("StartTime") = dtDetails.Rows(i)("LTGM_StartTime").ToString()
                    End If
                    If IsDBNull(dtDetails.Rows(i)("LTGM_AllottedTime").ToString()) = False Then
                        dRow("AllottedTime") = dtDetails.Rows(i)("LTGM_AllottedTime").ToString()
                    End If
                    dRow("Status") = "On Trip"
                    dt.Rows.Add(dRow)
                Next
            ElseIf iAvailability = 2 Then
                sSql = "Select * From Lgst_Driver_Master Where LDM_CompID=" & iACID & "  AND ldm_Delflag ='A'"
                sSql = sSql & " and  ldm_id not in (select LTGM_Driver from Lgst_TripGeneration_Master where LTGM_TripStatus =1 and ltgm_compid= " & iACID & ")  "
                sSql = sSql & "  Order By LDM_ID DESC"
                dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("LDM_DriverName").ToString()) = False Then
                        dRow("DriverName") = dtDetails.Rows(i)("LDM_DriverName").ToString()
                    End If
                    dRow("VehicleNo") = ""
                    dRow("TripNo") = ""
                    dRow("Route") = ""
                    dRow("StartDate") = ""
                    dRow("StartTime") = ""
                    dRow("AllottedTime") = ""
                    dRow("Status") = "Available"
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingTripGenNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LTGM_ID,LTGM_TransactionNo From Lgst_TripGeneration_Master Where LTGM_CompID=" & iCompID & " and LTGM_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDestinationCustomer(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerTripDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustId As Integer, ByVal dFromDate As String, ByVal dToDate As String, ByVal sTimeStatus As String, ByVal sMeterStatus As String, ByVal irouteId As Integer, ByVal iDriverId As Integer, ByVal iVehicleTypeId As Integer, ByVal iTripStatus As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Dim sVehicleType As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LTGM_ID")
            dt.Columns.Add("TripNo")
            dt.Columns.Add("Customer")
            dt.Columns.Add("VehicleType")
            dt.Columns.Add("Route")
            dt.Columns.Add("DistanceinKms")
            dt.Columns.Add("Rate")
            dt.Columns.Add("PetrolQty")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("DriverName")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("StartTime")
            dt.Columns.Add("StopDate")
            dt.Columns.Add("StopTime")
            dt.Columns.Add("AllottedTime")
            dt.Columns.Add("TimeTaken")
            dt.Columns.Add("TimeStatus")
            dt.Columns.Add("MRStart")
            dt.Columns.Add("MREnd")
            dt.Columns.Add("MeterStatus")
            dt.Columns.Add("TripStatus")
            sSql = "Select * from Lgst_TripGeneration_Master where LTGM_CompID =" & iACID & " and LTGM_YearID=" & iyearId & "" ' and LTGM_TripStatus =2"
            If iCustId <> 0 Then
                sSql = sSql & " And LTGM_DestinationCustomer ='" & iCustId & "'  "
            End If
            If dFromDate <> "01/01/1900" And dToDate <> "01/01/1900" Then
                sSql = sSql & " And LTGM_StopDate Between '" & dFromDate & "' and '" & dToDate & "' "
            End If
            If sTimeStatus <> "" Then
                sSql = sSql & " And LTGM_TimeStatus ='" & sTimeStatus & "'  "
            End If
            If iTripStatus <> 0 Then
                sSql = sSql & " And LTGM_TripStatus ='" & iTripStatus & "'  "
            End If
            If sMeterStatus <> "" Then
                sSql = sSql & " And LTGM_MRStatus ='" & sMeterStatus & "'  "
            End If
            If irouteId <> 0 Then
                sSql = sSql & " And LTGM_RouteID ='" & irouteId & "'  "
            End If
            If iDriverId <> 0 Then
                sSql = sSql & " And LTGM_Driver ='" & iDriverId & "'  "
            End If
            If iVehicleTypeId <> 0 Then
                sSql = sSql & " And LTGM_VehicleType ='" & iVehicleTypeId & "'  "
            End If
            sSql = sSql & " order by ltgm_id asc "
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LTGM_ID").ToString()) = False Then
                    dRow("LTGM_ID") = dtDetails.Rows(i)("LTGM_ID").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TransactionNo").ToString()) = False Then
                    dRow("TripNo") = dtDetails.Rows(i)("LTGM_TransactionNo").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_DistanceinKms").ToString()) = False Then
                    dRow("DistanceinKms") = dtDetails.Rows(i)("LTGM_DistanceinKms").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_Rate").ToString()) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("LTGM_Rate").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_PetrolQty").ToString()) = False Then
                    dRow("PetrolQty") = dtDetails.Rows(i)("LTGM_PetrolQty").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCustomer").ToString()) = False Then
                    dRow("Customer") = GetCustomerName(sAC, dtDetails.Rows(i)("LTGM_DestinationCustomer").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_Driver").ToString()) = False Then
                    dRow("DriverName") = GetDriverName(sAC, dtDetails.Rows(i)("LTGM_Driver").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehicleType").ToString()) = False Then
                    sVehicleType = getVechicleType(sAC, iACID, dtDetails.Rows(i)("LTGM_VehicleType").ToString())
                    dRow("VehicleType") = sVehicleType
                End If
                If ((IsDBNull(dtDetails.Rows(i)("LTGM_StartCity").ToString()) = False) And (IsDBNull(dtDetails.Rows(i)("LTGM_DestinationCity").ToString()) = False)) Then
                    dRow("Route") = dtDetails.Rows(i)("LTGM_StartCity").ToString() & " - " & dtDetails.Rows(i)("LTGM_DestinationCity").ToString() & " " & sVehicleType
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_VehivleNo").ToString()) = False Then
                    dRow("VehicleNo") = GetVehicleNo(sAC, iACID, dtDetails.Rows(i)("LTGM_VehivleNo").ToString())
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_StartDate").ToString()) = False Then
                    dRow("StartDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StartDate").ToString(), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_StartTime").ToString()) = False Then
                    dRow("StartTime") = dtDetails.Rows(i)("LTGM_StartTime").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_StopDate").ToString()) = False Then
                    dRow("StopDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StopDate").ToString(), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_StopTime").ToString()) = False Then
                    dRow("StopTime") = dtDetails.Rows(i)("LTGM_StopTime").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_AllottedTime").ToString()) = False Then
                    dRow("AllottedTime") = dtDetails.Rows(i)("LTGM_AllottedTime").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TripTakenTime").ToString()) = False Then
                    dRow("TimeTaken") = dtDetails.Rows(i)("LTGM_TripTakenTime").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TimeStatus").ToString()) = False Then
                    dRow("TimeStatus") = dtDetails.Rows(i)("LTGM_TimeStatus").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MRStart").ToString()) = False Then
                    dRow("MRStart") = dtDetails.Rows(i)("LTGM_MRStart").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MREnd").ToString()) = False Then
                    dRow("MREnd") = dtDetails.Rows(i)("LTGM_MREnd").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_MRStatus").ToString()) = False Then
                    dRow("MeterStatus") = dtDetails.Rows(i)("LTGM_MRStatus").ToString()
                End If
                If IsDBNull(dtDetails.Rows(i)("LTGM_TripStatus").ToString()) = False Then
                    If dtDetails(i)("LTGM_TripStatus").ToString() = 1 Then
                        dRow("TripStatus") = "Start"
                    Else
                        dRow("TripStatus") = "End"
                    End If
                End If

                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingRouteNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LRM_ID,LRM_StartDestPlace From Lgst_Route_Master Where LRM_CompID=" & iCompID & " and LRM_Delflag='A' "
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
    Public Function GetDriverName(ByVal sNameSpace As String, ByVal iDriverID As Integer) As String
        Dim sSQL As String = ""
        Dim sDriverName As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LDM_DriverName  from Lgst_Driver_Master where LDM_ID = " & iDriverID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LDM_DriverName").ToString()) = False Then
                    sDriverName = dt.Rows(0)("LDM_DriverName").ToString()
                Else
                    sDriverName = ""
                End If
            Else
                sDriverName = ""
            End If
            Return sDriverName
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
