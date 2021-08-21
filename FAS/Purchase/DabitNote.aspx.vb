
Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports DatabaseLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing
Partial Class Purchase_DabitNote
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Orders/PurchaseOrder"
    Dim sSession As New AllSession
    Dim objPO As New clsPurchaseOrder
    Dim objGin As New ClsGoodsInward
    Dim objInvD As New clsInvenotryDetails
    Dim objClsFASGnrl As New clsFASGeneral
    Dim objGnrlFnction As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails
    Dim objPreturn As New clsPurchaseReturn
    Private Shared sIKBBackStatus As String
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        ' imgbtnSave.ImageUrl = "~/Images/Save24.png"
        ' imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                GenerateOrderCode()
                BindOrder()
                LoadSuppliers()
                LoadCommodity()
                BindTypeOfPurchase()
                BindCategoryOfSale()
                BindTypeOfPurchasedetails()
                'Me.btnSave.Attributes.Add("OnClick", "return ValidateMasterData()")
                Me.txtAppRate.Attributes.Add("onblur", "return ValidateRate()")
                Me.txtAppRate.Attributes.Add("onblur", "return CalculateUsingRate()")
                Me.txtQuantity.Attributes.Add("onclick", "return ValidateddlUnit()")
                Me.txtQuantity.Attributes.Add("onblur", "return CalculateFromVat()")
                Me.txtDiscount.Attributes.Add("onblur", "return CalculateDiscount()")
                'Me.btnSave.Attributes.Add("onclick", "return ValidatePurcahseOrder()")
                'Me.btnPrint.Attributes.Add("onclick", "return ValidatePrint()")
                Me.chkCategory.Attributes.Add("onclick", "return CheckType()")

                Me.ddlVat.Attributes.Add("onclick", "return CalculateFromVat()")
                Me.ddlCst.Attributes.Add("onclick", "return CalculateFromCST()")
                Me.txtExcise.Attributes.Add("onblur", "return CalculateExcise()")
                Me.txtFreight.Attributes.Add("onblur", "return CalculateFrieght()")
                LoadCST()
                LoadVAT()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    'Private Sub LoadModeOfReturn()
    '    Try
    '        ddlm.DataSource = clsPurchaseReturn.LoadModeOfReturn(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlModeOfReturn.DataTextField = "Mas_desc"
    '        ddlModeOfReturn.DataValueField = "Mas_id"
    '        ddlModeOfReturn.DataBind()
    '        ddlModeOfReturn.Items.Insert(0, "--- Select Mode of Return ---")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub GenerateOrderCode()
        Try
            purchaseReturnNo.Text = objPreturn.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCode")
        End Try
    End Sub
    'Private Sub LoadSuppliers()
    '    Try
    '        ddlSupplier.DataSource = clsPurchaseReturn.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlSupplier.DataTextField = "CSM_Name"
    '        ddlSupplier.DataValueField = "CSM_ID"
    '        ddlSupplier.DataBind()
    '        ddlSupplier.Items.Insert(0, "--- Select Supplier ---")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub BindOrder()
        Try
            ddlOrderNo.DataSource = objPreturn.LoadOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlOrderNo.DataTextField = "POM_OrderNo"
            ddlOrderNo.DataValueField = "POM_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "--- Purchase Order No ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/Purchase/PurchaseOrderMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub





    Private Sub LoadSuppliers()
        Try
            ddlSupplier.DataSource = objPO.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplier.DataTextField = "CSM_Name"
            ddlSupplier.DataValueField = "CSM_ID"
            ddlSupplier.DataBind()
            ddlSupplier.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub BindTypeOfPurchasedetails()
        Try
            ddlreturntype.Items.Add(New ListItem("Select Type", 0))
            ddlreturntype.Items.Add(New ListItem("Excess Qty", 1))
            ddlreturntype.Items.Add(New ListItem("Rate diffrence", 2))
            ddlreturntype.Items.Add(New ListItem("Defective Goods", 3))
            ddlreturntype.Items.Add(New ListItem("Goods Shipped Too Late", 4))
            ddlreturntype.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub BindTypeOfPurchase()
        Try
            ddlTypeOfSale.Items.Add(New ListItem("Select Type Of Sale", 0))
            ddlTypeOfSale.Items.Add(New ListItem("Local", 1))
            ddlTypeOfSale.Items.Add(New ListItem("Inter State", 2))
            ddlTypeOfSale.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub BindCategoryOfSale()
        Try
            ddlCstCtgry.Items.Add(New ListItem("Select Type Of Sale", 0))
            ddlCstCtgry.Items.Add(New ListItem("Regular", 1))
            ddlCstCtgry.Items.Add(New ListItem("2 % CST", 2))
            ddlCstCtgry.Items.Add(New ListItem("H Form", 3))
            ddlCstCtgry.Items.Add(New ListItem("I Form", 4))
            ddlCstCtgry.Items.Add(New ListItem("F Form", 5))
            ddlCstCtgry.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingPurchaseOrder()
        Dim iBranchID As Integer
        Try
            ddlExistingOrder.DataSource = objPO.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            ddlExistingOrder.DataTextField = "POM_OrderNo"
            ddlExistingOrder.DataValueField = "POM_ID"
            ddlExistingOrder.DataBind()
            ddlExistingOrder.Items.Insert(0, "Existing Purchase Order")
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
            ddlCommodity.Items.Insert(0, "Select Brand")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub LoadVAT()
        Try
            ddlVat.DataSource = objInvD.LoadVAT(sSession.AccessCode, sSession.AccessCodeID)
            ddlVat.DataTextField = "Mas_Desc"
            ddlVat.DataValueField = "Mas_ID"
            ddlVat.DataBind()
            ddlVat.Items.Insert(0, "Select VAT")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCST()
        Try
            ddlCst.DataSource = objInvD.LoadCST(sSession.AccessCode, sSession.AccessCodeID)
            ddlCst.DataTextField = "Mas_Desc"
            ddlCst.DataValueField = "Mas_ID"
            ddlCst.DataBind()
            ddlCst.Items.Insert(0, "Select CST")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadDescitionStart()
        Try
            chkCategory.DataSource = objPO.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            chkCategory.DataTextField = "Inv_Code"
            chkCategory.DataValueField = "Inv_ID"
            chkCategory.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Protected Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
    '    Dim altPices As Integer
    '    Try
    '        If (chkCategory.SelectedValue > 0) Then
    '            ddlCstCtgry.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
    '        End If
    '        lblDescID.Text = chkCategory.SelectedValue
    '        LoadDesciptionDetails()
    '        altPices = objPO.GetAlterNatePiceValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
    '        txtPices.Text = altPices
    '        ddlUnit.SelectedValue = objPO.GetUnitsValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
    '        hfTotalPieces.Value = txtPices.Text
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
    '    End Try
    'End Sub
    'Private Sub LoadDesciptionDetails()
    '    Dim dt As New DataTable
    '    Dim sArray As Array
    '    Try
    '        'ddlRate.DataSource = dt
    '        'ddlRate.DataBind()
    '        'txtRDate.Text = "" : txtRate.Text = "" : txtRateAmount.Text = ""
    '        txtQuantity.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
    '        txtExcise.Text = "" : txtExciseAmount.Text = ""
    '        ddlVat.SelectedIndex = 0 : txtVatAmount.Text = "" : ddlCst.SelectedIndex = 0 : txtCSTAmount.Text = ""
    '        txtTotalAmount.Text = ""

    '        If lblDescID.Text <> "0" Then
    '            dt = objPO.CheckDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
    '            If dt.Rows.Count = 0 Then
    '                lblError.Text = "Enter Details in Inventory Master Details"
    '                Exit Sub
    '            End If
    '            If dt.Rows.Count > 1 Then
    '                'ddlRate.DataSource = dt
    '                'ddlRate.DataTextField = "INVH_PreDeterminedPrice"
    '                'ddlRate.DataValueField = "InvH_ID"
    '                ddlRate.DataBind()
    '                ddlRate.Enabled = True : txtRate.Enabled = False
    '                '   txtHistoryID.Text = ddlRate.SelectedValue
    '                'sArray = '5 ''ddlRate.SelectedItem.Text.Split("-")
    '                'txtRate.Text = sArray(0)
    '                GetOtherDetails(txtHistoryID.Text)
    '            Else
    '                sArray = dt.Rows(0)(1).ToString().Split("-")
    '                txtRate.Text = sArray(0)
    '                txtHistoryID.Text = dt.Rows(0)(0).ToString()
    '                ddlRate.Enabled = False : txtRate.Enabled = True
    '                If txtHistoryID.Text <> "" Then
    '                    GetPurchaseDetails(txtHistoryID.Text)
    '                    GetOtherDetails(txtHistoryID.Text)
    '                End If
    '            End If
    '            ddlUnit.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
    '            ddlUnit.DataTextField = "Mas_Desc"
    '            ddlUnit.DataValueField = "Mas_ID"
    '            ddlUnit.DataBind()
    '            ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub GetOtherDetails(ByVal iHistoryId As Integer)
        Dim dt As New DataTable
        Try
            dt = objPO.GetOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                    txtExcise.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                Else
                    txtExcise.Text = "0"
                End If
                If (ddlCommodity.SelectedValue = 1) Then
                    ddlVat.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    Else
                        ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCommodity.SelectedValue = 2) Then
                    ddlVat.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0)("InvH_CST").ToString()) = False Then
                        ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 15, dt.Rows(0)("InvH_CST").ToString())
                    Else
                        ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCommodity.SelectedValue = 3) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                    ddlCst.Enabled = False
                    ddlVat.Enabled = False
                ElseIf (ddlCommodity.SelectedValue = 4) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                    ddlCst.Enabled = False
                    ddlVat.Enabled = False
                ElseIf (ddlCommodity.SelectedValue = 5) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                    ddlCst.Enabled = False
                    ddlVat.Enabled = False
                Else
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        ddlVat.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetOtherDetails")
        End Try
    End Sub
    Private Function GetPurchaseDetails(ByVal iHistoryId As Integer) As Object
        Dim dt As New DataTable
        Try
            dt = objPO.GetPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                    txtExcise.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                Else
                    txtExcise.Text = "0"
                End If

                If (ddlCommodity.SelectedValue = 1) Then
                    ddlVat.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    Else
                        ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCommodity.SelectedValue = 2) Then
                    If IsDBNull(dt.Rows(0)("InvH_CST").ToString()) = False Then
                        ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 15, dt.Rows(0)("InvH_CST").ToString())
                    Else
                        ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCommodity.SelectedValue = 3) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                ElseIf (ddlCommodity.SelectedValue = 4) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                ElseIf (ddlCommodity.SelectedValue = 5) Then
                    ddlVat.SelectedIndex = 0
                    ddlCst.SelectedIndex = 0
                Else
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        ddlVat.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetPurchaseDetails")
        End Try
    End Function
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim iMasterID As Integer = 0
        Dim dOrderDate As Date
        Dim dRequiredDate As Date
        Try
            lblError.Text = ""
            dOrderDate = Date.ParseExact(Trim(txtReturnDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objPreturn.sPRM_ReturnNo = purchaseReturnNo.Text
            objPreturn.iPRM_Supplier = ddlSupplier.SelectedValue
            objPreturn.iPRM_CreatedBy = sSession.UserID
            objPreturn.iPRM_YearID = sSession.YearID
            If (ddlreturntype.SelectedIndex > 0) Then
                objPreturn.iPRM_TypeOfReturn = ddlreturntype.SelectedValue
            Else
                objPreturn.iPRM_TypeOfReturn = 0
            End If
            If (txtNarration.Text = "") Then
                objPreturn.sPRM_Remarks = ""
            Else
                objPreturn.sPRM_Remarks = txtNarration.Text
            End If
            If (ddlTypeOfSale.SelectedIndex > 0) Then
                objPreturn.iPRM_SaleType = ddlTypeOfSale.SelectedValue
            Else
                objPreturn.iPRM_SaleType = 0
            End If

            If (ddlCstCtgry.SelectedIndex > 0) Then
                objPreturn.iPRM_iCSTCtgry = ddlCstCtgry.SelectedValue
            Else
                objPreturn.iPRM_iCSTCtgry = 0
            End If
            objPreturn.sPRM_Status = "W"
            iMasterID = objPreturn.SavePurchaseReturn(sSession.AccessCode, sSession.AccessCodeID, dOrderDate, objPreturn)
            txtMasterID.Text = iMasterID
            objPreturn.iPRD_MasterID = iMasterID
            objPreturn.iPRD_DescriptionID = lblDescID.Text
            objPreturn.iPRD_HistoryID = txtHistoryID.Text
            objPreturn.iPRD_Unit = ddlUnit.SelectedValue
            If (ddlreturntype.SelectedIndex = 3) Then
                objPreturn.sPRD_Rate = Trim(txtAppRate.Text)
            ElseIf (ddlreturntype.SelectedIndex = 2) Then
                objPreturn.sPRD_Rate = Trim(txtAppRate.Text)
            Else
                objPreturn.sPRD_Rate = Trim(txtRate.Text)
            End If
            If hfRateAmount.Value <> "" Then
                objPreturn.sPRD_RateAmount = Request.Form(hfRateAmount.UniqueID)
            Else
                objPreturn.sPRD_RateAmount = objClsFASGnrl.SafeSQL(txtRateAmount.Text)
            End If

            If txtQuantity.Text = "" Then
                objPreturn.sPRD_Quantity = "0"
            Else
                objPreturn.sPRD_Quantity = objClsFASGnrl.SafeSQL(txtQuantity.Text)
            End If

            If txtDiscount.Text = "" Then
                objPreturn.sPRD_Discount = "0"
            Else
                objPreturn.sPRD_Discount = objClsFASGnrl.SafeSQL(txtDiscount.Text)
            End If

            If hfDiscountAmount.Value <> "" Then
                objPreturn.sPRD_DiscountAmount = Request.Form(hfDiscountAmount.UniqueID)
            Else
                objPreturn.sPRD_DiscountAmount = objClsFASGnrl.SafeSQL(txtDiscountAmount.Text)
            End If

            If txtExcise.Text = "" Then
                objPreturn.sPRD_Excise = "0"
            Else
                objPreturn.sPRD_Excise = objClsFASGnrl.SafeSQL(txtExcise.Text)
            End If

            If hfExciseAmount.Value <> "" Then
                objPreturn.sPRD_ExciseAmount = Request.Form(hfExciseAmount.UniqueID)
            Else
                objPreturn.sPRD_ExciseAmount = objClsFASGnrl.SafeSQL(txtExciseAmount.Text)
            End If

            If txtFreight.Text = "" Then
                objPreturn.sPRD_Frieght = "0"
            Else
                objPreturn.sPRD_Frieght = objClsFASGnrl.SafeSQL(txtFreight.Text)
            End If

            If hfFreightAmount.Value <> "" Then
                objPreturn.sPRD_FrieghtAmount = Request.Form(hfFreightAmount.UniqueID)
            Else
                objPreturn.sPRD_FrieghtAmount = objClsFASGnrl.SafeSQL(txtFreightAmount.Text)
            End If

            If ddlVat.SelectedIndex > 0 Then
                objPreturn.sPRD_VAT = objClsFASGnrl.SafeSQL(ddlVat.SelectedItem.Text)
            Else
                objPreturn.sPRD_VAT = "0"
            End If

            If hfVatAmount.Value <> "" Then
                objPreturn.sPRD_VATAmount = Request.Form(hfVatAmount.UniqueID)
            Else
                objPreturn.sPRD_VATAmount = objClsFASGnrl.SafeSQL(txtVatAmount.Text)
            End If

            If ddlCst.SelectedIndex > 0 Then
                objPreturn.sPRD_CST = objClsFASGnrl.SafeSQL(ddlCst.SelectedItem.Text)
            Else
                objPreturn.sPRD_CST = "0"
            End If

            If hfCSTAmount.Value <> "" Then
                objPreturn.sPRD_CSTAmount = Request.Form(hfCSTAmount.UniqueID)
            Else
                objPreturn.sPRD_CSTAmount = objClsFASGnrl.SafeSQL(txtCSTAmount.Text)
            End If

            'If txtReturnDate.Text <> "" Then
            '    objPO.dPRD_RequiredDate = Date.ParseExact(Trim(txtRequiredDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'Else
            '    objPO.dPRD_RequiredDate = "01/01/1900"
            'End If

            If hfTotalAmount.Value <> "" Then
                objPreturn.sPRD_TotalAmount = Request.Form(hfTotalAmount.UniqueID)
            Else
                objPreturn.sPRD_TotalAmount = objClsFASGnrl.SafeSQL(txtTotalAmount.Text)
            End If
            Dim sStatus As String = ""
            objPreturn.SavePurchaseReturnDetails(sSession.AccessCode, sSession.AccessCodeID, dRequiredDate, objPreturn)
            dgPurchaseReturn.DataSource = objPreturn.LoadPurchasereturnDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
            dgPurchaseReturn.DataBind()
            LoadExistingPurchaseOrder()
            'ddlSearch.SelectedValue = iMasterID
            ClearAll()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub ClearAll()
        Try
            For i = 0 To chkCategory.Items.Count - 1
                chkCategory.Items(i).Selected = False
            Next
            ddlUnit.Items.Clear()
            txtAppRate.Text = ""
            hfCSTAmount.Value = ""
            hfDiscountAmount.Value = ""
            hfExciseAmount.Value = ""
            hfVatAmount.Value = ""
            ddlSupplier.SelectedIndex = 0

            txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtExcise.Text = "" : txtExciseAmount.Text = ""
            ddlVat.SelectedIndex = 0 : txtVatAmount.Text = ""
            ddlCst.SelectedIndex = 0 : txtCSTAmount.Text = ""
            '   txtRDate.Text = "" : txtTotalAmount.Text = ""
            txtFreight.Text = "" : txtFreightAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            'lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgPurchaseReturn.Rows.Count - 1
                    chkField = dgPurchaseReturn.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgPurchaseReturn.Rows.Count - 1
                    chkField = dgPurchaseReturn.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Protected Sub ddlExistingOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingOrder.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Try
            If ddlExistingOrder.SelectedIndex > 0 Then
                dtMaster = objPreturn.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        'If IsDBNull(dtMaster.Rows(i)("PRM_ReturnNo")) = False Then
                        '    'ddlOrderNo.SelectedValue = dtMaster.Rows(i)("PRM_ReturnNo")
                        '    purchaseReturnNo.Text = dtMaster.Rows(i)("PRM_ReturnNo")
                        'End If
                        'If IsDBNull(dtMaster.Rows(i)("PRM_OrderDate")) = False Then
                        '    txtOrderDate.Text = clsTRACeGeneral.FormatDtForRDBMS(dtMaster.Rows(i)("PRM_OrderDate"), "D")
                        'End If
                        ''If IsDBNull(dtMaster.Rows(i)("PRM_ReferenceNo")) = False Then
                        ''    txtReturnRefNo.Text = dtMaster.Rows(i)("PRM_ReferenceNo")
                        ''End If
                        ''If IsDBNull(dtMaster.Rows(i)("PRM_ReturnDate")) = False Then
                        ''    txtReturnDate.Text = clsTRACeGeneral.FormatDtForRDBMS(dtMaster.Rows(i)("PRM_ReturnDate"), "D")
                        ''End If
                        'If IsDBNull(dtMaster.Rows(i)("PRM_Supplier")) = False Then
                        '    ddlSupplier.SelectedValue = dtMaster.Rows(i)("PRM_Supplier")
                        'End If
                        ''If IsDBNull(dtMaster.Rows(i)("PRM_SupplierCode")) = False Then
                        ''    txtSupplierCode.Text = dtMaster.Rows(i)("PRM_SupplierCode")
                        ''End If
                        ''If IsDBNull(dtMaster.Rows(i)("PRM_ModeOfReturn")) = False Then
                        ''    ddlModeOfReturn.SelectedValue = dtMaster.Rows(i)("PRM_ModeOfReturn")
                        ''End If
                        'If IsDBNull(dtMaster.Rows(i)("PRM_Remarks")) = False Then
                        '    txtNarration.Text = dtMaster.Rows(i)("PRM_Remarks")
                        'End If
                    Next
                End If
                dgPurchaseReturn.DataSource = objPreturn.LoadPurchasereturnDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                dgPurchaseReturn.DataBind()
            Else
                '  ddlSearch.Visible = False
                txtsearch.Text = ""
                txtsearch.Visible = True
                ClearAll()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingOrder_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub dgPurchase_PreRender(sender As Object, e As EventArgs) Handles dgPurchaseReturn.PreRender
        Dim dt As New DataTable
        Try
            If dgPurchaseReturn.Rows.Count > 0 Then
                dgPurchaseReturn.UseAccessibleHeader = True
                dgPurchaseReturn.HeaderRow.TableSection = TableRowSection.TableHeader
                dgPurchaseReturn.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_PreRender")
        End Try
    End Sub
    Protected Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCstCtgry.SelectedIndexChanged
        Try
            txtRate.Enabled = False : txtAppRate.Enabled = False
            If ddlCstCtgry.SelectedIndex > 0 Then
                chkCategory.DataSource = objPO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCstCtgry.SelectedValue)
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
    Protected Sub dgPurchase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchaseReturn.RowDataBound

    End Sub
    Protected Sub ddlSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSupplier.SelectedIndexChanged

    End Sub

    Protected Sub txtHistoryID_TextChanged(sender As Object, e As EventArgs) Handles txtHistoryID.TextChanged

    End Sub
    Protected Sub ddlTypeOfSale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTypeOfSale.SelectedIndexChanged
        Try
            ClearAll()
            If (ddlTypeOfSale.SelectedValue = 2) Then
                ddlCommodity.Enabled = True
                ddlVat.Enabled = False
                ddlCst.Enabled = True
            Else
                ddlCommodity.Enabled = False
                ddlVat.Enabled = True
                ddlCst.Enabled = False
                ddlCommodity.SelectedIndex = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTypeOfSale_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlCstCtgry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            ClearAll()
            If (ddlCommodity.SelectedValue = 1) Then
                loadRegular()
            ElseIf (ddlCommodity.SelectedValue = 2) Then
                LoadCST()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCstCtgry_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub loadRegular()
        Try
            ddlCst.Items.Clear()

            ddlCst.DataSource = objInvntry.LoadVAT(sSession.AccessCode, sSession.AccessCodeID)
            ddlCst.DataTextField = "Mas_Desc"
            ddlCst.DataValueField = "Mas_ID"
            ddlCst.DataBind()
            ddlCst.Items.Insert(0, "--- Select CST ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub txtPices_TextChanged(sender As Object, e As EventArgs) Handles txtPices.TextChanged

    End Sub
    'Protected Sub dgPurchase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPurchaseReturn.RowCommand
    '    Dim lnkDescription As New LinkButton
    '    Dim lblcomodityID As New Label
    '    Dim lblDescriptionId As New Label
    '    Dim lblHistoryID As New Label
    '    Dim dt As New DataTable
    '    Try

    '        lblError.Text = ""
    '        Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)

    '        lnkDescription = DirectCast(clickedRow.FindControl("lnkGoods"), LinkButton)
    '        lblcomodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
    '        lblDescriptionId = DirectCast(clickedRow.FindControl("lblDescriptionID"), Label)
    '        lblHistoryID = DirectCast(clickedRow.FindControl("lblHistoryID"), Label)
    '        If e.CommandName = "Delete" Then
    '            'objPO.DeleteOrderValues(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text, lblDescriptionId.Text)
    '            dgPurchaseReturn.DataSource = objPO.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
    '            dgPurchaseReturn.DataBind()
    '            If (dgPurchaseReturn.Rows.Count = 0) Then
    '                'objPO.DeleteOrderValuesFromMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text)
    '            End If
    '            LoadExistingPurchaseOrder()
    '        End If
    '        If e.CommandName = "Select" Then

    '            If (objPO.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtMasterID.Text) = "W") Then
    '                dt = objPO.LoadPurchaseOderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text, lblcomodityID.Text, lblDescriptionId.Text, lblHistoryID.Text)
    '                If dt.Rows.Count > 0 Then
    '                    If IsDBNull(dt.Rows(0)("POD_Commodity").ToString()) = False Then
    '                        ddlCstCtgry.SelectedValue = dt.Rows(0)("POD_Commodity").ToString()
    '                        chkCategory.DataSource = objPO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCstCtgry.SelectedValue)
    '                        chkCategory.DataTextField = "Inv_Code"
    '                        chkCategory.DataValueField = "Inv_ID"
    '                        chkCategory.DataBind()
    '                        chkCategory.SelectedValue = dt.Rows(0)("POD_DescriptionID").ToString()
    '                        lblDescID.Text = dt.Rows(0)("POD_DescriptionID").ToString()
    '                        LoadDesciptionDetails()
    '                    Else
    '                        ddlCstCtgry.SelectedIndex = 0
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_HistoryID").ToString()) = False Then
    '                        txtHistoryID.Text = dt.Rows(0)("POD_HistoryID").ToString()
    '                    Else
    '                        txtHistoryID.Text = "0"
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_Unit").ToString()) = False Then
    '                        ddlUnit.SelectedValue = dt.Rows(0)("POD_Unit").ToString()
    '                    Else
    '                        ddlUnit.SelectedIndex = 0
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_Rate").ToString()) = False Then
    '                        txtRate.Text = dt.Rows(0)("POD_Rate").ToString()
    '                    Else
    '                        txtRate.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_RateAmount").ToString()) = False Then
    '                        txtRateAmount.Text = dt.Rows(0)("POD_RateAmount").ToString()
    '                    Else
    '                        txtRateAmount.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_Quantity").ToString()) = False Then
    '                        txtQuantity.Text = dt.Rows(0)("POD_Quantity").ToString()
    '                    Else
    '                        txtQuantity.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_Discount").ToString()) = False Then
    '                        txtDiscount.Text = dt.Rows(0)("POD_Discount").ToString()
    '                    Else
    '                        txtDiscount.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_DiscountAmount").ToString()) = False Then
    '                        txtDiscountAmount.Text = dt.Rows(0)("POD_DiscountAmount").ToString()
    '                    Else
    '                        txtDiscountAmount.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_Excise").ToString()) = False Then
    '                        txtExcise.Text = dt.Rows(0)("POD_Excise").ToString()
    '                    Else
    '                        txtExcise.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_ExciseAmount").ToString()) = False Then
    '                        txtExciseAmount.Text = dt.Rows(0)("POD_ExciseAmount").ToString()
    '                    Else
    '                        txtExciseAmount.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_Frieght").ToString()) = False Then
    '                        txtFreight.Text = dt.Rows(0)("POD_Frieght").ToString()
    '                    Else
    '                        txtFreight.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_FrieghtAmount").ToString()) = False Then
    '                        txtFreightAmount.Text = dt.Rows(0)("POD_FrieghtAmount").ToString()
    '                    Else
    '                        txtFreightAmount.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_VAT").ToString()) = False Then
    '                        ddlVat.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("POD_VAT").ToString())

    '                    Else
    '                        ddlVat.SelectedValue = 0
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_VATAmount").ToString()) = False Then
    '                        txtVatAmount.Text = dt.Rows(0)("POD_VATAmount").ToString()
    '                    Else
    '                        txtVatAmount.Text = ""
    '                    End If

    '                    If (ddlCommodity.SelectedValue = 1) Then
    '                        If IsDBNull(dt.Rows(0)("POD_CST").ToString()) = False Then
    '                            ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("POD_CST").ToString())
    '                        Else
    '                            ddlCst.SelectedValue = 0
    '                        End If
    '                    ElseIf (ddlCommodity.SelectedValue = 2) Then
    '                        If IsDBNull(dt.Rows(0)("POD_CST").ToString()) = False Then
    '                            ddlCst.SelectedValue = objPO.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 15, dt.Rows(0)("POD_CST").ToString())
    '                        Else
    '                            ddlCst.SelectedValue = 0
    '                        End If
    '                    Else
    '                        ddlCst.SelectedIndex = 0
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_CSTAmount").ToString()) = False Then
    '                        txtCSTAmount.Text = dt.Rows(0)("POD_CSTAmount").ToString()
    '                    Else
    '                        txtCSTAmount.Text = ""
    '                    End If


    '                    If IsDBNull(dt.Rows(0)("POD_RequiredDate").ToString()) = False Then
    '                        If objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_RequiredDate").ToString(), "D") = "01/01/1900" Then
    '                            txtRDate.Text = ""
    '                        Else
    '                            txtRDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_RequiredDate").ToString(), "D")
    '                        End If
    '                    Else
    '                        txtRDate.Text = ""
    '                    End If

    '                    If IsDBNull(dt.Rows(0)("POD_TotalAmount").ToString()) = False Then
    '                        txtTotalAmount.Text = dt.Rows(0)("POD_TotalAmount").ToString()
    '                    Else
    '                        txtTotalAmount.Text = ""
    '                    End If
    '                End If

    '                'Dim sStatus As String = ""
    '                'sStatus = objPO.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
    '                'If sStatus = "A" Then
    '                '    'btnSave.Visible = True : btnSave.Enabled = False
    '                '    'btnApprove.Visible = True : btnApprove.Enabled = False
    '                'Else
    '                '    'btnSave.Visible = True : btnSave.Enabled = True
    '                '    'btnApprove.Visible = True : btnApprove.Enabled = True
    '                'End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Protected Sub ddlOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrderNo.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlOrderNo.SelectedIndex > 0 Then
                LoadOrderDetails(ddlOrderNo.SelectedValue)
                LoadExistGoodsInwardNo(ddlOrderNo.SelectedValue)
                dt = objPO.LoadPurchaseOderMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("POM_TypeOfPurchase").ToString()) = False Then
                        ddlTypeOfSale.SelectedValue = dt.Rows(0)("POM_TypeOfPurchase").ToString()
                    Else
                        ddlTypeOfSale.SelectedValue = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_CstCategory").ToString()) = False Then
                        ddlCstCtgry.SelectedValue = dt.Rows(0)("POM_CstCategory").ToString()
                        If (dt.Rows(0)("POM_CstCategory").ToString() = 1) Then
                            loadRegular()
                        Else
                            LoadCST()
                        End If
                    Else
                        ddlCommodity.SelectedValue = 0
                    End If
                    'dt = clsPurchaseReturn.BindOrderGrid(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
                    'dgPurchaseReturn.DataSource = dt
                    'dgPurchaseReturn.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlOrderNo_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadExistGoodsInwardNo(ByVal iTransactionID As Integer)
        Try
            ddlInvoiceNo.DataSource = objPreturn.LoadInwardNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransactionID)
            ddlInvoiceNo.DataTextField = "PGM_DocumentRefNo"
            ddlInvoiceNo.DataValueField = "PGM_ID"
            ddlInvoiceNo.DataBind()
            ddlInvoiceNo.Items.Insert(0, "--- Select Invoice NO ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadOrderDetails(ByVal iPONo As Integer)
        Dim dtable As New DataTable
        Try
            dtable = objGin.OrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPONo)
            If (dtable.Rows.Count > 0) Then
                For i = 0 To dtable.Rows.Count - 1
                    txtOrderDate.Text = objClsFASGnrl.FormatDtForRDBMS(dtable.Rows(i)("POM_OrderDate"), "D")
                    ddlSupplier.SelectedValue = objClsFASGnrl.ReplaceSafeSQL(dtable.Rows(i)("POM_Supplier"))
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOrderDetails")
        End Try
    End Sub
    Protected Sub ddlInvoiceNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInvoiceNo.SelectedIndexChanged
        Try
            If ddlInvoiceNo.SelectedIndex > 0 Then
                txtInvoiceDate.Text = objPreturn.getInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedItem.Text, ddlOrderNo.SelectedValue, sSession.YearID)
                chkCategory.DataSource = objPreturn.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                chkCategory.DataTextField = "Inv_Code"
                chkCategory.DataValueField = "Inv_ID"
                chkCategory.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInvoiceNo_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
        Dim iPices As Integer
        Try
            If (chkCategory.SelectedValue > 0) Then
                ddlCommodity.SelectedValue = objPreturn.GetComodityID(sSession.AccessCode, chkCategory.SelectedValue)
            End If
            lblDescID.Text = chkCategory.SelectedValue
            LoadDesciptionDetails()
            iPices = objPreturn.GetPicess(sSession.AccessCode, txtHistoryID.Text, sSession.AccessCodeID)
            'iPices = DBHelper.SQLDBExecScalarInteger(sSession.AccessCode, "Select INVH_PerPieces From Inventory_master_History Where InvH_ID ='" & txtHistoryID.Text & "' And INVH_CompID=" & sSession.AccessCodeID & " ")
            txtPices.Text = iPices
            ddlUnit.SelectedValue = objPreturn.GetUnitID(sSession.AccessCode, txtHistoryID.Text, sSession.AccessCodeID)
            hfTotalPieces.Value = txtPices.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadDesciptionDetails()
        Dim dt As New DataTable
        Dim sArray As Array
        Try
            lblError.Text = ""
            ' ddlRate.DataSource = dt
            ' ddlRate.DataBind()
            txtRate.Text = "" : txtRateAmount.Text = ""
            txtQuantity.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtExcise.Text = "" : txtExciseAmount.Text = ""
            ddlVat.SelectedIndex = 0 : txtVatAmount.Text = "" : ddlCst.SelectedIndex = 0 : txtCSTAmount.Text = ""
            txtTotalAmount.Text = ""
            If lblDescID.Text <> "0" Then
                dt = objPreturn.LoadStockRateQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "Enter Details in Inventory Master Details"
                    Exit Sub
                End If
                If dt.Rows.Count > 1 Then
                    txtRate.Enabled = True
                    txtHistoryID.Text = dt.Rows(0)("SL_HistoryID").ToString()
                    txtRate.Text = dt.Rows(0)("PurchaseRate").ToString()
                    txtQty.Text = dt.Rows(0)("SL_PurchaseQty").ToString()
                    txtERate.Text = dt.Rows(0)("PurchaseRate").ToString()
                    GetOtherDetails(dt.Rows(0)("SL_HistoryID").ToString())
                Else
                    txtHistoryID.Text = dt.Rows(0)("SL_HistoryID").ToString()
                    '  ddlRate.Enabled = False
                    txtRate.Enabled = True
                    If txtHistoryID.Text <> "" Then
                        GetPurchaseDetails(txtHistoryID.Text)
                        GetOtherDetails(txtHistoryID.Text)
                    End If
                End If
                ddlUnit.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                ddlUnit.DataTextField = "Mas_Desc"
                ddlUnit.DataValueField = "Mas_ID"
                ddlUnit.DataBind()
                ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDesciptionDetails")
        End Try
    End Sub
End Class
