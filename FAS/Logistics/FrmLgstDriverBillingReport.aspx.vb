Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports System.Globalization
Partial Class Logistics_FrmLgstDriverBillingReport
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/PumpBilling"
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
    Dim objDriverBilling As New clsDriverBillReport

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DBR")
                '      imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCMDSave = "NO" : imgbtnWaiting.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        '   imgbtnSave.Visible = True

                        sCMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        '   imgbtnAdd.Visible = True
                    End If
                End If
                LoadDrivers()
            End If
        Catch
        End Try
    End Sub
    Public Sub LoadDrivers()
        Try
            ddlDriver.DataSource = objDriverBilling.LoadDriver(sSession.AccessCode, sSession.AccessCodeID)
            ddlDriver.DataTextField = "LDM_DriverName"
            ddlDriver.DataValueField = "LDM_ID"
            ddlDriver.DataBind()
            ddlDriver.Items.Insert(0, "Select Driver Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlDriver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriver.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlDriver.SelectedIndex > 0 Then
                ddlBillNo.DataSource = objDriverBilling.LoadBillNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDriver.SelectedValue)
                ddlBillNo.DataTextField = "LDB_BillNo"
                ddlBillNo.DataValueField = "LDB_ID"
                ddlBillNo.DataBind()
                ddlBillNo.Items.Insert(0, "Select bill No")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlBillNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBillNo.SelectedIndexChanged
        Dim dtDriverName As New DataTable, dtTripDetails As New DataTable, dtDdBilldetails As New DataTable
        Dim sFromDate As String = "", sToDate As String = ""
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlDriver.SelectedIndex > 0 Then
                dt = objDriverBilling.GetAccessCode(sSession.AccessCode)
                dtDriverName = objDriverBilling.LoadDriverDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDriver.SelectedValue)
                dtDdBilldetails = objDriverBilling.LoadDriverBillDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDriver.SelectedValue, ddlBillNo.SelectedItem.Text, sSession.YearID)

                dtTripDetails = objDriverBilling.LoadTripDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDriver.SelectedValue, dtDdBilldetails.Rows(0).Item("sFromDate"), dtDdBilldetails.Rows(0).Item("sToDate"))
                Dim dTotalDue As Double = 0.0
                If dtTripDetails.Rows.Count > 0 Then
                    For j = 0 To dtTripDetails.Rows.Count - 1
                        dTotalDue = dTotalDue + Val(dtTripDetails.Rows(j).Item("Balance").ToString())
                    Next
                End If

                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet1", dtDriverName)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                Dim rds1 As New ReportDataSource("DataSet2", dtTripDetails)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                Dim rds2 As New ReportDataSource("DataSet3", dtDdBilldetails)
                ReportViewer1.LocalReport.DataSources.Add(rds2)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/DriverBilling.rdlc")
                ReportViewer1.LocalReport.Refresh()
                Dim dTotalAmtDue As ReportParameter() = {New ReportParameter("dTotalAmtDue", String.Format("{0:0.00}", Convert.ToDecimal(dTotalDue)))}

                Dim sCompName As ReportParameter() = {New ReportParameter("sCompName", dt.Rows(0).Item("Sad_CMS_Name").ToString())}
                ReportViewer1.LocalReport.SetParameters(sCompName)

                ReportViewer1.LocalReport.SetParameters(dTotalAmtDue)
                ReportViewer1.LocalReport.Refresh()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBillNo_SelectedIndexChanged")
        End Try
    End Sub
End Class
