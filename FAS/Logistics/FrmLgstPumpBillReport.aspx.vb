Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports System.Globalization

Partial Class Logistics_FrmLgstPumpBillReport
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
    Dim objPumpBilling As New clsPumpBillReport
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
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PBR")
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
                LoadCustomers()
            End If
        Catch
        End Try
    End Sub
    Public Sub LoadCustomers()
        Try
            ddlPump.DataSource = objPumpBilling.LoadPump(sSession.AccessCode, sSession.AccessCodeID)
            ddlPump.DataTextField = "LPM_PumpName"
            ddlPump.DataValueField = "LPM_ID"
            ddlPump.DataBind()
            ddlPump.Items.Insert(0, "Select Pump Station")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlPump_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPump.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlPump.SelectedIndex > 0 Then
                ddlBillNo.DataSource = objPumpBilling.LoadInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPump.SelectedValue)
                ddlBillNo.DataTextField = "LPB_BillNo"
                ddlBillNo.DataValueField = "LPB_ID"
                ddlBillNo.DataBind()
                ddlBillNo.Items.Insert(0, "Select bill No")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlBillNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBillNo.SelectedIndexChanged
        Dim dtPumpName As New DataTable, dtTripDetails As New DataTable, dtPpBilldetails As New DataTable
        Dim sFromDate As String = "", sToDate As String = ""
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlBillNo.SelectedIndex > 0 Then
                dt = objPumpBilling.GetAccessCode(sSession.AccessCode)
                dtPumpName = objPumpBilling.LoadPumpDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPump.SelectedValue)
                dtPpBilldetails = objPumpBilling.LoadPumpBillDetails(sSession.AccessCode, sSession.AccessCodeID, ddlPump.SelectedValue, ddlBillNo.SelectedItem.Text, sSession.YearID)

                dtTripDetails = objPumpBilling.LoadTripDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlPump.SelectedValue, dtPpBilldetails.Rows(0).Item("sFromDate"), dtPpBilldetails.Rows(0).Item("sToDate"))
                Dim dAmount As Double = 0.0, dAdvance As Double = 0.0, dTotalAmt As Double = 0.0, dotherExp As Double = 0.0
                If dtTripDetails.Rows.Count > 0 Then
                    For j = 0 To dtTripDetails.Rows.Count - 1
                        dAmount = dAmount + Val(dtTripDetails.Rows(j).Item("Amount").ToString())
                        dAdvance = dAdvance + Val(dtTripDetails.Rows(j).Item("Advance").ToString())
                        dotherExp = dotherExp + Val(dtTripDetails.Rows(j).Item("OtherExpense").ToString())
                        dTotalAmt = dTotalAmt + Val(dtTripDetails.Rows(j).Item("TotalAmount").ToString())
                    Next
                End If
                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet1", dtPumpName)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                Dim rds1 As New ReportDataSource("DataSet2", dtTripDetails)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                Dim rds2 As New ReportDataSource("DataSet3", dtPpBilldetails)
                ReportViewer1.LocalReport.DataSources.Add(rds2)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Logistics/PumpBilling.rdlc")
                ReportViewer1.LocalReport.Refresh()

                Dim sCompName As ReportParameter() = {New ReportParameter("sCompName", dt.Rows(0).Item("Sad_CMS_Name").ToString())}
                ReportViewer1.LocalReport.SetParameters(sCompName)

                Dim dAmt As ReportParameter() = {New ReportParameter("dAmt", String.Format("{0:0.0}", Convert.ToDecimal(dAmount)))}
                ReportViewer1.LocalReport.SetParameters(dAmt)

                Dim dOtherExpAmountt As ReportParameter() = {New ReportParameter("dOtherExpAmountt", String.Format("{0:0.00}", Convert.ToDecimal(dotherExp)))}
                ReportViewer1.LocalReport.SetParameters(dOtherExpAmountt)

                Dim dAdvanceAmountt As ReportParameter() = {New ReportParameter("dAdvanceAmountt", String.Format("{0:0.00}", Convert.ToDecimal(dAdvance)))}
                ReportViewer1.LocalReport.SetParameters(dAdvanceAmountt)

                Dim dTotalAmount As ReportParameter() = {New ReportParameter("dTotalAmount", String.Format("{0:0.00}", Convert.ToDecimal(dTotalAmt)))}
                ReportViewer1.LocalReport.SetParameters(dTotalAmount)

                ReportViewer1.LocalReport.Refresh()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBillNo_SelectedIndexChanged")
        End Try
    End Sub
End Class
