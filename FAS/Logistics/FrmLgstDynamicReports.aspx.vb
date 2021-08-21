Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Logistics_FrmLgstDynamicReports
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics_FrmLgstDynamicReports"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASPermission As New clsFASPermission
    Private Shared sSession As AllSession
    Dim objDR As New clsDynamicReport
    Dim objGen As New clsFASGeneral
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sModule As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sModule = objclsFASPermission.GetLoginUserModulePermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, 1)
                If sModule = "False" Then
                    ' Response.Redirect("~/Permissions/SysAdminPermissionModule.aspx", False) 'Permissions/SysAdminPermissionModule
                    Exit Sub
                End If
                loadReportType() : loadTimeStatus() : loadTripStatus()
                loadTimeAvailability() : loadMeterStatus() : BindTripNo()
                BindCustomer() : loadStatus() : BindRoute()
                bindVehicleType() : BindDriver()

                Dim sPID As String = ""
                sPID = Request.QueryString("PID")
                If sPID <> "" Then
                    Dim iInvID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    ddlReportType.SelectedIndex = 7
                    'BindDetails(iInvID)
                    ddlCustomers_SelectedIndexChanged(sender, e)
                End If
            End If

        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub loadReportType()
        Dim dt As New DataTable
        Try
            ddlReportType.Items.Insert(0, "---Select Report Type ---")
            ddlReportType.Items.Insert(1, "Trip Time Status")
            ddlReportType.Items.Insert(2, "Indent")
            ddlReportType.Items.Insert(3, "Trip Status")
            ddlReportType.Items.Insert(4, "Trip Meter Reading")
            ddlReportType.Items.Insert(5, "Vehicle Availability")
            ddlReportType.Items.Insert(6, "Driver Availability")
            ddlReportType.Items.Insert(7, "Customerwise")
            ddlReportType.Items.Insert(8, "Routewise")
            ddlReportType.Items.Insert(9, "Driverwise")
            ddlReportType.Items.Insert(10, "Vehicle Typewise")
            ddlReportType.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadReportType")
        End Try
    End Sub
    Public Sub loadTimeStatus()
        Dim dt As New DataTable
        Try
            ddlTimeStatus.Items.Insert(0, "---Select Report Type ---")
            ddlTimeStatus.Items.Insert(1, "Early")
            ddlTimeStatus.Items.Insert(2, "OnTime")
            ddlTimeStatus.Items.Insert(3, "Delay")
            ddlTimeStatus.Items.Insert(4, "All")
            ddlTimeStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadTimeStatus")
        End Try
    End Sub
    Public Sub loadTripStatus()
        Dim dt As New DataTable
        Try
            ddlTripStatus.Items.Insert(0, "---Select Report Type ---")
            ddlTripStatus.Items.Insert(1, "Start")
            ddlTripStatus.Items.Insert(2, "End")
            ddlTripStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadTripStatus")
        End Try
    End Sub
    Public Sub loadMeterStatus()
        Dim dt As New DataTable
        Try
            ddlMeter.Items.Insert(0, "---Select Report Type ---")
            ddlMeter.Items.Insert(1, "Lack")
            ddlMeter.Items.Insert(2, "Same")
            ddlMeter.Items.Insert(3, "Access")
            ddlMeter.Items.Insert(4, "All")
            ddlMeter.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadMeterStatus")
        End Try
    End Sub
    Public Sub loadTimeAvailability()
        Dim dt As New DataTable
        Try
            ddlAvailability.Items.Insert(0, "---Select Report Type ---")
            ddlAvailability.Items.Insert(1, "On Trip")
            ddlAvailability.Items.Insert(2, "Available")
            ' ddlAvailability.Items.Insert(3, "All")
            ddlAvailability.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadTimeAvailability")
        End Try
    End Sub
    Public Sub loadStatus()
        Dim dt As New DataTable
        Try
            ddlStatus.Items.Insert(0, "---Select Status ---")
            ddlStatus.Items.Insert(1, "Trip Time Status")
            ddlStatus.Items.Insert(2, "Trip Meter Reading")
            ddlStatus.Items.Insert(3, "Trip Status")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadStatus")
        End Try
    End Sub
    Protected Sub BindTripNo()
        Try
            lblError.Text = ""
            ddlTripNo.DataSource = objDR.LoadExistingTripGenNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlTripNo.DataTextField = "LTGM_TransactionNo"
            ddlTripNo.DataValueField = "LTGM_ID"
            ddlTripNo.DataBind()
            ddlTripNo.Items.Insert(0, "Select Trip No.")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindCustomer()
        Try
            ddlCustomers.DataSource = objDR.BindDestinationCustomer(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomers.DataTextField = "BM_Name"
            ddlCustomers.DataValueField = "BM_ID"
            ddlCustomers.DataBind()
            ddlCustomers.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindRoute()
        Try
            ddlRoute.DataSource = objDR.LoadExistingRouteNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlRoute.DataTextField = "LRM_StartDestPlace"
            ddlRoute.DataValueField = "LRM_Id"
            ddlRoute.DataBind()
            ddlRoute.Items.Insert(0, "Select Route Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub BindDriver()
        Try
            lblError.Text = ""
            ddlDriver.DataSource = objDR.LoadDriver(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlDriver.DataTextField = "LDM_DriverName"
            ddlDriver.DataValueField = "LDM_ID"
            ddlDriver.DataBind()
            ddlDriver.Items.Insert(0, "Select Driver")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub bindVehicleType()
        Try
            ddlVehicleType.DataSource = objDR.LoadVehicleType(sSession.AccessCode, sSession.AccessCodeID)
            ddlVehicleType.DataTextField = "Mas_Desc"
            ddlVehicleType.DataValueField = "Mas_Id"
            ddlVehicleType.DataBind()
            ddlVehicleType.Items.Insert(0, "Select Vehicle Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportType.SelectedIndexChanged
        Dim dFromdate As String = "", dToDatwe As String = ""
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlReportType.SelectedIndex > 0 Then
                'GvIndent.Visible = False : gvTimeStatus.Visible = False : GvMeterStatus.Visible = False : gvTrip.Visible = False
                'GvVehicle.Visible = False : gvDriver.Visible = False  'gvCustomerTrip.Visible = False
                txtFromDt.Text = "" : txtToDt.Text = ""
                'txtDate.Text = ""
                ddlTimeStatus.SelectedIndex = 0 : ddlTripStatus.SelectedIndex = 0
                ddlMeter.SelectedIndex = 0 : ddlCustomers.SelectedIndex = 0 : ddlRoute.SelectedIndex = 0
                ddlDriver.SelectedIndex = 0 : ddlVehicleType.SelectedIndex = 0 : ddlAvailability.SelectedIndex = 0
                If ddlReportType.SelectedIndex = 1 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = False
                    pnlTimeStatus.Visible = True
                    ' txtDate.Visible = False : lblDate.Visible = False
                    pnlMeterStatus.Visible = False : pnlAvailabity.Visible = False
                    pnlTripNo.Visible = False : pnlTripStatus.Visible = False : pnlStatus.Visible = False
                    pnlRoute.Visible = False : pnlCustomers.Visible = False
                    pnlFromToDt.Visible = False
                ElseIf ddlReportType.SelectedIndex = 2 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = False
                    '   txtDate.Visible = True : lblDate.Visible = True
                    pnlFromToDt.Visible = True
                    pnlTimeStatus.Visible = False : pnlMeterStatus.Visible = False
                    If txtFromDt.Text = "" And txtToDt.Text = "" Then
                        dFromdate = "01/01/1900"
                        dToDatwe = "01/01/1900"
                        BindIndent(dFromdate, dToDatwe)
                    End If
                    pnlAvailabity.Visible = False : pnlTripNo.Visible = True
                    pnlTripStatus.Visible = False : pnlStatus.Visible = False
                    pnlRoute.Visible = False : pnlCustomers.Visible = False
                ElseIf ddlReportType.SelectedIndex = 3 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = False
                    pnlTripStatus.Visible = True ': loadTripStatus()
                    ' txtDate.Visible = False : lblDate.Visible = False
                    pnlTimeStatus.Visible = False : pnlTripNo.Visible = False
                    pnlMeterStatus.Visible = False : pnlAvailabity.Visible = False
                    pnlStatus.Visible = False
                    pnlRoute.Visible = False : pnlCustomers.Visible = False
                    pnlFromToDt.Visible = False
                ElseIf ddlReportType.SelectedIndex = 4 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = False
                    pnlMeterStatus.Visible = True
                    pnlTripStatus.Visible = False : pnlTimeStatus.Visible = False
                    ' txtDate.Visible = False : lblDate.Visible = False
                    '   loadMeterStatus()
                    pnlAvailabity.Visible = False : pnlTripNo.Visible = False
                    pnlStatus.Visible = False
                    pnlRoute.Visible = False : pnlCustomers.Visible = False
                    pnlFromToDt.Visible = False
                ElseIf ddlReportType.SelectedIndex = 5 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = False
                    pnlAvailabity.Visible = True
                    pnlMeterStatus.Visible = False
                    pnlTripStatus.Visible = False : pnlTimeStatus.Visible = False
                    '  txtDate.Visible = False : lblDate.Visible = False
                    '  loadTimeAvailability() :
                    pnlTripNo.Visible = False : pnlStatus.Visible = False
                    pnlRoute.Visible = False : pnlCustomers.Visible = False
                    pnlFromToDt.Visible = False
                ElseIf ddlReportType.SelectedIndex = 6 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = False
                    pnlAvailabity.Visible = True
                    pnlMeterStatus.Visible = False
                    pnlTripStatus.Visible = False : pnlTimeStatus.Visible = False
                    '  txtDate.Visible = False : lblDate.Visible = False :
                    pnlStatus.Visible = False
                    'loadTimeAvailability()
                    pnlTripNo.Visible = False
                    pnlRoute.Visible = False : pnlCustomers.Visible = False
                    pnlFromToDt.Visible = False
                ElseIf ddlReportType.SelectedIndex = 7 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = False
                    pnlCustomers.Visible = True
                    pnlAvailabity.Visible = False
                    pnlMeterStatus.Visible = False
                    pnlTripStatus.Visible = False : pnlTimeStatus.Visible = False
                    '  txtDate.Visible = False : lblDate.Visible = False
                    'loadTimeAvailability()
                    pnlTripNo.Visible = False : pnlStatus.Visible = False
                    pnlRoute.Visible = False : pnlFromToDt.Visible = False
                ElseIf ddlReportType.SelectedIndex = 8 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = False
                    pnlAvailabity.Visible = False
                    pnlMeterStatus.Visible = False
                    pnlTripStatus.Visible = False : pnlTimeStatus.Visible = False
                    '  txtDate.Visible = False : lblDate.Visible = False
                    'loadTimeAvailability()
                    pnlTripNo.Visible = False : pnlStatus.Visible = False
                    pnlRoute.Visible = True : pnlCustomers.Visible = False
                    pnlFromToDt.Visible = False
                ElseIf ddlReportType.SelectedIndex = 9 Then
                    pnlDriver.Visible = True : pnlVehicleType.Visible = False
                    pnlCustomers.Visible = False
                    pnlAvailabity.Visible = False
                    pnlMeterStatus.Visible = False
                    pnlTripStatus.Visible = False : pnlTimeStatus.Visible = False
                    '  txtDate.Visible = False : lblDate.Visible = False
                    'loadTimeAvailability()
                    pnlTripNo.Visible = False : pnlStatus.Visible = False
                    pnlRoute.Visible = False : pnlFromToDt.Visible = False
                ElseIf ddlReportType.SelectedIndex = 10 Then
                    pnlDriver.Visible = False : pnlVehicleType.Visible = True
                    pnlAvailabity.Visible = False
                    pnlMeterStatus.Visible = False
                    pnlTripStatus.Visible = False : pnlTimeStatus.Visible = False
                    '  txtDate.Visible = False : lblDate.Visible = False
                    'loadTimeAvailability()
                    pnlTripNo.Visible = False : pnlStatus.Visible = False
                    pnlRoute.Visible = False : pnlCustomers.Visible = False
                    pnlFromToDt.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReportType_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlTimeStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTimeStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlReportType.SelectedIndex > 0 Then
                If ddlReportType.SelectedIndex = 1 Then
                    If ddlTimeStatus.SelectedIndex > 0 Then
                        If ddlTimeStatus.SelectedIndex = 1 Then
                            dt = objDR.LoadTimeStatusDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "Early")
                            If dt.Rows.Count > 0 Then
                                ReportViewer1.Reset()
                                Dim rds As New ReportDataSource("DataSet1", dt)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicTripTime.rdlc")
                                Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Trip Time Report")}
                                ReportViewer1.LocalReport.SetParameters(ReportType)
                                ReportViewer1.LocalReport.Refresh()
                            Else
                                ReportViewer1.Reset()
                                ReportViewer1.LocalReport.Refresh()
                                lblError.Text = "No data to display"
                            End If
                            'gvTimeStatus.DataSource = dt
                            'gvTimeStatus.DataBind() : gvTimeStatus.Visible = True
                        ElseIf ddlTimeStatus.SelectedIndex = 2 Then
                            ReportViewer1.Reset()
                            dt = objDR.LoadTimeStatusDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "OnTime")
                            If dt.Rows.Count > 0 Then
                                Dim rds As New ReportDataSource("DataSet1", dt)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicTripTime.rdlc")
                                Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Trip Time  Report")}
                                ReportViewer1.LocalReport.SetParameters(ReportType)
                                ReportViewer1.LocalReport.Refresh()
                            Else
                                ReportViewer1.Reset()
                                ReportViewer1.LocalReport.Refresh()
                                lblError.Text = "No data to display"
                            End If
                            'gvTimeStatus.DataSource = dt
                            'gvTimeStatus.DataBind() : gvTimeStatus.Visible = True
                        ElseIf ddlTimeStatus.SelectedIndex = 3 Then
                            ReportViewer1.Reset()
                            dt = objDR.LoadTimeStatusDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "Delay")
                            If dt.Rows.Count > 0 Then
                                Dim rds As New ReportDataSource("DataSet1", dt)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicTripTime.rdlc")
                                Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Trip Time Report")}
                                ReportViewer1.LocalReport.SetParameters(ReportType)
                                ReportViewer1.LocalReport.Refresh()
                            Else
                                ReportViewer1.Reset()
                                ReportViewer1.LocalReport.Refresh()
                                lblError.Text = "No data to display"
                            End If
                            'gvTimeStatus.DataSource = dt
                            'gvTimeStatus.DataBind() : gvTimeStatus.Visible = True
                        ElseIf ddlTimeStatus.SelectedIndex = 4 Then
                            ReportViewer1.Reset()
                            dt = objDR.LoadTimeStatusDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "All")
                            If dt.Rows.Count > 0 Then
                                Dim rds As New ReportDataSource("DataSet1", dt)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicTripTime.rdlc")
                                Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Trip Time Report")}
                                ReportViewer1.LocalReport.SetParameters(ReportType)
                                ReportViewer1.LocalReport.Refresh()
                                'gvTimeStatus.DataSource = dt
                                'gvTimeStatus.DataBind() : gvTimeStatus.Visible = True
                            Else
                                ReportViewer1.Reset()
                                ReportViewer1.LocalReport.Refresh()
                                lblError.Text = "No data to display"
                            End If
                        End If
                    End If
                Else
                    ddlCustomers_SelectedIndexChanged(sender, e)
                End If
            Else
                lblError.Text = "Select Report type"
            End If
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub BindIndent(ByVal dFromText As String, ByVal dtotext As String)
        Dim dt As New DataTable
        Dim iTrId As Integer = 0
        Try
            lblError.Text = ""
            If ddlReportType.SelectedIndex > 0 Then
                lblError.Text = ""
                dFromText = clsGeneralFunctions.FormatMyDate(dFromText)
                dtotext = clsGeneralFunctions.FormatMyDate(dtotext)
                If ddlTripNo.SelectedIndex > 0 Then
                    iTrId = ddlTripNo.SelectedValue
                Else
                End If
                ReportViewer1.Reset()
                ReportViewer1.LocalReport.Refresh()
                dt = objDR.LoadIndentDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dFromText, dtotext, iTrId)
                If dt.Rows.Count > 0 Then
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicIndent.rdlc")
                    Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Indent Report")}
                    ReportViewer1.LocalReport.SetParameters(ReportType)
                    ReportViewer1.LocalReport.Refresh()
                Else
                    ReportViewer1.Reset()
                    ReportViewer1.LocalReport.Refresh()
                    lblError.Text = "No data to display"
                End If
                'GvIndent.DataSource = dt
                'GvIndent.DataBind() : GvIndent.Visible = True
            End If
                If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindIndent")
        End Try
    End Sub
    'Private Sub txtDate_TextChanged(sender As Object, e As EventArgs) Handles txtDate.TextChanged
    '    Try
    '        lblError.Text = ""
    '        If txtDate.Text <> "" Then
    '            If ddlReportType.SelectedIndex = 2 Then
    '                BindIndent(txtDate.Text)
    '            ElseIf ddlReportType.SelectedIndex = 3 Then
    '                ddlTripStatus_SelectedIndexChanged(sender, e)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtDate_TextChanged")
    '    End Try
    'End Sub

    Private Sub ddlTripStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTripStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dFromDt As String = "", dTodt As String = ""
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlTripStatus.SelectedIndex > 0 Then
                If ddlReportType.SelectedIndex = 3 Then
                    If ddlTripStatus.SelectedIndex = 1 Or ddlTripStatus.SelectedIndex = 2 Then
                        '   txtDate.Visible = True : lblDate.Visible = True
                        pnlFromToDt.Visible = True
                    Else
                        '    txtDate.Visible = False : lblDate.Visible = False
                        pnlFromToDt.Visible = False
                    End If
                    If txtFromDt.Text = "" Then
                        dFromDt = "01/01/1900"
                        dFromDt = clsGeneralFunctions.FormatMyDate(dFromDt)
                        dTodt = "01/01/1900"
                        dTodt = clsGeneralFunctions.FormatMyDate(dTodt)
                    Else
                        dFromDt = clsGeneralFunctions.FormatMyDate(txtFromDt.Text)
                        dTodt = clsGeneralFunctions.FormatMyDate(txtToDt.Text)
                    End If
                    dt = objDR.LoadTripDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlTripStatus.SelectedIndex, dFromDt, dTodt)
                    If dt.Rows.Count > 0 Then
                        Dim rds As New ReportDataSource("DataSet1", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicTripStatus.rdlc")
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Indent Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                        ReportViewer1.LocalReport.Refresh()
                    Else
                        ReportViewer1.Reset()
                        ReportViewer1.LocalReport.Refresh()
                        lblError.Text = "No data to display"
                    End If
                    'gvTrip.DataSource = dt
                    'gvTrip.DataBind() : gvTrip.Visible = True
                    If ddlTripStatus.SelectedIndex = 3 Then
                        pnlFromToDt.Visible = False
                    End If
                Else
                    ddlCustomers_SelectedIndexChanged(sender, e)
                End If
            Else
                lblError.Text = "Select report type"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTripStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlMeter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMeter.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlMeter.SelectedIndex > 0 Then
                If ddlReportType.SelectedIndex = 4 Then
                    dt = objDR.LoadMeterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlMeter.SelectedItem.Text)
                    If dt.Rows.Count > 0 Then
                        Dim rds As New ReportDataSource("DataSet1", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicMeterStatus.rdlc")
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Meter Status Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                        ReportViewer1.LocalReport.Refresh()
                        'GvMeterStatus.DataSource = dt
                        'GvMeterStatus.DataBind() : GvMeterStatus.Visible = True
                    Else
                        ReportViewer1.Reset()
                        ReportViewer1.LocalReport.Refresh()
                        lblError.Text = "No data to display"
                    End If
                Else
                        ddlCustomers_SelectedIndexChanged(sender, e)
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMeter_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlAvailability_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAvailability.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlAvailability.SelectedIndex > 0 Then
                If ddlReportType.SelectedIndex = 5 Then
                    dt = objDR.LoadVehicleDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlAvailability.SelectedIndex)
                    If dt.Rows.Count > 0 Then
                        Dim rds As New ReportDataSource("DataSet1", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicVehicle.rdlc")
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Vehicle Status Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                        ReportViewer1.LocalReport.Refresh()
                    Else
                        ReportViewer1.Reset()
                        ReportViewer1.LocalReport.Refresh()
                        lblError.Text = "No data to display"
                    End If
                    'GvVehicle.DataSource = dt
                    'GvVehicle.DataBind() : GvVehicle.Visible = True
                ElseIf ddlReportType.SelectedIndex = 6 Then
                        dt = objDR.LoadDriverDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlAvailability.SelectedIndex)
                    If dt.Rows.Count > 0 Then
                        Dim rds As New ReportDataSource("DataSet1", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicDriver.rdlc")
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Driver Status Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                        ReportViewer1.LocalReport.Refresh()
                        'gvDriver.DataSource = dt
                        'gvDriver.DataBind() : gvDriver.Visible = True
                    Else
                        ReportViewer1.Reset()
                        ReportViewer1.LocalReport.Refresh()
                        lblError.Text = "No data to display"
                    End If
                End If
                End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAvailability_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlTripNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTripNo.SelectedIndexChanged
        Dim dFromDt As String = "", dToDate As String = ""
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlReportType.SelectedIndex > 0 Then
                If ddlTripNo.SelectedIndex > 0 Then
                    If txtFromDt.Text = "" And txtToDt.Text = "" Then
                        dFromDt = "01/01/1900"
                        dFromDt = clsGeneralFunctions.FormatMyDate(dFromDt)
                        dToDate = "01/01/1900"
                        dToDate = clsGeneralFunctions.FormatMyDate(dToDate)
                        BindIndent(dFromDt, dToDate)
                    Else
                        dFromDt = txtFromDt.Text
                        dToDate = txtToDt.Text
                        BindIndent(dFromDt, dToDate)
                    End If
                Else
                    If txtFromDt.Text <> "" And txtToDt.Text <> "" Then
                        dFromDt = txtFromDt.Text
                        dToDate = txtToDt.Text
                        BindIndent(dFromDt, dToDate)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTripNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomers.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dFromText As String = "" : Dim dToText As String = ""
        Dim sTimeStatus As String = "", sTripStatus As Integer = 0, sMeterStatus As String = "", iroutIde As Integer = 0, iCustId As Integer = 0
        Dim idriverId As Integer = 0, ivehicleTypeId As Integer = 0
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlReportType.SelectedIndex > 0 Then
                pnlFromToDt.Visible = True : pnlStatus.Visible = True : pnlRoute.Visible = True : pnlCustomers.Visible = True
                pnlDriver.Visible = True : pnlVehicleType.Visible = True
                If txtFromDt.Text = "" Or txtToDt.Text = "" Then
                    dFromText = "01/01/1900"
                    dToText = "01/01/1900"
                    dFromText = clsGeneralFunctions.FormatMyDate(dFromText)
                    dToText = clsGeneralFunctions.FormatMyDate(dToText)
                Else
                    dFromText = clsGeneralFunctions.FormatMyDate(txtFromDt.Text)
                    dToText = clsGeneralFunctions.FormatMyDate(txtToDt.Text)
                End If
                If ddlTimeStatus.SelectedIndex > 0 Then
                    sTimeStatus = ddlTimeStatus.SelectedItem.Text
                End If
                If ddlTripStatus.SelectedIndex > 0 Then
                    sTripStatus = ddlTripStatus.SelectedIndex
                End If
                'If ddlTimeStatus.SelectedIndex > 0 Then
                '    sTripStatus = ddlTripStatus.SelectedItem.Text
                'End If
                If ddlMeter.SelectedIndex > 0 Then
                    sMeterStatus = ddlMeter.SelectedItem.Text
                End If
                If ddlCustomers.SelectedIndex > 0 Then
                    iCustId = ddlCustomers.SelectedValue
                End If
                If ddlRoute.SelectedIndex > 0 Then
                    iroutIde = ddlRoute.SelectedValue
                End If
                If ddlDriver.SelectedIndex > 0 Then
                    idriverId = ddlDriver.SelectedValue
                End If
                If ddlVehicleType.SelectedIndex > 0 Then
                    ivehicleTypeId = ddlVehicleType.SelectedValue
                End If
                ReportViewer1.Reset()
                dt = objDR.LoadCustomerTripDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCustId, dFromText, dToText, sTimeStatus, sMeterStatus, iroutIde, idriverId, ivehicleTypeId, sTripStatus)
                If dt.Rows.Count > 0 Then
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicCustomerRpt.rdlc")
                    If ddlReportType.SelectedIndex = 7 Then
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Customer Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                    ElseIf ddlReportType.SelectedIndex = 8 Then
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Route Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                    ElseIf ddlReportType.SelectedIndex = 9 Then
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Driver Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                    ElseIf ddlReportType.SelectedIndex = 10 Then
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Vehicle Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                    Else
                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                    End If

                    ReportViewer1.LocalReport.Refresh()
                    'gvCustomerTrip.DataSource = dt
                    'gvCustomerTrip.DataBind() : gvCustomerTrip.Visible = True
                Else
                    ReportViewer1.Reset()
                    ReportViewer1.LocalReport.Refresh()
                    lblError.Text = "No data to display"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomers_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub gvCustomerTrip_PreRender(sender As Object, e As EventArgs) Handles gvCustomerTrip.PreRender
    '    Try
    '        If gvCustomerTrip.Rows.Count > 0 Then
    '            gvCustomerTrip.UseAccessibleHeader = True
    '            gvCustomerTrip.HeaderRow.TableSection = TableRowSection.TableHeader
    '            gvCustomerTrip.FooterRow.TableSection = TableRowSection.TableFooter
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCustomerTrip_PreRender")
    '    End Try
    'End Sub

    Private Sub txtToDt_TextChanged(sender As Object, e As EventArgs) Handles txtToDt.TextChanged
        Try
            lblError.Text = ""
            If txtFromDt.Text = "" Then
                lblError.Text = "Enter From Date"
                txtToDt.Text = "" : txtFromDt.Focus()
                Exit Sub
            Else
                If txtToDt.Text <> "" Then
                    If ddlReportType.SelectedIndex = 7 Then
                        ddlCustomers_SelectedIndexChanged(sender, e)
                    ElseIf ddlReportType.SelectedIndex = 8 Then
                        ddlRoute_SelectedIndexChanged(sender, e)
                    ElseIf ddlReportType.SelectedIndex = 2 Then
                        BindIndent(txtFromDt.Text, txtToDt.Text)
                    ElseIf ddlReportType.SelectedIndex = 3 Then
                        ddlTripStatus_SelectedIndexChanged(sender, e)
                    End If
                End If

                End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtToDt_TextChanged")
        End Try
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            ddlTimeStatus.SelectedIndex = 0 : ddlMeter.SelectedIndex = 0
            If ddlStatus.SelectedIndex = 1 Then
                pnlTimeStatus.Visible = True
                pnlMeterStatus.Visible = False
                pnlTripStatus.Visible = False
            ElseIf ddlStatus.SelectedIndex = 2 Then
                pnlMeterStatus.Visible = True
                pnlTimeStatus.Visible = False
                pnlTripStatus.Visible = False
            ElseIf ddlStatus.SelectedIndex = 3 Then
                pnlMeterStatus.Visible = False
                pnlTimeStatus.Visible = False
                pnlTripStatus.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlRoute_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoute.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dFromText As String = "" : Dim dToText As String = ""
        Dim sTimeStatus As String = "", sTripStatus As Integer = 0, sMeterStatus As String = "", iroutIde As Integer = 0
        Dim idriverId As Integer = 0, ivehicleTypeId As Integer = 0
        Dim iCustId As Integer = 0
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlRoute.SelectedIndex > 0 Then
                pnlFromToDt.Visible = True : pnlStatus.Visible = True : pnlRoute.Visible = True
                pnlCustomers.Visible = True
                pnlDriver.Visible = True : pnlVehicleType.Visible = True
                If txtFromDt.Text = "" Or txtToDt.Text = "" Then
                    dFromText = "01/01/1900"
                    dToText = "01/01/1900"
                    dFromText = clsGeneralFunctions.FormatMyDate(dFromText)
                    dToText = clsGeneralFunctions.FormatMyDate(dToText)
                Else
                    dFromText = clsGeneralFunctions.FormatMyDate(txtFromDt.Text)
                    dToText = clsGeneralFunctions.FormatMyDate(txtToDt.Text)
                End If
                If ddlTimeStatus.SelectedIndex > 0 Then
                    sTimeStatus = ddlTimeStatus.SelectedItem.Text
                End If
                If ddlTripStatus.SelectedIndex > 0 Then
                    sTripStatus = ddlTripStatus.SelectedIndex
                End If
                If ddlMeter.SelectedIndex > 0 Then
                    sMeterStatus = ddlMeter.SelectedItem.Text
                End If
                If ddlCustomers.SelectedIndex > 0 Then
                    iCustId = ddlCustomers.SelectedValue
                End If
                If ddlRoute.SelectedIndex > 0 Then
                    iroutIde = ddlRoute.SelectedValue
                End If
                If ddlDriver.SelectedIndex > 0 Then
                    idriverId = ddlDriver.SelectedValue
                End If
                If ddlVehicleType.SelectedIndex > 0 Then
                    ivehicleTypeId = ddlVehicleType.SelectedValue
                End If

                If ddlReportType.SelectedIndex = 7 Then
                    ddlCustomers_SelectedIndexChanged(sender, e)
                ElseIf ddlReportType.SelectedIndex = 8 Then
                    ReportViewer1.Reset()
                    dt = objDR.LoadCustomerTripDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCustId, dFromText, dToText, sTimeStatus, sMeterStatus, iroutIde, idriverId, ivehicleTypeId, sTripStatus)
                    If dt.Rows.Count > 0 Then
                        Dim rds As New ReportDataSource("DataSet1", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicCustomerRpt.rdlc")

                        Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Route Report")}
                        ReportViewer1.LocalReport.SetParameters(ReportType)
                        ReportViewer1.LocalReport.Refresh()
                    Else
                        ReportViewer1.Reset()
                        ReportViewer1.LocalReport.Refresh()
                        lblError.Text = "No data to display"
                    End If
                End If
                End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRoute_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlDriver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriver.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dFromText As String = "" : Dim dToText As String = ""
        Dim sTimeStatus As String = "", sTripStatus As Integer = 0, sMeterStatus As String = "", iroutIde As Integer = 0, iCustId As Integer = 0
        Dim idriverId As Integer = 0, ivehicleTypeId As Integer = 0
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlReportType.SelectedIndex > 0 Then
                pnlFromToDt.Visible = True : pnlStatus.Visible = True : pnlRoute.Visible = True : pnlCustomers.Visible = True
                pnlDriver.Visible = True : pnlVehicleType.Visible = True
                If txtFromDt.Text = "" Or txtToDt.Text = "" Then
                    dFromText = "01/01/1900"
                    dToText = "01/01/1900"
                    dFromText = clsGeneralFunctions.FormatMyDate(dFromText)
                    dToText = clsGeneralFunctions.FormatMyDate(dToText)
                Else
                    dFromText = clsGeneralFunctions.FormatMyDate(txtFromDt.Text)
                    dToText = clsGeneralFunctions.FormatMyDate(txtToDt.Text)
                End If
                If ddlTimeStatus.SelectedIndex > 0 Then
                    sTimeStatus = ddlTimeStatus.SelectedItem.Text
                End If
                If ddlTripStatus.SelectedIndex > 0 Then
                    sTripStatus = ddlTripStatus.SelectedIndex
                End If
                If ddlMeter.SelectedIndex > 0 Then
                    sMeterStatus = ddlMeter.SelectedItem.Text
                End If
                If ddlCustomers.SelectedIndex > 0 Then
                    iCustId = ddlCustomers.SelectedValue
                End If
                If ddlRoute.SelectedIndex > 0 Then
                    iroutIde = ddlRoute.SelectedValue
                End If
                If ddlDriver.SelectedIndex > 0 Then
                    idriverId = ddlDriver.SelectedValue
                End If
                If ddlVehicleType.SelectedIndex > 0 Then
                    ivehicleTypeId = ddlVehicleType.SelectedValue
                End If
                ReportViewer1.Reset()
                dt = objDR.LoadCustomerTripDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCustId, dFromText, dToText, sTimeStatus, sMeterStatus, iroutIde, idriverId, ivehicleTypeId, sTripStatus)
                If dt.Rows.Count > 0 Then
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicCustomerRpt.rdlc")
                    Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Driverwise Report")}
                    ReportViewer1.LocalReport.SetParameters(ReportType)
                    ReportViewer1.LocalReport.Refresh()
                    'gvCustomerTrip.DataSource = dt
                    'gvCustomerTrip.DataBind() : gvCustomerTrip.Visible = True
                Else
                    ReportViewer1.Reset()
                    ReportViewer1.LocalReport.Refresh()
                    lblError.Text = "No data to display"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDriver_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlVehicleType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVehicleType.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dFromText As String = "" : Dim dToText As String = ""
        Dim sTimeStatus As String = "", sTripStatus As Integer = 0, sMeterStatus As String = "", iroutIde As Integer = 0, iCustId As Integer = 0
        Dim idriverId As Integer = 0, ivehicleTypeId As Integer = 0
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlReportType.SelectedIndex > 0 Then
                pnlFromToDt.Visible = True : pnlStatus.Visible = True : pnlRoute.Visible = True : pnlCustomers.Visible = True
                pnlDriver.Visible = True : pnlVehicleType.Visible = True
                If txtFromDt.Text = "" Or txtToDt.Text = "" Then
                    dFromText = "01/01/1900"
                    dToText = "01/01/1900"
                    dFromText = clsGeneralFunctions.FormatMyDate(dFromText)
                    dToText = clsGeneralFunctions.FormatMyDate(dToText)
                Else
                    dFromText = clsGeneralFunctions.FormatMyDate(txtFromDt.Text)
                    dToText = clsGeneralFunctions.FormatMyDate(txtToDt.Text)
                End If
                If ddlTimeStatus.SelectedIndex > 0 Then
                    sTimeStatus = ddlTimeStatus.SelectedItem.Text
                End If
                If ddlTripStatus.SelectedIndex > 0 Then
                    sTripStatus = ddlTripStatus.SelectedIndex
                End If
                If ddlMeter.SelectedIndex > 0 Then
                    sMeterStatus = ddlMeter.SelectedItem.Text
                End If
                If ddlCustomers.SelectedIndex > 0 Then
                    iCustId = ddlCustomers.SelectedValue
                End If
                If ddlRoute.SelectedIndex > 0 Then
                    iroutIde = ddlRoute.SelectedValue
                End If
                If ddlDriver.SelectedIndex > 0 Then
                    idriverId = ddlDriver.SelectedValue
                End If
                If ddlVehicleType.SelectedIndex > 0 Then
                    ivehicleTypeId = ddlVehicleType.SelectedValue
                End If
                ReportViewer1.Reset()
                dt = objDR.LoadCustomerTripDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCustId, dFromText, dToText, sTimeStatus, sMeterStatus, iroutIde, idriverId, ivehicleTypeId, sTripStatus)
                If dt.Rows.Count > 0 Then
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DynamicCustomerRpt.rdlc")
                    Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", "Vehicle Typewise Report")}
                    ReportViewer1.LocalReport.SetParameters(ReportType)
                    ReportViewer1.LocalReport.Refresh()
                    'gvCustomerTrip.DataSource = dt
                    'gvCustomerTrip.DataBind() : gvCustomerTrip.Visible = True
                Else
                    ReportViewer1.Reset()
                    ReportViewer1.LocalReport.Refresh()
                    lblError.Text = "No data to display"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlVehicleType_SelectedIndexChanged")
        End Try
    End Sub
End Class
