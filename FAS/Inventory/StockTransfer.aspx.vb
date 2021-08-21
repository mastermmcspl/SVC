
Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports DatabaseLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing
Partial Class Purchase_Purchase_Return
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Orders/PurchaseOrder"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objPO As New clsPurchaseOrder
    Dim objGin As New ClsGoodsInward
    Dim objInvD As New clsInvenotryDetails
    Dim objClsFASGnrl As New clsFASGeneral
    Private objclsModulePermission As New clsModulePermission
    Dim objGnrlFnction As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails
    Dim objPreturn As New clsPurchaseReturn
    Dim objStockTrnsfr As New clsStockTransfar
    Dim ObjDB As New DBHelper
    Private Shared sIKBBackStatus As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnPrint.ImageUrl = "~/Images/Download24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "STOT")
                imgbtnAdd.Visible = False : imgRefresh.Visible = False : imgbtnWaiting.Visible = False : imgbtnPrint.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnPrint.Visible = True
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgRefresh.Visible = True
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnWaiting.Visible = True
                    End If
                End If

                '  CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                lblDescID.Text = "0"
                txtMasterID.Text = "" : txtHistoryID.Text = ""
                Me.ddlCommodity.Attributes.Add("onclick", "return ValidateCommodity()")
                Me.txtRate.Attributes.Add("onblur", "return ValidateRate()")
                Me.txtQuantity.Attributes.Add("onclick", "return ValidateddlUnit()")
                Me.txtQuantity.Attributes.Add("onblur", "return Calculate()")
                ' Me.txtDiscount.Attributes.Add("onblur", "return CalculateDiscount()")
                '    Me.btnSave.Attributes.Add("onclick", "return ValidatePurcahseOrder()")
                '   Me.btnPrint.Attributes.Add("onclick", "return ValidatePrint()")
                ' Me.btnSave.Attributes.Add("onclick", "return ValidateNarration()")
                LoadExistingPurchaseOrder()
                LoadCommodity()
                'LoadSuppliers()
                loadDescitionStart()
                lblDescID.Visible = False
                GenerateOrderCodeAnddate()
                ' lblCrBy.Text = sSession.UserFullName & "<B>" & "   On   " & "</B>" & clsTRACeGeneral.FormatDtForRDBMS(System.DateTime.Now, "D")
                'If sSession.ManOrTrader = "1" Then
                '    lblCompany.Text = clsSupplierMaster.GetCustomers(sSession.AccessCode, sSession.AccessCodeID) & " - " & "Manufacturer"
                'Else
                '    lblCompany.Text = clsSupplierMaster.GetCustomers(sSession.AccessCode, sSession.AccessCodeID) & " - " & "Trader"
                'End If
                LoadBranches()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadBranches()
        Try
            ddlBname.DataSource = objStockTrnsfr.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
            ddlBname.DataTextField = "CUSTB_Name"
            ddlBname.DataValueField = "CUSTB_ID"
            ddlBname.DataBind()
            ddlBname.Items.Insert(0, "--- Select Branch Name ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDeliverySchdule()
        Try
            ddlBname.DataSource = objStockTrnsfr.LoadDeliverySchdule(sSession.AccessCode, sSession.AccessCodeID)
            ddlBname.DataTextField = "Mas_desc"
            ddlBname.DataValueField = "Mas_id"
            ddlBname.DataBind()
            ddlBname.Items.Insert(0, "--- Select Mode of Shipping ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCodeAnddate()
        Try
            txtOrderCode.Text = objStockTrnsfr.GeneratePurchaseOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            txtTransferDate.Text = objClsFASGnrl.FormatDtForRDBMS(System.DateTime.Now, "D")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objPO.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "--- Select Commodity ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingPurchaseOrder()
        Try
            ddlExistingNote.DataSource = objStockTrnsfr.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingNote.DataTextField = "STM_OutwardNo"
            ddlExistingNote.DataValueField = "STM_ID"
            ddlExistingNote.DataBind()
            ddlExistingNote.Items.Insert(0, "--- Existing Purchase Order ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadDescitionStart()
        Try
            chkCategory.DataSource = objStockTrnsfr.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            chkCategory.DataTextField = "Inv_Code"
            chkCategory.DataValueField = "Inv_ID"
            chkCategory.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            ddlRate.Enabled = False : txtRate.Enabled = False
            If ddlCommodity.SelectedIndex > 0 Then
                chkCategory.DataSource = objStockTrnsfr.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
                chkCategory.DataTextField = "Inv_Code"
                chkCategory.DataValueField = "Inv_ID"
                chkCategory.DataBind()
            Else
                loadDescitionStart()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Private Function GetPurchaseDetails(ByVal iHistoryId As Integer) As Object
        Dim dt As New DataTable
        Try
            dt = objStockTrnsfr.GetPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then

            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Sub ddlExistingOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingNote.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlExistingNote.SelectedIndex > 0 Then
                ddlCommodity.SelectedIndex = 0
                chkCategory.Items.Clear()
                ClearAll()
                dgPurchase.DataSource = objStockTrnsfr.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingNote.SelectedValue)
                dgPurchase.DataBind()
                lblError.Text = ""
                dt = objStockTrnsfr.LoadPurchaseOderMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingNote.SelectedValue)
                If dt.Rows.Count > 0 Then

                    If IsDBNull(dt.Rows(0)("STM_OutwardNo").ToString()) = False Then
                        txtOrderCode.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("STM_OutwardNo").ToString())
                    Else
                        txtOrderCode.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("STM_FormDate").ToString()) = False Then
                        txtTransferDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("STM_FormDate").ToString(), "D")
                    Else
                        txtTransferDate.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("STM_Branch").ToString()) = False And dt.Rows(0)("STM_Branch").ToString() <> "0" Then
                        ddlBname.SelectedValue = dt.Rows(0)("STM_Branch").ToString()
                        'txtAdress.Text = clsStockTransfar.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, ddlBname.SelectedValue)
                        'txtPhone.Text =
                        LoadOrderDetails(dt.Rows(0)("STM_Branch"))
                    Else
                        '  ddlSupplier.SelectedIndex = 0
                    End If

                    'If IsDBNull(dt.Rows(0)("POM_ModeOfShipping").ToString()) = False And dt.Rows(0)("POM_ModeOfShipping").ToString() <> "0" Then
                    '    ddlVatClsfctn.SelectedValue = dt.Rows(0)("POM_ModeOfShipping").ToString()
                    'Else
                    '    ddlVatClsfctn.SelectedIndex = 0
                    'End If

                    If IsDBNull(dt.Rows(0)("STM_Narration").ToString()) = False Then
                        txtNaretion.Text = dt.Rows(0)("STM_Narration").ToString()
                    Else
                        txtNaretion.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("STM_FormNo").ToString()) = False Then
                        txtFormNumber.Text = dt.Rows(0)("STM_FormNo").ToString()
                    Else
                        txtFormNumber.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("STM_SeriesNo").ToString()) = False Then
                        txtReferenceNo.Text = dt.Rows(0)("STM_SeriesNo").ToString()
                    Else
                        txtReferenceNo.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("STM_FormReceive").ToString()) = False Then
                        ddlFToReceive.SelectedValue = dt.Rows(0)("STM_FormReceive").ToString()
                    Else
                        ddlFToReceive.SelectedValue = 1
                    End If

                    If IsDBNull(dt.Rows(0)("STM_ID").ToString()) = False Then
                        txtMasterID.Text = dt.Rows(0)("STM_ID").ToString()
                    Else
                        txtMasterID.Text = 0
                    End If
                    'If IsDBNull(dt.Rows(0)("STM_Status").ToString()) = False Then
                    '    If (dt.Rows(0)("STM_Status") = "W") Then
                    '        lblStatus.Text = "Waiting For approval"
                    '        'btnSave.Enabled = False
                    '        'btnApprove.Enabled = True
                    '    ElseIf dt.Rows(0)("STM_Status") = "A" Then
                    '        lblStatus.Text = "Approved."
                    '        'btnSave.Enabled = False
                    '        'btnApprove.Enabled = False
                    '    Else
                    '        'btnSave.Enabled = False
                    '        'btnApprove.Enabled = False
                    '    End If
                    'End If

                End If
            Else
                txtOrderCode.Text = "" : txtTransferDate.Text = "" : txtAdress.Text = "" : ddlBname.SelectedIndex = 0
                'ddlVatClsfctn.SelectedIndex = 0
                txtMasterID.Text = 0
                GenerateOrderCodeAnddate()
                'btnNew_Click(sender, e)
                dgPurchase.DataSource = dt
                dgPurchase.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingOrder_SelectedIndexChanged")
        End Try
    End Sub
    'Protected Sub dgPurchase_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPurchase.ItemCommand
    '    Dim lnkDescription As New LinkButton
    '    Dim dt As New DataTable
    '    Try
    '        lblError.Text = ""
    '        If e.CommandName = "Delete" Then
    '            objStockTrnsfr.DeleteOrderValues(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text, e.Item.Cells(2).Text)
    '            dgPurchase.DataSource = objStockTrnsfr.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
    '            dgPurchase.DataBind()
    '            If (dgPurchase.Rows.Count = 0) Then
    '                objStockTrnsfr.DeleteOrderValuesFromMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text)
    '            End If
    '            LoadExistingPurchaseOrder()
    '        End If
    '        If e.CommandName = "Select" Then
    '            lnkDescription = e.Item.FindControl("Goods")
    '            dt = clsStockTransfar.LoadPurchaseOderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text, e.Item.Cells(1).Text, e.Item.Cells(2).Text, e.Item.Cells(3).Text)
    '            If dt.Rows.Count > 0 Then
    '                If IsDBNull(dt.Rows(0)("STD_CommodityID").ToString()) = False Then
    '                    ddlCommodity.SelectedValue = dt.Rows(0)("STD_CommodityID").ToString()
    '                    chkCategory.DataSource = clsStockTransfar.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
    '                    chkCategory.DataTextField = "Inv_Code"
    '                    chkCategory.DataValueField = "Inv_ID"
    '                    chkCategory.DataBind()

    '                    chkCategory.SelectedValue = dt.Rows(0)("STD_DescriptionID").ToString()
    '                    lblDescID.Text = dt.Rows(0)("STD_DescriptionID").ToString()
    '                    LoadDesciptionDetails()
    '                Else
    '                    ddlCommodity.SelectedIndex = 0
    '                End If

    '                If IsDBNull(dt.Rows(0)("STD_HisotryID").ToString()) = False Then
    '                    txtHistoryID.Text = dt.Rows(0)("STD_HisotryID").ToString()
    '                Else
    '                    txtHistoryID.Text = "0"
    '                End If

    '                If IsDBNull(dt.Rows(0)("STD_Per").ToString()) = False Then
    '                    ddlUnit.SelectedValue = dt.Rows(0)("STD_Per").ToString()
    '                Else
    '                    ddlUnit.SelectedIndex = 0
    '                End If

    '                If IsDBNull(dt.Rows(0)("STD_Rate").ToString()) = False Then
    '                    txtRate.Text = dt.Rows(0)("STD_Rate").ToString()
    '                Else
    '                    txtRate.Text = ""
    '                End If

    '                If IsDBNull(dt.Rows(0)("STD_NetAmount").ToString()) = False Then
    '                    txtRateAmount.Text = dt.Rows(0)("STD_NetAmount").ToString()
    '                Else
    '                    txtRateAmount.Text = ""
    '                End If

    '                If IsDBNull(dt.Rows(0)("STD_Quantity").ToString()) = False Then
    '                    txtQuantity.Text = dt.Rows(0)("STD_Quantity").ToString()
    '                Else
    '                    txtQuantity.Text = ""
    '                End If
    '                'If IsDBNull(dt.Rows(0)("STD_NetAmount").ToString()) = False Then
    '                '    txtTotalAmount.Text = dt.Rows(0)("STD_NetAmount").ToString()
    '                'Else
    '                '    txtTotalAmount.Text = ""
    '                'End If


    '                'If IsDBNull(dt.Rows(0)("STD_Narration").ToString()) = False Then
    '                '    txtNaretion.Text = dt.Rows(0)("STD_Narration").ToString()
    '                'Else
    '                '    txtNaretion.Text = ""
    '                'End If

    '                'If IsDBNull(dt.Rows(0)("STD_FormNo").ToString()) = False Then
    '                '    txtFormNumber.Text = dt.Rows(0)("STD_FormNo").ToString()
    '                'Else
    '                '    txtFormNumber.Text = ""
    '                'End If

    '                'If IsDBNull(dt.Rows(0)("STD_FormReceive").ToString()) = False Then
    '                '    ddlFToReceive.SelectedValue = dt.Rows(0)("STD_FormReceive").ToString()
    '                'Else
    '                '    ddlFToReceive.SelectedValue = 1
    '                'End If


    '                'If IsDBNull(dt.Rows(0)("STD_SeriesNo").ToString()) = False Then
    '                '    txtReferenceNo.Text = dt.Rows(0)("STD_SeriesNo").ToString()
    '                'Else
    '                '    txtReferenceNo.Text = ""
    '                'End If
    '            End If

    '            Dim sStatus As String = ""
    '            'sStatus = clsStockTransfar.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingNote.SelectedValue)
    '            'If sStatus = "A" Then
    '            '    'btnSave.Visible = True : btnSave.Enabled = False
    '            '    'btnApprove.Visible = True : btnApprove.Enabled = False
    '            'Else
    '            '    'btnSave.Visible = True : btnSave.Enabled = True
    '            '    'btnApprove.Visible = True : btnApprove.Enabled = True
    '            'End If
    '        End If
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingOrder_SelectedIndexChanged")
    '    End Try
    'End Sub
    'Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
    '    'Try
    '    '    lblErrorUp.Text = "" : lblStatus.Text = ""
    '    '    ClearAll()
    '    '    ddlCommodity.SelectedIndex = 0
    '    '    ddlUnit.Items.Clear()
    '    '    chkCategory.Items.Clear()
    '    '    dgPurchase.DataSource = Nothing
    '    '    dgPurchase.DataBind()
    '    '    GenerateOrderCodeAnddate()
    '    '    ddlRate.Items.Clear()
    '    '    ' ddlSupplier.SelectedIndex = 0 : txtAdress.Text = ""
    '    '    ' ddlVatClsfctn.SelectedIndex = 0
    '    '    txtSearchItem.Text = ""
    '    '    LoadExistingPurchaseOrder()
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNew_Click")
    '    End Try
    'End Sub
    Public Sub ClearAll()
        Try
            For i = 0 To chkCategory.Items.Count - 1
                chkCategory.Items(i).Selected = False
            Next
            ddlUnit.Items.Clear()
            txtRate.Text = ""
            txtQuantity.Text = "" : txtRateAmount.Text = ""
            'txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            'txtExcise.Text = "" : txtExciseAmount.Text = ""
            'txtVat.Text = "" : txtVatAmount.Text = ""
            'txtCST.Text = "" : txtCSTAmount.Text = ""
            ' txtRequiredDate.Text = ""  ' : txtTotalAmount.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Protected Sub ddlSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSupplier.SelectedIndexChanged
    '    Try
    '        'If ddlSupplier.SelectedIndex > 0 Then
    '        '    txtAdress.Text = clsStockTransfar.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
    '        '    txtQuantity.Text = ""
    '        '    txtDiscount.Text = ""
    '        '    lblErrorUp.Text = ""
    '        'Else
    '        '    txtAdress.Text = ""
    '        'End If
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSupplier_SelectedIndexChanged")
    '    End Try
    'End Sub

    Private Sub LoadDesciptionDetails()
        Dim dt As New DataTable
        Dim sArray As Array
        Try
            lblError.Text = ""
            ddlRate.DataSource = dt
            ddlRate.DataBind()
            txtRate.Text = "" : txtRateAmount.Text = ""
            txtQuantity.Text = "" ' : txtDiscount.Text = ""
            ': txtDiscountAmount.Text = ""
            'txtExcise.Text = "" : txtExciseAmount.Text = ""
            'txtVat.Text = "" : txtVatAmount.Text = "" : txtCST.Text = "" : txtCSTAmount.Text = ""
            'txtTotalAmount.Text = ""

            If lblDescID.Text <> "0" Then
                dt = objStockTrnsfr.CheckDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "Enter Details in Inventory Master Details"
                    Exit Sub
                End If
                If dt.Rows.Count > 1 Then
                    ddlRate.DataSource = dt
                    ddlRate.DataTextField = "INVH_PreDeterminedPrice"
                    ddlRate.DataValueField = "InvH_ID"
                    ddlRate.DataBind()
                    ddlRate.Enabled = True : txtRate.Enabled = False
                    txtHistoryID.Text = ddlRate.SelectedValue
                    sArray = ddlRate.SelectedItem.Text.Split("-")
                    txtRate.Text = sArray(0)
                    'GetOtherDetails(txtHistoryID.Text)
                Else
                    sArray = dt.Rows(0)(1).ToString().Split("-")
                    txtRate.Text = sArray(0)
                    txtHistoryID.Text = dt.Rows(0)(0).ToString()
                    ddlRate.Enabled = False : txtRate.Enabled = True
                    If txtHistoryID.Text <> "" Then
                        GetPurchaseDetails(txtHistoryID.Text)
                        ' GetOtherDetails(txtHistoryID.Text)
                    End If
                End If

                ddlUnit.DataSource = objStockTrnsfr.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                ddlUnit.DataTextField = "Mas_Desc"
                ddlUnit.DataValueField = "Mas_ID"
                ddlUnit.DataBind()
                ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
        Dim iPices As Integer
        Dim availQty As Integer
        Try
            If (chkCategory.SelectedValue > 0) Then
                ddlCommodity.SelectedValue = ObjDB.SQLGetDescription(sSession.AccessCode, "select Inv_Parent from inventory_master where Inv_ID='" & chkCategory.SelectedValue & "'")
            End If

            lblDescID.Text = chkCategory.SelectedValue
            LoadDesciptionDetails()
            iPices = ObjDB.SQLExecuteScalarInt(sSession.AccessCode, "Select INVH_PerPieces From Inventory_master_History Where InvH_ID ='" & txtHistoryID.Text & "' And INVH_CompID=" & sSession.AccessCodeID & " ")
            txtPices.Text = iPices
            hfTotalPieces.Value = txtPices.Text
            availQty = ObjDB.SQLExecuteScalarInt(sSession.AccessCode, "Select sum(SL_ClosingBalanceQty) From Stock_Ledger Where SL_ItemID ='" & chkCategory.SelectedValue & "' And SL_CompID=" & sSession.AccessCodeID & " group by SL_ItemID")
            txtavailQty.Text = availQty
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnit.SelectedIndexChanged
        Dim iPices As Integer
        Dim altUnit As Integer
        Dim total As Decimal
        Try
            'btnSave.Visible = True : btnSave.Enabled = True
            If ddlUnit.SelectedIndex > 0 Then
                If (txtQuantity.Text <> "" And txtRate.Text <> "") Then
                    iPices = ObjDB.SQLExecuteScalarInt(sSession.AccessCode, "Select INVH_PerPieces From Inventory_master_History Where InvH_ID ='" & txtHistoryID.Text & "' And INVH_CompID=" & sSession.AccessCodeID & " and InvH_YearID = " & sSession.YearID & "")
                    altUnit = ObjDB.SQLExecuteScalarInt(sSession.AccessCode, "Select InvH_AlterUnit From Inventory_master_History Where InvH_ID ='" & txtHistoryID.Text & "' And INVH_CompID=" & sSession.AccessCodeID & " and InvH_YearID = " & sSession.YearID & " ")
                    txtPices.Text = iPices
                    If (ddlUnit.SelectedValue = altUnit) Then
                        total = ((Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(iPices)) * Convert.ToDecimal(txtRate.Text))
                        txtRateAmount.Text = total
                        hfRateAmount.Value = total
                        'If (txtExcise.Text <> "") Then
                        '    txtExciseAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtExcise.Text) / 100))
                        '    hfExciseAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtExcise.Text) / 100))
                        'End If
                        'txtCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtCST.Text) / 100))
                        'hfCSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtCST.Text) / 100))
                        'txtVatAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtVat.Text) / 100))
                        'hfVatAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtVat.Text) / 100))

                        'If txtDiscount.Text <> "" Then
                        '    txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtDiscount.Text) / 100))
                        '    hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtDiscount.Text) / 100))
                        '    txtTotalAmount.Text = String.Format("{0:0.00}", ((Convert.ToDecimal((total * txtExcise.Text) / 100) + Convert.ToDecimal((total * txtCST.Text) / 100) + total + Convert.ToDecimal((total * txtVat.Text) / 100)) - (Convert.ToDecimal((total * txtDiscount.Text) / 100))))
                        '    hfTotalAmount.Value = String.Format("{0:0.00}", ((Convert.ToDecimal((total * txtExcise.Text) / 100) + Convert.ToDecimal((total * txtCST.Text) / 100) + total + Convert.ToDecimal((total * txtVat.Text) / 100)) - (Convert.ToDecimal((total * txtDiscount.Text) / 100))))
                        'Else
                        '    txtDiscountAmount.Text = ""
                        '    hfDiscountAmount.Value = ""
                        '    txtTotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtExcise.Text) / 100) + Convert.ToDecimal((total * txtCST.Text) / 100) + total + Convert.ToDecimal((total * txtVat.Text) / 100))
                        '    hfTotalAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtExcise.Text) / 100) + Convert.ToDecimal((total * txtCST.Text) / 100) + total + Convert.ToDecimal((total * txtVat.Text) / 100))
                        'End If


                    Else
                        total = (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))
                        txtRateAmount.Text = total
                        hfRateAmount.Value = total

                        'txtExciseAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtExcise.Text) / 100))
                        'hfExciseAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtExcise.Text) / 100))
                        'txtCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtCST.Text) / 100))
                        'hfCSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtCST.Text) / 100))
                        'txtVatAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtVat.Text) / 100))
                        'hfVatAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtVat.Text) / 100))

                        'If txtDiscount.Text <> "" Then
                        '    txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtDiscount.Text) / 100))
                        '    hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtDiscount.Text) / 100))
                        '    txtTotalAmount.Text = String.Format("{0:0.00}", ((Convert.ToDecimal((total * txtExcise.Text) / 100) + Convert.ToDecimal((total * txtCST.Text) / 100) + total + Convert.ToDecimal((total * txtVat.Text) / 100)) - (Convert.ToDecimal((total * txtDiscount.Text) / 100))))
                        '    hfTotalAmount.Value = String.Format("{0:0.00}", ((Convert.ToDecimal((total * txtExcise.Text) / 100) + Convert.ToDecimal((total * txtCST.Text) / 100) + total + Convert.ToDecimal((total * txtVat.Text) / 100)) - (Convert.ToDecimal((total * txtDiscount.Text) / 100))))
                        'Else
                        '    txtDiscountAmount.Text = ""
                        '    hfDiscountAmount.Value = ""
                        '    txtTotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((total * txtExcise.Text) / 100) + Convert.ToDecimal((total * txtCST.Text) / 100) + total + Convert.ToDecimal((total * txtVat.Text) / 100))
                        '    hfTotalAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((total * txtExcise.Text) / 100) + Convert.ToDecimal((total * txtCST.Text) / 100) + total + Convert.ToDecimal((total * txtVat.Text) / 100))
                        'End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlUnit_SelectedIndexChanged")
        End Try
    End Sub

    'Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
    'Dim sStatus As String = ""
    'Dim ibtnCancel As New ImageButton
    'Try
    '    lblErrorUp.Text = ""
    '    If dgPurchase.Items.Count > 0 Then
    '        clsStockTransfar.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, txtOrderCode.Text)
    '        lblErrorUp.Text = "Approved Successfully."
    '        sStatus = clsStockTransfar.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtMasterID.Text)
    '        If sStatus = "W" Then
    '            lblStatus.Text = "Waiting For approve."
    '        ElseIf sStatus = "A" Then
    '            lblStatus.Text = "Approved."
    '        End If

    '        For i = 0 To dgPurchase.Items.Count - 1
    '            ibtnCancel = dgPurchase.Items(i).Cells(17).FindControl("imgDelete")
    '            ibtnCancel.Enabled = False
    '        Next

    '        'btnSave.Enabled = False : btnApprove.Enabled = False
    '        'btnSave.Visible = True : btnApprove.Visible = True
    '    Else
    '        lblErrorUp.Text = "Add Items."
    '        chkCategory.Focus()
    '        'btnSave.Enabled = True : btnApprove.Enabled = False
    '        'btnSave.Visible = True
    '        Exit Sub
    '    End If
    'Catch ex As Exception
    '    lblErrorUp.Text = ex.Message
    '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnApprove_Click")
    'End Try
    ' End Sub
    'Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    '    Try
    '        lblErrorUp.Text = ""
    '        Response.Redirect("~/Reports/Viewer/RvStockTransfar.aspx?ExistingNote=" & ddlExistingNote.SelectedValue)
    '        ' Response.Redirect("~/Reports/Viewer/RvStockTransfar.aspx")
    '        ' Response.Redirect("~/Reports/Viewer/RvPurchaseOrder.aspx")
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPrint_Click")
    '    End Try
    'End Sub
    'Protected Sub dgPurchase_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPurchase.ItemDataBound
    '    Dim ibtnCancel As New ImageButton
    '    Dim sStatus As String = ""
    '    Dim iOrderID As Integer
    '    Try
    '        If (e.Item.ItemType <> ListItemType.Header) And (e.Item.ItemType <> ListItemType.Footer) Then
    '            ibtnCancel = e.Item.FindControl("imgDelete")
    '            If ddlExistingNote.SelectedIndex > 0 Then
    '                iOrderID = ddlExistingNote.SelectedValue
    '            Else
    '                iOrderID = txtMasterID.Text
    '            End If
    '            sStatus = clsStockTransfar.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
    '            If sStatus = "A" Then
    '                ibtnCancel.Enabled = False
    '            Else
    '                ibtnCancel.Attributes.Add("OnClick", "javascript:return ValidateCancel()")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_ItemDataBound")
    '    End Try
    'End Sub
    'Protected Sub btnPrintHR_Click(sender As Object, e As EventArgs) Handles btnPrintHR.Click
    '    Try
    '        lblErrorUp.Text = ""
    '        If ddlExistingNote.SelectedIndex > 0 Then
    '            Response.Redirect("~/Reports/Viewer/PurchaseOrderHR.aspx?ExistingOrder=" & ddlExistingNote.SelectedValue)
    '        End If
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPrintHR_Click")
    '    End Try
    'End Sub
    Protected Sub ddlDSchedule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBname.SelectedIndexChanged
        Try
            LoadOrderDetails(ddlBname.SelectedValue)
        Catch ex As Exception
            Throw
        End Try
    End Sub


    Public Function StockLedgerData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal OrderID As Integer, ByVal iFIFO As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtFromTable As New DataTable
        Dim qty As Decimal = 0 : Dim Updateqty As Decimal = 0
        Dim str As String
        Try
            dtFromTable = DataFromStockTransfer(sNameSpace, iCompID, iYearID, OrderID)
            For i = 0 To dtFromTable.Rows.Count - 1
                qty = dtFromTable.Rows(i)("STD_Quantity")
                sSql = "select * from stock_ledger where SL_ItemID=" & dtFromTable.Rows(i)("STD_DescriptionID") & " and SL_historyID=" & dtFromTable.Rows(i)("STD_HisotryID") & " And SL_CompID=" & iCompID & "  order by SL_ID"
                dt = ObjDB.SQLExecuteDataTable(sNameSpace, sSql)
                For j = 0 To dt.Rows.Count - 1
                    If (dt.Rows(i)("SL_ClosingBalanceQty") > qty) Then
                        Updateqty = dt.Rows(i)("SL_ClosingBalanceQty") - qty
                        str = UpdateEditedData(sNameSpace, iCompID, dt.Rows(i)("SL_ID"), Updateqty)
                        Exit Function
                    ElseIf (dt.Rows(i)("SL_ClosingBalanceQty") < qty) Then
                        qty = qty - dt.Rows(i)("SL_ClosingBalanceQty")
                        Updateqty = 0
                        str = UpdateEditedData(sNameSpace, iCompID, dt.Rows(i)("SL_ID"), Updateqty)
                    Else
                        Updateqty = 0
                        str = UpdateEditedData(sNameSpace, iCompID, dt.Rows(i)("SL_ID"), Updateqty)
                        Exit Function
                    End If
                Next
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DataFromStockTransfer(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Stock_transfer_Details Where STD_MasterID =" & OrderID & " And STD_CompID=" & iCompID & ""
            dt = ObjDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try

    End Function


    Private Sub LoadOrderDetails(ByVal BranchID As Integer)
        Dim dtable As New DataTable
        Try
            dtable = objStockTrnsfr.BranchDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, BranchID)
            If (dtable.Rows.Count > 0) Then
                For i = 0 To dtable.Rows.Count - 1
                    'lblMShiping.Text = clsTRACeGeneral.ReplaceSafeSQL(dtable.Rows(i)("POM_ModeOfShipping"))
                    txtAdress.Text = objClsFASGnrl.ReplaceSafeSQL(dtable.Rows(i)("CUSTB_ADDRESS"))
                    txtCPerson.Text = objClsFASGnrl.ReplaceSafeSQL(dtable.Rows(i)("CUSTB_ContactPerson"))
                    txtPhone.Text = objClsFASGnrl.ReplaceSafeSQL(dtable.Rows(i)("CUSTB_TEL"))
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Dim iMasterID As Integer = 0
    '    Dim dOrderDate As Date
    '    Dim dRequiredDate As Date
    '    Try
    '        lblErrorUp.Text = ""
    '        objPO.STM_FormDate = Date.ParseExact(Trim(txtTransferDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objPO.STM_OutwardNo = txtOrderCode.Text
    '        objPO.STM_VATClass = 0 ' ddlVatClsfctn.SelectedValue
    '        objPO.STM_CreatedBy = sSession.UserID
    '        objPO.STM_YearID = sSession.YearID
    '        objPO.STM_RefNo = txtReferenceNo.Text
    '        objPO.STM_FormReceive = ddlFToReceive.SelectedValue
    '        objPO.STM_FormNo = txtFormNumber.Text
    '        objPO.STM_Narration = txtNaretion.Text
    '        If (ddlBname.SelectedIndex > 0) Then
    '            objPO.STM_Branch = ddlBname.SelectedValue
    '        Else
    '            objPO.STM_Branch = 0
    '        End If
    '        objPO.STM_Status = "W"
    '        iMasterID = clsStockTransfar.SavePurchaseOrder(sSession.AccessCode, sSession.AccessCodeID, dOrderDate, objPO)
    '        txtMasterID.Text = iMasterID
    '        objPO.STD_MasterID = iMasterID
    '        objPO.STD_CommodityID = ddlCommodity.SelectedValue
    '        objPO.STD_DescriptionID = lblDescID.Text
    '        objPO.STD_HisotryID = txtHistoryID.Text
    '        objPO.STD_Per = ddlUnit.SelectedValue
    '        objPO.STD_Rate = Trim(txtRate.Text)

    '        If hfRateAmount.Value <> "" Then
    '            objPO.STD_NetAmount = Request.Form(hfRateAmount.UniqueID)
    '        Else
    '            objPO.STD_NetAmount = clsTRACeGeneral.SafeSQL(txtRateAmount.Text)
    '        End If

    '        If txtQuantity.Text = "" Then
    '            objPO.STD_Quantity = "0"
    '        Else
    '            objPO.STD_Quantity = clsTRACeGeneral.SafeSQL(txtQuantity.Text)
    '        End If
    '        Dim sStatus As String = ""
    '        clsStockTransfar.SaveStockTransfarDetails(sSession.AccessCode, sSession.AccessCodeID, dRequiredDate, objPO)
    '        dgPurchase.DataSource = clsStockTransfar.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
    '        dgPurchase.DataBind()
    '        lblErrorUp.Text = "Successfully Saved"
    '        lblStatus.Text = "Waiting For Approval"
    '        LoadExistingPurchaseOrder()
    '        ddlExistingNote.SelectedValue = iMasterID
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    '  End Sub
    'Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
    '    Dim dt As New DataTable
    '    Try
    '        dt = StockLedgerData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlExistingNote.SelectedValue, 1)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Function UpdateEditedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal id As Integer, ByVal qty As Decimal) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Try
            sSql = "Update stock_ledger Set SL_ClosingBalanceQty=" & qty & " Where SL_CompID=" & iCompID & " And SL_ID = " & id & ""
            ObjDB.SQLExecuteNonQuery(sNameSpace, sSql)
            sStr = "Moved Successfully"
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim iMasterID As Integer = 0
        Dim dOrderDate As Date
        Dim dRequiredDate As Date
        Try
            lblError.Text = ""
            objStockTrnsfr.STM_FormDate = Date.ParseExact(Trim(txtTransferDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objStockTrnsfr.STM_OutwardNo = txtOrderCode.Text
            objStockTrnsfr.STM_VATClass = 0 ' ddlVatClsfctn.SelectedValue
            objStockTrnsfr.STM_CreatedBy = sSession.UserID
            objStockTrnsfr.STM_YearID = sSession.YearID
            objStockTrnsfr.STM_RefNo = txtReferenceNo.Text
            objStockTrnsfr.STM_FormReceive = ddlFToReceive.SelectedValue
            objStockTrnsfr.STM_FormNo = txtFormNumber.Text
            objStockTrnsfr.STM_Narration = txtNaretion.Text
            If (ddlBname.SelectedIndex > 0) Then
                objStockTrnsfr.STM_Branch = ddlBname.SelectedValue
            Else
                objStockTrnsfr.STM_Branch = 0
            End If
            objStockTrnsfr.STM_Status = "W"
            iMasterID = objStockTrnsfr.SavePurchaseOrder(sSession.AccessCode, sSession.AccessCodeID, dOrderDate, objStockTrnsfr)
            txtMasterID.Text = iMasterID
            objStockTrnsfr.STD_MasterID = iMasterID
            objStockTrnsfr.STD_CommodityID = ddlCommodity.SelectedValue
            objStockTrnsfr.STD_DescriptionID = lblDescID.Text
            objStockTrnsfr.STD_HisotryID = txtHistoryID.Text
            objStockTrnsfr.STD_Per = ddlUnit.SelectedValue
            objStockTrnsfr.STD_Rate = Trim(txtRate.Text)

            If hfRateAmount.Value <> "" Then
                objStockTrnsfr.STD_NetAmount = Request.Form(hfRateAmount.UniqueID)
            Else
                objStockTrnsfr.STD_NetAmount = objClsFASGnrl.SafeSQL(txtRateAmount.Text)
            End If

            If txtQuantity.Text = "" Then
                objStockTrnsfr.STD_Quantity = "0"
            Else
                objStockTrnsfr.STD_Quantity = objClsFASGnrl.SafeSQL(txtQuantity.Text)
            End If
            Dim sStatus As String = ""
            objStockTrnsfr.SaveStockTransfarDetails(sSession.AccessCode, sSession.AccessCodeID, dRequiredDate, objStockTrnsfr)
            dgPurchase.DataSource = objStockTrnsfr.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
            dgPurchase.DataBind()
            lblError.Text = "Successfully Saved"
            lblStatus.Text = "Waiting For Approval"
            LoadExistingPurchaseOrder()
            ddlExistingNote.SelectedValue = iMasterID
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim dt As New DataTable
        Try
            If (ddlExistingNote.SelectedIndex > 0) Then
                dt = StockLedgerData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlExistingNote.SelectedValue, 1)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
End Class
