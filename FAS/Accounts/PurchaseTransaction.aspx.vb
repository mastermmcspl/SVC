Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Windows.Forms
Imports DatabaseLayer
Partial Class Accounts_PurchaseTransaction
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts_PurchaseTransaction"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objPSJEDetails As New ClsPurchaseSalesJEDetails
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objJE As New clsJournalEntry
    Private Shared sSession As AllSession
    Public dtMerge As New DataTable
    Private Shared iDbOrCr As Integer = 0
    Dim objPurchase As New clsPurchaseVoucher
    Dim objPayment As New clsPayment
    Dim dt As New DataTable
    Private objAccSetting As New clsAccountSetting
    Dim objDb As New DBHelper

    Private Shared sTypeName As String
    Private Shared sMasterName As String
    Private Shared iMasterID As Integer
    Private Shared iPKID As Integer
    Private Shared sTableName As String
    Private Shared sGMSave As String
    Private Shared sGMFlag As String
    Private Shared sGMBackStatus As String
    Private Shared dVATAmt As Double
    Private Shared dVATAmtResult As Double
    Private Shared dChangedAmt As Double
    Private Shared sUMBackStatus As String
    Private Shared dtCOA As New DataTable
    Private Shared sPTSave As String
    Private objclsModulePermission As New clsModulePermission
    Public dtPurchase As New DataTable
    Private objSettings As New clsApplicationSettings
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
        imgbtnApprove.ImageUrl = "~/Images/CheckMark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterType As String = ""
        Dim sMasterID As String = ""
        Dim taxcategory As String
        Dim sFormButtons As String = ""
        Dim iDefaultBranch As Integer
        'Dim iSYear As Integer : Dim iEYear As Integer
        'Dim dStartDate As Date : Dim dEndDate As Date
        'Dim sArray() As String : Dim sStr As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PT")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnApprove.Visible = False
                imgbtnAddCharge.Visible = False : btnItemAdd.Visible = False : sPTSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        imgbtnAddCharge.Visible = True : btnItemAdd.Visible = True
                        sPTSave = "YES"
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

                'dStartDate = objGenFun.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'dEndDate = objGenFun.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                '    cclReceiptDate.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    cclReceiptDate.EndDate = New DateTime(iEYear, 3, dEndDate)

                '    cclBillDate.StartDate = New DateTime(iSYear, 4, dStartDate)
                '    cclBillDate.EndDate = New DateTime(iEYear, 3, dEndDate)
                'End If

                'rgvtxtReceiptDate.ErrorMessage = "Please enter date between " & dStartDate & " to " & dEndDate & " "
                'rgvtxtReceiptDate.MinimumValue = "" & dStartDate & ""
                'rgvtxtReceiptDate.MaximumValue = "" & dEndDate & ""

                'txtStartDate.Text = objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                'txtEndDate.Text = objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                LoadCommodity() : LoadCurrencyType() : GetAppSettings()

                imgbtnSave.ImageUrl = "~/Images/Save24.png"
                imgbtnSave.ToolTip = "Save"

                dgItems.DataSource = Nothing
                dgItems.DataBind()

                dgJEDetails.DataSource = Nothing
                dgJEDetails.DataBind()

                BindBranch() : BindCompanyType()

                dt = objPurchase.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("CUST_COMM_Address")) = True Or IsDBNull(dt.Rows(0)("CUST_ProvisionalNo")) = True Then
                        lblError.Text = "Fill the details in Company Master"
                        Exit Sub
                    End If
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    txtReceiveAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtReceiveGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    taxcategory = objPurchase.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CUST_TAXPayableCategory"))
                    If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                        txtReceiveGSTNRegNo.Enabled = False
                    Else
                        txtReceiveGSTNRegNo.Enabled = True
                    End If
                End If

                dt = Nothing
                Session("dtPayment") = Nothing : dtPurchase = Nothing : DivAccountsDetails.Visible = False

                lblStatus.Text = "Not Started."
                dtCOA = objPayment.GetchartofAccounts(sSession.AccessCode, sSession.AccessCodeID)

                txtBillAmount.Text = "0" : txtPaidAmount.Text = "0" : txtOtherCharge.Text = "0"

                LoadZone()
                LoadRegion(0)
                LoadArea(0)
                LoadAccBrnch(0)

                iDefaultBranch = objPurchase.GetDefaultBranch(sSession.AccessCode, sSession.AccessCodeID)
                If iDefaultBranch > 0 Then
                    ddlAccBrnch.SelectedValue = iDefaultBranch
                    ddlAccBrnch_SelectedIndexChanged(sender, e)
                End If

                LoadSupplier()
                LoadExistingPurchaseVoucher()
                LoadChargeType()
                Session("ChargesMaster") = Nothing

                txtTransactionNo.Text = objPurchase.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)

                RFVParty.InitialValue = "Select Supplier" : RFVParty.ErrorMessage = "Select Supplier"
                RFVBillNo.ErrorMessage = "Enter Valid Bill Number."

                REFBillDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                RFVBillDate.ErrorMessage = "Enter Valid Date Format."

                REVReceiptDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                RFVReceiptDate.ErrorMessage = "Enter Valid Date Format."

                'RFVEBillAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                'RFVEBillAmount.ErrorMessage = "Enter Valid Bill Amount."

                RFVAccZone.InitialValue = "--- Select Zone ---" : RFVAccZone.ErrorMessage = "Select Zone."
                RFVAccRgn.InitialValue = "--- Select Region ---" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "--- Select Area ---" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "--- Select Branch ---" : RFVAccBrnch.ErrorMessage = "Select Branch."
                RFVddlCommodity.InitialValue = "Select Commodity" : RFVddlCommodity.ErrorMessage = "Select Commodity"
                'RFVddlBranch.InitialValue = "Select Branch" : RFVddlBranch.ErrorMessage = "Select Branch"

                Me.chkCategory.Attributes.Add("OnClick", "return ValidateList()")

                Me.txtRate.Attributes.Add("onblur", "return ValidateRate()")

                Me.txtQuantity.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtDiscount.Attributes.Add("onblur", "return CalculateQuantity()")
                Me.txtGSTRate.Attributes.Add("onblur", "return CalculateQuantity()")

                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    ddlExistPurchase.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlExistPurchase_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sUMBackStatus = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
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
    Private Sub LoadChargeType()
        Try
            ddlChargeType.DataSource = objPurchase.LoadChargeType(sSession.AccessCode, sSession.AccessCodeID)
            ddlChargeType.DataTextField = "Mas_desc"
            ddlChargeType.DataValueField = "Mas_id"
            ddlChargeType.DataBind()
            ddlChargeType.Items.Insert(0, "Select Charge Type")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSupplier()
        Try
            ddlParty.DataSource = objJE.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "Name"
            ddlParty.DataValueField = "CSM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Supplier")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadExistingPurchaseVoucher()
        Try
            ddlExistPurchase.DataSource = objPurchase.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0)
            ddlExistPurchase.DataTextField = "Acc_Purchase_TransactionNo"
            ddlExistPurchase.DataValueField = "Acc_Purchase_ID"
            ddlExistPurchase.DataBind()
            ddlExistPurchase.Items.Insert(0, "Existing Purchase Voucher")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExistPurchase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistPurchase.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistPurchase.SelectedIndex > 0 Then
                imgbtnSave.ImageUrl = "~/Images/Update24.png"
                imgbtnSave.ToolTip = "Update"
                BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistPurchase.SelectedValue)
                'BindExeVoucherDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistPurchase.SelectedValue)
            Else
                imgbtnSave.ImageUrl = "~/Images/Save24.png"
                imgbtnSave.ToolTip = "Save"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistPurchase_SelectedIndexChanged")
        End Try
    End Sub
    'Public Sub BindExeVoucherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPayment As Integer)
    '    Dim dt As New DataTable
    '    Try
    '        dt = objPurchase.LoadPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPayment)
    '        Session("dtPurchase") = dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub BindTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPayment As Integer)
        Dim dtNew As New DataTable, dtDetails As New DataTable
        Dim dPaidAmount As Double
        Dim dtCharge As New DataTable
        Dim dtItems As New DataTable
        Dim iCurrnecy As Integer = 0
        Try
            ddlCurrency.Enabled = True
            dtNew = objPurchase.GetPurchaseDetails(sNameSpace, iCompID, iPayment)
            If dtNew.Rows.Count > 0 Then
                If IsDBNull(dtNew.Rows(0)("Acc_Purchase_TransactionNo").ToString()) = False Then
                    txtTransactionNo.Text = dtNew.Rows(0)("Acc_Purchase_TransactionNo").ToString()
                Else
                    txtTransactionNo.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Purchase_Party").ToString()) = False Then
                    ddlParty.SelectedValue = dtNew.Rows(0)("Acc_Purchase_Party").ToString()
                Else
                    ddlParty.SelectedIndex = 0
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Purchase_BillNo").ToString()) = False Then
                    txtBillNo.Text = dtNew.Rows(0)("Acc_Purchase_BillNo").ToString()
                Else
                    txtBillNo.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Purchase_ReceiptDate").ToString()) = False Then
                    txtReceiptDate.Text = objGen.FormatDtForRDBMS(dtNew.Rows(0)("Acc_Purchase_ReceiptDate").ToString(), "D")
                Else
                    txtReceiptDate.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Purchase_BillDate").ToString()) = False Then
                    txtBillDate.Text = objGen.FormatDtForRDBMS(dtNew.Rows(0)("Acc_Purchase_BillDate").ToString(), "D")
                Else
                    txtBillDate.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Purchase_BillAmount").ToString()) = False Then
                    txtBillAmount.Text = Convert.ToDecimal(dtNew.Rows(0)("Acc_Purchase_BillAmount").ToString()).ToString("#,##0.00")
                Else
                    txtBillAmount.Text = ""
                End If

                If IsDBNull(dtNew.Rows(0)("Acc_Purchase_OtherCharges").ToString()) = False Then
                    txtOtherCharge.Text = Convert.ToDecimal(dtNew.Rows(0)("Acc_Purchase_OtherCharges").ToString()).ToString("#,##0.00")
                Else
                    txtOtherCharge.Text = 0
                End If

                'dPaidAmount = Convert.ToDecimal(objPurchase.GetPaidAmount(sSession.AccessCode, sSession.AccessCodeID, iPayment)).ToString("#,##0.00")
                'txtPaidAmount.Text = dPaidAmount + Convert.ToDouble(txtOtherCharge.Text)
                'txtPaidAmount.Text = dPaidAmount
                txtPaidAmount.Text = 0
                If IsDBNull(dtNew.Rows(0)("Acc_Purchase_DelFlag").ToString()) = False Then
                    ddlCurrency.Enabled = False
                    If dtNew.Rows(0)("Acc_Purchase_DelFlag").ToString() = "W" Then
                        lblStatus.Text = "Waiting for Submission"
                    ElseIf dtNew.Rows(0)("Acc_Purchase_DelFlag").ToString() = "D" Then
                        lblStatus.Text = "De-Activated"
                    ElseIf dtNew.Rows(0)("Acc_Purchase_DelFlag").ToString() = "A" Then
                        lblStatus.Text = "Activated"
                    End If
                End If
                If dtNew.Rows(0)("Acc_Purchase_Status").ToString() = "S" Then
                    lblStatus.Text = "Submitted"
                End If

                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_ZoneID").ToString()) = False Then
                    If (dtNew.Rows(0)("ACC_Purchase_ZoneID").ToString() = "") Then
                    Else
                        ddlAccZone.SelectedValue = dtNew.Rows(0)("ACC_Purchase_ZoneID").ToString()
                        LoadRegion(ddlAccZone.SelectedValue)
                    End If
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_RegionID").ToString()) = False Then
                    If (dtNew.Rows(0)("ACC_Purchase_RegionID").ToString() = "") Then
                    Else
                        ddlAccRgn.SelectedValue = dtNew.Rows(0)("ACC_Purchase_RegionID").ToString()
                        LoadArea(ddlAccRgn.SelectedValue)
                    End If
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_AreaID").ToString()) = False Then
                    If (dtNew.Rows(0)("ACC_Purchase_AreaID").ToString() = "") Then
                    Else
                        ddlAccArea.SelectedValue = dtNew.Rows(0)("ACC_Purchase_AreaID").ToString()
                        LoadAccBrnch(ddlAccArea.SelectedValue)
                    End If
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_BranchID").ToString()) = False Then
                    If (dtNew.Rows(0)("ACC_Purchase_BranchID").ToString() = "") Then
                    Else
                        ddlAccBrnch.SelectedValue = dtNew.Rows(0)("ACC_Purchase_BranchID").ToString()
                    End If
                End If

                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_CompanyType")) = False Then
                    ddlCompanyType.SelectedValue = dtNew.Rows(0)("ACC_Purchase_CompanyType")
                Else
                    ddlCompanyType.SelectedIndex = 0
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_GSTNCategory")) = False Then
                    BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    ddlGSTCategory.SelectedValue = dtNew.Rows(0)("ACC_Purchase_GSTNCategory")
                Else
                    ddlGSTCategory.SelectedIndex = 0
                End If

                Dim description As String
                description = objPurchase.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                    txtDeliveryFromGSTNRegNo.Enabled = False
                Else
                    txtDeliveryFromGSTNRegNo.Enabled = True
                End If

                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_CompanyAddress")) = False Then
                    txtCompanyAddress.Text = dtNew.Rows(0)("ACC_Purchase_CompanyAddress")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_BillingAddress")) = False Then
                    txtBillingAddress.Text = dtNew.Rows(0)("ACC_Purchase_BillingAddress")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_DeliveryFrom")) = False Then
                    txtDeliveryFromAddress.Text = dtNew.Rows(0)("ACC_Purchase_DeliveryFrom")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_ReceiveAddress")) = False Then
                    txtReceiveAddress.Text = dtNew.Rows(0)("ACC_Purchase_ReceiveAddress")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_CompanyGSTNRegNo")) = False Then
                    txtCompanyGSTNRegNo.Text = dtNew.Rows(0)("ACC_Purchase_CompanyGSTNRegNo")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_BillingGSTNRegNo")) = False Then
                    txtBillingGSTNRegNo.Text = dtNew.Rows(0)("ACC_Purchase_BillingGSTNRegNo")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_DeliveryFromGSTNRegNo")) = False Then
                    txtDeliveryFromGSTNRegNo.Text = dtNew.Rows(0)("ACC_Purchase_DeliveryFromGSTNRegNo")
                End If
                If IsDBNull(dtNew.Rows(0)("ACC_Purchase_ReceiveGSTNRegNo")) = False Then
                    txtReceiveGSTNRegNo.Text = dtNew.Rows(0)("ACC_Purchase_ReceiveGSTNRegNo")
                End If

                If dtNew.Rows(0)("Acc_Purchase_PaymentStatus").ToString() = "P" Then
                    lblError.Text = "Cannot Update the Bill. Payment Already Paid."
                    Exit Sub
                End If


            End If
            iCurrnecy = objPurchase.GetCurrency(sNameSpace, iCompID, sSession.YearID, ddlExistPurchase.SelectedValue)
            If iCurrnecy > 0 Then
                ddlCurrency.SelectedValue = iCurrnecy
            End If
            DivAccountsDetails.Visible = True
            GvAccountDetails.DataSource = objPurchase.GetBillAccountsDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistPurchase.SelectedValue, dtCOA)
            GvAccountDetails.DataBind()

            dtCharge = objPurchase.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPurchase.SelectedValue, 8)
            GvCharge.DataSource = dtCharge
            GvCharge.DataBind()
            Session("ChargesMaster") = dtCharge

            dtItems = objPurchase.BindItemsData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPurchase.SelectedValue)
            Session("dtPurchase") = dtItems
            dgItems.DataSource = dtItems
            dgItems.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub OnConfirm(sender As Object, e As EventArgs)
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('You clicked YES!')", True)
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('You clicked NO!')", True)
        End If
    End Sub

    'Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
    '    Dim Arr() As String
    '    Dim i As Integer = 0
    '    Dim iBillID As Integer = 0
    '    Dim dTotalAmount As Double = 0
    '    Dim iFlag As Integer = 0
    '    Dim dTaxAmount As Double = 0
    '    Dim dDate, dSDate As Date : Dim m As Integer

    '    Try
    '        '  Dim result As New DialogResult
    '        lblError.Text = ""
    '        If ddlAccBrnch.SelectedIndex = 0 Then
    '            lblError.Text = "Select Branch."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
    '            Exit Sub
    '        End If

    '        'Cheque Date Comparision'
    '        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m < 0 Then
    '            lblError.Text = "Date (" & txtReceiptDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtReceiptDate.Focus()
    '            Exit Sub
    '        End If

    '        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m > 0 Then
    '            lblError.Text = "Date (" & txtReceiptDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtReceiptDate.Focus()
    '            Exit Sub
    '        End If
    '        'Cheque Date Comparision'

    '        'Cheque Date Comparision'
    '        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m < 0 Then
    '            lblError.Text = "Supplier Invoice Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtBillDate.Focus()
    '            Exit Sub
    '        End If

    '        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m > 0 Then
    '            lblError.Text = "Supplier Invoice Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtBillDate.Focus()
    '            Exit Sub
    '        End If
    '        'Cheque Date Comparision'

    '        If dgPurchase.Items.Count = 0 Then
    '            lblError.Text = "Add Amount"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Amount','', 'success');", True)
    '            Exit Sub
    '        End If

    '        For i = 0 To dgPurchase.Items.Count - 1
    '            dTotalAmount = dTotalAmount + Convert.ToDouble(dgPurchase.Items(i).Cells(13).Text)
    '        Next

    '        If txtBillAmount.Text = "0" Then
    '            lblError.Text = "Enter Bill Amount"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Bill Amount','', 'success');", True)
    '            Exit Sub
    '        End If

    '        If Math.Round(Convert.ToDouble(txtBillAmount.Text)) = Math.Round(dTotalAmount) = False Then
    '            iFlag = 1
    '            'lblError.Text = "Amount not matched with bill amount"
    '            ''ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not matched with Bill Amount','', 'success');", True)
    '            '' Exit Sub

    '            'result = MessageBox.Show("Amount not matched with bill amount.Do you want to Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    '            'If result = DialogResult.Yes Then
    '            '    GoTo SaveResult
    '            'ElseIf result = DialogResult.No Then
    '            '    Exit Sub
    '            'End If
    '        End If

    '        'SaveResult:
    '        objPurchase.iAcc_Purchase_ID = 0
    '        objPurchase.sAcc_Purchase_TransactionNo = txtTransactionNo.Text
    '        objPurchase.iAcc_Purchase_Party = ddlParty.SelectedValue
    '        objPurchase.sAcc_Purchase_BillNo = txtBillNo.Text
    '        objPurchase.dAcc_Purchase_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objPurchase.dAcc_Purchase_ReceiptDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objPurchase.dAcc_Purchase_BillAmount = txtBillAmount.Text
    '        objPurchase.iAcc_Purchase_CreatedBy = sSession.UserID
    '        objPurchase.iAcc_Purchase_Year = sSession.YearID
    '        objPurchase.iAcc_Purchase_CompID = sSession.AccessCodeID
    '        objPurchase.sAcc_Purchase_Status = "C"
    '        objPurchase.sAcc_Purchase_DelFlag = "W"
    '        objPurchase.sAcc_Purchase_Operation = "C"
    '        objPurchase.sAcc_Purchase_IPAddress = sSession.IPAddress
    '        objPurchase.iAcc_Purchase_MisMatchFlag = iFlag
    '        objPurchase.sAcc_Purchase_PaymentStatus = "N"  'Nil

    '        If txtOtherCharge.Text = "" Then
    '            objPurchase.dAcc_Purchase_OtherCharges = "0"
    '        Else
    '            objPurchase.dAcc_Purchase_OtherCharges = txtOtherCharge.Text
    '        End If

    '        objPurchase.iACC_Purchase_ZoneID = ddlAccZone.SelectedValue
    '        objPurchase.iACC_Purchase_RegionID = ddlAccRgn.SelectedValue
    '        objPurchase.iACC_Purchase_AreaID = ddlAccArea.SelectedValue
    '        objPurchase.iACC_Purchase_BranchID = ddlAccBrnch.SelectedValue

    '        Arr = objPurchase.SavePurchaseVoucher(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
    '        iBillID = Arr(1)

    '        For i = 0 To dgPurchase.Items.Count - 1
    '            objPurchase.iATD_ID = 0
    '            objPurchase.dATD_TransactionDate = Date.Today
    '            objPurchase.iATD_TrType = 8  'Purchase Voucher
    '            objPurchase.iATD_BillID = iBillID
    '            objPurchase.iATD_PaymentType = dgPurchase.Items(i).Cells(4).Text   'Tax Type
    '            objPurchase.iATD_Head = dgPurchase.Items(i).Cells(1).Text
    '            objPurchase.iATD_GL = dgPurchase.Items(i).Cells(2).Text
    '            objPurchase.iATD_SubGL = dgPurchase.Items(i).Cells(3).Text
    '            objPurchase.iATD_DbOrCr = 1

    '            If dgPurchase.Items(i).Cells(12).Text <> "" Then
    '                dTaxAmount = dTaxAmount + dgPurchase.Items(i).Cells(12).Text
    '            End If
    '            objPurchase.dATD_Debit = dgPurchase.Items(i).Cells(12).Text

    '            objPurchase.dATD_Credit = 0
    '            objPurchase.iATD_CreatedBy = sSession.UserID
    '            objPurchase.sATD_Status = "A"
    '            objPurchase.iATD_YearID = sSession.YearID
    '            objPurchase.iATD_CompID = sSession.AccessCodeID
    '            objPurchase.sATD_Operation = "C"
    '            objPurchase.sATD_IPAddress = sSession.IPAddress
    '            Arr = objPurchase.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
    '        Next

    '        SavePurhcaseDetails(iBillID)
    '        SaveOtherDetails(iBillID, dTaxAmount)

    '        SaveCharges(iBillID)

    '        LoadExistingPurchaseVoucher()
    '        ddlExistPurchase.SelectedValue = iBillID
    '        ddlExistPurchase_SelectedIndexChanged(sender, e)
    '        imgbtnSave.Visible = False : imgbtnUpdate.Visible = True

    '        If Arr(0) = "2" Then
    '            lblError.Text = "Successfully Updated."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

    '        ElseIf Arr(0) = "3" Then
    '            lblError.Text = "Successfully Saved & Waiting for Approval."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
    '        End If
    '        lblStatus.Text = "Waiting for Approval"
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
    '    End Try
    'End Sub
    'Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
    '    Dim Arr() As String
    '    Dim i As Integer = 0
    '    Dim iBillID As Integer = 0
    '    Dim dTotalAmount As Double = 0, dTaxAmount As Double = 0
    '    Dim iFlag As Integer = 0
    '    Dim dDate, dSDate As Date : Dim m As Integer

    '    Try
    '        lblError.Text = ""
    '        If ddlAccBrnch.SelectedIndex = 0 Then
    '            lblError.Text = "Select Branch."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
    '            Exit Sub
    '        End If

    '        'Cheque Date Comparision'
    '        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m < 0 Then
    '            lblError.Text = "Date (" & txtReceiptDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtReceiptDate.Focus()
    '            Exit Sub
    '        End If

    '        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m > 0 Then
    '            lblError.Text = "Date (" & txtReceiptDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtReceiptDate.Focus()
    '            Exit Sub
    '        End If
    '        'Cheque Date Comparision'

    '        'Cheque Date Comparision'
    '        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m < 0 Then
    '            lblError.Text = "Supplier Invoice Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtBillDate.Focus()
    '            Exit Sub
    '        End If

    '        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m > 0 Then
    '            lblError.Text = "Supplier Invoice Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtBillDate.Focus()
    '            Exit Sub
    '        End If
    '        'Cheque Date Comparision'

    '        If dgPurchase.Items.Count = 0 Then
    '            lblError.Text = "Add Amount"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Amount','', 'success');", True)
    '            Exit Sub
    '        End If

    '        For i = 0 To dgPurchase.Items.Count - 1
    '            dTotalAmount = dTotalAmount + Convert.ToDouble(dgPurchase.Items(i).Cells(13).Text)
    '        Next

    '        If txtBillAmount.Text = "0" Then
    '            lblError.Text = "Enter Bill Amount"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Bill Amount','', 'success');", True)
    '            Exit Sub
    '        End If

    '        If Math.Round(Convert.ToDouble(txtBillAmount.Text)) = Math.Round(dTotalAmount) = False Then
    '            iFlag = 1
    '            'lblError.Text = "Amount not matched with bill amount"
    '            ''ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not matched with Bill Amount','', 'success');", True)
    '            '' Exit Sub

    '            'result = MessageBox.Show("Amount not matched with bill amount.Do you want to Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    '            'If result = DialogResult.Yes Then
    '            '    GoTo SaveResult
    '            'ElseIf result = DialogResult.No Then
    '            '    Exit Sub
    '            'End If
    '        End If

    '        objPurchase.iAcc_Purchase_ID = ddlExistPurchase.SelectedValue
    '        objPurchase.sAcc_Purchase_TransactionNo = txtTransactionNo.Text
    '        objPurchase.iAcc_Purchase_Party = ddlParty.SelectedValue
    '        objPurchase.sAcc_Purchase_BillNo = txtBillNo.Text
    '        objPurchase.dAcc_Purchase_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objPurchase.dAcc_Purchase_BillAmount = txtBillAmount.Text
    '        objPurchase.iAcc_Purchase_CreatedBy = sSession.UserID
    '        objPurchase.iAcc_Purchase_Year = sSession.YearID
    '        objPurchase.iAcc_Purchase_CompID = sSession.AccessCodeID
    '        objPurchase.sAcc_Purchase_Status = "U"
    '        objPurchase.sAcc_Purchase_DelFlag = "A"
    '        objPurchase.sAcc_Purchase_Operation = "U"
    '        objPurchase.sAcc_Purchase_IPAddress = sSession.IPAddress
    '        objPurchase.sAcc_Purchase_PaymentStatus = "N"  'Nil

    '        If txtOtherCharge.Text = "" Then
    '            objPurchase.dAcc_Purchase_OtherCharges = "0"
    '        Else
    '            objPurchase.dAcc_Purchase_OtherCharges = txtOtherCharge.Text
    '        End If
    '        If txtTradeDiscountAmt.Text = "" Then
    '            objPurchase.dAcc_Purchase_TradeDiscount = "0"
    '        Else
    '            objPurchase.dAcc_Purchase_TradeDiscount = txtTradeDiscountAmt.Text
    '        End If

    '        objPurchase.iACC_Purchase_ZoneID = ddlAccZone.SelectedValue
    '        objPurchase.iACC_Purchase_RegionID = ddlAccRgn.SelectedValue
    '        objPurchase.iACC_Purchase_AreaID = ddlAccArea.SelectedValue
    '        objPurchase.iACC_Purchase_BranchID = ddlAccBrnch.SelectedValue

    '        Arr = objPurchase.SavePurchaseVoucher(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
    '        iBillID = Arr(1)

    '        If Arr(0) = "2" Then
    '            lblError.Text = "Successfully Updated."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

    '        ElseIf Arr(0) = "3" Then
    '            lblError.Text = "Successfully Saved & Waiting for Approval."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
    '        End If

    '        objPurchase.DeleteTransactionPurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, iBillID)
    '        objPurchase.DeletePurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, iBillID)

    '        For i = 0 To dgPurchase.Items.Count - 1
    '            objPurchase.iATD_ID = 0
    '            objPurchase.dATD_TransactionDate = Date.Today
    '            objPurchase.iATD_TrType = 8  'Purchase Voucher
    '            objPurchase.iATD_BillID = iBillID
    '            objPurchase.iATD_PaymentType = dgPurchase.Items(i).Cells(4).Text
    '            objPurchase.iATD_Head = dgPurchase.Items(i).Cells(1).Text
    '            objPurchase.iATD_GL = dgPurchase.Items(i).Cells(2).Text
    '            objPurchase.iATD_SubGL = dgPurchase.Items(i).Cells(3).Text
    '            objPurchase.iATD_DbOrCr = 1


    '            If dgPurchase.Items(i).Cells(12).Text <> "" Then
    '                dTaxAmount = dTaxAmount + dgPurchase.Items(i).Cells(12).Text
    '            End If
    '            objPurchase.dATD_Debit = dgPurchase.Items(i).Cells(12).Text
    '            objPurchase.dATD_Credit = 0
    '            objPurchase.iATD_CreatedBy = sSession.UserID
    '            objPurchase.sATD_Status = "A"
    '            objPurchase.iATD_YearID = sSession.YearID
    '            objPurchase.iATD_CompID = sSession.AccessCodeID
    '            objPurchase.sATD_Operation = "U"
    '            objPurchase.sATD_IPAddress = sSession.IPAddress
    '            Arr = objPurchase.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
    '        Next

    '        SavePurhcaseDetails(iBillID)
    '        SaveOtherDetails(iBillID, dTaxAmount)
    '        SaveCharges(iBillID)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
    '    End Try
    'End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object
        Try
            lblError.Text = ""

            If lblStatus.Text = "Waiting for Submission" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf lblStatus.Text = "De-Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf lblStatus.Text = "Activated" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Not Started." Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf lblStatus.Text = "Submitted" Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            End If

            Response.Redirect(String.Format("~/Accounts/PurchaseTransactionDashboard.aspx?StatusID={0}", oStatusID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Function BuildTable() As DataTable
        Dim dc As DataColumn
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("CommodityID", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("GoodsID", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitID", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HSNCode", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Commodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Goods", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Unit", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MRP", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Qty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RateAMt", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Discount", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("DiscountAmt", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Charge", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Total", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("GST", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("GSTAmt", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("NetAmount", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("CCurrency", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("FETotalAmount", GetType(String))
            dtTab.Columns.Add(dc)
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Private Sub SavePurhcaseDetails(ByVal iBillID As Integer)
    '    Dim Arr() As String
    '    Try
    '        For i = 0 To dgPurchase.Items.Count - 1
    '            objPurchase.iAcc_PMD_ID = 0
    '            objPurchase.iAcc_PMD_MasterID = iBillID
    '            objPurchase.iAcc_PMD_Head = dgPurchase.Items(i).Cells(1).Text
    '            objPurchase.iAcc_PMD_GL = dgPurchase.Items(i).Cells(2).Text
    '            objPurchase.iAcc_PMD_SubGL = dgPurchase.Items(i).Cells(3).Text
    '            objPurchase.dAcc_PMD_Amount = dgPurchase.Items(i).Cells(6).Text
    '            objPurchase.iAcc_PMD_TaxType = dgPurchase.Items(i).Cells(4).Text
    '            objPurchase.iAcc_PMD_TaxRate = dgPurchase.Items(i).Cells(5).Text
    '            objPurchase.dAcc_PMD_TaxAmount = dgPurchase.Items(i).Cells(12).Text
    '            objPurchase.dAcc_PMD_TotalAmount = dgPurchase.Items(i).Cells(13).Text
    '            objPurchase.dAcc_PMD_PendingAmount = dgPurchase.Items(i).Cells(13).Text
    '            objPurchase.sAcc_PMD_RoundOff = dgPurchase.Items(i).Cells(14).Text
    '            objPurchase.sAcc_PMD_Status = "A"
    '            objPurchase.iAcc_PMD_CompID = sSession.AccessCodeID
    '            objPurchase.iAcc_PMD_YearID = sSession.YearID

    '            objPurchase.dAcc_PMD_TradeDis = dgPurchase.Items(i).Cells(7).Text
    '            objPurchase.dAcc_PMD_TradeDisAmt = dgPurchase.Items(i).Cells(8).Text
    '            objPurchase.dAcc_PMD_NetAmount = dgPurchase.Items(i).Cells(9).Text

    '            Arr = objPurchase.SavePurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
    '        Next
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub SaveOtherDetails(ByVal iBillID As Integer, ByVal dTaxAmount As Double)
        Dim Arr() As String
        Dim iSubGL As Integer = 0
        Try
            Dim dtDGL As New DataTable
            Dim DVGLCODE As New DataView(dtCOA)
            iSubGL = objPurchase.GetCustomerGLID(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
            DVGLCODE.RowFilter = "Gl_id=" & iSubGL
            dtDGL = DVGLCODE.ToTable

            'Bill Amount
            objPurchase.iATD_ID = 0
            objPurchase.dATD_TransactionDate = Date.Today
            objPurchase.iATD_TrType = 8  'Purchase Voucher
            objPurchase.iATD_BillID = iBillID
            objPurchase.iATD_PaymentType = 0
            objPurchase.iATD_Head = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_Head")
            objPurchase.iATD_GL = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_GL")
            objPurchase.iATD_SubGL = objPSJEDetails.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase", "Acc_SubGL")
            objPurchase.iATD_DbOrCr = 1
            objPurchase.dATD_Debit = Convert.ToDouble(txtBillAmount.Text) - dTaxAmount
            objPurchase.dATD_Credit = 0
            objPurchase.iATD_CreatedBy = sSession.UserID
            objPurchase.sATD_Status = "A"
            objPurchase.iATD_YearID = sSession.YearID
            objPurchase.iATD_CompID = sSession.AccessCodeID
            objPurchase.sATD_Operation = "C"
            objPurchase.sATD_IPAddress = sSession.IPAddress
            Arr = objPurchase.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPurchase)


            'Customer
            objPurchase.iATD_ID = 0
            objPurchase.dATD_TransactionDate = Date.Today
            objPurchase.iATD_TrType = 8  'Purchase Voucher
            objPurchase.iATD_BillID = iBillID
            objPurchase.iATD_PaymentType = 0
            objPurchase.iATD_Head = dtDGL.Rows(0)("gl_AccHead")
            objPurchase.iATD_GL = dtDGL.Rows(0)("gl_Parent")
            objPurchase.iATD_SubGL = iSubGL
            objPurchase.iATD_DbOrCr = 2
            objPurchase.dATD_Debit = 0
            objPurchase.dATD_Credit = txtBillAmount.Text
            objPurchase.iATD_CreatedBy = sSession.UserID
            objPurchase.sATD_Status = "A"
            objPurchase.iATD_YearID = sSession.YearID
            objPurchase.iATD_CompID = sSession.AccessCodeID
            objPurchase.sATD_Operation = "C"
            objPurchase.sATD_IPAddress = sSession.IPAddress
            Arr = objPurchase.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblStatus.Text = ""
            Session("dtPurchase") = Nothing
            txtBillAmount.Text = "0" : txtPaidAmount.Text = "0" : txtOtherCharge.Text = "0"
            txtTransactionNo.Text = objPurchase.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlExistPurchase.SelectedIndex = 0 : lblStatus.Text = "Not Started"
            ddlParty.SelectedIndex = 0 : txtBillNo.Text = "" : txtBillDate.Text = ""

            Session("ChargesMaster") = Nothing
            GvCharge.DataSource = Nothing
            GvCharge.DataBind()

            ClearAll()
            imgbtnSave.ImageUrl = "~/Images/Save24.png"
            imgbtnSave.ToolTip = "Save"
            txtTransactionNo.Text = objPurchase.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub ClearAll()
        Try
            ddlAccZone.SelectedIndex = 0 : ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            txtTransactionNo.Text = "" : txtBillDate.Text = "" : ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.SelectedIndex = 0
            chkboxFrom.Checked = False : chkboxTo.Checked = False
            txtBillingAddress.Text = "" : txtDeliveryFromAddress.Text = "" : txtCompanyAddress.Text = "" : txtReceiveAddress.Text = ""
            txtBillingGSTNRegNo.Text = "" : txtDeliveryFromGSTNRegNo.Text = "" : txtCompanyGSTNRegNo.Text = "" : txtReceiveGSTNRegNo.Text = ""
            ddlBranch.SelectedIndex = 0 : txtBillDate.Text = "" : txtSprCode.Text = "" : txtReceiptDate.Text = ""

            txtHSNCode.Text = "" : chkCategory.ClearSelection() : ddlUnit.Items.Clear() : txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtTotalAmount.Text = ""

            dgItems.DataSource = Nothing
            dgItems.DataBind()

            dgJEDetails.DataSource = Nothing
            dgJEDetails.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub txtOtherCharge_TextChanged(sender As Object, e As EventArgs) Handles txtOtherCharge.TextChanged
        Dim dAmount As Double = 0
        Try
            If txtPaidAmount.Text = "" Then
                dAmount = 0
            Else
                dAmount = Convert.ToDouble(txtPaidAmount.Text)
            End If

            If IsNumeric(txtOtherCharge.Text) = True Then
                dAmount = dAmount + Convert.ToDouble(txtOtherCharge.Text)
            End If
            txtPaidAmount.Text = Convert.ToDecimal(dAmount).ToString("#,##0.00")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtOtherCharge_TextChanged")
        End Try
    End Sub
    'Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Try
    '        If ddlParty.SelectedIndex > 0 Then
    '            DivAccountsDetails.Visible = True
    '            GvAccountDetails.DataSource = objPurchase.GetAccountsDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue, dtCOA)
    '            GvAccountDetails.DataBind()
    '        Else
    '            DivAccountsDetails.Visible = False
    '            GvAccountDetails.DataSource = dt
    '            GvAccountDetails.DataBind()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
    '    End Try
    'End Sub

    Private Sub GvAccountDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvAccountDetails.RowDataBound
        Dim lblDebit As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'lblDebit = TryCast(e.Row.FindControl("lblDebit"), Label)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAccountDetails_RowDataBound")
        End Try
    End Sub
    Private Sub imgbtnAddCharge_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddCharge.Click
        Dim dt, dtTable As New DataTable
        Dim dTotalCharges As Double
        Try
            lblError.Text = ""
            If lblStatus.Text = "Submitted" Then
                lblError.Text = "This transaction has been submitted, Charges can not be add."
                Exit Sub
            ElseIf lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved, Charges can not be add."
                Exit Sub
            End If
            If ddlChargeType.SelectedIndex > 0 Then
                If txtShippingRate.Text <> "" Then
                    dt = AddCharges()
                    dtTable = objPurchase.RemoveDublicate(dt)
                    GvCharge.DataSource = dtTable
                    GvCharge.DataBind()

                    ddlChargeType.SelectedIndex = 0 : txtShippingRate.Text = ""

                    'grdDispatchDetails.DataSource = objDispatch.BindAllocatedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlOrderNo.SelectedValue, ddlAllocationNo.SelectedValue)
                    'grdDispatchDetails.DataBind()
                Else
                    lblError.Text = "Enter Amount charged."
                End If
            Else
                lblError.Text = "Select Charge Type."
            End If

            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1
                    dTotalCharges = dTotalCharges + Convert.ToDouble(GvCharge.Items(i).Cells(2).Text)
                Next
            End If
            txtOtherCharge.Text = dTotalCharges
            'txtPaidAmount.Text = dTotalCharges
            dTotalCharges = 0
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
            Throw
        End Try
    End Function
    Public Sub SaveCharges(ByVal iTRID As Integer)
        Dim Arr() As String
        Try
            'Deleting charges Everytime & Saving'
            objPurchase.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTRID, 8)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1
                    objPurchase.C_ID = 0
                    objPurchase.C_TRID = iTRID
                    objPurchase.C_TRType = 8    'Purchase Voucher
                    objPurchase.C_ChargeID = GvCharge.Items(i).Cells(0).Text
                    objPurchase.C_ChargeType = GvCharge.Items(i).Cells(1).Text
                    objPurchase.C_ChargeAmount = GvCharge.Items(i).Cells(2).Text
                    objPurchase.C_DelFlag = "W"
                    objPurchase.C_Status = "C"
                    objPurchase.C_CompID = sSession.AccessCodeID
                    objPurchase.C_YearID = sSession.YearID
                    objPurchase.C_CreatedBy = sSession.UserID
                    objPurchase.C_CreatedOn = System.DateTime.Now
                    objPurchase.C_Operation = "C"
                    objPurchase.C_IPAddress = sSession.IPAddress

                    Arr = objPurchase.SaveAccCharges(sSession.AccessCode, objPurchase)
                Next
            End If
            'Charges Saving'
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GvCharge_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles GvCharge.ItemCommand
        Dim dt As New DataTable
        Dim dTotalCharges As Double
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
                    dTotalCharges = dTotalCharges + Convert.ToDouble(GvCharge.Items(i).Cells(2).Text)
                Next
            End If
            txtOtherCharge.Text = dTotalCharges
            'txtPaidAmount.Text = dTotalCharges
            dTotalCharges = 0

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dtCustomer As New DataTable
        Dim description As String
        Try
            If ddlParty.SelectedIndex > 0 Then
                dtCustomer = objPurchase.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                If dtCustomer.Rows.Count > 0 Then
                    txtSprCode.Text = dtCustomer.Rows(0)("CSM_Code")
                    txtBillingAddress.Text = dtCustomer.Rows(0)("CSM_Address")
                    txtBillingGSTNRegNo.Text = dtCustomer.Rows(0)("CSM_GSTNRegNo")

                    txtDeliveryFromAddress.Text = dtCustomer.Rows(0)("CSM_Address")
                    txtDeliveryFromGSTNRegNo.Text = dtCustomer.Rows(0)("CSM_GSTNRegNo")

                    ddlCompanyType.SelectedValue = dtCustomer.Rows(0)("CSM_CompanyType")
                    If ddlCompanyType.SelectedIndex > 0 Then
                        BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                        ddlGSTCategory.SelectedValue = dtCustomer.Rows(0)("CSM_GSTNCategory")

                        description = objPurchase.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                        If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                            txtDeliveryFromGSTNRegNo.Enabled = False
                        Else
                            txtDeliveryFromGSTNRegNo.Enabled = True
                        End If

                    Else
                        ddlGSTCategory.Items.Clear()
                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindBranch()
        Try
            ddlBranch.DataSource = objPurchase.LoadBranches(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlCompanyType.DataSource = objPurchase.LoadCompanyType(sSession.AccessCode, sSession.AccessCodeID)
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
            dt = objPurchase.LoadGSTCategory(sSession.AccessCode, sSession.AccessCodeID, sCompanyType)
            ddlGSTCategory.DataSource = dt
            ddlGSTCategory.DataTextField = "GC_GSTCategory"
            ddlGSTCategory.DataValueField = "GC_Id"
            ddlGSTCategory.DataBind()
            ddlGSTCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkboxFrom_CheckedChanged")
        End Try
    End Sub
    Private Sub chkboxTo_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxTo.CheckedChanged
        Try
            If chkboxTo.Checked = True Then
                txtReceiveAddress.Text = ""
                txtReceiveGSTNRegNo.Text = ""
            Else
                txtReceiveAddress.Text = txtCompanyAddress.Text
                txtReceiveGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkboxTo_CheckedChanged")
        End Try
    End Sub
    Private Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Dim dt As New DataTable
        Dim description As String
        Try
            txtCompanyAddress.Text = "" : txtCompanyGSTNRegNo.Text = "" : txtReceiveAddress.Text = "" : txtReceiveGSTNRegNo.Text = ""
            If ddlBranch.SelectedIndex > 0 Then
                dt = objPurchase.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, ddlBranch.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUSTB_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUSTB_GSTNRegNo")

                    'ddlCompanyType.SelectedValue = dt.Rows(0)("CUSTB_CompanyType")
                    'BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    'ddlGSTCategory.SelectedValue = dt.Rows(0)("CUSTB_GSTNCategory")

                    'description = objPurchase.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    'If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                    '    txtReceiveGSTNRegNo.Enabled = False
                    'Else
                    '    txtReceiveGSTNRegNo.Enabled = True
                    'End If
                    txtReceiveAddress.Text = txtCompanyAddress.Text
                    txtReceiveGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
                End If
            Else
                dt = objPurchase.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    'ddlCompanyType.SelectedValue = dt.Rows(0)("CUST_INDTypeID")
                    'BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                    'ddlGSTCategory.SelectedValue = dt.Rows(0)("CUST_TaxPayableCategory")

                    'description = objPurchase.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                    'If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                    '    txtReceiveGSTNRegNo.Enabled = False
                    'Else
                    '    txtReceiveGSTNRegNo.Enabled = True
                    'End If
                    txtReceiveAddress.Text = txtCompanyAddress.Text
                    txtReceiveGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub btnItemAdd_Click(sender As Object, e As EventArgs) Handles btnItemAdd.Click
    '    Dim Arr() As String
    '    Dim i As Integer = 0
    '    Dim iBillID As Integer = 0
    '    Dim dTotalAmount As Double = 0
    '    Dim iFlag As Integer = 0
    '    Dim dTaxAmount As Double = 0
    '    Dim dDate, dSDate As Date : Dim m As Integer
    '    Dim objPur As New clsPurchaseVoucher.Purchase
    '    Try
    '        '  Dim result As New DialogResult
    '        lblError.Text = ""
    '        If ddlAccBrnch.SelectedIndex = 0 Then
    '            lblError.Text = "Select Branch."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
    '            Exit Sub
    '        End If

    '        'Cheque Date Comparision'
    '        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m < 0 Then
    '            lblError.Text = "Date (" & txtReceiptDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtReceiptDate.Focus()
    '            Exit Sub
    '        End If

    '        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m > 0 Then
    '            lblError.Text = "Date (" & txtReceiptDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtReceiptDate.Focus()
    '            Exit Sub
    '        End If
    '        'Cheque Date Comparision'

    '        'Cheque Date Comparision'
    '        dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m < 0 Then
    '            lblError.Text = "Supplier Invoice Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtBillDate.Focus()
    '            Exit Sub
    '        End If

    '        dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        m = DateDiff(DateInterval.Day, dDate, dSDate)
    '        If m > 0 Then
    '            lblError.Text = "Supplier Invoice Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
    '            lblCustomerValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
    '            txtBillDate.Focus()
    '            Exit Sub
    '        End If
    '        'Cheque Date Comparision'

    '        'If dgPurchase.Items.Count = 0 Then
    '        '    lblError.Text = "Add Amount"
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add Amount','', 'success');", True)
    '        '    Exit Sub
    '        'End If

    '        'For i = 0 To dgPurchase.Items.Count - 1
    '        '    dTotalAmount = dTotalAmount + Convert.ToDouble(dgPurchase.Items(i).Cells(13).Text)
    '        'Next

    '        If txtBillAmount.Text = "0" Then
    '            lblError.Text = "Enter Bill Amount"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Bill Amount','', 'success');", True)
    '            Exit Sub
    '        End If

    '        'If Math.Round(Convert.ToDouble(txtBillAmount.Text)) = Math.Round(dTotalAmount) = False Then
    '        '    iFlag = 1
    '        '    'lblError.Text = "Amount not matched with bill amount"
    '        '    ''ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not matched with Bill Amount','', 'success');", True)
    '        '    '' Exit Sub

    '        '    'result = MessageBox.Show("Amount not matched with bill amount.Do you want to Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    '        '    'If result = DialogResult.Yes Then
    '        '    '    GoTo SaveResult
    '        '    'ElseIf result = DialogResult.No Then
    '        '    '    Exit Sub
    '        '    'End If
    '        'End If

    '        'SaveResult:
    '        objPurchase.iAcc_Purchase_ID = 0
    '        objPurchase.sAcc_Purchase_TransactionNo = txtTransactionNo.Text
    '        objPurchase.iAcc_Purchase_Party = ddlParty.SelectedValue
    '        objPurchase.sAcc_Purchase_BillNo = txtBillNo.Text
    '        objPurchase.dAcc_Purchase_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objPurchase.dAcc_Purchase_ReceiptDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '        objPurchase.dAcc_Purchase_BillAmount = txtBillAmount.Text
    '        objPurchase.iAcc_Purchase_CreatedBy = sSession.UserID
    '        objPurchase.iAcc_Purchase_Year = sSession.YearID
    '        objPurchase.iAcc_Purchase_CompID = sSession.AccessCodeID
    '        objPurchase.sAcc_Purchase_Status = "C"
    '        objPurchase.sAcc_Purchase_DelFlag = "W"
    '        objPurchase.sAcc_Purchase_Operation = "C"
    '        objPurchase.sAcc_Purchase_IPAddress = sSession.IPAddress
    '        objPurchase.iAcc_Purchase_MisMatchFlag = iFlag
    '        objPurchase.sAcc_Purchase_PaymentStatus = "N"  'Nil

    '        If txtOtherCharge.Text = "" Then
    '            objPurchase.dAcc_Purchase_OtherCharges = "0"
    '        Else
    '            objPurchase.dAcc_Purchase_OtherCharges = txtOtherCharge.Text
    '        End If

    '        objPurchase.iACC_Purchase_ZoneID = ddlAccZone.SelectedValue
    '        objPurchase.iACC_Purchase_RegionID = ddlAccRgn.SelectedValue
    '        objPurchase.iACC_Purchase_AreaID = ddlAccArea.SelectedValue
    '        objPurchase.iACC_Purchase_BranchID = ddlAccBrnch.SelectedValue

    '        If txtCompanyAddress.Text <> "" Then
    '            objPurchase.Acc_Purchase_CompanyAddress = txtCompanyAddress.Text
    '        Else
    '            objPurchase.Acc_Purchase_CompanyAddress = ""
    '        End If

    '        If txtBillingAddress.Text <> "" Then
    '            objPurchase.Acc_Purchase_BillingAddress = txtBillingAddress.Text
    '        Else
    '            objPurchase.Acc_Purchase_BillingAddress = ""
    '        End If

    '        If txtDeliveryFromAddress.Text <> "" Then
    '            objPurchase.Acc_Purchase_DeliveryFrom = txtDeliveryFromAddress.Text
    '        Else
    '            objPurchase.Acc_Purchase_DeliveryFrom = ""
    '        End If

    '        If txtReceiveAddress.Text <> "" Then
    '            objPurchase.Acc_Purchase_ReceiveAddress = txtReceiveAddress.Text
    '        Else
    '            objPurchase.Acc_Purchase_ReceiveAddress = ""
    '        End If

    '        If txtCompanyGSTNRegNo.Text <> "" Then
    '            objPurchase.Acc_Purchase_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
    '        Else
    '            objPurchase.Acc_Purchase_CompanyGSTNRegNo = ""
    '        End If

    '        If txtBillingGSTNRegNo.Text <> "" Then
    '            objPurchase.Acc_Purchase_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
    '        Else
    '            objPurchase.Acc_Purchase_BillingGSTNRegNo = ""
    '        End If

    '        If txtDeliveryFromGSTNRegNo.Text <> "" Then
    '            objPurchase.Acc_Purchase_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
    '        Else
    '            objPurchase.Acc_Purchase_DeliveryFromGSTNRegNo = ""
    '        End If

    '        If txtReceiveGSTNRegNo.Text <> "" Then
    '            objPurchase.Acc_Purchase_ReceiveGSTNRegNo = txtReceiveGSTNRegNo.Text
    '        Else
    '            objPurchase.Acc_Purchase_ReceiveGSTNRegNo = ""
    '        End If

    '        If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
    '            objPurchase.Acc_Purchase_InvoiceStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
    '        End If

    '        objPurchase.Acc_Purchase_CompanyType = ddlCompanyType.SelectedValue
    '        objPurchase.Acc_Purchase_GSTNCategory = ddlGSTCategory.SelectedValue

    '        If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
    '            objPurchase.Acc_Purchase_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
    '        End If

    '        Arr = objPurchase.SavePurchaseVoucher(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
    '        iBillID = Arr(1)

    '        objPur.PD_MasterID = iBillID
    '        objPur.PD_HSNCode = txtHSNCode.Text
    '        objPur.PD_Commodity = txtcommodity.Text
    '        objPur.PD_Goods = txtgoods.Text
    '        objPur.PD_Unit = txtUnit.Text
    '        objPur.PD_Rate = txtMRP.Text
    '        objPur.PD_Quantity = txtQty.Text
    '        objPur.PD_ChargePerItem = txtcharge.Text
    '        objPur.PD_RateAmount = txtTotalAmt.Text

    '        'If ddlDiscount.SelectedIndex > 0 Then
    '        '    objInvoiceForm.PID_Discount = ddlDiscount.SelectedValue
    '        'Else
    '        '    objInvoiceForm.PID_Discount = 0
    '        'End If
    '        If txtDiscount.Text <> "" Then
    '            objPur.PD_Discount = txtDiscount.Text
    '        Else
    '            objPur.PD_Discount = 0
    '        End If

    '        If txtDiscountAmt.Text <> "" Then
    '            objPur.PD_DiscountAmount = txtDiscountAmt.Text
    '        Else
    '            objPur.PD_DiscountAmount = 0
    '        End If

    '        objPur.PD_Amount = txtAmt.Text

    '        objPur.PD_GSTRate = txtGST.Text

    '        If txtGSTAmt.Text <> "" Then
    '            objPur.PD_GSTAmount = txtGSTAmt.Text
    '        Else
    '            objPur.PD_GSTAmount = 0
    '        End If

    '        If objPurchase.Acc_Purchase_InvoiceStatus = "Local" Then
    '            objPur.PD_SGST = objPur.PD_GSTRate / 2
    '            objPur.PD_SGSTAmount = objPur.PD_GSTAmount / 2
    '            objPur.PD_CGST = objPur.PD_GSTRate / 2
    '            objPur.PD_CGSTAmount = objPur.PD_GSTAmount / 2
    '            objPur.PD_IGST = 0
    '            objPur.PD_IGSTAmount = 0
    '        ElseIf objPurchase.Acc_Purchase_InvoiceStatus = "Inter State" Then
    '            objPur.PD_SGST = 0
    '            objPur.PD_SGSTAmount = 0
    '            objPur.PD_CGST = 0
    '            objPur.PD_CGSTAmount = 0
    '            objPur.PD_IGST = objPur.PD_GSTRate
    '            objPur.PD_IGSTAmount = objPur.PD_GSTAmount
    '        End If

    '        If UCase(ddlGSTCategory.SelectedItem.Text) = "UNRIGISTERED DEALER" Then
    '            'objInvoiceForm.PID_URD_GSTRate = objInvoiceForm.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, lblCommodityID.Text, lblItemID.Text)
    '            'objInvoiceForm.PID_URD_GSTAmt = (((objInvoiceForm.PID_RateAmount - objInvoiceForm.PID_DiscountAmount) + objInvoiceForm.PID_ChargePerItem) * objInvoiceForm.PID_URD_GSTRate) / 100
    '            'objInvoiceForm.PID_URD_SGST = objInvoiceForm.PID_URD_GSTRate / 2
    '            'objInvoiceForm.PID_URD_SGSTAmt = objInvoiceForm.PID_URD_GSTAmt / 2
    '            'objInvoiceForm.PID_URD_CGST = objInvoiceForm.PID_URD_GSTRate / 2
    '            'objInvoiceForm.PID_URD_CGSTAmt = objInvoiceForm.PID_URD_GSTAmt / 2
    '            Dim URD_GSTRate, URD_GSTAmt As Double

    '            URD_GSTRate = txtGST.Text
    '            URD_GSTAmt = (((objPur.PD_RateAmount - objPur.PD_DiscountAmount) + objPur.PD_ChargePerItem) * URD_GSTRate) / 100

    '            objPur.PD_SGST = URD_GSTRate / 2
    '            objPur.PD_SGSTAmount = URD_GSTAmt / 2
    '            objPur.PD_CGST = URD_GSTRate / 2
    '            objPur.PD_CGSTAmount = URD_GSTAmt / 2
    '            objPur.PD_IGST = 0
    '            objPur.PD_IGSTAmount = 0
    '        Else
    '            'objInvoiceForm.PID_URD_GSTRate = 0
    '            'objInvoiceForm.PID_URD_GSTAmt = 0
    '            'objInvoiceForm.PID_URD_SGST = 0
    '            'objInvoiceForm.PID_URD_SGSTAmt = 0
    '            'objInvoiceForm.PID_URD_CGST = 0
    '            'objInvoiceForm.PID_URD_CGSTAmt = 0
    '        End If

    '        If txtNetAmt.Text <> "" Then
    '            objPur.PD_FinalTotal = txtNetAmt.Text
    '        Else
    '            objPur.PD_FinalTotal = 0
    '        End If

    '        objPur.PD_Status = "W"
    '        objPur.PD_CompID = sSession.AccessCodeID
    '        objPur.PD_CreatedBy = sSession.UserID
    '        objPur.PD_CreatedOn = System.DateTime.Now
    '        objPur.PD_Operation = "C"
    '        objPur.PD_IPAddress = sSession.IPAddress
    '        objPur.PD_YearID = sSession.YearID

    '        Arr = objPurchase.SaveAcceptedDetails(sSession.AccessCode, objPur)

    '        SaveCharges(iBillID)

    '        LoadExistingPurchaseVoucher()
    '        ddlExistPurchase.SelectedValue = iBillID
    '        ddlExistPurchase_SelectedIndexChanged(sender, e)
    '        imgbtnSave.Visible = False : imgbtnUpdate.Visible = True

    '        If Arr(0) = "2" Then
    '            lblError.Text = "Successfully Updated."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

    '        ElseIf Arr(0) = "3" Then
    '            lblError.Text = "Successfully Saved & Waiting for Approval."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
    '        End If
    '        lblStatus.Text = "Waiting for Approval"
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
    '    End Try
    'End Sub
    Public Function CheckSourceDestinationOfDispatch(ByVal sBillingAddress As String, ByVal sReceiveAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            sSource = sBillingAddress.Substring(0, 2)
            sDestination = sReceiveAddress.Substring(0, 2)

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
    Public Function GetSourceDestinationState(ByVal sBillingAddress As String, ByVal sReceiveAddress As String) As String
        Dim sSource As String = "" : Dim sDestination As String = ""
        Try
            'sSource = sBillingAddress.Substring(0, 2)
            'sDestination = sReceiveAddress.Substring(0, 2)

            'If sSource = sDestination Then
            '    GetSourceDestinationState = objPurchase.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
            'Else
            '    GetSourceDestinationState = objPurchase.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
            'End If
            'Return GetSourceDestinationState
            If sBillingAddress <> "" And sReceiveAddress = "" Then
                sSource = sBillingAddress.Substring(0, 2)
                GetSourceDestinationState = objPurchase.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)

            ElseIf sBillingAddress = "" And sReceiveAddress <> "" Then
                sDestination = sReceiveAddress.Substring(0, 2)
                GetSourceDestinationState = objPurchase.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)

            ElseIf sBillingAddress <> "" And sReceiveAddress <> "" Then
                sSource = sBillingAddress.Substring(0, 2)
                sDestination = sReceiveAddress.Substring(0, 2)
                If sSource = sDestination Then
                    GetSourceDestinationState = objPurchase.GetState(sSession.AccessCode, sSession.AccessCodeID, sSource)
                Else
                    GetSourceDestinationState = objPurchase.GetState(sSession.AccessCode, sSession.AccessCodeID, sDestination)
                End If
            End If
            Return GetSourceDestinationState
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSourceDestinationState")
        End Try
    End Function
    'Private Sub btnItemAdd_Click(sender As Object, e As EventArgs) Handles btnItemAdd.Click
    '    Dim dtDetails As New DataTable
    '    Dim dr As DataRow
    '    Dim dRateAmt, dDisAmt, dTotal, dGSTAmt, dNetAmt, dCharge, dItemTotal, dTotalAmt As Double
    '    Try
    '        lblError.Text = ""
    '        dtPurchase = BuildTable()

    '        If IsNothing(Session("dtPurchase")) = False Then
    '            dtPurchase = Session("dtPurchase")
    '        End If

    '        dr = dtPurchase.NewRow
    '        dr("HSNCode") = txtHSNCode.Text
    '        dr("Commodity") = txtcommodity.Text
    '        dr("Goods") = txtgoods.Text
    '        dr("Unit") = txtUnit.Text
    '        dr("MRP") = txtMRP.Text
    '        dr("Qty") = txtQty.Text
    '        dRateAmt = txtMRP.Text * txtQty.Text
    '        If dRateAmt <> txtTotalAmt.Text Then
    '            lblError.Text = "Total Amount is incorrect."
    '            Exit Sub
    '        End If
    '        dr("RateAMt") = txtTotalAmt.Text
    '        dr("Discount") = txtDiscount.Text

    '        dDisAmt = (txtTotalAmt.Text * txtDiscount.Text) / 100
    '        If dDisAmt <> txtDiscountAmt.Text Then
    '            lblError.Text = "Discount Amount is incorrect."
    '            Exit Sub
    '        End If
    '        dr("DiscountAmt") = txtDiscountAmt.Text

    '        'If txtOtherCharge.Text <> "" Then
    '        '    If dtPurchase.Rows.Count > 0 Then
    '        '        For i = 0 To dtPurchase.Rows.Count - 1
    '        '            dItemTotal = dItemTotal + dtPurchase.Rows(i)("RateAMt")
    '        '        Next
    '        '        dCharge = (txtTotalAmt.Text * txtOtherCharge.Text) / (dItemTotal + txtTotalAmt.Text)
    '        '        dr("Charge") = dCharge
    '        '    Else
    '        '        dCharge = (txtTotalAmt.Text * txtOtherCharge.Text) / (txtTotalAmt.Text)
    '        '        dr("Charge") = dCharge
    '        '    End If
    '        'Else
    '        dCharge = 0
    '        dr("Charge") = 0
    '        'End If

    '        dTotal = txtTotalAmt.Text - txtDiscountAmt.Text
    '        If dTotal <> txtAmt.Text Then
    '            lblError.Text = "Amount is incorrect."
    '            Exit Sub
    '        End If
    '        dr("Total") = txtAmt.Text
    '        dr("GST") = txtGST.Text
    '        dGSTAmt = (((txtTotalAmt.Text - txtDiscountAmt.Text) + dCharge) * txtGST.Text) / 100

    '        If dGSTAmt <> txtGSTAmt.Text Then
    '            lblError.Text = "GST Amount is incorrect."
    '            Exit Sub
    '        End If
    '        dr("GSTAmt") = txtGSTAmt.Text

    '        dNetAmt = Convert.ToDecimal(((txtTotalAmt.Text - txtDiscountAmt.Text) + dCharge)) + Convert.ToDecimal(txtGSTAmt.Text)
    '        If dNetAmt <> txtNetAmt.Text Then
    '            lblError.Text = "Net Amount is incorrect."
    '            Exit Sub
    '        End If
    '        dr("NetAmount") = txtNetAmt.Text
    '        dtPurchase.Rows.Add(dr)

    '        dgItems.DataSource = dtPurchase
    '        dgItems.DataBind()

    '        Session("dtPurchase") = dtPurchase

    '        For i = 0 To dgItems.Items.Count - 1
    '            dTotalAmt = dTotalAmt + Convert.ToDouble(dgItems.Items(i).Cells(13).Text)
    '        Next
    '        txtBillAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")

    '        txtHSNCode.Text = "" : txtcommodity.Text = "" : txtgoods.Text = "" : txtUnit.Text = "" : txtMRP.Text = "" : txtQty.Text = "" : txtTotalAmt.Text = ""
    '        txtDiscount.Text = "" : txtDiscountAmt.Text = "" : txtAmt.Text = "" : txtGST.Text = "" : txtGSTAmt.Text = "" : txtNetAmt.Text = ""

    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnItemAdd_Click")
    '    End Try
    'End Sub
    Private Sub dgItems_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgItems.ItemCommand
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Dim iMasterID As Integer
        Try
            lblError.Text = ""
            If e.CommandName = "Delete" Then
                If ddlExistPurchase.SelectedIndex > 0 Then
                    iMasterID = ddlExistPurchase.SelectedValue
                Else
                    iMasterID = 0
                End If
                If iMasterID > 0 Then
                    sStatus = objPurchase.GetStatus(sSession.AccessCode, sSession.AccessCodeID, iMasterID, sSession.YearID)
                    If sStatus = "S" Then
                        lblError.Text = "This is submitted,goods can not be deleted."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This is submitted,goods can not be deleted.','', 'success');", True)
                        Exit Sub
                    ElseIf sStatus = "A" Then
                        lblError.Text = "This is Approved,goods can not be deleted."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This is Approved,goods can not be deleted.','', 'success');", True)
                        Exit Sub
                    End If
                    dt = Session("dtPurchase")
                    dt.Rows.Item(e.Item.ItemIndex).Delete()
                    Session("dtPurchase") = dt
                Else
                    dt = Session("dtPurchase")
                    dt.Rows.Item(e.Item.ItemIndex).Delete()
                    Session("dtPurchase") = dt
                End If
            End If
            dgItems.DataSource = dt
            dgItems.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgItems_ItemCommand")
        End Try
    End Sub
    Private Sub dgItems_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgItems.ItemDataBound
        Dim dMRP, dQty, dRateAmt, dDis, dDisAmt, dCharge, dTotal, dGST, dGSTAmt, dNetAmt, dItemTotal As Double
        Dim dtPurchase As New DataTable
        Dim sStatus As String = ""
        Dim dtData As New DataTable
        Dim iCommodity, iGoodsID, iUnitID As Integer
        Try
            lblError.Text = ""
            dtPurchase = Session("dtPurchase")
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then

                If ddlExistPurchase.SelectedIndex > 0 Then
                    iCommodity = e.Item.Cells(0).Text
                    iGoodsID = e.Item.Cells(1).Text
                    iUnitID = e.Item.Cells(2).Text

                    dtData = objPurchase.BindSavedData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPurchase.SelectedValue, iCommodity, iGoodsID)
                    If dtData.Rows.Count > 0 Then
                        For m = 0 To dtData.Rows.Count - 1
                            e.Item.Cells(3).Text = dtData.Rows(m)("PD_HSNCode")
                            e.Item.Cells(4).Text = objDb.SQLGetDescription(sSession.AccessCode, "Select INV_Description From Inventory_master Where INV_ID=" & iCommodity & " And INV_Parent=0 And INV_CompID=" & sSession.AccessCodeID & " ")
                            e.Item.Cells(5).Text = objDb.SQLGetDescription(sSession.AccessCode, "Select INV_Description From Inventory_master Where INV_ID=" & iGoodsID & " And INV_Parent=" & iCommodity & " And INV_CompID=" & sSession.AccessCodeID & " ")
                            e.Item.Cells(6).Text = objDb.SQLGetDescription(sSession.AccessCode, "Select Mas_Desc From Acc_General_master Where Mas_ID=" & iUnitID & " And MAS_CompID=" & sSession.AccessCodeID & " And MAS_DelFlag='A' And MAS_Master in(Select MAS_ID From Acc_Master_Type where Mas_Type='Unit of Measurement') ")
                            e.Item.Cells(7).Text = dtData.Rows(m)("PD_Rate")
                            e.Item.Cells(8).Text = dtData.Rows(m)("PD_Quantity")
                            e.Item.Cells(9).Text = dtData.Rows(m)("PD_RateAmount")
                            e.Item.Cells(10).Text = dtData.Rows(m)("PD_Discount")
                            e.Item.Cells(11).Text = dtData.Rows(m)("PD_DiscountAmount")
                            e.Item.Cells(12).Text = dtData.Rows(m)("PD_ChargePerItem")
                            e.Item.Cells(13).Text = dtData.Rows(m)("PD_Amount")
                            e.Item.Cells(14).Text = dtData.Rows(m)("PD_GSTRate")
                            e.Item.Cells(15).Text = dtData.Rows(m)("PD_GSTAmount")
                            e.Item.Cells(16).Text = dtData.Rows(m)("PD_FinalTotal")
                        Next
                    End If

                    sStatus = objPurchase.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistPurchase.SelectedValue, sSession.YearID)
                    If sStatus = "S" Or sStatus = "A" Then
                        Dim imgbtnDelete As ImageButton = CType(e.Item.FindControl("imgbtnDelete"), ImageButton)
                        imgbtnDelete.Enabled = False
                    Else
                        If txtOtherCharge.Text <> "" Then
                            If dtData.Rows.Count > 0 Then
                                For i = 0 To dtData.Rows.Count - 1
                                    dItemTotal = dItemTotal + dtData.Rows(i)("PD_RateAmount")
                                Next
                                dCharge = (e.Item.Cells(9).Text * txtOtherCharge.Text) / (dItemTotal)
                                e.Item.Cells(12).Text = String.Format("{0:0.00}", Convert.ToDecimal(dCharge))

                                e.Item.Cells(13).Text = String.Format("{0:0.00}", Convert.ToDecimal(((e.Item.Cells(9).Text - e.Item.Cells(11).Text) + dCharge)))
                                e.Item.Cells(15).Text = String.Format("{0:0.00}", Convert.ToDecimal(((e.Item.Cells(13).Text) * e.Item.Cells(14).Text) / 100))
                                e.Item.Cells(16).Text = String.Format("{0:0.00}", Convert.ToDecimal(Convert.ToDecimal(e.Item.Cells(13).Text) + Convert.ToDecimal(e.Item.Cells(15).Text)))
                            End If
                        End If
                    End If

                Else

                    dMRP = e.Item.Cells(7).Text
                    dQty = e.Item.Cells(8).Text
                    dRateAmt = e.Item.Cells(9).Text
                    dDis = e.Item.Cells(10).Text
                    dDisAmt = e.Item.Cells(11).Text
                    'dCharge = e.Item.Cells(9).Text
                    'dTotal = e.Item.Cells(10).Text
                    dGST = e.Item.Cells(14).Text
                    'dGSTAmt = e.Item.Cells(12).Text
                    'dNetAmt = e.Item.Cells(13).Text

                    If txtOtherCharge.Text <> "" Then
                        If dtPurchase.Rows.Count > 0 Then
                            For i = 0 To dtPurchase.Rows.Count - 1
                                dItemTotal = dItemTotal + dtPurchase.Rows(i)("RateAMt")
                            Next
                            dCharge = (dRateAmt * txtOtherCharge.Text) / (dItemTotal)
                            e.Item.Cells(12).Text = String.Format("{0:0.00}", Convert.ToDecimal(dCharge))

                            e.Item.Cells(13).Text = String.Format("{0:0.00}", Convert.ToDecimal(((dRateAmt - dDisAmt) + dCharge)))
                            e.Item.Cells(15).Text = String.Format("{0:0.00}", Convert.ToDecimal(((e.Item.Cells(13).Text) * dGST) / 100))
                            e.Item.Cells(16).Text = String.Format("{0:0.00}", Convert.ToDecimal(Convert.ToDecimal(e.Item.Cells(13).Text) + Convert.ToDecimal(e.Item.Cells(15).Text)))
                        End If
                    End If
                End If

                dgItems.Columns(17).Visible = False
                If sPTSave = "YES" Then
                    dgItems.Columns(17).Visible = True
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgItems_ItemDataBound")
        End Try
    End Sub
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim dttab As New DataTable
        Dim dTotalAmt As Double
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
            dgItems.DataSource = dttab
            dgItems.DataBind()

            For i = 0 To dgItems.Items.Count - 1
                dTotalAmt = dTotalAmt + Convert.ToDouble(dgItems.Items(i).Cells(16).Text)
            Next
            txtBillAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCalculate_Click")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim i As Integer = 0
        Dim iBillID As Integer = 0
        Dim dTotalAmount As Double = 0
        Dim iFlag As Integer = 0
        Dim dTaxAmount As Double = 0
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim objPur As clsPurchaseVoucher.Purchase

        Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Dim sName As String = ""
        Try

            If lblStatus.Text = "Submitted" Then
                lblError.Text = "This transaction has been submitted, it can not be Updated again."
                Exit Sub
            ElseIf lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved, it can not be Updated again."
                Exit Sub
            End If

            '  Dim result As New DialogResult
            lblError.Text = ""
            If ddlAccBrnch.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Branch.','', 'success');", True)
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

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Date (" & txtReceiptDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtReceiptDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Date (" & txtReceiptDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtReceiptDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Supplier Invoice Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtBillDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Supplier Invoice Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtBillDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            For i = 0 To dgItems.Items.Count - 1
                dTotalAmount = dTotalAmount + Convert.ToDouble(dgItems.Items(i).Cells(16).Text)
            Next

            If txtBillAmount.Text = "0" Then
                lblError.Text = "Enter Bill Amount"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Bill Amount','', 'success');", True)
                Exit Sub
            End If

            If Math.Round(Convert.ToDouble(txtBillAmount.Text)) = Math.Round(dTotalAmount) = False Then
                iFlag = 1
                'lblError.Text = "Amount not matched with bill amount"
                ''ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Amount not matched with Bill Amount','', 'success');", True)
                '' Exit Sub

                'result = MessageBox.Show("Amount not matched with bill amount.Do you want to Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                'If result = DialogResult.Yes Then
                '    GoTo SaveResult
                'ElseIf result = DialogResult.No Then
                '    Exit Sub
                'End If
            End If

            If dgItems.Items.Count > 0 Then
            Else
                lblError.Text = "Add the goods details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Add the goods details','', 'success');", True)
                Exit Sub
            End If

            'SaveResult:
            If ddlExistPurchase.SelectedIndex > 0 Then
                objPurchase.iAcc_Purchase_ID = ddlExistPurchase.SelectedValue
            Else
                objPurchase.iAcc_Purchase_ID = 0
            End If

            objPurchase.sAcc_Purchase_TransactionNo = txtTransactionNo.Text
            objPurchase.iAcc_Purchase_Party = ddlParty.SelectedValue
            objPurchase.sAcc_Purchase_BillNo = txtBillNo.Text
            objPurchase.dAcc_Purchase_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objPurchase.dAcc_Purchase_ReceiptDate = Date.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objPurchase.dAcc_Purchase_BillAmount = txtBillAmount.Text
            objPurchase.iAcc_Purchase_CreatedBy = sSession.UserID
            objPurchase.iAcc_Purchase_Year = sSession.YearID
            objPurchase.iAcc_Purchase_CompID = sSession.AccessCodeID
            objPurchase.sAcc_Purchase_Status = "C"
            objPurchase.sAcc_Purchase_DelFlag = "W"
            objPurchase.sAcc_Purchase_Operation = "C"
            objPurchase.sAcc_Purchase_IPAddress = sSession.IPAddress
            objPurchase.iAcc_Purchase_MisMatchFlag = iFlag
            objPurchase.sAcc_Purchase_PaymentStatus = "N"  'Nil

            If txtOtherCharge.Text = "" Then
                objPurchase.dAcc_Purchase_OtherCharges = "0"
            Else
                objPurchase.dAcc_Purchase_OtherCharges = txtOtherCharge.Text
            End If

            objPurchase.iACC_Purchase_ZoneID = ddlAccZone.SelectedValue
            objPurchase.iACC_Purchase_RegionID = ddlAccRgn.SelectedValue
            objPurchase.iACC_Purchase_AreaID = ddlAccArea.SelectedValue
            'objPurchase.iACC_Purchase_BranchID = ddlAccBrnch.SelectedValue
            If ddlAccBrnch.SelectedIndex > 0 Then
                objPurchase.iACC_Purchase_BranchID = ddlAccBrnch.SelectedValue
            Else
                objPurchase.iACC_Purchase_BranchID = 0
            End If

            If txtCompanyAddress.Text <> "" Then
                objPurchase.Acc_Purchase_CompanyAddress = txtCompanyAddress.Text
            Else
                objPurchase.Acc_Purchase_CompanyAddress = ""
            End If

            If txtBillingAddress.Text <> "" Then
                objPurchase.Acc_Purchase_BillingAddress = txtBillingAddress.Text
            Else
                objPurchase.Acc_Purchase_BillingAddress = ""
            End If

            If txtDeliveryFromAddress.Text <> "" Then
                objPurchase.Acc_Purchase_DeliveryFrom = txtDeliveryFromAddress.Text
            Else
                objPurchase.Acc_Purchase_DeliveryFrom = ""
            End If

            If txtReceiveAddress.Text <> "" Then
                objPurchase.Acc_Purchase_ReceiveAddress = txtReceiveAddress.Text
            Else
                objPurchase.Acc_Purchase_ReceiveAddress = ""
            End If

            If txtCompanyGSTNRegNo.Text <> "" Then
                objPurchase.Acc_Purchase_CompanyGSTNRegNo = txtCompanyGSTNRegNo.Text
            Else
                objPurchase.Acc_Purchase_CompanyGSTNRegNo = ""
            End If

            If txtBillingGSTNRegNo.Text <> "" Then
                objPurchase.Acc_Purchase_BillingGSTNRegNo = txtBillingGSTNRegNo.Text
            Else
                objPurchase.Acc_Purchase_BillingGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" Then
                objPurchase.Acc_Purchase_DeliveryFromGSTNRegNo = txtDeliveryFromGSTNRegNo.Text
            Else
                objPurchase.Acc_Purchase_DeliveryFromGSTNRegNo = ""
            End If

            If txtReceiveGSTNRegNo.Text <> "" Then
                objPurchase.Acc_Purchase_ReceiveGSTNRegNo = txtReceiveGSTNRegNo.Text
            Else
                objPurchase.Acc_Purchase_ReceiveGSTNRegNo = ""
            End If

            If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
                objPurchase.Acc_Purchase_InvoiceStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text = "" Then
                objPurchase.Acc_Purchase_InvoiceStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtReceiveGSTNRegNo.Text <> "" Then
                objPurchase.Acc_Purchase_InvoiceStatus = "Local"
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtReceiveGSTNRegNo.Text = "" Then
                objPurchase.Acc_Purchase_InvoiceStatus = "Local"
            End If
            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
            '    objPurchase.Acc_Purchase_InvoiceStatus = CheckSourceDestinationOfDispatch(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
            'End If

            objPurchase.Acc_Purchase_CompanyType = ddlCompanyType.SelectedValue
            objPurchase.Acc_Purchase_GSTNCategory = ddlGSTCategory.SelectedValue

            'If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
            '    objPurchase.Acc_Purchase_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
            'End If
            If txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text <> "" Then
                objPurchase.Acc_Purchase_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), Trim(txtReceiveGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text <> "" And txtReceiveGSTNRegNo.Text = "" Then
                objPurchase.Acc_Purchase_State = GetSourceDestinationState(Trim(txtDeliveryFromGSTNRegNo.Text), (""))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtReceiveGSTNRegNo.Text <> "" Then
                objPurchase.Acc_Purchase_State = GetSourceDestinationState((""), Trim(txtReceiveGSTNRegNo.Text))
            ElseIf txtDeliveryFromGSTNRegNo.Text = "" And txtReceiveGSTNRegNo.Text = "" Then
                Dim ibranch As Integer
                ibranch = objPurchase.getBranchFromPO(sSession.AccessCode, sSession.AccessCodeID, txtTransactionNo.Text)
                If ibranch > 0 Then 'branch 
                    objPurchase.Acc_Purchase_State = objPurchase.CheckDetailsofBranchState(sSession.AccessCode, sSession.AccessCodeID, txtTransactionNo.Text)
                    If objPurchase.Acc_Purchase_State = "" Then
                        lblError.Text = "Update state in branch master"
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in branch master.','', 'success');", True)
                        Exit Sub
                    End If
                Else 'Company
                    objPurchase.Acc_Purchase_State = objPurchase.CheckDetailsofCompState(sSession.AccessCode, sSession.AccessCodeID)
                    If objPurchase.Acc_Purchase_State = "" Then
                        lblError.Text = "Update state in company master"
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Update state in company master.','', 'success');", True)
                        Exit Sub
                    End If
                End If
            End If


            'Extra'
            'Chart Of Accounts'

            sPerm = LoadDetailsSetttings(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase")
            sPerm = sPerm.Remove(0, 1)
            sArray1 = sPerm.Split(",")
            iHead = sArray1(0) '1
            iGroup = sArray1(1) '29
            iSubGroup = sArray1(2) '31
            iGL = sArray1(3) '146

            'objCSM.BM_SubGL = CreateChartOfAccounts(Trim(txtSupplierName.Text), 3, objCSM.BM_GL, 4)
            sName = "Purchase Of Product " & objPurchase.Acc_Purchase_State
            txtGLID.Text = objPurchase.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
            If txtGLID.Text > 0 Then
                'iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Update")
            Else
                iChartID = CreateChartOfAccounts(Trim(sName), 2, iSubGroup, 3, "Save", Trim(sName))
            End If
            'Chart Of Accounts'

            Dim dtGSTRates As New DataTable
            dtGSTRates = objPurchase.BindGSTRates(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'

            If dtGSTRates.Rows.Count > 0 Then
                For x = 0 To dtGSTRates.Rows.Count - 1

                    sName = "Local GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objPurchase.Acc_Purchase_State & " Purchase Account"
                    txtGLID.Text = objPurchase.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iChartID, 3, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                    sName = "Inter State GST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objPurchase.Acc_Purchase_State & " Purchase Account"
                    txtGLID.Text = objPurchase.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
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

                    sName = "INPUT SGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objPurchase.Acc_Purchase_State & " Purchase Account"
                    txtGLID.Text = objPurchase.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "INPUT CGST " & dtGSTRates.Rows(x)("GST_GSTRate") / 2 & " % " & objPurchase.Acc_Purchase_State & " Purchase Account"
                    txtGLID.Text = objPurchase.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate") / 2)
                    End If

                    sName = "INPUT IGST " & dtGSTRates.Rows(x)("GST_GSTRate") & " % " & objPurchase.Acc_Purchase_State & " Purchase Account"
                    txtGLID.Text = objPurchase.GetGLID(sSession.AccessCode, sSession.AccessCodeID, Trim(sName))
                    If txtGLID.Text > 0 Then
                        'CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Update")
                    Else
                        CreateChartOfAccounts(Trim(sName), 3, iGL, 4, "Save", dtGSTRates.Rows(x)("GST_GSTRate"))
                    End If

                Next
            End If
            'Extra'

            objPurchase.dAcc_Purchase_PendingAmount = txtBillAmount.Text

            Arr = objPurchase.SavePurchaseVoucher(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
            iBillID = Arr(1)

            objPurchase.DeleteDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iBillID)

            For i = 0 To dgItems.Items.Count - 1
                objPur.PD_MasterID = iBillID

                objPur.PD_Commodity = dgItems.Items(i).Cells(0).Text
                objPur.PD_Goods = dgItems.Items(i).Cells(1).Text
                objPur.PD_Unit = dgItems.Items(i).Cells(2).Text
                objPur.PD_HSNCode = dgItems.Items(i).Cells(3).Text

                objPur.PD_Rate = dgItems.Items(i).Cells(7).Text
                objPur.PD_Quantity = dgItems.Items(i).Cells(8).Text

                objPur.PD_RateAmount = dgItems.Items(i).Cells(9).Text

                If dgItems.Items(i).Cells(10).Text <> "" Then
                    objPur.PD_Discount = dgItems.Items(i).Cells(10).Text
                Else
                    objPur.PD_Discount = 0
                End If

                If dgItems.Items(i).Cells(11).Text <> "" Then
                    objPur.PD_DiscountAmount = dgItems.Items(i).Cells(11).Text
                Else
                    objPur.PD_DiscountAmount = 0
                End If

                objPur.PD_ChargePerItem = dgItems.Items(i).Cells(12).Text

                objPur.PD_Amount = dgItems.Items(i).Cells(13).Text

                objPur.PD_GSTRate = dgItems.Items(i).Cells(14).Text

                If dgItems.Items(i).Cells(15).Text <> "" Then
                    objPur.PD_GSTAmount = dgItems.Items(i).Cells(15).Text
                Else
                    objPur.PD_GSTAmount = 0
                End If

                If objPurchase.Acc_Purchase_InvoiceStatus = "Local" Then
                    objPur.PD_SGST = objPur.PD_GSTRate / 2
                    objPur.PD_SGSTAmount = objPur.PD_GSTAmount / 2
                    objPur.PD_CGST = objPur.PD_GSTRate / 2
                    objPur.PD_CGSTAmount = objPur.PD_GSTAmount / 2
                    objPur.PD_IGST = 0
                    objPur.PD_IGSTAmount = 0
                ElseIf objPurchase.Acc_Purchase_InvoiceStatus = "Inter State" Then
                    objPur.PD_SGST = 0
                    objPur.PD_SGSTAmount = 0
                    objPur.PD_CGST = 0
                    objPur.PD_CGSTAmount = 0
                    objPur.PD_IGST = objPur.PD_GSTRate
                    objPur.PD_IGSTAmount = objPur.PD_GSTAmount
                End If

                If UCase(ddlGSTCategory.SelectedItem.Text) = "UNRIGISTERED DEALER" Then
                    Dim URD_GSTRate, URD_GSTAmt As Double

                    'URD_GSTRate = txtGST.Text
                    URD_GSTRate = objPurchase.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, objPur.PD_Commodity, objPur.PD_Goods)
                    URD_GSTAmt = (((objPur.PD_RateAmount - objPur.PD_DiscountAmount) + objPur.PD_ChargePerItem) * URD_GSTRate) / 100

                    objPur.PD_SGST = URD_GSTRate / 2
                    objPur.PD_SGSTAmount = URD_GSTAmt / 2
                    objPur.PD_CGST = URD_GSTRate / 2
                    objPur.PD_CGSTAmount = URD_GSTAmt / 2
                    objPur.PD_IGST = 0
                    objPur.PD_IGSTAmount = 0
                End If

                If dgItems.Items(i).Cells(16).Text <> "" Then
                    objPur.PD_FinalTotal = dgItems.Items(i).Cells(16).Text
                Else
                    objPur.PD_FinalTotal = 0
                End If

                objPur.PD_Status = "W"
                objPur.PD_CompID = sSession.AccessCodeID
                objPur.PD_CreatedBy = sSession.UserID
                objPur.PD_CreatedOn = System.DateTime.Now
                objPur.PD_Operation = "C"
                objPur.PD_IPAddress = sSession.IPAddress
                objPur.PD_YearID = sSession.YearID
                If dgItems.Items(i).Cells(18).Text <> "" Then
                    objPur.PD_FETotalAmt = objGen.SafeSQL(dgItems.Items(i).Cells(18).Text)
                Else
                    objPur.PD_FETotalAmt = 0
                End If
                If ddlCurrency.SelectedIndex > 0 Then
                    objPur.PD_Currency = ddlCurrency.SelectedValue
                Else
                    objPur.PD_Currency = 0
                End If
                Arr = objPurchase.SaveAcceptedDetails(sSession.AccessCode, objPur)
            Next

            SaveCharges(iBillID)

            LoadExistingPurchaseVoucher()
            ddlExistPurchase.SelectedValue = iBillID
            ddlExistPurchase_SelectedIndexChanged(sender, e)

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Submission."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Submission','', 'success');", True)
            End If
            lblStatus.Text = "Waiting for Submission"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
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
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim iMasterID As Integer
        Dim dTable1 As New DataTable
        Try
            If lblStatus.Text = "Submitted" Then
                lblError.Text = "This transaction has been submitted, it can not be submitted again."
                Exit Sub
            ElseIf lblStatus.Text = "Activated" Then
                lblError.Text = "This transaction has been Approved, it can not be submitted again."
                Exit Sub
            End If
            If ddlExistPurchase.SelectedIndex > 0 Then
                iMasterID = ddlExistPurchase.SelectedValue
            Else
                iMasterID = 0
            End If

            If iMasterID > 0 Then
                objPurchase.ApproveTransaction(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
                '**** qty Stock Addition ****'
                dTable1 = objPurchase.GetItemDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
                objPurchase.SaveStockLedger(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.IPAddress, sSession.UserID, dTable1, 0, 0, ddlBranch.SelectedValue)
                '**** qty Stock Addition ****'
                GetPurchasedItemsGrid(iMasterID)
                SavePurchaseJE(iMasterID)

                lblStatus.Text = "Submitted"
                lblError.Text = "Submitted Successfully."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Submitted Successfully','', 'success');", True)
            Else
                lblError.Text = "Select Existing Purchase Transaction/Create new transaction to Submit."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
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

            'iParty = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            iParty = ddlParty.SelectedValue

            sTypeOfBill = objDb.SQLGetDescription(sSession.AccessCode, "Select Acc_Purchase_InvoiceStatus From Acc_Purchase_Master Where Acc_Purchase_ID=" & iInvoiceID & " And Acc_Purchase_CompID=" & sSession.AccessCodeID & " And Acc_Purchase_Year=" & sSession.YearID & " ")
            sState = objDb.SQLGetDescription(sSession.AccessCode, "Select Acc_Purchase_State From Acc_Purchase_Master Where Acc_Purchase_ID=" & iInvoiceID & " And Acc_Purchase_CompID=" & sSession.AccessCodeID & " And Acc_Purchase_Year=" & sSession.YearID & " ")

            sGSTCategory = objDb.SQLGetDescription(sSession.AccessCode, "Select GC_GSTCategory From GSTCategory_Table Where GC_ID in (Select Acc_Purchase_GSTNCategory From Acc_Purchase_Master Where Acc_Purchase_ID=" & iInvoiceID & " And Acc_Purchase_CompID=" & sSession.AccessCodeID & " And Acc_Purchase_Year=" & sSession.YearID & ")")

            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & sSession.AccessCodeID & " "
            dtGSTRates = objDb.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)
            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'

            If dtGSTRates.Rows.Count > 0 Then
                For k = 0 To dtGSTRates.Rows.Count - 1

                    dt1 = objDb.SQLExecuteDataSet(sSession.AccessCode, "Select * From ACC_Purchase_Details Where PD_GSTRate=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And PD_MasterID=" & iInvoiceID & " And PD_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For z = 0 To dt1.Rows.Count - 1
                            dTotalAmt = dTotalAmt + dt1.Rows(z)("PD_Amount")
                            dSGSTAmt = dSGSTAmt + dt1.Rows(z)("PD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dt1.Rows(z)("PD_CGSTAmount")
                            dIGSTAmt = dIGSTAmt + dt1.Rows(z)("PD_IGSTAmount")
                            dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("PD_FinalTotal"))
                        Next

                        If UCase(sGSTCategory) = "UNRIGISTERED DEALER" Or UCase(sGSTCategory) = "COMPOSITION DEALER" Then
                            dSGSTAmt = 0
                            dCGSTAmt = 0
                            dIGSTAmt = 0
                        End If

                        dRow = dt.NewRow 'Item Name
                        dRow("Id") = 0
                        dRow("HeadID") = objPurchase.GetCOAHeadID(sSession.AccessCode, sSession.AccessCodeID, "Purchase Of Product " & sState)
                        dRow("GLID") = objPurchase.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Purchase Of Product " & sState)
                        If sTypeOfBill = "Local" Then
                            dRow("SubGLID") = objPurchase.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Local GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
                        ElseIf sTypeOfBill = "Inter State" Then
                            dRow("SubGLID") = objPurchase.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Inter State GST " & dtGSTRates.Rows(k)("GST_GSTRate") & " % " & sState & " Purchase Account")
                        End If
                        dRow("PaymentID") = 5
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "Purchase Of Material"

                        sGL = objPurchase.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objPurchase.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
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
                        dRow("HeadID") = objPurchase.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objPurchase.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objPurchase.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input SGST " & SGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 6
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "SGST"

                        sGL = objPurchase.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objPurchase.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
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
                        dRow("HeadID") = objPurchase.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objPurchase.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objPurchase.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input CGST " & CGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 7
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "CGST"

                        sGL = objPurchase.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objPurchase.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
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
                        dRow("HeadID") = objPurchase.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_Head")
                        dRow("GLID") = objPurchase.GetAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST", "Acc_GL")
                        dRow("SubGLID") = objPurchase.GetCOAID(sSession.AccessCode, sSession.AccessCodeID, "Input IGST " & IGST & " % " & sState & " Purchase Account")
                        dRow("PaymentID") = 8
                        dRow("SrNo") = dt.Rows.Count + 1
                        dRow("Type") = "IGST"

                        sGL = objPurchase.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                        If sGL <> "" Then
                            sArray = sGL.Split("-")
                            dRow("GLCode") = sArray(0)
                            dRow("GLDescription") = sArray(1)
                        End If

                        sSubGL = objPurchase.GetSubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), dRow("SubGLID"))
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
                dRow("HeadID") = objPurchase.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_Head")
                dRow("GLID") = objPurchase.GetPartyAccountDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier", "Acc_GL")
                dRow("SubGLID") = objPurchase.GetPartySubGLID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
                dRow("PaymentID") = 9
                dRow("SrNo") = dt.Rows.Count + 1
                dRow("Type") = "Party/Customer"

                sGL = objPurchase.GetGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("HeadID"), dRow("GLID"))
                If sGL <> "" Then
                    sArray = sGL.Split("-")
                    dRow("GLCode") = sArray(0)
                    dRow("GLDescription") = sArray(1)
                End If

                sSubGL = objPurchase.GetPartySubGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dRow("GLID"), Val(iParty), "S")
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetPurchasedItemsGrid")
        End Try
    End Sub
    Private Sub SavePurchaseJE(ByVal iMasterID As Integer)
        Dim iPaymentType As Integer = 0, iTransID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim Arr() As String
        Dim objJE As New ClsPurchaseSalesJE
        Try
            'objVerification.iAcc_JE_ID = 0
            'objVerification.sAcc_JE_TransactionNo = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, "P")
            'objVerification.iAcc_JE_Party = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            'objVerification.iAcc_JE_Location = 0
            'objVerification.iAcc_JE_BillType = 0

            'objVerification.iAcc_JE_InvoiceID = iPVID
            'objVerification.sAcc_JE_BillNo = sBillNo

            'objVerification.dAcc_JE_BillDate = txtReceiptDate.Text

            'objVerification.dAcc_JE_BillAmount = txtBillAmount.Text
            'objVerification.iAcc_JE_YearID = sSession.YearID
            'objVerification.sAcc_JE_Status = "W"
            'objVerification.iAcc_JE_CreatedBy = sSession.UserID
            'objVerification.iAcc_JE_CreatedOn = DateTime.Today
            'objVerification.sAcc_JE_Operation = "C"
            'objVerification.sAcc_JE_IPAddress = sSession.IPAddress
            'objVerification.dAcc_JE_BillCreatedDate = DateTime.Today
            'objVerification.sAcc_JE_AdvanceNaration = ""
            'objVerification.sAcc_JE_PaymentNarration = ""
            'objVerification.sAcc_JE_ChequeNo = ""
            'objVerification.sAcc_JE_IFSCCode = ""
            'objVerification.sAcc_JE_BankName = ""
            'objVerification.sAcc_JE_BranchName = ""

            'objVerification.iAcc_JE_UpdatedBy = sSession.UserID
            'objVerification.iAcc_JE_UpdatedOn = DateTime.Today
            'objVerification.iAcc_JE_CompID = sSession.AccessCodeID
            'Arr = objVerification.SavePurchaseJournalMaster(sSession.AccessCode, objVerification)
            'iTransID = Arr(1)

            For i = 0 To dgJEDetails.Items.Count - 1

                objPurchase.iATD_TrType = 8 'Purchase voucher

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objPurchase.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objPurchase.iATD_ID = 0
                End If

                objPurchase.dATD_TransactionDate = DateTime.Today

                objPurchase.iATD_BillID = iMasterID
                objPurchase.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
                'iPaymentType

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objPurchase.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objPurchase.iATD_Head = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objPurchase.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objPurchase.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objPurchase.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objPurchase.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objPurchase.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objPurchase.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objPurchase.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objPurchase.dATD_Credit = 0
                End If

                If objPurchase.dATD_Debit > 0 And objPurchase.dATD_Credit = 0 Then
                    objPurchase.iATD_DbOrCr = 1 'Debit
                ElseIf objPurchase.dATD_Debit = 0 And objPurchase.dATD_Credit > 0 Then
                    objPurchase.iATD_DbOrCr = 2 'Credit
                End If

                objPurchase.iATD_CreatedBy = sSession.UserID
                objPurchase.dATD_CreatedOn = DateTime.Today

                objPurchase.sATD_Status = "A"
                objPurchase.iATD_YearID = sSession.YearID
                objPurchase.sATD_Operation = "C"
                objPurchase.sATD_IPAddress = sSession.IPAddress

                objPurchase.iATD_UpdatedBy = sSession.UserID
                objPurchase.dATD_UpdatedOn = DateTime.Today

                objPurchase.iATD_CompID = sSession.AccessCodeID

                objPurchase.iATD_ZoneID = ddlAccZone.SelectedValue
                objPurchase.iATD_RegionID = ddlAccRgn.SelectedValue
                objPurchase.iATD_AreaID = ddlAccArea.SelectedValue
                objPurchase.iATD_BranchID = ddlAccBrnch.SelectedValue

                objPurchase.dATD_OpenDebit = "0.00"
                objPurchase.dATD_OpenCredit = "0.00"
                objPurchase.dATD_ClosingDebit = "0.00"
                objPurchase.dATD_ClosingCredit = "0.00"
                objPurchase.iATD_SeqReferenceNum = 0

                objPurchase.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objPurchase)

            Next

            lblError.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

            dgJEDetails.DataSource = objPurchase.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID, txtTransactionNo.Text)
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SavePurchaseJE")
        End Try
    End Sub
    Public Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objPurchase.LoadGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "Select Commodity")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub loadDescitionStart()
        Try
            chkCategory.DataSource = objPurchase.LoadDescritionStart(sSession.AccessCode, sSession.AccessCodeID)
            chkCategory.DataTextField = "Inv_Code"
            chkCategory.DataValueField = "Inv_ID"
            chkCategory.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            txtRate.Enabled = True
            If ddlCommodity.SelectedIndex > 0 Then
                chkCategory.DataSource = objPurchase.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
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

    Private Sub chkCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkCategory.SelectedIndexChanged
        Dim altPices As Integer
        Try
            lblError.Text = ""
            txtFreight.Text = 0 : txtFreightAmount.Text = 0
            If (chkCategory.SelectedValue > 0) Then
                ddlCommodity.SelectedValue = objPurchase.GetBrandValue(sSession.AccessCode, sSession.AccessCodeID, chkCategory.SelectedValue)
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
            If ddlParty.SelectedIndex > 0 Then
                lblDescID.Text = chkCategory.SelectedValue
                LoadDesciptionDetails()
                altPices = objPurchase.GetAlterNatePiceValue(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                txtPices.Text = altPices
                ddlUnit.SelectedValue = objPurchase.GetUnitsValue(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
                hfTotalPieces.Value = txtPices.Text
                txtHSNCode.Text = objPurchase.GetGSTRatesDetails(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text)
            Else
                For i = 0 To chkCategory.Items.Count - 1
                    chkCategory.Items(i).Selected = False
                Next
                ddlUnit.Items.Clear() : txtHSNCode.Text = ""
            End If

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
            txtRate.Text = "" : txtRateAmount.Text = ""
            txtQuantity.Text = "" : txtDiscount.Text = "" : txtDiscountAmount.Text = ""
            txtGSTRate.Text = "" : txtGSTAmount.Text = ""
            txtTotalAmount.Text = ""

            If lblDescID.Text <> "0" Then
                iGSTRate = objPurchase.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                If iGSTRate > 0 Then
                Else
                    lblError.Text = "Enter the HSN Details in Inventory Master."
                    Exit Sub
                End If

                'If txtOrderDate.Text <> "" Then
                'dOrderDate = Date.ParseExact(Trim(txtOrderDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                '    dt = objPO.CheckDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text, dOrderDate)
                '    If dt.Rows.Count = 0 Then
                '        lblError.Text = "Enter Details in Inventory Master Details"
                '        Exit Sub
                '    End If
                'If dt.Rows.Count > 1 Then
                '        ddlRate.DataSource = dt
                '        ddlRate.DataTextField = "INVH_PreDeterminedPrice"
                '        ddlRate.DataValueField = "InvH_ID"
                '        ddlRate.DataBind()
                '        ddlRate.Enabled = True : txtRate.Enabled = False
                '        txtHistoryID.Text = ddlRate.SelectedValue
                '        sArray = ddlRate.SelectedItem.Text.Split("-")
                '        txtRate.Text = sArray(0)
                '        'LoadExciseUsingDate()

                '        GetOtherDetails(txtHistoryID.Text)
                '    Else
                '        sArray = dt.Rows(0)(1).ToString().Split("-")
                '        txtRate.Text = sArray(0)
                '        txtHistoryID.Text = dt.Rows(0)(0).ToString()

                '        'LoadExciseUsingDate()
                '        ddlRate.Enabled = False : txtRate.Enabled = True
                '        If txtHistoryID.Text <> "" Then
                '            ' GetPurchaseDetails(txtHistoryID.Text)
                '            GetOtherDetails(txtHistoryID.Text)
                '        End If
                '    End If

                ddlUnit.DataSource = objPurchase.LoadUnitOFMeasurement(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                ddlUnit.DataTextField = "Mas_Desc"
                ddlUnit.DataValueField = "Mas_ID"
                ddlUnit.DataBind()
                ddlUnit.Items.Insert(0, "--- Unit of Measurement ---")

                txtGSTID.Text = 0
                txtGSTRate.Text = 0

                Dim sGSTRate As String = ""
                sGSTRate = objPurchase.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                If sGSTRate <> "HSN" Then
                    txtGSTID.Text = 0
                    'txtGSTRate.Text = objPO.getGSTRate(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyType.SelectedItem.Text, ddlGSTCategory.SelectedItem.Text)
                    txtGSTRate.Text = 0
                Else
                    txtGSTID.Text = objPurchase.GetGSTID(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                    txtGSTRate.Text = objPurchase.GetGSTRateFromHSNTable(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue, chkCategory.SelectedValue)
                End If
                'End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDesciptionDetails")
        End Try
    End Sub
    Private Sub btnItemAdd_Click(sender As Object, e As EventArgs) Handles btnItemAdd.Click
        Dim dtDetails As New DataTable
        Dim dr As DataRow
        Dim dRateAmt, dDisAmt, dTotal, dGSTAmt, dNetAmt, dCharge, dItemTotal, dTotalAmt As Double
        Dim iBaseID As Integer = 0
        Try
            lblError.Text = ""
            dtPurchase = BuildTable()

            If IsNothing(Session("dtPurchase")) = False Then
                dtPurchase = Session("dtPurchase")
            End If

            dr = dtPurchase.NewRow
            dr("CommodityID") = ddlCommodity.SelectedValue
            dr("GoodsID") = chkCategory.SelectedValue
            dr("UnitID") = ddlUnit.SelectedValue

            dr("HSNCode") = txtHSNCode.Text
            dr("Commodity") = ddlCommodity.SelectedItem.Text
            dr("Goods") = chkCategory.SelectedItem.Text
            dr("Unit") = ddlUnit.SelectedItem.Text
            dr("MRP") = txtRate.Text
            dr("Qty") = txtQuantity.Text
            dr("RateAMt") = txtRateAmount.Text
            If txtDiscount.Text <> "" Then
                dr("Discount") = txtDiscount.Text
                dr("DiscountAmt") = txtDiscountAmount.Text
            Else
                dr("Discount") = 0
                dr("DiscountAmt") = 0
                txtDiscount.Text = 0
                txtDiscountAmount.Text = 0
            End If


            'If txtOtherCharge.Text <> "" Then
            '    If dtPurchase.Rows.Count > 0 Then
            '        For i = 0 To dtPurchase.Rows.Count - 1
            '            dItemTotal = dItemTotal + dtPurchase.Rows(i)("RateAMt")
            '        Next
            '        dCharge = (txtTotalAmt.Text * txtOtherCharge.Text) / (dItemTotal + txtTotalAmt.Text)
            '        dr("Charge") = dCharge
            '    Else
            '        dCharge = (txtTotalAmt.Text * txtOtherCharge.Text) / (txtTotalAmt.Text)
            '        dr("Charge") = dCharge
            '    End If
            'Else
            dCharge = 0
            dr("Charge") = 0
            'End If
            dr("Total") = txtRateAmount.Text - txtDiscountAmount.Text
            dr("GST") = txtGSTRate.Text
            dr("GSTAmt") = txtGSTAmount.Text
            dr("NetAmount") = txtTotalAmount.Text
            If ddlCurrency.SelectedIndex > 0 Then
                dr("CCurrency") = ddlCurrency.SelectedItem.Text
            Else
                dr("CCurrency") = 0
            End If
            iBaseID = objPurchase.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
            If txtTotalAmount.Text <> "" Then
                If ddlCurrency.SelectedValue = iBaseID Then
                    dr("FETotalAmount") = objGen.SafeSQL(txtTotalAmount.Text)
                Else
                    dr("FETotalAmount") = objPurchase.GetFERates(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue, objGen.SafeSQL(txtTotalAmount.Text))
                End If
            Else
                dr("FETotalAmount") = 0
            End If
            dtPurchase.Rows.Add(dr)

            dgItems.DataSource = dtPurchase
            dgItems.DataBind()

            Session("dtPurchase") = dtPurchase

            For i = 0 To dgItems.Items.Count - 1
                dTotalAmt = dTotalAmt + Convert.ToDouble(dgItems.Items(i).Cells(16).Text)
            Next
            txtBillAmount.Text = Convert.ToDecimal(Convert.ToDouble(dTotalAmt)).ToString("#,##0.00")

            txtHSNCode.Text = "" : chkCategory.ClearSelection() : ddlUnit.Items.Clear() : txtRate.Text = "" : txtQuantity.Text = "" : txtRateAmount.Text = ""
            txtDiscount.Text = "" : txtDiscountAmount.Text = "" : txtGSTRate.Text = "" : txtGSTAmount.Text = "" : txtTotalAmount.Text = ""

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnItemAdd_Click")
        End Try
    End Sub
    Private Sub lnkbtnCreateCommodity_Click(sender As Object, e As EventArgs) Handles lnkbtnCreateCommodity.Click
        Try
            Response.Redirect(String.Format("~/Masters/InventoryMaster.aspx?"), False)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub CalculateAlterQty(ByVal dAlterQty As Double, ByVal sUnit As String)
        Dim sBasicAmount As Double
        Dim sTotal As Double : Dim dDiscount As Double
        Dim dAmountOnCalculate As Double
        Try
            If sUnit = "A" Then
                txtRateAmount.Text = dAlterQty * txtQuantity.Text * txtRate.Text
                txtTotalAmount.Text = dAlterQty * txtQuantity.Text * txtRate.Text
            ElseIf sUnit = "U" Then
                txtRateAmount.Text = txtQuantity.Text * txtRate.Text
                txtTotalAmount.Text = txtQuantity.Text * txtRate.Text
            End If

            txtDiscountAmount.Text = 0

            If txtQuantity.Text <> "" And txtDiscount.Text <> "" Then
                txtDiscountAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal((txtRateAmount.Text * txtDiscount.Text) / 100))
                hfDiscountAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal((txtRateAmount.Text * txtDiscount.Text) / 100))
            End If

            If txtOtherCharge.Text > 0 Then
                sTotal = txtRateAmount.Text
                dDiscount = txtDiscountAmount.Text

                'lblCharges.Text = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal) * dChargeAmount) / dItemsTotalFromDispatch))
                'HFCharges.Value = String.Format("{0:0.00}", Convert.ToDecimal(((sTotal) * dChargeAmount) / dItemsTotalFromDispatch))
            Else
                'lblCharges.Text = 0
                'HFCharges.Value = 0
            End If

            Dim dItemChargeAmt As Double
            sTotal = txtRateAmount.Text
            dDiscount = txtDiscountAmount.Text
            'dItemChargeAmt = lblCharges.Text
            dItemChargeAmt = 0

            dAmountOnCalculate = String.Format("{0:0.00}", Convert.ToDecimal((sTotal - dDiscount) + dItemChargeAmt))
            'lblTotalAmount.Text = dAmountOnCalculate
            'HFTotalAmount.Value = dAmountOnCalculate

            If txtGSTRate.Text <> "" Then

                txtGSTAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * txtGSTRate.Text) / 100))
                hfGSTAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(((dAmountOnCalculate) * txtGSTRate.Text) / 100))

                txtTotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + txtGSTAmount.Text))
                hfTotalAmount.Value = String.Format("{0:0.00}", Convert.ToDecimal(dAmountOnCalculate + txtGSTAmount.Text))

            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CalculateGST")
        End Try
    End Sub
    Private Sub ddlUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnit.SelectedIndexChanged
        Dim iINVH_Unit As Integer
        Try
            If ddlUnit.SelectedIndex > 0 Then

                'If txtOtherCharge.Text <> "" Then
                '    If dtPurchase.Rows.Count > 0 Then
                '        For i = 0 To dtPurchase.Rows.Count - 1
                '            dItemTotal = dItemTotal + dtPurchase.Rows(i)("RateAMt")
                '        Next
                '        dCharge = (txtTotalAmt.Text * txtOtherCharge.Text) / (dItemTotal + txtTotalAmt.Text)
                '        dr("Charge") = dCharge
                '    Else
                '        dCharge = (txtTotalAmt.Text * txtOtherCharge.Text) / (txtTotalAmt.Text)
                '        dr("Charge") = dCharge
                '    End If
                'Else
                '    dCharge = 0
                '    dr("Charge") = 0
                'End If
                iINVH_Unit = objPurchase.GetINVH_Unit(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, lblDescID.Text)
                If ddlUnit.SelectedValue = iINVH_Unit Then
                    CalculateAlterQty(txtPices.Text, "A")
                Else
                    CalculateAlterQty(txtPices.Text, "U")
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim iParent As Integer
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                ddlBranch.SelectedValue = ddlAccBrnch.SelectedValue
                ddlBranch_SelectedIndexChanged(sender, e)

                If ddlAccBrnch.SelectedIndex > 0 Then
                    iParent = objPurchase.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                    ddlAccArea.SelectedValue = iParent
                End If
                If ddlAccArea.SelectedIndex > 0 Then
                    iParent = objPurchase.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccArea.SelectedValue)
                    ddlAccRgn.SelectedValue = iParent
                End If
                If ddlAccRgn.SelectedIndex > 0 Then
                    iParent = objPurchase.getOrgParent(sSession.AccessCode, sSession.AccessCodeID, ddlAccRgn.SelectedValue)
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

    Private Sub GvCharge_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles GvCharge.ItemDataBound
        Try
            GvCharge.Columns(3).Visible = False
            If sPTSave = "YES" Then
                GvCharge.Columns(3).Visible = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCurrency.SelectedIndexChanged
        Dim iBaseID As Integer, iCurrID As Integer
        Try
            lblError.Text = ""
            If ddlCurrency.SelectedIndex > 0 Then
                iBaseID = objPurchase.GetBaseCurrency(sSession.AccessCode, sSession.AccessCodeID)
                If ddlCurrency.SelectedValue = iBaseID Then
                Else
                    iCurrID = objPurchase.GetFEID(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
                    If iCurrID = 0 Then
                        lblError.Text = "Please set the exchange rates in Currency Master."
                        lblCustomerValidationMsg.Text = "Please set the exchange rates in Currency Master."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
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
