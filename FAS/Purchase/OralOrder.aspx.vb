
Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports DatabaseLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing
Partial Class Purchase_OralOrder
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Orders/PurchaseOrder"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objOral As New clsOralOrder
    Dim objPO As New clsPurchaseOrder
    Dim objDb As New DBHelper
    Dim objInvD As New clsInvenotryDetails
    Dim objClsFASGnrl As New clsFASGeneral
    Dim objGnrlFnction As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails
    Dim objGin As New ClsGoodsInward
    Private Shadows sOOSave As String
    Dim obDB As New DBHelper
    Dim objDispatch As New ClsDispatchDetails
    Private objAccSetting As New clsAccountSetting
    Dim objclsModulePermission As New clsModulePermission
    Private Shared sIKBBackStatus As String
    Public dtPurchase As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnPrint.ImageUrl = "~/Images/Download24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim iIKBID As Integer
        Dim dt As New DataTable
        Dim sFormButtons As String = True
        Dim iDefaultBranch As Integer
        Try
            sSession = Session("AllSession")
            sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "OCP")
            imgRefresh.Visible = False : imgbtnAdd.Visible = False : imgbtnWaiting.Visible = False : imgbtnPrint.Visible = False : btnItemAdd.Visible = False
            imgbtnAddCharge.Visible = False
            sOOSave = "NO"
            If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
                Exit Sub
            Else
                If sFormButtons.Contains(",View,") = True Then
                End If
                If sFormButtons.Contains(",New,") = True Then
                    imgRefresh.Visible = True
                End If
                If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                    imgbtnAdd.Visible = True
                    btnItemAdd.Visible = True
                    imgbtnAddCharge.Visible = True
                    sOOSave = "YES"
                End If
                If sFormButtons.Contains(",Approve,") = True Then
                    imgbtnWaiting.Visible = True
                End If
                If sFormButtons.Contains(",Report,") = True Then
                    imgbtnPrint.Visible = True
                End If
            End If

            ReFVOdate.ErrorMessage = "Enter Order Date."
            RFVSupplier.InitialValue = "Select Supplier"
            RFVUnits.ErrorMessage = "Select the goods from item to load units of measurments"
            RFVDschdule.InitialValue = "Select Delivery Schdule"
            RFVMshipping.InitialValue = "Select Mode of Shipping"
            RFVMpayment.InitialValue = "Select Method Of Payment"
            RFVCmdty.InitialValue = "Select Brand"

            RFVInvoiceRef.ErrorMessage = "Enter Invoice ReferenceNo"
            RFVDcNo.ErrorMessage = "Enter Delivery Chalan No"
            RFVEsugamNo.ErrorMessage = "Enter EsugamNo"
            'rfInvoiceDate.ErrorMessage = "Enter invoice Date"
            RFVInvoiceDate.ErrorMessage = "Enter invoice Date"

            'RFVTypeOsale.InitialValue = 0
            RFVPterms.InitialValue = "Select Payment Terms"
            RefRate.InitialValue = "Enter Rate Field"

            ReDiscount.ErrorMessage = "Only Integer." : ReDiscount.ValidationExpression = "^\s*-?[0-9]\d*(\.\d{1,2})?\s*$"

            RFVAccZone.InitialValue = "--- Select Zone ---" : RFVAccZone.ErrorMessage = "Select Zone."
            RFVAccRgn.InitialValue = "--- Select Region ---" : RFVAccRgn.ErrorMessage = "Select Region."
            RFVAccArea.InitialValue = "--- Select Area ---" : RFVAccArea.ErrorMessage = "Select Area."
            RFVAccBrnch.InitialValue = "--- Select Branch ---" : RFVAccBrnch.ErrorMessage = "Select Branch."

            If IsPostBack = False Then
                Me.txtmdate.Attributes.Add("onblur", "return CheckMDate()")
                Me.txtEdate.Attributes.Add("onblur", "return CheckEDate()")
                Me.txtinvoiceDate.Attributes.Add("onblur", "return checkInvoiceDate()")
                Me.txtRate.Attributes.Add("onblur", "return ValidateRate()")

                'Me.txtRate.Attributes.Add("onblur", "return CalculateUsingRate()")
                'Me.txtQuantity.Attributes.Add("onblur", "return FindRejectedQty()")

                Me.txtQuantity.Attributes.Add("onblur", "return RejectedQty()")
                'Me.txtQuantity.Attributes.Add("onblur", "return recievedAmount()")

                'Me.txtQuantity.Attributes.Add("onblur", "return CalculateFromVat()")
                'Me.txtDiscount.Attributes.Add("onblur", "return CalculateDiscount()")
                Me.txtDiscount.Attributes.Add("onblur", "return CalculateQuantity()")
                'Me.ddlVat.Attributes.Add("onclick", "return CalculateFromVat()")
                'Me.ddlCst.Attributes.Add("onclick", "return CalculateFromCST()")
                'Me.txtFreight.Attributes.Add("onblur", "return CalculateFrieght()")
                'Me.txtQuantity.Attributes.Add("onblur", "return FindRejectedQty()")

                'Me.txtFreight.Attributes.Add("onblur", "return CalculateeeFrieght()")
                txtItemTableID.Text = ""
                LoadExistingPurchaseOrder()
                GenerateOrderCodeAnddate()
                LoadSuppliers() : BindCompanyType() : BindBranch()

                dt = objOral.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("CUST_COMM_Address")) = True Or IsDBNull(dt.Rows(0)("CUST_ProvisionalNo")) = True Then
                        lblError.Text = "FIll the details in Company Master"
                        Exit Sub
                    End If

                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    Dim taxcategory As String
                    taxcategory = objOral.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CUST_TAXPayableCategory"))
                    If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryGSTNRegNo.Enabled = True
                    End If

                    txtDeleveryAddress.Text = txtCompanyAddress.Text
                    txtDeliveryGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
                End If
                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)

                iDefaultBranch = objOral.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                Session("dtPurchase") = Nothing
                Session("ChargesMaster") = Nothing
                LoadCommodity()
                'LoadChargeType()
                '   LoadVAT()
                '   LoadCST()
                LoadMethodOfPayment()
                LoadPaymentTerms()
                LoadDeliverySchdule()
                LoadModeShiping()
                loadNumberOfDays()
                loadDescitionStart()
                BindTypeOfSale()
                BindCategoryOfSale()
                LoadChargeType()
                Dim iAID As String = "" : Dim iDashBoard As String = ""
                iDashBoard = Request.QueryString("sStrID")
                If iDashBoard = "1" Then
                    iAID = objClsFASGnrl.DecryptQueryString(Request.QueryString("AID"))
                    If iAID <> "AddNew" Then
                        'ExistingAllocationNo(0)
                        ddlExistingOrder.SelectedValue = objClsFASGnrl.DecryptQueryString(Request.QueryString("AID"))
                        ddlExistingOrder_SelectedIndexChanged(sender, e)
                    Else

                    End If
                ElseIf iDashBoard = "0" Then

                End If

                'If iDashBoard = "" Then
                '    Dim iAllocateID As String = ""
                '    iAllocateID = objClsFASGnrl.DecryptQueryString(Request.QueryString("AllocationID"))
                '    If iAllocateID <> "" Then


                '        '      lblReAllocateID.Text = iAllocateID
                '        ddlExistingOrder.SelectedValue = objClsFASGnrl.DecryptQueryString(Request.QueryString("AllocationID"))
                '        ddlExistingOrder_SelectedIndexChanged(sender, e)
                '    End If

                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindBranch()
        Try
            ddlBranch.DataSource = objOral.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranch.DataTextField = "Org_Name"
            ddlBranch.DataValueField = "Org_Node"
            ddlBranch.DataBind()
            ddlBranch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objOral.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "--- Select Zone ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnPrint_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPrint.Click
        Dim flag As Integer = 1
        Dim objPO As New clsPurchaseOrder
        Try
            If (ddlExistingOrder.SelectedIndex > 0) Then
                flag = objOral.GetPrintFlagValue(sSession.AccessCode, sSession.AccessCodeID)
                If (flag = 1) Then
                    lblError.Text = ""
                    Response.Redirect("~/Reports/Purchase/PurchaseSizeWise.aspx?ExistingOrder=" & ddlExistingOrder.SelectedValue)
                Else
                    lblError.Text = ""
                    Response.Redirect("~/Reports/Purchase/PurchaseItemWise.aspx?ExistingOrder=" & ddlExistingOrder.SelectedValue)
                End If
            End If
            If (ddlExistingOrder.SelectedIndex < 0) Then
                lblError.Text = "Select the existing order no"
                lblUserMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPrint_Click")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/Purchase/OralOrderMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub LoadMethodOfPayment()
        Try
            ddlMPayment.DataSource = objOral.LoadMethodOfPayment(sSession.AccessCode, sSession.AccessCodeID)
            ddlMPayment.DataTextField = "Mas_desc"
            ddlMPayment.DataValueField = "Mas_id"
            ddlMPayment.DataBind()
            ddlMPayment.Items.Insert(0, "Select Method Of Payment")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDeliverySchdule()
        Try
            ddlDSchedule.DataSource = objOral.LoadDeliverySchdule(sSession.AccessCode, sSession.AccessCodeID)
            ddlDSchedule.DataTextField = "Mas_desc"
            ddlDSchedule.DataValueField = "Mas_id"
            ddlDSchedule.DataBind()
            ddlDSchedule.Items.Insert(0, "Select Delivery Schdule")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSuppCode()
        Try
            txtSprCode.Text = "hello"
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadModeShiping()
        Try
            ddlModeOfShipping.DataSource = objOral.LoadModeShiping(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfShipping.DataTextField = "Mas_desc"
            ddlModeOfShipping.DataValueField = "Mas_id"
            ddlModeOfShipping.DataBind()
            ddlModeOfShipping.Items.Insert(0, "Select Mode of Shipping")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadNumberOfDays()
        Try
            ddlNumberOfDays.DataSource = objOral.loadNumberOfDays(sSession.AccessCode, sSession.AccessCodeID)
            ddlNumberOfDays.DataTextField = "Mas_desc"
            ddlNumberOfDays.DataValueField = "Mas_id"
            ddlNumberOfDays.DataBind()
            ddlNumberOfDays.Items.Insert(0, "Select Number Of days")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadPaymentTerms()
        Try
            ddlPterms.DataSource = objOral.LoadPaymentTerms(sSession.AccessCode, sSession.AccessCodeID)
            ddlPterms.DataTextField = "Mas_desc"
            ddlPterms.DataValueField = "Mas_id"
            ddlPterms.DataBind()
            ddlPterms.Items.Insert(0, "Select Payment Terms")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCodeAnddate()
        Try
            txtOrderCode.Text = objOral.GeneratePurchaseOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCodeAnddate")
        End Try
    End Sub
    Private Sub LoadSuppliers()
        Try
            ddlSupplier.DataSource = objOral.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplier.DataTextField = "CSM_Name"
            ddlSupplier.DataValueField = "CSM_ID"
            ddlSupplier.DataBind()
            ddlSupplier.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindTypeOfSale()
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
            ddlCstCtgry.Items.Add(New ListItem("CST", 2))
            ddlCstCtgry.Items.Add(New ListItem("H Form", 3))
            ddlCstCtgry.Items.Add(New ListItem("I Form", 4))
            ddlCstCtgry.Items.Add(New ListItem("F Form", 5))
            ddlCstCtgry.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingPurchaseOrder()
        Try
            ddlExistingOrder.DataSource = objOral.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
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
            ddlCommodity.DataSource = objOral.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
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
            'ddlVat.DataSource = objInvD.LoadVAT(sSession.AccessCode, sSession.AccessCodeID)
            'ddlVat.DataTextField = "Mas_Desc"
            'ddlVat.DataValueField = "Mas_ID"
            'ddlVat.DataBind()
            'ddlVat.Items.Insert(0, "Select VAT")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCST()
        Try
            'ddlCst.DataSource = objInvD.LoadCST(sSession.AccessCode, sSession.AccessCodeID)
            'ddlCst.DataTextField = "Mas_Desc"
            'ddlCst.DataValueField = "Mas_ID"
            'ddlCst.DataBind()
            'ddlCst.Items.Insert(0, "Select CST")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadDescitionStart()
        Try
            chkCategory.DataSource = objOral.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            chkCategory.DataTextField = "Inv_Code"
            chkCategory.DataValueField = "Inv_ID"
            chkCategory.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged

        Dim altPices As Integer
        Try

            If (chkCategory.SelectedValue > 0) Then
                ddlUnit.Items.Clear() : txtRate.Text = "" : txtReceivedQty.Text = "" : txtRejectedQty.Text = ""
                txtGST.Text = "" : txtGSTAmount.Text = "" : txtTotalAmount.Text = ""
                ddlCommodity.SelectedValue = objOral.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                txtGST.Text = objOral.GetGSTValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                'txtGSTAmount.Text = txtGST.Text

                'If (ddlTypeOfSale.SelectedIndex > 0) Then
                '    If (ddlTypeOfSale.SelectedIndex = 1) Then
                '        ddlCommodity.SelectedValue = objOral.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                '    ElseIf (ddlTypeOfSale.SelectedIndex = 2) Then
                '        If (ddlCstCtgry.SelectedIndex > 0) Then
                '            ddlCommodity.SelectedValue = objOral.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                '        Else
                '            Exit Sub
                '        End If
                '    End If
                'Else
                '    Exit Sub
                'End If
            End If
            lblDescID.Text = chkCategory.SelectedValue
            LoadDesciptionDetails()
            altPices = objOral.GetAlterNatePiceValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
            txtPices.Text = altPices
            ddlUnit.SelectedValue = objOral.GetUnitsValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
            hfTotalPieces.Value = txtPices.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        End Try
        'Dim altPices As Integer
        'Try

        '    If (chkCategory.SelectedValue > 0) Then
        '        ddlCommodity.SelectedValue = objOral.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
        '        If (ddlTypeOfSale.SelectedIndex > 0) Then
        '            If (ddlTypeOfSale.SelectedIndex = 1) Then
        '                ddlCommodity.SelectedValue = objOral.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
        '            ElseIf (ddlTypeOfSale.SelectedIndex = 2) Then
        '                If (ddlCstCtgry.SelectedIndex > 0) Then
        '                    ddlCommodity.SelectedValue = objOral.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
        '                Else
        '                    Exit Sub
        '                End If
        '            End If
        '        Else
        '            Exit Sub
        '        End If
        '    End If
        '    lblDescID.Text = chkCategory.SelectedValue
        '    LoadDesciptionDetails()
        '    altPices = objOral.GetAlterNatePiceValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
        '    txtPices.Text = altPices
        '    ddlUnit.SelectedValue = objOral.GetUnitsValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
        '    hfTotalPieces.Value = txtPices.Text
        'Catch ex As Exception
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        'End Try
    End Sub
    Private Sub LoadDesciptionDetails()
        Dim dt As New DataTable
        Dim sArray As Array
        Try
            ddlRate.DataSource = dt
            ddlRate.DataBind()
            txtRDate.Text = "" : txtRate.Text = "" : txtRateAmount.Text = ""
            txtQuantity.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            'txtExcise.Text = "" : txtExciseAmount.Text = ""
            'txtVatAmount.Text = "" : txtCSTAmount.Text = ""
            'txtTotalAmount.Text = ""

            If lblDescID.Text <> "0" Then
                dt = objOral.CheckDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
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
                    LoadCSTsingDate()
                    LoadVATUsingDate()
                    LoadExciseUsingDate()
                    GetOtherDetails(txtHistoryID.Text)
                Else

                    sArray = dt.Rows(0)(1).ToString().Split("-")
                    txtRate.Text = sArray(0)
                    txtHistoryID.Text = dt.Rows(0)(0).ToString()
                    LoadCSTsingDate()
                    LoadVATUsingDate()
                    LoadExciseUsingDate()
                    ddlRate.Enabled = False : txtRate.Enabled = True
                    If txtHistoryID.Text <> "" Then
                        '   GetPurchaseDetails(txtHistoryID.Text)
                        GetOtherDetails(txtHistoryID.Text)
                    End If
                End If
                loadunits(lblDescID.Text)
                'ddlUnit.DataSource = objOral.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                'ddlUnit.DataTextField = "Mas_Desc"
                'ddlUnit.DataValueField = "Mas_ID"
                'ddlUnit.DataBind()
                'ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDesciptionDetails")
        End Try
    End Sub
    Private Sub loadunits(ByVal descID As Integer)
        Try
            ddlUnit.DataSource = objOral.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, descID)
            ddlUnit.DataTextField = "Mas_Desc"
            ddlUnit.DataValueField = "Mas_ID"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetOtherDetails(ByVal iHistoryId As Integer)
        Dim dt As New DataTable
        Try
            dt = objOral.GetOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                    'txtExcise.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                Else
                    'txtExcise.Text = "0"
                End If
                If (ddlCstCtgry.SelectedValue = 1) Then
                    'ddlVat.SelectedIndex = 0
                    loadRegularUsingDate()
                    'ddlCst.Enabled = True
                    'If (ddlCst.Items.Count > 1) Then
                    '    ddlCst.SelectedIndex = 1
                    'End If
                ElseIf (ddlCstCtgry.SelectedValue = 2) Then
                    'ddlVat.SelectedIndex = 0
                    'ddlCst.Enabled = True
                    'If (ddlCst.Items.Count > 1) Then
                    '    ddlCst.SelectedIndex = 1
                    'End If
                ElseIf (ddlCstCtgry.SelectedValue = 3) Then
                    'ddlVat.SelectedIndex = 0
                    'ddlCst.SelectedIndex = 0
                    'ddlCst.Enabled = False
                    'ddlVat.Enabled = False
                ElseIf (ddlCstCtgry.SelectedValue = 4) Then
                    'ddlVat.SelectedIndex = 0
                    'ddlCst.SelectedIndex = 0
                    'ddlCst.Enabled = False
                    'ddlVat.Enabled = False
                ElseIf (ddlCstCtgry.SelectedValue = 5) Then
                    'ddlVat.SelectedIndex = 0
                    'ddlCst.SelectedIndex = 0
                    'ddlCst.Enabled = False
                    'ddlVat.Enabled = False
                Else
                    'ddlCst.SelectedIndex = 0
                    'ddlVat.Enabled = True
                    'ddlCst.Items.Clear()
                    'If (ddlVat.Items.Count > 1) Then
                    '    ddlVat.SelectedIndex = 1
                    'End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetOtherDetails")
        End Try

        'Dim dt As New DataTable
        'Try
        '    dt = objOral.GetOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
        '    If dt.Rows.Count > 0 Then
        '        If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
        '            txtExcise.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
        '        Else
        '            txtExcise.Text = "0"
        '        End If
        '        If (ddlCstCtgry.SelectedValue = 1) Then
        '            ddlVat.SelectedIndex = 0
        '            If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
        '                ddlCst.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
        '            Else
        '                ddlCst.SelectedValue = 0
        '            End If
        '        ElseIf (ddlCstCtgry.SelectedValue = 2) Then
        '            ddlVat.SelectedIndex = 0
        '            If IsDBNull(dt.Rows(0)("InvH_CST").ToString()) = False Then
        '                ddlCst.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 15, dt.Rows(0)("InvH_CST").ToString())
        '            Else
        '                ddlCst.SelectedValue = 0
        '            End If
        '        ElseIf (ddlCstCtgry.SelectedValue = 3) Then
        '            ddlVat.SelectedIndex = 0
        '            ddlCst.SelectedIndex = 0
        '            ddlCst.Enabled = False
        '            ddlVat.Enabled = False
        '        ElseIf (ddlCstCtgry.SelectedValue = 4) Then
        '            ddlVat.SelectedIndex = 0
        '            ddlCst.SelectedIndex = 0
        '            ddlCst.Enabled = False
        '            ddlVat.Enabled = False
        '        ElseIf (ddlCstCtgry.SelectedValue = 5) Then
        '            ddlVat.SelectedIndex = 0
        '            ddlCst.SelectedIndex = 0
        '            ddlCst.Enabled = False
        '            ddlVat.Enabled = False
        '        Else
        '            If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
        '                ddlVat.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
        '            End If

        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw
        ' End Try
    End Sub
    Public Sub LoadVATUsingDate()
        Try
            If (chkCategory.SelectedValue > 0) Then
                'ddlVat.Items.Clear()
                'ddlVat.Enabled = True
                'lblDescID.Text = chkCategory.SelectedValue
                'ddlVat.DataSource = objInvD.LoadVATUsingDate(sSession.AccessCode, sSession.AccessCodeID, txtOrderDate.Text, txtHistoryID.Text)
                'ddlVat.DataTextField = "Mas_Desc"
                'ddlVat.DataValueField = "Mas_ID"
                'ddlVat.DataBind()
                'ddlVat.Items.Insert(0, "Select VAT")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub LoadExciseUsingDate()
        Try
            If (chkCategory.SelectedValue > 0) Then
                'txtExcise.Text = objInvD.LoadExciseUsingDate(sSession.AccessCode, sSession.AccessCodeID, txtOrderDate.Text, txtHistoryID.Text)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExciseUsingDate")
        End Try
    End Sub
    Public Sub LoadCSTsingDate()
        Try
            If (chkCategory.SelectedValue > 0) Then
                '   lblDescID.Text = chkCategory.SelectedValue
                'ddlCst.Items.Clear()
                'ddlCst.Enabled = True
                'ddlCst.DataSource = objInvD.LoadCSTusingDate(sSession.AccessCode, sSession.AccessCodeID, txtOrderDate.Text, txtHistoryID.Text)
                'ddlCst.DataTextField = "Mas_Desc"
                'ddlCst.DataValueField = "Mas_ID"
                'ddlCst.DataBind()
                'ddlCst.Items.Insert(0, "Select CST")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub loadRegularUsingDate()
        Try
            'ddlCst.Items.Clear()
            '  LoadCSTsingDate()
            'ddlCst.Enabled = True
            'ddlCst.DataSource = objInvD.LoadVATUsingDate(sSession.AccessCode, sSession.AccessCodeID, txtOrderDate.Text, txtHistoryID.Text)
            'ddlCst.DataTextField = "Mas_Desc"
            'ddlCst.DataValueField = "Mas_ID"
            'ddlCst.DataBind()
            'ddlCst.Items.Insert(0, "--- Select CST ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function GetPurchaseDetails(ByVal iHistoryId As Integer) As Object
        Dim dt As New DataTable
        Try
            dt = objOral.GetPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                    'txtExcise.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                Else
                    'txtExcise.Text = "0"
                End If

                If (ddlCstCtgry.SelectedValue = 1) Then
                    'ddlVat.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        'ddlCst.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
                    Else
                        'ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCstCtgry.SelectedValue = 2) Then
                    If IsDBNull(dt.Rows(0)("InvH_CST").ToString()) = False Then
                        'ddlCst.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 15, dt.Rows(0)("InvH_CST").ToString())
                    Else
                        'ddlCst.SelectedValue = 0
                    End If
                ElseIf (ddlCstCtgry.SelectedValue = 3) Then
                    'ddlVat.SelectedIndex = 0
                    'ddlCst.SelectedIndex = 0
                ElseIf (ddlCstCtgry.SelectedValue = 4) Then
                    'ddlVat.SelectedIndex = 0
                    'ddlCst.SelectedIndex = 0
                ElseIf (ddlCstCtgry.SelectedValue = 5) Then
                    'ddlVat.SelectedIndex = 0
                    'ddlCst.SelectedIndex = 0
                Else
                    If IsDBNull(dt.Rows(0)("InvH_Vat").ToString()) = False Then
                        'ddlVat.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("InvH_Vat").ToString())
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
        Dim iChargeID As Integer = 0
        Dim dOrderDate As Date
        Dim Arr As String
        'Dim dRequiredDate As Date
        Dim lblChargeID As New Label, lblChargeType As New Label, lblChargeAmount As New Label
        Dim iBaseID As Integer = 0
        Dim sStr As String = ""
        Dim lblReceivedQty, lblRejectedQty As New Label
        Try
            If (ddlAccBrnch.SelectedIndex = 0) Then
                lblError.Text = "Select Branch."
                Exit Sub
            End If

            If (ddlExistingOrder.SelectedIndex > 0) Then
                sStr = objOral.CheckApprovedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                If sStr = "A" Then
                    lblError.Text = "Order number Already Approved,It can not be updated"
                    lblUserMasterDetailsValidationMsg.Text = lblStatus.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
            End If

            lblError.Text = ""
            dOrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objOral.sPOM_OrderNo = txtOrderCode.Text
            objOral.iPOM_Supplier = ddlSupplier.SelectedValue
            objOral.iPOM_ModeOfShipping = ddlModeOfShipping.SelectedValue
            objOral.iPOM_CreatedBy = sSession.UserID
            objOral.iPOM_UpdatedBy = sSession.UserID
            objOral.iPOM_YearID = sSession.YearID
            'objOral.iPOM_YearID = ddlNumberOfDays.SelectedValue
            If (ddlPterms.SelectedIndex > 0) Then
                objOral.iPOM_Paymentterms = ddlPterms.SelectedValue
            Else
                objOral.iPOM_Paymentterms = 0
            End If
            If (ddlMPayment.SelectedIndex > 0) Then
                objOral.iPOM_MethodofPayment = ddlMPayment.SelectedValue
            Else
                objOral.iPOM_MethodofPayment = 0
            End If
            If (ddlDSchedule.SelectedIndex > 0) Then
                objOral.iPOM_DSchdule = ddlDSchedule.SelectedValue
            Else
                objOral.iPOM_DSchdule = 0
            End If
            If (ddlModeOfShipping.SelectedIndex > 0) Then
                objOral.iPOM_ModeOfShipping = ddlModeOfShipping.SelectedValue
            Else
                objOral.iPOM_ModeOfShipping = 0
            End If
            If (ddlMPayment.SelectedIndex > 0) Then
                objOral.iPOM_MethodofPayment = ddlMPayment.SelectedValue
            Else
                objOral.iPOM_MethodofPayment = 0
            End If

            'If (ddlTypeOfSale.SelectedIndex > 0) Then
            '    objOral.iPOM_SaleType = ddlTypeOfSale.SelectedValue
            'Else
            objOral.iPOM_SaleType = 0
            'End If

            'If (ddlCstCtgry.SelectedIndex > 0) Then
            '    objOral.iPOM_iCSTCtgry = ddlCstCtgry.SelectedValue
            'Else
            objOral.iPOM_iCSTCtgry = 0
            objOral.POM_TrType = 1
            'End If
            objOral.sPOM_Status = "W"

            If hfTotalAmount.Value <> "" Then
                objOral.sPOD_TotalAmount = Request.Form(hfTotalAmount.UniqueID)
            Else
                objOral.sPOD_TotalAmount = objClsFASGnrl.SafeSQL(txtTotalAmount.Text)
            End If
            objOral.sOralOrPO = "O"

            If txtEsugamNo.Text <> "" Then
                objOral.sPOM_ESugam = objClsFASGnrl.SafeSQL(txtEsugamNo.Text)
            Else
                objOral.sPOM_ESugam = objClsFASGnrl.SafeSQL(txtEsugamNo.Text)
            End If
            If txtDcNo.Text <> "" Then
                objOral.sPOM_DcNo = objClsFASGnrl.SafeSQL(txtDcNo.Text)
            Else
                objOral.sPOM_DcNo = objClsFASGnrl.SafeSQL(txtDcNo.Text)
            End If
            If txtInvoiceRef.Text <> "" Then
                objOral.sPOM_InvoiceRef = objClsFASGnrl.SafeSQL(txtInvoiceRef.Text)
            Else
                objOral.sPOM_InvoiceRef = objClsFASGnrl.SafeSQL(txtInvoiceRef.Text)
            End If

            If txtinvoiceDate.Text <> "" Then
                objOral.DPOM_InvoiceDate = Date.ParseExact(Trim(txtinvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objOral.DPOM_InvoiceDate = Date.ParseExact(Trim("01/01/1900"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtCompanyAddress.Text <> "" Then
                objOral.POM_CompanyAddress = txtCompanyAddress.Text
            Else
                objOral.POM_CompanyAddress = ""
            End If

            If txtBillingAddress.Text <> "" Then
                objOral.POM_BillingAddress = txtBillingAddress.Text
            Else
                objOral.POM_BillingAddress = ""
            End If

            If txtDeliveryFromAddress.Text <> "" Then
                objOral.POM_DeliveryFrom = txtDeliveryFromAddress.Text
            Else
                objOral.POM_DeliveryFrom = ""
            End If

            If txtDeleveryAddress.Text <> "" Then
                objOral.POM_DeliveryAddress = txtDeleveryAddress.Text
            Else
                objOral.POM_DeliveryAddress = ""
            End If

            If txtCompanyGSTNRegNo.Text <> "" Then
                objOral.POM_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
            Else
                objOral.POM_CompanyGSTNRegNo = ""
            End If

            If txtBillingGSTNRegNo.Text <> "" Then
                objOral.POM_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
            Else
                objOral.POM_BillingGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" Then
                objOral.POM_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
            Else
                objOral.POM_DeliveryFromGSTNRegNo = ""
            End If

            If txtDeliveryGSTNRegNo.Text <> "" Then
                objOral.POM_DeliveryGSTNRegNo = txtDeliveryGSTNRegNo.Text
            Else
                objOral.POM_DeliveryGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                objOral.POM_PurchaseStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objOral.POM_PurchaseStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                objOral.POM_PurchaseStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objOral.POM_PurchaseStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            End If
            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
            '    objOral.POM_PurchaseStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            'End If

            'objOral.POM_CompanyType = ddlCompanyType.SelectedValue
            'objOral.POM_GSTNCategory = ddlGSTCategory.SelectedValue

            If ddlCompanyType.SelectedIndex = 0 And ddlGSTCategory.SelectedIndex = -1 Then
                lblError.Text = "Update the Company type and GSTN Category in Supplier Master Form"
                Exit Sub
            Else
                objOral.POM_CompanyType = ddlCompanyType.SelectedValue
                objOral.POM_GSTNCategory = ddlGSTCategory.SelectedValue
            End If

            objOral.POM_ZoneID = ddlAccZone.SelectedValue
            objOral.POM_RegionID = ddlAccRgn.SelectedValue
            objOral.POM_AreaID = ddlAccArea.SelectedValue
            'objOral.POM_BranchID = ddlAccBrnch.SelectedValue
            If ddlAccBrnch.SelectedIndex > 0 Then
                objOral.POM_BranchID = ddlAccBrnch.SelectedValue
            Else
                objOral.POM_BranchID = 0
            End If

            objOral.POM_BatchNo = 0
            objOral.POM_BaseName = 0

            iMasterID = objOral.SavePurchaseOrder(sSession.AccessCode, sSession.AccessCodeID, dOrderDate, objOral)
            txtMasterID.Text = iMasterID

            objOral.DeleteDetails(sSession.AccessCode, sSession.AccessCodeID, iMasterID, sSession.YearID)

            'objOral.iPOD_MasterID = iMasterID
            'objOral.iPOD_Commodity = ddlCommodity.SelectedValue
            'objOral.iPOD_DescriptionID = lblDescID.Text
            'objOral.iPOD_HistoryID = txtHistoryID.Text
            'objOral.iPOD_Unit = ddlUnit.SelectedValue
            'objOral.sPOD_Rate = Trim(txtRate.Text)
            'objOral.DPOM_ExpryDate = txtEdate.Text
            'objOral.DPOM_ManfctreDate = txtmdate.Text
            'objOral.sPOM_BatchNumber = txtBatchNumber.Text

            objOral.sPOD_Excise = "0"
            objOral.sPOD_ExciseAmount = "0"
            objOral.sPOD_VAT = "0"
            objOral.sPOD_VATAmount = "0"
            objOral.sPOD_CST = "0"
            objOral.sPOD_CSTAmount = "0"

            If txtEdate.Text <> "" Then
                objOral.DPOM_ExpryDate = Date.ParseExact(Trim(txtEdate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objOral.DPOM_ExpryDate = Date.ParseExact(Trim("01/01/1900"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtmdate.Text <> "" Then
                objOral.DPOM_ManfctreDate = Date.ParseExact(Trim(txtmdate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objOral.DPOM_ManfctreDate = "01/01/1900"
            End If

            If txtBatchNumber.Text <> "" Then
                objOral.sPOM_BatchNumber = txtBatchNumber.Text
            Else
                objOral.sPOM_BatchNumber = ""
            End If

            'If hfRateAmount.Value <> "" Then
            '    objOral.sPOD_RateAmount = Request.Form(hfRateAmount.UniqueID)
            'Else
            '    objOral.sPOD_RateAmount = objClsFASGnrl.SafeSQL(txtRateAmount.Text)
            'End If
            'If txtQuantity.Text = "" Then
            '    objOral.sPOD_Quantity = "0"
            'Else
            '    objOral.sPOD_Quantity = objClsFASGnrl.SafeSQL(txtQuantity.Text)
            'End If
            'If txtDiscount.Text = "" Then
            '    objOral.sPOD_Discount = "0"
            'Else
            '    objOral.sPOD_Discount = objClsFASGnrl.SafeSQL(txtDiscount.Text)
            'End If
            'If hfDiscountAmount.Value <> "" Then
            '    objOral.sPOD_DiscountAmount = Request.Form(hfDiscountAmount.UniqueID)
            'Else
            '    objOral.sPOD_DiscountAmount = objClsFASGnrl.SafeSQL(txtDiscountAmount.Text)
            'End If
            'If txtExcise.Text = "" Then
            '    objOral.sPOD_Excise = "0"
            'Else
            '    objOral.sPOD_Excise = objClsFASGnrl.SafeSQL(txtExcise.Text)
            'End If

            'If hfExciseAmount.Value <> "" Then
            '    objOral.sPOD_ExciseAmount = Request.Form(hfExciseAmount.UniqueID)
            'Else
            '    objOral.sPOD_ExciseAmount = objClsFASGnrl.SafeSQL(txtExciseAmount.Text)
            'End If


            If txtFreight.Text = "" Then
                objOral.sPOD_Frieght = "0"
            Else
                objOral.sPOD_Frieght = objClsFASGnrl.SafeSQL(txtFreight.Text)
            End If

            'If hfFreightAmount.Value <> "" Then
            '    objOral.sPOD_FrieghtAmount = "0"
            'Else
            '    objOral.sPOD_FrieghtAmount = objClsFASGnrl.SafeSQL(txtOtherCharge.Text)
            'End If


            'If hfFreightAmount.Value <> "" Then
            '    objOral.sPOD_FrieghtAmount = Request.Form(hfFreightAmount.UniqueID)
            'Else
            '    objOral.sPOD_FrieghtAmount = objClsFASGnrl.SafeSQL(txtFreightAmount.Text)
            'End If


            'If ddlVat.SelectedIndex > 0 Then

            '    objOral.sPOD_VAT = objClsFASGnrl.SafeSQL(ddlVat.SelectedItem.Text)
            'Else
            '    objOral.sPOD_VAT = "0"
            'End If

            'If hfVatAmount.Value <> "" Then
            '    objOral.sPOD_VATAmount = Request.Form(hfVatAmount.UniqueID)
            'Else
            '    objOral.sPOD_VATAmount = objClsFASGnrl.SafeSQL(txtVatAmount.Text)
            'End If

            'If ddlCst.SelectedIndex > 0 Then
            '    objOral.sPOD_CST = objClsFASGnrl.SafeSQL(ddlCst.SelectedItem.Text)

            'Else
            '    objOral.sPOD_CST = "0"
            'End If

            'If hfCSTAmount.Value <> "" Then
            '    objOral.sPOD_CSTAmount = Request.Form(hfCSTAmount.UniqueID)
            'Else
            '    objOral.sPOD_CSTAmount = objClsFASGnrl.SafeSQL(txtCSTAmount.Text)
            'End If

            If txtRDate.Text <> "" Then
                objOral.dPOD_RequiredDate = Date.ParseExact(Trim(txtRDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objOral.dPOD_RequiredDate = Date.ParseExact(Trim("01/01/1900"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            'If hfTotalAmount.Value <> "" Then
            '    objOral.sPOD_TotalAmount = Request.Form(hfTotalAmount.UniqueID)
            'Else
            '    objOral.sPOD_TotalAmount = objClsFASGnrl.SafeSQL(txtTotalAmount.Text)
            'End If

            'If txtReceivedQty.Text = "" Then
            '    objOral.POD_ReceivedQty = "0"
            'Else
            '    objOral.POD_ReceivedQty = objClsFASGnrl.SafeSQL(txtReceivedQty.Text)
            'End If

            'If txtRejectedQty.Text = "" Then
            '    objOral.POD_Rejected = "0"
            'Else
            '    objOral.POD_Rejected = objClsFASGnrl.SafeSQL(txtRejectedQty.Text)
            'End If

            'If txtQuantity.Text = "" Then
            '    objOral.POD_Accepted = "0"
            'Else
            '    objOral.POD_Accepted = objClsFASGnrl.SafeSQL(txtQuantity.Text)
            'End If


            If txtGST_ID.Text <> "" Then
                objOral.iPOD_GST_ID = txtGST_ID.Text
            Else
                objOral.iPOD_GST_ID = 0
            End If

            'If txtGST.Text <> "" Then
            '    objOral.dPOD_GSTRate = txtGST.Text
            'Else
            '    objOral.dPOD_GSTRate = 0
            'End If
            'If txtGSTAmount.Text <> "" Then
            '    objOral.dPOD_GSTAmount = txtGSTAmount.Text
            'Else
            '    objOral.dPOD_GSTAmount = 0
            'End If

            'objOral.dPOD_SGST = objOral.dPOD_GSTRate / 2
            'objOral.dPOD_SGSTAmount = objOral.dPOD_GSTAmount / 2
            'objOral.dPOD_CGST = objOral.dPOD_GSTRate / 2
            'objOral.dPOD_CGSTAmount = objOral.dPOD_GSTAmount / 2

            'objOral.dPOD_IGST = 0
            'objOral.dPOD_IGSTAmount = 0

            'objOral.POD_CreatedBy = sSession.UserID
            'objOral.POD_UpdatedBy = sSession.UserID

            'Dim sStatus As String = ""
            'objOral.SavePurchaseOrderDetails(sSession.AccessCode, sSession.AccessCodeID, objOral)
            '/////preeti***********************
            Dim lblCommodityID, lblDescriptionID, lblHistoryID, lblUnitsID, lnkGoods As New Label
            Dim Units, lblRate, Quantity, RateAmount, Discount, DiscountAmt, Charge, GSTRate, GSTAmount, TotalAmount, lblAcceptedQty As New Label
            Dim iPKID As Integer
            For i = 0 To dgPurchase.Rows.Count - 1

                objOral.iPOD_MasterID = iMasterID
                If txtItemTableID.Text <> "" Then
                    iPKID = txtItemTableID.Text
                Else
                    iPKID = 0
                End If

                lblCommodityID = dgPurchase.Rows(i).FindControl("lblCommodityID")
                lblDescriptionID = dgPurchase.Rows(i).FindControl("lblDescriptionID")
                lblHistoryID = dgPurchase.Rows(i).FindControl("lblHistoryID")
                lblUnitsID = dgPurchase.Rows(i).FindControl("lblUnitsID")

                If lblCommodityID.Text <> "" Then
                    objOral.iPOD_Commodity = lblCommodityID.Text
                Else
                    objOral.iPOD_Commodity = 0
                End If

                objOral.iPOD_DescriptionID = lblDescriptionID.Text
                If lblHistoryID.Text <> "" Then
                    objOral.iPOD_HistoryID = lblHistoryID.Text
                Else
                    objOral.iPOD_HistoryID = 0
                End If

                If lblUnitsID.Text <> "" Then
                    objOral.iPOD_Unit = lblUnitsID.Text
                Else
                    objOral.iPOD_Unit = 0
                End If

                lnkGoods = dgPurchase.Rows(i).FindControl("lnkGoods")
                'If lnkGoods.Text <> "" Then
                '    objOral.iPOD_DescriptionID = lnkGoods.Text
                'Else
                '    objOral.iPOD_DescriptionID = 0
                'End If

                'Units = dgPurchase.Rows(i).FindControl("Units")
                'If ddlUnit.SelectedValue <> "" Then
                '    objOral.iPOD_Unit = ddlUnit.SelectedValue
                'Else
                '    objOral.iPOD_Unit = 0
                'End If
                lblRate = dgPurchase.Rows(i).FindControl("lblRate")
                If lblRate.Text <> "" Then
                    objOral.sPOD_Rate = lblRate.Text
                Else
                    objOral.sPOD_Rate = 0
                End If
                Quantity = dgPurchase.Rows(i).FindControl("Quantity")
                If Quantity.Text <> "" Then
                    objOral.sPOD_Quantity = Quantity.Text
                Else
                    objOral.sPOD_Quantity = 0
                End If
                RateAmount = dgPurchase.Rows(i).FindControl("RateAmount")
                If RateAmount.Text <> "" Then
                    objOral.sPOD_RateAmount = RateAmount.Text
                Else
                    objOral.sPOD_RateAmount = 0
                End If
                Discount = dgPurchase.Rows(i).FindControl("Discount")
                If Discount.Text <> "" Then
                    objOral.sPOD_Discount = Discount.Text
                Else
                    objOral.sPOD_Discount = 0
                End If

                DiscountAmt = dgPurchase.Rows(i).FindControl("DiscountAmt")
                If DiscountAmt.Text <> "" Then
                    objOral.sPOD_DiscountAmount = DiscountAmt.Text
                Else
                    objOral.sPOD_DiscountAmount = 0
                End If

                Charge = dgPurchase.Rows(i).FindControl("Charge")
                If Charge.Text <> "" Then
                    objOral.sPOD_FrieghtAmount = Charge.Text
                Else
                    objOral.sPOD_FrieghtAmount = 0
                End If
                GSTRate = dgPurchase.Rows(i).FindControl("GSTRate")
                If GSTRate.Text <> "" Then
                    objOral.dPOD_GSTRate = GSTRate.Text
                Else
                    objOral.dPOD_GSTRate = 0
                End If

                GSTAmount = dgPurchase.Rows(i).FindControl("GSTAmount")
                If GSTAmount.Text <> "" Then
                    objOral.dPOD_GSTAmount = GSTAmount.Text
                Else
                    objOral.dPOD_GSTAmount = 0
                End If


                TotalAmount = dgPurchase.Rows(i).FindControl("TotalAmount")
                If TotalAmount.Text <> "" Then
                    objOral.sPOD_TotalAmount = TotalAmount.Text
                    objOral.sPOD_FETotalAmt = TotalAmount.Text
                Else
                    objOral.sPOD_TotalAmount = 0
                    objOral.sPOD_FETotalAmt = 0
                End If

                iBaseID = objPO.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
                objOral.iPOD_Currency = iBaseID
                objOral.iPOD_CurrencyAmt = 0
                objOral.sPOD_CurrencyTime = ""





                lblAcceptedQty = dgPurchase.Rows(i).FindControl("lblAcceptedQty")

                'If lblAcceptedQty.Text <> "" Then
                '    objOral.POD_Accepted = Quantity.Text
                'Else
                '    objOral.POD_Accepted = 0
                'End If

                If Quantity.Text <> "" Then
                    objOral.POD_Accepted = Quantity.Text
                Else
                    objOral.POD_Accepted = 0
                End If

                lblReceivedQty = dgPurchase.Rows(i).FindControl("lblReceivedQty")
                If lblReceivedQty.Text = "" Then
                    objOral.POD_ReceivedQty = "0"
                Else
                    objOral.POD_ReceivedQty = lblReceivedQty.Text
                End If

                lblRejectedQty = dgPurchase.Rows(i).FindControl("lblRejectedQty")
                If lblRejectedQty.Text = "" Then
                    objOral.POD_Rejected = "0"
                Else
                    objOral.POD_Rejected = lblRejectedQty.Text
                End If

                objOral.dPOD_SGST = objOral.dPOD_GSTRate / 2
                objOral.dPOD_SGSTAmount = objOral.dPOD_GSTAmount / 2
                objOral.dPOD_CGST = objOral.dPOD_GSTRate / 2
                objOral.dPOD_CGSTAmount = objOral.dPOD_GSTAmount / 2
                objOral.dPOD_IGST = 0
                objOral.dPOD_IGSTAmount = 0

                If UCase(ddlGSTCategory.SelectedItem.Text) = "UNRIGISTERED DEALER" Then
                    Dim URD_GSTRate, URD_GSTAmt As Double

                    URD_GSTRate = objOral.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblDescriptionID.Text)
                    URD_GSTAmt = (((objOral.sPOD_RateAmount - objOral.sPOD_DiscountAmount) + objOral.sPOD_FrieghtAmount) * URD_GSTRate) / 100

                    objOral.dPOD_SGST = URD_GSTRate / 2
                    objOral.dPOD_SGSTAmount = URD_GSTAmt / 2
                    objOral.dPOD_CGST = URD_GSTRate / 2
                    objOral.dPOD_CGSTAmount = URD_GSTAmt / 2
                    objOral.dPOD_IGST = 0
                    objOral.dPOD_IGSTAmount = 0
                End If

                objOral.POD_CreatedBy = sSession.UserID
                objOral.POD_UpdatedBy = sSession.UserID
                Arr = objOral.SavePurchaseOrderDetails(sSession.AccessCode, sSession.AccessCodeID, objOral, iPKID)
            Next

            Dim sStatus As String = ""
            sStatus = objOral.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)

            If sStatus = "W" Then
                lblError.Text = "Successfully Saved & Waiting for aprroval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Submission','', 'success');", True)
            End If
            lblStatus.Text = "Waiting for aprroval"

            SaveCharges(iMasterID)

            Session("dtPurchase") = objOral.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text, sSession.YearID)
            dgPurchase.DataSource = objOral.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text, sSession.YearID)
            dgPurchase.DataBind()

            LoadExistingPurchaseOrder()
            ddlExistingOrder.SelectedValue = iMasterID
            ClearAll()


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub SaveCharges(ByVal iMasterID As Integer)
        Dim Arr() As String
        Dim dChrge As Integer = 0
        Try
            'Deleting charges Everytime & Saving'
            objOral.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1

                    'objOral.C_ID = 0
                    'objOral.C_OrderID = 0
                    'objOral.C_AllocatedID = 0
                    'objOral.C_DispatchID = 0

                    objOral.C_POrderID = iMasterID
                    objOral.C_PGinID = 0
                    objOral.C_PInvoiceDocRef = 0
                    objOral.C_OrderType = ""
                    objOral.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objOral.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objOral.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objOral.C_PSType = "P"
                    objOral.C_DelFlag = "W"
                    objOral.C_Status = "C"
                    objOral.C_CompID = sSession.AccessCodeID
                    objOral.C_YearID = sSession.YearID
                    objOral.C_CreatedBy = sSession.UserID
                    objOral.C_CreatedOn = System.DateTime.Now
                    objOral.C_Operation = "C"

                    'objOral.C_UpdatedBy = sSession.UserID
                    'objOral.C_UpdatedOn = System.DateTime.Now

                    objOral.C_IPAddress = sSession.IPAddress
                    Arr = objOral.SaveCharges(sSession.AccessCode, objOral)

                Next
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveCharges")
        End Try
    End Sub
    Public Function CheckSourceDestinationOfDispatch(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            sSource = sBillingAddress.Substring(0, 2)
            sDestination = sDeliveryAddress.Substring(0, 2)

            If sSource = sDestination Then
                CheckSourceDestinationOfDispatch = "Local"
            Else
                CheckSourceDestinationOfDispatch = "Inter State"
            End If
            Return CheckSourceDestinationOfDispatch
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationOfDispatch")
        End Try
    End Function
    Public Sub ClearAll()
        Try
            For i = 0 To chkCategory.Items.Count - 1
                chkCategory.Items(i).Selected = False
            Next
            ddlUnit.Items.Clear()
            txtRate.Text = ""
            'hfCSTAmount.Value = ""
            'hfDiscountAmount.Value = ""
            'hfExciseAmount.Value = ""
            'hfVatAmount.Value = ""
            txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            'txtExcise.Text = "" : txtExciseAmount.Text = ""
            'txtVatAmount.Text = ""
            'txtCSTAmount.Text = ""
            txtRDate.Text = "" : txtTotalAmount.Text = ""
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
                For iIndx = 0 To dgPurchase.Rows.Count - 1
                    chkField = dgPurchase.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgPurchase.Rows.Count - 1
                    chkField = dgPurchase.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Protected Sub ddlExistingOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingOrder.SelectedIndexChanged
        Dim dt, dtDetails As New DataTable
        Dim dt1 As New DataTable
        Dim lnkDescription As New Label
        Dim lblcomodityID As New Label
        Dim lblDescriptionId As New Label
        Dim lblHistoryID As New Label : Dim otherchargeSum As Double
        Try
            lblError.Text = ""
            If ddlExistingOrder.SelectedIndex > 0 Then
                ddlCommodity.SelectedIndex = 0
                chkCategory.Items.Clear()

                ClearAll()

                dtDetails = objOral.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, sSession.YearID)
                Session("dtPurchase") = dtDetails
                ViewState("dt") = dtDetails
                dgPurchase.DataSource = objOral.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, sSession.YearID)

                dgPurchase.DataBind()
                dt = objOral.LoadPurchaseOderMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                If dt.Rows.Count > 0 Then

                    If IsDBNull(dt.Rows(0)("POM_ZoneID").ToString()) = False Then
                        If dt.Rows(0)("POM_ZoneID").ToString() = "" Then
                        Else
                            ddlAccZone.SelectedValue = dt.Rows(0)("POM_ZoneID").ToString()
                            LoadRegion(ddlAccZone.SelectedValue)
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("POM_RegionID").ToString()) = False Then
                        If dt.Rows(0)("POM_RegionID").ToString() = "" Then
                        Else
                            ddlAccRgn.SelectedValue = dt.Rows(0)("POM_RegionID").ToString()
                            LoadArea(ddlAccRgn.SelectedValue)
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("POM_AreaID").ToString()) = False Then
                        If dt.Rows(0)("POM_AreaID").ToString() = "" Then
                        Else
                            ddlAccArea.SelectedValue = dt.Rows(0)("POM_AreaID").ToString()
                            LoadAccBrnch(ddlAccArea.SelectedValue)
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("POM_BranchID").ToString()) = False Then
                        If dt.Rows(0)("POM_BranchID").ToString() = "" Then
                        Else
                            ddlAccBrnch.SelectedValue = dt.Rows(0)("POM_BranchID").ToString()
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("POM_BranchID").ToString()) = False Then
                        If dt.Rows(0)("POM_BranchID").ToString() = "" Then
                        Else
                            ddlBranch.SelectedValue = dt.Rows(0)("POM_BranchID").ToString()
                        End If
                    End If

                    If IsDBNull(dt.Rows(0)("POM_OrderNo").ToString()) = False Then
                        txtOrderCode.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("POM_OrderNo").ToString())
                    Else
                        txtOrderCode.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("POM_OrderDate").ToString()) = False Then
                        txtOrderDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POM_OrderDate").ToString(), "D")
                    Else
                        txtOrderDate.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("POM_Supplier").ToString()) = False Then
                        ddlSupplier.SelectedValue = dt.Rows(0)("POM_Supplier").ToString()
                        lblScode.Text = objOral.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
                    Else
                        ddlSupplier.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_ModeOfShipping").ToString()) = False Then
                        ddlModeOfShipping.SelectedValue = dt.Rows(0)("POM_ModeOfShipping").ToString()
                    Else
                        ddlModeOfShipping.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_Dschdule").ToString()) = False Then
                        ddlDSchedule.SelectedValue = dt.Rows(0)("POM_Dschdule").ToString()

                    Else
                        ddlDSchedule.SelectedIndex = 0
                    End If


                    'ddlNumberOfDays.SelectedValue = dt.Rows(0)("POM_Dschdule").ToString()
                    'loadNumberOfDays()

                    If IsDBNull(dt.Rows(0)("POM_PaymentTerms").ToString()) = False Then
                        ddlPterms.SelectedValue = dt.Rows(0)("POM_PaymentTerms").ToString()
                    Else
                        ddlPterms.SelectedIndex = 0
                    End If


                    If IsDBNull(dt.Rows(0)("POM_MPayment").ToString()) = False Then
                            ddlMPayment.SelectedValue = dt.Rows(0)("POM_MPayment").ToString()
                        Else
                            ddlMPayment.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(0)("POM_ID").ToString()) = False Then
                            txtMasterID.Text = dt.Rows(0)("POM_ID").ToString()
                        Else
                            txtMasterID.Text = 0
                        End If

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
                            ddlCstCtgry.SelectedValue = 0
                        End If

                        If IsDBNull(dt.Rows(0)("POM_Status").ToString()) = False Then
                            If (dt.Rows(0)("POM_Status") = "W") Then
                                lblStatus.Text = "Waiting For approval"
                            ElseIf dt.Rows(0)("POM_Status") = "A" Then
                                lblStatus.Text = "Approved."
                            Else
                            End If
                        End If

                        Dim dtCharge As New DataTable
                        dtCharge = objOral.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, 0, 0)
                        GvCharge.DataSource = dtCharge
                        GvCharge.DataBind()
                        Session("ChargesMaster") = dtCharge



                        If IsDBNull(dt.Rows(0)("POM_InvoiceRef").ToString()) = False Then
                            txtInvoiceRef.Text = dt.Rows(0)("POM_InvoiceRef").ToString()
                        Else
                            txtInvoiceRef.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POM_DcNo").ToString()) = False Then
                            txtDcNo.Text = dt.Rows(0)("POM_DcNo").ToString()
                        Else
                            txtDcNo.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POM_ESugamNo").ToString()) = False Then
                            txtEsugamNo.Text = dt.Rows(0)("POM_ESugamNo").ToString()
                        Else
                            txtEsugamNo.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POM_InvoiceDate").ToString()) = False Then
                            txtinvoiceDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POM_InvoiceDate").ToString(), "D")
                        Else
                            txtinvoiceDate.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POM_BillingAddress").ToString()) = False Then
                            txtBillingAddress.Text = dt.Rows(0)("POM_BillingAddress").ToString()
                        Else
                            txtBillingAddress.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POM_DeliveryFrom").ToString()) = False Then
                            txtDeliveryFromAddress.Text = dt.Rows(0)("POM_DeliveryFrom").ToString()
                        Else
                            txtDeliveryFromAddress.Text = ""
                        End If


                        If IsDBNull(dt.Rows(0)("POM_CompanyAddress").ToString()) = False Then
                            txtCompanyAddress.Text = dt.Rows(0)("POM_CompanyAddress").ToString()
                        Else
                            txtCompanyAddress.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POM_DeliveryAddress").ToString()) = False Then
                            txtDeleveryAddress.Text = dt.Rows(0)("POM_DeliveryAddress").ToString()
                        Else
                            txtDeleveryAddress.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POM_BillingGSTNRegNo").ToString()) = False Then
                            txtBillingGSTNRegNo.Text = dt.Rows(0)("POM_BillingGSTNRegNo").ToString()
                        Else
                            txtBillingGSTNRegNo.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POM_DeliveryFromGSTNRegNo").ToString()) = False Then
                            txtDeliveryFromGSTNRegNo.Text = dt.Rows(0)("POM_DeliveryFromGSTNRegNo").ToString()
                        Else
                            txtDeliveryFromGSTNRegNo.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POM_CompanyGSTNRegNo").ToString()) = False Then
                            txtCompanyGSTNRegNo.Text = dt.Rows(0)("POM_CompanyGSTNRegNo").ToString()
                        Else
                            txtCompanyGSTNRegNo.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POM_DeliveryGSTNRegNo").ToString()) = False Then
                            txtDeliveryGSTNRegNo.Text = dt.Rows(0)("POM_DeliveryGSTNRegNo").ToString()
                        Else
                            txtDeliveryGSTNRegNo.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POM_CompanyType")) = False Then
                            ddlCompanyType.SelectedValue = dt.Rows(0)("POM_CompanyType")
                        Else
                            ddlCompanyType.SelectedIndex = 0
                        End If

                    If IsDBNull(dt.Rows(0)("POM_GSTNCategory")) = False Then
                        BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                        ddlGSTCategory.SelectedValue = dt.Rows(0)("POM_GSTNCategory")
                    Else
                        ddlGSTCategory.SelectedIndex = 0
                    End If

                    Dim description As String
                    description = objOral.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryFromGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNo.Enabled = True
                    End If

                    'txtOtherCharge.Text = objOral.GetTotalCharge(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, 0, 0)

                    otherchargeSum = objOral.GetTotalCharge(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, 0, 0)
                    txtOtherCharge.Text = otherchargeSum

                    dt1 = objOral.LoadPurchase_OderDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, sSession.YearID)
                    If dt1.Rows.Count > 0 Then
                        If IsDBNull(dt1.Rows(0)("POD_Commodity").ToString()) = False Then
                            ddlCommodity.SelectedValue = dt1.Rows(0)("POD_Commodity").ToString()
                            chkCategory.DataSource = objOral.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
                            chkCategory.DataTextField = "Inv_Code"
                            chkCategory.DataValueField = "Inv_ID"
                            chkCategory.DataBind()
                            chkCategory.SelectedValue = dt1.Rows(0)("POD_DescriptionID").ToString()
                            lblDescID.Text = dt1.Rows(0)("POD_DescriptionID").ToString()
                            LoadDesciptionDetails()
                        Else
                            ddlCommodity.SelectedIndex = 0
                        End If
                    End If


                End If


                '    If IsDBNull(dt1.Rows(0)("POD_RequiredDate").ToString()) = False Then
                '        txtRDate.Text = dt1.Rows(0)("POD_RequiredDate").ToString()
                '    Else
                '        txtRDate.Text = "0"
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_ReceivedQty").ToString()) = False Then
                '        txtReceivedQty.Text = dt1.Rows(0)("POD_ReceivedQty").ToString()
                '    Else
                '        txtReceivedQty.Text = "0"
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_RejectedQty").ToString()) = False Then
                '        txtRejectedQty.Text = dt1.Rows(0)("POD_RejectedQty").ToString()
                '    Else
                '        txtRejectedQty.Text = "0"
                '    End If
                '    If IsDBNull(dt1.Rows(0)("POD_HistoryID").ToString()) = False Then
                '        txtHistoryID.Text = dt1.Rows(0)("POD_HistoryID").ToString()
                '    Else
                '        txtHistoryID.Text = "0"
                '    End If

                '    'If IsDBNull(dt1.Rows(0)("POD_Unit").ToString()) = False Then
                '    '    If dt1.Rows(0)("POD_Unit").ToString() = "" Then
                '    '    Else
                '    '        ddlUnit.SelectedValue = dt1.Rows(0)("POD_Unit").ToString()
                '    '        loadunits(lblDescID.Text)
                '    '    End If
                '    'End If

                '    'If IsDBNull(dt1.Rows(0)("POD_Unit").ToString()) = False Then
                '    '    ddlUnit.SelectedValue = dt1.Rows(0)("POD_Unit").ToString()
                '    '    loadunits(lblDescID.Text)
                '    'End If

                '    If IsDBNull(dt1.Rows(0)("POD_Rate").ToString()) = False Then
                '        txtRate.Text = dt1.Rows(0)("POD_Rate").ToString()
                '    Else
                '        txtRate.Text = ""
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_RateAmount").ToString()) = False Then
                '        txtRateAmount.Text = dt1.Rows(0)("POD_RateAmount").ToString()
                '    Else
                '        txtRateAmount.Text = ""
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_Quantity").ToString()) = False Then
                '        txtQuantity.Text = dt1.Rows(0)("POD_Quantity").ToString()
                '    Else
                '        txtQuantity.Text = ""
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_Discount").ToString()) = False Then
                '        txtDiscount.Text = dt1.Rows(0)("POD_Discount").ToString()
                '    Else
                '        txtDiscount.Text = ""
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_DiscountAmount").ToString()) = False Then
                '        txtDiscountAmount.Text = dt1.Rows(0)("POD_DiscountAmount").ToString()
                '    Else
                '        txtDiscountAmount.Text = ""
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_GSTRate")) = False Then
                '        txtGST.Text = dt1.Rows(0)("POD_GSTRate").ToString()
                '    Else
                '        txtGST.Text = ""
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_GSTAmount")) = False Then
                '        txtGSTAmount.Text = dt1.Rows(0)("POD_GSTAmount").ToString()
                '    Else
                '        txtGSTAmount.Text = ""
                '    End If


                '    If IsDBNull(dt1.Rows(0)("POD_Frieght").ToString()) = False Then
                '        txtFreight.Text = dt1.Rows(0)("POD_Frieght").ToString()
                '    Else
                '        txtFreight.Text = ""
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_FrieghtAmount").ToString()) = False Then
                '        txtFreightAmount.Text = dt1.Rows(0)("POD_FrieghtAmount").ToString()
                '    Else
                '        txtFreightAmount.Text = ""
                '    End If

                '    If IsDBNull(dt1.Rows(0)("POD_ExpiryDate").ToString()) = False Then

                '        If objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(0)("POD_ExpiryDate").ToString(), "D") = "01/01/1900" Then
                '            txtRDate.Text = ""
                '        Else
                '            txtRDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(0)("POD_ExpiryDate").ToString(), "D")
                '        End If

                '    Else
                '        txtEdate.Text = ""
                '    End If
                '    If IsDBNull(dt1.Rows(0)("POD_ManufactureDate").ToString()) = False Then

                '        If objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(0)("POD_ManufactureDate").ToString(), "D") = "01/01/1900" Then
                '            txtmdate.Text = ""
                '        Else
                '            txtmdate.Text = objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(0)("POD_ManufactureDate").ToString(), "D")
                '        End If
                '    Else
                '        txtFreightAmount.Text = ""
                '    End If
                '    If IsDBNull(dt1.Rows(0)("POD_BatchNumber").ToString()) = False Then
                '        txtBatchNumber.Text = dt1.Rows(0)("POD_BatchNumber").ToString()
                '    Else
                '        txtBatchNumber.Text = ""
                '    End If

                '    If objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(0)("POD_ExpiryDate").ToString(), "D") = "01/01/1900" Then
                '        txtEdate.Text = ""
                '    Else
                '        txtEdate.Text = objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(0)("POD_ExpiryDate").ToString(), "D")
                '    End If
                '    If IsDBNull(dt1.Rows(0)("POD_TotalAmount").ToString()) = False Then
                '        txtTotalAmount.Text = dt1.Rows(0)("POD_TotalAmount").ToString()
                '    Else
                '        txtTotalAmount.Text = ""
                '    End If
                'End If
            Else
                txtOrderCode.Text = "" : txtOrderDate.Text = "" : lblScode.Text = "" : ddlSupplier.SelectedIndex = 0
                ddlModeOfShipping.SelectedIndex = 0 : txtMasterID.Text = 0 : txtRateAmount.Text = ""
                GenerateOrderCodeAnddate()


                dgPurchase.DataSource = dt
                dgPurchase.DataBind()

            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingOrder_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub dgPurchase_PreRender(sender As Object, e As EventArgs) Handles dgPurchase.PreRender
        Dim dt As New DataTable
        Try
            If dgPurchase.Rows.Count > 0 Then
                dgPurchase.UseAccessibleHeader = True
                dgPurchase.HeaderRow.TableSection = TableRowSection.TableHeader
                dgPurchase.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_PreRender")
        End Try
    End Sub
    Protected Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            ddlRate.Enabled = False : txtRate.Enabled = False
            If ddlCommodity.SelectedIndex > 0 Then
                chkCategory.DataSource = objOral.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
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

    'Protected Sub dgPurchase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchase.RowDataBound
    '    Dim imgbtnDelete As New ImageButton, imgbtnEdit As New ImageButton
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        imgbtnDelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
    '        imgbtnDelete.ImageUrl = "~/Images/DeActivate16.png"
    '        imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
    '        imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
    '    End If
    'End Sub
    Protected Sub dgPurchase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchase.RowDataBound
        Dim imgbtnDelete As New ImageButton, imgbtnEdit As New ImageButton
        Dim lblCommodityID As New Label, lblDescriptionID As New Label, lblHistoryID As New Label
        Dim lblUnitsID, lnkGoods, Units, lblRate, Quantity, RateAmount, Discount, DiscountAmt As New Label
        Dim Charge, GSTRate, GSTAmount, TotalAmount, lblAcceptedQty As New Label
        Dim dMRP, dQty, dRateAmt, dDis, dDisAmt, dCharge, dGST, dItemTotal As Double
        Dim lblReceivedQty, lblRejectedQty As New Label

        Dim dtData As New DataTable
        Dim iPO As Integer = 0
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnDelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtnDelete.ImageUrl = "~/Images/DeActivate16.png"
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
            End If

            dgPurchase.Columns(36).Visible = False
            If sOOSave = "YES" Then
                dgPurchase.Columns(36).Visible = True
            End If

            '//////////////Preeti

            lblError.Text = ""
            dtPurchase = Session("dtPurchase")
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then

                lblCommodityID = CType(e.Row.FindControl("lblCommodityID"), Label)
                lblDescriptionID = CType(e.Row.FindControl("lblDescriptionID"), Label)
                lblHistoryID = CType(e.Row.FindControl("lblHistoryID"), Label)
                lblUnitsID = CType(e.Row.FindControl("lblUnitsID"), Label)
                lnkGoods = CType(e.Row.FindControl("lblDescriptionID"), Label)
                lblRate = CType(e.Row.FindControl("lblRate"), Label)
                Quantity = CType(e.Row.FindControl("Quantity"), Label)

                lblReceivedQty = CType(e.Row.FindControl("lblReceivedQty"), Label)
                lblRejectedQty = CType(e.Row.FindControl("lblRejectedQty"), Label)

                RateAmount = CType(e.Row.FindControl("RateAmount"), Label)
                Discount = CType(e.Row.FindControl("Discount"), Label)
                DiscountAmt = CType(e.Row.FindControl("DiscountAmt"), Label)
                Charge = CType(e.Row.FindControl("Charge"), Label)
                GSTRate = CType(e.Row.FindControl("GSTRate"), Label)
                GSTAmount = CType(e.Row.FindControl("GSTAmount"), Label)
                TotalAmount = CType(e.Row.FindControl("TotalAmount"), Label)
                lblAcceptedQty = CType(e.Row.FindControl("lblAcceptedQty"), Label)

                If ddlExistingOrder.SelectedIndex > 0 Then
                    iPO = ddlExistingOrder.SelectedValue
                End If
                If ddlExistingOrder.SelectedIndex > 0 Then

                    dtData = objOral.BindSavedData(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, lblCommodityID.Text, lblUnitsID.Text, lblHistoryID.Text, sSession.YearID)
                    If dtData.Rows.Count > 0 Then
                        For m = 0 To dtData.Rows.Count - 1
                            'lblUnitsID.Text = objDb.SQLGetDescription(sSession.AccessCode, "Select Mas_Desc From Acc_General_master Where Mas_ID=" & dtData.Rows(m)("POD_Unit") & " And MAS_CompID=" & sSession.AccessCodeID & " And MAS_DelFlag='A' And MAS_Master in(Select MAS_ID From Acc_Master_Type where Mas_Type='Unit of Measurement') ")
                            lblUnitsID.Text = dtData.Rows(m)("POD_Unit")
                            lblCommodityID.Text = dtData.Rows(m)("POD_Commodity")
                            'objDb.SQLGetDescription(sSession.AccessCode, "Select INV_Description From Inventory_master Where INV_ID=" & lblCommodityID.Text & " And INV_Parent=0 And INV_CompID=" & sSession.AccessCodeID & " ")
                            'lnkGoods.Text = objDb.SQLGetDescription(sSession.AccessCode, "Select INV_Description From Inventory_master Where INV_ID=" & dtData.Rows(m)("POD_DescriptionID") & " And INV_Parent=" & lblCommodityID.Text & " And INV_CompID=" & sSession.AccessCodeID & " ")
                            lnkGoods.Text = dtData.Rows(m)("POD_DescriptionID")
                            lblHistoryID.Text = dtData.Rows(m)("POD_HistoryID")
                            lblDescriptionID.Text = dtData.Rows(m)("POD_DescriptionID")
                            lblRate.Text = dtData.Rows(m)("POD_Rate")
                            Quantity.Text = dtData.Rows(m)("POD_Quantity")
                            RateAmount.Text = dtData.Rows(m)("POD_RateAmount")
                            Discount.Text = dtData.Rows(m)("POD_Discount")
                            DiscountAmt.Text = dtData.Rows(m)("POD_DiscountAmount")
                            Charge.Text = dtData.Rows(m)("POD_FrieghtAmount")
                            GSTRate.Text = dtData.Rows(m)("POD_GSTRate")
                            GSTAmount.Text = dtData.Rows(m)("POD_GSTAmount")
                            TotalAmount.Text = dtData.Rows(m)("POD_TotalAmount")
                            lblAcceptedQty.Text = dtData.Rows(m)("POD_AcceptedQty")
                            lblRejectedQty.Text = dtData.Rows(m)("POD_RejectedQty")
                            lblReceivedQty.Text = dtData.Rows(m)("POD_ReceivedQty")


                        Next
                    End If
                    Dim sStatus As String = ""
                    sStatus = objOral.Getstatuus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)

                    If sStatus = "S" Or sStatus = "A" Then
                        imgbtnDelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                        imgbtnDelete.Enabled = False
                    Else
                        If txtOtherCharge.Text <> "" Then
                            If dtPurchase.Rows.Count > 0 Then
                                For i = 0 To dtPurchase.Rows.Count - 1
                                    dItemTotal = dItemTotal + dtPurchase.Rows(i)("RateAmount")
                                Next
                                dCharge = (RateAmount.Text * txtOtherCharge.Text) / (dItemTotal)
                                Charge.Text = String.Format("{0:0.00}", Convert.ToDecimal(dCharge))

                                TotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((RateAmount.Text - DiscountAmt.Text) + dCharge)))
                                GSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((TotalAmount.Text) * GSTRate.Text) / 100))
                                TotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(Convert.ToDecimal(TotalAmount.Text) + Convert.ToDecimal(GSTAmount.Text)))
                            End If
                        End If
                    End If
                Else
                    dMRP = lblRate.Text
                    dQty = Quantity.Text
                    dRateAmt = RateAmount.Text
                    dDis = Discount.Text
                    dDisAmt = DiscountAmt.Text
                    dGST = GSTRate.Text
                    If txtOtherCharge.Text <> "" Then
                        If dtPurchase.Rows.Count > 0 Then
                            For i = 0 To dtPurchase.Rows.Count - 1
                                dItemTotal = dItemTotal + dtPurchase.Rows(i)("RateAmount")
                            Next
                            dCharge = (RateAmount.Text * txtOtherCharge.Text) / (dItemTotal)
                            Charge.Text = String.Format("{0:0.00}", Convert.ToDecimal(dCharge))

                            TotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((RateAmount.Text - DiscountAmt.Text) + dCharge)))
                            GSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((TotalAmount.Text) * GSTRate.Text) / 100))
                            TotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(Convert.ToDecimal(TotalAmount.Text) + Convert.ToDecimal(GSTAmount.Text)))
                        End If
                    End If
                End If
            End If
            '//////////////Preeti
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowDataBound")
        End Try

    End Sub
    Protected Sub ddlSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSupplier.SelectedIndexChanged
        Dim dtCustomer As New DataTable
        Try
            If ddlSupplier.SelectedIndex > 0 Then
                txtSprCode.Text = objOral.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
                txtQuantity.Text = ""
                txtDiscount.Text = ""
                dtCustomer = objOral.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
                If dtCustomer.Rows.Count > 0 Then
                    'txtBillingAddress.Text = dtCustomer.Rows(0)("CSM_Address")
                    'txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("CSM_GSTNRegNo")
                    'ddlCompanyType.SelectedValue = dtCustomer.Rows(0)("CSM_CompanyType")

                    If IsDBNull(dtCustomer.Rows(0)("CSM_CompanyType")) = False Then
                        If dtCustomer.Rows(0)("CSM_CompanyType") > 0 Then
                            ddlCompanyType.SelectedValue = dtCustomer.Rows(0)("CSM_CompanyType")
                        Else
                            ddlCompanyType.SelectedIndex = 0
                        End If
                    Else
                        ddlCompanyType.SelectedIndex = 0
                End If

                If IsDBNull(dtCustomer.Rows(0)("CSM_GSTNRegNo")) = False Then
                    txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("CSM_GSTNRegNo")
                Else
                    txtBillingGSTNRegNo.Text = ""
                End If

                    If IsDBNull(dtCustomer.Rows(0)("CSM_Address")) = False Then
                        txtBillingAddress.Text = dtCustomer.Rows(0)("CSM_Address")
                    Else
                        txtBillingAddress.Text = ""
                    End If

                    If ddlCompanyType.SelectedIndex > 0 Then
                        BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                        ddlGSTCategory.SelectedValue = dtCustomer.Rows(0)("CSM_GSTNCategory")

                        Dim description As String
                        description = objOral.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                        If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                            txtDeliveryFromGSTNRegNo.Enabled = False
                        Else
                            txtDeliveryFromGSTNRegNo.Enabled = True
                        End If

                    Else
                        ddlGSTCategory.Items.Clear()
                    End If
                End If
                txtDeliveryFromAddress.Text = txtBillingAddress.Text
                txtDeliveryFromGSTNRegNo.Text = txtBillingGSTNRegNo.Text
            Else
                lblScode.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSupplier_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindCompanyType()
        Try
            ddlCompanyType.DataSource = objOral.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyType.DataTextField = "Mas_Desc"
            ddlCompanyType.DataValueField = "Mas_Id"
            ddlCompanyType.DataBind()
            ddlCompanyType.Items.Insert(0, "Select Company Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindGSTNCategory(ByVal sCompanyType As String)
        Dim dt As New DataTable
        Try
            dt = objOral.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategory.DataSource = dt
            ddlGSTCategory.DataTextField = "GC_GSTCategory"
            ddlGSTCategory.DataValueField = "GC_Id"
            ddlGSTCategory.DataBind()
            ddlGSTCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub txtOrderCode_TextChanged(sender As Object, e As EventArgs) Handles txtOrderCode.TextChanged

    End Sub
    Protected Sub txtHistoryID_TextChanged(sender As Object, e As EventArgs) Handles txtHistoryID.TextChanged

    End Sub
    Protected Sub ddlTypeOfSale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTypeOfSale.SelectedIndexChanged
        'Try
        '    ClearAll()
        '    If (ddlTypeOfSale.SelectedValue = 2) Then
        '        ddlCstCtgry.Enabled = True
        '        ddlVat.Enabled = False
        '        ddlCst.Enabled = True
        '    Else
        '        ddlCstCtgry.Enabled = False
        '        ddlVat.Enabled = True
        '        ddlCst.Enabled = False
        '        ddlCstCtgry.SelectedIndex = 0
        '    End If
        'Catch ex As Exception

        'End Try
        Try
            If (ddlTypeOfSale.SelectedValue = 2) Then
                ddlCstCtgry.Enabled = True
                'ddlVat.Enabled = False
                'ddlCst.Enabled = False
                ClearAll()
                If (Trim(txtOrderDate.Text) = "") Then
                    lblUserMasterDetailsValidationMsg.Text = "Select Order Date."
                    ddlTypeOfSale.SelectedIndex = 0
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                ClearAll()
                ddlCstCtgry.Enabled = False
                'ddlVat.Enabled = False
                'ddlCst.Enabled = False
                ddlCstCtgry.SelectedIndex = 0
                If (Trim(txtOrderDate.Text) = "") Then
                    lblUserMasterDetailsValidationMsg.Text = "Select Order Date."
                    ddlTypeOfSale.SelectedIndex = 0
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
                ' LoadCSTsingDate()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTypeOfSale_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlCstCtgry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCstCtgry.SelectedIndexChanged

        Try
            ClearAll()
            If (ddlCstCtgry.SelectedValue = 1) Then
                'loadRegular()
            ElseIf (ddlCstCtgry.SelectedValue = 2) Then
                ' LoadCST()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCstCtgry_SelectedIndexChanged")
        End Try

        'Try
        '    ClearAll()
        '    If (ddlCstCtgry.SelectedValue = 1) Then
        '        loadRegular()
        '    ElseIf (ddlCstCtgry.SelectedValue = 2) Then
        '        LoadCST()
        '    End If
        'Catch ex As Exception

        'End Try

    End Sub


    Public Function SaveInward() As Integer
        Dim Arr() As String
        Dim ObjGoods As New ClsGoodsInward
        Dim iMasterID As Integer
        Dim sCurrentMonth As String = "", sYear As String = "", sStaus As String = "", sStatus As String = "", Check As String
        Dim ddlUnit As New DropDownList
        Dim lblMRP As New Label
        Dim lblPending As New Label
        Dim lblOrderedQuantity As New Label, lblComodityId As New Label, lblDescription As Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label, UnitsID As New Label
        Dim lblReceivedQuantity As New Label
        Dim txtBatchNumber As New Label
        Dim lblAcceptedQuantity As New Label
        Dim lblRejectedQty As New Label
        Dim lblRejectedQuantityExcess As New Label
        Dim lblRemarks As New TextBox
        Dim txtManufactureDate As New Label
        Dim txtExpireDate As New Label
        Dim lblBatchNo As New TextBox
        Dim lblRate As New TextBox
        Dim row As GridViewRow
        Dim sCurrentMonthID As String


        Dim lblCommodityID, lblDescriptionID, lblUnitsID, lnkGoods As New Label
        Dim Units, Quantity, RateAmount, Discount, DiscountAmt, Charge, GSTRate, GSTAmount, TotalAmount, lblAcceptedQty As New Label

        Try
            lblStatus.Text = ""
            lblError.Text = ""
            If (txtDcNo.Text = "") Then
                lblError.Text = "Enter Document reference"
                ' btnSave.Visible = True
                Exit Function
            Else
                If (objGin.CheckVerifiedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, txtDcNo.Text)) Then
                    lblError.Text = "Already verified "
                    Exit Function
                End If
            End If
            Check = objGin.CheckInwardedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, txtDcNo.Text)
            If (Check = True) Then
                lblError.Text = "Document reference no already exist"
            Else
                If dgPurchase.Rows.Count > 0 Then
                    sCurrentMonthID = objGin.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnction.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnction.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    'ObjGoods.PGM_OrderDate = Date.ParseExact(Trim("28/02/2016"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ObjGoods.PGM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ObjGoods.PGM_DocRefNo = objClsFASGnrl.SafeSQL(txtDcNo.Text)
                    ObjGoods.PGM_CreatedBy = sSession.UserID
                    ObjGoods.PGM_ESugamNo = objClsFASGnrl.SafeSQL(txtEsugamNo.Text)
                    ObjGoods.PGM_Supplier = objGin.GetSupplierName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                    'If (ddlExistingDocRef.SelectedIndex > 0) Then
                    ObjGoods.PGM_DocRefNo = txtInvoiceRef.Text
                    ' Else
                    'ObjGoods.PGM_DocRefNo = objFasGnrl.SafeSQL(txtDocRefNo.Text)
                    'End If
                    ObjGoods.GIND_DCNO = txtDcNo.Text
                    ObjGoods.PGM_ID = 0
                    ObjGoods.PGM_CompID = sSession.AccessCodeID
                    ObjGoods.PGM_CrBy = sSession.UserID
                    ObjGoods.PGM_CrOn = DateTime.Today
                    ObjGoods.PGM_Status = "A"
                    ObjGoods.PGM_DelFlag = "X"
                    ObjGoods.PGM_YearID = sSession.YearID
                    ObjGoods.PGM_Operation = "C"
                    ObjGoods.PGM_IPAddress = sSession.IPAddress
                    ObjGoods.PGM_Gin_Number = objGin.GenerateInwardCode(sSession.AccessCode, sSession.AccessCodeID)
                    'objClsFASGnrl.SafeSQL(txtOrderCode.Text)
                    ObjGoods.PGM_ModeOfShiping = ddlModeOfShipping.SelectedValue 'lblMShiping.Text
                    ObjGoods.PGM_InvoiceDate = Date.ParseExact(Trim(txtinvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    ObjGoods.PGM_ESugamNo = objClsFASGnrl.SafeSQL(txtEsugamNo.Text)
                    ObjGoods.PGM_OrderID = ddlExistingOrder.SelectedValue

                    ObjGoods.PGM_BatchNo = 0
                    ObjGoods.PGM_BaseName = 0
                    ObjGoods.PGM_OrderNo = ""

                    Arr = objGin.SaveMaster(sSession.AccessCode, ObjGoods, 0)
                    iMasterID = Arr(1)
                    For i = 0 To dgPurchase.Rows.Count - 1

                        lblReceivedQuantity = dgPurchase.Rows(i).FindControl("lblReceivedQty")
                        If (lblReceivedQuantity.Text = "") Then
                            lblReceivedQuantity.Text = 0
                        Else
                            ObjGoods.PGD_ReceivedQnt = lblReceivedQuantity.Text
                        End If
                        If (Convert.ToDecimal(lblReceivedQuantity.Text) > 0) Then
                            ObjGoods.PGD_MasterID = iMasterID
                            lblMRP = dgPurchase.Rows(i).FindControl("lblRate")
                            If lblMRP.Text <> "" Then
                                ObjGoods.PGD_MRP = lblMRP.Text
                            Else
                                ObjGoods.PGD_MRP = 0
                            End If

                            lblUnitId = dgPurchase.Rows(i).FindControl("lblUnitsID")
                            If lblUnitId.Text <> "" Then
                                ObjGoods.PGD_UnitID = lblUnitId.Text 'objGIN.GetUnitID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescription.Text)
                            Else
                                ObjGoods.PGD_UnitID = 0
                            End If

                            lblHistoryID = dgPurchase.Rows(i).FindControl("lblHistoryID")
                            If lblHistoryID.Text <> "" Then
                                ObjGoods.PGD_HistoryID = lblHistoryID.Text
                            Else
                                ObjGoods.PGD_DescriptionID = 0
                            End If

                            lblItemId = dgPurchase.Rows(i).FindControl("lblDescriptionID")
                            If lblItemId.Text <> "" Then
                                ObjGoods.PGD_DescriptionID = lblItemId.Text
                            End If

                            lblComodityId = dgPurchase.Rows(i).FindControl("lblCommodityID")
                            If lblComodityId.Text <> "" Then
                                ObjGoods.PGD_CommodityID = lblComodityId.Text
                            End If

                            Quantity = dgPurchase.Rows(i).FindControl("Quantity")

                            If Quantity.Text <> "" Then
                                ObjGoods.PGD_OrderQnt = Quantity.Text
                            Else
                                ObjGoods.PGD_OrderQnt = 0
                            End If
                            'ObjGoods.PGD_OrderQnt = 'lblOrderedQuantity.Text

                            lblAcceptedQty = dgPurchase.Rows(i).FindControl("lblAcceptedQty")
                            If lblAcceptedQty.Text <> "" Then
                                ObjGoods.PGD_Accepted = lblAcceptedQty.Text
                            Else
                                ObjGoods.PGD_Accepted = 0
                            End If


                            'If lblAcceptedQuantity.Text <> "" Then
                            '    ObjGoods.PGD_ReceivedQnt = lblAcceptedQuantity.Text
                            'Else
                            '    ObjGoods.PGD_ReceivedQnt = 0
                            'End If
                            lblRejectedQty = dgPurchase.Rows(i).FindControl("lblRejectedQty")
                            If lblRejectedQty.Text <> "" Then
                                ObjGoods.PGD_RejectedQnt = lblRejectedQty.Text
                            Else
                                ObjGoods.PGD_RejectedQnt = 0
                            End If
                            ObjGoods.PGD_Excess = 0 'lblRejectedQuantityExcess.Text
                            txtManufactureDate = dgPurchase.Rows(i).FindControl("lblPOD_ManufactureDate")
                            If txtManufactureDate.Text <> "" Then
                                ObjGoods.PGD_ManufactureDate = Date.ParseExact(Trim(txtManufactureDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Else
                                ObjGoods.PGD_ManufactureDate = "01/01/1900"
                            End If
                            txtExpireDate = dgPurchase.Rows(i).FindControl("lblPOD_ExpiryDate")
                            If txtExpireDate.Text <> "" Then
                                ObjGoods.PGD_ExpireDate = Date.ParseExact(Trim(txtExpireDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Else
                                ObjGoods.PGD_ExpireDate = "01/01/1900"
                            End If

                            '   lblPending = dgPurchase.Rows(i).FindControl("lblPending")

                            ' If lblPending.Text <> "" Then
                            ObjGoods.PGD_PendingItem = 0 'lblPending.Text
                            ' End If
                            ObjGoods.PGD_CompID = sSession.AccessCodeID
                            ObjGoods.PGD_Status = "W"
                            ObjGoods.PGD_Delflag = "W"
                            ObjGoods.PGD_Operation = "C"
                            ObjGoods.PGD_IPAddress = sSession.IPAddress
                            txtBatchNumber = dgPurchase.Rows(i).FindControl("lblPOD_BatchNumber")
                            If (txtBatchNumber.Text <> "") Then
                                ObjGoods.GIND_BatchNo = txtBatchNumber.Text
                            Else
                                ObjGoods.GIND_BatchNo = " "
                            End If
                            ObjGoods.PGD_OrderID = ddlExistingOrder.SelectedValue
                            Arr = objGin.SaveMasterDetails(sSession.AccessCode, ObjGoods)
                        End If
                    Next
                    If Arr(0) = "2" Then
                        lblStatus.Text = "Successfully Updated"
                    ElseIf Arr(0) = "3" Then
                        lblStatus.Text = "Successfully Saved"
                    End If
                    Return iMasterID
                    '   LoadExistingInwardNo()
                    '  ddlExistingInwardNo.SelectedValue = iMasterID
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveInward")
        End Try
    End Function
    Public Function SaveRegistry()
        Dim objRegistry As New clsPurchaseRegistry
        Dim Arr As String
        Dim iMasterID As Integer
        Dim sCurrentMonth As String = "", sYear As String = "", sStaus As String = "", sStatus As String = "", Check As String
        Dim ddlUnit As New DropDownList
        Dim lblMRP As New Label
        Dim lblPending As New Label
        Dim lblOrderedQuantity As New Label, lblCommodityID As New Label, lblDescription As Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label, UnitsID As New Label
        Dim lblReceivedQty As New Label
        Dim txtBatchNumber As New Label
        Dim lblAcceptedQuantity As New Label
        Dim lblRejectedQuantity As New Label
        Dim lblRejectedQuantityExcess As New Label
        Dim lblRemarks As New TextBox
        Dim txtManufactureDate As New Label
        Dim txtExpireDate As New Label
        Dim lblBatchNo As New Label
        Dim lblRate As New TextBox
        'Dim row As GridViewRow
        Dim sCurrentMonthID As String
        Try
            If (txtInvoiceRef.Text = "") Then
                lblError.Text = "Enter Document reference"
                Exit Function
            Else
                If (objRegistry.CheckVerifiedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, txtInvoiceRef.Text)) Then
                    lblError.Text = "Already verified "
                    Exit Function
                End If
            End If
            Check = objRegistry.CheckRegistredOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, txtInvoiceRef.Text)
            If (Check = True) Then
                lblError.Text = "Document reference no already exist"
            Else
                If dgPurchase.Rows.Count > 0 Then
                    sCurrentMonthID = objRegistry.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnction.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnction.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    'objRegistry.dPRM_OrderDate = Date.ParseExact(Trim("28/02/2016"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objRegistry.dPRM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objRegistry.sPRM_DocumentRefNo = objClsFASGnrl.SafeSQL(txtInvoiceRef.Text)
                    objRegistry.iPRM_CreatedBy = sSession.UserID
                    objRegistry.sPRM_ESugamNo = objClsFASGnrl.SafeSQL(txtEsugamNo.Text)
                    objRegistry.iPRM_Supplier = objGin.GetSupplierName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                    'If (ddlDocNo.SelectedIndex > 0) Then
                    objRegistry.sPRM_DocumentRefNo = txtInvoiceRef.Text
                    'Else
                    objRegistry.sPRM_DocumentRefNo = objClsFASGnrl.SafeSQL(txtInvoiceRef.Text)
                End If
                objRegistry.sPRM_DcNo = txtDcNo.Text
                objRegistry.iPRM_ID = 0
                objRegistry.iPRM_CompID = sSession.AccessCodeID
                objRegistry.iPRM_CreatedBy = sSession.UserID
                objRegistry.dPRM_CreatedOn = DateTime.Today
                objRegistry.iPRM_Status = "A"
                objRegistry.sPRM_DelFlag = "W"
                objRegistry.iPRM_YearID = sSession.YearID
                ' objReg.prm_o = "C"
                objRegistry.sPRM_IPAddress = sSession.IPAddress
                objRegistry.sPRM_RegistryNo = objRegistry.GeneratePurchaseRegCode(sSession.AccessCode, sSession.AccessCodeID) 'objFasGnrl.SafeSQL(txt.Text)
                '  ObjGoods.PGM_ModeOfShiping = ddlModeOfShipping.SelectedItem.Text 'lblMShiping.Text
                objRegistry.dPRM_InvoiceDate = Date.ParseExact(Trim(txtinvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objRegistry.sPRM_ESugamNo = objClsFASGnrl.SafeSQL(txtEsugamNo.Text)
                objRegistry.iPRM_OrderNo = ddlExistingOrder.SelectedValue
                iMasterID = objRegistry.PurchaseRegistryMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objRegistry)
                ' iMasterID = Arr(0)
                For i = 0 To dgPurchase.Rows.Count - 1
                    lblReceivedQty = dgPurchase.Rows(i).FindControl("lblReceivedQty")
                    If (lblReceivedQty.Text = "") Then
                        lblReceivedQty.Text = 0
                    Else
                        objRegistry.dPRD_RecievedQnt = lblReceivedQty.Text
                    End If

                    If (Convert.ToDecimal(lblReceivedQty.Text) > 0) Then
                        objRegistry.iPRD_MasterID = iMasterID
                        lblMRP = dgPurchase.Rows(i).FindControl("lblRate")
                        If lblMRP.Text <> "" Then
                            objRegistry.dPRD_MRP = lblMRP.Text
                        Else
                            objRegistry.dPRD_MRP = 0
                        End If

                        lblDescription = dgPurchase.Rows(i).FindControl("lblDescriptionID")


                        lblUnitId = dgPurchase.Rows(i).FindControl("lblUnitsID")


                        If lblUnitId.Text <> "" Then
                            objRegistry.iPRD_UnitID = lblUnitId.Text ' objGIN.GetUnitID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescription.Text)
                        Else
                            objRegistry.iPRD_UnitID = 0
                        End If

                        lblHistoryID = dgPurchase.Rows(i).FindControl("lblHistoryID")
                        If lblHistoryID.Text <> "" Then
                            objRegistry.iPRD_HistoryID = lblHistoryID.Text
                        Else
                            objRegistry.iPRD_HistoryID = 0
                        End If
                        lblItemId = dgPurchase.Rows(i).FindControl("lblDescriptionID")
                        If lblItemId.Text <> "" Then
                            objRegistry.iPRD_DescID = lblItemId.Text
                        Else
                            objRegistry.iPRD_DescID = 0
                        End If


                        'lblUnitId = dgPurchaseRegistry.Rows(i).FindControl("lblUnitId")
                        'If lblUnitId.Text <> "" Then
                        '    ObjGoods.PGD_UnitID = lblUnitId.Text 'objGIN.GetUnitID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescription.Text)
                        'Else
                        '    ObjGoods.PGD_UnitID = 0
                        'End If

                        lblCommodityID = dgPurchase.Rows(i).FindControl("lblCommodityID")
                        If lblCommodityID.Text <> "" Then
                            objRegistry.iPRD_Commodity = lblCommodityID.Text 'objGIN.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblComodityId.Text)
                        Else
                            objRegistry.iPRD_Commodity = 0
                        End If
                        lblOrderedQuantity = dgPurchase.Rows(i).FindControl("Quantity")
                        If lblOrderedQuantity.Text <> "" Then
                            objRegistry.dPRD_OrderQuntity = lblOrderedQuantity.Text
                        Else
                            objRegistry.dPRD_OrderQuntity = 0
                        End If

                        'If lblReceivedQuantity.Text <> "" Then
                        '    objRegistry.dPRD_RecievedQnt = lblReceivedQuantity.Text
                        'Else
                        '    objRegistry.dPRD_RecievedQnt = 0
                        'End If
                        'If lblReceivedQuantity.Text <> "" Then
                        '    objRegistry.dPRD_RecievedQnt = lblReceivedQuantity.Text
                        'Else
                        '    objRegistry.dPRD_RecievedQnt = 0
                        'End If

                        lblAcceptedQuantity = dgPurchase.Rows(i).FindControl("lblAcceptedQty")
                        If lblAcceptedQuantity.Text <> "" Then
                            objRegistry.dPRD_Accepted = lblAcceptedQuantity.Text
                        Else
                            objRegistry.dPRD_Accepted = 0
                        End If

                        lblRejectedQuantity = dgPurchase.Rows(i).FindControl("lblRejectedQty")
                        If lblRejectedQuantity.Text <> "" Then
                            objRegistry.dPRD_Rejected = lblRejectedQuantity.Text
                        Else
                            objRegistry.dPRD_Rejected = 0
                        End If
                        ' lblRejectedQuantityExcess = dgPurchase.Rows(i).FindControl("txtExcessQty")
                        ' If lblRejectedQuantityExcess.Text <> "" Then
                        'objRegistry.dPRD_Rejected = lblRejectedQuantityExcess.Text
                        ' objRegistry.sPRD_DelFlag = "E"
                        '  Else
                        objRegistry.dPRD_Excise = 0
                        '    objRegistry.sPRD_DelFlag = "A"
                        'End If
                        txtManufactureDate = dgPurchase.Rows(i).FindControl("lblPOD_ManufactureDate")
                        If txtManufactureDate.Text <> "" Then
                            objRegistry.dPRD_ManufactureDate = Date.ParseExact(Trim(txtManufactureDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Else
                            objRegistry.dPRD_ExpireDate = "01/01/1900"
                        End If
                        txtExpireDate = dgPurchase.Rows(i).FindControl("lblPOD_ExpiryDate")
                        If txtExpireDate.Text <> "" Then
                            objRegistry.dPRD_ExpireDate = Date.ParseExact(Trim(txtExpireDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Else
                            objRegistry.dPRD_ExpireDate = "01/01/1900"
                        End If
                        'lblPending = dgPurchase.Rows(i).FindControl("lblPending")
                        'If lblPending.Text <> "" Then
                        objRegistry.dPRD_PendIng = 0 'lblPending.Text
                        'End If
                        objRegistry.iPRD_CompID = sSession.AccessCodeID
                        objRegistry.sPRD_Status = "W"
                        '  objReg.prd_ = "C"
                        objRegistry.sPRD_IPAddress = sSession.IPAddress
                        txtBatchNumber = dgPurchase.Rows(i).FindControl("lblPOD_BatchNumber")
                        If (txtBatchNumber.Text <> "") Then
                            objRegistry.sPRD_BatchNo = txtBatchNumber.Text
                        Else
                            objRegistry.sPRD_BatchNo = " "
                        End If
                        objRegistry.iPRD_OrderNo = ddlExistingOrder.SelectedValue
                        Arr = objRegistry.PurchaseRegistryDetails(sSession.AccessCode, sSession.AccessCodeID, objRegistry)
                    End If
                Next
                'If Arr = "2" Then
                '    lblError.Text = "Successfully Updated"
                'ElseIf Arr = "3" Then
                '    lblStatus.Text = "Successfully Saved"
                'End If
                'LoadExistingRegisterNo()
                'ddlExistRegisrtry.SelectedValue = iMasterID
                Return iMasterID
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveRegistry")
        End Try
    End Function

    'Public Sub GenerateGINCodeAnddate()
    '    Try
    '        txtOrderCode.Text = objGin.GenerateInwardCode(sSession.AccessCode, sSession.AccessCodeID)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub loadRegular()
        Try
            'ddlCst.Items.Clear()
            'ddlCst.DataSource = objInvntry.LoadVAT(sSession.AccessCode, sSession.AccessCodeID)
            'ddlCst.DataTextField = "Mas_Desc"
            'ddlCst.DataValueField = "Mas_ID"
            'ddlCst.DataBind()
            'ddlCst.Items.Insert(0, "--- Select CST ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub txtPices_TextChanged(sender As Object, e As EventArgs) Handles txtPices.TextChanged

    End Sub
    Protected Sub dgPurchase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPurchase.RowCommand
        Dim lnkDescription As New Label
        Dim lblcomodityID As New Label
        Dim lblDescriptionId As New Label
        Dim lblHistoryID As New Label
        Dim dt As New DataTable
        Dim dtdata As New DataTable
        Dim lblID As New Label
        Try

            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
            txtItemTableID.Text = lblID.Text
            lnkDescription = DirectCast(clickedRow.FindControl("lnkGoods"), Label)
            lblcomodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
            lblDescriptionId = DirectCast(clickedRow.FindControl("lblDescriptionID"), Label)
            lblHistoryID = DirectCast(clickedRow.FindControl("lblHistoryID"), Label)

            'lnkDescription = DirectCast(clickedRow.FindControl("lnkGoods"), LinkButton)
            'lblcomodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
            'lblDescriptionId = DirectCast(clickedRow.FindControl("lblDescriptionID"), Label)
            'lblHistoryID = DirectCast(clickedRow.FindControl("lblHistoryID"), Label)
            If e.CommandName = "Delete" Then
                'objOral.DeleteOrderValues(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text, lblcomodityID.Text, lblDescriptionId.Text)
                'lblStatus.Text = "Sucessfully Deleted"

                'dtdata = objOral.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
                'Session("dtPurchase") = dtdata
                'dgPurchase.DataSource = objOral.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
                'dgPurchase.DataBind()
                'If (dgPurchase.Rows.Count = 0) Then
                '    objOral.DeleteOrderValuesFromMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text)
                'End If

                'LoadExistingPurchaseOrder()
            End If

            If e.CommandName = "Edit1" Then

                If (objOral.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtMasterID.Text) = "W") Then
                    dt = objOral.LoadPurchaseOderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text, lblcomodityID.Text, lblDescriptionId.Text, lblHistoryID.Text, sSession.YearID, txtItemTableID.Text)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("POD_Commodity").ToString()) = False Then
                            ddlCommodity.SelectedValue = dt.Rows(0)("POD_Commodity").ToString()
                            chkCategory.DataSource = objOral.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
                            chkCategory.DataTextField = "Inv_Code"
                            chkCategory.DataValueField = "Inv_ID"
                            chkCategory.DataBind()
                            chkCategory.SelectedValue = dt.Rows(0)("POD_DescriptionID").ToString()
                            lblDescID.Text = dt.Rows(0)("POD_DescriptionID").ToString()
                            LoadDesciptionDetails()
                        Else
                            ddlCommodity.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(0)("POD_RequiredDate").ToString()) = False Then
                            txtRDate.Text = dt.Rows(0)("POD_RequiredDate").ToString()
                        Else
                            txtRDate.Text = "0"
                        End If

                        If IsDBNull(dt.Rows(0)("POD_ReceivedQty").ToString()) = False Then
                            txtReceivedQty.Text = dt.Rows(0)("POD_ReceivedQty").ToString()
                        Else
                            txtReceivedQty.Text = "0"
                        End If

                        If IsDBNull(dt.Rows(0)("POD_RejectedQty").ToString()) = False Then
                            txtRejectedQty.Text = dt.Rows(0)("POD_RejectedQty").ToString()
                        Else
                            txtRejectedQty.Text = "0"
                        End If



                        If IsDBNull(dt.Rows(0)("POD_HistoryID").ToString()) = False Then
                            txtHistoryID.Text = dt.Rows(0)("POD_HistoryID").ToString()
                        Else
                            txtHistoryID.Text = "0"
                        End If

                        If IsDBNull(dt.Rows(0)("POD_Unit").ToString()) = False Then
                            ddlUnit.SelectedValue = dt.Rows(0)("POD_Unit").ToString()
                        Else
                            ddlUnit.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(0)("POD_Rate").ToString()) = False Then
                            txtRate.Text = dt.Rows(0)("POD_Rate").ToString()
                        Else
                            txtRate.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POD_RateAmount").ToString()) = False Then
                            txtRateAmount.Text = dt.Rows(0)("POD_RateAmount").ToString()
                        Else
                            txtRateAmount.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POD_Quantity").ToString()) = False Then
                            txtQuantity.Text = dt.Rows(0)("POD_Quantity").ToString()
                        Else
                            txtQuantity.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POD_Discount").ToString()) = False Then
                            txtDiscount.Text = dt.Rows(0)("POD_Discount").ToString()
                        Else
                            txtDiscount.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POD_DiscountAmount").ToString()) = False Then
                            txtDiscountAmount.Text = dt.Rows(0)("POD_DiscountAmount").ToString()
                        Else
                            txtDiscountAmount.Text = ""
                        End If

                        'If IsDBNull(dt.Rows(0)("POD_Excise").ToString()) = False Then
                        '    txtExcise.Text = dt.Rows(0)("POD_Excise").ToString()
                        'Else
                        '    txtExcise.Text = ""
                        'End If

                        'If IsDBNull(dt.Rows(0)("POD_ExciseAmount").ToString()) = False Then
                        '    txtExciseAmount.Text = dt.Rows(0)("POD_ExciseAmount").ToString()
                        'Else
                        '    txtExciseAmount.Text = ""
                        'End If

                        If IsDBNull(dt.Rows(0)("POD_GSTRate")) = False Then
                            txtGST.Text = dt.Rows(0)("POD_GSTRate").ToString()
                        Else
                            txtGST.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POD_GSTAmount")) = False Then
                            txtGSTAmount.Text = dt.Rows(0)("POD_GSTAmount").ToString()
                        Else
                            txtGSTAmount.Text = ""
                        End If


                        If IsDBNull(dt.Rows(0)("POD_Frieght").ToString()) = False Then
                            txtFreight.Text = dt.Rows(0)("POD_Frieght").ToString()
                        Else
                            txtFreight.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POD_FrieghtAmount").ToString()) = False Then
                            txtFreightAmount.Text = dt.Rows(0)("POD_FrieghtAmount").ToString()
                        Else
                            txtFreightAmount.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POD_ExpiryDate").ToString()) = False Then

                            If objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_ExpiryDate").ToString(), "D") = "01/01/1900" Then
                                txtRDate.Text = ""
                            Else
                                txtRDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_ExpiryDate").ToString(), "D")
                            End If

                        Else
                            txtEdate.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POD_ManufactureDate").ToString()) = False Then

                            If objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_ManufactureDate").ToString(), "D") = "01/01/1900" Then
                                txtmdate.Text = ""
                            Else
                                txtmdate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_ManufactureDate").ToString(), "D")
                            End If
                        Else
                            txtFreightAmount.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POD_BatchNumber").ToString()) = False Then
                            txtBatchNumber.Text = dt.Rows(0)("POD_BatchNumber").ToString()
                        Else
                            txtBatchNumber.Text = ""
                        End If

                        If objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_ExpiryDate").ToString(), "D") = "01/01/1900" Then
                            txtEdate.Text = ""
                        Else
                            txtEdate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_ExpiryDate").ToString(), "D")
                        End If


                        'If IsDBNull(dt.Rows(0)("POD_VAT").ToString()) = False Then
                        '    ddlVat.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("POD_VAT").ToString())
                        'Else
                        '    ddlVat.SelectedValue = -1
                        'End If

                        'If IsDBNull(dt.Rows(0)("POD_VATAmount").ToString()) = False Then
                        '    txtVatAmount.Text = dt.Rows(0)("POD_VATAmount").ToString()
                        'Else
                        '    txtVatAmount.Text = ""
                        'End If

                        'If (ddlCstCtgry.SelectedValue = 1) Then
                        '    If IsDBNull(dt.Rows(0)("POD_CST").ToString()) = False Then
                        '        ddlCst.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 14, dt.Rows(0)("POD_CST").ToString())
                        '    Else
                        '        ddlCst.SelectedValue = -1
                        '    End If
                        'ElseIf (ddlCstCtgry.SelectedValue = 2) Then
                        '    If IsDBNull(dt.Rows(0)("POD_CST").ToString()) = False Then
                        '        ddlCst.SelectedValue = objOral.GetGeneralMasterValue(sSession.AccessCode, sSession.AccessCodeID, 15, dt.Rows(0)("POD_CST").ToString())
                        '    Else
                        '        ddlCst.SelectedValue = -1
                        '    End If
                        'Else
                        '    ddlCst.SelectedIndex = -1
                        'End If

                        'If IsDBNull(dt.Rows(0)("POD_CSTAmount").ToString()) = False Then
                        '    txtCSTAmount.Text = dt.Rows(0)("POD_CSTAmount").ToString()
                        'Else
                        '    txtCSTAmount.Text = ""
                        'End If


                        'If IsDBNull(dt.Rows(0)("POD_RequiredDate").ToString()) = False Then
                        '    If objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_RequiredDate").ToString(), "D") = "01/01/1900" Then
                        '        txtRDate.Text = ""
                        '    Else
                        '        txtRDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_RequiredDate").ToString(), "D")
                        '    End If
                        'Else
                        '    txtRDate.Text = ""
                        'End If

                        If IsDBNull(dt.Rows(0)("POD_TotalAmount").ToString()) = False Then
                            txtTotalAmount.Text = dt.Rows(0)("POD_TotalAmount").ToString()
                        Else
                            txtTotalAmount.Text = ""
                        End If
                    End If

                    'Dim sStatus As String = ""
                    'sStatus = objPO.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                    'If sStatus = "A" Then
                    '    'btnSave.Visible = True : btnSave.Enabled = False
                    '    'btnApprove.Visible = True : btnApprove.Enabled = False
                    'Else
                    '    'btnSave.Visible = True : btnSave.Enabled = True
                    '    'btnApprove.Visible = True : btnApprove.Enabled = True 
                    'End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowCommand")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim imasterID As Integer
        Dim BillNo As String
        Dim dt As New DataTable
        Dim dDate, dSDate As Date : Dim m As Integer
        Try

            '//////////////Preeti
            If txtOrderDate.Text <> "" Then

                dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtOrderDate.Focus()
                    Exit Sub
                End If
                dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtOrderDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(Trim(txtinvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dSDate, dDate)
                If m < 0 Then
                    lblError.Text = "invoice date (" & txtinvoiceDate.Text & ") should be Greater than or equal to invoice date (" & txtOrderDate.Text & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtOrderDate.Focus()
                    Exit Sub
                End If
            End If
            '//////////////Preeti
            If ddlExistingOrder.SelectedIndex > 0 Then

                '~~~~~~~~~~~~~~~~~~~Commented by darshan 01-09-2020 for mismatching GIN_ID , So Passed Registery Master ID
                'imasterID = SaveInward()
                'SaveRegistry()
                'objOral.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlExistingOrder.SelectedValue)
                'MakeTransactioPI(imasterID)
                'MakeTransactionPR(imasterID)
                SaveInward()
                imasterID = SaveRegistry()
                objOral.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlExistingOrder.SelectedValue)
                MakeTransactioPI(imasterID)
                MakeTransactionPR(imasterID)
                '~~~~~~~~~~~~~~~~~~~Commented by darshan 01-09-2020 for mismatching GIN_ID , So Passed Registery Master ID


                'BillNo = "PB" & "-" & sSession.YearID & "-" & "" & Date.Now.Month & "" & objOral.GenerateBillNo(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                '//////////////Preeti
                BillNo = objOral.GenerateBillNo(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)

                If imasterID > 0 Then
                    dt = objOral.GetTransactionDetailsPI(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                    objOral.SaveStockLedger(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, sSession.UserID, dt, ddlExistingOrder.SelectedValue, imasterID)
                End If
                '//////////////Preeti
                GetPurchasedItemsGrid(ddlExistingOrder.SelectedValue)
                SavePurchaseJE(ddlExistingOrder.SelectedValue, BillNo)
                lblStatus.Text = "Sucessfully Approved"
                lblUserMasterDetailsValidationMsg.Text = lblStatus.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            Else
                lblStatus.Text = "Select the existing Counter Purchase To Approve/Create new Counter Purchase"
                'lblError.Text = "Select the existing Counter Purchase To Approve/Create new Counter Purchase"
                lblUserMasterDetailsValidationMsg.Text = lblStatus.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub SavePurchaseJE(ByVal imasterID As Integer, ByVal sBillNo As String)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim objJE As New ClsPurchaseSalesJE
        Try
            objOral.iAcc_JE_ID = 0
            objOral.sAcc_JE_TransactionNo = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "P")
            objOral.iAcc_JE_Party = objOral.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, sSession.YearID)
            objOral.iAcc_JE_Location = 0
            objOral.iAcc_JE_BillType = 0

            objOral.iAcc_JE_InvoiceID = imasterID
            objOral.sAcc_JE_BillNo = sBillNo

            objOral.dAcc_JE_BillDate = Date.ParseExact(Trim(txtinvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'txtinvoiceDate.Text

            objOral.dAcc_JE_BillAmount = txtBillAmount.Text
            objOral.iAcc_JE_YearID = sSession.YearID
            objOral.sAcc_JE_Status = "W"
            objOral.iAcc_JE_CreatedBy = sSession.UserID
            objOral.iAcc_JE_CreatedOn = DateTime.Today
            objOral.sAcc_JE_Operation = "C"
            objOral.sAcc_JE_IPAddress = sSession.IPAddress
            objOral.dAcc_JE_BillCreatedDate = DateTime.Today
            objOral.sAcc_JE_AdvanceNaration = ""
            objOral.sAcc_JE_PaymentNarration = ""
            objOral.sAcc_JE_ChequeNo = ""
            objOral.sAcc_JE_IFSCCode = ""
            objOral.sAcc_JE_BankName = ""
            objOral.sAcc_JE_BranchName = ""

            objOral.iAcc_JE_UpdatedBy = sSession.UserID
            objOral.iAcc_JE_UpdatedOn = DateTime.Today
            objOral.iAcc_JE_CompID = sSession.AccessCodeID

            objOral.dAcc_JE_PendingAmount = txtBillAmount.Text
            objOral.sAcc_JE_Type = "CP"

            Arr = objOral.SavePurchaseJournalMaster(sSession.AccessCode, objOral)
            iTransID = Arr(1)

            For i = 0 To dgJEDetails.Items.Count - 1

                objOral.iATD_TrType = 5

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objOral.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objOral.iATD_ID = 0
                End If

                objOral.dATD_TransactionDate = DateTime.Today

                objOral.iATD_BillId = iTransID
                objOral.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
                'iPaymentType

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objOral.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objOral.iATD_Head = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objOral.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objOral.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objOral.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objOral.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objOral.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objOral.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objOral.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objOral.dATD_Credit = 0
                End If

                If objOral.dATD_Debit > 0 And objOral.dATD_Credit = 0 Then
                    objOral.iATD_DbOrCr = 1 'Debit
                ElseIf objOral.dATD_Debit = 0 And objOral.dATD_Credit > 0 Then
                    objOral.iATD_DbOrCr = 2 'Credit
                End If

                objOral.iATD_CreatedBy = sSession.UserID
                objOral.dATD_CreatedOn = DateTime.Today

                objOral.sATD_Status = "A"
                objOral.iATD_YearID = sSession.YearID
                objOral.sATD_Operation = "C"
                objOral.sATD_IPAddress = sSession.IPAddress

                objOral.iATD_UpdatedBy = sSession.UserID
                objOral.dATD_UpdatedOn = DateTime.Today

                objOral.iATD_CompID = sSession.AccessCodeID

                objOral.iATD_ZoneID = ddlAccZone.SelectedValue
                objOral.iATD_RegionID = ddlAccRgn.SelectedValue
                objOral.iATD_AreaID = ddlAccArea.SelectedValue
                objOral.iATD_BranchID = ddlAccBrnch.SelectedValue

                objOral.dATD_OpenDebit = "0.00"
                objOral.dATD_OpenCredit = "0.00"
                objOral.dATD_ClosingDebit = "0.00"
                objOral.dATD_ClosingCredit = "0.00"
                objOral.iATD_SeqReferenceNum = 0

                objOral.SaveUpdateTransactionDetails(sSession.AccessCode, objOral)

            Next

            lblUserMasterDetailsValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            dgJEDetails.DataSource = objOral.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID, objOral.sAcc_JE_TransactionNo)
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SavePurchaseJE")
        End Try
    End Sub


    Public Sub GetPurchasedItemsGrid(ByVal imasterID As Integer)
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
        Dim iParty As Integer = 0

        Dim dt1 As New DataTable
        Dim dPartyTotal As Double
        Dim dtGSTRates As New DataTable : Dim sSql As String = ""
        Dim dTotalAmt, dSGSTAmt, dCGSTAmt, dIGSTAmt As Double
        Dim SGST, CGST, IGST As Double
        Dim sTypeOfBill As String = "" : Dim sState As String = ""

        Dim Damount As Double = 0
        Dim sDeleveryRegNo As String = ""
        Dim sTinNo As String = ""
        Dim sGSTCategory As String = ""
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("PaymentID")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Type")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")

            iParty = ddlSupplier.SelectedValue

            sTypeOfBill = objDb.SQLGetDescription(sSession.AccessCode, "Select POM_PurchaseStatus From purchase_order_master Where POM_ID=" & imasterID & " And POM_CompID=" & sSession.AccessCodeID & " And POM_YearID=" & sSession.YearID & " ")

            sGSTCategory = objDb.SQLGetDescription(sSession.AccessCode, "Select GC_GSTCategory From GSTCategory_Table Where GC_ID in (Select POM_GSTNCategory From Purchase_Order_Master Where POM_ID=" & imasterID & " And POM_CompID=" & sSession.AccessCodeID & " And POM_YearID=" & sSession.YearID & ")")

            'sState = objDb.SQLGetDescription(sSession.AccessCode, "Select  From purchase_order_master Where POM_ID=" & imasterID & " And POM_CompID=" & sSession.AccessCodeID & " And POM_YearID=" & sSession.YearID & " ")

            'Commented To implement URD'
            'sDeleveryRegNo = objDb.SQLGetDescription(sSession.AccessCode, "Select POM_DeliveryGSTNRegNo From purchase_order_master Where POM_ID=" & imasterID & " And POM_CompID=" & sSession.AccessCodeID & " And POM_YearID=" & sSession.YearID & " ")
            'sTinNo = sDeleveryRegNo.Substring(0, 2)
            'sState = objDb.SQLGetDescription(sSession.AccessCode, "select GR_StateName from GSTN_RegNo_Master where GR_TIN='" & sTinNo & "' and GR_CompID=" & sSession.AccessCodeID & "")
            'Commented to implement URD'

            If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                sState = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                sState = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), (""))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                sState = GetSourceDestinationState((""), Trim(txtDeliveryGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                Dim ibranch As Integer
                ibranch = objOral.getBranchFromPO(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                If ibranch > 0 Then 'branch 
                    sState = objOral.CheckDetailsofBranchState(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                    If sState = "" Then
                        lblError.Text = "Update state in branch master"
                        lblUserMasterDetailsValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in branch master.','', 'success');", True)
                        Exit Sub
                    End If
                Else 'Company
                    sState = objOral.CheckDetailsofCompState(sSession.AccessCode, sSession.AccessCodeID)
                    If sState = "" Then
                        lblError.Text = "Update state in company master"
                        lblUserMasterDetailsValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in company master.','', 'success');", True)
                        Exit Sub
                    End If
                End If
            End If

            'Chart Of Accounts'
            Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
            Dim sPerm As String = ""
            Dim sArray1 As Array
            Dim sName As String = ""

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146

            'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
            sName = "Purchase Of Product " & sState
            txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
            Else
                iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
            End If
            'Chart Of Accounts'

            dtGSTRates = objOral.BindGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'
            If dtGSTRates.Rows.Count > 0 Then
                For x = 0 To dtGSTRates.Rows.Count - 1

                    sName = "Local GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & sState & " Purchase Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                    sName = "Inter State GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & sState & " Purchase Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                    sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST")
                    sPerm = sPerm.Remove(0, 1)
                    sArray1 = sPerm.Split(",")
                    iHead = sArray1(0) '1
                    iGroup = sArray1(1) '29
                    iSubGroup = sArray1(2) '31
                    iGL = sArray1(3) '146

                    sName = "INPUT SGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & sState & " Purchase Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "INPUT CGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & sState & " Purchase Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "INPUT IGST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & sState & " Purchase Account"
                    txtGLID.Text = objOral.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                Next
            End If
            'Chart Of Accounts'

            dtGSTRates = objOral.BindGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'
            If dtGSTRates.Rows.Count > 0 Then
                For k = 0 To dtGSTRates.Rows.Count - 1

                    'dt1 = objDb.SQLExecuteDataSet(sSession.AccessCode, "Select * From PI_Accepted_Details Where PID_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And PID_MasterID=" & imasterID & " And PID_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    dt1 = objDb.SQLExecuteDataSet(sSession.AccessCode, "Select * From purchase_order_details Where POD_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And POD_MasterID=" & imasterID & " And POD_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For z = 0 To dt1.Rows.Count - 1
                            Damount = 0
                            Damount = dt1.Rows(z)("POD_RateAmount") - dt1.Rows(z)("POD_DiscountAmount") + dt1.Rows(z)("POD_FrieghtAmount")
                            dTotalAmt = dTotalAmt + Damount
                            dSGSTAmt = dSGSTAmt + dt1.Rows(z)("POD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dt1.Rows(z)("POD_CGSTAmount")
                            dIGSTAmt = dIGSTAmt + dt1.Rows(z)("POD_IGSTAmount")
                            dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("POD_TotalAmount"))
                        Next

                        If UCase(sGSTCategory) = "UNRIGISTERED DEALER" Or UCase(sGSTCategory) = "COMPOSITION DEALER" Then
                            dSGSTAmt = 0
                            dCGSTAmt = 0
                            dIGSTAmt = 0
                        End If

                        dRow = dt.NewRow 'Item Name
                        dRow("Id") = 0
                        dRow("HeadID") = objOral.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Purchase Of Product " & sState)
                        dRow("GLID") = objOral.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Purchase Of Product " & sState)
                        If sTypeOfBill = "Local" Then
                            dRow("SubGLID") = objOral.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Local GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
                        ElseIf sTypeOfBill = "Inter State" Then
                            dRow("SubGLID") = objOral.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Inter State GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
                        End If
                        dRow("PaymentID") = 5
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "Purchase Of Material"

                        sGL = objOral.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objOral.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dTotalAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)


                        SGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        CGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        IGST = dtGSTRates.Rows(k)("GST_GSTRate")

                        dRow = dt.NewRow 'SGST
                        dRow("Id") = 0
                        dRow("HeadID") = objOral.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objOral.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objOral.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input SGST " & SGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 6
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "SGST"

                        sGL = objOral.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objOral.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dSGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'CGST
                        dRow("Id") = 0
                        dRow("HeadID") = objOral.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objOral.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objOral.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input CGST " & CGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 7
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "CGST"

                        sGL = objOral.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objOral.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dCGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'IGST
                        dRow("Id") = 0
                        dRow("HeadID") = objOral.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objOral.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objOral.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input IGST " & IGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 8
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "IGST"

                        sGL = objOral.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objOral.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = dIGSTAmt
                        dRow("Credit") = 0
                        dt.Rows.Add(dRow)

                        dTotalAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0 : dIGSTAmt = 0
                    End If

                Next

                dRow = dt.NewRow 'Party/Customer
                dRow("Id") = 0
                dRow("HeadID") = objOral.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
                dRow("GLID") = objOral.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
                dRow("SubGLID") = objOral.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                dRow("PaymentID") = 9
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Party/Customer"

                sGL = objOral.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objOral.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    dRow("SubGLDescription") = sArray(1)
                End If
                dRow("Debit") = 0
                dRow("Credit") = dPartyTotal

                txtBillAmount.Text = dPartyTotal

                dt.Rows.Add(dRow)

            End If

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetDefaultGridPurchase")
        End Try
    End Sub
    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer, ByVal sStatus As String, ByVal sReason As String) As Integer
        Dim sRet As String = ""
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try
            objCOA.igl_id = 0
            objCOA.igl_head = iHead
            objCOA.igl_Parent = iParent
            objCOA.sgl_glcode = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, iAccHead, iParent)
            objCOA.sgl_Desc = objClsFASGnrl.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objClsFASGnrl.SafeSQL(sReason)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = iAccHead
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "A"
            objCOA.sgl_IPAddress = sSession.IPAddress

            'sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            If sStatus = "Save" Then
                sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            ElseIf sStatus = "Update" Then
                objCOA.igl_id = txtGLID.Text
                sRet = objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            End If

            sArray = sRet.Split(",")
            Return sArray(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Acc_Head").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Head").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_Group").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Group").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGroup").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGroup").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_GL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_GL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                Return sPerm
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub MakeTransactioPI(ByVal MasterID As Integer)
        Dim ObjGoods As New ClsGoodsInward
        Dim Arr() As String
        Dim sCurrentMonth As String = "", sYear As String = "", sCheckAcceptedQTY As String = "", sStr As String = ""
        Dim ddlUnit As New DropDownList
        Dim lblAcceptedQuantity As New Label
        Dim lblOrderedQuentity As New Label
        Dim lblReceivedQuentity As New Label
        Dim lblExcessQuentity As New Label
        Dim lblRemarks As New TextBox
        Dim lblRate As New Label
        Dim lblMRP As New Label
        Dim lblComodityId As New Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label
        Dim j As Integer
        Dim ssql As String = "" : Dim sCurrentMonthID As String

        Try
            For j = 0 To dgPurchase.Rows.Count - 1
                lblReceivedQuentity = dgPurchase.Rows(j).FindControl("lblReceivedQty")
                If (lblReceivedQuentity.Text = "") Then
                    lblReceivedQuentity.Text = 0
                Else
                    ObjGoods.PGD_ReceivedQnt = lblReceivedQuentity.Text
                End If
                If (Convert.ToDecimal(lblReceivedQuentity.Text) > 0) Then
                    sCurrentMonthID = objGin.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnction.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnction.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    lblMRP = dgPurchase.Rows(j).FindControl("lblRate")
                    lblOrderedQuentity = dgPurchase.Rows(j).FindControl("Quantity")
                    If lblOrderedQuentity.Text <> "" Then
                        ObjGoods.PGD_OrderQnt = lblOrderedQuentity.Text
                    Else
                        ObjGoods.PGD_OrderQnt = 0
                    End If

                    lblReceivedQuentity = dgPurchase.Rows(j).FindControl("lblReceivedQty")
                    If lblReceivedQuentity.Text <> "" Then
                        ObjGoods.PGD_ReceivedQnt = lblReceivedQuentity.Text
                    Else
                        ObjGoods.PGD_ReceivedQnt = 0
                    End If
                    lblAcceptedQuantity = dgPurchase.Rows(j).FindControl("lblAcceptedQty")
                    If lblAcceptedQuantity.Text <> "" Then
                        ObjGoods.PGD_Accepted = lblAcceptedQuantity.Text
                    Else
                        ObjGoods.PGD_Accepted = 0
                    End If
                    '   lblExcessQuentity = dgPurchase.Rows(j).FindControl("txtExcessQty")
                    ' If lblExcessQuentity.Text <> "" Then
                    ObjGoods.PGD_Excess = 0 'lblExcessQuentity.Text
                    ObjGoods.PGD_Status = "W"
                    ObjGoods.PGD_ID = 0

                    'Else
                    '    ObjGoods.PGD_Excess = 0
                    '    ObjGoods.PGD_Status = "A"
                    'End If
                    ObjGoods.PGD_Delflag = "W"
                    ObjGoods.PGD_YearID = sSession.YearID
                    ObjGoods.PGM_Gin_Number = MasterID    'ddlExistingInwardNo.SelectedValue
                    ObjGoods.PGD_CompID = sSession.AccessCodeID
                    lblComodityId = dgPurchase.Rows(j).FindControl("lblCommodityID")
                    If lblComodityId.Text <> "" Then
                        ObjGoods.PGD_CommodityID = lblComodityId.Text
                    Else
                        ObjGoods.PGD_CommodityID = 0
                    End If
                    ObjGoods.PGD_CompID = sSession.AccessCodeID
                    lblHistoryID = dgPurchase.Rows(j).FindControl("lblHistoryID")
                    If lblHistoryID.Text <> "" Then
                        ObjGoods.PGD_HistoryID = lblHistoryID.Text
                    Else
                        ObjGoods.PGD_HistoryID = 0
                    End If

                    ObjGoods.PGD_OrderID = ddlExistingOrder.SelectedValue
                    lblUnitId = dgPurchase.Rows(j).FindControl("lblUnitsID")
                    If lblUnitId.Text <> "" Then
                        ObjGoods.PGD_UnitID = lblUnitId.Text
                    Else
                        ObjGoods.PGD_UnitID = 0
                    End If

                    lblItemId = dgPurchase.Rows(j).FindControl("lblDescriptionID")
                    If lblItemId.Text <> "" Then
                        ObjGoods.PGD_DescriptionID = lblItemId.Text
                    Else
                        ObjGoods.PGD_DescriptionID = 0
                    End If

                    lblMRP = dgPurchase.Rows(j).FindControl("lblRate")
                    If lblMRP.Text <> "" Then
                        ObjGoods.PGD_MRP = lblMRP.Text
                    Else
                        ObjGoods.PGD_MRP = 0
                    End If
                    If txtInvoiceRef.Text <> "" Then
                        ObjGoods.PGM_DocRefNo = txtInvoiceRef.Text
                    Else
                        ObjGoods.PGM_DocRefNo = 0
                    End If


                    Arr = objGin.SaveTransactionInvoiceDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ObjGoods)
                    If Arr(0) = "2" Then
                        lblStatus.Text = "Successfully Approved"
                    ElseIf Arr(0) = "3" Then
                        lblStatus.Text = "Successfully Approved"
                    End If
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MakeTransactioPI")
        End Try
    End Sub


    Public Sub MakeTransactionPR(ByVal ImasterID As Integer)
        Dim ObjGoods As New ClsGoodsInward
        Dim Arr() As String
        Dim sCurrentMonth As String = "", sYear As String = "", sCheckAcceptedQTY As String = "", sStr As String = "", ssql As String = ""
        Dim j As Integer
        Dim ddlUnit As New DropDownList
        Dim lblAcceptedQuantity As New Label
        Dim lblOrderedQuentity As New Label
        Dim lblReceivedQuentity As New Label
        Dim lblRejectedQty As New Label
        Dim lblComodityId As New Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label
        Dim lblRemarks As New TextBox
        Dim lblRate As New Label
        Dim lblMRP As New Label
        Dim sCurrentMonthID As String
        Try
            For j = 0 To dgPurchase.Rows.Count - 1
                lblReceivedQuentity = dgPurchase.Rows(j).FindControl("lblReceivedQty")
                If (lblReceivedQuentity.Text = "") Then
                    lblReceivedQuentity.Text = 0
                Else
                    ObjGoods.PGD_ReceivedQnt = lblReceivedQuentity.Text
                End If

                If (Convert.ToDecimal(lblReceivedQuentity.Text) > 0) Then
                    sCurrentMonthID = objGin.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnction.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnction.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    lblOrderedQuentity = dgPurchase.Rows(j).FindControl("Quantity")
                    'lblReceivedQuentity = dgInward.Rows(j).FindControl("txtReceivedQty")
                    lblRejectedQty = dgPurchase.Rows(j).FindControl("lblRejectedQty")
                    lblMRP = dgPurchase.Rows(j).FindControl("lblRate")
                    lblReceivedQuentity = dgPurchase.Rows(j).FindControl("lblReceivedQty")
                    If lblOrderedQuentity.Text <> "" Then
                        ObjGoods.PGD_OrderQnt = lblOrderedQuentity.Text
                    Else
                        ObjGoods.PGD_OrderQnt = 0
                    End If
                    lblReceivedQuentity = dgPurchase.Rows(j).FindControl("lblReceivedQty")
                    If lblReceivedQuentity.Text <> "" Then
                        ObjGoods.PGD_ReceivedQnt = lblReceivedQuentity.Text
                    Else
                        ObjGoods.PGD_ReceivedQnt = 0
                    End If
                    lblAcceptedQuantity = dgPurchase.Rows(j).FindControl("lblAcceptedQty")
                    If lblAcceptedQuantity.Text <> "" Then
                        ObjGoods.PGD_Accepted = lblAcceptedQuantity.Text
                    Else
                        ObjGoods.PGD_Accepted = 0
                    End If
                    If lblRejectedQty.Text <> "" Then
                        ObjGoods.PGD_RejectedQnt = lblRejectedQty.Text
                    Else
                        ObjGoods.PGD_RejectedQnt = 0
                    End If
                    lblComodityId = dgPurchase.Rows(j).FindControl("lblCommodityID")
                    If lblComodityId.Text <> "" Then
                        ObjGoods.PGD_CommodityID = lblComodityId.Text
                    Else
                        ObjGoods.PGD_CommodityID = 0
                    End If
                    ObjGoods.PGD_CompID = sSession.AccessCodeID
                    lblHistoryID = dgPurchase.Rows(j).FindControl("lblHistoryID")
                    If lblHistoryID.Text <> "" Then
                        ObjGoods.PGD_HistoryID = lblHistoryID.Text
                    Else
                        ObjGoods.PGD_HistoryID = 0
                    End If
                    ObjGoods.PGD_OrderID = ddlExistingOrder.SelectedValue

                    lblUnitId = dgPurchase.Rows(j).FindControl("lblUnitsID")
                    If lblUnitId.Text <> "" Then
                        ObjGoods.PGD_UnitID = lblUnitId.Text
                    Else
                        ObjGoods.PGD_UnitID = 0
                    End If
                    lblItemId = dgPurchase.Rows(j).FindControl("lblDescriptionID")
                    If lblItemId.Text <> "" Then
                        ObjGoods.PGD_DescriptionID = lblItemId.Text
                    Else
                        ObjGoods.PGD_DescriptionID = 0
                    End If
                    lblMRP = dgPurchase.Rows(j).FindControl("lblRate")
                    If lblMRP.Text <> "" Then
                        ObjGoods.PGD_MRP = lblMRP.Text
                    Else
                        ObjGoods.PGD_MRP = 0

                    End If

                    If txtInvoiceRef.Text <> "" Then
                        ObjGoods.PGM_DocRefNo = txtInvoiceRef.Text
                    Else
                        ObjGoods.PGM_DocRefNo = 0
                    End If
                    ObjGoods.PGD_YearID = sSession.YearID
                    ObjGoods.PGD_ID = 0
                    ObjGoods.PGD_Status = "A"
                    ObjGoods.PGD_OrderID = ddlExistingOrder.SelectedValue
                    ObjGoods.PGM_ID = ImasterID 'ddlExistingInwardNo.SelectedValue
                    Arr = objGin.SaveTransactionReturnsDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ObjGoods)
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MakeTransactionPR")
        End Try
    End Sub

    Private Sub imgRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgRefresh.Click
        Try
            For i = 0 To chkCategory.Items.Count - 1
                chkCategory.Items(i).Selected = False
            Next
            ddlAccArea.SelectedIndex = 0 : ddlAccBrnch.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccZone.SelectedIndex = 0
            ddlExistingOrder.SelectedIndex = 0 : txtOrderCode.Text = "" : txtOrderDate.Text = "" : ddlSupplier.SelectedIndex = 0 : txtSprCode.Text = ""
            ddlDSchedule.SelectedIndex = 0 : ddlNumberOfDays.SelectedIndex = 0 : ddlModeOfShipping.SelectedIndex = 0 : ddlMPayment.SelectedIndex = 0
            ddlPterms.SelectedIndex = 0 : ddlCommodity.SelectedIndex = 0 : ddlTypeOfSale.SelectedIndex = 0 : ddlCstCtgry.SelectedIndex = 0
            ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.SelectedIndex = 0 : txtInvoiceRef.Text = "" : txtEsugamNo.Text = "" : txtDcNo.Text = ""
            GenerateOrderCodeAnddate()
            ddlUnit.Items.Clear() : txtinvoiceDate.Text = "" : ddlBranch.SelectedIndex = 0 : txtBillingAddress.Text = "" : txtDeleveryAddress.Text = ""
            txtDeliveryFromAddress.Text = "" : txtCompanyAddress.Text = "" : txtBillingGSTNRegNo.Text = "" : txtDeliveryFromGSTNRegNo.Text = "" : txtCompanyGSTNRegNo.Text = ""
            txtDeliveryGSTNRegNo.Text = "" : ddlChargeType.SelectedIndex = 0 : txtShippingRate.Text = "" : txtsearch.Text = ""
            ddlRate.SelectedIndex = -1 : txtRate.Text = "" : txtReceivedQty.Text = "" : txtRejectedQty.Text = "" : txtBatchNumber.Text = ""

            dgPurchase.DataSource = "" : dgPurchase.DataBind() : GvCharge.DataSource = "" : GvCharge.DataBind()

            txtGST.Text = "" : txtGSTAmount.Text = "" : hfDiscountAmount.Value = "" : txtItemTableID.Text = ""
            'hfCSTAmount.Value = ""
            'hfExciseAmount.Value = ""
            'hfVatAmount.Value = ""

            txtQuantity.Text = "" : txtRateAmount.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtRDate.Text = "" : txtTotalAmount.Text = "" : hfTotalAmount.Value = "" : txtFreight.Text = "" : txtFreightAmount.Text = ""
            txtmdate.Text = "" : txtEdate.Text = ""
            'dgPurchase.DataSource = Nothing
            'dgPurchase.DataBind()
            Session("ChargesMaster") = Nothing
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgRefresh_Click")
        End Try
    End Sub
    Protected Sub txtQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtQuantity.TextChanged

    End Sub

    'Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
    '    Dim dt, dtTable As New DataTable
    '    Try
    '        If ddlChargeType.SelectedIndex > 0 Then
    '            If txtShippingRate.Text <> "" Then
    '                dt = AddCharges()
    '                dtTable = objDispatch.RemoveDublicate(dt)
    '                GvCharge.DataSource = dtTable
    '                GvCharge.DataBind()

    '                ddlChargeType.SelectedIndex = 0 : txtShippingRate.Text = ""
    '            Else
    '                lblError.Text = "Enter Amount charged."
    '            End If
    '        Else
    '            lblError.Text = "Select Charge Type."
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub


    Public Function loadExistingCharges(ByVal ImasterID As Integer, ByVal sNameSpace As Integer, ByVal iCompID As Integer) As DataTable
        Dim dr As DataRow
        Dim sSql As String
        Dim dtabl2 As New DataTable, dtable As New DataTable, dt1 As New DataTable
        Try
            dt1.Columns.Add("ChargeID")
            dt1.Columns.Add("ChargeType")
            dt1.Columns.Add("ChargeAmount")
            sSql = "Select * from Charges_Master where C_POrderID =" & ImasterID & " and C_CompID=" & iCompID & " and  C_PSType='P'"
            dtabl2 = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtabl2.Rows.Count - 1
                dr("ChargeID") = dtabl2.Rows(0)("C_ChargeID").ToString()
                dr("ChargeType") = dtabl2.Rows(0)("C_ChargeType").ToString()
                dr("ChargeAmount") = dtabl2.Rows(0)("C_ChargeAmount").ToString()
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingCharges")
        End Try
    End Function

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
            dt = objAccSetting.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
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
            dt = objAccSetting.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
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
            dt = objAccSetting.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub GvCharge_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles GvCharge.ItemCommand
        Dim dt As New DataTable
        Dim dchargeTotal As Double
        Try
            If e.CommandName = "Delete" Then
                dt = Session("ChargesMaster")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                Session("ChargesMaster") = dt
            End If
            GvCharge.DataSource = dt
            GvCharge.DataBind()

            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1
                    dchargeTotal = dchargeTotal + GvCharge.Items(i).Cells(2).Text
                Next
            End If
            txtFreight.Text = dchargeTotal
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub

    Private Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Dim dt As New DataTable
        Dim description As String = ""
        Try
            If ddlBranch.SelectedIndex > 0 Then
                dt = objOral.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, ddlBranch.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUSTB_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUSTB_GSTNRegNo")

                    'ddlCompanyType.SelectedValue = dt.Rows(0)("CUSTB_CompanyType")
                    'BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    'ddlGSTCategory.SelectedValue = dt.Rows(0)("CUSTB_GSTNCategory")

                    'description = objOral.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    'If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                    '    txtDeliveryGSTNRegNo.Enabled = False
                    'Else
                    '    txtDeliveryGSTNRegNo.Enabled = True
                    'End If
                    txtDeleveryAddress.Text = txtCompanyAddress.Text
                    txtDeliveryGSTNRegNo.Text = txtCompanyGSTNRegNo.Text

                End If
            Else
                dt = objOral.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    'ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    'BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    'ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TaxPayableCategory")

                    'description = objOral.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    'If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                    '    txtDeliveryGSTNRegNo.Enabled = False
                    'Else
                    '    txtDeliveryGSTNRegNo.Enabled = True
                    'End If
                    txtDeleveryAddress.Text = txtCompanyAddress.Text
                    txtDeliveryGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
        Dim dt, dtTable As New DataTable
        Dim dchargeTotal As Double
        Try
            If ddlChargeType.SelectedIndex > 0 Then
                If txtShippingRate.Text <> "" Then
                    dt = AddCharges()
                    dtTable = objOral.RemoveChargeDublicate(dt)
                    GvCharge.DataSource = dtTable
                    GvCharge.DataBind()

                    If GvCharge.Items.Count > 0 Then
                        For i = 0 To GvCharge.Items.Count - 1
                            dchargeTotal = dchargeTotal + GvCharge.Items(i).Cells(2).Text
                        Next
                    End If

                    txtOtherCharge.Text = dchargeTotal
                    txtFreight.Text = dchargeTotal
                    dchargeTotal = 0
                Else
                    lblError.Text = "Enter Amount charged."
                End If
            Else
                lblError.Text = "Select Charge Type."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddCharge_Click")
        End Try
    End Sub
    Public Function AddCharges() As DataTable
        Dim dr As DataRow
        Dim dt1 As New DataTable
        Try
            If IsNothing(Session("ChargesMaster")) = False Then
                dt1 = Session("ChargesMaster")
            Else
                dt1.Columns.Add("ChargeID")
                dt1.Columns.Add("ChargeType")
                dt1.Columns.Add("ChargeAmount")
            End If

            dr = dt1.NewRow
            dr("ChargeID") = ddlChargeType.SelectedValue
            dr("ChargeType") = Trim(ddlChargeType.SelectedItem.Text)
            dr("ChargeAmount") = Trim(txtShippingRate.Text)
            dt1.Rows.Add(dr)

            Session("ChargesMaster") = dt1
            Return dt1
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AddCharges")
        End Try
    End Function
    Private Sub dgPurchase_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles dgPurchase.RowDeleting
        Try
            Dim index As Integer = Convert.ToInt32(e.RowIndex)
            Dim dt As DataTable = TryCast(Session("dtPurchase"), DataTable)
            dt.Rows(index).Delete()
            Session("dtPurchase") = dt
            dgPurchase.DataSource = dt
            dgPurchase.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowDeleting")
        End Try
    End Sub

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim dttab As New DataTable
        Dim dTotalAmt As Double
        Dim TotalAmount As New Label
        Try
            lblError.Text = ""
            If lblStatus.Text = "Submitted" Then
                lblError.Text = "This transaction has been submitted, Charges can not be add."
                Exit Sub
            ElseIf lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved, Charges can not be add."
                Exit Sub
            End If

            dttab = Session("dtPurchase")
            dgPurchase.DataSource = dttab
            dgPurchase.DataBind()

            For i = 0 To dgPurchase.Rows.Count - 1
                TotalAmount = dgPurchase.Rows(i).FindControl("TotalAmount")
                dTotalAmt = dTotalAmt + Convert.ToDouble(TotalAmount.Text)
            Next

            txtBillAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCalculate_Click")
        End Try

    End Sub

    Private Function BuildTable() As DataTable
        Dim dt As New DataTable
        Try

            dt.Columns.Add("ID")
            dt.Columns.Add("CommodityID")
            dt.Columns.Add("DescriptionID")
            dt.Columns.Add("HistoryID")
            dt.Columns.Add("UnitsID")
            dt.Columns.Add("SLNO")
            dt.Columns.Add("Goods")
            dt.Columns.Add("Units")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("ExciseDuty")
            dt.Columns.Add("ExciseAmt")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("POD_ReceivedQty")
            dt.Columns.Add("POD_RejectedQty")
            dt.Columns.Add("POD_AcceptedQty")
            dt.Columns.Add("POD_BatchNumber")
            dt.Columns.Add("POD_ExpiryDate")
            dt.Columns.Add("POD_ManufactureDate")
            dt.Columns.Add("Charge")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("SGST")
            dt.Columns.Add("SGSTAmount")
            dt.Columns.Add("CGST")
            dt.Columns.Add("CGSTAmount")
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub btnItemAdd_Click(sender As Object, e As EventArgs) Handles btnItemAdd.Click
        Dim dr As DataRow
        Dim dt As New DataTable
        Dim dCharge, dTotalAmt As Double
        Dim TotalAmount As New Label
        Dim dta As New DataTable
        Try
            lblError.Text = ""
            dta = BuildTable()
            If IsNothing(Session("dtPurchase")) = False Then
                dta = Session("dtPurchase")
            End If

            dr = dta.NewRow
            dr("CommodityID") = ddlCommodity.SelectedValue
            dr("DescriptionID") = chkCategory.SelectedValue
            dr("HistoryID") = txtHistoryID.Text
            dr("UnitsID") = ddlUnit.SelectedValue

            dr("Goods") = chkCategory.SelectedItem.Text
            dr("Units") = ddlUnit.SelectedItem.Text
            dr("Rate") = txtRate.Text
            dr("Quantity") = txtQuantity.Text
            dr("POD_AcceptedQty") = txtQuantity.Text
            dr("POD_ReceivedQty") = txtReceivedQty.Text
            dr("POD_RejectedQty") = txtRejectedQty.Text

            dr("RateAmount") = txtRateAmount.Text
            If txtDiscount.Text <> "" Then
                dr("Discount") = txtDiscount.Text
                dr("DiscountAmt") = txtDiscountAmount.Text
            Else
                dr("Discount") = 0
                dr("DiscountAmt") = 0
                txtDiscount.Text = 0
                txtDiscountAmount.Text = 0
            End If

            dCharge = 0
            dr("Charge") = 0
            dr("TotalAmount") = txtRateAmount.Text - txtDiscountAmount.Text
            dr("GSTRate") = txtGST.Text
            dr("GSTAmount") = txtGSTAmount.Text
            dr("TotalAmount") = txtTotalAmount.Text
            dta.Rows.Add(dr)

            Session("dtPurchase") = dta
            dgPurchase.DataSource = dta
            dgPurchase.DataBind()

            For i = 0 To dgPurchase.Rows.Count - 1
                TotalAmount = dgPurchase.Rows(i).FindControl("TotalAmount")
                dTotalAmt = dTotalAmt + Convert.ToDouble(TotalAmount.Text)
            Next
            txtBillAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")

            chkCategory.ClearSelection() : ddlUnit.Items.Clear() : txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = "" : txtGST.Text = "" : txtGSTAmount.Text = "" : txtTotalAmount.Text = ""
            txtReceivedQty.Text = "" : txtRejectedQty.Text = ""

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnItemAdd_Click")
        End Try
    End Sub
    Public Function GetSourceDestinationState(ByVal sBillingAddress As String, ByVal sReceiveAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            'sSource = sBillingAddress.Substring(0, 2)
            'sDestination = sReceiveAddress.Substring(0, 2)

            'If sSource = sDestination Then
            '    GetSourceDestinationState = objInvoiceForm.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
            'Else
            '    GetSourceDestinationState = objInvoiceForm.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
            'End If
            'Return GetSourceDestinationState
            If sBillingAddress <> "" And sReceiveAddress = "" Then
                sSource = sBillingAddress.Substring(0, 2)
                GetSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sReceiveAddress <> "" Then
                sDestination = sReceiveAddress.Substring(0, 2)
                GetSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sReceiveAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sReceiveAddress.Substring(0, 2)
                If sSource = sDestination Then
                    GetSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    GetSourceDestinationState = objOral.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return GetSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSourceDestinationState")
        End Try
    End Function
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            txtCompanyAddress.Text = "" : txtCompanyGSTNRegNo.Text = ""
            If ddlAccBrnch.SelectedIndex > 0 Then
                ddlBranch.SelectedValue = ddlAccBrnch.SelectedValue
                ddlBranch_SelectedIndexChanged(sender, e)

                If ddlAccBrnch.SelectedIndex > 0 Then
                    iParent = objOral.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                    ddlAccArea.SelectedValue = iParent
                End If
                If ddlAccArea.SelectedIndex > 0 Then
                    iParent = objOral.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                    ddlAccRgn.SelectedValue = iParent
                End If
                If ddlAccRgn.SelectedIndex > 0 Then
                    iParent = objOral.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                    ddlAccZone.SelectedValue = iParent
                End If
            Else
                ddlBranch.SelectedIndex = 0
                ddlBranch_SelectedIndexChanged(sender, e)

                ddlAccArea.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccZone.SelectedIndex = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccBrnch_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub chkboxFrom_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxFrom.CheckedChanged
        Try
            If chkboxFrom.Checked = True Then
                txtDeliveryFromAddress.Text = ""
                txtDeliveryFromGSTNRegNo.Text = ""
            Else
                txtDeliveryFromAddress.Text = txtBillingAddress.Text
                txtDeliveryFromGSTNRegNo.Text = txtBillingGSTNRegNo.Text
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub chkboxTo_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxTo.CheckedChanged
        Try
            If chkboxTo.Checked = True Then
                txtDeleveryAddress.Text = ""
                txtDeliveryGSTNRegNo.Text = ""
            Else
                txtDeleveryAddress.Text = txtCompanyAddress.Text
                txtDeliveryGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GvCharge_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles GvCharge.ItemDataBound
        GvCharge.Columns(3).Visible = False
        If sOOSave = "YES" Then
            GvCharge.Columns(3).Visible = True
        End If
    End Sub
    Private Sub ddlRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRate.SelectedIndexChanged
        Dim sArray As Array
        Try
            If txtHistoryID.Text <> "" Then
                ' GetPurchaseDetails(ddlRate.SelectedValue)
                txtHistoryID.Text = ddlRate.SelectedValue
                sArray = ddlRate.SelectedItem.Text.Split("-")
                txtRate.Text = sArray(0)

                LoadExciseUsingDate()

                txtQuantity.Text = "" : txtDiscount.Text = ""
                ' txtVat.Text = "" : txtCST.Text = ""
                txtRDate.Text = ""
                GetOtherDetails(txtHistoryID.Text)

            Else
                txtHistoryID.Text = ddlRate.SelectedValue

                LoadExciseUsingDate()

                sArray = ddlRate.SelectedItem.Text.Split("-")
                txtRate.Text = sArray(0)
                txtQuantity.Text = "" : txtDiscount.Text = ""
                '  txtVat.Text = "" : txtCST.Text = ""
                txtRDate.Text = ""
                GetOtherDetails(txtHistoryID.Text)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRate_SelectedIndexChanged")
        End Try
    End Sub
End Class
