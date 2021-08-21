Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports System.IO
Imports DatabaseLayer
Partial Class Purchase_PurchaseInvoiceEntry
    Inherits System.Web.UI.Page
    Dim objPO As New clsPurchaseOrder
    Dim objGIN As New ClsGoodsInward
    Dim objRegistry As New clsPurchaseRegistry
    Dim sSession As New AllSession
    Dim objFasGnrl As New clsFASGeneral
    Dim objGnrlFnctn As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails
    Dim objDb As New DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Dim objclsModulePermission As New clsModulePermission
    Private Shared sFormName As String = "Purchase/PurchaseRegistry"
    Private dtTab As New DataTable
    Private Shared sIKBBackStatus As String
    Private Shared sCurrentMonthID As Integer = 0
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Save24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgRefresh.ImageUrl = "~/Images/Reresh24.png"
        'imgbtnNew.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub dgPurchaseRegistry_PreRender(sender As Object, e As EventArgs) Handles dgPurchaseRegistry.PreRender
        Dim dt As New DataTable
        Try
            If dgPurchaseRegistry.Rows.Count > 0 Then
                dgPurchaseRegistry.UseAccessibleHeader = True
                dgPurchaseRegistry.HeaderRow.TableSection = TableRowSection.TableHeader
                dgPurchaseRegistry.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchaseRegistry_PreRender")
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Dim iDefaultBranch As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PR")
                imgRefresh.Visible = False : imgbtnAdd.Visible = False : imgbtnWaiting.Visible = False
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
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnWaiting.Visible = True
                    End If
                End If
                'imgbtnAdd.Visible = False
                'imgbtnWaiting.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FASPIE", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Activate/De-Activate,") = True Then
                '        imgbtnWaiting.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnAdd.Visible = True
                '    End If
                'End If

                'txtStartDate.Text = objGnrlFnctn.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtEndDate.Text = objGnrlFnctn.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                GenerateOrderCodeAnddate()
                LoadRegistryNo()

                iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                LoadOrderNo(iDefaultBranch)
                LoadSuppliers()

                Dim iAID As String = "" : Dim iDashBoard As String = ""
                iDashBoard = Request.QueryString("sStrID")
                If iDashBoard = "1" Then
                    iAID = objFasGnrl.DecryptQueryString(Request.QueryString("AID"))
                    If iAID <> "AddNew" Then
                        'ExistingAllocationNo(0)
                        ddlExistRegisrtry.SelectedValue = objFasGnrl.DecryptQueryString(Request.QueryString("AID"))
                        ddlExistRegisrtry_SelectedIndexChanged(sender, e)
                    Else

                    End If
                ElseIf iDashBoard = "0" Then
                End If
                'If iDashBoard = "" Then
                '    Dim iAllocateID As String = ""
                '    iAllocateID = objFasGnrl.DecryptQueryString(Request.QueryString("AllocationID"))
                '    If iAllocateID <> "" Then
                '        '      lblReAllocateID.Text = iAllocateID
                '        ddlExistRegisrtry.SelectedValue = objFasGnrl.DecryptQueryString(Request.QueryString("AllocationID"))
                '        ddlExistRegisrtry_SelectedIndexChanged(sender, e)
                '    End If
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub GenerateOrderCodeAnddate()
        Try
            txtPrchseRegNo.Text = objRegistry.GeneratePurchaseRegCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCodeAnddate")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objFasGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/Purchase/PurchaseInvoiceMaster.aspx?"), False)
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
            ddlSupplier.Items.Insert(0, "--- Select Supplier ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadOrderNo(ByVal iBranch As Integer)
        Try
            ddlOrderNo.DataSource = objGIN.LoadOurRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBranch)
            ddlOrderNo.DataTextField = "POM_OrderNo"
            ddlOrderNo.DataValueField = "POM_ID"
            ddlOrderNo.DataBind()
            ddlOrderNo.Items.Insert(0, "--- Select Order No ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadRegistryNo()
        Try

            ddlExistRegisrtry.DataSource = objRegistry.LoadRegistryNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistRegisrtry.DataTextField = "PRM_RegistryNo"
            ddlExistRegisrtry.DataValueField = "PRM_ID" '
            ddlExistRegisrtry.DataBind()
            ddlExistRegisrtry.Items.Insert(0, "-- Select Purchase Registry No. ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExistRegisrtry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistRegisrtry.SelectedIndexChanged
        Try
            Clear()
            ddlExistRegisrtry.DataSource = Nothing
            ddlExistRegisrtry.DataBind()
            lblError.Text = ""
            txtDocRefNo.Visible = True
            ddlDocNo.Enabled = True
            If ddlExistRegisrtry.SelectedIndex > 0 Then
                ' LoadDocRefNo(ddlOrderNo.SelectedValue)
                'LoadOrderDetails(ddlOrderNo.SelectedValue)
                ViewEnable()
                LoadRegistrydDetails()
                dgPurchaseRegistry.DataSource = objRegistry.LoadPurchaseRegistry(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistRegisrtry.SelectedValue)
                dgPurchaseRegistry.DataBind()
                If (dgPurchaseRegistry.Rows.Count > 0) Then
                    dgPurchaseRegistry.Visible = True
                Else
                    dgPurchaseRegistry.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistRegisrtry_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub Clear()
        Try
            lblStatus.Text = ""
            ' ddlNBrand.SelectedIndex = 0
            '   ddlNItems.Items.Clear() : ddlNUnit.Items.Clear() : ddlRate.Items.Clear()
            '   txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""  'txtRequiredDate.Text = ""
            '   txtCSTAmount.Text = "" : ddlVat.Items.Clear() : ddlCst.Items.Clear() : txtVatAmount.Text = ""
            '  txtExcise.Text = "" : txtTotalAmount.Text = "" : txtAccepted.Text = "" : txtDiscount.Text = ""
            txtDocRefNo.Text = "" : txtInvoiceDate.Text = "" : txtEsugam.Text = ""
            ddlSupplier.SelectedIndex = 0 : txtDcNo.Text = "" ': ddlExistingDocRef.Items.Clear()
            txtDocRefNo.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Clear")
        End Try
    End Sub

    Public Sub ViewEnable()
        Try
            txtEsugam.Enabled = True : txtInvoiceDate.Enabled = True : txtPrchseRegNo.Enabled = True : ddlSupplier.Enabled = True
            txtDcNo.Enabled = True : txtDocRefNo.Enabled = True
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub ViewDisable()
        Try
            txtEsugam.Enabled = False : txtInvoiceDate.Enabled = False : txtPrchseRegNo.Enabled = False : ddlSupplier.Enabled = False
            txtDcNo.Enabled = False : txtDocRefNo.Enabled = False

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LoadRegistrydDetails()
        Dim dtable As New DataTable
        Try
            dtable = objRegistry.ExistingRegistryDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistRegisrtry.SelectedValue)
            If (dtable.Rows.Count > 0) Then
                For i = 0 To dtable.Rows.Count - 1
                    txtDocRefNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PRM_DocumentRefNo"))
                    txtInvoiceDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("PRM_InvoiceDate"), "D")
                    txtEsugam.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PRM_ESugamNo"))
                    txtPrchseRegNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PRM_Registry_Number"))
                    txtOrderDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("PRM_OrderDate"), "D")
                    ddlOrderNo.SelectedValue = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PRM_OrderID"))
                    txtDcNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PRM_DcNo"))
                    ddlSupplier.SelectedValue = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PRM_Supplier"))
                    lblStatus.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PRM_Status"))
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadRegistrydDetails")
        End Try
    End Sub
    Private Sub LoadOrderDetails(ByVal iPONo As Integer)
        Dim dtable As New DataTable
        Try
            dtable = objGIN.OrderDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPONo)
            If (dtable.Rows.Count > 0) Then
                For i = 0 To dtable.Rows.Count - 1
                    txtOrderDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("POM_OrderDate"), "D")
                    ddlSupplier.SelectedValue = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("POM_Supplier"))

                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOrderDetails")
        End Try
    End Sub
    Private Sub LoadDocRefNo(ByVal iPONo As Integer)
        Try
            ddlDocNo.DataSource = objRegistry.LoadExistDocRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPONo)
            ddlDocNo.DataTextField = "PGM_DocumentRefNo"
            ddlDocNo.DataValueField = "PGM_ID"
            ddlDocNo.DataBind()
            ddlDocNo.Items.Insert(0, New ListItem("--- Select Document Ref No ---", "0"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOrderNo.SelectedIndexChanged
        Try
            Clear()
            dgPurchaseRegistry.DataSource = Nothing
            dgPurchaseRegistry.DataBind()
            lblError.Text = ""
            lblStatus.Text = ""
            txtDocRefNo.Visible = True
            ddlDocNo.Enabled = True
            If ddlOrderNo.SelectedIndex > 0 Then
                ViewEnable()
                LoadDocRefNo(ddlOrderNo.SelectedValue)
                LoadOrderDetails(ddlOrderNo.SelectedValue)
                GenerateOrderCodeAnddate()
                dtTab = objRegistry.GetPODetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                Session("UpdateTab") = dtTab
                dgPurchaseRegistry.DataSource = dtTab
                dgPurchaseRegistry.DataBind()
                If (dgPurchaseRegistry.Rows.Count > 0) Then
                    dgPurchaseRegistry.Visible = True
                Else
                    dgPurchaseRegistry.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlOrderNo_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim Arr As String
        Dim iMasterID As Integer
        Dim sCurrentMonth As String = "", sYear As String = "", sStaus As String = "", sStatus As String = "", Check As String
        Dim ddlUnit As New DropDownList
        Dim lblMRP As New TextBox
        Dim lblPending As New Label
        Dim lblOrderedQuantity As New Label, lblComodityId As New Label, lblDescription As Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label, UnitsID As New Label
        Dim lblReceivedQuantity As New TextBox
        Dim txtBatchNumber As New TextBox
        Dim lblAcceptedQuantity As New TextBox
        Dim lblRejectedQuantity As New TextBox
        Dim lblRejectedQuantityExcess As New TextBox
        Dim lblRemarks As New TextBox
        Dim txtManufactureDate As New TextBox
        Dim txtExpireDate As New TextBox
        Dim lblBatchNo As New TextBox
        Dim lblRate As New TextBox
        'Dim row As GridViewRow
        Dim dDate, dSDate As Date : Dim m As Integer
        Try
            If txtInvoiceDate.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Invoice Date (" & txtInvoiceDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Invoice Date (" & txtInvoiceDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                    txtInvoiceDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If (txtDocRefNo.Text = "") Then
                lblError.Text = "Enter Document reference"
                Exit Sub
            Else
                If (objRegistry.CheckVerifiedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, txtDocRefNo.Text)) Then
                    lblError.Text = "Already verified "
                    Exit Sub
                End If
            End If
            Check = objRegistry.CheckRegistredOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, txtDocRefNo.Text)
            If (Check = True) Then
                lblError.Text = "Document reference no already exist"
            Else
                If dgPurchaseRegistry.Rows.Count > 0 Then
                    sCurrentMonthID = objRegistry.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnctn.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnctn.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    objRegistry.dPRM_OrderDate = Date.ParseExact(Trim("28/02/2016"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objRegistry.sPRM_DocumentRefNo = objFasGnrl.SafeSQL(txtDocRefNo.Text)
                    objRegistry.iPRM_CreatedBy = sSession.UserID
                    objRegistry.sPRM_ESugamNo = objFasGnrl.SafeSQL(txtEsugam.Text)
                    objRegistry.iPRM_Supplier = objGIN.GetSupplierName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue)
                    If (ddlDocNo.SelectedIndex > 0) Then
                        objRegistry.sPRM_DocumentRefNo = ddlDocNo.SelectedItem.Text
                    Else
                        objRegistry.sPRM_DocumentRefNo = objFasGnrl.SafeSQL(txtDocRefNo.Text)
                    End If
                    objRegistry.sPRM_DcNo = txtDcNo.Text
                    objRegistry.iPRM_ID = 0
                    objRegistry.iPRM_CompID = sSession.AccessCodeID
                    objRegistry.iPRM_CreatedBy = sSession.UserID
                    objRegistry.dPRM_CreatedOn = DateTime.Today
                    objRegistry.iPRM_Status = "W"
                    objRegistry.sPRM_DelFlag = "W"
                    objRegistry.iPRM_YearID = sSession.YearID
                    ' objReg.prm_o = "C"
                    objRegistry.sPRM_IPAddress = sSession.IPAddress
                    objRegistry.sPRM_RegistryNo = objFasGnrl.SafeSQL(txtPrchseRegNo.Text)
                    '  ObjGoods.PGM_ModeOfShiping = ddlModeOfShipping.SelectedItem.Text 'lblMShiping.Text
                    objRegistry.dPRM_InvoiceDate = Date.ParseExact(Trim(txtInvoiceDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objRegistry.sPRM_ESugamNo = objFasGnrl.SafeSQL(txtEsugam.Text)
                    objRegistry.iPRM_OrderNo = ddlOrderNo.SelectedValue
                    iMasterID = objRegistry.PurchaseRegistryMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objRegistry)
                    ' iMasterID = Arr(0)
                    For i = 0 To dgPurchaseRegistry.Rows.Count - 1
                        lblReceivedQuantity = dgPurchaseRegistry.Rows(i).FindControl("txtReceivedQty")
                        If (lblReceivedQuantity.Text = "") Then
                            lblReceivedQuantity.Text = 0
                        End If

                        If (Convert.ToDecimal(lblReceivedQuantity.Text) > 0) Then
                            objRegistry.iPRD_MasterID = iMasterID
                            lblMRP = dgPurchaseRegistry.Rows(i).FindControl("txtMrp")
                            If lblMRP.Text <> "" Then
                                objRegistry.dPRD_MRP = lblMRP.Text
                            Else
                                objRegistry.dPRD_MRP = 0
                            End If

                            lblDescription = dgPurchaseRegistry.Rows(i).FindControl("lblDescription")

                            lblUnitId = dgPurchaseRegistry.Rows(i).FindControl("lblUnitId")

                            If lblUnitId.Text <> "" Then
                                objRegistry.iPRD_UnitID = lblUnitId.Text ' objGIN.GetUnitID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescription.Text)
                            Else
                                objRegistry.iPRD_UnitID = 0
                            End If

                            lblHistoryID = dgPurchaseRegistry.Rows(i).FindControl("lblHistoryID")
                            If lblHistoryID.Text <> "" Then
                                objRegistry.iPRD_HistoryID = lblHistoryID.Text
                            Else
                                objRegistry.iPRD_HistoryID = 0
                            End If
                            lblItemId = dgPurchaseRegistry.Rows(i).FindControl("lblItemId")
                            If lblItemId.Text <> "" Then
                                objRegistry.iPRD_DescID = lblItemId.Text
                            Else
                                objRegistry.iPRD_DescID = 0
                            End If

                            lblComodityId = dgPurchaseRegistry.Rows(i).FindControl("lblComodityId")
                            If lblComodityId.Text <> "" Then
                                objRegistry.iPRD_Commodity = lblComodityId.Text 'objGIN.GetCommodityID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblComodityId.Text)
                            Else
                                objRegistry.iPRD_Commodity = 0
                            End If
                            lblOrderedQuantity = dgPurchaseRegistry.Rows(i).FindControl("lblOrderQty")
                            If lblOrderedQuantity.Text <> "" Then
                                objRegistry.dPRD_OrderQuntity = lblOrderedQuantity.Text
                            Else
                                objRegistry.dPRD_OrderQuntity = 0
                            End If

                            If lblReceivedQuantity.Text <> "" Then
                                objRegistry.dPRD_RecievedQnt = lblReceivedQuantity.Text
                            Else
                                objRegistry.dPRD_RecievedQnt = 0
                            End If
                            If lblReceivedQuantity.Text <> "" Then
                                objRegistry.dPRD_RecievedQnt = lblReceivedQuantity.Text
                            Else
                                objRegistry.dPRD_RecievedQnt = 0
                            End If

                            lblAcceptedQuantity = dgPurchaseRegistry.Rows(i).FindControl("txtAcceptedQty")
                            If lblAcceptedQuantity.Text <> "" Then
                                objRegistry.dPRD_Accepted = lblAcceptedQuantity.Text
                            Else
                                objRegistry.dPRD_Accepted = 0
                            End If
                            lblRejectedQuantity = dgPurchaseRegistry.Rows(i).FindControl("txtRejected")
                            If lblRejectedQuantity.Text <> "" Then
                                objRegistry.dPRD_Rejected = lblRejectedQuantity.Text
                            Else
                                objRegistry.dPRD_Rejected = 0
                            End If
                            lblRejectedQuantityExcess = dgPurchaseRegistry.Rows(i).FindControl("txtExcessQty")
                            If lblRejectedQuantityExcess.Text <> "" Then
                                'objRegistry.dPRD_Rejected = lblRejectedQuantityExcess.Text
                                objRegistry.sPRD_DelFlag = "E"
                            Else
                                objRegistry.dPRD_Excise = 0
                                objRegistry.sPRD_DelFlag = "A"
                            End If
                            txtManufactureDate = dgPurchaseRegistry.Rows(i).FindControl("txtMdate")
                            If txtManufactureDate.Text <> "" Then
                                objRegistry.dPRD_ManufactureDate = Date.ParseExact(Trim(txtManufactureDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Else
                                objRegistry.dPRD_ExpireDate = "01/01/1900"
                            End If
                            txtExpireDate = dgPurchaseRegistry.Rows(i).FindControl("txtEdate")
                            If txtExpireDate.Text <> "" Then
                                objRegistry.dPRD_ExpireDate = Date.ParseExact(Trim(txtExpireDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Else
                                objRegistry.dPRD_ExpireDate = "01/01/1900"
                            End If
                            lblPending = dgPurchaseRegistry.Rows(i).FindControl("lblPending")
                            If lblPending.Text <> "" Then
                                objRegistry.dPRD_PendIng = lblPending.Text
                            End If
                            objRegistry.iPRD_CompID = sSession.AccessCodeID
                            objRegistry.sPRD_Status = "W"
                            '  objReg.prd_ = "C"
                            objRegistry.sPRD_IPAddress = sSession.IPAddress
                            txtBatchNumber = dgPurchaseRegistry.Rows(i).FindControl("txtBatchNumber")
                            If (txtBatchNumber.Text <> "") Then
                                objRegistry.sPRD_BatchNo = txtBatchNumber.Text
                            Else
                                objRegistry.sPRD_BatchNo = " "
                            End If
                            objRegistry.iPRD_OrderNo = ddlOrderNo.SelectedValue
                            Arr = objRegistry.PurchaseRegistryDetails(sSession.AccessCode, sSession.AccessCodeID, objRegistry)
                        End If
                    Next
                    If Arr = "2" Then
                        lblError.Text = "Successfully Updated"
                    ElseIf Arr = "3" Then
                        lblStatus.Text = "Successfully Saved"
                    End If
                    LoadExistingRegisterNo()
                    ddlExistRegisrtry.SelectedValue = iMasterID
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub LoadExistingRegisterNo()
        Try
            ddlExistRegisrtry.DataSource = objRegistry.LoadRegistryNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistRegisrtry.DataTextField = "PRM_RegistryNo"
            ddlExistRegisrtry.DataValueField = "PRM_ID"
            ddlExistRegisrtry.DataBind()
            ddlExistRegisrtry.Items.Insert(0, "--- Existing Inward No ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlDocNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDocNo.SelectedIndexChanged
        Dim dt As New DataTable
        Dim RegNo As String = ""
        Try
            Clear()
            If (ddlDocNo.SelectedIndex > 0) Then
                txtDocRefNo.Text = objFasGnrl.ReplaceSafeSQL(ddlDocNo.SelectedItem.Text)
                If (objRegistry.CheckRegExistOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDocNo.SelectedItem.Text, ddlOrderNo.SelectedValue)) Then
                    RegNo = objRegistry.ExistingRegNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDocNo.SelectedItem.Text, ddlOrderNo.SelectedValue)
                    lblStatus.Text = "Invoice No already Created:-" & txtDocRefNo.Text & " System Generated Number is:-" & RegNo & ""
                    dgPurchaseRegistry.DataSource = Nothing
                    dgPurchaseRegistry.DataBind()
                    txtDocRefNo.Text = ""
                    ddlDocNo.SelectedIndex = 0
                    ViewDisable()
                Else
                    loadExistingInwardDetails()
                    dt = objRegistry.GetPODetailsFromGin(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlDocNo.SelectedValue)
                    dgPurchaseRegistry.DataSource = dt
                    dgPurchaseRegistry.DataBind()
                End If
            Else
                txtDocRefNo.Visible = True
            End If
            If (dgPurchaseRegistry.Rows.Count > 0) Then
                dgPurchaseRegistry.Visible = True
            Else
                dgPurchaseRegistry.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDocNo_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Try

            If (ddlExistRegisrtry.SelectedIndex > 0) Then
                objRegistry.AcceptMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlExistRegisrtry.SelectedValue)
                MakeTransactioPI()
                MakeTransactionPR()
                lblStatus.Text = "Sucessfully Approved"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Public Sub loadExistingInwardDetails()
        Dim dtable As New DataTable
        Try
            dtable = objRegistry.ExistingInwardDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlDocNo.SelectedItem.Text, ddlOrderNo.SelectedValue)
            If (dtable.Rows.Count > 0) Then
                For i = 0 To dtable.Rows.Count - 1
                    txtDocRefNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_DocumentRefNo"))
                    txtInvoiceDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("PGM_InvoiceDate"), "D")
                    txtEsugam.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_ESugamNo"))
                    txtOrderDate.Text = objFasGnrl.FormatDtForRDBMS(dtable.Rows(i)("PGM_OrderDate"), "D")
                    ddlOrderNo.SelectedValue = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_OrderID"))
                    txtDcNo.Text = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_DcNo"))
                    ddlSupplier.SelectedValue = objFasGnrl.ReplaceSafeSQL(dtable.Rows(i)("PGM_Supplier"))
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingInwardDetails")
        End Try
    End Sub
    Public Sub MakeTransactioPI()
        Dim ObjGoods As New ClsGoodsInward
        Dim Arr() As String
        Dim sCurrentMonth As String = "", sYear As String = "", sCheckAcceptedQTY As String = "", sStr As String = ""
        Dim ddlUnit As New DropDownList
        Dim lblAcceptedQuantity As New TextBox
        Dim lblOrderedQuentity As New Label
        Dim lblReceivedQuentity As New TextBox
        Dim lblExcessQuentity As New TextBox
        Dim lblRemarks As New TextBox
        Dim lblRate As New TextBox
        Dim lblMRP As New TextBox
        Dim lblComodityId As New Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label
        Dim j As Integer
        Dim ssql As String = ""

        Try
            For j = 0 To dgPurchaseRegistry.Rows.Count - 1
                objRegistry.iPRM_OrderNo = ddlOrderNo.SelectedValue
                lblReceivedQuentity = dgPurchaseRegistry.Rows(j).FindControl("txtReceivedQty")
                If (lblReceivedQuentity.Text = "") Then
                    lblReceivedQuentity.Text = 0
                End If
                If (Convert.ToDecimal(lblReceivedQuentity.Text) > 0) Then
                    sCurrentMonthID = objGIN.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnctn.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnctn.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                    lblOrderedQuentity = dgPurchaseRegistry.Rows(j).FindControl("lblOrderQty")

                    lblMRP = dgPurchaseRegistry.Rows(j).FindControl("txtMrp")
                    lblOrderedQuentity = dgPurchaseRegistry.Rows(j).FindControl("lblOrderQty")
                    If lblOrderedQuentity.Text <> "" Then
                        objRegistry.dPRD_OrderQuntity = lblOrderedQuentity.Text
                    Else
                        objRegistry.dPRD_OrderQuntity = 0
                    End If
                    lblReceivedQuentity = dgPurchaseRegistry.Rows(j).FindControl("txtReceivedQty")
                    If lblReceivedQuentity.Text <> "" Then
                        objRegistry.dPRD_RecievedQnt = lblReceivedQuentity.Text
                    Else
                        objRegistry.dPRD_RecievedQnt = 0
                    End If
                    lblAcceptedQuantity = dgPurchaseRegistry.Rows(j).FindControl("txtAcceptedQty")
                    If lblAcceptedQuantity.Text <> "" Then
                        objRegistry.dPRD_Accepted = lblAcceptedQuantity.Text
                    Else
                        objRegistry.dPRD_Accepted = 0
                    End If
                    lblExcessQuentity = dgPurchaseRegistry.Rows(j).FindControl("txtExcessQty")
                    If lblExcessQuentity.Text <> "" Then
                        objRegistry.dPRD_ExcessQty = lblExcessQuentity.Text
                        objRegistry.sPRD_Status = "W"
                    Else
                        objRegistry.dPRD_ExcessQty = 0
                        objRegistry.sPRD_Status = "A"
                    End If
                    objRegistry.sPRM_RegistryNo = ddlExistRegisrtry.SelectedValue

                    lblComodityId = dgPurchaseRegistry.Rows(j).FindControl("lblComodityId")
                    'If lblComodityId.Text <> "" Then
                    objRegistry.iPRD_Commodity = lblComodityId.Text
                    'Else
                    '    objRegistry.iPRD_Commodity = 0
                    'End If
                    objRegistry.iPRD_CompID = sSession.AccessCodeID
                    lblHistoryID = dgPurchaseRegistry.Rows(j).FindControl("lblHistoryID")
                    If lblHistoryID.Text <> "" Then
                        objRegistry.iPRD_HistoryID = lblHistoryID.Text
                    Else
                        objRegistry.iPRD_HistoryID = 0
                    End If

                    objRegistry.iPRD_OrderNo = ddlOrderNo.SelectedValue
                    lblUnitId = dgPurchaseRegistry.Rows(j).FindControl("lblUnitId")
                    If lblUnitId.Text <> "" Then
                        objRegistry.iPRD_UnitID = lblUnitId.Text
                    Else
                        objRegistry.iPRD_UnitID = 0
                    End If

                    lblItemId = dgPurchaseRegistry.Rows(j).FindControl("lblItemId")
                    If lblItemId.Text <> "" Then
                        objRegistry.iPRD_DescID = lblItemId.Text
                    Else
                        objRegistry.iPRD_DescID = 0
                    End If

                    lblMRP = dgPurchaseRegistry.Rows(j).FindControl("txtMrp")
                    If lblMRP.Text <> "" Then
                        objRegistry.dPRD_MRP = lblMRP.Text
                    Else
                        objRegistry.dPRD_MRP = 0
                    End If

                    If lblMRP.Text <> "" Then
                        objRegistry.dPRD_MRP = lblMRP.Text
                    Else
                        objRegistry.dPRD_MRP = 0
                    End If

                    If txtDocRefNo.Text <> "" Then
                        objRegistry.sPRM_DocumentRefNo = txtDocRefNo.Text
                    Else
                        objRegistry.sPRM_DocumentRefNo = 0
                    End If

                    Arr = objRegistry.SaveTransactionInvoiceDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objRegistry)
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
    Public Sub MakeTransactionPR()
        Dim ObjGoods As New ClsGoodsInward
        Dim Arr() As String
        Dim sCurrentMonth As String = "", sYear As String = "", sCheckAcceptedQTY As String = "", sStr As String = "", ssql As String = ""
        Dim j As Integer
        Dim ddlUnit As New DropDownList
        Dim lblAcceptedQuantity As New TextBox
        Dim lblOrderedQuentity As New Label
        Dim lblReceivedQuentity As New TextBox
        Dim lblRejectedQty As New TextBox
        Dim lblComodityId As New Label, lblItemId As New Label, lblHistoryID As New Label, lblUnitId As New Label
        Dim lblRemarks As New TextBox
        Dim lblRate As New TextBox
        Dim lblMRP As New TextBox
        Try
            For j = 0 To dgPurchaseRegistry.Rows.Count - 1
                lblReceivedQuentity = dgPurchaseRegistry.Rows(j).FindControl("txtReceivedQty")
                If (lblReceivedQuentity.Text = "") Then
                    lblReceivedQuentity.Text = 0
                End If

                If (Convert.ToDecimal(lblReceivedQuentity.Text) > 0) Then
                    sCurrentMonthID = objGIN.GetCurrentMonthID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sCurrentMonth = objGnrlFnctn.GetMonthNameFromMothID(sCurrentMonthID)
                    sYear = objGnrlFnctn.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    lblOrderedQuentity = dgPurchaseRegistry.Rows(j).FindControl("lblOrderQty")
                    'lblReceivedQuentity = dgInward.Rows(j).FindControl("txtReceivedQty")
                    lblRejectedQty = dgPurchaseRegistry.Rows(j).FindControl("txtRejected")
                    lblMRP = dgPurchaseRegistry.Rows(j).FindControl("txtMrp")
                    lblOrderedQuentity = dgPurchaseRegistry.Rows(j).FindControl("lblOrderQty")
                    If lblOrderedQuentity.Text <> "" Then
                        objRegistry.dPRD_OrderQuntity = lblOrderedQuentity.Text
                    Else
                        objRegistry.dPRD_OrderQuntity = 0
                    End If
                    lblReceivedQuentity = dgPurchaseRegistry.Rows(j).FindControl("txtReceivedQty")
                    If lblReceivedQuentity.Text <> "" Then
                        objRegistry.dPRD_RecievedQnt = lblReceivedQuentity.Text
                    Else
                        objRegistry.dPRD_RecievedQnt = 0
                    End If
                    lblAcceptedQuantity = dgPurchaseRegistry.Rows(j).FindControl("txtAcceptedQty")
                    If lblAcceptedQuantity.Text <> "" Then
                        objRegistry.dPRD_Accepted = lblAcceptedQuantity.Text
                    Else
                        objRegistry.dPRD_Accepted = 0
                    End If
                    If lblRejectedQty.Text <> "" Then
                        objRegistry.dPRD_Rejected = lblRejectedQty.Text
                    Else
                        objRegistry.dPRD_Rejected = 0
                    End If
                    lblComodityId = dgPurchaseRegistry.Rows(j).FindControl("lblComodityId")
                    If lblComodityId.Text <> "" Then
                        objRegistry.iPRD_Commodity = lblComodityId.Text
                    Else
                        objRegistry.iPRD_Commodity = 0
                    End If
                    objRegistry.iPRD_CompID = sSession.AccessCodeID
                    lblHistoryID = dgPurchaseRegistry.Rows(j).FindControl("lblHistoryID")
                    If lblHistoryID.Text <> "" Then
                        objRegistry.iPRD_HistoryID = lblHistoryID.Text
                    Else
                        objRegistry.iPRD_HistoryID = 0
                    End If
                    objRegistry.iPRD_OrderNo = ddlOrderNo.SelectedValue

                    lblUnitId = dgPurchaseRegistry.Rows(j).FindControl("lblUnitId")
                    If lblUnitId.Text <> "" Then
                        objRegistry.iPRD_UnitID = lblUnitId.Text
                    Else
                        objRegistry.iPRD_UnitID = 0
                    End If
                    lblItemId = dgPurchaseRegistry.Rows(j).FindControl("lblItemId")
                    If lblItemId.Text <> "" Then
                        objRegistry.iPRD_DescID = lblItemId.Text
                    Else
                        objRegistry.iPRD_DescID = 0
                    End If
                    lblMRP = dgPurchaseRegistry.Rows(j).FindControl("txtMrp")
                    If lblMRP.Text <> "" Then
                        objRegistry.dPRD_MRP = lblMRP.Text
                    Else
                        objRegistry.dPRD_MRP = 0
                    End If

                    lblUnitId = dgPurchaseRegistry.Rows(j).FindControl("lblUnitId")
                    If lblUnitId.Text <> "" Then
                        objRegistry.iPRD_UnitID = lblUnitId.Text
                    Else
                        objRegistry.iPRD_UnitID = 0
                    End If

                    If txtDocRefNo.Text <> "" Then
                        objRegistry.sPRM_DocumentRefNo = txtDocRefNo.Text
                    Else
                        objRegistry.sPRM_DocumentRefNo = 0
                    End If

                    objRegistry.sPRD_Status = "w"
                    objRegistry.iPRD_OrderNo = ddlOrderNo.SelectedValue
                    objRegistry.sPRM_RegistryNo = ddlExistRegisrtry.SelectedValue
                    Arr = objRegistry.SaveTransactionReturnsDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objRegistry)
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MakeTransactionPR")
        End Try
    End Sub
    Protected Sub dgPurchaseRegistry_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchaseRegistry.RowDataBound
        Dim txtRecivedQty As New TextBox
        Dim lblOderedQty As New Label
        Dim txtAcceptedQty As New TextBox
        Dim txtRejectedQty As New TextBox
        Dim txtExcssQty As New TextBox
        Dim txtMdate As New TextBox
        Dim txtEdate As New TextBox
        Dim lblPending As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                txtRecivedQty = e.Row.FindControl("txtReceivedQty")
                lblOderedQty = e.Row.FindControl("lblOrderQty")
                txtAcceptedQty = e.Row.FindControl("txtAcceptedQty")
                txtRejectedQty = e.Row.FindControl("txtRejected")
                txtExcssQty = e.Row.FindControl("txtExcessQty")
                lblPending = e.Row.FindControl("lblPending")
                txtMdate = e.Row.FindControl("txtMdate")
                txtEdate = e.Row.FindControl("txtEdate")
                txtEdate.Attributes.Add("Onblur", "javascript:return CheckEDate('" & txtMdate.ClientID & "','" & txtEdate.ClientID & "')")
                txtMdate.Attributes.Add("Onblur", "javascript:return CheckMDate('" & txtMdate.ClientID & "')")
                txtRejectedQty.Attributes.Add("Onblur", "javascript:return ValidateStringOrNot('" & txtRecivedQty.ClientID & "','" & txtRejectedQty.ClientID & "','" & txtExcssQty.ClientID & "','" & txtAcceptedQty.ClientID & "','" & txtAcceptedQty.ClientID & "')")
                txtExcssQty.Attributes.Add("Onblur", "javascript:return CheckExcess('" & txtExcssQty.ClientID & "')")
                txtRecivedQty.Attributes.Add("Onblur", "javascript:return ConfirmMessage('" & lblPending.ClientID & "','" & txtRecivedQty.ClientID & "','" & txtAcceptedQty.ClientID & "','" & txtRejectedQty.ClientID & "','" & txtExcssQty.ClientID & "','" & lblPending.ClientID & "')")
                txtAcceptedQty.Attributes.Add("Onblur", "javascript:return Amount('" & lblPending.ClientID & "','" & txtAcceptedQty.ClientID & "','" & txtRecivedQty.ClientID & "','" & txtRejectedQty.ClientID & "','" & txtExcssQty.ClientID & "')")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchaseRegistry_RowDataBound")
        End Try
    End Sub

    Private Sub imgRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgRefresh.Click
        Try
            lblStatus.Text = ""
            ' ddlNBrand.SelectedIndex = 0
            '   ddlNItems.Items.Clear() : ddlNUnit.Items.Clear() : ddlRate.Items.Clear()
            '   txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""  'txtRequiredDate.Text = ""
            '   txtCSTAmount.Text = "" : ddlVat.Items.Clear() : ddlCst.Items.Clear() : txtVatAmount.Text = ""
            '  txtExcise.Text = "" : txtTotalAmount.Text = "" : txtAccepted.Text = "" : txtDiscount.Text = ""
            txtDocRefNo.Text = "" : txtInvoiceDate.Text = "" : txtEsugam.Text = ""
            ddlSupplier.SelectedIndex = 0 : txtDcNo.Text = "" ': ddlExistingDocRef.Items.Clear()
            txtDocRefNo.Text = ""
            dgPurchaseRegistry.DataSource = Nothing
            dgPurchaseRegistry.DataBind()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgRefresh_Click")
        End Try
    End Sub
End Class


