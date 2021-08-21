Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports System.Globalization
Partial Class FixedAsset_AssetOpeningBalExcelUpload
    Inherits System.Web.UI.Page

    Private Shared sFormName As String = "FixedAsset\AssetOpeningBalExcelUpload.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sIKBBackStatus As String
    Dim objOPExcel As New ClsAssetOpeningBalExcelUpload
    Private Shared sSession As AllSession
    Dim objClsFASGnrl As New clsFASGeneral
    Private Shared sFile As String
    Dim dtExcel As New DataTable
    Private Shared dttable As New DataTable

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        imgbtUpload.ImageUrl = "~/Images/Upload24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        ImgBtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                dttable = Nothing
                LoadZone()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objOPExcel.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "Select Zone")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadZone")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objOPExcel.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "Select Region")
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
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objOPExcel.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "Select Area")
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
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objOPExcel.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "Select Branch")
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
    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            GvOPExcel.DataSource = Nothing
            GvOPExcel.DataBind()

            If FULoad.FileName <> String.Empty Then
                lblSheetName.Visible = True : ddlSheetName.Visible = True
                sExt = IO.Path.GetExtension(FULoad.PostedFile.FileName)
                Session("sExt") = sExt
                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FULoad.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, "ExcelPath")
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
                    lblError.Text = "Select Excel file only." : lblFXOPBalExcelMsg.Text = "Select Excel file only."
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblFXOPBalExcelMsg.Text = "Select Excel file."
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
    Protected Sub lnDown_Click(sender As Object, e As EventArgs) Handles lnDown.Click
        Try
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=AssetOpeningBalance.xlsx")
            Response.TransmitFile(Server.MapPath("~\SampleExcels\AssetOpeningBalUpload.xlsx"))
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnDown_Click")
        End Try
    End Sub
    Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sDateofPur As String = ""
        Dim sAmt As String
        Dim sString, sCode As String
        Dim bCheck As Boolean
        Dim iSupId As Integer
        Try
            dt.Columns.Add("Slno")
            dt.Columns.Add("AssetTransfer")
            dt.Columns.Add("CurrencyTypes")
            dt.Columns.Add("currencyAmount")
            dt.Columns.Add("ActualLocation")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("SupplierName")
            dt.Columns.Add("supplierCode")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetRefNo")
            dt.Columns.Add("Description")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("ItemDescription")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("DateofPurchase")
            dt.Columns.Add("DateOfCommission")
            dt.Columns.Add("Amount")
            dt.Columns.Add("Depreciation")
            If ddlSheetName.SelectedIndex > 0 Then
                dtExcel = LoadExcel(sFile)
                If dtExcel.Rows.Count > 0 Then
                    For i = 0 To dtExcel.Rows.Count - 1
                        Dim dRow As DataRow
                        dRow = dt.NewRow
                        If IsDBNull(dtExcel.Rows(i).Item("Slno")) = False Then
                            If dtExcel.Rows(i).Item("Slno").ToString <> "&nbsp;" Then
                                dRow("Slno") = dtExcel.Rows(i).Item("Slno")
                            Else
                                dRow("Slno") = 0
                            End If
                        End If
                        If dtExcel.Rows(i).Item("AssetTransfer").ToString() = "" Then
                            dRow("AssetTransfer") = ""
                        Else
                            sString = UCase(dtExcel.Rows(i).Item("AssetTransfer"))
                            If sString = "LOCAL" Then
                                If Stringcheck(sString) = False Then
                                    lblError.Text = "Enter Valid AssetTransfer"
                                    Exit Sub
                                Else
                                    dRow("AssetTransfer") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetTransfer"))
                                End If
                            ElseIf sString = "IMPORTED" Then
                                If Stringcheck(sString) = False Then
                                    lblError.Text = "Enter Valid AssetTransfer"
                                    Exit Sub
                                Else
                                    dRow("AssetTransfer") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetTransfer"))
                                End If
                            Else
                                lblError.Text = "Asset Transfer Not Matched"
                                Exit Sub
                            End If
                        End If
                        If dtExcel.Rows(i).Item("currencyAmount").ToString() = "" Then
                            dRow("currencyAmount") = "0.00"
                        Else
                            sAmt = dtExcel.Rows(i).Item("currencyAmount")
                            If Amountcheck(sAmt) = False Then
                                lblError.Text = "Enter Valid currencyAmount"
                                Exit Sub
                            Else
                                dRow("currencyAmount") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("currencyAmount"))
                            End If
                        End If
                        If dtExcel.Rows(i).Item("CurrencyTypes").ToString() = "" Then
                            dRow("CurrencyTypes") = ""
                        Else
                            sString = dtExcel.Rows(i).Item("CurrencyTypes")
                            sCode = objOPExcel.LoadCurrencyName(sSession.AccessCode, sSession.AccessCodeID, sString)
                            If sCode <> "" Then
                                If CurrencyCode(sCode) = False Then
                                    lblError.Text = "Enter Valid CurrencyTypes"
                                    Exit Sub
                                Else
                                    dRow("CurrencyTypes") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("CurrencyTypes"))
                                End If
                            Else
                                lblError.Text = "Currency name not Matched"
                                Exit Sub
                            End If
                        End If

                        If dtExcel.Rows(i).Item("ActualLocation").ToString() = "" Then
                            dRow("ActualLocation") = ""
                        Else
                            sString = dtExcel.Rows(i).Item("ActualLocation")
                            If CurrencyCheck1(sString) = False Then
                                lblError.Text = "Enter Valid ActualLocation"
                                Exit Sub
                            Else
                                dRow("ActualLocation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("ActualLocation"))
                            End If
                        End If

                        If dtExcel.Rows(i).Item("AssetAge").ToString() = "" Then
                            dRow("AssetAge") = "0.00"
                        Else
                            sAmt = dtExcel.Rows(i).Item("AssetAge")
                            If Amountcheck(sAmt) = False Then
                                lblError.Text = "Enter Valid AssetAge"
                                Exit Sub
                            Else
                                dRow("AssetAge") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetAge"))
                            End If
                        End If
                        If dtExcel.Rows(i).Item("TransactionType").ToString() = "" Then
                            dRow("TransactionType") = ""
                        Else
                            sString = UCase(dtExcel.Rows(i).Item("TransactionType"))
                            If sString = "ADDITION" Then
                                If Stringcheck(sString) = False Then
                                    lblError.Text = "Enter Valid TransactionType"
                                    Exit Sub
                                Else
                                    dRow("TransactionType") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("TransactionType")))
                                End If
                            ElseIf sString = "TRANSFERS" Then
                                If Stringcheck(sString) = False Then
                                    lblError.Text = "Enter Valid TransactionType"
                                    Exit Sub
                                Else
                                    dRow("TransactionType") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("TransactionType")))
                                End If
                            ElseIf sString = "REVALUATION" Then
                                If Stringcheck(sString) = False Then
                                    lblError.Text = "Enter Valid TransactionType"
                                    Exit Sub
                                Else
                                    dRow("TransactionType") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("TransactionType")))
                                End If
                            ElseIf sString = "FOREIGN EXCHANGE" Then
                                If Stringcheck(sString) = False Then
                                    lblError.Text = "Enter Valid TransactionType"
                                    Exit Sub
                                Else
                                    dRow("TransactionType") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("TransactionType")))
                                End If
                            Else
                                lblError.Text = "Transaction Type Not Matched"
                                Exit Sub
                            End If
                        End If

                        If dtExcel.Rows(i).Item("SupplierName").ToString() = "" Then
                            dRow("SupplierName") = ""
                        Else
                            sString = dtExcel.Rows(i).Item("SupplierName")
                            iSupId = objOPExcel.GetSupplierName(sSession.AccessCode, sSession.AccessCodeID, sString)
                            If iSupId > 0 Then
                                If SupplierNamecheck(iSupId) = False Then
                                    lblError.Text = "Enter Valid SupplierName"
                                    Exit Sub
                                Else
                                    dRow("SupplierName") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("SupplierName"))
                                End If
                            Else
                                lblError.Text = "Supplier name not matched,Create in supplier Master form"
                                Exit Sub
                            End If
                        End If

                        If dtExcel.Rows(i).Item("supplierCode").ToString() = "" Then
                            dRow("supplierCode") = ""
                        Else
                            sAmt = dtExcel.Rows(i).Item("supplierCode")
                            iSupId = objOPExcel.GetSupplierID1(sSession.AccessCode, sSession.AccessCodeID, dtExcel.Rows(i).Item("SupplierName"), sAmt)
                            If iSupId > 0 Then
                                If SupplierCodeCheck(sAmt) = False Then
                                    lblError.Text = "Enter Valid supplierCode"
                                    Exit Sub
                                Else
                                    dRow("supplierCode") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("supplierCode"))
                                End If
                            Else
                                lblError.Text = "Supplier Code Matched for Given Supplier,Create in Supplier Master from"
                                Exit Sub
                            End If
                        End If
                        If dtExcel.Rows(i).Item("AssetType").ToString() = "" Then
                            lblError.Text = "AssetType Can not be blank"
                            Exit Sub
                        Else
                            sString = dtExcel.Rows(i).Item("AssetType")
                            bCheck = objOPExcel.GetAssetType1(sSession.AccessCode, sSession.AccessCodeID, sString)
                            If bCheck = True Then
                                If StringcheckArea(sString) = False Then
                                    lblError.Text = "Enter Valid Asset Type"
                                    Exit Sub
                                Else
                                    dRow("AssetType") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetType"))
                                End If
                            Else
                                lblError.Text = "AssetType not matched,Create in Chart of Accounts"
                                Exit Sub
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item("AssetRefNo")) = True Then
                            lblError.Text = "Asset Reference no Can not be blank"
                            Exit Sub
                        Else
                            If dtExcel.Rows(i).Item("AssetRefNo").ToString <> "&nbsp;" Then
                                dRow("AssetRefNo") = dtExcel.Rows(i).Item("AssetRefNo")
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item("Description")) = False Then
                            If dtExcel.Rows(i).Item("Description").ToString <> "&nbsp;" Then
                                dRow("Description") = dtExcel.Rows(i).Item("Description")
                            Else
                                dRow("Description") = ""
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item("ItemCode")) = False Then
                            If dtExcel.Rows(i).Item("ItemCode").ToString <> "&nbsp;" Then
                                dRow("ItemCode") = dtExcel.Rows(i).Item("ItemCode")
                            Else
                                dRow("ItemCode") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item("ItemDescription")) = False Then
                            If dtExcel.Rows(i).Item("ItemDescription").ToString <> "&nbsp;" Then
                                dRow("ItemDescription") = dtExcel.Rows(i).Item("ItemDescription")
                            Else
                                dRow("ItemDescription") = ""
                            End If
                        End If
                        If dtExcel.Rows(i).Item("Quantity").ToString() = "" Then
                            dRow("Quantity") = ""
                        Else
                            sAmt = dtExcel.Rows(i).Item("Quantity")
                            If Amountcheck(sAmt) = False Then
                                lblError.Text = "Enter Valid Quantity"
                                Exit Sub
                            Else
                                dRow("Quantity") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Quantity"))
                            End If
                        End If
                        If Trim(dtExcel.Rows(i).Item("DateofPurchase").ToString()) = "" Then
                            dRow("DateofPurchase") = ""
                        Else
                            sDateofPur = dtExcel.Rows(i).Item("DateofPurchase")
                            If Datecheck(sDateofPur) = False Then
                                lblError.Text = "Enter Valid Date"
                                Exit Sub
                            Else
                                dRow("DateofPurchase") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("DateofPurchase"))
                            End If
                        End If

                        If Trim(dtExcel.Rows(i).Item("DateOfCommission").ToString()) = "" Then
                            dRow("DateOfCommission") = ""
                        Else
                            sDateofPur = dtExcel.Rows(i).Item("DateOfCommission")
                            If Datecheck(sDateofPur) = False Then
                                lblError.Text = "Enter Valid Date"
                                Exit Sub
                            Else
                                dRow("DateOfCommission") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("DateOfCommission"))
                            End If
                        End If
                        If dtExcel.Rows(i).Item("Amount").ToString() = "" Then
                            lblError.Text = "Amount  Can not be blank"
                            Exit Sub
                        Else
                            sAmt = dtExcel.Rows(i).Item("Amount")
                            If Amountcheck(sAmt) = False Then
                                lblError.Text = "Enter Valid Amount"
                                Exit Sub
                            Else
                                dRow("Amount") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Amount"))
                            End If
                        End If
                        If dtExcel.Rows(i).Item("Depreciation").ToString() = "" Then
                            lblError.Text = "Amount  Can not be blank"
                            Exit Sub
                        Else
                            sAmt = dtExcel.Rows(i).Item("Depreciation")
                            If Amountcheck(sAmt) = False Then
                                lblError.Text = "Enter Valid Amount"
                                Exit Sub
                            Else
                                dRow("Depreciation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Depreciation"))
                            End If
                        End If
                        dt.Rows.Add(dRow)
                    Next
                    If IsNothing(dt) = True Then
                        Exit Sub
                    End If
                    GvOPExcel.DataSource = dt
                    GvOPExcel.DataBind()
                    dttable = dt.Copy
                Else
                    lblError.Text = "No Data"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
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
        Dim pattern As String = "^[a-zA-Z]+(\s[a-zA-Z]+)?$"
        Dim StringMatch As Match = Regex.Match(sStringm, pattern)
        If StringMatch.Success Then
            Stringcheck = True
        Else
            Stringcheck = False
        End If
    End Function
    Private Function CurrencyCheck1(ByVal sStringm As String) As Boolean
        Dim pattern As String = "^[a-zA-Z]{0,100}?$"
        Dim CurrencyMatch As Match = Regex.Match(sStringm, pattern)
        If CurrencyMatch.Success Then
            CurrencyCheck1 = True
        Else
            CurrencyCheck1 = False
        End If
    End Function
    Private Function CurrencyCode(ByVal sCode As String) As Boolean
        Dim pattern As String = "^[a-zA-Z]*$"
        Dim CCodeMatch As Match = Regex.Match(sCode, pattern)
        If CCodeMatch.Success Then
            CurrencyCode = True
        Else
            CurrencyCode = False
        End If
    End Function
    Private Function SupplierNamecheck(ByVal sStringm As String) As Boolean
        Dim pattern As String = "^[0-9]*$"
        Dim StringMatch As Match = Regex.Match(sStringm, pattern)
        If StringMatch.Success Then
            SupplierNamecheck = True
        Else
            SupplierNamecheck = False
        End If
    End Function

    Private Function StringcheckArea(ByVal sStringArea As String) As Boolean
        Dim pattern As String = "^[(a-zA-Z)\s(a-zA-Z)/(a-zA-Z)]*$"
        Dim StringMatchArea As Match = Regex.Match(sStringArea, pattern)
        If StringMatchArea.Success Then
            StringcheckArea = True
        Else
            StringcheckArea = False
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
    Private Function SupplierCodeCheck(ByVal sCode As String) As Boolean
        Dim pattern As String = "^[(0-9)]?$"
        Dim CodeMatch As Match = Regex.Match(sCode, pattern)
        If CodeMatch.Success Then
            SupplierCodeCheck = True
        Else
            SupplierCodeCheck = False
        End If
    End Function
    Private Function LoadExcel(ByVal sFile As String) As DataTable
        Dim dbhelper As New DBHelper
        Dim dt As New DataTable
        Dim val As Integer = 0
        Try
            dt.Columns.Add("Slno")
            dt.Columns.Add("AssetTransfer")
            dt.Columns.Add("CurrencyTypes")
            dt.Columns.Add("currencyAmount")
            dt.Columns.Add("ActualLocation")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("TransactionType")
            dt.Columns.Add("SupplierName")
            dt.Columns.Add("supplierCode")
            dt.Columns.Add("AssetType")
            dt.Columns.Add("AssetRefNo")
            dt.Columns.Add("Description")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("ItemDescription")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("DateofPurchase")
            dt.Columns.Add("DateOfCommission")
            dt.Columns.Add("Amount")
            dt.Columns.Add("Depreciation")
            dtExcel = dbhelper.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtExcel) = True Then
                Return dtExcel
            End If

            For i = 0 To dtExcel.Rows.Count - 1
                Dim dRow As DataRow
                dRow = dt.NewRow
                If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
                    If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
                            If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                dRow("Slno") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(0))
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(1)) = False Then
                            If dtExcel.Rows(i).Item(1).ToString <> "&nbsp;" Then
                                dRow("AssetTransfer") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(1)))
                            Else
                                dRow("AssetTransfer") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(2)) = False Then
                            If dtExcel.Rows(i).Item(2).ToString <> "&nbsp;" Then
                                dRow("CurrencyTypes") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(2))
                            Else
                                dRow("CurrencyTypes") = ""
                            End If
                        End If

                        If String.IsNullOrEmpty(dtExcel.Rows(i).Item(3).ToString) = False Then
                            dRow("currencyAmount") = Convert.ToDecimal(dtExcel.Rows(i).Item(3).ToString()).ToString("#,##0.00")
                        Else
                            dRow("currencyAmount") = ""
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(4)) = False Then
                            If dtExcel.Rows(i).Item(4).ToString <> "&nbsp;" Then
                                dRow("ActualLocation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(4))
                            Else
                                dRow("ActualLocation") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(5)) = False Then
                            If dtExcel.Rows(i).Item(5).ToString <> "&nbsp;" Then
                                dRow("AssetAge") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(5))
                            Else
                                dRow("AssetAge") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(6)) = False Then
                            If dtExcel.Rows(i).Item(6).ToString <> "&nbsp;" Then
                                dRow("TransactionType") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(6))
                            Else
                                dRow("TransactionType") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(7)) = False Then
                            If dtExcel.Rows(i).Item(7).ToString <> "&nbsp;" Then
                                dRow("SupplierName") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(7))
                            Else
                                dRow("SupplierName") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(8)) = False Then
                            If dtExcel.Rows(i).Item(8).ToString <> "&nbsp;" Then
                                dRow("supplierCode") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(8))
                            Else
                                dRow("supplierCode") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(9)) = False Then
                            If dtExcel.Rows(i).Item(9).ToString <> "&nbsp;" Then
                                dRow("AssetType") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(9))
                            Else
                                dRow("AssetType") = ""
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(10)) = False Then
                            If dtExcel.Rows(i).Item(10).ToString <> "&nbsp;" Then
                                dRow("AssetRefNo") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(10))
                            Else
                                dRow("AssetRefNo") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(11)) = False Then
                            If dtExcel.Rows(i).Item(11).ToString <> "&nbsp;" Then
                                dRow("Description") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(11))
                            Else
                                dRow("Description") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(12)) = False Then
                            If dtExcel.Rows(i).Item(12).ToString <> "&nbsp;" Then
                                dRow("ItemCode") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(12))
                            Else
                                dRow("ItemCode") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(13)) = False Then
                            If dtExcel.Rows(i).Item(13).ToString <> "&nbsp;" Then
                                dRow("ItemDescription") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(13))
                            Else
                                dRow("ItemDescription") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(14)) = False Then
                            If dtExcel.Rows(i).Item(14).ToString <> "&nbsp;" Then
                                dRow("Quantity") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(14))
                            Else
                                dRow("Quantity") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(15)) = False Then
                            If dtExcel.Rows(i).Item(15).ToString <> "&nbsp;" Then
                                dRow("DateofPurchase") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(15))
                            Else
                                dRow("DateofPurchase") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(16)) = False Then
                            If dtExcel.Rows(i).Item(16).ToString <> "&nbsp;" Then
                                dRow("DateOfCommission") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(16))
                            Else
                                dRow("DateOfCommission") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(17)) = False Then
                            If dtExcel.Rows(i).Item(17).ToString <> "&nbsp;" Then
                                dRow("Amount") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(17))
                            Else
                                dRow("Amount") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(18)) = False Then
                            If dtExcel.Rows(i).Item(18).ToString <> "&nbsp;" Then
                                dRow("Depreciation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(18))
                            Else
                                dRow("Depreciation") = "0.00"
                            End If
                        End If
                    End If
                    End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub GvOPExcel_PreRender(sender As Object, e As EventArgs) Handles GvOPExcel.PreRender
        Try
            If GvOPExcel.Rows.Count > 0 Then
                GvOPExcel.UseAccessibleHeader = True
                GvOPExcel.HeaderRow.TableSection = TableRowSection.TableHeader
                GvOPExcel.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvOPExcel_PreRender")
        End Try
    End Sub
    Private Sub imgbtUpload_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtUpload.Click
        Dim Arr As Array
        Dim lblSrNo, lblAssetTransfer, lblCurrencyTypes, lblcurrencyAmount As New Label
        Dim lblActualLocation, lblAssetAge, lblTransactionType, lblSupplierName, lblsupplierCode, lblAssetType, lblAssetRefNo, lblDescription As New Label
        Dim lblItemCode, lblItemDescription, lblQuantity, lblDateofPurchase, lblDateOfCommission, lblAmount, lblDeprcn As New Label
        Dim iSupplierID As Integer
        Dim iAssetType As Integer
        Dim AssetTyp, RefNo As New DataTable
        Dim iCount As Integer
        Dim AssetLen As String
        Dim ilen As Integer : Dim increment As Integer = 0
        Dim bCheck As Boolean
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If GvOPExcel.Rows.Count > 0 Then
                For i = 0 To dttable.Rows.Count - 1
                    objOPExcel.iAFAA_ID = 0
                    lblAssetTransfer = GvOPExcel.Rows(i).FindControl("lblAssetTransfer")

                    If lblAssetTransfer.Text = "LOCAL" Then
                        If lblAssetTransfer.Text <> "" Then
                            objOPExcel.iAFAA_AssetTrType = 1
                        Else
                            objOPExcel.iAFAA_AssetTrType = 0
                        End If
                    ElseIf lblAssetTransfer.Text = "IMPORTED" Then
                        If lblAssetTransfer.Text <> "" Then
                            objOPExcel.iAFAA_AssetTrType = 2
                        End If
                    Else
                        objOPExcel.iAFAA_AssetTrType = 0
                    End If
                    lblCurrencyTypes = GvOPExcel.Rows(i).FindControl("lblCurrencyTypes")

                    Dim iCurnyID As Integer = objOPExcel.LoadCurrencyID(sSession.AccessCode, sSession.AccessCodeID, lblCurrencyTypes.Text)
                    If iCurnyID > 0 Then
                        objOPExcel.iAFAA_CurrencyType = iCurnyID
                    Else
                        objOPExcel.iAFAA_CurrencyType = 0
                    End If
                    lblcurrencyAmount = GvOPExcel.Rows(i).FindControl("lblcurrencyAmount")
                    If lblcurrencyAmount.Text <> "" Then
                        objOPExcel.dAFAA_CurrencyAmnt = lblcurrencyAmount.Text
                    Else
                        objOPExcel.dAFAA_CurrencyAmnt = "0.00"
                    End If
                    If ddlAccZone.SelectedIndex > 0 Then
                        objOPExcel.iAFAA_Zone = ddlAccZone.SelectedValue
                    Else
                        objOPExcel.iAFAA_Zone = 0
                    End If
                    If ddlAccRgn.SelectedIndex > 0 Then
                        objOPExcel.iAFAA_Region = ddlAccRgn.SelectedValue
                    Else
                        objOPExcel.iAFAA_Region = 0
                    End If
                    If ddlAccArea.SelectedIndex > 0 Then
                        objOPExcel.iAFAA_Area = ddlAccArea.SelectedValue
                    Else
                        objOPExcel.iAFAA_Area = 0
                    End If
                    If ddlAccBrnch.SelectedIndex > 0 Then
                        objOPExcel.iAFAA_Branch = ddlAccBrnch.SelectedValue
                    Else
                        objOPExcel.iAFAA_Branch = 0
                    End If
                    lblActualLocation = GvOPExcel.Rows(i).FindControl("lblActualLocation")
                    If lblActualLocation.Text <> "" Then
                        objOPExcel.sAFAA_ActualLocn = lblActualLocation.Text
                    Else
                        objOPExcel.sAFAA_ActualLocn = ""
                    End If
                    lblAssetAge = GvOPExcel.Rows(i).FindControl("lblAssetAge")
                    If lblAssetAge.Text <> "" Then
                        objOPExcel.dAFAA_AssetAge = lblAssetAge.Text
                    Else
                        objOPExcel.dAFAA_AssetAge = "0.00"
                    End If
                    lblTransactionType = GvOPExcel.Rows(i).FindControl("lblTransactionType")
                    If lblTransactionType.Text = "ADDITION" Then
                        objOPExcel.iAFAA_TrType = 1
                    ElseIf lblTransactionType.Text = "TRANSFERS" Then
                        objOPExcel.iAFAA_TrType = 2
                    ElseIf lblTransactionType.Text = "REVALUATION" Then
                        objOPExcel.iAFAA_TrType = 3
                    ElseIf lblTransactionType.Text = "FOREIGN EXCHANGE" Then
                        objOPExcel.iAFAA_TrType = 4
                    Else
                        objOPExcel.iAFAA_TrType = 0
                    End If
                    lblSupplierName = GvOPExcel.Rows(i).FindControl("lblSupplierName")
                    iSupplierID = objOPExcel.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, lblSupplierName.Text)
                    If iSupplierID > 0 Then
                        objOPExcel.iAFAA_SupplierName = iSupplierID
                    Else
                        objOPExcel.iAFAA_SupplierName = 0
                    End If
                    lblsupplierCode = GvOPExcel.Rows(i).FindControl("lblsupplierCode")
                    If lblsupplierCode.Text <> "" Then
                        objOPExcel.iAFAA_SupplierCode = lblsupplierCode.Text
                    Else
                        objOPExcel.iAFAA_SupplierCode = 0
                    End If

                    lblAssetType = GvOPExcel.Rows(i).FindControl("lblAssetType")
                    iAssetType = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text)

                    If iAssetType > 0 Then
                        objOPExcel.sAFAA_AssetType = iAssetType
                    Else
                        objOPExcel.sAFAA_AssetType = ""
                    End If

                    iCount = objOPExcel.GetGLID(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text)
                    If iCount > 0 Then
                        AssetLen = objOPExcel.GetAssetTypeNo(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text)
                        objOPExcel.sAFAA_AssetNo = AssetLen & iCount.ToString()
                    Else
                        AssetLen = objOPExcel.LoadAssetNo(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text)
                        ilen = AssetLen.Length
                        If ilen = 9 Then
                            increment = increment + 1
                            objOPExcel.sAFAA_AssetNo = AssetLen & increment.ToString()
                        End If
                    End If
                    lblAssetRefNo = GvOPExcel.Rows(i).FindControl("lblAssetRefNo")
                    If lblAssetRefNo.Text <> "" Then
                        objOPExcel.sAFAA_AssetRefNo = lblAssetRefNo.Text
                    Else
                        objOPExcel.sAFAA_AssetRefNo = ""
                    End If
                    objOPExcel.sAFAA_DelnType = ""

                    bCheck = objOPExcel.CheckExistorNot(sSession.AccessCode, sSession.AccessCodeID, iAssetType, lblAssetRefNo.Text)
                    If bCheck = True Then
                        objOPExcel.sAFAA_AddnType = "O"
                    Else
                        objOPExcel.sAFAA_AddnType = "N"
                    End If

                    lblDescription = GvOPExcel.Rows(i).FindControl("lblDescription")
                    If lblDescription.Text <> "" Then
                        objOPExcel.sAFAA_Description = lblDescription.Text
                    Else
                        objOPExcel.sAFAA_Description = ""
                    End If
                    lblItemCode = GvOPExcel.Rows(i).FindControl("lblItemCode")
                    If lblItemCode.Text <> "" Then
                        objOPExcel.sAFAA_ItemCode = lblItemCode.Text
                    Else
                        objOPExcel.sAFAA_ItemCode = ""
                    End If
                    lblItemDescription = GvOPExcel.Rows(i).FindControl("lblItemDescription")
                    If lblItemDescription.Text <> "" Then
                        objOPExcel.sAFAA_ItemDescription = lblItemDescription.Text
                    Else
                        objOPExcel.sAFAA_ItemDescription = ""
                    End If
                    lblQuantity = GvOPExcel.Rows(i).FindControl("lblQuantity")
                    If lblQuantity.Text <> "" Then
                        objOPExcel.iAFAA_Quantity = lblQuantity.Text
                    Else
                        objOPExcel.iAFAA_Quantity = 0
                    End If
                    lblDateofPurchase = GvOPExcel.Rows(i).FindControl("lblDateofPurchase")
                    If lblDateofPurchase.Text <> "" Then
                        objOPExcel.dAFAA_PurchaseDate = lblDateofPurchase.Text
                    Else
                        objOPExcel.dAFAA_PurchaseDate = "01/01/1991"
                    End If
                    lblDateOfCommission = GvOPExcel.Rows(i).FindControl("lblDateOfCommission")
                    If lblDateOfCommission.Text <> "" Then
                        objOPExcel.dAFAA_CommissionDate = lblDateOfCommission.Text
                    Else
                        objOPExcel.dAFAA_CommissionDate = "01/01/1991"
                    End If
                    lblAmount = GvOPExcel.Rows(i).FindControl("lblAmount")
                    If lblAmount.Text <> "" Then
                        objOPExcel.dAFAA_AssetAmount = lblAmount.Text
                    Else
                        objOPExcel.dAFAA_AssetAmount = "0.00"
                    End If
                    lblDeprcn = GvOPExcel.Rows(i).FindControl("lblDeprcn")
                    If lblDeprcn.Text <> "" Then
                        objOPExcel.dAFAA_Depreciation = lblDeprcn.Text
                    Else
                        objOPExcel.dAFAA_Depreciation = "0.00"
                    End If
                    objOPExcel.iAFAA_AssetDelID = 0
                    objOPExcel.dAFAA_AssetDelDate = Nothing
                    objOPExcel.dAFAA_AssetDeletionDate = Nothing
                    objOPExcel.dAFAA_Assetvalue = "0.00"
                    objOPExcel.sAFAA_AssetDesc = ""
                    objOPExcel.iAFAA_CreatedBy = sSession.UserID
                    objOPExcel.dAFAA_CreatedOn = DateTime.Today
                    objOPExcel.iAFAA_UpdatedBy = sSession.UserID
                    objOPExcel.dAFAA_UpdatedOn = DateTime.Today
                    objOPExcel.sAFAA_Delflag = "W"
                    objOPExcel.sAFAA_Status = "C"
                    objOPExcel.sAFAA_Operation = "U"
                    objOPExcel.sAFAA_IPAddress = sSession.IPAddress
                    objOPExcel.iAFAA_YearID = sSession.YearID
                    objOPExcel.iAFAA_CompID = sSession.AccessCodeID

                    Arr = objOPExcel.SaveFixedAssetAddition(sSession.AccessCode, sSession.AccessCodeID, objOPExcel)
                Next
            Else
                lblError.Text = "No Data"
                Exit Sub
            End If
            If Arr(0) = "2" Then
                lblFXOPBalExcelMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDOpExcel').modal('show');", True)
            ElseIf Arr(0) = "3" Then
                lblFXOPBalExcelMsg.Text = "Successfully Saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDOpExcel').modal('show');", True)
            End If
            imgbtnRefresh_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtUpload_Click")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            FULoad.Dispose() : txtPath.Text = "" : ddlSheetName.SelectedIndex = 0 : GvOPExcel.DataSource = Nothing : GvOPExcel.DataBind()
            ddlAccZone.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccArea.SelectedIndex = 0 : ddlAccBrnch.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub
    Private Sub ImgBtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/FixedAsset/AssetAdditionDashBoard.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnBack_Click")
        End Try
    End Sub
End Class
