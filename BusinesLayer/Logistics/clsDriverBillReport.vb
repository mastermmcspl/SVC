Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsDriverBillReport
    Dim objGen As New clsFASGeneral
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function GetAccessCode(ByVal sAccessName As String) As DataTable
        Dim sSql As String
        Dim sAccessCode As DataTable
        Try
            sSql = "Select SAD_CMS_AccessCode,Sad_CMS_Name from Sad_CompanyMaster_Settings"
            sAccessCode = objDBL.SQLExecuteDataSet(sAccessName, sSql).Tables(0)
            Return sAccessCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDriver(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select LDM_ID, (LDM_DriverName + '-' + LDM_LicenseNo) as LDM_DriverName From Lgst_Driver_Master Where LDM_CompID=" & iCompID & " order by LDM_ID "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBillNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPumpId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LDB_ID,LDB_BillNo From Lgst_DriverBilling Where LDB_DriverID='" & iPumpId & "' and LDB_CompID=" & iCompID & " and LDB_YearId=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDriverDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iDriverId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtDriverDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("DriverName")
            dt.Columns.Add("LicenseNo")
            dt.Columns.Add("ContactNo")
            sSql = "Select * From Lgst_Driver_Master Where LDM_ID='" & iDriverId & "' and LDM_CompID=" & iCompID & ""
            dtDriverDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtDriverDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("DriverName") = dtDriverDetails.Rows(0)("LDM_DriverName").ToString()
                dRow("LicenseNo") = dtDriverDetails.Rows(0)("LDM_LicenseNo").ToString()
                dRow("ContactNo") = dtDriverDetails.Rows(0)("LDM_ContactNo").ToString()
                dt.Rows.Add(dRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDriverBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDriverId As Integer, ByVal sBillNo As String, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtDriverBillDetails As New DataTable
        Dim dRow As DataRow
        Try

            dt.Columns.Add("BillNo")
            dt.Columns.Add("BillDate")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("TotalAmt")
            dt.Columns.Add("AdvancAmt")
            dt.Columns.Add("DueAmt")
            dt.Columns.Add("sFromDate")
            dt.Columns.Add("sToDate")
            sSql = "" : sSql = "Select * from Lgst_DriverBilling where LDB_DriverID = '" & iDriverId & "' and LDB_BillNo='" & sBillNo & "' and LDB_Delflag <> 'D' and LDB_CompID=" & iCompID & " and LDB_YearID=" & iYearID & ""
            dtDriverBillDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtDriverBillDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("BillNo") = dtDriverBillDetails.Rows(0)("LDB_BillNo").ToString()
                dRow("BillDate") = objGen.FormatDtForRDBMS(dtDriverBillDetails.Rows(0)("LDB_BillDate").ToString(), "D")
                dRow("FromDate") = objGen.FormatDtForRDBMS(dtDriverBillDetails.Rows(0)("LDB_FromDate").ToString(), "D")
                dRow("ToDate") = objGen.FormatDtForRDBMS(dtDriverBillDetails.Rows(0)("LDB_ToDate").ToString(), "D")
                dRow("TotalAmt") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LDB_TotalAmt").ToString()))
                dRow("AdvancAmt") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LDB_AdvanceGvnAmt").ToString()))
                dRow("DueAmt") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LDB_PendingAmt").ToString()))
                dRow("sFromDate") = dtDriverBillDetails.Rows(0)("LDB_FromDate")
                dRow("sToDate") = dtDriverBillDetails.Rows(0)("LDB_ToDate")
                dt.Rows.Add(dRow)
            End If
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadTripDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iDriverpId As Integer, ByVal sFromDate As String, ByVal sTodate As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTripGenDetails As New DataTable, dtTripDetails As New DataTable
        Dim dRow As DataRow
        Try
            sFromDate = objGen.FormatDtForRDBMS(sFromDate, "Q")
            sTodate = objGen.FormatDtForRDBMS(sTodate, "Q")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Date")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("RouteName")
            dt.Columns.Add("AllottedAmt")
            dt.Columns.Add("AdvanceGiven")
            dt.Columns.Add("Balance")
            sSql = "" : sSql = "Select LTGDD_TripID,sum(LTGDD_DriverAdvancGvnByPump) as advance from Lgst_TripGenDiesel_Details where  LTGDD_CompID=" & iCompID & " and LTGDD_YearID=" & iYearId & " and LTGDD_TripID in (select LTGM_ID from Lgst_TripGeneration_Master  where LTGM_Driver = '" & iDriverpId & "' and  LTGM_StopDate Between " & sFromDate & " And " & sTodate & " and LTGM_YearID=" & iYearId & " and LTGM_TripStatus=2 )group by ltgdd_tripid"
            dtTripGenDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtTripGenDetails.Rows.Count > 0 Then
                For i = 0 To dtTripGenDetails.Rows.Count - 1
                    dtTripDetails = GetTripDetails(sNameSpace, iCompID, dtTripGenDetails.Rows(i)("LTGDD_TripID").ToString(), iDriverpId, iYearId)
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    dRow("Date") = objGen.FormatDtForRDBMS(dtTripDetails.Rows(0)("LTGM_StopDate").ToString(), "D")
                    dRow("VehicleNo") = GetVehicleNo(sNameSpace, iCompID, dtTripDetails.Rows(0)("LTGM_VehivleNo").ToString())
                    dRow("RouteName") = dtTripDetails.Rows(0)("LTGM_StartCity").ToString() & " To " & dtTripDetails.Rows(0)("LTGM_DestinationCity").ToString()
                    dRow("AllottedAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtTripDetails.Rows(0)("LTGM_DriverAmount").ToString()))
                    dRow("AdvanceGiven") = String.Format("{0:0.00}", Convert.ToDecimal(dtTripGenDetails.Rows(i)("advance").ToString()))
                    dRow("Balance") = String.Format("{0:0.00}", Convert.ToDecimal(Val(dtTripDetails.Rows(0)("LTGM_DriverAmount").ToString()) - Val(dtTripGenDetails.Rows(i)("advance").ToString())))
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTripDetails(ByVal sNameSpace As String, ByVal iCompId As Integer, ByVal iTripId As Integer, ByVal iDriverId As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim sTripVehicle As New DataTable
        Try
            sSql = "" : sSql = "Select * from Lgst_TripGeneration_Master where LTGM_Driver = '" & iDriverId & "' and  LTGM_ID = '" & iTripId & "' and LTGM_CompID=" & iCompId & " and  LTGM_YearID=" & iYearId & ""
            sTripVehicle = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return sTripVehicle
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
End Class
