Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports DatabaseLayer
Partial Class Sales_UploadSalesOrders
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_UploadSalesOrders"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGenFun As New clsGeneralFunctions
    Dim objSOU As New ClsSalesOrderUpload
    Private Shared sFile As String
    Private objclsModulePermission As New clsModulePermission
    Private objDBL As New DBHelper
    Private Shared sExcelSave As String
    Dim objclsFASPermission As New clsFASPermission
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SOU")
                imgbtnSave.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                    End If
                End If
                'imgbtnSave.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasSOU", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then

                '    End If
                '    If sFormButtons.Contains(",Upload,") = True Then
                '        imgbtnSave.Visible = True
                '    End If
                'End If

                'imgbtnSave.Visible = False
                lblError.Text = ""
                LoadParty()
                lblMesg.Text = "If there are multiple rates for an item you need to update in sales Order form."
                Me.imgbtnSave.Attributes.Add("OnClick", "return validateSave()")
                Me.LnkbtnExcel.Attributes.Add("OnClick", "return validation()")
                Me.btnExcelSheetName.Attributes.Add("OnClick", "return validation()")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadParty()
        Try
            ddlParty.DataSource = objSOU.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "BM_Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "--- Select Party ---")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadParty")
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

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim dt As New DataTable
        Try
            lblError.Text = ""

            If IsNothing(Session("dtUpload")) = False Then
                dt = Session("dtUpload")
            Else
                lblError.Text = "Load before you Upload."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 1 To dt.Rows.Count - 1

                If dt.Rows(i)(4).ToString() <> "" Then   'Checking for Qty greater than 0 

                    If dt.Rows(i)(0).ToString() = "" Then
                        lblError.Text = "Item Code cannot be blank" & " Line No - " & i + 1
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If

                    If dt.Rows(i)(0).ToString() <> "" Then
                        Dim iRet As Integer = objSOU.CheckItemCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(0).ToString())
                        If iRet = 0 Then
                            lblError.Text = "Item Code does not exists." & " Line No - " & i + 1
                            lblCustomerValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    If dt.Rows(i)(1).ToString() = "" Then
                        lblError.Text = "Goods cannot be blank" & " Line No - " & i + 1
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If

                    If dt.Rows(i)(1).ToString() <> "" Then
                        Dim iRet As Integer = objSOU.CheckGoodsExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(1).ToString())
                        If iRet = 0 Then
                            lblError.Text = "Goods name does not exists." & " Line No - " & i + 1
                            lblCustomerValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    If dt.Rows(i)(2).ToString() = "" Then
                        lblError.Text = "Unit Of Meassurement cannot be blank" & " Line No - " & i + 1
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If

                    'Unit Of Meassurement
                    If dt.Rows(i)(2).ToString() <> "" Then
                        Dim iRet As Integer = objSOU.CheckCityExistOrNot(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(2).ToString(), 1)
                        If iRet = 0 Then
                            lblError.Text = "Create " & dt.Rows(i)(2).ToString() & " - Unit Of Meassurement in General Master " & " Line No - " & i + 1
                            lblCustomerValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    If dt.Rows(i)(3).ToString() = "" Then
                        lblError.Text = "Rate cannot be blank" & " Line No - " & i + 1
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If

                    'If dt.Rows(i)(3).ToString() = "" Then
                    '    lblError.Text = "Quantity cannot be blank" & " Line No - " & i + 1
                    '    Exit Sub
                    'End If

                End If

            Next
            Dim iDefaultBranch As Integer

            iDefaultBranch = objSOU.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
            If iDefaultBranch = 0 Then
                lblError.Text = "Set Default Branch in Organisation Structure."
                Exit Sub
            End If

            objSOU.SaveSalesOrderMasters(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Session("dtUpload"), sSession.UserID, sSession.IPAddress, ddlParty.SelectedValue)
            lblError.Text = "Uploaded Successfully"
            lblCustomerValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub

    Private Sub btnExcelSheetName_Click(sender As Object, e As EventArgs) Handles btnExcelSheetName.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            'imgbtnSave.Visible = False
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
            Else
                lblError.Text = "Select The Excel File"
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnExcelSheetName_Click")
        End Try
    End Sub

    Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable
        Try
            lblError.Text = ""
            'PanelLoad.Visible = False
            If ddlSheetName.SelectedIndex > 0 Then
                dttable = LoadExcelTable(sFile)
                If IsNothing(dttable) Then
                    lblError.Text = "Invalid Excel format in selected sheet."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ddlSheetName.Items.Clear()
                    Exit Sub
                End If
                dgUpload.DataSource = dttable
                dgUpload.DataBind()
                dgUpload.Visible = True

                Session("dtUpload") = dttable
                'If dttable.Rows.Count > 0 Then
                '    imgbtnSave.Visible = True : imgbtnSave.Enabled = True
                'Else
                '    imgbtnSave.Visible = True : imgbtnSave.Enabled = False
                'End If

            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                lblError.Text = "Invalid Excel format in selected sheet."
                ddlSheetName.Items.Clear()
                imgbtnSave.Enabled = False
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
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
    Private Function LoadExcelTable(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim dRow As DataRow
        Dim i As Integer, iSlNo As Integer
        Try
            dtTable.Columns.Add("ItemCode")
            dtTable.Columns.Add("Good")
            dtTable.Columns.Add("Unit")
            dtTable.Columns.Add("Rate")
            dtTable.Columns.Add("Quantity")

            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If

            'If dtStock.Columns.Count > 8 Then
            '    lblError.Text = "Invalid Excel format in selected sheet." : lblErrorDown.Text = "Invalid Excel format in selected sheet."
            '    ddlSheetName.Items.Clear()
            '    Exit Function
            'End If

            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow

                If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                    If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then

                        If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                            If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                dRow("ItemCode") = objGen.SafeSQL(dtStock.Rows(i).Item(0))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                            If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                                dRow("Good") = objGen.SafeSQL(dtStock.Rows(i).Item(1))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                            If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                                dRow("Unit") = objGen.SafeSQL(dtStock.Rows(i).Item(2))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                            If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                                dRow("Rate") = objGen.SafeSQL(dtStock.Rows(i).Item(3))
                            End If
                        End If

                        dRow("Quantity") = objGen.SafeSQL(dtStock.Rows(i).Item(4))

                        dtTable.Rows.Add(dRow)

                    End If
                End If

            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExcelTable")
        End Try
    End Function
    Public Function DownLoadMyFile1(ByVal sPath As String, ByVal sName As String)
        Dim pstrFileNameAndPath As String
        Dim pstrContentType As String
        Try
            'If chkInOut.Checked = False Then
            If InStr(UCase(sName), "PDF") <> 0 Then
                pstrContentType = "application/pdf"
            ElseIf InStr(UCase(sName), "XLS") <> 0 Then
                pstrContentType = "application/x-msexcel"
            ElseIf InStr(UCase(sName), "XLSX") <> 0 Then
                pstrContentType = "application/x-msexcel"
            ElseIf InStr(UCase(sName), "DOC") <> 0 Then
                pstrContentType = "application/x-msword"
            ElseIf InStr(UCase(sName), "DOCX") <> 0 Then
                pstrContentType = "application/x-msword"
            ElseIf InStr(UCase(sName), "MDB") <> 0 Then
                pstrContentType = "application/access"
            ElseIf InStr(UCase(sName), "MDBX") <> 0 Then
                pstrContentType = "application/access"
            ElseIf InStr(UCase(sName), "PPT") <> 0 Then
                pstrContentType = "image/vnd.ms-powerpoint"
            ElseIf InStr(UCase(sName), "PPTX") <> 0 Then
                pstrContentType = "image/vnd.ms-powerpoint"
            ElseIf InStr(UCase(sName), "RTF") <> 0 Then
                pstrContentType = "application/x-msword"
            End If
            pstrFileNameAndPath = sPath
            If IO.File.Exists(pstrFileNameAndPath) Then
                Dim myFileInfo As IO.FileInfo
                Dim StartPos As Long = 0, FileSize As Long, EndPos As Long
                myFileInfo = New IO.FileInfo(pstrFileNameAndPath)
                FileSize = myFileInfo.Length
                EndPos = FileSize
                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.ClearHeaders()
                HttpContext.Current.Response.ClearContent()
                Dim Range As String = HttpContext.Current.Request.Headers("Range")
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
                    HttpContext.Current.Response.StatusCode = 206
                    HttpContext.Current.Response.StatusDescription = "Partial Content"
                    HttpContext.Current.Response.AppendHeader("Content-Range", "bytes " & StartPos & "-" & EndPos & "/" & FileSize)
                End If
                Response.ContentType = pstrContentType
                HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename=" & sName)
                HttpContext.Current.Response.WriteFile(pstrFileNameAndPath, StartPos, EndPos)
                HttpContext.Current.Response.End()
                HttpContext.Current.Response.Clear()
            End If
            Response.Write("<script language=javascript> window.close();</script>")
        Catch EXP As System.Exception
        Finally
            Response.Write("<script language=javascript> window.close();</script>")
        End Try
    End Function

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

    Private Sub LnkbtnExcel_Click(sender As Object, e As EventArgs) Handles LnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dtConductRA As New DataTable
        Dim sCode As String = ""
        Dim sArray As String()
        Dim sbret As String
        Try

            If ddlParty.SelectedIndex > 0 Then
                'sCode = objSOU.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)

                sbret = objSOU.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                sArray = sbret.Split("-")
                For i = 0 To sArray.Length - 1
                    sCode = Trim(sArray(i))
                    If sCode = "P" Then
                        GoTo ItemCode
                    ElseIf sCode = "C" Then
                        GoTo ItemCode
                    End If

                Next

ItemCode:       ReportViewer1.Reset()
                dtConductRA = objSOU.BindStockItems(sSession.AccessCode, sSession.AccessCodeID, sCode)
                If dtConductRA.Rows.Count = 0 Then
                    lblError.Text = "No Rows"
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                End If
                Dim rds As New ReportDataSource("DataSet1", dtConductRA)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesOrderUpload.rdlc")
                ReportViewer1.LocalReport.Refresh()
                Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", "attachment; filename=Order" + ".xls")
                Response.BinaryWrite(RptViewer)
                Response.Flush()
                Response.End()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LnkbtnExcel_Click")
        End Try
    End Sub
End Class
