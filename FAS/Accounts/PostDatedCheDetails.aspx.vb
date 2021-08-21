Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class Accounts_PostDatedCheDetails
    Inherits System.Web.UI.Page

    Private sFormName As String = "Accounts/PostDatedCheDetails"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGenFun As New clsGeneralFunctions
    Dim objCheDetails As New clsChequeDetails
    Private objclsModulePermission As New clsModulePermission
    Public dtDep As DataTable
    Dim sDateNow As String
    Dim dAmount As Double = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = False
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PDCR")
                imgbtnReport.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If

                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                sDateNow = Date.Now.ToString("dd/MM/yyyy")
                imgbtnAdd.Visible = True
                'imgbtnReport.Visible = True
                BindStatus() : BindAllCheques(0, ddlStatus.SelectedIndex)

            End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "All")
            ddlStatus.Items.Insert(1, "Today")
            ddlStatus.Items.Insert(2, "Upcoming Dates")
            ddlStatus.Items.Insert(3, "Outdated")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindAllCheques(ByVal iPageIndex As Integer, ByVal iStatus As Integer)
        Dim sDateNow As String
        Try
            lblError.Text = ""
            sDateNow = Date.Now.ToString("dd/MM/yyyy")
            dgChequeDetails.CurrentPageIndex = iPageIndex
            dtDep = objCheDetails.LoadPDCheDetails(sSession.AccessCode, sSession.AccessCodeID, iStatus, sDateNow)
            Session("dtDep") = dtDep
            If dtDep.Rows.Count > dgChequeDetails.PageSize Then
                dgChequeDetails.AllowPaging = True
            Else
                dgChequeDetails.AllowPaging = False
            End If
            dgChequeDetails.DataSource = dtDep
            dgChequeDetails.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgChequeDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgChequeDetails.ItemDataBound
        Dim imgbtnEdit As New ImageButton
        Dim lblChequeDate As New Label
        Dim dDatetoday As Date, sCurrentDate As String, dPreviousDate As Date
        Dim DateCheck As Integer
        Try

            lblError.Text = ""
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnEdit = CType(e.Item.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                'sDatetoday = Date.Now.ToString("dd/MM/yyyy")
                sCurrentDate = Date.Now.ToString("dd/MM/yyyy")
                lblChequeDate = e.Item.FindControl("lblChequeDate")

                dDatetoday = Date.ParseExact(Trim(sCurrentDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dPreviousDate = Date.ParseExact(Trim(lblChequeDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                DateCheck = DateDiff(DateInterval.Day, dDatetoday, dPreviousDate)

                If ddlStatus.SelectedIndex = 0 And dPreviousDate = dDatetoday Then
                    e.Item.Cells(5).BackColor = Drawing.Color.Green
                    lblChequeDate.ForeColor = Drawing.Color.White
                End If

                If ddlStatus.SelectedIndex = 0 And DateCheck > 0 Then
                    e.Item.Cells(5).BackColor = Drawing.Color.Yellow
                End If

                If ddlStatus.SelectedIndex = 0 And DateCheck < 0 Then
                    e.Item.Cells(5).BackColor = Drawing.Color.Red
                    lblChequeDate.ForeColor = Drawing.Color.White
                End If

                If ddlStatus.SelectedIndex = 1 And dPreviousDate = dDatetoday Then
                    e.Item.Cells(5).BackColor = Drawing.Color.Green
                    lblChequeDate.ForeColor = Drawing.Color.White
                End If
                If ddlStatus.SelectedIndex = 2 And DateCheck > 0 Then
                    e.Item.Cells(5).BackColor = Drawing.Color.Yellow
                End If
                If ddlStatus.SelectedIndex = 3 And DateCheck < 0 Then
                    e.Item.Cells(5).BackColor = Drawing.Color.Red
                    lblChequeDate.ForeColor = Drawing.Color.White
                End If
                If e.Item.Cells(10).Text <> "" Then
                    dAmount = 0.0
                    e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
                    dAmount = dAmount + Convert.ToDouble(e.Item.Cells(10).Text)
                    e.Item.Cells(10).Text = Convert.ToDecimal(dAmount).ToString("#,##0.00")
                    e.Item.Cells(10).Font.Bold = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgChequeDetails_ItemCommand")
        End Try
    End Sub

    Private Sub dgChequeDetails_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgChequeDetails.PageIndexChanged
        Try
            lblError.Text = ""
            BindAllCheques(e.NewPageIndex, ddlStatus.SelectedIndex)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgChequeDetails_PageIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(3))
            End If
            Response.Redirect(String.Format("~/Accounts/ChequeDetails.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            If ddlStatus.SelectedIndex >= 0 Then
                BindAllCheques(0, ddlStatus.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub dgChequeDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgChequeDetails.ItemCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblACMID As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            lblACMID = e.Item.FindControl("lblACMID")
            If e.CommandName = "Edit" Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblACMID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblACMID.Text)))
                Response.Redirect(String.Format("~/Accounts/ChequeDetails.aspx?StatusID={0}&MasterID={1}&MasterName={2}", oStatusID, oMasterID, oMasterName), False)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgChequeDetails_ItemCommand")
        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim dtC As New DataTable
        Try
            dtC = objCheDetails.ChequeDetailsToExcel(sSession.AccessCode)
            ExcelChequeDetails(dtC)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Public Sub ExcelChequeDetails(ByVal dt1 As DataTable)
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
                sPath = Server.MapPath("../") & "SampleExcels\ChequeDetails.xlsx"
                wBook = excel.Workbooks.Add(sPath)
                wSheet = wBook.ActiveSheet()
                For i = 0 To 19
                    colIndex = colIndex + 1
                    excel.Cells(1, colIndex) = dt.Columns(i).ColumnName
                    excel.Cells(1, colIndex).Font.Bold = True
                Next
                For Each dr In dt.Rows
                    rowIndex = rowIndex + 1
                    colIndex = 0
                    For i = 0 To 19
                        colIndex = colIndex + 1
                        excel.Cells(rowIndex + 1, colIndex) = dr(dt.Columns(i).ColumnName)
                    Next
                Next
                wSheet.Columns.AutoFit()
                sExcelFileName = "ChequeDetails.xlsx"
                strFileNamePath = objGenFun.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
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
                'lblExcelValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalFASPostDatedCheDetails').modal('show');", True)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub DownloadFile(ByVal pstrFileNameAndPath As String)
        Dim Extn As String, pstrContentType As String, sFileName As String, sFullName As String
        Dim myFileInfo As IO.FileInfo
        Dim StartPos As Long = 0, FileSize As Long, EndPos As Long
        Try
            If IO.File.Exists(pstrFileNameAndPath) Then
                myFileInfo = New IO.FileInfo(pstrFileNameAndPath)
                FileSize = myFileInfo.Length
                EndPos = FileSize
                Web.HttpContext.Current.Response.Clear()
                Web.HttpContext.Current.Response.ClearHeaders()
                Web.HttpContext.Current.Response.ClearContent()
                Extn = objGen.GetFileExt(pstrFileNameAndPath)
                sFileName = System.IO.Path.GetFileNameWithoutExtension(pstrFileNameAndPath)
                sFullName = sFileName & "." & Extn
                pstrContentType = "application/x-msexcel"
                Dim Range As String = Web.HttpContext.Current.Request.Headers("Range")
                If Not ((Range Is Nothing) Or (Range = "")) Then
                    Dim StartEnd As Array = Range.Substring(Range.LastIndexOf("=") + 1).Split("-")
                    If Not StartEnd(0) = "" Then
                        StartPos = CType(StartEnd(0), Long)
                    End If
                    If StartEnd.GetUpperBound(0) >= 1 And Not StartEnd(1) = "" Then
                        EndPos = CType(StartEnd(1), Long)
                    Else
                        EndPos = FileSize - StartPos
                    End If
                    If EndPos > FileSize Then
                        EndPos = FileSize - StartPos
                    End If
                    System.Web.HttpContext.Current.Response.StatusCode = 206
                    System.Web.HttpContext.Current.Response.StatusDescription = "Partial Content"
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Range", "bytes " & StartPos & "-" & EndPos & "/" & FileSize)
                End If
                System.Web.HttpContext.Current.Response.ContentType = pstrContentType
                System.Web.HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename=" & sFullName & "")
                System.Web.HttpContext.Current.Response.WriteFile(Server.HtmlEncode(pstrFileNameAndPath), StartPos, EndPos)
                System.Web.HttpContext.Current.Response.Flush()
                System.Web.HttpContext.Current.Response.End()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LnkbtnChallan_Click(sender As Object, e As EventArgs) Handles LnkbtnChallan.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(3))
            End If
            Response.Redirect(String.Format("~/Accounts/ChallanDetails.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LnkbtnChallan_Click")
        End Try
    End Sub
End Class

