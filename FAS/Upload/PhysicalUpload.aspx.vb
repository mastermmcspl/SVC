Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports DatabaseLayer
Partial Class Purchase_SupplierUpload
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_UploadSalesOrders"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGenFun As New clsGeneralFunctions
    Dim objSU As New clsSupplierUpload
    Private Shared sFile As String
    Private objDBL As New DBHelper
    Private Shared sExcelSave As String
    Dim objphysicalstock As New clsPhysicalStockExcelUpload
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnSave.Visible = False
                lblErrorup.Text = ""
                Me.imgbtnSave.Attributes.Add("OnClick", "return validationSave()")
            End If
        Catch ex As Exception
            lblErrorup.Text = ex.Message
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

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim dt As New DataTable

        Dim dt1 As New DataTable
        Dim Arr() As String, sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iParent, iMax As Integer, iUnit As Integer = 0, iAlternative As Integer = 0, iExistID As Integer, iExistStockID As Integer, iHistoryID As Integer = 0, iPerPiece As Integer = 0
        Dim sCommidity As String = "", sVat As String = "", sExcise As String = "", sColor As String = "", sSize As String = "", sAcode As String = "", scst As String = ""
        'Dim sCst As String = ""
        Dim dMRP As Double = "0.0", dRetail As Double = "0.0", dPreDetermined As Double = "0.0", dOthers As Double = "0.0"
        Dim iPQuantity As Integer, iOpeningQty As Integer, igetVat As Integer, igetCst As Integer, iExcise As Integer
        Dim dEffectiveFrom As Date, dEffectiveTo As Date
        Try

            If dgUpload.Rows.Count = 0 Then
                lblErrorup.Text = "Please select the Load Button "
                lblErrorup.Text = "Please select the Load Button "
                Exit Sub
            End If
            dt = Session("dtUpload")
            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(0).ToString() = "" Then
                    dt.Rows(i)(0) = sCommidity
                    If (sCommidity = "") Then
                        lblErrorup.Text = "Commidity cannot be blank" & " Line No - " & i + 1
                        lblErrorup.Text = "Commidity cannot be blank" & " Line No - " & i + 1
                        Exit Sub
                    End If
                Else
                    sCommidity = dt.Rows(i)(0).ToString()
                End If

                If dt.Rows(i)(1).ToString() = "" Then
                    lblErrorup.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
                    lblErrorup.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
                    Exit Sub
                End If

                If dt.Rows(i)(2).ToString() = "" Then
                    lblErrorup.Text = "Code cannot be blank" & " Line No - " & i + 1
                    lblErrorup.Text = "Code cannot be blank" & " Line No - " & i + 1
                    Exit Sub
                End If

                If dt.Rows(i)(3).ToString() = "" Then
                    lblErrorup.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    lblErrorup.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    Exit Sub
                Else
                    If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString()) = 0 Then
                        lblErrorup.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                        lblErrorup.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                        Exit Sub
                    End If
                End If

                If dt.Rows(i)(4).ToString() = "" Then
                    lblErrorup.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    lblErrorup.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    Exit Sub
                Else
                    If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString()) = 0 Then
                        lblErrorup.Text = "Create Alternate unit of Measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                        lblErrorup.Text = "Create Alternate unit of measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                        Exit Sub
                    End If
                End If

                If dt.Rows(i)(6).ToString() = "" Then
                    lblErrorup.Text = "VAT Cannot be blank" & " Line No - " & i + 1
                    Exit Sub
                Else
                    If objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString()) = 0 Then
                        lblErrorup.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
                        lblErrorup.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
                        Exit Sub
                    End If
                End If

                If dt.Rows(i)(8).ToString() = "" Then
                    lblErrorup.Text = "Excise Cannot be blank" & " Line No - " & i + 1
                    Exit Sub
                Else
                    If objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString()) = 0 Then
                        lblErrorup.Text = "Create Excise Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
                        lblErrorup.Text = "Create Excise Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
                        Exit Sub
                    End If
                End If

                If dt.Rows(i)(7).ToString() = "" Then
                    lblErrorup.Text = "VAT CST can not  be blank" & " Line No - " & i + 1
                    Exit Sub
                Else
                    If objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString()) = 0 Then
                        lblErrorup.Text = "Create CST Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
                        lblErrorup.Text = "Create CST Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
                        Exit Sub
                    End If
                End If

                'Qty in Pieces
                If dt.Rows(i)(5).ToString() <> "" Then
                    iPerPiece = dt.Rows(i)(5).ToString()
                Else
                    iPerPiece = "0"
                End If

                'VAT
                If dt.Rows(i)(6).ToString() <> "" Then
                    sVat = dt.Rows(i)(6).ToString()
                Else
                    sVat = "0"
                End If

                'Excise
                If dt.Rows(i)(8).ToString() <> "" Then
                    sExcise = dt.Rows(i)(8).ToString()
                Else
                    sExcise = "0"
                End If

                'CST
                If dt.Rows(i)(7).ToString() <> "" Then
                    scst = dt.Rows(i)(7).ToString()
                Else
                    scst = "0"
                End If

                'MRP
                If dt.Rows(i)(9).ToString() <> "" Then
                    dMRP = Convert.ToDouble(dt.Rows(i)(9).ToString())
                Else
                    dMRP = "0.0"
                End If

                'Retail
                If dt.Rows(i)(10).ToString() <> "" Then
                    dRetail = Convert.ToDouble(dt.Rows(i)(10).ToString())
                Else
                    dRetail = "0.0"
                End If

                'Effective From
                If dt.Rows(i)(11).ToString() = "" Then
                    dEffectiveFrom = DateTime.ParseExact(Date.Today, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    If dt.Rows(i)(11).ToString().Length > 10 Then
                        dEffectiveFrom = DateTime.ParseExact(dt.Rows(i)(11).ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        dEffectiveFrom = DateTime.ParseExact(dt.Rows(i)(11).ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                End If

                'Effective To
                If dt.Rows(i)(12).ToString() = "" Then
                    dEffectiveTo = DateTime.ParseExact(Date.Today, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    If dt.Rows(i)(12).ToString().Length > 10 Then
                        dEffectiveTo = DateTime.ParseExact(dt.Rows(i)(12).ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Else
                        dEffectiveTo = DateTime.ParseExact(dt.Rows(i)(12).ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If

                End If


                'PreDetermined
                If dt.Rows(i)(13).ToString() <> "" Then
                    dPreDetermined = Convert.ToDouble(dt.Rows(i)(13).ToString())
                Else
                    dPreDetermined = "0.0"
                End If

                'Others
                If dt.Rows(i)(14).ToString() <> "" Then
                    dOthers = dt.Rows(i)(18).ToString()
                Else
                    dOthers = "0.0"
                End If

                'Color
                If dt.Rows(i)(15).ToString() <> "" Then
                    sColor = dt.Rows(i)(15).ToString()
                Else
                    sColor = ""
                End If

                'Size
                If dt.Rows(i)(16).ToString() <> "" Then
                    sSize = dt.Rows(i)(16).ToString()
                Else
                    sSize = "0"
                End If

                'Alternative No/Color Code
                If dt.Rows(i)(17).ToString() <> "" Then
                    sAcode = dt.Rows(i)(17).ToString()
                Else
                    sAcode = ""
                End If

                'Physical Quantity
                If dt.Rows(i)(18).ToString() <> "" Then
                    iPQuantity = dt.Rows(i)(18).ToString()
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
                objphysicalstock.INVH_EffeFrom = objGen.FormatDtForRDBMS(dEffectiveFrom, "D")
                objphysicalstock.INVH_EffeTo = objGen.FormatDtForRDBMS(dEffectiveTo, "D")
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
                objphysicalstock.InvH_Excise = objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString())
                objphysicalstock.InvH_Cst = objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString())
                objphysicalstock.InvH_Vat = objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString())
                Arr = objphysicalstock.SaveTaxDetails(sSession.AccessCode, objphysicalstock)

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

            Next
            lblErrorup.Text = "Successfully Upload" : lblErrorup.Text = "Successfully Upload"
            'clsSupplierUpload.SaveCommidityMaster(sSession.AccessCode, sSession.AccessCodeID, Session("dtUpload"), sSession.UserID, sSession.IPAddress)
        Catch ex As Exception
            lblErrorup.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub

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
                    ddlSheetName.Items.Insert(0, "Select Sheet")
                Else
                    lblErrorup.Text = "Select Excel file only."
                    lblCustomerValidationMsg.Text = lblErrorup.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblErrorup.Text = ex.Message
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
        End Try
    End Function
    Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable
        Try
            lblErrorup.Text = ""
            If ddlSheetName.SelectedIndex > 0 Then
                dttable = LoadExcelTable(sFile)
                If IsNothing(dttable) Then
                    lblErrorup.Text = "Invalid Excel format in selected sheet."
                    lblCustomerValidationMsg.Text = lblErrorup.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
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
                lblErrorup.Text = "Invalid Excel format in selected sheet."
                ddlSheetName.Items.Clear()
                imgbtnSave.Enabled = False
            Else
                lblErrorup.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadExcelTable(ByVal sFile As String) As DataTable
        Dim objphysicalstock As New clsPhysicalStockExcelUpload
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim dRow As DataRow
        Dim i As Integer, iSlNo As Integer
        Dim objclsBCMUploads As New clsPhysicalStockExcelUpload
        Try
            dtTable.Columns.Add("Commidity")
            dtTable.Columns.Add("Description of Goods")
            dtTable.Columns.Add("Code")
            dtTable.Columns.Add("unit of Measurement")
            dtTable.Columns.Add("Alternative")
            dtTable.Columns.Add("Qty in Pieces")
            dtTable.Columns.Add("VAT")
            dtTable.Columns.Add("Excise")
            dtTable.Columns.Add("CST")
            dtTable.Columns.Add("MRP")
            dtTable.Columns.Add("Retail")
            dtTable.Columns.Add("Effective From")
            dtTable.Columns.Add("Effective To")
            dtTable.Columns.Add("Pre determined Price")
            dtTable.Columns.Add("Others")
            dtTable.Columns.Add("color")
            dtTable.Columns.Add("size")
            dtTable.Columns.Add("article No/Color Code")
            dtTable.Columns.Add("Physical quantity")
            dtTable.Columns.Add("cst")
            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblErrorup.Text = "Invalid Excel format in selected sheet." : lblErrorup.Text = "Invalid Excel format in selected sheet."
                Return dtStock
            End If
            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow
                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then

                        If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                            If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                dRow("Commidity") = objGen.SafeSQL(dtStock.Rows(i).Item(0))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                            If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                                dRow("Description of Goods") = objGen.SafeSQL(dtStock.Rows(i).Item(1))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                            If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                                dRow("Code") = objGen.SafeSQL(dtStock.Rows(i).Item(2))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                            If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                                dRow("unit of Measurement") = objGen.SafeSQL(dtStock.Rows(i).Item(3))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                            dRow("Alternative") = objGen.SafeSQL(dtStock.Rows(i).Item(4))
                        Else
                            dRow("Alternative") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                            dRow("Qty in Pieces") = objGen.SafeSQL(dtStock.Rows(i).Item(5))
                        Else
                            dRow("Qty in Pieces") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(6)) = False Then

                            dRow("VAT") = objGen.SafeSQL(dtStock.Rows(i).Item(6))
                        Else
                            dRow("VAT") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(7)) = False Then

                            dRow("Excise") = objGen.SafeSQL(dtStock.Rows(i).Item(7))
                        Else
                            dRow("Excise") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                            dRow("CST") = objGen.SafeSQL(dtStock.Rows(i).Item(8))
                        Else
                            dRow("CST") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
                            dRow("MRP") = objGen.SafeSQL(dtStock.Rows(i).Item(9))
                        Else
                            dRow("MRP") = ""
                        End If


                        If IsDBNull(dtStock.Rows(i).Item(10)) = False Then
                            dRow("Retail") = objGen.SafeSQL(dtStock.Rows(i).Item(10))
                        Else
                            dRow("Retail") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(11)) = False Then
                            dRow("Effective From") = dtStock.Rows(i).Item(11)
                        Else
                            dRow("Effective From") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(12)) = False Then
                            dRow("Effective To") = dtStock.Rows(i).Item(11)
                        Else
                            dRow("Effective To") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(13)) = False Then
                            dRow("Pre determined Price") = objGen.SafeSQL(dtStock.Rows(i).Item(13))
                        Else
                            dRow("Pre determined Price") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(14)) = False Then
                            dRow("Others") = objGen.SafeSQL(dtStock.Rows(i).Item(14))
                        Else
                            dRow("Others") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(15)) = False Then
                            dRow("color") = objGen.SafeSQL(dtStock.Rows(i).Item(15))
                        Else
                            dRow("color") = ""
                        End If
                        If IsDBNull(dtStock.Rows(i).Item(16)) = False Then
                            If dtStock.Rows(i).Item(12).ToString <> "&nbsp;" Then
                                dRow("size") = objGen.SafeSQL(dtStock.Rows(i).Item(16))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(17)) = False Then
                            dRow("article No/Color Code") = objGen.SafeSQL(dtStock.Rows(i).Item(17))
                        Else
                            dRow("article No/Color Code") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(18)) = False Then
                            dRow("Physical quantity") = objGen.SafeSQL(dtStock.Rows(i).Item(18))
                        End If
                    End If
                    dtTable.Rows.Add(dRow)
                End If
            Next
            Return dtTable
        Catch ex As Exception
            Throw
        End Try

        ''Dim objphysicalstock As New clsPhysicalStockExcelUpload
        'Dim dtTable As New DataTable, dtStock As New DataTable
        'Dim dRow As DataRow
        'Dim i As Integer
        ''Dim objclsBCMUploads As New clsPhysicalStockExcelUpload
        'Try
        '    dtTable.Columns.Add("Code")
        '    dtTable.Columns.Add("Name")
        '    dtTable.Columns.Add("ContactPerson")
        '    dtTable.Columns.Add("Email")
        '    dtTable.Columns.Add("Mobile No")
        '    dtTable.Columns.Add("Land Line No")
        '    dtTable.Columns.Add("Fax")
        '    dtTable.Columns.Add("Address")
        '    dtTable.Columns.Add("Address1")
        '    dtTable.Columns.Add("Address2")
        '    dtTable.Columns.Add("Address3")
        '    dtTable.Columns.Add("PinCode")
        '    dtTable.Columns.Add("City")
        '    dtTable.Columns.Add("State")
        '    dtTable.Columns.Add("Tin")
        '    dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
        '    If IsNothing(dtStock) = True Then
        '        lblErrorup.Text = "Invalid Excel format in selected sheet."
        '        ddlSheetName.Items.Clear()
        '        Return dtStock
        '        lblCustomerValidationMsg.Text = lblErrorup.Text
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        '    End If

        '    If dtStock.Columns.Count > 15 Then
        '        lblErrorup.Text = "Invalid Excel format in selected sheet."
        '        ddlSheetName.Items.Clear()
        '        lblCustomerValidationMsg.Text = lblErrorup.Text
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        '        Exit Function
        '    End If

        '    For i = 0 To dtStock.Rows.Count - 1
        '        dRow = dtTable.NewRow

        '        If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
        '            If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
        '                dRow("Code") = objGen.SafeSQL(dtStock.Rows(i).Item(0))
        '            End If
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
        '            If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
        '                dRow("Name") = objGen.SafeSQL(dtStock.Rows(i).Item(1))
        '            End If
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
        '            If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
        '                dRow("ContactPerson") = objGen.SafeSQL(dtStock.Rows(i).Item(2))
        '            End If
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
        '            If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
        '                dRow("Email") = objGen.SafeSQL(dtStock.Rows(i).Item(3))
        '            End If
        '        End If


        '        If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
        '            dRow("Mobile No") = objGen.SafeSQL(dtStock.Rows(i).Item(4))
        '        Else
        '            dRow("Mobile No") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
        '            dRow("Land Line No") = objGen.SafeSQL(dtStock.Rows(i).Item(5))
        '        Else
        '            dRow("Land Line No") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(6)) = False Then

        '            dRow("Fax") = objGen.SafeSQL(dtStock.Rows(i).Item(6))
        '        Else
        '            dRow("Fax") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(7)) = False Then

        '            dRow("Address") = objGen.SafeSQL(dtStock.Rows(i).Item(7))
        '        Else
        '            dRow("Address") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(8)) = False Then

        '            dRow("Address1") = objGen.SafeSQL(dtStock.Rows(i).Item(8))
        '        Else
        '            dRow("Address1") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(9)) = False Then

        '            dRow("Address2") = objGen.SafeSQL(dtStock.Rows(i).Item(9))
        '        Else
        '            dRow("Address2") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(10)) = False Then

        '            dRow("Address3") = objGen.SafeSQL(dtStock.Rows(i).Item(10))
        '        Else
        '            dRow("Address3") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(11)) = False Then
        '            dRow("PinCode") = objGen.SafeSQL(dtStock.Rows(i).Item(11))
        '        Else
        '            dRow("PinCode") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(12)) = False Then
        '            dRow("City") = objGen.SafeSQL(dtStock.Rows(i).Item(12))
        '        Else
        '            dRow("City") = ""
        '        End If

        '        If IsDBNull(dtStock.Rows(i).Item(13)) = False Then
        '            dRow("State") = objGen.SafeSQL(dtStock.Rows(i).Item(13))
        '        Else
        '            dRow("State") = ""
        '        End If
        '        If IsDBNull(dtStock.Rows(i).Item(14)) = False Then
        '            dRow("Tin") = objGen.SafeSQL(dtStock.Rows(i).Item(14))
        '        Else
        '            dRow("Tin") = ""
        '        End If

        '        dtTable.Rows.Add(dRow)
        '    Next
        '    Return dtTable
        'Catch ex As Exception
        '    Throw
        'End Try
    End Function
    Private Sub LnkbtnExcel_Click(sender As Object, e As EventArgs) Handles LnkbtnExcel.Click
        Dim sPath As String = ""
        Dim objEApp As Excel.Application
        Try
            'objEApp = DirectCast(CreateObject("Excel.Application"), Excel.Application)
            'If (String.Compare(objEApp.Version, "12.0") >= 0) Then
            '    sPath = Server.MapPath("../") & "SampleExcels\PhysicalStock.xlsx"
            'Else
            '    sPath = Server.MapPath("../") & "SampleExcels\PhysicalStock.xls"
            'End If
            'DownloadFile(sPath)

            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=PhysicalStock.xlsx")
            Response.TransmitFile(Server.MapPath("../") & "SampleExcels\PhysicalStock.xlsx")
            Response.End()

        Catch ex As Exception
            lblErrorup.Text = ex.Message
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgUpload_PreRender")
        End Try
    End Sub
    Protected Sub dgUpload_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgUpload.SelectedIndexChanged

    End Sub
End Class
