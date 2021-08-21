Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Sales_DispatchForm
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_DispatchForm"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Dim objDispatch As New ClsDispatchForm
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Dim objPO As New clsPurchaseOrder
    Dim iDefaultBranch As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/BackWard24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
        imgbtnApprove.ImageUrl = "~/Images/CheckMark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Dim dt As New DataTable

        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DFOR")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnApprove.Visible = False : imgbtnAddCharge.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        imgbtnAddCharge.Visible = True
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnApprove.Visible = True
                    End If
                End If
                'If sSession.YearID > 0 Then
                '    sStr = sSession.YearName
                '    sArray = sStr.Split("-")
                '    iSYear = sArray(0)
                '    iEYear = sArray(1)

                'dStartDate = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'dEndDate = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                '    txtDispatchDate_CalendarExtender.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    txtDispatchDate_CalendarExtender.EndDate = New DateTime(iEYear, 3, dEndDate)

                'End If
                'rgvtxtDispatchDate.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvtxtDispatchDate.MinimumValue = "" & dStartDate & ""
                'rgvtxtDispatchDate.MaximumValue = "" & dEndDate & ""

                'imgbtnSave.Visible = False : imgbtnApprove.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FaDD", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Approve,") = True Then
                '        imgbtnApprove.Visible = True
                '    End If
                'End If

                'txtStartDate.Text = objGenFun.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtEndDate.Text = objGenFun.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                RFVddlOrderNo.InitialValue = "Select Order No" : RFVddlAllocationNo.InitialValue = "Existing Allocation No"
                RFVddlModeOfShipping.InitialValue = "Select Mode of Shipping"
                RFVddlChargeType.InitialValue = "Select Charge Type"

                BindCompanyType()
                BindBranch()
                'BindGSTNCategory()
                dt = objDispatch.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")
                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TAXPayableCategory")

                    Dim taxcategory As String

                    taxcategory = objDispatch.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryFromGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNo.Enabled = True
                    End If

                End If

                iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                LoadOrderNo(iDefaultBranch)
                LoadParty()
                LoadPaymentType()
                LoadMethodOfShiping()
                LoadExistingDispatchNo()
                lblDispatch.Visible = True : ddlSearch.Visible = True

                LoadSalesMan()
                LoadChargeType()

                If ddlPaymentType.SelectedItem.Text = "Cheque" Then
                    'divcollapseChequeDetails.Visible = True
                    divcollapseChequeDetails.Visible = False
                Else
                    divcollapseChequeDetails.Visible = False
                End If

                Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return ValidatePaymentType();")

                Session("ChargesMaster") = Nothing
                Dim iDisID As String = ""
                iDisID = objGen.DecryptQueryString(Request.QueryString("DFID"))
                If iDisID <> "" Then
                    DashBoardOrderNo()
                    LoadDashBoardDispatchNo()
                    ddlSearch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("DFID"))
                    ddlSearch_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindBranch()
        Try
            'ddlBranch.DataSource = objDispatch.LoadBranch(sSession.AccessCode, sSession.AccessCodeID)
            'ddlBranch.DataTextField = "CUSTB_Name"
            'ddlBranch.DataValueField = "CUSTB_Id"
            'ddlBranch.DataBind()
            'ddlBranch.Items.Insert(0, "Select Branch")

            ddlBranch.DataSource = objDispatch.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
            ddlBranch.DataTextField = "Org_Name"
            ddlBranch.DataValueField = "Org_Node"
            ddlBranch.DataBind()
            ddlBranch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindCompanyType()
        Try
            ddlCompanyType.DataSource = objDispatch.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
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
            dt = objDispatch.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategory.DataSource = dt
            ddlGSTCategory.DataTextField = "GC_GSTCategory"
            ddlGSTCategory.DataValueField = "GC_Id"
            ddlGSTCategory.DataBind()
            ddlGSTCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindBankName(ByVal iID As Integer)
        Try
            ddlBankName.DataSource = objDispatch.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, iID)
            ddlBankName.DataTextField = "GlDesc"
            ddlBankName.DataValueField = "gl_Id"
            ddlBankName.DataBind()
            ddlBankName.Items.Insert(0, "Select Bank Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCode()
        Try
            txtDispatchNo.Text = objDispatch.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCode")
        End Try
    End Sub
    Public Sub LoadExistingDispatchNo()
        Dim dt As New DataTable
        Try
            iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
            dt = objDispatch.BindDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iDefaultBranch)
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "DM_Code"
            ddlSearch.DataValueField = "DM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Dispatch No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingDispatchNo")
        End Try
    End Sub
    Public Sub LoadDashBoardDispatchNo()
        Dim dt As New DataTable
        Try
            dt = objDispatch.BindDashBoardDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "DM_Code"
            ddlSearch.DataValueField = "DM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Dispatch No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingAllocateCode(ByVal iOrderID As Integer)
        Dim dt As New DataTable
        Try
            dt = objDispatch.BindExistingCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            ddlAllocationNo.DataSource = dt
            ddlAllocationNo.DataTextField = "SAM_Code"
            ddlAllocationNo.DataValueField = "SAM_ID"
            ddlAllocationNo.DataBind()
            ddlAllocationNo.Items.Insert(0, "Existing Allocation No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSalesMan()
        Try
            ddlSalesMan.DataSource = objDispatch.LoadSalesMan(sSession.AccessCode, sSession.AccessCodeID)
            ddlSalesMan.DataTextField = "username"
            ddlSalesMan.DataValueField = "Usr_id"
            ddlSalesMan.DataBind()
            ddlSalesMan.Items.Insert(0, "Select Sales Person")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadMethodOfShiping()
        Try
            ddlModeOfShipping.DataSource = objDispatch.LoadMethodOfShiping(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfShipping.DataTextField = "Mas_desc"
            ddlModeOfShipping.DataValueField = "Mas_id"
            ddlModeOfShipping.DataBind()
            ddlModeOfShipping.Items.Insert(0, "Select Mode of Shipping")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objDispatch.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadOrderNo(ByVal iDefaultBranch As Integer)
        Try
            ddlOrderNo.DataSource = objDispatch.LoadOrderNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iDefaultBranch)
            ddlOrderNo.DataTextField = "SPO_OrderCode"
            ddlOrderNo.DataValueField = "SPO_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "Select Order No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub DashBoardOrderNo()
        Try
            ddlOrderNo.DataSource = objDispatch.DashBoardOrderNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlOrderNo.DataTextField = "SPO_OrderCode"
            ddlOrderNo.DataValueField = "SPO_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "Select Order No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DashBoardAllocateCode(ByVal iOrderID As Integer)
        Dim dt As New DataTable
        Try
            dt = objDispatch.BindDashBoardAllocationNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            ddlAllocationNo.DataSource = dt
            ddlAllocationNo.DataTextField = "SAM_Code"
            ddlAllocationNo.DataValueField = "SAM_ID"
            ddlAllocationNo.DataBind()
            ddlAllocationNo.Items.Insert(0, "Existing Allocation No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadPaymentType()
        Try
            ddlPaymentType.DataSource = objDispatch.BindPaymentType(sSession.AccessCode, sSession.AccessCodeID)
            ddlPaymentType.DataTextField = "Mas_Desc"
            ddlPaymentType.DataValueField = "Mas_ID"
            ddlPaymentType.DataBind()
            ddlPaymentType.Items.Insert(0, "Select Payment Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadParty()
        Try
            ddlParty.DataSource = objDispatch.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "BM_Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String, OrderNo As String = ""
        Dim iMasterID As Integer = 0
        Dim bCheck As String = ""
        Dim iAllocateID As Integer

        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblGoods, lblUnit, lblMRP, lblOrderedQty, lblTotalAmount, lblCST As New Label
        Dim HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount As HiddenField
        Dim ddlDiscount, ddlVAT, ddlCST, ddlExice As New DropDownList
        Dim sSource As String = "" : Dim sDestination As String = ""
        Dim lblGSTRate As New Label : Dim HFGSTAmount As HiddenField
        Dim lblGSTID As New Label
        Dim sCompanyGSTNRegNo As String = ""
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            If ddlOrderNo.SelectedIndex > 0 Then

                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Invoice Date (" & txtDispatchDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtDispatchDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Invoice Date (" & txtDispatchDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtDispatchDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'

                bCheck = objDispatch.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
                If bCheck = True Then
                    lblError.Text = "Selected Allocation No has been dispatched already."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If

                If ddlPaymentType.SelectedIndex = 0 Then
                    lblError.Text = "Select Payement Type"
                    Exit Sub
                End If

                If GvCharge.Items.Count = 0 Then
                    lblError.Text = "Enter Charges"
                    Exit Sub
                End If

                ''To Check Item Vat 0'
                ''Dim lblCommodityID, lblItemID, lblHistoryID, lblGoods As New Label
                'Dim dVAT As Double
                'If grdDispatchDetails.Rows.Count > 0 Then
                '    For i = 0 To grdDispatchDetails.Rows.Count - 1
                '        lblCommodityID = grdDispatchDetails.Rows(i).FindControl("lblCommodityID")
                '        lblItemID = grdDispatchDetails.Rows(i).FindControl("lblItemID")
                '        lblHistoryID = grdDispatchDetails.Rows(i).FindControl("lblHistoryID")
                '        lblGoods = grdDispatchDetails.Rows(i).FindControl("lblGoods")

                '        dVAT = objDispatch.GetItemVAT(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text, lblHistoryID.Text)
                '        If dVAT > 0 Then
                '        Else
                '            lblError.Text = "Update vat for the item " & lblGoods.Text & " "
                '            Exit Sub
                '        End If
                '    Next
                'End If
                ''To Check Item Vat 0'

                objDispatch.DM_Code = txtDispatchNo.Text
                objDispatch.DM_OrderID = objGen.SafeSQL(Trim(ddlOrderNo.SelectedValue))
                If txtOrderDate.Text <> "" Then
                    objDispatch.DM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                objDispatch.DM_SupplierID = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
                objDispatch.DM_ModeOfShipping = ddlModeOfShipping.SelectedValue
                If txtDispatchDate.Text <> "" Then
                    objDispatch.DM_DispatchDate = Date.ParseExact(Trim(txtDispatchDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If

                'Dim dDate, dSDate As Date
                'Cheque Date Comparision'
                dDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'Dim m As Integer
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Dispatch Date (" & txtDispatchDate.Text & ") should be Greater than or equal to Order Date(" & txtOrderDate.Text & ")."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtDispatchDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'

                'Check Source & Destination State Code'
                Dim sSStr As String = "" : Dim sDStr As String = ""
                If txtDeliveryFromGSTNRegNo.Text <> "" Then
                    sSStr = objDispatch.CheckStateCode(sSession.AccessCode, sSession.AccessCodeID, Trim(txtDeliveryFromGSTNRegNo.Text))
                    If sSStr = False Then
                        lblError.Text = "Delivery From GSTN Reg.No Does Not Exists."
                        Exit Sub
                    End If
                End If
                If txtDeliveryGSTNRegNo.Text <> "" Then
                    sDStr = objDispatch.CheckStateCode(sSession.AccessCode, sSession.AccessCodeID, Trim(txtDeliveryGSTNRegNo.Text))
                    If sDStr = False Then
                        lblError.Text = "Shipping To GSTN Reg.No Does Not Exists."
                        Exit Sub
                    End If
                End If
                'Check Source & Destination State Code'

                objDispatch.DM_PaymentType = ddlPaymentType.SelectedValue
                If txtShippingRate.Text <> "" Then
                    objDispatch.DM_ShippingRate = objGen.SafeSQL(Trim(txtShippingRate.Text))
                Else
                    objDispatch.DM_ShippingRate = 0
                End If

                If txtExpectedNoOfDays.Text <> "" Then
                    objDispatch.DM_ExpectedDays = txtExpectedNoOfDays.Text
                Else
                    objDispatch.DM_ExpectedDays = 0
                End If

                objDispatch.DM_Status = "W"
                objDispatch.DM_CompID = sSession.AccessCodeID
                objDispatch.DM_YearID = sSession.YearID
                objDispatch.DM_CreatedBy = sSession.UserID
                objDispatch.DM_CreatedOn = System.DateTime.Now

                objDispatch.DM_Operation = "C"
                objDispatch.DM_IPAddress = sSession.IPAddress

                If txtChequeNo.Text <> "" Then
                    objDispatch.DM_ChequeNo = txtChequeNo.Text
                Else
                    objDispatch.DM_ChequeNo = ""
                End If
                If txtChequeDate.Text <> "" Then
                    objDispatch.DM_ChequeDate = Date.ParseExact(Trim(txtChequeDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                If txtIFSCCode.Text <> "" Then
                    objDispatch.DM_IFSCCode = txtIFSCCode.Text
                Else
                    objDispatch.DM_IFSCCode = ""
                End If
                If ddlBankName.SelectedIndex > 0 Then
                    objDispatch.DM_BankName = ddlBankName.SelectedValue
                Else
                    objDispatch.DM_BankName = 0
                End If
                If txtBranch.Text <> "" Then
                    objDispatch.DM_Branch = txtBranch.Text
                Else
                    objDispatch.DM_Branch = ""
                End If

                If lblTradeDiscount.Text <> "" Then
                    objDispatch.DM_GrandDiscount = lblTradeDiscount.Text
                End If
                If lblTradeDiscountAmount.Text <> "" Then
                    objDispatch.DM_GrandDiscountAmt = lblTradeDiscountAmount.Text
                End If
                If lblGrandTotal.Text <> "" Then
                    objDispatch.DM_GrandTotal = lblGrandTotal.Text
                End If
                If lblGrandTotalAmount.Text <> "" Then
                    objDispatch.DM_GrandTotalAmt = lblGrandTotalAmount.Text
                End If

                If ddlSalesMan.SelectedIndex > 0 Then
                    objDispatch.DM_SalesManID = ddlSalesMan.SelectedValue
                Else
                    objDispatch.DM_SalesManID = 0
                End If

                If txtDispatchRefNo.Text <> "" Then
                    objDispatch.DM_DispatchRefNo = txtDispatchRefNo.Text
                Else
                    objDispatch.DM_DispatchRefNo = ""
                End If

                If txtESugamNo.Text <> "" Then
                    objDispatch.DM_ESugamNo = txtESugamNo.Text
                Else
                    objDispatch.DM_ESugamNo = ""
                End If

                If txtRemarks.Text <> "" Then
                    objDispatch.DM_Remarks = txtRemarks.Text
                Else
                    objDispatch.DM_Remarks = ""
                End If

                objDispatch.DM_SaleType = 0
                objDispatch.DM_OtherType = 0

                If ddlAllocationNo.SelectedIndex > 0 Then
                    objDispatch.DM_AllocateID = objGen.SafeSQL(Trim(ddlAllocationNo.SelectedValue))
                Else
                    objDispatch.DM_AllocateID = 0
                End If

                objDispatch.DM_TrType = 4

                If txtCompanyAddress.Text <> "" Then
                    objDispatch.DM_CompanyAddress = txtCompanyAddress.Text
                Else
                    objDispatch.DM_CompanyAddress = ""
                End If

                If txtBillingAddress.Text <> "" Then
                    objDispatch.DM_BillingAddress = txtBillingAddress.Text
                Else
                    objDispatch.DM_BillingAddress = ""
                End If

                If txtDeliveryFromAddress.Text <> "" Then
                    objDispatch.DM_DeliveryFrom = txtDeliveryFromAddress.Text
                Else
                    objDispatch.DM_DeliveryFrom = ""
                End If

                If txtDeleveryAddress.Text <> "" Then
                    objDispatch.DM_DeliveryAddress = txtDeleveryAddress.Text
                Else
                    objDispatch.DM_DeliveryAddress = ""
                End If

                If txtCompanyGSTNRegNo.Text <> "" Then
                    objDispatch.DM_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
                Else
                    objDispatch.DM_CompanyGSTNRegNo = ""
                End If

                If txtBillingGSTNRegNo.Text <> "" Then
                    objDispatch.DM_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
                Else
                    objDispatch.DM_BillingGSTNRegNo = ""
                End If

                If txtDeliveryFromGSTNRegNo.Text <> "" Then
                    objDispatch.DM_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
                Else
                    objDispatch.DM_DeliveryFromGSTNRegNo = ""
                End If

                If txtDeliveryGSTNRegNo.Text <> "" Then
                    objDispatch.DM_DeliveryGSTNRegNo = txtDeliveryGSTNRegNo.Text
                Else
                    objDispatch.DM_DeliveryGSTNRegNo = ""
                End If

                If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                    objDispatch.DM_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
                ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                    objDispatch.DM_DispatchStatus = "Local"
                ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                    objDispatch.DM_DispatchStatus = "Local"
                ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                    objDispatch.DM_DispatchStatus = "Local"
                End If
                'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                '    objDispatch.DM_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
                'End If

                objDispatch.DM_CompanyType = ddlCompanyType.SelectedValue
                objDispatch.DM_GSTNCategory = ddlGSTCategory.SelectedValue

                'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                '    objDispatch.DM_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
                'End If
                If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                    objDispatch.DM_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
                ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                    objDispatch.DM_State = CheckSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), (""))
                ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                    objDispatch.DM_State = CheckSourceDestinationState((""), Trim(txtDeliveryGSTNRegNo.Text))
                ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                    Dim ibranch As Integer
                    ibranch = objDispatch.getBranchFromPO(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
                    If ibranch > 0 Then 'branch 
                        objDispatch.DM_State = objDispatch.CheckDetailsofBranchState(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
                        If objDispatch.DM_State = "" Then
                            lblError.Text = "Update state in branch master"
                            lblCustomerValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in branch master.','', 'success');", True)
                            Exit Sub
                        End If
                    Else 'Company
                        objDispatch.DM_State = objDispatch.CheckDetailsofCompState(sSession.AccessCode, sSession.AccessCodeID)
                        If objDispatch.DM_State = "" Then
                            lblError.Text = "Update state in company master"
                            lblCustomerValidationMsg.Text = lblError.Text
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

                sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales")
                sPerm = sPerm.Remove(0, 1)
                sArray1 = sPerm.Split(",")
                iHead = sArray1(0) '1
                iGroup = sArray1(1) '29
                iSubGroup = sArray1(2) '31
                iGL = sArray1(3) '146

                'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
                sName = "Sale Of Product " & objDispatch.DM_State
                txtGLID.Text = objDispatch.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                If txtGLID.Text > 0 Then
                    'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
                Else
                    iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
                End If
                'Chart Of Accounts'

                Dim dtGSTRates As New DataTable
                dtGSTRates = objDispatch.BindGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'Extra'
                dtGSTRates.Rows.Add("0")
                'Extra'
                If dtGSTRates.Rows.Count > 0 Then
                    For x = 0 To dtGSTRates.Rows.Count - 1

                        sName = "Local GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objDispatch.DM_State & " Sale Account"
                        txtGLID.Text = objDispatch.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                        End If

                        sName = "Inter State GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objDispatch.DM_State & " Sale Account"
                        txtGLID.Text = objDispatch.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                        End If

                        sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST")
                        sPerm = sPerm.Remove(0, 1)
                        sArray1 = sPerm.Split(",")
                        iHead = sArray1(0) '1
                        iGroup = sArray1(1) '29
                        iSubGroup = sArray1(2) '31
                        iGL = sArray1(3) '146

                        sName = "OUTPUT SGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objDispatch.DM_State & " Sale Account"
                        txtGLID.Text = objDispatch.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                        End If

                        sName = "OUTPUT CGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objDispatch.DM_State & " Sale Account"
                        txtGLID.Text = objDispatch.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                        End If

                        sName = "OUTPUT IGST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objDispatch.DM_State & " Sale Account"
                        txtGLID.Text = objDispatch.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                        If txtGLID.Text > 0 Then
                            'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                        Else
                            CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                        End If

                    Next
                End If

                objDispatch.DM_OrderNo = ""
                objDispatch.DM_AllocationNo = ""
                objDispatch.DM_BatchNo = 0
                objDispatch.DM_BaseName = 0

                Arr = objDispatch.SaveDispatchMaster(sSession.AccessCode, objDispatch)
                iMasterID = Arr(1)
                txtMasterID.Text = iMasterID

                If grdDispatchDetails.Rows.Count > 0 Then
                    For i = 0 To grdDispatchDetails.Rows.Count - 1

                        lblCommodityID = grdDispatchDetails.Rows(i).FindControl("lblCommodityID")
                        lblItemID = grdDispatchDetails.Rows(i).FindControl("lblItemID")
                        lblHistoryID = grdDispatchDetails.Rows(i).FindControl("lblHistoryID")
                        lblUnitID = grdDispatchDetails.Rows(i).FindControl("lblUnitID")
                        lblCommodity = grdDispatchDetails.Rows(i).FindControl("lblCommodity")
                        lblGoods = grdDispatchDetails.Rows(i).FindControl("lblGoods")
                        lblUnit = grdDispatchDetails.Rows(i).FindControl("lblUnit")
                        lblMRP = grdDispatchDetails.Rows(i).FindControl("lblMRP")
                        lblOrderedQty = grdDispatchDetails.Rows(i).FindControl("lblOrderedQty")
                        lblTotalAmount = grdDispatchDetails.Rows(i).FindControl("lblTotal")

                        HFDiscountAmount = grdDispatchDetails.Rows(i).FindControl("HFDiscountAmount")

                        'HFVATAmount = grdDispatchDetails.Rows(i).FindControl("HFVATAmount")
                        'HFCSTAmount = grdDispatchDetails.Rows(i).FindControl("HFCSTAmount")
                        'HFExiceAmount = grdDispatchDetails.Rows(i).FindControl("HFExiceAmount")

                        HFNetAmount = grdDispatchDetails.Rows(i).FindControl("HFNetAmount")

                        ddlDiscount = grdDispatchDetails.Rows(i).FindControl("ddlDiscount")
                        'ddlVAT = grdDispatchDetails.Rows(i).FindControl("ddlVAT")
                        'ddlExice = grdDispatchDetails.Rows(i).FindControl("ddlExice")
                        'ddlCST = grdDispatchDetails.Rows(i).FindControl("ddlCST")


                        lblGSTID = grdDispatchDetails.Rows(i).FindControl("lblGSTID")
                        lblGSTRate = grdDispatchDetails.Rows(i).FindControl("lblGSTRate")
                        HFGSTAmount = grdDispatchDetails.Rows(i).FindControl("HFGSTAmount")

                        objDispatch.DD_MasterID = iMasterID
                        objDispatch.DD_CommodityID = lblCommodityID.Text
                        objDispatch.DD_DescID = lblItemID.Text
                        objDispatch.DD_HistoryID = lblHistoryID.Text
                        objDispatch.DD_UnitID = lblUnitID.Text

                        objDispatch.DD_Rate = lblMRP.Text
                        objDispatch.DD_Quantity = lblOrderedQty.Text
                        objDispatch.DD_RateAmount = lblTotalAmount.Text

                        objDispatch.DD_GST_ID = lblGSTID.Text
                        objDispatch.DD_GSTRate = lblGSTRate.Text

                        objDispatch.DD_Status = "W"
                        objDispatch.DD_CompID = sSession.AccessCodeID
                        objDispatch.DD_Operation = "C"
                        objDispatch.DD_IPAddress = sSession.IPAddress
                        objDispatch.DD_CreatedBy = sSession.UserID
                        objDispatch.DD_CreatedOn = System.DateTime.Now

                        Arr = objDispatch.SaveDispatchDetails(sSession.AccessCode, objDispatch)

                    Next
                    If Arr(0) = "2" Then
                        lblError.Text = "Successfully Updated"
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    ElseIf Arr(0) = "3" Then
                        lblError.Text = "Successfully Saved"
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    End If
                    lblStatus.Text = "Waiting For Approval"

                End If

                LoadExistingDispatchNo()
                ddlSearch.SelectedValue = iMasterID

                grdDispatchDetails.DataSource = objDispatch.BindDispatchedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, iMasterID, iAllocateID)
                grdDispatchDetails.DataBind()

                SaveCharges(iMasterID)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
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
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
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
    Private Function CreateChartOfAccounts(ByVal sName As String, ByVal iHead As Integer, ByVal iParent As Integer, ByVal iAccHead As Integer, ByVal sStatus As String, ByVal sReason As String) As Integer
        Dim sRet As String = ""
        Dim sArray As Array
        Dim objCOA As New clsChartOfAccounts
        Try
            objCOA.igl_id = 0
            objCOA.igl_head = iHead
            objCOA.igl_Parent = iParent
            objCOA.sgl_glcode = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, iAccHead, iParent)
            objCOA.sgl_Desc = objGen.SafeSQL(sName)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(sReason)
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
    Public Function CheckSourceDestinationState(ByVal sBillingAddress As String, ByVal sDeliveryAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            'sSource = sBillingAddress.Substring(0, 2)
            'sDestination = sDeliveryAddress.Substring(0, 2)

            'If sSource = sDestination Then
            '    CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
            'Else
            '    CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
            'End If
            If sBillingAddress <> "" And sDeliveryAddress = "" Then
                sSource = sBillingAddress.Substring(0, 2)
                CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sDeliveryAddress <> "" Then
                sDestination = sDeliveryAddress.Substring(0, 2)
                CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sDeliveryAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sDeliveryAddress.Substring(0, 2)
                If sSource = sDestination Then
                    CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    CheckSourceDestinationState = objDispatch.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return CheckSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckSourceDestinationState")
        End Try
    End Function
    Public Sub SaveCharges(ByVal iMasterID As Integer)
        Dim Arr() As String
        Try
            'Deleting charges Everytime & Saving'
            Dim iAllocationID As Integer
            If ddlAllocationNo.SelectedIndex > 0 Then
                iAllocationID = ddlAllocationNo.SelectedValue
            Else
                iAllocationID = 0
            End If
            objDispatch.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, iAllocationID, iMasterID)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1

                    objDispatch.C_ID = 0
                    objDispatch.C_OrderID = ddlOrderNo.SelectedValue
                    If ddlAllocationNo.SelectedIndex > 0 Then
                        objDispatch.C_AllocatedID = ddlAllocationNo.SelectedValue
                    Else
                        objDispatch.C_AllocatedID = 0
                    End If
                    objDispatch.C_DispatchID = iMasterID
                    objDispatch.C_OrderType = ""
                    objDispatch.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objDispatch.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objDispatch.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objDispatch.C_PSType = "S"
                    objDispatch.C_DelFlag = "W"
                    objDispatch.C_Status = "C"
                    objDispatch.C_CompID = sSession.AccessCodeID
                    objDispatch.C_YearID = sSession.YearID
                    objDispatch.C_CreatedBy = sSession.UserID
                    objDispatch.C_CreatedOn = System.DateTime.Now
                    objDispatch.C_Operation = "C"
                    objDispatch.C_IPAddress = sSession.IPAddress
                    objDispatch.C_SalesReturnID = 0
                    objDispatch.C_GoodsReturnID = 0

                    Arr = objDispatch.SaveCharges(sSession.AccessCode, objDispatch)
                Next
            End If
            'Charges Saving'
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveCharges")
        End Try
    End Sub

    Private Sub ddlOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrderNo.SelectedIndexChanged
        Dim iAllocateID As Integer
        Try
            lblError.Text = ""
            ClearAll()
            GenerateOrderCode()
            LoadExistingAllocateCode(ddlOrderNo.SelectedValue)

            If ddlAllocationNo.Items.Count > 1 Then
                If ddlAllocationNo.SelectedIndex > 0 Then
                    iAllocateID = ddlAllocationNo.SelectedValue
                Else
                    lblError.Text = "Select Allocation No"
                    'lblCustomerValidationMsg.Text = lblError.Text
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                iAllocateID = 0
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlOrderNo_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub ClearAll()
        Try
            ddlSearch.SelectedIndex = 0
            txtDispatchDate.Text = ""
            'ddlSalesType.SelectedIndex = 0
            txtOrderDate.Text = "" : txtShippingRate.Text = "" : ddlPaymentType.SelectedIndex = 0 : ddlParty.SelectedIndex = 0
            txtExpectedNoOfDays.Text = "" : lblShippingCharges.Text = "" : ddlModeOfShipping.SelectedIndex = 0

            txtChequeNo.Text = "" : txtChequeDate.Text = ""
            txtIFSCCode.Text = "" : ddlBankName.SelectedIndex = 0 : txtBranch.Text = ""

            grdDispatchDetails.DataSource = Nothing
            grdDispatchDetails.DataBind()

            GvCharge.DataSource = Nothing
            GvCharge.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll")
        End Try
    End Sub

    Private Sub ddlPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentType.SelectedIndexChanged
        Try
            If UCase(ddlPaymentType.SelectedItem.Text) = UCase("Cheque") Then
                'divcollapseChequeDetails.Visible = True
                divcollapseChequeDetails.Visible = False
                GetBankDDL()
            Else
                divcollapseChequeDetails.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPaymentType_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub GetBankDDL()
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Try
            sPerm = objDispatch.LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Bank", "Bank")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            BindBankName(sArray1(3))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetBankDDL")
        End Try
    End Sub
    Private Sub ddlAllocationNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAllocationNo.SelectedIndexChanged
        Dim dtAllocateOrderMaster, dtPRO As New DataTable
        Dim sOrderType As String = ""
        Dim sStr As Boolean
        Dim dtCustomer As New DataTable
        Try
            lblError.Text = ""
            Clear()
            GenerateOrderCode()
            ddlOrderNo.SelectedValue = objDispatch.GetOrderNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlAllocationNo.SelectedValue)

            sStr = objDispatch.CheckAllocationSaved(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
            If sStr = True Then
                lblError.Text = "This Allocation No has been already dispatched.(Not yet Approved)"
                Exit Sub
            End If

            If ddlAllocationNo.SelectedIndex > 0 Then
                lblError.Text = "Do you want to Re-Allocate ?."
                lblAllocation.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divAllocationType').addClass('alert alert-success');$('#ModalAllocationValidation').modal('show');", True)
            End If

            dtPRO = objDispatch.BindOrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
            If dtPRO.Rows.Count > 0 Then
                txtOrderDate.Text = objGen.FormatDtForRDBMS(dtPRO.Rows(0)("SPO_OrderDate"), "D")
                ddlParty.SelectedValue = dtPRO.Rows(0)("SPO_PartyName")
                txtCode.Value = objDispatch.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)

                If IsDBNull(dtPRO.Rows(0)("SPO_ModeOfDispatch")) = False Then
                    If dtPRO.Rows(0)("SPO_ModeOfDispatch") > 0 Then
                        ddlModeOfShipping.SelectedValue = dtPRO.Rows(0)("SPO_ModeOfDispatch")
                    Else
                        ddlModeOfShipping.SelectedIndex = 0
                    End If
                Else
                    ddlModeOfShipping.SelectedIndex = 0
                End If

                If IsDBNull(dtPRO.Rows(0)("SPO_PaymentType")) = False Then
                    ddlPaymentType.SelectedValue = dtPRO.Rows(0)("SPO_PaymentType")
                Else
                    ddlPaymentType.SelectedIndex = 0
                End If

                If ddlPaymentType.SelectedItem.Text = "Cheque" Then
                    'divcollapseChequeDetails.Visible = True
                    divcollapseChequeDetails.Visible = False
                    GetBankDDL()
                Else
                    divcollapseChequeDetails.Visible = False
                End If

                If IsDBNull(dtPRO.Rows(0)("SPO_ShippingCharge")) = False Then
                    If dtPRO.Rows(0)("SPO_ShippingCharge") = 1 Then
                        lblShippingCharges.Text = "Payable on deleivery"
                    ElseIf dtPRO.Rows(0)("SPO_ShippingCharge") = 2 Then
                        lblShippingCharges.Text = "Paid recoverable"
                    ElseIf dtPRO.Rows(0)("SPO_ShippingCharge") = 3 Then
                        lblShippingCharges.Text = "Not recoverable "
                    Else
                        lblShippingCharges.Text = ""
                    End If
                End If

                If IsDBNull(dtPRO.Rows(0)("SPO_OrderType")) = False Then
                    sOrderType = dtPRO.Rows(0)("SPO_OrderType")
                End If

                'dtCustomer = objDispatch.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                'If dtCustomer.Rows.Count > 0 Then
                '    txtBillingAddress.Text = dtCustomer.Rows(0)("BM_Address")
                '    txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("BM_GSTNRegNo")
                'End If
                dtCustomer = objDispatch.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                If dtCustomer.Rows.Count > 0 Then
                    txtBillingAddress.Text = dtCustomer.Rows(0)("BM_Address")
                    txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("BM_GSTNRegNo")
                    Dim description As String
                    description = objDispatch.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, dtCustomer.Rows(0)("BM_GSTNCategory"))
                    If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryGSTNRegNo.Enabled = True
                    End If

                End If

            End If
            dtAllocateOrderMaster = objDispatch.GetAllocatedOrderMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
            If dtAllocateOrderMaster.Rows.Count > 0 Then
                If IsDBNull(dtAllocateOrderMaster.Rows(0)("SAM_GrandDiscount")) = False Then
                    lblTradeDiscount.Text = dtAllocateOrderMaster.Rows(0)("SAM_GrandDiscount")
                Else
                    lblTradeDiscount.Text = ""
                End If
                If IsDBNull(dtAllocateOrderMaster.Rows(0)("SAM_GrandDiscountAmt")) = False Then
                    lblTradeDiscountAmount.Text = dtAllocateOrderMaster.Rows(0)("SAM_GrandDiscountAmt")
                Else
                    lblTradeDiscountAmount.Text = ""
                End If
                If IsDBNull(dtAllocateOrderMaster.Rows(0)("SAM_GrandTotal")) = False Then
                    lblGrandTotal.Text = dtAllocateOrderMaster.Rows(0)("SAM_GrandTotal")
                Else
                    lblGrandTotal.Text = ""
                End If
                If IsDBNull(dtAllocateOrderMaster.Rows(0)("SAM_GrandTotalAmt")) = False Then
                    lblGrandTotalAmount.Text = dtAllocateOrderMaster.Rows(0)("SAM_GrandTotalAmt")
                Else
                    lblGrandTotalAmount.Text = ""
                End If
            End If
            grdDispatchDetails.DataSource = objDispatch.BindAllocatedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue, Trim(ddlCompanyType.SelectedItem.Text), Trim(ddlGSTCategory.SelectedItem.Text))
            grdDispatchDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAllocationNo_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtExpectedNoOfDays.Text = ""
            txtDispatchRefNo.Text = "" : txtESugamNo.Text = "" : txtShippingRate.Text = "" : txtDispatchDate.Text = ""
            'ddlSalesType.SelectedIndex = 0
            'ddlOthers.SelectedIndex = 0 : ddlOthers.Enabled = False

            grdDispatchDetails.DataSource = Nothing
            grdDispatchDetails.DataBind()

            GvCharge.DataSource = Nothing
            GvCharge.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Clear")
        End Try
    End Sub

    Private Sub grdDispatchDetails_PreRender(sender As Object, e As EventArgs) Handles grdDispatchDetails.PreRender
        Dim dt As New DataTable
        Try
            If grdDispatchDetails.Rows.Count > 0 Then
                grdDispatchDetails.UseAccessibleHeader = True
                grdDispatchDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                grdDispatchDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdDispatchDetails_PreRender")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            RefreshAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub RefreshAll()
        Try
            lblStatus.Text = ""
            'imgbtnSave.Visible = True : imgbtnApprove.Visible = False
            lblError.Text = "" : ddlAllocationNo.Items.Clear()
            ddlParty.SelectedIndex = 0 : ddlModeOfShipping.SelectedIndex = 0 : txtDispatchNo.Text = "" : txtDispatchDate.Text = "" : txtOrderDate.Text = ""
            txtExpectedNoOfDays.Text = "" : txtDispatchRefNo.Text = "" : txtESugamNo.Text = "" : txtShippingRate.Text = ""
            ddlPaymentType.SelectedIndex = 0 : txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFSCCode.Text = "" : txtBranch.Text = "" : ddlBankName.SelectedIndex = 0
            ddlSalesMan.SelectedIndex = 0 : lblShippingCharges.Text = "" : lblGrandTotal.Text = "" : lblTradeDiscount.Text = "" : lblTradeDiscountAmount.Text = "" : lblGrandTotalAmount.Text = ""
            'ddlSalesType.SelectedIndex = 0
            txtRemarks.Text = ""
            'ddlOthers.SelectedIndex = 0 : ddlOthers.Enabled = False

            grdDispatchDetails.DataSource = Nothing
            grdDispatchDetails.DataBind()

            txtMasterID.Text = ""
            iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
            LoadOrderNo(iDefaultBranch)
            ddlOrderNo.SelectedIndex = 0
            LoadExistingDispatchNo()
            ddlSearch.SelectedIndex = 0

            Session("ChargesMaster") = Nothing
            GvCharge.DataSource = Nothing
            GvCharge.DataBind()

            txtBillingAddress.Text = "" : txtBillingGSTNRegNo.Text = "" : txtDeliveryFromAddress.Text = "" : txtDeliveryFromGSTNRegNo.Text = ""
            txtDeleveryAddress.Text = "" : txtDeliveryGSTNRegNo.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "RefreshAll")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/Sales/DispatchFormMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
        Dim dt, dtTable As New DataTable
        Try
            If ddlChargeType.SelectedIndex > 0 Then
                If txtShippingRate.Text <> "" Then
                    dt = AddCharges()
                    dtTable = objDispatch.RemoveDublicate(dt)
                    GvCharge.DataSource = dtTable
                    GvCharge.DataBind()
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

    Private Sub ddlSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearch.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dtCharge As New DataTable
        Try
            dt = objDispatch.BindDispatchMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlSearch.SelectedValue)
            If IsNothing(dt) = False Then
                If IsDBNull(dt.Rows(0)("DM_OrderID")) = False Then
                    ddlOrderNo.SelectedValue = dt.Rows(0)("DM_OrderID")
                Else
                    ddlOrderNo.SelectedIndex = 0
                End If
                If txtMasterID.Text <> "" Then
                    LoadExistingAllocateCode(ddlOrderNo.SelectedValue)
                Else
                    DashBoardAllocateCode(ddlOrderNo.SelectedValue)
                End If
                If IsDBNull(dt.Rows(0)("DM_AllocateID")) = False Then
                    ddlAllocationNo.SelectedValue = dt.Rows(0)("DM_AllocateID")
                Else
                    ddlAllocationNo.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("DM_Code")) = False Then
                    txtDispatchNo.Text = dt.Rows(0)("DM_Code")
                Else
                    txtDispatchNo.Text = ""
                End If
                txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("DM_OrderDate"), "D")
                txtDispatchDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("DM_DispatchDate"), "D")
                ddlParty.SelectedValue = dt.Rows(0)("DM_SupplierID")
                txtCode.Value = objDispatch.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)

                ddlCompanyType.SelectedValue = dt.Rows(0)("DM_CompanyType")
                BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                ddlGSTCategory.SelectedValue = dt.Rows(0)("DM_GSTNCategory")

                If IsDBNull(dt.Rows(0)("DM_ModeOfShipping")) = False Then
                    If dt.Rows(0)("DM_ModeOfShipping") > 0 Then
                        ddlModeOfShipping.SelectedValue = dt.Rows(0)("DM_ModeOfShipping")
                    Else
                        ddlModeOfShipping.SelectedIndex = 0
                    End If
                Else
                    ddlModeOfShipping.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("DM_SalesManID")) = False Then
                    If dt.Rows(0)("DM_SalesManID") > 0 Then
                        ddlSalesMan.SelectedValue = dt.Rows(0)("DM_SalesManID")
                    Else
                        ddlSalesMan.SelectedIndex = 0
                    End If
                Else
                    ddlSalesMan.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("DM_DispatchRefNo")) = False Then
                    txtDispatchRefNo.Text = dt.Rows(0)("DM_DispatchRefNo")
                Else
                    txtDispatchRefNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("DM_ESugamNo")) = False Then
                    txtESugamNo.Text = dt.Rows(0)("DM_ESugamNo")
                Else
                    txtESugamNo.Text = ""
                End If

                ddlPaymentType.SelectedValue = dt.Rows(0)("DM_PaymentType")
                If UCase(ddlPaymentType.SelectedItem.Text) = UCase("Cheque") Then
                    'divcollapseChequeDetails.Visible = True
                    divcollapseChequeDetails.Visible = False
                    GetBankDDL()
                    txtChequeNo.Text = dt.Rows(0)("DM_ChequeNo") : txtChequeDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("DM_ChequeDate"), "D")
                    txtIFSCCode.Text = dt.Rows(0)("DM_IFSCCode")
                    If IsDBNull(dt.Rows(0)("DM_BankName")) = False Then
                        If dt.Rows(0)("DM_BankName") > 0 Then
                            ddlBankName.SelectedValue = dt.Rows(0)("DM_BankName")
                        Else
                            ddlBankName.SelectedIndex = 0
                        End If
                    Else
                        ddlBankName.SelectedIndex = 0
                    End If
                    txtBranch.Text = dt.Rows(0)("DM_Branch")
                Else
                    divcollapseChequeDetails.Visible = False
                End If
                If dt.Rows(0)("DM_ExpectedDays") > 0 Then
                    txtExpectedNoOfDays.Text = dt.Rows(0)("DM_ExpectedDays")
                Else
                    txtExpectedNoOfDays.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("DM_GrandDiscount")) = False Then
                    lblTradeDiscount.Text = dt.Rows(0)("DM_GrandDiscount")
                Else
                    lblTradeDiscount.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("DM_GrandDiscountAmt")) = False Then
                    lblTradeDiscountAmount.Text = dt.Rows(0)("DM_GrandDiscountAmt")
                Else
                    lblTradeDiscountAmount.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("DM_GrandTotal")) = False Then
                    lblGrandTotal.Text = dt.Rows(0)("DM_GrandTotal")
                Else
                    lblGrandTotal.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("DM_GrandTotalAmt")) = False Then
                    lblGrandTotalAmount.Text = dt.Rows(0)("DM_GrandTotalAmt")
                Else
                    lblGrandTotalAmount.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("DM_Status")) = False Then
                    If dt.Rows(0)("DM_Status") = "A" Then
                        lblStatus.Text = "Approved"
                    Else
                        lblStatus.Text = "Waiting For Approval"
                    End If
                Else
                    lblStatus.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("DM_CompanyAddress")) = False Then
                    txtCompanyAddress.Text = dt.Rows(0)("DM_CompanyAddress")
                Else
                    txtCompanyAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("DM_CompanyGSTNRegNo")) = False Then
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("DM_CompanyGSTNRegNo")
                Else
                    txtCompanyGSTNRegNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("DM_BillingAddress")) = False Then
                    txtBillingAddress.Text = dt.Rows(0)("DM_BillingAddress")
                Else
                    txtBillingAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("DM_BillingGSTNRegNo")) = False Then
                    txtBillingGSTNRegNo.Text = dt.Rows(0)("DM_BillingGSTNRegNo")
                Else
                    txtBillingGSTNRegNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("DM_DeliveryFrom")) = False Then
                    txtDeliveryFromAddress.Text = dt.Rows(0)("DM_DeliveryFrom")
                Else
                    txtDeliveryFromAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("DM_DeliveryFromGSTNRegNo")) = False Then
                    txtDeliveryFromGSTNRegNo.Text = dt.Rows(0)("DM_DeliveryFromGSTNRegNo")
                Else
                    txtDeliveryFromGSTNRegNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("DM_DeliveryAddress")) = False Then
                    txtDeleveryAddress.Text = dt.Rows(0)("DM_DeliveryAddress")
                Else
                    txtDeleveryAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("DM_DeliveryGSTNRegNo")) = False Then
                    txtDeliveryGSTNRegNo.Text = dt.Rows(0)("DM_DeliveryGSTNRegNo")
                Else
                    txtDeliveryGSTNRegNo.Text = ""
                End If

                Dim taxcategory As String
                taxcategory = objDispatch.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                    txtDeliveryFromGSTNRegNo.Enabled = False
                Else
                    txtDeliveryFromGSTNRegNo.Enabled = True
                End If


                grdDispatchDetails.DataSource = objDispatch.BindDispatchedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlSearch.SelectedValue, 0)
                grdDispatchDetails.DataBind()

                dtCharge = objDispatch.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlSearch.SelectedValue, 0)
                GvCharge.DataSource = dtCharge
                GvCharge.DataBind()
                Session("ChargesMaster") = dtCharge
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSearch_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Dim iOrederID, iAllocationID As String
        Try
            iOrederID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(ddlOrderNo.SelectedValue)))
            iAllocationID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(ddlAllocationNo.SelectedValue)))
            Response.Redirect(String.Format("~/Sales/Allocation.aspx?OrderID={0}&AllocationID={1}", iOrederID, iAllocationID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnYes_Click")
        End Try
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Try
            lblError.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub GvCharge_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles GvCharge.ItemCommand
        Dim dt As New DataTable
        Try
            If e.CommandName = "Delete" Then
                dt = Session("ChargesMaster")
                dt.Rows.Item(e.Item.ItemIndex).Delete()
                Session("ChargesMaster") = dt
            End If
            GvCharge.DataSource = dt
            GvCharge.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub
    Private Sub chkboxFrom_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxFrom.CheckedChanged
        Try
            If chkboxFrom.Checked = True Then
                txtDeliveryFromAddress.Text = txtCompanyAddress.Text
                txtDeliveryFromGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
            Else
                txtDeliveryFromAddress.Text = ""
                txtDeliveryFromGSTNRegNo.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkboxFrom_CheckedChanged")
        End Try
    End Sub
    Private Sub chkboxTo_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxTo.CheckedChanged
        Try
            If chkboxTo.Checked = True Then
                txtDeleveryAddress.Text = txtBillingAddress.Text
                txtDeliveryGSTNRegNo.Text = txtBillingGSTNRegNo.Text
            Else
                txtDeleveryAddress.Text = ""
                txtDeliveryGSTNRegNo.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkboxTo_CheckedChanged")
        End Try
    End Sub
    Private Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Dim dt As New DataTable
        Dim taxcategory As String
        Try
            If ddlBranch.SelectedIndex > 0 Then
                dt = objDispatch.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, ddlBranch.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUSTB_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUSTB_GSTNRegNo")

                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUSTB_CompanyType")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUSTB_GSTNCategory")


                    taxcategory = objDispatch.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryFromGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNo.Enabled = True
                    End If

                End If
            Else
                dt = objDispatch.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TaxPayableCategory")

                    taxcategory = objDispatch.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    If ddlGSTCategory.SelectedValue = 1 Then
                        txtDeliveryFromGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryFromGSTNRegNo.Enabled = True
                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim bCheck As String = ""
        Try
            If ddlSearch.SelectedIndex > 0 Then
                bCheck = objDispatch.CheckApprove(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If bCheck = "A" Then
                    lblError.Text = "This DispatchNo already approved."
                    Exit Sub
                End If
                objDispatch.ApproveInvoice(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                lblError.Text = "Approved Successfully."
                lblStatus.Text = "Approved"
            Else
                lblError.Text = "Select Existing Dispatch No To Approve."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    'Private Sub chkboxSameAdd_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxSameAdd.CheckedChanged
    '    Try
    '        If chkboxSameAdd.Checked = True Then
    '            txtDeleveryAddress.Text = txtBillingAddress.Text
    '            txtDeliveryGSTNRegNo.Text = txtBillingGSTNRegNo.Text

    '            txtDeleveryAddress.Enabled = False : txtDeliveryGSTNRegNo.Enabled = False
    '        Else
    '            txtDeleveryAddress.Text = ""
    '            txtDeliveryGSTNRegNo.Text = ""
    '            txtDeleveryAddress.Enabled = True : txtDeliveryGSTNRegNo.Enabled = True
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
End Class
