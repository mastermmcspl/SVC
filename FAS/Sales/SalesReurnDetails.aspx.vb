Imports System
Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Partial Class Sales_SalesReurnDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Sales_SalesReturn"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Private Shared sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Dim objclsSaleReturn As New clsSaleReturn
    Dim lblID As New Label
    Dim iStatus As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnNew.ImageUrl = "~/Images/Reresh24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.ImageUrl = "~/Images/Download24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iMasterID As String = ""
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SRET")
                imgbtnNew.Visible = False : imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnAddCharge.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnAdd.Visible = True
                        imgbtnAddCharge.Visible = True
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnNew.Visible = True
                    End If
                End If
                Session.Remove("SRItemDetails")
                LoadChargeType()
                Session("ChargesMaster") = Nothing
                GvCharge.DataSource = Nothing
                GvCharge.DataBind()
                ddlOrderNo.Enabled = False : ddlDispatchNo.Enabled = False : ddlCustomer.Enabled = False
                LoadExistingSalesReturn() : LoadInvoiceNo() : LoadCommodity()
                txtReturnNo.Text = objclsSaleReturn.GenerateReturnNo(sSession.AccessCode, sSession.AccessCodeID)
                If Request.QueryString("StatusID") IsNot Nothing Then
                    iStatus = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("MasterID") IsNot Nothing Then
                    iMasterID = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterID")))
                    ddlExistSales.SelectedValue = iMasterID
                    ddlExistSales_SelectedIndexChanged(sender, e)
                Else
                    lblStatus.Text = "Not Started"
                End If
                RFVInvoiceNo.InitialValue = "Select Invoice No" : RFVInvoiceNo.ErrorMessage = "Select Invoice No."
                RFVOrderNo.InitialValue = "Select Order No" : RFVOrderNo.ErrorMessage = "Select Order No."
                RFVDispatchNo.InitialValue = "Select Dispatch No" : RFVDispatchNo.ErrorMessage = "Select Dispatch No."
                RFVCustomer.InitialValue = "Select Customer" : RFVCustomer.ErrorMessage = "Select Customer."
                RFVCommodity.InitialValue = "Select Commodity" : RFVCommodity.ErrorMessage = "Select Commodity"
                RFVReturn.InitialValue = "0" : RFVReturn.ErrorMessage = "Select Reason For Return"
                'REVCharges.ValidationExpression = "^[0-9]\d*(\.\d+)?$" : REVCharges.ErrorMessage = "Enter Valid Amount."
                REFReturnDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REFReturnDate.ErrorMessage = "Enter Valid Date Format."

                'Me.txtQuantity.Attributes.Add("onblur", "return RateAmount()")
                Me.ddlDiscount.Attributes.Add("onChange", "return RateAmount()")
                'Me.txtMRP.Attributes.Add("onblur", "return CalculateFinalAmount()")
                Me.txtMRP.Attributes.Add("onblur", "return RateAmount()")

                Me.txtQuantity.Attributes.Add("Onblur", "javascript:return RateAmountQtyCheck('" & lblInvoiceQty.ClientID & "','" & txtQuantity.ClientID & "')")

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objclsSaleReturn.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExistingSalesReturn()
        Try
            ddlExistSales.DataSource = objclsSaleReturn.LoadExistingReturnNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistSales.DataTextField = "Sales_Return_ReturnNo"
            ddlExistSales.DataValueField = "Sales_Return_ID"
            ddlExistSales.DataBind()
            ddlExistSales.Items.Insert(0, "Select Existing Return No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadInvoiceNo()
        Try
            ddlInvoiceNo.DataSource = objclsSaleReturn.LoadInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlInvoiceNo.DataTextField = "SDM_Code"
            ddlInvoiceNo.DataValueField = "SDM_ID"
            ddlInvoiceNo.DataBind()
            ddlInvoiceNo.Items.Insert(0, "Select Invoice No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCustomer()
        Try
            ddlCustomer.DataSource = objclsSaleReturn.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomer.DataTextField = "BM_Name"
            ddlCustomer.DataValueField = "BM_ID"
            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objclsSaleReturn.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadOrderNo()
        Try
            ddlOrderNo.DataSource = objclsSaleReturn.LoadOrderNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlOrderNo.DataTextField = "SPO_OrderCode"
            ddlOrderNo.DataValueField = "SPO_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "Select Order No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadDispatchNo()
        Try
            ddlDispatchNo.DataSource = objclsSaleReturn.BindDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlDispatchNo.DataTextField = "DM_Code"
            ddlDispatchNo.DataValueField = "DM_ID"
            ddlDispatchNo.DataBind()
            ddlDispatchNo.Items.Insert(0, "Select Dispatch No")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlInvoiceNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInvoiceNo.SelectedIndexChanged
        Dim dt As New DataTable, dtItem As New DataTable
        Try
            lblError.Text = "" : txtGoodsReturnRefNo.Text = ""
            If ddlInvoiceNo.SelectedIndex > 0 Then
                dt = objclsSaleReturn.LoadSalesDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("SDM_DispatchDate")) = False Then
                        txtInvoiceDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0).Item("SDM_DispatchDate").ToString(), "D")
                    End If
                    If IsDBNull(dt.Rows(0).Item("SDM_OrderID")) = False Then
                        LoadOrderNo()
                        ddlOrderNo.SelectedValue = dt.Rows(0).Item("SDM_OrderID").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("DM_ID")) = False Then
                        LoadDispatchNo()
                        ddlDispatchNo.SelectedValue = dt.Rows(0).Item("DM_ID").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("SDM_SupplierID")) = False Then
                        LoadCustomer()
                        ddlCustomer.SelectedValue = dt.Rows(0).Item("SDM_SupplierID").ToString()
                    End If
                End If
                lstBoxDescription.DataSource = objclsSaleReturn.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue, 0)
                lstBoxDescription.DataTextField = "INV_Code"
                lstBoxDescription.DataValueField = "INV_ID"
                lstBoxDescription.DataBind()
                ddlUnitOfMeassurement.Items.Clear() : txtQuantity.Text = "" : txtAmount.Text = "" : txtMRP.Text = "" : ddlDiscount.Items.Clear()
                txtDiscountAmount.Text = "" : txtGSTID.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtNetAmount.Text = ""
            Else
                txtInvoiceDate.Text = "" : ddlOrderNo.Items.Clear() : ddlDispatchNo.Items.Clear() : ddlCustomer.Items.Clear()
                lstBoxDescription.Items.Clear()
                txtReturnNo.Text = objclsSaleReturn.GenerateReturnNo(sSession.AccessCode, sSession.AccessCodeID)
                lblStatus.Text = "Not Started" : ddlInvoiceNo.Enabled = True
                LoadExistingSalesReturn() : LoadInvoiceNo() : LoadCommodity()
                txtReturnDate.Text = "" : txtSearchItem.Text = "" : ddlReturn.SelectedIndex = 0 : txtCharges.Text = "" : txtRemarks.Text = "" : txtShipTo.Text = ""
                dgSRItemDetails.DataSource = Nothing
                dgSRItemDetails.DataBind()
                lstBoxDescription.Items.Clear()
                ddlUnitOfMeassurement.Items.Clear() : txtQuantity.Text = "" : txtAmount.Text = "" : txtMRP.Text = "" : ddlDiscount.Items.Clear()
                txtDiscountAmount.Text = "" : txtGSTID.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtNetAmount.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInvoiceNo_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            lblID.Text = "" : lblError.Text = ""
            ddlUnitOfMeassurement.Items.Clear() : txtQuantity.Text = "" : txtMRP.Text = "" : ddlReturn.SelectedIndex = 0 : ddlDiscount.Items.Clear()
            txtCharges.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtNetAmount.Text = ""
            If ddlCommodity.SelectedIndex > 0 Then
                If ddlInvoiceNo.SelectedIndex > 0 Then
                    lstBoxDescription.DataSource = objclsSaleReturn.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue, ddlCommodity.SelectedValue)
                    lstBoxDescription.DataTextField = "INV_Code"
                    lstBoxDescription.DataValueField = "INV_ID"
                    lstBoxDescription.DataBind()
                End If
            Else
                If ddlInvoiceNo.SelectedIndex > 0 Then
                    lstBoxDescription.DataSource = objclsSaleReturn.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue, 0)
                    lstBoxDescription.DataTextField = "INV_Code"
                    lstBoxDescription.DataValueField = "INV_ID"
                    lstBoxDescription.DataBind()
                Else
                    lstBoxDescription.Items.Clear()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInvoiceNo_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadDiscount()
        Dim dt As New DataTable
        Try
            dt = objclsSaleReturn.LoadDiscounts(sSession.AccessCode, sSession.AccessCodeID)
            ddlDiscount.DataSource = dt
            ddlDiscount.DataTextField = "Mas_Desc"
            ddlDiscount.DataValueField = "Mas_Id"
            ddlDiscount.DataBind()
            ddlDiscount.Items.Insert(0, "Select Discount")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lstBoxDescription_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxDescription.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sCode As String = ""
        Dim IHistoryID As Integer, iDiscount As Integer = 0
        Dim dInvoiceQty, dReturnQty As Double
        Try
            lblID.Text = "" : lblError.Text = ""
            lblError.Text = ""
            'btnSave.Text = "Add"
            txtQuantity.Text = "" : txtAmount.Text = "" : hfAmount.Value = ""
            txtDiscountAmount.Text = "" : hfDiscountAmount.Value = ""
            txtNetAmount.Text = "" : hfNetAmount.Value = ""
            txtGSTAmount.Text = "" : txtGSTRate.Text = "" : txtGSTID.Text = ""
            hfGSTAmount.Value = ""
            If lstBoxDescription.SelectedIndex <> -1 Then
                ddlCommodity.SelectedValue = objclsSaleReturn.GetCommodity(sSession.AccessCode, sSession.AccessCodeID, lstBoxDescription.SelectedValue)
                IHistoryID = objclsSaleReturn.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedValue, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)

                'Check For Return Qty is greater than Order qty'
                If ddlInvoiceNo.SelectedIndex > 0 Then
                    dInvoiceQty = objclsSaleReturn.GetInvoiceQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, IHistoryID)
                    dReturnQty = objclsSaleReturn.GetReturnedQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue, IHistoryID)

                    txtAvailableQty.Text = dInvoiceQty - dReturnQty
                End If
                'Check For Return Qty is greater than Order qty'
                lblInvoiceQty.Text = dInvoiceQty

                If txtAvailableQty.Text = 0 Then
                    lblError.Text = "You can not return Qty more than invoice Qty."
                    lblSalesValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                    Exit Sub
                End If

                BindUnitOfMeassurement(lstBoxDescription.SelectedValue, IHistoryID)
                LoadDiscount()
                dt = objclsSaleReturn.GetDetails(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedValue, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("SDD_UnitID")) = False Then
                        ddlUnitOfMeassurement.SelectedValue = dt.Rows(0).Item("SDD_UnitID").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("SDD_Rate")) = False Then
                        txtMRP.Text = dt.Rows(0).Item("SDD_Rate").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("SDD_Discount")) = False Then
                        If dt.Rows(0).Item("SDD_Discount") > 0.00 Then
                            iDiscount = objclsSaleReturn.GetDiscountID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0).Item("SDD_Discount").ToString())
                            ddlDiscount.SelectedValue = iDiscount
                        Else
                            ddlDiscount.SelectedIndex = 0
                        End If
                    End If
                    If IsDBNull(dt.Rows(0).Item("SDD_GSTRate")) = False Then
                        txtGSTID.Text = objclsSaleReturn.GetGSTID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
                        txtGSTRate.Text = dt.Rows(0).Item("SDD_GSTRate").ToString()
                    End If
                End If
                txtAmt.Text = objclsSaleReturn.GetChargeAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDispatchNo.SelectedValue, ddlOrderNo.SelectedValue)
                txtSAmt.Text = objclsSaleReturn.GetRateAmount(sSession.AccessCode, sSession.AccessCodeID, ddlDispatchNo.SelectedValue)

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstBoxDescription_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object
        Try
            lblError.Text = ""
            If lblStatus.Text = "De-Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf lblStatus.Text = "Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Not Started" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Waiting for Approval" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            End If
            Response.Redirect(String.Format("~/Sales/SalesReturnDashboard.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSRItemDetails_ItemDataBound")
        End Try
    End Sub
    Private Sub ddlExistSales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistSales.SelectedIndexChanged
        Dim dt, dtCharge As New DataTable
        Try
            lblID.Text = "" : lblError.Text = ""
            If ddlExistSales.SelectedIndex > 0 Then
                dt = objclsSaleReturn.LoadSalesReturnDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistSales.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_ReturnNo").ToString()) = False Then
                        txtReturnNo.Text = dt.Rows(0).Item("Sales_Return_ReturnNo").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_RetrunDate").ToString()) = False Then
                        txtReturnDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0).Item("Sales_Return_RetrunDate").ToString(), "D")
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_InvoiceNo").ToString()) = False Then
                        ddlInvoiceNo.SelectedValue = dt.Rows(0).Item("Sales_Return_InvoiceNo").ToString()
                        lstBoxDescription.DataSource = objclsSaleReturn.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue, 0)
                        lstBoxDescription.DataTextField = "INV_Code"
                        lstBoxDescription.DataValueField = "INV_ID"
                        lstBoxDescription.DataBind()
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_InvoiceDate").ToString()) = False Then
                        txtInvoiceDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0).Item("Sales_Return_InvoiceDate").ToString(), "D")
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_OrderNo").ToString()) = False Then
                        LoadOrderNo()
                        ddlOrderNo.SelectedValue = dt.Rows(0).Item("Sales_Return_OrderNo").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_DispatchNo").ToString()) = False Then
                        LoadDispatchNo()
                        ddlDispatchNo.SelectedValue = dt.Rows(0).Item("Sales_Return_DispatchNo").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_Customer").ToString()) = False Then
                        LoadCustomer()
                        ddlCustomer.SelectedValue = dt.Rows(0).Item("Sales_Return_Customer").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_ShipTo").ToString()) = False Then
                        txtShipTo.Text = dt.Rows(0).Item("Sales_Return_ShipTo").ToString()
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_DelFlag").ToString()) = False Then
                        ddlInvoiceNo.Enabled = False
                        If dt.Rows(0).Item("Sales_Return_DelFlag").ToString() = "W" Then
                            lblStatus.Text = "Waiting for Approval"
                        ElseIf dt.Rows(0).Item("Sales_Return_DelFlag").ToString() = "A" Then
                            lblStatus.Text = "Activated"
                        ElseIf dt.Rows(0).Item("Sales_Return_DelFlag").ToString() = "D" Then
                            lblStatus.Text = "De-Activated"
                        End If
                    End If
                    If IsDBNull(dt.Rows(0).Item("Sales_Return_GoodsReturnNo").ToString()) = False Then
                        txtGoodsReturnRefNo.Text = dt.Rows(0).Item("Sales_Return_GoodsReturnNo").ToString()
                    End If

                End If
                dgSRItemDetails.DataSource = objclsSaleReturn.LoadSalesReturn(sSession.AccessCode, sSession.AccessCodeID, ddlExistSales.SelectedValue)
                dgSRItemDetails.DataBind()

                dtCharge = objclsSaleReturn.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistSales.SelectedValue)
                GvCharge.DataSource = dtCharge
                GvCharge.DataBind()
                Session("ChargesMaster") = dtCharge

                lstBoxDescription.Items.Clear()
                txtSearchItem.Text = "" : ddlReturn.SelectedIndex = 0 : txtCharges.Text = "" : txtRemarks.Text = ""
                ddlUnitOfMeassurement.Items.Clear() : txtQuantity.Text = "" : txtAmount.Text = "" : txtMRP.Text = "" : ddlDiscount.Items.Clear()
                txtDiscountAmount.Text = "" : txtGSTID.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtNetAmount.Text = ""
            Else
                txtReturnNo.Text = objclsSaleReturn.GenerateReturnNo(sSession.AccessCode, sSession.AccessCodeID)
                lblStatus.Text = "Not Started" : ddlInvoiceNo.Enabled = True
                LoadExistingSalesReturn() : LoadInvoiceNo() : LoadCommodity()
                txtReturnDate.Text = "" : txtSearchItem.Text = "" : ddlReturn.SelectedIndex = 0 : txtCharges.Text = "" : txtRemarks.Text = "" : txtShipTo.Text = ""
                ddlInvoiceNo_SelectedIndexChanged(sender, e)
                dgSRItemDetails.DataSource = Nothing
                dgSRItemDetails.DataBind()
                txtGoodsReturnRefNo.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistSales_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim Arr() As String, Arr1() As String
        Dim iMasterID As Integer = 0, iHistoryID As Integer = 0
        Dim sDispatchStatus As String = "", sState As String = ""
        Try
            lblError.Text = ""
            If lblStatus.Text = "Activated" Then
                lblSalesValidationMsg.Text = "Return No (" & txtReturnNo.Text & ") has been Approved, It can not be Edited/Updated."
                lblError.Text = "Return No (" & txtReturnNo.Text & ") has been Approved, It can not be Edited/Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                Exit Sub
            End If
            If txtReturnNo.Text.Trim() = "" Then
                lblSalesValidationMsg.Text = "Enter Return No." : lblError.Text = "Enter Return No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                txtReturnNo.Focus()
                Exit Sub
            End If
            If txtReturnDate.Text.Trim() = "" Then
                lblSalesValidationMsg.Text = "Enter Return Date." : lblError.Text = "Enter Return Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                txtReturnDate.Focus()
                Exit Sub
            End If
            Dim dDate As Date, dTargetDate As Date
            dDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dTargetDate = Date.ParseExact(txtReturnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim l As Integer
            l = DateDiff(DateInterval.Day, dDate, dTargetDate)
            If l < 0 Then
                lblError.Text = "Return Date (" & txtReturnDate.Text & ") should be greater than or equal to Invoice Date " & dDate & "."
                lblSalesValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                txtReturnDate.Focus()
                Exit Sub
            End If
            If ddlInvoiceNo.SelectedIndex = 0 Then
                lblSalesValidationMsg.Text = "Select Invoice No." : lblError.Text = "Select Invoice No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                ddlInvoiceNo.Focus()
                Exit Sub
            End If
            If txtInvoiceDate.Text.Trim() = "" Then
                lblSalesValidationMsg.Text = "Enter Invoice Date." : lblError.Text = "Enter Invoice Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                txtInvoiceDate.Focus()
                Exit Sub
            End If
            If ddlOrderNo.SelectedIndex <= 0 Then
                lblSalesValidationMsg.Text = "Select Order No." : lblError.Text = "Select Order No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                ddlOrderNo.Focus()
                Exit Sub
            End If
            If ddlDispatchNo.SelectedIndex <= 0 Then
                lblSalesValidationMsg.Text = "Select Dispatch No." : lblError.Text = "Select Dispatch No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                ddlDispatchNo.Focus()
                Exit Sub
            End If
            If ddlCustomer.SelectedIndex <= 0 Then
                lblSalesValidationMsg.Text = "Select Customer." : lblError.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                ddlCustomer.Focus()
                Exit Sub
            End If
            If txtShipTo.Text.Trim() = "" Then
                lblSalesValidationMsg.Text = "Enter Ship To." : lblError.Text = "Enter Ship To."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                txtShipTo.Focus()
                Exit Sub
            End If
            If txtShipTo.Text.Length > 8000 Then
                lblSalesValidationMsg.Text = "Ship To exceeded maximum size(max 8000 characters)." : lblError.Text = "Ship To exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                txtShipTo.Focus()
                Exit Sub
            End If
            If ddlCommodity.SelectedIndex = 0 Then
                lblSalesValidationMsg.Text = "Select Commodity." : lblError.Text = "Select Commodity."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                ddlCommodity.Focus()
                Exit Sub
            End If
            If ddlReturn.SelectedIndex = 0 Then
                lblSalesValidationMsg.Text = "Select Reason For Return." : lblError.Text = "Select Reason For Return."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                ddlReturn.Focus()
                Exit Sub
            End If
            'If txtCharges.Text.Trim() = "" Then
            '    lblSalesValidationMsg.Text = "Enter Charges." : lblError.Text = "Enter Charges."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
            '    txtCharges.Focus()
            '    Exit Sub
            'End If
            If txtRemarks.Text.Length > 2000 Then
                lblSalesValidationMsg.Text = "Remarks exceeded maximum size(max 2000 characters)." : lblError.Text = "Remarks exceeded maximum size(max 2000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                txtRemarks.Focus()
                Exit Sub
            End If
            If ddlExistSales.SelectedIndex > 0 Then
                objclsSaleReturn.Sales_Return_ID = ddlExistSales.SelectedValue
            Else
                objclsSaleReturn.Sales_Return_ID = 0
            End If
            objclsSaleReturn.Sales_Return_Year = sSession.YearID
            objclsSaleReturn.Sales_Return_ReturnNo = txtReturnNo.Text
            objclsSaleReturn.Sales_Return_RetrunDate = Date.ParseExact(txtReturnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsSaleReturn.Sales_Return_InvoiceNo = ddlInvoiceNo.SelectedValue
            objclsSaleReturn.Sales_Return_InvoiceDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsSaleReturn.Sales_Return_OrderNo = ddlOrderNo.SelectedValue
            objclsSaleReturn.Sales_Return_DispatchNo = ddlDispatchNo.SelectedValue
            objclsSaleReturn.Sales_Return_Customer = ddlCustomer.SelectedValue
            objclsSaleReturn.Sales_Return_ShipTo = objGen.SafeSQL(txtShipTo.Text.Trim())
            objclsSaleReturn.Sales_Return_CreatedBy = sSession.UserID
            objclsSaleReturn.Sales_Return_UpdatedBy = sSession.UserID
            objclsSaleReturn.Sales_Return_IPAddress = sSession.IPAddress
            objclsSaleReturn.Sales_Return_CompID = sSession.AccessCodeID
            sDispatchStatus = objclsSaleReturn.GetStateStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDispatchNo.SelectedValue, "D")
            sState = objclsSaleReturn.GetStateStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDispatchNo.SelectedValue, "S")
            objclsSaleReturn.Sales_Return_DispatchStatus = sDispatchStatus
            objclsSaleReturn.Sales_Return_State = sState

            objclsSaleReturn.Sales_Return_GoodsReturnNo = txtGoodsReturnRefNo.Text

            Arr = objclsSaleReturn.SaveSalesReturnMaster(sSession.AccessCode, sSession.AccessCodeID, objclsSaleReturn)
            iMasterID = Arr(1)
            If lblID.Text <> "" Then
                objclsSaleReturn.SRD_ID = lblID.Text.Trim
            Else
                objclsSaleReturn.SRD_ID = 0
            End If
            objclsSaleReturn.SRD_MasterID = iMasterID
            objclsSaleReturn.SRD_Commodity = ddlCommodity.SelectedValue
            objclsSaleReturn.SRD_Item = lstBoxDescription.SelectedValue
            objclsSaleReturn.SRD_UnitID = ddlUnitOfMeassurement.SelectedValue
            iHistoryID = objclsSaleReturn.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedValue, ddlCommodity.SelectedValue, lstBoxDescription.SelectedValue)
            objclsSaleReturn.SRD_HistoryID = iHistoryID
            objclsSaleReturn.SRD_Rate = txtMRP.Text.Trim
            objclsSaleReturn.SRD_Quantity = txtQuantity.Text.Trim
            objclsSaleReturn.SRD_RateAmount = txtAmount.Text.Trim
            If ddlDiscount.SelectedIndex > 0 Then
                objclsSaleReturn.SRD_Discount = ddlDiscount.SelectedValue
                objclsSaleReturn.SRD_DiscountAmount = txtDiscountAmount.Text.Trim
            Else
                objclsSaleReturn.SRD_Discount = 0
                objclsSaleReturn.SRD_DiscountAmount = 0
            End If

            objclsSaleReturn.SRD_TotalAmount = txtNetAmount.Text.Trim

            'objclsSaleReturn.SRD_Amount = (txtAmount.Text.Trim - objclsSaleReturn.SRD_DiscountAmount) + txtCharges.Text.Trim
            objclsSaleReturn.SRD_Amount = (txtAmount.Text.Trim - objclsSaleReturn.SRD_DiscountAmount)
            objclsSaleReturn.SRD_Reason = ddlReturn.SelectedValue

            'If txtCharges.Text <> "" Then
            '    objclsSaleReturn.SRD_Charges = txtCharges.Text.Trim
            'Else
            '    objclsSaleReturn.SRD_Charges = 0
            'End If
            objclsSaleReturn.SRD_Charges = 0

            objclsSaleReturn.SRD_GST_ID = txtGSTID.Text.Trim
            objclsSaleReturn.SRD_GSTRate = txtGSTRate.Text.Trim
            objclsSaleReturn.SRD_GSTAmount = txtGSTAmount.Text.Trim

            If objclsSaleReturn.Sales_Return_DispatchStatus = "Local" Then
                objclsSaleReturn.SRD_SGST = objclsSaleReturn.SRD_GSTRate / 2
                objclsSaleReturn.SRD_SGSTAmount = objclsSaleReturn.SRD_GSTAmount / 2
                objclsSaleReturn.SRD_CGST = objclsSaleReturn.SRD_GSTRate / 2
                objclsSaleReturn.SRD_CGSTAmount = objclsSaleReturn.SRD_GSTAmount / 2
                objclsSaleReturn.SRD_IGST = 0
                objclsSaleReturn.SRD_IGSTAmount = 0
            ElseIf objclsSaleReturn.Sales_Return_DispatchStatus = "Inter State" Then
                objclsSaleReturn.SRD_SGST = 0
                objclsSaleReturn.SRD_SGSTAmount = 0
                objclsSaleReturn.SRD_CGST = 0
                objclsSaleReturn.SRD_CGSTAmount = 0
                objclsSaleReturn.SRD_IGST = objclsSaleReturn.SRD_GSTRate
                objclsSaleReturn.SRD_IGSTAmount = objclsSaleReturn.SRD_GSTAmount
            End If
            objclsSaleReturn.SRD_Remarks = objGen.SafeSQL(txtRemarks.Text)
            objclsSaleReturn.SRD_IPAddress = sSession.IPAddress
            objclsSaleReturn.SRD_CompID = sSession.AccessCodeID
            Arr1 = objclsSaleReturn.SaveSalesReturnDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSaleReturn)
            If Arr1(0) = 2 Then
                lblSalesValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
            Else
                lblSalesValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
            End If

            SaveCharges(iMasterID)

            LoadExistingSalesReturn()
            ddlExistSales.SelectedValue = iMasterID
            ddlExistSales_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub SaveCharges(ByVal iMasterID As Integer)
        Dim Arr() As String
        Try
            'Deleting charges Everytime & Saving'
            Dim iAllocationID As Integer

            iAllocationID = 0

            objclsSaleReturn.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1

                    objclsSaleReturn.C_ID = 0
                    objclsSaleReturn.C_OrderID = ddlOrderNo.SelectedValue
                    objclsSaleReturn.C_AllocatedID = 0
                    If ddlDispatchNo.SelectedIndex > 0 Then
                        objclsSaleReturn.C_DispatchID = ddlDispatchNo.SelectedValue
                    Else
                        objclsSaleReturn.C_DispatchID = 0
                    End If
                    objclsSaleReturn.C_OrderType = ""
                    objclsSaleReturn.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objclsSaleReturn.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objclsSaleReturn.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objclsSaleReturn.C_PSType = "S"
                    objclsSaleReturn.C_DelFlag = "W"
                    objclsSaleReturn.C_Status = "C"
                    objclsSaleReturn.C_CompID = sSession.AccessCodeID
                    objclsSaleReturn.C_YearID = sSession.YearID
                    objclsSaleReturn.C_CreatedBy = sSession.UserID
                    objclsSaleReturn.C_CreatedOn = System.DateTime.Now
                    objclsSaleReturn.C_Operation = "C"
                    objclsSaleReturn.C_IPAddress = sSession.IPAddress
                    objclsSaleReturn.C_SalesReturnID = iMasterID
                    objclsSaleReturn.C_GoodsReturnID = 0

                    Arr = objclsSaleReturn.SaveCharges(sSession.AccessCode, objclsSaleReturn)
                Next
            End If
            'Charges Saving'
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveCharges")
        End Try
    End Sub
    Private Sub dgSRItemDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgSRItemDetails.RowDataBound
        Dim imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSRItemDetails_RowDataBound")
        End Try
    End Sub
    Private Sub dgSRItemDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgSRItemDetails.RowCommand
        Dim lblDescID As New Label, lblMasterID As New Label, lblCommodityID As New Label, lblItemID As New Label, lblUnitID As New Label, lblDiscountID As New Label
        Dim lblQTY As New Label, lblAmt As New Label, lblRate As New Label, lblTotDiscount As New Label, lblGst As New Label, lblGSTAmount As New Label, lblTotAmt As New Label
        Dim lblReasonID As New Label, lblCharges As New Label, lblRemarks As New Label, lblGSTID As New Label, lblHistoryID As New Label
        Try
        lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblId"), Label)
            lblID.Text = lblDescID.Text
            If e.CommandName.Equals("GrdEdit") Then
                lblMasterID = DirectCast(clickedRow.FindControl("lblMasterID"), Label)
                lblCommodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
                lblItemID = DirectCast(clickedRow.FindControl("lblItemID"), Label)
                lblReasonID = DirectCast(clickedRow.FindControl("lblReasonID"), Label)
                lblUnitID = DirectCast(clickedRow.FindControl("lblUnitID"), Label)
                lblDiscountID = DirectCast(clickedRow.FindControl("lblDiscountID"), Label)
                lblGSTID = DirectCast(clickedRow.FindControl("lblGSTID"), Label)
                lblHistoryID = DirectCast(clickedRow.FindControl("lblHistoryID"), Label)
                lblQTY = DirectCast(clickedRow.FindControl("lblQTY"), Label)
                lblAmt = DirectCast(clickedRow.FindControl("lblAmt"), Label)
                lblRate = DirectCast(clickedRow.FindControl("lblRate"), Label)
                lblTotDiscount = DirectCast(clickedRow.FindControl("lblTotDiscount"), Label)
                lblGst = DirectCast(clickedRow.FindControl("lblGst"), Label)
                lblGSTAmount = DirectCast(clickedRow.FindControl("lblGSTAmount"), Label)
                lblTotAmt = DirectCast(clickedRow.FindControl("lblTotAmt"), Label)
                lblCharges = DirectCast(clickedRow.FindControl("lblCharges"), Label)
                lblRemarks = DirectCast(clickedRow.FindControl("lblRemarks"), Label)
                If lblCommodityID.Text <> "" Then
                    ddlCommodity.SelectedValue = lblCommodityID.Text
                End If
                If lblItemID.Text <> "" Then
                    lstBoxDescription.DataSource = objclsSaleReturn.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue, 0)
                    lstBoxDescription.DataTextField = "INV_Code"
                    lstBoxDescription.DataValueField = "INV_ID"
                    lstBoxDescription.DataBind()
                    lstBoxDescription.SelectedValue = lblItemID.Text
                End If
                If lblReasonID.Text <> "" Then
                    ddlReturn.SelectedValue = lblReasonID.Text
                End If

                If lblUnitID.Text <> "" Then
                    BindUnitOfMeassurement(lstBoxDescription.SelectedValue, lblHistoryID.Text)
                    LoadDiscount()
                    ddlUnitOfMeassurement.SelectedValue = lblUnitID.Text
                End If
                If lblDiscountID.Text <> "" Then
                    If lblDiscountID.Text <> 0 Then
                        ddlDiscount.SelectedValue = lblDiscountID.Text
                    Else
                        ddlDiscount.Items.Clear()
                    End If
                End If
                If lblGSTID.Text <> "" Then
                    txtGSTID.Text = lblGSTID.Text
                End If

                If lblQTY.Text <> "" Then
                    txtQuantity.Text = lblQTY.Text
                End If
                If lblAmt.Text <> "" Then
                    txtAmount.Text = lblAmt.Text
                End If
                If lblRate.Text <> "" Then
                    txtMRP.Text = lblRate.Text
                End If
                If lblTotDiscount.Text <> "" Then
                    If lblDiscountID.Text <> 0 Then
                        txtDiscountAmount.Text = lblTotDiscount.Text
                    Else
                        txtDiscountAmount.Text = ""
                    End If
                End If
                If lblGst.Text <> "" Then
                    txtGSTRate.Text = lblGst.Text
                End If
                If lblGSTAmount.Text <> "" Then
                    txtGSTAmount.Text = lblGSTAmount.Text
                End If
                If lblTotAmt.Text <> "" Then
                    txtNetAmount.Text = lblTotAmt.Text
                End If
                'If lblCharges.Text <> "" Then
                '    txtCharges.Text = lblCharges.Text
                'End If
                txtCharges.Text = 0
                If lblRemarks.Text <> "" Then
                    txtRemarks.Text = objGen.ReplaceSafeSQL(lblRemarks.Text.Trim())
                End If
                txtAmt.Text = objclsSaleReturn.GetChargeAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDispatchNo.SelectedValue, ddlOrderNo.SelectedValue)
                txtSAmt.Text = objclsSaleReturn.GetRateAmount(sSession.AccessCode, sSession.AccessCodeID, ddlDispatchNo.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSRItemDetails_RowCommand")
        End Try
    End Sub
    Private Sub dgSRItemDetails_PreRender(sender As Object, e As EventArgs) Handles dgSRItemDetails.PreRender
        Try
            If dgSRItemDetails.Rows.Count > 0 Then
                dgSRItemDetails.UseAccessibleHeader = True
                dgSRItemDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                dgSRItemDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSRItemDetails_PreRender")
        End Try
    End Sub
    Private Sub imgbtnNew_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNew.Click
        Try
            lblInvoiceQty.Text = ""
            lblError.Text = "" : lblID.Text = ""
            txtReturnNo.Text = objclsSaleReturn.GenerateReturnNo(sSession.AccessCode, sSession.AccessCodeID)
            lblStatus.Text = "Not Started" : ddlInvoiceNo.Enabled = True
            LoadExistingSalesReturn() : LoadInvoiceNo() : LoadCommodity()
            txtReturnDate.Text = "" : txtSearchItem.Text = "" : ddlReturn.SelectedIndex = 0 : txtCharges.Text = "" : txtRemarks.Text = "" : txtShipTo.Text = ""
            ddlUnitOfMeassurement.Items.Clear() : txtQuantity.Text = "" : txtAmount.Text = "" : txtMRP.Text = "" : ddlDiscount.Items.Clear()
            txtDiscountAmount.Text = "" : txtGSTID.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtNetAmount.Text = ""
            ddlInvoiceNo_SelectedIndexChanged(sender, e)
            dgSRItemDetails.DataSource = Nothing
            dgSRItemDetails.DataBind()

            txtAvailableQty.Text = ""
            Session("ChargesMaster") = Nothing
            GvCharge.DataSource = Nothing
            GvCharge.DataBind()
            txtGoodsReturnRefNo.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNew_Click")
        End Try
    End Sub
    Private Sub imgbtnReport_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnReport.Click
        Dim oID As Object
        Try
            If ddlExistSales.SelectedIndex > 0 Then
                oID = HttpUtility.UrlEncode(objGen.EncryptQueryString(ddlExistSales.SelectedValue))
                Response.Redirect(String.Format("~/Reports/SalesReturnReport.aspx?MasterID={0}", oID), False)
            Else
                lblSalesValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnReport_Click")
        End Try
    End Sub
    Public Sub BindUnitOfMeassurement(ByVal iItemID As Integer, ByVal iHistoryID As Integer)
        Try
            ddlUnitOfMeassurement.DataSource = objclsSaleReturn.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iItemID, iHistoryID)
            ddlUnitOfMeassurement.DataTextField = "Mas_Desc"
            ddlUnitOfMeassurement.DataValueField = "Mas_ID"
            ddlUnitOfMeassurement.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlReturn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReturn.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlReturn.SelectedIndex > 0 Then
                If ddlReturn.SelectedIndex = 3 Then
                    txtMRP.CssClass = "aspxcontrols" : txtMRP.Enabled = True
                Else
                    txtMRP.CssClass = "aspxcontrolsdisable" : txtMRP.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReturn_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
        Dim dt, dtTable As New DataTable
        Dim dchargeTotal As Double
        Try
            If ddlChargeType.SelectedIndex > 0 Then
                If txtShippingRate.Text <> "" Then
                    dt = AddCharges()
                    dtTable = objclsSaleReturn.RemoveChargeDublicate(dt)
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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub
End Class
