Imports BusinesLayer
Imports System.Data
Imports System.Web
Imports DatabaseLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing

Imports Microsoft.Office.Interop
Imports System.Object
Partial Class Purchase_PurchaseOrder
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Orders/PurchaseOrder"
    Dim sSession As New AllSession
    Dim objPO As New clsPurchaseOrder
    Dim objInvD As New clsInvenotryDetails
    Dim objClsFASGnrl As New clsFASGeneral
    Dim objGnrlFnction As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails
    Private objclsModulePermission As New clsModulePermission
    Dim objclsFASPermission As New clsFASPermission
    Private Shared sIKBBackStatus As String
    Private Shared sPOSave As String
    Private objAccSetting As New clsAccountSetting
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objSettings As New clsApplicationSettings
    Dim ObjDBL As New DBHelper

    Private Shared iDocID As Integer
    Private objIndex As New clsIndexing
    Dim dt As New DataTable
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnPrint.ImageUrl = "~/Images/Download24.png"
        imgRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iIKBID As Integer
        Dim sFormButtons As String = True
        Dim dt As New DataTable
        Dim iDefaultBranch As Integer
        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Try
            ReFVOdate.ErrorMessage = "Enter Order Date."
            RFVSupplier.InitialValue = "Select Supplier"
            RFVDschdule.InitialValue = "Select Delivery Schdule"
            RFVMshipping.InitialValue = "Select Mode of Shipping"
            RFVMpayment.InitialValue = "Select Method Of Payment"
            RFVCmdty.InitialValue = "Select Brand"

            RFVPterms.InitialValue = "Select Payment Terms"
            RefRate.InitialValue = "Enter Rate Field"
            ReDiscount.ErrorMessage = "Only Integer." : ReDiscount.ValidationExpression = "^\s*-?[0-9]\d*(\.\d{1,2})?\s*$"
            sSession = Session("AllSession")
            If IsPostBack = False Then

                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                lblDateDisplay.Text = objGnrlFnction.GetCurrentDate(sSession.AccessCode)
                LoadCurrencyType() : GetAppSettings()
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PO")
                imgbtnAdd.Visible = False : imgbtnPrint.Visible = False : imgRefresh.Visible = False : sPOSave = "NO"
                imgbtnAddCharge.Visible = False
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
                        imgbtnAddCharge.Visible = True
                        sPOSave = "YES"
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnPrint.Visible = True
                    End If
                    If sFormButtons = ",View,New,SaveOrUpdate,Report," Then
                        imgbtnAdd.Visible = True : imgbtnPrint.Visible = True : imgRefresh.Visible = True
                    End If
                End If
                'If sSession.YearID > 0 Then
                '    sStr = sSession.YearName
                '    sArray = sStr.Split("-")
                '    iSYear = sArray(0)
                '    iEYear = sArray(1)

                'dStartDate = objGnrlFnction.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'dEndDate = objGnrlFnction.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                '    cclFromDate.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    cclFromDate.EndDate = New DateTime(iEYear, 3, dEndDate)

                'End If

                'rgvtxtOrderDate.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvtxtOrderDate.MinimumValue = "" & dStartDate & ""
                'rgvtxtOrderDate.MaximumValue = "" & dEndDate & ""

                'txtStartDate.Text = objGnrlFnction.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtEndDate.Text = objGnrlFnction.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtStartDate.Text = sSession.StartDate
                'txtEndDate.Text = sSession.EndDate

                'imgbtnPrint.Visible = True
                'imgbtnAdd.Visible = True

                'imgbtnPrint.Visible = False
                'imgbtnAdd.Visible = False
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FASPO", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnPrint.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnAdd.Visible = True
                '    End If
                'End If

                Session("ChargesMaster") = Nothing
                LoadChargeType()
                BindCompanyType()
                BindBranch()
                'BindGSTNCategory()
                dt = objPO.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    'txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    'txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")
                    If IsDBNull(dt.Rows(0)("CUST_COMM_Address")) = True Or IsDBNull(dt.Rows(0)("CUST_ProvisionalNo")) = True Then
                        lblError.Text = "FIll the details in Company Master"
                        Exit Sub
                    End If
                    Dim taxcategory As String
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")
                    taxcategory = objPO.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CUST_TAXPayableCategory"))
                    If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                        txtDeliveryGSTNRegNo.Enabled = False
                    Else
                        txtDeliveryGSTNRegNo.Enabled = True
                    End If
                End If

                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)
                iDefaultBranch = objPO.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                ' Me.txtRate.Attributes.Add("onblur", "return CalculateQuantity()")
                txtFreight.Text = 0 : txtFreightAmount.Text = 0

                RFVAccZone.InitialValue = "--- Select Zone ---" : RFVAccZone.ErrorMessage = "Select Zone."

                RFVAccRgn.InitialValue = "--- Select Region ---" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "--- Select Area ---" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "--- Select Branch ---" : RFVAccBrnch.ErrorMessage = "Select Branch."

                Me.chkCategory.Attributes.Add("onclick", "return validatePage()")
                Me.txtQuantity.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtDiscount.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtRate.Attributes.Add("onblur", "return CalculateQuantity()")

                'Me.txtFreight.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtGSTRate.Attributes.Add("onblur", "return CalculateQuantity()")
                'Me.chkCategory.Attributes.Add("onclick", "return ValidateAll()")
                LoadExistingPurchaseOrder()
                GenerateOrderCodeAnddate()
                LoadSuppliers()
                LoadCommodity()


                LoadMethodOfPayment()
                LoadPaymentTerms()
                LoadDeliverySchdule()
                LoadModeShiping()
                loadNumberOfDays()
                loadDescitionStart()

                Dim iAID As String = "" : Dim iDashBoard As String = ""
                iDashBoard = Request.QueryString("sStrID")
                If iDashBoard = "1" Then
                    iAID = Request.QueryString("sStrID")
                    'objClsFASGnrl.DecryptQueryString(Request.QueryString("AID"))
                    If iAID <> "AddNew" Then
                        'ExistingAllocationNo(0)
                        ddlExistingOrder.SelectedValue = Request.QueryString("AID")
                        ddlExistingOrder_SelectedIndexChanged(sender, e)
                    Else
                    End If
                ElseIf iDashBoard = "0" Then
                End If
                If iDashBoard = "" Then
                    Dim iAllocateID As String = ""
                    'iAllocateID = objClsFASGnrl.DecryptQueryString(Request.QueryString("AllocationID"))
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
    Private Sub LoadCurrencyType()
        Dim dt As New DataTable
        Try
            dt = objSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCurrency.DataSource = dt
            ddlCurrency.DataTextField = "CUR_CODE"
            ddlCurrency.DataValueField = "CUR_ID"
            ddlCurrency.DataBind()
            ddlCurrency.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub GetAppSettings()
        Dim dt As New DataTable
        Dim i As Integer
        Try
            dt = objSettings.GetApllicationSettingsDetails(sSession.AccessCode, sSession.AccessCodeID)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("sad_Config_Key") = "Currency" Then    'Currency
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        ddlCurrency.SelectedValue = dt.Rows(i)("sad_Config_Value")
                    End If
                End If
            Next
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
    Public Sub LoadUnit()
        Try
            ddlUnit.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
            ddlUnit.DataTextField = "Mas_Desc"
            ddlUnit.DataValueField = "Mas_ID"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objPO.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindBranch()
        Try
            'ddlBranch.DataSource = objPO.LoadBranch(sSession.AccessCode, sSession.AccessCodeID)
            'ddlBranch.DataTextField = "CUSTB_Name"
            'ddlBranch.DataValueField = "CUSTB_Id"
            'ddlBranch.DataBind()
            'ddlBranch.Items.Insert(0, "Select Branch")

            ddlBranch.DataSource = objPO.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlCompanyType.DataSource = objPO.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
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
            dt = objPO.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategory.DataSource = dt
            ddlGSTCategory.DataTextField = "GC_GSTCategory"
            ddlGSTCategory.DataValueField = "GC_Id"
            ddlGSTCategory.DataBind()
            ddlGSTCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadExciseUsingDate()
        Try
            Dim oDate As DateTime
            If (chkCategory.SelectedIndex > 0) Then
                If txtOrderDate.Text <> "" Then
                    oDate = Date.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    txtGSTRate.Text = objInvD.LoadExciseUsingDate(sSession.AccessCode, sSession.AccessCodeID, oDate, txtHistoryID.Text)
                    'txtGSTRate.Text = objInvD.LoadExciseUsingDate(sSession.AccessCode, sSession.AccessCodeID, txtOrderDate.Text, txtHistoryID.Text)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExciseUsingDate")
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
    Private Sub LoadMethodOfPayment()
        Try
            ddlMPayment.DataSource = objPO.LoadMethodOfPayment(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlDSchedule.DataSource = objPO.LoadDeliverySchdule(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlModeOfShipping.DataSource = objPO.LoadModeShiping(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlNumberOfDays.DataSource = objPO.loadNumberOfDays(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlPterms.DataSource = objPO.LoadPaymentTerms(sSession.AccessCode, sSession.AccessCodeID)
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
            txtOrderCode.Text = objPO.GeneratePurchaseOrderCode(sSession.AccessCode, sSession.AccessCodeID)
            ' txtOrderDate.Text = clsTRACeGeneral.FormatDtForRDBMS(System.DateTime.Now, "D")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateOrderCodeAnddate")
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
    Public Sub LoadExistingPurchaseOrder()
        Dim iBranchID As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                iBranchID = ddlAccBrnch.SelectedValue
            Else
                iBranchID = 0
            End If
            ddlExistingOrder.DataSource = objPO.LoadExistingOrder(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBranchID)
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
    Protected Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
        Dim altPices As Integer
        Dim dDate, dSDate As Date : Dim m As Integer

        Try
            lblError.Text = ""
            txtFreight.Text = 0 : txtFreightAmount.Text = 0
            If (chkCategory.SelectedValue > 0) Then

                If txtOrderDate.Text <> "" Then
                    'Cheque Date Comparision'
                    dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m < 0 Then
                        lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                        lblUserMasterDetailsValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                        txtOrderDate.Focus()
                        Exit Sub
                    End If

                    dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dDate, dSDate)
                    If m > 0 Then
                        lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                        lblUserMasterDetailsValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                        txtOrderDate.Focus()
                        Exit Sub
                    End If
                    'Cheque Date Comparision'
                End If

                ddlCommodity.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                'If (ddlTypeOfSale.SelectedIndex > 0) Then
                '    If (ddlTypeOfSale.SelectedIndex = 1) Then
                '        ddlCommodity.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
                '    ElseIf (ddlTypeOfSale.SelectedIndex = 2) Then
                '        If (ddlCstCtgry.SelectedIndex > 0) Then
                '            ddlCommodity.SelectedValue = objPO.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
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
            altPices = objPO.GetAlterNatePiceValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
            txtPices.Text = altPices
            ddlUnit.SelectedValue = objPO.GetUnitsValue(sSession.AccessCode, sSession.AccessCodeID, txtHistoryID.Text)
            hfTotalPieces.Value = txtPices.Text
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkCategory_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadDesciptionDetails()
        Dim dt As New DataTable
        Dim sArray As Array
        Dim dOrderDate As Date
        Dim iGSTRate As Integer
        Try
            ddlRate.DataSource = dt
            ddlRate.DataBind()
            txtRDate.Text = "" : txtRate.Text = "" : txtRateAmount.Text = ""
            txtQuantity.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtGSTRate.Text = "" : txtGSTAmount.Text = ""
            txtTotalAmount.Text = ""

            If lblDescID.Text <> "0" Then
                iGSTRate = objPO.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                If iGSTRate > 0 Then
                Else
                    lblError.Text = "Enter the HSN Details in Inventory Master."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                If txtOrderDate.Text <> "" Then
                    dOrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    dt = objPO.CheckDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text, dOrderDate)
                    If dt.Rows.Count = 0 Then
                        lblError.Text = "Enter Details in Inventory Master Details"
                        lblUserMasterDetailsValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If dt.Rows.Count > 1 Then
                        ddlRate.DataSource = dt
                        ddlRate.DataTextField = "INVH_PreDeterminedPrice"
                        ddlRate.DataValueField = "InvH_ID"
                        ddlRate.DataBind()
                        ddlRate.Enabled = True
                        'txtRate.Enabled = False
                        txtHistoryID.Text = ddlRate.SelectedValue
                        sArray = ddlRate.SelectedItem.Text.Split("-")
                        txtRate.Text = sArray(0)
                        'LoadExciseUsingDate()

                        GetOtherDetails(txtHistoryID.Text)
                    Else
                        sArray = dt.Rows(0)(1).ToString().Split("-")
                        txtRate.Text = sArray(0)
                        txtHistoryID.Text = dt.Rows(0)(0).ToString()

                        'LoadExciseUsingDate()
                        ddlRate.Enabled = False
                        'txtRate.Enabled = True
                        If txtHistoryID.Text <> "" Then
                            ' GetPurchaseDetails(txtHistoryID.Text)
                            GetOtherDetails(txtHistoryID.Text)
                        End If
                    End If
                    ddlUnit.DataSource = objPO.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                    ddlUnit.DataTextField = "Mas_Desc"
                    ddlUnit.DataValueField = "Mas_ID"
                    ddlUnit.DataBind()
                    ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")

                    txtGSTID.Text = 0
                    txtGSTRate.Text = 0

                    Dim sGSTRate As String = ""
                    sGSTRate = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                    If sGSTRate <> "HSN" Then
                        txtGSTID.Text = 0
                        'txtGSTRate.Text = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                        txtGSTRate.Text = 0
                    Else
                        txtGSTID.Text = objPO.GetGSTID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                        txtGSTRate.Text = objPO.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDesciptionDetails")
        End Try
    End Sub
    Private Sub GetOtherDetails(ByVal iHistoryId As Integer)
        'Dim dt As New DataTable
        Try
            'dt = objPO.GetOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            'If dt.Rows.Count > 0 Then
            '    If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
            '        txtGSTRate.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
            '    Else
            '        txtGSTRate.Text = "0"
            '    End If

            'End If
            Dim sGSTRate As String = ""
            sGSTRate = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
            If sGSTRate <> "HSN" Then
                txtGSTID.Text = 0
                'txtGSTRate.Text = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                txtGSTRate.Text = 0
            Else
                txtGSTID.Text = objPO.GetGSTID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                txtGSTRate.Text = objPO.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetOtherDetails")
        End Try
    End Sub
    Private Sub gwtDetails(ByVal iHistoryId As Integer)
        Dim dt As New DataTable
        Try
            'dt = objPO.GetOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            'If dt.Rows.Count > 0 Then
            'If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
            '    txtExcise.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
            'Else
            '    txtExcise.Text = "0"
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gwtDetails")
        End Try
    End Sub
    Private Function GetPurchaseDetails(ByVal iHistoryId As Integer) As Object
        Dim dt As New DataTable
        Try
            dt = objPO.GetPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iHistoryId)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("InvH_Excise").ToString()) = False Then
                    txtGSTRate.Text = objClsFASGnrl.ReplaceSafeSQL(dt.Rows(0)("InvH_Excise").ToString())
                Else
                    txtGSTRate.Text = "0"
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
        Dim dDate, dSDate As Date : Dim m As Integer

        Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer, iBaseID As Integer = 0
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Try
            If (ddlAccBrnch.SelectedIndex = 0) Then
                lblError.Text = "Select Branch."
                Exit Sub
            End If

            'Check Application Settings'

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146
            If iGL = 0 Then
                lblError.Text = "Set The Application Settings."
                Exit Sub
            End If

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146
            If iGL = 0 Then
                lblError.Text = "Set The Application Settings."
                Exit Sub
            End If

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            If iSubGroup = 0 Then
                lblError.Text = "Set The Application Settings."
                Exit Sub
            End If
            'Check Application Settings'

            'Extra
            If txtGSTRate.Text = "" Or ddlUnit.Items.Count = 0 Then
                lblError.Text = "For this item add the Inventory Details in Inventory Details form."
                lblUserMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            'Extra

            If txtOrderDate.Text <> "" Then
                'Cheque Date Comparision'
                dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                    txtOrderDate.Focus()
                    Exit Sub
                End If

                dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                    lblUserMasterDetailsValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                    txtOrderDate.Focus()
                    Exit Sub
                End If
                'Cheque Date Comparision'
            End If

            If (ddlExistingOrder.SelectedIndex > 0) Then
                If (objPO.CheckApprovedOrNot(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue) = False) Then
                    lblStatus.Text = "Already Approved "
                    Exit Sub
                End If
            End If
            lblError.Text = ""
            txtOrderDate.Enabled = False

            If txtOrderDate.Text <> "" Then
                dOrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            objPO.sPOM_OrderNo = txtOrderCode.Text
            objPO.iPOM_Supplier = ddlSupplier.SelectedValue
            objPO.iPOM_ModeOfShipping = ddlModeOfShipping.SelectedValue
            objPO.iPOM_CreatedBy = sSession.UserID
            objPO.iPOM_YearID = sSession.YearID

            If (ddlPterms.SelectedIndex > 0) Then
                objPO.iPOM_Paymentterms = ddlPterms.SelectedValue
            Else
                objPO.iPOM_Paymentterms = 0
            End If
            If (ddlMPayment.SelectedIndex > 0) Then
                objPO.iPOM_MethodofPayment = ddlMPayment.SelectedValue
            Else
                objPO.iPOM_MethodofPayment = 0
            End If
            If (ddlDSchedule.SelectedIndex > 0) Then
                objPO.iPOM_DSchdule = ddlDSchedule.SelectedValue
            Else
                objPO.iPOM_DSchdule = 0
            End If
            If (ddlModeOfShipping.SelectedIndex > 0) Then
                objPO.iPOM_ModeOfShipping = ddlModeOfShipping.SelectedValue
            Else
                objPO.iPOM_ModeOfShipping = 0
            End If
            If (ddlMPayment.SelectedIndex > 0) Then
                objPO.iPOM_MethodofPayment = ddlMPayment.SelectedValue
            Else
                objPO.iPOM_MethodofPayment = 0
            End If
            objPO.iPOM_SaleType = 0
            objPO.iPOM_iCSTCtgry = 0

            objPO.sPOM_Status = "W"
            iBaseID = objPO.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
            If hfTotalAmount.Value <> "" Then
                objPO.sPOD_TotalAmount = Request.Form(hfTotalAmount.UniqueID)
                If ddlCurrency.SelectedValue = iBaseID Then
                    objPO.sPOD_FETotalAmt = Request.Form(hfTotalAmount.UniqueID)
                Else
                    objPO.sPOD_FETotalAmt = objPO.GetFERates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue, Request.Form(hfTotalAmount.UniqueID))
                End If
            Else
                objPO.sPOD_TotalAmount = objClsFASGnrl.SafeSQL(txtTotalAmount.Text)
                If ddlCurrency.SelectedValue = iBaseID Then
                    objPO.sPOD_FETotalAmt = objClsFASGnrl.SafeSQL(txtTotalAmount.Text)
                Else
                    objPO.sPOD_FETotalAmt = objPO.GetFERates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue, objClsFASGnrl.SafeSQL(txtTotalAmount.Text))
                End If
            End If
            objPO.sOralOrPO = "P"
            objPO.sPOM_ESugam = ""
            objPO.sPOM_DEliveryChlnNo = ""
            objPO.sPOM_InvoiceRef = ""

            objPO.POM_TrType = 1

            If txtCompanyAddress.Text <> "" Then
                objPO.POM_CompanyAddress = txtCompanyAddress.Text
            Else
                objPO.POM_CompanyAddress = ""
            End If

            If txtBillingAddress.Text <> "" Then
                objPO.POM_BillingAddress = txtBillingAddress.Text
            Else
                objPO.POM_BillingAddress = ""
            End If

            If txtDeliveryFromAddress.Text <> "" Then
                objPO.POM_DeliveryFrom = txtDeliveryFromAddress.Text
            Else
                objPO.POM_DeliveryFrom = ""
            End If

            If txtDeleveryAddress.Text <> "" Then
                objPO.POM_DeliveryAddress = txtDeleveryAddress.Text
            Else
                objPO.POM_DeliveryAddress = ""
            End If

            If txtCompanyGSTNRegNo.Text <> "" Then
                objPO.POM_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
            Else
                objPO.POM_CompanyGSTNRegNo = ""
            End If

            If txtBillingGSTNRegNo.Text <> "" Then
                objPO.POM_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
            Else
                objPO.POM_BillingGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" Then
                objPO.POM_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
            Else
                objPO.POM_DeliveryFromGSTNRegNo = ""
            End If

            If txtDeliveryGSTNRegNo.Text <> "" Then
                objPO.POM_DeliveryGSTNRegNo = txtDeliveryGSTNRegNo.Text
            Else
                objPO.POM_DeliveryGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text = "" Then
                objPO.POM_PurchaseStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objPO.POM_PurchaseStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtDeliveryGSTNRegNo.Text = "" Then
                objPO.POM_PurchaseStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
                objPO.POM_PurchaseStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            End If

            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtDeliveryGSTNRegNo.Text <> "" Then
            '    objPO.POM_PurchaseStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtDeliveryGSTNRegNo.Text))
            'End If

            'objPO.POM_CompanyType = ddlCompanyType.SelectedValue
            'objPO.POM_GSTNCategory = ddlGSTCategory.SelectedValue

            If ddlCompanyType.SelectedIndex = 0 And ddlGSTCategory.SelectedIndex = -1 Then
                lblError.Text = "Update the Company type and GSTN Category in Supplier Master Form"
                Exit Sub
            Else
                objPO.POM_CompanyType = ddlCompanyType.SelectedValue
                objPO.POM_GSTNCategory = ddlGSTCategory.SelectedValue
            End If

            '**** Preethika 
            objPO.POM_ZoneID = ddlAccZone.SelectedValue
            objPO.POM_RegionID = ddlAccRgn.SelectedValue
            objPO.POM_AreaID = ddlAccArea.SelectedValue
            'objPO.POM_BranchID = ddlAccBrnch.SelectedValue
            If ddlAccBrnch.SelectedIndex > 0 Then
                objPO.POM_BranchID = ddlAccBrnch.SelectedValue
            Else
                objPO.POM_BranchID = 0
            End If

            objPO.POM_BatchNo = 0
            objPO.POM_BaseName = 0

            iMasterID = objPO.SavePurchaseOrder(sSession.AccessCode, sSession.AccessCodeID, dOrderDate, objPO)
            txtMasterID.Text = iMasterID
            Dim iPKID As Integer
            If txtDetailsID.Text <> "" Then
                iPKID = txtDetailsID.Text
            Else
                iPKID = 0
            End If

            objPO.iPOD_MasterID = iMasterID
            objPO.iPOD_Commodity = ddlCommodity.SelectedValue
            objPO.iPOD_DescriptionID = lblDescID.Text
            If txtHistoryID.Text <> "" Then
                objPO.iPOD_HistoryID = txtHistoryID.Text
            Else
                objPO.iPOD_HistoryID = 0
            End If
            objPO.iPOD_Unit = ddlUnit.SelectedValue
            objPO.sPOD_Rate = Trim(txtRate.Text)

            If txtRateAmount.Text <> "" Then
                objPO.sPOD_RateAmount = objClsFASGnrl.SafeSQL(txtRateAmount.Text)
            Else
                objPO.sPOD_RateAmount = 0
            End If

            If txtQuantity.Text = "" Then
                objPO.sPOD_Quantity = "0"
            Else
                objPO.sPOD_Quantity = objClsFASGnrl.SafeSQL(txtQuantity.Text)
            End If
            If txtDiscount.Text = "" Then
                objPO.sPOD_Discount = "0"
            Else
                objPO.sPOD_Discount = objClsFASGnrl.SafeSQL(txtDiscount.Text)
            End If
            If txtDiscountAmount.Text <> "" Then
                objPO.sPOD_DiscountAmount = objClsFASGnrl.SafeSQL(txtDiscountAmount.Text)
            Else
                objPO.sPOD_DiscountAmount = 0
            End If

            objPO.sPOD_Excise = "0"
            objPO.sPOD_ExciseAmount = 0

            If txtFreight.Text = "" Then
                objPO.sPOD_Frieght = "0"
            Else
                objPO.sPOD_Frieght = objClsFASGnrl.SafeSQL(txtFreight.Text)
            End If

            If txtFreightAmount.Text = "" Then
                objPO.sPOD_FrieghtAmount = "0"
            Else
                objPO.sPOD_FrieghtAmount = objClsFASGnrl.SafeSQL(txtFreightAmount.Text)
            End If

            objPO.sPOD_VAT = "0"
            objPO.sPOD_VATAmount = 0
            objPO.sPOD_CST = "0"
            objPO.sPOD_CSTAmount = 0

            If txtRDate.Text <> "" Then
                objPO.dPOD_RequiredDate = Date.ParseExact(Trim(txtRDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objPO.dPOD_RequiredDate = "01/01/1900"
            End If

            If txtGSTID.Text <> "" Then
                objPO.POD_GST_ID = txtGSTID.Text
            Else
                txtGSTID.Text = 0
            End If

            objPO.POD_GSTRate = txtGSTRate.Text
            objPO.POD_GSTAmount = txtGSTAmount.Text

            If txtTotalAmount.Text <> "" Then
                If ddlCurrency.SelectedValue = iBaseID Then
                    objPO.sPOD_FETotalAmt = objClsFASGnrl.SafeSQL(txtTotalAmount.Text)
                    objPO.dPOD_CurrencyAmt = 0
                    objPO.sPOD_CurrencyTime = ""
                Else
                    objPO.sPOD_FETotalAmt = objPO.GetFERates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue, objClsFASGnrl.SafeSQL(txtTotalAmount.Text))
                    objPO.dPOD_CurrencyAmt = objPO.GetFECRates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                    objPO.sPOD_CurrencyTime = objPO.GetFECTime(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                End If
            Else
                objPO.sPOD_TotalAmount = 0
                objPO.sPOD_FETotalAmt = 0
            End If
            If ddlCurrency.SelectedIndex > 0 Then
                objPO.iPOD_Currency = ddlCurrency.SelectedValue
            Else
                objPO.iPOD_Currency = 0
            End If

            If objPO.POM_PurchaseStatus = "Local" Then
                objPO.POD_SGST = objPO.POD_GSTRate / 2
                objPO.POD_SGSTAmount = objPO.POD_GSTAmount / 2
                objPO.POD_CGST = objPO.POD_GSTRate / 2
                objPO.POD_CGSTAmount = objPO.POD_GSTAmount / 2
                objPO.POD_IGST = 0
                objPO.POD_IGSTAmount = 0
            ElseIf objPO.POM_PurchaseStatus = "Inter State" Then
                objPO.POD_SGST = 0
                objPO.POD_SGSTAmount = 0
                objPO.POD_CGST = 0
                objPO.POD_CGSTAmount = 0
                objPO.POD_IGST = objPO.POD_GSTRate
                objPO.POD_IGSTAmount = objPO.POD_GSTAmount
            End If

            'If UCase(ddlGSTCategory.SelectedItem.Text) = "UNRIGISTERED DEALER" Then
            '    Dim URD_GSTRate, URD_GSTAmt As Double

            '    URD_GSTRate = objPO.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
            '    URD_GSTAmt = (((objPO.sPOD_RateAmount - objPO.sPOD_DiscountAmount) + objPO.PID_ChargePerItem) * URD_GSTRate) / 100

            '    objPO.POD_SGST = URD_GSTRate / 2
            '    objPO.POD_SGSTAmount = URD_GSTAmt / 2
            '    objPO.POD_CGST = URD_GSTRate / 2
            '    objPO.POD_CGSTAmount = URD_GSTAmt / 2
            '    objPO.POD_IGST = 0
            '    objPO.POD_IGSTAmount = 0
            'End If


            Dim sStatus As String = ""
            objPO.SavePurchaseOrderDetails(sSession.AccessCode, sSession.AccessCodeID, dRequiredDate, objPO, iPKID)
            sStatus = objPO.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            If sStatus = "W" Then
                lblStatus.Text = "Waiting for aprroval."
            End If

            SaveCharges(iMasterID)

            objPO.POD_ReceivedQty = 0
            objPO.POD_Rejected = 0
            objPO.POD_Accepted = 0
            dgPurchase.DataSource = objPO.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
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
        Try
            'Deleting charges Everytime & Saving'
            objPO.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1

                    objPO.C_POrderID = iMasterID
                    objPO.C_PGinID = 0
                    objPO.C_PInvoiceDocRef = 0
                    objPO.C_OrderType = ""
                    objPO.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objPO.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objPO.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objPO.C_PSType = "P"
                    objPO.C_DelFlag = "W"
                    objPO.C_Status = "C"
                    objPO.C_CompID = sSession.AccessCodeID
                    objPO.C_YearID = sSession.YearID
                    objPO.C_CreatedBy = sSession.UserID
                    objPO.C_CreatedOn = System.DateTime.Now
                    objPO.C_Operation = "C"
                    objPO.C_IPAddress = sSession.IPAddress

                    Arr = objPO.SaveCharges(sSession.AccessCode, objPO)
                Next
            End If
            'Charges Saving'
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

            hfDiscountAmount.Value = ""
            hfGSTAmount.Value = ""

            ' ddlSupplier.SelectedIndex = 0

            txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtGSTRate.Text = "" : txtGSTAmount.Text = ""

            txtRDate.Text = "" : txtTotalAmount.Text = ""
            txtFreight.Text = "" : txtFreightAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll")
        End Try
    End Sub
    'Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
    '    Dim chkField As New CheckBox, chkAll As New CheckBox
    '    Dim iIndx As Integer
    '    Try
    '        'lblError.Text = ""
    '        chkAll = CType(sender, CheckBox)
    '        If chkAll.Checked = True Then
    '            For iIndx = 0 To dgPurchase.Rows.Count - 1
    '                chkField = dgPurchase.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = True
    '            Next
    '        Else
    '            For iIndx = 0 To dgPurchase.Rows.Count - 1
    '                chkField = dgPurchase.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = False
    '            Next
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
    '    End Try
    'End Sub
    Protected Sub ddlExistingOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingOrder.SelectedIndexChanged
        Dim dt As New DataTable
        Dim iCurrnecy As Integer = 0
        Try
            lblError.Text = ""
            lblStatus.Text = ""
            If ddlExistingOrder.SelectedIndex > 0 Then
                ddlCommodity.SelectedIndex = 0
                chkCategory.Items.Clear()
                ClearAll()
                dgPurchase.DataSource = objPO.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                dgPurchase.DataBind()
                dt = objPO.LoadPurchaseOderMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue)
                If dt.Rows.Count > 0 Then

                    If dt.Rows(0)("POM_BatchNo") > 0 And dt.Rows(0)("POM_BaseName") > 0 Then
                        lblError.Text = "This Purchase Order from Remote Data entry"

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
                        lblScode.Text = objPO.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
                    Else
                        ddlSupplier.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_ModeOfShipping").ToString()) = False Then

                        If (dt.Rows(0)("POM_ModeOfShipping") > 0) Then
                            ddlModeOfShipping.SelectedValue = dt.Rows(0)("POM_ModeOfShipping").ToString()
                        Else
                            ddlModeOfShipping.SelectedIndex = 0
                        End If
                    Else
                        ddlModeOfShipping.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_Dschdule").ToString()) = False Then

                        If (dt.Rows(0)("POM_Dschdule") > 0) Then
                            ddlDSchedule.SelectedValue = dt.Rows(0)("POM_Dschdule").ToString()
                        Else
                            ddlDSchedule.SelectedIndex = 0
                        End If
                    Else
                        ddlDSchedule.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_PaymentTerms").ToString()) = False Then
                        If (dt.Rows(0)("POM_PaymentTerms") > 0) Then
                            ddlPterms.SelectedValue = dt.Rows(0)("POM_PaymentTerms").ToString()
                        Else
                            ddlPterms.SelectedIndex = 0
                        End If
                    Else
                        ddlPterms.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_MPayment").ToString()) = False Then
                        If (dt.Rows(0)("POM_MPayment") > 0) Then
                            ddlMPayment.SelectedValue = dt.Rows(0)("POM_MPayment").ToString()
                        Else
                            ddlMPayment.SelectedIndex = 0
                        End If
                    Else
                        ddlMPayment.SelectedIndex = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_ID").ToString()) = False Then
                        txtMasterID.Text = dt.Rows(0)("POM_ID").ToString()
                    Else
                        txtMasterID.Text = 0
                    End If

                    If IsDBNull(dt.Rows(0)("POM_CompanyAddress")) = False Then
                        txtCompanyAddress.Text = dt.Rows(0)("POM_CompanyAddress")
                    Else
                        txtCompanyAddress.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("POM_CompanyGSTNRegNo")) = False Then
                        txtCompanyGSTNRegNo.Text = dt.Rows(0)("POM_CompanyGSTNRegNo")
                    Else
                        txtCompanyGSTNRegNo.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("POM_BillingAddress")) = False Then
                        txtBillingAddress.Text = dt.Rows(0)("POM_BillingAddress")
                    Else
                        txtBillingAddress.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("POM_BillingGSTNRegNo")) = False Then
                        txtBillingGSTNRegNo.Text = dt.Rows(0)("POM_BillingGSTNRegNo")
                    Else
                        txtBillingGSTNRegNo.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("POM_DeliveryFrom")) = False Then
                        txtDeliveryFromAddress.Text = dt.Rows(0)("POM_DeliveryFrom")
                    Else
                        txtDeliveryFromAddress.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("POM_DeliveryFromGSTNRegNo")) = False Then
                        txtDeliveryFromGSTNRegNo.Text = dt.Rows(0)("POM_DeliveryFromGSTNRegNo")
                    Else
                        txtDeliveryFromGSTNRegNo.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("POM_DeliveryAddress")) = False Then
                        txtDeleveryAddress.Text = dt.Rows(0)("POM_DeliveryAddress")
                    Else
                        txtDeleveryAddress.Text = ""
                    End If
                    If IsDBNull(dt.Rows(0)("POM_DeliveryGSTNRegNo")) = False Then
                        txtDeliveryGSTNRegNo.Text = dt.Rows(0)("POM_DeliveryGSTNRegNo")
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

                    txtDeliveryFromGSTNRegNo.Enabled = False
                    If ddlGSTCategory.SelectedIndex <> -1 Then
                        Dim description As String
                        description = objPO.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                        If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                            txtDeliveryFromGSTNRegNo.Enabled = False
                        Else
                            txtDeliveryFromGSTNRegNo.Enabled = True
                        End If
                    End If

                    If IsDBNull(dt.Rows(0)("POM_Status").ToString()) = False Then
                        ddlCurrency.Enabled = False
                        If (dt.Rows(0)("POM_Status") = "W") Then
                            txtOrderDate.Enabled = True
                            lblStatus.Text = "Waiting For approval"
                        ElseIf dt.Rows(0)("POM_Status") = "A" Then
                            lblStatus.Text = "Approved."
                            txtOrderDate.Enabled = False
                        Else
                        End If
                    End If

                    Dim dtCharge As New DataTable
                    dtCharge = objPO.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingOrder.SelectedValue, 0, 0)
                    GvCharge.DataSource = dtCharge
                    GvCharge.DataBind()
                    Session("ChargesMaster") = dtCharge

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

                    GetAttachFile(ddlExistingOrder.SelectedItem.Text)

                    'If IsDBNull(dt.Rows(0)("POM_BatchNo")) = False Then
                    '    If (dt.Rows(0)("POM_BatchNo") > 0) And (dt.Rows(0)("POM_BaseName") > 0) Then
                    '        lblError.Text = "This PO is from Remote Data Module,you can no modify the data."
                    '        imgbtnAdd.Enabled = False : imgbtnAttachment.Enabled = False
                    '        Exit Sub
                    '    End If
                    'End If

                End If

                iCurrnecy = objPO.GetCurrency(sSession.AccessCode, sSession.AccessCodeID, ddlExistingOrder.SelectedValue)
                If iCurrnecy > 0 Then
                    ddlCurrency.SelectedValue = iCurrnecy
                End If
            Else
                txtOrderCode.Text = "" : txtOrderDate.Text = "" : lblScode.Text = "" : ddlSupplier.SelectedIndex = 0
                ddlModeOfShipping.SelectedIndex = 0 : txtMasterID.Text = 0 : txtOrderDate.Enabled = True
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
            ddlRate.Enabled = False
            'txtRate.Enabled = False
            If ddlCommodity.SelectedIndex > 0 Then
                chkCategory.DataSource = objPO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
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
    Protected Sub dgPurchase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgPurchase.RowDataBound
        Dim imgbtnDelete As New ImageButton, imgbtnEdit As New ImageButton
        If e.Row.RowType = DataControlRowType.DataRow Then
            imgbtnDelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
            imgbtnDelete.ImageUrl = "~/Images/DeActivate16.png"
            imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
            imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
            dgPurchase.Columns(31).Visible = False
            If sPOSave = "YES" Then
                dgPurchase.Columns(31).Visible = True
            End If
        End If
    End Sub
    Protected Sub ddlSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSupplier.SelectedIndexChanged
        Dim dtCustomer As New DataTable
        Try
            If ddlSupplier.SelectedIndex > 0 Then
                txtSprCode.Text = objPO.GetSupplierCode(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
                txtQuantity.Text = ""
                txtDiscount.Text = ""
                dtCustomer = objPO.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlSupplier.SelectedValue)
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
                        description = objPO.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                        If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                            txtDeliveryFromGSTNRegNo.Enabled = False
                        Else
                            txtDeliveryFromGSTNRegNo.Enabled = True
                        End If
                    Else
                        ddlGSTCategory.Items.Clear()
                    End If

                End If
            Else
                lblScode.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSupplier_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub dgPurchase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgPurchase.RowCommand
        Dim lnkDescription As New Label
        Dim lblcomodityID As New Label
        Dim lblDescriptionId As New Label
        Dim lblHistoryID As New Label
        Dim dt As New DataTable : Dim lblID As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
            txtDetailsID.Text = lblID.Text
            lnkDescription = DirectCast(clickedRow.FindControl("lnkGoods"), Label)
            lblcomodityID = DirectCast(clickedRow.FindControl("lblCommodityID"), Label)
            lblDescriptionId = DirectCast(clickedRow.FindControl("lblDescriptionID"), Label)
            lblHistoryID = DirectCast(clickedRow.FindControl("lblHistoryID"), Label)
            If e.CommandName = "Delete1" Then
                objPO.DeleteOrderValues(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text, lblDescriptionId.Text, lblHistoryID.Text, lblID.Text)
                lblStatus.Text = "Sucessfully Deleted"
                dgPurchase.DataSource = objPO.LoadPurchaseORderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text)
                dgPurchase.DataBind()
                If (dgPurchase.Rows.Count = 0) Then
                    objPO.DeleteOrderValuesFromMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtOrderCode.Text)
                End If
                LoadExistingPurchaseOrder()
            End If
            If e.CommandName = "Edit1" Then
                If (objPO.GetStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtMasterID.Text) = "W") Then
                    txtHistoryID.Text = lblHistoryID.Text

                    dt = objPO.LoadPurchaseOderDetails(sSession.AccessCode, sSession.AccessCodeID, txtMasterID.Text, lblcomodityID.Text, lblDescriptionId.Text, lblHistoryID.Text, lblID.Text)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("POD_Commodity").ToString()) = False Then
                            ddlCommodity.SelectedValue = dt.Rows(0)("POD_Commodity").ToString()
                            chkCategory.DataSource = objPO.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
                            chkCategory.DataTextField = "Inv_Code"
                            chkCategory.DataValueField = "Inv_ID"
                            chkCategory.DataBind()
                            chkCategory.SelectedValue = dt.Rows(0)("POD_DescriptionID").ToString()
                            lblDescID.Text = dt.Rows(0)("POD_DescriptionID").ToString()

                            gwtDetails(txtHistoryID.Text)
                        Else
                            ddlCommodity.SelectedIndex = 0
                        End If

                        If IsDBNull(dt.Rows(0)("POD_HistoryID").ToString()) = False Then
                            txtHistoryID.Text = dt.Rows(0)("POD_HistoryID").ToString()
                        Else
                            txtHistoryID.Text = "0"
                        End If

                        LoadUnit()
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

                        If IsDBNull(dt.Rows(0)("POD_GSTRate").ToString()) = False Then
                            txtGSTRate.Text = dt.Rows(0)("POD_GSTRate").ToString()
                        Else
                            txtGSTRate.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("POD_GSTAmount").ToString()) = False Then
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

                        If IsDBNull(dt.Rows(0)("POD_RequiredDate")) = False Then
                            txtRDate.Text = objClsFASGnrl.FormatDtForRDBMS(dt.Rows(0)("POD_RequiredDate").ToString(), "D")
                        Else
                            txtRDate.Text = ""
                        End If
                        If IsDBNull(dt.Rows(0)("POD_TotalAmount").ToString()) = False Then
                            txtTotalAmount.Text = dt.Rows(0)("POD_TotalAmount").ToString()
                        Else
                            txtTotalAmount.Text = ""
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPurchase_RowCommand")
        End Try
    End Sub
    Private Sub imgbtnPrint_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPrint.Click
        Dim flag As Integer = 1
        Try
            If (ddlExistingOrder.SelectedIndex > 0) Then
                flag = objPO.GetPrintFlagValue(sSession.AccessCode, sSession.AccessCodeID)
                If (flag = 1) Then
                    lblError.Text = ""
                    Response.Redirect("~/Reports/Purchase/PurchaseSizeWise.aspx?ExistingOrder=" & ddlExistingOrder.SelectedValue)
                Else
                    lblError.Text = ""
                    Response.Redirect("~/Reports/Purchase/PurchaseItemWise.aspx?ExistingOrder=" & ddlExistingOrder.SelectedValue)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPrint_Click")
        End Try
    End Sub
    Private Sub imgRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgRefresh.Click
        Try
            For i = 0 To chkCategory.Items.Count - 1
                chkCategory.Items(i).Selected = False
            Next
            ddlExistingOrder.SelectedIndex = 0
            GenerateOrderCodeAnddate()
            ddlUnit.Items.Clear()
            txtRate.Text = ""

            ddlCurrency.Enabled = True

            hfDiscountAmount.Value = ""
            hfGSTAmount.Value = ""

            ddlSupplier.SelectedIndex = 0 : txtDetailsID.Text = ""
            txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtGSTRate.Text = "" : txtGSTAmount.Text = ""

            txtRDate.Text = "" : txtTotalAmount.Text = ""
            txtFreight.Text = 0 : txtFreightAmount.Text = 0
            txtOrderDate.Enabled = True
            dgPurchase.DataSource = Nothing
            dgPurchase.DataBind()

            Session("ChargesMaster") = Nothing
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgRefresh_Click")
        End Try
    End Sub
    Protected Sub ddlRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRate.SelectedIndexChanged
        Dim sArray As Array
        Try
            If txtHistoryID.Text <> "" Then
                ' GetPurchaseDetails(ddlRate.SelectedValue)
                txtHistoryID.Text = ddlRate.SelectedValue
                sArray = ddlRate.SelectedItem.Text.Split("-")
                txtRate.Text = sArray(0)

                LoadExciseUsingDate()

                txtQuantity.Text = "" : txtDiscount.Text = "" : txtGSTRate.Text = ""
                ' txtVat.Text = "" : txtCST.Text = ""
                txtRDate.Text = ""
                GetOtherDetails(txtHistoryID.Text)

            Else
                txtHistoryID.Text = ddlRate.SelectedValue

                LoadExciseUsingDate()

                sArray = ddlRate.SelectedItem.Text.Split("-")
                txtRate.Text = sArray(0)
                txtQuantity.Text = "" : txtDiscount.Text = "" : txtGSTRate.Text = ""
                '  txtVat.Text = "" : txtCST.Text = ""
                txtRDate.Text = ""
                GetOtherDetails(txtHistoryID.Text)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRate_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub chkboxFrom_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxFrom.CheckedChanged
        Try
            If chkboxFrom.Checked = True Then
                txtDeliveryFromAddress.Text = txtBillingAddress.Text
                txtDeliveryFromGSTNRegNo.Text = txtBillingGSTNRegNo.Text
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
                txtDeleveryAddress.Text = txtCompanyAddress.Text
                txtDeliveryGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
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
        Dim description As String = ""
        Try
            txtCompanyAddress.Text = "" : txtCompanyGSTNRegNo.Text = ""
            If ddlBranch.SelectedIndex > 0 Then
                dt = objPO.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, ddlBranch.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUSTB_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUSTB_GSTNRegNo")

                    'ddlCompanyType.SelectedValue = dt.Rows(0)("CUSTB_CompanyType")
                    'BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    'ddlGSTCategory.SelectedValue = dt.Rows(0)("CUSTB_GSTNCategory")

                    'description = objPO.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    'If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                    '    txtDeliveryGSTNRegNo.Enabled = False
                    'Else
                    '    txtDeliveryGSTNRegNo.Enabled = True
                    'End If

                End If
            Else
                dt = objPO.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    'ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    'BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    'ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TaxPayableCategory")

                    'description = objPO.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    'If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                    '    txtDeliveryGSTNRegNo.Enabled = False
                    'Else
                    '    txtDeliveryGSTNRegNo.Enabled = True
                    'End If

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
                    dtTable = objPO.RemoveChargeDublicate(dt)
                    GvCharge.DataSource = dtTable
                    GvCharge.DataBind()

                    If GvCharge.Items.Count > 0 Then
                        For i = 0 To GvCharge.Items.Count - 1
                            dchargeTotal = dchargeTotal + GvCharge.Items(i).Cells(2).Text
                        Next
                    End If
                    txtFreight.Text = dchargeTotal

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
    Private Sub txtOrderDate_TextChanged(sender As Object, e As EventArgs) Handles txtOrderDate.TextChanged
        Dim dDate As Date
        Dim m As Integer
        Dim dSDate As Date
        Try
            lblError.Text = ""
            'Cheque Date Comparision'
            dDate = Date.ParseExact(Trim(sSession.StartDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblUserMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                txtOrderDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(Trim(sSession.EndDate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Order Date (" & txtOrderDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblUserMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                txtOrderDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtOrderDate_TextChanged")
        End Try
    End Sub
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = ObjDBL.SQLExecuteDataTable(sNameSpace, sSql)
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
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                ddlBranch.SelectedValue = ddlAccBrnch.SelectedValue
                ddlBranch_SelectedIndexChanged(sender, e)

                If ddlAccBrnch.SelectedIndex > 0 Then
                    iParent = objPO.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                    ddlAccArea.SelectedValue = iParent
                End If
                If ddlAccArea.SelectedIndex > 0 Then
                    iParent = objPO.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                    ddlAccRgn.SelectedValue = iParent
                End If
                If ddlAccRgn.SelectedIndex > 0 Then
                    iParent = objPO.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
                    ddlAccZone.SelectedValue = iParent
                End If
                LoadExistingPurchaseOrder()
            Else
                ddlBranch.SelectedIndex = 0
                ddlBranch_SelectedIndexChanged(sender, e)

                ddlAccArea.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccZone.SelectedIndex = 0
                LoadExistingPurchaseOrder()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccBrnch_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
        Dim objBatch As clsIndexing.BatchScan
        Dim Arr() As String
        Try
            If gvattach.Rows.Count > 0 Then
                AutomaticIndexing()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
            Else
                lblError.Text = "Add the files before index"
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub AutomaticIndexing()
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
        Dim Arr() As String
        Dim dDate As Date
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        'Dim iCabinet As Integer
        'Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable
        Dim bCheckCabinet As Boolean

        Try
            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlAccBrnch.Focus()
                Exit Sub
            Else
                icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
            End If

            iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Payment Voucher")

            If ddlExistingOrder.SelectedIndex = 0 Then
                lblError.Text = "Select Existing Purchase Order No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlExistingOrder.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlExistingOrder.SelectedItem.Text)
            End If

            iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

            'If ddlType.SelectedIndex = 0 Then
            '    lblModelError.Text = "Select Type."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            '    ddlType.Focus()
            '    Exit Sub
            'Else
            '    iType = ddlType.SelectedValue
            'End If

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                If gvattach.Rows.Count > 0 Then
                    For i = 0 To gvattach.Rows.Count - 1
                        iPageDetailsid = 0
                        chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                        lblPath = gvattach.Rows(i).FindControl("lblPath")
                        If chkSelect.Checked = True Then
                            sPageExt = UCase(gvattach.Rows(i).Cells(3).Text)
                            sFilePath = lblPath.Text
                            sFileName = gvattach.Rows(i).Cells(2).Text
                            objIndex.iPGEBASENAME = objGnrlFnction.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objClsFASGnrl.SafeSQL(txtTitle.Text.Trim)
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objGnrlFnction.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt
                            If gvKeywords.Rows.Count > 0 Then

                                For k = 0 To gvKeywords.Rows.Count - 1
                                    txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
                                    If txtKeywords.Text <> "" Then
                                        sKeywords = sKeywords & "," & txtKeywords.Text
                                    End If
                                Next
                            End If
                            If sKeywords.StartsWith(",") = True Then
                                sKeywords = sKeywords.Remove(0, 1)
                            End If
                            If sKeywords.EndsWith(",") = True Then
                                sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
                            End If
                            objIndex.sPGEKeyWORD = objClsFASGnrl.SafeSQL(sKeywords)
                            objIndex.sPGEOCRText = ""
                            objIndex.iPGESIZE = 0
                            objIndex.iPGECURRENT_VER = 0
                            Select Case UCase(sPageExt)
                                Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                                    objIndex.sPGEOBJECT = "IMAGE"
                                Case Else
                                    objIndex.sPGEOBJECT = "OLE"
                            End Select
                            objIndex.sPGESTATUS = "A"
                            objIndex.iPGESubCabinet = iSubCabinet
                            objIndex.iPgeUpdatedBy = sSession.UserID

                            objIndex.spgeDelflag = "A"
                            objIndex.iPGEQCUsrGrpId = 0
                            objIndex.sPGEFTPStatus = "F"
                            objIndex.iPGEbatchname = objIndex.iPGEBASENAME
                            objIndex.spgeOrignalFileName = objClsFASGnrl.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
                            objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)

                            If gvDocumentType.Rows.Count > 0 Then
                                For j = 0 To gvDocumentType.Rows.Count - 1
                                    lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                                    txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                                    If objIndex.iPGEBASENAME = iPageDetailsid Then
                                        objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If Arr(0) = "3" Then
                        lblUserMasterDetailsValidationMsg.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AutomaticIndexing")
        End Try
    End Sub
    Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            If sFileInDB = "FALSE" Then
                sImagePath = objIndex.GetImagePath(sSession.AccessCode)
                sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
                objGnrlFnction.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
                sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                If System.IO.File.Exists(sImagePath) = False Then
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub btnAttch_Click(sender As Object, e As EventArgs) Handles btnAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Try
            lblError.Text = "" : iDocID = 0

            If ddlExistingOrder.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Existing Payment No."
                ddlExistingOrder.Focus()
                Exit Sub
            End If

            Dim hfc As HttpFileCollection = Request.Files

            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dt.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        dt = Session("Attachment")
                        If dt.Rows.Count = 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objGnrlFnction.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)

                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        ElseIf dt.Rows.Count > 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objGnrlFnction.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        End If
                    End If
                Next
            End If

            If dt.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            End If

            Session("Attachment") = dt
            gvattach.DataSource = dt
            gvattach.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub gvattach_PreRender(sender As Object, e As EventArgs) Handles gvattach.PreRender
        Try
            If gvattach.Rows.Count > 0 Then
                gvattach.UseAccessibleHeader = True
                gvattach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvattach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach_PreRender")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Public Sub GetAttachFile(ByVal sTrNo As String)
        Dim dRow As DataRow
        Dim dt, dt1 As New DataTable
        Try
            dt.Columns.Add("FilePath")
            dt.Columns.Add("FileName")
            dt.Columns.Add("Extension")
            dt.Columns.Add("CreatedOn")

            dt1 = objPO.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("FilePath") = ""
                    dRow("FileName") = dt1.Rows(i)("pge_Orignalfilename")
                    dRow("Extension") = dt1.Rows(i)("pge_ext")
                    dRow("CreatedOn") = objClsFASGnrl.FormatDtForRDBMS(dt1.Rows(i)("pge_createdon"), "D")
                    dt.Rows.Add(dRow)
                Next
            End If

            gvattach.DataSource = dt
            gvattach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        Dim dt As New DataTable
        Dim dOrderDate As Date
        Try
            If txtRate.Text <> "" Then
                dOrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dt = objPO.CheckHistoryID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text, dOrderDate, Trim(txtRate.Text))

                txtHistoryID.Text = dt.Rows(0)(0).ToString()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtRate_TextChanged")
        End Try
    End Sub
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim iCabinetID, iSubCabinetID, iFolderID As Integer
        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Dim dt As New DataTable
        Try
            If ddlExistingOrder.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAccBrnch.SelectedItem.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Payment Voucher")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlExistingOrder.SelectedItem.Text)

                    dt = objPO.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            sSelectedChecksIDs = sSelectedChecksIDs & "," & dt.Rows(i)("PGE_BASENAME")
                        Next
                    End If

                    If sSelectedChecksIDs.StartsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                    End If
                    If sSelectedChecksIDs.EndsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(Len(sSelectedChecksIDs) - 1, 1)
                    End If

                    oSelectedCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iCabinetID))
                    oSelectedSubCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iSubCabinetID))
                    oSelectedFolID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iFolderID))
                    oSelectedChecksIDs = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedChecksIDs))
                    oSelectedIndexID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(0))

                    Response.Redirect(String.Format("~/Viewer/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={7}&SelectedDescID={8}&SelectedFrmtID={9}&SelectedCrByID={10}&SelectedIndexID={11}", "", "", oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, "", "", "", "", "", oSelectedIndexID), False)
                Else
                    lblError.Text = "No Attachments to view"
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Existing Payment No"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnView_Click")
        End Try
    End Sub
    Private Sub ddlCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCurrency.SelectedIndexChanged
        Dim iBaseID As Integer, iCurrID As Integer
        Try
            lblError.Text = ""
            If ddlCurrency.SelectedIndex > 0 Then
                iBaseID = objPO.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
                If ddlCurrency.SelectedValue = iBaseID Then
                Else
                    iCurrID = objPO.GetFEID(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                    If iCurrID = 0 Then
                        lblError.Text = "Please set the exchange rates in Currency Master."
                        lblUserMasterDetailsValidationMsg.Text = "Please set the exchange rates in Currency Master."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCurrency_SelectedIndexChanged")
        End Try
    End Sub

End Class
