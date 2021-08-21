Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Text.RegularExpressions
Imports System.Web.Services
Imports System.Net
Imports System.Web.Script.Services
Imports Microsoft.Reporting.WebForms
Partial Class Masters_CurrencyMasterDashboard
    Inherits System.Web.UI.Page
    Private Shared sSession As AllSession
    Private objSettings As New clsApplicationSettings
    Private Shared dtCurrencyMaster As DataTable
    Private Shared objclsCurrencyMaster As New clsCurrencyMaster
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private sFormName As String = "CurrencyMasterDashboard"
    Private Shared iPkID As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadCurrencyTypeDB() : LoadCurrencyType() : GetAppSettings()
                ddlfromcurrency_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadCurrencyTypeDB()
        Dim dt As New DataTable
        Try
            dt = objSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlToCurrency.DataSource = dt
            ddlToCurrency.DataTextField = "CUR_CODE"
            ddlToCurrency.DataValueField = "CUR_ID"
            ddlToCurrency.DataBind()
            ddlToCurrency.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCurrencyType()
        Dim dt As New DataTable
        Try
            dt = objSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlfromcurrency.DataSource = dt
            ddlfromcurrency.DataTextField = "CUR_CODE"
            ddlfromcurrency.DataValueField = "CUR_ID"
            ddlfromcurrency.DataBind()
            ddlfromcurrency.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetAppSettings()
        Dim dt As New DataTable
        Dim i As Integer
        Try
            dt = objSettings.GetApllicationSettingsDetails(sSession.AccessCode, sSession.AccessCodeID)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("sad_Config_Key") = "Currency" Then    'Currency
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        ddlfromcurrency.SelectedValue = dt.Rows(i)("sad_Config_Value")
                    End If
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub gvcurrencyMaster_PreRender(sender As Object, e As EventArgs) Handles gvcurrencyMaster.PreRender
        Try
            If gvcurrencyMaster.Rows.Count > 0 Then
                gvcurrencyMaster.UseAccessibleHeader = True
                gvcurrencyMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvcurrencyMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvcurrencyMaster_PreRender")
        End Try
    End Sub
    Private Function LoadAllCurrencyDeatils() As DataTable
        Dim sSearchText As String = "", sFrom As String = "", sTo As String = ""
        Dim iOperateOn As Integer = 0, iCurrency As Integer
        Try
            If (ddlfromcurrency.SelectedIndex > 0) Then
                iCurrency = ddlfromcurrency.SelectedValue
            End If
            If (ddlToCurrency.SelectedIndex > 0) Then
                iOperateOn = ddlToCurrency.SelectedValue
            End If
            If txtStartDate.Text <> "" Then
                sFrom = txtStartDate.Text
            End If
            If txtEndDate.Text <> "" Then
                sTo = txtEndDate.Text
            End If
            dtCurrencyMaster = objclsCurrencyMaster.LoadCurrencyDashboard(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCurrency, iOperateOn, sFrom, sTo)
            gvcurrencyMaster.DataSource = dtCurrencyMaster
            gvcurrencyMaster.DataBind()
            Return dtCurrencyMaster
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub ddlfromcurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlfromcurrency.SelectedIndexChanged
        Dim sFrom As String = "", sTo As String = ""
        Try
            lblError.Text = ""
            iPkID = 0
            If (ddlfromcurrency.SelectedIndex = 0) Then
                txtStartDate.Text = "" : txtEndDate.Text = ""
            End If
            LoadAllCurrencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlfromcurrency_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlToCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlToCurrency.SelectedIndexChanged
        Dim sFrom As String = "", sTo As String = ""
        Try
            lblError.Text = ""
            iPkID = 0
            If (ddlToCurrency.SelectedIndex = 0) Then
                txtStartDate.Text = "" : txtEndDate.Text = ""
            End If
            LoadAllCurrencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlToCurrency_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Dim dASDate As Date, dAEDate As Date
        Dim iOperateOn As Integer = 0
        Dim sFrom As String = "", sTo As String = ""
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If txtStartDate.Text.Trim = "" Then
                lblValidationMsg.Text = "Select From Date." : lblError.Text = "Select From Date."
                txtStartDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            Else
                Try
                    dASDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblValidationMsg.Text = "Enter valid From Date." : lblError.Text = "Enter valid From Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    txtStartDate.Focus()
                    Exit Sub
                End Try
            End If

            If txtEndDate.Text.Trim = "" Then
                lblValidationMsg.Text = "Select To Date." : lblError.Text = "Select To Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                txtEndDate.Focus()
                Exit Sub
            Else
                Try
                    dAEDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblValidationMsg.Text = "Enter valid To Date." : lblError.Text = "Enter valid To Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    txtEndDate.Focus()
                    Exit Sub
                End Try
            End If

            Dim dSSDate As Date, dSCDate As Date
            dSSDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSCDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dSSDate > dSCDate Then
                txtEndDate.Focus()
                lblValidationMsg.Text = "To Date (" & txtEndDate.Text & ") should be greater than or equal to From Date (" & txtStartDate.Text & ")."
                lblError.Text = "To Date (" & txtEndDate.Text & ") should be greater than or equal to From Date (" & txtStartDate.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            LoadAllCurrencyDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvcurrencyMaster.Rows.Count - 1
                    chkField = gvcurrencyMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvcurrencyMaster.Rows.Count - 1
                    chkField = gvcurrencyMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            Response.Redirect(String.Format("~/Masters/CurrencyMasterDetails.aspx"), False) 'BankCurrencyDetails
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            dt = LoadAllCurrencyDeatils()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/CurrencyMatser.rdlc")

            If ddlfromcurrency.SelectedIndex > 0 Then
                Dim BaseCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("BaseCurrency", ddlfromcurrency.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(BaseCurrency)
            Else
                Dim BaseCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("BaseCurrency", " ")}
                ReportViewer1.LocalReport.SetParameters(BaseCurrency)
            End If

            If ddlToCurrency.SelectedIndex > 0 Then
                Dim Currency As ReportParameter() = New ReportParameter() {New ReportParameter("Currency", ddlToCurrency.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Currency)
            Else
                Dim Currency As ReportParameter() = New ReportParameter() {New ReportParameter("Currency", " ")}
                ReportViewer1.LocalReport.SetParameters(Currency)
            End If

            If txtStartDate.Text <> "" Then
                Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", txtStartDate.Text)}
                ReportViewer1.LocalReport.SetParameters(FromDate)
            Else
                Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", " ")}
                ReportViewer1.LocalReport.SetParameters(FromDate)
            End If

            If txtEndDate.Text <> "" Then
                Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", txtEndDate.Text)}
                ReportViewer1.LocalReport.SetParameters(ToDate)
            Else
                Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", " ")}
                ReportViewer1.LocalReport.SetParameters(ToDate)
            End If
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Currency Master Dashboard", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CurrencyMatser" + ".pdf")
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
        Dim dt As New DataTable
        Dim iOperateOn As Integer = 0
        Dim sFrom As String = "", sTo As String = ""
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            dt = LoadAllCurrencyDeatils()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/CurrencyMatser.rdlc")


            If ddlfromcurrency.SelectedIndex > 0 Then
                Dim BaseCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("BaseCurrency", ddlfromcurrency.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(BaseCurrency)
            Else
                Dim BaseCurrency As ReportParameter() = New ReportParameter() {New ReportParameter("BaseCurrency", " ")}
                ReportViewer1.LocalReport.SetParameters(BaseCurrency)
            End If

            If ddlToCurrency.SelectedIndex > 0 Then
                Dim Currency As ReportParameter() = New ReportParameter() {New ReportParameter("Currency", ddlToCurrency.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Currency)
            Else
                Dim Currency As ReportParameter() = New ReportParameter() {New ReportParameter("Currency", " ")}
                ReportViewer1.LocalReport.SetParameters(Currency)
            End If

            If txtStartDate.Text <> "" Then
                Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", txtStartDate.Text)}
                ReportViewer1.LocalReport.SetParameters(FromDate)
            Else
                Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", " ")}
                ReportViewer1.LocalReport.SetParameters(FromDate)
            End If

            If txtEndDate.Text <> "" Then
                Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", txtEndDate.Text)}
                ReportViewer1.LocalReport.SetParameters(ToDate)
            Else
                Dim ToDate As ReportParameter() = New ReportParameter() {New ReportParameter("ToDate", " ")}
                ReportViewer1.LocalReport.SetParameters(ToDate)
            End If
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Currency Master Dashboard", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CurrencyMatser" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
End Class
