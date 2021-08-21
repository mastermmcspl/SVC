Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Data.SqlClient
Imports System.Diagnostics

Partial Class Reports_Reports
    Inherits System.Web.UI.Page
    Private sFormName As String = "Reports/Reports.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objReports As New clsReports
    Private objclsModulePermission As New clsModulePermission
    Dim objSettings As New clsSettings
    Dim objCOA As New clsChartOfAccounts
    Private objAccSetting As New clsAccountSetting
    Dim drtFrmDate, drtTodate As String
    Dim drtFRMMonth As String = ""
    Dim drtTOmonth As String = ""
    Dim drtYear As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        'imgbtnReport.Src = "~/Images/Download24.png"
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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Dim iDefaultBranch As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                lblMesg.Text = ""
                Session("TrailBalanceActual") = Nothing
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "RPTS")
                'imgbtnReport.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        'imgbtnReport.Visible = True
                    End If
                End If
                btnFreeze.Attributes.Add("OnClick", "javascript:return Validate()")
                LoadFreeDDL()

                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)


                iDefaultBranch = objReports.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                'txtFromDate.Text = "" : txtToDate.Text = ""
                pnlGL.Visible = False : PnlPayment.Visible = False : pnlBankDaybook.Visible = False : PnlMonth.Visible = False
                pnlPurchase.Visible = False : pnlSales.Visible = False
                LoadReports()
                'LoadDuration
                loadDurationMonth()
                loadDurationWeek()
                DurationHalfYearly()
                DurationQuarterly()
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadGL(ByVal sType As String, ByVal sLedgerType As String)
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Try
            sPerm = objSettings.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, sType, sLedgerType)
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")

            ddlGL.DataSource = objCOA.LoadGL(sSession.AccessCode, sSession.AccessCodeID, sArray1(2))
            ddlGL.DataTextField = "Description"
            ddlGL.DataValueField = "gl_id"
            ddlGL.DataBind()
            ddlGL.Items.Insert(0, "Select General Ledger")
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadGL")
        End Try
    End Sub
    Private Sub LoadDebtorsOrCreditors()
        Try
            ddlGL.DataSource = objReports.LoadDebtorsOrCreditorsGL(sSession.AccessCode, sSession.AccessCodeID)
            ddlGL.DataTextField = "Description"
            ddlGL.DataValueField = "ID"
            ddlGL.DataBind()
            ddlGL.Items.Insert(0, "Select General Ledger")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadAllGls()
        Try
            ddlGL.DataSource = objReports.LoadAllGlList(sSession.AccessCode, sSession.AccessCodeID)
            ddlGL.DataTextField = "gl_desc"
            ddlGL.DataValueField = "gl_id"
            ddlGL.DataBind()
            ddlGL.Items.Insert(0, "Select General Ledger")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub loadDurationWeek()
        Try
            ddlDurationweek.Items.Add(New ListItem("Select Week", 0))
            ddlDurationweek.Items.Add(New ListItem("1st-7th", 1))
            ddlDurationweek.Items.Add(New ListItem("8th-14th", 2))
            ddlDurationweek.Items.Add(New ListItem("15th-21st", 3))
            ddlDurationweek.Items.Add(New ListItem("22nd-EOM", 4))
            ddlDurationweek.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub loadDurationMonth()
        Try
            ddlDurationmonth.Items.Add(New ListItem("Select Month", 0))
            ddlDurationmonth.Items.Add(New ListItem("January", 1))
            ddlDurationmonth.Items.Add(New ListItem("February", 2))
            ddlDurationmonth.Items.Add(New ListItem("March", 3))
            ddlDurationmonth.Items.Add(New ListItem("April", 4))
            ddlDurationmonth.Items.Add(New ListItem("May", 5))
            ddlDurationmonth.Items.Add(New ListItem("June", 6))
            ddlDurationmonth.Items.Add(New ListItem("July", 7))
            ddlDurationmonth.Items.Add(New ListItem("August", 8))
            ddlDurationmonth.Items.Add(New ListItem("September", 9))
            ddlDurationmonth.Items.Add(New ListItem("October", 10))
            ddlDurationmonth.Items.Add(New ListItem("November", 11))
            ddlDurationmonth.Items.Add(New ListItem("December", 12))
            ddlDurationmonth.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DurationQuarterly()
        Try
            ddlDurationQuarter.Items.Add(New ListItem("Select Month", 0))
            ddlDurationQuarter.Items.Add(New ListItem("Apr-Jun", 1))
            ddlDurationQuarter.Items.Add(New ListItem("Jul-Sep", 2))
            ddlDurationQuarter.Items.Add(New ListItem("Oct-Dec", 3))
            ddlDurationQuarter.Items.Add(New ListItem("Jan-Mar", 4))
            ddlDurationQuarter.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DurationHalfYearly()
        Try
            ddlDurationhalfyear.Items.Add(New ListItem("Select Month", 0))
            ddlDurationhalfyear.Items.Add(New ListItem("Apr-Sep", 1))
            ddlDurationhalfyear.Items.Add(New ListItem("Oct-Mar", 2))
            ddlDurationhalfyear.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadReports()
        Try
            rbtDuration.SelectedIndex = -1
            ddlReports.Items.Add(New ListItem("Select Types of Reports", 0))
            ddlReports.Items.Add(New ListItem("Transactions View", 1))
            ddlReports.Items.Add(New ListItem("Opening Balance", 2))
            ddlReports.Items.Add(New ListItem("Schedule SubGL Reports", 3))   'Primary General Ledger
            ddlReports.Items.Add(New ListItem("Balance Sheet", 4))
            ddlReports.Items.Add(New ListItem("P/L Report", 5))
            ddlReports.Items.Add(New ListItem("Schedule Reports", 6))
            ddlReports.Items.Add(New ListItem("Schedule Reports With Note", 7))
            ddlReports.Items.Add(New ListItem("Day Book", 8))
            ddlReports.Items.Add(New ListItem("Bank Book", 9))
            ddlReports.Items.Add(New ListItem("Cash Book", 10))
            ddlReports.Items.Add(New ListItem("Payment", 11))
            ddlReports.Items.Add(New ListItem("Receipt", 12))
            ddlReports.Items.Add(New ListItem("Petty Cash", 13))
            ddlReports.Items.Add(New ListItem("Journal Entry", 14))
            ddlReports.Items.Add(New ListItem("Trial Balance Without P/L", 15))
            ddlReports.Items.Add(New ListItem("Sub Ledger ", 16)) ' interchnaged Sub Ledger of Debtors/Creditors and sub leadger
            ddlReports.Items.Add(New ListItem("", 17)) 'Sub Ledger of Debtors/Creditors
            ddlReports.Items.Add(New ListItem("Purchase Register", 18))
            ddlReports.Items.Add(New ListItem("Sales Register", 19))
            ddlReports.Items.Add(New ListItem("Trail Balance", 20))
            ddlReports.Items.Add(New ListItem("Petty Cash DayBook", 21))
            ddlReports.Items.Add(New ListItem("GL Summary", 22))
            ddlReports.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlReports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReports.SelectedIndexChanged
        Dim sFrequency As String = "" : Dim iStatus As Integer
        Try
            lblError.Text = "" : lblMesg.Text = ""
            GvPettyCashDetailsReport.Visible = False
            PnlWeekly.Visible = False
            PnlDurationMonthly.Visible = False
            pnlyear.Visible = False : pnlQuarterly.Visible = False
            pnlHalfYearly.Visible = False
            ddlDurationweek.SelectedIndex = 0 : ddlDurationmonth.SelectedIndex = 0
            ddlDurationQuarter.SelectedIndex = 0 : ddlDurationhalfyear.SelectedIndex = 0
            lblError.Text = "" : lblMesg.Text = ""
            txtFromDate.Text = "" : txtToDate.Text = ""
            pnlGL.Visible = False : PnlPayment.Visible = False : pnlBankDaybook.Visible = False : PnlMonth.Visible = False
            ddlExistPurchase.Items.Clear() : pnlPurchase.Visible = False : pnlSales.Visible = False : ddlExistSales.Items.Clear()
            PnlFreeze.Visible = False
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()

            txtFromDate.Text = ""
            txtToDate.Text = ""
            ddlSubGL.Items.Clear()
            ddlGL.Items.Clear()
            rbtDuration.SelectedIndex = -1
            If ddlReports.SelectedIndex = 1 Then 'Transactions View
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                txtToDate.Text = ""
                'LoadTrialBalance()

            ElseIf ddlReports.SelectedIndex = 2 Then   'Opening Balance
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False
                'LoadOpeningBalance()

            ElseIf ddlReports.SelectedIndex = 3 Then   'Primary General LEdger
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False
                '  LoadGeneralLedgerReport()

            ElseIf ddlReports.SelectedIndex = 4 Then   'Balance Sheet
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False
                'LadBalanceSheet()

            ElseIf ddlReports.SelectedIndex = 5 Then   'PL reports

                'iStatus = objReports.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'If iStatus = 1 Then

                'ElseIf iStatus = 2 Then
                '    lblError.Text = "For Future year records are not there to display the report"
                '    Exit Sub
                'Else
                '    sFrequency = objReports.GetFrequency(sSession.AccessCode, sSession.AccessCodeID)
                '    If sFrequency = "Select" Then
                '        lblError.Text = "Set the frequency of report,in application configuration."
                '        Exit Sub
                '    ElseIf sFrequency = "Monthly" Then
                '        objReports.GetWriteGLTableMonth(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sFrequency)
                '    ElseIf sFrequency = "Quarterly" Then
                '        objReports.GetWriteGLTableQuarter(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sFrequency)
                '    ElseIf sFrequency = "Half Yearly" Then
                '        objReports.GetWriteGLTableHalfYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sFrequency)
                '    ElseIf sFrequency = "Yearly" Then
                '        objReports.GetWriteGLTableYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sFrequency)
                '    End If
                'End If
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False
                'LoadPLREPORTS()

            ElseIf ddlReports.SelectedIndex = 6 Then   'Schedule Report
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                'iStatus = objReports.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'If iStatus = 1 Then

                'ElseIf iStatus = 2 Then
                '    lblError.Text = "For Future year records are not there to display the report"
                '    Exit Sub
                'Else
                '    sFrequency = objReports.GetFrequency(sSession.AccessCode, sSession.AccessCodeID)
                '    If sFrequency = "Select" Then
                '        lblError.Text = "Set the frequency of report,in application configuration."
                '        Exit Sub
                '    ElseIf sFrequency = "Monthly" Then
                '        objReports.GetWriteGLTableMonth(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sFrequency)
                '    ElseIf sFrequency = "Quarterly" Then
                '        objReports.GetWriteGLTableQuarter(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sFrequency)
                '    ElseIf sFrequency = "Half Yearly" Then
                '        objReports.GetWriteGLTableHalfYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sFrequency)
                '    ElseIf sFrequency = "Yearly" Then
                '        objReports.GetWriteGLTableYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sFrequency)
                '    End If
                'End If

                'LoadScheduleReport()


            ElseIf ddlReports.SelectedIndex = 7 Then   'Schedule Report Notes
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                txtToDate.Text = ""
                btnOk.Visible = False

            ElseIf ddlReports.SelectedIndex = 8 Then   'DayBook
                ReportViewer1.Reset()
                PnlMonth.Visible = True
                ReportViewer1.LocalReport.Refresh()
                ddlCustomerParty.Visible = True
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                ddlParty.Items.Clear()
                BindMonth() : BinPartyOrCustomerORGL()
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False
                txtToDate.Text = ""

            ElseIf ddlReports.SelectedIndex = 9 Then 'Bank book
                ReportViewer1.Reset()
                PnlMonth.Visible = True
                ReportViewer1.LocalReport.Refresh()
                ddlMonth.Items.Clear()
                ddlCustomerParty.Visible = False ' : ddlCustomerParty.Items.Clear() Darshan
                ddlParty.Items.Clear()
                BindMonth() : LoadParty(4)
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False

            ElseIf ddlReports.SelectedIndex = 10 Then 'Cash book
                ReportViewer1.Reset()
                PnlMonth.Visible = True
                ReportViewer1.LocalReport.Refresh()
                ddlCustomerParty.Visible = True
                ddlParty.Items.Clear()
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinPartyOrCustomerORGL()
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False


            ElseIf ddlReports.SelectedIndex = 11 Then   'Payment
                PnlPayment.Visible = True : PnlMonth.Visible = True
                LoadPaymentBillNo()
                ddlCustomerParty.Visible = True
                ddlParty.Items.Clear()
                ddlRVoucherType.Visible = True
                lblvouchertype.Visible = True
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinPartyOrCustomerORGL()
                ddlRVoucherType.Items.Clear()
                loadPaymentVoucherType()
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False

            ElseIf ddlReports.SelectedIndex = 12 Then   'Receipt
                PnlPayment.Visible = True : PnlMonth.Visible = True
                LoadReceiptBillNo()
                ddlCustomerParty.Visible = True
                ddlParty.Items.Clear()
                ddlRVoucherType.Visible = True
                lblvouchertype.Visible = True
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinPartyOrCustomerORGL()
                ddlRVoucherType.Visible = True
                lblvouchertype.Visible = True
                loadReceiptVoucherType()
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False

            ElseIf ddlReports.SelectedIndex = 13 Then   'Petty Cash
                PnlPayment.Visible = True : PnlMonth.Visible = True
                ddlCustomerParty.Visible = True
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinPartyOrCustomerORGL()
                ddlParty.Items.Clear()
                LoadPettyCashBillNo()
                ddlRVoucherType.Visible = False
                lblvouchertype.Visible = False
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False

            ElseIf ddlReports.SelectedIndex = 14 Then   'Journal Entry
                PnlPayment.Visible = True : PnlMonth.Visible = True
                ddlCustomerParty.Visible = True
                ddlParty.Items.Clear()
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinPartyOrCustomerORGL()
                LoadJournalEntryBillNo()
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                ddlRVoucherType.Visible = False
                lblvouchertype.Visible = False
                btnOk.Visible = False

            ElseIf ddlReports.SelectedIndex = 16 Then   'Sub Ledger 
                ReportViewer1.Reset()
                ReportViewer1.LocalReport.Refresh()
                pnlGL.Visible = True
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                LoadAllGls()

            ElseIf ddlReports.SelectedIndex = 17 Then   'Sub Ledger of Debtors/Creditors 
                ReportViewer1.Reset()
                ReportViewer1.LocalReport.Refresh()

                pnlGL.Visible = True
                LoadDebtorsOrCreditors()
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False
                'ElseIf ddlReports.SelectedIndex = 18 Then   'Purchase Register
                '    PnlMonth.Visible = True
                '    LoadPurchaseRegister()
                '    ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                '    BindMonth() : BinSupplierCustomer("Supplier")

                'ElseIf ddlReports.SelectedIndex = 19 Then   'Sales Register
                '    PnlMonth.Visible = True
                '    LoadSalesRegister()
                '    ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                '    BindMonth() : BinSupplierCustomer("Customer")
            ElseIf ddlReports.SelectedIndex = 18 Then   'Purchase Register
                PnlMonth.Visible = True

                pnlPurchase.Visible = True : pnlSales.Visible = False
                LoadExistingPurchaseVoucher()
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinSupplierCustomer("Supplier")
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False

            ElseIf ddlReports.SelectedIndex = 19 Then   'Sales Register
                PnlMonth.Visible = True

                pnlPurchase.Visible = False : pnlSales.Visible = True
                LoadExistingSalesVoucher()
                ddlCustomerParty.Visible = True
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinSupplierCustomer("Customer")
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False

            ElseIf ddlReports.SelectedIndex = 20 Then   'Trail Balance
                ddlCustomerParty.Visible = True
                pnlfreeze.Visible = True
                Label1.Visible = True : ddlFRReport.Visible = True : btnFreeze.Visible = True
                Dim dToDate As String = ""
                dToDate = objReports.CheckFreezeDtae(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If dToDate <> "" Then
                    txtFromDate.Text = objGen.FormatDtForRDBMS(dToDate, "D")
                Else
                    txtFromDate.Text = objGen.FormatDtForRDBMS(objReports.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
                End If
            ElseIf ddlReports.SelectedIndex = 21 Then   'Peety Cash DayBook
                ReportViewer1.Reset()
                PnlMonth.Visible = True
                ReportViewer1.LocalReport.Refresh()
                'ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                'BindMonth()
                'BinPartyOrCustomerORGL()
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False : ddlMonth.Visible = False : ddlCustomerParty.Visible = False
                ddlParty.Visible = False
                txtToDate.Text = ""
            ElseIf ddlReports.SelectedIndex = 22 Then 'GL Summary
                ReportViewer1.Reset()
                PnlMonth.Visible = True
                ReportViewer1.LocalReport.Refresh()
                ddlCustomerParty.Visible = True
                ddlParty.Items.Clear()
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinPartyOrCustomerORGL()
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                btnOk.Visible = False
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReports_SelectedIndexChanged")
        End Try
    End Sub

    Public Sub WriteGLTableMonth()
        Dim iMonth As Integer
        Try
            iMonth = objReports.GetMonth(sSession.AccessCode, sSession.AccessCodeID)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingPurchaseVoucher()
        Try
            ddlExistPurchase.DataSource = objReports.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistPurchase.DataTextField = "Acc_Purchase_TransactionNo"
            ddlExistPurchase.DataValueField = "Acc_Purchase_ID"
            ddlExistPurchase.DataBind()
            ddlExistPurchase.Items.Insert(0, "Existing Purchase Voucher")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingSalesVoucher()
        Try
            ddlExistSales.DataSource = objReports.LoadExistingSalesVocher(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistSales.DataTextField = "Acc_Sales_TransactionNo"
            ddlExistSales.DataValueField = "Acc_Sales_ID"
            ddlExistSales.DataBind()
            ddlExistSales.Items.Insert(0, "Existing Sales Voucher")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadPettyCashBillNo()
        Try
            ddlPBillNo.DataSource = objReports.LoadPettyCashBillNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlPBillNo.DataTextField = "Acc_PCM_BillNo"
            ddlPBillNo.DataValueField = "Acc_PCM_ID"
            ddlPBillNo.DataBind()
            ddlPBillNo.Items.Insert(0, "Select Bill No")

            ddlPVoucherNo.DataSource = objReports.LoadPettyCashVoucherNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlPVoucherNo.DataTextField = "Acc_PCM_TransactionNo"
            ddlPVoucherNo.DataValueField = "Acc_PCM_ID"
            ddlPVoucherNo.DataBind()
            ddlPVoucherNo.Items.Insert(0, "Select Voucher No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadJournalEntryBillNo()
        Try
            ddlPBillNo.DataSource = objReports.LoadJEBillNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlPBillNo.DataTextField = "Acc_JE_BillNo"
            ddlPBillNo.DataValueField = "Acc_JE_ID"
            ddlPBillNo.DataBind()
            ddlPBillNo.Items.Insert(0, "Select Bill No")

            ddlPVoucherNo.DataSource = objReports.LoadJEVoucherNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlPVoucherNo.DataTextField = "Acc_JE_TransactionNo"
            ddlPVoucherNo.DataValueField = "Acc_JE_ID"
            ddlPVoucherNo.DataBind()
            ddlPVoucherNo.Items.Insert(0, "Select Voucher No")

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LoadPaymentBillNo()
        Try
            ddlPBillNo.DataSource = objReports.LoadPaymentBillNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlPBillNo.DataTextField = "Acc_PM_BillNo"
            ddlPBillNo.DataValueField = "Acc_PM_ID"
            ddlPBillNo.DataBind()
            ddlPBillNo.Items.Insert(0, "Select Bill No")

            ddlPVoucherNo.DataSource = objReports.LoadPaymentVoucherNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlPVoucherNo.DataTextField = "Acc_PM_TransactionNo"
            ddlPVoucherNo.DataValueField = "Acc_PM_ID"
            ddlPVoucherNo.DataBind()
            ddlPVoucherNo.Items.Insert(0, "Select Voucher No")

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadReceiptBillNo()
        Try
            ddlPBillNo.DataSource = objReports.LoadReceiptBillNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlPBillNo.DataTextField = "Acc_RM_BillNo"

            ddlPBillNo.DataValueField = "Acc_RM_ID"
            ddlPBillNo.DataBind()
            ddlPBillNo.Items.Insert(0, "Select Bill No")

            ddlPVoucherNo.DataSource = objReports.LoadReceiptVoucherNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlPVoucherNo.DataTextField = "Acc_RM_TransactionNo"
            ddlPVoucherNo.DataValueField = "Acc_RM_ID"
            ddlPVoucherNo.DataBind()
            ddlPVoucherNo.Items.Insert(0, "Select Voucher No")

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        lblError.Text = ""
        Try
            If ddlReports.SelectedIndex = 8 Then 'DayBook
                LoadDayBook()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()

            ElseIf ddlReports.SelectedIndex = 9 Then 'Bank Book
                LoadbankBook()
                ddlMonth.SelectedIndex = 0 : ddlParty.SelectedIndex = 0

            ElseIf ddlReports.SelectedIndex = 10 Then 'Cash Book
                LoadCashBook()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()

            ElseIf ddlReports.SelectedIndex = 11 Then 'Payment
                LoadPaymentDetails()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()

            ElseIf ddlReports.SelectedIndex = 12 Then 'Receipt
                LoadReceiptDetails()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()

            ElseIf ddlReports.SelectedIndex = 13 Then 'Petty Cash
                LoadPettyCashDetails()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()

            ElseIf ddlReports.SelectedIndex = 14 Then 'JE
                LoadJEDetails()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()
            End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOk_Click")
        End Try
    End Sub
    Private Sub LoadDayBook()
        Dim dt As New DataTable
        Dim dFromDate As String = ""
        Dim dToDate As String = ""
        Dim iCustPartyType As Integer

        Dim iMonthID As Integer : Dim iParty As Integer
        Dim dTotalDebit As Double : Dim dTotalCredit As Double
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0

        Try
            'Dim yForm = "yyyy/MM/dd"
            'Dim dFromDate = Format(CDate(txtFromDate.Text), yForm)
            'Dim dToDate = Format(CDate(txtToDate.Text), yForm)

            'dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            ReportViewer1.Reset()
            'dt = objReports.LoadDayBookReport(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dFromDate, dToDate)

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iParty = ddlParty.SelectedValue
            Else
                iParty = 0
            End If

            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If ddlCustomerParty.SelectedIndex > 0 Then
                iCustPartyType = ddlCustomerParty.SelectedValue
            End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If

            dt = objReports.LoadDayBookReport(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iParty, iCustPartyType, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptDayBook.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim TrialBalance As ReportParameter() = New ReportParameter() {New ReportParameter("TrialBalance", "Day Book Report")}
            ReportViewer1.LocalReport.SetParameters(TrialBalance)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dTotalDebit = dTotalDebit + dt.Rows(i)("ATD_Debit")
                    dTotalCredit = dTotalCredit + dt.Rows(i)("ATD_Credit")
                Next
            End If

            Dim dTotalD As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalDebit", dTotalDebit)}
            ReportViewer1.LocalReport.SetParameters(dTotalD)

            Dim dTotalC As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalCredit", dTotalCredit)}
            ReportViewer1.LocalReport.SetParameters(dTotalC)

            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDayBook")
        End Try
    End Sub
    Private Sub LoadPettyCashDayBook()
        Dim dt As New DataTable
        Dim dFromDate As String
        Dim dToDate As String
        Dim iCustPartyType As Integer

        Dim iMonthID As Integer : Dim iParty As Integer
        Dim dTotalDebit As Double : Dim dTotalCredit As Double
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0

        Try
            'Dim yForm = "yyyy/MM/dd"
            'Dim dFromDate = Format(CDate(txtFromDate.Text), yForm)
            'Dim dToDate = Format(CDate(txtToDate.Text), yForm)

            'dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            ReportViewer1.Reset()
            'dt = objReports.LoadDayBookReport(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dFromDate, dToDate)

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            'If ddlParty.SelectedIndex > 0 Then
            '    iParty = ddlParty.SelectedValue
            'Else
            '    iParty = 0
            'End If

            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            'If ddlCustomerParty.SelectedIndex > 0 Then
            '    iCustPartyType = ddlCustomerParty.SelectedValue
            'End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            GvPettyCashDetailsReport.DataSource = ""

            dt = objReports.LoadPettyCashDayBook(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iParty, iCustPartyType, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            GvPettyCashDetailsReport.DataSource = dt
            GvPettyCashDetailsReport.DataBind()
            'Dim iVal As Integer = 0
            Dim Pnumber As Integer
            Dim drow As DataRow
            dt.NewRow()
            drow = dt.NewRow()
            dt.Rows.Add(drow)
            For i = 0 To dt.Columns.Count - 1
                If i = 1 Or i = 2 Or i = 3 = True Then
                    i = 4
                End If
                For j = 0 To dt.Rows.Count - 1
                    Dim Number As Integer = 0
                    If IsDBNull(dt.Rows(j)(i)).ToString() = True Then
                        dt.Rows(j)(i) = 0
                    End If
                    Number = Val(dt.Rows(j)(i))
                    If Val(Number) = 0 Then
                        Number = Pnumber + 0
                    Else
                        Pnumber = Number + Pnumber
                    End If
                Next
                drow(i) = Pnumber
                'GvPettyCashDetailsReport.FooterRow.Cells(i + 1).Text = Pnumber
                'GvPettyCashDetailsReport.FooterRow.DataBind()
                Pnumber = 0
            Next
            GvPettyCashDetailsReport.DataSource = dt
            GvPettyCashDetailsReport.DataBind()

            'Dim LBLTOTAL As New Label
            'Dim i As Integer
            'GvPettyCashDetailsReport.DataBind()
            'Dim dDebitTotal, dCreditTotal As Double
            'If dt.Rows.Count > 0 Then
            '    For i = 0 To dt.Rows.Count - 1

            '        If dt.Rows(i)("CashReceived") = "" Then
            '            dDebitTotal = 0
            '        Else
            '            dDebitTotal = dDebitTotal + dt.Rows(i)("CashReceived")
            '        End If
            '        GvPettyCashDetailsReport.FooterRow.Cells(i).Text = dDebitTotal
            '    Next
            'End If
            'GvPettyCashDetailsReport.FooterRow.Cells(1).Text = dDebitTotal
            'GvPettyCashDetailsReport.FooterRow.Cells(2).Text = 500
            'GvPettyCashDetailsReport.FooterRow.DataBind()
            ' GvPettyCashDetailsReport.FooterRow.(dDebitTotal)

            'Dim Cnt As Decimal
            'For i = 0 To GvPettyCashDetailsReport.Rows.Count - 1
            '    For j = 5 To GvPettyCashDetailsReport.Rows.Count - 1
            '        If GvPettyCashDetailsReport.Rows(i).Cells(1).ToString = "" Then
            '            Cnt += 0
            '        Else
            '            Cnt += GvPettyCashDetailsReport.Rows(i).Cells(1).ToString
            '        End If

            '        'Cnt += Convert.ToDouble(GvPettyCashDetailsReport.Rows(i).Cells())
            '        'Cnt = GvPettyCashDetailsReport.Rows(i).Cells(j).ToString
            '        'If dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)(i)) = "" Then
            '        '    Cnt = 0
            '        'Else
            '        '    Cnt = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)(i))

            '        'End If
            '    Next
            'Next
            'For j = 1 To GvPettyCashDetailsReport.Rows.Count - 1
            '    Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("TotalPayment"))
            '    GvPettyCashDetailsReport.FooterRow.Cells(1).Text = "Total"
            '    GvPettyCashDetailsReport.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Right
            '    GvPettyCashDetailsReport.FooterRow.Cells(2).Text = total.ToString("N2")
            'Next
            '  GvPettyCashDetailsReport.FooterRow.Cells(j).DataBind()


            'Dim iAmount As String = ""
            'Dim dAmount As String = ""
            'Dim drow As DataRow
            'For j = 0 To GvPettyCashDetailsReport.Columns.Count - 1

            '    For i = 0 To GvPettyCashDetailsReport.Rows.Count - 1

            '        iAmount = Val(GvPettyCashDetailsReport.Rows(i).Cells(j)) + iAmount
            '    Next

            'Next



            'dt.Columns.Add("TotalPaymentValue")
            'For i = 0 To GvPettyCashDetailsReport.Rows.Count - 1

            '    If IsDBNull(dt.Rows(0)("TotalPayment").ToString()) = False Then
            '        dAmount = dt.Rows(0)("TotalPayment").ToString() + dAmount
            '    Else
            '        dAmount = dAmount + 0
            '    End If
            '    drow("TotalPaymentValue") = Val(dAmount)
            'Next

            'drow = dt.NewRow()
            'dt.NewRow()
            'dt.Rows.Add(dRow)
            'GvPettyCashDetailsReport.DataSource = dt



            'dt.Rows().Cells(4).Value += Row.Cells(4).Value
            'DataGridView1.Rows(max).Cells(3).Value = total

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPettyCashDayBook")
        End Try
    End Sub

    Private Sub LoadSubLedgersDbCr()
        Dim dt As New DataTable
        Dim dtSearch As New DataTable
        Dim iParty As Integer = 0
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try
            If (ddlGL.SelectedIndex <> 0 And ddlSubGL.SelectedIndex <> 0) Then
                ReportViewer1.Reset()

                If ddlSubGL.SelectedIndex > 0 Then
                    iParty = ddlSubGL.SelectedValue
                Else
                    iParty = 0
                End If

                If ddlAccZone.SelectedIndex > 0 Then
                    iZoneID = ddlAccZone.SelectedValue
                End If
                If ddlAccRgn.SelectedIndex > 0 Then
                    iRegionID = ddlAccRgn.SelectedValue
                End If
                If ddlAccArea.SelectedIndex > 0 Then
                    iAreaID = ddlAccArea.SelectedValue
                End If
                If ddlAccBrnch.SelectedIndex > 0 Then
                    iBranchID = ddlAccBrnch.SelectedValue
                End If

                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If

                dt = objReports.LoadDebtorsOrCreditors(sSession.AccessCode, sSession.AccessCodeID, iParty, ddlGL.SelectedValue, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate, sSession.YearID)

                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptSubGLDebitors.rdlc")

                Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
                ReportViewer1.LocalReport.SetParameters(Vocher)

                Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "SUB GENERAL LEDGER FOR THE YEAR OF " & sSession.YearName & "")}
                ReportViewer1.LocalReport.SetParameters(GeneralLedger)
                ReportViewer1.LocalReport.Refresh()
            Else
                lblError.Text = "Select GL and SubGl"
                Exit Sub
            End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSubLedgersDbCr")
        End Try
    End Sub

    Private Sub LoadScheduleReportNotes()
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try
            ReportViewer1.Reset()
            Dim dt As New DataTable
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If

            dt = objReports.LoadScheduleReportsNotes(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptScheduleNotes.rdlc")

            Dim BalanceSheet As ReportParameter() = New ReportParameter() {New ReportParameter("BalanceSheet", "NOTES TO FINANCIAL STATEMENT FOR THE YEAR OF " & sSession.YearName)}
            ReportViewer1.LocalReport.SetParameters(BalanceSheet)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadScheduleReportNotes")
        End Try
    End Sub

    Private Sub LoadScheduleReport()
        Dim dt As New DataTable
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try
            ReportViewer1.Reset()
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            dt = objReports.LoadScheduleReport(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptSchedule.rdlc")


            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim BalanceSheet As ReportParameter() = New ReportParameter() {New ReportParameter("BalanceSheet", "SCHEDULE REPORT FOR THE YEAR  " & sSession.YearName)}
            ReportViewer1.LocalReport.SetParameters(BalanceSheet)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadScheduleReport")
        End Try
    End Sub

    Private Sub LoadPettyCashDetails()
        Dim dt As New DataTable, dtTotal As New DataTable
        Dim i As Integer = 0, iVoucher As Integer = 0
        Dim cr As Double = 0, db As Double = 0
        Dim dPBillFromDate As String = "", dPBillToDate As String = ""
        Dim sBillNo As String = ""

        Dim iMonthID As Integer : Dim iParty As Integer
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Try
            ReportViewer1.Reset()


            'If txtFromDate.Text <> "" Then
            '    dPBillFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dPBillFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dPBillToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dPBillToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dPBillFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dPBillFromDate = "01/01/1900"
                End If
            Else
                dPBillFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dPBillToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dPBillToDate = "01/01/1900"
                End If
            Else
                dPBillToDate = drtTOmonth
            End If

            If ddlPVoucherNo.SelectedIndex > 0 Then
                iVoucher = ddlPVoucherNo.SelectedValue
            End If

            If ddlPBillNo.SelectedIndex > 0 Then
                sBillNo = ddlPBillNo.SelectedItem.Text
            End If

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iParty = ddlParty.SelectedValue
            Else
                iParty = 0
            End If

            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            dt = objReports.LoadPettyCashReportsDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo, iVoucher, iMonthID, iParty, iZoneID, iRegionID, iAreaID, iBranchID, dPBillFromDate, dPBillToDate)
            dtTotal = objReports.LoadPettyCashReportsDetailsTotal(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo, iVoucher, iMonthID, iParty, iZoneID, iRegionID, iAreaID, iBranchID, dPBillFromDate, dPBillToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            Dim rds1 As New ReportDataSource("DataSet2", dtTotal)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptPettyCash.rdlc")
            ReportViewer1.LocalReport.Refresh()

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "PETTY CASH TRANSACTIONS FOR THE YEAR OF " & sSession.YearName & "")}
            ReportViewer1.LocalReport.SetParameters(GeneralLedger)

            Dim dDebitTotal, dCreditTotal As Double
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dDebitTotal = dDebitTotal + dt.Rows(i)("ATD_Debit")
                    dCreditTotal = dCreditTotal + dt.Rows(i)("ATD_Credit")
                Next
            End If

            Dim TotalDebit As ReportParameter() = New ReportParameter() {New ReportParameter("TotalDebit", dDebitTotal)}
            ReportViewer1.LocalReport.SetParameters(TotalDebit)

            Dim TotalCredit As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCredit", dCreditTotal)}
            ReportViewer1.LocalReport.SetParameters(TotalCredit)

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPettyCashDetails")
        End Try
    End Sub

    Private Sub LoadReceiptDetails()
        Dim dt As New DataTable, dttotal As New DataTable
        Dim iVoucher As Integer = 0
        Dim iReceiptVoucherType As Integer = 0
        Dim dPBillFromDate As String = "", dPBillToDate As String = ""
        Dim sBillNo As String = ""

        Dim dDebitTotal As Double : Dim dCreditTotal As Double
        Dim iMonthID As Integer : Dim iPartyID As Integer
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Try
            ReportViewer1.Reset()

            'If txtPBillFromDate.Text <> "" Then
            '    dPBillFromDate = Format(CDate(txtPBillFromDate.Text), "dd/MM/yyyy")
            '    dPBillToDate = Format(CDate(txtPBillToDate.Text), "dd/MM/yyyy")
            'Else
            '    dPBillFromDate = "01/01/1900"
            '    dPBillToDate = "01/01/1900"
            'End If

            'If txtPBillFromDate.Text <> "" Then
            '    dPBillFromDate = Date.ParseExact(txtPBillFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            '    dPBillToDate = Date.ParseExact(txtPBillToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'Else
            'If txtFromDate.Text <> "" Then
            '    dPBillFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dPBillFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dPBillToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dPBillToDate = "01/01/1900"
            'End If
            'dPBillFromDate = "01/01/1900"
            'dPBillToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dPBillFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dPBillFromDate = "01/01/1900"
                End If
            Else
                dPBillFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dPBillToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dPBillToDate = "01/01/1900"
                End If
            Else
                dPBillToDate = drtTOmonth
            End If

            If ddlPVoucherNo.SelectedIndex > 0 Then
                iVoucher = ddlPVoucherNo.SelectedValue
            End If

            If ddlPBillNo.SelectedIndex > 0 Then
                sBillNo = ddlPBillNo.SelectedItem.Text
            End If

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iPartyID = ddlParty.SelectedValue
            Else
                iPartyID = 0
            End If
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            If ddlRVoucherType.SelectedIndex > 0 Then
                iReceiptVoucherType = ddlRVoucherType.SelectedValue
            End If

            dt = objReports.LoadReceiptReportsDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo, iVoucher, dPBillFromDate, dPBillToDate, iMonthID, iPartyID, iZoneID, iRegionID, iAreaID, iBranchID, iReceiptVoucherType)
            dttotal = objReports.LoadReceiptReportsDetailsTotal(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo, iVoucher, dPBillFromDate, dPBillToDate, iMonthID, iPartyID, iZoneID, iRegionID, iAreaID, iBranchID, iReceiptVoucherType)
            If (dt.Rows.Count > 0) Then
                Dim rds As New ReportDataSource("DataSet1", dt)
                Dim rds1 As New ReportDataSource("DataSet2", dttotal)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptReceipt.rdlc")
                ReportViewer1.LocalReport.Refresh()

                Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
                ReportViewer1.LocalReport.SetParameters(Vocher)

                Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "RECEIPT TRANSACTIONS FOR THE YEAR OF " & sSession.YearName & "")}
                ReportViewer1.LocalReport.SetParameters(GeneralLedger)

                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dDebitTotal = dDebitTotal + dt.Rows(i)("ATD_Debit")
                        dCreditTotal = dCreditTotal + dt.Rows(i)("ATD_Credit")
                    Next
                End If

                Dim TotalDebit As ReportParameter() = New ReportParameter() {New ReportParameter("TotalDebit", dDebitTotal)}
                ReportViewer1.LocalReport.SetParameters(TotalDebit)

                Dim TotalCredit As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCredit", dCreditTotal)}
                ReportViewer1.LocalReport.SetParameters(TotalCredit)
            Else
                lblError.Text = "No Data Found"
            End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadReceiptDetails")
        End Try
    End Sub

    Private Sub LoadPLREPORTS()
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try
            ReportViewer1.Reset()
            Dim dt As New DataTable
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            Dim sDate As String
            'dFromDate = objReports.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'dToDate = objReports.GetCurrentDate(sSession.AccessCode, sSession.AccessCodeID)

            dt = objReports.LoadPLReports(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptPLReportNote.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim BalanceSheet As ReportParameter() = New ReportParameter() {New ReportParameter("BalanceSheet", "PROFIT AND LOSS REPORTS FOR THE YEAR OF " & sSession.YearName)}
            ReportViewer1.LocalReport.SetParameters(BalanceSheet)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPLREPORTS")
        End Try
    End Sub

    Private Sub LadBalanceSheet()
        Dim dt As New DataTable
        Dim sToYear As String = ""
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try
            ReportViewer1.Reset()
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            Dim sDate As String
            ' dFromDate = objReports.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'dToDate = objReports.GetCurrentDate(sSession.AccessCode, sSession.AccessCodeID)

            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            dt = objReports.LoadBalanceSheet(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Session("BalanceSheet") = dt
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptBalanceSheet.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            sToYear = objReports.GetYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            Dim BalanceSheet As ReportParameter() = New ReportParameter() {New ReportParameter("BalanceSheet", "BALANCE SHEET AS ON  31/03/" & sToYear)}
            ReportViewer1.LocalReport.SetParameters(BalanceSheet)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LadBalanceSheet")
        End Try
    End Sub
    Private Sub LoadGeneralLedgerReport()
        Dim dt As New DataTable
        Dim dtSearch As New DataTable
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Try
            ReportViewer1.Reset()
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            dt = objReports.GeneralLedgerWithDetail(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID)

            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptGeneralLedgerDetails.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "GENERAL LEDGER FOR THE YEAR OF " & sSession.YearName & "")}
            ReportViewer1.LocalReport.SetParameters(GeneralLedger)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadGeneralLedgerReport")
        End Try
    End Sub
    Private Sub LoadSubLeadger()
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try
            ReportViewer1.Reset()
            Dim dt As New DataTable
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            dt = objReports.LoadSubLeadger(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptSchedule.rdlc")


            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim BalanceSheet As ReportParameter() = New ReportParameter() {New ReportParameter("BalanceSheet", "SCHEDULE REPORT FOR THE YEAR  " & sSession.YearName)}
            ReportViewer1.LocalReport.SetParameters(BalanceSheet)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadScheduleReportNotes")
        End Try
    End Sub

    Private Sub LoadOpeningBalance()
        Dim dt As New DataTable
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try
            ReportViewer1.Reset()
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            'dFromDate = objReports.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'dToDate = objReports.GetCurrentDate(sSession.AccessCode, sSession.AccessCodeID)

            dt = objReports.GetOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)

            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptOpeningBalance.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "OPENING BALANCE FOR THE YEAR OF " & sSession.YearName & "")}
            ReportViewer1.LocalReport.SetParameters(GeneralLedger)
            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOpeningBalance")
        End Try
    End Sub

    Private Sub LoadTrialBalance()
        Dim dt As New DataTable, dttotal As DataTable
        Dim sToYear As String = ""
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Dim dDebitTotal As Double : Dim dCreditTotal As Double
        Try
            ReportViewer1.Reset()
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            'If txtFromDate.Text <> "" Then
            'dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'End If
            'If txtToDate.Text <> "" Then
            'dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If


            dt = objReports.LoadTrialBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            dttotal = objReports.LoadGlSummary(sSession.AccessCode)
            If dttotal.Rows.Count > 0 Then
                For i = 0 To dttotal.Rows.Count - 1
                    dDebitTotal = dDebitTotal + dttotal.Rows(i)("Debit")
                    dCreditTotal = dCreditTotal + dttotal.Rows(i)("Credit")
                Next
            End If

            Dim dtGrandtotal As New DataTable
            dtGrandtotal.Columns.Add("GrandDebit")
            dtGrandtotal.Columns.Add("GrandCredit")
            Dim dr As DataRow
            dr = dtGrandtotal.NewRow()
            dr("GrandDebit") = dDebitTotal
            dr("GrandCredit") = dCreditTotal
            dtGrandtotal.Rows.Add(dr)

            Dim rds As New ReportDataSource("DataSet1", dt)
            Dim rds1 As New ReportDataSource("DataSet2", dttotal)
            Dim rds2 As New ReportDataSource("DataSet3", dtGrandtotal)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptTrialBalance.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            sToYear = objReports.GetYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            Dim TrialBalance As ReportParameter() = New ReportParameter() {New ReportParameter("TrialBalance", "General Ledger Report AS ON  31/03/" & sToYear)}

            ReportViewer1.LocalReport.SetParameters(TrialBalance)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadTrialBalance")
        End Try
    End Sub

    Private Sub LoadTrialBalanceActual()
        Dim dt As New DataTable
        Dim sToYear As String = ""
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        'Dim dFromDate As DateTime, dToDate As DateTime
        Try
            ReportViewer1.Reset()
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            'If txtFromDate.Text <> "" Then
            '    'dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'End If
            'If txtToDate.Text <> "" Then
            '    'dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                    If ddlReports.SelectedIndex = 20 Then
                        dFromDate = objGen.FormatDtForRDBMS(txtFromDate.Text, "T")
                        'dfDate = objGen.FormatDtForRDBMS(dFromDate, "Q")
                    End If
                Else
                    dFromDate = "01/01/1900"
                End If

            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            dt = objReports.LoadTrialBalanceActual(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Session("TrailBalanceActual") = dt
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RPTTBalance.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            sToYear = objReports.GetYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            Dim TrialBalance As ReportParameter() = New ReportParameter() {New ReportParameter("TrialBalance", "TRIAL BALANCE AS ON  31/03/" & sToYear)}

            ReportViewer1.LocalReport.SetParameters(TrialBalance)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadTrialBalance")
        End Try
    End Sub

    Private Sub ddlGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGL.SelectedIndexChanged
        Dim objParty As New clsPartyMasters
        Try
            If ddlReports.SelectedIndex = 11 Then
                ddlSubGL.DataSource = objParty.LoadExistingCustomer(sSession.AccessCode, sSession.AccessCodeID, 1)
                ddlSubGL.DataTextField = "Name"
                ddlSubGL.DataValueField = "ACM_ID"
                ddlSubGL.DataBind()
                ddlSubGL.Items.Insert(0, "Select Sub General Ledger")
            ElseIf ddlReports.SelectedIndex = 12 Then
                ddlSubGL.DataSource = objParty.LoadExistingCustomer(sSession.AccessCode, sSession.AccessCodeID, 0)
                ddlSubGL.DataTextField = "Name"
                ddlSubGL.DataValueField = "ACM_ID"
                ddlSubGL.DataBind()
                ddlSubGL.Items.Insert(0, "Select Sub General Ledger")
            ElseIf ddlReports.SelectedIndex = 16 Then
                ' ddlSubGL.DataSource = objCOA.LoadAllSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)
                ddlSubGL.DataSource = objReports.LoadAllSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)
                ddlSubGL.DataTextField = "Description"
                ddlSubGL.DataValueField = "gl_id"
                ddlSubGL.DataBind()
                ddlSubGL.Items.Insert(0, "Select Sub General Ledger")

            ElseIf ddlReports.SelectedIndex = 17 Then
                If (ddlGL.SelectedIndex > 0) Then
                    ddlSubGL.DataSource = objCOA.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue, ddlGL.SelectedIndex)
                    ddlSubGL.DataTextField = "Description"
                    ddlSubGL.DataValueField = "gl_id"
                    ddlSubGL.DataBind()
                    ddlSubGL.Items.Insert(0, "Select Sub General Ledger")

                Else
                    ReportViewer1.Reset()
                    ddlSubGL.Items.Clear()
                End If
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub LoadSubLedgersWiseDbCr()
        Dim dt As New DataTable
        Dim dtSearch As New DataTable
        Dim iParty As Integer = 0, iGl As Integer = 0
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try
            ReportViewer1.Reset()
            If ddlSubGL.SelectedIndex > 0 Then
                iParty = ddlSubGL.SelectedValue
            Else
                iParty = 0
            End If
            If ddlGL.SelectedIndex > 0 Then
                iGl = ddlGL.SelectedValue
            Else
                iGl = 0
            End If
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            If (ddlGL.SelectedIndex <> 0) Then
                dt = objReports.SubLedgerWithDetail(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iParty, iGl, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            ElseIf ddlSubGL.SelectedIndex <> 0 Then
                dt = objReports.PartySubledgerLedgerWithDetail(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iParty, iGl, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Purchase/RptLedgerBillWiseDetails.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "SUB GENERAL LEDGER FOR THE YEAR OF " & sSession.YearName & "")}
            ReportViewer1.LocalReport.SetParameters(GeneralLedger)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSubLedgersWiseDbCr")
        End Try
    End Sub
    Private Sub ddlSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubGL.SelectedIndexChanged
        Try
            If ddlReports.SelectedIndex = 11 Then
                'LoadPaymentDetails()
            ElseIf ddlReports.SelectedIndex = 12 Then
                'LoadReceiptDetails()
            ElseIf ddlReports.SelectedIndex = 16 Then
                ' LoadSubLedgersDbCr()
            ElseIf (ddlReports.SelectedIndex = 17) Then
                'LoadPartWisesDbCr()
            End If
            PnlFreeze.Visible = False
            PnlWeekly.Visible = False
            PnlDurationMonthly.Visible = False
            pnlyear.Visible = False : pnlQuarterly.Visible = False
            pnlBankDaybook.Visible = False : pnlHalfYearly.Visible = False
            rbtDuration.SelectedIndex = -1
            ReportViewer1.Reset()
            btnGo_Click(sender, e)
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub LoadPartWisesDbCr()
        Dim dt As New DataTable
        Dim dtSearch As New DataTable
        Dim iParty As Integer = 0, iGl As Integer = 0
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""

        Dim obDTotal As Double : Dim obCTotal As Double : Dim trDTotal As Double : Dim trCTotal As Double : Dim cbDTotal As Double : Dim cbCTotal As Double
        Try
            ReportViewer1.Reset()
            If ddlSubGL.SelectedIndex > 0 Then
                iParty = ddlSubGL.SelectedValue
            Else
                iParty = 0
            End If
            If ddlGL.SelectedIndex > 0 Then
                iGl = ddlGL.SelectedValue
            Else
                iGl = 0
            End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            dt = objReports.PartySubledgerLedgerWithDetail(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iParty, iGl, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Purchase/RptLedgerBillWiseDetails.rdlc")
            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)
            Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "SUB GENERAL LEDGER FOR THE YEAR OF " & sSession.YearName & "")}
            ReportViewer1.LocalReport.SetParameters(GeneralLedger)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If dt.Rows(i)("TransDebit").ToString() IsNot "" Then
                        trDTotal = trDTotal + dt.Rows(i)("TransDebit")
                    End If
                    If dt.Rows(i)("TransCredit").ToString() IsNot "" Then
                        trCTotal = trCTotal + dt.Rows(i)("TransCredit")
                    End If
                    If dt.Rows(i)("OpDebit").ToString() IsNot "" Then
                        obDTotal = obDTotal + dt.Rows(i)("OpDebit")
                    End If
                    If dt.Rows(i)("OpCredit").ToString() IsNot "" Then
                        obCTotal = obCTotal + dt.Rows(i)("OpCredit")
                    End If
                    If dt.Rows(i)("ClosingDebit").ToString() IsNot "" Then
                        cbDTotal = cbDTotal + dt.Rows(i)("ClosingDebit")
                    End If
                    If dt.Rows(i)("ClosingCredit").ToString() IsNot "" Then
                        cbCTotal = cbCTotal + dt.Rows(i)("ClosingCredit")
                    End If
                Next
            End If

            'Dim trTotalD As ReportParameter() = New ReportParameter() {New ReportParameter("trDTotal", trDTotal)}
            'ReportViewer1.LocalReport.SetParameters(trTotalD)

            'Dim trTotalC As ReportParameter() = New ReportParameter() {New ReportParameter("trCTotal", trCTotal)}
            'ReportViewer1.LocalReport.SetParameters(trTotalC)

            'Dim obTotalD As ReportParameter() = New ReportParameter() {New ReportParameter("obDTotal", obDTotal)}
            'ReportViewer1.LocalReport.SetParameters(obTotalD)

            'Dim obTotalC As ReportParameter() = New ReportParameter() {New ReportParameter("obCTotal", obCTotal)}
            'ReportViewer1.LocalReport.SetParameters(obTotalC)

            'Dim cbTotalD As ReportParameter() = New ReportParameter() {New ReportParameter("cbDTotal", cbDTotal)}
            'ReportViewer1.LocalReport.SetParameters(cbTotalD)

            'Dim cbTotalC As ReportParameter() = New ReportParameter() {New ReportParameter("cbCTotal", cbCTotal)}
            'ReportViewer1.LocalReport.SetParameters(cbTotalC)

            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPartWisesDbCr")
        End Try
    End Sub

    Private Sub LoadbankBook()
        Dim sToYear As String = ""
        Dim dt As New DataTable
        Dim dFromDate As String = "", dToDate As String = ""
        Dim iMonthID As Integer : Dim iParty As Integer
        Dim dTotalDebit As Double : Dim dTotalCredit As Double
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Try
            ReportViewer1.Reset()

            'Dim yForm = "yyyy/MM/dd"
            'Dim dFromDate = Format(CDate(txtFromDate.Text), yForm)
            'Dim dToDate = Format(CDate(txtToDate.Text), yForm)

            'dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iParty = ddlParty.SelectedValue
            Else
                iParty = 0
            End If
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            dt = objReports.LoadBankBookReport(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iParty, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptBankBook.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)
            Dim BankBook As ReportParameter() = New ReportParameter() {New ReportParameter("BankBook", "Bank Book Report")}

            ReportViewer1.LocalReport.SetParameters(BankBook)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dTotalDebit = dTotalDebit + dt.Rows(i)("ATD_Debit")
                    dTotalCredit = dTotalCredit + dt.Rows(i)("ATD_Credit")
                Next
            End If

            Dim dTotalD As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalDebit", dTotalDebit)}
            ReportViewer1.LocalReport.SetParameters(dTotalD)

            Dim dTotalC As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalCredit", dTotalCredit)}
            ReportViewer1.LocalReport.SetParameters(dTotalC)

            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadbankBook")
        End Try
    End Sub
    'Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
    '    Try
    '        If ddlReports.SelectedIndex = 11 Then
    '            LoadPaymentDetails()
    '        ElseIf ddlReports.SelectedIndex = 12 Then
    '            LoadReceiptDetails()
    '        ElseIf ddlReports.SelectedIndex = 13 Then   'Petty Cash
    '            LoadPettyCashDetails()
    '        ElseIf ddlReports.SelectedIndex = 14 Then   'Journal entry
    '            LoadJEDetails()
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Private Sub LoadJEDetails()
        Dim dt As New DataTable, dttotal As New DataTable
        Dim i As Integer = 0, iVoucher As Integer = 0
        Dim cr As Double = 0, db As Double = 0
        Dim dPBillFromDate As String = "", dPBillToDate As String = ""
        Dim sBillNo As String = ""
        Dim iMonthID As Integer : Dim iParty As Integer
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim iCustPartyType As Integer
        Try
            ReportViewer1.Reset()

            'If txtPBillFromDate.Text <> "" Then
            '    dPBillFromDate = Format(CDate(txtPBillFromDate.Text), "dd/MM/yyyy")
            '    dPBillToDate = Format(CDate(txtPBillToDate.Text), "dd/MM/yyyy")
            'Else
            ' dPBillFromDate = "01/01/1900"
            ' dPBillToDate = "01/01/1900"
            'End If
            'If txtFromDate.Text <> "" Then
            '    dPBillFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dPBillFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dPBillToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dPBillToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dPBillFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dPBillFromDate = "01/01/1900"
                End If
            Else
                dPBillFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dPBillToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dPBillToDate = "01/01/1900"
                End If
            Else
                dPBillToDate = drtTOmonth
            End If

            If ddlPVoucherNo.SelectedIndex > 0 Then
                iVoucher = ddlPVoucherNo.SelectedValue
            End If

            If ddlPBillNo.SelectedIndex > 0 Then
                sBillNo = ddlPBillNo.SelectedItem.Text
            End If

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iParty = ddlParty.SelectedValue
            Else
                iParty = 0
            End If

            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If ddlCustomerParty.SelectedIndex > 0 Then
                iCustPartyType = ddlCustomerParty.SelectedValue
            End If
            dt = objReports.LoadJEReportsDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo, iVoucher, dPBillFromDate, dPBillToDate, iMonthID, iParty, iZoneID, iRegionID, iAreaID, iBranchID, iCustPartyType)
            dttotal = objReports.LoadJEReportsDetailsTotal(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo, iVoucher, dPBillFromDate, dPBillToDate, iMonthID, iParty, iZoneID, iRegionID, iAreaID, iBranchID, iCustPartyType)
            Dim rds As New ReportDataSource("DataSet1", dt)
            Dim rds1 As New ReportDataSource("DataSet2", dttotal)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptJournalEntry.rdlc")
            ReportViewer1.LocalReport.Refresh()

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "JOURNAL ENTRY FOR THE YEAR OF " & sSession.YearName & "")}
            ReportViewer1.LocalReport.SetParameters(GeneralLedger)

            Dim dDebitTotal, dCreditTotal As Double
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If IsDBNull(dt.Rows(i)("Acc_JE_TransactionNo").ToString()) = False Then
                        If dt.Rows(i)("Acc_JE_TransactionNo").ToString() <> "" Then
                            dDebitTotal = dDebitTotal + dt.Rows(i)("ATD_Debit")
                            dCreditTotal = dCreditTotal + dt.Rows(i)("ATD_Credit")
                        End If
                    End If
                Next
            End If

            Dim TotalDebit As ReportParameter() = New ReportParameter() {New ReportParameter("TotalDebit", dDebitTotal)}
            ReportViewer1.LocalReport.SetParameters(TotalDebit)

            Dim TotalCredit As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCredit", dCreditTotal)}
            ReportViewer1.LocalReport.SetParameters(TotalCredit)


        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadJEDetails")
        End Try
    End Sub

    Private Sub LoadPurchaseRegister()
        Dim iMonthID As Integer : Dim iPartyID As Integer
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iPartyID = ddlParty.SelectedValue
            Else
                iPartyID = 0
            End If

            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            ReportViewer1.Reset()
            Dim dt As New DataTable
            dt = objReports.LoadPurchaseRegister(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iPartyID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/PurchaseRegister.rdlc")
            ReportViewer1.LocalReport.Refresh()

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "Purchase Register For The Year " & sSession.YearName & " ")}
            ReportViewer1.LocalReport.SetParameters(GeneralLedger)

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPurchaseRegister")
        End Try
    End Sub

    Private Sub LoadSalesRegister()
        Dim iMonthID As Integer : Dim iPartyID As Integer
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate As String = "", dToDate As String = ""
        Try

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iPartyID = ddlParty.SelectedValue
            Else
                iPartyID = 0
            End If


            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            ReportViewer1.Reset()
            Dim dt As New DataTable
            dt = objReports.LoadSalesRegister(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iPartyID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/SalesRegister.rdlc")
            ReportViewer1.LocalReport.Refresh()

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "Sales Register For The Year " & sSession.YearName & " ")}
            ReportViewer1.LocalReport.SetParameters(GeneralLedger)

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSalesRegister")
        End Try
    End Sub
    Public Sub BindMonth()
        Try
            ddlMonth.Items.Add(New ListItem("Select Month", 0))
            ddlMonth.Items.Add(New ListItem("April", 4))
            ddlMonth.Items.Add(New ListItem("May", 5))
            ddlMonth.Items.Add(New ListItem("June", 6))
            ddlMonth.Items.Add(New ListItem("July", 7))
            ddlMonth.Items.Add(New ListItem("August", 8))
            ddlMonth.Items.Add(New ListItem("September", 9))
            ddlMonth.Items.Add(New ListItem("October", 10))
            ddlMonth.Items.Add(New ListItem("November", 11))
            ddlMonth.Items.Add(New ListItem("December", 12))
            ddlMonth.Items.Add(New ListItem("January", 1))
            ddlMonth.Items.Add(New ListItem("Febraury", 2))
            ddlMonth.Items.Add(New ListItem("March", 3))
            ddlMonth.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BinPartyOrCustomerORGL()
        Try
            ddlCustomerParty.Items.Add(New ListItem("Select Customer/Supplier/GL", 0))
            ddlCustomerParty.Items.Add(New ListItem("Customer", 1))
            ddlCustomerParty.Items.Add(New ListItem("Supplier", 2))
            ddlCustomerParty.Items.Add(New ListItem("General Ledger", 3))
            ddlCustomerParty.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BinSupplierCustomer(ByVal sSC As String)
        Try
            If sSC = "Supplier" Then
                ddlCustomerParty.Items.Add(New ListItem("Select Customer/Supplier/GL", 0))
                ddlCustomerParty.Items.Add(New ListItem("Supplier", 2))
                ddlCustomerParty.SelectedIndex = 0
            ElseIf sSC = "Customer" Then
                ddlCustomerParty.Items.Add(New ListItem("Select Customer/Supplier/GL", 0))
                ddlCustomerParty.Items.Add(New ListItem("Customer", 1))
                ddlCustomerParty.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCustomerParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerParty.SelectedIndexChanged
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlCustomerParty.SelectedValue = 1 Then
                LoadParty(1)
            ElseIf ddlCustomerParty.SelectedValue = 2 Then
                LoadParty(2)
            ElseIf ddlCustomerParty.SelectedValue = 3 Then
                LoadParty(3)
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadParty(ByVal iType As Integer)
        Try

            If iType = 1 Then
                'ddlParty.DataSource = objPayment.LoadParty(sSession.AccessCode, sSession.AccessCodeID, iType)
                'ddlParty.DataTextField = "Name"
                'ddlParty.DataValueField = "ACM_ID"
                'ddlParty.DataBind()
                'ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
                ddlParty.DataSource = objReports.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "BM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")

            ElseIf iType = 2 Then
                'ddlParty.DataSource = objPayment.LoadParty(sSession.AccessCode, sSession.AccessCodeID, iType)
                'ddlParty.DataTextField = "Name"
                'ddlParty.DataValueField = "ACM_ID"
                'ddlParty.DataBind()
                'ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")
                ddlParty.DataSource = objReports.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "CSM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")

            ElseIf iType = 3 Then

                ddlParty.DataSource = objReports.LoadAllGLCodes(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "GlDesc"
                ddlParty.DataValueField = "gl_Id"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Supplier/Party")

            ElseIf iType = 4 Then
                ddlParty.DataSource = objReports.LoadBankNames(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "Gl_Desc"
                ddlParty.DataValueField = "gl_Id"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Bank Name")

            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonth.SelectedIndexChanged
        Try
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlReports.SelectedIndex = 11 Then 'Payment
                LoadPaymentDetails()
            ElseIf ddlReports.SelectedIndex = 12 Then   'Receipt
                LoadReceiptDetails()
                'ElseIf ddlReports.SelectedIndex = 18 Then   'Purchase Register
                '    LoadPurchaseRegister()
                'ElseIf ddlReports.SelectedIndex = 19 Then   'Sales Register
                '    LoadSalesRegister()
            ElseIf ddlReports.SelectedIndex = 18 Then 'Purchase Register
                If ddlExistPurchase.SelectedIndex > 0 Then
                    LoadPurchaseRegisterPO()
                Else
                    LoadPurchaseRegister()
                End If
            ElseIf ddlReports.SelectedIndex = 19 Then   'Sales Register
                If ddlExistSales.SelectedIndex > 0 Then
                    LoadSalesRegisterSO()
                Else
                    LoadSalesRegister()
                End If
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMonth_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub LoadPaymentDetails()
        Dim dt As New DataTable
        Dim dtTotal As New DataTable
        Dim i As Integer = 0
        Dim cr As Double = 0
        Dim db As Double = 0
        Dim iVoucher As Integer = 0
        Dim dPBillFromDate As String = "", dPBillToDate As String = ""
        Dim sBillNo As String = ""
        Dim iPaymentVoucherType As Integer = 0

        Dim dDebitTotal As Double : Dim dCreditTotal As Double

        Dim iMonthID As Integer : Dim iPartyID As Integer
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Try
            ReportViewer1.Reset()

            'If txtPBillFromDate.Text <> "" Then
            '    dPBillFromDate = Format(CDate(txtPBillFromDate.Text), "dd/MM/yyyy")
            '    dPBillToDate = Format(CDate(txtPBillToDate.Text), "dd/MM/yyyy")
            'Else
            '    dPBillFromDate = "01/01/1900"
            '    dPBillToDate = "01/01/1900"
            'End If

            'If txtPBillFromDate.Text <> "" Then
            '    dPBillFromDate = Date.ParseExact(txtPBillFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            '    dPBillToDate = Date.ParseExact(txtPBillToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'Else
            'dPBillFromDate = "01/01/1900"
            'dPBillToDate = "01/01/1900"
            'End If

            'If txtFromDate.Text <> "" Then
            '    dPBillFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dPBillFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dPBillToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dPBillToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dPBillFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dPBillFromDate = "01/01/1900"
                End If
            Else
                dPBillFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dPBillToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dPBillToDate = "01/01/1900"
                End If
            Else
                dPBillToDate = drtTOmonth
            End If
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            If ddlPVoucherNo.SelectedIndex > 0 Then
                iVoucher = ddlPVoucherNo.SelectedValue
            End If

            If ddlPBillNo.SelectedIndex > 0 Then
                sBillNo = ddlPBillNo.SelectedItem.Text
            End If

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iPartyID = ddlParty.SelectedValue
            Else
                iPartyID = 0
            End If

            If ddlRVoucherType.SelectedIndex > 0 Then
                iPaymentVoucherType = ddlRVoucherType.SelectedValue
            End If
            dt = objReports.LoadPaymentReportsDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo, iVoucher, dPBillFromDate, dPBillToDate, iMonthID, iPartyID, iZoneID, iRegionID, iAreaID, iBranchID, iPaymentVoucherType)
            dtTotal = objReports.LoadPaymentReportsDetailsTota(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sBillNo, iVoucher, dPBillFromDate, dPBillToDate, iMonthID, iPartyID, iZoneID, iRegionID, iAreaID, iBranchID, iPaymentVoucherType)
            If (dt.Rows.Count > 0) Then
                Dim rds As New ReportDataSource("DataSet1", dt)
                Dim rds1 As New ReportDataSource("DataSet2", dtTotal)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptPayment.rdlc")
                'ReportViewer1.LocalReport.Refresh()

                'ReportViewer1.Reset()
                'Dim rds As New ReportDataSource("DataSet1", dt)
                'ReportViewer1.LocalReport.DataSources.Add(rds)
                'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesDynamicRpt.rdlc")

                Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
                ReportViewer1.LocalReport.SetParameters(Vocher)

                Dim GeneralLedger As ReportParameter() = New ReportParameter() {New ReportParameter("GeneralLedger", "PAYMENT TRANSACTIONS FOR THE YEAR OF " & sSession.YearName & "")}
                ReportViewer1.LocalReport.SetParameters(GeneralLedger)

                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dDebitTotal = dDebitTotal + dt.Rows(i)("ATD_Debit")
                        dCreditTotal = dCreditTotal + dt.Rows(i)("ATD_Credit")
                    Next
                End If

                Dim TotalDebit As ReportParameter() = New ReportParameter() {New ReportParameter("TotalDebit", dDebitTotal)}
                ReportViewer1.LocalReport.SetParameters(TotalDebit)

                Dim TotalCredit As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCredit", dCreditTotal)}
                ReportViewer1.LocalReport.SetParameters(TotalCredit)
            Else
                lblError.Text = "No Data Found"
            End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPaymentDetails")
        End Try
    End Sub
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Try
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            If ddlReports.SelectedIndex = 11 Then 'Payment
                LoadPaymentDetails()
            ElseIf ddlReports.SelectedIndex = 12 Then   'Receipt
                LoadReceiptDetails()
                'ElseIf ddlReports.SelectedIndex = 18 Then   'Purchase Register
                '    LoadPurchaseRegister()
                'ElseIf ddlReports.SelectedIndex = 19 Then   'Sales Register
                '    LoadSalesRegister()
            ElseIf ddlReports.SelectedIndex = 18 Then   'Purchase Register
                If ddlExistPurchase.SelectedIndex > 0 Then
                    LoadPurchaseRegisterPO()
                Else
                    LoadPurchaseRegister()
                End If
            ElseIf ddlReports.SelectedIndex = 19 Then   'Sales Register                
                If ddlExistPurchase.SelectedIndex > 0 Then
                    LoadSalesRegisterSO()
                Else
                    LoadSalesRegister()
                End If
            Else
                If rbtDuration.SelectedIndex = -1 Then
                    rbtDuration.SelectedIndex = 0
                    rbtDuration_SelectedIndexChanged(sender, e)
                Else
                    btnGo_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub LoadCashBook()
        Dim sToYear As String = ""
        Dim dt As New DataTable

        Dim dFromDate As String = ""
        Dim dToDate As String = ""
        Dim iCustPartyType As Integer

        Dim iMonthID As Integer : Dim iParty As Integer
        Dim dTotalDebit As Double : Dim dTotalCredit As Double
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Try
            ReportViewer1.Reset()

            'Dim yForm = "yyyy/MM/dd"
            'Dim dFromDate = Format(CDate(txtFromDate.Text), yForm)
            'Dim dToDate = Format(CDate(txtToDate.Text), yForm)

            'dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iParty = ddlParty.SelectedValue
            Else
                iParty = 0
            End If
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If ddlCustomerParty.SelectedIndex > 0 Then
                iCustPartyType = ddlCustomerParty.SelectedValue
            End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If
            dt = objReports.LoadCashBookReport(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iParty, iCustPartyType, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptCashBook.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)
            Dim BankBook As ReportParameter() = New ReportParameter() {New ReportParameter("CashBook", "Cash Book Report")}

            ReportViewer1.LocalReport.SetParameters(BankBook)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dTotalDebit = dTotalDebit + dt.Rows(i)("ATD_Debit")
                    dTotalCredit = dTotalCredit + dt.Rows(i)("ATD_Credit")
                Next
            End If

            Dim dTotalD As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalDebit", dTotalDebit)}
            ReportViewer1.LocalReport.SetParameters(dTotalD)

            Dim dTotalC As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalCredit", dTotalCredit)}
            ReportViewer1.LocalReport.SetParameters(dTotalC)

            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCashBook")
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "--- Select Zone ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccZone.SelectedIndexChanged
        Try
            If ddlAccZone.SelectedIndex > 0 Then
                LoadRegion(ddlAccZone.SelectedValue)
            Else
                ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
            'ddlReports_SelectedIndexChanged(sender, e)
            If ddlExistPurchase.SelectedIndex > 0 Then
                LoadPurchaseRegisterPO()
            ElseIf ddlExistSales.SelectedIndex > 0 Then
                LoadSalesRegisterSO()
            Else
                ddlReports_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "--- Select Region ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccRgn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccRgn.SelectedIndexChanged
        Try
            If ddlAccRgn.SelectedIndex > 0 Then
                LoadArea(ddlAccRgn.SelectedValue)
            Else
                ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
            'ddlReports_SelectedIndexChanged(sender, e)
            If ddlExistPurchase.SelectedIndex > 0 Then
                LoadPurchaseRegisterPO()
            ElseIf ddlExistSales.SelectedIndex > 0 Then
                LoadSalesRegisterSO()
            Else
                ddlReports_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "--- Select Area ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccArea.SelectedIndexChanged
        Try
            If ddlAccArea.SelectedIndex > 0 Then
                LoadAccBrnch(ddlAccArea.SelectedValue)
            Else
                ddlAccBrnch.Items.Clear()
            End If
            'ddlReports_SelectedIndexChanged(sender, e)
            If ddlExistPurchase.SelectedIndex > 0 Then
                LoadPurchaseRegisterPO()
            ElseIf ddlExistSales.SelectedIndex > 0 Then
                LoadSalesRegisterSO()
            Else
                ddlReports_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                iParent = objReports.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                ddlAccArea.SelectedValue = iParent
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iParent = objReports.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                ddlAccRgn.SelectedValue = iParent
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iParent = objReports.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                ddlAccZone.SelectedValue = iParent
            End If

            If ddlExistPurchase.SelectedIndex > 0 Then
                LoadPurchaseRegisterPO()
            ElseIf ddlExistSales.SelectedIndex > 0 Then
                LoadSalesRegisterSO()
            Else
                ddlReports_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccBrnch_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlExistPurchase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistPurchase.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistPurchase.SelectedIndex > 0 Then
                PnlMonth.Visible = True
                LoadPurchaseRegisterPO()
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinSupplierCustomer("Supplier")
            Else
                ddlReports_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistPurchase_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadPurchaseRegisterPO()
        Dim iMonthID As Integer = 0 : Dim iPartyID As Integer = 0
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0, iPONO As Integer = 0
        Dim dtComp As DataTable
        Try
            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            End If

            If ddlParty.SelectedIndex > 0 Then
                iPartyID = ddlParty.SelectedValue
            End If

            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If ddlExistPurchase.SelectedIndex > 0 Then
                iPONO = ddlExistPurchase.SelectedValue
            End If
            ReportViewer1.Reset()
            Dim dt As New DataTable
            dt = objReports.BindItemsData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iPartyID, iZoneID, iRegionID, iAreaID, iBranchID, iPONO)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/PurchaseRegisterPO.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim PONO As ReportParameter() = New ReportParameter() {New ReportParameter("PONO", ddlExistPurchase.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(PONO)
            dtComp = objReports.GetPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, iPONO)
            If dtComp.Rows.Count > 0 Then
                Dim CName As ReportParameter() = New ReportParameter() {New ReportParameter("CName", " " & UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
                ReportViewer1.LocalReport.SetParameters(CName)
                Dim CAddress As ReportParameter() = New ReportParameter() {New ReportParameter("CAddress", " " & dtComp.Rows(0)("ACC_Purchase_CompanyAddress").ToString())}
                ReportViewer1.LocalReport.SetParameters(CAddress)
                Dim SName As ReportParameter() = New ReportParameter() {New ReportParameter("SName", " " & UCase(objReports.GetSupplierName(sSession.AccessCode, sSession.AccessCodeID, dtComp.Rows(0)("Acc_Purchase_Party"))))}
                ReportViewer1.LocalReport.SetParameters(SName)
                Dim SAddress As ReportParameter() = New ReportParameter() {New ReportParameter("SAddress", " " & dtComp.Rows(0)("ACC_Purchase_DeliveryFrom").ToString())}
                ReportViewer1.LocalReport.SetParameters(SAddress)
                Dim CGSTN As ReportParameter() = New ReportParameter() {New ReportParameter("CGSTN", " " & dtComp.Rows(0)("ACC_Purchase_CompanyGSTNRegNo").ToString())}
                ReportViewer1.LocalReport.SetParameters(CGSTN)
                Dim SGSTN As ReportParameter() = New ReportParameter() {New ReportParameter("SGSTN", " " & dtComp.Rows(0)("ACC_Purchase_DeliveryFromGSTNRegNo").ToString())}
                ReportViewer1.LocalReport.SetParameters(SGSTN)
            End If

            Dim CVAT As ReportParameter() = New ReportParameter() {New ReportParameter("CVAT", "<B>" & "VAT : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CVAT)

            Dim CTIN As ReportParameter() = New ReportParameter() {New ReportParameter("CTIN", "<B>" & "TIN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CTIN)

            Dim CTAN As ReportParameter() = New ReportParameter() {New ReportParameter("CTAN", "<B>" & "TAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CTAN)

            Dim CPAN As ReportParameter() = New ReportParameter() {New ReportParameter("CPAN", "<B>" & "PAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CPAN)

            Dim SVAT As ReportParameter() = New ReportParameter() {New ReportParameter("SVAT", "<B>" & "VAT : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(SVAT)

            Dim STIN As ReportParameter() = New ReportParameter() {New ReportParameter("STIN", "<B>" & "TIN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(STIN)

            Dim STAN As ReportParameter() = New ReportParameter() {New ReportParameter("STAN", "<B>" & "TAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(STAN)

            Dim SPAN As ReportParameter() = New ReportParameter() {New ReportParameter("SPAN", "<B>" & "PAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(SPAN)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlExistSales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistSales.SelectedIndexChanged
        Try
            If ddlExistSales.SelectedIndex > 0 Then
                PnlMonth.Visible = True
                LoadSalesRegisterSO()
                ddlMonth.Items.Clear() : ddlCustomerParty.Items.Clear()
                BindMonth() : BinSupplierCustomer("Customer")
            Else
                ddlReports_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistSales_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadSalesRegisterSO()
        Dim iMonthID As Integer = 0 : Dim iPartyID As Integer = 0
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0, iPONO As Integer = 0
        Dim dtComp As DataTable
        Try
            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            End If

            If ddlParty.SelectedIndex > 0 Then
                iPartyID = ddlParty.SelectedValue
            End If

            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If ddlExistSales.SelectedIndex > 0 Then
                iPONO = ddlExistSales.SelectedValue
            End If
            ReportViewer1.Reset()
            Dim dt As New DataTable
            dt = objReports.BindItemsDataSales(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iPartyID, iZoneID, iRegionID, iAreaID, iBranchID, iPONO)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/SalesRegisterSO.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim PONO As ReportParameter() = New ReportParameter() {New ReportParameter("PONO", ddlExistSales.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(PONO)
            dtComp = objReports.GetSalesDetails(sSession.AccessCode, sSession.AccessCodeID, iPONO)
            If dtComp.Rows.Count > 0 Then
                Dim CName As ReportParameter() = New ReportParameter() {New ReportParameter("CName", " " & UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
                ReportViewer1.LocalReport.SetParameters(CName)
                Dim CAddress As ReportParameter() = New ReportParameter() {New ReportParameter("CAddress", " " & dtComp.Rows(0)("ACC_Sales_CompanyAddress").ToString())}
                ReportViewer1.LocalReport.SetParameters(CAddress)
                Dim CustName As ReportParameter() = New ReportParameter() {New ReportParameter("CustName", " " & UCase(objReports.GetCustomerName(sSession.AccessCode, sSession.AccessCodeID, dtComp.Rows(0)("Acc_Sales_Party"))))}
                ReportViewer1.LocalReport.SetParameters(CustName)
                Dim CustAddress As ReportParameter() = New ReportParameter() {New ReportParameter("CustAddress", " " & dtComp.Rows(0)("ACC_Sales_BillingAddress").ToString())}
                ReportViewer1.LocalReport.SetParameters(CustAddress)
                Dim CGSTN As ReportParameter() = New ReportParameter() {New ReportParameter("CGSTN", " " & dtComp.Rows(0)("ACC_Sales_CompanyGSTNRegNo").ToString())}
                ReportViewer1.LocalReport.SetParameters(CGSTN)
                Dim SGSTN As ReportParameter() = New ReportParameter() {New ReportParameter("SGSTN", " " & dtComp.Rows(0)("ACC_Sales_BillingGSTNRegNo").ToString())}
                ReportViewer1.LocalReport.SetParameters(SGSTN)
            End If

            Dim CVAT As ReportParameter() = New ReportParameter() {New ReportParameter("CVAT", "<B>" & "VAT : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CVAT)

            Dim CTIN As ReportParameter() = New ReportParameter() {New ReportParameter("CTIN", "<B>" & "TIN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CTIN)

            Dim CTAN As ReportParameter() = New ReportParameter() {New ReportParameter("CTAN", "<B>" & "TAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CTAN)

            Dim CPAN As ReportParameter() = New ReportParameter() {New ReportParameter("CPAN", "<B>" & "PAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CPAN)

            Dim SVAT As ReportParameter() = New ReportParameter() {New ReportParameter("SVAT", "<B>" & "VAT : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(SVAT)

            Dim STIN As ReportParameter() = New ReportParameter() {New ReportParameter("STIN", "<B>" & "TIN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(STIN)

            Dim STAN As ReportParameter() = New ReportParameter() {New ReportParameter("STAN", "<B>" & "TAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(STAN)

            Dim SPAN As ReportParameter() = New ReportParameter() {New ReportParameter("SPAN", "<B>" & "PAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(SPAN)

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSalesRegister")
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim dDate, dSDate As Date
        Dim m As Integer
        Try
            GvPettyCashDetailsReport.DataSource = ""
            lblError.Text = ""
            'Cheque Date Comparision'
            If (txtFromDate.Text <> "" And txtToDate.Text <> "") Then
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "From Date (" & txtFromDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    txtFromDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "From Date (" & txtFromDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    txtFromDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'

                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "To Date Date (" & txtToDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    txtToDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "To Date Date (" & txtToDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    txtToDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'


                dDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "To Date (" & txtToDate.Text & ") should be Greater than From Date(" & txtFromDate.Text & ")."
                    txtToDate.Focus()
                    Exit Sub
                End If
            End If
            If ddlReports.SelectedIndex = 2 Then       'Opening Balance
                LoadOpeningBalance()
            ElseIf ddlReports.SelectedIndex = 3 Then
                LoadSubLeadger()
            ElseIf ddlReports.SelectedIndex = 4 Then   'Balance Sheet
                'LadBalanceSheet()
                Dim sDesc As String = ""
                sDesc = objReports.GetUnGroupedGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If sDesc <> "" Then
                    lblError.Text = "These are all the Un-Grouped Ledgers, Group it to get the Report."
                    lblMesg.Text = "" & sDesc & ""
                    lblMesg.ForeColor = Drawing.Color.Blue
                    Exit Sub
                Else
                    LadBalanceSheet()
                End If
            ElseIf ddlReports.SelectedIndex = 5 Then   'PL reports
                'LoadPLREPORTS()
                Dim sDesc As String = ""
                sDesc = objReports.GetUnGroupedGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If sDesc <> "" Then
                    lblError.Text = "These are all the Un-Grouped Ledgers, Group it to get the Report."
                    lblMesg.Text = "" & sDesc & ""
                    lblMesg.ForeColor = Drawing.Color.Blue
                    Exit Sub
                Else
                    LoadPLREPORTS()
                End If

            ElseIf ddlReports.SelectedIndex = 6 Then   'Schedule Report
                Label1.Visible = False : ddlFRReport.Visible = False : btnFreeze.Visible = False
                LoadScheduleReport()
            ElseIf ddlReports.SelectedIndex = 20 Then   'Trail Balance Actual
                LoadTrialBalanceActual()
            ElseIf ddlReports.SelectedIndex = 1 Then    'Transactions View
                LoadTrialBalance()
            ElseIf ddlReports.SelectedIndex = 8 Then    ' Day Book
                LoadDayBook()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()
            ElseIf ddlReports.SelectedIndex = 9 Then    'Bank Book 
                LoadbankBook()
                ddlMonth.SelectedIndex = 0 : ddlParty.SelectedIndex = 0
            ElseIf ddlReports.SelectedIndex = 10 Then   'Cash Book  
                LoadCashBook()
            ElseIf ddlReports.SelectedIndex = 7 Then   ' Schedule Reports With note
                LoadScheduleReportNotes()
            ElseIf ddlReports.SelectedIndex = 11 Then   'Payment
                LoadPaymentDetails()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()
            ElseIf ddlReports.SelectedIndex = 12 Then   'Receipt
                LoadReceiptDetails()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()
            ElseIf ddlReports.SelectedIndex = 13 Then   'Petty Cash
                LoadPettyCashDetails()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()
            ElseIf ddlReports.SelectedIndex = 14 Then   'Journal entry
                LoadJEDetails()
                ddlMonth.SelectedIndex = 0 : ddlCustomerParty.SelectedIndex = 0 : ddlParty.Items.Clear()
            ElseIf ddlReports.SelectedIndex = 15 Then   'Trial Balance Without P/L
            ElseIf ddlReports.SelectedIndex = 16 Then   'Sub Ledger 
                LoadPartWisesDbCr()
                'LoadSubLedgersDbCr()
            ElseIf ddlReports.SelectedIndex = 17 Then   'Sub Ledger of Debtors/Creditors
                ' LoadSubLedgersWiseDbCr()
            ElseIf ddlReports.SelectedIndex = 18 Then   'Purchase Register  
                LoadPurchaseRegister()
            ElseIf ddlReports.SelectedIndex = 19 Then   'Sales Register
                LoadSalesRegister()
            ElseIf ddlReports.SelectedIndex = 21 Then   'Petty Cash DayBook
                GvPettyCashDetailsReport.Visible = True
                LoadPettyCashDayBook()
            ElseIf ddlReports.SelectedIndex = 22 Then   'GL Summary  
                LoadGLSummary()
            End If

        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGo_Click")
        End Try
    End Sub
    Private Sub btnFreeze_Click(sender As Object, e As EventArgs) Handles btnFreeze.Click
        Dim dt As New DataTable
        Dim objFL As clsReports.TrailBal
        Try
            lblError.Text = ""
            dt = Session("TrailBalanceActual")
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If IsDBNull(dt.Rows(i)("gl_ID")) = False Then
                        objFL.iGLID = dt.Rows(i)("gl_ID")
                    End If
                    objFL.iAccHead = 0
                    If IsDBNull(dt.Rows(i)("gl_Head")) = False Then
                        objFL.iHead = dt.Rows(i)("gl_Head")
                    End If

                    If txtFromDate.Text <> "" Then
                        objFL.dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                    If txtToDate.Text <> "" Then
                        objFL.dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    If IsDBNull(dt.Rows(i)("OpDebit")) = False Then
                        objFL.dOpDebit = dt.Rows(i)("OpDebit")
                    End If
                    If IsDBNull(dt.Rows(i)("OpCredit")) = False Then
                        objFL.dOpCredit = dt.Rows(i)("OpCredit")
                    End If
                    If IsDBNull(dt.Rows(i)("TransDebit")) = False Then
                        objFL.dTrDebit = dt.Rows(i)("TransDebit")
                    End If
                    If IsDBNull(dt.Rows(i)("TransCredit")) = False Then
                        objFL.dTrCredit = dt.Rows(i)("TransCredit")
                    End If
                    If IsDBNull(dt.Rows(i)("ClosingDebit")) = False Then
                        objFL.dClDebit = dt.Rows(i)("ClosingDebit")
                    End If
                    If IsDBNull(dt.Rows(i)("ClosingCredit")) = False Then
                        objFL.dClCredit = dt.Rows(i)("ClosingCredit")
                    End If

                    objFL.iYearId = sSession.YearID
                    objFL.iCompid = sSession.AccessCodeID
                    objFL.iUserid = sSession.UserID
                    objFL.iZoneID = ddlAccZone.SelectedValue
                    objFL.iReigionID = ddlAccRgn.SelectedValue
                    objFL.iAreaID = ddlAccArea.SelectedValue
                    objFL.iBranchID = ddlAccBrnch.SelectedValue

                    objReports.FreezeGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objFL)
                    objFL.iGLID = 0
                Next
            End If
            lblError.Text = "Freezed Successfully"
            txtFromDate.Text = "" : txtToDate.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub FreezBalanceSheet()
        Dim dt As New DataTable
        Dim sToYear As String = ""
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim dFromDate, dToDate As Date
        Try
            ReportViewer1.Reset()
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If

            If txtFromDate.Text <> "" Then
                dFromDate = txtFromDate.Text
            Else
                dFromDate = "01/01/1900"
            End If

            If txtToDate.Text <> "" Then
                dToDate = txtToDate.Text
            Else
                dToDate = "01/01/1900"
            End If
            objReports.FreezBalanceSheet(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LadBalanceSheet")
        End Try
    End Sub
    Public Sub LoadFreeDDL()
        Try
            ddlFRReport.DataSource = objReports.LoadDuration(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlFRReport.DataTextField = "FreezeDate"
            'ddlFRReport.DataValueField = "0"
            ddlFRReport.DataBind()
            ddlFRReport.Items.Insert(0, "Select Report Duration")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlFRReport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFRReport.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sToYear As String = ""
        Try
            Dim sArray As String()
            Dim sFromDate As String = ""
            Dim sToDate As String = ""

            Dim sbret As String
            Dim sToSplit As String = ddlFRReport.Text
            sbret = sToSplit
            sArray = sbret.Split("-")

            sFromDate = sArray(0)
            sToDate = sArray(1).Trim().ToString()


            dt = objReports.GetFreezedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sFromDate, sToDate)
            Session("TrailBalanceActual") = dt
            ReportViewer1.LocalReport.Refresh()

            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RPTTBalance.rdlc")

            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)

            sToYear = objReports.GetYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            Dim TrialBalance As ReportParameter() = New ReportParameter() {New ReportParameter("TrialBalance", "TRIAL BALANCE AS ON  31/03/" & sToYear)}

            ReportViewer1.LocalReport.SetParameters(TrialBalance)
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LadBalanceSheet")
        End Try
    End Sub
    Public Sub loadReceiptVoucherType()
        Try
            ddlRVoucherType.DataSource = objReports.LoadBIllType(sSession.AccessCode, sSession.AccessCodeID)
            ddlRVoucherType.DataTextField = "Mas_Desc"
            ddlRVoucherType.DataValueField = "Mas_ID"
            ddlRVoucherType.DataBind()
            ddlRVoucherType.Items.Insert(0, "Select Voucher Type")
        Catch ex As Exception

        End Try
    End Sub
    Public Sub loadPaymentVoucherType()
        Try
            ddlRVoucherType.DataSource = objReports.LoadPBillType(sSession.AccessCode, sSession.AccessCodeID)
            ddlRVoucherType.DataTextField = "Mas_Desc"
            ddlRVoucherType.DataValueField = "Mas_ID"
            ddlRVoucherType.DataBind()
            ddlRVoucherType.Items.Insert(0, "Select Voucher Type")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GvPettyCashDetailsReport_PreRender(sender As Object, e As EventArgs) Handles GvPettyCashDetailsReport.PreRender
        Dim dt As New DataTable
        Try
            If GvPettyCashDetailsReport.Rows.Count > 0 Then
                GvPettyCashDetailsReport.UseAccessibleHeader = True
                GvPettyCashDetailsReport.HeaderRow.TableSection = TableRowSection.TableHeader
                GvPettyCashDetailsReport.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvPettyCashDetailsReport_PreRender")
        End Try
    End Sub
    Private Sub rbtDuration_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtDuration.SelectedIndexChanged
        Try
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.Refresh()
            PnlWeekly.Visible = False
            PnlDurationMonthly.Visible = False
            pnlyear.Visible = False : pnlQuarterly.Visible = False
            pnlBankDaybook.Visible = False : pnlHalfYearly.Visible = False
            ddlDurationweek.SelectedIndex = 0 : ddlDurationmonth.SelectedIndex = 0
            ddlDurationQuarter.SelectedIndex = 0 : ddlDurationhalfyear.SelectedIndex = 0
            txtFromDate.Text = "" : txtToDate.Text = ""
            If ddlReports.SelectedIndex = 0 Then
                lblError.Text = "Select Report Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ScriptManager1').modal('show');", True)
                rbtDuration.SelectedIndex = -1
                Exit Sub
            End If
            If rbtDuration.SelectedIndex = 0 Then
                Dim sYear As String
                Dim sYear1 As String
                Dim ayear As Array
                ayear = sSession.YearName.Split("-")
                sYear1 = Trim(ayear(1))
                sYear = Trim(ayear(0))
                drtFRMMonth = "04/01/" & sYear
                drtTOmonth = "03/31/" & sYear1
                btnGo_Click(sender, e)
            ElseIf rbtDuration.SelectedIndex = 1 Then
                pnlHalfYearly.Visible = True : pnlyear.Visible = True
            ElseIf rbtDuration.SelectedIndex = 2 Then
                ' pnlyear.Visible = True
                pnlQuarterly.Visible = True
            ElseIf rbtDuration.SelectedIndex = 3 Then
                PnlDurationMonthly.Visible = True
                'pnlyear.Visible = True
            ElseIf rbtDuration.SelectedIndex = 4 Then
                PnlWeekly.Visible = True
                PnlDurationMonthly.Visible = True
                ' pnlyear.Visible = True
            ElseIf rbtDuration.SelectedIndex = 5 Then
                Dim dToDate As String = ""
                dToDate = objReports.CheckFreezeDtae(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If dToDate <> "" Then
                    txtFromDate.Text = objGen.FormatDtForRDBMS(dToDate, "D")
                Else
                    txtFromDate.Text = objGen.FormatDtForRDBMS(objReports.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
                End If
                pnlBankDaybook.Visible = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlDurationmonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDurationmonth.SelectedIndexChanged
        Dim sFdate As String
        Dim ayear As Array
        Dim sYear As String
        Dim smonthid As String
        Dim EOMDate As Date
        Try
            If rbtDuration.SelectedIndex = 4 Then
                ddlDurationweek.SelectedIndex = 0
            Else
                If ddlDurationmonth.SelectedIndex = 1 Or ddlDurationmonth.SelectedIndex = 2 Or ddlDurationmonth.SelectedIndex = 3 Then
                    ayear = sSession.YearName.Split("-")
                    sYear = ayear(1)
                Else
                    ayear = sSession.YearName.Split("-")
                    sYear = ayear(0)
                End If
                If ddlDurationmonth.SelectedIndex = 10 Or ddlDurationmonth.SelectedIndex = 11 Or ddlDurationmonth.SelectedIndex = 12 Then
                    smonthid = ddlDurationmonth.SelectedIndex
                Else
                    smonthid = "0" & ddlDurationmonth.SelectedIndex
                End If
                If rbtDuration.SelectedIndex = 3 Then
                    drtFRMMonth = smonthid & "/01/" & sYear
                    sFdate = sYear & "-" & smonthid & "-01"
                    EOMDate = objReports.getEomDate(sSession.AccessCode, sFdate)
                    drtTOmonth = Convert.ToString(EOMDate.ToString("MM/dd/yyyy"))
                End If
                btnGo_Click(sender, e)
            End If


        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlDurationQuarter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDurationQuarter.SelectedIndexChanged
        Dim sYear As String
        Dim ayear As Array
        Try
            If ddlDurationQuarter.SelectedIndex = 4 Then
                ayear = sSession.YearName.Split("-")
                sYear = ayear(1)
            Else
                ayear = sSession.YearName.Split("-")
                sYear = ayear(0)
            End If
            If ddlDurationQuarter.SelectedIndex = 1 Then
                drtFRMMonth = "04/01/" & sYear
                drtTOmonth = "06/30/" & sYear
            ElseIf ddlDurationQuarter.SelectedIndex = 2 Then
                drtFRMMonth = "07/01/" & sYear
                drtTOmonth = "09/30/" & sYear
            ElseIf ddlDurationQuarter.SelectedIndex = 3 Then
                drtFRMMonth = "10/01/" & sYear
                drtTOmonth = "12/31/" & sYear
            ElseIf ddlDurationQuarter.SelectedIndex = 4 Then
                drtFRMMonth = "01/01/" & sYear
                drtTOmonth = "03/31/" & sYear
            End If
            btnGo_Click(sender, e)
            ' ddlReports_SelectedIndexChanged(sender, e)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlDurationhalfyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDurationhalfyear.SelectedIndexChanged
        Dim sYear As String
        Dim sYear1 As String
        Dim ayear As Array
        Try
            ' If ddlDurationhalfyear.SelectedIndex = 1 Then
            ayear = sSession.YearName.Split("-")
            sYear1 = ayear(1)
            sYear = ayear(0)
            ' Else
            'ayear = sSession.YearName.Split("-")
            '    sYear = ayear(1)
            '    sYear1 = ayear(0)
            ' End If
            If ddlDurationhalfyear.SelectedIndex = 1 Then
                drtFRMMonth = "04/01/" & sYear
                drtTOmonth = "09/30/" & sYear
            Else
                drtFRMMonth = "10/01/" & sYear
                drtTOmonth = "03/31/" & sYear1
            End If
            btnGo_Click(sender, e)
            ' ddlReports_SelectedIndexChanged(sender, e)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub txtToDate_TextChanged(sender As Object, e As EventArgs)
        Try
            btnGo_Click(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlRVoucherType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRVoucherType.SelectedIndexChanged
        Try
            PnlWeekly.Visible = False
            PnlDurationMonthly.Visible = False
            pnlyear.Visible = False : pnlQuarterly.Visible = False
            pnlBankDaybook.Visible = False : pnlHalfYearly.Visible = False
            rbtDuration.SelectedIndex = -1
            PnlFreeze.Visible = False
            btnGo_Click(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlPVoucherNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPVoucherNo.SelectedIndexChanged
        Try

            If ddlReports.SelectedIndex = 12 Or ddlReports.SelectedIndex = 13 Or ddlReports.SelectedIndex = 14 Then
                PnlWeekly.Visible = False
                PnlDurationMonthly.Visible = False
                pnlyear.Visible = False : pnlQuarterly.Visible = False
                pnlBankDaybook.Visible = False : pnlHalfYearly.Visible = False
                rbtDuration.SelectedIndex = -1
                PnlFreeze.Visible = False
                btnGo_Click(sender, e)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlDurationweek_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDurationweek.SelectedIndexChanged
        Dim sFdate As String
        Dim ayear As Array
        Dim sYear As String
        Dim smonthid As String
        Dim EOMDate As Date
        Try
            If rbtDuration.SelectedIndex = 4 Then
                If ddlDurationmonth.SelectedIndex = 0 Then
                    lblError.Text = "Select Month."
                    ddlDurationweek.SelectedIndex = 0
                    Exit Sub
                End If
            End If
            If ddlDurationmonth.SelectedIndex = 1 Or ddlDurationmonth.SelectedIndex = 2 Or ddlDurationmonth.SelectedIndex = 3 Then
                ayear = sSession.YearName.Split("-")
                sYear = ayear(1)
            Else
                ayear = sSession.YearName.Split("-")
                sYear = ayear(0)
            End If
            If ddlDurationmonth.SelectedIndex = 10 Or ddlDurationmonth.SelectedIndex = 11 Or ddlDurationmonth.SelectedIndex = 12 Then
                smonthid = ddlDurationmonth.SelectedIndex
            Else
                smonthid = "0" & ddlDurationmonth.SelectedIndex
            End If
            If rbtDuration.SelectedIndex = 4 Then
                If ddlDurationweek.SelectedIndex = 1 Then
                    drtFRMMonth = smonthid & "/01/" & sYear
                    drtTOmonth = smonthid & "/07/" & sYear
                ElseIf ddlDurationweek.SelectedIndex = 2 Then
                    drtFRMMonth = smonthid & "/08/" & sYear
                    drtTOmonth = smonthid & "/14/" & sYear
                ElseIf ddlDurationweek.SelectedIndex = 3 Then
                    drtFRMMonth = smonthid & "/15/" & sYear
                    drtTOmonth = smonthid & "/21/" & sYear
                ElseIf ddlDurationweek.SelectedIndex = 4 Then
                    drtFRMMonth = smonthid & "/22/" & sYear
                    sFdate = sYear & "-" & smonthid & "-22"
                    EOMDate = objReports.getEomDate(sSession.AccessCode, sFdate)
                    drtTOmonth = Convert.ToString(EOMDate.ToString("MM/dd/yyyy"))
                End If
            ElseIf rbtDuration.SelectedIndex = 3 Then
                drtFRMMonth = smonthid & "/01/" & sYear
                sFdate = sYear & "-" & smonthid & "-01"
                EOMDate = objReports.getEomDate(sSession.AccessCode, sFdate)
                drtTOmonth = Convert.ToString(EOMDate.ToString("MM/dd/yyyy"))
            End If
            btnGo_Click(sender, e)
            'ddlReports_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvPettyCashDetailsReport_PreRender")
        End Try
    End Sub
    Private Sub LoadGLSummary()
        Dim sToYear As String = ""
        Dim dt As New DataTable

        Dim dFromDate As String = ""
        Dim dToDate As String = ""
        Dim iCustPartyType As Integer

        Dim iMonthID As Integer : Dim iParty As Integer
        Dim dTotalDebit As Double : Dim dTotalCredit As Double
        Dim dTotalOpnDebit As Double : Dim dTotalOpnCredit As Double
        Dim dTotalClosingDebit As Double : Dim dTotalClosingCredit As Double
        Dim iZoneID As Integer = 0, iRegionID As Integer = 0, iAreaID As Integer = 0, iBranchID As Integer = 0
        Dim iGLID As Integer = 0, iGLIndex As Integer = 0

        Try
            ReportViewer1.Reset()

            'Dim yForm = "yyyy/MM/dd"
            'Dim dFromDate = Format(CDate(txtFromDate.Text), yForm)
            'Dim dToDate = Format(CDate(txtToDate.Text), yForm)

            'dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            Else
                iMonthID = 0
            End If

            If ddlParty.SelectedIndex > 0 Then
                iParty = ddlParty.SelectedValue

            Else
                iParty = 0
            End If
            If ddlAccZone.SelectedIndex > 0 Then
                iZoneID = ddlAccZone.SelectedValue
            End If
            If ddlAccRgn.SelectedIndex > 0 Then
                iRegionID = ddlAccRgn.SelectedValue
            End If
            If ddlAccArea.SelectedIndex > 0 Then
                iAreaID = ddlAccArea.SelectedValue
            End If
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            End If
            If ddlCustomerParty.SelectedIndex > 0 Then
                iCustPartyType = ddlCustomerParty.SelectedValue
            End If
            If ddlGL.SelectedIndex > 0 Then
                iGLID = ddlGL.SelectedValue
            End If
            If ddlSubGL.SelectedIndex > 0 Then
                iGLIndex = ddlSubGL.SelectedValue
            End If
            'If txtFromDate.Text <> "" Then
            '    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
            'Else
            '    dFromDate = "01/01/1900"
            'End If
            'If txtToDate.Text <> "" Then
            '    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
            'Else
            '    dToDate = "01/01/1900"
            'End If
            If rbtDuration.SelectedIndex = 5 Or drtFRMMonth = "" Then
                If txtFromDate.Text <> "" Then
                    dFromDate = clsGeneralFunctions.FormatMyDate(txtFromDate.Text)
                Else
                    dFromDate = "01/01/1900"
                End If
            Else
                dFromDate = drtFRMMonth
            End If
            If rbtDuration.SelectedIndex = 5 Or drtTOmonth = "" Then
                If txtToDate.Text <> "" Then
                    dToDate = clsGeneralFunctions.FormatMyDate(txtToDate.Text)
                Else
                    dToDate = "01/01/1900"
                End If
            Else
                dToDate = drtTOmonth
            End If

            dt = objReports.LoadGLSummaryReport(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMonthID, iParty, iCustPartyType, iZoneID, iRegionID, iAreaID, iBranchID, dFromDate, dToDate)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/rptGLSummary.rdlc")



            Dim Vocher As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", UCase(objReports.GetCustomers(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(Vocher)
            Dim BankBook As ReportParameter() = New ReportParameter() {New ReportParameter("GLSummary", "GL Summary Report")}

            ReportViewer1.LocalReport.SetParameters(BankBook)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dTotalDebit = dTotalDebit + dt.Rows(i)("ATD_Debit")
                    dTotalCredit = dTotalCredit + dt.Rows(i)("ATD_Credit")

                    dTotalOpnDebit = dTotalOpnDebit + dt.Rows(i)("ATD_OpenDebit")
                    dTotalOpnCredit = dTotalOpnCredit + dt.Rows(i)("ATD_OpenCredit")

                    dTotalClosingDebit = dTotalClosingDebit + dt.Rows(i)("ATD_ClosingDebit")
                    dTotalClosingCredit = dTotalClosingCredit + dt.Rows(i)("ATD_ClosingCredit")
                Next
            End If

            Dim dTotalD As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalDebit", dTotalDebit)}
            ReportViewer1.LocalReport.SetParameters(dTotalD)

            Dim dTotalC As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalCredit", dTotalCredit)}
            ReportViewer1.LocalReport.SetParameters(dTotalC)

            Dim dTotalOpnD As ReportParameter() = New ReportParameter() {New ReportParameter("dTotlaOpnDebit", dTotalOpnDebit)}
            ReportViewer1.LocalReport.SetParameters(dTotalOpnD)

            Dim dTotalOpnC As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalOpnCredit", dTotalOpnCredit)}
            ReportViewer1.LocalReport.SetParameters(dTotalOpnC)

            Dim dTotalClosingD As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalClosingDebit", dTotalClosingDebit)}
            ReportViewer1.LocalReport.SetParameters(dTotalClosingD)

            Dim dTotalClosingC As ReportParameter() = New ReportParameter() {New ReportParameter("dTotalClosingCredit", dTotalClosingCredit)}
            ReportViewer1.LocalReport.SetParameters(dTotalClosingC)

            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            GetLineNumber(ex)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadGLSummary")
        End Try
    End Sub

End Class
