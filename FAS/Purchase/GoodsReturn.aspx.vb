Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports DatabaseLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing

Partial Class Purchase_GoodsReturn
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Orders/PurchaseOrder"
    Dim sSession As New AllSession
    Dim objGreturn As New clsGoodsReturn
    Dim objVerification As New clsPurchaseVrification
    Dim objPO As New clsPurchaseOrder
    Dim objDb As New DBHelper
    Dim objGin As New ClsGoodsInward
    Private Shared sIKBBackStatus As String
    Dim objClsFASGnrl As New clsFASGeneral
    Dim objclsModulePermission As New clsModulePermission
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
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
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "GR")
                imgRefresh.Visible = False : imgbtnWaiting.Visible = False : imgbtnAdd.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgRefresh.Visible = True
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnWaiting.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If
                Session("ChargesMaster") = Nothing

                ReFVONo.ErrorMessage = "Select Purchase Order No." : ReFVONo.InitialValue = "--- Purchase Order No ---"
                RFVRDate.ErrorMessage = "Enter Return Date."
                RFVINo.ErrorMessage = "Select Invoice No." : RFVINo.InitialValue = "--- Select Invoice No. ---"
                RFVRRefNo.ErrorMessage = "Enter Return Refference No."
                RFVReason.ErrorMessage = "Please Select Reason." : RFVReason.InitialValue = "Select Type"
                RFVRemarks.ErrorMessage = "Enter Remarks."
                RFVCat.ErrorMessage = "Select Item" : RFVCat.InitialValue = ""

                Me.txtQuantity.Attributes.Add("onblur", "return CalculateQuantityCheck('" & lblPurchasedQty.ClientID & "','" & txtQuantity.ClientID & "')")
                'Me.txtQuantity.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtRate.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtDiscount.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtGSTRate.Attributes.Add("onblur", "return CalculateQuantity()")

                GenerateOrderCode()
                BindOrder()
                LoadSuppliers()
                LoadCommodity()
                LoadChargeType()
                BindTypeOfPurchasedetails()
                LoadExistingGoodsReturn()
                ddlInvoiceNo.Items.Insert(0, "--- Select Invoice No. ---")

                Dim iAID As String = "" : Dim iDashBoard As String = ""
                iDashBoard = Request.QueryString("sStrID")
                If iDashBoard = "1" Then
                    iAID = Request.QueryString("sStrID")
                    If iAID <> "AddNew" Then
                        ddlExistingOrder.SelectedValue = Request.QueryString("AID")
                        ddlExistingOrder_SelectedIndexChanged(sender, e)
                    Else
                    End If
                ElseIf iDashBoard = "0" Then
                End If
                If iDashBoard = "" Then
                    Dim iAllocateID As String = ""
                    iAllocateID = Request.QueryString("AllocationID")
                    If iAllocateID <> "" Then
                        ddlExistingOrder.SelectedValue = Request.QueryString("AllocationID")
                        ddlExistingOrder_SelectedIndexChanged(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    Public Sub LoadUnit(ByVal DescID As Integer)
        Try
            ddlUnit.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, DescID)
            ddlUnit.DataTextField = "Mas_Desc"
            ddlUnit.DataValueField = "Mas_ID"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub LoadExistingGoodsReturn()
        Try
            ddlExistingOrder.DataSource = objGreturn.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingOrder.DataTextField = "GRM_ReturnNo"
            ddlExistingOrder.DataValueField = "GRM_ID"
            ddlExistingOrder.DataBind()
            ddlExistingOrder.Items.Insert(0, "Existing Return No")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LoadExistGoodsInvoiceNo(ByVal iPONo As Integer)
        Try
            ddlInvoiceNo.Items.Clear()
            ddlInvoiceNo.DataSource = objGreturn.BindInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, iPONo)
            ddlInvoiceNo.DataTextField = "pgm_documentrefno"
            ddlInvoiceNo.DataValueField = "pgm_id"
            ddlInvoiceNo.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistGoodsInvoiceNo")
        End Try
    End Sub
    Private Sub LoadSuppliers()
        Try
            ddlSupplier.DataSource = objPO.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSupplier.DataTextField = "CSM_Name"
            ddlSupplier.DataValueField = "CSM_ID"
            ddlSupplier.DataBind()
            ddlSupplier.Items.Insert(0, "-- Select Supplier Name. --")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindOrder()
        Try
            ddlOrderNo.DataSource = objGreturn.LoadOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlOrderNo.DataTextField = "POM_OrderNo"
            ddlOrderNo.DataValueField = "POM_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "--- Purchase Order No ---")
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
    Public Sub GenerateOrderCode()
        Try
            purchaseReturnNo.Text = objGreturn.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCode")
        End Try
    End Sub
    Public Sub BindTypeOfPurchasedetails()
        Try
            ddlreturntype.Items.Insert(0, "Select Type")
            ddlreturntype.Items.Insert(1, "Excess Qty")
            ddlreturntype.Items.Insert(2, "Rate diffrence")
            ddlreturntype.Items.Insert(3, "Defective Goods")
            ddlreturntype.Items.Insert(4, "Goods Shipped Too Late")
            ddlreturntype.SelectedIndex = 0
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
    Public Sub ClearAll()
        Try
            lblPurchasedQty.Text = "" : txtHistoryID.Text = ""
            chkCategory.Items.Clear() : ddlExistingOrder.SelectedIndex = 0 : ddlOrderNo.SelectedIndex = 0 : txtOrderDate.Text = "" : ddlSupplier.SelectedIndex = 0
            txtSupplierCode.Text = "" : txtReturnDate.Text = "" : txtInvoiceDate.Text = "" : txtReturnRefNo.Text = "" : ddlCommodity.SelectedIndex = 0
            ddlreturntype.SelectedIndex = 0 : ddlInvoiceNo.Items.Clear() : ddlInvoiceNo.Items.Insert(0, "--- Select Invoice NO ---") : txtNarration.Text = ""

            ddlUnit.SelectedIndex = 0 : txtRate.Text = "" : txtRate.CssClass = "aspxcontrolsdisable" : txtQuantity.Text = "" : txtQuantity.CssClass = "aspxcontrolsdisable"
            txtRateAmount.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = "" : txtCharges.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = ""
            txtTotalAmount.Text = ""

            dgPurchaseReturn.DataBind() : GenerateOrderCode() : lblStatus.Text = "" : lblError.Text = ""

            LoadChargeType() : txtShippingRate.Text = "" : GvCharge.DataBind()

            Session("ChargesMaster") = Nothing : dgJEDetails.DataSource = Nothing : dgJEDetails.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll")
        End Try
    End Sub
    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objGreturn.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/Purchase/GoodsReturnMaster.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
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
    Private Sub ddlExistingOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingOrder.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Try
            If ddlExistingOrder.SelectedIndex > 0 Then
                txtOrderDate.Text = "" : txtSupplierCode.Text = ""
                txtReturnDate.Text = "" : txtReturnRefNo.Text = "" : ddlCommodity.SelectedIndex = 0 : ddlreturntype.SelectedIndex = 0
                ddlInvoiceNo.Items.Clear() : ddlInvoiceNo.Items.Insert(0, "--- Select Invoice NO ---") : txtNarration.Text = ""

                ddlUnit.SelectedIndex = 0 : txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
                txtCharges.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtTotalAmount.Text = "" : txtRate.CssClass = "aspxcontrolsdisable" : txtQuantity.CssClass = "aspxcontrolsdisable"

                dgPurchaseReturn.DataBind() : GenerateOrderCode() : GvCharge.DataBind() : lblStatus.Text = "" : lblError.Text = ""

                dtMaster = objGreturn.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("GRM_ReturnNo")) = False Then
                            purchaseReturnNo.Text = dtMaster.Rows(i)("GRM_ReturnNo")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("GRM_OrderID")) = False Then
                            ddlOrderNo.SelectedValue = dtMaster.Rows(i)("GRM_OrderID")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("GRM_OrderDate")) = False Then
                            txtOrderDate.Text = objClsFASGnrl.FormatDtForRDBMS(dtMaster.Rows(i)("GRM_OrderDate"), "D")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("GRM_ReturnRefNo")) = False Then
                            txtReturnRefNo.Text = dtMaster.Rows(i)("GRM_ReturnRefNo")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("GRM_ReturnDate")) = False Then
                            txtReturnDate.Text = objClsFASGnrl.FormatDtForRDBMS(dtMaster.Rows(i)("GRM_ReturnDate"), "D")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("GRM_Supplier")) = False Then
                            ddlSupplier.SelectedValue = dtMaster.Rows(i)("GRM_Supplier")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("GRM_Supplier")) = False Then
                            txtSupplierCode.Text = dtMaster.Rows(i)("GRM_Supplier")
                        End If
                        If IsDBNull(dtMaster.Rows(i)("GRM_DelFlag")) = False Then
                            If dtMaster.Rows(i)("GRM_DelFlag") = "A" Then
                                lblStatus.Text = "Status : Activated"
                            ElseIf dtMaster.Rows(i)("GRM_DelFlag") = "W" Then
                                lblStatus.Text = "Status : Waiting For Approval"
                            End If
                        End If
                    Next
                    LoadExistGoodsInvoiceNo(ddlOrderNo.SelectedValue)
                    LoadChkCategory()
                End If
                dgPurchaseReturn.DataSource = objGreturn.LoadGoodsreturnDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                dgPurchaseReturn.DataBind()

                Dim dtCharge As New DataTable
                dtCharge = objGreturn.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, 0, 0)
                GvCharge.DataSource = dtCharge
                GvCharge.DataBind()
                Session("ChargesMaster") = dtCharge
            Else
                ClearAll()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingOrder_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Try
            If (chkCategory.SelectedValue > 0) Then
                txtRate.CssClass = "aspxcontrolsdisable"
                If ddlExistingOrder.SelectedIndex = 0 Then
                    LoadAllInvoiceDetails(chkCategory.SelectedValue)
                ElseIf ddlExistingOrder.SelectedIndex > 0 Then
                    LoadAllReturnDetails(chkCategory.SelectedValue)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub GetOtherDetails(ByVal iHistoryId As Integer)
        Try
            Dim sGSTRate As String = ""
            If sGSTRate <> "HSN" Then
                txtGSTRate.Text = 0
            Else
                txtGSTRate.Text = objPO.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetOtherDetails")
        End Try
    End Sub
    Private Sub LoadChkCategory()
        Dim dtMaster As New DataTable
        Try
            If ddlInvoiceNo.SelectedValue > 0 Then
                'txtInvoiceDate.Text = objGreturn.getInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedItem.Text, ddlOrderNo.SelectedValue, sSession.YearID)
                txtInvoiceDate.Text = objClsFASGnrl.FormatDtForRDBMS(objGreturn.getInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedItem.Text, ddlOrderNo.SelectedValue, sSession.YearID), "D")
                dtMaster = objGreturn.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("GRM_PR_ID")) = False Then
                            If dtMaster.Rows(i)("GRM_PR_ID") = "1" Then
                                chkCategory.DataSource = objGreturn.LoadPVDescription(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                                chkCategory.DataTextField = "Inv_Code"
                                chkCategory.DataValueField = "Inv_ID"
                                chkCategory.DataBind()
                            Else
                                chkCategory.DataSource = objGreturn.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                                chkCategory.DataTextField = "Inv_Code"
                                chkCategory.DataValueField = "Inv_ID"
                                chkCategory.DataBind()
                            End If
                        End If
                    Next
                End If
            Else
                txtInvoiceDate.Text = ""
                chkCategory.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadChkCategory")
        End Try

    End Sub
    Protected Sub ddlOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrderNo.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Try
            If ddlOrderNo.SelectedIndex > 0 Then

                ddlExistingOrder.SelectedIndex = 0 : txtOrderDate.Text = "" : txtSupplierCode.Text = ""
                txtReturnDate.Text = "" : txtReturnRefNo.Text = "" : ddlCommodity.SelectedIndex = 0 : ddlreturntype.SelectedIndex = 0
                ddlInvoiceNo.Items.Clear() : ddlInvoiceNo.Items.Insert(0, "--- Select Invoice NO ---") : txtNarration.Text = ""

                ddlUnit.SelectedIndex = 0 : txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
                txtCharges.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtTotalAmount.Text = "" : txtRate.CssClass = "aspxcontrolsdisable" : txtQuantity.CssClass = "aspxcontrolsdisable"

                dgPurchaseReturn.DataBind() : GenerateOrderCode() : GvCharge.DataBind() : chkCategory.Items.Clear() : lblStatus.Text = "" : lblError.Text = ""

                LoadOrderDetails(ddlOrderNo.SelectedValue)
                LoadExistGoodsInvoiceNo(ddlOrderNo.SelectedValue)
                txtSupplierCode.Text = objPO.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)

                dtMaster = objGreturn.CheckPurchaseVerification(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    Dim invoiceId As Integer
                    If IsDBNull(dtMaster.Rows(0)("PV_InvoiceID")) = False Then
                        invoiceId = dtMaster.Rows(0)("PV_InvoiceID")
                    End If
                    If ddlInvoiceNo.SelectedValue > 0 Then
                        txtInvoiceDate.Text = objClsFASGnrl.FormatDtForRDBMS(objGreturn.getInvoiceDate(sSession.AccessCode, sSession.AccessCodeID, ddlInvoiceNo.SelectedItem.Text, ddlOrderNo.SelectedValue, sSession.YearID), "D")
                        chkCategory.DataSource = objGreturn.LoadInvoiceDescription(sSession.AccessCode, sSession.AccessCodeID, invoiceId, ddlOrderNo.SelectedValue)
                        chkCategory.DataTextField = "Inv_Code"
                        chkCategory.DataValueField = "Inv_ID"
                        chkCategory.DataBind()
                    Else
                        txtInvoiceDate.Text = ""
                        chkCategory.Items.Clear()
                    End If
                Else
                    lblError.Text = "Purchase Verification is not done for this Purchase Order"
                    Exit Sub
                End If
            Else
                ClearAll()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlOrderNo_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim iMasterID As Integer = 0
        Dim dReturnDate As Date
        Dim dOrderDate As Date
        Dim dInvoiceDate As Date
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim dQuantity As Integer : Dim dHistoryID As Integer : Dim dInvoiceID As Integer
        Dim dtMaster As New DataTable : Dim dtDetails As New DataTable
        Try
            lblError.Text = ""

            dDate = Date.ParseExact(Trim(txtReturnDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(Trim(txtInvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Return Date (" & txtReturnDate.Text & ") should be Greater than or Equal to Invoice Date(" & txtInvoiceDate.Text & ")."
                txtReturnDate.Focus()
                Exit Sub
            End If

            If ddlExistingOrder.SelectedIndex <> 0 Then
                dtMaster = objGreturn.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("GRM_DelFlag")) = False Then
                            If dtMaster.Rows(i)("GRM_DelFlag") = "A" Then
                                lblError.Text = "Already Approved."
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            End If

            dtMaster = objGreturn.CheckPurchaseVerification(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
            If dtMaster.Rows.Count > 0 Then
                If IsDBNull(dtMaster.Rows(0)("PV_InvoiceID")) = False Then
                    dInvoiceID = dtMaster.Rows(0)("PV_InvoiceID")
                End If
            End If

            'dHistoryID = objGreturn.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
            dHistoryID = txtHistoryID.Text

            If ddlExistingOrder.SelectedIndex = 0 Then
                dtMaster = objGreturn.LoadInvoiceDetails(sSession.AccessCode, sSession.AccessCodeID, dInvoiceID, ddlCommodity.SelectedValue, chkCategory.SelectedValue, dHistoryID)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("PID_Quantity")) = False Then
                            dQuantity = dtMaster.Rows(i)("PID_Quantity")
                        End If
                    Next
                End If

                Dim dMasterID As Integer : Dim dGRQuantity As Integer
                dtMaster = objGreturn.CheckMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("GRM_ID")) = False Then
                            dMasterID = dtMaster.Rows(i)("GRM_ID")
                        End If
                        dtDetails = objGreturn.SumQtyDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dMasterID, ddlCommodity.SelectedValue, chkCategory.SelectedValue, dHistoryID)
                        For j = 0 To dtDetails.Rows.Count - 1
                            If IsDBNull(dtDetails.Rows(j)("GRD_Quantity")) = False Then
                                dGRQuantity = dtDetails.Rows(j)("GRD_Quantity")
                            End If
                        Next
                        dQuantity = dQuantity - dGRQuantity

                        If dQuantity < txtQuantity.Text Then
                            lblError.Text = "Goods Return has been made for this invoice No & Approved."
                            Exit Sub
                        End If
                    Next
                End If
            End If

            dReturnDate = Date.ParseExact(Trim(txtReturnDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dOrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dInvoiceDate = Date.ParseExact(Trim(txtInvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If ddlExistingOrder.SelectedIndex = 0 Then
                objGreturn.sGRM_GRID = 0
                objGreturn.sGRM_PRID = 1
                If txtCharges.Text = "" Then
                    txtCharges.Text = "0"
                End If
                hfAmount.Value = Format((txtRateAmount.Text - txtDiscountAmount.Text) + txtCharges.Text, "0.00")
            Else
                objGreturn.sGRM_GRID = 1
                objGreturn.sGRM_PRID = 0
            End If

            objGreturn.sPRM_ReturnNo = purchaseReturnNo.Text
            objGreturn.sPRM_ReturnRefNo = txtReturnRefNo.Text
            objGreturn.sPRM_OrderID = ddlOrderNo.SelectedValue
            objGreturn.sPRM_GINInvID = ddlInvoiceNo.SelectedValue
            objGreturn.sPRM_GINInvNo = ddlInvoiceNo.SelectedItem.Text
            objGreturn.iPRM_Supplier = ddlSupplier.SelectedValue
            objGreturn.iPRD_Commodity = ddlCommodity.SelectedValue
            objGreturn.iPRM_CreatedBy = sSession.UserID
            objGreturn.iPRM_YearID = sSession.YearID
            objGreturn.iPRD_DescriptionID = chkCategory.SelectedValue
            objGreturn.sPRD_IPAddress = sSession.IPAddress
            If txtHistoryID.Text <> "" Then
                objGreturn.iPRD_HistoryID = txtHistoryID.Text
            Else
                objGreturn.iPRD_HistoryID = 0
            End If
            'objGreturn.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)

            objGreturn.sGRD_UnitID = ddlUnit.SelectedValue
            objGreturn.sGRD_Rate = txtRate.Text
            objGreturn.sGRD_Amount = hfAmount.Value
            objGreturn.sGRD_Total = txtRateAmount.Text
            objGreturn.sGRD_Quantity = txtQuantity.Text
            objGreturn.sGRD_Discount = txtDiscount.Text
            objGreturn.sGRD_DiscountAmount = txtDiscountAmount.Text
            objGreturn.sGRD_TotalAmount = txtTotalAmount.Text
            objGreturn.sGRD_ChargesPerItem = txtCharges.Text
            objGreturn.sGRD_GST_ID = hfGSTID.Value
            objGreturn.sGRD_GSTRate = txtGSTRate.Text
            objGreturn.sGRD_GSTAmount = txtGSTAmount.Text
            objGreturn.sGRM_InvoiceSatus = hfPurchaseStatus.Value
            objGreturn.sGRM_State = objGreturn.GetState(sSession.AccessCode, sSession.AccessCodeID, hfsStateCode.Value)

            If hfPurchaseStatus.Value = "Local" Then
                objGreturn.sGRD_SGST = txtGSTRate.Text / 2
                objGreturn.sGRD_CGST = txtGSTRate.Text / 2
                objGreturn.sGRD_SGSTAmount = txtGSTAmount.Text / 2
                objGreturn.sGRD_CGSTAmount = txtGSTAmount.Text / 2
                objGreturn.sGRD_IGST = 0
                objGreturn.sGRD_IGSTAmount = 0
            ElseIf hfPurchaseStatus.Value = "Inter State" Then
                objGreturn.sGRD_SGST = 0
                objGreturn.sGRD_CGST = 0
                objGreturn.sGRD_SGSTAmount = 0
                objGreturn.sGRD_CGSTAmount = 0
                objGreturn.sGRD_IGST = txtGSTRate.Text
                objGreturn.sGRD_IGSTAmount = txtGSTAmount.Text
            End If

            If (ddlreturntype.SelectedIndex > 0) Then
                objGreturn.iPRM_TypeOfReturn = ddlreturntype.SelectedIndex
            Else
                objGreturn.iPRM_TypeOfReturn = 0
            End If
            If (txtNarration.Text = "") Then
                objGreturn.sPRM_Remarks = ""
            Else
                objGreturn.sPRM_Remarks = txtNarration.Text
            End If
            objGreturn.sPRM_Status = "W"
            iMasterID = objGreturn.SaveGoodsReturn(sSession.AccessCode, sSession.AccessCodeID, dReturnDate, dOrderDate, dInvoiceDate, objGreturn)

            SaveCharges(iMasterID)

            Dim sStatus As String = ""
            objGreturn.SaveGoodsReturnDetails(sSession.AccessCode, sSession.AccessCodeID, objGreturn, iMasterID)
            dgPurchaseReturn.DataSource = objGreturn.LoadGoodsreturnDetails(sSession.AccessCode, sSession.AccessCodeID, iMasterID)
            dgPurchaseReturn.DataBind()
            If ddlExistingOrder.SelectedIndex = 0 Then
                LoadExistingGoodsReturn()
                ddlExistingOrder.SelectedValue = iMasterID
            End If
            lblUserMasterDetailsValidationMsg.Text = "Successfully Added"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
            lblError.Text = "Successfully Added"
            lblStatus.Text = "Status : Waiting For Approval"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgRefresh.Click
        Try
            ClearAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgRefresh_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim dtMaster As New DataTable
        Dim dtDetails As New DataTable
        Dim InwrdNo As String = 0
        Dim status As String
        Dim dt As New DataTable, dTable1 As New DataTable

        Dim dtPO As New DataTable
        Dim iZoneID, iRegionID, iAreaID, iBranchID As Integer
        Try
            If (ddlExistingOrder.SelectedIndex > 0) Then
                status = objGreturn.CheckGRStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                If status = "A" Then
                    lblError.Text = "Already Approved"
                    Exit Sub
                End If

                dtMaster = objGreturn.CheckGoodsReturnMaster(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                If dtMaster.Rows.Count = 0 Then
                    lblError.Text = "Please Save All The Details First"
                    Exit Sub
                End If

                dtDetails = objGreturn.CheckNoOfGoodsReturnDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                For i = 0 To dtDetails.Rows.Count - 1
                    If IsDBNull(dtDetails.Rows(i)("GRD_Reason")) = True And IsDBNull(dtDetails.Rows(i)("GRD_Remarks")) = True Then
                        lblError.Text = "Please Save All The Details First"
                        Exit Sub
                    End If
                Next

                dt = objGreturn.GetTransactionDetailsGR(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, 0, ddlExistingOrder.SelectedValue, ddlInvoiceNo.SelectedValue)
                dTable1.Merge(dt)
                InwrdNo = objGreturn.GetGINID(sSession.AccessCode, ddlInvoiceNo.SelectedItem.Text, sSession.YearID, sSession.AccessCodeID, ddlOrderNo.SelectedValue)

                dtMaster = objGreturn.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        If IsDBNull(dtMaster.Rows(i)("GRM_PR_ID")) = False Then
                            If dtMaster.Rows(i)("GRM_PR_ID") = "1" Then
                                dtMaster = objGreturn.CheckReason(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                                For j = 0 To dtMaster.Rows.Count - 1
                                    If IsDBNull(dtMaster.Rows(j)("GRD_Reason")) = False Then
                                        If dtMaster.Rows(i)("GRD_Reason") <> "2" Then
                                            objGreturn.SaveStockLedger(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, sSession.UserID, dTable1, ddlOrderNo.SelectedValue, InwrdNo)
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
                GetPurchasedItemsGrid(ddlExistingOrder.SelectedValue)

                dtPO = objGreturn.GetZone(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                If dtPO.Rows.Count > 0 Then
                    iZoneID = dtPO.Rows(0)("POM_ZoneID")
                    iRegionID = dtPO.Rows(0)("POM_RegionID")
                    iAreaID = dtPO.Rows(0)("POM_AreaID")
                    iBranchID = dtPO.Rows(0)("POM_BranchID")
                End If

                SavePurchaseJE(ddlExistingOrder.SelectedValue, purchaseReturnNo.Text, iZoneID, iRegionID, iAreaID, iBranchID)
                objGreturn.SaveApproveGR(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)

                lblError.Text = "Successfully Approved"
                lblStatus.Text = "Status : Activated"

            Else
                lblError.Text = "Please Select Existing Order"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub dgPurchaseReturn_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchaseReturn.RowDataBound
        Dim imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowDataBound")
        End Try
    End Sub
    Private Sub dgPurchaseReturn_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPurchaseReturn.RowCommand
        Dim dtMaster As New DataTable
        Dim lblDid As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""

            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDid = DirectCast(clickedRow.FindControl("lblItemID"), Label)
            If e.CommandName.Equals("EditRow") Then
                LoadAllReturnDetails(lblDid.Text)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowCommand")
        End Try
    End Sub
    Public Sub LoadAllInvoiceDetails(ByVal GRDescriptionID As Integer)
        Dim dtMaster As New DataTable
        Dim invoiceId As Integer
        Dim historyID As Integer
        Try

            txtQuantity.CssClass = "aspxcontrols"

            ddlreturntype.SelectedIndex = 0 : txtNarration.Text = ""
            chkCategory.SelectedValue = GRDescriptionID
            ddlCommodity.SelectedValue = objGreturn.GetComodityID(sSession.AccessCode, GRDescriptionID)
            LoadUnit(chkCategory.SelectedValue)
            'historyID = objGreturn.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, GRDescriptionID)
            historyID = objGreturn.TakeHistoryID(sSession.AccessCode, sSession.AccessCodeID, GRDescriptionID, ddlInvoiceNo.SelectedValue)
            txtHistoryID.Text = historyID
            hfsStateCode.Value = objGreturn.GetStateCode(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)

            dtMaster = objGreturn.CheckPurchaseVerification(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
            If dtMaster.Rows.Count > 0 Then

                If IsDBNull(dtMaster.Rows(0)("PV_InvoiceID")) = False Then
                    invoiceId = dtMaster.Rows(0)("PV_InvoiceID")
                End If
            End If

            dtMaster = objGreturn.LoadInvoiceDetails(sSession.AccessCode, sSession.AccessCodeID, invoiceId, ddlCommodity.SelectedValue, GRDescriptionID, historyID)
            If dtMaster.Rows.Count > 0 Then
                For i = 0 To dtMaster.Rows.Count - 1
                    If IsDBNull(dtMaster.Rows(i)("PID_UnitID")) = False Then
                        ddlUnit.SelectedValue = dtMaster.Rows(i)("PID_UnitID")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("PID_Rate")) = False Then
                        txtRate.Text = Format(dtMaster.Rows(i)("PID_Rate"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("PID_Discount")) = False Then
                        txtDiscount.Text = Format(dtMaster.Rows(i)("PID_Discount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("PID_GSTRate")) = False Then
                        txtGSTRate.Text = Format(dtMaster.Rows(i)("PID_GSTRate"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("PID_GSTID")) = False Then
                        hfGSTID.Value = dtMaster.Rows(i)("PID_GSTID")
                    End If
                    lblPurchasedQty.Text = dtMaster.Rows(i)("PID_Quantity")
                Next
            End If
            dtMaster = objGreturn.LoadPRMaster(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
            If dtMaster.Rows.Count > 0 Then
                For i = 0 To dtMaster.Rows.Count - 1
                    If IsDBNull(dtMaster.Rows(i)("POM_PurchaseStatus")) = False Then
                        hfPurchaseStatus.Value = dtMaster.Rows(i)("POM_PurchaseStatus")
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllReturnDetails")
        End Try
    End Sub
    Public Sub LoadAllReturnDetails(ByVal GRDescriptionID As Integer)
        Dim dtMaster As New DataTable
        Dim historyID As Integer
        Try
            ddlreturntype.SelectedIndex = 0 : txtNarration.Text = ""
            chkCategory.SelectedValue = GRDescriptionID
            ddlCommodity.SelectedValue = objGreturn.GetComodityID(sSession.AccessCode, GRDescriptionID)
            LoadUnit(chkCategory.SelectedValue)
            historyID = objGreturn.GetHistoryID(sSession.AccessCode, sSession.AccessCodeID, GRDescriptionID)
            hfsStateCode.Value = objGreturn.GetStateCode(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
            dtMaster = objGreturn.LoadGRDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue, ddlCommodity.SelectedValue, GRDescriptionID, historyID)
            If dtMaster.Rows.Count > 0 Then
                For i = 0 To dtMaster.Rows.Count - 1
                    If IsDBNull(dtMaster.Rows(i)("GRD_UnitID")) = False Then
                        ddlUnit.SelectedValue = dtMaster.Rows(i)("GRD_UnitID")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_RateAmount")) = False Then
                        txtRate.Text = Format(dtMaster.Rows(i)("GRD_RateAmount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Discount")) = False Then
                        txtDiscount.Text = Format(dtMaster.Rows(i)("GRD_Discount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_GSTRate")) = False Then
                        txtGSTRate.Text = Format(dtMaster.Rows(i)("GRD_GSTRate"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Quantity")) = False Then
                        txtQuantity.Text = dtMaster.Rows(i)("GRD_Quantity")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Reason")) = False Then
                        ddlreturntype.SelectedIndex = dtMaster.Rows(i)("GRD_Reason")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Remarks")) = False Then
                        txtNarration.Text = dtMaster.Rows(i)("GRD_Remarks")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Total")) = False Then
                        txtRateAmount.Text = Format(dtMaster.Rows(i)("GRD_Total"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Total")) = False Then
                        txtRateAmount.Text = Format(dtMaster.Rows(i)("GRD_Total"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_DiscountAmount")) = False Then
                        txtDiscountAmount.Text = Format(dtMaster.Rows(i)("GRD_DiscountAmount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_ChargesPerItem")) = False Then
                        txtCharges.Text = Format(dtMaster.Rows(i)("GRD_ChargesPerItem"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_GSTAmount")) = False Then
                        txtGSTAmount.Text = Format(dtMaster.Rows(i)("GRD_GSTAmount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_TotalAmount")) = False Then
                        txtTotalAmount.Text = Format(dtMaster.Rows(i)("GRD_TotalAmount"), "0.00")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_GST_ID")) = False Then
                        hfGSTID.Value = dtMaster.Rows(i)("GRD_GST_ID")
                    End If
                    If IsDBNull(dtMaster.Rows(i)("GRD_Amount")) = False Then
                        hfAmount.Value = dtMaster.Rows(i)("GRD_Amount")
                    End If
                Next
            End If

            dtMaster = objGreturn.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, ddlInvoiceNo.SelectedValue, ddlOrderNo.SelectedValue)
            If dtMaster.Rows.Count > 0 Then
                For i = 0 To dtMaster.Rows.Count - 1
                    If IsDBNull(dtMaster.Rows(i)("GRM_PR_ID")) = False Then
                        If dtMaster.Rows(i)("GRM_PR_ID") = "1" Then
                            txtQuantity.CssClass = "aspxcontrols"
                        Else
                            txtQuantity.CssClass = "aspxcontrolsdisable"
                        End If
                    End If
                Next
            End If

            dtMaster = objGreturn.LoadPRMaster(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue)
            If dtMaster.Rows.Count > 0 Then
                For i = 0 To dtMaster.Rows.Count - 1
                    If IsDBNull(dtMaster.Rows(i)("POM_PurchaseStatus")) = False Then
                        hfPurchaseStatus.Value = dtMaster.Rows(i)("POM_PurchaseStatus")
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllReturnDetails")
        End Try

    End Sub
    Public Sub GetPurchasedItemsGrid(ByVal iInvoiceID As Integer)
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

            'iParty = objGreturn.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            iParty = ddlSupplier.SelectedValue

            sTypeOfBill = objDb.SQLGetDescription(sSession.AccessCode, "Select GRM_InvoiceStatus From Goods_Return_Master Where GRM_ID=" & iInvoiceID & " And GRM_CompID=" & sSession.AccessCodeID & " And GRM_YearID=" & sSession.YearID & " ")
            sState = objDb.SQLGetDescription(sSession.AccessCode, "Select GRM_State From Goods_Return_Master Where GRM_ID=" & iInvoiceID & " And GRM_CompID=" & sSession.AccessCodeID & " And GRM_YearID=" & sSession.YearID & " ")

            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & sSession.AccessCodeID & " "
            dtGSTRates = objDb.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)


            If dtGSTRates.Rows.Count > 0 Then
                For k = 0 To dtGSTRates.Rows.Count - 1

                    dt1 = objDb.SQLExecuteDataSet(sSession.AccessCode, "Select * From Goods_Return_Details Where GRD_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And GRD_MasterID=" & iInvoiceID & " And GRD_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For z = 0 To dt1.Rows.Count - 1
                            dTotalAmt = dTotalAmt + dt1.Rows(z)("GRD_Amount")
                            dSGSTAmt = dSGSTAmt + dt1.Rows(z)("GRD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dt1.Rows(z)("GRD_CGSTAmount")
                            dIGSTAmt = dIGSTAmt + dt1.Rows(z)("GRD_IGSTAmount")
                            dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("GRD_TotalAmount"))
                        Next

                        dRow = dt.NewRow 'Item Name
                        dRow("Id") = 0
                        dRow("HeadID") = objGreturn.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Purchase Of Product " & sState)
                        dRow("GLID") = objGreturn.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Purchase Of Product " & sState)
                        If sTypeOfBill = "Local" Then
                            dRow("SubGLID") = objGreturn.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Local GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
                        ElseIf sTypeOfBill = "Inter State" Then
                            dRow("SubGLID") = objGreturn.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Inter State GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
                        End If
                        dRow("PaymentID") = 5
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "Purchase Of Material"

                        sGL = objGreturn.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objGreturn.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
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
                        dRow("HeadID") = objGreturn.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objGreturn.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objGreturn.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input SGST " & SGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 6
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "SGST"

                        sGL = objGreturn.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objGreturn.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
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
                        dRow("HeadID") = objGreturn.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objGreturn.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objGreturn.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input CGST " & CGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 7
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "CGST"

                        sGL = objGreturn.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objGreturn.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
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
                        dRow("HeadID") = objGreturn.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objGreturn.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objGreturn.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input IGST " & IGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 8
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "IGST"

                        sGL = objGreturn.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objGreturn.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
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
                dRow("HeadID") = objGreturn.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
                dRow("GLID") = objGreturn.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
                dRow("SubGLID") = objGreturn.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                dRow("PaymentID") = 9
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Party/Customer"

                sGL = objGreturn.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objGreturn.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
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
    Private Sub SavePurchaseJE(ByVal iPVID As Integer, ByVal sBillNo As String, ByVal iZoneID As Integer, ByVal iRegionID As Integer, ByVal iAreaID As Integer, ByVal iBranchID As Integer)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim objJE As New ClsPurchaseSalesJE
        Try
            objGreturn.iAcc_JE_ID = 0
            objGreturn.sAcc_JE_TransactionNo = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "P")
            objGreturn.iAcc_JE_Party = objGreturn.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlOrderNo.SelectedValue, sSession.YearID)
            objGreturn.iAcc_JE_Location = 0
            objGreturn.iAcc_JE_BillType = 0

            objGreturn.iAcc_JE_InvoiceID = iPVID
            objGreturn.sAcc_JE_BillNo = sBillNo

            objGreturn.dAcc_JE_BillDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            objGreturn.dAcc_JE_BillAmount = txtBillAmount.Text
            objGreturn.iAcc_JE_YearID = sSession.YearID
            objGreturn.sAcc_JE_Status = "W"
            objGreturn.iAcc_JE_CreatedBy = sSession.UserID
            objGreturn.iAcc_JE_CreatedOn = DateTime.Today
            objGreturn.sAcc_JE_Operation = "C"
            objGreturn.sAcc_JE_IPAddress = sSession.IPAddress
            objGreturn.dAcc_JE_BillCreatedDate = DateTime.Today
            objGreturn.sAcc_JE_AdvanceNaration = ""
            objGreturn.sAcc_JE_PaymentNarration = ""
            objGreturn.sAcc_JE_ChequeNo = ""
            objGreturn.sAcc_JE_IFSCCode = ""
            objGreturn.sAcc_JE_BankName = ""
            objGreturn.sAcc_JE_BranchName = ""

            objGreturn.iAcc_JE_UpdatedBy = sSession.UserID
            objGreturn.iAcc_JE_UpdatedOn = DateTime.Today
            objGreturn.iAcc_JE_CompID = sSession.AccessCodeID

            'here.......................

            objGreturn.iAcc_JE_PendingAmount = txtBillAmount.Text
            objGreturn.iAcc_JE_Type = "GR"
            Arr = objGreturn.SavePurchaseJournalMaster(sSession.AccessCode, objGreturn)

            'end.............................
            iTransID = Arr(1)

            For i = 0 To dgJEDetails.Items.Count - 1

                objGreturn.iATD_TrType = 5

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objGreturn.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objGreturn.iATD_ID = 0
                End If

                objGreturn.dATD_TransactionDate = DateTime.Today

                objGreturn.iATD_BillId = iTransID
                objGreturn.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
                'iPaymentType

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objGreturn.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objGreturn.iATD_Head = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objGreturn.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objGreturn.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objGreturn.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objGreturn.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objGreturn.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objGreturn.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objGreturn.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objGreturn.dATD_Credit = 0
                End If

                If objGreturn.dATD_Debit > 0 And objGreturn.dATD_Credit = 0 Then
                    objGreturn.iATD_DbOrCr = 1 'Debit
                ElseIf objGreturn.dATD_Debit = 0 And objGreturn.dATD_Credit > 0 Then
                    objGreturn.iATD_DbOrCr = 2 'Credit
                End If

                objGreturn.iATD_CreatedBy = sSession.UserID
                objGreturn.dATD_CreatedOn = DateTime.Today

                objGreturn.sATD_Status = "A"
                objGreturn.iATD_YearID = sSession.YearID
                objGreturn.sATD_Operation = "C"
                objGreturn.sATD_IPAddress = sSession.IPAddress

                objGreturn.iATD_UpdatedBy = sSession.UserID
                objGreturn.dATD_UpdatedOn = DateTime.Today

                objGreturn.iATD_CompID = sSession.AccessCodeID

                objGreturn.iATD_ZoneID = iZoneID
                objGreturn.iATD_RegionID = iRegionID
                objGreturn.iATD_AreaID = iAreaID
                objGreturn.iATD_BranchID = iBranchID

                objGreturn.dATD_OpenDebit = "0.00"
                objGreturn.dATD_OpenCredit = "0.00"
                objGreturn.dATD_ClosingDebit = "0.00"
                objGreturn.dATD_ClosingCredit = "0.00"
                objGreturn.iATD_SeqReferenceNum = 0


                objGreturn.SaveUpdateTransactionDetails(sSession.AccessCode, objGreturn)

            Next

            lblUserMasterDetailsValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            dgJEDetails.DataSource = objGreturn.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransID, objGreturn.sAcc_JE_TransactionNo)
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SavePurchaseJE")
        End Try
    End Sub

    Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
        Dim dt, dtTable As New DataTable
        Dim dchargeTotal As Double
        Try
            If ddlChargeType.SelectedIndex > 0 Then
                If txtShippingRate.Text <> "" Then
                    dt = AddCharges()
                    dtTable = objGreturn.RemoveChargeDublicate(dt)
                    GvCharge.DataSource = dtTable
                    GvCharge.DataBind()

                    If GvCharge.Items.Count > 0 Then
                        For i = 0 To GvCharge.Items.Count - 1
                            dchargeTotal = dchargeTotal + GvCharge.Items(i).Cells(2).Text
                        Next
                    End If
                    'txtFreight.Text = dchargeTotal

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
    Public Sub SaveCharges(ByVal iMasterID As Integer)
        Dim Arr() As String
        Try
            'Deleting charges Everytime & Saving'
            objGreturn.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1

                    objGreturn.C_POrderID = 0
                    objGreturn.C_PGinID = 0
                    objGreturn.C_PInvoiceDocRef = 0
                    objGreturn.C_OrderType = ""
                    objGreturn.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objGreturn.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objGreturn.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objGreturn.C_PSType = "P"
                    objGreturn.C_DelFlag = "W"
                    objGreturn.C_Status = "C"
                    objGreturn.C_CompID = sSession.AccessCodeID
                    objGreturn.C_YearID = sSession.YearID
                    objGreturn.C_CreatedBy = sSession.UserID
                    objGreturn.C_CreatedOn = System.DateTime.Now
                    objGreturn.C_Operation = "C"
                    objGreturn.C_IPAddress = sSession.IPAddress
                    objGreturn.C_GoodsReturnID = iMasterID

                    Arr = objGreturn.SaveCharges(sSession.AccessCode, objGreturn)
                Next
            End If
            'Charges Saving'
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveCharges")
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
            'txtFreight.Text = dchargeTotal
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub
    Private Sub ddlreturntype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlreturntype.SelectedIndexChanged
        If ddlreturntype.SelectedIndex = 2 Then
            txtRate.CssClass = "aspxcontrols"
        Else
            txtRate.CssClass = "aspxcontrolsdisable"
        End If
    End Sub
End Class
