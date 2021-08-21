Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports DatabaseLayer
Partial Class Masters_ExcelUpload
    Inherits System.Web.UI.Page

    Private sFormName As String = "Masters/ExcelUpload"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGenFun As New clsGeneralFunctions
    Dim objclsExcel As New clsExcelUpload
    Private objclsModulePermission As New clsModulePermission
    Private Shared sFile As String
    Private objDBL As New DBHelper
    Private Shared sExcelSave As String
    Dim objphysicalstock As New clsPhysicalStockExcelUpload
    Dim objInvS As New clsInventoryMaster.Inventory
    Dim objINV As New clsInventoryMaster
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Upload24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                Session("dtUpload") = Nothing
                divZone.Visible = False
                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)
                ddlAccBrnch.SelectedValue = objclsExcel.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                ddlAccBrnch_SelectedIndexChanged(sender, e)

                rboWithGST.Visible = False : rboWithoutGST.Visible = False
                DivOpBreak.Visible = False
                BindSubLedger()
                imgbtnSave.Visible = False : LnkbtnExcel.Visible = False
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "EXU")
                imgbtnSave.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = False
                    End If
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then

                End If
                BindMasterType()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objclsExcel.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsExcel.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsExcel.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsExcel.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindSubLedger()
        Dim dt As New DataTable
        Try
            dt = objclsExcel.BindSubLedger(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlSubLedger.DataSource = dt
            ddlSubLedger.DataTextField = "GL_Desc"
            ddlSubLedger.DataValueField = "GL_ID"
            ddlSubLedger.DataBind()
            ddlSubLedger.Items.Insert(0, "Select Sub Ledger")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindMasterType()
        Try
            ddlMasterName.Items.Insert(0, "Select Upload Types")
            ddlMasterName.Items.Insert(1, "Inventory")
            ddlMasterName.Items.Insert(2, "Supplier")
            ddlMasterName.Items.Insert(3, "Customer")
            ddlMasterName.Items.Insert(4, "")
            ddlMasterName.Items.Insert(5, "Opening Balance")
            ddlMasterName.Items.Insert(6, "User")
            ddlMasterName.Items.Insert(7, "Sub ledger Opening Balance")
            ddlMasterName.Items.Insert(8, "PhysicalStockUpload")
            ddlMasterName.Items.Insert(9, "GST Schedule")
            ddlMasterName.Items.Insert(10, "Opening Balance Breakup")
            ddlMasterName.Items.Insert(11, "Asset Master")
            ddlMasterName.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlMasterName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMasterName.SelectedIndexChanged
        Try
            divZone.Visible = False
            lblMsg.Text = ""
            DivOpBreak.Visible = False
            rboWithGST.Visible = False : rboWithoutGST.Visible = False

            dgUpload.DataSource = Nothing
            dgUpload.DataBind()
            dgUpload.Visible = False
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
            dgGeneral.Visible = False
            imgbtnSave.Visible = False

            If ddlMasterName.SelectedIndex > 0 Then
                LnkbtnExcel.Visible = True
            Else
                LnkbtnExcel.Visible = False
            End If
            If ddlMasterName.SelectedIndex = 1 Then
                rboWithGST.Visible = True : rboWithoutGST.Visible = True
                LnkbtnExcel.Text = "Download Inventory Sample Excel."
                lblMsg.Text = "Only one Rate Can upload for one item.(provided facility to create multiple rate in inventory_Price_details)"
            ElseIf ddlMasterName.SelectedIndex = 2 Then
                LnkbtnExcel.Text = "Supplier Upload Excel."
            ElseIf ddlMasterName.SelectedIndex = 3 Then
                LnkbtnExcel.Text = "Customer Upload Excel."
            ElseIf ddlMasterName.SelectedIndex = 4 Then
                LnkbtnExcel.Text = "Download Party Sample Excel."
            ElseIf ddlMasterName.SelectedIndex = 5 Then
                divZone.Visible = True
                LnkbtnExcel.Text = "Download Opening Balance Sample Excel."
            ElseIf ddlMasterName.SelectedIndex = 6 Then
                divZone.Visible = True
                ddlAccRgn.Visible = False : ddlAccArea.Visible = False : ddlAccBrnch.Visible = False
                LnkbtnExcel.Text = "User Upload Excel."
            ElseIf ddlMasterName.SelectedIndex = 7 Then
                LnkbtnExcel.Text = "Download Sub ledger Opening Balance Sample Excel."
            ElseIf ddlMasterName.SelectedIndex = 8 Then
                rboWithGST.Visible = True : rboWithoutGST.Visible = True
                divZone.Visible = True
                LnkbtnExcel.Text = "Download Physical Upload Sample"
            ElseIf ddlMasterName.SelectedIndex = 9 Then
                LnkbtnExcel.Text = "Download GST Schedule Sample"
            ElseIf ddlMasterName.SelectedIndex = 10 Then
                LnkbtnExcel.Text = "Download Opening Balance BreakUp Sample"
                DivOpBreak.Visible = True
            ElseIf ddlMasterName.SelectedIndex = 11 Then
                LnkbtnExcel.Text = "Download Asset Master Sample"
                DivOpBreak.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMasterName_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub LnkbtnExcel_Click(sender As Object, e As EventArgs) Handles LnkbtnExcel.Click
        Dim sPath As String = ""
        Dim objEApp As Excel.Application
        Try
            If ddlMasterName.SelectedIndex = 5 Then  'Opening Balance
                'ExportoExcelOpeningBalance(objclsExcel.LoadOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID))
                GetOpeningBalanceExcel()
            ElseIf (ddlMasterName.SelectedIndex = 7) Then
                'ExportoExcelSubLedgerOpeningBalance(objclsExcel.LoadSubLedgerOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID))
                GetSubLedgerOpeningBalanceEXcel()
            ElseIf (ddlMasterName.SelectedIndex = 8) Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & ddlMasterName.SelectedItem.Text & ".xlsx")
                Response.TransmitFile(Server.MapPath("../") & "SampleExcels\" & ddlMasterName.SelectedItem.Text & ".xlsx")
                Response.End()
            ElseIf (ddlMasterName.SelectedIndex = 1) Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & ddlMasterName.SelectedItem.Text & ".xlsx")
                Response.TransmitFile(Server.MapPath("../") & "SampleExcels\" & ddlMasterName.SelectedItem.Text & ".xlsx")
                Response.End()
            Else
                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & ddlMasterName.SelectedItem.Text & ".xlsx")
                Response.TransmitFile(Server.MapPath("../") & "SampleExcels\" & ddlMasterName.SelectedItem.Text & ".xlsx")
                Response.End()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LnkbtnExcel_Click")
        End Try
    End Sub
    Public Sub GetOpeningBalanceEXcel()
        Dim mimeType As String = Nothing
        Dim dtConductRA As New DataTable
        Dim sCode As String = ""
        Dim sArray As String()
        Dim sbret As String
        Try

            ReportViewer1.Reset()
            dtConductRA = objclsExcel.LoadOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dtConductRA.Rows.Count = 0 Then
                lblError.Text = "No Rows"
                lblExcelValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
            End If
            Dim rds As New ReportDataSource("DataSet1", dtConductRA)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptOB.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Opening Balance" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GetSubLedgerOpeningBalanceEXcel()
        Dim mimeType As String = Nothing
        Dim dtConductRA As New DataTable
        Dim sCode As String = ""
        Dim sArray As String()
        Dim sbret As String
        Try

            ReportViewer1.Reset()
            dtConductRA = objclsExcel.LoadSubLedgerOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dtConductRA.Rows.Count = 0 Then
                lblError.Text = "No Rows"
                lblExcelValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
            End If
            Dim rds As New ReportDataSource("DataSet1", dtConductRA)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Accounts/RptSubOB.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=SubLedger Opening Balance" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ExportoExcelSubLedgerOpeningBalance(ByVal dt1 As DataTable)
        Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim dt As System.Data.DataTable
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0, rowIndex As Integer = 0
        Dim sPath As String, strFileNameFullPath As String, strFileNamePath As String, sExcelFileName As String
        Dim i As Integer
        Try
            If dt1.Rows.Count > 0 Then
                dt = dt1
                sPath = Server.MapPath("../") & "SampleExcels\OpeningBalance.xlsx"
                wBook = excel.Workbooks.Add(sPath)
                wSheet = wBook.ActiveSheet()
                For i = 0 To 8
                    colIndex = colIndex + 1
                    excel.Cells(1, colIndex) = dt.Columns(i).ColumnName
                    excel.Cells(1, colIndex).Font.Bold = True
                Next
                For Each dr In dt.Rows
                    rowIndex = rowIndex + 1
                    colIndex = 0
                    For i = 0 To 8
                        colIndex = colIndex + 1
                        excel.Cells(rowIndex + 1, colIndex) = dr(dt.Columns(i).ColumnName)
                    Next
                Next
                wSheet.Columns.AutoFit()
                strFileNamePath = objGenFun.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
                If ddlMasterName.SelectedIndex = 5 Then
                    sExcelFileName = "SubledgerOpeningBalance.xlsx"
                Else
                    sExcelFileName = "SubledgerOpeningBalance.xlsx"
                End If
                If strFileNamePath.EndsWith("\") = False Then
                    strFileNameFullPath = strFileNamePath & "\" & sExcelFileName
                Else
                    strFileNameFullPath = strFileNamePath & sExcelFileName
                End If

                Dim blnFileOpen As Boolean = False
                Try
                    If System.IO.File.Exists(strFileNameFullPath) Then
                        System.IO.File.Delete(strFileNameFullPath)
                    End If
                    Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileNameFullPath)
                    fileTemp.Close()
                Catch ex As Exception
                    blnFileOpen = False
                End Try
                If System.IO.File.Exists(strFileNameFullPath) Then
                    System.IO.File.Delete(strFileNameFullPath)
                End If
                wBook.SaveAs(strFileNameFullPath)
                wBook.Close()
                excel.Quit()
                excel = Nothing
                DownloadFile(strFileNameFullPath)
            Else
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ExportoExcelSubLedgerOpeningBalance")
        End Try
    End Sub

    Private Sub DownloadFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
            dgGeneral.Visible = False
            imgbtnSave.Visible = False
            If FULoad.FileName <> String.Empty Then
                lblSheetName.Visible = True : ddlSheetName.Visible = True
                sExt = IO.Path.GetExtension(FULoad.PostedFile.FileName)
                Session("sExt") = sExt
                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FULoad.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    sPath = objGenFun.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
                    If sPath.EndsWith("\") = False Then
                        sFile = sPath & "\" & sFileName
                    Else
                        sFile = sPath & sFileName
                    End If
                    FULoad.PostedFile.SaveAs(sFile)
                    ddlSheetName.Items.Clear()
                    dt = ExcelSheetNames(sFile)
                    ddlSheetName.DataSource = dt
                    ddlSheetName.DataTextField = "Name"
                    ddlSheetName.DataValueField = "ID"
                    ddlSheetName.DataBind()
                    ddlSheetName.Items.Insert(0, "Select Sheet")
                Else
                    lblError.Text = "Select Excel file only." : lblExcelValidationMsg.Text = "Select Excel file only."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblExcelValidationMsg.Text = "Select Excel file."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOk_Click")
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
    Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable

        Dim sStr As String = ""
        Try
            lblError.Text = ""
            dgUpload.DataSource = Nothing
            dgUpload.DataBind()
            dgUpload.Visible = False
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
            dgGeneral.Visible = False
            imgbtnSave.Visible = False
            If ddlSheetName.SelectedIndex > 0 Then

                If ddlMasterName.SelectedIndex = 1 Then      'Inventory
                    dttable = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
                    If IsNothing(dttable) Then
                        lblError.Text = "Invalid Excel format in selected sheet."
                        ddlSheetName.Items.Clear()
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                    dgUpload.DataSource = dttable
                    dgUpload.DataBind()
                    dgUpload.Visible = True
                    Session("dtUpload") = dttable
                    imgbtnSave.Visible = True

                ElseIf ddlMasterName.SelectedIndex = 2 Or ddlMasterName.SelectedIndex = 3 Then      'Supplier & Customer
                    dttable = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
                    If IsNothing(dttable) Then
                        lblError.Text = "Invalid Excel format in selected sheet."
                        ddlSheetName.Items.Clear()
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                    dgUpload.DataSource = dttable
                    dgUpload.DataBind()
                    dgUpload.Visible = True
                    Session("dtUpload") = dttable
                    imgbtnSave.Visible = True
                ElseIf ddlMasterName.SelectedIndex = 6 Then     'User
                    dttable = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
                    If IsNothing(dttable) Then
                        lblError.Text = "Invalid Excel format in selected sheet."
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        ddlSheetName.Items.Clear()
                        Exit Sub
                    End If
                    dgUpload.DataSource = dttable
                    dgUpload.DataBind()
                    dgUpload.Visible = True
                    Session("dtUpload") = dttable
                    imgbtnSave.Visible = True

                ElseIf ddlMasterName.SelectedIndex = 8 Then     'Physical Stock Upload
                    dttable = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
                    If IsNothing(dttable) Then
                        lblError.Text = "Invalid Excel format in selected sheet."
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        ddlSheetName.Items.Clear()
                        Exit Sub
                    End If
                    dgUpload.DataSource = dttable
                    dgUpload.DataBind()
                    dgUpload.Visible = True
                    Session("dtUpload") = dttable
                    imgbtnSave.Visible = True

                ElseIf ddlMasterName.SelectedIndex = 9 Then     'GST
                    dttable = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
                    If IsNothing(dttable) Then
                        lblError.Text = "Invalid Excel format in selected sheet."
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        ddlSheetName.Items.Clear()
                        Exit Sub
                    End If
                    dgUpload.DataSource = dttable
                    dgUpload.DataBind()
                    dgUpload.Visible = True
                    Session("dtUpload") = dttable
                    imgbtnSave.Visible = True

                Else
                    dttable = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)

                    If IsNothing(dttable) Then
                        ddlSheetName.SelectedIndex = 0
                        lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    ElseIf dttable.Rows.Count = 0 Then
                        lblError.Text = "No Data." : lblExcelValidationMsg.Text = "No Data."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If

                    dgGeneral.DataSource = dttable
                    dgGeneral.DataBind()
                    Session("dtUpload") = dttable
                    dgGeneral.Visible = True
                    imgbtnSave.Visible = True

                End If

            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                ddlSheetName.SelectedIndex = 0
                imgbtnSave.Visible = False
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
                imgbtnSave.Visible = False
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            If ddlMasterName.SelectedIndex = 1 Then   'Inventory
                If dgUpload.Rows.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If rboWithGST.Checked = False And rboWithoutGST.Checked = False Then
                    lblError.Text = "Select any one radio button."
                    Exit Sub
                End If
                InventorySave()

            ElseIf ddlMasterName.SelectedIndex = 2 Then  'Supplier
                If dgUpload.Rows.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                SaveSupplierMaster()
            ElseIf ddlMasterName.SelectedIndex = 3 Then  'CUstomer
                If dgUpload.Rows.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                SaveCustomerMaster()
            ElseIf ddlMasterName.SelectedIndex = 5 Then  'Opening Balance
                If dgGeneral.Items.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If ddlAccZone.SelectedIndex = 0 Then
                    lblError.Text = "Select Zone"
                    Exit Sub
                End If
                If ddlAccRgn.SelectedIndex = 0 Then
                    lblError.Text = "Select Region"
                    Exit Sub
                End If
                If ddlAccArea.SelectedIndex = 0 Then
                    lblError.Text = "Select Area"
                    Exit Sub
                End If
                If ddlAccBrnch.SelectedIndex = 0 Then
                    lblError.Text = "Select Branch"
                    Exit Sub
                End If
                SaveOpeningBalance()
            ElseIf ddlMasterName.SelectedIndex = 6 Then    'User
                If dgUpload.Rows.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If ddlAccZone.SelectedIndex = 0 Then
                    lblError.Text = "Select Zone"
                    Exit Sub
                End If
                'If ddlAccRgn.SelectedIndex = 0 Then
                '    lblError.Text = "Select Region"
                '    Exit Sub
                'End If
                'If ddlAccArea.SelectedIndex = 0 Then
                '    lblError.Text = "Select Area"
                '    Exit Sub
                'End If
                'If ddlAccBrnch.SelectedIndex = 0 Then
                '    lblError.Text = "Select Branch"
                '    Exit Sub
                'End If
                SaveUseMaster()

            ElseIf ddlMasterName.SelectedIndex = 7 Then    'Sub Ledger
                If dgGeneral.Items.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                SaveSubLedgerOpeningBalance()

            ElseIf ddlMasterName.SelectedIndex = 8 Then    'Physical Stock
                If dgUpload.Rows.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If rboWithGST.Checked = False And rboWithoutGST.Checked = False Then
                    lblError.Text = "Select any one radio button."
                    Exit Sub
                End If
                If ddlAccBrnch.SelectedIndex > 0 Then
                    SAvePhyscalStock()
                Else
                    lblError.Text = "Select Branch for which u wish to upload physical stock"
                    Exit Sub
                End If


            ElseIf ddlMasterName.SelectedIndex = 9 Then    'GST
                If dgUpload.Rows.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                SaveGST()
            ElseIf ddlMasterName.SelectedIndex = 10 Then    'OpBal BreakUP
                If dgGeneral.Items.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                Dim iCheck As Integer
                If ddlSubLedger.SelectedIndex > 0 Then
                    iCheck = objclsExcel.CheckBreakUp(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlSubLedger.SelectedValue)
                    If iCheck > 0 Then
                        lblError.Text = "BreakUp for this subLedger has been Uploaded,it can not be upload again."
                        Exit Sub
                    End If
                    SaveOpBalBreakUP()
                Else
                    lblError.Text = "Select Sub-Ledger And then upload the excel for BreakUp."
                    Exit Sub
                End If
            ElseIf ddlMasterName.SelectedIndex = 11 Then    'Asset Master
                If dgUpload.Rows.Count = 0 Then
                    lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                SaveAssetMaster()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Public Sub SaveOpBalBreakUP()
        Dim dt As New DataTable
        Dim dDebitAmt, dCreditAmt As Double
        Dim dtChart As New DataTable
        Dim iOpnID As Integer
        Dim sStr As String : Dim sStartDate As String = ""
        Dim sBillDate As String = "" : Dim dBillDate As Date
        Try
            dt = Session("dtUpload")
            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(0).ToString() = "" And dt.Rows(i)(1).ToString() = "" And dt.Rows(i)(2).ToString() = "" Then
                    Try

                    Catch ex As Exception
                        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
                        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "1")
                    End Try
                Else
                    If dt.Rows(i)(0).ToString() = "" Then
                        lblError.Text = "Bill No cannot be blank" & " Line No - " & i + 1
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If

                    If dt.Rows(i)(1).ToString() = "" Then
                        Try

                        Catch ex As Exception
                            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
                            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "2")
                        End Try
                        lblError.Text = "Bill Date cannot be blank" & " Line No - " & i + 1
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    Else

                        Try
                            sStartDate = objclsExcel.GetSDate(sSession.AccessCode, sSession.AccessCodeID)
                            sStr = objclsExcel.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(1))
                        Catch ex As Exception
                            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
                            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "D")
                        End Try
                        If sStr = 1 Then
                            lblError.Text = "Bill Date of Line No - " & i + 1 & " should be lesser than equal to application Start Date " & sStartDate & ""
                            Exit Sub
                        End If
                    End If

                    If dt.Rows(i)(2).ToString() = "" And dt.Rows(i)(3).ToString() = "" Then
                        lblError.Text = "Both Debit and Credit cannot be blank" & " Line No - " & i + 1
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                End If
            Next

            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)(0).ToString() <> "" And dt.Rows(i)(1).ToString() <> "" Then
                    If dt.Rows(i)(2).ToString() <> "" Then
                        dDebitAmt = dDebitAmt + dt.Rows(i)(2).ToString()
                    End If
                    If dt.Rows(i)(3).ToString() <> "" Then
                        dCreditAmt = dCreditAmt + dt.Rows(i)(3).ToString()
                    End If
                End If
            Next

            If lblOpBalDebit.Text > 0 Then
                If lblOpBalDebit.Text <> dDebitAmt Then
                    lblError.Text = "Debit Amount And Opening Balance Debit Amount are not matching."
                    Exit Sub
                End If
            ElseIf lblOpBalCreadit.Text > 0 Then
                If lblOpBalCreadit.Text <> dCreditAmt Then
                    lblError.Text = "Credit Amount And Opening Balance Credit Amount are not matching."
                    Exit Sub
                End If
            End If

            If lblOpBalDebit.Text > 0 Then
                iOpnID = objclsExcel.UpdateOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlSubLedger.SelectedValue, dDebitAmt, 1)
            ElseIf lblOpBalCreadit.Text > 0 Then
                iOpnID = objclsExcel.UpdateOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlSubLedger.SelectedValue, dCreditAmt, 2)
            End If

            'Save to Breakup Table'
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)(0).ToString() <> "" Then
                        Try
                            sBillDate = Trim(dt.Rows(i)(1))
                        Catch ex As Exception
                            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
                            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "3")
                        End Try

                        Try
                            dBillDate = Date.ParseExact(sBillDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Catch ex As Exception
                            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
                            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "4")
                        End Try

                        Try
                            objclsExcel.SaveBreakUp(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, dt.Rows(i)(0).ToString(), dBillDate, dt.Rows(i)(2).ToString(), dt.Rows(i)(3).ToString(), iOpnID, ddlSubLedger.SelectedValue)
                        Catch ex As Exception
                            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
                            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "5")
                        End Try
                    End If
                Next
            End If
            'Save to Breakup Table'

            For i = 0 To dt.Rows.Count - 1

                objclsExcel.iATD_TrType = 12
                objclsExcel.iATD_ID = 0
                objclsExcel.dATD_TransactionDate = Date.Today
                'Date.ParseExact(Date.Today, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                objclsExcel.iATD_BillId = iOpnID
                objclsExcel.iATD_PaymentType = 0
                'iPaymentType

                dtChart = objclsExcel.GetChartOfAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSubLedger.SelectedValue)

                If (IsDBNull(dtChart.Rows(0)("GL_AccHead")) = False) Then
                    objclsExcel.iATD_Head = dtChart.Rows(0)("GL_AccHead")
                Else
                    objclsExcel.iATD_Head = 0
                End If

                If (IsDBNull(dtChart.Rows(0)("GL_Parent")) = False) Then
                    objclsExcel.iATD_GL = dtChart.Rows(0)("GL_Parent")
                Else
                    objclsExcel.iATD_GL = 0
                End If

                If (IsDBNull(dtChart.Rows(0)("GL_ID")) = False) Then
                    objclsExcel.iATD_SubGL = dtChart.Rows(0)("GL_ID")
                Else
                    objclsExcel.iATD_SubGL = 0
                End If

                If (IsDBNull(dt.Rows(i)(2).ToString()) = False) And (dt.Rows(i)(2).ToString() <> "&nbsp;") And dt.Rows(i)(2).ToString() <> "" Then
                    objclsExcel.dATD_Debit = dt.Rows(i)(2).ToString()
                Else
                    objclsExcel.dATD_Debit = 0
                End If

                If (IsDBNull(dt.Rows(i)(3).ToString()) = False) And (dt.Rows(i)(3).ToString() <> "&nbsp;") And dt.Rows(i)(3).ToString() <> "" Then
                    objclsExcel.dATD_Credit = dt.Rows(i)(3).ToString()
                Else
                    objclsExcel.dATD_Credit = 0
                End If

                If objclsExcel.dATD_Debit > 0 And objclsExcel.dATD_Credit = 0 Then
                    objclsExcel.iATD_DbOrCr = 1 'Debit
                ElseIf objclsExcel.dATD_Debit = 0 And objclsExcel.dATD_Credit > 0 Then
                    objclsExcel.iATD_DbOrCr = 2 'Credit
                End If

                objclsExcel.iATD_CreatedBy = sSession.UserID
                objclsExcel.dATD_CreatedOn = DateTime.Today

                objclsExcel.sATD_Status = "A"
                objclsExcel.iATD_YearID = sSession.YearID
                objclsExcel.sATD_Operation = "C"
                objclsExcel.sATD_IPAddress = sSession.IPAddress

                objclsExcel.iATD_UpdatedBy = sSession.UserID
                objclsExcel.dATD_UpdatedOn = DateTime.Today

                objclsExcel.iATD_CompID = sSession.AccessCodeID

                objclsExcel.iATD_ZoneID = ddlAccZone.SelectedValue
                objclsExcel.iATD_RegionID = ddlAccRgn.SelectedValue
                objclsExcel.iATD_AreaID = ddlAccArea.SelectedValue
                objclsExcel.iATD_BranchID = ddlAccBrnch.SelectedValue

                objclsExcel.SaveUpdateTransactionDetails(sSession.AccessCode, objclsExcel)

            Next

            lblError.Text = "Successfully Uploaded."
            lblExcelValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveOpBalBreakUP")
        End Try
    End Sub
    Public Sub SaveGST()
        Dim dtUpload As New DataTable
        Dim objclsEx As New clsExcelUpload.GST
        Dim Arr() As String
        Dim iMasterID As Integer
        Try
            If IsNothing(Session("dtUpload")) = False Then
                dtUpload = Session("dtUpload")
            Else
                lblError.Text = "Laod before you Upload."
                lblExcelValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            dtUpload = Session("dtUpload")

            'Save Master'
            If (dtUpload.Rows.Count > 0) Then
                objclsEx.AGS_NotificationNo = (dtUpload.Rows(0).Item(9)).ToString
                objclsEx.AGS_NotificationDate = (dtUpload.Rows(0).Item(10)).ToString
                objclsEx.AGS_FileNo = (dtUpload.Rows(0).Item(11)).ToString
                objclsEx.AGS_FileDate = (dtUpload.Rows(0).Item(12)).ToString
                objclsEx.AGS_Createdby = sSession.UserID
                objclsEx.AGS_CreatedOn = Date.Today
                objclsEx.AGS_Status = "W"
                objclsEx.AGS_YearID = sSession.YearID
                objclsEx.AGS_CompID = sSession.AccessCodeID
                objclsEx.AGS_Operation = "C"
                objclsEx.AGS_IPAddress = sSession.IPAddress

                iMasterID = objclsExcel.SaveGSTMaster(sSession.AccessCode, sSession.AccessCodeID, objclsEx)
            End If
            'Save Master'

            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1
                    objclsEx.AGS_ID = 0
                    objclsEx.AGS_GSTM_ID = iMasterID
                    objclsEx.AGS_Schedule_Type = (dtUpload.Rows(j).Item(0)).ToString
                    objclsEx.AGS_GSTRate = (dtUpload.Rows(j).Item(1)).ToString
                    objclsEx.AGS_SlnoOfSchedule = (dtUpload.Rows(j).Item(2)).ToString
                    objclsEx.AGS_CHST = (dtUpload.Rows(j).Item(3)).ToString
                    objclsEx.AGS_Chapter = (dtUpload.Rows(j).Item(4)).ToString
                    objclsEx.AGS_Heading = (dtUpload.Rows(j).Item(5)).ToString
                    objclsEx.AGS_SubHeading = (dtUpload.Rows(j).Item(6)).ToString
                    objclsEx.AGS_Tarrif = (dtUpload.Rows(j).Item(7)).ToString
                    objclsEx.AGS_GoodDescription = (dtUpload.Rows(j).Item(8)).ToString
                    objclsEx.AGS_NotificationNo = (dtUpload.Rows(j).Item(9)).ToString
                    objclsEx.AGS_NotificationDate = (dtUpload.Rows(j).Item(10)).ToString
                    objclsEx.AGS_FileNo = (dtUpload.Rows(j).Item(11)).ToString
                    objclsEx.AGS_FileDate = (dtUpload.Rows(j).Item(12)).ToString
                    objclsEx.AGS_Createdby = sSession.UserID
                    objclsEx.AGS_CreatedOn = Date.Today
                    objclsEx.AGS_Status = "W"
                    objclsEx.AGS_YearID = sSession.YearID
                    objclsEx.AGS_CompID = sSession.AccessCodeID

                    objclsEx.AGS_Operation = "C"
                    objclsEx.AGS_IPAddress = sSession.IPAddress
                    Arr = objclsExcel.SaveGSTDetails(sSession.AccessCode, objclsEx)

                Next
            End If
            'Save Excel Data once it is correct format'
            lblError.Text = "Uploaded Successfully"
            lblExcelValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveSubLedgerOpeningBalance()
        Dim objOP As New clsOpeningBalance
        Dim Arr() As String
        Dim lblGLCode As New Label, lblDebitAmt As New Label, lblCreditAmount As New Label, lblRefNo As New Label, lblPendingAmount As New Label
        Dim lblDueDate As New Label, lblBillDate As New Label, lblOverDueDays As New Label
        Dim iErrorLine As Integer = 0
        Dim id As Integer
        Try
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                objclsExcel.sSubOpn_GLCode = dgGeneral.Items(i).Cells(1).Text
                lblGLCode.Text = dgGeneral.Items(i).Cells(1).Text
                objclsExcel.iSubOpn_DabitOrCredit = 0
                If dgGeneral.Items(i).Cells(8).Text <> "&nbsp;" Then
                    objclsExcel.dSubOpn_DebitAmt = dgGeneral.Items(i).Cells(7).Text
                    lblDebitAmt.Text = dgGeneral.Items(i).Cells(7).Text
                    objclsExcel.iSubOpn_DabitOrCredit = 1
                Else
                    objclsExcel.dSubOpn_DebitAmt = "0.00"
                    lblDebitAmt.Text = ""
                End If

                If dgGeneral.Items(i).Cells(8).Text <> "&nbsp;" Then
                    objclsExcel.dSubOpn_CreditAmount = dgGeneral.Items(i).Cells(8).Text
                    lblCreditAmount.Text = dgGeneral.Items(i).Cells(8).Text
                    objclsExcel.iSubOpn_DabitOrCredit = 2
                Else
                    objclsExcel.dSubOpn_CreditAmount = "0.00"
                    lblCreditAmount.Text = ""
                End If

                'If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
                '    objclsExcel.dSubPenidingAmount = dgGeneral.Items(i).Cells(5).Text
                '    lblPendingAmount.Text = dgGeneral.Items(i).Cells(5).Text
                'Else
                objclsExcel.dSubPenidingAmount = "0.00"
                '    lblPendingAmount.Text = ""
                'End If

                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    objclsExcel.sSubOpn_ReferenceNo = dgGeneral.Items(i).Cells(3).Text
                    lblRefNo.Text = dgGeneral.Items(i).Cells(3).Text
                Else
                    objclsExcel.sSubOpn_ReferenceNo = "0.00"
                    lblRefNo.Text = ""
                End If
                If Trim(dgGeneral.Items(i).Cells(4).Text) <> "&nbsp;" Then

                    lblBillDate.Text = dgGeneral.Items(i).Cells(4).Text
                Else
                    lblBillDate.Text = ""
                End If

                If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then

                    lblDueDate.Text = dgGeneral.Items(i).Cells(5).Text
                Else
                    lblDueDate.Text = ""
                End If
                If dgGeneral.Items(i).Cells(6).Text <> "&nbsp;" Then
                    objclsExcel.iSubOpn_OverDueDays = dgGeneral.Items(i).Cells(6).Text
                    lblOverDueDays.Text = dgGeneral.Items(i).Cells(6).Text
                Else
                    objclsExcel.iSubOpn_OverDueDays = "0"
                    lblOverDueDays.Text = ""
                End If
                If lblGLCode.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Debit Amount. Line No " & iErrorLine & "." : lblError.Text = "Enter Debit Amount. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf lblGLCode.Text.Trim.Length > 50 Then
                    lblExcelValidationMsg.Text = "GLCode exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "GLCode exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblDebitAmt.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Debit Amount. Line No " & iErrorLine & "." : lblError.Text = "Enter Debit Amount. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblCreditAmount.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Credit Amount. Line No " & iErrorLine & "." : lblError.Text = "Enter Credit Amount. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblRefNo.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Reference. Line No " & iErrorLine & "." : lblError.Text = "Enter Rereference. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblBillDate.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Bill Date. Line No " & iErrorLine & "." : lblError.Text = "Enter Bill Date. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblDueDate.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter DueDate. Line No " & iErrorLine & "." : lblError.Text = "Enter DueDate. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                Try
                    objclsExcel.dSubBillDate = objGen.FormatDtForRDBMS(lblBillDate.Text.Trim, "D")
                    '   Dim Hdate As Date = Date.ParseExact(lblBillDate.Text.Trim, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    '  Dim Ctdate As Integer = 0
                Catch ex As Exception
                    lblExcelValidationMsg.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                    lblError.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                End Try
                Try

                    objclsExcel.dSubDueOn = objGen.FormatDtForRDBMS(lblDueDate.Text.Trim, "D")
                    '    Date.ParseExact(lblDueDate.Text.Trim, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim Ctdate As Integer = 0
                Catch ex As Exception
                    lblExcelValidationMsg.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                    lblError.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                End Try
                'Try
                '    objclsExcel.dSubBillDate = objGen.FormatDtForRDBMS(Trim(dgGeneral.Items(i).Cells(4).Text), "D")
                'Catch ex As Exception
                '    lblExcelValidationMsg.Text = "Invalid Date Format - " & dgGeneral.Items(i).Cells(4).Text & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                '    lblError.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                'End Try
                'Try
                '    If Trim(dgGeneral.Items(i).Cells(4).Text) <> "&nbsp;" Then
                '        objclsExcel.dSubBillDate = Date.ParseExact(lblBillDate.Text.Trim, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                '    Else
                '        lblBillDate.Text = ""
                '    End If
                'Catch ex As Exception
                '    lblExcelValidationMsg.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                '    lblError.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                'End Try
                'Try
                '    If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
                '        objclsExcel.dSubDueOn = Date.ParseExact(lblDueDate.Text.Trim, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                '    Else
                '        lblDueDate.Text = ""
                '    End If
                'Catch ex As Exception
                '    lblExcelValidationMsg.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                '    lblError.Text = "Invalid Date Format - " & iErrorLine & ". Date should be dd/MM/yyyy. (Red color indicates invalid data in below grid)."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                'End Try
                If lblOverDueDays.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Over Due Days. Line No " & iErrorLine & "." : lblError.Text = "Enter  Over Due Days. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                objclsExcel.SubOpn_sTransctionNo = objclsExcel.GenerateJECode(sSession.AccessCode, sSession.AccessCodeID)
                'objclsExcel.dSubCreatedOpn_Date = Date.ParseExact(Date.Today, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'objclsExcel.dSubBillDate = Date.ParseExact(objclsExcel.dSubBillDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'objclsExcel.dSubDueOn = Date.ParseExact(objclsExcel.dSubDueOn, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objclsExcel.iSubOpn_YearId = sSession.YearID
                objclsExcel.iSubOpn_CompId = sSession.AccessCodeID
                objclsExcel.iSubOpn_SubGlId = objclsExcel.GetPOSubGlid(sSession.AccessCode, sSession.AccessCodeID, objclsExcel.sSubOpn_GLCode)
                objclsExcel.iSubOpn_GlId = objclsExcel.GetPOGlid(sSession.AccessCode, sSession.AccessCodeID, objclsExcel.sSubOpn_GLCode)
                objclsExcel.sSubOpn_IPAddress = sSession.IPAddress
                objclsExcel.iSubOpn_AccHead = objclsExcel.GetAccHeadID(sSession.AccessCode, sSession.AccessCodeID, objclsExcel.sSubOpn_GLCode)
                Arr = objclsExcel.SaveSubLedgerOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objclsExcel)
                objclsExcel.iSubOpn_BilLID = Arr(1)
                id = objclsExcel.SaveSubLedgerOpeningBalanceJE(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objclsExcel)

            Next
            SaveOpeningBalanceAfterSubledger()
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub SaveOpeningBalanceAfterSubledger()
        Dim objOP As New clsOpeningBalance
        Dim Arr() As String
        Dim lblGLCode As New Label, lblDebitAmt As New Label, lblCreditAmount As New Label
        Dim iErrorLine As Integer = 0
        Dim dtable As New DataTable
        Dim dunique As New DataTable
        Dim dCopy As New DataTable
        Try
            dtable = objclsExcel.loadSubledgerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dCopy = dtable.Copy()
            dunique = objclsExcel.RemoveDublicate(dCopy)
            For i = 0 To dunique.Rows.Count - 1
                iErrorLine = iErrorLine + 1
                objOP.sOpn_GLCode = dunique.Rows(i)("sOpn_GLCode")
                objOP.dOpn_DebitAmt = objclsExcel.GetTotalDabitAmount(sSession.AccessCode, sSession.AccessCodeID, dunique.Rows(i)("sOpn_GLCode"))
                objOP.dOpn_CreditAmount = objclsExcel.GetTotalCreditAmount(sSession.AccessCode, sSession.AccessCodeID, dunique.Rows(i)("sOpn_GLCode"))
                objOP.dOpn_Date = objGen.FormatDtForRDBMS(dunique.Rows(i)("dOpn_Date"), "D")
                ' objOP.dOpn_Date = 'Date.ParseExact(objOP.dOpn_Date, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objOP.iOpn_YearId = dunique.Rows(i)("iOpn_YearId")
                objOP.iOpn_CompId = dunique.Rows(i)("iOpn_CompId")
                objOP.iOpn_GlId = dunique.Rows(i)("iOpn_GlId")
                objOP.sOpn_IPAddress = dunique.Rows(i)("sOpn_IPAddress")
                objOP.iOpn_AccHead = dunique.Rows(i)("iOpn_AccHead")
                Arr = objOP.SaveOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objOP)
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub SaveUseMaster()
        Dim dtUpload As New DataTable
        Dim Code As String = ""
        Dim Name As String = ""
        Dim Loginname As String = ""
        Dim sContact As String = ""
        Dim Email As String = ""
        Dim Designation As String = ""
        Dim sMobile As String = ""
        Dim Address As String = ""
        Dim State As String = ""
        Dim Squestion As String = ""
        Dim flag As String = ""
        Dim Sanswer As String = ""
        Dim Password As String = ""
        Dim j As Integer
        Try
            dtUpload = Session("dtUpload")
            'Dim objUser As New clsUserCreation
            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1
                    If IsDBNull(dtUpload.Rows(j).Item(0)) = False And dtUpload.Rows(j).Item(0).ToString <> "" Then
                        objclsExcel.sUsrCode = (dtUpload.Rows(j).Item(0))
                    Else
                        lblError.Text = "Code cannot be blank" & "Line No:" & j
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If IsDBNull(dtUpload.Rows(j).Item(1)) = False And dtUpload.Rows(j).Item(1).ToString <> "" Then
                        objclsExcel.sUsrFullName = (dtUpload.Rows(j).Item(1))
                    Else
                        lblError.Text = "Name cannot be blank" & "Line No:" & j
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If IsDBNull(dtUpload.Rows(j).Item(2)) = False And dtUpload.Rows(j).Item(2).ToString <> "" Then
                        objclsExcel.sUsrLoginName = (dtUpload.Rows(j).Item(2))
                    Else
                        lblError.Text = "Login Name cannot be blank" & "Line No:" & j
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(3)) = False And dtUpload.Rows(j).Item(3).ToString <> "" Then
                        objclsExcel.sUsrEmail = (dtUpload.Rows(j).Item(3))
                    Else
                        lblError.Text = "Email cannot be blank" & "Line No:" & j
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                Next
            End If


            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1

                    objclsExcel.sUsrCode = (dtUpload.Rows(j).Item(0)).ToString
                    objclsExcel.sUsrFullName = (dtUpload.Rows(j).Item(1)).ToString
                    objclsExcel.sUsrLoginName = (dtUpload.Rows(j).Item(2)).ToString
                    objclsExcel.sUsrEmail = (dtUpload.Rows(j).Item(3)).ToString

                    If IsDBNull(dtUpload.Rows(j).Item(4)) = False And dtUpload.Rows(j).Item(4).ToString <> "" Then
                        objclsExcel.sUsrMobileNo = (dtUpload.Rows(j).Item(4))
                    Else
                        objclsExcel.sUsrMobileNo = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(5)) = False And dtUpload.Rows(j).Item(5).ToString <> "" Then
                        objclsExcel.sUsrOfficePhone = (dtUpload.Rows(j).Item(5))
                    Else
                        objclsExcel.sUsrOfficePhone = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(6)) = False And dtUpload.Rows(j).Item(6).ToString <> "" Then
                        objclsExcel.sUsrAddress = (dtUpload.Rows(j).Item(6))
                    Else
                        objclsExcel.sUsrAddress = ""
                    End If

                    objclsExcel.sUsrPassword = objGen.EncryptPassword("A")

                    objclsExcel.iUserID = 0
                    objclsExcel.sUsrStatus = "C"

                    If ddlAccZone.SelectedIndex > 0 Then
                        objclsExcel.iUsrOrgID = ddlAccZone.SelectedValue
                        objclsExcel.iUsrNode = 1
                    End If

                    'If ddlAccRgn.SelectedIndex > 0 Then
                    '    objclsExcel.iUsrOrgID = ddlAccRgn.SelectedValue
                    '    objclsExcel.iUsrNode = 2
                    'End If

                    'If ddlAccArea.SelectedIndex > 0 Then
                    '    objclsExcel.iUsrOrgID = ddlAccArea.SelectedValue
                    '    objclsExcel.iUsrNode = 3
                    'End If

                    'If ddlAccBrnch.SelectedIndex > 0 Then
                    '    objclsExcel.iUsrOrgID = ddlAccBrnch.SelectedValue
                    '    objclsExcel.iUsrNode = 4
                    'End If

                    'objclsExcel.iUsrOrgID = 0
                    'objclsExcel.iUsrNode = 0

                    objclsExcel.iUsrSentMail = 0
                    objclsExcel.sUsrDutyStatus = "W"
                    objclsExcel.sUsrPhoneNo = ""

                    objclsExcel.sUsrOffPhExtn = ""
                    objclsExcel.iUsrDesignation = 0
                    objclsExcel.iUsrRole = 0
                    objclsExcel.iUsrLevelGrp = 0
                    objclsExcel.iUsrGrpOrUserLvlPerm = 0
                    objclsExcel.sUsrFlag = "W"
                    objclsExcel.iUsrCompID = sSession.AccessCodeID
                    objclsExcel.iUsrCreatedBy = sSession.UserID
                    objclsExcel.sUsrIPAdress = sSession.IPAddress
                    objclsExcel.iUsrMasterModule = 0 : objclsExcel.iUsrPurchaseModule = 0
                    objclsExcel.iUsrSalesModule = 0 : objclsExcel.iUsrAccountsModule = 0

                    objclsExcel.iUsrMasterRole = 0 : objclsExcel.iUsrPurchaseRole = 0
                    objclsExcel.iUsrSalesRole = 0 : objclsExcel.iUsrAccountsRole = 0

                    objclsExcel.SaveEmployeeDetails(sSession.AccessCode, objclsExcel)

                    'objclsExcel.SaveUserData(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.YearID, Code, Name, Loginname, Email, Designation, sMobile, Address, Squestion, Sanswer, Password)
                Next
            Else
                lblError.Text = "Load before you Save."
                lblExcelValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            lblError.Text = "Uploaded Successfully"
            lblExcelValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveSupplierMaster()
        Dim dt As New DataTable
        Dim Tin As String = ""
        Dim iHead, iGroup, iSubGroup, iGL, iSubGL As Integer
        Dim objSettings As New clsSettings
        Dim iid As Integer
        Try
            dt = Session("dtUpload")
            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(0).ToString() = "" And dt.Rows(i)(1).ToString() = "" And dt.Rows(i)(2).ToString() = "" And dt.Rows(i)(3).ToString() = "" And dt.Rows(i)(4).ToString() = "" And dt.Rows(i)(5).ToString() = "" And dt.Rows(i)(6).ToString() = "" And dt.Rows(i)(7).ToString() = "" And dt.Rows(i)(8).ToString() = "" And dt.Rows(i)(9).ToString() = "" And dt.Rows(i)(10).ToString() = "" And dt.Rows(i)(11).ToString() = "" And dt.Rows(i)(12).ToString() = "" And dt.Rows(i)(13).ToString() = "" And dt.Rows(i)(14).ToString() = "" Then

                Else
                    If dt.Rows(i)(0).ToString() = "" Then
                        lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If

                    If dt.Rows(i)(1).ToString() = "" Then
                        lblError.Text = "Name cannot be blank" & " Line No - " & i + 1
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If

                    If dt.Rows(i)(2).ToString() = "" Then
                        lblError.Text = "Contact Person cannot be blank" & " Line No - " & i + 1
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If

                    If dt.Rows(i)(3).ToString() <> "" Then
                        Dim sMail As String
                        sMail = (dt.Rows(i)(3).ToString())
                        If emailaddresscheck(sMail) = False Then
                            lblError.Text = "Enter email address correctly Line No " & i + 1 & " "
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    If dt.Rows(i)(4).ToString() <> "" Then
                        Dim sMobile As String
                        sMobile = (dt.Rows(i)(4).ToString())
                        If mobilenumbercheck(sMobile) = False Then
                            lblError.Text = "Enter mobile number correctly [10 digits] Line No " & i + 1 & " "
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    'If dt.Rows(i)(6).ToString() <> "" Then
                    '    Dim sFax As String
                    '    sFax = (dt.Rows(i)(6).ToString())
                    '    If faxcheck(sFax) = False Then
                    '        lblError.Text = "Enter fax number correctly [min 11 digits, max 13 digits] Line No " & i + 1 & " "
                    '        lblExcelValidationMsg.Text = lblError.Text
                    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    '        Exit Sub
                    '    End If
                    'End If

                    If dt.Rows(i)(11).ToString() <> "" Then
                        Dim sPincode As String
                        sPincode = (dt.Rows(i)(11).ToString())
                        If pincodecheck(sPincode) = False Then
                            lblError.Text = "Enter pincode correctly [6 digits] Line No " & i + 1 & " "
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    'City
                    If dt.Rows(i)(12).ToString() <> "" Then
                        Dim iRet As Integer = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(12).ToString(), 0)
                        If iRet = 0 Then
                            lblError.Text = "Create " & dt.Rows(i)(12).ToString() & " - city in General Master " & " Line No - " & i + 1
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If
                    'State
                    If dt.Rows(i)(13).ToString() <> "" Then
                        Dim iRet As Integer = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(13).ToString(), 1)
                        If iRet = 0 Then
                            lblError.Text = "Create " & dt.Rows(i)(13).ToString() & " - State in General Master " & " Line No - " & i + 1
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    If dt.Rows(i)(14).ToString() <> "" Then
                        Dim sTin As String
                        sTin = (dt.Rows(i)(14).ToString())
                        If tincheck(sTin) = False Then
                            lblError.Text = "Enter GSTIN number correctly [15 digits] Line No " & i + 1 & " "
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    'If dt.Rows(i)(3).ToString() = "" Then
                    '    lblError.Text = "Email ID cannot be blank" & " Line No - " & i + 1
                    '    lblExcelValidationMsg.Text = lblError.Text
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    '    Exit Sub
                    'End If

                    'If dt.Rows(i)(4).ToString() = "" Then
                    '    lblError.Text = "Mobile No cannot be blank" & " Line No - " & i + 1
                    '    lblExcelValidationMsg.Text = lblError.Text
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                    '    Exit Sub
                    'End If

                    'Company type
                    Dim Gstniret As Integer
                    If dt.Rows(i)(15).ToString() <> "" Then
                        Dim iRet As Integer = objclsExcel.CheckCompanyTypeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(15).ToString(), 1)
                        Gstniret = iRet
                        If iRet = 0 Then
                            lblError.Text = "Create " & dt.Rows(i)(15).ToString() & " - Company Type in General Master " & " Line No - " & i + 1
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If


                    'GSTIN Category
                    If dt.Rows(i)(16).ToString() <> "" Then
                        Dim iRet As Integer = objclsExcel.CheckGSTINCategoryExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(16).ToString(), Gstniret)
                        If iRet = 0 Then
                            lblError.Text = "Create " & dt.Rows(i)(16).ToString() & " - GSTIN Category For The Defined CompanyType in General Master " & " Line No - " & i + 1
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                End If

            Next

            Dim dtUpload As New DataTable
            dtUpload = Session("dtUpload")

            If (dtUpload.Rows.Count > 0) Then
                For i = 0 To dtUpload.Rows.Count - 1
                    objclsExcel.CSM_ID = 0
                    objclsExcel.CSM_IndType = 0
                    objclsExcel.CSM_Inventry = 0
                    objclsExcel.CSM_Code = dtUpload.Rows(i)(0).ToString()
                    objclsExcel.CSM_Name = dtUpload.Rows(i)(1).ToString()

                    objclsExcel.CSM_ContactPerson = dtUpload.Rows(i)(2).ToString()
                    objclsExcel.CSM_EmailID = dtUpload.Rows(i)(3).ToString()
                    objclsExcel.CSM_MobileNo = dtUpload.Rows(i)(4).ToString()
                    objclsExcel.CSM_LandLineNo = dtUpload.Rows(i)(5).ToString()
                    objclsExcel.CSM_Fax = dtUpload.Rows(i)(6).ToString()

                    objclsExcel.CSM_Address = dtUpload.Rows(i)(7).ToString()
                    objclsExcel.CSM_Address1 = dtUpload.Rows(i)(8).ToString()
                    objclsExcel.CSM_Address2 = dtUpload.Rows(i)(9).ToString()
                    objclsExcel.CSM_Address3 = dtUpload.Rows(i)(10).ToString()
                    objclsExcel.CSM_Pincode = dtUpload.Rows(i)(11).ToString()

                    Dim iRet As Integer = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(i)(12).ToString(), 0)
                    If iRet > 0 Then
                        objclsExcel.CSM_City = iRet
                    Else
                        objclsExcel.CSM_City = 0
                    End If

                    iRet = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(i)(13).ToString(), 1)
                    If iRet > 0 Then
                        objclsExcel.CSM_State = iRet
                    Else
                        objclsExcel.CSM_State = 0
                    End If

                    iRet = objclsExcel.CheckCompanyTypeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(i)(15).ToString(), 0)
                    Dim compiret As Integer = iRet
                    If iRet > 0 Then
                        objclsExcel.CSM_CompanyType = iRet
                    Else
                        objclsExcel.CSM_CompanyType = 0
                    End If

                    iRet = objclsExcel.CheckGSTINCategoryExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(i)(16).ToString(), compiret)
                    If iRet > 0 Then
                        objclsExcel.CSM_GSTNCategory = iRet
                    Else
                        objclsExcel.CSM_GSTNCategory = 0
                    End If

                    objclsExcel.CSM_Delflag = "A"
                    objclsExcel.CSM_CompID = sSession.AccessCodeID
                    objclsExcel.CSM_Status = "A"
                    objclsExcel.CSM_Operation = "C"
                    objclsExcel.CSM_IPAddress = sSession.IPAddress
                    objclsExcel.CSM_CreatedBy = sSession.UserID
                    objclsExcel.CSM_ProductDescription = ""
                    objclsExcel.CSM_CreatedOn = Date.Today
                    objclsExcel.CSM_ApprovedBy = Nothing
                    objclsExcel.CSM_ApprovedOn = Date.Today
                    objclsExcel.CSM_DeletedBy = Nothing
                    objclsExcel.CSM_DeletedOn = Date.Today
                    objclsExcel.CSM_UpdatedBy = sSession.UserID
                    objclsExcel.CSM_UpdatedOn = Date.Today
                    objclsExcel.CSM_GSTNRegNo = dtUpload.Rows(i)(14).ToString()


                    Dim iCheckSubGL As Integer = 0
                    iCheckSubGL = objclsExcel.CheckSupplierExistOrnot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(i)(0).ToString())

                    Dim sPerm As String = ""
                    Dim sArray1 As Array
                    sPerm = objSettings.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")

                    iHead = sArray1(0)
                    iGroup = sArray1(1)
                    iSubGroup = sArray1(2)
                    iGL = sArray1(3)

                    If iCheckSubGL = 0 Then
                        iSubGL = CreateChartOfAccounts(objclsExcel.CSM_Name, 3, sArray1(3), 4)
                    Else
                        iSubGL = iCheckSubGL
                        objclsExcel.UpdateChartOfAccounts(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(i)(1).ToString(), iCheckSubGL)
                    End If

                    objclsExcel.CSM_Group = iGroup
                    objclsExcel.CSM_SubGroup = iSubGroup
                    objclsExcel.CSM_GL = iGL
                    objclsExcel.CSM_SubGL = iSubGL

                    objclsExcel.SaveCumsterSupplierDetails(sSession.AccessCode, objclsExcel)


                    'Tin
                    If dtUpload.Rows(i)(14).ToString() = "" Then
                        Tin = "0"
                    Else
                        Tin = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(i)(14).ToString(), 1)
                        iid = objclsExcel.GetStatutoryNameValueID(sSession.AccessCode, sSession.AccessCodeID, "Tin")
                        objclsExcel.SaveStatutoryNameValue(sSession.AccessCode, sSession.AccessCodeID, "Tin", dtUpload.Rows(i)(14).ToString(), iid)
                    End If

                Next
            End If

            lblError.Text = "Successfully Uploaded."
            lblExcelValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub SaveCustomerMaster()
        Dim dtUpload As New DataTable : Dim sName As String = "" : Dim sAddress As String = "" : Dim sPinCode As String = "" : Dim sContact As String = "" : Dim sMail As String = ""
        Dim sOffice As String = "" : Dim sMobile As String = "" : Dim Code As String = "" : Dim ContectPerson As String = "" : Dim FaxNo As String = "" : Dim Adress As String = ""
        Dim PinCode As String = "" : Dim city As String = "" : Dim State As String = "" : Dim flag As String = "" : Dim j As Integer

        Dim sAddress1 As String = "" : Dim sAddress2 As String = "" : Dim sAddress3 As String = "" : Dim PreFix As String = "" : Dim TINNo As String = ""
        Dim objSettings As New clsSettings
        Try
            If IsNothing(Session("dtUpload")) = False Then
                dtUpload = Session("dtUpload")
            Else
                lblError.Text = "Laod before you Upload."
                lblExcelValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            dtUpload = Session("dtUpload")

            'Validate Excel Data'
            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1

                    If IsDBNull(dtUpload.Rows(j).Item(2)) = False And dtUpload.Rows(j).Item(2).ToString <> "" And dtUpload.Rows(j).Item(2).ToString <> "&nbsp;" Then 'Checking for blank row

                        If IsDBNull(dtUpload.Rows(j).Item(0)) = False And dtUpload.Rows(j).Item(0).ToString <> "" Then
                            PreFix = dtUpload.Rows(j).Item(0)
                        Else
                            lblError.Text = "PreFix cannot be blank" & "Line No:" & j + 1
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(1)) = False And dtUpload.Rows(j).Item(1).ToString <> "" Then
                            Code = PreFix & " - " & (dtUpload.Rows(j).Item(1))
                        Else
                            lblError.Text = "Party Code cannot be blank" & "Line No:" & j + 1
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(2)) = False And dtUpload.Rows(j).Item(2).ToString <> "" Then
                            sName = (dtUpload.Rows(j).Item(2))
                        Else
                            lblError.Text = "Party Name cannot be blank" & "Line No:" & j + 1
                            lblExcelValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                            Exit Sub
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(3)) = False And dtUpload.Rows(j).Item(3).ToString <> "" Then
                            sAddress = (dtUpload.Rows(j).Item(3))
                        Else
                            sAddress = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(4)) = False And dtUpload.Rows(j).Item(4).ToString <> "" Then
                            sAddress1 = (dtUpload.Rows(j).Item(4))
                        Else
                            sAddress1 = ""
                        End If
                        If IsDBNull(dtUpload.Rows(j).Item(5)) = False And dtUpload.Rows(j).Item(5).ToString <> "" Then
                            sAddress2 = (dtUpload.Rows(j).Item(5))
                        Else
                            sAddress2 = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(6)) = False And dtUpload.Rows(j).Item(6).ToString <> "" Then
                            sAddress3 = (dtUpload.Rows(j).Item(6))
                        Else
                            sAddress3 = ""
                        End If

                        'If IsDBNull(dtUpload.Rows(j).Item(7)) = False And dtUpload.Rows(j).Item(7).ToString <> "" Then
                        '    If IsNumeric(dtUpload.Rows(j).Item(7)) = True Then
                        '        sPinCode = (dtUpload.Rows(j).Item(7))
                        '        If sPinCode.Length < 6 Or sPinCode.Length > 6 Then
                        '            lblError.Text = "Line No " & j + 1 & " PinCode maximum size is 6"
                        '            lblExcelValidationMsg.Text = lblError.Text
                        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        '            Exit Sub
                        '        End If
                        '    Else
                        '        lblError.Text = "Line No " & j + 1 & " PinCode is not valid"
                        '        lblExcelValidationMsg.Text = lblError.Text
                        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        '        Exit Sub
                        '    End If
                        'Else
                        '    sPinCode = ""
                        'End If

                        If dtUpload.Rows(j)(7).ToString() <> "" Then
                            Dim cPincode As String
                            cPincode = (dtUpload.Rows(j)(7).ToString())
                            If pincodecheck(cPincode) = False Then
                                lblError.Text = "Enter pincode correctly [6 digits] Line No " & j + 1 & " "
                                lblExcelValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(8)) = False And dtUpload.Rows(j).Item(8).ToString <> "" Then
                            sContact = (dtUpload.Rows(j).Item(8))
                        Else
                            sContact = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(9)) = False And dtUpload.Rows(j).Item(9).ToString <> "" Then
                            If IsNumeric(dtUpload.Rows(j).Item(9)) = True Then
                                sOffice = (dtUpload.Rows(j).Item(9))
                                If sOffice.Length > 15 Then
                                    lblError.Text = "Line No " & j + 1 & " OfficePhone No maximum size is 15."
                                    lblExcelValidationMsg.Text = lblError.Text
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                                    Exit Sub
                                End If
                            Else
                                sOffice = ""
                            End If
                        Else
                            sOffice = ""
                        End If

                        'If IsDBNull(dtUpload.Rows(j).Item(10)) = False And dtUpload.Rows(j).Item(10).ToString <> "" Then
                        '    If IsNumeric(dtUpload.Rows(j).Item(10)) = True Then
                        '        sMobile = (dtUpload.Rows(j).Item(10))
                        '        If sMobile.Length < 10 Or sMobile.Length > 10 Then
                        '            lblError.Text = "Line No " & j + 1 & " Party Phone No maximum size is 10"
                        '            lblExcelValidationMsg.Text = lblError.Text
                        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        '            Exit Sub
                        '        End If
                        '    Else
                        '        sMobile = ""
                        '    End If
                        'Else
                        '    sMobile = ""
                        'End If

                        If dtUpload.Rows(j)(10).ToString() <> "" Then
                            Dim cMobileNumber As String
                            cMobileNumber = (dtUpload.Rows(j)(10).ToString())
                            If mobilenumbercheck(cMobileNumber) = False Then
                                lblError.Text = "Enter mobile number correctly [10 digits] Line No " & j + 1 & " "
                                lblExcelValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(11)) = False And dtUpload.Rows(j).Item(11).ToString <> "" Then
                            sMail = (dtUpload.Rows(j).Item(11))
                            If emailaddresscheck(sMail) = False Then
                                lblError.Text = "Enter email address correctly Line No " & j + 1 & " "
                                lblExcelValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                                Exit Sub
                            End If
                        Else
                            sMail = ""
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(12)) = False And dtUpload.Rows(j).Item(12).ToString <> "" Then
                            Dim iRet As Integer = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(12), 0)
                            If iRet = 0 Then
                                lblError.Text = "Create " & dtUpload.Rows(j).Item(12) & " - city in General Master " & " Line No - " & j + 1
                                lblExcelValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                                Exit Sub
                            Else
                                city = iRet.ToString()
                            End If
                        End If

                        'State
                        If IsDBNull(dtUpload.Rows(j).Item(13)) = False And dtUpload.Rows(j).Item(13).ToString <> "" Then
                            Dim iRet As Integer = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(13), 1)
                            If iRet = 0 Then
                                lblError.Text = "Create " & dtUpload.Rows(j).Item(13) & " - State in General Master " & " Line No - " & j + 1
                                lblExcelValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                                Exit Sub
                            Else
                                State = iRet.ToString()
                            End If
                        End If

                        If dtUpload.Rows(j)(14).ToString() <> "" Then
                            Dim cFax As String
                            cFax = (dtUpload.Rows(j)(14).ToString())
                            If faxcheck(cFax) = False Then
                                lblError.Text = "Enter fax number correctly [min 11 digits, max 13 digits] Line No " & j + 1 & " "
                                lblExcelValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If

                        If dtUpload.Rows(j)(15).ToString() <> "" Then
                            Dim cTin As String
                            cTin = (dtUpload.Rows(j)(15).ToString())
                            If tincheck(cTin) = False Then
                                lblError.Text = "Enter TIN number correctly [11 digits] Line No " & j + 1 & " "
                                lblExcelValidationMsg.Text = lblError.Text
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If

                        'If IsDBNull(dtUpload.Rows(j).Item(14)) = False And dtUpload.Rows(j).Item(14).ToString <> "" Then
                        '    If IsNumeric(dtUpload.Rows(j).Item(14)) = True Then
                        '        FaxNo = (dtUpload.Rows(j).Item(14))
                        '        If FaxNo.Length > 12 Then
                        '            lblError.Text = "Fax No " & j + 1 & " Fax Maximum size is 12"
                        '            lblExcelValidationMsg.Text = lblError.Text
                        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        '            Exit Sub
                        '        End If
                        '    Else
                        '        lblError.Text = "Fax No " & j + 1 & " Enter valid Fax"
                        '        lblExcelValidationMsg.Text = lblError.Text
                        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        '        Exit Sub
                        '    End If
                        'Else
                        '    FaxNo = ""
                        'End If

                        'If IsDBNull(dtUpload.Rows(j).Item(15)) = False And dtUpload.Rows(j).Item(15).ToString <> "" Then
                        '    If IsNumeric(dtUpload.Rows(j).Item(15)) = True Then
                        '        TINNo = (dtUpload.Rows(j).Item(15))
                        '        If TINNo.Length > 11 Then
                        '            lblError.Text = "TIN No " & j + 1 & " TINNo Exceeds, Maximum size is 11"
                        '            lblExcelValidationMsg.Text = lblError.Text
                        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        '            Exit Sub
                        '        End If
                        '    Else
                        '        lblError.Text = "TIN No " & j + 1 & " Enter valid TINNo"
                        '        lblExcelValidationMsg.Text = lblError.Text
                        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        '        Exit Sub
                        '    End If
                        'Else
                        '    TINNo = ""
                        'End If

                    End If

                Next
            End If
            'Validate Excel Data'

            'Save Excel Data once it is correct format'

            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1

                    If IsDBNull(dtUpload.Rows(j).Item(0)) = False And dtUpload.Rows(j).Item(0).ToString <> "" Then
                        PreFix = dtUpload.Rows(j).Item(0)

                        objclsExcel.BM_ID = 0
                        objclsExcel.BM_IndType = 0
                        objclsExcel.BM_Inventry = 0
                        objclsExcel.BM_Code = PreFix & " - " & (dtUpload.Rows(j).Item(1)).ToString
                        objclsExcel.BM_Name = (dtUpload.Rows(j).Item(2)).ToString
                        objclsExcel.BM_Address = (dtUpload.Rows(j).Item(3)).ToString
                        objclsExcel.BM_Address1 = (dtUpload.Rows(j).Item(4)).ToString
                        objclsExcel.BM_Address2 = (dtUpload.Rows(j).Item(5)).ToString
                        objclsExcel.BM_Address3 = (dtUpload.Rows(j).Item(6)).ToString
                        objclsExcel.BM_Pincode = (dtUpload.Rows(j).Item(7)).ToString
                        objclsExcel.BM_ContactPerson = (dtUpload.Rows(j).Item(8)).ToString
                        objclsExcel.BM_LandLineNo = (dtUpload.Rows(j).Item(9)).ToString
                        objclsExcel.BM_MobileNo = (dtUpload.Rows(j).Item(10)).ToString
                        objclsExcel.BM_EmailID = (dtUpload.Rows(j).Item(11)).ToString

                        If IsDBNull(dtUpload.Rows(j).Item(12)) = False And dtUpload.Rows(j).Item(12).ToString <> "" Then
                            Dim iRet As Integer = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(12), 0)
                            city = iRet.ToString()
                            If city > 0 Then
                                objclsExcel.BM_City = city
                            Else
                                objclsExcel.BM_City = 0
                            End If
                        End If

                        If IsDBNull(dtUpload.Rows(j).Item(13)) = False And dtUpload.Rows(j).Item(13).ToString <> "" Then
                            Dim iRet As Integer = objclsExcel.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(13), 1)
                            State = iRet.ToString()
                            If State > 0 Then
                                objclsExcel.BM_State = State
                            Else
                                objclsExcel.BM_State = 0
                            End If
                        End If

                        objclsExcel.BM_Fax = (dtUpload.Rows(j).Item(14)).ToString
                        TINNo = (dtUpload.Rows(j).Item(15)).ToString

                        objclsExcel.BM_Delflag = "A"
                        objclsExcel.BM_CompID = sSession.AccessCodeID
                        objclsExcel.BM_Status = "A"
                        'objclsExcel.BM_Operation = "C"
                        objclsExcel.BM_IPAddress = sSession.IPAddress
                        objclsExcel.BM_CreatedBy = sSession.UserID
                        objclsExcel.BM_CreatedOn = Date.Today
                        objclsExcel.BM_ApprovedBy = Nothing
                        objclsExcel.BM_ApprovedOn = Date.Today
                        objclsExcel.BM_DeletedBy = Nothing
                        objclsExcel.BM_DeletedOn = Date.Today
                        objclsExcel.BM_UpdatedBy = sSession.UserID
                        objclsExcel.BM_UpdatedOn = Date.Today
                        objclsExcel.BM_YearID = sSession.YearID
                        objclsExcel.BM_GenCategory = 0

                        Dim iCheckSubGL As Integer = 0
                        iCheckSubGL = objclsExcel.CheckCustomersExistOrnot(sSession.AccessCode, sSession.AccessCodeID, objclsExcel.BM_Code)

                        'Saving to Chart Of Accounts'
                        Dim sPerm As String = ""
                        Dim sArray1 As Array
                        sPerm = objSettings.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer")
                        sPerm = sPerm.Remove(0, 1)
                        sArray1 = sPerm.Split(",")

                        Dim iHead, iGroup, iSubGroup, iGL, iSubGL As Integer
                        iHead = sArray1(0) '1
                        iGroup = sArray1(1) '29
                        iSubGroup = sArray1(2) '31
                        iGL = sArray1(3) '146


                        If iCheckSubGL = 0 Then
                            iSubGL = CreateChartOfAccounts(objclsExcel.BM_Name, 3, sArray1(3), 1)
                        Else
                            iSubGL = iCheckSubGL
                            objclsExcel.UpdateChartOfAccounts(sSession.AccessCode, sSession.AccessCodeID, dtUpload.Rows(j).Item(2).ToString(), iCheckSubGL)
                        End If

                        objclsExcel.BM_Group = iGroup
                        objclsExcel.BM_SubGroup = iSubGroup
                        objclsExcel.BM_GL = iGL
                        objclsExcel.BM_SubGL = iSubGL

                        objclsExcel.BM_GSTNRegNo = ""
                        objclsExcel.BM_CompanyType = 0
                        objclsExcel.BM_GSTNCategory = 0

                        objclsExcel.SavePartyDetails(sSession.AccessCode, objclsExcel)
                        objclsExcel.SaveVATPANtable(sSession.AccessCode, sSession.AccessCodeID, sSession.IPAddress, objclsExcel.BM_Code, TINNo)

                    End If

                Next
            End If
            'Save Excel Data once it is correct format'
            lblError.Text = "Uploaded Successfully"
            lblExcelValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function emailaddresscheck(ByVal emailaddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailaddress, pattern)
        If emailAddressMatch.Success Then
            emailaddresscheck = True
        Else
            emailaddresscheck = False
        End If
    End Function

    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer) As Integer
        Dim sRet As String
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try

            objCOA.igl_id = 0
            objCOA.igl_head = iHead
            objCOA.igl_Parent = iParent
            objCOA.sgl_glcode = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, iAccHead, iParent)
            objCOA.sgl_Desc = objGen.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(sName)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = iAccHead
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "A"
            objCOA.sgl_IPAddress = sSession.IPAddress
            sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            sArray = sRet.Split(",")
            Return sArray(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub ExportoExcelOpeningBalance(ByVal dt1 As DataTable)
        Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim dt As System.Data.DataTable
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0, rowIndex As Integer = 0
        Dim sPath As String, strFileNameFullPath As String, strFileNamePath As String, sExcelFileName As String
        Dim i As Integer
        Try
            If dt1.Rows.Count > 0 Then
                dt = dt1
                sPath = Server.MapPath("../") & "SampleExcels\OpeningBalance.xlsx"
                wBook = excel.Workbooks.Add(sPath)
                wSheet = wBook.ActiveSheet()
                For i = 0 To 4
                    colIndex = colIndex + 1
                    excel.Cells(1, colIndex) = dt.Columns(i).ColumnName
                    excel.Cells(1, colIndex).Font.Bold = True
                Next
                For Each dr In dt.Rows
                    rowIndex = rowIndex + 1
                    colIndex = 0
                    For i = 0 To 4
                        colIndex = colIndex + 1
                        excel.Cells(rowIndex + 1, colIndex) = dr(dt.Columns(i).ColumnName)
                    Next
                Next
                wSheet.Columns.AutoFit()
                strFileNamePath = objGenFun.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
                If ddlMasterName.SelectedIndex = 5 Then
                    sExcelFileName = "OpeningBalance.xlsx"
                Else
                    sExcelFileName = "OpeningBalance.xlsx"
                End If
                If strFileNamePath.EndsWith("\") = False Then
                    strFileNameFullPath = strFileNamePath & "\" & sExcelFileName
                Else
                    strFileNameFullPath = strFileNamePath & sExcelFileName
                End If

                Dim blnFileOpen As Boolean = False
                Try
                    If System.IO.File.Exists(strFileNameFullPath) Then
                        System.IO.File.Delete(strFileNameFullPath)
                    End If
                    Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileNameFullPath)
                    fileTemp.Close()
                Catch ex As Exception
                    blnFileOpen = False
                End Try
                If System.IO.File.Exists(strFileNameFullPath) Then
                    System.IO.File.Delete(strFileNameFullPath)
                End If
                wBook.SaveAs(strFileNameFullPath)
                wBook.Close()
                excel.Quit()
                excel = Nothing
                DownloadFile(strFileNameFullPath)
            Else
                lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveOpeningBalance()
        Dim objOP As New clsOpeningBalance
        Dim Arr() As String
        Dim lblGLCode As New Label, lblDebitAmt As New Label, lblCreditAmount As New Label
        Dim iErrorLine As Integer = 0
        Dim iManual As Integer
        Dim sDate As String = ""
        Try
            iManual = objOP.GetManual(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If iManual = 1 Then
                lblMsg.Text = "Opening Balance Has been entered using Opening Balance Form,Opening Balance Excel can not be upload."
                Exit Sub
            End If

            'Remove Data'
            objclsExcel.RemoveOpBal(sSession.AccessCode, sSession.AccessCodeID)
            'Remove Data'
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                objOP.sOpn_GLCode = dgGeneral.Items(i).Cells(1).Text
                lblGLCode.Text = dgGeneral.Items(i).Cells(1).Text
                If (dgGeneral.Items(i).Cells(3).Text <> "") And (dgGeneral.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objOP.dOpn_DebitAmt = dgGeneral.Items(i).Cells(3).Text
                    lblDebitAmt.Text = dgGeneral.Items(i).Cells(3).Text
                Else
                    objOP.dOpn_DebitAmt = "0.00"
                    lblDebitAmt.Text = ""
                End If

                If (dgGeneral.Items(i).Cells(4).Text <> "") And (dgGeneral.Items(i).Cells(4).Text <> "&nbsp;") Then
                    objOP.dOpn_CreditAmount = dgGeneral.Items(i).Cells(4).Text
                    lblCreditAmount.Text = dgGeneral.Items(i).Cells(4).Text
                Else
                    objOP.dOpn_CreditAmount = "0.00"
                    lblCreditAmount.Text = ""
                End If

                'If lblGLCode.Text.Trim = "" Then
                '    lblExcelValidationMsg.Text = "Enter Debit Amount. Line No " & iErrorLine & "." : lblError.Text = "Enter Debit Amount. Line No " & iErrorLine & "."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Sub
                'ElseIf lblGLCode.Text.Trim.Length > 50 Then
                '    lblExcelValidationMsg.Text = "GLCode exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "GLCode exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Sub
                'End If

                'If lblDebitAmt.Text.Trim = "" Then
                '    lblExcelValidationMsg.Text = "Enter Debit Amount. Line No " & iErrorLine & "." : lblError.Text = "Enter Debit Amount. Line No " & iErrorLine & "."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Sub
                'End If

                'If lblCreditAmount.Text.Trim = "" Then
                '    lblExcelValidationMsg.Text = "Enter Credit Amount. Line No " & iErrorLine & "." : lblError.Text = "Enter Credit Amount. Line No " & iErrorLine & "."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Sub
                'End If
                sDate = Date.Today
                objOP.dOpn_Date = objGenFun.GetFinancialYearFromDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID) 'Date.Today
                'Date.ParseExact(sDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objOP.iOpn_YearId = sSession.YearID
                objOP.iOpn_CompId = sSession.AccessCodeID
                objOP.iOpn_GlId = objclsExcel.GetGLID(sSession.AccessCode, sSession.AccessCodeID, objOP.sOpn_GLCode)
                objOP.sOpn_IPAddress = sSession.IPAddress
                objOP.iOpn_AccHead = objclsExcel.GetAccHeadID(sSession.AccessCode, sSession.AccessCodeID, objOP.sOpn_GLCode)
                objOP.iOpn_Manual = 2

                objOP.iOpn_ZoneID = ddlAccZone.SelectedValue
                objOP.iOpn_RegionID = ddlAccRgn.SelectedValue
                objOP.iOpn_AreaID = ddlAccArea.SelectedValue
                objOP.iOpn_BranchID = ddlAccBrnch.SelectedValue

                Arr = objOP.SaveOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objOP)
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgUpload_PreRender(sender As Object, e As EventArgs) Handles dgUpload.PreRender
        Dim dt As New DataTable
        Try
            If dgUpload.Rows.Count > 0 Then
                dgUpload.UseAccessibleHeader = True
                dgUpload.HeaderRow.TableSection = TableRowSection.TableHeader
                dgUpload.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgUpload_PreRender")
        End Try
    End Sub
    'Public Function SAvePhyscalStock() As String
    '    Dim dt As New DataTable
    '    Dim dt1 As New DataTable
    '    Dim Arr() As String, sSql As String = ""
    '    Dim dr As OleDb.OleDbDataReader
    '    Dim iParent, iMax As Integer, iUnit As Integer = 0, iAlternative As Integer = 0, iExistID As Integer, iExistStockID As Integer, iHistoryID As Integer = 0, iPerPiece As Integer = 0
    '    Dim sCommidity As String = "", sVat As String = "", sExcise As String = "", sColor As String = "", sSize As String = "", sAcode As String = "", scst As String = ""
    '    'Dim sCst As String = ""
    '    Dim dMRP As Double = "0.0", dRetail As Double = "0.0", dPreDetermined As Double = "0.0", dOthers As Double = "0.0"
    '    Dim iPQuantity As Integer, iOpeningQty As Integer, igetVat As Integer, igetCst As Integer, iExcise As Integer
    '    Dim dEffectiveFrom As Date, dEffectiveTo As Date
    '    Dim bCheck As Boolean
    '    Try

    '        bCheck = objphysicalstock.CheckPhysicalStockUpload(sSession.AccessCode, sSession.AccessCodeID)
    '        If bCheck = True Then
    '            lblError.Text = "Physical Stock Upload has been done, can not be upload again."
    '            Exit Function
    '        End If

    '        If dgUpload.Rows.Count = 0 Then
    '            lblError.Text = "Please select the Load Button "
    '            lblError.Text = "Please select the Load Button "
    '            Exit Function
    '        End If
    '        dt = Session("dtUpload")
    '        For i = 0 To dt.Rows.Count - 1

    '            If dt.Rows(i)(0).ToString() <> "" And dt.Rows(i)(1).ToString() <> "" And dt.Rows(i)(2).ToString() <> "" And dt.Rows(i)(3).ToString() <> "" And dt.Rows(i)(4).ToString() <> "" Then

    '                If dt.Rows(i)(0).ToString() = "" Then
    '                    dt.Rows(i)(0) = sCommidity
    '                    If (sCommidity = "") Then
    '                        lblError.Text = "Commidity cannot be blank" & " Line No - " & i + 1
    '                        lblError.Text = "Commidity cannot be blank" & " Line No - " & i + 1
    '                        Exit Function
    '                    End If
    '                Else
    '                    sCommidity = dt.Rows(i)(0).ToString()
    '                End If

    '                If dt.Rows(i)(1).ToString() = "" Then
    '                    lblError.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
    '                    lblError.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
    '                    Exit Function
    '                End If

    '                If dt.Rows(i)(2).ToString() = "" Then
    '                    lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
    '                    lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
    '                    Exit Function
    '                End If

    '                If dt.Rows(i)(3).ToString() = "" Then
    '                    lblError.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
    '                    lblError.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
    '                    Exit Function
    '                Else
    '                    If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString()) = 0 Then
    '                        lblError.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
    '                        lblError.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
    '                        Exit Function
    '                    End If
    '                End If

    '                If dt.Rows(i)(4).ToString() = "" Then
    '                    lblError.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
    '                    lblError.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
    '                    Exit Function
    '                Else
    '                    If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString()) = 0 Then
    '                        lblError.Text = "Create Alternate unit of Measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
    '                        lblError.Text = "Create Alternate unit of measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
    '                        Exit Function
    '                    End If
    '                End If

    '                'If dt.Rows(i)(6).ToString() = "" Then
    '                '    lblError.Text = "VAT Cannot be blank" & " Line No - " & i + 1
    '                '    Exit Function
    '                'Else
    '                '    If objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString()) = 0 Then
    '                '        lblError.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
    '                '        lblError.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
    '                '        Exit Function
    '                '    End If
    '                'End If

    '                'If dt.Rows(i)(7).ToString() = "" Then
    '                '    lblError.Text = "Excise Cannot be blank" & " Line No - " & i + 1
    '                '    Exit Function
    '                'Else
    '                '    If objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString()) = 0 Then
    '                '        lblError.Text = "Create Excise Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
    '                '        lblError.Text = "Create Excise Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
    '                '        Exit Function
    '                '    End If
    '                'End If

    '                'If dt.Rows(i)(8).ToString() = "" Then
    '                '    lblError.Text = "VAT CST can not  be blank" & " Line No - " & i + 1
    '                '    Exit Function
    '                'Else
    '                '    If objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString()) = 0 Then
    '                '        lblError.Text = "Create CST Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
    '                '        lblError.Text = "Create CST Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
    '                '        Exit Function
    '                '    End If
    '                'End If

    '                'Qty in Pieces
    '                If dt.Rows(i)(5).ToString() <> "" Then
    '                    iPerPiece = dt.Rows(i)(5).ToString()
    '                Else
    '                    iPerPiece = "0"
    '                End If

    '                'VAT
    '                'If dt.Rows(i)(6).ToString() <> "" Then
    '                '    sVat = dt.Rows(i)(6).ToString()
    '                'Else
    '                '    sVat = "0"
    '                'End If

    '                'Excise
    '                'If dt.Rows(i)(7).ToString() <> "" Then
    '                '    sExcise = dt.Rows(i)(7).ToString()
    '                'Else
    '                '    sExcise = "0"
    '                'End If

    '                'CST
    '                'If dt.Rows(i)(8).ToString() <> "" Then
    '                '    scst = dt.Rows(i)(8).ToString()
    '                'Else
    '                '    scst = "0"
    '                'End If

    '                'MRP
    '                If dt.Rows(i)(6).ToString() <> "" Then
    '                    dMRP = Convert.ToDouble(dt.Rows(i)(6).ToString())
    '                Else
    '                    dMRP = "0.0"
    '                End If

    '                'Retail
    '                If dt.Rows(i)(7).ToString() <> "" Then
    '                    dRetail = Convert.ToDouble(dt.Rows(i)(7).ToString())
    '                Else
    '                    dRetail = "0.0"
    '                End If


    '                If Trim(dt.Rows(i)(8).ToString()) = "" Then
    '                    dEffectiveFrom = "01/01/1900"
    '                Else
    '                    'dEffectiveFrom = objGen.FormatDtForRDBMS(Trim(dt.Rows(i)(8).ToString()), "D")
    '                    dEffectiveFrom = Date.ParseExact(Trim(dt.Rows(i)(8)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                End If

    '                Dim sEffectiveTo As String = ""
    '                If Trim(dt.Rows(i)(9).ToString()) = "" Then
    '                    'dEffectiveTo = "01/01/1900"
    '                    sEffectiveTo = "01/01/1900"
    '                Else
    '                    'dEffectiveTo = objGen.FormatDtForRDBMS(Trim(dt.Rows(i)(9).ToString()), "D")
    '                    'dEffectiveTo = Trim(dt.Rows(i)(9))
    '                    sEffectiveTo = Trim(dt.Rows(i)(9))
    '                End If

    '                'PreDetermined
    '                If dt.Rows(i)(10).ToString() <> "" Then
    '                    dPreDetermined = Convert.ToDouble(dt.Rows(i)(10).ToString())
    '                Else
    '                    dPreDetermined = "0.0"
    '                End If

    '                'Others
    '                If dt.Rows(i)(11).ToString() <> "" Then
    '                    dOthers = dt.Rows(i)(11).ToString()
    '                Else
    '                    dOthers = "0.0"
    '                End If

    '                'Color
    '                If dt.Rows(i)(12).ToString() <> "" Then
    '                    sColor = dt.Rows(i)(12).ToString()
    '                Else
    '                    sColor = ""
    '                End If

    '                'Size
    '                If dt.Rows(i)(13).ToString() <> "" Then
    '                    sSize = dt.Rows(i)(13).ToString()
    '                Else
    '                    sSize = "0"
    '                End If

    '                'Alternative No/Color Code
    '                If dt.Rows(i)(14).ToString() <> "" Then
    '                    sAcode = dt.Rows(i)(14).ToString()
    '                Else
    '                    sAcode = ""
    '                End If

    '                'Physical Quantity
    '                If dt.Rows(i)(15).ToString() <> "" Then
    '                    iPQuantity = dt.Rows(i)(15).ToString()
    '                Else
    '                    iPQuantity = "0"
    '                End If

    '                objphysicalstock.Inv_Code = ""
    '                objphysicalstock.Inv_Description = dt.Rows(i)(0).ToString()
    '                objphysicalstock.Inv_Flag = "x"
    '                objphysicalstock.Inv_CompID = sSession.AccessCodeID
    '                objphysicalstock.InvH_Flag = "x"
    '                objphysicalstock.InvH_Excise = sExcise
    '                objphysicalstock.InvH_Cst = scst
    '                objphysicalstock.InvH_Vat = sVat
    '                objphysicalstock.Inv_Size = sSize
    '                objphysicalstock.Inv_Color = sColor
    '                objphysicalstock.Inv_Acode = sAcode
    '                objphysicalstock.Inv_CreatedBy = sSession.UserID

    '                'Inventory Master
    '                iParent = objphysicalstock.CheckCommidityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(0).ToString())
    '                If iParent = 0 Then
    '                    objphysicalstock.Inv_ID = 0
    '                    objphysicalstock.Inv_Code = dt.Rows(i)(0).ToString()
    '                    objphysicalstock.Inv_Parent = iParent
    '                    Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
    '                    iParent = Arr(1)
    '                End If

    '                objphysicalstock.Inv_Parent = iParent
    '                objphysicalstock.Inv_Code = dt.Rows(i)(2).ToString()
    '                objphysicalstock.Inv_Description = dt.Rows(i)(1).ToString()

    '                'Inventory Master Description
    '                iExistID = objphysicalstock.CheckDescriptionExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(2).ToString())
    '                objphysicalstock.Inv_ID = iExistID
    '                If iExistID = 0 Then
    '                    objphysicalstock.Inv_ID = 0
    '                    objphysicalstock.Inv_Parent = iParent
    '                    Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
    '                    'iParent = Arr(1)
    '                    iHistoryID = Arr(1)
    '                Else
    '                    Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
    '                    iHistoryID = Arr(1)
    '                End If
    '                'Inventory Master History
    '                iUnit = objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString())
    '                iAlternative = objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString())
    '                iExistID = objphysicalstock.CheckInventoryMasterHistory(sSession.AccessCode, sSession.AccessCodeID, iHistoryID)
    '                objphysicalstock.InvH_Flag = "x"
    '                objphysicalstock.InvH_Unit = iUnit
    '                objphysicalstock.InvH_AlterUnit = iAlternative
    '                objphysicalstock.InvH_Excise = sExcise
    '                objphysicalstock.InvH_Cst = scst
    '                objphysicalstock.InvH_Vat = sVat
    '                objphysicalstock.InvH_CreatedBy = sSession.UserID
    '                objphysicalstock.InvH_CompID = sSession.AccessCodeID
    '                objphysicalstock.InvH_PerPieces = iPerPiece
    '                objphysicalstock.INVH_MRP = dMRP
    '                objphysicalstock.INVH_Retail = dRetail
    '                objphysicalstock.INVH_PreDeterminedPrice = dPreDetermined

    '                objphysicalstock.INVH_EffeFrom = objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                objphysicalstock.INVH_EffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '                'Extra'
    '                objphysicalstock.INVH_RetailEffeFrom = objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                objphysicalstock.INVH_RetailEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                objphysicalstock.INVH_PurchaseEffeFrom = objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                objphysicalstock.INVH_PurchaseEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                'Extra'

    '                'Rakshan
    '                'objphysicalstock.INVH_EffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                'objphysicalstock.INVH_EffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                ''Extra'
    '                'objphysicalstock.INVH_RetailEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                'objphysicalstock.INVH_RetailEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                'objphysicalstock.INVH_PurchaseEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                'objphysicalstock.INVH_PurchaseEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")
    '                'Rakshan

    '                objphysicalstock.INVH_Others = dOthers
    '                objphysicalstock.InvH_ID = iExistID
    '                objphysicalstock.InvH_INV_ID = iHistoryID
    '                objphysicalstock.SL_IPAddress = sSession.IPAddress
    '                If iExistID = 0 Then
    '                    objphysicalstock.InvH_ID = 0
    '                    Arr = objphysicalstock.SaveInventoryMasterHistory(sSession.AccessCode, objphysicalstock)
    '                    iHistoryID = Arr(1)
    '                    objphysicalstock.InvH_ID = iHistoryID
    '                Else
    '                    Arr = objphysicalstock.SaveInventoryMasterHistory(sSession.AccessCode, objphysicalstock)
    '                    iHistoryID = Arr(1)
    '                    objphysicalstock.InvH_ID = iHistoryID
    '                End If

    '                'Stock Ledger
    '                iParent = objphysicalstock.CheckCommidityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(0).ToString())
    '                iExistID = objphysicalstock.CheckDescriptionExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(2).ToString())
    '                iExistStockID = objphysicalstock.GetPhysicalStock(sSession.AccessCode, sSession.AccessCodeID, iParent, iExistID)

    '                'Tax details
    '                '--- Bcz of GST implementation ---'
    '                'objphysicalstock.InvH_Excise = objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString())
    '                'objphysicalstock.InvH_Cst = objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString())
    '                'objphysicalstock.InvH_Vat = objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString())
    '                'Arr = objphysicalstock.SaveTaxDetails(sSession.AccessCode, objphysicalstock)
    '                '--- Bcz of GST implementation ---'

    '                objphysicalstock.SL_Commodity = iParent
    '                objphysicalstock.SL_ItemID = iExistID
    '                objphysicalstock.SL_CompID = sSession.AccessCodeID
    '                objphysicalstock.SL_YearID = sSession.YearID
    '                objphysicalstock.SL_CrBy = sSession.UserID
    '                objphysicalstock.SL_UpdatedBy = sSession.UserID
    '                objphysicalstock.SL_IPAddress = sSession.IPAddress
    '                objphysicalstock.SL_historyId = iHistoryID
    '                objphysicalstock.PreDetermined = dPreDetermined
    '                objphysicalstock.SL_OpeningBalanceAmount = 0
    '                If iExistStockID = 0 Then
    '                    objphysicalstock.SL_ID = 0
    '                    objphysicalstock.SL_OpeningBalanceQty = iPQuantity
    '                    objphysicalstock.SL_ClosingBalanceQty = iPQuantity
    '                    Arr = objphysicalstock.SaveStockLedger(sSession.AccessCode, objphysicalstock)
    '                Else
    '                    objphysicalstock.SL_ID = iExistStockID
    '                    iOpeningQty = objphysicalstock.GetOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, iParent, iExistID)
    '                    iOpeningQty = iOpeningQty + iPQuantity
    '                    objphysicalstock.SL_OpeningBalanceQty = iOpeningQty
    '                    objphysicalstock.SL_ClosingBalanceQty = iOpeningQty
    '                    Arr = objphysicalstock.SaveStockLedger(sSession.AccessCode, objphysicalstock)
    '                End If

    '            End If
    '        Next
    '        lblError.Text = "Successfully Upload" : lblError.Text = "Successfully Upload"
    '        'clsSupplierUpload.SaveCommidityMaster(sSession.AccessCode, sSession.AccessCodeID, Session("dtUpload"), sSession.UserID, sSession.IPAddress)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function SAvePhyscalStock() As String
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim Arr() As String, sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iParent, iMax As Integer, iUnit As Integer = 0, iAlternative As Integer = 0, iExistID As Integer, iExistStockID As Integer, iHistoryID As Integer = 0, iPerPiece As Integer = 0
        Dim sCommidity As String = "", sVat As String = "", sExcise As String = "", sColor As String = "", sSize As String = "", sAcode As String = "", scst As String = ""
        'Dim sCst As String = ""
        Dim dMRP As Double = "0.0", dRetail As Double = "0.0", dPreDetermined As Double = "0.0", dOthers As Double = "0.0"
        Dim iPQuantity As Integer, iOpeningQty As Integer, igetVat As Integer, igetCst As Integer, iExcise As Integer
        'Dim dEffectiveFrom As Date, dEffectiveTo As Date
        Dim bCheck As Boolean
        Try

            bCheck = objphysicalstock.CheckPhysicalStockUpload(sSession.AccessCode, sSession.AccessCodeID)
            If bCheck = True Then
                lblError.Text = "Physical Stock Upload has been done, can not be upload again."
                Exit Function
            End If

            If dgUpload.Rows.Count = 0 Then
                lblError.Text = "Please select the Load Button "
                lblError.Text = "Please select the Load Button "
                Exit Function
            End If
            dt = Session("dtUpload")
            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(0).ToString() <> "" And dt.Rows(i)(1).ToString() <> "" And dt.Rows(i)(2).ToString() <> "" And dt.Rows(i)(3).ToString() <> "" And dt.Rows(i)(4).ToString() <> "" Then

                    If dt.Rows(i)(0).ToString() = "" Then
                        dt.Rows(i)(0) = sCommidity
                        If (sCommidity = "") Then
                            lblError.Text = "Commidity cannot be blank" & " Line No - " & i + 1
                            lblError.Text = "Commidity cannot be blank" & " Line No - " & i + 1
                            Exit Function
                        End If
                    Else
                        sCommidity = dt.Rows(i)(0).ToString()
                    End If

                    If dt.Rows(i)(1).ToString() = "" Then
                        lblError.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
                        lblError.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    End If

                    If dt.Rows(i)(2).ToString() = "" Then
                        lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
                        lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    End If

                    If dt.Rows(i)(3).ToString() = "" Then
                        lblError.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                        lblError.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    Else
                        If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString()) = 0 Then
                            lblError.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                            lblError.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                            Exit Function
                        End If
                    End If

                    If dt.Rows(i)(4).ToString() = "" Then
                        lblError.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                        lblError.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    Else
                        If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString()) = 0 Then
                            lblError.Text = "Create Alternate unit of Measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                            lblError.Text = "Create Alternate unit of measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                            Exit Function
                        End If
                    End If

                    If rboWithGST.Checked = True Then
                        If dt.Rows(i)(16).ToString() = "" Then
                            lblError.Text = "GST Rate cannot be blank" & " Line No - " & i + 1
                            lblError.Text = "GST Rate cannot be blank" & " Line No - " & i + 1
                            Exit Function
                        End If
                        If dt.Rows(i)(17).ToString() = "" Then
                            lblError.Text = "HSN No cannot be blank" & " Line No - " & i + 1
                            lblError.Text = "HSN No cannot be blank" & " Line No - " & i + 1
                            Exit Function
                        End If
                    End If

                    'If dt.Rows(i)(6).ToString() = "" Then
                    '    lblError.Text = "VAT Cannot be blank" & " Line No - " & i + 1
                    '    Exit Function
                    'Else
                    '    If objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString()) = 0 Then
                    '        lblError.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
                    '        lblError.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
                    '        Exit Function
                    '    End If
                    'End If

                    'If dt.Rows(i)(7).ToString() = "" Then
                    '    lblError.Text = "Excise Cannot be blank" & " Line No - " & i + 1
                    '    Exit Function
                    'Else
                    '    If objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString()) = 0 Then
                    '        lblError.Text = "Create Excise Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
                    '        lblError.Text = "Create Excise Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
                    '        Exit Function
                    '    End If
                    'End If

                    'If dt.Rows(i)(8).ToString() = "" Then
                    '    lblError.Text = "VAT CST can not  be blank" & " Line No - " & i + 1
                    '    Exit Function
                    'Else
                    '    If objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString()) = 0 Then
                    '        lblError.Text = "Create CST Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
                    '        lblError.Text = "Create CST Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
                    '        Exit Function
                    '    End If
                    'End If

                    'Qty in Pieces
                    If dt.Rows(i)(5).ToString() <> "" Then
                        iPerPiece = dt.Rows(i)(5).ToString()
                    Else
                        iPerPiece = "0"
                    End If

                    'VAT
                    'If dt.Rows(i)(6).ToString() <> "" Then
                    '    sVat = dt.Rows(i)(6).ToString()
                    'Else
                    '    sVat = "0"
                    'End If

                    'Excise
                    'If dt.Rows(i)(7).ToString() <> "" Then
                    '    sExcise = dt.Rows(i)(7).ToString()
                    'Else
                    '    sExcise = "0"
                    'End If

                    'CST
                    'If dt.Rows(i)(8).ToString() <> "" Then
                    '    scst = dt.Rows(i)(8).ToString()
                    'Else
                    '    scst = "0"
                    'End If

                    'MRP
                    If dt.Rows(i)(6).ToString() <> "" Then
                        dMRP = Convert.ToDouble(dt.Rows(i)(6).ToString())
                    Else
                        dMRP = "0.0"
                    End If

                    'Retail
                    If dt.Rows(i)(7).ToString() <> "" Then
                        dRetail = Convert.ToDouble(dt.Rows(i)(7).ToString())
                    Else
                        dRetail = "0.0"
                    End If

                    Dim dEffectiveFrom As String = ""
                    If Trim(dt.Rows(i)(8).ToString()) = "" Then
                        dEffectiveFrom = "01/01/1900"
                    Else
                        'dEffectiveFrom = objGen.FormatDtForRDBMS(Trim(dt.Rows(i)(8).ToString()), "D")
                        dEffectiveFrom = Trim(dt.Rows(i)(8))
                    End If

                    Dim sEffectiveTo As String = ""
                    If Trim(dt.Rows(i)(9).ToString()) = "" Then
                        'dEffectiveTo = "01/01/1900"
                        sEffectiveTo = "01/01/1900"
                    Else
                        'dEffectiveTo = objGen.FormatDtForRDBMS(Trim(dt.Rows(i)(9).ToString()), "D")
                        'dEffectiveTo = Trim(dt.Rows(i)(9))
                        sEffectiveTo = Trim(dt.Rows(i)(9))
                    End If

                    'PreDetermined
                    If dt.Rows(i)(10).ToString() <> "" Then
                        dPreDetermined = Convert.ToDouble(dt.Rows(i)(10).ToString())
                    Else
                        dPreDetermined = "0.0"
                    End If

                    'Others
                    If dt.Rows(i)(11).ToString() <> "" Then
                        dOthers = dt.Rows(i)(11).ToString()
                    Else
                        dOthers = "0.0"
                    End If

                    'Color
                    If dt.Rows(i)(12).ToString() <> "" Then
                        sColor = dt.Rows(i)(12).ToString()
                    Else
                        sColor = ""
                    End If

                    'Size
                    If dt.Rows(i)(13).ToString() <> "" Then
                        sSize = dt.Rows(i)(13).ToString()
                    Else
                        sSize = "0"
                    End If

                    'Alternative No/Color Code
                    If dt.Rows(i)(14).ToString() <> "" Then
                        sAcode = dt.Rows(i)(14).ToString()
                    Else
                        sAcode = ""
                    End If

                    'Physical Quantity
                    If dt.Rows(i)(15).ToString() <> "" Then
                        iPQuantity = dt.Rows(i)(15).ToString()
                    Else
                        iPQuantity = "0"
                    End If

                    objphysicalstock.Inv_Code = ""
                    objphysicalstock.Inv_Description = dt.Rows(i)(0).ToString()
                    objphysicalstock.Inv_Flag = "x"
                    objphysicalstock.Inv_CompID = sSession.AccessCodeID
                    objphysicalstock.InvH_Flag = "x"
                    objphysicalstock.InvH_Excise = sExcise
                    objphysicalstock.InvH_Cst = scst
                    objphysicalstock.InvH_Vat = sVat
                    objphysicalstock.Inv_Size = sSize
                    objphysicalstock.Inv_Color = sColor
                    objphysicalstock.Inv_Acode = sAcode
                    objphysicalstock.Inv_CreatedBy = sSession.UserID

                    'Inventory Master
                    iParent = objphysicalstock.CheckCommidityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(0).ToString())
                    If iParent = 0 Then
                        objphysicalstock.Inv_ID = 0
                        objphysicalstock.Inv_Code = dt.Rows(i)(0).ToString()
                        objphysicalstock.Inv_Parent = iParent
                        Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
                        iParent = Arr(1)
                    End If

                    objphysicalstock.Inv_Parent = iParent
                    objphysicalstock.Inv_Code = dt.Rows(i)(2).ToString()
                    objphysicalstock.Inv_Description = dt.Rows(i)(1).ToString()

                    'Inventory Master Description
                    iExistID = objphysicalstock.CheckDescriptionExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(2).ToString())
                    objphysicalstock.Inv_ID = iExistID
                    If iExistID = 0 Then
                        objphysicalstock.Inv_ID = 0
                        objphysicalstock.Inv_Parent = iParent
                        Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
                        'iParent = Arr(1)
                        iHistoryID = Arr(1)
                    Else
                        Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
                        iHistoryID = Arr(1)
                    End If
                    'Inventory Master History
                    iUnit = objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString())
                    iAlternative = objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString())
                    iExistID = objphysicalstock.CheckInventoryMasterHistory(sSession.AccessCode, sSession.AccessCodeID, iHistoryID)
                    objphysicalstock.InvH_Flag = "x"
                    objphysicalstock.InvH_Unit = iUnit
                    objphysicalstock.InvH_AlterUnit = iAlternative
                    objphysicalstock.InvH_Excise = sExcise
                    objphysicalstock.InvH_Cst = scst
                    objphysicalstock.InvH_Vat = sVat
                    objphysicalstock.InvH_CreatedBy = sSession.UserID
                    objphysicalstock.InvH_CompID = sSession.AccessCodeID
                    objphysicalstock.InvH_PerPieces = iPerPiece
                    objphysicalstock.INVH_MRP = dMRP
                    objphysicalstock.INVH_Retail = dRetail
                    objphysicalstock.INVH_PreDeterminedPrice = dPreDetermined

                    objphysicalstock.INVH_EffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objphysicalstock.INVH_EffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    'Extra'
                    objphysicalstock.INVH_RetailEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objphysicalstock.INVH_RetailEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    objphysicalstock.INVH_PurchaseEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objphysicalstock.INVH_PurchaseEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    'Extra'

                    'Rakshan
                    'objphysicalstock.INVH_EffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
                    'objphysicalstock.INVH_EffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    ''Extra'
                    'objphysicalstock.INVH_RetailEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
                    'objphysicalstock.INVH_RetailEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    'objphysicalstock.INVH_PurchaseEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
                    'objphysicalstock.INVH_PurchaseEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")
                    'Rakshan

                    objphysicalstock.INVH_Others = dOthers
                    objphysicalstock.InvH_ID = iExistID
                    objphysicalstock.InvH_INV_ID = iHistoryID
                    objphysicalstock.SL_IPAddress = sSession.IPAddress
                    If iExistID = 0 Then
                        objphysicalstock.InvH_ID = 0
                        Arr = objphysicalstock.SaveInventoryMasterHistory(sSession.AccessCode, objphysicalstock)
                        iHistoryID = Arr(1)
                        objphysicalstock.InvH_ID = iHistoryID
                    Else
                        Arr = objphysicalstock.SaveInventoryMasterHistory(sSession.AccessCode, objphysicalstock)
                        iHistoryID = Arr(1)
                        objphysicalstock.InvH_ID = iHistoryID
                    End If

                    'Stock Ledger
                    iParent = objphysicalstock.CheckCommidityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(0).ToString())
                    iExistID = objphysicalstock.CheckDescriptionExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(2).ToString())
                    iExistStockID = objphysicalstock.GetPhysicalStock(sSession.AccessCode, sSession.AccessCodeID, iParent, iExistID)

                    'Tax details
                    '--- Bcz of GST implementation ---'
                    'objphysicalstock.InvH_Excise = objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString())
                    'objphysicalstock.InvH_Cst = objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString())
                    'objphysicalstock.InvH_Vat = objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString())
                    'Arr = objphysicalstock.SaveTaxDetails(sSession.AccessCode, objphysicalstock)
                    '--- Bcz of GST implementation ---'

                    objphysicalstock.SL_Commodity = iParent
                    objphysicalstock.SL_ItemID = iExistID
                    objphysicalstock.SL_CompID = sSession.AccessCodeID
                    objphysicalstock.SL_YearID = sSession.YearID
                    objphysicalstock.SL_CrBy = sSession.UserID
                    objphysicalstock.SL_UpdatedBy = sSession.UserID
                    objphysicalstock.SL_IPAddress = sSession.IPAddress
                    objphysicalstock.SL_historyId = iHistoryID
                    objphysicalstock.PreDetermined = dPreDetermined
                    objphysicalstock.SL_OpeningBalanceAmount = 0
                    If ddlAccBrnch.SelectedIndex > 0 Then
                        objphysicalstock.SL_Branch = ddlAccBrnch.SelectedValue
                    Else
                        lblError.Text = "Select Branch for which you wish to upload physical stock."
                        Exit Function
                    End If
                    If iExistStockID = 0 Then
                        objphysicalstock.SL_ID = 0
                        objphysicalstock.SL_OpeningBalanceQty = iPQuantity
                        objphysicalstock.SL_ClosingBalanceQty = iPQuantity
                        Arr = objphysicalstock.SaveStockLedger(sSession.AccessCode, objphysicalstock)
                    Else
                        objphysicalstock.SL_ID = iExistStockID
                        iOpeningQty = objphysicalstock.GetOpeningBalance(sSession.AccessCode, sSession.AccessCodeID, iParent, iExistID)
                        iOpeningQty = iOpeningQty + iPQuantity
                        objphysicalstock.SL_OpeningBalanceQty = iOpeningQty
                        objphysicalstock.SL_ClosingBalanceQty = iOpeningQty
                        Arr = objphysicalstock.SaveStockLedger(sSession.AccessCode, objphysicalstock)
                    End If

                    Dim iCommodityID, iItemID As Integer

                    'Save To GST Rate Table'
                    If rboWithGST.Checked = True Then
                        iCommodityID = objclsExcel.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(1).ToString())
                        iItemID = objclsExcel.GetItemID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(1).ToString())

                        objInvS.iGST_ID = 0
                        If iCommodityID = 0 Then

                        Else
                            objInvS.iGST_ScheduleID = 0
                            objInvS.iGST_CommodityID = iCommodityID
                            objInvS.iGST_ItemID = iItemID

                            objInvS.iGST_ScheduleType = 0

                            If dt.Rows(i)(16).ToString() <> "" Then
                                objInvS.dGST_GSTRate = dt.Rows(i)(16).ToString()
                            Else
                                objInvS.dGST_GSTRate = 0
                            End If

                            objInvS.sGST_SlNo = ""

                            If dt.Rows(i)(17).ToString() <> "" Then
                                objInvS.sGST_CHST = dt.Rows(i)(17).ToString()
                            Else
                                objInvS.sGST_CHST = ""
                            End If

                            If dt.Rows(i)(18).ToString() <> "" Then
                                objInvS.sGST_Chapter = dt.Rows(i)(18).ToString()
                            Else
                                objInvS.sGST_Chapter = ""
                            End If

                            If dt.Rows(i)(18).ToString() <> "" Then
                                objInvS.sGST_Heading = dt.Rows(i)(18).ToString()
                            Else
                                objInvS.sGST_Heading = ""
                            End If

                            objInvS.sGST_SubHeading = ""
                            objInvS.sGST_Tarrif = ""
                            objInvS.sGST_SubSlNo = ""
                            objInvS.sGST_CESS = 0
                            objInvS.sGST_GoodDescription = ""
                            objInvS.sGST_NotificationNo = ""
                            objInvS.dGST_NotificationFromDate = "01/01/1900"
                            objInvS.dGST_NotificationToDate = "01/01/1900"
                            objInvS.sGST_FileNo = ""
                            objInvS.dGST_FileFromDate = "01/01/1900"
                            objInvS.dGST_FileToDate = "01/01/1900"

                            objInvS.sGST_Status = "W"
                            objInvS.iGST_CompID = sSession.AccessCodeID
                            objInvS.iGST_YearID = sSession.YearID
                            objInvS.sGST_Operation = "C"
                            objInvS.sGST_IPAddress = sSession.IPAddress

                            objINV.SaveGSTRates(sSession.AccessCode, sSession.AccessCodeID, objInvS, 0)
                        End If
                        'Save to GST Rate Table'
                    End If


                End If
            Next
            lblError.Text = "Successfully Upload" : lblError.Text = "Successfully Upload"
            'clsSupplierUpload.SaveCommidityMaster(sSession.AccessCode, sSession.AccessCodeID, Session("dtUpload"), sSession.UserID, sSession.IPAddress)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function InventorySave() As String
    '    Dim dt As New DataTable

    '    Dim dt1 As New DataTable
    '    Dim Arr() As String, sSql As String = ""
    '    Dim dr As OleDb.OleDbDataReader
    '    Dim iParent, iMax As Integer, iUnit As Integer = 0, iAlternative As Integer = 0, iExistID As Integer, iExistStockID As Integer, iHistoryID As Integer = 0, iPerPiece As Integer = 0
    '    Dim sCommidity As String = "", sVat As String = "", sExcise As String = "", sColor As String = "", sSize As String = "", sAcode As String = "", scst As String = ""
    '    'Dim sCst As String = ""
    '    Dim dMRP As Double = "0.0", dRetail As Double = "0.0", dPreDetermined As Double = "0.0", dOthers As Double = "0.0"
    '    Dim iPQuantity As Integer, iOpeningQty As Integer, igetVat As Integer, igetCst As Integer, iExcise As Integer
    '    Dim dEffectiveFrom As Date, dEffectiveTo As Date
    '    Try

    '        If dgUpload.Rows.Count = 0 Then
    '            lblError.Text = "Please select the Load Button "
    '            lblError.Text = "Please select the Load Button "
    '            Exit Function
    '        End If
    '        dt = Session("dtUpload")
    '        For i = 0 To dt.Rows.Count - 1

    '            If dt.Rows(i)(0).ToString() <> "" And dt.Rows(i)(1).ToString() <> "" And dt.Rows(i)(2).ToString() <> "" And dt.Rows(i)(3).ToString() <> "" And dt.Rows(i)(4).ToString() <> "" Then

    '                If dt.Rows(i)(0).ToString() = "" Then
    '                    dt.Rows(i)(0) = sCommidity
    '                    If (sCommidity = "") Then
    '                        lblError.Text = "Commidity cannot be blank" & " Line No - " & i + 1
    '                        lblError.Text = "Commidity cannot be blank" & " Line No - " & i + 1
    '                        Exit Function
    '                    End If
    '                Else
    '                    sCommidity = dt.Rows(i)(0).ToString()
    '                End If

    '                If dt.Rows(i)(1).ToString() = "" Then
    '                    lblError.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
    '                    lblError.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
    '                    Exit Function
    '                End If

    '                If dt.Rows(i)(2).ToString() = "" Then
    '                    lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
    '                    lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
    '                    Exit Function
    '                End If

    '                If dt.Rows(i)(3).ToString() = "" Then
    '                    lblError.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
    '                    lblError.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
    '                    Exit Function
    '                Else
    '                    If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString()) = 0 Then
    '                        lblError.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
    '                        lblError.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
    '                        Exit Function
    '                    End If
    '                End If

    '                If dt.Rows(i)(4).ToString() = "" Then
    '                    lblError.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
    '                    lblError.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
    '                    Exit Function
    '                Else
    '                    If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString()) = 0 Then
    '                        lblError.Text = "Create Alternate unit of Measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
    '                        lblError.Text = "Create Alternate unit of measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
    '                        Exit Function
    '                    End If
    '                End If

    '                'If dt.Rows(i)(6).ToString() = "" Then
    '                '    lblError.Text = "VAT Cannot be blank" & " Line No - " & i + 1
    '                '    Exit Function
    '                'Else
    '                '    If objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString()) = 0 Then
    '                '        lblError.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
    '                '        lblError.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
    '                '        Exit Function
    '                '    End If
    '                'End If

    '                'If dt.Rows(i)(7).ToString() = "" Then
    '                '    lblError.Text = "Excise Cannot be blank" & " Line No - " & i + 1
    '                '    Exit Function
    '                'Else
    '                '    If objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString()) = 0 Then
    '                '        lblError.Text = "Create Excise Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
    '                '        lblError.Text = "Create Excise Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
    '                '        Exit Function
    '                '    End If
    '                'End If

    '                'If dt.Rows(i)(8).ToString() = "" Then
    '                '    lblError.Text = "CST can not  be blank" & " Line No - " & i + 1
    '                '    Exit Function
    '                'Else
    '                '    If objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString()) = 0 Then
    '                '        lblError.Text = "Create CST Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
    '                '        lblError.Text = "Create CST Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
    '                '        Exit Function
    '                '    End If
    '                'End If

    '                'Qty in Pieces
    '                If dt.Rows(i)(5).ToString() <> "" Then
    '                    iPerPiece = dt.Rows(i)(5).ToString()
    '                Else
    '                    iPerPiece = "0"
    '                End If

    '                'VAT
    '                'If dt.Rows(i)(6).ToString() <> "" Then
    '                '    sVat = dt.Rows(i)(6).ToString()
    '                'Else
    '                '    sVat = "0"
    '                'End If

    '                'Excise
    '                'If dt.Rows(i)(7).ToString() <> "" Then
    '                '    sExcise = dt.Rows(i)(7).ToString()
    '                'Else
    '                '    sExcise = "0"
    '                'End If

    '                'CST
    '                'If dt.Rows(i)(8).ToString() <> "" Then
    '                '    scst = dt.Rows(i)(8).ToString()
    '                'Else
    '                '    scst = "0"
    '                'End If

    '                'MRP
    '                If dt.Rows(i)(6).ToString() <> "" Then
    '                    dMRP = Convert.ToDouble(dt.Rows(i)(6).ToString())
    '                Else
    '                    dMRP = "0.0"
    '                End If

    '                'Retail
    '                If dt.Rows(i)(7).ToString() <> "" Then
    '                    dRetail = Convert.ToDouble(dt.Rows(i)(7).ToString())
    '                Else
    '                    dRetail = "0.0"
    '                End If

    '                If Trim(dt.Rows(i)(8).ToString()) = "" Then
    '                    dEffectiveFrom = "01/01/1900"
    '                Else
    '                    Try
    '                        'dEffectiveFrom = objGen.FormatDtForRDBMS(Trim(dt.Rows(i)(8).ToString()), "D")
    '                        dEffectiveFrom = Date.ParseExact(Trim(dt.Rows(i)(8)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                    Catch ex As Exception
    '                        lblExcelValidationMsg.Text = "Invalid Date Format(Enter Date in dd/MM/yyyy format)."
    '                        lblError.Text = "Invalid Date Format(Enter Date in dd/MM/yyyy format)."
    '                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
    '                        Exit Function
    '                    End Try
    '                End If

    '                Dim sEffectiveTo As String = ""
    '                If Trim(dt.Rows(i)(9).ToString()) = "" Then
    '                    'dEffectiveTo = "01/01/1900"
    '                    sEffectiveTo = "01/01/1900"
    '                Else
    '                    Try
    '                        sEffectiveTo = Trim(dt.Rows(i)(9))
    '                        'dEffectiveTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                        'dEffectiveTo = Trim(dt.Rows(i)(9))
    '                        'Date.ParseExact(Trim(dt.Rows(i)(9)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                    Catch ex As Exception
    '                        lblExcelValidationMsg.Text = "Invalid Date Format(Enter Date in dd/MM/yyyy format)."
    '                        lblError.Text = "Invalid Date Format(Enter Date in dd/MM/yyyy format)."
    '                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
    '                        Exit Function
    '                    End Try
    '                End If


    '                'PreDetermined
    '                If dt.Rows(i)(10).ToString() <> "" Then
    '                    dPreDetermined = Convert.ToDouble(dt.Rows(i)(10).ToString())
    '                Else
    '                    dPreDetermined = "0.0"
    '                End If

    '                'Others
    '                If dt.Rows(i)(11).ToString() <> "" Then
    '                    dOthers = dt.Rows(i)(11).ToString()
    '                Else
    '                    dOthers = "0.0"
    '                End If

    '                'Color
    '                If dt.Rows(i)(12).ToString() <> "" Then
    '                    sColor = dt.Rows(i)(12).ToString()
    '                Else
    '                    sColor = ""
    '                End If

    '                'Size
    '                If dt.Rows(i)(13).ToString() <> "" Then
    '                    sSize = dt.Rows(i)(13).ToString()
    '                Else
    '                    sSize = "0"
    '                End If

    '                'Alternative No/Color Code
    '                If dt.Rows(i)(14).ToString() <> "" Then
    '                    sAcode = dt.Rows(i)(14).ToString()
    '                Else
    '                    sAcode = ""
    '                End If

    '                ''Physical Quantity
    '                'If dt.Rows(i)(18).ToString() <> "" Then
    '                '    iPQuantity = dt.Rows(i)(18).ToString()
    '                'Else
    '                '    iPQuantity = "0"
    '                'End If

    '                objphysicalstock.Inv_Code = ""
    '                objphysicalstock.Inv_Description = dt.Rows(i)(0).ToString()
    '                objphysicalstock.Inv_Flag = "x"
    '                objphysicalstock.Inv_CompID = sSession.AccessCodeID
    '                objphysicalstock.InvH_Flag = "x"
    '                objphysicalstock.InvH_Excise = sExcise
    '                objphysicalstock.InvH_Cst = scst
    '                objphysicalstock.InvH_Vat = sVat
    '                objphysicalstock.Inv_Size = sSize
    '                objphysicalstock.Inv_Color = sColor
    '                objphysicalstock.Inv_Acode = sAcode
    '                objphysicalstock.Inv_CreatedBy = sSession.UserID

    '                'Inventory Master
    '                iParent = objphysicalstock.CheckCommidityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(0).ToString())
    '                If iParent = 0 Then
    '                    objphysicalstock.Inv_ID = 0
    '                    objphysicalstock.Inv_Code = dt.Rows(i)(0).ToString()
    '                    objphysicalstock.Inv_Parent = iParent
    '                    Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
    '                    iParent = Arr(1)
    '                End If

    '                objphysicalstock.Inv_Parent = iParent
    '                objphysicalstock.Inv_Code = dt.Rows(i)(2).ToString()
    '                objphysicalstock.Inv_Description = dt.Rows(i)(1).ToString()

    '                'Inventory Master Description
    '                iExistID = objphysicalstock.CheckDescriptionExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(2).ToString())
    '                objphysicalstock.Inv_ID = iExistID
    '                If iExistID = 0 Then
    '                    objphysicalstock.Inv_ID = 0
    '                    objphysicalstock.Inv_Parent = iParent
    '                    Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
    '                    'iParent = Arr(1)
    '                    iHistoryID = Arr(1)
    '                Else
    '                    Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
    '                    iHistoryID = Arr(1)
    '                End If
    '                'Inventory Master History

    '                iUnit = objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString())
    '                iAlternative = objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString())
    '                iExistID = objphysicalstock.CheckInventoryMasterHistory(sSession.AccessCode, sSession.AccessCodeID, iHistoryID)


    '                objphysicalstock.InvH_Flag = "x"
    '                objphysicalstock.InvH_Unit = iUnit
    '                objphysicalstock.InvH_AlterUnit = iAlternative
    '                objphysicalstock.InvH_Excise = sExcise
    '                objphysicalstock.InvH_Cst = scst
    '                objphysicalstock.InvH_Vat = sVat
    '                objphysicalstock.InvH_CreatedBy = sSession.UserID
    '                objphysicalstock.InvH_CompID = sSession.AccessCodeID
    '                objphysicalstock.InvH_PerPieces = iPerPiece
    '                objphysicalstock.INVH_MRP = dMRP
    '                objphysicalstock.INVH_Retail = dRetail
    '                objphysicalstock.INVH_PreDeterminedPrice = dPreDetermined
    '                objphysicalstock.INVH_EffeFrom = objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                objphysicalstock.INVH_EffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                'Extra'
    '                objphysicalstock.INVH_RetailEffeFrom = objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                objphysicalstock.INVH_RetailEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                objphysicalstock.INVH_PurchaseEffeFrom = objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                objphysicalstock.INVH_PurchaseEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                'Extra'

    '                'Rakshan
    '                'objphysicalstock.INVH_EffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                'objphysicalstock.INVH_EffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                ''Extra'
    '                'objphysicalstock.INVH_RetailEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                'objphysicalstock.INVH_RetailEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")

    '                'objphysicalstock.INVH_PurchaseEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
    '                'objphysicalstock.INVH_PurchaseEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")
    '                'Rakshan

    '                objphysicalstock.INVH_Others = dOthers
    '                objphysicalstock.InvH_ID = iExistID
    '                objphysicalstock.InvH_INV_ID = iHistoryID
    '                objphysicalstock.SL_IPAddress = sSession.IPAddress
    '                If iExistID = 0 Then
    '                    objphysicalstock.InvH_ID = 0
    '                    Arr = objphysicalstock.SaveInventoryMasterHistory(sSession.AccessCode, objphysicalstock)
    '                    iHistoryID = Arr(1)
    '                    objphysicalstock.InvH_ID = iHistoryID
    '                Else
    '                    Arr = objphysicalstock.SaveInventoryMasterHistory(sSession.AccessCode, objphysicalstock)
    '                    iHistoryID = Arr(1)
    '                    objphysicalstock.InvH_ID = iHistoryID
    '                End If

    '                'Vat and cst saving

    '                '---Bcz GST Applied---'
    '                'objphysicalstock.InvH_Excise = sExcise
    '                'objphysicalstock.InvH_Cst = scst
    '                'objphysicalstock.InvH_Vat = sVat
    '                'objphysicalstock.InvH_Excise = objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString())
    '                'objphysicalstock.InvH_Cst = objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString())
    '                'objphysicalstock.InvH_Vat = objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString())
    '                'Arr = objphysicalstock.SaveTaxDetails(sSession.AccessCode, objphysicalstock)
    '                '---Bcz GST Applied---'

    '            End If
    '        Next
    '        lblError.Text = "Successfully Upload"
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "InventorySave")
    '    End Try
    'End Function
    Public Function InventorySave() As String
        Dim dt As New DataTable

        Dim dt1 As New DataTable
        Dim Arr() As String, sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iParent, iMax As Integer, iUnit As Integer = 0, iAlternative As Integer = 0, iExistID As Integer, iExistStockID As Integer, iHistoryID As Integer = 0, iPerPiece As Integer = 0
        Dim sCommidity As String = "", sVat As String = "", sExcise As String = "", sColor As String = "", sSize As String = "", sAcode As String = "", scst As String = ""
        'Dim sCst As String = ""
        Dim dMRP As Double = "0.0", dRetail As Double = "0.0", dPreDetermined As Double = "0.0", dOthers As Double = "0.0"
        Dim iPQuantity As Integer, iOpeningQty As Integer, igetVat As Integer, igetCst As Integer, iExcise As Integer
        'Dim dEffectiveFrom As Date, dEffectiveTo As Date
        Try

            If dgUpload.Rows.Count = 0 Then
                lblError.Text = "Please select the Load Button "
                lblError.Text = "Please select the Load Button "
                Exit Function
            End If
            dt = Session("dtUpload")
            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(0).ToString() <> "" And dt.Rows(i)(1).ToString() <> "" And dt.Rows(i)(2).ToString() <> "" And dt.Rows(i)(3).ToString() <> "" And dt.Rows(i)(4).ToString() <> "" Then

                    If dt.Rows(i)(0).ToString() = "" Then
                        dt.Rows(i)(0) = sCommidity
                        If (sCommidity = "") Then
                            lblError.Text = "Commidity cannot be blank" & " Line No - " & i + 1
                            lblError.Text = "Commidity cannot be blank" & " Line No - " & i + 1
                            Exit Function
                        End If
                    Else
                        sCommidity = dt.Rows(i)(0).ToString()
                    End If

                    If dt.Rows(i)(1).ToString() = "" Then
                        lblError.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
                        lblError.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    End If

                    If dt.Rows(i)(2).ToString() = "" Then
                        lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
                        lblError.Text = "Code cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    End If

                    If dt.Rows(i)(3).ToString() = "" Then
                        lblError.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                        lblError.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    Else
                        If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString()) = 0 Then
                            lblError.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                            lblError.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                            Exit Function
                        End If
                    End If

                    If dt.Rows(i)(4).ToString() = "" Then
                        lblError.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                        lblError.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    Else
                        If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString()) = 0 Then
                            lblError.Text = "Create Alternate unit of Measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                            lblError.Text = "Create Alternate unit of measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                            Exit Function
                        End If
                    End If

                    If rboWithGST.Checked = True Then
                        If dt.Rows(i)(15).ToString() = "" Then
                            lblError.Text = "GST Rate cannot be blank" & " Line No - " & i + 1
                            lblError.Text = "GST Rate cannot be blank" & " Line No - " & i + 1
                            Exit Function
                        End If
                        If dt.Rows(i)(16).ToString() = "" Then
                            lblError.Text = "HSN No cannot be blank" & " Line No - " & i + 1
                            lblError.Text = "HSN No cannot be blank" & " Line No - " & i + 1
                            Exit Function
                        End If
                    End If

                    'If dt.Rows(i)(6).ToString() = "" Then
                    '    lblError.Text = "VAT Cannot be blank" & " Line No - " & i + 1
                    '    Exit Function
                    'Else
                    '    If objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString()) = 0 Then
                    '        lblError.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
                    '        lblError.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
                    '        Exit Function
                    '    End If
                    'End If

                    'If dt.Rows(i)(7).ToString() = "" Then
                    '    lblError.Text = "Excise Cannot be blank" & " Line No - " & i + 1
                    '    Exit Function
                    'Else
                    '    If objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString()) = 0 Then
                    '        lblError.Text = "Create Excise Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
                    '        lblError.Text = "Create Excise Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
                    '        Exit Function
                    '    End If
                    'End If

                    'If dt.Rows(i)(8).ToString() = "" Then
                    '    lblError.Text = "CST can not  be blank" & " Line No - " & i + 1
                    '    Exit Function
                    'Else
                    '    If objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString()) = 0 Then
                    '        lblError.Text = "Create CST Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
                    '        lblError.Text = "Create CST Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
                    '        Exit Function
                    '    End If
                    'End If

                    'Qty in Pieces
                    If dt.Rows(i)(5).ToString() <> "" Then
                        iPerPiece = dt.Rows(i)(5).ToString()
                    Else
                        iPerPiece = "0"
                    End If

                    'VAT
                    'If dt.Rows(i)(6).ToString() <> "" Then
                    '    sVat = dt.Rows(i)(6).ToString()
                    'Else
                    '    sVat = "0"
                    'End If

                    'Excise
                    'If dt.Rows(i)(7).ToString() <> "" Then
                    '    sExcise = dt.Rows(i)(7).ToString()
                    'Else
                    '    sExcise = "0"
                    'End If

                    'CST
                    'If dt.Rows(i)(8).ToString() <> "" Then
                    '    scst = dt.Rows(i)(8).ToString()
                    'Else
                    '    scst = "0"
                    'End If

                    'MRP
                    If dt.Rows(i)(6).ToString() <> "" Then
                        dMRP = Convert.ToDouble(dt.Rows(i)(6).ToString())
                    Else
                        dMRP = "0.0"
                    End If

                    'Retail
                    If dt.Rows(i)(7).ToString() <> "" Then
                        dRetail = Convert.ToDouble(dt.Rows(i)(7).ToString())
                    Else
                        dRetail = "0.0"
                    End If

                    Dim dEffectiveFrom As String = ""
                    If Trim(dt.Rows(i)(8).ToString()) = "" Then
                        dEffectiveFrom = "01/01/1900"
                    Else
                        Try
                            'dEffectiveFrom = objGen.FormatDtForRDBMS(Trim(dt.Rows(i)(8).ToString()), "D")
                            dEffectiveFrom = Trim(dt.Rows(i)(8))
                        Catch ex As Exception
                            lblExcelValidationMsg.Text = "Invalid Date Format(Enter Date in dd/MM/yyyy format)."
                            lblError.Text = "Invalid Date Format(Enter Date in dd/MM/yyyy format)."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                            Exit Function
                        End Try
                    End If

                    Dim sEffectiveTo As String = ""
                    If Trim(dt.Rows(i)(9).ToString()) = "" Then
                        'dEffectiveTo = "01/01/1900"
                        sEffectiveTo = "01/01/1900"
                    Else
                        Try
                            sEffectiveTo = Trim(dt.Rows(i)(9))
                            'dEffectiveTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            'dEffectiveTo = Trim(dt.Rows(i)(9))
                            'Date.ParseExact(Trim(dt.Rows(i)(9)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Catch ex As Exception
                            lblExcelValidationMsg.Text = "Invalid Date Format(Enter Date in dd/MM/yyyy format)."
                            lblError.Text = "Invalid Date Format(Enter Date in dd/MM/yyyy format)."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                            Exit Function
                        End Try
                    End If


                    'PreDetermined
                    If dt.Rows(i)(10).ToString() <> "" Then
                        dPreDetermined = Convert.ToDouble(dt.Rows(i)(10).ToString())
                    Else
                        dPreDetermined = "0.0"
                    End If

                    'Others
                    If dt.Rows(i)(11).ToString() <> "" Then
                        dOthers = dt.Rows(i)(11).ToString()
                    Else
                        dOthers = "0.0"
                    End If

                    'Color
                    If dt.Rows(i)(12).ToString() <> "" Then
                        sColor = dt.Rows(i)(12).ToString()
                    Else
                        sColor = ""
                    End If

                    'Size
                    If dt.Rows(i)(13).ToString() <> "" Then
                        sSize = dt.Rows(i)(13).ToString()
                    Else
                        sSize = "0"
                    End If

                    'Alternative No/Color Code
                    If dt.Rows(i)(14).ToString() <> "" Then
                        sAcode = dt.Rows(i)(14).ToString()
                    Else
                        sAcode = ""
                    End If

                    ''Physical Quantity
                    'If dt.Rows(i)(18).ToString() <> "" Then
                    '    iPQuantity = dt.Rows(i)(18).ToString()
                    'Else
                    '    iPQuantity = "0"
                    'End If

                    objphysicalstock.Inv_Code = ""
                    objphysicalstock.Inv_Description = dt.Rows(i)(0).ToString()
                    objphysicalstock.Inv_Flag = "x"
                    objphysicalstock.Inv_CompID = sSession.AccessCodeID
                    objphysicalstock.InvH_Flag = "x"
                    objphysicalstock.InvH_Excise = sExcise
                    objphysicalstock.InvH_Cst = scst
                    objphysicalstock.InvH_Vat = sVat
                    objphysicalstock.Inv_Size = sSize
                    objphysicalstock.Inv_Color = sColor
                    objphysicalstock.Inv_Acode = sAcode
                    objphysicalstock.Inv_CreatedBy = sSession.UserID

                    'Inventory Master
                    iParent = objphysicalstock.CheckCommidityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(0).ToString())
                    If iParent = 0 Then
                        objphysicalstock.Inv_ID = 0
                        objphysicalstock.Inv_Code = dt.Rows(i)(0).ToString()
                        objphysicalstock.Inv_Parent = iParent
                        Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
                        iParent = Arr(1)
                    End If

                    objphysicalstock.Inv_Parent = iParent
                    objphysicalstock.Inv_Code = dt.Rows(i)(2).ToString()
                    objphysicalstock.Inv_Description = dt.Rows(i)(1).ToString()

                    'Inventory Master Description
                    iExistID = objphysicalstock.CheckDescriptionExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(2).ToString())
                    objphysicalstock.Inv_ID = iExistID
                    If iExistID = 0 Then
                        objphysicalstock.Inv_ID = 0
                        objphysicalstock.Inv_Parent = iParent
                        Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
                        'iParent = Arr(1)
                        iHistoryID = Arr(1)
                    Else
                        Arr = objphysicalstock.SaveInventoryMaster(sSession.AccessCode, objphysicalstock)
                        iHistoryID = Arr(1)
                    End If
                    'Inventory Master History

                    iUnit = objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString())
                    iAlternative = objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString())
                    iExistID = objphysicalstock.CheckInventoryMasterHistory(sSession.AccessCode, sSession.AccessCodeID, iHistoryID)


                    objphysicalstock.InvH_Flag = "x"
                    objphysicalstock.InvH_Unit = iUnit
                    objphysicalstock.InvH_AlterUnit = iAlternative
                    objphysicalstock.InvH_Excise = sExcise
                    objphysicalstock.InvH_Cst = scst
                    objphysicalstock.InvH_Vat = sVat
                    objphysicalstock.InvH_CreatedBy = sSession.UserID
                    objphysicalstock.InvH_CompID = sSession.AccessCodeID
                    objphysicalstock.InvH_PerPieces = iPerPiece
                    objphysicalstock.INVH_MRP = dMRP
                    objphysicalstock.INVH_Retail = dRetail
                    objphysicalstock.INVH_PreDeterminedPrice = dPreDetermined
                    objphysicalstock.INVH_EffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objphysicalstock.INVH_EffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    'Extra'
                    objphysicalstock.INVH_RetailEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objphysicalstock.INVH_RetailEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    objphysicalstock.INVH_PurchaseEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objphysicalstock.INVH_PurchaseEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    'Extra'

                    'Rakshan
                    'objphysicalstock.INVH_EffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
                    'objphysicalstock.INVH_EffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    ''Extra'
                    'objphysicalstock.INVH_RetailEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
                    'objphysicalstock.INVH_RetailEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")

                    'objphysicalstock.INVH_PurchaseEffeFrom = Date.ParseExact(dEffectiveFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
                    'objphysicalstock.INVH_PurchaseEffeTo = Date.ParseExact(sEffectiveTo, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ''objGen.FormatDtForRDBMS(sEffectiveTo, "D")
                    'Rakshan

                    objphysicalstock.INVH_Others = dOthers
                    objphysicalstock.InvH_ID = iExistID
                    objphysicalstock.InvH_INV_ID = iHistoryID
                    objphysicalstock.SL_IPAddress = sSession.IPAddress
                    If iExistID = 0 Then
                        objphysicalstock.InvH_ID = 0
                        Arr = objphysicalstock.SaveInventoryMasterHistory(sSession.AccessCode, objphysicalstock)
                        iHistoryID = Arr(1)
                        objphysicalstock.InvH_ID = iHistoryID
                    Else
                        Arr = objphysicalstock.SaveInventoryMasterHistory(sSession.AccessCode, objphysicalstock)
                        iHistoryID = Arr(1)
                        objphysicalstock.InvH_ID = iHistoryID
                    End If

                    'Vat and cst saving

                    '---Bcz GST Applied---'
                    'objphysicalstock.InvH_Excise = sExcise
                    'objphysicalstock.InvH_Cst = scst
                    'objphysicalstock.InvH_Vat = sVat
                    'objphysicalstock.InvH_Excise = objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString())
                    'objphysicalstock.InvH_Cst = objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString())
                    'objphysicalstock.InvH_Vat = objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString())
                    'Arr = objphysicalstock.SaveTaxDetails(sSession.AccessCode, objphysicalstock)
                    '---Bcz GST Applied---'


                    Dim iCommodityID, iItemID As Integer

                    'Save To GST Rate Table'
                    If rboWithGST.Checked = True Then
                        iCommodityID = objclsExcel.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(1).ToString())
                        iItemID = objclsExcel.GetItemID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(1).ToString())

                        objInvS.iGST_ID = 0
                        If iCommodityID = 0 Then

                        Else
                            objInvS.iGST_ScheduleID = 0
                            objInvS.iGST_CommodityID = iCommodityID
                            objInvS.iGST_ItemID = iItemID

                            objInvS.iGST_ScheduleType = 0

                            If dt.Rows(i)(15).ToString() <> "" Then
                                objInvS.dGST_GSTRate = dt.Rows(i)(15).ToString()
                            Else
                                objInvS.dGST_GSTRate = 0
                            End If

                            objInvS.sGST_SlNo = ""

                            If dt.Rows(i)(16).ToString() <> "" Then
                                objInvS.sGST_CHST = dt.Rows(i)(16).ToString()
                            Else
                                objInvS.sGST_CHST = ""
                            End If

                            If dt.Rows(i)(17).ToString() <> "" Then
                                objInvS.sGST_Chapter = dt.Rows(i)(17).ToString()
                            Else
                                objInvS.sGST_Chapter = ""
                            End If

                            If dt.Rows(i)(17).ToString() <> "" Then
                                objInvS.sGST_Heading = dt.Rows(i)(17).ToString()
                            Else
                                objInvS.sGST_Heading = ""
                            End If

                            objInvS.sGST_SubHeading = ""
                            objInvS.sGST_Tarrif = ""
                            objInvS.sGST_SubSlNo = ""
                            objInvS.sGST_CESS = 0
                            objInvS.sGST_GoodDescription = ""
                            objInvS.sGST_NotificationNo = ""
                            objInvS.dGST_NotificationFromDate = "01/01/1900"
                            objInvS.dGST_NotificationToDate = "01/01/1900"
                            objInvS.sGST_FileNo = ""
                            objInvS.dGST_FileFromDate = "01/01/1900"
                            objInvS.dGST_FileToDate = "01/01/1900"

                            objInvS.sGST_Status = "W"
                            objInvS.iGST_CompID = sSession.AccessCodeID
                            objInvS.iGST_YearID = sSession.YearID
                            objInvS.sGST_Operation = "C"
                            objInvS.sGST_IPAddress = sSession.IPAddress

                            objINV.SaveGSTRates(sSession.AccessCode, sSession.AccessCodeID, objInvS, 0)
                        End If
                        'Save to GST Rate Table'
                    End If
                End If
            Next
            lblError.Text = "Successfully Upload"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "InventorySave")
        End Try
    End Function
    Private Sub ddlSubLedger_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubLedger.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            If ddlSubLedger.SelectedIndex > 0 Then
                sStatus = objclsExcel.getBreakupStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSubLedger.SelectedValue)
                If sStatus = "Y" Then
                    lblError.Text = "Break Up Has been Uploaded for this Ledger,you can not upload again."
                    Exit Sub
                End If
                dt = objclsExcel.GetOpBalDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSubLedger.SelectedValue)
                If dt.Rows.Count > 0 Then
                    lblOpBalDebit.Text = dt.Rows(0)("Opn_DebitAmt")
                    lblOpBalCreadit.Text = dt.Rows(0)("Opn_CreditAmount")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubLedger_SelectedIndexChanged")
        End Try
    End Sub
    Private Function mobilenumbercheck(ByVal mobilenumber As String) As Boolean
        Dim pattern As String = "^[0-9]{10}$"
        Dim mobilenumberMatch As Match = Regex.Match(mobilenumber, pattern)
        If mobilenumberMatch.Success Then
            mobilenumbercheck = True
        Else
            mobilenumbercheck = False
        End If
    End Function
    Private Function pincodecheck(ByVal pincode As String) As Boolean
        Dim pattern As String = "^[0-9]{6}$"
        Dim pincodeMatch As Match = Regex.Match(pincode, pattern)
        If pincodeMatch.Success Then
            pincodecheck = True
        Else
            pincodecheck = False
        End If
    End Function
    Private Function faxcheck(ByVal fax As String) As Boolean
        Dim pattern As String = "^[0-9]{11,13}$"
        Dim faxMatch As Match = Regex.Match(fax, pattern)
        If faxMatch.Success Then
            faxcheck = True
        Else
            faxcheck = False
        End If
    End Function
    Private Function tincheck(ByVal tin As String) As Boolean
        Dim pattern As String = "^[0-9]{11}$"
        Dim tinMatch As Match = Regex.Match(tin, pattern)
        If tinMatch.Success Then
            tincheck = True
        Else
            tincheck = False
        End If
    End Function
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                If ddlAccBrnch.SelectedIndex > 0 Then
                    iParent = objclsExcel.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                    ddlAccArea.SelectedValue = iParent
                End If
                If ddlAccArea.SelectedIndex > 0 Then
                    iParent = objclsExcel.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                    ddlAccRgn.SelectedValue = iParent
                End If
                If ddlAccRgn.SelectedIndex > 0 Then
                    iParent = objclsExcel.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                    ddlAccZone.SelectedValue = iParent
                End If
            Else
                ddlAccArea.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccZone.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveAssetMaster()
        Dim objFxdAsst As New ClsFexedAsst
        Dim objAsst As New ClsAssetMaster
        Dim objDepComp As New ClsDepreciationComputation
        Dim Arr() As String
        Dim Depcal() As String
        Dim dtUpload As New DataTable


        Dim iAssetID As New Integer
        Dim sAssetCode As String = "" : Dim sDescription As String = "" : Dim sAssetAge As String = "" : Dim sQTY As String = ""
        Dim sCommissinDate As String = "" : Dim sPurchaseAMT As String = "" : Dim sDepRate As String = "" : Dim sNoDays As String = ""
        Dim sDepreciation As String = "" : Dim sYTD As String = "" : Dim sWDV As String = ""

        Try

            If IsNothing(Session("dtUpload")) = False Then
                dtUpload = Session("dtUpload")
            Else
                lblError.Text = "Laod before you Upload."
                lblExcelValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            dtUpload = Session("dtUpload")

            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1
                    If IsDBNull(dtUpload.Rows(j).Item(4)) = False And dtUpload.Rows(j).Item(4).ToString <> "" Then
                        objFxdAsst.AFAM_AssetAge = (dtUpload.Rows(j).Item(4))
                    Else
                        lblError.Text = "Code Asset age cant be blank" & ":" & j
                        lblExcelValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                Next
            End If

            If (dtUpload.Rows.Count > 0) Then
                For j = 0 To dtUpload.Rows.Count - 1

                    objFxdAsst.AFAM_AssetType = objFxdAsst.CheckAssteType(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, (dtUpload.Rows(j).Item(2)).ToString)

                    If objFxdAsst.AFAM_AssetType = 0 Then
                        lblError.Text = "Row number of " & j & " Asset type " & (dtUpload.Rows(j).Item(2)).ToString & " doesnt exist,create in the chart of account"
                        Exit Sub
                    End If

                    '/////Depreciation rate comparison

                    objAsst.AM_ID = 0
                    objAsst.AM_AssetID = 0
                    objAsst.AM_CreatedBy = sSession.UserID
                    objAsst.AM_CreatedOn = Date.Today
                    objAsst.AM_DelFlag = ""
                    objAsst.AM_Status = ""
                    objAsst.AM_YearID = sSession.YearID
                    objAsst.AM_CompID = sSession.AccessCodeID
                    objAsst.AM_Deprate = (dtUpload.Rows(j).Item(8)).ToString
                    objAsst.AM_Opeartion = ""
                    objAsst.AM_IPAddress = sSession.IPAddress

                    objAsst.AM_Deprate = objAsst.CheckDepreciationRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, (dtUpload.Rows(j).Item(2)).ToString, (dtUpload.Rows(j).Item(8)).ToString, objAsst)

                    'If objAsst.AM_Deprate = (dtUpload.Rows(j).Item(8)).ToString Then
                    '    lblError.Text = "Row number of " & j & " Depreciation rate " & (dtUpload.Rows(j).Item(8)).ToString & " doesnt not exist,create in the asset master"
                    'Else
                    '    lblError.Text = "Row number of " & j & " Depreciation rate " & (dtUpload.Rows(j).Item(8)).ToString & "is exist"
                    'End If

                    '///////////////////

                    objFxdAsst.AFAM_AssetCode = (dtUpload.Rows(j).Item(1)).ToString
                    objFxdAsst.AFAM_Description = (dtUpload.Rows(j).Item(2)).ToString
                    objFxdAsst.AFAM_ItemCode = ""
                    objFxdAsst.AFAM_ItemDescription = (dtUpload.Rows(j).Item(3)).ToString
                    objFxdAsst.AFAM_Quantity = (dtUpload.Rows(j).Item(5)).ToString
                    objFxdAsst.AFAM_PurchaseDate = Date.Today
                    objFxdAsst.AFAM_CommissionDate = objGen.FormatDtForRDBMS(dtUpload.Rows(j).Item(6).date, "D")
                    objFxdAsst.AFAM_AssetAge = (dtUpload.Rows(j).Item(4)).ToString
                    objFxdAsst.AFAM_PurchaseAmount = (dtUpload.Rows(j).Item(7)).ToString
                    objFxdAsst.AFAM_PolicyNo = 0
                    objFxdAsst.AFAM_Amount = 0
                    objFxdAsst.AFAM_Date = Date.Today
                    objFxdAsst.AFAM_Department = 0
                    objFxdAsst.AFAM_Employee = 0
                    objFxdAsst.AFAM_SuplierName = ""
                    objFxdAsst.AFAM_ContactPerson = ""
                    objFxdAsst.AFAM_Address = ""
                    objFxdAsst.AFAM_Phone = 0
                    objFxdAsst.AFAM_Fax = 0
                    objFxdAsst.AFAM_EmailID = ""
                    objFxdAsst.AFAM_Website = ""
                    objFxdAsst.AFAM_CreatedBy = 0
                    objFxdAsst.AFAM_CreatedOn = Date.Today
                    objFxdAsst.AFAM_UpdatedBy = 0
                    objFxdAsst.AFAM_UpdatedOn = Date.Today
                    objFxdAsst.AFAM_DelFlag = ""
                    objFxdAsst.AFAM_Status = ""
                    objFxdAsst.AFAM_YearID = sSession.YearID
                    objFxdAsst.AFAM_CompID = sSession.AccessCodeID
                    objFxdAsst.AFAM_Opeartion = ""
                    objFxdAsst.AFAM_IPAddress = sSession.IPAddress
                    objFxdAsst.AFAM_BrokerName = ""
                    objFxdAsst.AFAM_CompanyName = ""
                    objFxdAsst.AFAM_WrntyDesc = ""
                    objFxdAsst.AFAM_ContactPrsn = ""
                    objFxdAsst.AFAM_AMCFrmDate = Date.Today
                    objFxdAsst.AFAM_AMCTo = Date.Today
                    objFxdAsst.AFAM_Contprsn = ""
                    objFxdAsst.AFAM_PhoneNo = 0
                    objFxdAsst.AFAM_AMCCompanyName = ""
                    objFxdAsst.AFAM_ToDate = Date.Today
                    objFxdAsst.AFAM_Location = 0
                    objFxdAsst.AFAM_AssetDeletion = 0
                    objFxdAsst.AFAM_DlnDate = Date.Today
                    objFxdAsst.AFAM_DateOfDeletion = Date.Today
                    objFxdAsst.AFAM_Remark = ""
                    objFxdAsst.AFAM_Value = 0
                    objFxdAsst.AFAM_EMPCode = ""

                    Arr = objFxdAsst.SaveFxedAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objFxdAsst)

                    '///validating no.of days  ytd And wdv values in depreciation table

                    objDepComp.ADep_ID = 0
                    objDepComp.ADep_Asset_MasterID = objDepComp.checkingmasterid(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, (dtUpload.Rows(j).Item(2)).ToString, objDepComp)
                    objDepComp.ADep_AssetID = objDepComp.checkAssetID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, (dtUpload.Rows(j).Item(2)).ToString, objDepComp)
                    objDepComp.ADep_Description = (dtUpload.Rows(j).Item(2)).ToString
                    objDepComp.ADep_CommissionDate = objGen.FormatDtForRDBMS(dtUpload.Rows(j).Item(6).date, "D")
                    objDepComp.ADep_Quantity = (dtUpload.Rows(j).Item(5)).ToString
                    objDepComp.ADep_Depreciation_rate = (dtUpload.Rows(j).Item(8)).ToString
                    objDepComp.ADep_PurchaseAmount = (dtUpload.Rows(j).Item(7)).ToString
                    objDepComp.ADep_AssetAge = (dtUpload.Rows(j).Item(4)).ToString
                    objDepComp.ADep_NoOfDays = (dtUpload.Rows(j).Item(9)).ToString
                    objDepComp.ADep_Depreciationfor_theyear = (dtUpload.Rows(j).Item(10)).ToString
                    objDepComp.ADep_YTD = (dtUpload.Rows(j).Item(11)).ToString
                    objDepComp.ADep_WDV = (dtUpload.Rows(j).Item(12)).ToString
                    objDepComp.ADep_ResidualValue = 0
                    objDepComp.ADep_CreatedBy = sSession.UserID
                    objDepComp.ADep_CreatedOn = DateTime.Today
                    objDepComp.ADep_DelFlag = "X"
                    objDepComp.ADep_Status = "W"
                    objDepComp.ADep_YearID = sSession.YearID
                    objDepComp.ADep_CompID = sSession.AccessCodeID
                    objDepComp.ADep_Opeartion = "C"
                    objDepComp.ADep_IPAddress = sSession.IPAddress

                    Depcal = objDepComp.SaveDepreciationComputation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objDepComp)

                    '///validating no.of days  ytd And wdv values in depreciation table
                Next
            End If
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveAssetMaster")
        End Try
    End Sub
End Class
