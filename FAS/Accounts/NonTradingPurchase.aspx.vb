Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Windows.Forms
Imports DatabaseLayer
Partial Class Accounts_NonTradingPurchase
    Inherits System.Web.UI.Page
    Private sFormName As String = "Accounts_PurchaseTransaction"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared iDbOrCr As Integer = 0
    Dim objPurchase As New ClsNonTradingPurchase
    Private objAccSetting As New clsAccountSetting
    Dim objDb As New DBHelper
    Private Shared sUMBackStatus As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAddCharge.ImageUrl = "~/Images/Add16.png"
        imgbtnApprove.ImageUrl = "~/Images/CheckMark24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnSubmit.ImageUrl = "~/Images/Submit24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim taxcategory As String
        Dim dt As New DataTable
        Dim sMasterID As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                Session("ChargesMaster") = Nothing
                divcollapseChequeDetails.Visible = False
                BindExistingPurchase() : LoadZone() : BindBankName() : LoadChargeType()
                LoadSupplier() : BindBranch() : BindCompanyType() : BindPaymentType()
                txtTransactionNo.Text = objPurchase.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)

                RFVParty.InitialValue = "Select Supplier" : RFVParty.ErrorMessage = "Select Supplier"
                RFVAccZone.InitialValue = "Select Zone" : RFVAccZone.ErrorMessage = "Select Zone."
                RFVAccRgn.InitialValue = "Select Region" : RFVAccRgn.ErrorMessage = "Select Region."
                RFVAccArea.InitialValue = "Select Area" : RFVAccArea.ErrorMessage = "Select Area."
                RFVAccBrnch.InitialValue = "Select Branch" : RFVAccBrnch.ErrorMessage = "Select Branch."
                REVTransactionDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REVTransactionDate.ErrorMessage = "Enter Valid Date Format."
                REVBillDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REVBillDate.ErrorMessage = "Enter Valid Date Format."
                RFvddlPaymentType.InitialValue = "Select Payment Type" : RFvddlPaymentType.ErrorMessage = "Select Payment Type"

                RFVEChequeNo.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                RFVEChequeNo.ErrorMessage = "Enter Valid Cheque No."

                REFChequeDate.ValidationExpression = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                REFChequeDate.ErrorMessage = "Enter Valid cheque Date."

                dt = objPurchase.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("CUST_COMM_Address")) = True Or IsDBNull(dt.Rows(0)("CUST_ProvisionalNo")) = True Then
                        lblError.Text = "Fill the details in Company Master"
                        Exit Sub
                    End If
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    txtReceiveAddress.Text = txtCompanyAddress.Text
                    txtReceiveGSTNRegNo.Text = txtCompanyGSTNRegNo.Text

                    taxcategory = objPurchase.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("CUST_TAXPayableCategory"))
                    If UCase(taxcategory) = UCase("UNRIGISTERED DEALER") Then
                        txtReceiveGSTNRegNo.Enabled = False
                    Else
                        txtReceiveGSTNRegNo.Enabled = True
                    End If
                End If

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
    Public Sub BindBankName()
        Try
            ddlBankName.DataSource = objPurchase.LoadBankNames(sSession.AccessCode, sSession.AccessCodeID)
            ddlBankName.DataTextField = "GL_Desc"
            ddlBankName.DataValueField = "GL_Id"
            ddlBankName.DataBind()
            ddlBankName.Items.Insert(0, "Select Bank Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindPaymentType()
        Try
            ddlPaymentType.Items.Insert(0, "Select Payment Type")
            ddlPaymentType.Items.Insert(1, "Cash")
            ddlPaymentType.Items.Insert(2, "Bank")
            ddlPaymentType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindExistingPurchase()
        Try
            ddlExistPurchase.DataSource = objPurchase.LoadExistingPurchases(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistPurchase.DataTextField = "Acc_Purchase_TransactionNo"
            ddlExistPurchase.DataValueField = "Acc_Purchase_ID"
            ddlExistPurchase.DataBind()
            ddlExistPurchase.Items.Insert(0, "Select Existing Purchase")
        Catch ex As Exception
            Throw
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
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "Select Zone")
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
            ddlAccRgn.Items.Insert(0, "Select Region")
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
            ddlAccArea.Items.Insert(0, "Select Area")
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
            ddlAccBrnch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadSupplier()
        Try
            ddlParty.DataSource = objPurchase.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "CSM_Name"
            ddlParty.DataValueField = "CSM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Supplier")
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
    Private Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dtCustomer As New DataTable
        Dim description As String
        Try
            If ddlParty.SelectedIndex > 0 Then
                dtCustomer = objPurchase.GetCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
                If dtCustomer.Rows.Count > 0 Then
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
            Else
                ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.Items.Clear()

                txtBillingAddress.Text = "" : txtBillingGSTNRegNo.Text = ""
                txtDeliveryFromAddress.Text = "" : txtDeliveryFromGSTNRegNo.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlAccBrnch.SelectedIndex > 0 Then
                dt = objPurchase.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAccBrnch.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUSTB_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUSTB_GSTNRegNo")

                    txtReceiveAddress.Text = txtCompanyAddress.Text
                    txtReceiveGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
                End If
            Else
                dt = objPurchase.GetCompanyGSTNRegNo(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    txtCompanyAddress.Text = dt.Rows(0)("CUST_COMM_Address")
                    txtCompanyGSTNRegNo.Text = dt.Rows(0)("CUST_ProvisionalNo")

                    txtReceiveAddress.Text = txtCompanyAddress.Text
                    txtReceiveGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
                End If
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
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim i As Integer = 0
        Dim iBillID As Integer = 0
        Dim iHead, iGroup, iSubGroup, iGL, iChartID As Integer
        Dim sPerm As String = ""
        Dim sArray1 As Array
        Dim sName As String = ""
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim objPur As New ClsNonTradingPurchase.NonTradingDetails
        Try
            lblError.Text = ""
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
            dSDate = Date.ParseExact(txtTransactionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Transaction Date (" & txtTransactionDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtTransactionDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtTransactionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Transaction Date (" & txtTransactionDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtTransactionDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            'Cheque Date Comparision'
            dDate = Date.ParseExact(sSession.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & sSession.StartDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtBillDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(sSession.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Bill Date (" & txtBillDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & sSession.EndDate & ")."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                txtBillDate.Focus()
                Exit Sub
            End If
            'Cheque Date Comparision'

            If ddlExistPurchase.SelectedIndex > 0 Then
                objPurchase.iAcc_Purchase_ID = ddlExistPurchase.SelectedValue
            Else
                objPurchase.iAcc_Purchase_ID = 0
            End If
            objPurchase.sAcc_Purchase_TransactionNo = txtTransactionNo.Text
            objPurchase.dAcc_Purchase_TransactionDate = Date.ParseExact(txtTransactionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If ddlParty.SelectedIndex > 0 Then
                objPurchase.iAcc_Purchase_Party = ddlParty.SelectedValue
            Else
                objPurchase.iAcc_Purchase_Party = 0
            End If
            objPurchase.sAcc_Purchase_BillNo = txtBillNo.Text
            objPurchase.dAcc_Purchase_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objPurchase.dAcc_Purchase_BillAmount = txtBillAmount.Text
            objPurchase.iAcc_Purchase_CreatedBy = sSession.UserID
            objPurchase.iAcc_Purchase_Year = sSession.YearID
            objPurchase.iAcc_Purchase_CompID = sSession.AccessCodeID
            objPurchase.sAcc_Purchase_Status = "C"
            objPurchase.sAcc_Purchase_DelFlag = "W"
            objPurchase.sAcc_Purchase_Operation = "C"
            objPurchase.sAcc_Purchase_IPAddress = sSession.IPAddress
            objPurchase.iAcc_Purchase_PaymentType = ddlPaymentType.SelectedIndex

            If txtOtherCharge.Text = "" Then
                objPurchase.dAcc_Purchase_OtherCharges = "0"
            Else
                objPurchase.dAcc_Purchase_OtherCharges = txtOtherCharge.Text
            End If

            objPurchase.iACC_Purchase_ZoneID = ddlAccZone.SelectedValue
            objPurchase.iACC_Purchase_RegionID = ddlAccRgn.SelectedValue
            objPurchase.iACC_Purchase_AreaID = ddlAccArea.SelectedValue
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

            If ddlCompanyType.SelectedIndex > 0 Then
                objPurchase.Acc_Purchase_CompanyType = ddlCompanyType.SelectedValue
            Else
                objPurchase.Acc_Purchase_CompanyType = 0
            End If

            If ddlGSTCategory.SelectedIndex > 0 Then
                objPurchase.Acc_Purchase_GSTNCategory = ddlGSTCategory.SelectedValue
            Else
                objPurchase.Acc_Purchase_GSTNCategory = 0
            End If

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
                iChartID = txtGLID.Text
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

            If ddlPaymentType.SelectedIndex = 2 Then
                objPurchase.sAcc_Purchase_ChequeNo = txtChequeNo.Text

                If txtChequeDate.Text = "" Then
                    objPurchase.dAcc_Purchase_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Else
                    objPurchase.dAcc_Purchase_ChequeDate = Date.ParseExact(txtChequeDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                End If

                If txtIFSC.Text <> "" Then
                    objPurchase.sAcc_Purchase_IFSCCode = txtIFSC.Text
                Else
                    objPurchase.sAcc_Purchase_IFSCCode = ""
                End If

                If ddlBankName.SelectedIndex > 0 Then
                    objPurchase.sAcc_Purchase_BankName = ddlBankName.SelectedValue
                Else
                    objPurchase.sAcc_Purchase_BankName = 0
                End If

                If txtBranchName.Text <> "" Then
                    objPurchase.sAcc_Purchase_BranchName = txtBranchName.Text
                Else
                    objPurchase.sAcc_Purchase_BranchName = ""
                End If
            Else
                objPurchase.sAcc_Purchase_ChequeNo = ""
                objPurchase.dAcc_Purchase_ChequeDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objPurchase.sAcc_Purchase_IFSCCode = ""
                objPurchase.sAcc_Purchase_BankName = 0
                objPurchase.sAcc_Purchase_BranchName = ""
            End If

            Arr = objPurchase.SavePurchaseVoucher(sSession.AccessCode, sSession.AccessCodeID, objPurchase)
            iBillID = Arr(1)

            objPur.iAcc_P_ID = 0
            objPur.iAcc_P_MasterID = iBillID
            objPur.sAcc_P_BillNo = txtBillNo.Text
            objPur.dAcc_P_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If txtDis.Text <> "" Then
                objPur.dAcc_P_Discount = txtDis.Text
            Else
                objPur.dAcc_P_Discount = 0
            End If
            If txtDiscountAmt.Text <> "" Then
                objPur.dAcc_P_DiscountAmt = txtDiscountAmt.Text
            Else
                objPur.dAcc_P_DiscountAmt = 0
            End If
            If txtCharges.Text <> "" Then
                objPur.dAcc_P_OtherCharges = txtCharges.Text
            Else
                objPur.dAcc_P_OtherCharges = 0
            End If

            If txtGST.Text <> "" Then
                objPur.dAcc_P_GST = txtGST.Text
            Else
                objPur.dAcc_P_GST = 0
            End If
            If txtGSTAmt.Text <> "" Then
                objPur.dAcc_P_GSTAmt = txtGSTAmt.Text
            Else
                objPur.dAcc_P_GSTAmt = 0
            End If

            If txtSGST.Text <> "" Then
                objPur.dAcc_P_SGST = txtSGST.Text
            Else
                objPur.dAcc_P_SGST = 0
            End If
            If txtSGSTAmt.Text <> "" Then
                objPur.dAcc_P_SGSTAmt = txtSGSTAmt.Text
            Else
                objPur.dAcc_P_SGSTAmt = 0
            End If

            If txtCGST.Text <> "" Then
                objPur.dAcc_P_CGST = txtCGST.Text
            Else
                objPur.dAcc_P_CGST = 0
            End If
            If txtCGSTAmt.Text <> "" Then
                objPur.dAcc_P_CGSTAmt = txtCGSTAmt.Text
            Else
                objPur.dAcc_P_CGSTAmt = 0
            End If

            If txtIGST.Text <> "" Then
                objPur.dAcc_P_IGST = txtIGST.Text
            Else
                objPur.dAcc_P_IGST = 0
            End If
            If txtIGSTAmt.Text <> "" Then
                objPur.dAcc_P_IGSTAmt = txtIGSTAmt.Text
            Else
                objPur.dAcc_P_IGSTAmt = 0
            End If

            objPur.dAcc_P_BillAmount = txtBillAmount.Text

            objPur.dAcc_P_Amount = (txtBillAmount.Text - objPur.dAcc_P_GSTAmt)

            objPur.iAcc_P_Year = sSession.YearID
            objPur.iAcc_P_CompID = sSession.AccessCodeID
            objPur.sAcc_P_Status = "C"
            objPur.sAcc_P_DelFlag = "W"
            objPur.dAcc_P_PendingAmount = txtBillAmount.Text
            objPur.iAcc_P_CrBy = sSession.UserID
            objPur.dAcc_P_CrOn = Date.Today
            objPur.sAcc_P_Operation = "C"
            objPur.sAcc_P_IPAddress = sSession.IPAddress

            Arr = objPurchase.SavePurchaseDetails(sSession.AccessCode, sSession.AccessCodeID, objPur)
            If Arr(0) = 3 Then
                lblError.Text = "Successfully Saved."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved.','', 'success');", True)
            ElseIf Arr(0) = 2 Then
                lblError.Text = "Successfully Updated."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
            End If
            SaveCharges(iBillID)
            BindExistingPurchase()
            ddlExistPurchase.SelectedValue = iBillID
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDetailsSetttings")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CreateChartOfAccounts")
        End Try
    End Function
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            ClearAll()
            txtTransactionNo.Text = objPurchase.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Public Sub ClearAll()
        Try
            lblError.Text = "" : lblStatus.Text = "" : ddlExistPurchase.SelectedIndex = 0 : txtTransactionNo.Text = "" : txtTransactionDate.Text = ""
            ddlParty.SelectedIndex = 0 : ddlCompanyType.SelectedIndex = 0 : ddlGSTCategory.Items.Clear() : ddlPaymentType.SelectedIndex = 0
            ddlChargeType.SelectedIndex = 0 : txtShippingRate.Text = "" : txtBillingAddress.Text = "" : txtBillingGSTNRegNo.Text = ""
            txtDeliveryFromAddress.Text = "" : txtDeliveryFromGSTNRegNo.Text = "" : txtReceiveAddress.Text = txtCompanyAddress.Text : txtReceiveGSTNRegNo.Text = txtCompanyGSTNRegNo.Text
            txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = "" : txtDis.Text = "" : txtDiscountAmt.Text = "" : txtCharges.Text = ""
            txtGST.Text = "" : txtGSTAmt.Text = "" : txtSGST.Text = "" : txtSGSTAmt.Text = "" : txtCGST.Text = "" : txtCGSTAmt.Text = "" : txtIGST.Text = "" : txtIGSTAmt.Text = ""
            txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFSC.Text = "" : ddlBankName.SelectedIndex = 0 : txtBranchName.Text = ""

            Session("ChargesMaster") = Nothing
            GvCharge.DataSource = Nothing
            GvCharge.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExistPurchase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistPurchase.SelectedIndexChanged
        Dim dtNew As New DataTable
        Dim dtDetails As New DataTable
        Dim dtCharge As New DataTable
        Try
            lblError.Text = ""
            If ddlExistPurchase.SelectedIndex > 0 Then
                dtNew = objPurchase.GetNonTradingMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPurchase.SelectedValue)
                If dtNew.Rows.Count > 0 Then
                    If IsDBNull(dtNew.Rows(0)("Acc_Purchase_TransactionNo").ToString()) = False Then
                        txtTransactionNo.Text = dtNew.Rows(0)("Acc_Purchase_TransactionNo").ToString()
                    Else
                        txtTransactionNo.Text = ""
                    End If

                    If IsDBNull(dtNew.Rows(0)("Acc_Purchase_Party").ToString()) = False Then
                        If dtNew.Rows(0)("Acc_Purchase_Party") > 0 Then
                            ddlParty.SelectedValue = dtNew.Rows(0)("Acc_Purchase_Party").ToString()
                        Else
                            ddlParty.SelectedIndex = 0
                        End If
                    Else
                        ddlParty.SelectedIndex = 0
                    End If

                    If IsDBNull(dtNew.Rows(0)("Acc_Purchase_PaymentType").ToString()) = False Then
                        ddlPaymentType.SelectedIndex = dtNew.Rows(0)("Acc_Purchase_PaymentType").ToString()
                    Else
                        ddlPaymentType.SelectedIndex = 0
                    End If

                    If ddlPaymentType.SelectedIndex = 2 Then    'Bank
                        divcollapseChequeDetails.Visible = True
                    Else
                        divcollapseChequeDetails.Visible = False
                    End If

                    'Bank Details'
                    If IsDBNull(dtNew.Rows(0)("ACC_Purchase_ChequeNo").ToString()) = False Then
                        txtChequeNo.Text = dtNew.Rows(0)("ACC_Purchase_ChequeNo").ToString()
                    Else
                        txtChequeNo.Text = ""
                    End If

                    If IsDBNull(dtNew.Rows(0)("Acc_Purchase_ChequeDate").ToString()) = False Then
                        If (dtNew.Rows(0)("Acc_Purchase_ChequeDate").ToString() <> "") Then
                            If (dtNew.Rows(0)("Acc_Purchase_ChequeDate").ToString() <> "01/01/1990 12:00:00 AM") Then
                                txtChequeDate.Text = objGen.FormatDtForRDBMS(dtNew.Rows(0)("Acc_Purchase_ChequeDate").ToString(), "D")
                            Else
                                txtChequeDate.Text = ""
                            End If
                        Else
                            txtChequeDate.Text = ""
                        End If
                    Else
                        txtChequeDate.Text = ""
                    End If

                    If IsDBNull(dtNew.Rows(0)("Acc_Purchase_IFSCCode").ToString()) = False Then
                        txtIFSC.Text = dtNew.Rows(0)("Acc_Purchase_IFSCCode").ToString()
                    Else
                        txtIFSC.Text = ""
                    End If

                    If IsDBNull(dtNew.Rows(0)("ACC_Purchase_BankName").ToString()) = False Then
                        If dtNew.Rows(0)("ACC_Purchase_BankName").ToString() = "" Then
                            BindBankName()
                            ddlBankName.SelectedIndex = 0
                        Else
                            If dtNew.Rows(0)("ACC_Purchase_BankName").ToString() > 0 Then
                                ddlBankName.SelectedValue = dtNew.Rows(0)("ACC_Purchase_BankName").ToString()
                            Else
                                BindBankName()
                                ddlBankName.SelectedIndex = 0
                            End If
                        End If
                    Else
                        ddlBankName.SelectedIndex = 0
                    End If

                    If IsDBNull(dtNew.Rows(0)("Acc_Purchase_BranchName").ToString()) = False Then
                        txtBranchName.Text = dtNew.Rows(0)("Acc_Purchase_BranchName").ToString()
                    Else
                        txtBranchName.Text = ""
                    End If
                    'Bank Details'

                    If IsDBNull(dtNew.Rows(0)("Acc_Purchase_TransactionDate").ToString()) = False Then
                        txtTransactionDate.Text = objGen.FormatDtForRDBMS(dtNew.Rows(0)("Acc_Purchase_TransactionDate").ToString(), "D")
                    Else
                        txtTransactionDate.Text = ""
                    End If

                    If IsDBNull(dtNew.Rows(0)("Acc_Purchase_OtherCharges").ToString()) = False Then
                        txtOtherCharge.Text = Convert.ToDecimal(dtNew.Rows(0)("Acc_Purchase_OtherCharges").ToString()).ToString("#,##0.00")
                    Else
                        txtOtherCharge.Text = 0
                    End If

                    If dtNew.Rows(0)("Acc_Purchase_DelFlag").ToString() = "W" Then
                        lblStatus.Text = "Waiting for Submission"
                    ElseIf dtNew.Rows(0)("Acc_Purchase_DelFlag").ToString() = "D" Then
                        lblStatus.Text = "De-Activated"
                    ElseIf dtNew.Rows(0)("Acc_Purchase_DelFlag").ToString() = "A" Then
                        lblStatus.Text = "Activated"
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
                        If dtNew.Rows(0)("ACC_Purchase_CompanyType") > 0 Then
                            ddlCompanyType.SelectedValue = dtNew.Rows(0)("ACC_Purchase_CompanyType")
                        Else
                            ddlCompanyType.SelectedIndex = 0
                        End If
                    Else
                        ddlCompanyType.SelectedIndex = 0
                    End If
                    If IsDBNull(dtNew.Rows(0)("ACC_Purchase_GSTNCategory")) = False Then
                        If dtNew.Rows(0)("ACC_Purchase_GSTNCategory") > 0 Then
                            BindGSTNCategory(ddlCompanyType.SelectedItem.Text)
                            ddlGSTCategory.SelectedValue = dtNew.Rows(0)("ACC_Purchase_GSTNCategory")
                        Else
                            ddlGSTCategory.SelectedIndex = 0
                        End If
                    Else
                        ddlGSTCategory.SelectedIndex = 0
                    End If

                    Dim description As String
                    If ddlGSTCategory.SelectedIndex > 0 Then
                        description = objPurchase.GetGSTDescription(sSession.AccessCode, sSession.AccessCodeID, ddlGSTCategory.SelectedValue)
                        If UCase(description) = UCase("UNRIGISTERED DEALER") Then
                            txtDeliveryFromGSTNRegNo.Enabled = False
                        Else
                            txtDeliveryFromGSTNRegNo.Enabled = True
                        End If
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

                    dtDetails = objPurchase.GetDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPurchase.SelectedValue)
                    If dtDetails.Rows.Count > 0 Then
                        txtBillNo.Text = dtDetails.Rows(0)("Acc_P_BillNo")
                        txtBillDate.Text = objGen.FormatDtForRDBMS(dtDetails.Rows(0)("Acc_P_BillDate").ToString(), "D")
                        txtDis.Text = dtDetails.Rows(0)("Acc_P_Discount")
                        txtDiscountAmt.Text = dtDetails.Rows(0)("Acc_P_DiscountAmt")
                        txtCharges.Text = dtDetails.Rows(0)("Acc_P_OtherCharges")
                        txtGST.Text = dtDetails.Rows(0)("Acc_P_GST")
                        txtGSTAmt.Text = dtDetails.Rows(0)("Acc_P_GSTAmt")
                        txtSGST.Text = dtDetails.Rows(0)("Acc_P_SGST")
                        txtSGSTAmt.Text = dtDetails.Rows(0)("Acc_P_SGSTAmt")
                        txtCGST.Text = dtDetails.Rows(0)("Acc_P_CGST")
                        txtCGSTAmt.Text = dtDetails.Rows(0)("Acc_P_CGSTAmt")
                        txtIGST.Text = dtDetails.Rows(0)("Acc_P_IGST")
                        txtIGSTAmt.Text = dtDetails.Rows(0)("Acc_P_IGSTAmt")
                        txtBillAmount.Text = Convert.ToDecimal(dtDetails.Rows(0)("Acc_P_BillAmount").ToString()).ToString("#,##0.00")
                    End If
                End If
                dtCharge = objPurchase.BindChargeData(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistPurchase.SelectedValue, 10)
                GvCharge.DataSource = dtCharge
                GvCharge.DataBind()
                Session("ChargesMaster") = dtCharge
            Else
                ClearAll()
                txtTransactionNo.Text = objPurchase.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistPurchase_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSubmit_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSubmit.Click
        Dim iMasterID As Integer
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
                objPurchase.SubmitTransaction(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSubmit_Click")
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

            'iParty = objVerification.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingTransactions.SelectedValue, sSession.YearID)
            iParty = ddlParty.SelectedValue

            sTypeOfBill = objDb.SQLGetDescription(sSession.AccessCode, "Select Acc_Purchase_InvoiceStatus From Acc_NonTrading_Purchase_Master Where Acc_Purchase_ID=" & iInvoiceID & " And Acc_Purchase_CompID=" & sSession.AccessCodeID & " And Acc_Purchase_Year=" & sSession.YearID & " ")
            sState = objDb.SQLGetDescription(sSession.AccessCode, "Select Acc_Purchase_State From Acc_NonTrading_Purchase_Master Where Acc_Purchase_ID=" & iInvoiceID & " And Acc_Purchase_CompID=" & sSession.AccessCodeID & " And Acc_Purchase_Year=" & sSession.YearID & " ")

            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & sSession.AccessCodeID & " "
            dtGSTRates = objDb.SQLExecuteDataSet(sSession.AccessCode, sSql).Tables(0)
            'Extra'
            dtGSTRates.Rows.Add("0")
            'Extra'

            If dtGSTRates.Rows.Count > 0 Then
                For k = 0 To dtGSTRates.Rows.Count - 1

                    dt1 = objDb.SQLExecuteDataSet(sSession.AccessCode, "Select * From Acc_NonTrading_Purchase_Details Where Acc_P_GST=" & dtGSTRates.Rows(k)("GST_GSTRate") & " And Acc_P_MasterID=" & iInvoiceID & " And Acc_P_CompID=" & sSession.AccessCodeID & " ").Tables(0)
                    If dt1.Rows.Count > 0 Then
                        For z = 0 To dt1.Rows.Count - 1
                            dTotalAmt = dTotalAmt + dt1.Rows(z)("Acc_P_Amount")
                            dSGSTAmt = dSGSTAmt + dt1.Rows(z)("Acc_P_SGSTAmt")
                            dCGSTAmt = dCGSTAmt + dt1.Rows(z)("Acc_P_CGSTAmt")
                            dIGSTAmt = dIGSTAmt + dt1.Rows(z)("Acc_P_IGSTAmt")
                            dPartyTotal = dPartyTotal + Convert.ToDecimal(dt1.Rows(z)("Acc_P_BillAmount"))
                        Next

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
        Dim objJE As New ClsNonTradingPurchase.NonTradingDetails
        Try
            For i = 0 To dgJEDetails.Items.Count - 1

                objJE.iATD_TrType = 10 'Non Trading Purchase voucher

                If (IsDBNull(dgJEDetails.Items(i).Cells(0).Text) = False) And (dgJEDetails.Items(i).Cells(0).Text <> "&nbsp;") Then
                    objJE.iATD_ID = dgJEDetails.Items(i).Cells(0).Text
                Else
                    objJE.iATD_ID = 0
                End If

                objJE.dATD_TransactionDate = DateTime.Today

                objJE.iATD_BillID = iMasterID
                objJE.iATD_PaymentType = dgJEDetails.Items(i).Cells(4).Text
                'iPaymentType

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objJE.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objJE.iATD_Head = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objJE.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objJE.iATD_GL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                    objJE.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                Else
                    objJE.iATD_SubGL = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objJE.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objJE.dATD_Debit = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objJE.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                Else
                    objJE.dATD_Credit = 0
                End If

                If objJE.dATD_Debit > 0 And objJE.dATD_Credit = 0 Then
                    objJE.iATD_DbOrCr = 1 'Debit
                ElseIf objJE.dATD_Debit = 0 And objJE.dATD_Credit > 0 Then
                    objJE.iATD_DbOrCr = 2 'Credit
                End If

                objJE.iATD_CreatedBy = sSession.UserID
                objJE.dATD_CreatedOn = DateTime.Today

                objJE.sATD_Status = "A"
                objJE.iATD_YearID = sSession.YearID
                objJE.sATD_Operation = "C"
                objJE.sATD_IPAddress = sSession.IPAddress

                objJE.iATD_UpdatedBy = sSession.UserID
                objJE.dATD_UpdatedOn = DateTime.Today

                objJE.iATD_CompID = sSession.AccessCodeID
                objPurchase.SaveTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, objJE)

            Next

            dgJEDetails.DataSource = objPurchase.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID, txtTransactionNo.Text)
            dgJEDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SavePurchaseJE")
        End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim iPSID As Integer
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlExistPurchase.SelectedIndex > 0 Then
                If txtTransactionNo.Text.StartsWith("P") Then
                    iPSID = 0
                Else
                    iPSID = 1
                End If
                sStatus = objPurchase.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistPurchase.SelectedValue, sSession.YearID)
                If sStatus = "A" Then
                    lblError.Text = "This Transaction is already Approved."
                    Exit Sub
                ElseIf sStatus = "S" Then
                    objPurchase.UpdatePurchaseVoucherStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistPurchase.SelectedValue, "W", sSession.UserID, sSession.IPAddress)
                    objPurchase.CheckLedgerTable(sSession.AccessCode, sSession.AccessCodeID, ddlExistPurchase.SelectedValue, sSession.YearID, sSession.UserID, sSession.IPAddress)
                    lblError.Text = "Successfully Approved."
                    lblStatus.Text = "Approved"
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
                Else
                    lblError.Text = "This Transaction is not Submitted."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This Transaction is not Submitted','', 'success');", True)
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Existing JE to Approve."
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect("~/Accounts/NonTradingPurchaseDashBoard.aspx")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub ddlPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentType.SelectedIndexChanged
        Try
            If ddlPaymentType.SelectedIndex = 1 Then  'Cash
                divcollapseChequeDetails.Visible = False
            ElseIf ddlPaymentType.SelectedIndex = 2 Then  'Bank
                divcollapseChequeDetails.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPaymentType_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub SaveCharges(ByVal iTRID As Integer)
        Dim Arr() As String
        Try
            'Deleting charges Everytime & Saving'
            objPurchase.DeleteCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTRID, 10)
            'Deleting charges Everytime & Saving'

            'Charges Saving'
            If GvCharge.Items.Count > 0 Then
                For i = 0 To GvCharge.Items.Count - 1
                    objPurchase.C_ID = 0
                    objPurchase.C_TRID = iTRID
                    objPurchase.C_TRType = 10    'Non Trading Purchase Voucher
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
            txtCharges.Text = dTotalCharges
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
            txtCharges.Text = dTotalCharges
            'txtPaidAmount.Text = dTotalCharges
            dTotalCharges = 0

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvCharge_ItemCommand")
        End Try
    End Sub
End Class
