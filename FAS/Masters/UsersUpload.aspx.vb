Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports DatabaseLayer
Partial Class Masters_UsersUpload
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_UploadSalesOrders"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGenFun As New clsGeneralFunctions
    Dim objUP As New clsUsersUpload
    Private Shared sFile As String
    Private objDBL As New DBHelper
    Private Shared sExcelSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        sSession = Session("AllSession")
        Try
            If IsPostBack = False Then
                ' btnSave.Visible = False
                lblError.Text = ""
                Me.imgbtnSave.Attributes.Add("OnClick", "return validationSave()")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Function ReadExcel(ByVal sSql As String) As DataTable
        Dim con As New OleDb.OleDbConnection
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As New DataSet
        Try
            con = MSAccessOpenConnection()
            If IsNothing(con) = False Then
                da = New OleDb.OleDbDataAdapter(sSql, con)
                da.Fill(ds)
                con.Close()
                Return ds.Tables(0)
            End If
        Catch ex As Exception
        Finally
            da.Dispose()
        End Try
    End Function
    Private Function MSAccessOpenConnection() As OleDb.OleDbConnection
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
        End Try
    End Function
    Private Sub btnExcelSheetName_Click(sender As Object, e As EventArgs) Handles btnExcelSheetName.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            imgbtnSave.Visible = False
            dgUpload.Visible = False
            If FULoad.FileName <> String.Empty Then
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
                    ddlSheetName.Items.Insert(0, "--- Select Sheet ---")
                Else
                    lblError.Text = "Select Excel file only."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnExcelSheetName_Click")
        End Try
    End Sub
    Public Function ExcelSheetNames(ByVal sPath As String) As DataTable
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim XLCon As OleDb.OleDbConnection
        Dim dtTab As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            XLCon = MSAccessOpenConnection(sPath)
            dt = XLCon.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables,
                   New Object() {Nothing, Nothing, Nothing, "TABLE"})
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ExcelSheetNames")
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
        End Try
    End Function

    Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable
        Try
            lblError.Text = ""
            If ddlSheetName.SelectedIndex > 0 Then
                dttable = LoadExcelTable(sFile)
                If IsNothing(dttable) Then
                    'lblError.Text = "Invalid Excel format in selected sheet."
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ddlSheetName.Items.Clear()
                    Exit Sub
                End If
                dgUpload.DataSource = dttable
                dgUpload.DataBind()
                dgUpload.Visible = True
                Session("dtUpload") = dttable
                imgbtnSave.Visible = True : imgbtnSave.Enabled = True
            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                lblError.Text = "Invalid Excel format in selected sheet"
                ddlSheetName.Items.Clear()
                imgbtnSave.Enabled = False
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadExcelTable(ByVal sFile As String) As DataTable
        'Dim objphysicalstock As New clsPhysicalStockExcelUpload
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim dRow As DataRow
        Dim i As Integer, iSlNo As Integer
        'Dim objclsBCMUploads As New clsPhysicalStockExcelUpload
        Try
            dtTable.Columns.Add("Code")
            dtTable.Columns.Add("Name")
            dtTable.Columns.Add("LoginName")
            dtTable.Columns.Add("Email")
            dtTable.Columns.Add("MobileNo")
            dtTable.Columns.Add("OfficeNo")
            dtTable.Columns.Add("Address")

            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If
            If dtStock.Columns.Count > 13 Then
                lblError.Text = "Invalid Excel format in selected sheet."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Exit Function
            End If

            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow

                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("Code") = objGen.SafeSQL(dtStock.Rows(i).Item(0))
                    Else
                        lblError.Text = "Code cannot be blank" & "Line No:" & i
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Function
                    End If
                Else
                    lblError.Text = "Code cannot be blank" & "Line No:" & i
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Function
                End If

                If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("Name") = objGen.SafeSQL(dtStock.Rows(i).Item(1))
                    Else
                        lblError.Text = "Name cannot be blank" & "Line No:" & i
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Function
                    End If
                Else
                    lblError.Text = "Name cannot be blank" & "Line No:" & i
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Function
                End If

                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("LoginName") = objGen.SafeSQL(dtStock.Rows(i).Item(2))
                    Else
                        lblError.Text = "Login Name cannot be blank" & "Line No:" & i
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Function
                    End If
                Else
                    lblError.Text = "Login Name cannot be blank" & "Line No:" & i
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Function
                End If

                If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                    If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("Email") = objGen.SafeSQL(dtStock.Rows(i).Item(3))
                    Else
                        lblError.Text = "Email cannot be blank" & "Line No:" & i
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Function
                    End If
                Else
                    lblError.Text = "Email cannot be blank" & "Line No:" & i
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Function
                End If

                If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                    If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
                        dRow("MobileNo") = objGen.SafeSQL(dtStock.Rows(i).Item(4))
                    Else
                        dRow("MobileNo") = ""
                    End If
                Else
                    dRow("MobileNo") = ""
                End If
                If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                    If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                        dRow("OfficeNo") = objGen.SafeSQL(dtStock.Rows(i).Item(5))
                    Else
                        dRow("OfficeNo") = ""
                    End If
                Else
                    dRow("OfficeNo") = ""
                End If

                If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                    If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
                        dRow("Address") = objGen.SafeSQL(dtStock.Rows(i).Item(6))
                    Else
                        dRow("Address") = ""
                    End If
                Else
                    dRow("Address") = ""
                End If
                dtTable.Rows.Add(dRow)

            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExcelTable")
        End Try
    End Function

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            SaveUserDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub SaveUserDetails()
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
                    If IsDBNull(dtUpload.Rows(j).Item(0)) = False Then
                        objUP.sUsrCode = (dtUpload.Rows(j).Item(0))
                    Else
                        lblError.Text = "Code cannot be blank" & "Line No:" & j
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If IsDBNull(dtUpload.Rows(j).Item(1)) = False Then
                        objUP.sUsrFullName = (dtUpload.Rows(j).Item(1))
                    Else
                        lblError.Text = "Name cannot be blank" & "Line No:" & j
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If IsDBNull(dtUpload.Rows(j).Item(2)) = False Then
                        objUP.sUsrLoginName = (dtUpload.Rows(j).Item(2))
                    Else
                        lblError.Text = "Login Name cannot be blank" & "Line No:" & j
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(3)) = False Then
                        objUP.sUsrEmail = (dtUpload.Rows(j).Item(3))
                    Else
                        lblError.Text = "Email cannot be blank" & "Line No:" & j
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If IsDBNull(dtUpload.Rows(j).Item(4)) = False Then
                        objUP.sUsrMobileNo = (dtUpload.Rows(j).Item(4))
                    Else
                        objUP.sUsrMobileNo = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(5)) = False Then
                        objUP.sUsrOfficePhone = (dtUpload.Rows(j).Item(5))
                    Else
                        objUP.sUsrOfficePhone = ""
                    End If

                    If IsDBNull(dtUpload.Rows(j).Item(6)) = False Then
                        objUP.sUsrAddress = (dtUpload.Rows(j).Item(6))
                    Else
                        objUP.sUsrAddress = ""
                    End If

                    objUP.sUsrPassword = objGen.EncryptPassword("A")

                    objUP.iUserID = 0
                    objUP.sUsrStatus = "C"
                    objUP.iUsrOrgID = 0
                    objUP.iUsrNode = 0
                    objUP.iUsrSentMail = 0
                    objUP.sUsrDutyStatus = "W"
                    objUP.sUsrPhoneNo = ""

                    objUP.sUsrOffPhExtn = ""
                    objUP.iUsrDesignation = 0
                    objUP.iUsrRole = 0
                    objUP.iUsrLevelGrp = 0
                    objUP.iUsrGrpOrUserLvlPerm = 0
                    objUP.sUsrFlag = "W"
                    objUP.iUsrCompID = sSession.AccessCodeID
                    objUP.iUsrCreatedBy = sSession.UserID
                    objUP.sUsrIPAdress = sSession.IPAddress
                    objUP.iUsrMasterModule = 0 : objUP.iUsrPurchaseModule = 0
                    objUP.iUsrSalesModule = 0 : objUP.iUsrAccountsModule = 0

                    objUP.iUsrMasterRole = 0 : objUP.iUsrPurchaseRole = 0
                    objUP.iUsrSalesRole = 0 : objUP.iUsrAccountsRole = 0

                    objUP.SaveEmployeeDetails(sSession.AccessCode, objUP)

                    'objUP.SaveUserData(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.YearID, Code, Name, Loginname, Email, Designation, sMobile, Address, Squestion, Sanswer, Password)
                Next
            Else
                lblError.Text = "Laod before you Save."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            lblError.Text = "Uploaded Successfully"
            lblCustomerValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveUserDetails")
        End Try
    End Sub

    Private Sub LnkbtnExcel_Click(sender As Object, e As EventArgs) Handles LnkbtnExcel.Click
        Try
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=UserUploadDetails.xlsx")
            Response.TransmitFile(Server.MapPath("../") & "SampleExcels\UserUploadDetails.xlsx")
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LnkbtnExcel_Click")
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
End Class
