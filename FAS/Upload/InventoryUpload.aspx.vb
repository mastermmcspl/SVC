Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Imports DatabaseLayer
Partial Class InventoryUpload
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_UploadSalesOrders"
    Dim objFasGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGenFun As New clsGeneralFunctions
    Dim objSU As New clsSupplierUpload
    Private Shared sFile As String
    Private objDBL As New DBHelper
    Private Shared sExcelSave As String
    Dim objphysicalstock As New clsPhysicalStockExcelUpload
    Dim objInvUpload As New clsInventoryMasterUpload
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        sSession = Session("AllSession")
        Try
            If IsPostBack = False Then

            End If
        Catch ex As Exception
            lblErrorup.Text = ex.Message
            '  lblErrorDown.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
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
        Try
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';Data Source=" & sFile & ""
            con.Open()
            Return con
        Catch ex As Exception
        End Try
    End Function
    Public Function CheckFoodMasterExcelSheet(ByVal sStr As String) As String
        Dim dt As New DataTable
        Dim sArray As String()
        Dim sEffectiveFrom As String = ""
        Dim sEffectiveTo As String = ""
        Try
            dt = Session("dtUpload")
            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(2).ToString() = "" Then
                    lblErrorup.Text = "Description cannot be blank" & " Line No - " & i + 1
                    Exit Function
                End If

                If dt.Rows(i)(2).ToString() = "" Then
                    lblErrorup.Text = "Code cannot be blank" & " Line No - " & i + 1
                    Exit Function
                    'Else
                    '    Dim iRet As Integer = clsInventoryMasterUpload.CheckInventoryCode(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(2).ToString())
                    '    If iRet = 1 Then
                    '        lblErrorup.Text = "Code is already exist. Enter new code" & " Line No - " & i + 1
                    '        Exit Function
                    '    End If
                End If

                If dt.Rows(i)(3).ToString() = "" Then
                    lblErrorup.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    Exit Function
                Else
                    If objInvUpload.CheckUnitOfMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString()) = False Then
                        lblErrorup.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                        Exit Function
                    End If
                End If

                If dt.Rows(i)(4).ToString() = "" Then
                    lblErrorup.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    Exit Function
                Else
                    If objInvUpload.CheckUnitOfMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString()) = False Then
                        lblErrorup.Text = "Create Alternate unit of measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                        Exit Function
                    End If
                End If

                If dt.Rows(i)(5).ToString() = "" Then
                    lblErrorup.Text = "Qty in Pieces cannot be blank" & " Line No - " & i + 1
                    Exit Function
                End If

                If dt.Rows(i)(10).ToString() <> "" Then
                    sArray = dt.Rows(i)(10).ToString().Split("/")
                    If (sArray(0) <> "") And (IsNumeric(sArray(0)) = True) Then
                        If (sArray(0) >= 1) And (sArray(0) <= 31) Then
                            If (sArray(1) >= 1) And (sArray(1) <= 12) = False Then
                                lblErrorup.Text = "Enter Effective From Date in dd/MM/yyyy format" & " Line No - " & i + 1
                                Exit Function
                            End If
                        Else
                            lblErrorup.Text = "Enter Effective From Date in dd/MM/yyyy format" & " Line No - " & i + 1
                            Exit Function
                        End If
                    Else
                        lblErrorup.Text = "Enter Effective From Date in dd/MM/yyyy format" & " Line No - " & i + 1
                        Exit Function
                    End If
                End If

                If dt.Rows(i)(11).ToString() <> "" Then
                    sArray = dt.Rows(i)(11).ToString().Split("/")
                    If (sArray(0) <> "") And (IsNumeric(sArray(0)) = True) Then
                        If (sArray(0) >= 1) And (sArray(0) <= 31) Then
                            If (sArray(1) >= 1) And (sArray(1) <= 12) = False Then
                                lblErrorup.Text = "Enter Effective To Date in dd/MM/yyyy format" & " Line No - " & i + 1
                                Exit Function
                            End If
                        Else
                            lblErrorup.Text = "Enter Effective To Date in dd/MM/yyyy format" & " Line No - " & i + 1
                            Exit Function
                        End If
                    Else
                        lblErrorup.Text = "Enter Effective To Date in dd/MM/yyyy format" & " Line No - " & i + 1
                        Exit Function
                    End If
                End If
            Next

            objInvUpload.SaveInvenotryMasters(sSession.AccessCode, sSession.AccessCodeID, Session("dtUpload"), sSession.UserID, sStr)
            lblErrorup.Text = "Successfully Upload" : lblErrorup.Text = "Successfully Upload"
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckNONFoodMasterExcelSheet(ByVal sStr As String) As String
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
                Exit Function
            End If
            dt = Session("dtUpload")
            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(0).ToString() = "" Then
                    dt.Rows(i)(0) = sCommidity
                    If (sCommidity = "") Then
                        lblErrorup.Text = "Commidity cannot be blank" & " Line No - " & i + 1
                        lblErrorup.Text = "Commidity cannot be blank" & " Line No - " & i + 1
                        Exit Function
                    End If
                Else
                    sCommidity = dt.Rows(i)(0).ToString()
                End If

                If dt.Rows(i)(1).ToString() = "" Then
                    lblErrorup.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
                    lblErrorup.Text = "Description of Goods cannot be blank" & " Line No - " & i + 1
                    Exit Function
                End If

                If dt.Rows(i)(2).ToString() = "" Then
                    lblErrorup.Text = "Code cannot be blank" & " Line No - " & i + 1
                    lblErrorup.Text = "Code cannot be blank" & " Line No - " & i + 1
                    Exit Function
                End If

                If dt.Rows(i)(3).ToString() = "" Then
                    lblErrorup.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    lblErrorup.Text = "Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    Exit Function
                Else
                    If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(3).ToString()) = 0 Then
                        lblErrorup.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                        lblErrorup.Text = "Create Unit of Measurement " & dt.Rows(i)(3).ToString() & " in the General Master"
                        Exit Function
                    End If
                End If

                If dt.Rows(i)(4).ToString() = "" Then
                    lblErrorup.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    lblErrorup.Text = "Alternate Unit of Measurement cannot be blank" & " Line No - " & i + 1
                    Exit Function
                Else
                    If objphysicalstock.GetUnitofMeasurement(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(4).ToString()) = 0 Then
                        lblErrorup.Text = "Create Alternate unit of Measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                        lblErrorup.Text = "Create Alternate unit of measurement " & dt.Rows(i)(4).ToString() & " in the General Master"
                        Exit Function
                    End If
                End If

                If dt.Rows(i)(6).ToString() = "" Then
                    lblErrorup.Text = "VAT Cannot be blank" & " Line No - " & i + 1
                    Exit Function
                Else
                    If objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString()) = 0 Then
                        lblErrorup.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
                        lblErrorup.Text = "Create VAT Rate " & dt.Rows(i)(6).ToString() & " in the General Master"
                        Exit Function
                    End If
                End If

                If dt.Rows(i)(8).ToString() = "" Then
                    lblErrorup.Text = "Excise Cannot be blank" & " Line No - " & i + 1
                    Exit Function
                Else
                    If objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString()) = 0 Then
                        lblErrorup.Text = "Create Excise Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
                        lblErrorup.Text = "Create Excise Rate " & dt.Rows(i)(8).ToString() & " in the General Master"
                        Exit Function
                    End If
                End If

                If dt.Rows(i)(7).ToString() = "" Then
                    lblErrorup.Text = "CST can not  be blank" & " Line No - " & i + 1
                    Exit Function
                Else
                    If objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString()) = 0 Then
                        lblErrorup.Text = "Create CST Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
                        lblErrorup.Text = "Create CST Rate " & dt.Rows(i)(7).ToString() & " in the General Master"
                        Exit Function
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
                objphysicalstock.INVH_EffeFrom = objFasGen.FormatDtForRDBMS(dEffectiveFrom, "D")
                objphysicalstock.INVH_EffeTo = objFasGen.FormatDtForRDBMS(dEffectiveTo, "D")
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
                objphysicalstock.InvH_Excise = sExcise
                objphysicalstock.InvH_Cst = scst
                objphysicalstock.InvH_Vat = sVat
                objphysicalstock.InvH_Excise = objphysicalstock.GetExciseRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(8).ToString())
                objphysicalstock.InvH_Cst = objphysicalstock.GetCSTRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(7).ToString())
                objphysicalstock.InvH_Vat = objphysicalstock.GetVatRate(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(i)(6).ToString())
                Arr = objphysicalstock.SaveTaxDetails(sSession.AccessCode, objphysicalstock)

            Next
            lblErrorup.Text = "Successfully Upload"
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Sub btnExcelSheetName_Click(sender As Object, e As EventArgs) Handles btnExcelSheetName.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try

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
                    lblErrorup.Text = "Select Excel file only."
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
    Protected Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable
        Dim sStr As String = ""
        Try
            lblErrorup.Text = ""
            'PanelLoad.Visible = False
            If ddlSheetName.SelectedIndex > 0 Then
                'sStr = clsInventoryMasterUpload.GetFoodORNonFood(sSession.AccessCode, sSession.AccessCodeID)
                'If sStr = "Food" Then

                '    dttable = LoadExcelFoodTable(sFile)
                '    If IsNothing(dttable) Then
                '        lblErrorup.Text = "Invalid Excel format in selected sheet." : lblErrorDown.Text = "Invalid Excel format in selected sheet."
                '        ddlSheetName.Items.Clear()
                '        Exit Sub
                '    End If
                '    dgUpload.DataSource = dttable
                '    dgUpload.DataBind()
                '    dgUpload.Visible = True

                'ElseIf sStr = "NonFood" Then
                '    dttable = LoadExcelNonFoodTable(sFile)
                '    If IsNothing(dttable) Then
                '        lblErrorup.Text = "Invalid Excel format in selected sheet." : lblErrorDown.Text = "Invalid Excel format in selected sheet."
                '        ddlSheetName.Items.Clear()
                '        Exit Sub
                '    End If
                '    dgNonFood.DataSource = dttable
                '    dgNonFood.DataBind()
                '    dgNonFood.Visible = True

                'ElseIf sStr = "BothFoodAndNonFood" Then

                dttable = LoadExcelNonFoodTable(sFile)
                If IsNothing(dttable) Then
                    lblErrorup.Text = "Invalid Excel format in selected sheet."
                    ddlSheetName.Items.Clear()
                    Exit Sub
                End If
                dgUpload.DataSource = dttable
                dgUpload.DataBind()
                dgUpload.Visible = True
                'End If
                Session("dtUpload") = dttable

            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                lblErrorup.Text = "Invalid Excel format in selected sheet."
                ddlSheetName.Items.Clear()

            Else
                lblErrorup.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadExcelFoodTable(ByVal sFile As String) As DataTable
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
            dtTable.Columns.Add("MRP")
            dtTable.Columns.Add("Retail")
            dtTable.Columns.Add("Effective From")
            dtTable.Columns.Add("Effective To")
            dtTable.Columns.Add("Pre determined Price")
            dtTable.Columns.Add("Others")

            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblErrorup.Text = "Invalid Excel format in selected sheet."
                Return dtStock
            End If

            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow

                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then

                        If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                            If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                dRow("Commidity") = objFasGen.SafeSQL(dtStock.Rows(i).Item(0))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                            If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                                dRow("Description of Goods") = objFasGen.SafeSQL(dtStock.Rows(i).Item(1))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                            If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                                dRow("Code") = objFasGen.SafeSQL(dtStock.Rows(i).Item(2))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                            If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                                dRow("unit of Measurement") = objFasGen.SafeSQL(dtStock.Rows(i).Item(3))
                            End If
                        End If


                        If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                            dRow("Alternative") = objFasGen.SafeSQL(dtStock.Rows(i).Item(4))
                        Else
                            dRow("Alternative") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                            dRow("Qty in Pieces") = objFasGen.SafeSQL(dtStock.Rows(i).Item(5))
                        Else
                            dRow("Qty in Pieces") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(6)) = False Then

                            dRow("VAT") = objFasGen.SafeSQL(dtStock.Rows(i).Item(6))
                        Else
                            dRow("VAT") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(7)) = False Then

                            dRow("Excise") = objFasGen.SafeSQL(dtStock.Rows(i).Item(7))
                        Else
                            dRow("Excise") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                            dRow("MRP") = objFasGen.SafeSQL(dtStock.Rows(i).Item(8))
                        Else
                            dRow("MRP") = ""
                        End If


                        If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
                            dRow("Retail") = objFasGen.SafeSQL(dtStock.Rows(i).Item(9))
                        Else
                            dRow("Retail") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(10)) = False Then
                            dRow("Effective From") = objFasGen.SafeSQL(dtStock.Rows(i).Item(10))
                            'Else
                            '    dRow("Effective From") = "#01/01/1900#"
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(11)) = False Then
                            dRow("Effective To") = objFasGen.SafeSQL(dtStock.Rows(i).Item(11))
                            'Else
                            '    dRow("Effective To") = "#01/01/1900#"
                        End If


                        If IsDBNull(dtStock.Rows(i).Item(12)) = False Then
                            dRow("Pre determined Price") = objFasGen.SafeSQL(dtStock.Rows(i).Item(12))
                        Else
                            dRow("Pre determined Price") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(13)) = False Then
                            dRow("Others") = objFasGen.SafeSQL(dtStock.Rows(i).Item(13))
                        Else
                            dRow("Others") = ""
                        End If

                    End If
                    dtTable.Rows.Add(dRow)
                End If
            Next
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function LoadExcelNonFoodTable(ByVal sFile As String) As DataTable
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
                                dRow("Commidity") = objFasGen.SafeSQL(dtStock.Rows(i).Item(0))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                            If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                                dRow("Description of Goods") = objFasGen.SafeSQL(dtStock.Rows(i).Item(1))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                            If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                                dRow("Code") = objFasGen.SafeSQL(dtStock.Rows(i).Item(2))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                            If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                                dRow("unit of Measurement") = objFasGen.SafeSQL(dtStock.Rows(i).Item(3))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                            dRow("Alternative") = objFasGen.SafeSQL(dtStock.Rows(i).Item(4))
                        Else
                            dRow("Alternative") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                            dRow("Qty in Pieces") = objFasGen.SafeSQL(dtStock.Rows(i).Item(5))
                        Else
                            dRow("Qty in Pieces") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(11)) = False Then

                            dRow("VAT") = objFasGen.SafeSQL(dtStock.Rows(i).Item(11))
                        Else
                            dRow("VAT") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(13)) = False Then

                            dRow("Excise") = objFasGen.SafeSQL(dtStock.Rows(i).Item(13))
                        Else
                            dRow("Excise") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(12)) = False Then
                            dRow("CST") = objFasGen.SafeSQL(dtStock.Rows(i).Item(12))
                        Else
                            dRow("CST") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                            dRow("MRP") = objFasGen.SafeSQL(dtStock.Rows(i).Item(6))
                        Else
                            dRow("MRP") = ""
                        End If


                        If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                            dRow("Retail") = objFasGen.SafeSQL(dtStock.Rows(i).Item(7))
                        Else
                            dRow("Retail") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                            dRow("Effective From") = dtStock.Rows(i).Item(8)
                        Else
                            dRow("Effective From") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
                            dRow("Effective To") = dtStock.Rows(i).Item(9)
                        Else
                            dRow("Effective To") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(10)) = False Then
                            dRow("Pre determined Price") = objFasGen.SafeSQL(dtStock.Rows(i).Item(10))
                        Else
                            dRow("Pre determined Price") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(14)) = False Then
                            dRow("Others") = objFasGen.SafeSQL(dtStock.Rows(i).Item(14))
                        Else
                            dRow("Others") = ""
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(15)) = False Then
                            dRow("color") = objFasGen.SafeSQL(dtStock.Rows(i).Item(15))
                        Else
                            dRow("color") = ""
                        End If
                        If IsDBNull(dtStock.Rows(i).Item(16)) = False Then
                            If dtStock.Rows(i).Item(12).ToString <> "&nbsp;" Then
                                dRow("size") = objFasGen.SafeSQL(dtStock.Rows(i).Item(16))
                            End If
                        End If

                        If IsDBNull(dtStock.Rows(i).Item(17)) = False Then
                            dRow("article No/Color Code") = objFasGen.SafeSQL(dtStock.Rows(i).Item(17))
                        Else
                            dRow("article No/Color Code") = ""
                        End If


                    End If
                    dtTable.Rows.Add(dRow)
                End If
            Next
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub LnkbtnExcel_Click(sender As Object, e As EventArgs) Handles LnkbtnExcel.Click
        Try
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=NonFoodMasters.xlsx")
            Response.TransmitFile(Server.MapPath("../") & "SampleExcels\NonFoodMasters.xlsx")
            Response.End()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            CheckNONFoodMasterExcelSheet("hello")
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
