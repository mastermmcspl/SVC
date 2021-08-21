Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports System.Globalization

Partial Class Logistics_FrmLgstCustomerBillingReport
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/CustomerBilling"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Dim objCOA As New clsChartOfAccounts
    Private Shared sCMDSave As String
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objcustBilling As New clsCustBillReport
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sStr As String = ""
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "CBR")
                'imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCMDSave = "NO" : imgbtnWaiting.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        '    imgbtnSave.Visible = True

                        '  sCMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        '      imgbtnAdd.Visible = True
                    End If
                End If
                LoadCustomers()
            End If
        Catch
        End Try
    End Sub
    Public Function GetLineNumber(ByVal ex As Exception)
        Dim lineNumber As Int32 = 0
        Const lineSearch As String = ":line "
        Dim index = ex.StackTrace.LastIndexOf(lineSearch)
        If index <> -1 Then
            Dim lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length)
            If Int32.TryParse(lineNumberText, lineNumber) Then
            End If
        End If
        Return lineNumber
    End Function
    Public Sub LoadCustomers()
        Try
            ddlCustomers.DataSource = objcustBilling.LoadCustomer1(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomers.DataTextField = "BM_Name"
            ddlCustomers.DataValueField = "BM_ID"
            ddlCustomers.DataBind()
            ddlCustomers.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlInvoiceNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInvoiceNo.SelectedIndexChanged
        Dim dt0 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim dt As New DataTable
        Dim dtDetails As New DataTable
        Dim dtCustBillDetails As New DataTable
        Dim dtCustInvDetails As New DataTable
        Dim company As String = ""
        Dim route As String = ""
        Dim sRouteName As String = ""
        Dim customerState As String = "", CompState As String = ""
        Dim dRow As DataRow
        Dim dtotal As Double = 0.0, custState As String = ""
        Dim dtotaltax As Double = 0.0
        Dim invtotal As Double = 0.0, dtotalAmt As Double = 0.0
        Dim sFromDate As String = "", sToDate As String = ""
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlInvoiceNo.SelectedIndex > 0 Then
                dt3 = objcustBilling.GetAccessCode(sSession.AccessCode)
                company = dt3.Rows(0).Item("SAD_CMS_AccessCode").ToString()
                dt0 = objcustBilling.LoadCompanyDetails1(sSession.AccessCode, sSession.AccessCodeID, company)
                dt2 = objcustBilling.LoadCustomerDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue)
                customerState = objcustBilling.CustomerStates(sSession.AccessCode, sSession.AccessCodeID, dt2.Rows(0).Item("State").ToString())
                CompState = objcustBilling.CustomerStates(sSession.AccessCode, sSession.AccessCodeID, dt0.Rows(0).Item("CSTATE").ToString())
                dtCustInvDetails = objcustBilling.LoadInvDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlInvoiceNo.SelectedItem.Text, sSession.YearID)
                sRouteName = objcustBilling.RouteNAme(sSession.AccessCode, sSession.AccessCodeID, dtCustInvDetails.Rows(0).Item("RouteID").ToString())
                company = dt3.Rows(0).Item("SAD_CMS_AccessCode").ToString()
                ' sFromDate = objGen.FormatDtForRDBMS(dtCustInvDetails.Rows(0).Item("FromDate").ToString(), "Q1") 'Modified Vijayalakshmi 04/03/2021
                ' sToDate = objGen.FormatDtForRDBMS(dtCustInvDetails.Rows(0).Item("ToDate").ToString(), "Q1")
                ' sFromDate = objGen.FormatDtForRDBMS(dtCustInvDetails.Rows(0).Item("FromDate"), "T")
                '  sToDate = objGen.FormatDtForRDBMS(dtCustInvDetails.Rows(0).Item("ToDate"), "T")
                dt.Columns.Add("SrNo")
                dt.Columns.Add("VehicleNo")
                dt.Columns.Add("DepartureDate")
                dt.Columns.Add("Origin")
                dt.Columns.Add("Destination")
                dt.Columns.Add("SVCTSNo")
                dt.Columns.Add("ClientRefNo")
                dt.Columns.Add("ContractAmount")
                dtDetails = objcustBilling.loadTripDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlInvoiceNo.SelectedValue, dtCustInvDetails.Rows(0).Item("RouteId").ToString(), dtCustInvDetails.Rows(0).Item("sFromDate"), dtCustInvDetails.Rows(0).Item("sToDate"), sSession.YearID)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1
                        dRow = dt.NewRow()
                        dRow("SrNo") = i + 1
                        dRow("VehicleNo") = objDBL.SQLExecuteScalar(sSession.AccessCode, "select LVM_RegNo from Lgst_Vehicle_Master where LVM_ID= " & dtDetails.Rows(i)("LTGM_VehivleNo") & "")
                        dRow("DepartureDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("LTGM_StartDate").ToString(), "D")
                        dRow("Origin") = dtDetails.Rows(i)("LTGM_StartCity")
                        dRow("Destination") = dtDetails.Rows(i)("LTGM_DestinationCity")
                        dRow("SVCTSNo") = dtDetails.Rows(i)("LTGM_SVCNo")
                        dRow("ClientRefNo") = dtDetails.Rows(i)("LTGM_ClientRefNo")
                        dRow("ContractAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(0).Item("LTGM_Rate").ToString()))
                        dtotal = dtotal + dtDetails.Rows(i)("LTGM_Rate")
                        dt.Rows.Add(dRow)
                    Next
                End If

                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                Dim rds2 As New ReportDataSource("DataSet2", dtCustInvDetails)
                ReportViewer1.LocalReport.DataSources.Add(rds2)
                Dim rds3 As New ReportDataSource("DataSet3", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds3)
                Dim rds4 As New ReportDataSource("DataSet4", dt0)
                ReportViewer1.LocalReport.DataSources.Add(rds4)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/CustomerBilling.rdlc")
                ReportViewer1.LocalReport.Refresh()

                Dim sLRM_Route As ReportParameter() = {New ReportParameter("sLRM_Route", sRouteName)}
                ReportViewer1.LocalReport.SetParameters(sLRM_Route)

                Dim sCustSTate As ReportParameter() = {New ReportParameter("sCustSTate", customerState)}
                ReportViewer1.LocalReport.SetParameters(sCustSTate)

                If CompState = "" Then
                    CompState = "State"
                End If
                Dim sCompSTate As ReportParameter() = {New ReportParameter("sCompSTate", CompState)}
                ReportViewer1.LocalReport.SetParameters(sCompSTate)

                Dim sCompName As ReportParameter() = {New ReportParameter("sCompName", dt3.Rows(0).Item("Sad_CMS_Name").ToString())}
                ReportViewer1.LocalReport.SetParameters(sCompName)

                Dim sCustName As ReportParameter() = {New ReportParameter("sCustName", dt2.Rows(0).Item("Name").ToString())}
                ReportViewer1.LocalReport.SetParameters(sCustName)

                invtotal = dtotal + String.Format("{0:0.00}", Convert.ToDecimal(dtCustInvDetails.Rows(0).Item("SGSTAmount").ToString())) + String.Format("{0:0.00}", Convert.ToDecimal(dtCustInvDetails.Rows(0).Item("CGSTAmount").ToString())) + String.Format("{0:0.00}", Convert.ToDecimal(dtCustInvDetails.Rows(0).Item("IGSTAmount").ToString()))
                dtotaltax = invtotal
                Dim dtotaltax1 As ReportParameter() = {New ReportParameter("dtotaltax1", String.Format("{0:0.00}", Convert.ToDecimal(dtotaltax)))}
                ReportViewer1.LocalReport.SetParameters(dtotaltax1)

                dtotalAmt = dtotal
                Dim sdtotalAmt As ReportParameter() = {New ReportParameter("sdtotalAmt", String.Format("{0:0.00}", Convert.ToDecimal(dtotalAmt)))}
                ReportViewer1.LocalReport.SetParameters(sdtotalAmt)

                If invtotal <> 0 Then
                    Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", objcustBilling.NumberToWord(String.Format("{0:0.00}", invtotal)) & "Only")}
                    ReportViewer1.LocalReport.SetParameters(NoToWord)
                Else
                    Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", "0")}
                    ReportViewer1.LocalReport.SetParameters(NoToWord)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInvoiceNo_SelectedIndexChanged", GetLineNumber(ex))
        End Try
    End Sub

    Private Sub ddlCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomers.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlCustomers.SelectedIndex > 0 Then
                ReportViewer1.Reset()
                ReportViewer1.LocalReport.Refresh()
                ddlInvoiceNo.DataSource = objcustBilling.LoadInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomers.SelectedValue)
                ddlInvoiceNo.DataTextField = "LCB_InvNo"
                ddlInvoiceNo.DataValueField = "LCB_ID"
                ddlInvoiceNo.DataBind()
                ddlInvoiceNo.Items.Insert(0, "Select Invoice No")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
