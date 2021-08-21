Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Partial Class Reports_VendorDynamicReports
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Reports\VendorDynamicReports.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Dim objDrpt As New ClsVendorDynamicReports
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search16.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadAssetReferenceNo()
                LoadSupplier()
                LoadVoucherNo()
                loaddetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadSupplier()
        Dim dt As New DataTable
        Try
            dt = objDrpt.LoadSupplierName(sSession.AccessCode, sSession.AccessCodeID)
            ddlSuppliers.DataTextField = "AVR_Party"
            ddlSuppliers.DataValueField = "AVR_ID"
            ddlSuppliers.DataSource = dt
            ddlSuppliers.DataBind()
            ddlSuppliers.Items.Insert(0, "Select supplierName")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSupplier")
        End Try
    End Sub
    Public Sub LoadAssetReferenceNo()
        Dim dt As New DataTable
        Try
            dt = objDrpt.LoadAssetReferenceNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlRefno.DataTextField = "AVR_RefNo"
            ddlRefno.DataValueField = "AVR_ID"
            ddlRefno.DataSource = dt
            ddlRefno.DataBind()
            ddlRefno.Items.Insert(0, "Select Refno")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAssetReferenceNo")
        End Try
    End Sub
    Public Sub LoadVoucherNo()
        Dim dt As New DataTable
        Try
            dt = objDrpt.LoadVoucherNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlVoucherNo.DataTextField = "AVR_VoucherNo"
            ddlVoucherNo.DataValueField = "AVR_ID"
            ddlVoucherNo.DataSource = dt
            ddlVoucherNo.DataBind()
            ddlVoucherNo.Items.Insert(0, "Select VoucherNo")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadVoucherNo")
        End Try
    End Sub
    Private Sub loaddetails()
        Dim dt, dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim dtCust As New DataTable
        Dim onjFasGnrl As New clsFASGeneral
        Dim dRow As DataRow
        Dim iRefId As Integer = 0
        Dim iSuppliers As Integer = 0
        Dim iVoucherNo As Integer = 0
        Dim str As String = " "
        Dim dFromDate As Date, dTo As Date
        Dim irstatus As Integer = 0
        Dim sd As String = "", st As String = ""
        Try
            lblError.Text = ""
            str = "Report is based on "
            If (rbtnStatus.SelectedValue > 0) Then
                irstatus = rbtnStatus.SelectedValue
                str = str + " And " + rbtnStatus.SelectedItem.Text
            End If
            If (ddlRefno.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + " " + ddlRefno.SelectedItem.Text
                Else
                    str = str + " And " + ddlRefno.SelectedItem.Text
                End If
                iRefId = ddlRefno.SelectedValue
            End If
            If (ddlSuppliers.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + " " + ddlSuppliers.SelectedItem.Text
                Else
                    str = str + " And " + ddlSuppliers.SelectedItem.Text
                End If
            Else
                If (ddlSuppliers.SelectedIndex <> -1 And ddlRefno.SelectedIndex > 0) Then
                    iSuppliers = ddlSuppliers.SelectedValue
                End If
            End If
            If (ddlVoucherNo.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + " " + ddlVoucherNo.SelectedItem.Text
                Else
                    str = str + " And " + ddlVoucherNo.SelectedItem.Text
                End If
            Else
                If (ddlVoucherNo.SelectedIndex <> -1 And ddlVoucherNo.SelectedIndex > 0) Then
                    iVoucherNo = ddlVoucherNo.SelectedValue
                End If
            End If
            If (txtfrom.Text <> "" And txtTo.Text <> "") Then
                If str = " " Then
                    str = str + " " + " Date between" + onjFasGnrl.FormatDtForRDBMS(txtfrom.Text, "D") + " And " + onjFasGnrl.FormatDtForRDBMS(txtTo.Text, "D")
                Else
                    str = str + " And " + "Date between" + onjFasGnrl.FormatDtForRDBMS(txtfrom.Text, "D") + " And " + onjFasGnrl.FormatDtForRDBMS(txtTo.Text, "D")
                End If
            End If
            If txtfrom.Text <> "" Then
                dFromDate = Date.ParseExact(Trim(onjFasGnrl.FormatDtForRDBMS(txtfrom.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                dFromDate = "01/01/1900"
            End If
            If txtTo.Text <> "" Then
                dTo = Date.ParseExact(Trim(onjFasGnrl.FormatDtForRDBMS(txtTo.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                dTo = "01/01/1900"
            End If

            If txtTrtype.Text <> "" Then
                If str = "" Then
                    str = str + "" + txtTrtype.Text
                Else
                    str = str + "and" + txtTrtype.Text
                End If
            End If
            If txtDebit.Text <> "" Then
                If str = "" Then
                    str = str + "" + txtDebit.Text
                Else
                    str = str + "and" + txtDebit.Text
                End If
            End If
            If txtcredit.Text <> "" Then
                If str = "" Then
                    str = str + "" + txtcredit.Text
                Else
                    str = str + "and" + txtcredit.Text
                End If
            End If
            dt = objDrpt.LoadDetails1(sSession.AccessCode, sSession.AccessCodeID, rbtnStatus.SelectedValue, iRefId, iSuppliers, iVoucherNo, txtTrtype.Text, dFromDate, dTo, txtDebit.Text, txtcredit.Text)
            'dt = objDrpt.LoadDetails1(sSession.AccessCode, sSession.AccessCodeID, 2, 0, 0, 0, "a", "a", "a", "a", "a")
            If (rbtnStatus.SelectedValue = 3) Then
                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet2", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptVendorDynamic.rdlc")
                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                ReportViewer1.ZoomPercent = 125
                ReportViewer1.LocalReport.Refresh()
                dt2.Columns.Add("CCust_name")
                dt2.Columns.Add("CCust_address")
                dt2.Columns.Add("CCust_email")
                dt2.Columns.Add("CCust_ph")
                dt2.Columns.Add("CCust_vat")
                dt2.Columns.Add("CCust_tax")
                dt2.Columns.Add("CCust_pan")
                dt2.Columns.Add("CCust_tan")
                dt2.Columns.Add("CCust_tin")
                dt2.Columns.Add("CCust_cin")
                dtCust = objDrpt.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If dtCust.Rows.Count > 0 Then
                    dRow = dt2.NewRow()
                    dRow("CCust_name") = dtCust.Rows(0)("CUST_NAME")
                    dRow("CCust_address") = dtCust.Rows(0)("CUST_Comm_Address")
                    dRow("CCust_email") = dtCust.Rows(0)("CUST_Email")
                    dRow("CCust_ph") = dtCust.Rows(0)("CUST_Comm_Tel")
                    dRow("CCust_vat") = dtCust.Rows(0)("CVAT")
                    dRow("CCust_tax") = dtCust.Rows(0)("CTAX")
                    dRow("Ccust_pan") = dtCust.Rows(0)("CPAN")
                    dRow("CCust_tan") = dtCust.Rows(0)("CTAN")
                    dRow("CCust_tin") = dtCust.Rows(0)("CTIN")
                    dRow("CCust_cin") = dtCust.Rows(0)("CCIN")
                    dt2.Rows.Add(dRow)
                End If
                Dim rds1 As New ReportDataSource("DataSet1", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptVendorDynamic.rdlc")
                ReportViewer1.LocalReport.Refresh()
            Else
                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet2", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptVendorDynamic.rdlc")
                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                ReportViewer1.ZoomPercent = 125
                ReportViewer1.ZoomPercent = 125
                dt2.Columns.Add("CCust_name")
                dt2.Columns.Add("CCust_address")
                dt2.Columns.Add("CCust_email")
                dt2.Columns.Add("CCust_ph")
                dt2.Columns.Add("CCust_vat")
                dt2.Columns.Add("CCust_tax")
                dt2.Columns.Add("CCust_pan")
                dt2.Columns.Add("CCust_tan")
                dt2.Columns.Add("CCust_tin")
                dt2.Columns.Add("CCust_cin")
                dtCust = objDrpt.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If dtCust.Rows.Count > 0 Then
                    dRow = dt2.NewRow()
                    dRow("CCust_name") = dtCust.Rows(0)("CUST_NAME")
                    dRow("CCust_address") = dtCust.Rows(0)("CUST_Comm_Address")
                    dRow("CCust_email") = dtCust.Rows(0)("CUST_Email")
                    dRow("CCust_ph") = dtCust.Rows(0)("CUST_Comm_Tel")
                    dRow("CCust_vat") = dtCust.Rows(0)("CVAT")
                    dRow("CCust_tax") = dtCust.Rows(0)("CTAX")
                    dRow("Ccust_pan") = dtCust.Rows(0)("CPAN")
                    dRow("CCust_tan") = dtCust.Rows(0)("CTAN")
                    dRow("CCust_tin") = dtCust.Rows(0)("CTIN")
                    dRow("CCust_cin") = dtCust.Rows(0)("CCIN")
                    dt2.Rows.Add(dRow)
                End If
                Dim rds1 As New ReportDataSource("DataSet1", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptVendorDynamic.rdlc")
                ReportViewer1.LocalReport.Refresh()
            End If
            'Dim VendorReconcilation As ReportParameter() = New ReportParameter() {New ReportParameter("VendorReconcilation", str)}
            'ReportViewer1.LocalReport.SetParameters(VendorReconcilation)
            If dt.Rows.Count = 0 Then
                Dim Msg1 As String = "NoData."
                Dim Msg As ReportParameter() = New ReportParameter() {New ReportParameter("Msg", Msg1)}
                ReportViewer1.LocalReport.SetParameters(Msg)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loaddetails")
        End Try
    End Sub

    Private Sub rbtnStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtnStatus.SelectedIndexChanged
        Try
            loaddetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rbtnStatus_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Try
            loaddetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
End Class
