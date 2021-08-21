Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports Microsoft.Reporting.WebForms
Imports System.Drawing
Imports System.IO
Imports System.Diagnostics

Partial Class Accounts_BankReconciliation
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Accounts\BankReconciliation.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sIKBBackStatus As String
    Dim objBank As New clsBankReconciliation
    Private Shared sSession As AllSession
    Dim objClsFASGnrl As New clsFASGeneral

    Dim objJE As New clsJournalEntry
    Dim objGen As New clsFASGeneral

    Dim clsgeneral As New clsFASGeneral
    Private Shared sMasterStatus As String
    Private Shared sEMDSave As String
    Private Shared sEMDFlag As String
    Private Shared sEMDBackStatus As String
    Private Shared sExcelSave As String
    Private Shared sFilePath As String
    Private Shared sFile As String
    'Dim dtpublic As DataTable
    Private Shared dtpublic As New DataTable
    Dim dtStock As New DataTable
    Private Shared dttable As New DataTable
    Private Shared dttable1 As New DataTable
    Private Shared CompuniqueRow As New DataTable
    Private Shared dttabledatacapture1 As New DataTable
    Private Shared iBNKDebitCount As Integer = 0
    Private Shared iBNKCreditCount As Integer = 0
    Private Shared iCompDebitCount As Integer = 0
    Private Shared iCompCreditCount As Integer = 0
    Private Shared iBNKDebSum As Integer = 0
    Private Shared iBNKCreditSum As Integer = 0
    Private Shared iCompDebitSum As Integer = 0
    Private Shared iCompCreditSum As Integer = 0
    Private Shared result As Double
    Private Shared iCmpDebitCount As Integer = 0
    Private Shared icmpCreditCount As Integer = 0

    Private Shared dtBankDetails As New DataTable
    Private Shared iJEID As Integer
    Private Shared Adjstedamunt As Integer
    Private Shared BalComp As Integer
    Private Shared BalBnk As Integer
    Private Shared TotalAmnt As Integer
    Private Shared iCompDebitCount1 As Integer = 0
    Private Shared iCOMPCrdCount1 As Integer = 0
    Private Shared iBOpeningBal1 As Integer = 0


    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        ImgbtnPrint.ImageUrl = "~/Images/Download24.png"
        'ImgReport.ImageUrl = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dt As New DataTable
        Try
            sSession = Session("AllSession")
            If (IsPostBack = False) Then
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("SerialNo"), New DataColumn("TrType")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("TransactionNo"), New DataColumn("TxnDate")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("ChequeNo"), New DataColumn("ChequeDate")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("IFSCCODEBNK"), New DataColumn("BankName")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("BranchName"), New DataColumn("Debit")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("Credit"), New DataColumn("Description")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("RefNo"), New DataColumn("BranchCode")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("Balance"), New DataColumn("Status")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("DabitDiff"), New DataColumn("CreditDiff")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("CDebit"), New DataColumn("CompDebitSum")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("CCredit"), New DataColumn("CompCreditSum")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("BDebit"), New DataColumn("BCredit")})
                dt.Columns.AddRange(New DataColumn(1) {New DataColumn("BillType"), New DataColumn("Opening_Bal")})
                ViewState("ManualEntry") = dt
                Me.BindGrid()
                Me.btnGo.Attributes.Add("OnClick", "javascript:return clientfunction();")
                'Me.btnCompare.Attributes.Add("OnClick", "javascript:return Compare();")
                iBNKDebitCount = 0 : iBNKCreditCount = 0 : iCompDebitCount = 0 : iCompCreditCount = 0 : iBOpeningBal1 = 0
                iCompDebitCount1 = 0 : iCOMPCrdCount1 = 0
                iCmpDebitCount = 0 : icmpCreditCount = 0
                iJEID = 0 : Adjstedamunt = 0 : BalComp = 0 : BalBnk = 0 : TotalAmnt = 0
                dttable = Nothing : dtpublic = Nothing : dttabledatacapture1 = Nothing
                dttable1 = Nothing
                dtBankDetails = Nothing
                CompuniqueRow = Nothing
                LoadReconciliationNo()
                LoadBank()
                BindReports()
                'Else
                '    iBNKDebitCount = 0 : iBNKCreditCount = 0 : iCompDebitCount = 0 : iCompCreditCount = 0 : iBOpeningBal1 = 0
                '    iCompDebitCount1 = 0 : iCOMPCrdCount1 = 0
                '    iCmpDebitCount = 0 : icmpCreditCount = 0
                '    iJEID = 0 : Adjstedamunt = 0 : BalComp = 0 : BalBnk = 0 : TotalAmnt = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        Try
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            Dim cell As New TableHeaderCell()

            cell.ColumnSpan = 10
            cell.Text = ""
            row.Controls.Add(cell)

            cell = New TableHeaderCell()
            cell.Text = "Bank Book"
            cell.ColumnSpan = 2
            row.Controls.Add(cell)

            cell = New TableHeaderCell()
            cell.ColumnSpan = 5
            cell.Text = ""
            row.Controls.Add(cell)

            cell = New TableHeaderCell()
            cell.Text = "Company Book"
            cell.ColumnSpan = 2
            row.Controls.Add(cell)

            cell = New TableHeaderCell()
            cell.ColumnSpan = 2
            cell.Text = ""
            row.Controls.Add(cell)
            dgMatchedRows.HeaderRow.Parent.Controls.AddAt(0, row)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "OnDataBound")
        End Try
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            dgBank.DataSource = Nothing
            dgBank.DataBind()

            If FULoad.FileName <> String.Empty Then
                lblSheetName.Visible = True : ddlSheetName.Visible = True
                sExt = IO.Path.GetExtension(FULoad.PostedFile.FileName)
                Session("sExt") = sExt
                If sExt = ".pdf" Then                                                                                    'Vijeth
                    sFileName = System.IO.Path.GetFileName(FULoad.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    'sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
                    sPath = "C:\FASINFO\temp\Sa"
                    If Directory.Exists(sPath) = True Then
                        Dim files() As String = Directory.GetFileSystemEntries(sPath)
                        For Each element As String In files
                            If System.IO.File.Exists(element) = True Then
                                Try
                                    File.Delete(element)
                                    ' My.Computer.FileSystem.DeleteFile(element)y
                                Catch ex As Exception
                                End Try
                            End If
                        Next
                    End If

                    If System.IO.Directory.Exists(sPath & "Temp_pdf") = False Then
                        System.IO.Directory.CreateDirectory(sPath & "Temp_pdf")
                    End If

                    Dim Tpaths As String = (sPath & "Temp_pdf")
                    If Directory.Exists(Tpaths) = True Then
                        Dim files() As String = Directory.GetFileSystemEntries(Tpaths)
                        For Each element As String In files
                            If System.IO.File.Exists(element) = True Then
                                Try
                                    File.Delete(element)
                                    ' My.Computer.FileSystem.DeleteFile(element)
                                Catch ex As Exception
                                End Try
                            End If
                        Next
                    End If

                    If Tpaths.EndsWith("\") = False Then
                        sFile = Tpaths & "\" & sFileName
                    Else
                        sFile = Tpaths & sFileName
                    End If

                    FULoad.PostedFile.SaveAs(sFile)
                    Dim Sfil = System.IO.Path.GetFileNameWithoutExtension(FULoad.PostedFile.FileName)


                    'objclstest.LoadPDF(sFile, sPath & "\" & Sfil & ".xlsx")


                    Dim Command As String = "C:\python\RecPDF.py"
                    Dim ProcessInfo As ProcessStartInfo
                    Dim Process As Process
                    ProcessInfo = New ProcessStartInfo("cmd.exe", "/C " + Command)
                    ProcessInfo.UseShellExecute = False
                    ProcessInfo.CreateNoWindow = True
                    Process = Process.Start(ProcessInfo)
                    Process.WaitForExit()
                    Process.Close()

                    ' SurroundingSub(sPath & "\" & "w12" & ".xlsx", ".xlsx")

                    ddlSheetName.Items.Clear()
                    dt = ExcelSheetNames(sPath & "\" & "w" & ".xlsx")
                    ddlSheetName.DataSource = dt
                    ddlSheetName.DataTextField = "Name"
                    ddlSheetName.DataValueField = "ID"
                    ddlSheetName.DataBind()
                    ddlSheetName.Items.Insert(0, "Select Sheet")
                    ' dtSAPAttachment = dt.Copy
                    sFile = sPath & "\" & "w" & ".xlsx"

                    Exit Sub

                End If

                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FULoad.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    ' sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
                    sPath = "C:\FASINFO\temp\Sa"
                    If Directory.Exists(sPath) = True Then
                        Dim files() As String = Directory.GetFileSystemEntries(sPath)
                        For Each element As String In files
                            If System.IO.File.Exists(element) = True Then
                                Try
                                    File.Delete(element)
                                    ' My.Computer.FileSystem.DeleteFile(element)
                                Catch ex As Exception
                                End Try
                            End If
                        Next
                    End If

                    If System.IO.Directory.Exists(sPath) = False Then
                        System.IO.Directory.CreateDirectory(sPath)
                    End If

                    Dim Tpaths As String = (sPath)
                    If Directory.Exists(Tpaths) = True Then
                        Dim files() As String = Directory.GetFileSystemEntries(Tpaths)
                        For Each element As String In files
                            If System.IO.File.Exists(element) = True Then
                                Try
                                    File.Delete(element)
                                    ' My.Computer.FileSystem.DeleteFile(element)
                                Catch ex As Exception
                                End Try
                            End If
                        Next
                    End If
                    If sPath.EndsWith("\") = False Then
                        sFile = sPath & "\" & sFileName
                    Else
                        sFile = sPath & sFileName
                    End If

                    FULoad.PostedFile.SaveAs(sFile)
                    Dim Command As String = "C:\Python37\RecEXCEL.py"
                    Dim ProcessInfo As ProcessStartInfo
                    Dim Process As Process
                    ProcessInfo = New ProcessStartInfo("cmd.exe", "/C " + Command)
                    ProcessInfo.UseShellExecute = False
                    ProcessInfo.CreateNoWindow = True
                    Process = Process.Start(ProcessInfo)
                    Process.WaitForExit()
                    Process.Close()

                    ddlSheetName.Items.Clear()
                    dt = ExcelSheetNames(sFile)
                    ddlSheetName.DataSource = dt
                    ddlSheetName.DataTextField = "Name"
                    ddlSheetName.DataValueField = "ID"
                    ddlSheetName.DataBind()
                    ddlSheetName.Items.Insert(0, "Select Sheet")
                    sFile = sPath & "\" & "w" & ".xlsx"
                Else
                    lblError.Text = "Select Excel file only." : lblExcelValidationMsg.Text = "Select Excel file only."
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblExcelValidationMsg.Text = "Select Excel file."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOk_Click")
        End Try
    End Sub
    Protected Sub ValidateDate(sender As Object, e As ServerValidateEventArgs)
        If Regex.IsMatch(txtfrom.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then
            Dim dt As DateTime
            e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", New CultureInfo("en-GB"), DateTimeStyles.None, dt)
            If e.IsValid Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Valid Date.');", True)
            End If
        Else
            e.IsValid = False
        End If
    End Sub
    Private Sub LoadReconciliationNo()
        Try
            ddlExistinReconciliation.DataSource = objBank.LoadReconciliationNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistinReconciliation.DataTextField = "ABRM_BRID"
            ddlExistinReconciliation.DataValueField = "ABRM_ID"
            ddlExistinReconciliation.DataBind()
            ddlExistinReconciliation.Items.Insert(0, "--- Select Reconciliation No ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function ExcelSheetNames(ByVal sPath As String) As DataTable
        Dim dt As New DataTable
        Dim XLCon As OleDb.OleDbConnection
        Dim dtTab As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            XLCon = MSAccessOpenConnection(sPath)
            dt = XLCon.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
            If dt.Rows.Count > 0 Then
                dtTab.Columns.Add("ID")
                dtTab.Columns.Add("Name")
                For i = 0 To dt.Rows.Count - 1
                    drow = dtTab.NewRow
                    drow("ID") = i + 1
                    drow("Name") = dt.Rows(i)(2)
                    dtTab.Rows.Add(drow)
                Next
            End If
            XLCon.Close()
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function MSAccessOpenConnection(ByVal sFile As String) As OleDb.OleDbConnection
        Dim con As New OleDb.OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.8.0;Data Source=" & sFile & ";Extended Properties=Excel 8.0;"
            con.Open()
            Return con
        Catch ex As Exception
        End Try
        Try
            con.ConnectionString = "Data Source=" & sFile & ";Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;"
            con.Open()
            Return con
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function Datecheck(ByVal sDateofPur As String) As Boolean
        Dim pattern As String = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
        Dim DateMatch As Match = Regex.Match(sDateofPur, pattern)
        If DateMatch.Success Then
            Datecheck = True
        Else
            Datecheck = False
        End If
    End Function
    Private Function Stringcheck(ByVal sStringm As String) As Boolean
        Dim pattern As String = "^[a-zA-Z\s]+$"
        Dim StringMatch As Match = Regex.Match(sStringm, pattern)
        If StringMatch.Success Then
            Stringcheck = True
        Else
            Stringcheck = False
        End If
    End Function
    Private Function Amountcheck(ByVal sAmt As String) As Boolean
        Dim pattern As String = "^[0-9]\d*(\.\d+)?$"
        Dim AmountMatch As Match = Regex.Match(sAmt, pattern)
        If AmountMatch.Success Then
            Amountcheck = True
        Else
            Amountcheck = False
        End If
    End Function
    '    Protected Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
    '        Dim dt As New DataTable
    '        Dim Uniquecompdt As New DataTable
    '        Dim sDateofPur As String = ""
    '        Dim sString As String
    '        Dim sAmt As String
    '        Try
    '            If dtpublic Is Nothing Then
    '                lblCustomerValidationMsg.Text = "Click Go Button"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASBankValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            dt.Columns.Add("SlrNo")
    '            dt.Columns.Add("SerialNo")
    '            dt.Columns.Add("TrType")
    '            dt.Columns.Add("TransactionNo")
    '            dt.Columns.Add("TxnDate")
    '            dt.Columns.Add("ChequeNo")
    '            dt.Columns.Add("ChequeDate")
    '            dt.Columns.Add("IFSCCODEBNK")
    '            dt.Columns.Add("BankName")
    '            dt.Columns.Add("BranchName")
    '            dt.Columns.Add("Debit")
    '            dt.Columns.Add("Credit")
    '            dt.Columns.Add("Description")
    '            dt.Columns.Add("RefNo")
    '            dt.Columns.Add("BranchCode")
    '            dt.Columns.Add("Balance")
    '            dt.Columns.Add("Status")
    '            dt.Columns.Add("DabitDiff")
    '            dt.Columns.Add("CreditDiff")
    '            dt.Columns.Add("CDebit")
    '            dt.Columns.Add("CCredit")
    '            dt.Columns.Add("BCredit")
    '            dt.Columns.Add("BDebit")
    '            dt.Columns.Add("Creditresult")
    '            dt.Columns.Add("Debitresult")
    '            dt.Columns.Add("CompDebitresult")
    '            dt.Columns.Add("CompCreditresult")
    '            dt.Columns.Add("BillType")
    '            dt.Columns.Add("Opening_Bal")

    '            If ddlSheetName.SelectedIndex > 0 Then
    '                dtStock = LoadExcelTable(sFile)
    '                For i = 0 To dtStock.Rows.Count - 1
    '                    Dim dRow As DataRow
    '                    dRow = dt.NewRow
    '                    If IsDBNull(dtStock.Rows(i).Item("SlrNo")) = False Then
    '                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
    '                            dRow("SlrNo") = dtStock.Rows(i).Item("SlrNo")
    '                        End If
    '                    End If
    '                    If IsDBNull(dtStock.Rows(i).Item("SerialNo")) = False Then
    '                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
    '                            dRow("SerialNo") = dtStock.Rows(i).Item("SerialNo")
    '                        End If
    '                    End If
    '                    If IsDBNull(dtStock.Rows(i).Item("TrType")) = False Then
    '                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
    '                            dRow("TrType") = dtStock.Rows(i).Item("TrType")
    '                        End If
    '                    End If

    '                    If IsDBNull(dtStock.Rows(i).Item("TransactionNo")) = False Then
    '                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
    '                            dRow("TransactionNo") = dtStock.Rows(i).Item("TransactionNo")
    '                        End If
    '                    End If


    '                    If Trim(dtStock.Rows(i).Item("TxnDate").ToString()) = "" Then
    '                        dRow("TxnDate") = ""
    '                    Else
    '                        sDateofPur = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("TxnDate"), "D")
    '                        If Datecheck(sDateofPur) = False Then
    '                            lblError.Text = "Enter Valid TxnDate"
    '                            Exit Sub
    '                        Else
    '                            dRow("TxnDate") = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("TxnDate"), "D")
    '                        End If
    '                    End If
    '                    If IsDBNull(dtStock.Rows(i).Item("ChequeNo")) = False Then
    '                        If dtStock.Rows(i).Item("ChequeNo").ToString <> "&nbsp;" Then
    '                            dRow("ChequeNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("ChequeNo").ToString())
    '                            'Else
    '                            '    dRow("ChequeNo") = ""
    '                        End If
    '                    End If
    '                    If Trim(dtStock.Rows(i).Item("ChequeDate").ToString()) = "" Then
    '                        dRow("ChequeDate") = ""
    '                    Else
    '                        sDateofPur = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("ChequeDate"), "D")
    '                        If Datecheck(sDateofPur) = False Then
    '                            lblError.Text = "Enter Valid ChequeDate"
    '                            Exit Sub
    '                        Else
    '                            dRow("ChequeDate") = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("ChequeDate"), "D")
    '                        End If
    '                    End If

    '                    If Trim(dtStock.Rows(i).Item("IFSCCODEBNK").ToString()) = "" Then
    '                        dRow("IFSCCODEBNK") = ""
    '                    Else
    '                        sDateofPur = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("IFSCCODEBNK"), "D")
    '                        If Datecheck(sDateofPur) = False Then
    '                            lblError.Text = "Enter Valid ValueDate"
    '                            Exit Sub
    '                        Else
    '                            dRow("IFSCCODEBNK") = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("IFSCCODEBNK"), "D")
    '                        End If
    '                    End If
    '                    If Trim(dtStock.Rows(i).Item("BankName").ToString()) = "" Then
    '                        dRow("BankName") = ""
    '                    Else
    '                        sString = dtStock.Rows(i).Item("BankName")
    '                        If Stringcheck(sString) = False Then
    '                            lblError.Text = "Enter Valid BankName no special characters are allowed"
    '                            Exit Sub
    '                        Else
    '                            dRow("BankName") = objClsFASGnrl.SafeSQL(dtStock.Rows(i).Item("BankName"))
    '                        End If
    '                    End If

    '                    If Trim(dtStock.Rows(i).Item("BranchName").ToString()) = "" Then
    '                        dRow("BranchName") = ""
    '                    Else
    '                        sString = dtStock.Rows(i).Item("BranchName")
    '                        If Stringcheck(sString) = False Then
    '                            lblError.Text = "Enter Valid BranchName no special characters are allowed"
    '                            Exit Sub
    '                        Else
    '                            dRow("BranchName") = objClsFASGnrl.SafeSQL(dtStock.Rows(i).Item("BranchName"))
    '                        End If
    '                    End If

    '                    If dtStock.Rows(i).Item("Debit").ToString() = "" Or dtStock.Rows(i).Item("Debit") = 0 Then
    '                        dRow("Debit") = "0.00"
    '                    Else
    '                        dRow("Debit") = String.Format("{0:0.00}", Convert.ToDecimal(dtStock.Rows(i)("Debit")))
    '                    End If

    '                    If dtStock.Rows(i).Item("Credit").ToString() = "" Or dtStock.Rows(i).Item("Credit") = 0 Then
    '                        dRow("Credit") = "0.00"
    '                    Else
    '                        dRow("Credit") = String.Format("{0:0.00}", Convert.ToDecimal(dtStock.Rows(i)("Credit")))
    '                    End If

    '                    If IsDBNull(dtStock.Rows(i).Item("Description")) = False Then
    '                        If dtStock.Rows(i).Item("Description").ToString <> "&nbsp;" Then
    '                            dRow("Description") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("Description"))
    '                        End If
    '                    End If

    '                    If IsDBNull(dtStock.Rows(i).Item("RefNo")) = False Then
    '                        If dtStock.Rows(i).Item("RefNo").ToString <> "&nbsp;" Then
    '                            dRow("RefNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("RefNo"))
    '                        End If
    '                    End If

    '                    If IsDBNull(dtStock.Rows(i).Item("BranchCode")) = False Then
    '                        If dtStock.Rows(i).Item("BranchCode").ToString <> "&nbsp;" Then
    '                            dRow("BranchCode") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("BranchCode"))
    '                        End If
    '                    End If
    '                    If IsDBNull(dtStock.Rows(i).Item("Balance")) = False Then
    '                        dRow("Balance") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("Balance"))
    '                    Else
    '                        dRow("Balance") = ""
    '                    End If
    '                    If IsDBNull(dtStock.Rows(i).Item("BillType")) = False Then
    '                        dRow("BillType") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("BillType"))
    '                    Else
    '                        dRow("BillType") = ""
    '                    End If

    '                    If dtStock.Rows(i).Item("Opening_Bal").ToString() = "" Then
    '                        dRow("Opening_Bal") = "0.000"
    '                    Else
    '                        sAmt = dtStock.Rows(i).Item("Opening_Bal")
    '                        If Amountcheck(sAmt) = False Then
    '                            lblError.Text = "Enter Valid Opening Balance No Special Characters Alowed"
    '                            Exit Sub
    '                        Else
    '                            dRow("Opening_Bal") = objClsFASGnrl.SafeSQL(dtStock.Rows(i).Item("Opening_Bal"))
    '                        End If

    '                    End If

    '                    dRow("CreditDiff") = "0.00"
    '                    dRow("DabitDiff") = "0.00"
    '                    dRow("CDebit") = "0.00"
    '                    dRow("CCredit") = "0.00"
    '                    dRow("BCredit") = "0.00"
    '                    dRow("BDebit") = "0.00"
    '                    dRow("Creditresult") = "0.00"
    '                    dRow("Debitresult") = "0.00"
    '                    dRow("CompDebitresult") = "0.00"
    '                    Dim dt1 As New DataTable
    '                    For j = 0 To dtpublic.Rows.Count - 1
    '                        If (dtpublic.Rows(j).Item("Acc_PM_ChequeNo").ToString()) = (dtStock.Rows(i).Item("ChequeNo").ToString()) Then
    '                            If (((dtpublic.Rows(j).Item("ATD_Debit")).ToString() = dRow("Debit")) And ((dtpublic.Rows(j).Item("ATD_Credit")).ToString() = dRow("Credit"))) Then
    '                                dRow("Status") = "EAM"
    '                                dRow("CCredit") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Credit")).ToString("#,##0.00")
    '                                dRow("CDebit") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Debit")).ToString("#,##0.00")
    '                                dRow("BDebit") = Convert.ToDecimal(dtStock.Rows(i)("Debit")).ToString("#,##0.00")
    '                                dRow("BCredit") = Convert.ToDecimal(dtStock.Rows(i)("Credit")).ToString("#,##0.00")
    '                                objBank.DeleteMAtchedINOUTData(sSession.AccessCode, sSession.AccessCodeID, dtpublic.Rows(j).Item("SrNo"))
    '                                GoTo Skip
    '                            Else
    '                                dRow("Status") = "EAN"
    '                                dRow("CCredit") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Credit")).ToString("#,##0.00")
    '                                dRow("CDebit") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Debit")).ToString("#,##0.00")
    '                                dRow("BDebit") = Convert.ToDecimal(dtStock.Rows(i)("Debit")).ToString("#,##0.00")
    '                                dRow("BCredit") = Convert.ToDecimal(dtStock.Rows(i)("Credit")).ToString("#,##0.00")
    '                                dRow("DabitDiff") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Debit").ToString()) - Convert.ToDecimal(dtStock.Rows(i)("Debit")).ToString("#,##0.00")
    '                                dRow("CreditDiff") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Credit").ToString()) - Convert.ToDecimal(dtStock.Rows(i)("Credit")).ToString("#,##0.00")
    '                                GoTo Skip
    '                            End If
    '                        Else
    '                            dRow("Status") = "N"
    '                            dRow("BDebit") = Convert.ToDecimal(dtStock.Rows(i)("Debit")).ToString("#,##0.00")
    '                            dRow("BCredit") = Convert.ToDecimal(dtStock.Rows(i)("Credit")).ToString("#,##0.00")
    '                        End If
    '                    Next
    'Skip:               dt.Rows.Add(dRow)
    '                Next

    '                If IsNothing(dt) = True Then
    '                    Exit Sub
    '                End If

    '                dgBank.DataSource = dt
    '                dgBank.DataBind()

    '                If dgBank.Rows.Count > 0 Then
    '                    lbl_BankBook.Visible = True
    '                Else
    '                    lbl_BankBook.Visible = False
    '                End If

    '                dttable = dt.Copy

    '                Dim dtTemp As New DataTable
    '                Uniquecompdt = dtpublic.Copy
    '                dtTemp = dtpublic.Copy

    '                For a As Integer = Uniquecompdt.Rows.Count - 1 To 0 Step -1
    '                    For b As Integer = 0 To dt.Rows.Count - 1
    '                        If (Uniquecompdt.Rows(a)("Acc_PM_ChequeNo").ToString()) = Nothing Then
    '                        Else
    '                            If ((Uniquecompdt.Rows(a)("Acc_PM_ChequeNo").ToString()) = (dt.Rows(b)("chequeNo").ToString())) Then
    '                                dtTemp.Rows.RemoveAt(a)
    '                                Exit For
    '                            End If
    '                        End If
    '                    Next
    '                Next
    '                dtTemp.AcceptChanges()
    '                CompuniqueRow = dtTemp.Copy
    '                Uniquecompdt1.DataSource = CompuniqueRow
    '                Uniquecompdt1.DataBind()
    '            End If
    '        Catch ex As Exception
    '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
    '        End Try
    '    End Sub

    Protected Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim Uniquecompdt As New DataTable
        Dim sDateofPur As String = ""
        Dim sString As String
        Dim sAmt As String
        Try
            If dtpublic Is Nothing Then
                lblCustomerValidationMsg.Text = "Click Go Button"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASBankValidation').modal('show');", True)
                Exit Sub
            End If
            dt.Columns.Add("SlrNo")
            dt.Columns.Add("SerialNo")
            dt.Columns.Add("TrType")
            dt.Columns.Add("TransactionNo")
            dt.Columns.Add("TxnDate")
            dt.Columns.Add("ChequeNo")
            dt.Columns.Add("ChequeDate")
            dt.Columns.Add("IFSCCODEBNK")
            dt.Columns.Add("BankName")
            dt.Columns.Add("BranchName")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Description")
            dt.Columns.Add("RefNo")
            dt.Columns.Add("BranchCode")
            dt.Columns.Add("Balance")
            dt.Columns.Add("Status")
            dt.Columns.Add("DabitDiff")
            dt.Columns.Add("CreditDiff")
            dt.Columns.Add("CDebit")
            dt.Columns.Add("CCredit")
            dt.Columns.Add("BCredit")
            dt.Columns.Add("BDebit")
            dt.Columns.Add("Creditresult")
            dt.Columns.Add("Debitresult")
            dt.Columns.Add("CompDebitresult")
            dt.Columns.Add("CompCreditresult")
            dt.Columns.Add("BillType")
            dt.Columns.Add("Opening_Bal")

            If ddlSheetName.SelectedIndex > 0 Then
                dtStock = LoadExcelTable(sFile)
                For i = 0 To dtStock.Rows.Count - 1
                    Dim dRow As DataRow
                    dRow = dt.NewRow
                    If IsDBNull(dtStock.Rows(i).Item("SlrNo")) = False Then
                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            dRow("SlrNo") = dtStock.Rows(i).Item("SlrNo")
                        End If
                    End If
                    If IsDBNull(dtStock.Rows(i).Item("SerialNo")) = False Then
                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            dRow("SerialNo") = dtStock.Rows(i).Item("SerialNo")
                        End If
                    End If
                    If IsDBNull(dtStock.Rows(i).Item("TrType")) = False Then
                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            dRow("TrType") = dtStock.Rows(i).Item("TrType")
                        End If
                    End If

                    If IsDBNull(dtStock.Rows(i).Item("TransactionNo")) = False Then
                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            dRow("TransactionNo") = dtStock.Rows(i).Item("TransactionNo")
                        End If
                    End If


                    If Trim(dtStock.Rows(i).Item("TxnDate").ToString()) = "" Then
                        dRow("TxnDate") = ""
                    Else
                        sDateofPur = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("TxnDate"), "D")
                        'Date.ParseExact(dtStock.Rows(i).Item("TxnDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        If Datecheck(sDateofPur) = False Then
                            lblError.Text = "Enter Valid TxnDate"
                            Exit Sub
                        Else
                            dRow("TxnDate") = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("TxnDate"), "D")
                        End If
                    End If
                    If IsDBNull(dtStock.Rows(i).Item("ChequeNo")) = False Then
                        If dtStock.Rows(i).Item("ChequeNo").ToString <> "&nbsp;" Then
                            dRow("ChequeNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("ChequeNo").ToString())
                        End If
                    End If

                    If Trim(dtStock.Rows(i).Item("ChequeDate").ToString()) = "" Then
                        dRow("ChequeDate") = ""
                    Else
                        sDateofPur = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("ChequeDate"), "D")
                        'Date.ParseExact(dtStock.Rows(i).Item("ChequeDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        If Datecheck(sDateofPur) = False Then
                            lblError.Text = "Enter Valid ChequeDate"
                            Exit Sub
                        Else
                            dRow("ChequeDate") = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("ChequeDate"), "D")
                        End If
                    End If

                    If Trim(dtStock.Rows(i).Item("IFSCCODEBNK").ToString()) = "" Then
                        dRow("IFSCCODEBNK") = ""
                    Else
                        sDateofPur = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("IFSCCODEBNK"), "D")
                        'Date.ParseExact(dtStock.Rows(i).Item("IFSCCODEBNK"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        If Datecheck(sDateofPur) = False Then
                            lblError.Text = "Enter Valid ValueDate"
                            Exit Sub
                        Else
                            dRow("IFSCCODEBNK") = clsgeneral.FormatDtForRDBMS(dtStock.Rows(i).Item("IFSCCODEBNK"), "D")
                        End If
                    End If
                    If Trim(dtStock.Rows(i).Item("BankName").ToString()) = "" Then
                        dRow("BankName") = ""
                    Else
                        sString = dtStock.Rows(i).Item("BankName")
                        If Stringcheck(sString) = False Then
                            lblError.Text = "Enter Valid BankName no special characters are allowed"
                            Exit Sub
                        Else
                            dRow("BankName") = objClsFASGnrl.SafeSQL(dtStock.Rows(i).Item("BankName"))
                        End If
                    End If

                    If Trim(dtStock.Rows(i).Item("BranchName").ToString()) = "" Then
                        dRow("BranchName") = ""
                    Else
                        sString = dtStock.Rows(i).Item("BranchName")
                        If Stringcheck(sString) = False Then
                            lblError.Text = "Enter Valid BranchName no special characters are allowed"
                            Exit Sub
                        Else
                            dRow("BranchName") = objClsFASGnrl.SafeSQL(dtStock.Rows(i).Item("BranchName"))
                        End If
                    End If

                    If dtStock.Rows(i).Item("Debit").ToString() = "" Or dtStock.Rows(i).Item("Debit") = 0 Then
                        dRow("Debit") = "0.00"
                    Else
                        dRow("Debit") = String.Format("{0:0.00}", Convert.ToDecimal(dtStock.Rows(i)("Debit")))
                    End If

                    If dtStock.Rows(i).Item("Credit").ToString() = "" Or dtStock.Rows(i).Item("Credit") = 0 Then
                        dRow("Credit") = "0.00"
                    Else
                        dRow("Credit") = String.Format("{0:0.00}", Convert.ToDecimal(dtStock.Rows(i)("Credit")))
                    End If

                    If IsDBNull(dtStock.Rows(i).Item("Description")) = False Then
                        If dtStock.Rows(i).Item("Description").ToString <> "&nbsp;" Then
                            dRow("Description") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("Description"))
                        End If
                    End If

                    If IsDBNull(dtStock.Rows(i).Item("RefNo")) = False Then
                        If dtStock.Rows(i).Item("RefNo").ToString <> "&nbsp;" Then
                            dRow("RefNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("RefNo"))
                        End If
                    End If

                    If IsDBNull(dtStock.Rows(i).Item("BranchCode")) = False Then
                        If dtStock.Rows(i).Item("BranchCode").ToString <> "&nbsp;" Then
                            dRow("BranchCode") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("BranchCode"))
                        End If
                    End If
                    If IsDBNull(dtStock.Rows(i).Item("Balance")) = False Then
                        dRow("Balance") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("Balance"))
                    Else
                        dRow("Balance") = ""
                    End If
                    If IsDBNull(dtStock.Rows(i).Item("BillType")) = False Then
                        dRow("BillType") = clsgeneral.SafeSQL(dtStock.Rows(i).Item("BillType"))
                    Else
                        dRow("BillType") = ""
                    End If

                    If dtStock.Rows(i).Item("Opening_Bal").ToString() = "" Then
                        dRow("Opening_Bal") = "0.000"
                    Else
                        sAmt = dtStock.Rows(i).Item("Opening_Bal")
                        If Amountcheck(sAmt) = False Then
                            lblError.Text = "Enter Valid Opening Balance No Special Characters Alowed"
                            Exit Sub
                        Else
                            dRow("Opening_Bal") = objClsFASGnrl.SafeSQL(dtStock.Rows(i).Item("Opening_Bal"))
                        End If

                    End If

                    dRow("CreditDiff") = "0.00"
                    dRow("DabitDiff") = "0.00"
                    dRow("CDebit") = "0.00"
                    dRow("CCredit") = "0.00"
                    dRow("BCredit") = "0.00"
                    dRow("BDebit") = "0.00"
                    dRow("Creditresult") = "0.00"
                    dRow("Debitresult") = "0.00"
                    dRow("CompDebitresult") = "0.00"
                    For j = 0 To dtpublic.Rows.Count - 1
                        If ((dtpublic.Rows(j).Item("Acc_PM_ChequeNo").ToString()) <> "" And (dtStock.Rows(i).Item("ChequeNo").ToString()) <> "") Then    'If check no present in both datatable
                            If ((dtpublic.Rows(j).Item("Acc_PM_ChequeNo").ToString()) = dtStock.Rows(i)("ChequeNo").ToString()) Then
                                'If (((dtpublic.Rows(j).Item("ATD_Debit")).ToString() = dRow("Debit")) And ((dtpublic.Rows(j).Item("ATD_Credit")).ToString() = dRow("Credit")) And ((dtpublic.Rows(j).Item("ATD_TransactionDate")) = dRow("TxnDate"))) Then
                                If (((dtpublic.Rows(j).Item("ATD_Debit")).ToString() = dRow("Credit")) And ((dtpublic.Rows(j).Item("ATD_Credit")).ToString() = dRow("Debit"))) Then
                                    dRow("Status") = "EAM"
                                    dRow("CCredit") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Credit")).ToString("#,##0.00")
                                    dRow("CDebit") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Debit")).ToString("#,##0.00")
                                    dRow("BDebit") = Convert.ToDecimal(dtStock.Rows(i)("Debit")).ToString("#,##0.00")
                                    dRow("BCredit") = Convert.ToDecimal(dtStock.Rows(i)("Credit")).ToString("#,##0.00")
                                    objBank.DeleteMAtchedINOUTData(sSession.AccessCode, sSession.AccessCodeID, dtpublic.Rows(j).Item("SrNo"))
                                    GoTo Skip
                                Else
                                    dRow("Status") = "EAN"
                                    dRow("CCredit") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Credit")).ToString("#,##0.00")
                                    dRow("CDebit") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Debit")).ToString("#,##0.00")
                                    dRow("BDebit") = Convert.ToDecimal(dtStock.Rows(i)("Debit")).ToString("#,##0.00")
                                    dRow("BCredit") = Convert.ToDecimal(dtStock.Rows(i)("Credit")).ToString("#,##0.00")
                                    dRow("DabitDiff") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Debit").ToString()) - Convert.ToDecimal(dtStock.Rows(i)("Debit")).ToString("#,##0.00")
                                    dRow("CreditDiff") = Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Credit").ToString()) - Convert.ToDecimal(dtStock.Rows(i)("Credit")).ToString("#,##0.00")
                                    GoTo Skip
                                End If
                            End If
                        Else

                            dRow("Status") = "N"
                            dRow("BDebit") = Convert.ToDecimal(dtStock.Rows(i)("Debit")).ToString("#,##0.00")
                            dRow("BCredit") = Convert.ToDecimal(dtStock.Rows(i)("Credit")).ToString("#,##0.00")
                        End If

                    Next
Skip:               dt.Rows.Add(dRow)
                Next

                If IsNothing(dt) = True Then
                    Exit Sub
                End If

                dgBank.DataSource = dt
                dgBank.DataBind()

                If dgBank.Rows.Count > 0 Then
                    lbl_BankBook.Visible = True
                Else
                    lbl_BankBook.Visible = False
                End If

                dttable = dt.Copy

                Dim dtTemp As New DataTable
                Uniquecompdt = dtpublic.Copy
                dtTemp = dtpublic.Copy

                For a As Integer = Uniquecompdt.Rows.Count - 1 To 0 Step -1
                    For b As Integer = 0 To dt.Rows.Count - 1
                        If (Uniquecompdt.Rows(a)("Acc_PM_ChequeNo").ToString()) = Nothing Then
                        Else
                            If ((Uniquecompdt.Rows(a)("Acc_PM_ChequeNo").ToString()) = (dt.Rows(b)("chequeNo").ToString())) Then
                                dtTemp.Rows.RemoveAt(a)
                                Exit For
                            End If
                        End If
                    Next
                Next
                dtTemp.AcceptChanges()
                CompuniqueRow = dtTemp.Copy
                Uniquecompdt1.DataSource = CompuniqueRow
                Uniquecompdt1.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadExcelTable(ByVal sFile As String) As DataTable
        Dim dbhelper As New DBHelper
        Dim dt As New DataTable
        Dim dtStock1 As New DataTable
        Dim val As Integer = 0
        Try
            dt.Columns.Add("SlrNo")
            dt.Columns.Add("SerialNo")
            dt.Columns.Add("TrType")
            dt.Columns.Add("TransactionNo")
            dt.Columns.Add("TxnDate")
            dt.Columns.Add("ChequeNo")
            dt.Columns.Add("ChequeDate")
            dt.Columns.Add("IFSCCODEBNK")
            dt.Columns.Add("BankName")
            dt.Columns.Add("BranchName")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Description")
            dt.Columns.Add("RefNo")
            dt.Columns.Add("BranchCode")
            dt.Columns.Add("Balance")
            dt.Columns.Add("Status")
            dt.Columns.Add("BillType")
            dt.Columns.Add("Opening_Bal")
            dtStock = dbhelper.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                Return dtStock
            End If

            For i = 0 To dtStock.Rows.Count - 1
                Dim dRow As DataRow
                dRow = dt.NewRow
                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("SlrNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(0))
                    End If
                Else
                    dRow("SlrNo") = "0"
                End If
                If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("SerialNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(1))
                    End If
                Else
                    dRow("SerialNo") = "0"
                End If

                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("TrType") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(2))
                    End If
                End If

                If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                    If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("TransactionNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(3))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                    If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
                        dRow("TxnDate") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(4))

                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                    If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                        dRow("ChequeNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(5).ToString())
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                    If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
                        dRow("ChequeDate") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(6))
                    End If
                End If

                If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                    If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
                        dRow("IFSCCODEBNK") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(7))
                    End If
                End If

                If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                    If dtStock.Rows(i).Item(8).ToString <> "&nbsp;" Then
                        dRow("BankName") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(8))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
                    If dtStock.Rows(i).Item(9).ToString <> "&nbsp;" Then
                        dRow("BranchName") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(9))
                    End If
                End If

                If IsDBNull(dtStock.Rows(i).Item(10)) = False Then
                    If dtStock.Rows(i).Item(10).ToString <> "&nbsp;" Then
                        dRow("Debit") = dtStock.Rows(i).Item(10)
                    Else
                        dRow("Debit") = "0.00"
                    End If
                Else
                    dRow("Debit") = "0.00"
                End If

                If IsDBNull(dtStock.Rows(i).Item(11)) = False Then
                    If dtStock.Rows(i).Item(11).ToString <> "&nbsp;" Then
                        dRow("Credit") = dtStock.Rows(i).Item(11)
                    Else
                        dRow("Credit") = "0.00"
                    End If
                Else
                    dRow("Credit") = "0.00"
                End If
                If IsDBNull(dtStock.Rows(i).Item(12)) = False Then
                    If dtStock.Rows(i).Item(12).ToString <> "&nbsp;" Then
                        dRow("Description") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(12))
                    End If
                End If

                If IsDBNull(dtStock.Rows(i).Item(13)) = False Then
                    If dtStock.Rows(i).Item(13).ToString <> "&nbsp;" Then
                        dRow("RefNo") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(13))
                    End If
                End If

                If IsDBNull(dtStock.Rows(i).Item(14)) = False Then
                    If dtStock.Rows(i).Item(14).ToString <> "&nbsp;" Then
                        dRow("BranchCode") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(14))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(15)) = False Then
                    dRow("Balance") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(15))
                Else
                    dRow("Balance") = 0
                End If
                If IsDBNull(dtStock.Rows(i).Item(16)) = False Then
                    dRow("BillType") = clsgeneral.SafeSQL(dtStock.Rows(i).Item(16))
                Else
                    dRow("BillType") = 0
                End If
                If IsDBNull(dtStock.Rows(i).Item(17)) = False Then
                    If dtStock.Rows(i).Item(17).ToString <> "&nbsp;" Then
                        dRow("Opening_Bal") = dtStock.Rows(i).Item(17)
                    Else
                        dRow("Opening_Bal") = "0.000"
                    End If
                Else
                    dRow("Opening_Bal") = "0.000"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function LoadFromCompany(ByVal ibank As String, ByVal dfrom As String, ByVal dto As String) As DataTable
        Try
            dtpublic = objBank.LoadCompanyAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ibank, dfrom, dto)
            dgCbook.DataSource = dtpublic
            dgCbook.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function LoadFromCompanyb(ByVal ibank As String, ByVal dfrom As String, ByVal dto As String) As DataTable
        Try
            lblError.Text = ""
            dtpublic = objBank.LoadCompanyAccountsb(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ibank, dfrom, dto)
            dgCbook.DataSource = dtpublic
            dgCbook.DataBind()
            If dgCbook.Rows.Count > 0 Then
                lbl_CompBook.Visible = True
            Else
                lbl_CompBook.Visible = False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function LoadBank() As DataTable
        Try
            ddlBank.DataSource = objBank.LoadBankD(sSession.AccessCode, sSession.AccessCodeID)
            ddlBank.DataTextField = "gl_desc"
            ddlBank.DataValueField = "gl_id"
            ddlBank.DataBind()
            ddlBank.Items.Insert(0, "Select Bank")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Protected Sub ddlBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBank.SelectedIndexChanged
        Try
            dgBank.DataSource = Nothing
            dgBank.DataBind()
            dgCbook.DataSource = Nothing
            dgCbook.DataBind()
            txtfrom.Text = "" : txtto.Text = ""
            ddlSheetName.Items.Clear()
            If ddlBank.SelectedIndex > 0 Then
                LoadBranch(ddlBank.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBank_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadBranch(ByVal iBankID As Integer)
        Dim dt As New DataTable
        Try
            dt = objBank.LoadBranchNames(sSession.AccessCode, sSession.AccessCodeID, iBankID)
            ddlBrnch.DataTextField = "BD_BranchName"
            ddlBrnch.DataValueField = "BD_ID"
            ddlBrnch.DataSource = dt
            ddlBrnch.DataBind()
            ddlBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub lnDown_Click(sender As Object, e As EventArgs) Handles lnDown.Click
        Try
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=BANKRECONCILIATION.xlsx")
            Response.TransmitFile(Server.MapPath("~\SampleExcels\BANKRECONCILIATION5.xlsx"))
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnDown_Click")
        End Try
    End Sub
    Protected Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            ddlBank.SelectedIndex = 0 : ddlBrnch.SelectedIndex = 0 : txtfrom.Text = "" : txtto.Text = ""
            dgBank.DataSource = "" : dgBank.DataBind() : dgCbook.DataSource = "" : dgCbook.DataBind()
            dgMatchedRows.DataSource = "" : dgMatchedRows.DataBind() : Uniquecompdt1.DataSource = "" : Uniquecompdt1.DataBind()
            lblReconcilation.Visible = False : lblRed.Visible = False : lblgreen.Visible = False
            lblgreen1.Visible = False : lblRed1.Visible = False
            lbl_CompBook.Text = "" : lbl_BankBook.Text = "" : lblGreenDesc.Text = "" : lblRedDesc.Text = "" : lblNoColr.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub
    Protected Sub dgMatchedRows_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgMatchedRows.RowCommand
        Dim lblStatus, lblDebit, lblCredit, lblSerialNo, lblTrType As New Label
        Dim lblCDebit, lblCCredit As New Label
        Dim dt As New DataTable
        Dim TxnDate As New Label
        Dim TxnDate1 As New DateTime
        Dim postdate, postdate1 As String
        Dim btnpost As Button
        Dim imgbtnEdit As ImageButton
        Try
            lblError.Text = ""
            lblExcelValidationMsg.Text = ""
            If e.CommandName.Equals("POST") Then
                Dim Row As GridViewRow = TryCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
                lblStatus = DirectCast(Row.FindControl("lblStatus"), Label)
                btnpost = DirectCast(Row.FindControl("btnpost"), Button)
                If lblStatus.Text = "C" Then 'Data Not exist in bank which is present in company
                    lblSerialNo = DirectCast(Row.FindControl("SerialNo"), Label)
                    TxnDate = DirectCast(Row.FindControl("TxnDate"), Label)
                    TxnDate1 = TxnDate.Text
                    postdate = TxnDate1.AddDays(3)
                    postdate1 = Date.ParseExact(postdate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'objBank.PosttonextMonth(sSession.AccessCode, sSession.AccessCodeID, lblStatus.Text, postdate, lblSerialNo.Text)
                    objBank.PosttonextMonth(sSession.AccessCode, sSession.AccessCodeID, lblStatus.Text, postdate1, lblSerialNo.Text)
                Else
                    btnpost.Enabled = False
                End If
            ElseIf e.CommandName.Equals("EditRow") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblStatus = DirectCast(clickedRow.FindControl("lblStatus"), Label)
                imgbtnEdit = DirectCast(clickedRow.FindControl("imgbtnEdit"), ImageButton)
                If lblStatus.Text = "EAN" Or lblStatus.Text = "EAM" Or lblStatus.Text = "N" Then
                    imgbtnEdit.Enabled = False
                    'ElseIf lblStatus.Text = "W" Or lblStatus.Text = "B" Or lblStatus.Text = "C" Then
                ElseIf lblStatus.Text = "W" Or lblStatus.Text = "B" Then 'B amount unmatched data
                    imgbtnEdit.Enabled = True
                    lblSerialNo = DirectCast(clickedRow.FindControl("SerialNo"), Label)
                    lblBANKID.Text = lblSerialNo.Text
                    lblTrType = DirectCast(clickedRow.FindControl("TrType"), Label)
                    lblTrtypes.Text = lblTrType.Text
                    lblDebit = DirectCast(clickedRow.FindControl("Debit"), Label) 'Bank debit and credit
                    lblCredit = DirectCast(clickedRow.FindControl("Credit"), Label)
                    lblCDebit = DirectCast(clickedRow.FindControl("lblCDebit"), Label) 'Company debit and credit
                    lblCCredit = DirectCast(clickedRow.FindControl("lblCCredit"), Label)
                    txtCredit.Text = lblCredit.Text
                    txtdebit.Text = lblDebit.Text
                    txtCompCredit.Text = lblCCredit.Text
                    txtcompdebit.Text = lblCDebit.Text
                    txtbedidiff.Text = lblDebit.Text - lblCDebit.Text
                    txtcredbdiff.Text = lblCredit.Text - lblCCredit.Text
                    If (lblCCredit.Text <> "0.00" Or lblCDebit.Text <> "0.00") Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    End If
                Else
                    lblExcelValidationMsg.Text = "Edit only UN-Matched Data" : lblError.Text = "Edit only UN-Matched Data"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgMatchedRows_RowCommand")
        End Try
    End Sub
    Protected Sub btnCompare_Click(sender As Object, e As EventArgs) Handles btnCompare.Click
        Try

            If dttable Is Nothing Then
                If dttabledatacapture1 Is Nothing Then
                    Exit Sub
                ElseIf dttabledatacapture1.Rows.Count > 0 Then
                    lblGreenDesc.Visible = True
                    lblRedDesc.Visible = True
                    lblNoColr.Visible = True

                    lblReconcilation.Visible = True
                    lblRed.Visible = True
                    lblgreen.Visible = True
                    If lblgreen.Visible = True Then
                        lblgreen1.Visible = True
                    End If
                    If lblRed.Visible = True Then
                        lblRed1.Visible = True
                    End If
                    dgMatchedRows.DataSource = dttabledatacapture1
                    dgMatchedRows.DataBind()
                    For i = 0 To dttabledatacapture1.Rows.Count - 1
                        If (dttabledatacapture1.Rows(i).Item("Status").ToString = "EAM") Then
                            dgMatchedRows.Rows(i).BackColor = Drawing.Color.Green
                        ElseIf (dttabledatacapture1.Rows(i).Item("Status").ToString = "EAN") Then
                            dgMatchedRows.Rows(i).BackColor = Drawing.Color.Red
                        End If
                    Next
                End If
            ElseIf dttable.Rows.Count > 0 Then
                lblGreenDesc.Visible = True
                lblRedDesc.Visible = True
                lblNoColr.Visible = True

                lblReconcilation.Visible = True
                lblRed.Visible = True
                lblgreen.Visible = True
                If lblgreen.Visible = True Then
                    lblgreen1.Visible = True
                End If
                If lblRed.Visible = True Then
                    lblRed1.Visible = True
                End If
                dgMatchedRows.DataSource = dttable
                dgMatchedRows.DataBind()
                dttable1 = dttable.Copy
                For i = 0 To dttable.Rows.Count - 1
                    If (dttable.Rows(i).Item("Status").ToString = "EAM") Then
                        dgMatchedRows.Rows(i).BackColor = Drawing.Color.Green
                    ElseIf (dttable.Rows(i).Item("Status").ToString = "EAN") Then
                        dgMatchedRows.Rows(i).BackColor = Drawing.Color.Red
                    End If
                Next
            Else
                lblExcelValidationMsg.Text = "No Data" : lblError.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCompare_Click")
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim dFromDate, dTodate As String
        Try
            lblError.Text = ""
            If ddlBank.SelectedIndex = 0 Or ddlBrnch.SelectedIndex = -1 Then
                'btnGo.Attributes.Add("onclick", "return clientfunction();")
                Me.btnGo.Attributes.Add("OnClick", "javascript:return clientfunction();")
            Else
                If txtfrom.Text <> "" Then
                    dFromDate = Date.ParseExact(txtfrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    dFromDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                If txtto.Text <> "" Then
                    dTodate = Date.ParseExact(txtto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    dTodate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                'LoadFromCompanyb(ddlBank.SelectedValue, txtfrom.Text, txtto.Text)
                LoadFromCompanyb(ddlBank.SelectedValue, dFromDate, dTodate)
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGo_Click")
            Throw
        End Try
    End Sub

    Private Sub dgMatchedRows_PreRender(sender As Object, e As EventArgs) Handles dgMatchedRows.PreRender
        Try
            If dgMatchedRows.Rows.Count > 0 Then
                dgMatchedRows.UseAccessibleHeader = True
                dgMatchedRows.HeaderRow.TableSection = TableRowSection.TableHeader
                dgMatchedRows.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgMatchedRows_PreRender")
        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr As Array
        Dim brCode As String
        Dim SerialNo, TrType, TransactionNo, TxnDate, ChequeNo, ChequeDate, ValueDate As New Label
        Dim lblBNKIFSCCODE, BankName, BranchName, Debit, Credit, lblCCredit, lblCDebit, Description, RefNo, BranchCode, Balance, lblStatus, BillTypelbl As New Label
        '///Company data
        Dim lblSrNo, lblSerialNo, lblTrtype, lblParty, lblTransactionNo, lblTransactionDate, lblChequeNo, lblChequeDate As New Label
        Dim lblIFSCCode, lblBankName, lblBranchName, lblDebit, lblCredit, lblflag, lblbilltype1, lblDebitresult, lblCreditresult As New Label
        Dim ImasterID As Int32
        Try
            If Uniquecompdt1.Rows.Count = Nothing And dgMatchedRows.Rows.Count = Nothing Then
                lblCustomerValidationMsg.Text = "Click Compare Button"
                'lblError.Text = "Click Compare Button"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASBankValidation').modal('show');", True)
            Else
                If ddlBank.SelectedValue <> "" Then
                    objBank.iABR_Bank = ddlBank.SelectedValue
                Else
                    objBank.iABR_Bank = 0
                End If
                objBank.iABR_CreatedBy = sSession.UserID
                objBank.iABR_UpdatedBy = sSession.UserID
                objBank.iABR_YearID = sSession.YearID
                objBank.iABR_CompID = sSession.AccessCodeID

                If ddlBrnch.SelectedValue <> "" Then
                    If ddlBrnch.SelectedIndex > 0 Then
                        objBank.iABRM_BankBranch = ddlBrnch.SelectedValue
                    Else
                        objBank.iABRM_BankBranch = 0
                    End If
                Else
                    objBank.iABRM_BankBranch = 0
                End If

                objBank.iABRM_CreatedBy = sSession.UserID
                objBank.dABRM_CreatedOn = DateTime.Today
                objBank.sABRM_Status = "W"
                objBank.sABRM_Operation = "C"
                objBank.sABRM_IPAddress = sSession.IPAddress
                objBank.iABRM_YearID = sSession.YearID
                objBank.iABRM_CompID = sSession.AccessCodeID

                brCode = objBank.GenerateReconciliationCode(sSession.AccessCode, sSession.AccessCodeID)

                objBank.sABRM_BRID = brCode

                'ImasterID = objBank.SaveBankReconciliationMaster(sSession.AccessCode, sSession.AccessCodeID, objBank)
                Arr = objBank.SaveBankReconciliation2(sSession.AccessCode, sSession.AccessCodeID, objBank)
                ImasterID = Arr(1)
                txtmasterID.Text = ImasterID
                objBank.iABRM_ID = txtmasterID.Text


                For j = 0 To Uniquecompdt1.Rows.Count - 1
                    lblSrNo = Uniquecompdt1.Rows(j).FindControl("lblSrNo")
                    lblSerialNo = Uniquecompdt1.Rows(j).FindControl("lblSerialNo")
                    lblTrtype = Uniquecompdt1.Rows(j).FindControl("lblTrtype")
                    lblParty = Uniquecompdt1.Rows(j).FindControl("lblParty")
                    lblTransactionNo = Uniquecompdt1.Rows(j).FindControl("lblTransactionNo")
                    lblTransactionDate = Uniquecompdt1.Rows(j).FindControl("lblTransactionDate")
                    lblChequeNo = Uniquecompdt1.Rows(j).FindControl("lblChequeNo")
                    lblChequeDate = Uniquecompdt1.Rows(j).FindControl("lblChequeDate")
                    lblIFSCCode = Uniquecompdt1.Rows(j).FindControl("lblIFSCCode")
                    lblBankName = Uniquecompdt1.Rows(j).FindControl("lblBankName")
                    lblBranchName = Uniquecompdt1.Rows(j).FindControl("lblBranchName")
                    lblDebit = Uniquecompdt1.Rows(j).FindControl("lblDebit")
                    lblCredit = Uniquecompdt1.Rows(j).FindControl("lblCredit")

                    BillTypelbl = Uniquecompdt1.Rows(j).FindControl("BillTypelbl")

                    If BillTypelbl.Text <> "" Then
                        objBank.sABR_Vouchertype = BillTypelbl.Text
                    Else
                        objBank.sABR_Vouchertype = ""
                    End If


                    If lblSerialNo.Text <> "" Then
                        objBank.sABR_SerialNo = lblSerialNo.Text
                    Else
                        objBank.sABR_SerialNo = 0
                    End If
                    If lblTrtype.Text <> "" Then
                        If lblTrtype.Text = "PAYMENT" Then
                            objBank.iABR_TrType = 1
                        ElseIf lblTrtype.Text = "RECEIPT" Then
                            objBank.iABR_TrType = 3
                        End If
                    End If
                    If lblParty.Text <> "" Then
                        objBank.iABR_Party = lblParty.Text
                    Else
                        objBank.iABR_Party = 0
                    End If
                    If lblTransactionNo.Text <> "" Then
                        objBank.sABR_TransactionNo = lblTransactionNo.Text
                    Else
                        objBank.sABR_TransactionNo = 0
                    End If

                    If lblTransactionDate.Text <> "" Then
                        objBank.dABR_TransactionDate = Date.ParseExact(objClsFASGnrl.FormatDtForRDBMS(lblTransactionDate.Text, "D"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objBank.dABR_TransactionDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                    If lblChequeNo.Text <> "" Then
                        objBank.sABR_ChequeNo = lblChequeNo.Text
                    Else
                        objBank.sABR_ChequeNo = ""
                    End If
                    If lblChequeDate.Text <> "" Then
                        objBank.dABR_ChequeDate = Date.ParseExact(objClsFASGnrl.FormatDtForRDBMS(lblChequeDate.Text, "D"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objBank.dABR_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                    If lblIFSCCode.Text <> "" Then
                        objBank.sABR_IFSCCode = lblIFSCCode.Text
                    Else
                        objBank.sABR_IFSCCode = lblIFSCCode.Text
                    End If

                    If lblDebit.Text <> "" Then
                        objBank.dABR_CDabit = Convert.ToDecimal(lblDebit.Text).ToString("#,##0.00")
                    Else
                        objBank.dABR_CDabit = "0.00"
                    End If
                    If lblCredit.Text <> "" Then
                        objBank.dABR_CCradit = Convert.ToDecimal(lblCredit.Text).ToString("#,##0.00")
                    Else
                        objBank.dABR_CCradit = "0.00"
                    End If
                    objBank.sABR_Status = "C" 'amount not exist in Bank

                    If txtfrom.Text <> "" Then
                        objBank.dABR_FromDate = Date.ParseExact(txtfrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objBank.dABR_FromDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                    If txtto.Text <> "" Then
                        objBank.dABR_ToDate = Date.ParseExact(txtto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    Else
                        objBank.dABR_ToDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                    objBank.dABR_ValueDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objBank.dABR_Debit = "0.00"
                    objBank.dABR_Credit = "0.00"
                    objBank.sABR_Description = ""
                    objBank.sABR_RefNo = ""

                    If ddlBrnch.SelectedValue <> "" Then
                        If ddlBrnch.SelectedIndex > 0 Then
                            objBank.sABR_BranchCode = ddlBrnch.SelectedItem.Text
                        Else
                            objBank.sABR_BranchCode = ""
                        End If
                    Else
                        objBank.sABR_BranchCode = ""
                    End If

                    objBank.dABR_PostedDateIO = DateTime.Today
                    objBank.iABRM_ID = txtmasterID.Text
                    Arr = objBank.SaveBankReconciliation3(sSession.AccessCode, sSession.AccessCodeID, objBank)
                Next

                For i = 0 To dgMatchedRows.Rows.Count - 1
                    lblbilltype1 = dgMatchedRows.Rows(i).FindControl("lblbilltype1")
                    TransactionNo = dgMatchedRows.Rows(i).FindControl("TransactionNo")
                    Debit = dgMatchedRows.Rows(i).FindControl("Debit")
                    Credit = dgMatchedRows.Rows(i).FindControl("Credit")
                    lblStatus = dgMatchedRows.Rows(i).FindControl("lblStatus")
                    lblCCredit = dgMatchedRows.Rows(i).FindControl("lblCCredit")
                    lblCDebit = dgMatchedRows.Rows(i).FindControl("lblCDebit")
                    lblBNKIFSCCODE = dgMatchedRows.Rows(i).FindControl("lblBNKIFSCCODE")
                    lblDebitresult = dgMatchedRows.FooterRow.FindControl("lblDebitresult")
                    lblCreditresult = dgMatchedRows.FooterRow.FindControl("lblCreditresult")

                    If lblDebitresult.Text <> "" Then
                        objBank.dABR_ClosingBal = Convert.ToDecimal(lblDebitresult.Text).ToString("#,##0.00")
                    ElseIf lblCreditresult.Text <> "" Then
                        objBank.dABR_ClosingBal = Convert.ToDecimal(lblCreditresult.Text).ToString("#,##0.00")
                    Else
                        objBank.dABR_ClosingBal = "0.000"
                    End If

                    If txtfrom.Text <> "" Then
                        objBank.dABR_FromDate = Date.ParseExact(txtfrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objBank.dABR_FromDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    If txtto.Text <> "" Then
                        objBank.dABR_ToDate = Date.ParseExact(txtto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objBank.dABR_ToDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If


                    If TransactionNo.Text <> "" Then
                        objBank.sABR_TransactionNo = TransactionNo.Text
                    Else
                        objBank.sABR_TransactionNo = ""
                    End If


                    If lblbilltype1.Text <> "" Then
                        objBank.sABR_Vouchertype = lblbilltype1.Text
                    Else
                        objBank.sABR_Vouchertype = ""
                    End If

                    SerialNo = dgMatchedRows.Rows(i).FindControl("SerialNo")
                    If SerialNo.Text <> "" Then
                        objBank.sABR_SerialNo = SerialNo.Text
                    Else
                        objBank.sABR_SerialNo = ""
                    End If

                    TrType = dgMatchedRows.Rows(i).FindControl("TrType")
                    If TrType.Text = "PAYMENT" Then
                        objBank.iABR_TrType = 1
                    ElseIf TrType.Text = "RECEIPT" Then
                        objBank.iABR_TrType = 3
                    End If

                    TxnDate = dgMatchedRows.Rows(i).FindControl("TxnDate")
                    If TxnDate.Text <> "" Then
                        objBank.dABR_TransactionDate = Date.ParseExact(TxnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    Else
                        objBank.dABR_TransactionDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                    ChequeNo = dgMatchedRows.Rows(i).FindControl("ChequeNo")
                    If ChequeNo.Text <> "" Then
                        objBank.sABR_ChequeNo = ChequeNo.Text
                    Else
                        objBank.sABR_ChequeNo = ""
                    End If

                    ChequeDate = dgMatchedRows.Rows(i).FindControl("ChequeDate")
                    If ChequeDate.Text <> "" Then
                        objBank.dABR_ChequeDate = Date.ParseExact(ChequeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objBank.dABR_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                    If lblBNKIFSCCODE.Text <> "" Then
                        objBank.sABR_IFSCCode = lblBNKIFSCCODE.Text
                    Else
                        objBank.sABR_IFSCCode = ""
                    End If

                    If lblBNKIFSCCODE.Text <> "" Then
                        objBank.dABR_ValueDate = Date.ParseExact(lblBNKIFSCCODE.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        objBank.dABR_ValueDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                    If ddlBank.SelectedValue <> "" Then
                        objBank.iABR_Bank = ddlBank.SelectedValue
                    Else
                        objBank.iABR_Bank = 0
                    End If
                    If Debit.Text <> "" Then
                        objBank.dABR_Debit = Convert.ToDecimal(Debit.Text).ToString("#,##0.00")
                    Else
                        objBank.dABR_Debit = "0.00"
                    End If

                    If Credit.Text <> "" Then
                        objBank.dABR_Credit = Convert.ToDecimal(Credit.Text).ToString("#,##0.00")
                    Else
                        objBank.dABR_Credit = "0.00"
                    End If


                    If lblCCredit.Text <> "" Then
                        objBank.dABR_CCradit = Convert.ToDecimal(lblCCredit.Text).ToString("#,##0.00")
                    Else
                        objBank.dABR_CCradit = "0.00"
                    End If
                    If lblCDebit.Text <> "" Then
                        objBank.dABR_CDabit = Convert.ToDecimal(lblCDebit.Text).ToString("#,##0.00")
                    Else
                        objBank.dABR_CDabit = "0.00"
                    End If

                    objBank.iABR_JID = 0

                    Description = dgMatchedRows.Rows(i).FindControl("Description")
                    If Description.Text <> "" Then
                        objBank.sABR_Description = Description.Text
                    Else
                        objBank.sABR_Description = ""
                    End If

                    RefNo = dgMatchedRows.Rows(i).FindControl("RefNo")
                    If RefNo.Text <> "" Then
                        objBank.sABR_RefNo = RefNo.Text
                    Else
                        objBank.sABR_RefNo = ""
                    End If

                    BranchCode = dgMatchedRows.Rows(i).FindControl("BranchCode")

                    If ddlBrnch.SelectedValue <> "" Then
                        If ddlBrnch.SelectedIndex > 0 Then
                            objBank.sABR_BranchCode = ddlBrnch.SelectedItem.Text
                        Else
                            objBank.sABR_BranchCode = ""
                        End If
                    Else
                        objBank.sABR_BranchCode = ""
                    End If

                    Balance = dgMatchedRows.Rows(i).FindControl("Balance")
                    If Balance.Text <> "" Then
                        objBank.dABR_Balance = Balance.Text
                    Else
                        objBank.dABR_Balance = 0
                    End If

                    objBank.iABR_CreatedBy = sSession.UserID
                    objBank.iABR_UpdatedBy = sSession.UserID
                    objBank.iABR_YearID = sSession.YearID
                    objBank.iABR_CompID = sSession.AccessCodeID
                    objBank.iABRM_ID = txtmasterID.Text

                    If lblStatus.Text = "EAM" Then
                        objBank.sABR_Status = "A"
                        Arr = objBank.SaveBankReconciliation1(sSession.AccessCode, sSession.AccessCodeID, objBank)
                    ElseIf lblStatus.Text = "EAN" Then
                        objBank.sABR_Status = "W" 'Amount Not Matched
                        Arr = objBank.SaveBankReconciliation1(sSession.AccessCode, sSession.AccessCodeID, objBank)
                    ElseIf lblStatus.Text = "N" Then
                        objBank.sABR_Status = "B" 'Amount not exist in company
                        Arr = objBank.SaveBankReconciliation3(sSession.AccessCode, sSession.AccessCodeID, objBank)
                    Else
                        objBank.sABR_Status = ""
                        Arr = objBank.SaveBankReconciliation1(sSession.AccessCode, sSession.AccessCodeID, objBank)
                    End If
                    If Arr(0) = "2" Then
                        lblError.Text = "Successfully Updated"
                        imgbtnSave.ImageUrl = "~/Images/Add24.png"
                    ElseIf Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved"
                    End If
                Next
                ddlBank.SelectedIndex = 0 : ddlBrnch.SelectedIndex = 0 : txtfrom.Text = "" : txtto.Text = "" : btnGo.Enabled = False
                btnCompare.Enabled = False
                dgCbook.DataSource = Nothing : dgCbook.DataBind() : dgBank.DataSource = Nothing : dgBank.DataBind()
                Uniquecompdt1.DataSource = Nothing : Uniquecompdt1.DataBind() : dgMatchedRows.DataSource = Nothing : dgMatchedRows.DataBind()
                lbl_CompBook.Text = "" : lbl_BankBook.Text = "" : lblGreenDesc.Text = "" : lblRedDesc.Text = "" : lblNoColr.Text = ""
                lblReconcilation.Text = "" : lblRed.Visible = False : lblgreen.Visible = False : lblgreen1.Visible = False : lblRed1.Visible = False
                LoadReconciliationNo()
                '/// After save if we click existing reconcilation And take report gridtotal value Is Not clearing bcoz globaly declared value are nt clearing
                iBNKDebitCount = 0 : iBNKCreditCount = 0 : iCompDebitCount = 0 : iCompCreditCount = 0 : iBOpeningBal1 = 0
                iCompDebitCount1 = 0 : iCOMPCrdCount1 = 0
                iCmpDebitCount = 0 : icmpCreditCount = 0
                iJEID = 0 : Adjstedamunt = 0 : BalComp = 0 : BalBnk = 0 : TotalAmnt = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt, rowmatched_data As New DataTable
        Try
            lblError.Text = ""
            rowmatched_data = dttable1
            dt = objBank.LoadBanckSAvedDetails(sSession.AccessCode, sSession.AccessCodeID, rowmatched_data)
            If dt.Rows.Count = 0 Then
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/BankReconcilation.rdlc")
            'ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Bankreconcilation_Matched" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub


    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt, rowmatched_data As New DataTable
        Try
            lblError.Text = ""
            rowmatched_data = dttable1
            dt = objBank.LoadBanckSAvedDetails(sSession.AccessCode, sSession.AccessCodeID, rowmatched_data)
            If dt.Rows.Count = 0 Then
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/BankReconcilation.rdlc")
            ' ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Bankreconcilation_Matched" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Private Sub lnkbtnNotmatched_Excel_Click(sender As Object, e As EventArgs) Handles lnkbtnNotmatched_Excel.Click
        Dim mimeType As String = Nothing
        Dim dt, rowNotmatched_data As New DataTable
        Try
            lblError.Text = ""
            rowNotmatched_data = dttable1
            dt = objBank.LoadBanckSAvedDetailsNOtMatched(sSession.AccessCode, sSession.AccessCodeID, rowNotmatched_data)
            If rowNotmatched_data.Rows.Count = 0 Then
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/BankReconcilation.rdlc")
            'ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Bankreconcilation_notmatched" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnNotmatched_Excel_Click")
        End Try
    End Sub
    Private Sub lnkbtnNotmatched_PDF_Click(sender As Object, e As EventArgs) Handles lnkbtnNotmatched_PDF.Click
        Dim mimeType As String = Nothing
        Dim dt, rowNotmatched_data As New DataTable
        Try
            lblError.Text = ""
            rowNotmatched_data = dttable1
            dt = objBank.LoadBanckSAvedDetailsNOtMatched(sSession.AccessCode, sSession.AccessCodeID, rowNotmatched_data)
            If rowNotmatched_data.Rows.Count = 0 Then
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/BankReconcilation.rdlc")
            'ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Bankreconcilation_notmatched" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnNotmatched_PDF_Click")
        End Try
    End Sub
    Private Sub lnkbtn_notexit_excel_Click(sender As Object, e As EventArgs) Handles lnkbtn_notexit_excel.Click
        Dim mimeType As String = Nothing
        Dim dt, rowNotmatched_data As New DataTable
        Try
            lblError.Text = ""
            rowNotmatched_data = dttable1
            dt = objBank.LoadBanckSAvedDetailsNOtExit(sSession.AccessCode, sSession.AccessCodeID, rowNotmatched_data)
            If rowNotmatched_data.Rows.Count = 0 Then
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/BankReconcilation.rdlc")
            'ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Bankreconcilation_notexist" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtn_notexit_excel_Click")
        End Try
    End Sub
    Private Sub lnkbtn_notexit_Pdf_Click(sender As Object, e As EventArgs) Handles lnkbtn_notexit_Pdf.Click
        Dim mimeType As String = Nothing
        Dim dt, rowNotmatched_data As New DataTable
        Try
            lblError.Text = ""
            rowNotmatched_data = dttable1
            dt = objBank.LoadBanckSAvedDetailsNOtExit(sSession.AccessCode, sSession.AccessCodeID, rowNotmatched_data)
            If rowNotmatched_data.Rows.Count = 0 Then
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/BankReconcilation.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Bankreconcilation_notexist" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtn_notexit_Pdf_Click")
        End Try
    End Sub
    Protected Sub dgMatchedRows_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgMatchedRows.RowDataBound
        Dim lblBNKDebitSum, Debit As New Label
        Dim lblBNKCreditSum, Credit As New Label
        Dim lblCompDebitSum, lblCompCreditSum As New Label
        Dim lblCDebit, lblCCredit As New Label
        Dim BCredit As Double = 0, BDebit As Double = 0, BOpeningBal As Double = 0
        Dim CCredit As Double = 0, CDebit As Double = 0
        Dim lblCompDebitresult, lblCompCreditresult As Label
        Dim lblCreditresult, lblDebitresult, lblOpeningBal As New Label
        Dim imgbutton As ImageButton
        Dim Debit1, credit1 As String
        Debit1 = "ATD_Debit"
        credit1 = "ATD_Credit"
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbutton = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbutton.ImageUrl = "~/Images/Edit16.png"

                Debit = e.Row.FindControl("Debit")
                If Debit.Text <> "" Then
                    BDebit = BDebit + Convert.ToDecimal(Debit.Text)
                    iBNKDebitCount = iBNKDebitCount + BDebit
                End If
                Credit = e.Row.FindControl("Credit")
                If Credit.Text <> "" Then
                    BCredit = BCredit + Convert.ToDecimal(Credit.Text)
                    iBNKCreditCount = iBNKCreditCount + BCredit
                End If
                lblCDebit = e.Row.FindControl("lblCDebit")
                lblCCredit = e.Row.FindControl("lblCCredit")
                If lblCDebit.Text <> "" Then
                    CDebit = CDebit + Convert.ToDecimal(lblCDebit.Text)
                    iCompDebitCount = iCompDebitCount + CDebit
                End If
                If lblCCredit.Text <> "" Then
                    CCredit = CCredit + Convert.ToDecimal(lblCCredit.Text)
                    iCompCreditCount = iCompCreditCount + CCredit
                End If

                lblOpeningBal = e.Row.FindControl("lblOpeningBal")
                If lblOpeningBal.Text <> "" Then
                    BOpeningBal = BOpeningBal + Convert.ToDecimal(lblOpeningBal.Text)
                    iBOpeningBal1 = iBOpeningBal1 + BOpeningBal
                End If

                txtAdjComp.Text = objBank.AmountAdjustedCompany(sSession.AccessCode, sSession.AccessCodeID, Debit1, credit1)

                Adjstedamunt = txtAdjComp.Text

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                lblBNKDebitSum = e.Row.FindControl("lblBNKDebitSum")
                lblBNKDebitSum.Text = "=" + Convert.ToDecimal(iBNKDebitCount).ToString("#,##0.00") + "Dr"

                lblBNKCreditSum = e.Row.FindControl("lblBNKCreditSum")
                lblBNKCreditSum.Text = "=" + Convert.ToDecimal(iBNKCreditCount).ToString("#,##0.00") + "Cr"

                lblCompDebitSum = e.Row.FindControl("lblCompDebitSum")
                lblCompDebitSum.Text = "=" + Convert.ToDecimal(iCompDebitCount).ToString("#,##0.00") + "Dr"

                lblCompCreditSum = e.Row.FindControl("lblCompCreditSum")
                lblCompCreditSum.Text = "=" + Convert.ToDecimal(iCompCreditCount).ToString("#,##0.00") + "Cr"

                If iCompDebitCount > iCompCreditCount Then 'company
                    lblCompDebitresult = e.Row.FindControl("lblCompDebitresult")
                    lblCompDebitresult.Text = Convert.ToDecimal(iCompDebitCount - iCompCreditCount).ToString("#,##0.00")
                    txtBCbk.Text = lblCompDebitresult.Text
                    BalComp = txtBCbk.Text
                ElseIf iCompCreditCount > iCompDebitCount Then
                    'lblCompCreditresult = e.Row.FindControl("lblCompDebitresult")
                    lblCompCreditresult = e.Row.FindControl("lblCompCreditresult")
                    lblCompCreditresult.Text = Convert.ToDecimal(iCompCreditCount - iCompDebitCount).ToString("#,##0.00")
                    txtBCbk.Text = lblCompCreditresult.Text
                    BalComp = txtBCbk.Text
                End If
                If iBNKDebitCount > iBNKCreditCount Then 'Bank
                    lblDebitresult = e.Row.FindControl("lblDebitresult")
                    lblDebitresult.Text = Convert.ToDecimal(iBNKDebitCount - iBNKCreditCount + iBOpeningBal1).ToString("#,##0.00")
                    txtBBBK.Text = lblDebitresult.Text
                    'BalBnk = txtBBBK.Text + lblOpeningBal.Text
                    BalBnk = txtBBBK.Text
                    lblCreditresult.Visible = False
                ElseIf iBNKCreditCount > iBNKDebitCount Then
                    lblCreditresult = e.Row.FindControl("lblCreditresult")
                    lblCreditresult.Text = Convert.ToDecimal(iBNKCreditCount - iBNKDebitCount + iBOpeningBal1).ToString("#,##0.00")
                    txtBBBK.Text = lblCreditresult.Text
                    ' BalBnk = txtBBBK.Text + lblOpeningBal.Text
                    BalBnk = txtBBBK.Text
                    lblDebitresult.Visible = False
                End If
            End If
            txttotal.Text = Convert.ToDecimal(BalComp + Adjstedamunt).ToString("#,##0.00")
            TotalAmnt = txttotal.Text

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgMatchedRows_RowDataBound")
        End Try
    End Sub

    Private Sub ImgbtnPrint_Click(sender As Object, e As ImageClickEventArgs) Handles ImgbtnPrint.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgbtnPrint_Click")
        End Try
    End Sub

    Private Sub dgBank_PreRender(sender As Object, e As EventArgs) Handles dgBank.PreRender
        Try
            If dgBank.Rows.Count > 0 Then
                dgBank.UseAccessibleHeader = True
                dgBank.HeaderRow.TableSection = TableRowSection.TableHeader
                dgBank.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgBank_PreRender")
        End Try
    End Sub

    Private Sub dgCbook_PreRender(sender As Object, e As EventArgs) Handles dgCbook.PreRender
        Try
            If dgCbook.Rows.Count > 0 Then
                dgCbook.UseAccessibleHeader = True
                dgCbook.HeaderRow.TableSection = TableRowSection.TableHeader
                dgCbook.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCbook_PreRender")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/Accounts/BankReconciliationMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub dgCbook_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgCbook.RowDataBound
        Dim imgbutton As ImageButton
        Dim lblCOmpCreditSUm1, lblComDEbitSUM1, lblclosingBalComp As New Label
        Dim lblDebit, lblCredit As New Label
        Dim CDebit As Double = 0 : Dim CCredit1 As Double = 0
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbutton = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbutton.ImageUrl = "~/Images/Edit16.png"

                lblDebit = e.Row.FindControl("lblDebit")
                If lblDebit.Text <> "" Then
                    CDebit = CDebit + Convert.ToDecimal(lblDebit.Text)
                    iCompDebitCount1 = iCompDebitCount1 + CDebit
                End If

                lblCredit = e.Row.FindControl("lblCredit")
                If lblCredit.Text <> "" Then
                    CCredit1 = CCredit1 + Convert.ToDecimal(lblCredit.Text)
                    iCOMPCrdCount1 = iCOMPCrdCount1 + CCredit1
                End If

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                lblComDEbitSUM1 = e.Row.FindControl("lblComDEbitSUM1")
                lblComDEbitSUM1.Text = "=" + Convert.ToDecimal(iCompDebitCount1).ToString("#,##0.00") + "Dr"

                lblCOmpCreditSUm1 = e.Row.FindControl("lblCOmpCreditSUm1")
                lblCOmpCreditSUm1.Text = "=" + Convert.ToDecimal(iCOMPCrdCount1).ToString("#,##0.00") + "Cr"

                lblclosingBalComp = e.Row.FindControl("lblclosingBalComp")
                lblclosingBalComp.Text = "ClsBal=" + Convert.ToDecimal(iCompDebitCount1 - iCOMPCrdCount1).ToString("#,##0.00")
                txtclosingBal.Text = lblclosingBalComp.Text

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCbook_RowDataBound")
        End Try
    End Sub

    Private Sub dgCbook_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgCbook.RowCommand
        Dim lblIFSCCode, lblChequeNo, lblTransactionNo, lblParty, lblTransactionDate As New Label
        Dim dtrdate As Date
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            If e.CommandName.Equals("EditRow") Then
                lblIFSCCode = DirectCast(clickedRow.FindControl("lblIFSCCode"), Label)
                txtifsc.Text = lblIFSCCode.Text
                lblChequeNo = DirectCast(clickedRow.FindControl("lblChequeNo"), Label)
                txtCheque.Text = lblChequeNo.Text
                lblTransactionNo = DirectCast(clickedRow.FindControl("lblTransactionNo"), Label)
                lblsrlno.Text = lblTransactionNo.Text
                lblParty = DirectCast(clickedRow.FindControl("lblParty"), Label)
                txtParty.Text = lblParty.Text
                'lblTransactionDate = DirectCast(clickedRow.FindControl("lblTransactionDate"), Label)
                'txtTrnDate.Text = clsgeneral.FormatDtForRDBMS(lblTransactionDate.Text, "D")
                lblTransactionDate = DirectCast(clickedRow.FindControl("lblTransactionDate"), Label)
                dtrdate = Date.ParseExact(lblTransactionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                txtTrnDate.Text = clsgeneral.FormatDtForRDBMS(dtrdate, "D")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#MyCompBook').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCbook_RowCommand")
        End Try
    End Sub
    Public Sub BindReports()
        Try
            ddlCbook.Items.Insert(0, "NotExist")
            ddlCbook.Items.Insert(1, "Debit")
            ddlCbook.Items.Insert(2, "Credit")
            ddlCbook.SelectedIndex = 0
            ddlBbook.Items.Insert(0, "NotExist")
            ddlBbook.Items.Insert(1, "Debit")
            ddlBbook.Items.Insert(2, "Credit")
            ddlBbook.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnCompUpdate_Click(sender As Object, e As EventArgs) Handles btnCompUpdate.Click
        Dim chequeno As Integer
        Dim dFromDate, dTodate As String
        Dim dTrndate As String
        Try
            dTrndate = Date.ParseExact(txtTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'chequeno = objBank.UpdateChequeNo(sSession.AccessCode, sSession.AccessCodeID, txtifsc.Text, txtCheque.Text, lblsrlno.Text, txtTrnDate.Text, txtParty.Text)
            chequeno = objBank.UpdateChequeNo(sSession.AccessCode, sSession.AccessCodeID, txtifsc.Text, txtCheque.Text, lblsrlno.Text, dTrndate, txtParty.Text)
            If txtfrom.Text <> "" Then
                dFromDate = Date.ParseExact(txtfrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                dFromDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtto.Text <> "" Then
                dTodate = Date.ParseExact(txtto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                dTodate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            ' LoadFromCompanyb(ddlBank.SelectedValue, txtfrom.Text, txtto.Text)
            LoadFromCompanyb(ddlBank.SelectedValue, dFromDate, dTodate)
            If chequeno = 0 Then
                ddlSheetName_SelectedIndexChanged(sender, e)
                btnCompare_Click(sender, e)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCompUpdate_Click")
        End Try
    End Sub
    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim Cbook As String
        Dim Bbook As String
        ' Dim Bbook1 As New DataTable
        Dim dt As New DataTable
        Dim mimeType As String = Nothing
        Try
            If (ddlCbook.SelectedIndex = 0 And ddlBbook.SelectedIndex = 1) Then
                Cbook = "ABR_CDabit=0.00"
                Bbook = "ABR_Debit>0"
                'Bbook1 = objBank.LoadDebitGreaterAmount(sSession.AccessCode, sSession.AccessCodeID)
            ElseIf ((ddlCbook.SelectedIndex = 1 And ddlBbook.SelectedIndex = 0)) Then
                Cbook = "ABR_CDabit>0"
                Bbook = "ABR_Debit=0.00"
            ElseIf (ddlCbook.SelectedIndex = 0 And ddlBbook.SelectedIndex = 2) Then
                Cbook = "ABR_CCradit=0.00"
                Bbook = "ABR_Credit>0"
            ElseIf ((ddlCbook.SelectedIndex = 2 And ddlBbook.SelectedIndex = 0)) Then
                Cbook = "ABR_CCradit>0"
                Bbook = "ABR_Credit=0.00"
            End If

            dt = objBank.LoadReport(sSession.AccessCode, sSession.AccessCodeID, Cbook, Bbook)
            If dt.Rows.Count = 0 Then
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/BankReconcilation.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Bankreconcilation" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGenerate_Click")
        End Try
    End Sub

    Private Sub ddlExistinReconciliation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistinReconciliation.SelectedIndexChanged
        Dim dt, dt1 As New DataTable
        'Dim debit As String
        'Dim credit As String
        'Dim debitB As String
        'Dim creditB As String
        Try
            lblError.Text = ""
            If ddlExistinReconciliation.SelectedIndex > 0 Then
                'debit = "ABR_CDabit"
                'credit = "ABR_CCradit"
                'txtntBooks.Text = objBank.NotExistinBankBookDetail(sSession.AccessCode, sSession.AccessCodeID, ddlExistinReconciliation.SelectedValue, debit, credit)
                'debitB = "ABR_Debit"
                'creditB = "ABR_Credit"
                'txtntComp.Text = objBank.NotExistinCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistinReconciliation.SelectedValue, debitB, creditB)

                'txtmthd.Text = objBank.MatchedCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistinReconciliation.SelectedValue, debitB, creditB)
                'txtnthd.Text = objBank.UnmathedCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistinReconciliation.SelectedValue, debitB, creditB)

                lblReconcilation.Visible = True
                lblRed.Visible = True
                lblgreen.Visible = True
                If lblgreen.Visible = True Then
                    lblgreen1.Visible = True
                End If
                If lblRed.Visible = True Then
                    lblRed1.Visible = True
                End If


                FULoad.Enabled = False : txtPath.Enabled = False : btnOk.Enabled = False : ddlSheetName.Enabled = False : btnCompare.Enabled = False
                dt = objBank.LoadBanckSAvedDetailsofBank(sSession.AccessCode, sSession.AccessCodeID, ddlExistinReconciliation.SelectedValue)

                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("ABR_Bank")) = False Then
                        ddlBank.SelectedValue = dt.Rows(0)("ABR_Bank").ToString()
                    Else
                        ddlBank.SelectedValue = 0
                    End If
                    If IsDBNull(dt.Rows(0)("ABR_FromDate")) = False Then
                        txtfrom.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("ABR_FromDate").ToString(), "D")
                    Else
                        txtfrom.Text = "01/01/1999"
                    End If
                    If IsDBNull(dt.Rows(0)("ABR_ToDate")) = False Then
                        txtto.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("ABR_ToDate").ToString(), "D")
                    Else
                        txtto.Text = "01/01/1999"
                    End If
                    If IsDBNull(dt.Rows(0)("ABR_BranchCode")) = False Then
                        Dim Brnname As String = dt.Rows(0)("ABR_BranchCode").ToString()
                        LoadBranch(ddlBank.SelectedValue)
                        LoadBranchName(ddlBank.SelectedValue, Brnname)
                    Else
                        ddlBrnch.SelectedIndex = 0
                    End If
                Else
                    dt1 = objBank.LoadBanckSAvedDetailsofBank1(sSession.AccessCode, sSession.AccessCodeID, ddlExistinReconciliation.SelectedValue)
                    If IsDBNull(dt1.Rows(0)("ABR_BankIO")) = False Then
                        ddlBank.SelectedValue = dt1.Rows(0)("ABR_BankIO").ToString()
                    Else
                        ddlBank.SelectedValue = 0
                    End If
                    If IsDBNull(dt1.Rows(0)("ABR_FromDateIO")) = False Then
                        txtfrom.Text = objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(0)("ABR_FromDateIO").ToString(), "D")
                    Else
                        txtfrom.Text = "01/01/1999"
                    End If
                    If IsDBNull(dt1.Rows(0)("ABR_ToDateIO")) = False Then
                        txtto.Text = objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(0)("ABR_ToDateIO").ToString(), "D")
                    Else
                        txtto.Text = "01/01/1999"
                    End If
                    If IsDBNull(dt1.Rows(0)("ABR_BranchCodeIO")) = False Then
                        Dim Brnname As String = dt1.Rows(0)("ABR_BranchCodeIO").ToString()
                        LoadBranch(ddlBank.SelectedValue)
                        LoadBranchName(ddlBank.SelectedValue, Brnname)

                    Else
                        ddlBrnch.SelectedIndex = 0
                    End If
                End If

                ddlBank.Enabled = False : ddlBrnch.Enabled = False : txtfrom.Enabled = False : txtto.Enabled = False : btnGo.Enabled = False
                dt = objBank.LoadReconciliation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistinReconciliation.SelectedValue)
                dgMatchedRows.DataSource = dt
                dgMatchedRows.DataBind()

                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If (dt.Rows(i).Item("Status").ToString = "A") Then
                            dgMatchedRows.Rows(i).BackColor = Drawing.Color.Green
                        ElseIf (dt.Rows(i).Item("Status").ToString = "W") Then
                            dgMatchedRows.Rows(i).BackColor = Drawing.Color.Red
                        End If
                    Next
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalBRS').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistinReconciliation_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadBranchName(ByVal ibankId As String, ByVal branchname As String)
        Dim iID As Integer
        Try
            iID = objBank.LoadBranchName(sSession.AccessCode, sSession.AccessCodeID, ibankId, branchname)
            ddlBrnch.SelectedValue = iID
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim imasterID As Integer
        Dim Arr() As String
        Dim iTransID As String
        Dim lnkSerialNo As New LinkButton
        Dim dtBankDetails As New DataTable
        Dim Debit1, credit1 As String
        Debit1 = "ATD_Debit"
        credit1 = "ATD_Credit"
        Try
            lblError.Text = ""
            If chkbxJE.Checked = True Then
                objJE.iAcc_JE_ID = 0
                objJE.sAcc_JE_TransactionNo = 0
                objJE.iAcc_JE_Location = 0
                objJE.iAcc_JE_Party = 0
                objJE.iAcc_JE_BillType = 0
                objJE.sAcc_JE_ChequeNo = ""
                objJE.dAcc_JE_ChequeDate = "01/01/1900"
                objJE.sAcc_JE_IFSCCode = ""
                objJE.sAcc_JE_BankName = ""
                objJE.sAcc_JE_BranchName = ""
                objJE.sAcc_JE_AdvanceNaration = ""
                objJE.sAcc_JE_BillNo = 0
                objJE.dAcc_JE_BillDate = Date.ParseExact(Date.Today, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objJE.dAcc_JE_BillAmount = 0
                objJE.iAcc_JE_YearID = sSession.YearID
                objJE.sAcc_JE_Status = "W"
                objJE.iAcc_JE_CreatedBy = sSession.UserID
                objJE.iAcc_JE_UpdatedBy = sSession.UserID
                objJE.sAcc_JE_Operation = "C"
                objJE.sAcc_JE_IPAddress = sSession.IPAddress
                objJE.dAcc_JE_InvoiceDate = Date.ParseExact(Date.Today, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objJE.iAcc_JE_AttachID = 0
                objJE.iACC_JE_ZoneID = 0
                objJE.iACC_JE_RegionID = 0
                objJE.iACC_JE_AreaID = 0
                objJE.iACC_JE_BranchID = 0
                Arr = objJE.SaveJournalMaster(sSession.AccessCode, sSession.AccessCodeID, objJE)

                iTransID = Arr(1)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully JE Passed."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully JE Passed."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                End If

                If lblTrtypes.Text = "PAYMENT" Then
                    objJE.iATD_TrType = 11  'bankreconcilation form transaction
                    objJE.iATD_BillId = iTransID
                    objJE.iATD_PaymentType = 1
                    objJE.iATD_Head = 1
                    objJE.iATD_GL = 194
                    objJE.iATD_SubGL = 195
                    objJE.iATD_DbOrCr = 1
                    objJE.dATD_Debit = txtbedidiff.Text
                    objJE.dATD_Credit = txtcredbdiff.Text
                    objJE.iATD_CreatedBy = sSession.UserID
                    objJE.iATD_UpdatedBy = sSession.UserID
                    objJE.sATD_Status = "A"
                    objJE.iATD_YearID = sSession.YearID
                    objJE.sATD_Operation = "U"
                    objJE.sATD_IPAddress = sSession.IPAddress
                    objJE.dATD_TransactionDate = Date.Today
                    Arr = objJE.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objJE)
                    imasterID = objBank.UpdateDescription(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text, txtDescription.Text, Arr(1))
                    iJEID = Arr(1)
                    ' Adjstedamunt = objBank.ADjustedAmount(sSession.AccessCode, sSession.AccessCodeID, Debit1, credit1, iJEID)
                    dtBankDetails = objBank.LoadReconciliation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistinReconciliation.SelectedValue)
                    dgMatchedRows.DataSource = dtBankDetails
                    dgMatchedRows.DataBind()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalBRS').modal('show');", True)
                Else  'Receipt                    
                    objJE.iATD_TrType = 11 'bankreconcilation form transaction
                    objJE.iATD_BillId = iTransID
                    objJE.iATD_PaymentType = 1
                    objJE.iATD_Head = 1
                    objJE.iATD_GL = 191
                    objJE.iATD_SubGL = 192
                    objJE.iATD_DbOrCr = 1
                    objJE.dATD_Debit = txtbedidiff.Text
                    objJE.dATD_Credit = txtcredbdiff.Text
                    objJE.iATD_CreatedBy = sSession.UserID
                    objJE.iATD_UpdatedBy = sSession.UserID
                    objJE.sATD_Status = "A"
                    objJE.iATD_YearID = sSession.YearID
                    objJE.sATD_Operation = "U"
                    objJE.sATD_IPAddress = sSession.IPAddress
                    objJE.dATD_TransactionDate = Date.Today
                    Arr = objJE.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objJE)
                    imasterID = objBank.UpdateDescription(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text, txtDescription.Text, Arr(1))
                    iJEID = Arr(1)
                    ' Adjstedamunt = objBank.ADjustedAmount(sSession.AccessCode, sSession.AccessCodeID, Debit1, credit1, iJEID)
                    dtBankDetails = objBank.LoadReconciliation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistinReconciliation.SelectedValue)
                    dgMatchedRows.DataSource = dtBankDetails
                    dgMatchedRows.DataBind()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalBRS').modal('show');", True)
                End If
            Else
                imasterID = objBank.UpdateDescription(sSession.AccessCode, sSession.AccessCodeID, lblBANKID.Text, txtDescription.Text, 0)
                iJEID = 0
                'Adjstedamunt = objBank.ADjustedAmount(sSession.AccessCode, sSession.AccessCodeID, Debit1, credit1, iJEID)
                dtBankDetails = objBank.LoadReconciliation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistinReconciliation.SelectedValue)
                dgMatchedRows.DataSource = dtBankDetails
                dgMatchedRows.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalBRS').modal('show');", True)
            End If

            If dtBankDetails.Rows.Count > 0 Then
                For i = 0 To dtBankDetails.Rows.Count - 1
                    If (dtBankDetails.Rows(i).Item("Status").ToString = "A") Then
                        dgMatchedRows.Rows(i).BackColor = Drawing.Color.Green
                    ElseIf (dtBankDetails.Rows(i).Item("Status").ToString = "W") Then
                        dgMatchedRows.Rows(i).BackColor = Drawing.Color.Red
                    End If
                Next
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub
    Protected Sub chkSelect_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblStatus As New Label
        Dim sID As String
        Dim lblDebit, lblCredit, lblSerialNo, lblTrType As New Label
        Dim lblCDebit, lblCCredit As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            For i = 0 To dgMatchedRows.Rows.Count - 1
                chkSelect = dgMatchedRows.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next

            If iCount = 0 Then
                lblError.Text = "Select Checkbox to Edit."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgMatchedRows.Rows.Count - 1
                chkSelect = dgMatchedRows.Rows(i).FindControl("chkSelect")
                lblStatus = dgMatchedRows.Rows(i).FindControl("lblStatus")
                lblSerialNo = dgMatchedRows.Rows(i).FindControl("SerialNo")
                lblTrType = dgMatchedRows.Rows(i).FindControl("TrType")
                lblDebit = dgMatchedRows.Rows(i).FindControl("Debit") 'bank debit and credit
                lblCredit = dgMatchedRows.Rows(i).FindControl("Credit")
                lblCDebit = dgMatchedRows.Rows(i).FindControl("lblCDebit") 'company debit and credit
                lblCCredit = dgMatchedRows.Rows(i).FindControl("lblCCredit")

                If chkSelect.Checked = True Then
                    sID = lblStatus.Text
                    If lblStatus.Text = "EAN" Or lblStatus.Text = "EAM" Or lblStatus.Text = "N" Then
                    ElseIf lblStatus.Text = "W" Then
                        lblBANKID.Text = lblSerialNo.Text
                        lblTrtypes.Text = lblTrType.Text
                        txtCredit.Text = lblCredit.Text
                        txtdebit.Text = lblDebit.Text
                        txtCompCredit.Text = lblCCredit.Text
                        txtcompdebit.Text = lblCDebit.Text
                        txtbedidiff.Text = lblDebit.Text - lblCDebit.Text
                        txtcredbdiff.Text = lblCredit.Text - lblCCredit.Text
                        If (lblCCredit.Text <> "0.00" Or lblCDebit.Text <> "0.00") Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelect_CheckedChanged")
        End Try
    End Sub
    Private Sub BRSREport_Click(sender As Object, e As EventArgs) Handles BRSREport.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Dim Bankname1 As String
        Dim dFromdate, dTodate As String
        Try
            lblError.Text = ""
            If ddlExistinReconciliation.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Existing Reconcilation No."
                'lblError.Text = "Select Existing Reconcilation No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASBankValidation').modal('show');", True)
                Exit Sub
            End If
            dt = objBank.BRSReport(sSession.AccessCode, sSession.AccessCodeID, ddlBank.SelectedValue, ddlExistinReconciliation.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/BRSReport.rdlc")
            'ReportViewer1.LocalReport.Refresh()
            Dim BalaceAsPerBook As ReportParameter() = New ReportParameter() {New ReportParameter("BalaceAsPerBook", BalComp)}
            ReportViewer1.LocalReport.SetParameters(BalaceAsPerBook)
            Dim BalanceasPerBank As ReportParameter() = New ReportParameter() {New ReportParameter("BalanceasPerBank", BalBnk)}
            ReportViewer1.LocalReport.SetParameters(BalanceasPerBank)
            Dim AdjustedAmount As ReportParameter() = New ReportParameter() {New ReportParameter("AdjustedAmount", Adjstedamunt)}
            ReportViewer1.LocalReport.SetParameters(AdjustedAmount)
            Dim TotalBal As ReportParameter() = New ReportParameter() {New ReportParameter("TotalBal", TotalAmnt)}
            ReportViewer1.LocalReport.SetParameters(TotalBal)

            Bankname1 = objBank.BankName(sSession.AccessCode, sSession.AccessCodeID, ddlBank.SelectedValue)

            Dim BankName As ReportParameter() = New ReportParameter() {New ReportParameter("BankName", Bankname1)}
            ReportViewer1.LocalReport.SetParameters(BankName)
            If txtfrom.Text <> "" Then
                dFromdate = Date.ParseExact(txtfrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                dFromdate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            Dim FromDate As ReportParameter() = New ReportParameter() {New ReportParameter("FromDate", dFromdate)}
            ReportViewer1.LocalReport.SetParameters(FromDate)
            If txtto.Text <> "" Then
                dTodate = Date.ParseExact(txtto.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                dTodate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            Dim Todate As ReportParameter() = New ReportParameter() {New ReportParameter("Todate", dTodate)}
            ReportViewer1.LocalReport.SetParameters(Todate)
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=BankreconcilationReport" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BRSREport_Click")
        End Try
    End Sub
    Protected Sub BindGrid()
        Try
            dgBank.DataSource = dttabledatacapture1
            dgBank.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindGrid")
        End Try
    End Sub
    Private Sub BankManualAdd_Click(sender As Object, e As EventArgs) Handles BankManualAdd.Click
        Try
            If dtpublic Is Nothing Then
                lblCustomerValidationMsg.Text = "Select Button Go."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASBankValidation').modal('show');", True)
                Exit Sub
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal2').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BankManualAdd_Click")
        End Try
    End Sub

    Private Sub btnSAveManualBank_Click(sender As Object, e As EventArgs) Handles btnSAveManualBank.Click
        Dim dttabledatacapture As New DataTable
        Dim Uniquecompdt As New DataTable
        Try
            Dim dt As DataTable = DirectCast(ViewState("ManualEntry"), DataTable)
            dt.Rows.Add(txtSlnoBK1.Text.Trim(), txtTrtypeBK1.Text.Trim(), txtTrNoBK1.Text.Trim(), txtTrnDteBK1.Text.Trim(), txtchenoBK1.Text.Trim(), txtchqdtBK1.Text.Trim(), txtvalDteBK1.Text.Trim(),
            txtBankNBK1.Text.Trim(), txtBrnnBK1.Text.Trim(), txtdebitBK1.Text.Trim(), txtcrebitBK1.Text.Trim(), txtDescBK1.Text.Trim(), txtRefBK1.Text.Trim(), txtbrncodBK1.Text.Trim(), txtBalBK1.Text.Trim(),
             "", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", txtvtypBK1.Text.Trim(), txtOpeningBal.Text.Trim())
            ViewState("ManualEntry") = dt
            dttabledatacapture = dt.Copy

            For i = 0 To dttabledatacapture.Rows.Count - 1
                For j = 0 To dtpublic.Rows.Count - 1
                    If (dtpublic.Rows(j).Item("Acc_PM_ChequeNo").ToString()) = (dttabledatacapture.Rows(i).Item("ChequeNo").ToString()) Then
                        If ((Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Debit")).ToString("#,##0.00")) = (Convert.ToDecimal(dttabledatacapture.Rows(i).Item("Debit"))).ToString("#,##0.00") And (Convert.ToDecimal(dtpublic.Rows(j).Item("ATD_Credit"))).ToString("#,##0.00") = (Convert.ToDecimal(dttabledatacapture.Rows(i).Item("Credit"))).ToString("#,##0.00") And (clsgeneral.FormatDtForRDBMS(dtpublic.Rows(j).Item("ATD_TransactionDate"), "D")) = (clsgeneral.FormatDtForRDBMS(dttabledatacapture.Rows(i).Item("TxnDate"), "D"))) Then
                            dttabledatacapture.Rows(i).Item(15) = "EAM"
                            objBank.DeleteMAtchedINOUTData(sSession.AccessCode, sSession.AccessCodeID, dtpublic.Rows(j).Item("SrNo"))
                            GoTo Skip
                        Else
                            dttabledatacapture.Rows(i).Item(15) = "EAN"
                            GoTo Skip
                        End If
                    Else
                        dttabledatacapture.Rows(i).Item(15) = "N"
                    End If
                Next
Skip:           dttabledatacapture.AcceptChanges()
            Next

            dttabledatacapture1 = dttabledatacapture.Copy
            If dttabledatacapture1.Rows.Count > 0 Then
                lbl_BankBook.Visible = True
            Else
                lbl_BankBook.Visible = False
            End If

            Dim dtTemp As New DataTable
            Uniquecompdt = dtpublic.Copy
            dtTemp = dtpublic.Copy

            For a As Integer = Uniquecompdt.Rows.Count - 1 To 0 Step -1
                For b As Integer = 0 To dt.Rows.Count - 1
                    If (Uniquecompdt.Rows(a)("Acc_PM_ChequeNo").ToString()) = Nothing Then
                    Else
                        If ((Uniquecompdt.Rows(a)("Acc_PM_ChequeNo").ToString()) = (dt.Rows(b)("chequeNo").ToString())) Then
                            dtTemp.Rows.RemoveAt(a)
                            Exit For
                        End If
                    End If
                Next
            Next
            dtTemp.AcceptChanges()
            CompuniqueRow = dtTemp.Copy
            Uniquecompdt1.DataSource = CompuniqueRow
            Uniquecompdt1.DataBind()

            Me.BindGrid()
            txtSlnoBK1.Text = String.Empty : txtTrtypeBK1.Text = String.Empty : txtTrNoBK1.Text = String.Empty
            txtTrnDteBK1.Text = String.Empty : txtchenoBK1.Text = String.Empty : txtchqdtBK1.Text = String.Empty
            txtvalDteBK1.Text = String.Empty : txtBankNBK1.Text = String.Empty : txtBrnnBK1.Text = String.Empty
            txtdebitBK1.Text = String.Empty : txtcrebitBK1.Text = String.Empty : txtDescBK1.Text = String.Empty
            txtRefBK1.Text = String.Empty : txtbrncodBK1.Text = String.Empty : txtBalBK1.Text = String.Empty : txtvtypBK1.Text = String.Empty
            txtOpeningBal.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSAveManualBank_Click")
        End Try
    End Sub
End Class