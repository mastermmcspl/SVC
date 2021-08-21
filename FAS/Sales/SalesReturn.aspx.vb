Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Sales_SalesReturn
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_SalesReturn"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objSR As New ClsSalesReturn
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnBack.ImageUrl = "~/Images/BackWard24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try

            'RFVddlModeOfShipping.InitialValue = "Select Mode of Return" : RFVddlreturntype.InitialValue = "Select Reason For Return"
            'RFVddlOrderNo.InitialValue = "Select Order No" : RFVddlInvoiceNo.InitialValue = "Select Invoice No"

            sSession = Session("AllSession")
            If IsPostBack = False Then

                imgbtnSave.Visible = False : imgbtnApprove.Visible = False
                sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasSRe", 1)
                If sFormButtons = "False" Or sFormButtons = "" Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",Save/Update,") = True Then
                        imgbtnSave.Visible = True
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnApprove.Visible = True
                    End If
                End If

                'imgbtnApprove.Visible = False

                LoadExistingReturnNo()
                GenerateOrderCode()
                LoadOrderNo()
                LoadMethodOfShiping()

                Me.imgbtnSave.Attributes.Add("OnClick", "return ValidateSave()")

                Me.txtEnterPrice.Attributes.Add("onblur", "return CalculatePrice()")
                Me.ddlOrderNo.Attributes.Add("OnClick", "return ValidateData()")
                Me.txtQuantity.Attributes.Add("onblur", "return RateAmount()")

                Me.ddlDiscount.Attributes.Add("onChange", "return CalculateFinalAmount()")
                Me.ddlVAT.Attributes.Add("onChange", "return CalculateFinalAmountVAT()")
                Me.ddlCST.Attributes.Add("onChange", "return CalculateFinalAmountCST()")
                Me.ddlExcise.Attributes.Add("onChange", "return CalculateFinalAmountEX()")
                Me.txtExciseAmount.Attributes.Add("onblur", "return CalculateFinalAmount()")

                Dim iSRID As String = ""
                iSRID = objGen.DecryptQueryString(Request.QueryString("SRID"))
                If iSRID <> "" Then
                    ddlSearch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("SRID"))
                    ddlSearch_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadExistingReturnNo()
        Dim dt As New DataTable
        Try
            dt = objSR.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtSearch.Text))
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "SRM_ReturnOrderCode"
            ddlSearch.DataValueField = "SRM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Sales Return No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub GenerateOrderCode()
        Try
            txtSaleReturnCode.Text = objSR.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCode")
        End Try
    End Sub
    Private Sub LoadOrderNo()
        Try
            ddlOrderNo.DataSource = objSR.Invoice(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlOrderNo.DataTextField = "SPO_OrderCode"
            ddlOrderNo.DataValueField = "SPO_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, New ListItem("Select Order No", "0"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDispatchNo(ByVal iOrderID As Integer)
        Try
            ddlInvoiceNo.DataSource = objSR.BindDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            ddlInvoiceNo.DataTextField = "SDM_Code"
            ddlInvoiceNo.DataValueField = "SDM_ID"
            ddlInvoiceNo.DataBind()
            ddlInvoiceNo.Items.Insert(0, New ListItem("Select Invoice No", "0"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadMethodOfShiping()
        Try
            ddlModeOfShipping.DataSource = objSR.LoadMethodOfShiping(sSession.AccessCode, sSession.AccessCodeID)
            ddlModeOfShipping.DataTextField = "Mas_desc"
            ddlModeOfShipping.DataValueField = "Mas_id"
            ddlModeOfShipping.DataBind()
            ddlModeOfShipping.Items.Insert(0, "Select Mode of Return")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadParty()
        Try
            ddlParty.DataSource = objSR.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "BM_Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Party")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadPaymentType()
        Try
            ddlPaymentType.DataSource = objSR.BindPaymentType(sSession.AccessCode, sSession.AccessCodeID)
            ddlPaymentType.DataTextField = "Mas_Desc"
            ddlPaymentType.DataValueField = "Mas_ID"
            ddlPaymentType.DataBind()
            ddlPaymentType.Items.Insert(0, "Select Payment Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objSR.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDiscount()
        Dim dt As New DataTable
        Try
            dt = objSR.LoadDiscounts(sSession.AccessCode, sSession.AccessCodeID)
            ddlDiscount.DataSource = dt
            ddlDiscount.DataTextField = "Mas_Desc"
            ddlDiscount.DataValueField = "Mas_Id"
            ddlDiscount.DataBind()
            ddlDiscount.Items.Insert(0, "Select Discount")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadVAT()
        Dim dt As New DataTable
        Try
            dt = objSR.LoadVATAndCSTFromGeneralMaster(sSession.AccessCode, sSession.AccessCodeID, "VAT")
            ddlVAT.DataSource = dt
            ddlVAT.DataTextField = "Mas_Desc"
            ddlVAT.DataValueField = "Mas_Id"
            ddlVAT.DataBind()
            ddlVAT.Items.Insert(0, "Select VAT")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExcise()
        Dim dt As New DataTable
        Try
            dt = objSR.LoadExciseFromGeneralMaster(sSession.AccessCode, sSession.AccessCodeID)
            ddlExcise.DataSource = dt
            ddlExcise.DataTextField = "Mas_Desc"
            ddlExcise.DataValueField = "Mas_Id"
            ddlExcise.DataBind()
            ddlExcise.Items.Insert(0, "Select Excise")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCST()
        Dim dt As New DataTable
        Try
            dt = objSR.LoadVATAndCSTFromGeneralMaster(sSession.AccessCode, sSession.AccessCodeID, "CST")
            ddlCST.DataSource = dt
            ddlCST.DataTextField = "Mas_Desc"
            ddlCST.DataValueField = "Mas_Id"
            ddlCST.DataBind()
            ddlCST.Items.Insert(0, "Select CST")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDescription(ByVal iInvoiceID As Integer)
        Try
            lstBoxDescription.DataSource = objSR.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvoiceID)
            lstBoxDescription.DataTextField = "INV_Code"
            lstBoxDescription.DataValueField = "INV_ID"
            lstBoxDescription.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCategory()
        Try
            ddlCategory.DataSource = objSR.BindCategory(sSession.AccessCode, sSession.AccessCodeID)
            ddlCategory.DataTextField = "Mas_Desc"
            ddlCategory.DataValueField = "Mas_ID"
            ddlCategory.DataBind()
            ddlCategory.Items.Insert(0, "Select Category")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlInvoiceNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInvoiceNo.SelectedIndexChanged
        Dim dt As New DataTable
        Dim iCategoryID As Integer
        Try
            If ddlOrderNo.SelectedIndex > 0 Then
                If ddlInvoiceNo.SelectedIndex > 0 Then
                    dt = objSR.BindDispatchMasterData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlInvoiceNo.SelectedValue)
                    txtDispatchDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SDM_DispatchDate"), "D")

                    LoadCategory()
                    iCategoryID = objSR.GetPartyCategory(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    If iCategoryID > 0 Then
                        ddlCategory.SelectedValue = iCategoryID
                    Else
                        ddlCategory.SelectedIndex = 0
                    End If


                    txtOrderDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("SDM_OrderDate"), "D")
                    txtDispatchRefNo.Text = dt.Rows(0)("SDM_DispatchRefNo")
                    txtESugamNo.Text = dt.Rows(0)("SDM_ESugamNo")

                    LoadParty()
                    ddlParty.SelectedValue = dt.Rows(0)("SDM_SupplierID")
                    txtPartyCode.Text = objSR.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)

                    'LoadMethodOfShiping()
                    'If dt.Rows(0)("SDM_ModeOfShipping") > 0 Then
                    '    ddlModeOfShipping.SelectedValue = dt.Rows(0)("SDM_ModeOfShipping")
                    'Else
                    '    ddlModeOfShipping.SelectedIndex = 0
                    'End If

                    LoadPaymentType()
                    ddlPaymentType.SelectedValue = dt.Rows(0)("SDM_PaymentType")

                    If dt.Rows(0)("SDM_SaleType") > 0 Then
                        ddlSalesType.SelectedValue = dt.Rows(0)("SDM_SaleType")
                    Else
                        ddlSalesType.SelectedIndex = 0
                    End If
                    If dt.Rows(0)("SDM_OtherType") > 0 Then
                        ddlOthers.SelectedValue = dt.Rows(0)("SDM_OtherType")
                    Else
                        ddlOthers.SelectedIndex = 0
                    End If
                    BindDescription(ddlInvoiceNo.SelectedValue)

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInvoiceNo_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindUnitOfMeassurement(ByVal iItemID As Integer, ByVal iHistoryID As Integer)
        Try
            ddlUnitOfMeassurement.DataSource = objSR.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iItemID, iHistoryID)
            ddlUnitOfMeassurement.DataTextField = "Mas_Desc"
            ddlUnitOfMeassurement.DataValueField = "Mas_ID"
            ddlUnitOfMeassurement.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrderNo.SelectedIndexChanged
        Try
            If ddlOrderNo.SelectedIndex > 0 Then
                LoadDispatchNo(ddlOrderNo.SelectedValue)
            Else
                ddlOrderNo.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlOrderNo_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub DisableAll()
        Try
            ddlUnitOfMeassurement.Enabled = False : txtAmount.Enabled = False : ddlDiscount.Enabled = False : txtDiscountAmount.Enabled = False : ddlVAT.Enabled = False : txtVATAmount.Enabled = False
            ddlCST.Enabled = False : txtCSTAmount.Enabled = False : ddlExcise.Enabled = False : txtExciseAmount.Enabled = False : txtNetAmount.Enabled = False
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lstBoxDescription_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxDescription.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Dim IHistoryID As Integer

        Dim sStockHistoryID As String = ""
        Dim iCategoryID As Integer
        Try

            'imgbtnApprove.Visible = False
            hfAvailableQty.Value = ""
            lblError.Text = ""
            'btnSave.Text = "Add"
            txtQuantity.Text = "" : txtAmount.Text = "" : hfAmount.Value = ""
            txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtVATAmount.Text = "" : hfVATAmount.Value = ""
            txtCSTAmount.Text = "" : hfCSTAmount.Value = ""
            'txtExciseAmount.Text = "" : hfExciseAmount.Value = ""
            txtExciseAmount.Text = 0 : hfExciseAmount.Value = 0
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            If ddlOrderNo.SelectedIndex > 0 And ddlInvoiceNo.SelectedIndex > 0 Then

                If ddlParty.SelectedIndex > 0 Then
                    If lstBoxDescription.SelectedIndex <> -1 Then

                        LoadDiscount()
                        LoadVAT()
                        LoadCST()
                        LoadExcise()

                        LoadCommodity()
                        ddlCommodity.SelectedValue = objSR.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlInvoiceNo.SelectedValue, lstBoxDescription.SelectedValue)
                        sStockHistoryID = objSR.GetStockHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlInvoiceNo.SelectedValue, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                        'Stock qty restriction for 0 & -ve'
                        'hfAvailableQty.Value = clsPROFormaSalesOrder.GetAvailableStockOfThisItem(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                        'Stock qty restriction for 0 & -ve'
                        sCode = objSR.GetCode(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)
                        If sCode.StartsWith("P") Then
                            txtCode.Value = "P"
                        ElseIf sCode.StartsWith("C") Then
                            txtCode.Value = "C"
                        End If

                        If ddlCategory.SelectedIndex > 0 Then
                            iCategoryID = ddlCategory.SelectedValue
                        Else
                            iCategoryID = 0
                        End If

                        dt = objSR.GetAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlInvoiceNo.SelectedValue, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                        If dt.Rows.Count > 0 Then

                            lblQty.Text = dt.Rows(0)("SDD_Quantity")
                            lblRate.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(0)("SDD_Rate")))

                            hfMRP.Value = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(0)("SDD_Rate")))
                            txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(0)("SDD_Rate")))
                            txtMRPFromTable.Text = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(0)("SDD_Rate")))

                            IHistoryID = dt.Rows(0)("SDD_HistoryID")
                            BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)

                            If dt.Rows(0)("SDD_VAT") > 0 Then
                                ddlVAT.SelectedValue = dt.Rows(0)("SDD_VAT")
                                'objSR.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("SDD_VAT"))
                            Else
                                ddlVAT.SelectedIndex = 0
                                'ddlVAT.SelectedValue = objSR.GetVATOFThisRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "0")
                            End If
                            'If ddlVAT.SelectedItem.Text = 0 Then
                            '    ddlVAT.SelectedIndex = 0
                            'End If

                            If dt.Rows(0)("SDD_CST") > 0 Then
                                ddlCST.SelectedValue = dt.Rows(0)("SDD_CST")
                                'objSR.GetCSTOFThisRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("SDD_CST"))
                            Else
                                ddlCST.SelectedIndex = 0
                                'ddlCST.SelectedValue = objSR.GetCSTOFThisRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "0")
                            End If
                            'If ddlCST.SelectedItem.Text = 0 Then
                            '    ddlCST.SelectedIndex = 0
                            'End If

                            If dt.Rows(0)("SDD_Excise") > 0 Then
                                ddlExcise.SelectedValue = dt.Rows(0)("SDD_Excise")
                                'objSR.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("SDD_Excise"))
                            Else
                                ddlExcise.SelectedIndex = 0
                                'ddlExcise.SelectedValue = objSR.GetExciseOFThisRate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "0")
                            End If
                            If ddlExcise.SelectedItem.Text = 0 Then
                                txtExciseAmount.Text = 0
                                hfExciseAmount.Value = 0
                            End If

                            hfItemVAT.Value = objSR.GetVATOFEachItem(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue, dt.Rows(0)("SDD_HistoryID"))
                        End If

                        If ddlreturntype.SelectedValue = "1" Or ddlreturntype.SelectedValue = "3" Then
                            txtQuantity.Enabled = True : ddlRate.Enabled = False : txtMRP.Enabled = False : txtEnterPrice.Enabled = False
                            DisableAll()
                        Else
                            txtQuantity.Enabled = True : ddlRate.Enabled = True : txtMRP.Enabled = True : txtEnterPrice.Enabled = True
                            DisableAll()
                        End If

                        'Category Code'
                        'If UCase(ddlCategory.SelectedItem.Text) = "NA" Then
                        '    txtMRP.Enabled = True
                        'ElseIf UCase(ddlCategory.SelectedItem.Text) = "NOT FOR SALE" Then
                        '    txtMRP.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtVATAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                        '    txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtCSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
                        '    txtExciseAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0)) : txtNetAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))

                        '    hfMRP.Value = 0 : hfAmount.Value = 0 : hfDiscountAmount.Value = 0 : hfVATAmount.Value = 0
                        '    hfCSTAmount.Value = 0 : hfExciseAmount.Value = 0 : hfNetAmount.Value = 0
                        'Else
                        '    txtMRP.Enabled = False
                        'End If
                        'Category Code'

                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstBoxDescription_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim dt As New DataTable
        Dim iMasterID As Integer
        Dim sId As Integer = 0
        Try
            lblError.Text = ""

            'Save Master'
            objSR.SRM_ID = sId
            objSR.SRM_ReturnOrderCode = objGen.SafeSQL(Trim(txtSaleReturnCode.Text))
            objSR.SRM_ReferenceNo = objGen.SafeSQL(Trim(txtReturnRefNo.Text))

            objSR.SRM_OrderNo = objGen.SafeSQL(Trim(ddlOrderNo.SelectedValue))

            If txtOrderDate.Text <> "" Then
                objSR.SRM_OrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                'ObjGen.FormatDtForRDBMS(Trim(txtOrderDate.Text), "D")
            End If
            If txtReturnDate.Text <> "" Then
                objSR.SRM_ReturnDate = Date.ParseExact(Trim(txtReturnDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            objSR.SRM_PartyID = objGen.SafeSQL(Trim(ddlParty.SelectedValue))
            objSR.SRM_PartyCode = objGen.SafeSQL(Trim(txtPartyCode.Text))
            objSR.SRM_ModeOfReturn = objGen.SafeSQL(Trim(ddlModeOfShipping.SelectedValue))
            objSR.SRM_Narration = objGen.SafeSQL(Trim(txtRemarks.Text))
            objSR.SRM_Status = "W"
            objSR.SRM_YearID = sSession.YearID
            objSR.SRM_CompID = sSession.AccessCodeID
            objSR.SRM_CreatedBy = sSession.UserID
            objSR.SRM_CreatedOn = System.DateTime.Today

            objSR.SRM_Operation = "C"
            objSR.SRM_IPAddress = sSession.IPAddress

            objSR.SRM_DispatchID = ddlInvoiceNo.SelectedValue
            objSR.SRM_DispatchRefNo = txtDispatchRefNo.Text
            objSR.SRM_ESugamNo = txtESugamNo.Text

            If ddlPaymentType.SelectedIndex > 0 Then
                objSR.SRM_PaymentType = ddlPaymentType.SelectedValue
            Else
                objSR.SRM_PaymentType = 0
            End If
            If ddlCategory.SelectedIndex > 0 Then
                objSR.SRM_Category = ddlCategory.SelectedValue
            Else
                objSR.SRM_Category = 0
            End If
            If ddlSalesType.SelectedIndex > 0 Then
                objSR.SRM_SaleType = ddlSalesType.SelectedValue
            Else
                objSR.SRM_SaleType = 0
            End If
            If ddlOthers.SelectedIndex > 0 Then
                objSR.SRM_OtherType = ddlOthers.SelectedValue
            Else
                objSR.SRM_OtherType = 0
            End If
            If txtDispatchDate.Text <> "" Then
                objSR.SRM_DispatchDate = Date.ParseExact(Trim(txtDispatchDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If ddlPaymentType.SelectedIndex > 0 Then
                objSR.SRM_ReturnReason = ddlreturntype.SelectedValue
            Else
                objSR.SRM_ReturnReason = 0
            End If

            Arr = objSR.SaveSalesReturnMaster(sSession.AccessCode, objSR)
            iMasterID = Arr(1)

            If txtQuantity.Text <> "" Then

                objSR.SRD_MasterID = iMasterID
                objSR.SRD_CommodityID = ddlCommodity.SelectedValue
                objSR.SRD_DescriptionID = lstBoxDescription.SelectedValue

                objSR.SRD_SaleQnty = lblQty.Text
                objSR.SRD_ReturnQnty = txtQuantity.Text

                objSR.SRD_Return = ddlreturntype.SelectedValue

                'If txtDiscount.Text <> "" Then
                '    objProForma.SPOD_Discount = txtDiscount.Text
                'End If
                'If ddlDiscount.SelectedIndex > 0 Then
                '    objProForma.SPOD_Discount = ddlDiscount.SelectedValue
                'End If

                If ddlDiscount.SelectedIndex > 0 Then
                    objSR.SRD_Discount = ddlDiscount.SelectedItem.Text
                Else
                    objSR.SRD_Discount = 0
                End If

                objSR.SRD_UnitOfMeasurement = ddlUnitOfMeassurement.SelectedValue

                If hfAmount.Value <> "" Then
                    objSR.SRD_RateAmount = Request.Form(hfAmount.UniqueID)
                Else
                    objSR.SRD_RateAmount = txtAmount.Text
                End If
                'objProForma.SPOD_Rate = txtAmount.Text

                If hfDiscountAmount.Value <> "" Then
                    objSR.SRD_DiscountAmount = Request.Form(hfDiscountAmount.UniqueID)
                Else
                    objSR.SRD_DiscountAmount = 0
                End If

                objSR.SRD_HistoryID = objSR.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                objSR.SRD_CompID = sSession.AccessCodeID
                objSR.SRD_Status = "A"

                If txtMRP.Text <> "" Then
                    hfMRP.Value = txtMRP.Text
                End If
                If hfMRP.Value <> "" Then
                    'objProForma.SPOD_MRPRate = Request.Form(hfMRP.UniqueID)
                    objSR.SRD_Rate = hfMRP.Value
                    'objSR.SRD_Rate = lblRate.Text
                Else
                    objSR.SRD_Rate = 0
                End If

                If hfNetAmount.Value <> "" Then
                    objSR.SRD_TotalAmount = Request.Form(hfNetAmount.UniqueID)
                Else
                    objSR.SRD_TotalAmount = 0
                End If

                'If txtVAT.Text <> "" Then
                '    objProForma.SPOD_VAT = txtVAT.Text
                'End If
                If ddlVAT.SelectedIndex > 0 Then
                    objSR.SRD_VAT = ddlVAT.SelectedValue
                Else
                    objSR.SRD_VAT = objSR.GetZeroVAT(sSession.AccessCode, sSession.AccessCodeID)
                End If

                If hfVATAmount.Value <> "" Then
                    objSR.SRD_VATAmount = Request.Form(hfVATAmount.UniqueID)
                Else
                    objSR.SRD_VATAmount = 0
                End If

                If ddlCST.SelectedIndex > 0 Then
                    objSR.SRD_CST = ddlCST.SelectedValue
                Else
                    objSR.SRD_CST = objSR.GetZeroCST(sSession.AccessCode, sSession.AccessCodeID)
                End If

                If hfCSTAmount.Value <> "" Then
                    objSR.SRD_CSTAmount = Request.Form(hfCSTAmount.UniqueID)
                Else
                    objSR.SRD_CSTAmount = 0
                End If

                If ddlExcise.SelectedIndex > 0 Then
                    objSR.SRD_Excise = ddlExcise.SelectedValue
                Else
                    objSR.SRD_Excise = objSR.GetZeroExcise(sSession.AccessCode, sSession.AccessCodeID)
                End If

                If hfExciseAmount.Value <> "" Then
                    objSR.SRD_ExciseAmount = Request.Form(hfExciseAmount.UniqueID)
                Else
                    objSR.SRD_ExciseAmount = 0
                End If

                objSR.SRD_Operation = "C"
                objSR.SRD_IPAddress = sSession.IPAddress

                If Trim(txtEnterPrice.Text) <> "" Then
                    objSR.SRD_EnteredPrice = txtEnterPrice.Text
                Else
                    objSR.SRD_EnteredPrice = 0
                End If

                If Trim(txtMRP.Text) <> "" Then
                    objSR.SRD_DifferencePrice = txtMRP.Text
                Else
                    objSR.SRD_DifferencePrice = 0
                End If

                objSR.SRD_YearID = sSession.YearID

                Arr = objSR.SaveSalesReturnDetails(sSession.AccessCode, objSR)

                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated"
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    imgbtnSave.ImageUrl = "~/Images/Save24.png"
                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Saved"
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    imgbtnSave.ImageUrl = "~/Images/Update24.png"
                End If
            End If
            LoadExistingOrderGrid(iMasterID)
            txtOrderID.Text = iMasterID
            ClearDetails()

            If ddlreturntype.SelectedValue = 1 Or ddlreturntype.SelectedValue = 3 Then

                'imgbtnApprove.Visible = True
            Else

                'imgbtnApprove.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Public Sub ClearDetails()
        Try
            For i = 0 To lstBoxDescription.Items.Count - 1
                lstBoxDescription.Items(i).Selected = False
            Next
            ddlUnitOfMeassurement.Items.Clear() : ddlRate.Items.Clear() : txtMRP.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = ""
            ddlDiscount.SelectedIndex = 0 : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            ddlVAT.SelectedIndex = 0 : txtVATAmount.Text = "" : hfVATAmount.Value = ""
            ddlCST.SelectedIndex = 0 : txtCSTAmount.Text = "" : hfCSTAmount.Value = ""
            ddlExcise.SelectedIndex = 0 : txtExciseAmount.Text = "" : hfExciseAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = "" : txtEnterPrice.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearDetails")
        End Try
    End Sub
    Public Sub LoadExistingOrderGrid(ByVal iMasterID As Integer)
        Dim dt As New DataTable
        Try
            dt = objSR.BindExistingOrder(sSession.AccessCode, sSession.AccessCodeID, iMasterID)
            If dt.Rows.Count > 0 Then
                dgSaleReturn.DataSource = dt
                dgSaleReturn.DataBind()
                dgSaleReturn.Visible = True
                Session("SaleReturn") = dt
            Else
                dgSaleReturn.DataSource = Nothing
                dgSaleReturn.DataBind()
                lblError.Text = "No Orders Found."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingOrderGrid")
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        Try
            dt = objSR.GetSearch(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, Trim(txtSearch.Text))
            ddlSearch.DataSource = dt
            ddlSearch.DataTextField = "SRM_ReturnOrderCode"
            ddlSearch.DataValueField = "SRM_ID"
            ddlSearch.DataBind()
            ddlSearch.Items.Insert(0, "Existing Sales Return No")

            'ddlSearch.Visible = True
            'txtSearch.Visible = False : btnSearch.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSearch_Click")
        End Try
    End Sub
    Private Sub ddlSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearch.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Try
            If ddlSearch.SelectedIndex > 0 Then
                dtMaster = objSR.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlSearch.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("SRM_ReturnOrderCode")) = False Then
                            txtSaleReturnCode.Text = dtMaster.Rows(i)("SRM_ReturnOrderCode")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_ReferenceNo")) = False Then
                            txtReturnRefNo.Text = dtMaster.Rows(i)("SRM_ReferenceNo")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_ReturnDate")) = False Then
                            txtReturnDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SRM_ReturnDate"), "D")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_OrderNo")) = False Then
                            ddlOrderNo.SelectedValue = dtMaster.Rows(i)("SRM_OrderNo")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_OrderDate")) = False Then
                            txtOrderDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SRM_OrderDate"), "D")
                        End If
                        LoadDispatchNo(ddlOrderNo.SelectedValue)
                        If IsDBNull(dtMaster.Rows(i)("SRM_DispatchID")) = False Then
                            ddlInvoiceNo.SelectedValue = dtMaster.Rows(i)("SRM_DispatchID")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_DispatchDate")) = False Then
                            txtDispatchDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("SRM_DispatchDate"), "D")
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SRM_ReturnReason")) = False Then
                            If dtMaster.Rows(i)("SRM_ReturnReason") > 0 Then
                                ddlreturntype.SelectedValue = dtMaster.Rows(i)("SRM_ReturnReason")
                            Else
                                ddlreturntype.SelectedIndex = 0
                            End If
                        Else
                            ddlreturntype.SelectedIndex = 0
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SRM_DispatchRefNo")) = False Then
                            txtDispatchRefNo.Text = dtMaster.Rows(i)("SRM_DispatchRefNo")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_ESugamNo")) = False Then
                            txtESugamNo.Text = dtMaster.Rows(i)("SRM_ESugamNo")
                        End If

                        LoadPaymentType()
                        If dtMaster.Rows(i)("SRM_PaymentType") > 0 Then
                            ddlPaymentType.SelectedValue = dtMaster.Rows(i)("SRM_PaymentType")
                        Else
                            ddlPaymentType.SelectedIndex = 0
                        End If

                        LoadParty()
                        If IsDBNull(dtMaster.Rows(i)("SRM_PartyID")) = False Then
                            ddlParty.SelectedValue = dtMaster.Rows(i)("SRM_PartyID")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_PartyCode")) = False Then
                            txtPartyCode.Text = dtMaster.Rows(i)("SRM_PartyCode")
                        End If

                        LoadCategory()
                        If IsDBNull(dtMaster.Rows(i)("SRM_Category")) = False Then
                            If dtMaster.Rows(i)("SRM_Category") > 0 Then
                                ddlCategory.SelectedValue = dtMaster.Rows(i)("SRM_Category")
                            Else
                                ddlCategory.SelectedIndex = 0
                            End If
                            ddlCategory.SelectedIndex = 0
                        End If

                        LoadMethodOfShiping()
                        If IsDBNull(dtMaster.Rows(i)("SRM_ModeOfReturn")) = False Then
                            ddlModeOfShipping.SelectedValue = dtMaster.Rows(i)("SRM_ModeOfReturn")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_SaleType")) = False Then
                            ddlSalesType.SelectedValue = dtMaster.Rows(i)("SRM_SaleType")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("SRM_OtherType")) = False Then
                            ddlOthers.SelectedValue = dtMaster.Rows(i)("SRM_OtherType")
                        End If

                        If IsDBNull(dtMaster.Rows(i)("SRM_Narration")) = False Then
                            txtRemarks.Text = dtMaster.Rows(i)("SRM_Narration")
                        End If
                    Next
                End If
                LoadExistingOrderGrid(ddlSearch.SelectedValue)

                If ddlreturntype.SelectedValue = 1 Or ddlreturntype.SelectedValue = 3 Then

                    'imgbtnApprove.Visible = True
                Else

                    'imgbtnApprove.Visible = False
                End If
            Else
                'ddlSearch.Visible = False
                'txtSearch.Text = ""
                'txtSearch.Visible = True : btnSearch.Visible = True
                Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSearch_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub Clear()
        Try
            ddlSearch.SelectedIndex = 0
            txtSaleReturnCode.Text = "" : txtReturnRefNo.Text = "" : txtReturnDate.Text = ""
            ddlOrderNo.SelectedIndex = 0 : txtOrderDate.Text = ""
            ddlInvoiceNo.Items.Clear() : txtDispatchRefNo.Text = "" : txtDispatchDate.Text = "" : ddlModeOfShipping.Items.Clear()
            ddlPaymentType.Items.Clear() : ddlCommodity.Items.Clear() : txtESugamNo.Text = "" : ddlCategory.Items.Clear()
            ddlParty.Items.Clear() : txtPartyCode.Text = "" : ddlreturntype.SelectedIndex = 0 : ddlSalesType.SelectedIndex = 0 : ddlOthers.SelectedIndex = 0
            txtOrderID.Text = "" : txtMRPFromTable.Text = ""

            'For i = 0 To lstBoxDescription.Items.Count - 1
            '    lstBoxDescription.Items(i).Selected = False
            'Next
            lstBoxDescription.Items.Clear()
            ddlUnitOfMeassurement.Items.Clear() : ddlRate.Items.Clear() : txtMRP.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = ""

            'ddlDiscount.SelectedIndex = 0
            ddlDiscount.Items.Clear() : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            'ddlVAT.SelectedIndex = 0
            ddlVAT.Items.Clear()
            txtVATAmount.Text = "" : hfVATAmount.Value = ""
            'ddlCST.SelectedIndex = 0
            ddlCST.Items.Clear()
            txtCSTAmount.Text = "" : hfCSTAmount.Value = ""
            'ddlExcise.SelectedIndex = 0
            ddlExcise.Items.Clear()
            txtExciseAmount.Text = "" : hfExciseAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""

            dgSaleReturn.DataSource = Nothing
            dgSaleReturn.DataBind()
            GenerateOrderCode()
            txtEnterPrice.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Clear")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try

            'imgbtnApprove.Visible = False
            lblError.Text = ""
            'ddlSearch.Visible = False
            'txtSearch.Visible = True : btnSearch.Visible = True
            Clear()
            lblQty.Text = "" : lblRate.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim iSRMID As Integer
        Dim iCommodityID As Integer : Dim iItemID As Integer : Dim iHistoryID As Integer : Dim iReturnQty As Double

        Dim lblCommodityID, lblItemID, lblHistoryID, lblReturnQty As New Label
        Try
            If ddlreturntype.SelectedValue = 1 Or ddlreturntype.SelectedValue = 3 Then
                If ddlSearch.SelectedIndex > 0 Then
                    iSRMID = ddlSearch.SelectedValue
                Else
                    iSRMID = txtOrderID.Text
                End If
                objSR.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, iSRMID)

                If dgSaleReturn.Rows.Count > 0 Then
                    For i = 0 To dgSaleReturn.Rows.Count - 1

                        lblCommodityID = dgSaleReturn.Rows(i).FindControl("lblCommodityID")
                        lblItemID = dgSaleReturn.Rows(i).FindControl("lblDescID")
                        lblHistoryID = dgSaleReturn.Rows(i).FindControl("lblHistoryID")
                        lblReturnQty = dgSaleReturn.Rows(i).FindControl("lblReturnQty")

                        iCommodityID = lblCommodityID.Text
                        iItemID = lblItemID.Text
                        iHistoryID = lblHistoryID.Text
                        iReturnQty = lblReturnQty.Text
                        objSR.EffectToStockMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, sSession.IPAddress, ddlOrderNo.SelectedValue, iSRMID, txtRemarks.Text, iCommodityID, iItemID, iHistoryID, iReturnQty)
                    Next
                End If
                lblError.Text = "Approved Successfully."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    Private Sub ddlreturntype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlreturntype.SelectedIndexChanged
        Try
            For i = 0 To lstBoxDescription.Items.Count - 1
                lstBoxDescription.Items(i).Selected = False
            Next
            ddlUnitOfMeassurement.Items.Clear() : ddlRate.Items.Clear() : txtMRP.Text = ""
            txtQuantity.Text = "" : txtAmount.Text = ""
            ddlDiscount.Items.Clear() : txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            ddlVAT.Items.Clear() : txtVATAmount.Text = "" : hfVATAmount.Value = ""
            ddlCST.Items.Clear() : txtCSTAmount.Text = "" : hfCSTAmount.Value = ""
            ddlExcise.Items.Clear() : txtExciseAmount.Text = "" : hfExciseAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = "" : lblQty.Text = "" : lblRate.Text = "" : txtEnterPrice.Text = ""

            If ddlreturntype.SelectedValue = "1" Or ddlreturntype.SelectedValue = "3" Then
                txtQuantity.Enabled = True : ddlRate.Enabled = False : txtMRP.Enabled = False : txtEnterPrice.Enabled = False
                DisableAll()
            Else
                txtQuantity.Enabled = True : ddlRate.Enabled = True : txtMRP.Enabled = True : txtEnterPrice.Enabled = True
                DisableAll()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlreturntype_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub dgSaleReturn_PreRender(sender As Object, e As EventArgs) Handles dgSaleReturn.PreRender
        Dim dt As New DataTable
        Try
            If dgSaleReturn.Rows.Count > 0 Then
                dgSaleReturn.UseAccessibleHeader = True
                dgSaleReturn.HeaderRow.TableSection = TableRowSection.TableHeader
                dgSaleReturn.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSaleReturn_PreRender")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/Sales/SalesReturnMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
End Class
