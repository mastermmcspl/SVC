Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Sales_DispatchDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_DispatchDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objDispatch As New ClsDispatchDetails
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Dim iDefaultBranch As Integer
    Dim objPO As New clsPurchaseOrder
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        'imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/BackWard24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
        imgbtnApprove.ImageUrl = "~/Images/CheckMark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""

        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "INVF")
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
                        imgbtnAddCharge.Visible = False
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
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasDD", 1)
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
                'RFVddlSalesType.InitialValue = "Select Sale Type"

                iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                'LoadSaleType()
                'LoadOtherType()
                BindCompanyType()
                'BindGSTNCategory()
                LoadDispatchFormNo(0)
                LoadOrderNo(iDefaultBranch)
                LoadParty()
                LoadPaymentType()
                LoadMethodOfShiping()
                LoadExistingDispatchNo()
                lblDispatch.Visible = True : ddlSearch.Visible = True

                LoadSalesMan()
                LoadChargeType()

                If ddlPaymentType.SelectedItem.Text = "Cheque" Then
                    divcollapseChequeDetails.Visible = True
                Else
                    divcollapseChequeDetails.Visible = False
                End If

                Me.imgbtnSave.Attributes.Add("OnClick", "javascript:return ValidatePaymentType();")

                Session("ChargesMaster") = Nothing
                Dim iDisID As String = ""
                iDisID = objGen.DecryptQueryString(Request.QueryString("DID"))
                If iDisID <> "" Then
                    DashBoardOrderNo()
                    LoadDashBoardDispatchNo()
                    LoadDispatchFormNo(1)
                    ddlSearch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("DID"))
                    ddlSearch_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCompanyType")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindGSTNCategory")
        End Try
    End Sub
    Public Sub LoadDispatchFormNo(ByVal iDashBoard As Integer)
        Dim dt As New DataTable
        Try
            iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)

            dt = objDispatch.BindDispatchFormNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iDashBoard, iDefaultBranch)
            ddldispatchNo.DataSource = dt
            ddldispatchNo.DataTextField = "DM_Code"
            ddldispatchNo.DataValueField = "DM_ID"
            ddldispatchNo.DataBind()
            ddldispatchNo.Items.Insert(0, "Existing Dispatch No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDispatchFormNo")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindBankName")
        End Try
    End Sub
    'Public Sub LoadSaleType()
    '    Try
    '        ddlSalesType.Items.Insert(0, New ListItem("Select Sale Type", "0"))
    '        ddlSalesType.Items.Insert(1, New ListItem("Local Bill", "1"))
    '        ddlSalesType.Items.Insert(2, New ListItem("Inter State Bill", "2"))

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Sub LoadOtherType()
    '    Try

    '        ddlOthers.Items.Insert(0, New ListItem("Select Other Type", "0"))
    '        ddlOthers.Items.Insert(1, New ListItem("Normal", "1"))
    '        ddlOthers.Items.Insert(2, New ListItem("CST against C-Form", "2"))
    '        ddlOthers.Items.Insert(3, New ListItem("E - 1 FORM", "3"))
    '        ddlOthers.Items.Insert(4, New ListItem("F FORM - DIRECT EXPORT", "4"))
    '        ddlOthers.Items.Insert(5, New ListItem("H FORM - DEEMED EXPORT", "5"))
    '        ddlOthers.Items.Insert(6, New ListItem("I FORM - IMPORT", "6"))
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
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
            ddlSearch.DataTextField = "SDM_Code"
            ddlSearch.DataValueField = "SDM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Invoice No")
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
            ddlSearch.DataTextField = "SDM_Code"
            ddlSearch.DataValueField = "SDM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Invoice No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDashBoardDispatchNo")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingAllocateCode")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSalesMan")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadMethodOfShiping")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadChargeType")
        End Try
    End Sub
    Private Sub LoadOrderNo(ByVal iBranch As Integer)
        Try
            ddlOrderNo.DataSource = objDispatch.LoadOrderNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBranch)
            ddlOrderNo.DataTextField = "SPO_OrderCode"
            ddlOrderNo.DataValueField = "SPO_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "Select Order No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOrderNo")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DashBoardOrderNo")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DashBoardAllocateCode")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPaymentType")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadParty")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String, OrderNo As String = ""
        Dim iMasterID As Integer = 0
        Dim bCheck As String = ""
        Dim iAllocateID As Integer, iCurrency As Integer = 0, iBaseID As Integer

        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblGoods, lblUnit, lblMRP, lblOrderedQty, lblTotalAmount, lblCST As New Label
        Dim HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount As HiddenField
        Dim ddlDiscount, ddlVAT, ddlCST, ddlExice As New DropDownList
        Dim sSource As String = "" : Dim sDestination As String = "", sTime As String = ""
        Dim lblGSTRate As New Label : Dim HFGSTAmount, HFCharges, HFAmount As HiddenField
        Dim lblGSTID As New Label
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim dCValue As Double = 0
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

                If ddlAllocationNo.SelectedIndex > 0 Then
                    bCheck = objDispatch.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
                    If bCheck = True Then
                        lblError.Text = "Selected Allocation No has been dispatched already."
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                Else
                    bCheck = objDispatch.CheckOrderForDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, 0)
                    If bCheck = True Then
                        lblError.Text = "Selected Order No has been dispatched already."
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
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

                objDispatch.SDM_Code = txtDispatchNo.Text
                objDispatch.SDM_OrderID = objGen.SafeSQL(Trim(ddlOrderNo.SelectedValue))
                If txtOrderDate.Text <> "" Then
                    objDispatch.SDM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                objDispatch.SDM_SupplierID = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
                objDispatch.SDM_ModeOfShipping = ddlModeOfShipping.SelectedValue
                If txtDispatchDate.Text <> "" Then
                    objDispatch.SDM_DispatchDate = Date.ParseExact(Trim(txtDispatchDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
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

                objDispatch.SDM_PaymentType = ddlPaymentType.SelectedValue
                If txtShippingRate.Text <> "" Then
                    objDispatch.SDM_ShippingRate = objGen.SafeSQL(Trim(txtShippingRate.Text))
                Else
                    objDispatch.SDM_ShippingRate = 0
                End If

                If txtExpectedNoOfDays.Text <> "" Then
                    objDispatch.SDM_ExpectedDays = txtExpectedNoOfDays.Text
                Else
                    objDispatch.SDM_ExpectedDays = 0
                End If

                objDispatch.SDM_Status = "W"
                objDispatch.SDM_CompID = sSession.AccessCodeID
                objDispatch.SDM_YearID = sSession.YearID
                objDispatch.SDM_CreatedBy = sSession.UserID
                objDispatch.SDM_CreatedOn = System.DateTime.Now

                objDispatch.SDM_Operation = "C"
                objDispatch.SDM_IPAddress = sSession.IPAddress

                If txtChequeNo.Text <> "" Then
                    objDispatch.SDM_ChequeNo = txtChequeNo.Text
                Else
                    objDispatch.SDM_ChequeNo = ""
                End If
                If txtChequeDate.Text <> "" Then
                    objDispatch.SDM_ChequeDate = Date.ParseExact(Trim(txtChequeDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If
                If txtIFSCCode.Text <> "" Then
                    objDispatch.SDM_IFSCCode = txtIFSCCode.Text
                Else
                    objDispatch.SDM_IFSCCode = ""
                End If
                If ddlBankName.SelectedIndex > 0 Then
                    objDispatch.SDM_BankName = ddlBankName.SelectedValue
                Else
                    objDispatch.SDM_BankName = 0
                End If
                If txtBranch.Text <> "" Then
                    objDispatch.SDM_Branch = txtBranch.Text
                Else
                    objDispatch.SDM_Branch = ""
                End If

                If lblTradeDiscount.Text <> "" Then
                    objDispatch.SDM_GrandDiscount = lblTradeDiscount.Text
                End If
                If lblTradeDiscountAmount.Text <> "" Then
                    objDispatch.SDM_GrandDiscountAmt = lblTradeDiscountAmount.Text
                End If
                If lblGrandTotal.Text <> "" Then
                    objDispatch.SDM_GrandTotal = lblGrandTotal.Text
                End If
                If lblGrandTotalAmount.Text <> "" Then
                    objDispatch.SDM_GrandTotalAmt = lblGrandTotalAmount.Text
                End If

                If ddlSalesMan.SelectedIndex > 0 Then
                    objDispatch.SDM_SalesManID = ddlSalesMan.SelectedValue
                Else
                    objDispatch.SDM_SalesManID = 0
                End If

                If txtDispatchRefNo.Text <> "" Then
                    objDispatch.SDM_DispatchRefNo = txtDispatchRefNo.Text
                Else
                    objDispatch.SDM_DispatchRefNo = ""
                End If

                If txtESugamNo.Text <> "" Then
                    objDispatch.SDM_ESugamNo = txtESugamNo.Text
                Else
                    objDispatch.SDM_ESugamNo = ""
                End If

                If txtRemarks.Text <> "" Then
                    objDispatch.SDM_Remarks = txtRemarks.Text
                Else
                    objDispatch.SDM_Remarks = ""
                End If

                'If ddlSalesType.SelectedIndex > 0 Then
                '    objDispatch.SDM_SaleType = ddlSalesType.SelectedValue
                'Else
                '    objDispatch.SDM_SaleType = 0
                'End If

                'If ddlOthers.SelectedIndex > 0 Then
                '    objDispatch.SDM_OtherType = ddlOthers.SelectedValue
                'Else
                '    objDispatch.SDM_OtherType = 0
                'End If
                objDispatch.SDM_SaleType = 0
                objDispatch.SDM_OtherType = 0

                If ddlAllocationNo.SelectedIndex > 0 Then
                    objDispatch.SDM_AllocateID = objGen.SafeSQL(Trim(ddlAllocationNo.SelectedValue))
                Else
                    objDispatch.SDM_AllocateID = 0
                End If

                objDispatch.SDM_TrType = 5

                If txtCompanyAddress.Text <> "" Then
                    objDispatch.SDM_CompanyAddress = txtCompanyAddress.Text
                Else
                    objDispatch.SDM_CompanyAddress = ""
                End If

                If txtBillingAddress.Text <> "" Then
                    objDispatch.SDM_BillingAddress = txtBillingAddress.Text
                Else
                    objDispatch.SDM_BillingAddress = ""
                End If

                If txtDeliveryFromAddress.Text <> "" Then
                    objDispatch.SDM_DeliveryFrom = txtDeliveryFromAddress.Text
                Else
                    objDispatch.SDM_DeliveryFrom = ""
                End If

                If txtDeleveryAddress.Text <> "" Then
                    objDispatch.SDM_DeliveryAddress = txtDeleveryAddress.Text
                Else
                    objDispatch.SDM_DeliveryAddress = ""
                End If

                If txtCompanyGSTNRegNo.Text <> "" Then
                    objDispatch.SDM_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
                Else
                    objDispatch.SDM_CompanyGSTNRegNo = ""
                End If

                If txtBillingGSTNRegNo.Text <> "" Then
                    objDispatch.SDM_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
                Else
                    objDispatch.SDM_BillingGSTNRegNo = ""
                End If

                If txtDeliveryFromGSTNRegNo.Text <> "" Then
                    objDispatch.SDM_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
                Else
                    objDispatch.SDM_DeliveryFromGSTNRegNo = ""
                End If

                If txtDeliveryGSTNRegNo.Text <> "" Then
                    objDispatch.SDM_DeliveryGSTNRegNo = txtDeliveryGSTNRegNo.Text
                Else
                    objDispatch.SDM_DeliveryGSTNRegNo = ""
                End If

                If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                    objDispatch.SDM_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
                ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                    objDispatch.SDM_DispatchStatus = "Local"
                ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                    objDispatch.SDM_DispatchStatus = "Local"
                ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                    objDispatch.SDM_DispatchStatus = "Local"
                End If
                'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                '    objDispatch.SDM_DispatchStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
                'End If

                If ddldispatchNo.SelectedIndex > 0 Then
                    objDispatch.SDM_DispatchID = ddldispatchNo.SelectedValue
                Else
                    objDispatch.SDM_DispatchID = 0
                End If

                objDispatch.SDM_CompanyType = ddlCompanyType.SelectedValue
                objDispatch.SDM_GSTNCategory = ddlGSTCategory.SelectedValue

                objDispatch.SDM_OrderNo = ""
                objDispatch.SDM_AllocationNo = ""
                objDispatch.SDM_DispatchNo = ""
                objDispatch.SDM_BatchNo = 0
                objDispatch.SDM_BaseName = 0

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

                        HFCharges = grdDispatchDetails.Rows(i).FindControl("HFCharges")
                        HFAmount = grdDispatchDetails.Rows(i).FindControl("HFAmount")

                        lblGSTID = grdDispatchDetails.Rows(i).FindControl("lblGSTID")
                        lblGSTRate = grdDispatchDetails.Rows(i).FindControl("lblGSTRate")
                        HFGSTAmount = grdDispatchDetails.Rows(i).FindControl("HFGSTAmount")

                        objDispatch.SDD_MasterID = iMasterID
                        objDispatch.SDD_CommodityID = lblCommodityID.Text
                        objDispatch.SDD_DescID = lblItemID.Text
                        objDispatch.SDD_HistoryID = lblHistoryID.Text
                        objDispatch.SDD_UnitID = lblUnitID.Text

                        objDispatch.SDD_Rate = lblMRP.Text
                        objDispatch.SDD_Quantity = lblOrderedQty.Text
                        objDispatch.SDD_RateAmount = lblTotalAmount.Text

                        If ddlDiscount.SelectedIndex > 0 Then
                            objDispatch.SDD_Discount = ddlDiscount.SelectedItem.Text
                        Else
                            objDispatch.SDD_Discount = 0
                        End If
                        If HFDiscountAmount.Value <> "" Then
                            objDispatch.SDD_DiscountAmount = HFDiscountAmount.Value
                        Else
                            objDispatch.SDD_DiscountAmount = 0
                        End If

                        objDispatch.SDD_ChargesPeritem = HFCharges.Value
                        objDispatch.SDD_Amount = HFAmount.Value

                        objDispatch.SDD_GST_ID = lblGSTID.Text
                        objDispatch.SDD_GSTRate = lblGSTRate.Text
                        objDispatch.SDD_GSTAmount = HFGSTAmount.Value

                        objDispatch.SDD_VAT = 0
                        objDispatch.SDD_VATAmount = 0
                        objDispatch.SDD_CST = 0
                        objDispatch.SDD_CSTAmount = 0
                        objDispatch.SDD_Excise = 0
                        objDispatch.SDD_ExciseAmount = 0

                        'If ddlVAT.SelectedIndex > 0 Then
                        '    objDispatch.SDD_VAT = ddlVAT.SelectedValue
                        'Else
                        '    objDispatch.SDD_VAT = 0
                        'End If
                        'If HFVATAmount.Value <> "" Then
                        '    objDispatch.SDD_VATAmount = HFVATAmount.Value
                        'Else
                        '    objDispatch.SDD_VATAmount = 0
                        'End If

                        'If ddlCST.SelectedIndex > 0 Then
                        '    objDispatch.SDD_CST = ddlCST.SelectedValue
                        'Else
                        '    objDispatch.SDD_CST = 0
                        'End If

                        'If HFCSTAmount.Value <> "" Then
                        '    objDispatch.SDD_CSTAmount = HFCSTAmount.Value
                        'Else
                        '    objDispatch.SDD_CSTAmount = 0
                        'End If

                        'If ddlExice.SelectedIndex > 0 Then
                        '    objDispatch.SDD_Excise = ddlExice.SelectedValue
                        'Else
                        '    objDispatch.SDD_Excise = 0
                        'End If

                        'If HFExiceAmount.Value <> "" Then
                        '    objDispatch.SDD_ExciseAmount = HFExiceAmount.Value
                        'Else
                        '    objDispatch.SDD_ExciseAmount = 0
                        'End If

                        If HFNetAmount.Value <> "" Then
                            objDispatch.SDD_TotalAmount = HFNetAmount.Value
                        Else
                            objDispatch.SDD_TotalAmount = 0
                        End If

                        objDispatch.SDD_Status = "W"
                        objDispatch.SDD_CompID = sSession.AccessCodeID
                        objDispatch.SDD_Operation = "C"
                        objDispatch.SDD_IPAddress = sSession.IPAddress
                        objDispatch.SDD_CreatedBy = sSession.UserID
                        objDispatch.SDD_CreatedOn = System.DateTime.Now

                        If objDispatch.SDM_DispatchStatus = "Local" Then
                            objDispatch.SDD_SGST = objDispatch.SDD_GSTRate / 2
                            objDispatch.SDD_SGSTAmount = objDispatch.SDD_GSTAmount / 2
                            objDispatch.SDD_CGST = objDispatch.SDD_GSTRate / 2
                            objDispatch.SDD_CGSTAmount = objDispatch.SDD_GSTAmount / 2
                            objDispatch.SDD_IGST = 0
                            objDispatch.SDD_IGSTAmount = 0
                        ElseIf objDispatch.SDM_DispatchStatus = "Inter State" Then
                            objDispatch.SDD_SGST = 0
                            objDispatch.SDD_SGSTAmount = 0
                            objDispatch.SDD_CGST = 0
                            objDispatch.SDD_CGSTAmount = 0
                            objDispatch.SDD_IGST = objDispatch.SDD_GSTRate
                            objDispatch.SDD_IGSTAmount = objDispatch.SDD_GSTAmount
                        End If

                        'If UCase(ddlGSTCategory.SelectedItem.Text) = "UNRIGISTERED DEALER" Then
                        '    Dim URD_GSTRate, URD_GSTAmt As Double

                        '    URD_GSTRate = objDispatch.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
                        '    URD_GSTAmt = (((objDispatch.SDD_RateAmount - objDispatch.SDD_DiscountAmount) + objDispatch.SDD_ChargesPeritem) * URD_GSTRate) / 100

                        '    objDispatch.SDD_SGST = URD_GSTRate / 2
                        '    objDispatch.SDD_SGSTAmount = URD_GSTAmt / 2
                        '    objDispatch.SDD_CGST = URD_GSTRate / 2
                        '    objDispatch.SDD_CGSTAmount = URD_GSTAmt / 2
                        '    objDispatch.SDD_IGST = 0
                        '    objDispatch.SDD_IGSTAmount = 0
                        'End If
                        iCurrency = objDispatch.GetCurrency(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
                        iBaseID = objDispatch.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
                        If HFNetAmount.Value <> "" Then
                            If iBaseID = iCurrency Then
                                dCValue = HFNetAmount.Value
                            Else
                                dCValue = objDispatch.GetFERates(sSession.AccessCode, sSession.AccessCodeID, iCurrency, HFNetAmount.Value)
                                If dCValue = 0 Then
                                    lblError.Text = "Please set the exchange rates in Currency Master."
                                    lblCustomerValidationMsg.Text = "Please set the exchange rates in Currency Master."
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                                    Exit Sub
                                End If
                            End If
                        End If
                        If dCValue > 0 Then
                            objDispatch.SDD_FETotalAmt = dCValue
                            objDispatch.SDD_CurrencyAmt = objDispatch.GetFECRates(sSession.AccessCode, sSession.AccessCodeID, iCurrency)
                            'objDispatch.SDD_CurrencyTime = objDispatch.GetFECTime(sSession.AccessCode, sSession.AccessCodeID, iCurrency)
                            sTime = objDispatch.GetFECTime(sSession.AccessCode, sSession.AccessCodeID, iCurrency)
                            If sTime <> "" Then
                                objDispatch.SDD_CurrencyTime = sTime
                            Else
                                objDispatch.SDD_CurrencyTime = " "
                            End If
                        Else
                            objDispatch.SDD_FETotalAmt = 0
                            objDispatch.SDD_CurrencyAmt = 0
                            objDispatch.SDD_CurrencyTime = " "
                        End If
                        If iCurrency > 0 Then
                            objDispatch.SDD_Currency = iCurrency
                        End If
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

                'SaveCharges(iMasterID)
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
        Dim dtPRO As New DataTable
        Dim dtCharge As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            ClearAll()
            GenerateOrderCode()

            txtOrderType.Text = objDispatch.GetOrderType(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
            If txtOrderType.Text = "S" Then
                lblOrderType.Text = "Sales Order"

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

            ElseIf txtOrderType.Text = "O"
                lblOrderType.Text = "Cash Sales"

                sStatus = objDispatch.GetStatusOfOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                If sStatus <> "" Then
                    If sStatus = "A" Then
                        lblStatus.Text = "Approved"
                        imgbtnSave.Enabled = False : imgbtnApprove.Enabled = False
                        grdDispatchDetails.Enabled = False
                    ElseIf sStatus = "W" Then
                        lblStatus.Text = "Waiting For Approval"
                        imgbtnApprove.Enabled = False
                    End If
                Else
                    lblStatus.Text = ""
                    imgbtnSave.Enabled = True : imgbtnApprove.Enabled = True
                    grdDispatchDetails.Enabled = True
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

                    ddlPaymentType.SelectedValue = dtPRO.Rows(0)("SPO_PaymentType")
                    If ddlPaymentType.SelectedItem.Text = "Cheque" Then
                        divcollapseChequeDetails.Visible = True
                        GetBankDDL()
                    Else
                        divcollapseChequeDetails.Visible = False
                    End If

                    If IsDBNull(dtPRO.Rows(0)("SPO_GrandDiscount")) = False Then
                        lblTradeDiscount.Text = dtPRO.Rows(0)("SPO_GrandDiscount")
                    Else
                        lblTradeDiscount.Text = ""
                    End If
                    If IsDBNull(dtPRO.Rows(0)("SPO_GrandDiscountAmt")) = False Then
                        lblTradeDiscountAmount.Text = dtPRO.Rows(0)("SPO_GrandDiscountAmt")
                    Else
                        lblTradeDiscountAmount.Text = ""
                    End If
                    If IsDBNull(dtPRO.Rows(0)("SPO_GrandTotal")) = False Then
                        lblGrandTotal.Text = dtPRO.Rows(0)("SPO_GrandTotal")
                    Else
                        lblGrandTotal.Text = ""
                    End If
                    If IsDBNull(dtPRO.Rows(0)("SPO_GrandTotalAmt")) = False Then
                        lblGrandTotalAmount.Text = dtPRO.Rows(0)("SPO_GrandTotalAmt")
                    Else
                        lblGrandTotalAmount.Text = ""
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

                    If IsDBNull(dtPRO.Rows(0)("SPO_CompanyType")) = False Then
                        ddlCompanyType.SelectedValue = dtPRO.Rows(0)("SPO_CompanyType")
                    Else
                        ddlCompanyType.SelectedIndex = 0
                    End If
                    If IsDBNull(dtPRO.Rows(0)("SPO_GSTNCategory")) = False Then
                        BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                        ddlGSTCategory.SelectedValue = dtPRO.Rows(0)("SPO_GSTNCategory")
                    Else
                        ddlGSTCategory.SelectedIndex = 0
                    End If

                    If IsDBNull(dtPRO.Rows(0)("SPO_CompanyAddress")) = False Then
                        txtCompanyAddress.Text = dtPRO.Rows(0)("SPO_CompanyAddress")
                    Else
                        txtCompanyAddress.Text = ""
                    End If
                    If IsDBNull(dtPRO.Rows(0)("SPO_CompanyGSTNRegNo")) = False Then
                        txtCompanyGSTNRegNo.Text = dtPRO.Rows(0)("SPO_CompanyGSTNRegNo")
                    Else
                        txtCompanyGSTNRegNo.Text = ""
                    End If

                    If IsDBNull(dtPRO.Rows(0)("SPO_BillingAddress")) = False Then
                        txtBillingAddress.Text = dtPRO.Rows(0)("SPO_BillingAddress")
                    Else
                        txtBillingAddress.Text = ""
                    End If
                    If IsDBNull(dtPRO.Rows(0)("SPO_BillingGSTNRegNo")) = False Then
                        txtBillingGSTNRegNo.Text = dtPRO.Rows(0)("SPO_BillingGSTNRegNo")
                    Else
                        txtBillingGSTNRegNo.Text = ""
                    End If

                    If IsDBNull(dtPRO.Rows(0)("SPO_DeliveryFrom")) = False Then
                        txtDeliveryFromAddress.Text = dtPRO.Rows(0)("SPO_DeliveryFrom")
                    Else
                        txtDeliveryFromAddress.Text = ""
                    End If
                    If IsDBNull(dtPRO.Rows(0)("SPO_DeliveryFromGSTNRegNo")) = False Then
                        txtDeliveryFromGSTNRegNo.Text = dtPRO.Rows(0)("SPO_DeliveryFromGSTNRegNo")
                    Else
                        txtDeliveryFromGSTNRegNo.Text = ""
                    End If

                    If IsDBNull(dtPRO.Rows(0)("SPO_DeliveryAddress")) = False Then
                        txtDeleveryAddress.Text = dtPRO.Rows(0)("SPO_DeliveryAddress")
                    Else
                        txtDeleveryAddress.Text = ""
                    End If
                    If IsDBNull(dtPRO.Rows(0)("SPO_DeliveryGSTNRegNo")) = False Then
                        txtDeliveryGSTNRegNo.Text = dtPRO.Rows(0)("SPO_DeliveryGSTNRegNo")
                    Else
                        txtDeliveryGSTNRegNo.Text = ""
                    End If

                    grdDispatchDetails.DataSource = objDispatch.BindOralSalesData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    grdDispatchDetails.DataBind()

                    dtCharge = objDispatch.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, 0, 0)
                    GvCharge.DataSource = dtCharge
                    GvCharge.DataBind()
                    Session("ChargesMaster") = dtCharge

                End If

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
                divcollapseChequeDetails.Visible = True
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
    'Private Sub ddlSalesType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSalesType.SelectedIndexChanged
    '    Try
    '        If ddlSalesType.SelectedItem.Text = "Inter State Bill" Then
    '            ddlOthers.Enabled = True
    '        Else
    '            ddlOthers.Items.Clear()
    '            LoadOtherType()
    '            ddlOthers.Enabled = False
    '            If ddlOrderNo.SelectedIndex > 0 And ddlAllocationNo.SelectedIndex > 0 Then
    '                grdDispatchDetails.DataSource = objDispatch.BindAllocatedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
    '                grdDispatchDetails.DataBind()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
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

                ddlPaymentType.SelectedValue = dtPRO.Rows(0)("SPO_PaymentType")
                If ddlPaymentType.SelectedItem.Text = "Cheque" Then
                    divcollapseChequeDetails.Visible = True
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
                    txtOrderType.Text = dtPRO.Rows(0)("SPO_OrderType")
                End If

                dtCustomer = objDispatch.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                If dtCustomer.Rows.Count > 0 Then
                    txtBillingAddress.Text = dtCustomer.Rows(0)("BM_Address")
                    txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("BM_GSTNRegNo")
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
            grdDispatchDetails.DataSource = objDispatch.BindAllocatedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
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
            imgbtnSave.ImageUrl = "~/Images/Save24.png"
            imgbtnSave.ToolTip = "Save"
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

            txtCompanyAddress.Text = "" : txtCompanyGSTNRegNo.Text = ""
            txtBillingAddress.Text = "" : txtBillingGSTNRegNo.Text = "" : txtDeliveryFromAddress.Text = "" : txtDeliveryFromGSTNRegNo.Text = ""
            txtDeleveryAddress.Text = "" : txtDeliveryGSTNRegNo.Text = ""
            ddldispatchNo.Items.Clear()
            LoadDispatchFormNo(0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "RefreshAll")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/Sales/DispatchDetailsMaster.aspx?"), False)
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
            imgbtnSave.ImageUrl = "~/Images/Update24.png"
            imgbtnSave.ToolTip = "Update"
            dt = objDispatch.BindDispatchMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlSearch.SelectedValue)
            If IsNothing(dt) = False Then

                If IsDBNull(dt.Rows(0)("SDM_DispatchID")) = False Then
                    If dt.Rows(0)("SDM_DispatchID") > 0 Then
                        LoadDispatchFormNo(1)
                        ddldispatchNo.SelectedValue = dt.Rows(0)("SDM_DispatchID")
                    Else
                        ddldispatchNo.Items.Clear()
                    End If
                Else
                    ddldispatchNo.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("SDM_OrderID")) = False Then
                    DashBoardOrderNo()
                    ddlOrderNo.SelectedValue = dt.Rows(0)("SDM_OrderID")
                Else
                    ddlOrderNo.SelectedIndex = 0
                End If

                txtOrderType.Text = objDispatch.GetOrderType(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                If txtOrderType.Text = "S" Then
                    lblOrderType.Text = "Sales order"
                ElseIf txtOrderType.Text = "O" Then
                    lblOrderType.Text = "Cash Sales"
                End If

                If dt.Rows(0)("SDM_AllocateID") > 0 Then
                    If txtMasterID.Text <> "" Then
                        LoadExistingAllocateCode(ddlOrderNo.SelectedValue)
                    Else
                        DashBoardAllocateCode(ddlOrderNo.SelectedValue)
                    End If
                    If IsDBNull(dt.Rows(0)("SDM_AllocateID")) = False Then
                        ddlAllocationNo.SelectedValue = dt.Rows(0)("SDM_AllocateID")
                    Else
                        ddlAllocationNo.SelectedIndex = 0
                    End If
                Else
                    ddlAllocationNo.Items.Clear()
                End If

                If IsDBNull(dt.Rows(0)("SDM_Code")) = False Then
                    txtDispatchNo.Text = dt.Rows(0)("SDM_Code")
                Else
                    txtDispatchNo.Text = ""
                End If
                txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SDM_OrderDate"), "D")
                txtDispatchDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SDM_DispatchDate"), "D")
                ddlParty.SelectedValue = dt.Rows(0)("SDM_SupplierID")
                txtCode.Value = objDispatch.GetPartyCode(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)

                ddlCompanyType.SelectedValue = dt.Rows(0)("SDM_CompanyType")
                BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                ddlGSTCategory.SelectedValue = dt.Rows(0)("SDM_GSTNCategory")

                If IsDBNull(dt.Rows(0)("SDM_ModeOfShipping")) = False Then
                    If dt.Rows(0)("SDM_ModeOfShipping") > 0 Then
                        ddlModeOfShipping.SelectedValue = dt.Rows(0)("SDM_ModeOfShipping")
                    Else
                        ddlModeOfShipping.SelectedIndex = 0
                    End If
                Else
                    ddlModeOfShipping.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("SDM_SalesManID")) = False Then
                    If dt.Rows(0)("SDM_SalesManID") > 0 Then
                        ddlSalesMan.SelectedValue = dt.Rows(0)("SDM_SalesManID")
                    Else
                        ddlSalesMan.SelectedIndex = 0
                    End If
                Else
                    ddlSalesMan.SelectedIndex = 0
                End If

                'If IsDBNull(dt.Rows(0)("SDM_SaleType")) = False Then
                '    If dt.Rows(0)("SDM_SaleType") > 0 Then
                '        ddlSalesType.SelectedValue = dt.Rows(0)("SDM_SaleType")
                '    Else
                '        ddlSalesType.SelectedIndex = 0
                '    End If
                'Else
                '    ddlSalesType.SelectedIndex = 0
                'End If
                'If IsDBNull(dt.Rows(0)("SDM_OtherType")) = False Then
                '    If dt.Rows(0)("SDM_OtherType") > 0 Then
                '        ddlOthers.SelectedValue = dt.Rows(0)("SDM_OtherType")
                '    Else
                '        ddlOthers.SelectedIndex = 0
                '    End If
                'Else
                '    ddlOthers.SelectedIndex = 0
                'End If

                If IsDBNull(dt.Rows(0)("SDM_DispatchRefNo")) = False Then
                    txtDispatchRefNo.Text = dt.Rows(0)("SDM_DispatchRefNo")
                Else
                    txtDispatchRefNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_ESugamNo")) = False Then
                    txtESugamNo.Text = dt.Rows(0)("SDM_ESugamNo")
                Else
                    txtESugamNo.Text = ""
                End If

                ddlPaymentType.SelectedValue = dt.Rows(0)("SDM_PaymentType")
                If UCase(ddlPaymentType.SelectedItem.Text) = UCase("Cheque") Then
                    divcollapseChequeDetails.Visible = True
                    GetBankDDL()
                    txtChequeNo.Text = dt.Rows(0)("SDM_ChequeNo") : txtChequeDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SDM_ChequeDate"), "D")
                    txtIFSCCode.Text = dt.Rows(0)("SDM_IFSCCode")
                    If IsDBNull(dt.Rows(0)("SDM_BankName")) = False Then
                        If dt.Rows(0)("SDM_BankName") > 0 Then
                            ddlBankName.SelectedValue = dt.Rows(0)("SDM_BankName")
                        Else
                            ddlBankName.SelectedIndex = 0
                        End If
                    Else
                        ddlBankName.SelectedIndex = 0
                    End If
                    txtBranch.Text = dt.Rows(0)("SDM_Branch")
                Else
                    divcollapseChequeDetails.Visible = False
                End If
                If dt.Rows(0)("SDM_ExpectedDays") > 0 Then
                    txtExpectedNoOfDays.Text = dt.Rows(0)("SDM_ExpectedDays")
                Else
                    txtExpectedNoOfDays.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_GrandDiscount")) = False Then
                    lblTradeDiscount.Text = dt.Rows(0)("SDM_GrandDiscount")
                Else
                    lblTradeDiscount.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_GrandDiscountAmt")) = False Then
                    lblTradeDiscountAmount.Text = dt.Rows(0)("SDM_GrandDiscountAmt")
                Else
                    lblTradeDiscountAmount.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_GrandTotal")) = False Then
                    lblGrandTotal.Text = dt.Rows(0)("SDM_GrandTotal")
                Else
                    lblGrandTotal.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_GrandTotalAmt")) = False Then
                    lblGrandTotalAmount.Text = dt.Rows(0)("SDM_GrandTotalAmt")
                Else
                    lblGrandTotalAmount.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_Status")) = False Then
                    If dt.Rows(0)("SDM_Status") = "A" Then
                        lblStatus.Text = "Approved"
                    Else
                        lblStatus.Text = "Waiting For Approval"
                    End If
                Else
                    lblStatus.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_CompanyAddress")) = False Then
                    txtCompanyAddress.Text = dt.Rows(0)("SDM_CompanyAddress")
                Else
                    txtCompanyAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_CompanyGSTNRegNo")) = False Then
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("SDM_CompanyGSTNRegNo")
                Else
                    txtCompanyGSTNRegNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_BillingAddress")) = False Then
                    txtBillingAddress.Text = dt.Rows(0)("SDM_BillingAddress")
                Else
                    txtBillingAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_BillingGSTNRegNo")) = False Then
                    txtBillingGSTNRegNo.Text = dt.Rows(0)("SDM_BillingGSTNRegNo")
                Else
                    txtBillingGSTNRegNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_DeliveryFrom")) = False Then
                    txtDeliveryFromAddress.Text = dt.Rows(0)("SDM_DeliveryFrom")
                Else
                    txtDeliveryFromAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_DeliveryFromGSTNRegNo")) = False Then
                    txtDeliveryFromGSTNRegNo.Text = dt.Rows(0)("SDM_DeliveryFromGSTNRegNo")
                Else
                    txtDeliveryFromGSTNRegNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("SDM_DeliveryAddress")) = False Then
                    txtDeleveryAddress.Text = dt.Rows(0)("SDM_DeliveryAddress")
                Else
                    txtDeleveryAddress.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("SDM_DeliveryGSTNRegNo")) = False Then
                    txtDeliveryGSTNRegNo.Text = dt.Rows(0)("SDM_DeliveryGSTNRegNo")
                Else
                    txtDeliveryGSTNRegNo.Text = ""
                End If

                grdDispatchDetails.DataSource = objDispatch.BindDispatchedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlSearch.SelectedValue, 0)
                grdDispatchDetails.DataBind()

                'If txtOrderType.Text = "S" Then
                '    dtCharge = objDispatch.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, 0)
                '    GvCharge.DataSource = dtCharge
                '    GvCharge.DataBind()
                '    Session("ChargesMaster") = dtCharge
                'ElseIf txtOrderType.Text = "O"
                '    dtCharge = objDispatch.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, 0, 0)
                '    GvCharge.DataSource = dtCharge
                '    GvCharge.DataBind()
                '    Session("ChargesMaster") = dtCharge
                'End If

                dtCharge = objDispatch.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, 0, 0)
                GvCharge.DataSource = dtCharge
                GvCharge.DataBind()
                Session("ChargesMaster") = dtCharge

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSearch_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim lblCommodityID, lblItemID, lblHistoryID, lblUnitID, lblCommodity, lblGoods, lblUnit, lblOrderedQty As New Label
        Dim iOrderID, iAllocateID, iMasterID As Integer
        Dim sStatus As String = ""
        Dim iDispatchID As Integer
        Dim iZoneID, iRegionID, iAreaID, iBranchID As Integer
        Dim dtPO As New DataTable
        Try
            If ddlSearch.SelectedIndex = 0 And txtMasterID.Text = "" Then
                lblError.Text = "Select Existing DispatchNo to Approve."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlSearch.SelectedIndex > 0 Then
                iMasterID = ddlSearch.SelectedValue
            Else
                iMasterID = txtMasterID.Text
            End If

            If ddldispatchNo.SelectedIndex > 0 Then
                iDispatchID = ddldispatchNo.SelectedValue
            Else
                iDispatchID = 0
            End If

            sStatus = objDispatch.GetStatusOfDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, iMasterID)
            If UCase(sStatus) = Trim(UCase("A")) Then
                lblError.Text = "This is Already Approved."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)

            objDispatch.ApproveDispatchMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, iMasterID)
            If grdDispatchDetails.Rows.Count > 0 Then
                For i = 0 To grdDispatchDetails.Rows.Count - 1
                    lblCommodityID = grdDispatchDetails.Rows(i).FindControl("lblCommodityID")
                    lblItemID = grdDispatchDetails.Rows(i).FindControl("lblItemID")
                    lblHistoryID = grdDispatchDetails.Rows(i).FindControl("lblHistoryID")
                    lblUnitID = grdDispatchDetails.Rows(i).FindControl("lblUnitID")
                    lblCommodity = grdDispatchDetails.Rows(i).FindControl("lblCommodity")
                    lblGoods = grdDispatchDetails.Rows(i).FindControl("lblGoods")
                    lblUnit = grdDispatchDetails.Rows(i).FindControl("lblUnit")
                    lblOrderedQty = grdDispatchDetails.Rows(i).FindControl("lblOrderedQty")

                    objDispatch.UpdateStockLedgerClosingBalance(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, lblCommodityID.Text, lblItemID.Text, lblHistoryID.Text, lblOrderedQty.Text, sSession.IPAddress, ddlOrderNo.SelectedValue, iMasterID, 1, iDefaultBranch)
                Next

            End If

            If ddlAllocationNo.SelectedIndex > 0 Then
                iAllocateID = ddlAllocationNo.SelectedValue
            Else
                iAllocateID = 0
            End If
            If ddlOrderNo.SelectedIndex > 0 Then
                iOrderID = ddlOrderNo.SelectedValue
            Else
                iOrderID = 0
            End If
            objDispatch.UpdateDispatchFlag(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, iAllocateID)

            'SaveCharges(iMasterID)

            ''Creating JE For Sales once we approve'
            'Dim dtSales As New DataTable
            'dtSales = LoadSalesDetails(iMasterID)
            'If dtSales.Rows.Count > 0 Then
            '    txtBillAmount.Text = dtSales.Rows(0)("Total")
            '    GetDefaultGridSales(dtSales.Rows(0)("Total"), dtSales.Rows(0)("VATAmt"), dtSales.Rows(0)("CSTAmt"), dtSales.Rows(0)("ExciseAmt"), dtSales.Rows(0)("BasicPrice"))
            'End If
            ''Creating JE For Sales once we approve'

            dtPO = objDispatch.GetZone(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            If dtPO.Rows.Count > 0 Then
                iZoneID = dtPO.Rows(0)("SPO_ZoneID")
                iRegionID = dtPO.Rows(0)("SPO_RegionID")
                iAreaID = dtPO.Rows(0)("SPO_AreaID")
                iBranchID = dtPO.Rows(0)("SPO_BranchID")
            End If

            GetSaleItemsGrid(iDispatchID, iMasterID, txtOrderType.Text, iOrderID)

            SaveSalesJEDetails(iMasterID, iZoneID, iRegionID, iAreaID, iBranchID)

            lblError.Text = "Successfully Approved."
            lblCustomerValidationMsg.Text = "Successfully Approved."
            lblStatus.Text = "Approved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    Public Sub GetSaleItemsGrid(ByVal iDispatchID As Integer, ByVal iMasterID As Integer, ByVal sOrderType As String, ByVal iOrderID As Integer)
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

            'iParty = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            iParty = ddlParty.SelectedValue

            If sOrderType = "S" Then
                sTypeOfBill = objDBL.SQLGetDescription(sSession.AccessCode, "Select DM_Dispatchstatus From Dispatch_Master Where DM_ID=" & iDispatchID & " And DM_CompID=" & sSession.AccessCodeID & " And DM_YearID=" & sSession.YearID & " ")
                sState = objDBL.SQLGetDescription(sSession.AccessCode, "Select DM_State From Dispatch_Master Where DM_ID=" & iDispatchID & " And DM_CompID=" & sSession.AccessCodeID & " And DM_YearID=" & sSession.YearID & " ")
            ElseIf sOrderType = "O" Then
                sTypeOfBill = objDBL.SQLGetDescription(sSession.AccessCode, "Select SPO_Dispatchstatus From Sales_Proforma_Order Where SPO_ID=" & iOrderID & " And SPO_CompID=" & sSession.AccessCodeID & " And SPO_YearID=" & sSession.YearID & " ")
                sState = objDBL.SQLGetDescription(sSession.AccessCode, "Select SPO_State From Sales_Proforma_Order Where SPO_ID=" & iOrderID & " And SPO_CompID=" & sSession.AccessCodeID & " And SPO_YearID=" & sSession.YearID & " ")
            End If

            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & sSession.AccessCodeID & " "
            dtGSTRates = objDBL.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)

            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'

            If dtGSTRates.Rows.Count > 0 Then
                For k = 0 To dtGSTRates.Rows.Count - 1

                    dt1 = objDBL.SQLExecuteDataSet(sSession.AccessCode, "Select * From Sales_Dispatch_Details Where SDD_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And SDD_MasterID=" & iMasterID & " And SDD_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For z = 0 To dt1.Rows.Count - 1
                            dTotalAmt = dTotalAmt + dt1.Rows(z)("SDD_Amount")
                            dSGSTAmt = dSGSTAmt + dt1.Rows(z)("SDD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dt1.Rows(z)("SDD_CGSTAmount")
                            dIGSTAmt = dIGSTAmt + dt1.Rows(z)("SDD_IGSTAmount")
                            dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("SDD_TotalAmount"))
                        Next

                        dRow = dt.NewRow 'Item Name
                        dRow("Id") = 0
                        dRow("HeadID") = objDispatch.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Sale Of Product " & sState)
                        dRow("GLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Sale Of Product " & sState)
                        If sTypeOfBill = "Local" Then
                            dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Local GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Sale Account")
                        ElseIf sTypeOfBill = "Inter State" Then
                            dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Inter State GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Sale Account")
                        End If
                        dRow("PaymentID") = 5
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "Sale Of Material"

                        sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = 0
                        dRow("Credit") = dTotalAmt
                        dt.Rows.Add(dRow)


                        SGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        CGST = dtGSTRates.Rows(k)("GST_GSTRate") / 2
                        IGST = dtGSTRates.Rows(k)("GST_GSTRate")

                        dRow = dt.NewRow 'SGST
                        dRow("Id") = 0
                        dRow("HeadID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output SGST " & SGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 6
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "SGST"

                        sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = 0
                        dRow("Credit") = dSGSTAmt
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'CGST
                        dRow("Id") = 0
                        dRow("HeadID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output CGST " & CGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 7
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "CGST"

                        sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = 0
                        dRow("Credit") = dCGSTAmt
                        dt.Rows.Add(dRow)

                        dRow = dt.NewRow 'IGST
                        dRow("Id") = 0
                        dRow("HeadID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
                        dRow("GLID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
                        dRow("SubGLID") = objDispatch.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Output IGST " & IGST & " % " & sState & " Sale Account")
                        dRow("PaymentID") = 8
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "IGST"

                        sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
                        If sSubGL <> "" Then
                            sArray = sSubGL.Split("-")
                            dRow("SubGL") = sArray(0)
                            dRow("SubGLDescription") = sArray(1)
                        End If

                        dRow("Debit") = 0
                        dRow("Credit") = dIGSTAmt
                        dt.Rows.Add(dRow)

                        dTotalAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0 : dIGSTAmt = 0
                    End If

                Next

                dRow = dt.NewRow 'Party/Customer
                dRow("Id") = 0
                dRow("HeadID") = objDispatch.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_Head")
                dRow("GLID") = objDispatch.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_GL")
                dRow("SubGLID") = objDispatch.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "C")
                dRow("PaymentID") = 9
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Party/Customer"

                sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objDispatch.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                If sSubGL <> "" Then
                    sArray = sSubGL.Split("-")
                    dRow("SubGL") = sArray(0)
                    dRow("SubGLDescription") = sArray(1)
                End If
                dRow("Debit") = dPartyTotal
                dRow("Credit") = 0

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
    'Private Sub grdDispatchDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdDispatchDetails.RowDataBound
    '    Dim dtVAT, dtCST, dtDiscount, dtExice As New DataTable
    '    Dim ddlVAT, ddlCST, ddlDiscount, ddlExice As New DropDownList

    '    Dim lblCommodityID, lblItemID, lblHistoryID, lblOrderedQty, lblMRP, lblTotal, lblDiscountAmount, lblNetAmount, lblVATAmount, lblCSTAmount, lblExiceAmount As New Label
    '    Dim iCommodityID, IItemID, iHistoryID As Integer
    '    Dim InvVAT As String = "" : Dim InvExcise As String = ""

    '    Dim dtDispatchDetails As New DataTable
    '    Dim iVATID, iCSTID, iDiscountID, iExciseID As Integer

    '    Dim HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount As HiddenField
    '    Dim dDispatchDate As Date
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            lblCommodityID = e.Row.FindControl("lblCommodityID")
    '            lblItemID = e.Row.FindControl("lblItemID")
    '            lblHistoryID = e.Row.FindControl("lblHistoryID")

    '            iCommodityID = lblCommodityID.Text
    '            IItemID = lblItemID.Text
    '            iHistoryID = lblHistoryID.Text

    '            'If ddlOthers.SelectedIndex <> -1 Then
    '            If ddlSalesType.SelectedValue = "2" And ddlOthers.SelectedItem.Text = "Normal" Then 'Normal
    '                ddlVAT = e.Row.FindControl("ddlVAT")
    '                ddlVAT.Items.Clear()

    '                ddlCST = e.Row.FindControl("ddlCST")
    '                ddlCST.Items.Clear()
    '                If txtDispatchDate.Text <> "" Then

    '                    dDispatchDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

    '                    dtCST = objDispatch.GetVATOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dDispatchDate)
    '                    If dtCST.Rows.Count > 0 Then
    '                        ddlCST.DataSource = dtCST
    '                        ddlCST.DataTextField = "MAS_DESC"
    '                        ddlCST.DataValueField = "MAS_ID"
    '                        ddlCST.DataBind()
    '                        ddlCST.Items.Insert(0, "Select")
    '                        If dtCST.Rows.Count = 1 Then
    '                            ddlCST.SelectedValue = dtCST.Rows(0)("MAS_ID")
    '                        End If
    '                    End If
    '                    'Else
    '                    '    dtVAT = objDispatch.BindVAt(sSession.AccessCode, sSession.AccessCodeID)
    '                End If

    '                If ddlSearch.SelectedIndex > 0 Then
    '                    iCSTID = objDispatch.GetCSTFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue)
    '                    If iCSTID > 0 Then
    '                        ddlCST.SelectedValue = iCSTID
    '                    Else
    '                        ddlCST.SelectedIndex = 0
    '                    End If
    '                End If

    '            ElseIf ddlSalesType.SelectedValue = "2" And ddlOthers.SelectedValue = "2" Then 'CST
    '                ddlVAT = e.Row.FindControl("ddlVAT")
    '                If txtDispatchDate.Text <> "" Then
    '                    dDispatchDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                    dtVAT = objDispatch.GetVATOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dDispatchDate)
    '                    If dtVAT.Rows.Count > 0 Then
    '                        ddlVAT.DataSource = dtVAT
    '                        ddlVAT.DataTextField = "MAS_DESC"
    '                        ddlVAT.DataValueField = "MAS_ID"
    '                        ddlVAT.DataBind()
    '                        ddlVAT.Items.Insert(0, "Select")
    '                        If dtVAT.Rows.Count = 1 Then
    '                            ddlVAT.SelectedValue = dtVAT.Rows(0)("MAS_ID")
    '                        End If
    '                    End If
    '                    'Else
    '                    '    dtVAT = objDispatch.BindVAt(sSession.AccessCode, sSession.AccessCodeID)
    '                End If

    '                If ddlSearch.SelectedIndex > 0 Then
    '                    iVATID = objDispatch.GetVATFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue)
    '                    If iVATID > 0 Then
    '                        ddlVAT.SelectedValue = iVATID
    '                    Else
    '                        ddlVAT.SelectedIndex = 0
    '                    End If
    '                End If

    '                ddlCST = e.Row.FindControl("ddlCST")
    '                If txtDispatchDate.Text <> "" Then
    '                    dDispatchDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                    dtCST = objDispatch.GetCSTOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dDispatchDate)
    '                    If dtCST.Rows.Count > 0 Then
    '                        ddlCST.DataSource = dtCST
    '                        ddlCST.DataTextField = "MAS_DESC"
    '                        ddlCST.DataValueField = "MAS_ID"
    '                        ddlCST.DataBind()
    '                        ddlCST.Items.Insert(0, "Select")
    '                        If dtCST.Rows.Count = 1 Then
    '                            ddlCST.SelectedValue = dtCST.Rows(0)("MAS_ID")
    '                        End If
    '                    End If
    '                    'Else
    '                    '    dtVAT = objDispatch.BindVAt(sSession.AccessCode, sSession.AccessCodeID)
    '                End If

    '                If ddlSearch.SelectedIndex > 0 Then
    '                    iCSTID = objDispatch.GetCSTFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue)
    '                    If iCSTID > 0 Then
    '                        ddlCST.SelectedValue = iCSTID
    '                    Else
    '                        ddlCST.SelectedIndex = 0
    '                    End If
    '                End If

    '            ElseIf ddlSalesType.SelectedValue = "1" Then 'Local Bill
    '                ddlVAT = e.Row.FindControl("ddlVAT")
    '                If txtDispatchDate.Text <> "" Then
    '                    dDispatchDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                    dtVAT = objDispatch.GetVATOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dDispatchDate)
    '                    If dtVAT.Rows.Count > 0 Then
    '                        ddlVAT.DataSource = dtVAT
    '                        ddlVAT.DataTextField = "MAS_DESC"
    '                        ddlVAT.DataValueField = "MAS_ID"
    '                        ddlVAT.DataBind()
    '                        ddlVAT.Items.Insert(0, "Select")
    '                        If dtVAT.Rows.Count = 1 Then
    '                            ddlVAT.SelectedValue = dtVAT.Rows(0)("MAS_ID")
    '                        End If
    '                    End If
    '                    'Else
    '                    '    dtVAT = objDispatch.BindVAt(sSession.AccessCode, sSession.AccessCodeID)
    '                End If

    '                If ddlSearch.SelectedIndex > 0 Then
    '                    iVATID = objDispatch.GetVATFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue)
    '                    If iVATID > 0 Then
    '                        ddlVAT.SelectedValue = iVATID
    '                    Else
    '                        ddlVAT.SelectedIndex = 0
    '                    End If
    '                End If

    '                ddlCST = e.Row.FindControl("ddlCST")
    '                ddlCST.Items.Clear()

    '            Else
    '                ddlVAT = e.Row.FindControl("ddlVAT")
    '                If txtDispatchDate.Text <> "" Then
    '                    dDispatchDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                    dtVAT = objDispatch.GetVATOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dDispatchDate)
    '                    If dtVAT.Rows.Count > 0 Then
    '                        ddlVAT.DataSource = dtVAT
    '                        ddlVAT.DataTextField = "MAS_DESC"
    '                        ddlVAT.DataValueField = "MAS_ID"
    '                        ddlVAT.DataBind()
    '                        ddlVAT.Items.Insert(0, "Select")
    '                        If dtVAT.Rows.Count = 1 Then
    '                            ddlVAT.SelectedValue = dtVAT.Rows(0)("MAS_ID")
    '                        End If
    '                    End If
    '                    'Else
    '                    '    dtVAT = objDispatch.BindVAt(sSession.AccessCode, sSession.AccessCodeID)
    '                End If

    '                If ddlSearch.SelectedIndex > 0 Then
    '                    iVATID = objDispatch.GetVATFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue)
    '                    If iVATID > 0 Then
    '                        ddlVAT.SelectedValue = iVATID
    '                    Else
    '                        ddlVAT.SelectedIndex = 0
    '                    End If
    '                End If

    '                ddlCST = e.Row.FindControl("ddlCST")
    '                If txtDispatchDate.Text <> "" Then
    '                    dDispatchDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                    dtCST = objDispatch.GetCSTOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dDispatchDate)
    '                    If dtCST.Rows.Count > 0 Then
    '                        ddlCST.DataSource = dtCST
    '                        ddlCST.DataTextField = "MAS_DESC"
    '                        ddlCST.DataValueField = "MAS_ID"
    '                        ddlCST.DataBind()
    '                        ddlCST.Items.Insert(0, "Select")
    '                        If dtCST.Rows.Count = 1 Then
    '                            ddlCST.SelectedValue = dtCST.Rows(0)("MAS_ID")
    '                        End If
    '                    End If
    '                    'Else
    '                    '    dtVAT = objDispatch.BindVAt(sSession.AccessCode, sSession.AccessCodeID)
    '                End If

    '                If ddlSearch.SelectedIndex > 0 Then
    '                    iCSTID = objDispatch.GetCSTFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue)
    '                    If iCSTID > 0 Then
    '                        ddlCST.SelectedValue = iCSTID
    '                    Else
    '                        ddlCST.SelectedIndex = 0
    '                    End If
    '                End If

    '            End If
    '            'End If

    '            ddlDiscount = e.Row.FindControl("ddlDiscount")
    '            dtDiscount = objDispatch.BindDiscount(sSession.AccessCode, sSession.AccessCodeID)
    '            ddlDiscount.DataSource = dtDiscount
    '            ddlDiscount.DataTextField = "MAS_DESC"
    '            ddlDiscount.DataValueField = "MAS_ID"
    '            ddlDiscount.DataBind()
    '            ddlDiscount.Items.Insert(0, "Select")
    '            If ddlSearch.SelectedIndex > 0 Then
    '                iDiscountID = objDispatch.GetDiscountFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue)
    '                If iDiscountID > 0 Then
    '                    ddlDiscount.SelectedValue = iDiscountID
    '                Else
    '                    ddlDiscount.SelectedIndex = 0
    '                End If
    '            End If

    '            ddlExice = e.Row.FindControl("ddlExice")
    '            If txtDispatchDate.Text <> "" Then
    '                dDispatchDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDispatchDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                dtExice = objDispatch.GetExciseOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, dDispatchDate)
    '                If dtExice.Rows.Count > 0 Then
    '                    ddlExice.DataSource = dtExice
    '                    ddlExice.DataTextField = "MAS_DESC"
    '                    ddlExice.DataValueField = "MAS_ID"
    '                    ddlExice.DataBind()
    '                    ddlExice.Items.Insert(0, "Select")

    '                    If dtExice.Rows.Count = 1 Then
    '                        ddlExice.SelectedValue = dtExice.Rows(0)("MAS_ID")
    '                    End If
    '                End If
    '                'Else
    '                '    dtExice = objDispatch.BindExice(sSession.AccessCode, sSession.AccessCodeID)
    '            End If

    '            If ddlSearch.SelectedIndex > 0 Then
    '                iExciseID = objDispatch.GetiExciseFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue)
    '                If iExciseID > 0 Then
    '                    ddlExice.SelectedValue = iExciseID
    '                Else
    '                    ddlExice.SelectedIndex = 0
    '                End If
    '            End If

    '            lblOrderedQty = e.Row.FindControl("lblOrderedQty")
    '            lblMRP = e.Row.FindControl("lblMRP")
    '            lblTotal = e.Row.FindControl("lblTotal")
    '            lblDiscountAmount = e.Row.FindControl("lblDiscountAmount")
    '            lblNetAmount = e.Row.FindControl("lblNetAmount")
    '            lblVATAmount = e.Row.FindControl("lblVATAmount")
    '            lblCSTAmount = e.Row.FindControl("lblCSTAmount")
    '            lblExiceAmount = e.Row.FindControl("lblExiceAmount")

    '            HFDiscountAmount = e.Row.FindControl("HFDiscountAmount")
    '            HFVATAmount = e.Row.FindControl("HFVATAmount")
    '            HFCSTAmount = e.Row.FindControl("HFCSTAmount")
    '            HFExiceAmount = e.Row.FindControl("HFExiceAmount")
    '            HFNetAmount = e.Row.FindControl("HFNetAmount")

    '            'InvVAT = objDispatch.GetVATOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID)
    '            'InvExcise = objDispatch.GetExciseOnInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, iHistoryID)

    '            If ddlSearch.SelectedIndex > 0 Then
    '                dtDispatchDetails = objDispatch.BindDispatchedROWData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlSearch.SelectedValue, 0, iCommodityID, IItemID, iHistoryID)
    '                If dtDispatchDetails.Rows.Count > 0 Then
    '                    For m = 0 To dtDispatchDetails.Rows.Count - 1
    '                        lblMRP.Text = dtDispatchDetails.Rows(m)("SDD_Rate")
    '                        lblOrderedQty.Text = dtDispatchDetails.Rows(m)("SDD_Quantity")
    '                        lblTotal.Text = dtDispatchDetails.Rows(m)("SDD_RateAmount")
    '                        lblDiscountAmount.Text = dtDispatchDetails.Rows(m)("SDD_DiscountAmount")
    '                        lblVATAmount.Text = dtDispatchDetails.Rows(m)("SDD_VATAmount")
    '                        lblCSTAmount.Text = dtDispatchDetails.Rows(m)("SDD_CSTAmount")
    '                        lblExiceAmount.Text = dtDispatchDetails.Rows(m)("SDD_ExciseAmount")
    '                        lblNetAmount.Text = dtDispatchDetails.Rows(m)("SDD_TotalAmount")

    '                    Next
    '                End If

    '                'Extra code pasted inorder to work with existing data'
    '                'HFDiscountAmount.Value = 0 : HFVATAmount.Value = 0 : HFCSTAmount.Value = 0 : HFExiceAmount.Value = 0 : HFNetAmount.Value = 0
    '                'lblDiscountAmount.Text = 0 : HFDiscountAmount.Value = 0

    '                'If ddlSalesType.SelectedValue = 1 And (ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex > 0) Then 'Local
    '                '    CalculateLocalDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                'ElseIf ddlSalesType.SelectedValue = 2 And (ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0) Then 'Inter State - Normal
    '                '    CalculateNormalDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                'ElseIf ddlSalesType.SelectedIndex = 2 And (ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0) Then 'Inter State - CST
    '                '    CalculateDefaultDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                'ElseIf ddlSalesType.SelectedIndex = 2 And (ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex = -1) Then 'Inter State - EHFI Exempted
    '                '    CalculateEHFIDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)
    '                'End If
    '                'Extra code pasted inorder to work with existing data'
    '            Else
    '                If ddlSalesType.SelectedValue = "2" And (ddlOthers.SelectedItem.Text = "E - 1 FORM" Or ddlOthers.SelectedItem.Text = "F FORM - DIRECT EXPORT" Or ddlOthers.SelectedItem.Text = "H FORM - DEEMED EXPORT" Or ddlOthers.SelectedItem.Text = "I FORM - IMPORT") Then
    '                    ddlVAT.Items.Clear() : ddlCST.Items.Clear() : ddlExice.Items.Clear()
    '                    'ElseIf ddlSalesType.SelectedValue = "2" And ddlOthers.SelectedValue = "2" Then 'CST
    '                    '    If ddlCST.SelectedIndex > 0 Then
    '                    '        ddlVAT.SelectedValue = objDispatch.GetVATONCST(sSession.AccessCode, sSession.AccessCodeID, iHistoryID, ddlCST.SelectedValue)
    '                    '    End If
    '                End If

    '                'Working'
    '                'HFDiscountAmount.Value = 0 : HFVATAmount.Value = 0 : HFCSTAmount.Value = 0 : HFExiceAmount.Value = 0 : HFNetAmount.Value = 0
    '                'If ddlDiscount.SelectedIndex = 0 Then
    '                '    lblDiscountAmount.Text = 0 : HFDiscountAmount.Value = 0

    '                '    If ddlSalesType.SelectedValue = 1 And (ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex > 0) Then 'Local
    '                '        CalculateLocal(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                '    ElseIf ddlSalesType.SelectedValue = 2 And (ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0) Then 'Inter State - Normal
    '                '        CalculateNormal(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                '    ElseIf ddlSalesType.SelectedIndex = 2 And (ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0) Then 'Inter State - CST
    '                '        CalculateDefault(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                '    ElseIf ddlSalesType.SelectedIndex = 2 And (ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex = -1) Then 'Inter State - EHFI Exempted
    '                '        CalculateEHFI(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)
    '                '    End If
    '                'End If
    '                'Working'

    '                HFDiscountAmount.Value = 0 : HFVATAmount.Value = 0 : HFCSTAmount.Value = 0 : HFExiceAmount.Value = 0 : HFNetAmount.Value = 0

    '                If ddlSalesType.SelectedValue = 1 And (ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex > 0) Then 'Local
    '                    CalculateLocalDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                ElseIf ddlSalesType.SelectedValue = 2 And (ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0) Then 'Inter State - Normal
    '                    CalculateNormalDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                ElseIf ddlSalesType.SelectedIndex = 2 And (ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0) Then 'Inter State - CST
    '                    CalculateDefaultDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

    '                ElseIf ddlSalesType.SelectedIndex = 2 And (ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex = -1) Then 'Inter State - EHFI Exempted
    '                    CalculateEHFIDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)
    '                End If

    '            End If

    '            'If ddlSalesType.SelectedValue = "2" And (ddlOthers.SelectedItem.Text = "E - 1 FORM" Or ddlOthers.SelectedItem.Text = "F FORM - DIRECT EXPORT" Or ddlOthers.SelectedItem.Text = "H FORM - DEEMED EXPORT" Or ddlOthers.SelectedItem.Text = "I FORM - IMPORT") Then
    '            '    ddlVAT.Items.Clear() : ddlCST.Items.Clear() : ddlExice.Items.Clear()

    '            '    ddlDiscount.Attributes.Add("onChange", "javascript:return CalculateExemptedAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            '    ddlVAT.Attributes.Add("onChange", "javascript:return CalculateExemptedAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            '    ddlExice.Attributes.Add("onChange", "javascript:return CalculateExemptedAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            '    ddlCST.Attributes.Add("onChange", "javascript:return CalculateExemptedAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            'Else
    '            '    ddlDiscount.Attributes.Add("onChange", "javascript:return CalculateFinalAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlVAT.ClientID & "','" & lblVATAmount.ClientID & "','" & ddlCST.ClientID & "','" & lblCSTAmount.ClientID & "','" & ddlExice.ClientID & "','" & lblExiceAmount.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            '    ddlVAT.Attributes.Add("onChange", "javascript:return CalculateFinalAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlVAT.ClientID & "','" & lblVATAmount.ClientID & "','" & ddlCST.ClientID & "','" & lblCSTAmount.ClientID & "','" & ddlExice.ClientID & "','" & lblExiceAmount.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            '    ddlExice.Attributes.Add("onChange", "javascript:return CalculateFinalAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlVAT.ClientID & "','" & lblVATAmount.ClientID & "','" & ddlCST.ClientID & "','" & lblCSTAmount.ClientID & "','" & ddlExice.ClientID & "','" & lblExiceAmount.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            '    ddlCST.Attributes.Add("onChange", "javascript:return CalculateFinalAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlVAT.ClientID & "','" & lblVATAmount.ClientID & "','" & ddlCST.ClientID & "','" & lblCSTAmount.ClientID & "','" & ddlExice.ClientID & "','" & lblExiceAmount.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            'End If




    '            ddlDiscount.Attributes.Add("onChange", "javascript:return CalculateFinalAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlVAT.ClientID & "','" & lblVATAmount.ClientID & "','" & ddlCST.ClientID & "','" & lblCSTAmount.ClientID & "','" & ddlExice.ClientID & "','" & lblExiceAmount.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            ddlVAT.Attributes.Add("onChange", "javascript:return CalculateFinalAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlVAT.ClientID & "','" & lblVATAmount.ClientID & "','" & ddlCST.ClientID & "','" & lblCSTAmount.ClientID & "','" & ddlExice.ClientID & "','" & lblExiceAmount.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            ddlExice.Attributes.Add("onChange", "javascript:return CalculateFinalAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlVAT.ClientID & "','" & lblVATAmount.ClientID & "','" & ddlCST.ClientID & "','" & lblCSTAmount.ClientID & "','" & ddlExice.ClientID & "','" & lblExiceAmount.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")
    '            ddlCST.Attributes.Add("onChange", "javascript:return CalculateFinalAmount('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlVAT.ClientID & "','" & lblVATAmount.ClientID & "','" & ddlCST.ClientID & "','" & lblCSTAmount.ClientID & "','" & ddlExice.ClientID & "','" & lblExiceAmount.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFVATAmount.ClientID & "','" & HFCSTAmount.ClientID & "','" & HFExiceAmount.ClientID & "','" & HFNetAmount.ClientID & "')")

    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub CalculateDefault(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlVAT As DropDownList, ByVal lblVATAmount As Label, ByVal ddlCST As DropDownList, ByVal lblCSTAmount As Label, ByVal ddlExice As DropDownList, ByVal lblExiceAmount As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFVATAmount As HiddenField, ByVal HFCSTAmount As HiddenField, ByVal HFExiceAmount As HiddenField, ByVal HFNetAmount As HiddenField)
        Dim sBasicAmount As Double : Dim sVATAMT As Double : Dim VATAmount As Double
        Try
            lblTotal.Text = lblOrderedQty.Text * lblMRP.Text
            lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text

            If lblOrderedQty.Text <> "" And ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0 And ddlDiscount.SelectedIndex = 0 Then
                If txtCode.Value = "C" Then

                    sBasicAmount = (lblTotal.Text * 100) / (ddlVAT.SelectedItem.Text + 100)
                    sVATAMT = (lblTotal.Text - sBasicAmount)

                    lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlExice.SelectedItem.Text) / 100))
                    HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlExice.SelectedItem.Text) / 100))

                    lblCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount + lblExiceAmount.Text) * ddlCST.SelectedItem.Text) / 100))
                    HFCSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount + lblExiceAmount.Text) * ddlCST.SelectedItem.Text) / 100))

                    lblVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount + lblExiceAmount.Text) * ddlVAT.SelectedItem.Text) / 100))
                    HFVATAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount + lblExiceAmount.Text) * ddlVAT.SelectedItem.Text) / 100))

                    lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount))
                    HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount))
                End If
                If txtCode.Value = "P" Then

                    lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlExice.SelectedItem.Text) / 100))
                    HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlExice.SelectedItem.Text) / 100))

                    Dim sTotal As Double : Dim sExcise As Double
                    sTotal = lblTotal.Text
                    sExcise = lblExiceAmount.Text
                    VATAmount = ((sTotal + sExcise) * ddlVAT.SelectedItem.Text) / 100

                    lblVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((VATAmount)))
                    HFVATAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((VATAmount)))

                    lblCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal + sExcise) * ddlCST.SelectedItem.Text) / 100))
                    HFCSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal + sExcise) * ddlCST.SelectedItem.Text) / 100))

                    lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblTotal.Text))
                    HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(lblTotal.Text))
                End If

            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateDefault")
        End Try
    End Sub

    Public Sub CalculateLocal(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlVAT As DropDownList, ByVal lblVATAmount As Label, ByVal ddlCST As DropDownList, ByVal lblCSTAmount As Label, ByVal ddlExice As DropDownList, ByVal lblExiceAmount As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFVATAmount As HiddenField, ByVal HFCSTAmount As HiddenField, ByVal HFExiceAmount As HiddenField, ByVal HFNetAmount As HiddenField)
        Dim sBasicAmount As Double : Dim sVATAMT As Double : Dim VATAmount As Double
        Try
            lblTotal.Text = lblOrderedQty.Text * lblMRP.Text
            lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text

            If lblOrderedQty.Text <> "" And ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex > 0 And ddlDiscount.SelectedIndex = 0 Then
                If txtCode.Value = "C" Then

                    sBasicAmount = (lblTotal.Text * 100) / (ddlVAT.SelectedItem.Text + 100)
                    sVATAMT = (lblTotal.Text - sBasicAmount)

                    lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlExice.SelectedItem.Text) / 100))
                    HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlExice.SelectedItem.Text) / 100))

                    lblCSTAmount.Text = 0
                    HFCSTAmount.Value = 0

                    lblVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount + lblExiceAmount.Text) * ddlVAT.SelectedItem.Text) / 100))
                    HFVATAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount + lblExiceAmount.Text) * ddlVAT.SelectedItem.Text) / 100))

                    lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount))
                    HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount))
                End If
                If txtCode.Value = "P" Then

                    lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlExice.SelectedItem.Text) / 100))
                    HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlExice.SelectedItem.Text) / 100))

                    Dim sTotal As Double : Dim sExcise As Double
                    sTotal = lblTotal.Text
                    sExcise = lblExiceAmount.Text
                    VATAmount = ((sTotal + sExcise) * ddlVAT.SelectedItem.Text) / 100

                    lblVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((VATAmount)))
                    HFVATAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((VATAmount)))

                    lblCSTAmount.Text = 0
                    HFCSTAmount.Value = 0

                    lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblTotal.Text))
                    HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(lblTotal.Text))
                End If

            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateLocal")
        End Try
    End Sub

    Public Sub CalculateNormal(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlVAT As DropDownList, ByVal lblVATAmount As Label, ByVal ddlCST As DropDownList, ByVal lblCSTAmount As Label, ByVal ddlExice As DropDownList, ByVal lblExiceAmount As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFVATAmount As HiddenField, ByVal HFCSTAmount As HiddenField, ByVal HFExiceAmount As HiddenField, ByVal HFNetAmount As HiddenField)
        Dim sBasicAmount As Double : Dim sVATAMT As Double : Dim VATAmount As Double
        Try
            lblTotal.Text = lblOrderedQty.Text * lblMRP.Text
            lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text

            If lblOrderedQty.Text <> "" And ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0 And ddlDiscount.SelectedIndex = 0 Then
                If txtCode.Value = "C" Then

                    sBasicAmount = (lblTotal.Text * 100) / (ddlCST.SelectedItem.Text + 100)
                    sVATAMT = (lblTotal.Text - sBasicAmount)

                    lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlExice.SelectedItem.Text) / 100))
                    HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlExice.SelectedItem.Text) / 100))

                    lblCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount + lblExiceAmount.Text) * ddlCST.SelectedItem.Text) / 100))
                    HFCSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount + lblExiceAmount.Text) * ddlCST.SelectedItem.Text) / 100))

                    lblVATAmount.Text = 0
                    HFVATAmount.Value = 0

                    lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount))
                    HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount))
                End If
                If txtCode.Value = "P" Then

                    lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlExice.SelectedItem.Text) / 100))
                    HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlExice.SelectedItem.Text) / 100))

                    lblVATAmount.Text = 0
                    HFVATAmount.Value = 0

                    Dim sTotal As Double : Dim sExcise As Double
                    sTotal = lblTotal.Text
                    sExcise = lblExiceAmount.Text
                    lblCSTAmount.Text = ((sTotal + sExcise) * ddlCST.SelectedItem.Text) / 100
                    HFCSTAmount.Value = ((sTotal + sExcise) * ddlCST.SelectedItem.Text) / 100

                    lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblTotal.Text))
                    HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(lblTotal.Text))
                End If

            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateNormal")
        End Try
    End Sub
    Public Sub CalculateEHFI(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlVAT As DropDownList, ByVal lblVATAmount As Label, ByVal ddlCST As DropDownList, ByVal lblCSTAmount As Label, ByVal ddlExice As DropDownList, ByVal lblExiceAmount As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFVATAmount As HiddenField, ByVal HFCSTAmount As HiddenField, ByVal HFExiceAmount As HiddenField, ByVal HFNetAmount As HiddenField)
        Try
            If lblOrderedQty.Text <> "" And ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex = -1 And ddlDiscount.SelectedIndex = 0 Then
                lblTotal.Text = lblOrderedQty.Text * lblMRP.Text

                lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text
                HFNetAmount.Value = lblOrderedQty.Text * lblMRP.Text

                lblExiceAmount.Text = 0
                HFExiceAmount.Value = 0

                lblCSTAmount.Text = 0
                HFCSTAmount.Value = 0

                lblVATAmount.Text = 0
                HFVATAmount.Value = 0
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateEHFI")
        End Try
    End Sub
    'Private Sub txtDispatchDate_TextChanged(sender As Object, e As EventArgs) Handles txtDispatchDate.TextChanged
    '    Try
    '        If IsDate(txtDispatchDate.Text) = True Then
    '            If ddlOrderNo.SelectedIndex > 0 And ddlAllocationNo.SelectedIndex > 0 Then
    '                grdDispatchDetails.DataSource = objDispatch.BindAllocatedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
    '                grdDispatchDetails.DataBind()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNo_Click")
        End Try
    End Sub
    'Private Sub ddlOthers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOthers.SelectedIndexChanged
    '    Try
    '        If IsDate(txtDispatchDate.Text) = True Then
    '            If ddlOrderNo.SelectedIndex > 0 And ddlAllocationNo.SelectedIndex > 0 Then
    '                grdDispatchDetails.DataSource = objDispatch.BindAllocatedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
    '                grdDispatchDetails.DataBind()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
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
    Public Sub CalculateLocalDiscountOptional(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlVAT As DropDownList, ByVal lblVATAmount As Label, ByVal ddlCST As DropDownList, ByVal lblCSTAmount As Label, ByVal ddlExice As DropDownList, ByVal lblExiceAmount As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFVATAmount As HiddenField, ByVal HFCSTAmount As HiddenField, ByVal HFExiceAmount As HiddenField, ByVal HFNetAmount As HiddenField)
        Dim sBasicAmount As Double : Dim sVATAMT As Double : Dim VATAmount As Double
        Try
            lblTotal.Text = lblOrderedQty.Text * lblMRP.Text
            lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text

            lblDiscountAmount.Text = 0
            HFDiscountAmount.Value = 0

            If lblOrderedQty.Text <> "" And ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex > 0 And ddlDiscount.SelectedIndex > 0 Then
                If txtCode.Value = "C" Then
                    sBasicAmount = (lblTotal.Text * 100) / (ddlVAT.SelectedItem.Text + 100)
                    sVATAMT = (lblTotal.Text - sBasicAmount)

                    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((sBasicAmount * ddlDiscount.SelectedItem.Text) / 100))
                    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((sBasicAmount * ddlDiscount.SelectedItem.Text) / 100))
                End If
                If txtCode.Value = "P" Then
                    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                End If

            End If

            If txtCode.Value = "C" Then

                sBasicAmount = (lblTotal.Text * 100) / (ddlVAT.SelectedItem.Text + 100)
                sVATAMT = (lblTotal.Text - sBasicAmount)

                lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount - lblDiscountAmount.Text) * ddlExice.SelectedItem.Text) / 100))
                HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount - lblDiscountAmount.Text) * ddlExice.SelectedItem.Text) / 100))

                lblCSTAmount.Text = 0
                HFCSTAmount.Value = 0

                lblVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((((sBasicAmount - lblDiscountAmount.Text) + lblExiceAmount.Text) * ddlVAT.SelectedItem.Text) / 100))
                HFVATAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((((sBasicAmount - lblDiscountAmount.Text) + lblExiceAmount.Text) * ddlVAT.SelectedItem.Text) / 100))

                lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount - lblDiscountAmount.Text))
                HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount - lblDiscountAmount.Text))
            End If
            If txtCode.Value = "P" Then
                Dim sTotal As Double : Dim dDiscount As Double
                sTotal = lblTotal.Text
                dDiscount = lblDiscountAmount.Text

                lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal - dDiscount) * ddlExice.SelectedItem.Text) / 100))
                HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal - dDiscount) * ddlExice.SelectedItem.Text) / 100))

                Dim sExcise As Double
                sExcise = lblExiceAmount.Text

                VATAmount = (((sTotal - dDiscount) + sExcise) * ddlVAT.SelectedItem.Text) / 100

                lblVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((VATAmount)))
                HFVATAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((VATAmount)))

                lblCSTAmount.Text = 0
                HFCSTAmount.Value = 0

                lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sTotal - dDiscount))
                HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sTotal - dDiscount))
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateLocalDiscountOptional")
        End Try
    End Sub
    Public Sub CalculateNormalDiscountOptional(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlVAT As DropDownList, ByVal lblVATAmount As Label, ByVal ddlCST As DropDownList, ByVal lblCSTAmount As Label, ByVal ddlExice As DropDownList, ByVal lblExiceAmount As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFVATAmount As HiddenField, ByVal HFCSTAmount As HiddenField, ByVal HFExiceAmount As HiddenField, ByVal HFNetAmount As HiddenField)
        Dim sBasicAmount As Double : Dim sVATAMT As Double : Dim VATAmount As Double
        Try
            lblTotal.Text = lblOrderedQty.Text * lblMRP.Text
            lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text

            lblDiscountAmount.Text = 0
            HFDiscountAmount.Value = 0

            If lblOrderedQty.Text <> "" And ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0 And ddlDiscount.SelectedIndex > 0 Then
                If txtCode.Value = "C" Then
                    sBasicAmount = (lblTotal.Text * 100) / (ddlCST.SelectedItem.Text + 100)
                    sVATAMT = (lblTotal.Text - sBasicAmount)

                    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlDiscount.SelectedItem.Text) / 100))
                    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlDiscount.SelectedItem.Text) / 100))
                End If
                If txtCode.Value = "P" Then
                    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                End If
            End If

            If txtCode.Value = "C" Then

                sBasicAmount = (lblTotal.Text * 100) / (ddlCST.SelectedItem.Text + 100)
                sVATAMT = (lblTotal.Text - sBasicAmount)

                lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount - lblDiscountAmount.Text) * ddlExice.SelectedItem.Text) / 100))
                HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount - lblDiscountAmount.Text) * ddlExice.SelectedItem.Text) / 100))

                lblCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((((sBasicAmount - lblDiscountAmount.Text) + lblExiceAmount.Text) * ddlCST.SelectedItem.Text) / 100))
                HFCSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((((sBasicAmount - lblDiscountAmount.Text) + lblExiceAmount.Text) * ddlCST.SelectedItem.Text) / 100))

                lblVATAmount.Text = 0
                HFVATAmount.Value = 0

                lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount - lblDiscountAmount.Text))
                HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount - lblDiscountAmount.Text))
            End If
            If txtCode.Value = "P" Then

                Dim sTotal As Double : Dim dDiscount As Double
                sTotal = lblTotal.Text
                dDiscount = lblDiscountAmount.Text

                lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal - dDiscount) * ddlExice.SelectedItem.Text) / 100))
                HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal - dDiscount) * ddlExice.SelectedItem.Text) / 100))

                lblVATAmount.Text = 0
                HFVATAmount.Value = 0

                Dim sExcise As Double
                sExcise = lblExiceAmount.Text

                lblCSTAmount.Text = (((sTotal - dDiscount) + sExcise) * ddlCST.SelectedItem.Text) / 100
                HFCSTAmount.Value = (((sTotal - dDiscount) + sExcise) * ddlCST.SelectedItem.Text) / 100

                lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sTotal - dDiscount))
                HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sTotal - dDiscount))
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateNormalDiscountOptional")
        End Try
    End Sub
    Public Sub CalculateDefaultDiscountOptional(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlVAT As DropDownList, ByVal lblVATAmount As Label, ByVal ddlCST As DropDownList, ByVal lblCSTAmount As Label, ByVal ddlExice As DropDownList, ByVal lblExiceAmount As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFVATAmount As HiddenField, ByVal HFCSTAmount As HiddenField, ByVal HFExiceAmount As HiddenField, ByVal HFNetAmount As HiddenField)
        Dim sBasicAmount As Double : Dim sVATAMT As Double : Dim VATAmount As Double
        Try
            lblTotal.Text = lblOrderedQty.Text * lblMRP.Text
            lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text

            lblDiscountAmount.Text = 0
            HFDiscountAmount.Value = 0

            If lblOrderedQty.Text <> "" And ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0 And ddlDiscount.SelectedIndex > 0 Then
                If txtCode.Value = "C" Then
                    sBasicAmount = (lblTotal.Text * 100) / (ddlVAT.SelectedItem.Text + 100)
                    sVATAMT = (lblTotal.Text - sBasicAmount)

                    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlDiscount.SelectedItem.Text) / 100))
                    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * ddlDiscount.SelectedItem.Text) / 100))
                End If
                If txtCode.Value = "P" Then
                    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                End If
            End If
            If txtCode.Value = "C" Then

                sBasicAmount = (lblTotal.Text * 100) / (ddlVAT.SelectedItem.Text + 100)
                sVATAMT = (lblTotal.Text - sBasicAmount)

                lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount - lblDiscountAmount.Text) * ddlExice.SelectedItem.Text) / 100))
                HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount - lblDiscountAmount.Text) * ddlExice.SelectedItem.Text) / 100))

                lblCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((((sBasicAmount - lblDiscountAmount.Text) + lblExiceAmount.Text) * ddlCST.SelectedItem.Text) / 100))
                HFCSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((((sBasicAmount - lblDiscountAmount.Text) + lblExiceAmount.Text) * ddlCST.SelectedItem.Text) / 100))

                lblVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((((sBasicAmount - lblDiscountAmount.Text) + lblExiceAmount.Text) * ddlVAT.SelectedItem.Text) / 100))
                HFVATAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((((sBasicAmount - lblDiscountAmount.Text) + lblExiceAmount.Text) * ddlVAT.SelectedItem.Text) / 100))

                lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount - lblDiscountAmount.Text))
                HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sBasicAmount - lblDiscountAmount.Text))
            End If
            If txtCode.Value = "P" Then
                Dim sTotal As Double : Dim dDiscount As Double
                sTotal = lblTotal.Text
                dDiscount = lblDiscountAmount.Text

                lblExiceAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal - dDiscount) * ddlExice.SelectedItem.Text) / 100))
                HFExiceAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal - dDiscount) * ddlExice.SelectedItem.Text) / 100))

                Dim sExcise As Double

                sExcise = lblExiceAmount.Text
                VATAmount = (((sTotal - dDiscount) + sExcise) * ddlVAT.SelectedItem.Text) / 100

                lblVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((VATAmount)))
                HFVATAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((VATAmount)))

                lblCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((((sTotal - dDiscount) + sExcise) * ddlCST.SelectedItem.Text) / 100))
                HFCSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((((sTotal - dDiscount) + sExcise) * ddlCST.SelectedItem.Text) / 100))

                lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(sTotal - dDiscount))
                HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(sTotal - dDiscount))
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateDefaultDiscountOptional")
        End Try
    End Sub
    Public Sub CalculateEHFIDiscountOptional(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlVAT As DropDownList, ByVal lblVATAmount As Label, ByVal ddlCST As DropDownList, ByVal lblCSTAmount As Label, ByVal ddlExice As DropDownList, ByVal lblExiceAmount As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFVATAmount As HiddenField, ByVal HFCSTAmount As HiddenField, ByVal HFExiceAmount As HiddenField, ByVal HFNetAmount As HiddenField)
        Try
            lblTotal.Text = lblOrderedQty.Text * lblMRP.Text

            lblDiscountAmount.Text = 0
            HFDiscountAmount.Value = 0

            lblExiceAmount.Text = 0
            HFExiceAmount.Value = 0

            lblCSTAmount.Text = 0
            HFCSTAmount.Value = 0

            lblVATAmount.Text = 0
            HFVATAmount.Value = 0

            lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text
            HFNetAmount.Value = lblOrderedQty.Text * lblMRP.Text

            If lblOrderedQty.Text <> "" And ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex = -1 And ddlDiscount.SelectedIndex > 0 Then
                lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))

                lblNetAmount.Text = lblTotal.Text - lblDiscountAmount.Text
                HFNetAmount.Value = lblTotal.Text - lblDiscountAmount.Text
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateEHFIDiscountOptional")
        End Try
    End Sub
    Protected Function LoadSalesDetails(ByVal sDispatchNo As String) As DataTable
        Dim dtDetails, dt As New DataTable
        Dim dTotalAmt As Double
        Dim dVatAmt As Double : Dim dCSTAmt As Double : Dim dExciseAmt As Double : Dim dBasicPrice As Double
        Dim dtR As New DataTable
        Dim dRow As DataRow
        Try
            dtDetails = objDispatch.GetAllSalesDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sDispatchNo)
            dt = GetSinkGrid(dtDetails)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dTotalAmt = dTotalAmt + dt.Rows(i)("Total")
                    dVatAmt = dVatAmt + dt.Rows(i)("VATAmt")
                    dCSTAmt = dCSTAmt + dt.Rows(i)("CSTAmt")
                    dExciseAmt = dExciseAmt + dt.Rows(i)("ExciseAmt")
                    dBasicPrice = dBasicPrice + dt.Rows(i)("BasicPrice")
                Next
            End If

            dtR.Columns.Add("Total")
            dtR.Columns.Add("VATAmt")
            dtR.Columns.Add("CSTAmt")
            dtR.Columns.Add("ExciseAmt")
            dtR.Columns.Add("BasicPrice")

            dRow = dtR.NewRow
            dRow("Total") = dTotalAmt
            dRow("VATAmt") = dVatAmt
            dRow("CSTAmt") = dCSTAmt
            dRow("ExciseAmt") = dExciseAmt
            dRow("BasicPrice") = dBasicPrice
            dtR.Rows.Add(dRow)

            Return dtR
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSalesDetails")
        End Try
    End Function
    Public Sub GetDefaultGridSales(ByVal dTotal As Double, ByVal dVATAmt As Double, ByVal dCSTAmt As Double, ByVal dExciseAmt As Double, ByVal dBasicPrice As Double)
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sGL As String = "" : Dim sSubGL As String = ""
        Dim sArray As Array
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

            dRow = dt.NewRow

            dRow("Id") = 0
            dRow("HeadID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_Head")
            dRow("GLID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_GL")
            dRow("SubGLID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales", "Acc_SubGL")
            dRow("PaymentID") = 5
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Bill Amount"

            'sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            'If sGL <> "" Then
            '    sArray = sGL.Split("-")
            '    dRow("GLCode") = sArray(0)
            '    dRow("GLDescription") = sArray(1)
            'End If

            'sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            'If sSubGL <> "" Then
            '    sArray = sSubGL.Split("-")
            '    dRow("SubGL") = sArray(0)
            '    dRow("SubGLDescription") = sArray(1)
            'End If
            dRow("GLCode") = ""
            dRow("GLDescription") = ""

            dRow("SubGL") = ""
            dRow("SubGLDescription") = ""

            dRow("Debit") = 0
            dRow("Credit") = dBasicPrice

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'VAT

            dRow("Id") = 0
            dRow("HeadID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_Head")
            dRow("GLID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_GL")
            dRow("SubGLID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST", "Acc_SubGL")
            dRow("PaymentID") = 6
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "SGST"

            'sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            'If sGL <> "" Then
            '    sArray = sGL.Split("-")
            '    dRow("GLCode") = sArray(0)
            '    dRow("GLDescription") = sArray(1)
            'End If

            'sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            'If sSubGL <> "" Then
            '    sArray = sSubGL.Split("-")
            '    dRow("SubGL") = sArray(0)
            '    dRow("SubGLDescription") = sArray(1)
            'End If

            dRow("GLCode") = ""
            dRow("GLDescription") = ""

            dRow("SubGL") = ""
            dRow("SubGLDescription") = ""

            dRow("Debit") = 0
            dRow("Credit") = dVATAmt

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'CST

            dRow("Id") = 0
            dRow("HeadID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "CGST", "Acc_Head")
            dRow("GLID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "CGST", "Acc_GL")
            dRow("SubGLID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "CGST", "Acc_SubGL")
            dRow("PaymentID") = 7
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "CGST"

            'sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            'If sGL <> "" Then
            '    sArray = sGL.Split("-")
            '    dRow("GLCode") = sArray(0)
            '    dRow("GLDescription") = sArray(1)
            'End If

            'sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            'If sSubGL <> "" Then
            '    sArray = sSubGL.Split("-")
            '    dRow("SubGL") = sArray(0)
            '    dRow("SubGLDescription") = sArray(1)
            'End If

            dRow("GLCode") = ""
            dRow("GLDescription") = ""

            dRow("SubGL") = ""
            dRow("SubGLDescription") = ""

            dRow("Debit") = 0
            dRow("Credit") = dCSTAmt

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'Excise

            dRow("Id") = 0
            dRow("HeadID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "IGST", "Acc_Head")
            dRow("GLID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "IGST", "Acc_GL")
            dRow("SubGLID") = 0
            'objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "IGST", "Acc_SubGL")
            dRow("PaymentID") = 8
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "IGST"

            'sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            'If sGL <> "" Then
            '    sArray = sGL.Split("-")
            '    dRow("GLCode") = sArray(0)
            '    dRow("GLDescription") = sArray(1)
            'End If

            'sSubGL = objDispatch.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
            'If sSubGL <> "" Then
            '    sArray = sSubGL.Split("-")
            '    dRow("SubGL") = sArray(0)
            '    dRow("SubGLDescription") = sArray(1)
            'End If

            dRow("GLCode") = ""
            dRow("GLDescription") = ""

            dRow("SubGL") = ""
            dRow("SubGLDescription") = ""

            dRow("Debit") = 0
            dRow("Credit") = dExciseAmt

            dt.Rows.Add(dRow)

            dRow = dt.NewRow 'Party/Customer

            dRow("Id") = 0
            dRow("HeadID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_Head")
            dRow("GLID") = objDispatch.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer", "Acc_GL")
            dRow("SubGLID") = objDispatch.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "C")

            dRow("PaymentID") = 9
            dRow("SrNo") = dt.Rows.Count + 1
            dRow("Type") = "Party/Customer"

            sGL = objDispatch.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
            If sGL <> "" Then
                sArray = sGL.Split("-")
                dRow("GLCode") = sArray(0)
                dRow("GLDescription") = sArray(1)
            End If

            sSubGL = objDispatch.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), ddlParty.SelectedValue, "C")
            If sSubGL <> "" Then
                sArray = sSubGL.Split("-")
                dRow("SubGL") = sArray(0)
                dRow("SubGLDescription") = sArray(1)
            End If
            dRow("Debit") = dTotal
            dRow("Credit") = 0

            dt.Rows.Add(dRow)

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetDefaultGridSales")
        End Try
    End Sub
    Private Function CheckDebitAndCredit() As Integer
        Dim i As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Try
            For i = 0 To dgJEDetails.Items.Count - 1
                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    dDebit = dDebit + Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    dCredit = dCredit + Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                End If
            Next

            If String.Format("{0:0.00}", Convert.ToDecimal(dDebit)) <> String.Format("{0:0.00}", Convert.ToDecimal(dCredit)) Then
                Return 1  ' Debit and Credit amount not Matched
            End If

            If dDebit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(dDebit)) <> String.Format("{0:0.00}", Convert.ToDecimal(txtBillAmount.Text)) Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            Else
                If dDebit <> txtBillAmount.Text Then 'Checking debit total with total invoice bill amount
                    Return 4
                End If
            End If

            If dCredit > 0 Then
                If String.Format("{0:0.00}", Convert.ToDecimal(dCredit)) <> String.Format("{0:0.00}", Convert.ToDecimal(txtBillAmount.Text)) Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            Else
                If dCredit <> txtBillAmount.Text Then 'Checking Credit total with total invoice bill amount
                    Return 5
                End If
            End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckDebitAndCredit")
        End Try
    End Function
    Public Sub SaveSalesJEDetails(ByVal iMasterID As Integer, ByVal iZoneID As Integer, ByVal iRegionID As Integer, ByVal iAreaID As Integer, ByVal iBranchID As Integer)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Try
            iRet = CheckDebitAndCredit()

            If iRet = 1 Then
                lblCustomerValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 2 Then
                lblCustomerValidationMsg.Text = "Amount Not Matched with Advance Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 3 Then
                lblCustomerValidationMsg.Text = "Amount Not Matched with Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 4 Then
                lblCustomerValidationMsg.Text = "Total Debit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 5 Then
                lblCustomerValidationMsg.Text = "Total Credit Amount Not Matched with Invoice Total Bill Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            objDispatch.iAcc_JE_ID = 0
            objDispatch.sAcc_JE_TransactionNo = objDispatch.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "S")

            If ddlParty.SelectedIndex > 0 Then
                objDispatch.iAcc_JE_Party = ddlParty.SelectedValue
            Else
                objDispatch.iAcc_JE_Party = 0
            End If
            objDispatch.iAcc_JE_Location = objDispatch.GetBranchID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            objDispatch.iAcc_JE_BillType = 0

            objDispatch.iAcc_JE_InvoiceID = iMasterID
            objDispatch.sAcc_JE_BillNo = txtDispatchNo.Text
            objDispatch.dAcc_JE_BillDate = Date.ParseExact(txtDispatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objDispatch.dAcc_JE_BillAmount = txtBillAmount.Text
            objDispatch.iAcc_JE_YearID = sSession.YearID
            objDispatch.sAcc_JE_Status = "W"
            If ddldispatchNo.SelectedIndex > 0 And ddlAllocationNo.SelectedIndex > 0 Then
                objDispatch.sAcc_JE_Type = "SI"
            Else
                objDispatch.sAcc_JE_Type = "CS"
            End If

            objDispatch.iAcc_JE_CreatedBy = sSession.UserID
            objDispatch.iAcc_JE_CreatedOn = DateTime.Today
            objDispatch.sAcc_JE_Operation = "C"
            objDispatch.sAcc_JE_IPAddress = sSession.IPAddress
            objDispatch.dAcc_JE_BillCreatedDate = DateTime.Today

            objDispatch.dAcc_JE_AdvanceAmount = 0.00
            objDispatch.dAcc_JE_BalanceAmount = 0.00
            objDispatch.dAcc_JE_NetAmount = 0.00
            objDispatch.sAcc_JE_AdvanceNaration = ""
            objDispatch.sAcc_JE_PaymentNarration = ""
            objDispatch.sAcc_JE_ChequeNo = ""
            objDispatch.sAcc_JE_IFSCCode = ""
            objDispatch.sAcc_JE_BankName = ""
            objDispatch.sAcc_JE_BranchName = ""

            objDispatch.iAcc_JE_UpdatedBy = sSession.UserID
            objDispatch.iAcc_JE_UpdatedOn = DateTime.Today
            objDispatch.iAcc_JE_CompID = sSession.AccessCodeID


            If objDispatch.sAcc_JE_TransactionNo <> "" Then
                If objDispatch.sAcc_JE_TransactionNo.StartsWith("S") Then
                    Arr = objDispatch.SaveSalesJournalMaster(sSession.AccessCode, objDispatch)
                    iTransID = Arr(1)
                End If
            End If

            For i = 0 To dgJEDetails.Items.Count - 1

                If objDispatch.sAcc_JE_TransactionNo.StartsWith("S") Then
                    objDispatch.iATD_TrType = 6
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objDispatch.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objDispatch.iATD_ID = 0
                End If

                objDispatch.dATD_TransactionDate = DateTime.Today

                objDispatch.iATD_BillId = iTransID
                objDispatch.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objDispatch.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objDispatch.iATD_Head = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objDispatch.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objDispatch.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objDispatch.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objDispatch.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objDispatch.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objDispatch.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objDispatch.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objDispatch.dATD_Credit = 0
                End If

                If objDispatch.dATD_Debit > 0 And objDispatch.dATD_Credit = 0 Then
                    objDispatch.iATD_DbOrCr = 1 'Debit
                ElseIf objDispatch.dATD_Debit = 0 And objDispatch.dATD_Credit > 0 Then
                    objDispatch.iATD_DbOrCr = 2 'Credit
                End If

                objDispatch.iATD_CreatedBy = sSession.UserID
                objDispatch.dATD_CreatedOn = DateTime.Today

                objDispatch.sATD_Status = "W"
                objDispatch.iATD_YearID = sSession.YearID
                objDispatch.sATD_Operation = "C"
                objDispatch.sATD_IPAddress = sSession.IPAddress

                objDispatch.iATD_UpdatedBy = sSession.UserID
                objDispatch.dATD_UpdatedOn = DateTime.Today

                objDispatch.iATD_CompID = sSession.AccessCodeID

                objDispatch.iATD_ZoneID = iZoneID
                objDispatch.iATD_RegionID = iRegionID
                objDispatch.iATD_AreaID = iAreaID
                objDispatch.iATD_BranchID = iBranchID

                objDispatch.dATD_OpenDebit = "0.00"
                objDispatch.dATD_OpenCredit = "0.00"
                objDispatch.dATD_ClosingDebit = "0.00"
                objDispatch.dATD_ClosingCredit = "0.00"
                objDispatch.iATD_SeqReferenceNum = 0

                objDispatch.SaveUpdateTransactionDetails(sSession.AccessCode, objDispatch)

            Next

            lblCustomerValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            dgJEDetails.DataSource = objDispatch.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID, objDispatch.sAcc_JE_TransactionNo)
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveSalesJEDetails")
        End Try
    End Sub
    Protected Function GetSinkGrid(ByVal dtData As DataTable) As DataTable
        Dim dt, dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim sOrderNO As String = "" : Dim sCommodity As String = ""

        Dim iTotalOrderedQty, iTotalAllocatedQty, iTotalDispatchedQty, iTotalPendingQty As Double
        Dim dTotalDiscount, dTotalDiscountAmt, dMRPRate As Double
        Dim dTotalVATAmt As Double
        Dim dTotalCST, dTotalCSTAmt As Double
        Dim dTotalExcise, dTotalExciseAmt As Double
        Dim dBasicAmt, dTotalNetAmt As Double

        Dim iOrderID As Integer
        Dim dtDetails As New DataTable
        Dim sOrderID As String = "" : Dim sParty As String = "" : Dim sOrderDate As String = "" : Dim sDispatchDate As String = ""
        Dim dShipping, dTradeDiscount, dTradeDiscountAmt As Double

        Dim sDispatchNo As String = "" : Dim sDispatchRefNo As String = ""
        Dim dUnitTotal, dVAT, dCST, dExcise As Double
        Dim dTotalVAT As Double

        Dim sVat As String = "" : Dim sVatD As String = ""
        Dim sVatA As String = "" : Dim sVatDA As String = ""
        Dim dVatSingleAmt As Double
        Dim dLastVatAmt As Double
        Dim sDisplayVat As String = ""

        Dim sCst As String = "" : Dim sCstD As String = ""
        Dim sCstA As String = "" : Dim sCstDA As String = ""
        Dim dCstSingleAmt As Double
        Dim dLastCstAmt As Double
        Dim sDisplayCst As String = ""

        Dim sArrayV As String() : Dim sbretV As String = "" : Dim sRVat As String = ""
        Dim sArrayC As String() : Dim sbretC As String = "" : Dim sRCst As String = ""

        Dim ReturnStr, temp, ReturnStrC, tempC As String
        Dim sInvoice, sInvoiceStr As String : Dim sInvoiceDate, sInvoiceDateStr As String

        Dim sStrDRefNo As String = ""
        Try
            dt1.Columns.Add("OrderNo")
            dt1.Columns.Add("Commodity")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("OrderDate")
            dt1.Columns.Add("Party")
            dt1.Columns.Add("DispatchedNo")
            dt1.Columns.Add("DispatchRefNo")
            dt1.Columns.Add("DispatchedDate")
            dt1.Columns.Add("ShippingRate")
            dt1.Columns.Add("MRPRate")
            dt1.Columns.Add("OrderedQty")
            dt1.Columns.Add("AllocatedQty")
            dt1.Columns.Add("PendingQty")
            dt1.Columns.Add("DispatchedQty")
            dt1.Columns.Add("TradeDiscount")
            dt1.Columns.Add("TradeDiscountAmt")
            dt1.Columns.Add("Discount")
            dt1.Columns.Add("DiscountAmt")
            dt1.Columns.Add("VAT")
            dt1.Columns.Add("VATAmt")
            dt1.Columns.Add("CST")
            dt1.Columns.Add("CSTAmt")
            dt1.Columns.Add("Excise")
            dt1.Columns.Add("ExciseAmt")
            dt1.Columns.Add("BasicPrice")
            dt1.Columns.Add("Total")

            dt = dtData
            Dim dview As New DataView(dt)

            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    iOrderID = dt.Rows(j)("SDM_OrderID")

                    If sOrderID.Contains(iOrderID) = False Then
                        If (iOrderID > 0) Then
                            dview = dt.DefaultView
                            dview.RowFilter = "SDM_OrderID='" & iOrderID & "'"
                            dtDetails = dview.ToTable
                            sOrderID = sOrderID & "," & iOrderID

                            If dtDetails.Rows.Count > 0 Then
                                For i = 0 To dtDetails.Rows.Count - 1
                                    dRow = dt1.NewRow

                                    dRow("OrderNo") = dtDetails.Rows(i)("SPO_OrderCode")
                                    If dRow("OrderNo") = sOrderNO Then
                                        dRow("OrderNo") = ""
                                    End If
                                    sOrderNO = dtDetails.Rows(i)("SPO_OrderCode")

                                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                                    If dRow("Commodity") = sCommodity Then
                                        dRow("Commodity") = ""
                                    End If
                                    sCommodity = dtDetails.Rows(i)("Commodity")

                                    dRow("Description") = dtDetails.Rows(i)("Item")

                                    dRow("Party") = dtDetails.Rows(i)("Party")
                                    sParty = dtDetails.Rows(i)("Party")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_Code")) = False Then
                                        dRow("DispatchedNo") = dtDetails.Rows(i)("SDM_Code")
                                        sDispatchNo = sDispatchNo & "," & dtDetails.Rows(i)("SDM_Code")
                                    Else
                                        dRow("DispatchedNo") = ""
                                        sDispatchNo = sDispatchNo & "," & ""
                                    End If

                                    sDispatchRefNo = sStrDRefNo

                                    dShipping = dShipping + dtDetails.Rows(i)("SDM_ShippingRate")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_ShippingRate")) = False Then
                                        dRow("ShippingRate") = dtDetails.Rows(i)("SDM_ShippingRate")
                                    Else
                                        dRow("ShippingRate") = ""
                                    End If


                                    dRow("MRPRate") = dtDetails.Rows(i)("SDD_Rate")
                                    dMRPRate = dMRPRate + dRow("MRPRate")

                                    dRow("OrderedQty") = dtDetails.Rows(i)("SPOD_Quantity")

                                    If IsDBNull(dtDetails.Rows(i)("SAD_PlacedQnt")) = False Then
                                        dRow("AllocatedQty") = dtDetails.Rows(i)("SAD_PlacedQnt")
                                        iTotalAllocatedQty = iTotalAllocatedQty + dRow("AllocatedQty")
                                    End If

                                    dRow("PendingQty") = dtDetails.Rows(i)("SAD_PendingQty")

                                    dRow("DispatchedQty") = dtDetails.Rows(i)("SDD_Quantity")
                                    iTotalDispatchedQty = iTotalDispatchedQty + dRow("DispatchedQty")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_GrandDiscount")) = False Then
                                        dRow("TradeDiscount") = ""
                                        dTradeDiscount = dtDetails.Rows(i)("SDM_GrandDiscount")
                                    Else
                                        dRow("TradeDiscount") = ""
                                    End If

                                    If IsDBNull(dtDetails.Rows(i)("SDM_GrandDiscountAmt")) = False Then
                                        dRow("TradeDiscountAmt") = ""
                                        dTradeDiscountAmt = dtDetails.Rows(i)("SDM_GrandDiscountAmt")
                                    Else
                                        dRow("TradeDiscountAmt") = ""
                                    End If

                                    dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Discount")))
                                    dTotalDiscount = dRow("Discount")

                                    dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_DiscountAmount")))
                                    dTotalDiscountAmt = dTotalDiscountAmt + dRow("DiscountAmt")

                                    sVat = sVat & "," & dtDetails.Rows(i)("SDD_Vat")

                                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_SGSTAmount")))
                                    dLastVatAmt = dLastVatAmt + dRow("VATAmt")

                                    sCst = sCst & "," & dtDetails.Rows(i)("SDD_CST")

                                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CGSTAmount")))
                                    dLastCstAmt = dLastCstAmt + dRow("CSTAmt")

                                    dRow("Excise") = dtDetails.Rows(i)("SDD_IGST")
                                    dTotalExcise = dTotalExcise + dRow("Excise")

                                    dRow("ExciseAmt") = dtDetails.Rows(i)("SDD_IGSTAmount")
                                    dTotalExciseAmt = dTotalExciseAmt + dRow("ExciseAmt")

                                    dRow("BasicPrice") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Amount")))
                                    dBasicAmt = dBasicAmt + dRow("BasicPrice")

                                    dUnitTotal = dtDetails.Rows(i)("SDD_TotalAmount")
                                    dVAT = dtDetails.Rows(i)("SDD_SGSTAmount")
                                    dCST = dtDetails.Rows(i)("SDD_CGSTAmount")
                                    dExcise = dtDetails.Rows(i)("SDD_IGSTAmount")

                                    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dUnitTotal))
                                    dTotalNetAmt = dTotalNetAmt + dtDetails.Rows(i)("SDD_TotalAmount")

                                    dUnitTotal = 0 : dVAT = 0 : dCST = 0 : dExcise = 0
                                Next

                                If sVat.StartsWith(",") Then
                                    sVat = sVat.Remove(0, 1)
                                End If
                                If sVat.EndsWith(",") Then
                                    sVat = sVat.Remove(Len(sVat) - 1, 1)
                                End If

                                sbretV = sVat
                                sArrayV = sbretV.Split(",")
                                For i = 0 To sArrayV.Length - 1
                                    If sArrayV(i) <> "0.0000" And sArrayV(i) <> "0" Then
                                        sRVat = sRVat & "," & sArrayV(i)
                                    End If
                                Next
                                If sRVat.StartsWith(",") Then
                                    sRVat = sRVat.Remove(0, 1)
                                End If
                                If sRVat.EndsWith(",") Then
                                    sRVat = sRVat.Remove(Len(sRVat) - 1, 1)
                                End If

                                ReturnStr = sRVat
                                temp = String.Join(",", ReturnStr.Split(","c).Distinct().ToArray())

                                If sCst.StartsWith(",") Then
                                    sCst = sCst.Remove(0, 1)
                                End If
                                If sCst.EndsWith(",") Then
                                    sCst = sCst.Remove(Len(sCst) - 1, 1)
                                End If

                                sbretC = sCst
                                sArrayC = sbretC.Split(",")
                                For i = 0 To sArrayC.Length - 1
                                    If sArrayC(i) <> "0.0000" And sArrayC(i) <> "0" Then
                                        sRCst = sRCst & "," & sArrayC(i)
                                    End If
                                Next
                                If sRCst.StartsWith(",") Then
                                    sRCst = sRCst.Remove(0, 1)
                                End If
                                If sRCst.EndsWith(",") Then
                                    sRCst = sRCst.Remove(Len(sRCst) - 1, 1)
                                End If

                                ReturnStrC = sRCst
                                tempC = String.Join(",", ReturnStrC.Split(","c).Distinct().ToArray())


                                If sDispatchNo.StartsWith(",") Then
                                    sDispatchNo = sDispatchNo.Remove(0, 1)
                                End If
                                If sDispatchNo.EndsWith(",") Then
                                    sDispatchNo = sDispatchNo.Remove(Len(sDispatchNo) - 1, 1)
                                End If

                                If sDispatchRefNo.StartsWith(",") Then
                                    sDispatchRefNo = sDispatchRefNo.Remove(0, 1)
                                End If
                                If sDispatchRefNo.EndsWith(",") Then
                                    sDispatchRefNo = sDispatchRefNo.Remove(Len(sDispatchRefNo) - 1, 1)
                                End If

                                If sDispatchDate.StartsWith(",") Then
                                    sDispatchDate = sDispatchDate.Remove(0, 1)
                                End If
                                If sDispatchDate.EndsWith(",") Then
                                    sDispatchDate = sDispatchDate.Remove(Len(sDispatchDate) - 1, 1)
                                End If

                                sInvoiceStr = sDispatchNo
                                sInvoice = String.Join(",", sInvoiceStr.Split(","c).Distinct().ToArray())

                                sInvoiceDateStr = sDispatchDate
                                sInvoiceDate = String.Join(",", sInvoiceDateStr.Split(","c).Distinct().ToArray())

                                dRow = dt1.NewRow
                                dRow("OrderNo") = "<B>" & sOrderNO & "</B>"
                                dRow("Commodity") = ""
                                dRow("Description") = ""
                                dRow("OrderDate") = sOrderDate
                                dRow("Party") = sParty
                                dRow("DispatchedNo") = sInvoice
                                dRow("DispatchRefNo") = sDispatchRefNo
                                dRow("DispatchedDate") = sInvoiceDate
                                dRow("ShippingRate") = "<B>" & dShipping & "</B>"
                                dRow("MRPRate") = ""
                                dRow("OrderedQty") = "<B>" & iTotalOrderedQty & "</B>"
                                dRow("AllocatedQty") = "<B>" & iTotalAllocatedQty & "</B>"
                                dRow("PendingQty") = "<B>" & iTotalOrderedQty - iTotalAllocatedQty & "</B>"
                                dRow("DispatchedQty") = "<B>" & iTotalDispatchedQty & "</B>"
                                dRow("TradeDiscount") = "<B>" & dTradeDiscount & "</B>"
                                dRow("TradeDiscountAmt") = "<B>" & dTradeDiscountAmt & "</B>"
                                dRow("Discount") = "<B>" & dTotalDiscount & "</B>"
                                dRow("DiscountAmt") = "<B>" & dTotalDiscountAmt & "</B>"
                                dRow("VAT") = ""
                                dRow("VATAmt") = dLastVatAmt
                                dRow("CST") = ""
                                dRow("CSTAmt") = dLastCstAmt
                                dRow("Excise") = ""
                                dRow("ExciseAmt") = dTotalExciseAmt
                                dRow("BasicPrice") = dBasicAmt
                                'If dLastCstAmt > 0 Then
                                '    dRow("Total") = ((dTotalNetAmt - dTradeDiscountAmt) + dLastCstAmt + dTotalExciseAmt + dShipping)
                                'Else
                                '    dRow("Total") = ((dTotalNetAmt - dTradeDiscountAmt) + dLastVatAmt + dTotalExciseAmt + dShipping)
                                'End If
                                dRow("Total") = ((dTotalNetAmt - dTradeDiscountAmt) + dShipping)

                                dt1.Rows.Add(dRow)

                                dShipping = 0 : dTradeDiscountAmt = 0 : dTradeDiscountAmt = 0
                                dMRPRate = 0 : iTotalOrderedQty = 0 : iTotalAllocatedQty = 0 : iTotalDispatchedQty = 0 : dTotalDiscount = 0 : dTotalDiscountAmt = 0
                                dTotalVAT = 0 : dTotalVATAmt = 0 : dTotalCST = 0 : dTotalCSTAmt = 0 : dTotalExcise = 0 : dTotalExciseAmt = 0 : dTotalNetAmt = 0 : iTotalPendingQty = 0
                                sDispatchNo = "" : sDispatchRefNo = "" : sDispatchDate = ""
                                sVat = "" : sVatD = "" : sDisplayVat = "" : sVatDA = "" : dVatSingleAmt = 0 : dLastVatAmt = 0 : dBasicAmt = 0
                                sCst = "" : sCstD = "" : sDisplayCst = "" : sCstDA = "" : dCstSingleAmt = 0 : dLastCstAmt = 0
                                temp = "" : tempC = "" : ReturnStr = "" : ReturnStrC = "" : sRVat = "" : sRCst = ""

                                sInvoice = "" : sInvoiceStr = "" : sInvoiceDate = "" : sInvoiceDateStr = ""
                                sStrDRefNo = ""
                            End If

                        End If

                    End If
                Next
            End If
            Return dt1
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSinkGrid")
        End Try
    End Function
    Private Sub grdDispatchDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdDispatchDetails.RowDataBound
        Dim dtVAT, dtCST, dtDiscount, dtExice As New DataTable
        Dim ddlVAT, ddlCST, ddlDiscount, ddlExice As New DropDownList

        Dim lblCommodityID, lblItemID, lblHistoryID, lblOrderedQty, lblMRP, lblTotal, lblDiscountAmount, lblNetAmount, lblVATAmount, lblCSTAmount, lblExiceAmount As New Label
        Dim iCommodityID, IItemID, iHistoryID As Integer
        Dim InvVAT As String = "" : Dim InvExcise As String = ""

        Dim dtDispatchDetails As New DataTable
        Dim iDiscountID As Integer

        Dim HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount As HiddenField
        Dim dDispatchDate As Date

        Dim lblGSTRate As New Label : Dim lblGSTAmount As New Label : Dim HFGSTAmount As HiddenField
        Dim lblCharges, lblAmount As New Label
        Dim HFCharges, HFAmount As HiddenField
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblCommodityID = e.Row.FindControl("lblCommodityID")
                lblItemID = e.Row.FindControl("lblItemID")
                lblHistoryID = e.Row.FindControl("lblHistoryID")

                iCommodityID = lblCommodityID.Text
                IItemID = lblItemID.Text
                iHistoryID = lblHistoryID.Text

                ddlDiscount = e.Row.FindControl("ddlDiscount")
                dtDiscount = objDispatch.BindDiscount(sSession.AccessCode, sSession.AccessCodeID)
                ddlDiscount.DataSource = dtDiscount
                ddlDiscount.DataTextField = "MAS_DESC"
                ddlDiscount.DataValueField = "MAS_ID"
                ddlDiscount.DataBind()
                ddlDiscount.Items.Insert(0, "Select")

                If txtOrderType.Text = "O" Then
                    iDiscountID = objDispatch.GetDiscountFromOral(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, iCommodityID, IItemID, iHistoryID)
                    If iDiscountID > 0 Then
                        ddlDiscount.SelectedValue = iDiscountID
                    Else
                        ddlDiscount.SelectedIndex = 0
                    End If
                End If
                If ddlSearch.SelectedIndex > 0 Then
                    If ddlAllocationNo.SelectedIndex > 0 Then
                        iDiscountID = objDispatch.GetDiscountFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, ddlAllocationNo.SelectedValue, iCommodityID, IItemID, iHistoryID)
                    Else
                        iDiscountID = objDispatch.GetDiscountFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlSearch.SelectedValue, 0, iCommodityID, IItemID, iHistoryID)
                    End If
                    If iDiscountID > 0 Then
                        ddlDiscount.SelectedValue = iDiscountID
                    Else
                        ddlDiscount.SelectedIndex = 0
                    End If
                End If

                lblOrderedQty = e.Row.FindControl("lblOrderedQty")
                lblMRP = e.Row.FindControl("lblMRP")
                lblTotal = e.Row.FindControl("lblTotal")
                lblDiscountAmount = e.Row.FindControl("lblDiscountAmount")
                lblCharges = e.Row.FindControl("lblCharges")
                lblAmount = e.Row.FindControl("lblAmount")

                lblNetAmount = e.Row.FindControl("lblNetAmount")
                lblGSTRate = e.Row.FindControl("lblGSTRate")
                lblGSTAmount = e.Row.FindControl("lblGSTAmount")

                HFDiscountAmount = e.Row.FindControl("HFDiscountAmount")
                HFCharges = e.Row.FindControl("HFCharges")
                HFAmount = e.Row.FindControl("HFAmount")
                HFGSTAmount = e.Row.FindControl("HFGSTAmount")
                HFNetAmount = e.Row.FindControl("HFNetAmount")

                Dim dChargeAmount, dItemsTotalFromDispatch As Double
                If ddldispatchNo.SelectedIndex > 0 Then
                    dChargeAmount = objDispatch.GetDispatchFromChargeAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddldispatchNo.SelectedValue, 0)
                    dItemsTotalFromDispatch = objDispatch.GetItemsTotalFromDispatch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddldispatchNo.SelectedValue)
                End If

                If txtOrderType.Text = "O" Then
                    dChargeAmount = objDispatch.GetChargeAmountFromOral(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    dItemsTotalFromDispatch = objDispatch.GetItemsTotalFromOral(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                End If

                If ddlSearch.SelectedIndex > 0 Then
                    '*** Working But Commented Bcz Same item getting repeated ***'
                    'dtDispatchDetails = objDispatch.BindDispatchedROWData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddlSearch.SelectedValue, 0, iCommodityID, IItemID, iHistoryID)
                    'If dtDispatchDetails.Rows.Count > 0 Then
                    '    For m = 0 To dtDispatchDetails.Rows.Count - 1
                    '        lblMRP.Text = dtDispatchDetails.Rows(m)("SDD_Rate")
                    '        lblOrderedQty.Text = dtDispatchDetails.Rows(m)("SDD_Quantity")
                    '        lblTotal.Text = dtDispatchDetails.Rows(m)("SDD_RateAmount")
                    '        lblDiscountAmount.Text = dtDispatchDetails.Rows(m)("SDD_DiscountAmount")
                    '        lblCharges.Text = dtDispatchDetails.Rows(m)("SDD_ChargesPeritem")
                    '        lblAmount.Text = dtDispatchDetails.Rows(m)("SDD_Amount")
                    '        lblGSTRate.Text = dtDispatchDetails.Rows(m)("SDD_GSTRate")
                    '        lblGSTAmount.Text = dtDispatchDetails.Rows(m)("SDD_GSTAmount")
                    '        lblNetAmount.Text = dtDispatchDetails.Rows(m)("SDD_TotalAmount")
                    '    Next
                    'End If
                    '*** Working But Commented Bcz Same item getting repeated ***'

                    'Extra code pasted inorder to work with existing data'
                    'HFDiscountAmount.Value = 0 : HFVATAmount.Value = 0 : HFCSTAmount.Value = 0 : HFExiceAmount.Value = 0 : HFNetAmount.Value = 0
                    'lblDiscountAmount.Text = 0 : HFDiscountAmount.Value = 0

                    'If ddlSalesType.SelectedValue = 1 And (ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex > 0) Then 'Local
                    '    CalculateLocalDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

                    'ElseIf ddlSalesType.SelectedValue = 2 And (ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0) Then 'Inter State - Normal
                    '    CalculateNormalDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

                    'ElseIf ddlSalesType.SelectedIndex = 2 And (ddlVAT.SelectedIndex > 0 And ddlCST.SelectedIndex > 0 And ddlExice.SelectedIndex > 0) Then 'Inter State - CST
                    '    CalculateDefaultDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)

                    'ElseIf ddlSalesType.SelectedIndex = 2 And (ddlVAT.SelectedIndex = -1 And ddlCST.SelectedIndex = -1 And ddlExice.SelectedIndex = -1) Then 'Inter State - EHFI Exempted
                    '    CalculateEHFIDiscountOptional(lblOrderedQty, lblMRP, lblTotal, ddlVAT, lblVATAmount, ddlCST, lblCSTAmount, ddlExice, lblExiceAmount, ddlDiscount, lblDiscountAmount, lblNetAmount, HFDiscountAmount, HFVATAmount, HFCSTAmount, HFExiceAmount, HFNetAmount)
                    'End If
                    'Extra code pasted inorder to work with existing data'
                Else
                    HFDiscountAmount.Value = 0 : HFCharges.Value = 0 : HFAmount.Value = 0 : HFGSTAmount.Value = 0 : HFNetAmount.Value = 0
                    CalculateGST(lblOrderedQty, lblMRP, lblTotal, ddlDiscount, lblDiscountAmount, lblCharges, lblAmount, lblGSTRate, lblGSTAmount, lblNetAmount, HFDiscountAmount, HFCharges, HFAmount, HFGSTAmount, HFNetAmount, dChargeAmount, dItemsTotalFromDispatch)
                End If

                ddlDiscount.Attributes.Add("onChange", "javascript:return CalculateGST('" & lblOrderedQty.ClientID & "','" & lblMRP.ClientID & "','" & lblTotal.ClientID & "','" & ddlDiscount.ClientID & "','" & lblDiscountAmount.ClientID & "','" & lblCharges.ClientID & "','" & lblAmount.ClientID & "','" & lblGSTRate.ClientID & "','" & lblGSTAmount.ClientID & "','" & lblNetAmount.ClientID & "','" & HFDiscountAmount.ClientID & "','" & HFCharges.ClientID & "','" & HFAmount.ClientID & "','" & HFGSTAmount.ClientID & "','" & HFNetAmount.ClientID & "'," & dChargeAmount & "," & dItemsTotalFromDispatch & ")")

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdDispatchDetails_RowDataBound")
        End Try
    End Sub
    'Public Sub CalculateGST(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblCharges As Label, ByVal lblAmount As Label, ByVal lblGSTRate As Label, ByVal lblGSTAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFCharges As HiddenField, ByVal HFAmount As HiddenField, ByVal HFGSTAmount As HiddenField, ByVal HFNetAmount As HiddenField, ByVal dChargeAmount As Double, ByVal dItemsTotalFromDispatch As Double)
    '    Dim sBasicAmount As Double
    '    Dim sTotal As Double : Dim dDiscount As Double
    '    Dim dAmountOnCalculate As Double
    '    Try
    '        lblTotal.Text = lblOrderedQty.Text * lblMRP.Text
    '        lblNetAmount.Text = lblOrderedQty.Text * lblMRP.Text

    '        lblDiscountAmount.Text = 0
    '        HFDiscountAmount.Value = 0

    '        If lblOrderedQty.Text <> "" And ddlDiscount.SelectedIndex > 0 Then
    '            If txtCode.Value = "C" Then
    '                sBasicAmount = lblTotal.Text

    '                lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((sBasicAmount * ddlDiscount.SelectedItem.Text) / 100))
    '                HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((sBasicAmount * ddlDiscount.SelectedItem.Text) / 100))
    '            End If
    '            If txtCode.Value = "P" Then
    '                lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
    '                HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
    '            End If
    '        End If

    '        If dChargeAmount > 0 Then
    '            If txtCode.Value = "C" Then
    '                sBasicAmount = lblTotal.Text

    '                lblCharges.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * dChargeAmount) / dItemsTotalFromDispatch))
    '                HFCharges.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * dChargeAmount) / dItemsTotalFromDispatch))
    '            End If
    '            If txtCode.Value = "P" Then
    '                sTotal = lblTotal.Text
    '                dDiscount = lblDiscountAmount.Text

    '                lblCharges.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal) * dChargeAmount) / dItemsTotalFromDispatch))
    '                HFCharges.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal) * dChargeAmount) / dItemsTotalFromDispatch))
    '            End If
    '        Else
    '            lblCharges.Text = 0
    '            HFCharges.Value = 0
    '        End If

    '        Dim dItemChargeAmt As Double
    '        sTotal = lblTotal.Text
    '        dDiscount = lblDiscountAmount.Text
    '        dItemChargeAmt = lblCharges.Text

    '        dAmountOnCalculate = String.Format("{0:0.00}", Convert.ToDecimal((sTotal - dDiscount) + dItemChargeAmt))
    '        lblAmount.Text = dAmountOnCalculate
    '        HFAmount.Value = dAmountOnCalculate

    '        If lblGSTRate.Text <> "" Then
    '            If txtCode.Value = "C" Then

    '                'sBasicAmount = lblTotal.Text

    '                lblGSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))
    '                HFGSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))

    '                lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
    '                HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
    '            End If
    '            If txtCode.Value = "P" Then
    '                'sTotal = lblTotal.Text
    '                'dDiscount = lblDiscountAmount.Text

    '                lblGSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))
    '                HFGSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))

    '                lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
    '                HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
    '            End If
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateGST")
    '    End Try
    'End Sub
    Public Sub CalculateGST(ByVal lblOrderedQty As Label, ByVal lblMRP As Label, ByVal lblTotal As Label, ByVal ddlDiscount As DropDownList, ByVal lblDiscountAmount As Label, ByVal lblCharges As Label, ByVal lblAmount As Label, ByVal lblGSTRate As Label, ByVal lblGSTAmount As Label, ByVal lblNetAmount As Label, ByVal HFDiscountAmount As HiddenField, ByVal HFCharges As HiddenField, ByVal HFAmount As HiddenField, ByVal HFGSTAmount As HiddenField, ByVal HFNetAmount As HiddenField, ByVal dChargeAmount As Double, ByVal dItemsTotalFromDispatch As Double)
        Dim sBasicAmount As Double
        Dim sTotal As Double : Dim dDiscount As Double
        Dim dAmountOnCalculate As Double
        Dim dAmt, dRate As Double
        Try

            If txtCode.Value = "C" Then
                dAmt = (lblMRP.Text * 100) / (100 + lblGSTRate.Text)
                dRate = dAmt
            End If
            If txtCode.Value = "P" Then
                dRate = lblMRP.Text
            End If

            lblMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(dRate))

            lblTotal.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblOrderedQty.Text * dRate))
            lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblOrderedQty.Text * dRate))

            lblDiscountAmount.Text = 0
            HFDiscountAmount.Value = 0

            If lblOrderedQty.Text <> "" And ddlDiscount.SelectedIndex > 0 Then
                If txtCode.Value = "C" Then
                    sBasicAmount = lblTotal.Text

                    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((sBasicAmount * ddlDiscount.SelectedItem.Text) / 100))
                    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((sBasicAmount * ddlDiscount.SelectedItem.Text) / 100))
                End If
                If txtCode.Value = "P" Then
                    lblDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                    HFDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((lblTotal.Text * ddlDiscount.SelectedItem.Text) / 100))
                End If
            End If

            If dChargeAmount > 0 Then
                If txtCode.Value = "C" Then
                    sBasicAmount = lblTotal.Text

                    lblCharges.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * dChargeAmount) / dItemsTotalFromDispatch))
                    HFCharges.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sBasicAmount) * dChargeAmount) / dItemsTotalFromDispatch))
                End If
                If txtCode.Value = "P" Then
                    sTotal = lblTotal.Text
                    dDiscount = lblDiscountAmount.Text

                    lblCharges.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal) * dChargeAmount) / dItemsTotalFromDispatch))
                    HFCharges.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal) * dChargeAmount) / dItemsTotalFromDispatch))
                End If
            Else
                lblCharges.Text = 0
                HFCharges.Value = 0
            End If

            Dim dItemChargeAmt As Double
            sTotal = lblTotal.Text
            dDiscount = lblDiscountAmount.Text
            dItemChargeAmt = lblCharges.Text

            dAmountOnCalculate = String.Format("{0:0.00}", Convert.ToDecimal((sTotal - dDiscount) + dItemChargeAmt))
            lblAmount.Text = dAmountOnCalculate
            HFAmount.Value = dAmountOnCalculate

            If lblGSTRate.Text <> "" Then
                If txtCode.Value = "C" Then

                    'sBasicAmount = lblTotal.Text

                    lblGSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))
                    HFGSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))

                    lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
                    HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
                End If
                If txtCode.Value = "P" Then
                    'sTotal = lblTotal.Text
                    'dDiscount = lblDiscountAmount.Text

                    lblGSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))
                    HFGSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * lblGSTRate.Text) / 100))

                    lblNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
                    HFNetAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + lblGSTAmount.Text))
                End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Private Sub chkboxSameAdd_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxSameAdd.CheckedChanged
    '    Try
    '        If chkboxSameAdd.Checked = True Then
    '            txtDeleveryAddress.Text = txtBillingAddress.Text
    '            txtDeliveryGSTNRegNo.Text = txtBillingGSTNRegNo.Text
    '        Else
    '            txtDeleveryAddress.Text = ""
    '            txtDeliveryGSTNRegNo.Text = ""
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub ddldispatchNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldispatchNo.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dtCharge As New DataTable
        Try
            dt = objDispatch.BindDispatchFormData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddldispatchNo.SelectedValue)
            If IsNothing(dt) = False Then
                GenerateOrderCode()
                If IsDBNull(dt.Rows(0)("DM_OrderID")) = False Then
                    DashBoardOrderNo()
                    ddlOrderNo.SelectedValue = dt.Rows(0)("DM_OrderID")
                Else
                    ddlOrderNo.SelectedIndex = 0
                End If
                txtOrderType.Text = objDispatch.GetOrderType(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                If txtOrderType.Text = "S" Then
                    lblOrderType.Text = "Sales Order"
                ElseIf txtOrderType.Text = "O"
                    lblOrderType.Text = "Cash Sales"
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

                txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("DM_OrderDate"), "D")
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

                ddlPaymentType.SelectedValue = dt.Rows(0)("DM_PaymentType")
                If UCase(ddlPaymentType.SelectedItem.Text) = UCase("Cheque") Then
                    divcollapseChequeDetails.Visible = True
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

                grdDispatchDetails.DataSource = objDispatch.BindDispatchFROMData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, ddldispatchNo.SelectedValue, 0)
                grdDispatchDetails.DataBind()

                'dtCharge = objDispatch.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddldispatchNo.SelectedValue, 0)
                dtCharge = objDispatch.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, 0, 0)
                GvCharge.DataSource = dtCharge
                GvCharge.DataBind()
                Session("ChargesMaster") = dtCharge
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldispatchNo_SelectedIndexChanged")
        End Try
    End Sub
End Class

