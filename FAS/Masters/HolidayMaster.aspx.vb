Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Masters_HolidayMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_HolidayMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsHolidayMaster As New clsHolidayMaster
    Private objclsFASPermission As New clsFASPermission
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Private Shared sHolidayDeleteDate As String
    Private Shared sHMDelete As String
    Private Shared dtHolidayDetails As New DataTable
    Private Shared sHMSaveDel As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnAddDays.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                chkCurrentYear.Visible = True : lnkWeeklyOff.Visible = True

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "HYM")
                imgbtnAddDays.Visible = False : imgbtnReport.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnAddDays.Visible = True
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If
                'chkCurrentYear.Visible = False : lnkWeeklyOff.Visible = False : imgbtnAddDays.Visible = False : imgbtnReport.Visible = False
                'sHMSaveDel = "NO"
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasHYM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        chkCurrentYear.Visible = True : lnkWeeklyOff.Visible = True : imgbtnAddDays.Visible = True
                '        sHMSaveDel = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If

                BindYearMaster()
                ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                ddlFinancialYear_SelectedIndexChanged(sender, e)
                RFVSelDate.ErrorMessage = "Enter Holiday Date." : REVSelDate.ErrorMessage = "Enter valid Holiday Date."
                REVSelDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVOccasion.ErrorMessage = "Enter Occasion." : REVOccasion.ValidationExpression = "^[\s\S]{0,500}$" : REVOccasion.ErrorMessage = "Description exceeded maximum size(max 500 character)."

                imgbtnAddDays.Attributes.Add("OnClick", "$('#ModalHoliday').modal('show');return false;")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    Public Sub BindYearMaster()
        Try
            ddlFinancialYear.DataSource = objclsHolidayMaster.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "Year"
            ddlFinancialYear.DataValueField = "YMS_ID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Dim ds As New DataSet
        Try
            lblError.Text = "" : lblHMError.Text = ""
            ds = objclsHolidayMaster.BindYearsDetails(sSession.AccessCode, sSession.AccessCodeID, 103, ddlFinancialYear.SelectedValue)
            If ds.Tables(0).Rows.Count <> 0 Then
                If IsDBNull(ds.Tables(0).Rows(0).Item("YMS_FROMDATE")) = False Then
                    txtFromDate.Text = objclsFASGeneral.FormatDtForRDBMS(ds.Tables(0).Rows(0).Item("YMS_FROMDATE"), "F")
                    lblFromDate.Text = objclsFASGeneral.FormatDtForRDBMS(ds.Tables(0).Rows(0).Item("YMS_FROMDATE"), "D")
                End If
                If IsDBNull(ds.Tables(0).Rows(0).Item("YMS_TODATE")) = False Then
                    txtToDate.Text = objclsFASGeneral.FormatDtForRDBMS(ds.Tables(0).Rows(0).Item("YMS_TODATE"), "F")
                    lblToDate.Text = objclsFASGeneral.FormatDtForRDBMS(ds.Tables(0).Rows(0).Item("YMS_TODATE"), "D")
                End If
                If IsDBNull(ds.Tables(0).Rows(0).Item("YMS_Default")) = False Then
                    If ds.Tables(0).Rows(0).Item("YMS_Default") = "1" Then
                        If sHMSaveDel = "Yes" Then
                            chkCurrentYear.Checked = True
                        End If
                    Else
                        If sHMSaveDel = "Yes" Then
                            chkCurrentYear.Checked = False
                        End If
                    End If
                End If
                LoadHolidayDetails(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged")
        End Try
    End Sub


    Private Sub chkCurrentYear_CheckedChanged(sender As Object, e As EventArgs) Handles chkCurrentYear.CheckedChanged
        Try
            lblError.Text = "" : lblHMError.Text = ""
            If chkCurrentYear.Checked = True Then
                objclsHolidayMaster.UpdateCurrentYear(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue)
                sSession.YearID = ddlFinancialYear.SelectedValue
                sSession.YearName = ddlFinancialYear.SelectedItem.Text
                Session("AllSession") = sSession
                lblYMValidationMsg.Text = ddlFinancialYear.SelectedItem.Text & " is Current Year for FAS." : lblError.Text = ddlFinancialYear.SelectedItem.Text & " is Current Year for FAS."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divYMMsgType').addClass('alert alert-success');$('#ModalYMValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCurrentYear_CheckedChanged")
        End Try
    End Sub
    Protected Sub lnkWeeklyOff_Click(sender As Object, e As EventArgs)
        Dim dSDate As Date, dEDate As Date, dDate As Date
        Dim diff As TimeSpan
        Dim days As Integer, i As Integer
        Dim testDate As String
        Dim Arr As String()
        Dim objHoliday As New clsHolidayMaster
        Dim dt As New DataTable, dt1 As New DataTable
        Try
            lblError.Text = "" : lblHMError.Text = ""
            dSDate = Date.ParseExact(Trim(lblFromDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dEDate = Date.ParseExact(Trim(lblToDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            diff = dEDate - dSDate
            days = diff.Days
            For i = 0 To days - 1
                dDate = dSDate.AddDays(i)
                testDate = dDate.DayOfWeek()
                If testDate = "0" Then
                    Dim DVHolidayDetails As New DataView(dtHolidayDetails)
                    DVHolidayDetails.RowFilter = "HDFormat='" & objclsFASGeneral.FormatDtForRDBMS(dDate.AddDays(-1), "D") & "'"
                    dt = DVHolidayDetails.ToTable
                    If dt.Rows.Count = 0 Then
                        objHoliday.iHolYearId = ddlFinancialYear.SelectedValue
                        objHoliday.dHoldate = dDate.AddDays(-1)
                        objHoliday.sHolRemarks = "Saturday"
                        objHoliday.iHolCreatedby = sSession.UserID
                        objHoliday.dHolCreatedOn = Date.Now()
                        objHoliday.iHolUpdatedBy = sSession.UserID
                        objHoliday.dHolUpdatedOn = Date.Now()
                        objHoliday.sHolDelflag = "A"
                        objHoliday.sHolStatus = "C"
                        objHoliday.sHolIPAddress = sSession.IPAddress
                        objHoliday.iHolCompID = sSession.AccessCodeID
                        Arr = objclsHolidayMaster.SaveHolidayDetails(sSession.AccessCode, objHoliday)
                        objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Year Master", "Saved", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, 0, objHoliday.dHoldate, sSession.IPAddress)
                    End If

                    Dim DVHolidayDetails1 As New DataView(dtHolidayDetails)
                    DVHolidayDetails1.RowFilter = "HDFormat='" & objclsFASGeneral.FormatDtForRDBMS(dDate, "D") & "'"
                    dt1 = DVHolidayDetails1.ToTable
                    If dt1.Rows.Count = 0 Then
                        objHoliday.iHolYearId = ddlFinancialYear.SelectedValue
                        objHoliday.dHoldate = dDate
                        objHoliday.sHolRemarks = "Sunday"
                        objHoliday.iHolCreatedby = sSession.UserID
                        objHoliday.dHolCreatedOn = Date.Now()
                        objHoliday.iHolUpdatedBy = sSession.UserID
                        objHoliday.dHolUpdatedOn = Date.Now()
                        objHoliday.sHolDelflag = "A"
                        objHoliday.sHolStatus = "C"
                        objHoliday.sHolIPAddress = sSession.IPAddress
                        objHoliday.iHolCompID = sSession.AccessCodeID
                        Arr = objclsHolidayMaster.SaveHolidayDetails(sSession.AccessCode, objHoliday)
                        objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Year Master", "Saved", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, 0, objHoliday.dHoldate, sSession.IPAddress)
                    End If
                End If
            Next

            lblYMValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divYMMsgType').addClass('alert alert-success');$('#ModalYMValidation').modal('show');", True)
            LoadHolidayDetails(0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkWeeklyOff_Click")
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = "" : lblHMError.Text = ""
            txtSelDate.Text = "" : txtOccasion.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalHoliday').modal('hide');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCancel_Click")
        End Try
    End Sub
    Protected Sub btnSaveHolidays_Click(sender As Object, e As EventArgs)
        Dim dHD As Date, dFD As Date, dTD As Date
        Dim l As Integer, m As Integer
        Dim objHoliday As New clsHolidayMaster
        Dim Arr As String()
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblHMError.Text = ""
            If txtOccasion.Text.Trim = "" Then
                lblHMValidationMsg.Text = "Enter Occasion." : lblHMError.Text = "Enter Occasion."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divHMMsgType').addClass('alert alert-warning');$('#ModalHMValidation').modal('show');", True)
                txtOccasion.Focus()
                Exit Sub
            End If
            If txtOccasion.Text.Trim.Length > 500 Then
                lblHMValidationMsg.Text = "Occasion exceeded maximum size(max 500 characters)." : lblHMError.Text = "Occasion exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divHMMsgType').addClass('alert alert-danger');$('#ModalHMValidation').modal('show');", True)
                txtOccasion.Focus()
                Exit Sub
            End If

            Dim SDate As String
            Try
                SDate = Date.ParseExact(txtSelDate.Text.Trim, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Catch ex As Exception
                lblHMValidationMsg.Text = "Enter valid Holiday Date(dd/MM/yyyy)." : lblHMError.Text = "Enter valid Holiday Date(dd/MM/yyyy)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divHMMsgType').addClass('alert alert-warning');$('#ModalHMValidation').modal('show');", True)
                txtSelDate.Focus()
                Exit Sub
            End Try

            dFD = Date.ParseExact(Trim(lblFromDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dTD = Date.ParseExact(Trim(lblToDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dHD = Date.ParseExact(Trim(txtSelDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            l = DateDiff(DateInterval.Day, dFD, dHD)
            If l < 0 Then
                lblHMValidationMsg.Text = "Holiday Date should be greater than From Date(" & lblFromDate.Text & ")." : lblHMError.Text = "Holiday Date should be greater than From Date(" & lblFromDate.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divHMMsgType').addClass('alert alert-danger');$('#ModalHMValidation').modal('show');", True)
                txtSelDate.Focus()
                Exit Sub
            End If
            m = DateDiff(DateInterval.Day, dHD, dTD)
            If m < 0 Then
                lblHMValidationMsg.Text = "Holiday Date should be less than To Date(" & lblToDate.Text & ")." : lblHMError.Text = "Holiday Date should be less than To Date(" & lblToDate.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divHMMsgType').addClass('alert alert-danger');$('#ModalHMValidation').modal('show');", True)
                txtSelDate.Focus()
                Exit Sub
            End If
            Dim DVHolidayDetails As New DataView(dtHolidayDetails)
            DVHolidayDetails.RowFilter = "HDFormat='" & txtToDate.Text & "'"
            dt = DVHolidayDetails.ToTable
            If dt.Rows.Count = 0 Then
                objHoliday.iHolYearId = ddlFinancialYear.SelectedValue
                objHoliday.dHoldate = dHD
                objHoliday.sHolRemarks = txtOccasion.Text
                objHoliday.iHolCreatedby = sSession.UserID
                objHoliday.dHolCreatedOn = Date.Now()
                objHoliday.iHolUpdatedBy = sSession.UserID
                objHoliday.dHolUpdatedOn = Date.Now()
                objHoliday.sHolDelflag = "A"
                objHoliday.sHolStatus = "C"
                objHoliday.sHolIPAddress = sSession.IPAddress
                objHoliday.iHolCompID = sSession.AccessCodeID
                Arr = objclsHolidayMaster.SaveHolidayDetails(sSession.AccessCode, objHoliday)
                lblYMValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divYMMsgType').addClass('alert alert-success');$('#ModalYMValidation').modal('show');", True)
            Else
                lblHMValidationMsg.Text = "Holiday Date already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divHMMsgType').addClass('alert alert-danger');$('#ModalHMValidation').modal('show');", True)
            End If
            txtSelDate.Text = "" : txtOccasion.Text = ""
            LoadHolidayDetails(0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveHolidays_Click")
        End Try
    End Sub
    Public Sub LoadHolidayDetails(ByVal iPageIndex As Integer)
        Try
            grdHolidays.DataSource = Nothing
            grdHolidays.DataBind()
            dtHolidayDetails = objclsHolidayMaster.HolidayMasterDetails(sSession.AccessCode, sSession.AccessCodeID, 103, ddlFinancialYear.SelectedValue)
            grdHolidays.CurrentPageIndex = iPageIndex
            If dtHolidayDetails.Rows.Count > grdHolidays.PageSize Then
                grdHolidays.AllowPaging = True
            Else
                grdHolidays.AllowPaging = False
            End If
            grdHolidays.DataSource = dtHolidayDetails
            grdHolidays.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub grdHolidays_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdHolidays.ItemDataBound
        Dim imgbtnDelete As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDelete = e.Item.FindControl("imgbtnDelete")
                imgbtnDelete.ImageUrl = "~/Images/Trash16.png"
                imgbtnDelete.Visible = False
                If sHMSaveDel = "YES" Then
                    imgbtnDelete.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdHolidays_ItemDataBound")
        End Try
    End Sub
    Private Sub grdHolidays_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdHolidays.ItemCommand
        Dim lblDate As New Label
        Try
            lblError.Text = "" : sHolidayDeleteDate = ""
            If e.CommandName = "Delete" Then
                lblDate = e.Item.FindControl("lblDate")
                sHolidayDeleteDate = lblDate.Text
                lblConfirmDelete.Text = "Are you sure you want to Remove?"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divDeleteConfirm').addClass('alert alert-warning');$('#ModalDeleteconfirmation').modal('show');$('#ModalYMValidation').modal('hide');$('#ModalHoliday').modal('hide');$('#ModalHMValidation').modal('hide');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdHolidays_ItemCommand")
        End Try
    End Sub
    Protected Sub grdHolidays_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles grdHolidays.PageIndexChanged
        Try
            lblError.Text = "" : lblHMError.Text = ""
            LoadHolidayDetails(e.NewPageIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdHolidays_PageIndexChanged")
        End Try
    End Sub
    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = "" : lblHMError.Text = ""
            objclsHolidayMaster.DeleteHoliday(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sHolidayDeleteDate, ddlFinancialYear.SelectedValue)
            lblYMValidationMsg.Text = "Successfully Removed." : lblError.Text = "Successfully Removed."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divYMMsgType').addClass('alert alert-success');$('#ModalDeleteconfirmation').modal('hide');$('#ModalYMValidation').modal('show');$('#ModalHoliday').modal('hide');$('#ModalHMValidation').modal('hide');", True)
            LoadHolidayDetails(0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnConfirmDelete_Click")
        End Try
    End Sub
    Protected Sub btnOkHM_Click(sender As Object, e As EventArgs)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalDeleteconfirmation').modal('hide');$('#ModalYMValidation').modal('hide');$('#ModalHoliday').modal('show');$('#ModalHMValidation').modal('hide');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOkHM_Click")
        End Try
    End Sub
    Public Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = "" : lblHMError.Text = ""
            If dtHolidayDetails.Rows.Count = 0 Then
                lblYMValidationMsg.Text = "No Data."  : lblError.Text = "No Data." 
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divYMMsgType').addClass('alert alert-info');$('#ModalYMValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtHolidayDetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/HolidayMaster.rdlc")
            Dim FY As ReportParameter() = New ReportParameter() {New ReportParameter("FinancialYear", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(FY)
            Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", txtFromDate.Text)}
            ReportViewer1.LocalReport.SetParameters(FromDate)
            Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", txtToDate.Text)}
            ReportViewer1.LocalReport.SetParameters(ToDate)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Year Master", "PDF", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=HolidayMaster" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = "" : lblHMError.Text = ""
            If dtHolidayDetails.Rows.Count = 0 Then
                lblYMValidationMsg.Text = "No Data."  : lblError.Text = "No Data." 
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divYMMsgType').addClass('alert alert-info');$('#ModalYMValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtHolidayDetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/HolidayMaster.rdlc")
            Dim FY As ReportParameter() = New ReportParameter() {New ReportParameter("FinancialYear", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(FY)
            Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", txtFromDate.Text)}
            ReportViewer1.LocalReport.SetParameters(FromDate)
            Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", txtToDate.Text)}
            ReportViewer1.LocalReport.SetParameters(ToDate)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Year Master", "Excel", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=HolidayMaster" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
End Class