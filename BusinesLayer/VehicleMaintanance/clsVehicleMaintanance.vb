Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsVehicleMaintanance
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions


    Public Function LoadTyreCompliance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVTM_ID")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("TyreSLNo")
            dt.Columns.Add("MtrReading")
            dt.Columns.Add("TyreFreq")
            dt.Columns.Add("Status")


            sSql = "select *,LVAM_TotalMeterValue,LVAM_RegNo from Lgst_Vehicle_TyreMaster join Lgst_Vehicle_AdditionalMaster on LVAM_ID=LVTM_MasterID"
            sSql = sSql & " where   LVAM_TotalMeterValue > LVTM_TyreFreq and  LVTM_CompID=" & iCompID & ""
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVTM_ID")) = False Then
                    dRow("LVTM_ID") = dtDetails.Rows(i)("LVTM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_RegNo")) = False Then
                    dRow("VehicleNo") = dtDetails.Rows(i)("LVAM_RegNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVTM_TyreSLNo")) = False Then
                    dRow("TyreSLNo") = dtDetails.Rows(i)("LVTM_TyreSLNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_TotalMeterValue")) = False Then
                    dRow("MtrReading") = dtDetails.Rows(i)("LVAM_TotalMeterValue")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVTM_TyreFreq")) = False Then
                    dRow("TyreFreq") = dtDetails.Rows(i)("LVTM_TyreFreq")
                End If
                dRow("Status") = "Frequency Exhausted"
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCompliance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable, dtCompInKm As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVCM_ID")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("ComplianceType")
            dt.Columns.Add("MtrReading")
            dt.Columns.Add("Frequency")
            dt.Columns.Add("Status")

            sSql = "select * from Lgst_Vehicle_ComplianceMaster where LVCM_CompID=" & iCompID & ""
            dtCompInKm = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtCompInKm.Rows.Count > 0 Then
                For i = 0 To dtCompInKm.Rows.Count - 1
                    If dtCompInKm.Rows(i)("LVCM_ComplianceFreqInKM") = 0 Then
                        sSql = "select *,LVAM_RegNo,LVAM_VehiclePurchaseDate from Lgst_Vehicle_ComplianceMaster join Lgst_Vehicle_AdditionalMaster on LVAM_MasterID=" & dtCompInKm.Rows(i)("LVCM_MasterId") & ""
                        sSql = sSql & "where LVCM_Id= " & dtCompInKm.Rows(i)("LVCM_Id") & " and getdate() >= ( SELECT DATEADD (year, LVCM_ComplianceFreqInYear, LVAM_VehiclePurchaseDate) AS DateAdd) And  LVCM_CompID=" & iCompID & ""
                        dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                        If dtDetails.Rows.Count > 0 Then
                            For j = 0 To dtDetails.Rows.Count - 1

                                dRow = dt.NewRow()
                                dRow("SrNo") = i + 1
                                If IsDBNull(dtDetails.Rows(j)("LVCM_ID")) = False Then
                                    dRow("LVCM_ID") = dtDetails.Rows(j)("LVCM_ID")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVAM_RegNo")) = False Then
                                    dRow("VehicleNo") = dtDetails.Rows(j)("LVAM_RegNo")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVCM_ComplianceID")) = False Then
                                    dRow("ComplianceType") = GetComplianceName(sNameSpace, dtDetails.Rows(j)("LVCM_ComplianceID"), iCompID)
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVAM_VehiclePurchaseDate")) = False Then
                                    dRow("MtrReading") = objGen.FormatDtForRDBMS(dtDetails.Rows(j)("LVAM_VehiclePurchaseDate").ToString(), "D")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVCM_ComplianceFreqInYear")) = False Then
                                    dRow("Frequency") = dtDetails.Rows(j)("LVCM_ComplianceFreqInYear")
                                End If
                                dRow("Status") = "Frequency Exhausted"
                                dt.Rows.Add(dRow)
                            Next
                        End If
                    Else
                        sSql = "select *,LVAM_TotalMeterValue,LVAM_RegNo from Lgst_Vehicle_ComplianceMaster join Lgst_Vehicle_AdditionalMaster on LVAM_MasterID=" & dtCompInKm.Rows(i)("LVCM_MasterId") & " "
                        sSql = sSql & " where LVCM_Id= " & dtCompInKm.Rows(i)("LVCM_Id") & " and  LVAM_TotalMeterValue > LVCM_ComplianceFreqInKM and  LVCM_CompID=" & iCompID & ""
                        dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                        If dtDetails.Rows.Count > 0 Then
                            For j = 0 To dtDetails.Rows.Count - 1

                                dRow = dt.NewRow()
                                dRow("SrNo") = i + 1
                                If IsDBNull(dtDetails.Rows(j)("LVCM_ID")) = False Then
                                    dRow("LVCM_ID") = dtDetails.Rows(j)("LVCM_ID")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVAM_RegNo")) = False Then
                                    dRow("VehicleNo") = dtDetails.Rows(j)("LVAM_RegNo")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVCM_ComplianceID")) = False Then
                                    dRow("ComplianceType") = GetComplianceName(sNameSpace, dtDetails.Rows(j)("LVCM_ComplianceID"), iCompID)
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVAM_TotalMeterValue")) = False Then
                                    dRow("MtrReading") = dtDetails.Rows(j)("LVAM_TotalMeterValue")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVCM_ComplianceFreqInKM")) = False Then
                                    dRow("Frequency") = dtDetails.Rows(j)("LVCM_ComplianceFreqInKM")
                                End If
                                dRow("Status") = "Frequency Exhausted"
                                dt.Rows.Add(dRow)
                            Next
                        End If
                    End If
                Next
            End If
            'For i = 0 To dtDetails.Rows.Count - 1
            'Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBatteryCompliance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVAM_ID")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("BatteryNo")
            dt.Columns.Add("PurchaseDt")
            dt.Columns.Add("Frequency")
            dt.Columns.Add("Status")


            sSql = "select * from Lgst_Vehicle_AdditionalMaster "
            sSql = sSql & "where getdate() >= ( SELECT DATEADD(year, LVAM_BatteryFrequency, LVAM_VehiclePurchaseDate) AS DateAdd) And  LVAM_CompID=" & iCompID & ""
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVAM_ID")) = False Then
                    dRow("LVAM_ID") = dtDetails.Rows(i)("LVAM_ID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_RegNo")) = False Then
                    dRow("VehicleNo") = dtDetails.Rows(i)("LVAM_RegNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_BatteryNo")) = False Then
                    dRow("BatteryNo") = dtDetails.Rows(i)("LVAM_BatteryNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_VehiclePurchaseDate")) = False Then
                    dRow("PurchaseDt") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LVAM_VehiclePurchaseDate").ToString(), "D")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVAM_BatteryFrequency")) = False Then
                    dRow("Frequency") = dtDetails.Rows(i)("LVAM_BatteryFrequency")
                End If
                dRow("Status") = "Frequency Exhausted"
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceName(ByVal sNameSpace As String, ByVal iMasID As Integer, ByVal iCompID As Integer) As String
        Dim sSQL As String = ""
        Dim sPump As String = ""
        Dim dt As New DataTable
        Try
            sSQL = "Select Mas_desc from ACC_General_Master  where Mas_id=" & iMasID & " And Mas_CompID =" & iCompID & " And Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Compliance' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Mas_desc").ToString()) = False Then
                    sPump = dt.Rows(0)("Mas_desc").ToString()
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
    Public Function LoadInsuranceDue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable, dtVehicle As New DataTable, dtVehDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVID_ID")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("PolicyNo")
            dt.Columns.Add("Amount")
            dt.Columns.Add("ExpiryDate")
            dt.Columns.Add("Status")

            sSql = "select * from Lgst_Vehicle_Master where LVM_CompID=" & iCompID & " "
            dtVehicle = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtVehicle.Rows.Count > 0 Then
                For m = 0 To dtVehicle.Rows.Count - 1
                    sSql = "select * from Lgst_Vehicle_InsuranceDetails where LVID_MasterID=" & dtVehicle.Rows(m)("LVM_ID") & " and LVID_CompID=" & iCompID & " "
                    dtVehDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                    If dtVehDetails.Rows.Count > 0 Then
                        For i = 0 To dtVehDetails.Rows.Count - 1

                            sSql = "select * from Lgst_Vehicle_InsuranceDetails"
                            sSql = sSql & " where LVID_Id= " & dtVehDetails.Rows(i)("LVID_Id") & " and getdate() >= LVID_InsToDate And  LVID_CompID=" & iCompID & ""
                            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                            If dtDetails.Rows.Count > 0 Then
                                For j = 0 To dtDetails.Rows.Count - 1

                                    dRow = dt.NewRow()
                                    dRow("SrNo") = i + 1
                                    If IsDBNull(dtDetails.Rows(j)("LVID_ID")) = False Then
                                        dRow("LVID_ID") = dtDetails.Rows(j)("LVID_MasterID")
                                    End If
                                    If IsDBNull(dtDetails.Rows(j)("LVID_RegNo")) = False Then
                                        dRow("VehicleNo") = dtDetails.Rows(j)("LVID_RegNo")
                                    End If
                                    If IsDBNull(dtDetails.Rows(j)("LVID_PolicyNo")) = False Then
                                        dRow("PolicyNo") = dtDetails.Rows(j)("LVID_PolicyNo")
                                    End If

                                    If IsDBNull(dtDetails.Rows(j)("LVID_TotalAmt")) = False Then
                                        dRow("Amount") = dtDetails.Rows(j)("LVID_TotalAmt")
                                    End If
                                    If IsDBNull(dtDetails.Rows(j)("LVID_InsToDate")) = False Then
                                        dRow("ExpiryDate") = dtDetails.Rows(j)("LVID_InsToDate")
                                    End If
                                    dRow("Status") = "Frequency Exhausted"
                                    dt.Rows.Add(dRow)
                                Next
                            End If
                        Next
                    Else
                        sSql = "select * from Lgst_Vehicle_Master where LVM_ID=" & dtVehicle.Rows(m)("LVM_Id") & " "
                        sSql = sSql & " and getdate() >= LVM_InsuranceExpDate And  LVM_CompID=" & iCompID & ""
                        dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                        If dtDetails.Rows.Count > 0 Then
                            For j = 0 To dtDetails.Rows.Count - 1

                                dRow = dt.NewRow()
                                dRow("SrNo") = m + 1
                                If IsDBNull(dtDetails.Rows(j)("LVM_ID")) = False Then
                                    dRow("LVID_ID") = dtDetails.Rows(j)("LVM_ID")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVM_RegNo")) = False Then
                                    dRow("VehicleNo") = dtDetails.Rows(j)("LVM_RegNo")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVM_InsuranceNo")) = False Then
                                    dRow("PolicyNo") = dtDetails.Rows(j)("LVM_InsuranceNo")
                                End If

                                If IsDBNull(dtDetails.Rows(j)("LVM_InsuranceAmt")) = False Then
                                    dRow("Amount") = dtDetails.Rows(j)("LVM_InsuranceAmt")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVM_InsuranceExpDate")) = False Then
                                    dRow("ExpiryDate") = dtDetails.Rows(j)("LVM_InsuranceExpDate")
                                End If
                                dRow("Status") = "Frequency Exhausted"
                                dt.Rows.Add(dRow)
                            Next
                        End If

                    End If
                Next
            End If
            'For i = 0 To dtDetails.Rows.Count - 1
            'Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLoanDue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable, dtVehicle As New DataTable, dtVehDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVLM_ID")
            dt.Columns.Add("VehicleNo")
            dt.Columns.Add("AccNo")
            dt.Columns.Add("InstallmentAmt")
            dt.Columns.Add("DueDt")
            dt.Columns.Add("Status")

            sSql = "select * from Lgst_Vehicle_LoanMaster where LVLM_CompID=" & iCompID & " "
            dtVehicle = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtVehicle.Rows.Count > 0 Then
                For m = 0 To dtVehicle.Rows.Count - 1
                    sSql = "select * from Lgst_Vehicle_LoanDetails where LVLD_MasterID=" & dtVehicle.Rows(m)("LVLM_ID") & " and LVLD_CompID=" & iCompID & " "
                    dtVehDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                    If dtVehDetails.Rows.Count > 0 Then
                        For i = 0 To dtVehDetails.Rows.Count - 1

                            sSql = "select * from Lgst_Vehicle_LoanDetails"
                            sSql = sSql & " where LVLD_Id= " & dtVehDetails.Rows(i)("LVLD_Id") & " and getdate() >= LVLD_LoanInsDueDate And  LVLD_CompID=" & iCompID & ""
                            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                            If dtDetails.Rows.Count > 0 Then
                                For j = 0 To dtDetails.Rows.Count - 1

                                    dRow = dt.NewRow()
                                    dRow("SrNo") = i + 1
                                    If IsDBNull(dtDetails.Rows(j)("LVLD_MasterID")) = False Then
                                        dRow("LVLM_ID") = dtDetails.Rows(j)("LVLD_MasterID")
                                    End If
                                    If IsDBNull(dtDetails.Rows(j)("LVLD_RegNo")) = False Then
                                        dRow("VehicleNo") = dtDetails.Rows(j)("LVLD_RegNo")
                                    End If
                                    If IsDBNull(dtDetails.Rows(j)("LVLD_MasterID")) = False Then
                                        dRow("AccNo") = dtDetails.Rows(j)("LVLD_MasterID")
                                    End If

                                    If IsDBNull(dtDetails.Rows(j)("LVLD_InstallmentPaidAmt")) = False Then
                                        dRow("InstallmentAmt") = dtDetails.Rows(j)("LVLD_InstallmentPaidAmt")
                                    End If
                                    If IsDBNull(dtDetails.Rows(j)("LVLD_LoanInsDueDate")) = False Then
                                        dRow("DueDt") = dtDetails.Rows(j)("LVLD_LoanInsDueDate")
                                    End If
                                    dRow("Status") = "Frequency Exhausted"
                                    dt.Rows.Add(dRow)
                                Next
                            End If
                        Next
                    Else
                        sSql = "select * from Lgst_Vehicle_LoanMaster where LVLM_ID=" & dtVehicle.Rows(m)("LVLM_Id") & " "
                        sSql = sSql & " and getdate() >= LVLM_LoanDueDate And  LVLM_CompID=" & iCompID & ""
                        dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
                        If dtDetails.Rows.Count > 0 Then
                            For j = 0 To dtDetails.Rows.Count - 1

                                dRow = dt.NewRow()
                                dRow("SrNo") = m + 1
                                If IsDBNull(dtDetails.Rows(j)("LVLM_ID")) = False Then
                                    dRow("LVLM_ID") = dtDetails.Rows(j)("LVLM_ID")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVLM_RegNo")) = False Then
                                    dRow("VehicleNo") = dtDetails.Rows(j)("LVLM_RegNo")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVLM_ID")) = False Then
                                    dRow("AccNo") = dtDetails.Rows(j)("LVLM_ID")
                                End If

                                If IsDBNull(dtDetails.Rows(j)("LVLM_InstallmentAmt")) = False Then
                                    dRow("InstallmentAmt") = dtDetails.Rows(j)("LVLM_InstallmentAmt")
                                End If
                                If IsDBNull(dtDetails.Rows(j)("LVLM_LoanDueDate")) = False Then
                                    dRow("DueDt") = dtDetails.Rows(j)("LVLM_LoanDueDate")
                                End If
                                dRow("Status") = "Frequency Exhausted"
                                dt.Rows.Add(dRow)
                            Next
                        End If

                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
