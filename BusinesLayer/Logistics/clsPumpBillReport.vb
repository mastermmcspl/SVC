Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data

Public Class clsPumpBillReport
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
    Public Function LoadPump(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LPM_ID,LPM_PumpName From Lgst_Pump_Master Where LPM_CompID=" & iCompID & " order by LPM_ID "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvoiceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPumpId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LPB_ID,LPB_BillNo From Lgst_PumpBilling Where LPB_PumpID='" & iPumpId & "' and LPB_CompID=" & iCompID & " and LPB_YearId=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iPumpId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtPumpDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("PumpName")
            dt.Columns.Add("PumpAddress")
            dt.Columns.Add("PumpPhNo")
            dt.Columns.Add("PumpGstNo")
            dt.Columns.Add("PumpPinCode")
            sSql = "Select * From Lgst_Pump_Master Where LPM_ID='" & iPumpId & "' and LPM_CompID=" & iCompID & " order by LPM_ID asc"
            dtPumpDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtPumpDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("PumpName") = dtPumpDetails.Rows(0)("LPM_PumpName").ToString()
                dRow("PumpAddress") = dtPumpDetails.Rows(0)("LPM_Address").ToString()
                dRow("PumpPhNo") = dtPumpDetails.Rows(0)("LPM_MobNo").ToString()
                dRow("PumpGstNo") = dtPumpDetails.Rows(0)("LPM_GstNo").ToString()
                dRow("PumpPinCode") = dtPumpDetails.Rows(0)("LPM_PinCode").ToString()
                dt.Rows.Add(dRow)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadTripDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iPumpId As Integer, ByVal sFromDate As String, ByVal sTodate As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTripDetails As New DataTable, dtVehicleDetails As New DataTable
        Dim dRow As DataRow
        Try
            sFromDate = objGen.FormatDtForRDBMS(sFromDate, "Q")
            sTodate = objGen.FormatDtForRDBMS(sTodate, "Q")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Date")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("RouteName")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Amount")
            dt.Columns.Add("Qty")
            dt.Columns.Add("Advance")
            dt.Columns.Add("OtherExpense")
            dt.Columns.Add("TotalAmount")
            sSql = "" : sSql = "Select * from Lgst_TripGenDiesel_Details where LTGDD_PumpID = " & iPumpId & " and LTGDD_YearID=" & iYearId & "  and  LTGDD_CompID=" & iCompID & " and LTGDD_IndDate Between " & sFromDate & " And " & sTodate & " and LTGDD_DelFlag <> 'D' order by LTGDD_ID asc " 'LTGDD_TripID in (select LTGM_ID from Lgst_TripGeneration_Master where LTGM_StopDate Between " & sFromDate & " And " & sTodate & "  and LTGM_YearID=" & iYearId & " and LTGM_TripStatus=2 )order by LTGDD_ID asc"
            dtTripDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtTripDetails.Rows.Count > 0 Then
                For i = 0 To dtTripDetails.Rows.Count - 1
                    dtVehicleDetails = GetVehicleDetails(sNameSpace, iCompID, dtTripDetails.Rows(i)("LTGDD_TripID").ToString())
                    dRow = dt.NewRow()
                    dRow("SrNo") = i + 1
                    dRow("Date") = objGen.FormatDtForRDBMS(dtTripDetails.Rows(i)("LTGDD_IndDate").ToString(), "D")
                    dRow("VehicleNo") = GetVehicleNo(sNameSpace, iCompID, dtVehicleDetails.Rows(0)("LTGM_VehivleNo").ToString())
                    dRow("RouteName") = dtVehicleDetails.Rows(0)("LTGM_StartCity").ToString() & " To " & dtVehicleDetails.Rows(0)("LTGM_DestinationCity").ToString()
                    dRow("Rate") = dtTripDetails.Rows(i)("LTGDD_DieselRatePerltr").ToString()
                    dRow("Amount") = dtTripDetails.Rows(i)("LTGDD_DieselAmount").ToString()
                    dRow("Qty") = dtTripDetails.Rows(i)("LTGDD_DieselinLtrs").ToString()
                    dRow("Advance") = String.Format("{0:0.00}", Convert.ToDecimal(dtTripDetails.Rows(i)("LTGDD_DriverAdvancGvnByPump").ToString()))
                    dRow("OtherExpense") = String.Format("{0:0.00}", Convert.ToDecimal(dtTripDetails.Rows(i)("LTGDD_OtherExpenses").ToString()))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((Val(dtTripDetails.Rows(i)("LTGDD_DieselAmount").ToString()) + Val(dtTripDetails.Rows(i)("LTGDD_DriverAdvancGvnByPump").ToString())) + Val(dtTripDetails.Rows(i)("LTGDD_OtherExpenses").ToString())))
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVehicleDetails(ByVal sNameSpace As String, ByVal iCompId As Integer, ByVal iTripId As Integer) As DataTable
        Dim sSql As String = ""
        Dim sTripVehicle As New DataTable
        Try
            sSql = "" : sSql = "Select * from Lgst_TripGeneration_Master where LTGM_ID = '" & iTripId & "' and LTGM_CompID=" & iCompId & ""
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
    Public Function GetVehicleType(ByVal sNameSpace As String, ByVal iCompId As Integer, ByVal iVehID As Integer) As String
        Dim sSql As String = ""
        Dim sVehType As String = ""
        Try
            sSql = "" : sSql = "Select Mas_desc from ACC_General_Master where Mas_id='" & iVehID & "' and Mas_CompID =" & iCompId & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Vehicle Type' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            sVehType = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sVehType
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadPumpBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iPumpId As Integer, ByVal iBillId As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dtPumpBillDetails As New DataTable
    '    Dim dRow As DataRow
    '    Try
    '        dt.Columns.Add("SrNo")
    '        dt.Columns.Add("Date")
    '        dt.Columns.Add("VehicleNo")
    '        dt.Columns.Add("RefBillNo")
    '        dt.Columns.Add("Particulars")
    '        dt.Columns.Add("Qty")
    '        dt.Columns.Add("Rate")
    '        dt.Columns.Add("Others")
    '        dt.Columns.Add("Amount")
    '        sSql = "Select * From Lgst_PumpBilling Where LPB_BillNo='" & iBillId & "' and LPB_CompID=" & iCompID & " and LPB_YearId=" & iYearId & " "
    '        dtPumpBillDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dtPumpBillDetails.Rows.Count > 0 Then
    '            dRow = dt.NewRow()
    '            dRow("SrNo") = dRow("SrNo") + 1
    '            dRow("Date") = objGen.FormatDtForRDBMS(dtPumpBillDetails.Rows(0)("LPM_Address").ToString(), "D")
    '            dRow("VehicleNo") = dtPumpBillDetails.Rows(0)("LPM_MobNo").ToString()
    '            dRow("RefBillNo") = dtPumpBillDetails.Rows(0)("LPM_GstNo").ToString()
    '            dRow("Particulars") = dtPumpBillDetails.Rows(0)("LPM_PinCode").ToString()
    '            dRow("Qty") = dtPumpBillDetails.Rows(0)("LPM_MobNo").ToString()
    '            dRow("Rate") = dtPumpBillDetails.Rows(0)("LPM_GstNo").ToString()
    '            dRow("Others") = dtPumpBillDetails.Rows(0)("LPM_PinCode").ToString()
    '            dRow("Amount") = String.Format("{0:0.0}", Convert.ToDecimal(dtPumpBillDetails.Rows(0)("LPM_PinCode").ToString()))
    '            dt.Rows.Add(dRow)
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadPumpBillDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPumpId As Integer, ByVal sBillNo As String, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtPumpBillDetails As New DataTable
        Dim dRow As DataRow
        Try

            dt.Columns.Add("BillNo")
            dt.Columns.Add("BillDate")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("TotalAmt")
            dt.Columns.Add("sFromDate")
            dt.Columns.Add("sToDate")
            sSql = "" : sSql = "Select * from Lgst_PumpBilling where LPB_PumpID = '" & iPumpId & "' and LPB_BillNo='" & sBillNo & "' and LPB_Delflag <> 'D' and LPB_CompID=" & iCompID & "  and LPB_YearID=" & iYearId & ""
            dtPumpBillDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtPumpBillDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("BillNo") = dtPumpBillDetails.Rows(0)("LPB_BillNo").ToString()
                dRow("BillDate") = objGen.FormatDtForRDBMS(dtPumpBillDetails.Rows(0)("LPB_BillDate").ToString(), "D")
                dRow("FromDate") = objGen.FormatDtForRDBMS(dtPumpBillDetails.Rows(0)("LPB_FromDate").ToString(), "D")
                dRow("ToDate") = objGen.FormatDtForRDBMS(dtPumpBillDetails.Rows(0)("LPB_ToDate").ToString(), "D")
                dRow("TotalAmt") = String.Format("{0:0.0}", Convert.ToDecimal(dtPumpBillDetails.Rows(0)("LPB_TotalAmt").ToString()))
                dRow("sFromDate") = dtPumpBillDetails.Rows(0)("LPB_FromDate")
                dRow("sToDate") = dtPumpBillDetails.Rows(0)("LPB_ToDate")
                dt.Rows.Add(dRow)
            End If
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
